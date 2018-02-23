using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NewXzc.Common.DEncrypt;

namespace NewXzc.Common
{
    public class CookieHelper
    {
        CookieHelper()
        {

        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 返回密码密文
        /// </summary>
        /// <param name="password">密码明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string SetCookiePassword(string password, string key)
        {
            //			if (password.Length < 32)
            //			{
            //				password = password.PadRight(32);
            //			}
            return DESEncrypt.Encrypt(password, key);
        }
        /// <summary>
        /// 获得管理员Cookei值
        /// </summary>
        /// <param name="strName">项</param>
        /// <returns>值</returns>
        public static string GetCookieForManager(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies["InRedD6Manager"] != null
                && HttpContext.Current.Request.Cookies["InRedD6Manager"][strName] != null)
            {
                return UrlDecode(HttpContext.Current.Request.Cookies["InRedD6Manager"][strName].ToString());
            }
            return "";
        }
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <param name="strValue">值</param>
        /// <param name="Domain">域名</param>
        public static void WriteCookie(string strName, string strValue, string Domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["REDParty"];

            if (cookie == null)
            {
                cookie = new HttpCookie("REDParty");
                cookie.Values[strName] = UrlEncode(strValue);
            }
            else
            {
                cookie.Values[strName] = UrlEncode(strValue);
                if (HttpContext.Current.Request.Cookies["REDParty"]["expires"] != null)
                {
                    int expires = StrToInt(HttpContext.Current.Request.Cookies["REDParty"]["expires"].ToString(), 0);
                    if (expires > 0)
                    {
                        cookie.Expires = DateTime.Now.AddDays(StrToInt(HttpContext.Current.Request.Cookies["REDParty"]["expires"].ToString(), 0));
                    }
                }
            }

            string cookieDomain = Domain;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <param name="strValue">值</param>
        /// <param name="expires">有效期</param>
        /// <param name="Domain">域名</param>
        public static void WriteCookie(string strName, string strValue, int expires, string Domain)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies["REDParty"];

            if (cookie == null)
            {
                cookie = new HttpCookie("REDParty");
                cookie.Values[strName] = UrlEncode(strValue);
            }
            else
            {
                cookie.Values[strName] = UrlEncode(strValue);

                if (expires > 0)
                {
                    cookie.Expires = DateTime.Now.AddDays(expires);
                }
                else
                {
                    cookie.Expires = DateTime.Now.AddDays(1);
                }
            }

            string cookieDomain = Domain;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;

            HttpContext.Current.Response.AppendCookie(cookie);

        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="parameter">参数列表</param>
        /// <param name="expires">有效期</param>
        public static void WriteCookie(string[] parameter, int expires)
        {
            HttpCookie cookie = new HttpCookie("REDParty");
            foreach (string tmp in parameter)
            {
                string[] strtmp = tmp.Split(',');
                cookie.Values[strtmp[0]] = strtmp[1];
            }

            if (expires > 0)
            {
                cookie.Expires = DateTime.Now.AddDays(expires);
            }

            string cookieDomain = "redd6party.com";
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
            {
                cookie.Domain = cookieDomain;
            }
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得cookie值
        /// </summary>
        /// <param name="strName">项</param>
        /// <returns>值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null
                && HttpContext.Current.Request.Cookies["REDParty"] != null
                && HttpContext.Current.Request.Cookies["REDParty"][strName] != null)
            {
                return UrlDecode(HttpContext.Current.Request.Cookies["REDParty"][strName].ToString());
            }
            return "";
        }

        /// <summary>
        /// 清除964455登录用户的cookie
        /// </summary>
        public static void ClearUserCookie(string cookieName, string Domain)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-2);
            string cookieDomain = Domain;
            if (cookieDomain != string.Empty && HttpContext.Current.Request.Url.Host.IndexOf(cookieDomain) > -1 && IsValidDomain(HttpContext.Current.Request.Url.Host))
                cookie.Domain = cookieDomain;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object Expression, int defValue)
        {

            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return Convert.ToInt32(str);
                    }
                }
            }
            return defValue;
        }

        /// <summary>
        /// 是否为有效域
        /// </summary>
        /// <param name="host">域名</param>
        /// <returns></returns>
        public static bool IsValidDomain(string host)
        {
            Regex r = new Regex(@"^\d+$");
            if (host.IndexOf(".") == -1)
            {
                return false;
            }
            return r.IsMatch(host.Replace(".", string.Empty)) ? false : true;
        }
    }
}
