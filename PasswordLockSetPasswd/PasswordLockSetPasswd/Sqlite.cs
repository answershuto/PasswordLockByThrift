using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace Sql
{


    class Sqlite
    {
        private const string TABLE_NAME_PASSWORD = "Password";

        private string m_strPath;

        private SQLiteConnection oSQLiteConnection;

        private Sqlite()
        {
           
        }

        public int openSqliteFlie(string strFilePath)
        {
            string connectString = "Data Source=" + strFilePath + ";Pooling=true;FailIfMissing=false;";

            oSQLiteConnection = new SQLiteConnection(connectString);

            try
            {
                oSQLiteConnection.Open();//打开数据库
            }
            catch (Exception ex)
            {
                return 1;
            }

            return 0;
        }

        private static Sqlite oSqlite;

        public static Sqlite instance()
        {
            if (oSqlite == null)
            {
                oSqlite = new Sqlite();
            }
            return oSqlite;
        }



        /*在Password表中添加新密码*/
        public void writePassword(string strPasswd)
        {
            SQLiteCommand oSQLiteCommand = oSQLiteConnection.CreateCommand();
            oSQLiteCommand.CommandText = "insert into " + TABLE_NAME_PASSWORD + " (Password) values(\"" + strPasswd + "\");";
            try
            {
                oSQLiteCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("写入密码失败！");
            }
        }

    }
}

        
