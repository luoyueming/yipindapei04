using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NewXzc.Common
{
    public class Input
    {
        /// <summary>
        /// 根目录
        /// </summary>
        public static string rootDir
        {
            get
            {
                string root = HttpContext.Current.Request.ApplicationPath;
                if (root == "/") root = string.Empty;
                return root;
            }
        }

        /// <summary>
        /// 将字符转化为HTML编码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回编译后的字符串</returns>
        public static string HtmlEncode(string Input)
        {
            return HttpContext.Current.Server.HtmlEncode(Input);
        }

        /// <summary>
        /// 将字符HTML解码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string HtmlDecode(string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            else
                return HttpContext.Current.Server.HtmlDecode(Input);
        }

        /// <summary>
        /// URL地址编码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返编码后的字符串</returns>
        public static string URLEncode(string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            else
                return HttpContext.Current.Server.UrlEncode(Input);
        }

        /// <summary>
        /// URL地址解码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string URLDecode(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                return HttpContext.Current.Server.UrlDecode(Input);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="Input">输入字串符（object）</param>
        /// <returns>返回true或false</returns>
        public static bool IsInteger(object Input)
        {
            if (Input == null) { return false; } else { return IsInteger(Input, true); }
        }

        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="Input">输入字符串（object类）</param>
        /// <param name="Plus">true表示是否正整数</param>
        /// <returns>返回true或false</returns>
        public static bool IsInteger(object Input, bool Plus)
        {
            if (Input == null) return false;
            if (string.IsNullOrEmpty(Input.ToString())) { return false; }
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus) pattern = "^[0-9]+$";
                if (Regex.Match(Input.ToString(), pattern, RegexOptions.Compiled).Success) { return true; }
                else { return false; }
            }
        }

        /// <summary>
        /// 判断输入是否为日期类型
        /// </summary>
        /// <param name="s">待检查数据</param>
        /// <returns>返回true或false</returns>
        public static bool IsDate(string s)
        {
            if (s == null)
            {
                return false;
            }
            else
            {
                try
                {
                    DateTime d = DateTime.Parse(s);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 判断是否是电子邮件
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>返回true或false</returns>
        public static bool isEmail(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }
            return Regex.IsMatch(strIn, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        /// <summary>
        /// 判断是否是国内手机号码,前面不加0
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>返回true或false</returns>
        public static bool isMobile(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }
            string regu = "^[1][3,5,8][0-9]{9}$";
            if (Regex.Match(strIn, regu, RegexOptions.Compiled).Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 过滤字符串中的html代码
        /// </summary>
        /// <param name="Str">传入字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string LostHTML(string Str)
        {
            string Re_Str = "";
            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "<\\/*[^<>]*>";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }
            return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
        }


        /// <summary>
        /// 截取字符串函数
        /// </summary>
        public static string GetSubString(string Str, int Num)
        {
            string sdot = string.Empty;
            if (Str == null || Str == "")
                return string.Empty;
            string outstr = string.Empty;
            int n = 0;
            foreach (char ch in Str)
            {
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (n > Num)
                {
                    sdot = "...";
                    break;
                }
                else
                {
                    outstr += ch;
                }
            }
            if (Str.Length > 3)
            {
                return outstr + sdot;
            }
            else
            {
                return outstr;
            }
        }
        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <param name="Len">返回实际长度</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num, out int Len)
        {
            Len = 0;
            if (string.IsNullOrEmpty(Str))
                return "";
            string outstr = string.Empty;

            foreach (char ch in Str)
            {
                Len += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (Len > Num)
                    break;
                else
                    outstr += ch;
            }
            return outstr;
        }

        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <param name="Num">截取字符串后省略部分的字符串</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num, string LastStr)
        {
            return (Str.Length > Num) ? Str.Substring(0, Num) + LastStr : Str;
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        public static string Htmls(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("</script", "&lt;/script");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ToshowTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            return sb.ToString();
        }

        /// <summary>
        /// 把字符转化为文本格式
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string ForTXT(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("<font", " ");
            sb.Replace("<span", " ");
            sb.Replace("<style", " ");
            sb.Replace("<div", " ");
            sb.Replace("<p", "");
            sb.Replace("</p>", "");
            sb.Replace("<label", " ");
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "");
            sb.Replace("<br />", "");
            sb.Replace("<br />", "");
            sb.Replace("&lt;", "");
            sb.Replace("&gt;", "");
            sb.Replace("&amp;", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ToHtml(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                StringBuilder sb = new StringBuilder(Input);
                sb.Replace("&", "&amp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\r\n", "<br />");
                sb.Replace("\n", "<br />");
                sb.Replace("\t", " ");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
        public static string MD5(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half) output = output.Substring(8, 16);
            return output;
        }

        public static string MD5(string Input)
        {
            return MD5(Input, true);
        }

        /// <summary>
        /// 过滤HTML语法
        /// </summary>
        public static string FilterHTML(object input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            else
            {
                string html = input.ToString();
                if (string.IsNullOrEmpty(html)) return string.Empty;
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                html = regex1.Replace(html, string.Empty);
                return html.Replace("\"", "“");
            }
        }

        public static string FilterLogHTML(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            if (html.ToLower().IndexOf("<img") > -1 && html.ToLower().IndexOf(">") > -1)
            {
                html = "<span class=\"sp_reshow\">【图文日志】</span>" + html;
            }
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, string.Empty);
            return html;
        }

        /// <summary>
        /// 去除script
        /// </summary>
        public static string loseScript(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            return html;
        }

        /// <summary>
        /// 去除iframe
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string loseIframe(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            return html;
        }

        /// <summary>
        /// 去除除IMG以外的HTML
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string loseExceptImg(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<(?!/?img)[\s\S]*?>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex.Replace(html, "");
            return html;
        }

        /// <summary>
        /// 把图文分解成图，文
        /// </summary>
        /// <param name="html">输入HTML</param>
        /// <param name="img">图</param>
        /// <param name="text">文</param>
        public static void splitImgAndText(string html, out string img, out string text)
        {
            if (html == null)
            {
                img = "";
                text = "";
                return;
            }
            Regex regex = new System.Text.RegularExpressions.Regex(@"<img[\s\S]+</img *>|<img[^>]+/? *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Match m = regex.Match(html);
            string imgstr = string.Empty;
            while (m.Success)
            {
                imgstr += m.Value;
                m = m.NextMatch();
            }
            string textStr = FilterHTML(html);
            img = imgstr;
            text = textStr;
        }

        /// <summary>
        /// 去除多于的逗号
        /// </summary>
        public static string FixCommaStr(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string[] arr = str.Split(',');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Trim() != "")
                    {
                        sb.Append(arr[i]);
                        sb.Append(",");
                    }
                }
                string sbstr = sb.ToString();
                if (!string.IsNullOrEmpty(sbstr))
                {
                    sbstr = sbstr.Substring(0, sbstr.Length - 1);
                }
                return sbstr;
            }
            else
            {
                return string.Empty;
            }
        }

   
        /// <summary>
        /// 替换表情
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceSmaile(object content)
        {
            if (content == null) return string.Empty;
            string Input = content.ToString();
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            string s = string.Empty;
            Match match = Regex.Match(Input, "\\[em\\:[^\\:\\]\\[em\\:><,/]*\\:\\]", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            while (match.Success)
            {
                s = match.Value;
                Input = Input.Replace(s, "<img src=\"" + rootDir + "/template/images/face/" + s + ".gif\"   align=\"middle\" style=\"border:0\" />");
                match = match.NextMatch();
            }
            string rList = Input.Replace("[em:", string.Empty);
            rList = rList.Replace(":]", string.Empty);
            return rList;
        }


        #region 去除HTML标记
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion

        /// <summary>
        /// 生成json格式字符串
        /// </summary>
        public static string jsonString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("/", "\\/");
            str = str.Replace("'", "\"");
            return str;
        }
        /// <summary>
        /// 过滤字符
        /// </summary>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput.Trim() == string.Empty)
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }


        /// <summary>
        /// 取得HTML中所有图片的 URL。
        /// </summary>
        /// <param name="sHtmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }

    }
}
