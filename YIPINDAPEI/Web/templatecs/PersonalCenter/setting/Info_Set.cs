using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Common;

namespace NewXzc.Web.templatecs.PersonalCenter.Setting
{
    public class Info_Set : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "基础资料_个人中心");
            context.Put("keywords", "红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜");
            context.Put("description", "衣品搭配,专注发掘不一样的红人神咖.衣品搭配是红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜等综合红人报道平台.衣品搭配网络红人聚集平台，跟踪报道红人资讯,最新红人作品等等,第一时间满足粉丝需求.");
            #endregion

            #region  企业基础资料
            Model.RED_USER user_model = new BLL.RED_USER().GetModel(UserID_HongRenHui);
            if (user_model != null)
            {
                string user_head = user_model.USER_HEAD;
                context.Put("comlogo", user_head);
                context.Put("comlogo_small", ImgHelper.Return_User_Head(user_head, 3));
                context.Put("comlogo_middle", ImgHelper.Return_User_Head(user_head, 2));
                context.Put("comlogo_big", ImgHelper.Return_User_Head(user_head, 1));
                context.Put("nickname", user_model.USERNAME);
            }
            else
            {
                context.Put("redirecturl", "/404");
            }
            #endregion
        }
    }
}