using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Web;
using System.Xml;

namespace NewXzc.Common
{
    public class Public
    {
        /// <summary>
        /// 去掉结尾,
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string LostDot(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            else
            {
                if (input.IndexOf(",") > -1)
                {
                    int intLast = input.LastIndexOf(",");
                    if ((intLast + 1) == input.Length)
                    {
                        return input.Remove(intLast);
                    }
                    else
                    {
                        return input;
                    }
                }
                else
                {
                    return input;
                }
            }
        }
        /// <summary>
        /// 读取模板
        /// </summary>
        /// <param name="path">模板路径</param>
        /// <returns></returns>
        public static string GetTempleContent(string path)
        {
            string result = string.Empty;
            string sFileName = HttpContext.Current.Server.MapPath(path);
            if (File.Exists(sFileName))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(sFileName))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch
                {
                    result = "读取模板文件(" + path + ")出错";
                }
            }
            else
            {
                result = "找不到模板文件：" + path;
            }
            return result;
        }

        /// <summary>
        /// 检查当前IP是否是受限IP
        /// </summary>
        /// <param name="LimitedIP">受限的IP，格式如:192.168.1.110|212.235.*.*|232.*.*.*</param>
        /// <returns>返回true表示IP未受到限制</returns>
        static public bool ValidateIP(string LimitedIP)
        {
            string CurrentIP = GetClientIP();
            if (string.IsNullOrEmpty(LimitedIP))
                return true;
            LimitedIP.Replace(".", @"\.");
            LimitedIP.Replace("*", @"[^\.]{1,3}");
            Regex reg = new Regex(LimitedIP, RegexOptions.Compiled);
            Match match = reg.Match(CurrentIP);
            return !match.Success;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void DelFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch { }
        }


        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static void DelDir(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
            }
            catch { }
        }


        /// <summary>
        /// 取得用户客户端IP(穿过代理服务器取远程用户真实IP地址)
        /// </summary>
        public static string GetClientIP()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }

        /// <summary>
        /// 取得前一个（上次提交或链接进来的）网页的URL
        /// </summary>
        /// <returns></returns>
        public static string GetReferrerUrl()
        {
            Uri uri = HttpContext.Current.Request.UrlReferrer;
            if (uri != null)
            {
                return uri.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <param name="strValue">新值</param>
        /// <param name="strSource">路径</param>
        public static void SaveXmlConfig(string strTarget, string strValue, string strSource)
        {
            string xmlPath = HttpContext.Current.Server.MapPath(strSource);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            elemList[0].InnerXml = strValue;
            xdoc.Save(xmlPath);
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM-dd hh:mm 刚刚更新)
        /// </summary>
        public static string getTimeSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (second < 0) { second = 0; }
            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return strTime;
        }


        /// <summary>
        /// 取得与当前时间的间隔(yyyy年MM月dd日 刚刚更新)
        /// </summary>
        public static string getTimeLEXYearSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("yyyy年MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 取得与当前时间的间隔(yy-MM-dd 刚刚更新)
        /// </summary>
        public static string getTimeEXYearSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("yyyy-MM-dd");
            }
            return strTime;
        }


        /// <summary>
        /// 取得与当前时间的间隔(MM-dd 刚刚更新)
        /// </summary>
        public static string getTimeEXSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                //strTime = second + "秒钟前";
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("MM-dd");
            }
            return strTime;
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM月dd日 刚刚更新)
        /// </summary>
        public static string getTimeEXPINSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM月dd日 刚刚)
        /// </summary>
        public static string getTimeEXTSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 获取统一时间格式
        /// </summary>
        /// <param name="time1">时间参数string类型</param>
        /// <returns></returns>
        public static string getDateTime(string time1)
        {
            DateTime time = Convert.ToDateTime(time1.ToString());
            return time.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 获取时间相隔天数
        /// </summary>
        /// <param name="time1">时间1</param>
        /// <returns></returns>
        public static string getDaySpan(DateTime time1)
        {
            TimeSpan ts = DateTime.Now - time1;
            return ts.Days.ToString();
        }

        /// <summary>
        /// 读取并返回一个文本文件的内容
        /// </summary>
        /// <param name="filePath">文件的物理路径</param>
        /// <returns></returns>
        public static string GetTextFileContent(string filePath)
        {
            string result = string.Empty;
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch
                { }
            }
            return result;
        }

        /// <summary>
        /// 替换文本中的空格和换行
        /// </summary>
        public static string ReplaceSpace(string str)
        {
            string s = str;
            s = s.Replace(" ", "&nbsp;");
            s = s.Replace("\n", "<BR />");
            return s;
        }

        /// <summary>
        /// 取得文件扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileEXT(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return "";
            }
            if (filename.IndexOf(@".") == -1)
            {
                return "";
            }
            int pos = -1;
            if (!(filename.IndexOf(@"\") == -1))
            {
                pos = filename.LastIndexOf(@"\");
            }
            string[] s = filename.Substring(pos + 1).Split('.');
            return s[1];
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="Target">节点</param>
        /// <param name="Path">配置文件的路径</param>
        /// <returns></returns>
        public static string GetXMLValue(string Target, string Path)
        {
            try
            {
                string XmlPath = HttpContext.Current.Server.MapPath(Path);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(XmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNode node = root.SelectSingleNode(Target);
                if (node != null)
                {
                    return node.InnerXml;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="xpath"></param>
        /// <param name="value"></param>
        static public void setXmlInnerText(string filepath, string xpath, string value)
        {
            XmlDocument xmldoc = new XmlDocument();
            string physicsPath = HttpContext.Current.Server.MapPath(filepath);
            xmldoc.Load(physicsPath);
            XmlNode node = xmldoc.SelectSingleNode(xpath);
            if (node != null)
            {
                node.InnerText = value;
                xmldoc.Save(physicsPath);
            }
        }

        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="path"></param>
        static public void CreatUserConfig(string path)
        {
            string npath = HttpContext.Current.Server.MapPath(path);
            if (!File.Exists(npath))
            {
                using (StreamWriter sw = new StreamWriter(npath, true))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<configuration>");
                    sw.WriteLine("<menulist></menulist>");
                    sw.WriteLine("<killuser>0</killuser>");
                    sw.WriteLine("<dynnumber>30</dynnumber>");
                    sw.WriteLine("<space>default</space>");
                    sw.WriteLine("</configuration>");
                }
            }
        }
        
        /// <summary>
        /// 得到生日
        /// </summary>
        /// <param name="birthday"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        static public string GetBirthday(DateTime birthday, int flag)
        {
            switch (flag)
            {
                case 0:
                    return birthday.ToString("yyyy年MM月dd日");
                case 1:
                    return birthday.ToString("MM月dd日");
                case 2:
                    return birthday.ToString("yyyy年");
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// 转换为 json
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        static public string toJson(Dictionary<string, string> dic)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("{");
            int i = 0;
            foreach (var kv in dic)
            {
                if (kv.Value.StartsWith("{")
                    || kv.Value.StartsWith("["))
                {
                    sb.AppendFormat("'{0}':{1}", kv.Key, kv.Value);
                }
                else
                {
                    sb.AppendFormat("'{0}':'{1}'", kv.Key, Regex.Replace(kv.Value, "'", "\'"));
                }
                if (i < dic.Count - 1)
                {
                    sb.Append(",");
                }
                i++;
            }
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 转换为json 数组
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        static public string toJsonArray(List<string> jsonList)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("[");
            int i = 0;
            foreach (string str in jsonList)
            {
                sb.Append(str);
                if (i < jsonList.Count - 1)
                {
                    sb.Append(",");
                }
                i++;
            }
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// 得到可见度
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static public string GetPrivacy(object k)
        {
            string listSTR = string.Empty;
            string[] ARR = { "全站用户可见", "仅好友可见", "仅自己可见" };
            for (int j = 0; j < ARR.Length; j++)
            {
                if (Convert.ToInt16(k) == j)
                {
                    listSTR += "<option value=\"" + j + "\" selected>" + ARR[j] + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + j + "\">" + ARR[j] + "</option>";
                }
            }
            return listSTR;
        }

        /// <summary>
        /// 得到可见度
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static public string GetPublics(object k)
        {
            string listSTR = string.Empty;
            string[] ARR = { "允许任何人加入该群", "需要群主审核", "不允许任何人加入该群" };
            for (int j = 0; j < ARR.Length; j++)
            {
                if (Convert.ToInt16(k) == j)
                {
                    listSTR += "<option value=\"" + j + "\" selected>" + ARR[j] + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + j + "\">" + ARR[j] + "</option>";
                }
            }
            return listSTR;
        }


        /// <summary>
        /// 返回性别样式
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        static public string ReturnSex(object obj)
        {
            if (obj.ToString() == "2")
            {
                return "sex_ico.png";
            }
            else
            {
                return "man";
            }
        }
    }
}
