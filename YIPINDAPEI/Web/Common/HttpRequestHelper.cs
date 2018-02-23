using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.IO.Compression;
using System.IO;

namespace NewXzc.Web.Common
{
    public class HttpRequestHelper
    {
        private static Encoding DEFAULT_ENCODING = Encoding.UTF8;
        private static string ACCEPT = "application/json, text/javascript, */*; q=0.01";
        private static string CONTENT_TYPE = "application/x-www-form-urlencoded";
        private static string USERAGENT = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.116 Safari/537.36";

        public static string GetHtmlContent(string url)
        {
            return GetHtmlContent(url, DEFAULT_ENCODING);
        }

        public static string GetHtmlContent(string url, Encoding encoding)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = USERAGENT;
            request.Credentials = CredentialCache.DefaultCredentials;
            using (var webResponse = (HttpWebResponse)request.GetResponse())
            {
                var bytes = GetWebResponseData(webResponse);
                return encoding.GetString(bytes);
            }
        }

        public static string GetCookie(string url)
        {
            string cookie = string.Empty;
            var request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            using (var response = request.GetResponse())
            {
                cookie = response.Headers.Get("Set-Cookie");
            }
            return cookie;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="referer"></param>
        /// <param name="cookieHeader">name=value,name=value</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static byte[] Post(string url, byte[] data = null, string[] headers = null, string referer = null, string cookieHeader = null, IList<Cookie> cookies = null)
        {
            return Submit(url, "POST", data, headers, referer, cookieHeader, cookies);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="referer"></param>
        /// <param name="cookieHeader">name=value,name=value</param>
        /// <param name="cookies"></param>
        /// <param name="headers">name=value,name=value</param>
        /// <returns></returns>
        private static byte[] Submit(string url, string method, byte[] data = null, string[] headers = null, string referer = null, string cookieHeader = null, IList<Cookie> cookies = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentNullException("url不能为空。");
            if (string.IsNullOrEmpty(method)) throw new ArgumentNullException("method不能为空。");

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Accept = ACCEPT;
            httpWebRequest.Referer = referer ?? url;
            httpWebRequest.UserAgent = USERAGENT;
            httpWebRequest.Method = method;
            httpWebRequest.CookieContainer = GetCookieContainer(url, cookieHeader, cookies);
            if (headers != null)
            {
                foreach (var item in headers)
                {
                    httpWebRequest.Headers.Add(item);
                }
            }
            if (method == "POST")
            {
                httpWebRequest.ContentType = CONTENT_TYPE;
            }
            if (data != null)
            {
                httpWebRequest.ContentLength = (long)data.Length;
                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            using (var webResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                return GetWebResponseData(webResponse);
            }
        }

        private static byte[] GetWebResponseData(HttpWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            {
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                    {
                        var bytes = ReadFully(gZipStream);
                        return bytes;
                    }
                }
                else
                {
                    var bytes = ReadFully(stream);
                    return bytes;
                }
            }
        }
        private static byte[] ReadFully(Stream stream)
        {
            byte[] buffer = new byte[128];
            byte[] result;
            using (var ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        break;
                    }
                    ms.Write(buffer, 0, read);
                }
                result = ms.ToArray();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieHeader">name=value,name=value</param>
        /// <param name="cookies"></param>
        /// <returns></returns>
        public static byte[] Get(string url, string[] headers = null, string referer = null, string cookieHeader = null, IList<Cookie> cookies = null)
        {
            return Submit(url, "GET", null, headers, referer, cookieHeader, cookies);
        }

        private static CookieContainer GetCookieContainer(string url, string cookieHeader = null, IList<Cookie> cookies = null)
        {
            if (string.IsNullOrEmpty(cookieHeader) && (cookies == null || cookies.Count == 0)) return null;
            var container = new CookieContainer();
            if (!string.IsNullOrEmpty(cookieHeader))
            {
                container.SetCookies(new Uri(url), cookieHeader);
            }
            if (cookies != null)
            {
                foreach (var item in cookies)
                {
                    container.Add(item);
                }
            }
            return container;
        }
    }
}