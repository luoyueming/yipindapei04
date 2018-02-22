using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace NewXzc.Web.Common
{
    public class Get_A_Href
    {
        /// <summary>
        /// 获取A标签的所有信息
        /// </summary>
        /// <param name="html">被解析的字符串</param>
        public static string get_a_info(string html)
        {
            Regex regex = new Regex(@"<a[^>]+href=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)""|(?<href>[^>\s]+))\s*[^>]*>(?<text>.*?)</a>", RegexOptions.IgnoreCase); 
            MatchCollection matchs = regex.Matches(html);
            //List<string> hyperlinks = new List<string>(); 
            foreach (Match m in matchs)
            {
                if (m.Success)
                {
                    //获取A标签的HREF和HTML内容
                    //hyperlinks.Add(m.Groups["href"].Value + "  " + m.Groups["text"].Value);
                    string ahref = m.Groups["href"].Value;
                    string new_a = m.ToString();
                    new_a = new_a.Replace(m.Groups["href"].Value,"javascript:;");
                    new_a = new_a.Replace("<a", "<a onclick=\"gourl('" + ahref + "')\" ");
                    html=html.Replace(m.ToString(),new_a);
                }
            } 
            //foreach (string href in hyperlinks)
            //{
            //    if (href.Substring(0, 4) == "http")
            //    {
                    
            //    }
            //}

            return html;
        }
    }
}