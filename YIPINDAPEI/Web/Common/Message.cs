using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using NewXzc.Common;
using System.Text;
using NewXzc.Model;
using System.Data;
using NewXzc.DBUtility;

namespace NewXzc.Web.Common
{
    /**
     *  消息通知类，发送通知的方法 
     *  创建时间：2013.7.1
     */

    public class Message
    {
        
        /// <summary>
        /// 错误日志记录方法
        /// </summary>
        /// <param name="ex">错误信息</param>
        /// <param name="fun">所执行的函数</param>
        /// <param name="pageUrl">执行的页面（尽量填写，无法确定可为空）</param>
        /// <param name="parmeter">执行时的参数</param>
        public static void WriteError(string ex, string fun, string pageUrl,string[] parmeter)
        {
            string FilePath = "";
            string FileName = "";
            string directoryPath = "";
            DateTime dt = DateTime.Now;

            string physicsPath = System.Web.HttpContext.Current.Server.MapPath("~/log/");

            directoryPath = physicsPath + dt.Year + dt.Month;

            if (!Directory.Exists(directoryPath))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(directoryPath);
            }

            FileName = dt.Day + ".txt";
            FilePath = directoryPath + "/" + FileName;

            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }

            StreamReader sr = new StreamReader(FilePath, System.Text.Encoding.UTF8);
            
            try
            {
                string input = sr.ReadToEnd();
                sr.Close();
                input = input + "" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + pageUrl + "\t" + fun + "\t" + ex + "\t";
                //input = input + ex;
                for (int i = 0; i < parmeter.Length; i++)
                {
                    input += parmeter[i] + "\t";
                    //input += parmeter[i];
                }
                input += "\r\n\n";
                StreamWriter sw = new StreamWriter(FilePath, false, System.Text.Encoding.UTF8);
                sw.Write(input);
                sw.Flush();
                sw.Close();

            }
            catch(Exception exc)
            {

            }
        }


    }
}