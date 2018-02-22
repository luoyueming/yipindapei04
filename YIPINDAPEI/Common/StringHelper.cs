using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace NewXzc.Common
{
    /// <summary>
    /// StringHelper 的摘要说明。
    /// </summary>
    public class StringHelper
    {
        public StringHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 获取字符串的实际字节长度的方法
        /// <summary>
        /// 获取字符串的实际字节长度的方法
        /// </summary>
        /// <param name="source">字符串</param>
        /// <returns>实际长度</returns>
        public static int GetRealLength(string source)
        {
            if (source == null) return 0;
            return Encoding.Default.GetByteCount(source);
        }
        #endregion

        #region 按字节数截取字符串的方法
        /// <summary>
        /// 按字节数截取字符串的方法
        /// </summary>
        /// <param name="source">要截取的字符串</param>
        /// <param name="n">要截取的字节数</param>
        /// <param name="needEndDot">是否需要结尾的省略号</param>
        /// <returns>截取后的字符串</returns>
        public static string SubString(string source, int n, bool needEndDot)
        {
            string temp = string.Empty;
            if (GetRealLength(source) <= n)//如果长度比需要的长度n小,返回原字符串
            {
                return source;
            }
            else
            {
                int t = 0;
                char[] q = source.ToCharArray();
                for (int i = 0; i < q.Length && t < n; i++)
                {
                    if ((int)q[i] > 127)//是否汉字
                    {
                        temp += q[i];
                        t += 2;
                    }
                    else
                    {
                        temp += q[i];
                        t++;
                    }
                }
                if (needEndDot)
                    temp += "...";
                return temp;
            }
        }
        #endregion


        /// <summary>
        /// 过滤字符串中注入SQL脚本的方法
        /// </summary>
        /// <param name="source">传入的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string SqlFilter(string source)
        {
            //半角括号替换为全角括号
            source = source.Replace("'", "''").Replace(";", "；").Replace("(", "（").Replace(")", "）");

            //去除执行SQL语句的命令关键字
            source = Regex.Replace(source, "select", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "insert", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "update", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "delete", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "drop", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "truncate", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "declare", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "xp_cmdshell", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "/add", "", RegexOptions.IgnoreCase);
            //Regex.Replace(source, "asc(", "", RegexOptions.IgnoreCase);
            //Regex.Replace(source, "mid(", "", RegexOptions.IgnoreCase);
            //Regex.Replace(source, "char(", "", RegexOptions.IgnoreCase);
            //Regex.Replace(source, "count(", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "net user", "", RegexOptions.IgnoreCase);

            //去除执行存储过程的命令关键字 
            source = Regex.Replace(source, "exec", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "execute", "", RegexOptions.IgnoreCase);

            //去除系统存储过程或扩展存储过程关键字
            source = Regex.Replace(source, "xp_", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "sp_", "", RegexOptions.IgnoreCase);

            //防止16进制注入
            source = Regex.Replace(source, "0x", "", RegexOptions.IgnoreCase);

            //防止特殊字符注入
            source = Regex.Replace(source, ";", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "'", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "&", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "%20", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "--", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "==", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "<", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, ">", "", RegexOptions.IgnoreCase);
            source = Regex.Replace(source, "%", "", RegexOptions.IgnoreCase);

            return source;
        }

        /// <summary>
        /// 过滤字符串中的注入跨站脚本(先进行UrlDecode再过滤脚本关键字)
        /// </summary>
        /// <param name="source">需要过滤的字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string XSSFilter(string source)
        {
            source = source.Trim();

            if (source == "") return source;

            //return wipescript( source );

            string result = System.Web.HttpUtility.UrlDecode(source);

            string replaceEventStr = " onXXX =";
            string tmpStr = "";

            string patternGeneral = @"<[^<>]*>";                              //例如 <abcd>
            string patternEvent = @"([\s]|[:])+[o]{1}[n]{1}\w*\s*={1}";     // 空白字符或: on事件=
            string patternScriptBegin = @"\s*((javascript)|(vbscript))\s*[:]?";  // javascript或vbscript:
            string patternScriptEnd = @"<([\s/])*script.*>";                       // </script>   

            Regex regexGeneral = new Regex(patternGeneral, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex regexEvent = new Regex(patternEvent, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Regex regexScriptEnd = new Regex(patternScriptEnd, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex regexScriptBegin = new Regex(patternScriptBegin, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            System.Text.RegularExpressions.Regex regexIframe = new System.Text.RegularExpressions.Regex(@"<frameset[sS]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            source = regexIframe.Replace(source, ""); //过滤frameset 



            Match matchGeneral, matchEvent, matchScriptEnd, matchScriptBegin;

            //过滤字符串中的 </script>   
            for (matchGeneral = regexGeneral.Match(result); matchGeneral.Success; matchGeneral = matchGeneral.NextMatch())
            {
                tmpStr = matchGeneral.Groups[0].Value;
                matchScriptEnd = regexScriptEnd.Match(tmpStr);
                if (matchScriptEnd.Success)
                {
                    tmpStr = regexScriptEnd.Replace(tmpStr, " ");
                    result = result.Replace(matchGeneral.Groups[0].Value, tmpStr);
                }
            }

            //过滤字符串中的脚本事件
            for (matchGeneral = regexGeneral.Match(result); matchGeneral.Success; matchGeneral = matchGeneral.NextMatch())
            {
                tmpStr = matchGeneral.Groups[0].Value;
                matchEvent = regexEvent.Match(tmpStr);
                if (matchEvent.Success)
                {
                    tmpStr = regexEvent.Replace(tmpStr, replaceEventStr);
                    result = result.Replace(matchGeneral.Groups[0].Value, tmpStr);
                }
            }

            //过滤字符串中的 javascript或vbscript:
            for (matchGeneral = regexGeneral.Match(result); matchGeneral.Success; matchGeneral = matchGeneral.NextMatch())
            {
                tmpStr = matchGeneral.Groups[0].Value;
                matchScriptBegin = regexScriptBegin.Match(tmpStr);
                if (matchScriptBegin.Success)
                {
                    tmpStr = regexScriptBegin.Replace(tmpStr, " ");
                    result = result.Replace(matchGeneral.Groups[0].Value, tmpStr);
                }
            }
            System.Text.RegularExpressions.Regex regexScript = new System.Text.RegularExpressions.Regex(@"<script[sS]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            source = regexScript.Replace(source, ""); //过滤style 

            System.Text.RegularExpressions.Regex regexStyle = new System.Text.RegularExpressions.Regex(@"<style[sS]+</style *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            source = regexStyle.Replace(source, ""); //过滤style 
            System.Text.RegularExpressions.Regex regexForm = new System.Text.RegularExpressions.Regex(@"<form[sS]+</form *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            source = regexForm.Replace(source, ""); //过滤Form


            System.Text.RegularExpressions.Regex regexInput = new System.Text.RegularExpressions.Regex(@"<input", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            source = regexInput.Replace(source, ""); //过滤input

            System.Text.RegularExpressions.Regex regexButton = new System.Text.RegularExpressions.Regex(@"<button", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            source = regexButton.Replace(source, ""); //过滤input



            //	throw new Exception( result );
            return result;
        }

        public static string wipescript(string html)
        {
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\s]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@" href *= *[\s\s]*script *:", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex3 = new System.Text.RegularExpressions.Regex(@" on[\s\s]*=", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex4 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\s]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex5 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\s]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记 
            html = regex2.Replace(html, ""); //过滤href=javascript: (<a>) 属性 
            html = regex3.Replace(html, " _disibledevent="); //过滤其它控件的on...事件 
            html = regex4.Replace(html, ""); //过滤iframe 
            html = regex5.Replace(html, ""); //过滤frameset 


            //	throw new Exception( html );
            return html;
        }
        /// <summary>
        /// 过滤字符串中注入Flash代码
        /// </summary>
        /// <param name="htmlCode">输入字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string FlashFilter(string htmlCode)
        {
            string pattern = @"\w*<OBJECT\s+.*(macromedia)[\s*|\S*]{1,1300}</OBJECT>";

            return Regex.Replace(htmlCode, pattern, "", RegexOptions.Multiline);
        }


        #region excel方法
        /// <summary>
        /// 过滤excel中的科学计数法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ScientificToString(System.Object obj)
        {
            string txt = obj.ToString();
            ////try
            ////{
            ////    //double numbertypes = Convert.ToDouble(txt);
            ////    //NumberFormatInfo ni = NumberFormatInfo.CurrentInfo.Clone() as NumberFormatInfo;
            ////    //ni.NumberDecimalDigits = 0;
            ////    //txt = numbertypes.ToString("F", ni);
            ////}
            //catch { }
            return txt;
        }
        #endregion

        #region  用正则表达式过滤脚本的研究

        /// <summary>

        /// 标记中包含的代码

        ///  a href= javascript :...中的代码

        /// 3. 其它基本控件的 on...事件中的代码

        /// 4. iframe 和 frameset 中载入其它页面造成的攻击

        /// </summary>

        /// <param name="html"></param>

        /// <returns></returns>
        private static string ReplaceMatch(string html, string regexstr)
        {

            Regex regex = new Regex(regexstr, RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase | RegexOptions.Singleline);


            string script = "";
            MatchCollection matches = regex.Matches(html);

            for (int i = 0; i != matches.Count; ++i)
            {
                //  TODO:  Do  something  with  result
                GroupCollection mg = matches[i].Groups;
                script = matches[i].Value;
                for (int j = 0; j < mg.Count; j++)
                {
                    script = mg[j].Value;

                    html = html.Replace(script, "");
                }



            }
            return html;

        }
        public static string WipeScript(string html)
        {

            if (string.IsNullOrEmpty(html) || html.Length == 0)
            {
                return html;
            }
            string regexstr = "";




            //regexstr = @"\s[on].+?=([\""|\'])(.*?)([\""|\'])";
            //html = ReplaceMatch(html, regexstr);

            regexstr = @"<script[^>]*>.*?</script>";
            html = ReplaceMatch(html, regexstr);

            regexstr = @"<iframe[^>]*>.*?</iframe>";
            html = ReplaceMatch(html, regexstr);

            regexstr = @"<frameset[^>]*>.*?</frameset>";
            html = ReplaceMatch(html, regexstr);

            regexstr = @"<object[^>]*>.*?</object>";
            html = ReplaceMatch(html, regexstr);


            regexstr = @"<map[^>]*>.*?</map>";
            html = ReplaceMatch(html, regexstr);


            regexstr = @"<input[^>]*>";
            html = ReplaceMatch(html, regexstr);

            regexstr = @"<select[^>]*>.*?</select>";
            html = ReplaceMatch(html, regexstr);


            //regexstr = @"<style[^>]*>.*?</style>";
            //html = ReplaceMatch(html, regexstr);



            regexstr = @"<form[^>]*>";
            html = ReplaceMatch(html, regexstr);

            regexstr = @"</form>";
            html = ReplaceMatch(html, regexstr);




            return html;

        }



        //   '//去除onclick,onload等脚本
        //    regEx.Pattern = "\s[on].+?=([\""|\'])(.*?)\1"
        //    sReallyDo = regEx.Replace(sReallyDo, "")
        //    '//将SRC不带引号的图片地址加上引号
        //    regEx.Pattern = "<img.*?\ssrc=([^\""\'\s][^\""\'\s>]*).*?>"
        //    sReallyDo = regEx.Replace(sReallyDo, "<img src=""$1"" />")
        //    '//正则匹配图片SRC地址
        //    regEx.Pattern = "<img.*?\ssrc=([\""\'])([^\""\']+?)\1.*?>"
        //本文来源于 KinJAVA日志 (http://jorkin.reallydo.com) 
        //原文地址: http://jorkin.reallydo.com/default.asp?tag=getIMG

        #endregion

        #region FilterHtml
        ///   <summary> 
        ///   去除HTML标记 
        ///   </summary> 
        public static string NoHTML(string Htmlstring)
        {
            //删除脚本 
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
              RegexOptions.IgnoreCase);
            //删除HTML 
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
              RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
              RegexOptions.IgnoreCase);
            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();

            return Htmlstring;
        }

        public static string StripHTML(string strHtml)
        {
            string[] aryReg =
            {
              @"<script[^>]*?>.*?</script>",
              @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[",
               @"'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", @"([\r\n])[\s]+", 
                @"&(quot|#34);", @"&(amp|#38);", @"&(lt|#60);", @"&(gt|#62);", 
                @"&(nbsp|#160);", @"&(iexcl|#161);", @"&(cent|#162);", @"&(pound|#163);",
                @"&(copy|#169);", @"&#(\d+);", @"-->", @"<!--.*\n"
            };

            string[] aryRep =
            {
              "", "", "", "\"", "&", "<", ">", "   ", "\xa1",  //chr(161), 
              "\xa2",  //chr(162), 
              "\xa3",  //chr(163), 
              "\xa9",  //chr(169), 
              "", "\r\n", ""
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, aryRep[i]);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");
            return strOutput;
        }
        ///   <summary> 
        ///   移除HTML标签 
        ///   </summary> 
        ///   <param   name="HTMLStr">HTMLStr</param> 
        public static string ParseTags(string HTMLStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(HTMLStr, "<[^>]*>", "");
        }
        ///   <summary> 
        ///   取出文本中的图片地址 
        ///   </summary> 
        ///   <param   name="HTMLStr">HTMLStr</param> 
        public static string GetImgUrl(string HTMLStr)
        {
            string str = string.Empty;
            string sPattern = @"^<img\s+[^>]*>";
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>",
              RegexOptions.Compiled);
            Match m = r.Match(HTMLStr.ToLower());
            if (m.Success)
                str = m.Result("${url}");
            return str;
        }

        /// <summary>
        /// 判断当前值是否为空，为空则返回""，否则原样返回
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Return_Val(string str)
        {
            string val = "";
            if (!string.IsNullOrEmpty(str))
            {
                val = str;
            }
            return val;
        }

        /// <summary>
        /// 判断当前值是否为空，为空则返回0，否则原样返回
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int Return_Int_Val(string num)
        {
            int val = 0;
            try
            {
                if (!string.IsNullOrEmpty(num))
                {
                    val =Convert.ToInt32(num);
                }
            }
            catch (Exception ex)
            {

            }
            return val;
        }

        /// <summary>
        /// 判断当前值是否为空，为空则返回0，否则原样返回
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int Return_Int_Val(int num)
        {
            int val = 0;
            try
            {
                if (!string.IsNullOrEmpty(num.ToString()))
                {
                    val = num;
                }
            }
            catch (Exception ex)
            {

            }
            return val;
        }

        /// <summary>
        /// 返回指定长度的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string ReturnNumStr(string str,int num)
        {
            string val =Return_Val(str);
            if (str.Length > num)
            {
                //val = str.Substring(0,num)+"...";
                val = CutString(str, num * 2);
            }
            return val;
        }

        /// <summary>
        /// 获取截取后的字符串，num为0时，为获取所有信息，type：解析字符串的类型（0：StringPlus.GetEncode，1：StringHelper.NoHTML，2：HTMLHelper.GetStringReplace，3：不进行任何解析）
        /// </summary>
        /// <param name="val">需要被解析的字符串</param>
        /// <param name="type">解析字符串的类型（0：StringPlus.GetEncode，1：StringHelper.NoHTML，2：HTMLHelper.GetStringReplace，3：不进行任何解析）</param>
        /// <param name="num">指定显示的信息数，0：获取所有信息</param>
        /// <returns></returns>
        public static string ReturnNumStr(string val, int type, int num)
        {
            if (val != "")
            {
                if (type == 0)
                {
                    val = StringPlus.GetEncode(val);
                }
                else if (type == 1)
                {
                    val = StringHelper.NoHTML(val);
                }
                else if (type == 2)
                {
                    val = HTMLHelper.GetStringReplace(val);
                }

                if (num > 0)
                {
                    if (val.Length > num)
                    {
                        //val = val.Substring(0, num) + "...";
                        val = StringHelper.ReturnNumStr(val, num);
                    }
                }

                //if (num > 0)
                //{
                //    val = CutString(val, num * 2);
                //}
            }

            return val;

        }

        /// <summary>
        /// 按字节数返回指定长度的字符串
        /// </summary>
        /// <param name="strInput">需要被截取的字符串</param>
        /// <param name="intLen">需要被截取的长度(字节的长度，在原有的长度*2)</param>
        /// <returns></returns>
        public static string CutString(string strInput, int intLen)
        {
            strInput = strInput.Trim();
            byte[] myByte = System.Text.Encoding.Default.GetBytes(strInput);
            if (myByte.Length > intLen)
            {
                string resultStr = "";
                for (int i = 0; i < strInput.Length; i++)
                {
                    byte[] tempByte = System.Text.Encoding.Default.GetBytes(resultStr);
                    if (tempByte.Length < intLen)
                    {
                        resultStr += strInput.Substring(i, 1);
                    }
                    else
                    {
                        break;
                    }
                }
                return resultStr + "...";
            }
            else
            {
                return strInput;
            }
        }

        /// <summary>
        /// 转换为时间格式 转换不成功返回当前时间
        /// </summary>
        /// <param name="dt">需要转换的时间</param>
        /// <returns></returns>
        public static DateTime RetTime(object dt)
        {
            DateTime retDt = DateTime.Now;
            try
            {
                retDt = Convert.ToDateTime(dt);
            }
            catch (Exception ex) { }
            return retDt;
        }



        #endregion
    }
}
