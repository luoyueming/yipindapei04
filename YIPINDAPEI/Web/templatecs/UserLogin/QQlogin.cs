﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewXzc.Web.templatecs.UserLogin
{
    public class QQlogin:Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);


            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "QQ登录");
            context.Put("keywords", "红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜");
            context.Put("description", "红人汇,专注发掘不一样的红人神咖.红人汇是红人,红人汇,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜等综合红人报道平台.衣品搭配网络红人聚集平台，跟踪报道红人资讯,最新红人作品等等,第一时间满足粉丝需求.");
            #endregion

        }
    }
}