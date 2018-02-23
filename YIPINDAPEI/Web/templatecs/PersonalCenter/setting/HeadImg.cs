using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.templatecs.PersonalCenter.Setting
{
    public class HeadImg : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            Model.RED_USER resumeModel = new BLL.RED_USER().GetModel(UserID_HongRenHui);
            string HeadImg = "/template/img/default_head/large_default.jpg";
            if (!string.IsNullOrEmpty(resumeModel.USER_HEAD))
            {
                HeadImg = NewXzc.Common.ImgHelper.Return_User_Head(resumeModel.USER_HEAD, 1);
            }
            context.Put("HeadImg", HeadImg);
        }
    }
}