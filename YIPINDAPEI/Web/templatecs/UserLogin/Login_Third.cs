using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs.UserLogin
{
    public class Login_Third : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "登录");
            context.Put("keywords", "红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜");
            context.Put("description", "红人汇,专注发掘不一样的红人神咖.红人汇是红人,红人汇,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜等综合红人报道平台.红人汇网络红人聚集平台，跟踪报道红人资讯,最新红人作品等等,第一时间满足粉丝需求.");
            #endregion


            //验证是否登录成功
            Login_States(context);
        }

        /// <summary>
        /// 验证是否登录成功
        /// </summary>
        /// <param name="context"></param>
        private void Login_States(NVelocity.VelocityContext context)
        {
            int login_type = String_Manage.Return_Request_Int("logintype",0);
            string urls = String_Manage.Return_Request_Str("urls");

            CookieManage_HongRenHui.AddCookie("RED_HREN_Login_Type", login_type.ToString(), 7, false);

            if (urls != "" && urls != "0")
            {
                CookieManage_HongRenHui.AddCookie("RED_HREN_Login_Urls", urls, 7, false);
            }
            else
            {
                CookieManage_HongRenHui.AddCookie("RED_HREN_Login_Urls", CheckIsLogin_HongRenHui.Get_User_CenterUrl_HongRenHui(), 7, false);
            }

            if (CheckIsLogin_HongRenHui.IsLogin("RED_HREN_USERID"))
            {
                if (urls != "")
                {
                    context.Put("redirecturl", urls);
                }
                else
                {
                    context.Put("redirecturl", CheckIsLogin_HongRenHui.Get_User_CenterUrl_HongRenHui());
                }
            }
        }
    }
}