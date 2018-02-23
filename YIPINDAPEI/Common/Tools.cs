using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Text.RegularExpressions;


namespace NewXzc.Common
{
    public class Tools
    {
        /// <summary>
        /// 获取允许上传文件类型 
        /// </summary>
        /// <param name="path">当前文件的路径</param>
        /// <returns>允许上传类型数组</returns>
        public static string[] GetFileType(string Path)
        {
            string[] filetype;
            if (Cache.GetCache("filetype") == null)
            {
                XmlDocument domWebConfig = new XmlDocument();
                domWebConfig.Load((HttpContext.Current.Server.MapPath(Path)));
                XmlNode node = domWebConfig.SelectSingleNode("/configuration/appSettings/add[@key='filetype']");
                filetype = node.Attributes["value"].Value.Split(',');
                Cache.SetCache("filetype", filetype);
            }
            else
            {
                filetype = (string[])Cache.GetCache("filetype");
            }
            return filetype;
        }

        /// <summary>
        /// 删除文件文件或图片
        /// </summary>
        /// <param name="path">当前文件的路径</param>
        /// <returns>是否删除成功</returns>
        public static bool FilePicDelete(string path)
        {
            bool ret = false;
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                ret = true;
            }
            return ret;
        }

        /// <summary>
        /// 验证Email格式, 是 返回true,否 返回false
        /// </summary>
        /// <param name="strEmail"></param>
        /// <returns></returns>
        public static bool isEmail(string strEmail)
        {
            //return Regex.IsMatch(strEmail, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        //////////////////




        /// <summary>
        /// 是否是数字
        /// </summary>
        /// <param name="num">数字字符串</param>
        /// <returns>是 返回 true，否 返回 false</returns>
        public static bool isNumbers(string num)
        {
            return Regex.IsMatch(num, @"^[0-9]*[1-9][0-9]*$");
        }

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
        /// 判断是否是数字   
        /// </summary>   
        /// <param name="str">字符串</param>   
        /// <returns>bool</returns>   
        public static bool IsNumeric64(string str)
        {
            if (str == null || str.Length == 0)
            {
                return false;
            }

            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            byte[] bytestr = ascii.GetBytes(str);
            foreach (byte c in bytestr)
            {
                if (c < 48 || c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric32(string expression)
        {
            if (expression != null)
            {
                string str = expression;
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 解密前台escape()后的字符串
        /// </summary>
        public static string UnescapeString(string str)
        {
            return HttpUtility.HtmlEncode(Microsoft.JScript.GlobalObject.unescape(str));
        }

        /// <summary>
        /// 按长度截取字符串
        /// </summary>
        /// <param name="theValue">字符串</param>
        /// <param name="len">长度</param>        
        public static string SubStr(string theValue, int len)
        {
            string str = "";

            if (!string.IsNullOrEmpty(theValue))
            {
                if (theValue.Length > len)
                {
                    str = theValue.Substring(0, len) + "...";
                }
                else
                {
                    str = theValue;
                }
            }
            return str;
        }

        /// <summary>
        /// 判断当前字符串是否为空，如果将其转换为数字是否有错，并将字符串转换为哪种类型，0：int，1：decimal
        /// </summary>
        /// <param name="val">当前字符串</param>
        /// <param name="isnum">是否转换为数字，是：true，否：false</param>
        /// <param name="type">将字符串转换为哪种类型，0：int，1：decimal</param>
        /// <returns></returns>
        public static string IsNull_Default(string val, bool isnum,int type)
        {
            if (isnum)
            {
                if (type == 0)//int
                {
                    try
                    {
                        int cnt = Convert.ToInt32(val);
                    }
                    catch (Exception ex)
                    {
                        val = "0";
                    }
                }
                else//decimal
                {
                    try
                    {
                        decimal cnt = Convert.ToDecimal(val);
                    }
                    catch (Exception ex)
                    {
                        val = "0";
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(val))
                {
                    val = "";
                }
            }

            return val;
        }

    }
}
