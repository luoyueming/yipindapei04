using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.IO;

namespace NewXzc.Common
{

    public class ShortMessageService
    {
        public ShortMessageService() { }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="strContent">短信内容</param> 
        /// <param name="phone">手机号</param>
        public string sendmessage(string strContent, string phone)
        {
            //POST
            StringBuilder sbTemp = new StringBuilder();

            sbTemp.Append("Sn=SDK-ZQ-0040&Pwd=888888&mobile=" + phone + "&content=" + strContent);

            byte[] bTemp = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(sbTemp.ToString().Trim());


            //byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString().Trim());

            //uid=9999&pwd=fa246d0262c3925617b0c72bb20eeb1d&mobile=13900008888,13900009999,13100006666,0218882228&content=%D6%D0%B9%FA%B6%CC%D0%C5%CD%F8%B7%A2%CB%CD%B2%E2%CA%D4
            String postReturn = doPostRequest("http://124.172.250.160/WebService.asmx/gxmt?", bTemp);
            return postReturn;
            //Console.WriteLine("Post response is: " + postReturn);
            //Console.ReadLine();
        }

        //POST方式发送得结果
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }
    }
}
