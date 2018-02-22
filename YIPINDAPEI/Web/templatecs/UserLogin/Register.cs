using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.templatecs.UserLogin
{
    public class Register : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "注册");
            context.Put("keywords", "注册衣品搭配");
            context.Put("description", "注册衣品搭配");
            #endregion
        }
    }
}