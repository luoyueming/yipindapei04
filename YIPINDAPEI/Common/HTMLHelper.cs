using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewXzc.Common
{
    public static class HTMLHelper
    {
        /// <summary>
        /// 获得需要的html 2014-02-07 zhangzilong
        /// </summary>
        /// <param name="html">原始html</param>
        /// <param name="start_html">以此为截取开始</param>
        /// <param name="end_html">以此为截取结束</param>
        /// <returns>需要的html</returns>
        public static string GetNeedHTML(string html, string start_html, string end_html)
        {
            string NeedHTML = "";
            html = GetMatchesStr(html, start_html + ".*?" + end_html)[0].ToString();
            NeedHTML = Regex.Replace(html, start_html, "", RegexOptions.IgnoreCase);
            NeedHTML = Regex.Replace(NeedHTML, end_html, "", RegexOptions.IgnoreCase);
            return NeedHTML;
        }

        /// <summary>
        /// 提取HTML代码中的网址 2014-02-07 zhangzilong
        /// </summary>
        /// <param name="htmlCode">html代码</param>
        /// <param name="strRegex">正则字符串</param>
        /// <returns>HTML代码中的网址</returns>
        public static ArrayList GetMatchesStr(string htmlCode, string strRegex)
        {
            ArrayList al = new ArrayList();

            Regex r = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection m = r.Matches(htmlCode);

            for (int i = 0; i < m.Count; i++)
            {
                bool rep = false;
                string strNew = m[i].ToString();

                // 过滤重复的URL 
                foreach (string str in al)
                {
                    if (strNew == str)
                    {
                        rep = true;
                        break;
                    }
                }

                if (!rep) al.Add(strNew);
            }

            al.Sort();

            return al;
        }

        /// <summary>
        /// 获得列表页链接地址列表 2014-02-07 zhangzilong
        /// </summary>
        /// <param name="listUrl">带(*)的规则地址</param>
        /// <param name="start_page">开始页数</param>
        /// <param name="end_page">结束页数</param>
        /// <param name="start_html">以此为截取开始</param>
        /// <param name="end_html">以此为截取结束</param>
        /// <param name="encod">页面编码格式</param>
        /// <returns></returns>
        public static string GetListLink(string listUrl, int start_page, int end_page, string start_html, string end_html, string encod)
        {
            StringBuilder article_html = new StringBuilder();
            article_html.AppendFormat("");
            for (int i = start_page; i < end_page; i++)
            {
                string list_detail_url = listUrl.Replace("(*)", i.ToString());
                string list_html = getHtmlString(list_detail_url, encod).Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("javascript:; ","");
                list_html = GetNeedHTML(list_html, start_html, end_html);
                ArrayList list = GetMatchesStr(list_html, @"href\s*=\s*(?:[\'\""\s](?<1>[^\""\']*)[\'\""])");//提取链接 

                int j = 0;
                foreach (object o in list)
                {
                    j++;
                    string link = o.ToString();
                    link = Regex.Replace(link, "href=", "", RegexOptions.IgnoreCase);
                    link = link.Replace("\"", "").Replace("javascript:;", "");
                    article_html.AppendFormat("<br/>" + link);
                }
            }
            return article_html.ToString();
        }

        /// <summary>
        /// 获得列表页链接地址列表数组 2014-02-18 zhangzilong
        /// </summary>
        /// <param name="listUrl">带(*)的规则地址</param>
        /// <param name="start_page">开始页数</param>
        /// <param name="end_page">结束页数</param>
        /// <param name="start_html">以此为截取开始</param>
        /// <param name="end_html">以此为截取结束</param>
        /// <param name="encod">页面编码格式</param>
        /// <returns></returns>
        public static string GetStrListLink(string listUrl, int start_page, int end_page, string start_html, string end_html, string encod)
        {
            string article_html="";
            for (int i = start_page; i < end_page; i++)
            {
                string list_detail_url = listUrl.Replace("(*)", i.ToString());
                string list_html = getHtmlString(list_detail_url, encod).Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("javascript:; ", "");
                list_html = GetNeedHTML(list_html, start_html, end_html);
                ArrayList list = GetMatchesStr(list_html, @"href\s*=\s*(?:[\'\""\s](?<1>[^\""\']*)[\'\""])");//提取链接 

                int j = 0;
                foreach (object o in list)
                {
                    
                    string link = o.ToString();
                    link = Regex.Replace(link, "href=", "", RegexOptions.IgnoreCase);
                    link = link.Replace("\"", "").Replace("javascript:;", "");
                    if (link.Contains("http://")) 
                    {
                        article_html += link + ",";
                    }
                    j++;
                }
            }
            return article_html;
        }





        /// <summary>
        /// 获取页面源码字符串 2014-02-07 zhangzilong
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <param name="encod">编码格式</param>
        /// <returns>页面源码字符串</returns>
        public static string getHtmlString(string url, string encod)
        {
            string content = "";
            try
            {
                WebRequest myrequest = WebRequest.Create(url);
                WebResponse resultstrs = myrequest.GetResponse();
                StreamReader sr = new StreamReader(resultstrs.GetResponseStream(), System.Text.Encoding.GetEncoding(encod));
                content = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("不支持的视频地址！");
            }
            return content;
        }

        /// <summary>
        /// 获得标签字符串，以","隔开
        /// </summary>
        /// <param name="tag_html">带有标签的字符串</param>
        public static string GetTagStrList(string tag_html)
        {
            ArrayList list = GetMatchesStr(tag_html, @"<a[^>]*?>.*?</a>");//提取链接 
            string tagStrList = "";
            foreach (object o in list)
            {
                string tag = o.ToString();
                tag = Regex.Replace(tag, @"<a[^>]*?>", "", RegexOptions.IgnoreCase);
                tagStrList += tag.Replace("</a>", ",");
            }
            return tagStrList;
        }

       
        
        // 取出文本中的图片地址
        public static string GetImgUrl(string HTMLStr)
        {
            string str = string.Empty;
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>",
                RegexOptions.Compiled);
            Match m = r.Match(HTMLStr);
            if (m.Success)
                str = m.Result("${url}");
            return str;
        }

        public static string GetFileExt(string FullPath)
        {
            if (FullPath != "") return FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
            else return "";
        }

        public static void WriteResponse(string picName, byte[] content)
        {
            System.IO.Stream stream = new System.IO.MemoryStream(content);
            try
            {
                ImgHelper.SaveStreamToFile(stream, picName);
            }
            catch (Exception ex)
            {
                //Response.Write("保存第一步");
            }
        }

        public static string[] GetArticleInfo(string article_url, string encod, string Detail_Need_Start_HTML,string Detail_Need_End_HTML,string start_title, string end_title, string start_content, string end_content, string start_tag, string end_tag)
        {
            string html = "";
            html =getHtmlString(article_url, encod).Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\\", "");
            Detail_Need_Start_HTML = Detail_Need_Start_HTML.Replace("\r", "").Replace("\n", "").Replace("\t", "").Replace("\\", "");
            Detail_Need_End_HTML = Detail_Need_End_HTML.Replace("\r", "").Replace("\n", "").Replace("\t", "");
            html = GetNeedHTML(html, Detail_Need_Start_HTML, Detail_Need_End_HTML);
            string title = "", content = "";
            string[] article_content = new string[3];
            title = GetNeedHTML(html, start_title, end_title);
            article_content[0] = title;
            html = Regex.Replace(html, "\\s{2,}", "");
            //content = GetNeedHTML(html, start_content, end_content);
            int a = html.IndexOf(start_content.Replace("\\", ""));
            int b = html.IndexOf(end_content.Replace("\\", "")) - html.IndexOf(start_content.Replace("\\", ""));
            content = html.Substring(a,b);
            content = Regex.Replace(content, @"<a[^>]*?>", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"</a>", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"<div[^>]*?>", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"</div>", "", RegexOptions.IgnoreCase);
            string tags = "";
            if(!string.IsNullOrEmpty(start_tag))
            {
                tags = GetNeedHTML(html, start_tag, end_tag);
                tags = GetTagStrList(tags);
            }
            article_content[2] = tags;

            string imgFile;
            string righthtml = content;
            imgFile = GetImgUrl(content).Replace('"', ' ').Replace(" ", "");
            if (imgFile != "")
            {
                #region     循环照图片
                bool abc = true;
                //处理图片, 并把新的图片名称，覆盖到现有的，处理完，继续寻找
                while (GetImgUrl(righthtml).Replace('"', ' ').Replace(" ", "").Length > 0 || abc)
                {
                    abc = false;
                    imgFile = GetImgUrl(righthtml).Replace('"', ' ').Replace(" ", "");

                    string Articleimg = content.Substring(content.IndexOf(imgFile));
                    Articleimg = Articleimg.Substring(0, Articleimg.IndexOf('"'));

                    string upext = GetFileExt(Articleimg);
                    string folder = DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd");//userguid;//DateTime.Now.ToString("yyyyMM");
                    string new_folder = ImgHelper.GetImg_Use_Pre(3);
                    if (new_folder.IndexOf("/") >= 0)
                    {
                        new_folder = new_folder.Substring(new_folder.IndexOf("/") + 1);
                    }
                    folder = new_folder + folder;
                    string path = ImgHelper.GetCofigUploadUrl();
                    //如果文件夹不存在，则创建
                    if (!Directory.Exists(path + folder))
                    {
                        Directory.CreateDirectory(path + folder);
                    }


                    string strDateTime = DateTime.Now.ToString("yyMMddhhmmssfff");//取得时间字符串
                    Random ran = new Random();
                    string strRan = Convert.ToString(ran.Next(100, 999));//生成三位随机数
                    string saveName = strDateTime + strRan + upext;

                    //保存到数据库中的文件路径
                    
                    string file_url =  folder + "/" + saveName;

                    string filename = new_folder + DateTime.Now.ToString("yyyy") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("yyMMddhhmmssfff") + "." + upext;
                    string saveToUrl = path + filename;
                    string newUrl = ImgHelper.GetCofigShowUrl() + filename;

                    if (!imgFile.Contains("http")) 
                    {
                        imgFile = "http://www.trjcn.com" + imgFile;
                    }
                    byte[] byte_img = null;
                    try 
                    {
                        byte_img = new System.Net.WebClient().DownloadData(imgFile);
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
                    
                    WriteResponse(saveToUrl, byte_img);
                    newUrl = newUrl.Replace("\\", "/");
                    int index = content.IndexOf(Articleimg);
                    string lefthtml = content;
                    lefthtml = lefthtml.Substring(0, index);
                    righthtml = content.Substring(index + Articleimg.Length);
                    content = lefthtml + newUrl + righthtml;
                }
                #endregion
            }
            else
            {

            }
            article_content[1] = content;
            //Response.Write(title+"</br>"+content);
            return article_content;
        }


        public static string GetStringReplace(string content) 
        {
            content = Regex.Replace(content, @"<p[^>]*?>", "<p>", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"<span[^>]*?>", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"</span>", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"<div[^>]*?>", "", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"</div>", "", RegexOptions.IgnoreCase);
            content = content.Replace("&ldquo;", "");
            content = content.Replace("&rdquo;", "");
            content = content.Replace("ldquo;", "“");
            content = content.Replace("rdquo;", "”");
            content = content.Replace("&amp;", "");
            content = content.Replace("&mdash;", "—");
            content = content.Replace("投融资", "易佰投融资网");
            return content;
        }

        #region 去除HTML标记
        /// <summary>
        /// 去除HTML标记
        /// </summary>
        /// <param name="NoHTML">包括HTML的源码 </param>
        /// <returns>已经去除后的文字</returns>
        public static string NoHTML(string Htmlstring)
        {
            if (!string.IsNullOrEmpty(Htmlstring))
            {
                //删除脚本
                Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
                //删除HTML
                Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
                Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "", RegexOptions.IgnoreCase);
                Htmlstring.Replace("<", "");
                Htmlstring.Replace(">", "");
                Htmlstring.Replace("\r\n", "");
                Htmlstring = HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            }
            else
            {
                Htmlstring = "";
            }

            return Htmlstring;
        }
        #endregion

        /// <summary>
        /// 替换文本中的字符为html字符
        /// </summary>                
        public static string TextToHtml(string theValue)
        {
            theValue = theValue.Replace(",", "''");
            theValue = theValue.Replace("<", "&lt;");
            theValue = theValue.Replace(">", "&gt;");
            theValue = theValue.Replace("\n", "<br/>");
            theValue = theValue.Replace(" ", "&nbsp;");
            return theValue;
        }
    }
}
