using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewXzc.Common
{
    public class ShareHelper
    {
        /// <summary>
        /// 去除当前文本中的回车和换行符号
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ReturnBlogIntroduce(string text)
        {
            string str = System.Web.HttpUtility.HtmlEncode(text);
            if (String.IsNullOrEmpty(str))
            {
                str = "";
            }
            else
            {
                if (str.IndexOf("\n") >= 0)
                {
                    str = str.Replace("\n", " ");
                }
                else if (str.IndexOf("\r") >= 0)
                {
                    str = str.Replace("\r", "");
                }
            }
            return str;
        }
        /// <summary>
        /// 返回当前所传文本的值
        /// </summary>
        /// <param name="title">文章标题</param>
        /// <param name="content">文章内容</param>
        /// <returns></returns>
        public static string ReturnNowStr(string title, string content)
        {
            content = StringHelper.NoHTML(content);
            if (title == "")
            {
                return ReturnBlogIntroduce(content);
            }
            else
            {
                return ReturnBlogIntroduce(title);
            }
        }
        /// <summary>
        /// 返回当前内容的值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReturnCurStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }


            return str;
        }
    }
}
