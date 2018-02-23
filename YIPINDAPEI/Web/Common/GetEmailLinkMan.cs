using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
namespace NewXzc.Web.Common
{
    public class GetEmailLinkMan
    {
        //邮箱入口定义
        const string mail126 = "http://reg.163.com/login.jsp?type=1&product=mail126&url=http://entry.mail.126.com/cgi/ntesdoor?hid%3D10010102%26lightweight%3D1%26verifycookie%3D1%26language%3D0%26style%3D-1&username={0}&amp;amp;amp;amp;password={1}";


        const string mail163 = "https://reg.163.com/logins.jsp?username={0}&password={1}&type=1&url=http://entry.mail.163.com/coremail/fcg/ntesdoor2?lightweight%3D1%26verifycookie%3D1%26language%3D-1%26style%3D35";


        protected static string cookieheader = string.Empty;    //定义公共的 Cookie Header 变量
        protected static string NextUrl = string.Empty;   //定义下次访问的Url变量
        CookieContainer cookieCon = new CookieContainer();

        private string uName;
        private string pwd;
        private EmailType en;
        byte[] b;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">邮箱名称</param>
        /// <param name="pwd">邮箱密码</param>
        public GetEmailLinkMan(string name, string pwd, EmailType type)
        {
            this.uName = name;
            this.pwd = pwd;
            en = type;
            //记录登陆邮箱的用户名和密码
            StringBuilder sb = new StringBuilder();
            sb.Append("&domain=126.com");//域
            sb.Append("&language=-1");//语言
            sb.Append("&bCookie=");
            sb.Append("&username=" + name);
            sb.Append("&savelogin=");
            sb.Append("url2=http://mail.126.com/errorpage/err_126.htm");
            sb.Append("&user=" + name.Substring(0, name.IndexOf("@")));//登录名
            sb.Append("&password=" + pwd);//密码
            sb.Append("&style=-1"); //样式
            sb.Append("&secure=");
            b = Encoding.ASCII.GetBytes(sb.ToString());
        }
        #endregion

        #region 得到网页数据
        /// <summary>
        /// 得到网页数据
        /// </summary>
        /// <returns>得到网页HTML数据</returns>
        private string GetHtml()
        {
            string EntryUrl = GetEntryUrl();
            return Process126mail(EntryUrl);
        }
        #endregion

        #region 发送请求获取页面信息HTML
        /// <summary>
        /// 发送请求获取页面信息HTML
        /// </summary>
        /// <param name="EntryUrl">解析地址</param>
        /// <returns></returns>
        private string Process126mail(string EntryUrl)
        {
            try
            {
                #region 第一次请求登陆地址
                //第一请求126登陆邮箱地址
                //获取请求的内容
                HttpWebRequest hwr = (HttpWebRequest)HttpWebRequest.Create(new Uri(EntryUrl));
                hwr.Method = "POST";
                hwr.KeepAlive = false;
                hwr.ContentType = "application/x-www-form-urlencoded";
                hwr.ContentLength = b.Length;
                hwr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; GTB6; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                hwr.CookieContainer = cookieCon;

                //// 发送数据
                using (Stream s = hwr.GetRequestStream())
                {
                    s.Write(b, 0, b.Length);
                }
                string rb = GetWebRequest(hwr);
                EntryUrl = TransCode(rb);

                //把用户名和密码添加到头部cookies
                Uri nurl1 = new Uri("http://www.163.com");
                Uri nurl2 = new Uri("http://reg.163.com");
                Uri nurl3 = new Uri(EntryUrl);
                foreach (System.Net.Cookie cookie in cookieCon.GetCookies(nurl1))
                {
                    cookie.Domain = nurl3.Host;
                }
                cookieCon.Add(cookieCon.GetCookies(nurl1));

                foreach (System.Net.Cookie cookie in cookieCon.GetCookies(nurl2))
                {
                    cookie.Domain = nurl3.Host;
                }
                cookieCon.Add(cookieCon.GetCookies(nurl2));

                foreach (System.Net.Cookie cookie in cookieCon.GetCookies(nurl3))
                {
                    cookie.Domain = ".126.com";
                    cookie.Expires = DateTime.Now.AddHours(1);
                }
                cookieCon.Add(cookieCon.GetCookies(nurl3));
                #endregion

                #region 第二次请求新地址
                string rUrlTwo = string.Empty;

                hwr = (HttpWebRequest)HttpWebRequest.Create(new Uri(EntryUrl));
                hwr.Method = "POST";
                hwr.KeepAlive = false;
                hwr.ContentType = "application/x-www-form-urlencoded";
                hwr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; GTB6; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                hwr.CookieContainer = cookieCon;

                string rbTwo = GetWebRequest(hwr);
                EntryUrl = TransCode(rbTwo);
                #endregion

                #region 第三次请求新地址
                //根据第二个地址获取第三个地址

                string rUrl = string.Empty;
                hwr = (HttpWebRequest)HttpWebRequest.Create(new Uri(EntryUrl));
                hwr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; GTB6; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                hwr.CookieContainer = cookieCon;
                hwr.Method = "HEAD";
                hwr.AllowAutoRedirect = false;

                // 获取返回信息
                using (HttpWebResponse wr = (HttpWebResponse)hwr.GetResponse())
                {
                    ////获取到rurl http://cwebmail.mail.126.com/js4/main.jsp?sid=WBGSoZlBkEKZafrfivBBqbkxsHBowIPx
                    rUrl = wr.GetResponseHeader("Location");
                }
                #endregion

                #region 第四次请求新地址获得数据
                //根据第三个地址获取到http://cwebmail.mail.126.com/js4/s?sid=WBGSoZlBkEKZafrfivBBqbkxsHBowIPx&func=global:sequential&from=nav&action=showHideFolder&showAd=false&userType=newuser&uid=xxxx@sina.com

                //然后请求分析获取联系人存为XML数据
                string post = "<?xml version=\"1.0\"?><object><array name=\"items\"><object><string name=\"func\">pab:searchContacts</string><object name=\"var\"><array name=\"order\"><object><string name=\"field\">FN</string><boolean name=\"desc\">false</boolean><boolean name=\"ignoreCase\">true</boolean></object></array></object></object><object><string name=\"func\">pab:getAllGroups</string></object></array></object>";
                byte[] pb = Encoding.ASCII.GetBytes(post.ToString());
                string bookurl = (rUrl + "&func=global:sequential").Replace("js3", "a");
                hwr = (HttpWebRequest)HttpWebRequest.Create(new Uri(bookurl.Replace("main.jsp", "s")));
                hwr.Method = "POST";
                hwr.ContentType = "application/xml";
                hwr.ContentLength = pb.Length;
                hwr.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; GTB6; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
                hwr.CookieContainer = cookieCon;

                // 发送数据
                using (Stream s = hwr.GetRequestStream())
                {
                    s.Write(pb, 0, pb.Length);
                }

                // 获取返回信息
                rb = GetWebRequest(hwr);
                #endregion

                return rb;
            }
            catch (Exception ex)
            {
                return ex.ToString() + "登录失败，请检查用户名称和密码";

            }
        }
        #endregion

