using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace NewXzc.Common
{
    public class ReturnUrl
    {
        /// <summary>
        /// 返回当前访问页面的路径
        /// </summary>
        /// <returns></returns>
        public static string ReturnCurUrl()
        {
            string cururl = HttpContext.Current.Request.Url.ToString().Trim();
            return cururl;
        }

        /// <summary>
        /// 跳回登录页面
        /// </summary>
        public static string ReturnLogin()
        {
            //HttpContext.Current.Response.Write("<script type='text/javascript'>window.parent.location.href='/login.aspx'</script>");
            string url = "<script type='text/javascript'>top.location.href='/Admin/login.aspx'</script>";
            url = "<script type='text/javascript'>window.top.document.location='/Admin/login.aspx'</script>";
            return url;
        }

        /// <summary>
        /// 跳回登录页面
        /// </summary>
        public static string ReturnLogin_Index()
        {
            //HttpContext.Current.Response.Write("<script type='text/javascript'>window.parent.location.href='/login.aspx'</script>");
            string url = "<script type='text/javascript'>top.location.href='../admin/login.aspx'</script>";
            url = "<script type='text/javascript'>window.top.document.location='../admin/login.aspx'</script>";
            return url;
        }
    }
}
