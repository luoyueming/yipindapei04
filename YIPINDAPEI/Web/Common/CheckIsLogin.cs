using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using NewXzc.BLL;
using NewXzc.Common;

namespace NewXzc.Web.Common
{
    public class CheckIsLogin
    {
        private static int RED_USERID = 0;


        private static NewXzc.BLL.RED_USER user_bll_h = new NewXzc.BLL.RED_USER();

        private static NewXzc.BLL.RED_USER_LOGIN_RECORD login_bll = new RED_USER_LOGIN_RECORD();

        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        public static bool IsLogin()
        {
            bool f = true;

            try
            {
                if (CookieManage.IsHaveCookie())
                {
                    RED_USERID = Convert.ToInt32(CookieManage.GetCookieValue());
                    if (user_bll_h.Exists(RED_USERID))
                    {
                        //结束时间
                        DateTime end_time = CookieManage.GetCookieValue(2);

                        //判断是否已经过期
                        if (!TimeParser.IsMoreSevenDays(DateTime.Now, end_time))
                        {
                            f = false;
                        }
                        else
                        {
                            f = true;
                        }
                    }
                    else
                    {
                        f = false;
                    }
                }
                else
                {
                    f = false;
                }
            }
            catch (Exception ex)
            {
                f = false;
            }


            if (f)
            {
                #region 记录用户的登录和时间

                NewXzc.Model.RED_USER_LOGIN_RECORD model_logintime=new Model.RED_USER_LOGIN_RECORD();
                string ipAddress = RequestHelper.GetIP();
                int isLogin = login_bll.GetRecordCount(" userid="+RED_USERID+" and datediff(day,Login_Time,getdate())=0 ");

                string sessoinid = "0";
                try
                {
                    sessoinid = HttpContext.Current.Session.SessionID;
                }
                catch (Exception ex)
                {
                    
                }

                try
                {
                    if (isLogin<=0)
                    {
                        model_logintime.USERID = RED_USERID;
                        model_logintime.Login_IP = ipAddress;
                        model_logintime.SessionID = sessoinid;
                        model_logintime.Login_Time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        model_logintime.Remark = "";

                        int logintype = 1;

                        model_logintime.Login_Type = logintype;

                        login_bll.Add(model_logintime);
                    }
                    else
                    {
                        model_logintime.Login_IP = ipAddress;
                        model_logintime.Login_Time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        model_logintime.Remark = "";

                        int logintype = 1;

                        model_logintime.Login_Type = logintype;

                        login_bll.Update(model_logintime);
                    }
                }
                catch (Exception ex)
                {

                }
                #endregion
            }

            return f;
        }

        /// <summary>
        /// 判断当前cookie是否存在，应用于后台
        /// </summary>
        /// <param name="cookiename">名称</param>
        /// <returns></returns>
        public static bool IsLogin(string cookiename)
        {
            bool f = true;

            try
            {

                if (CookieManage.IsHaveCookie(cookiename))
                {
                    RED_USERID = Convert.ToInt32(CookieManage.GetCookieValue(cookiename));
                    if (user_bll_h.Exists(RED_USERID))
                    {
                        //结束时间
                        DateTime end_time = CookieManage.GetCookieValue(cookiename, 2);

                        //判断是否已经过期
                        if (!TimeParser.IsMoreSevenDays(DateTime.Now, end_time))
                        {
                            f = false;
                        }
                        else
                        {
                            f = true;
                        }
                    }
                    else
                    {
                        f = false;
                    }
                }
                else
                {
                    f = false;
                }
            }
            catch (Exception ex)
            {
                f = false;
            }

            return f;
        }

        /// <summary>
        /// 获取用户ID（变量名称为RED_USERID的COOKIE的值）
        /// </summary>
        /// <returns></returns>
        public static int GetUserID()
        {
            int uid = 0;

            if (IsLogin())
            {
                if (!String.IsNullOrEmpty(CookieManage.GetCookieValue()))
                {
                    uid = Convert.ToInt32(CookieManage.GetCookieValue());
                }
            }
            
            return uid;
        }

        
        /// <summary>
        /// 获取用户ID（指定变量名称的COOKIE的值）
        /// </summary>
        /// <returns></returns>
        public static int GetUserID(string cookiename)
        {
            int uid = 0;

            if (IsLogin(cookiename))
            {
                if (!String.IsNullOrEmpty(CookieManage.GetCookieValue(cookiename)))
                {
                    uid = Convert.ToInt32(CookieManage.GetCookieValue(cookiename));
                }
            }

            return uid;
        }


        /// <summary>
        /// 获取用户登录成功后，应该跳转到哪个页面
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static string Get_User_CenterUrl()
        {
            int uid = GetUserID();

            string result = "ok";

            result = "/people_c";

            return result;
        }

    }
}