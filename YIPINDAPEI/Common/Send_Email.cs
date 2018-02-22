using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Collections;


namespace NewXzc.Common
{
    public class Send_Email
    {
        /// <summary>
        /// 得到smtp发送端口
        /// </summary>
        /// <returns></returns>
        public static int GetSMPTPort(int port, string selEmail)
        {
            //int port = 25;
            //string selEmail = mailHidden.Value;
            switch (selEmail)
            {
                case "@163.com":
                    port = 25;
                    break;
                case "@126.com":
                    port = 25;
                    break;
                case "@sina.com":
                    port = 25;
                    break;
                case "@yeah.net":
                    port = 25;
                    break;
                case "@sohu.com":
                    port = 25;
                    break;
                case "@hotmail.com":
                    port = 25;
                    break;
                case "@gmail.com":
                    port = 587;
                    //port = 465;
                    break;
                case "@yahoo.cn":
                    port = 25;
                    break;
                case "@yahoo.com":
                    port = 25;
                    break;
                case "@tom.com":
                    port = 25;
                    break;
            }
            return port;
        }
        /// <summary>
        /// 得到smtp服务器地址
        /// </summary>
        /// <returns></returns>
        public static string GetSMTP(string smtp, string selEmail)
        {
            //string smtp = "";
            //string selEmail = mailHidden.Value;
            switch (selEmail)
            {
                case "@163.com":
                    smtp = "smtp.163.com";
                    break;
                case "@qq.com":
                    smtp = "smtp.qq.com";
                    break;
                case "@126.com":
                    smtp = "smtp.126.com";
                    break;
                case "@sina.com":
                    smtp = "smtp.sina.com.cn";
                    break;
                case "@yeah.net":
                    smtp = "smtp.yeah.net";
                    break;
                case "@sohu.com":
                    smtp = "smtp.sohu.com";
                    break;
                case "@hotmail.com":
                    smtp = "smtp.live.com";
                    break;
                case "@gmail.com":
                    smtp = "smtp.gmail.com";
                    break;
                case "@yahoo.cn":
                    smtp = "smtp.mail.yahoo.cn";
                    break;
                case "@yahoo.com":
                    smtp = "smtp.mail.yahoo.com";
                    break;
                case "@tom.com":
                    smtp = "pop.tom.com";
                    break;
            }
            return smtp;
        }

        /// <summary>
        /// 得到smtp服务器HTTP地址
        /// </summary>
        /// <returns></returns>
        public static string GetSMTPHttp(string selEmail)
        {
            string smtp = "http://mail.";
            selEmail = selEmail.Substring(selEmail.LastIndexOf("@") + 1);
            smtp = smtp + selEmail;

            return smtp;
        }



