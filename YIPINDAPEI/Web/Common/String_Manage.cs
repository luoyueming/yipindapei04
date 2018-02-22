using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Common;

namespace NewXzc.Web.Common
{
    public class String_Manage
    {
        /// <summary>
        /// 对字符串进行解码、通过xss消除sql注入中的关键字，name：需要进行操作的request所对应的变量名称
        /// </summary>
        /// <param name="name">需要进行操作的request所对应的变量名称</param>
        /// <returns></returns>
        public static string Return_Request_Str(string name)
        {
            string str = "";
            try
            {
                str = HttpContext.Current.Request[name].ToString();

                if (!string.IsNullOrEmpty(str))
                {
                    str = HttpContext.Current.Server.UrlDecode(str);
                    str = StringHelper.XSSFilter(str);
                }
                else
                {
                    str = "";
                }
            }
            catch (Exception ex)
            {

            }
            return str;
        }

        /// <summary>
        /// 将字符串转换为数字类型，name：需要进行操作的request所对应的变量名称，val：如果当前变量的值为空，指定的返回值
        /// </summary>
        /// <param name="name">需要进行操作的request所对应的变量名称</param>
        /// <param name="val">如果当前变量的值为空，指定的返回值</param>
        /// <returns></returns>
        public static int Return_Request_Int(string name, int val)
        {
            int cnt = val;

            try
            {
                string str = HttpContext.Current.Request[name].ToString();
                if (!string.IsNullOrEmpty(str))
                {
                    try
                    {
                        cnt = Convert.ToInt32(str);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return cnt;
        }

        /// <summary>
        /// 将字符串转换为数字类型，name：需要进行操作的字符串，val：如果当前变量的值为空，指定的返回值
        /// </summary>
        /// <param name="name">需要进行操作的字符串</param>
        /// <param name="val">如果当前变量的值为空，指定的返回值</param>
        /// <returns></returns>
        public static int Return_Int(string name, int val)
        {
            int cnt = val;
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    cnt = Convert.ToInt32(name);
                }
                catch (Exception ex)
                {

                }
            }

            return cnt;
        }

        /// <summary>
        /// 将字符串转换为数字类型，name：需要进行操作的字符串，val：如果当前变量的值为空，指定的返回值
        /// </summary>
        /// <param name="name">需要进行操作的字符串</param>
        /// <param name="val">如果当前变量的值为空，指定的返回值</param>
        /// <returns></returns>
        public static string Return_Str(string name, string val)
        {
            string str = name;
            if (string.IsNullOrEmpty(str))
            {
                str = val;
            }
            return str;
        }
    }
}