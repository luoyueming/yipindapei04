using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace NewXzc.Common
{
    public class StringPlus
    {
        public static List<string> GetStrArray(string str, char speater,bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss =  str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) &&s !=speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        public static string[] GetStrArray(string str)
        {
            return str.Split(new char[',']);
        }
        public static string GetArrayStr(List<string> list,string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        
        
        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str,string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion




        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }        

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] ss = o_str.Split(sepeater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }

        /// <summary>
        /// 将字符串中脚本转换为其相应的替换符
        /// </summary>
        /// <param name="str">需要进行操作的字符串</param>
        /// <returns></returns>
        public static string GetEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        /// <summary>
        /// 将字符串中替换符还原为其相应的脚本
        /// </summary>
        /// <param name="str">需要进行操作的字符串</param>
        /// <returns></returns>
        public static string GetDecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        /// <summary>
        /// 获取时间字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string returnDate(DateTime dt)
        {
            DateTime now = DateTime.Now;
            TimeSpan ts = now.Subtract(dt);
            if (ts.TotalDays > 4)
                //if (dt.Year != now.Year)
                //   return dt.Year + "-" + dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute;
                //else
                //    return dt.Month + "-" + dt.Day + " " + dt.Hour + ":" + dt.Minute;
                return dt.ToString("yyyy-MM-dd");
            else if (ts.TotalDays >= 1 && ts.TotalDays < 4)
                return Convert.ToInt32(ts.TotalDays) + "天前";
            else if (ts.TotalDays > 0 && ts.TotalHours > 1)
                return Convert.ToInt32(ts.TotalHours) + "小时前";
            else if (ts.TotalDays > 0 && ts.TotalHours > 0 && ts.TotalMinutes > 1)
                return Convert.ToInt32(ts.TotalMinutes) + "分钟前";
            else
                return "刚刚";
        }

        #region 将字符串样式转换为纯字符串
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //如果为空，返回空值
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //返回去掉分隔符
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region 将字符串转换为新样式
        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string ReturnValue = "";
            //如果输入空值，返回空，并给出错误提示
            if (StrList == null)
            {
                ReturnValue = "";
                Error = "请输入需要划分格式的字符串";
            }
            else
            {
                //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
                int strListLength = StrList.Length;
                int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                if (strListLength != NewStyleLength)
                {
                    ReturnValue = "";
                    Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                }
                else
                {
                    //检查新样式中分隔符的位置
                    string Lengstr = "";
                    for (int i = 0; i < NewStyle.Length; i++)
                    {
                        if (NewStyle.Substring(i, 1) == SplitString)
                        {
                            Lengstr = Lengstr + "," + i;
                        }
                    }
                    if (Lengstr != "")
                    {
                        Lengstr = Lengstr.Substring(1);
                    }
                    //将分隔符放在新样式中的位置
                    string[] str = Lengstr.Split(',');
                    foreach (string bb in str)
                    {
                        StrList = StrList.Insert(int.Parse(bb), SplitString);
                    }
                    //给出最后的结果
                    ReturnValue = StrList;
                    //因为是正常的输出，没有错误
                    Error = "";
                }
            }
            return ReturnValue;
        }
        #endregion        

        #region 剪切字符串
        /// <summary>
        /// 剪切字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="num">数量</param>
        /// <returns></returns>
        public static string sub(string str, int num)
        {
            if (str.Length > num)
            {
                return str.Substring(0, num)+"...";
            }
            return str;
        }
        #endregion

        #region 剪切字符串
        /// <summary>
        /// 剪切字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="num">超过数量进行截取</param>
        /// <param name="cutNum">截取后剩余数量</param>
        /// <returns></returns>
        public static string sub(string str, int num,int cutNum)
        {
            if (str.Length > num)
            {
                return str.Substring(0, cutNum) + "...";
            }
            return str;
        }
        #endregion

        #region 字符串整理
        /// <summary>
        /// 字符串整理
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type">0：籍贯 1行业 职能</param>
        /// <returns></returns>
        public static string returnString(string str, int type)
        {
            str = str.TrimEnd(',').TrimEnd('-');
            if (type == 0)
            {
                if (str == ",")
                {
                    return "";
                }
                else
                {
                    return str.Replace(",","-").Trim().TrimEnd('-');
                }
            }
            else if (type == 1)
            {
                string[] strs = str.Split(',');
                string tempStr = "";
                foreach (string s in strs)
                {
                    if (s != "")
                    {
                        tempStr += s + " ";
                    }
                }
                return tempStr.Trim();
            }
            else if (type == 2)
            {
                if (str == ",")
                {
                    return "";
                }
                else
                {
                    return str.Replace(",", "  ");
                }
            }
            else if (type == 3)
            {
                string[] strs = str.Split(',');
                string tempStr = "";
                foreach (string s in strs)
                {
                    if (s != "")
                    {
                        tempStr += "<b class=\"t_InduSpan active notSpan\">" + s + "</b>";
                    }
                }
                return tempStr.Trim();
            }
            else
            {
                return str;
            }

            return "";
        }
        #endregion

        /**/
        /// <summary>
        /// 根据年月日计算星期几(Label2.Text=CaculateWeekDay(2004,12,9);) zhangzilong 2013-07-24
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns></returns>
        public static string CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            string weekstr = "";
            switch (week)
            {
                case 1: weekstr = "星期一"; break;
                case 2: weekstr = "星期二"; break;
                case 3: weekstr = "星期三"; break;
                case 4: weekstr = "星期四"; break;
                case 5: weekstr = "星期五"; break;
                case 6: weekstr = "星期六"; break;
                case 7: weekstr = "星期日"; break;
            }

            return weekstr;
        }

        /// <summary>
        /// 返回当前COOKIES中存储的域名
        /// </summary>
        /// <returns></returns>
        public static string ReturnDomain()
        {
            return ".redd6party";
        }

        /// <summary>
        /// 返回当前COOKIES中存储的域名
        /// </summary>
        /// <returns></returns>
        public static string ReturnDomain_HongRenHui()
        {
            return ".Hren_Redd6party";
        }

        /// <summary>
        /// 返回当前COOKIES中存储的域名，用于邀请码
        /// </summary>
        /// <returns></returns>
        public static string ReturnDomain_Invite()
        {
            return ".invite";
        }

        /// <summary>
        /// 获取当前路径的前缀如http://xzc.d6315.com/login  获取结果http://xzc.d6315.com
        /// </summary>
        /// <param name="httpurl">获取的当前路径</param>
        /// <param name="flag">是否在获取结果http://xzc.d6315.com加/，true为加，false为不加</param>
        /// <returns></returns>
        public static string ReturnHttpPre(string httpurl,bool flag)
        {
            string url = httpurl;
            if (httpurl.IndexOf("/")>=0)
            {
                string[] url_arr = httpurl.Split('/');
                if (url_arr.Length > 2)
                {
                    url = url_arr[0] + "//" + url_arr[2];
                    if (flag)
                    {
                        url = url + "/";
                    }
                }
            }
            return url;
        }
        /// <summary>
        /// 字符串加密后把%替换成@
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string UrlEncode(string txt)
        {
            string t = System.Web.HttpUtility.UrlEncode(System.Web.HttpUtility.UrlEncode(txt));
            t = t.Replace('%', '@');
            return t;
        }
        /// <summary>
        /// 字符串解密后把@替换成%
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static string UrlDecode(string txt)
        {            
            string t = txt.Replace('@', '%');
            t = System.Web.HttpUtility.UrlDecode(System.Web.HttpUtility.UrlDecode(t));
            return t;
        }

        //获取一段时间前的时间点
        public static string beforeTime(int num)
        {
            string time = "";
            switch (num)
            {
                case 1:
                    time = DateTime.Now.AddDays(-3).ToShortDateString();  //三天前
                    break;
                case 2:
                    time = DateTime.Now.AddDays(-7).ToShortDateString();  //一周前
                    break;
                case 3:
                    time = DateTime.Now.AddDays(-14).ToShortDateString();  //两周前
                    break;
                case 4:
                    time = DateTime.Now.AddMonths(-1).ToShortDateString(); //一个月前
                    break;
                case 5:
                    time = DateTime.Now.AddMonths(-3).ToShortDateString(); //三个月前
                    break;
                case 6:
                    time = DateTime.Now.AddMonths(-6).ToShortDateString(); //六个月前
                    break;
                case 7:
                    time = DateTime.Now.AddYears(-1).ToShortDateString(); //一年前
                    break;
            }
            return time;
        }

        /// <summary>
        /// 资金类型
        /// </summary>
        public static string[] Equity_Type = new string[]{
         "独资/投资","参股合作","收购/并购","其他方式"
        };

        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns></returns>
        public static string IsNull_Str(string val)
        {
            if(string.IsNullOrEmpty(val))
            {
                val="";
            }
            
            return val;
        }



        public static string GetExportPath()
        {
            return @"E:\share\imgdata\ExportExcel\";
        }

        /// <summary>
        /// 获得是否为预览页面
        /// </summary>
        /// <param name="position_id">推荐位ID</param>
        /// <returns>0预览 1正式</returns>
        public static int GetVerifyState(int position_id)
        {
            int state = 1;
            if (!string.IsNullOrEmpty(RequestHelper.GetQueryString("read_id")))
            {
                if (position_id.ToString() == RequestHelper.GetQueryString("read_id"))
                {
                    state = 0;
                }
                else
                {
                    state = 1;
                }
            }
            return state;
        }

        public static string ProvinceCity(string Province, string City)
        {
            if (!string.IsNullOrEmpty(Province))
            {
                if (!string.IsNullOrEmpty(City))
                {
                    return Province + "-" + City;
                }
                else
                {
                    return Province;
                }
            }
            return "";
        }
    }
}
