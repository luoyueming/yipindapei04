using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;

namespace NewXzc.Web.Common
{
    public class Get_Img_UrlList
    {
        /// <summary>
        /// 获取多个字符串中图片的路径，返回字符串替换后的结果
        /// </summary>
        /// <param name="text">需要获取路径的字符串</param>
        /// <returns></returns>
        public static string MyGetImgUrl_Str(string text)
        {
            string result = text;

            StringBuilder str = new StringBuilder();
            string pat = @"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>";

            Regex r = new Regex(pat, RegexOptions.Compiled);

            Match m = r.Match(text);

            string s = "";

            while (m.Success)
            {
                //str.Append(m.Groups[2]);
                s = m.Groups[2].ToString();

                //s = s.Replace("\"", "");
                //string[] imgtype_arr = s.Split('=');
                //string imgtype = imgtype_arr[imgtype_arr.Length - 1];
                //替换原图片的路径为新路径，和用ASP.NET实现下载远程图片保存到本地的方法 保存抓取远程图片的方法联用
                //result = result.Replace(s, Save_Img_Http.Save_Img_WebClient(s, imgtype));

                result = result.Replace(s, "\"http://image01.xzhichang.com/xzcimg.png\"");

                m = m.NextMatch();
            }

            return result;
        }


        /// <summary>
        /// 获取多个字符串中图片的路径，返回字符串替换后的结果
        /// </summary>
        /// <param name="text">需要获取路径的字符串</param>
        /// <returns></returns>
        public static List<string> MyGetImgUrl_List(string text)
        {
            List<string> result = new List<string>();

            StringBuilder str = new StringBuilder();
            string pat = @"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>";

            Regex r = new Regex(pat, RegexOptions.Compiled);

            Match m = r.Match(text);

            string s = "";

            while (m.Success)
            {
                //str.Append(m.Groups[2]);
                s = m.Groups[2].ToString();

                s = s.Replace("\"", "");
                //string[] imgtype_arr = s.Split('=');
                //string imgtype = imgtype_arr[imgtype_arr.Length - 1];
                //替换原图片的路径为新路径，和用ASP.NET实现下载远程图片保存到本地的方法 保存抓取远程图片的方法联用
                //result = result.Replace(s, Save_Img_Http.Save_Img_WebClient(s, imgtype));

                if (s != "")
                {
                    result.Add(s);
                }

                m = m.NextMatch();
            }

            return result;
        }


    }
}