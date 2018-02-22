using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace NewXzc.Web.Common
{
    public class MyHttpModule : IHttpModule
    {
        public void Dispose()
        {
        }
        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += (new EventHandler(Process301));
        }
        public void Process301(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            HttpRequest request = app.Context.Request;
            string lRequestedPath = request.Url.DnsSafeHost.ToString();
            string strDomainURL = ConfigurationManager.AppSettings["WebDomain"].ToString();
            string strWebURL = ConfigurationManager.AppSettings["URL301Location"].ToString();
            if (lRequestedPath.IndexOf(strWebURL) == -1)
            {
                app.Response.StatusCode = 301;
                app.Response.AddHeader("Location", lRequestedPath.Replace(lRequestedPath, "http://" + strWebURL + request.RawUrl.ToString().Trim())); //这里面的域名根据自己的实际情况修改
                app.Response.End();
            }
        }
    } 
}