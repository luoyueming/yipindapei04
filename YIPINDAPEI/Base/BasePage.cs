using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using NewXzc.Common;

namespace Base
{
    public class BasePage : IHandlerFactory
    {
        /// <summary>
        /// 当前登录用户ID
        /// </summary>
        protected int UserID = 0;
        protected int UserID_HongRenHui = 0;
        string cookiename = "RED_HREN_USERID";

        NewXzc.BLL.RED_USER user_bll_h = new NewXzc.BLL.RED_USER();

        public BasePage()
        {
            string url_s = HttpContext.Current.Request.Url.ToString().ToLower();

            if (!url_s.Contains("userlogin"))
            {
                if (!url_s.Contains("hongrenhui"))
                {
                    //当前用户还未登录
                    if (GetCurUserID_HongRenHui(cookiename) == 0)
                    {
                        //进入中心页面，直接跳转回登录页
                        if (url_s.Contains("personalcenter"))
                        {
                            HttpContext.Current.Response.Redirect("/login");
                        }
                    }
                }
                else
                {
                    //当前用户还未登录
                    if (GetCurUserID_HongRenHui(cookiename) == 0)
                    {
                        //进入中心页面，直接跳转回登录页
                        if (url_s.Contains("personalcenter"))
                        {
                            HttpContext.Current.Response.Redirect("/login");
                        }
                    }
                }
            }

            if (!url_s.Contains("hongrenhui"))
            {
                GetUserID_HongRenHui(cookiename);
            }
            else
            {
                GetUserID_HongRenHui(cookiename);
            }


        }

