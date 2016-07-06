using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace PasswdLock
{
    class CLog
    {
        private string m_strPath;//执行路径

        private static StreamWriter oLogError;//错误保存路径

        private static CLog oClog;
        
        private CLog()
        {
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            string strDay = DateTime.Now.Day.ToString();
            string strWriteLine = strYear + "-" + strMonth + "-" + strDay;

            m_strPath = System.Environment.CurrentDirectory;
            oLogError = new StreamWriter((m_strPath + "\\Log\\" + "LogError(" + strWriteLine + ").txt"), true);
        }

        ~CLog()
        {
            
        }

        public static CLog instance()
        {
            if (null == oClog)
            {
                oClog = new CLog();
            }
            return oClog;
        }

        public void write(string strLine)
        {
            string strTime = DateTime.Now.ToLongTimeString();
            string strYear = DateTime.Now.Year.ToString();
            string strMonth = DateTime.Now.Month.ToString();
            string strDay = DateTime.Now.Day.ToString();
            string strWriteLine = strYear + "-" + strMonth + "-" + strDay + " " + strTime + ": " + strLine;

            try
            {
                Console.WriteLine(strLine); 
                oLogError.WriteLine(strWriteLine);
                oLogError.Flush();
            }
            catch (Exception ex)
            { 
                
            }

            //oLogError.Close();
        }
    }
}
