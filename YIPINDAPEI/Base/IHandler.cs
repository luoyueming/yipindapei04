using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NVelocity.App;
using NVelocity;
using System.Web;
using System.Web.SessionState;
using System.IO;
using NewXzc.Common;
using System.Reflection;

namespace Base
{
    public class IHandler : IHttpHandler, IRequiresSessionState
    {
        NewXzc.BLL.RED_USER user_bll_h = new NewXzc.BLL.RED_USER();

        public void ProcessRequest(HttpContext context)
        {



            //context.Response.Write("1");
            //IPage fac=(IPage)Assembly.Load("NewXzc.Web").CreateInstance("NewXzc.Web.Index",true);
            //fac.Page_Load(context);
            #region
            string physicalApplicationPath = context.Request.PhysicalApplicationPath;
            string str4 = context.Request.PhysicalPath.Replace(physicalApplicationPath, string.Empty).Replace("/", ".").Replace(@"\", ".").Replace(".aspx", string.Empty);
            string name = str4.Replace(".", "/") + ".html";
            string str6 = string.Empty;

            //新添加测试
            //if (str4.ToLower() == "default")
            //{
            //    HttpContext.Current.Response.Redirect("/index");
            //    return;
            //}
            //测试结束


            if (str4.ToLower() == "index" || str4.ToLower() == "webscan_360_cn" || str4.ToLower() == "baidu_verify_kyltyss6um")
                str6 = "";
            else
                str6 = "template";
            if (str4.ToLower() == "tools.ueditor.dialogs.image.image")
            {
                str6 = "";
                str4 = "image";
            }
            else if (str4.ToLower() == "tools.ueditor.dialogs.link.link")
            {
                str6 = "";
                str4 = "link";
            }
            else if (str4.ToLower() == "tools.ueditor.dialogs.emotion.emotion")
            {
                str6 = "";
                str4 = "emotion";
            }
            else
                str6 = "template";
            string str8 = "text/html";
            string str9 = context.Request.ServerVariables["HTTP_ACCEPT"];
            VelocityEngine engine = new VelocityEngine();
            engine.SetProperty("resource.loader", "file");
            engine.SetProperty("file.resource.loader.path", physicalApplicationPath + str6 + @"\");
            engine.Init();
            VelocityContext ctx = new VelocityContext();
            string applicationPath = HttpContext.Current.Request.ApplicationPath;
            if (applicationPath == "/")
            {
                applicationPath = string.Empty;
            }

            #region  获取当前用户的登录状态
            string url_s = HttpContext.Current.Request.Url.ToString().ToLower();

            if (!url_s.Contains("hongrenhui"))
            {
                ctx.Put("headinfo", GetUserLoginState(1));
            }
            else
            {
                ctx.Put("headinfo", GetUserLoginState(1));
            }



            if (!url_s.Contains("userlogin"))
            {
                if (!url_s.Contains("hongrenhui"))
                {
                    //当前用户还未登录
                    if (GetCurUserID_HongRenHui() == 0)
                    {
                        //进入中心页面，直接跳转回登录页
                        if (url_s.Contains("personalcenter"))
                        {
                            HttpContext.Current.Response.Redirect("/login");
                        }
                    }
                }
                else
                {
                    //当前用户还未登录
                    if (GetCurUserID_HongRenHui() == 0)
                    {
                        //进入中心页面，直接跳转回登录页
                        if (url_s.Contains("personalcenter"))
                        {
                            HttpContext.Current.Response.Redirect("/login");
                        }
                    }
                }


            }
            #endregion

            ctx.Put("style", "/template/style");
            ctx.Put("js", "/template/js");
            ctx.Put("img", "/template/img");
            ctx.Put("exname", ".aspx");
            ctx.Put("showimg", NewXzc.Common.ImgHelper.GetCofigShowUrl());
            IHandlerFactory factory = null;
            try
            {
                factory = (IHandlerFactory)Assembly.Load("NewXzc.Web").CreateInstance("NewXzc.Web.templatecs." + str4, true);

                if (factory == null)
                {
                    //name = "/index";
                    name = "/404";
                }
                else if (!(!(context.Request.HttpMethod == "POST")))
                {
                    factory.Page_PostBack(ref ctx);
                }
                else
                {
                    factory.Page_Load(ref ctx);
                }
            }
            catch (Exception ex)
            {
                //context.Response.Redirect("/404");
                context.Response.Write(ex.Message);
            }
            if (ctx.Get("redirecturl") != null)
            {
                string url = ctx.Get("redirecturl").ToString();
                context.Response.Redirect(url);
            }
            try
            {

                Template template = engine.GetTemplate(name);

                StringWriter writer2 = new StringWriter();
                template.Merge(ctx, writer2);
                context.Response.ContentType = str8;
                context.Response.Write(writer2.GetStringBuilder().ToString());
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
            #endregion

        }


        #region  所有页面的头部的验证及用户登录状态的初始化



        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        public bool Is_Logins()
        {
            int userguid = 0;
            bool f = true;
            try
            {
                if (CookieManage2.IsHaveCookie())
                {
                    userguid = Convert.ToInt32(CookieManage2.GetCookieValue());
                    if (user_bll_h.Exists(userguid))
                    {
                        //结束时间
                        DateTime end_time = CookieManage2.GetCookieValue(2);

                        //判断是否已经过期
                        if (!TimeParser.IsMoreSevenDays(DateTime.Now, end_time))
                        {
                            f = false;
                        }
                        else
                        {
                            f = true;
                        }
                    }
                    else
                    {
                        f = false;
                    }
                }
                else
                {
                    f = false;
                }
            }
            catch (Exception ex)
            {
                f = false;
            }

            return f;
        }

        /// <summary>
        /// 获取当前登录用户的ID，红人议会
        /// </summary>
        /// <returns></returns>
        private int GetCurUserID()
        {
            int user_id = 0;

            if (Is_Logins())
            {
                if (!String.IsNullOrEmpty(CookieManage2.GetCookieValue()))
                {
                    user_id = Convert.ToInt32(CookieManage2.GetCookieValue());
                }
            }
            return user_id;
        }



        /// <summary>
        /// 判断是否登录成功
        /// </summary>
        /// <returns></returns>
        public bool Is_Logins_HongRenHui(string cookiename)
        {
            int userguid = 0;
            bool f = true;

            try
            {
                if (CookieManage2_HongRenHui.IsHaveCookie(cookiename))
                {
                    userguid = Convert.ToInt32(CookieManage2_HongRenHui.GetCookieValue(cookiename));
                    if (user_bll_h.Exists(userguid))
                    {
                        //结束时间
                        DateTime end_time = CookieManage2_HongRenHui.GetCookieValue(cookiename, 2);

                        //判断是否已经过期
                        if (!TimeParser.IsMoreSevenDays(DateTime.Now, end_time))
                        {
                            f = false;
                        }
                        else
                        {
                            f = true;
                        }
                    }
                    else
                    {
                        f = false;
                    }
                }
                else
                {
                    f = false;
                }
            }
            catch (Exception ex)
            {
                f = false;
            }

            return f;
        }

        /// <summary>
        /// 获取当前登录用户的ID，最新项目（红人汇）
        /// </summary>
        /// <returns></returns>
        private int GetCurUserID_HongRenHui()
        {
            int user_id = 0;

            string cookiename = "RED_HREN_USERID";

            if (Is_Logins_HongRenHui(cookiename))
            {
                if (!String.IsNullOrEmpty(CookieManage2_HongRenHui.GetCookieValue(cookiename)))
                {
                    user_id = Convert.ToInt32(CookieManage2_HongRenHui.GetCookieValue(cookiename));
                }
            }
            return user_id;
        }



        /// <summary>
        /// 获取当前用户的登录状态
        /// </summary>
        /// <param name="type">0：Head.html，1：HongRenHui下的Head.html</param>
        /// <returns></returns>
        private string GetUserLoginState(int type)
        {
            StringBuilder head_html = new StringBuilder();

            var http_url = "javascript:void(0);";

            int user_id = 0;

            if (type == 0)
            {
                user_id = GetCurUserID();
            }
            else if (type == 1)
            {
                user_id = GetCurUserID_HongRenHui();
            }

            if (user_id == 0)
            {
                if (type == 0)
                {
                    #region  类型0
                    head_html.AppendFormat("<div class=\"login-box\">");
                    head_html.AppendFormat("<a href=\"/userlogin/login\" title=\"登录\">登录</a>");
                    //head_html.AppendFormat("<a href=\"javascript:void(0)\" title=\"登录\" class=\"thirdlogin\">登录</a>");
                    head_html.AppendFormat("<i>丨</i>");
                    head_html.AppendFormat("<a href=\"/userlogin/register\" title=\"注册\">注册</a>");
                    head_html.AppendFormat("</div>");
                    #endregion
                }
                else if (type == 1)
                {
                    #region  类型0
                    //head_html.AppendFormat("<div class=\"opera-box\">");
                    ////head_html.AppendFormat("<span><a href=\"javascript:void(0)\" title=\"QQ\" id=\"qqLoginBtn\">QQ</a></span>");
                    //head_html.AppendFormat("<span><a class=\"login\" href=\"/login\" title=\"登录\">登录</a></span>");
                    ////head_html.AppendFormat("<span><a class=\"login\" href=\"javascript:;\" title=\"登录\" onclick=\"returnlogin()\">登录</a></span>");
                    //head_html.AppendFormat("<span>");
                    //head_html.AppendFormat("<a href=\"/register\" title=\"注册\">注册</a>");
                    //head_html.AppendFormat("</span>");
                    //head_html.AppendFormat("<span class=\"hrcolor\">");
                    //head_html.AppendFormat("<a href=\"/report\" title=\"红人求报道\">红人求报道</a>");
                    //head_html.AppendFormat("</span>");
                    //head_html.AppendFormat("<span class=\"hrcolor\">");
                    //head_html.AppendFormat("<a href=\"/hrip\" title=\"入驻红人爱品\">入驻红人爱品</a>");
                    //head_html.AppendFormat("</span>");
                    //head_html.AppendFormat("</div>");


                    head_html.AppendFormat("<div class=\"login\"><a href=\"/login\"  title=\"登录\">登录</a><a href=\"/register\" title=\"注册\">注册</a></div>");

                    #endregion
                }
            }
            else
            {
                NewXzc.Model.RED_USER user_model_head = user_bll_h.GetModel(user_id);

                if (user_model_head != null)
                {

                    if (type == 0)
                    {
                        #region  类型0

                        string nickname = user_model_head.USERNAME;

                        head_html.AppendFormat("<div class=\"login-box\">");
                        head_html.AppendFormat("<a class=\"user-center\" href=\"/people_c\" title=\"{0}\">", nickname);
                        head_html.AppendFormat("<span>");
                        head_html.AppendFormat("<img src=\"{0}\" height=\"28\" width=\"28\" alt=\"{1}\">", ImgHelper.Return_User_Head(user_model_head.USER_HEAD, 3), nickname);
                        head_html.AppendFormat("</span>");
                        head_html.AppendFormat("<em>{0}</em>", StringHelper.ReturnNumStr(nickname, 0, 7));
                        head_html.AppendFormat("</a>");
                        head_html.AppendFormat("<a href=\"/userlogin/logout\" title=\"退出\">退出</a>");
                        head_html.AppendFormat("</div>");
                        #endregion
                    }
                    else if (type == 1)
                    {
                        #region  类型0

                        string nickname = user_model_head.USERNAME;

                        //head_html.AppendFormat("<div class=\"opera-box login-box\">");
                        //head_html.AppendFormat("<a class=\"user-center\" href=\"/people_c\" title=\"{0}\">", nickname);
                        //head_html.AppendFormat("<span>");
                        //head_html.AppendFormat("<img src=\"{0}\" height=\"28\" width=\"28\" alt=\"{1}\">", ImgHelper.Return_User_Head(user_model_head.USER_HEAD, 3), nickname);
                        //head_html.AppendFormat("</span>");
                        //head_html.AppendFormat("<em>{0}</em>", StringHelper.ReturnNumStr(nickname, 0, 7));
                        //head_html.AppendFormat("</a>");
                        //head_html.AppendFormat("<a href=\"/logout\" title=\"退出\">退出</a>");

                        ////head_html.AppendFormat("<span>");
                        ////head_html.AppendFormat("<a href=\"/report\" title=\"红人求报道\">红人求报道</a>");
                        ////head_html.AppendFormat("</span>");

                        //head_html.AppendFormat("</div>");


                        head_html.AppendFormat("<div class=\"out\"><a href=\"/logout\" title=\"退出\" class=\"tc\">退出</a><a href=\"/people_c\" title=\"{0}\" class=\"name\">{0}</a></div>", nickname);


                        #endregion
                    }
                }
                else
                {
                    HttpContext.Current.Response.Redirect("/404");
                }
            }

            return head_html.ToString();
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        private bool IsReSubmit(ref HttpContext context, ref VelocityContext ctx)
        {
            return true;
        }
    }
}
