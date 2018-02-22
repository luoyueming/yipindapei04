using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs.FindPwd
{
    public class Step3 : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "找回密码_女装搭配|衣品搭配");
            #endregion

            if (CheckIsLogin_HongRenHui.GetUserID("FINDRED_HREN_USERID") <= 0 || CookieManage_HongRenHui.GetCookieValue("FINDTEL_HREN_STATE") != "1")
            {
                context.Put("redirecturl", "/findpwd");
            }
        }
    }
}