        /// <summary>
        /// 返回邮件内容，找回密码
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody_FindPwd(string messTo, string userId, string return_url)
        {
            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "www.xzhichang.com";
            string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + return_url + ".aspx?uid=" + userId + "";

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            //sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");

            //sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            //sbr.Append("<head>");
            //sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
            //sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            //sbr.Append("<meta name='Description' content='轻微博' />");
            //sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
            //sbr.Append("<meta name='copyright' content='X职场 2011' />");
            //sbr.Append("<title>邮箱验证-X职场</title>");
            //sbr.Append("</head>");
            //sbr.Append("<body style='margin:0; padding:0'>");
            //sbr.Append("<div style='width:740px; margin:0 auto; min-height:500px; color:#171717; border-bottom:10px solid #171717; background:url(" + imgurl + "images/mail/ib.png) no-repeat 507px bottom #f1f1f1'>");
            //sbr.Append("<div style='height:98px; border-top:2px solid #2980b9; background:url(" + imgurl + "template/images/logo.png) no-repeat 68px center #171717;'>");
            //sbr.Append("<h2 style=\"width:580px; margin:0 auto;background:url(" + imgurl + "images/mail/r.png) no-repeat right center; height:98px;padding:0\"></h2></div>");
            //sbr.Append("<div style='width:580px; margin:30px auto 0; font:14px/30px microsoft yahei;'>");
            //sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            //sbr.Append("<p>请点击下面的链接激活你的帐号：<br/><a href='" + http_url + "' style='color:#3498db'>" + http_url + "</a><br/>若无法点击，请将上面的地址复制到浏览器地址栏直接访问</p>");
            //sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，010—59625717。X职场——开启职场人生新篇章</p>");
            //sbr.Append("<p>" + time + "</p>");
            //sbr.Append("</div>");
            //sbr.Append("</div>");
            //sbr.Append("</body>");
            //sbr.Append("</html>");


            sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");

            sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbr.Append("<head>");
            sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
            sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            sbr.Append("<meta name='Description' content='轻微博' />");
            sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>邮箱验证-X职场</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style='width:740px; margin:0 auto; min-height:500px; color:#171717; border-bottom:10px solid #171717; background:url(" + imgurl + "images/mail/ib.png) no-repeat 507px bottom #f1f1f1'>");
            sbr.Append("<div style='height:98px; border-top:2px solid #2980b9; background:url(" + imgurl + "template/images/logo.png) no-repeat 68px center #171717;'>");
            sbr.Append("<h2 style=\"width:580px; margin:0 auto;background:url(" + imgurl + "images/mail/r.png) no-repeat right center; height:98px;padding:0\"></h2></div>");
            sbr.Append("<div style='width:580px; margin:30px auto 0; font:14px/30px microsoft yahei;'>");
            sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            sbr.Append("<p>请点击下面的链接激活你的帐号：<br/><a href='" + http_url + "' style='color:#3498db'>" + http_url + "</a><br/>若无法点击，请将上面的地址复制到浏览器地址栏直接访问</p>");
            sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，010—59625717。X职场——开启职场人生新篇章</p>");
            sbr.Append("<p>" + time + "</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");



            return sbr.ToString();
        }



        /// <summary>
        /// 返回邮件内容，注册
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <returns></returns>
        public static string GetMessageBody_Register(string messTo, string userId)
        {

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = ImgHelper.GetCofigHttpUrl();// "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            //sbr.Append("<!DOCTYPE html>");

            //sbr.Append("<html>");
            //sbr.Append("<head>");
            //sbr.Append("<meta charset=\"utf-8\">");
            //sbr.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">");
            //sbr.Append("<meta name='Keywords' content='邮箱认证,项目融资,投资理财,小额投资,企业项目融资，投资理财公司' />");
            //sbr.Append("<meta name='Description' content='X职场会员中心方便会员查看信息,快速数据整理统计,查看商机信息,找好项目,找资金来源等.' />");

            //sbr.Append("<meta name='copyright' content='X职场 2011' />");
            //sbr.Append("<title>邮箱验证-X职场</title>");
            //sbr.Append("</head>");
            //sbr.Append("<body style='margin:0; padding:0'>");
            //sbr.Append("<div style=\"width:1000px;margin:0 auto; background-color:#f8f8f8 \"");
            //sbr.Append("<img alt=\"X职场\" src=\"" + imgurl + "images/m.jpg\"/>");
            //sbr.Append("<div style=\"padding:0 120px 30px;font:14px/24px 'microsoft yahei'\">");
            //sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            //sbr.Append("<p>感谢您注册X职场！</p>");
            //sbr.Append("<p>恭喜你已注册成功！敬请开始你的X旅程~</p>");
            //sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，</p><p>010—59625717。X职场——打造你的个人品牌</p>");
            //sbr.Append("<p>" + time + "</p>");
            //sbr.Append("</div>");
            //sbr.Append("</div>");
            //sbr.Append("</body>");
            //sbr.Append("</html>");

            sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");

            sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbr.Append("<head>");
            sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
            sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            sbr.Append("<meta name='Description' content='轻微博' />");
            sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>邮箱验证-X职场</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style='width:740px; margin:0 auto; min-height:500px; color:#171717; border-bottom:10px solid #171717; background:url(" + imgurl + "images/mail/ib.png) no-repeat 507px bottom #f1f1f1'>");
            sbr.Append("<div style='height:98px; border-top:2px solid #2980b9; background:url(" + imgurl + "template/images/logo.png) no-repeat 68px center #171717;'>");
            sbr.Append("<h2 style=\"width:580px; margin:0 auto;background:url(" + imgurl + "images/mail/r.png) no-repeat right center; height:98px;padding:0\"></h2></div>");
            sbr.Append("<div style='width:580px; margin:30px auto 0; font:14px/30px microsoft yahei;'>");
            sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            sbr.Append("<p>感谢您注册X职场！</p>");
            sbr.Append("<p>恭喜你已注册成功！敬请开始你的X旅程~</p>");
            sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，</p><p>010—59625717。X职场——打造你的个人品牌</p>");
            sbr.Append("<p>" + time + "</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");


            return sbr.ToString();
        }

