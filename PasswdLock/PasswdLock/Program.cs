using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.IO;
using Microsoft.Win32;

/*Thrift相关*/
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

/*线程相关*/
using System.Threading;

/*IO相关*/
using System.IO.Ports;



namespace PasswdLock
{
    class Program
    {
        const int THRIFT_MAIN_SERVER_PORT = 7911;//thrift服务端口号    

        static void Main(string[] args)
        {
            init();   
        }

        static private void initLog()
        {
            string strPath = System.Environment.CurrentDirectory;
            try
            {
                if (Directory.Exists(strPath + "\\Log"))
                {
                    //CLog.instance().write("存在Log文件夹");
                }
                else
                {
                    Directory.CreateDirectory(strPath + "\\Log");
                    CLog.instance().write("不存在Log文件夹");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log文件夹不存在，会导致日志异常，请处理！");    
            }
        }

        /*串口类对象*/
        static private SerialPort serialPort1 = new SerialPort();

        /*初始化串口设备*/
        static private void initCom()
        {
            /*从文件中读取用户名*/
            StreamReader oStreamReader = new StreamReader((System.Environment.CurrentDirectory + "\\" + "COM.txt"));

            /*初始化串口*/
            serialPort1.PortName = oStreamReader.ReadToEnd();
            serialPort1.BaudRate = 9600;
            serialPort1.Open(); 
        }

        /*清理线程*/
        static private void funcThread()
        {
            /*每隔10分钟(600秒)清理一次*/
            for (int i = 0; i < 600; i++)
            {
                Thread.Sleep(1000);
            }

            Dictionary<string, string> oDictionary = new Dictionary<string, string>();
            oDictionary = Sqlite.instance().queryLoginOnlineList();

            /*获取本地时间*/
            DateTime TimeNow = DateTime.Now;

            foreach (string session in oDictionary.Keys)
            {
                DateTime oDateTime = DateTime.Parse(oDictionary[session]);
                TimeSpan ts1 = new TimeSpan(oDateTime.Ticks);
                TimeSpan ts2 = new TimeSpan(TimeNow.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                int iMilliseconds = ts.Minutes * 60 + ts.Milliseconds;
                if (("0" != ts.Days.ToString()) || ("0" != ts.Hours.ToString()) || (iMilliseconds > 90))
                {
                    /*保活时间大于90s的代表客户端程序可能已崩溃，直接清理数据库*/
                    Sqlite.instance().delLoginOnline(session);
                }
            }

        }

        static private void init()
        {
            initLog();//初始化Log文件夹

            Sqlite.instance();//初始化Sqlite数据库

            initCom();//初始化串口设备


            TServerSocket serverTransport = new TServerSocket(THRIFT_MAIN_SERVER_PORT, 0, false);
            passwordLock.Processor processor = new passwordLock.Processor(new CThrift());
            //TServer server = new TSimpleServer(processor, serverTransport);
            TThreadedServer server = new TThreadedServer(processor, serverTransport);//使用多线程方法启动thrift服务


            Console.WriteLine("*********************************************************************");
            Console.WriteLine("");
            Console.WriteLine("本程序为thrift服务程序！");
            Console.WriteLine("作者：曹阳");
            Console.WriteLine("thrift框架搭建日期：2015年3月31日");
            Console.WriteLine("本设计为毕业设计\"无线遥控密码锁\"课题服务端windows平台应用程序");
            Console.WriteLine("源码以C#语言为基础，运行于.net2.0及以上平台");
            Console.WriteLine("");
            Console.WriteLine("*********************************************************************");

            Console.WriteLine("");
            CLog.instance().write("thrift Server Begin！");
            Console.WriteLine("");

            /*启动线程清理*/
            Thread oThread = new Thread(new ThreadStart(funcThread));
            oThread.Start();
            
            /*thrift服务启动*/
            server.Serve();

            
        }



        /*Thrift实现类*/
        public class CThrift : passwordLock.Iface
        {

            public CThrift()
            {
                for (int i = 1; i < 9; i++)
                {
                    oDictionaryNumErrPasswd[i] = 0;
                }
            }

            /*连接服务器*/
            public string connectServer(string strDevCode)
            {
                Random rad = new Random();//实例化随机数产生器rad；
                int value = rad.Next(0, 999999);//用rad生成大于等于0，小于等于999999的随机数；

                string strValue = value.ToString();

                while (strValue.Length < 6)//当产生的随机数长度不足6的时候用0补全
                {
                    strValue = "0" + strValue;
                }

                string strSession = DateTime.Now.ToString("yyyyMMddHHmmssms") + strValue;//本地时间加上随机数，保证session唯一性

                try
                {
                    /*记录在Login表中*/
                    string strLoginTime = DateTime.Now.ToString();
                    Sqlite.instance().writeLogin(strSession, strDevCode, strLoginTime);

                    /*记录在LoginOnline表中*/
                    Sqlite.instance().writeLoginOnline(strSession);
                }
                catch 
                {
                    AirException ex = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "连接失败！", "连接失败！");
                    throw (ex);
                    return "";
                }

                CLog.instance().write("connectServer success,session is " + strSession);
                return strSession;
            }

            /*与服务器断开连接*/
            public void disconnectServer(string Session)
            {
                
                Sqlite.instance().delLoginOnline(Session);

                Sqlite.instance().modifyLogin(Session);

                return;
            }


            /*保活*/
            public void keepAlive(string Session)
            {
                /*检测该session是否在线*/
                bool bIsOnline = Sqlite.instance().isSessionOnline(Session);
                if (!bIsOnline)
                {
                    CLog.instance().write("no online session!");
                    return;
                }

                Sqlite.instance().modifyLoginOnline(Session);

                return;
            }

            private Dictionary<int, int> oDictionaryNumErrPasswd = new Dictionary<int, int>();

            /*输入密码*/
            public void inputPasswd(string Session, int iLockType, string strPassword)
            {
                /*检测该session是否在线*/
                bool bIsOnline = Sqlite.instance().isSessionOnline(Session);
                if (!bIsOnline)
                {
                    CLog.instance().write("no online session!");
                    //出错处理
                    AirException exOnline = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "保活失败！", "保活失败！");
                    throw (exOnline);
                }


                /*获取数据库中保存的加密密码*/
                Dictionary<string, string> oDictionary = Sqlite.instance().queryPasswdList();

                try
                {
                    if (oDictionary[iLockType.ToString()] == strPassword)
                    {
                        //成功！
                        serialPort1.Write(iLockType.ToString());
                        oDictionaryNumErrPasswd[iLockType] = 0;
                        serialPort1.Write("10");
                        return;
                    }
                    else
                    {
                        oDictionaryNumErrPasswd[iLockType]++;
                        if (3 == oDictionaryNumErrPasswd[iLockType])
                        {
                            oDictionaryNumErrPasswd[iLockType] = 0;
                            serialPort1.Write("9");
                        }
                    }
                }
                catch(Exception ex)
                {
                    AirException ex2 = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "该锁尚未设置密码！", "该锁尚未设置密码！");
                    throw (ex2);   
                }

                //密码错误
                AirException ex3 = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "输入密码有误，请重新输入！", "输入密码有误，请重新输入！");
                throw (ex3);
                
  
            }

