using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Security.Cryptography;

namespace PasswordLockClient
{
    public partial class modifyPasswdForm : Form
    {
        public modifyPasswdForm()
        {
            InitializeComponent();
        }

        /*MD5加密算法*/
        private string MD5passwd(string strPasswd)
        {
            byte[] result = Encoding.Default.GetBytes(strPasswd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        private void button_modifyPasswd_Click(object sender, EventArgs e)
        {
            int iLockType = 0;
            if (true == radioButton1.Checked)
            {
                iLockType = 1;
            }
            else if (true == radioButton2.Checked)
            {
                iLockType = 2;
            }
            else if (true == radioButton3.Checked)
            {
                iLockType = 3;
            }
            else if (true == radioButton4.Checked)
            {
                iLockType = 4;
            }
            else if (true == radioButton5.Checked)
            {
                iLockType = 5;
            }
            else if (true == radioButton6.Checked)
            {
                iLockType = 6;
            }
            else if (true == radioButton7.Checked)
            {
                iLockType = 7;
            }
            else if (true == radioButton8.Checked)
            {
                iLockType = 8;
            }

            if (0 == iLockType)
            {
                MessageBox.Show("请选择锁类型");
                return;
            }

            if (("" == textBox_oldPasswd.Text) || ("" == textBox_newPasswd.Text) || ("" == textBox_ReNewPasswd.Text))
            {
                MessageBox.Show("请输入密码");
                return;
            }

            if (textBox_newPasswd.Text != textBox_ReNewPasswd.Text)
            {
                MessageBox.Show("新密码输入不一致，请重新输入！");
                textBox_newPasswd.Text = "";
                textBox_ReNewPasswd.Text = "";
                return;
            }

            /*修改密码*/
            ThriftClient.instance().modifyPasswd(MD5passwd(textBox_oldPasswd.Text), MD5passwd(textBox_newPasswd.Text), iLockType);

        }
    }
}