        #region 正则过滤获取META中URL跳转地址
        /// <summary>
        /// 正则过滤获取META中URL跳转地址
        /// </summary>
        /// <param name="rb"></param>
        /// <returns></returns>
        private string TransCode(string rb)
        {
            Regex reg = new Regex(@"<META HTTP-EQUIV=REFRESH CONTENT=""[\d];URL=(.+?)"">");
            Match ma = reg.Match(rb.ToUpper());
            string rusult = "";
            if (ma.Success)
            {
                rusult = ma.Groups[1].Value.ToLower();
            }

            Regex r = new Regex(@"&#([\d]{1,5})", RegexOptions.None);
            StringBuilder s = new StringBuilder();

            foreach (Match m in r.Matches(rusult))
            {
                char c = (char)int.Parse(m.Groups[1].Value);
                s.Append(c.ToString());
            }
            return s.ToString();
        }
        #endregion

        #region 获取请求页面返回页面信息
        /// <summary>
        /// 获取请求页面返回页面信息
        /// </summary>
        /// <param name="hwr"></param>
        /// <returns></returns>
        private string GetWebRequest(HttpWebRequest hwr)
        {
            string rb = string.Empty;
            // 获取返回信息
            using (HttpWebResponse wr = (HttpWebResponse)hwr.GetResponse())
            {
                StreamReader sr = new StreamReader(wr.GetResponseStream(), Encoding.UTF8);
                rb = sr.ReadToEnd();
                sr.Close();
            }
            return rb;
        }
        #endregion

        #region 得到126或163通讯录的内容
        /// <summary>
        /// 得到126或163通讯录的内容
        /// </summary>
        /// <returns>通讯录集合</returns>
        public List<Person> getContact()
        {
            List<Person> ls = new List<Person>();
            //读取XML数据然后进行    选择匹配筛选出来匹配的邮箱
            string resHtml = Encoding.UTF8.GetString(Encoding.Convert(Encoding.UTF8, Encoding.UTF8, Encoding.UTF8.GetBytes(this.GetHtml())));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(resHtml);

            XmlNodeList xnl = xmlDoc.SelectNodes("/result/array/object");
            if (xnl == null || xnl.Count <= 0)
                return ls;

            XmlNodeList linkNOdes = xnl[0].SelectNodes("array/object");
            foreach (XmlNode linkNode in linkNOdes)
            {
                Person ps = new Person();
                foreach (XmlNode xn2 in linkNode.ChildNodes)
                {
                    //取得邮箱地址
                    if (xn2.Attributes["name"].Value == "EMAIL;PREF")
                    {
                        ps.Email = xn2.InnerText;
                    }
                    if (xn2.Attributes["name"].Value == "FN")
                    {
                        if (!string.IsNullOrEmpty(xn2.InnerText))
                        {
                            ps.Name = xn2.InnerText;
                        }
                        else
                        {
                            ps.Name = "暂无名称";
                        }
                    }
                }
                ls.Add(ps);
            }

            return ls;
        }
        #endregion

        #region 得到请求地址
        /// <summary>
        /// 得到请求地址
        /// </summary>
        /// <returns>得到请求地址</returns>
        private string GetEntryUrl()
        {
            string EntryUrl = string.Empty;
            switch (en)
            {
                case EmailType.email126:
                    EntryUrl = mail126;
                    break;
                case EmailType.email163:
                    EntryUrl = mail163;
                    break;
                default:
                    break;
            }
            return string.Format(EntryUrl, uName, pwd);
        }
        #endregion

        #region 枚举来定义邮箱类型
        public enum EmailType
        {
            email126, email163
        }
        #endregion

        private GetEmailLinkMan()
        {
        } //封闭接口
    }

    public class Person
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}