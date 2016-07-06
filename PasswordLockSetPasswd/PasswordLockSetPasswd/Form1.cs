using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace PasswordLockSetPasswd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            openSQLite oopenSQLite = new openSQLite();
            oopenSQLite.ShowDialog();
        }

        /*MD5加密算法*/
        private string MD5passwd(string strPasswd)
        {
            byte[] result = Encoding.Default.GetBytes(strPasswd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_addPasswd_Click(object sender, EventArgs e)
        {
            Sql.Sqlite.instance().writePassword(MD5passwd(textBox_passwd.Text));
        }
    }
}
