using System;
using System.Collections.Generic;
using System.Text;

/*SQLite数据库相关*/
using System.Data.SQLite;

namespace PasswdLock
{
    class Sqlite
    {
        private string m_strPath;

        private SQLiteConnection oSQLiteConnection;

        public const string TABLE_NAME_LOGIN = "Login";//登陆表 

        public const string TABLE_NAME_LOGIN_ONLINE = "LoginOnline";//登陆在线表

        public const string TABLE_NAME_PASSWORD = "Password";//密码表

        public const string DBNAME = "date.db";//数据库名

        private Sqlite()
        {
            m_strPath = System.Environment.CurrentDirectory;//获取进程系统路径

            string strDbPath = m_strPath + "\\" + DBNAME;//数据库路径
            string connectString = "Data Source=" + strDbPath + ";Pooling=true;FailIfMissing=false;";

            oSQLiteConnection = new SQLiteConnection(connectString);
            try
            {
                oSQLiteConnection.Open();//打开数据库
            }
            catch (Exception ex)
            {

            }


            /*检测是否存在Login表，若不存在则创建*/
            if (false == isLogin())
            {
                createLogin();
            }

            /*检测是否存在LoginOnline表，若不存在则创建*/
            if (false == isLoginOnline())
            {
                createLoginOnline();
            }

            /*检测是否存在Password表，若不存在则创建*/
            if (false == isPassword())
            {
                createPassword();
            }


        }


        private static Sqlite oSqlite;

        /*单例类*/
        public static Sqlite instance()
        {
            if (oSqlite == null)
            {
                oSqlite = new Sqlite();
            }
            return oSqlite;
        }



        /*一下为对数据库表的一些操作方法*/




        /*Login表*/

        /*判断是否存在Login表*/
        private bool isLogin()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "SELECT COUNT(*) FROM sqlite_master where type=\'table\' and name=\'" + TABLE_NAME_LOGIN + "\';";
            if (0 == Convert.ToInt32(oSQLiteCommand.ExecuteScalar()))
            {
                return false;//不存在该表
                CLog.instance().write("SQLite there is no Login Table!");
            }
            else
            {
                return true;
                CLog.instance().write("SQLite there is Login Table!");
            }
        }