        /// <summary>
        /// 返回邮件内容
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody(string messTo, string userId, string return_url)
        {
            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "100.by.com";
            string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + return_url + "?uid="+userId; //".aspx?uid=" + userId + "";

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time,6);

            string imgurl = ImgHelper.GetCofigHttpUrl();// "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            sbr.Append("<!DOCTYPE html>");

            sbr.Append("<html>");
            sbr.Append("<head>");
            sbr.Append("<meta charset=\"utf-8\">");
            sbr.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">");
            sbr.Append("<meta name='Keywords' content='邮箱认证,项目融资,投资理财,小额投资,企业项目融资，投资理财公司' />");
            sbr.Append("<meta name='Description' content='X职场会员中心方便会员查看信息,快速数据整理统计,查看商机信息,找好项目,找资金来源等.' />");
            
            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>邮箱验证-X职场</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style=\"width:1000px;margin:0 auto; background-color:#f8f8f8 \"");
            sbr.Append("<img alt=\"X职场\" src=\""+imgurl+"images/m.jpg\"/>");
            sbr.Append("<div style=\"padding:0 120px 30px;font:14px/24px 'microsoft yahei'\">");
            sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            sbr.Append("<p>感谢您注册X职场！</p>");
            sbr.Append("<p>请点击下面的链接激活你的帐号：<br/><a href='" + http_url + "' style='color:#6397e7;text-decoration:none'>" + http_url + "</a><br/>若无法点击，请将上面的地址复制到浏览器地址栏直接访问</p>");
            sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，</p><p>010—59625717。X职场——打造你的个人品牌</p>");
            sbr.Append("<p>" + time + "</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");

            return sbr.ToString();
        }


        /// <summary>
        /// 返回邮件内容
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody_Interview(string messTo, string userId, string return_url,string company_name,string realms_name)
        {
            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "100.by.com";
            string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + return_url + "?uid=" + userId; //".aspx?uid=" + userId + "";

            http_url = ImgHelper.GetCofigHttpUrl() + "company_h/" + realms_name;

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = ImgHelper.GetCofigHttpUrl();// "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            sbr.Append("<!DOCTYPE html>");

            sbr.Append("<html>");
            sbr.Append("<head>");
            sbr.Append("<meta charset=\"utf-8\">");
            sbr.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">");
            sbr.Append("<meta name='Keywords' content='邮箱认证,项目融资,投资理财,小额投资,企业项目融资，投资理财公司' />");
            sbr.Append("<meta name='Description' content='X职场会员中心方便会员查看信息,快速数据整理统计,查看商机信息,找好项目,找资金来源等.' />");

            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>X职场-面试函通知</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style=\"width:1000px;margin:0 auto; background-color:#f8f8f8 \"");
            //sbr.Append("<img alt=\"X职场\" src=\"" + imgurl + "images/m.jpg\"/>");
            sbr.Append("<div style=\"padding:0 120px 30px;font:14px/24px 'microsoft yahei'\">");
            //sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            sbr.Append("<p>尊敬的用户您好：</p>");
            sbr.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;你收到一封来自<a href='" + http_url + "' style='color:#6397e7;text-decoration:none'>" + company_name + "</a>公司的面试邀请函，赶快查看一下吧。</p>");
            //sbr.Append("<p>请点击下面的链接激活你的帐号：<br/><a href='" + http_url + "' style='color:#6397e7;text-decoration:none'>" + http_url + "</a><br/>若无法点击，请将上面的地址复制到浏览器地址栏直接访问</p>");
            //sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，</p><p>010—59625717。X职场——打造你的个人品牌</p>");
            //sbr.Append("<p>" + time + "</p>");
            sbr.Append("<p style=\"margin-left:330px;margin-top:20px;\">—&nbsp;—X职场</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");

            return sbr.ToString();
        }

        /// <summary>
        /// 返回邮件内容
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody_Fish(string messTo, string userId, string return_url,string code)
        {
            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "100.by.com";
            string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + return_url + "?uid=" + userId; //".aspx?uid=" + userId + "";

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = ImgHelper.GetCofigHttpUrl();// "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            sbr.Append("<!DOCTYPE html>");

            sbr.Append("<html>");
            sbr.Append("<head>");
            sbr.Append("<meta charset=\"utf-8\">");
            sbr.Append("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge,chrome=1\">");
            sbr.Append("<meta name='Keywords' content='邮箱认证,项目融资,投资理财,小额投资,企业项目融资，投资理财公司' />");
            sbr.Append("<meta name='Description' content='X职场会员中心方便会员查看信息,快速数据整理统计,查看商机信息,找好项目,找资金来源等.' />");

            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>X职场-激活码通知</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style=\"width:1000px;margin:0 auto; background-color:#f8f8f8 \"");
            //sbr.Append("<img alt=\"X职场\" src=\"" + imgurl + "images/m.jpg\"/>");
            sbr.Append("<div style=\"padding:0 120px 30px;font:14px/24px 'microsoft yahei'\">");
            sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            sbr.Append("<p>您的激活码是："+code+"</p>");
            //sbr.Append("<p>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;你收到一封来自<a href='" + http_url + "' style='color:#6397e7;text-decoration:none'></a>公司的面试邀请函，赶快查看一下吧。</p>");
            //sbr.Append("<p>请点击下面的链接激活你的帐号：<br/><a href='" + http_url + "' style='color:#6397e7;text-decoration:none'>" + http_url + "</a><br/>若无法点击，请将上面的地址复制到浏览器地址栏直接访问</p>");
            //sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，</p><p>010—59625717。——X职场</p>");
            sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，010—59625717。——X职场</p>");
            sbr.Append("<p>" + time + "</p>");
            //sbr.Append("<p style=\"margin-left:330px;margin-top:20px;\">—&nbsp;—X职场</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");

            return sbr.ToString();
        }


        /// <summary>
        /// 返回邮件内容
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody_Review_Resume(string messTo, string userId, string return_url, string code)
        {
            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "100.by.com";
            string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + return_url + "?uid=" + userId; //".aspx?uid=" + userId + "";

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = ImgHelper.GetCofigHttpUrl();// "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");

            sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbr.Append("<head>");
            sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
            sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            sbr.Append("<meta name='Description' content='轻微博' />");
            sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>X职场-简历评价通知</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style='width:740px; margin:0 auto; min-height:500px; color:#171717; border-bottom:10px solid #171717; background:url(" + imgurl + "images/mail/ib.png) no-repeat 507px bottom #f1f1f1'>");
            sbr.Append("<div style='height:98px; border-top:2px solid #2980b9; background:url(" + imgurl + "template/images/logo.png) no-repeat 68px center #171717;'>");
            sbr.Append("<h2 style=\"width:580px; margin:0 auto;background:url(" + imgurl + "images/mail/r.png) no-repeat right center; height:98px;padding:0\"></h2></div>");
            sbr.Append("<div style='width:580px; margin:30px auto 0; font:14px/30px microsoft yahei;'>");
            sbr.Append("<p>您好，<span style='color:#3498db'>" + messTo + "</span></p>");
            sbr.Append("<p>评价内容是：" + code + "</p>");
            sbr.Append("<p>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，010—59625717。——X职场</p>");
            sbr.Append("<p>" + time + "</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");



            return sbr.ToString();
        }



        /// <summary>
        /// 返回邮件内容，找回密码
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody_InviteCode(string messTo, string userId,string code)
        {
            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "www.xzhichang.com";
            string http_url = "http://" + strUrlp + "/register";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + "register";

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");

            sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbr.Append("<head>");
            sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
            sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            sbr.Append("<meta name='Description' content='轻微博' />");
            sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>邀请函-X职场</title>");
            sbr.Append("<style type=\"text/css\">");
            sbr.Append("body,p,h1{margin:0;padding:0;font-family: \"microsoft yahei\";}");
            sbr.Append(".box{width:447px;height:764px;background:url(" + imgurl + "images/yqh_bg.png) no-repeat;padding:180px 80px 0;margin:0 auto;position:relative;}");
            sbr.Append(".link{width:136px;height:13px;display:block;position:absolute;right:25px;top:44px;}");
            sbr.Append(".box h1{height:50px;line-height:50px;font-size:14px;color:#231815;font-weight:normal;}");
            sbr.Append(".box p{line-height:20px; text-indent:28px;font-size:13px;color:#231815;margin-bottom:20px;}");
            sbr.Append(".box p strong{color:#00abe5;font-size:18px;font-weight:normal;}");
            sbr.Append(".yqh{width:432px;height:250px;padding-left:15px;position:relative;}");
            sbr.Append(".yqm{width:301px;height:182px;background:url(" + imgurl + "images/yqh_yqm.png) no-repeat;float:left;}");
            sbr.Append(".link_1{width:133px;height:104px;display:block;position:absolute;left:160px;top:14px;}");
            sbr.Append(".yqh_yqm{width:177px;height:58px;line-height:58px;font-size:24px;color:#231815;text-align:center;margin:125px 0 0 124px;}");
            sbr.Append(".time{float:right;text-align:right;line-height:25px;padding-top:200px;font-size:14px;color:#231815;width:130px;height:50px;}");
            sbr.Append("</style>");

            sbr.Append("</head>");
            sbr.Append("<body>");
            sbr.Append("<div class=\"box\">");
            sbr.Append("<a href=\"http://www.xzhichang.com\" target=\"_blank\" class=\"link\"></a>");
            sbr.Append("<h1>尊敬的用户：</h1>");
            sbr.Append("<p><strong>X职场</strong>规划职场人生的新篇章，历经巨成博游团队半载努力，终于<strong>10月10日至10月29日开启千名精英体验活动！</strong>现X职场团队诚邀您的加入，建设国内最好的职场社交网站。找同行、找同事，发布属于自己的行业宣言。更广行业交流空间，更多的同行人脉；让思想飞扬，让知识沉淀；在这里实现职业拓展，在这里实现职场升华。</p>");
            sbr.Append("<p><strong>X职场</strong>内设“行业交流”、“先知”、“职场人生”、“Office Story”等栏目，更广交流空间，令你实现职场价值；更多职场人脉，令你找到职场知己；更大展示舞台，令你成为职场瞩目的明星。如果你是职场新人，这里有行业精英的真知灼见，令你轻松步入职场，完成菜鸟到精英的转变；如果是精英白领，这里有同行间最新的行业资讯，轻松实现业内交流，职场生涯再进一步。</p>");
            sbr.Append("<p><strong>X职场</strong>热诚欢迎您的到来。征集优秀原创、行业文章写手作者。X职场会每月评出优秀文章，给予丰厚的稿费奖励。同时做为X职场元老用户拥有更多优势，令您在X职场社交活动中领先一步。我们会定期组织用户开展论坛、沙龙等活动，将我们网络上的朋友聚集一堂线下交流也精彩。同时X职场也准备了精心设计的纪念版物料、玩偶，为您办公增光填色。</p>");

            sbr.Append("<div class=\"yqh\">");
            sbr.Append("<a href=\"http://www.xzhichang.com\" target=\"_blank\" class=\"link_1\"></a>");
            sbr.Append("<div class=\"yqm\">");
            sbr.Append("<div class=\"yqh_yqm\">");
            sbr.Append(code);
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("<div class=\"time\">");
            sbr.Append("X职场<br/>");
            sbr.Append(time);
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");

            return sbr.ToString();
        }

        /// <summary>
        /// 返回邮件内容，找回密码
        /// </summary>
        /// <param name="messTo">发送给谁</param>
        /// <param name="userId">需要传递的参数，用户的USERGUID</param>
        /// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        /// <returns></returns>
        public static string GetMessageBody_IPHONE6_SECOND(string messTo, string userId, string return_url)
        {


            string strUrl = "localhost:11006";
            string strUrl3 = "192.168.1.3";
            string strUrlp = "www.xzhichang.com";
            string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";
            http_url = ImgHelper.GetCofigHttpUrl() + return_url + ".aspx?uid=" + userId + "";

            string time = DateTime.Now.ToString();
            //time = time.Substring(0, time.LastIndexOf(":"));
            time = TimeParser.ReturnCurTime(time, 6);

            string imgurl = "http://" + strUrlp + "/";


            StringBuilder sbr = new StringBuilder();

            sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");

            sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
            sbr.Append("<head>");
            sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
            sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
            sbr.Append("<meta name='Description' content='轻微博' />");
            sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
            sbr.Append("<meta name='copyright' content='X职场 2011' />");
            sbr.Append("<title>幸运女神  二次降临-X职场</title>");
            sbr.Append("</head>");
            sbr.Append("<body style='margin:0; padding:0'>");
            sbr.Append("<div style='width:740px; margin:0 auto; min-height:500px; color:#171717; border-bottom:10px solid #171717; background:url(" + imgurl + "images/mail/ib.png) no-repeat 507px bottom #f1f1f1'>");
            sbr.Append("<div style='height:98px; border-top:2px solid #2980b9; background:url(" + imgurl + "template/images/logo.png) no-repeat 68px center #171717;'>");
            sbr.Append("<h2 style=\"width:580px; margin:0 auto;background:url(" + imgurl + "images/mail/r.png) no-repeat right center; height:98px;padding:0\"></h2></div>");
            sbr.Append("<div style='width:580px; margin:30px auto 0; font:14px/30px microsoft yahei;'>");
            sbr.Append("<p><span style='color:#3498db'>幸运女神  二次降临</span></p>");
            sbr.Append("<p><br/>iphone6活动已顺利结束，所有一二等奖已经下发结束。<br/><br/>但是目前为止，还有158名二等奖得住尚未领取奖品，<br/><br/><strong><a href=\"http://www.xzhichang.com\" target=\"_blank\">X职场</a></strong>特此下发通知，所有未及时领奖的二等奖得主，可以在2014.11.14日10:00——2014.11.15日16:00期间<br/><br/>加入官方QQ群：X职场官方交流群 215892779，于群管理：王焕明 处索取奖品。<br/><br/>这是最后领取奖品的机会，如之后还有未领之奖品，则我们会将其随机发放给其他参与此次活动的用户。<br/><br/>X职场热诚欢迎您的到来。征集优秀原创、行业文章写手作者。<br/><br/>X职场会每月评出优秀文章，给予丰厚的稿费奖励。<br/><br/>同时做为X职场元老用户拥有更多优势，令您在X职场社交活动中领先一步。我们会定期组织用户开展论坛、沙龙等活动，将我们网络上的朋友聚集一堂线下交流也精彩。<br/><br/>同时X职场也准备了精心设计的纪念版物料、玩偶，为您办公增光填色。</p>");
            sbr.Append("<p>" + time + "</p>");
            sbr.Append("</div>");
            sbr.Append("</div>");
            sbr.Append("</body>");
            sbr.Append("</html>");

            return sbr.ToString();
        }

        

        ///// <summary>
        ///// 返回邮件内容
        ///// </summary>
        ///// <param name="messTo">发送给谁</param>
        ///// <param name="userId">需要传递的参数，用户的USERGUID</param>
        ///// <param name="return_url">需要传递到哪个页面，页面的名称</param>
        ///// <returns></returns>
        //public static string GetMessageBody(string messTo, string userId,string return_url)
        //{
        //    string strUrl = "localhost:11006";
        //    string strUrl3 = "192.168.1.3";
        //    string strUrlp = "xzc.d6315.com";
        //    string http_url = "http://" + strUrlp + "/" + return_url + ".aspx?uid=" + userId + "";//"http://" + strUrl + "/CheckMailResult.aspx?uid=" + userId + "";

        //    string time = DateTime.Now.ToString();
        //    //time = time.Substring(0, time.LastIndexOf(":"));
        //    time = TimeParser.ReturnCurTime(time, 0);


        //    StringBuilder sbr = new StringBuilder();
            
        //    sbr.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>");
        //    sbr.Append("<html xmlns='http://www.w3.org/1999/xhtml'>");
        //    sbr.Append("<head>");
        //    sbr.Append("<meta http-equiv='X-UA-Compatible' content='IE=7' />");
        //    sbr.Append("<meta http-equiv='Content-Type' content='text/html; charset=utf-8' />");
        //    sbr.Append("<meta name='Description' content='轻微博' />");
        //    sbr.Append("<meta name='Keywords' content='轻微博,专业人士注册,Blog,注册' />");
        //    sbr.Append("<meta name='copyright' content='X职场 2011' />");
        //    sbr.Append("<title>邮箱里验证-轻博客-X职场</title>");
        //    sbr.Append("</head>"); 
        //    sbr.Append("<body style='margin:0;padding:0;background:#e4e4e4;'>");
        //    sbr.Append("<div style='margin:83px auto 0 auto;width:740px;height:586px;background:#FFF url(http://www.boyouba.com/images/registerB/registerBg.gif) repeat-x;'>");
        //    sbr.Append("<div style='padding:0 32px 0 52px;height:116px;overflow:hidden;'>");
        //    sbr.Append("<h1 style='margin:0;padding:0;width:108px;height:116px;float:left;'>");
        //    sbr.Append("<a href='http://www.xzhichang.com' title='X职场' target='_self' style='width:108px;height:116px;display:block;background:url(http://www.boyouba.com/images/registerB/bdlogo.png) no-repeat center center;'></a></h1>");
        //    sbr.Append("<p style='margin:0;padding:45px 0 0 11px;float:left;font-size:20px;line-height:22px;color:#333;'><b>欢迎注册X职场</b><br />www.boyouba.com</p>");
        //    sbr.Append("</div>");
        //    sbr.Append("<div style='padding:50px 71px 0 71px;font-size:13px;font-family:'Microsoft YaHei';word-break:break-all;line-height:22px;color:#000000;'>");
        //    sbr.Append("<p style='margin:0;padding:0;'>您好，<a style='color:#1978a8;text-decoration:underline;'>" + messTo + "</a></p><br/>");
        //    sbr.Append("<p style='margin:0;padding:0;'>感谢您注册X职场！</p>");
        //    sbr.Append("<p style='margin:0;padding:0;'>请点击下面的链接激活你的帐号，若无法点击，请将下面的地址复制到浏览器地址栏直接访问：</p><br/>");
        //    sbr.Append("<p style='margin:0;padding:0;'><a href='" + http_url + "' style='color:#1978a8;'>" + http_url + "</a></p><br/>");
        //    sbr.Append("<p style='margin:0;padding:0;'>该邮件由X职场系统发出，因此请勿直接回复。您如有问题，请咨询客服电话010—59625351，010—59625717。</p><br/>");
        //    sbr.Append("<p style='margin:0;padding:0;'>X职场——全球华语职场社交网络</p>");
        //    sbr.Append("<p style='margin:0;padding:0;'>" + time + "</p>");
        //    sbr.Append("</div>");
        //    sbr.Append("</div>");
        //    sbr.Append("</div>");
        //    sbr.Append("</body>");
        //    sbr.Append("</html>");

        //    return sbr.ToString();
        //}

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="MessageTo">收件人邮箱地址</param> 
        /// <param name="MessageSubject">邮件主题</param> 
        /// <param name="MessageBody">邮件内容</param> 
        /// <returns></returns>
        public static bool Send(string MessageTo, string MessageSubject, string MessageBody)
        {
            string email_smpt = ConfigHelper.GetConfigString("email_smpt");
            string email_name = ConfigHelper.GetConfigString("email_name");
            string email_pwd = ConfigHelper.GetConfigString("email_pwd");
            string email_from = ConfigHelper.GetConfigString("email_from");

            //MailAddress MessageFrom = new MailAddress("boyoubakefu@163.com");
            //MailAddress MessageFrom = new MailAddress("service@boyouba.com","X职场");//第二个参数是更改发送邮件时，邮件的发送人的名称的
            //MailAddress MessageFrom = new MailAddress("service@xzhichang.com", "X职场");//第二个参数是更改发送邮件时，邮件的发送人的名称的
            //MailAddress MessageFrom = new MailAddress("service@message.xzhichang.com", "X职场");//第二个参数是更改发送邮件时，邮件的发送人的名称的

            MailAddress MessageFrom = new MailAddress(email_name, email_from);//第二个参数是更改发送邮件时，邮件的发送人的名称的

            //MailAddress MessageFrom = new MailAddress("xzhichang@126.com", "X职场");//第二个参数是更改发送邮件时，邮件的发送人的名称的

            MailMessage message = new MailMessage();

            // if (FileUpload1.PostedFile.FileName != "")
            // {
            // Attachment att = new Attachment("d://test.txt");//发送附件的内容
            //    message.Attachments.Add(att);
            // }

            message.From = MessageFrom;
            message.To.Add(MessageTo); //收件人邮箱地址可以是多个以实现群发 
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            //message.Attachments.Add(objMailAttachment);
            message.IsBodyHtml = true; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 

            SmtpClient sc = new SmtpClient();
            //sc.Host = "smtp.boyouba.com"; //指定发送邮件的服务器地址或IP 
            //sc.Host = "smtp.163.com";
            //sc.Host = "message.xzhichang.com"; //指定发送邮件的服务器地址或IP 
            sc.Host = email_smpt; //指定发送邮件的服务器地址或IP 
            //sc.Host = "smtp.xzhichang.com"; //指定发送邮件的服务器地址或IP 
            //sc.Host = "smtp.exmail.qq.com";//指定发送邮件的服务器地址或IP 
            //sc.Host = "smtp.126.com";
            sc.Port = 25; //指定发送邮件端口 
            //sc.Credentials = new System.Net.NetworkCredential("boyoubakefu@163.com", "boyoubayingyong"); //指定登录服务器的用户名和密码(发件人的邮箱登陆密码)
            //sc.Credentials = new System.Net.NetworkCredential("service@boyouba.com", "d6party123");
            //sc.Credentials = new System.Net.NetworkCredential("service@xzhichang.com", "d6party");
            //sc.Credentials = new System.Net.NetworkCredential("service@message.xzhichang.com", "d6party");
            sc.Credentials = new System.Net.NetworkCredential(email_name , email_pwd);
            //sc.Credentials = new System.Net.NetworkCredential("xzhichang@126.com", "juchengboyou");

            try
            {
                sc.Send(message); //发送邮件 
            }
            catch
            {
                return false;
            }

            //生成TXT文本文档
            //CreateText(MessageTo);

            return true;

        }


        /// <summary>
        /// 生成TXT文本文档
        /// </summary>
        /// <param name="email"></param>
        private static void CreateText(string email)
        {
            string FilePath = "E:/TextTest";

            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            FilePath = FilePath + "/"+TimeParser.ReturnCurTime(DateTime.Now.ToString(),0)+".txt";

            FileStream fs;

            try
            {
                fs = new FileStream(@""+FilePath+"", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                sw.BaseStream.Seek(0, SeekOrigin.End);
                sw.WriteLine(email);
                sw.Flush();
                sw.Close();
            }
            catch(Exception ex)
            {

            }

        }


        /// <summary> 
        /// 发送电子邮件 
        /// </summary> 
        /// <param name="MessageFrom">发件人邮箱地址</param> 
        /// <param name="MessageTo">收件人邮箱地址</param> 
        /// <param name="MessageSubject">邮件主题</param> 
        /// <param name="MessageBody">邮件内容</param> 
        /// <returns></returns> 
        public static string SendStr(MailAddress MessageFrom, string MessageTo, string MessageSubject, string MessageBody)
        {
            string msg = "邮件发送成功！";

            MailMessage message = new MailMessage();

            // if (FileUpload1.PostedFile.FileName != "")
            // {
            // Attachment att = new Attachment("d://test.txt");//发送附件的内容
            //    message.Attachments.Add(att);
            // }

            message.From = MessageFrom;
            message.To.Add(MessageTo); //收件人邮箱地址可以是多个以实现群发 
            message.Subject = MessageSubject;
            message.Body = MessageBody;
            //message.Attachments.Add(objMailAttachment);
            message.IsBodyHtml = true; //是否为html格式 
            message.Priority = MailPriority.High; //发送邮件的优先等级 

            SmtpClient sc = new SmtpClient();
            sc.Host = "smtp.boyouba.com"; //指定发送邮件的服务器地址或IP 
            sc.Port = 25; //指定发送邮件端口 
            sc.Credentials = new System.Net.NetworkCredential("service@boyouba.com", "d6party123"); //指定登录服务器的用户名和密码(发件人的邮箱登陆密码)

            try
            {
                sc.Send(message); //发送邮件 
            }
            catch (Exception es)
            {
                msg = es.ToString();
            }
            return msg;

        }

    }
}
