using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs.UserLogin
{
    public class LogOut : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            int t = 0;

            t = String_Manage.Return_Request_Int("t", 0);

            context.Put("t", t);

            int logintype = 0;
            int third_logintype = 1;

            try
            {
                if (CookieManage_HongRenHui.IsHaveCookie("RED_HREN_USERID"))
                {
                    logintype = Convert.ToInt32(CookieManage_HongRenHui.GetCookieValue("RED_HREN_Login_Type"));
                    third_logintype = Convert.ToInt32(CookieManage_HongRenHui.GetCookieValue("RED_HREN_Third_Login"));


                    CookieManage_HongRenHui.AddCookie("RED_HREN_USERID", CookieManage_HongRenHui.GetCookieValue("RED_HREN_USERID"), -1, true);
                    CookieManage_HongRenHui.AddCookie("FINDRED_HREN_USERID", CookieManage_HongRenHui.GetCookieValue("FINDRED_HREN_USERID"), -1, true);
                    CookieManage_HongRenHui.AddCookie("FINDTEL_HREN_STATE", CookieManage_HongRenHui.GetCookieValue("FINDTEL_HREN_STATE"), -1, true);
                    CookieManage_HongRenHui.AddCookie("RED_HREN_Login_Type", CookieManage_HongRenHui.GetCookieValue("RED_HREN_Login_Type"), -1, true);
                    CookieManage_HongRenHui.AddCookie("RED_HREN_Third_Login", CookieManage_HongRenHui.GetCookieValue("RED_HREN_Third_Login"), -1, true);
                    CookieManage_HongRenHui.AddCookie("RED_HREN_Login_Urls", CookieManage_HongRenHui.GetCookieValue("RED_HREN_Login_Urls"), -1, true);


                    CookieManage_HongRenHui.ClearCookie("RED_HREN_USERID");
                    CookieManage_HongRenHui.ClearCookie("FINDRED_HREN_USERID");
                    CookieManage_HongRenHui.ClearCookie("FINDTEL_HREN_STATE");
                    CookieManage_HongRenHui.ClearCookie("RED_HREN_Login_Type");
                    CookieManage_HongRenHui.ClearCookie("RED_HREN_Third_Login");
                    CookieManage_HongRenHui.ClearCookie("RED_HREN_Login_Urls");


                    HttpCookie aCookie;
                    string cookieName;
                    int limit = HttpContext.Current.Request.Cookies.Count;
                    for (int i = 0; i < limit; i++)
                    {
                        cookieName = HttpContext.Current.Request.Cookies[i].Name;
                        if (!cookieName.ToLower().Contains("redparty"))
                        {
                            aCookie = new HttpCookie(cookieName);
                            aCookie.Expires = DateTime.Now.AddDays(-1);
                            HttpContext.Current.Response.Cookies.Add(aCookie);
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }


            context.Put("logintype", logintype);
            context.Put("third_logintype", third_logintype);

        }
    }
}