            /*重置密码*/
            public void ResetPasswd(string strDevCode, int iLockType, string strPassword)
            {
                try
                {
                    /*删除密码数据*/
                    Sqlite.instance().delPasswd(iLockType);

                    /*写入新密码*/
                    Sqlite.instance().writePassword(strPassword, iLockType);
                }
                catch
                {
                    AirException ex = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "执行失败！", "执行失败！");
                    throw (ex);
                }
                return;    
            }


            /*修改密码*/
            public void modifyPasswd(string strOldPasswd, string strNewPasswd, int iLockType)
            {
                /*获取数据库中保存的加密密码*/
                Dictionary<string, string> oDictionary = Sqlite.instance().queryPasswdList();

                if (oDictionary[iLockType.ToString()] == strOldPasswd)
                {
                    //成功！

                    try
                    {
                        /*删除密码数据*/
                        Sqlite.instance().delPasswd(iLockType);

                        /*写入新密码*/
                        Sqlite.instance().writePassword(strNewPasswd, iLockType);
                    }
                    catch
                    {
                        AirException ex = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "执行失败！", "执行失败！");
                        throw (ex);
                    }

                    return;
                }

                AirException ex2 = new AirException(AirExceptionType.PASSWD_EXCEPTION, 1, "密码错误！", "密码错误！");
                throw (ex2);

            }

        }
    }
}
