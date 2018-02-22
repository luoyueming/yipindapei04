using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace NewXzc.Common
{
    public class CookieManage2_HongRenHui
    {
        private static string domin = StringPlus.ReturnDomain_HongRenHui();

        /// <summary>
        /// 创建Cookie
        /// </summary>
        /// <param name="cookiename">Cookie的变量名称</param>
        /// <param name="value">Cookie的值</param>
        /// <param name="expires">Cookie的过期时间，默认为1</param>
        /// <param name="isexpires">是否存储过期时间</param>
        public static void AddCookie(string cookiename, string value, int expires, bool isexpires)
        {
            //设置过期时间默认值
            //if (expires < 1 || string.IsNullOrEmpty(expires.ToString()))
            //{
            //    expires = 1;
            //}
            if (string.IsNullOrEmpty(expires.ToString()))
            {
                expires = 1;
            }

            //创建票证
            FormsAuthenticationTicket RED_USERID_ticket = new FormsAuthenticationTicket(1, value, DateTime.Now, DateTime.Now.AddDays(expires), true, domin, "/");
            //给票证加密
            var now_ticket = FormsAuthentication.Encrypt(RED_USERID_ticket);
            //创建cookie并给cookie赋值
            CookieHelper_HongRenHui.WriteCookie(MD5Tool.getMd5Hash(cookiename), now_ticket, domin);
            //判断是否有过期时间
            if (isexpires)
            {
                //CookieHelper_HongRenHui.WriteCookie("expires", expires.ToString(), domin);
                //CookieHelper_HongRenHui.WriteCookie(new string[] { MD5Tool.getMd5Hash(cookiename) }, expires);
                CookieHelper_HongRenHui.WriteCookie(MD5Tool.getMd5Hash(cookiename), now_ticket, expires, domin);
            }
        }


        /// <summary>
        /// 判断变量名称为所传参数的cookie是否存在
        /// </summary>
        /// <param name="cookiename">cookie的变量的名称</param>
        /// <returns></returns>
        public static bool IsHaveCookie(string cookiename)
        {
            bool f = true;
            //获取当前COOKIE
            var user_cookie = CookieHelper_HongRenHui.GetCookie(MD5Tool.getMd5Hash(cookiename));
            //登录票证
            if (string.IsNullOrEmpty(user_cookie))
            {
                f = false;
            }

            return f;
        }

        /// <summary>
        /// 使变量名称为所传参数的cookie过期
        /// </summary>
        /// <param name="cookiename">cookie的变量的名称</param>
        public static void ClearCookie(string cookiename)
        {
            CookieHelper_HongRenHui.ClearUserCookie(MD5Tool.getMd5Hash(cookiename), domin);
        }

        /// <summary>
        /// 获取变量名称为所传参数的cookie的值
        /// </summary>
        /// <param name="cookiename">cookie的变量的名称</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            string value = "0";
            try
            {
                if (IsHaveCookie(cookiename))
                {
                    //获取当前COOKIE
                    var user_cookie = CookieHelper_HongRenHui.GetCookie(MD5Tool.getMd5Hash(cookiename));
                    //登录票证
                    var user_ticket = FormsAuthentication.Decrypt(user_cookie);

                    //获取变量名称为RED_USERID的cookie的值
                    value = user_ticket.Name;
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }

        /// <summary>
        /// 获取变量名称为所传参数的cookie的开始时间或结束时间，1：开始时间，2：结束时间
        /// </summary>
        /// <param name="cookiename">cookie的变量的名称</param>
        /// <param name="num">1：开始时间，2：结束时间</param>
        /// <returns></returns>
        public static DateTime GetCookieValue(string cookiename, int num)
        {
            DateTime value = DateTime.Now;
            try
            {
                if (IsHaveCookie(cookiename))
                {
                    //获取当前COOKIE
                    var user_cookie = CookieHelper_HongRenHui.GetCookie(MD5Tool.getMd5Hash(cookiename));
                    //登录票证
                    var user_ticket = FormsAuthentication.Decrypt(user_cookie);

                    if (num == 1)
                    {
                        //变量名称为RED_USERID的开始时间
                        value = user_ticket.IssueDate;
                    }
                    else
                    {
                        //变量名称为RED_USERID的过期时间
                        value = user_ticket.Expiration;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return value;
        }
    }
}