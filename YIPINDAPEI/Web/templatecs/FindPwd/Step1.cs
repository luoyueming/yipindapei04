using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.templatecs.FindPwd
{
    public class Step1 : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "找回密码_女装搭配|衣品搭配");
            #endregion
        }
    }
}