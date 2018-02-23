using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs.UserLogin
{
    public class WeiXinLogin : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);


            #region  加载头部及Title
            NewXzc.Web.templatecs.Head head = new Head();
            head.Init_Head(context, 0);

            context.Put("title", "微信登录");
            context.Put("keywords", "红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜");
            context.Put("description", "衣品搭配,专注发掘不一样的红人神咖.衣品搭配是红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜等综合红人报道平台.衣品搭配网络红人聚集平台，跟踪报道红人资讯,最新红人作品等等,第一时间满足粉丝需求.");
            #endregion


            //string appid = "wx2fbe800be068f5ec";// NewXzc.Common.ConfigHelper.GetConfigString("weixin_appid");
            //string appsecret = "592edb2d54ec68eb935b3b731441cbf8";// NewXzc.Common.ConfigHelper.GetConfigString("weixin_AppSecret");
            //string recall_url = HttpContext.Current.Request.Url.ToString().ToLower();

            //string cur_host = HttpContext.Current.Request.Url.Host;
            //recall_url = "http://m.ypindapei.com/userlogin/wechat_return_url.aspx";

            //recall_url = HttpContext.Current.Server.UrlEncode(recall_url);

            //string return_url = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + recall_url + "&response_type=code&scope=snsapi_login&state=1#wechat_redirect";

            ////context.Put("redirecturl",return_url);

            //http://www.ypindapei.com/wechat?appid=wx2fbe800be068f5ec&scope=snsapi_login&state=1&redirect_uri=http%3a%2f%2fm.ypindapei.com%2fuserlogin%2fwechat_return_url.aspx&code=001HvRos1i2eer08Tjms1mB6ps1HvRod

            string returl_url = String_Manage.Return_Request_Str("redirect_uri").ToLower();
            string returl_code = String_Manage.Return_Request_Str("code");

            if (returl_url.Contains("m.ypindapei.com"))
            {
                if (returl_code != "")
                {
                    context.Put("redirecturl",returl_url+"?code="+returl_code);
                }
            }

        }
    }
}