        /*创建Login表*/
        public void createLogin()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "create table " + TABLE_NAME_LOGIN + " (Session char(30) null primary key,DevCode char(30) null ,LoginTime char(30) null,LogoutTime char(30) null);";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite createLogin succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite createLogin failed!");
            }
        }

        
        /*写Login表并记录登陆时间*/
        public void writeLogin(string strSession, string strDevCode, string strLoginTime)
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "insert into " + TABLE_NAME_LOGIN + " (Session,DevCode,LoginTime) values(\"" + strSession + "\",\"" + strDevCode + "\",\"" + strLoginTime + "\");";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite writeLogin succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite writeLogin failed!");
            }
        }

        /*修改Login表相应数据，登出时写入登出时间*/
        public void modifyLogin(string strSession)
        {
            /*获取本地时间*/
            string strTime = DateTime.Now.ToString();

            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "update " + TABLE_NAME_LOGIN + " set LogoutTime = \'" + strTime + "\' where Session = \'" + strSession + "\';";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite modifyLogin succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite modifyLogin failed!");
            }
        }




        /*LoginOnline表*/


        /*判断是否存在LoginOnline表*/
        private bool isLoginOnline()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "SELECT COUNT(*) FROM sqlite_master where type=\'table\' and name=\'" + TABLE_NAME_LOGIN_ONLINE + "\';";
            if (0 == Convert.ToInt32(oSQLiteCommand.ExecuteScalar()))
            {
                return false;//不存在该表
                CLog.instance().write("SQLite there is no LoginOnline Table!");
            }
            else
            {
                return true;
                CLog.instance().write("SQLite there is LoginOnline Table!");
            }
        }

        /*创建LoginOnline表*/
        public void createLoginOnline()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "create table " + TABLE_NAME_LOGIN_ONLINE + " (Session char(30) null primary key,keepAliveTime char(30) null);";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite createLoginOnline succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite createLoginOnline failed!");
            }
        }


        /*写LoginOnline表，登陆时写*/
        public void writeLoginOnline(string strSession)
        {
            /*获取本地时间*/
            string strTime = DateTime.Now.ToString();

            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "insert into " + TABLE_NAME_LOGIN_ONLINE + " (Session,keepAliveTime) values(\"" + strSession + "\",\"" + strTime + "\");";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite writeLoginOnline succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite writeLoginOnline failed!");
            }
        }

        /*删除LoginOnline表相应数据，登出时清除*/
        public void delLoginOnline(string strSession)
        {

            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "delete from " + TABLE_NAME_LOGIN_ONLINE + " where Session = \'" + strSession + "\';";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite delLoginOnline succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite delLoginOnline failed!");
            }
        }

        /*修改LoginOnline表相应数据，保活时使用*/
        public void modifyLoginOnline(string strSession)
        {
            /*获取本地时间*/
            string strTime = DateTime.Now.ToString();


            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "update " + TABLE_NAME_LOGIN_ONLINE + " set keepAliveTime = \'" + strTime + "\' where Session = \'" + strSession + "\';";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite modifyLoginOnline succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite modifyLoginOnline failed!");
            }
        }


        /*检测某个用户session对应的用户是否在线，在线返回0，失败或者不在线返回1*/
        public bool isSessionOnline(string strSession)
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "select * from " + TABLE_NAME_LOGIN_ONLINE + " where Session=\'" + strSession + "\'";

            try
            {
                System.Data.SQLite.SQLiteDataReader reader = oSQLiteCommand.ExecuteReader();
                CLog.instance().write("SQLite isSessionOnline succesed!");
                while (reader.Read())
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite isSessionOnline failed!");
                return false;
            }

            return false;
        }


        /*查询LoginOnline表中所有值*/
        public Dictionary<string,string> queryLoginOnlineList()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "select Session,keepAliveTime from " + TABLE_NAME_LOGIN_ONLINE + ";";

            Dictionary<string, string> oDictionary = new Dictionary<string, string>();

            try
            {
                System.Data.SQLite.SQLiteDataReader reader = oSQLiteCommand.ExecuteReader();
                CLog.instance().write("SQLite queryLoginOnlineList succesed!");
                while (reader.Read())
                {
                    oDictionary.Add(reader.GetString(0), reader.GetString(1));
                }
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite queryLoginOnlineList failed!");
                return oDictionary;
            }

            return oDictionary;
        }






        /*Password表*/


        /*判断是否存在Password表*/
        private bool isPassword()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "SELECT COUNT(*) FROM sqlite_master where type=\'table\' and name=\'" + TABLE_NAME_PASSWORD + "\';";
            if (0 == Convert.ToInt32(oSQLiteCommand.ExecuteScalar()))
            {
                return false;//不存在该表
                CLog.instance().write("SQLite there is no Password Table!");
            }
            else
            {
                return true;
                CLog.instance().write("SQLite there is Password Table!");
            }
        }

        /*创建Password表*/
        public void createPassword()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "create table " + TABLE_NAME_PASSWORD + " (LockType int null primary key,Password char(30) null);";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite createPassword succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite createPassword failed!");
            }
        }


        /*查询密码*/
        public Dictionary<string,string> queryPasswdList()
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "select * from " + TABLE_NAME_PASSWORD + ";";

            Dictionary<string, string> oDictionary = new Dictionary<string, string>();

            try
            {
                System.Data.SQLite.SQLiteDataReader reader = oSQLiteCommand.ExecuteReader();
                CLog.instance().write("SQLite queryPasswdList succesed!");
                while (reader.Read())
                {
                    oDictionary[reader.GetInt32(0).ToString()] = reader.GetString(1);    
                }
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite queryPasswdList failed!");
                return oDictionary;
            }

            return oDictionary;
        }


        /*删除密码*/
        public void delPasswd(int iLockType)
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "delete from " + TABLE_NAME_PASSWORD + " where LockType=" + iLockType.ToString() + ";";

            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite delPasswd succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite delPasswd failed!");
                return;
            }

            return;
        }


        /*写入密码*/
        public void writePassword(string strPasswd,int iLockType)
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "insert into " + TABLE_NAME_PASSWORD + " (Password,LockType) values(\"" + strPasswd + "\"," + iLockType.ToString() + " );";
            try
            {
                oSQLiteCommand.ExecuteScalar();
                CLog.instance().write("SQLite writePassword succesed!");
            }
            catch (Exception ex)
            {
                CLog.instance().write("SQLite writePassword failed!");
            }
        }

    }
}
