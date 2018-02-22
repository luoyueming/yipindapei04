using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs.FindPwd
{
    public class Step2 : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "找回密码_女装搭配|衣品搭配");
            #endregion

            int uid = CheckIsLogin_HongRenHui.GetUserID("FINDRED_HREN_USERID");

            if (uid <= 0)
            {
                context.Put("redirecturl", "/findpwd");
            }
            else
            {

                context.Put("codes",String_Manage.Return_Request_Str("code"));

                string tel = new BLL.RED_USER().GetModel(uid).TEL;

                //tel = tel.Substring(0, 3) + "****" + tel.Substring(7, 4);

                context.Put("tel", tel);
            }
        }
    }
}