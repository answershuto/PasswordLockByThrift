using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

using System.Timers;

namespace PasswordLockClient
{
    class ThriftClient
    {
        passwordLock.Client client;

        private string IP = "";

        private int Port;

        private static Dictionary<int, int> oDictionaryErrPasswd = new Dictionary<int, int>();

        private static System.Timers.Timer time1 = new System.Timers.Timer(1000);

        private ThriftClient()//私有的构造函数创造单例类
        {
            oDictionaryErrPasswd[1] = 0;
            oDictionaryErrPasswd[2] = 0;
            oDictionaryErrPasswd[3] = 0;
            oDictionaryErrPasswd[4] = 0;
            oDictionaryErrPasswd[5] = 0;
            oDictionaryErrPasswd[6] = 0;
            oDictionaryErrPasswd[7] = 0;
            oDictionaryErrPasswd[8] = 0;

            time1.Elapsed += new System.Timers.ElapsedEventHandler(timeFunc);
            time1.AutoReset = true;
            time1.Enabled = false;
        }

        static int iNumErrPasswd = 0;
        private static void timeFunc(object sender, ElapsedEventArgs e)
        {
            if (iNumErrPasswd == 90)
            {
                iNumErrPasswd = 0;

                for (int i = 1; i < 9; i++)
                {
                    oDictionaryErrPasswd[i] = 0;
                }

                    time1.Enabled = false;
            }
            iNumErrPasswd++;


        }

        private static ThriftClient oThriftClient;//单例类

        public static ThriftClient instance()
        {
            if (null == oThriftClient)
            {
                oThriftClient = new ThriftClient();
            }

            return oThriftClient;
        }


        /*连接服务器获取session*/
        public string connectServer(string strIP, int iPort)
        {
            /*记录IP以及端口号*/
            IP = strIP;
            Port = iPort;

            TTransport transport = new TSocket(strIP, iPort);
            TProtocol Potocol = new TBinaryProtocol(transport);
            client = new passwordLock.Client(Potocol);
            transport.Open();

            string session = "";

            try
            {
                session = client.connectServer("devCode");
            }
            catch
            {
                return "";
            }

            return session;
        }

        /*与服务器断开连接*/
        public void disconnectServer(string Session)
        {

            try
            {
                client.disconnectServer(Session);
            }
            catch
            {
                return;
            }

            return;
        }


        /*保活*/
        public void keepAlive(string Session)
        {

            try
            {
                client.keepAlive(Session);
            }
            catch
            {
                return;
            }

            return;
        }

        /*输入密码*/
        public void inputPasswd(string Session, int iLockType, string strPasswd)
        {
            if (oDictionaryErrPasswd[iLockType] >= 3)
            {
                MessageBox.Show("你已输错密码超过三次，请于90秒后再试！");
                time1.Enabled = true;
                return;
            }

            try
            {
                client.inputPasswd(Session, iLockType, strPasswd);
                MessageBox.Show("解锁成功！");
                oDictionaryErrPasswd[iLockType] = 0;
            }
            catch (AirException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
                oDictionaryErrPasswd[iLockType]++;
                return;
            }

            return;
        }


        /*输入密码*/
        public void modifyPasswd(string strOldPasswd, string strNewPasswd, int iLockType)
        {

            try
            {
                client.modifyPasswd(strOldPasswd, strNewPasswd, iLockType);
                MessageBox.Show("密码修改成功！");
            }
            catch (AirException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
                return;
            }

            return;
        }



    }
}
