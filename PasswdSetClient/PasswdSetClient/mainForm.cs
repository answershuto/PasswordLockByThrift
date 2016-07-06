using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Server;
using Thrift.Transport;

using System.Security.Cryptography;

namespace PasswdSetClient
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        /*MD5加密算法*/
        private string MD5passwd(string strPasswd)
        {
            byte[] result = Encoding.Default.GetBytes(strPasswd);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");
        }

        private passwordLock.Client client;

        private bool bFlag = true;

        private void button_sure_Click(object sender, EventArgs e)
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

            if (bFlag)
            {
                TTransport transport = new TSocket(textBox_IP.Text, int.Parse(textBox_port.Text));
                TProtocol Potocol = new TBinaryProtocol(transport);
                client = new passwordLock.Client(Potocol);
                transport.Open();

                bFlag = false;
            }

            try
            {
                client.ResetPasswd("dev", iLockType, MD5passwd(textBox_newPasswd.Text));
                MessageBox.Show("重置密码成功！");
            }
            catch(AirException ex)
            {
                MessageBox.Show(ex.ErrorMessage);
                return;
            }
        }


    }
}