        #region  所有页面的头部的验证及用户登录状态的初始化

        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        public bool Is_Logins()
        {
            int userguid = 0;
            bool f = true;
            try
            {
                if (CookieManage2.IsHaveCookie())
                {
                    userguid = Convert.ToInt32(CookieManage2.GetCookieValue());
                    if (user_bll_h.Exists(userguid))
                    {
                        //结束时间
                        DateTime end_time = CookieManage2.GetCookieValue(2);

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
        /// 获取当前登录用户的ID
        /// </summary>
        /// <returns></returns>
        private int GetCurUserID()
        {
            int user_id = 0;

            if (Is_Logins())
            {
                if (!String.IsNullOrEmpty(CookieManage2.GetCookieValue()))
                {
                    user_id = Convert.ToInt32(CookieManage2.GetCookieValue());
                }
            }
            return user_id;
        }

        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        private bool Is_Login()
        {
            int userguid = 0;
            bool f = true;
            if (CookieManage2.IsHaveCookie())
            {
                userguid = Convert.ToInt32(CookieManage2.GetCookieValue());
                if (user_bll_h.Exists(userguid))
                {
                    //结束时间
                    DateTime end_time = CookieManage2.GetCookieValue(2);

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

            return f;
        }


        /// <summary>
        /// 取得当前登录ID
        /// </summary>
        /// <returns></returns>
        public void GetUserID()
        {
            if (Is_Login())
            {
                if (!String.IsNullOrEmpty(CookieManage2.GetCookieValue()))
                {
                    UserID = Convert.ToInt32(CookieManage2.GetCookieValue());
                }
            }
        }

        #endregion




        #region  最新项目（红人汇）

        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        public bool Is_Logins_HongRenHui(string cookiename)
        {
            int userguid = 0;
            bool f = true;
            try
            {
                if (CookieManage2_HongRenHui.IsHaveCookie(cookiename))
                {
                    userguid = Convert.ToInt32(CookieManage2_HongRenHui.GetCookieValue(cookiename));
                    if (user_bll_h.Exists(userguid))
                    {
                        //结束时间
                        DateTime end_time = CookieManage2_HongRenHui.GetCookieValue(cookiename, 2);

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
        /// 获取当前登录用户的ID
        /// </summary>
        /// <returns></returns>
        private int GetCurUserID_HongRenHui(string cookiename)
        {
            int user_id = 0;

            if (Is_Logins_HongRenHui(cookiename))
            {
                if (!String.IsNullOrEmpty(CookieManage2_HongRenHui.GetCookieValue(cookiename)))
                {
                    user_id = Convert.ToInt32(CookieManage2_HongRenHui.GetCookieValue(cookiename));
                }
            }
            return user_id;
        }

        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        private bool Is_Login_HongRenHui(string cookiename)
        {
            int userguid = 0;
            bool f = true;
            if (CookieManage2_HongRenHui.IsHaveCookie(cookiename))
            {
                userguid = Convert.ToInt32(CookieManage2_HongRenHui.GetCookieValue(cookiename));
                if (user_bll_h.Exists(userguid))
                {
                    //结束时间
                    DateTime end_time = CookieManage2_HongRenHui.GetCookieValue(cookiename, 2);

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

            return f;
        }


        /// <summary>
        /// 取得当前登录ID
        /// </summary>
        /// <returns></returns>
        public void GetUserID_HongRenHui(string cookiename)
        {
            if (Is_Login_HongRenHui(cookiename))
            {
                if (!String.IsNullOrEmpty(CookieManage2_HongRenHui.GetCookieValue(cookiename)))
                {
                    UserID_HongRenHui = Convert.ToInt32(CookieManage2_HongRenHui.GetCookieValue(cookiename));
                }
            }
        }

        #endregion

        public virtual void Page_Load(ref NVelocity.VelocityContext context)
        {

        }

        public virtual void Page_PostBack(ref NVelocity.VelocityContext context)
        {

        }
        /// <summary>
        /// 获得QeryString参数STRING
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>`
        protected string GetQueryString(string key)
        {
            string val = HttpContext.Current.Request.QueryString[key];
            if (null == val) return string.Empty;
            return val;
        }

        /// <summary>
        /// 获得QeryString参数INT
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected int GetQueryInt(string key, int defValue)
        {
            string val = HttpContext.Current.Request.QueryString[key];


            if (null == val) { return defValue; }
            int.TryParse(val, out defValue);
            return Convert.ToInt32(NewXzc.Common.StringHelper.XSSFilter(NewXzc.Common.StringHelper.SqlFilter(NewXzc.Common.Input.URLDecode(defValue.ToString()))));
        }


        /// <summary>
        /// 获得QeryString参数DATETIEM
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected DateTime GetQueryDateTime(string key, DateTime defValue)
        {
            string val = HttpContext.Current.Request.QueryString[key];
            if (null == val) { return defValue; }
            DateTime.TryParse(val, out defValue);
            return defValue;
        }

        /// <summary>
        /// 获得Form参数 STRING
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetFormString(string key)
        {
            string val = HttpContext.Current.Request.Form[key];
            if (null == val) return string.Empty;
            return val;
        }

        /// <summary>
        /// 获得Form参数INT
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        protected int GetFormInt(string key, int defValue)
        {
            string val = HttpContext.Current.Request.Form[key];
            if (null == val) { return defValue; }
            int.TryParse(val, out defValue);
            return Convert.ToInt32(NewXzc.Common.StringHelper.XSSFilter(NewXzc.Common.StringHelper.SqlFilter(NewXzc.Common.Input.URLDecode(defValue.ToString()))));
        }

        /// <summary>
        /// 获得Form参数DateTime
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        protected DateTime GetFormDateTime(string key, DateTime defValue)
        {
            string val = HttpContext.Current.Request.Form[key];
            if (null == val) { return defValue; }
            DateTime.TryParse(val, out defValue);
            return defValue;
        }

        /// <summary>
        /// 获得参数，Query和Form通用 STRING
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetString(string key)
        {
            string val = GetQueryString(key);
            if (string.IsNullOrEmpty(val))
            {
                val = GetFormString(key);
            }
            val = NewXzc.Common.StringHelper.XSSFilter(NewXzc.Common.StringHelper.SqlFilter(NewXzc.Common.Input.URLDecode(val)));
            return val;
        }

        /// <summary>
        /// 获得参数，Query和Form通用 INT
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected int GetInt(string key, int defValue)
        {
            int val = GetQueryInt(key, defValue);
            if (val == defValue)
            {
                val = GetFormInt(key, defValue);
            }
            return val;
        }

        /// <summary>
        /// 获得参数，Query和Form通用 DateTime
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected DateTime GetDateTime(string key, DateTime defValue)
        {
            DateTime val = GetQueryDateTime(key, defValue);
            if (val == defValue)
            {
                val = GetFormDateTime(key, defValue);
            }
            return val;
        }


        public string GetGenerTime(object time)
        {
            DateTime dt = Convert.ToDateTime(time);
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }


        /// <summary>
        /// 获取头像路径
        /// </summary>
        public string GetHeadImage(string user_path, int type)
        {

            return "头像路径";
        }

        public void OutText(object s)
        {
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
    }
}
