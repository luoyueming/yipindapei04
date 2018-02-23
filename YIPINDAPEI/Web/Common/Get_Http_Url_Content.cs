using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

namespace NewXzc.Web.Common
{
    public class Get_Http_Url_Content
    {
        /// <summary>
        /// 获取指定网页的内容的具体操作，方法一
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static string GetPostContent(string strUrl)
        {
            //string title = String_Manage.Return_Str("title");
            //string keywords = String_Manage.Return_Str("keywords");
            //string description = String_Manage.Return_Str("description");

            string strMsg = string.Empty;
            try
            {
                string data = "userName=admin&passwd=admin888";
                byte[] requestBuffer = System.Text.Encoding.GetEncoding("utf-8").GetBytes(data);


                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = requestBuffer.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(requestBuffer, 0, requestBuffer.Length);
                    requestStream.Close();
                }

                WebResponse response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    strMsg = reader.ReadToEnd();


                    

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.ToString();
            }
            return strMsg;
        }

        /// <summary>
        /// 获取指定网页的内容的具体操作，方法二（WebClient）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebClient(string url)
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            return strHTML;
        }

        /// <summary>
        /// 获取指定网页的内容的具体操作，方法三（WebRequest）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetWebRequest(string url)
        {
            Uri uri = new Uri(url);
            WebRequest myReq = WebRequest.Create(uri);
            WebResponse result = myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }

        /// <summary>
        /// 获取指定网页的内容的具体操作，方法四（HttpWebRequest）
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHttpWebRequest(string url)
        {
            Uri uri = new Uri(url);
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
            myReq.UserAgent = "User-Agent:Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705";
            myReq.Accept = "*/*";
            myReq.KeepAlive = true;
            myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
            HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
            Stream receviceStream = result.GetResponseStream();
            StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding("utf-8"));
            string strHTML = readerOfStream.ReadToEnd();
            readerOfStream.Close();
            receviceStream.Close();
            result.Close();
            return strHTML;
        }
    }
}