using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace NewXzc.Web.Common
{
    public class DwResumeHelper
    {
        public DwResumeHelper()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string[] video_Analysis(string url)
        {
            string[] video_message;
            try
            {
                if (url.IndexOf("youku") > 0)
                {
                    video_message = getMessage_youku(getHtmlString(url));
                }
                else if (url.IndexOf("yinyuetai") > 0)
                {
                    video_message = getMessage_yinyuetai(getHtmlString(url));
                }
                else if (url.IndexOf("ku6") > 0)
                {
                    video_message = getMessage_ku6(url);
                }
                else if (url.IndexOf("56.com") > 0)
                {
                    video_message = getMessage_wuliu(getHtmlString(url), url);
                }
                else if (url.IndexOf("tudou") > 0)
                {
                    video_message = getMessage_tudou(url);
                }
                else if (url.IndexOf("sohu") > 0)
                {
                    video_message = getMessage_yinyuetai(getHtmlString(url));
                }
                else
                {
                    video_message = null;
                }
                //else if (url.IndexOf("qiyi") > 0)
                //{
                //    video_message = getMessage_qiyi(getHtmlString(url), url);
                //}

            }
            catch
            {
                video_message = null;
            }

            return video_message;
        }

        #region
        /// <summary>
        /// 获取页面源码字符串
        /// </summary>
        /// <param name="url">视频页面地址</param>
        /// <returns></returns>
        public string getHtmlString(string url)
        {
            string content = "";
            try
            {
                WebRequest myrequest = WebRequest.Create(url);
                WebResponse resultstrs = myrequest.GetResponse();
                string encod = "";
                if (url.IndexOf("sohu") > 0)
                {
                    encod = "GB2312-80";
                }
                else if (url.IndexOf("ku6") > 0 || url.IndexOf("163.") > 0 || url.IndexOf("taobao") > 0 || url.IndexOf("360.") > 0 || url.IndexOf("tmall.") > 0)
                {
                    encod = "GBK";
                }
                else if (url.IndexOf("sina") > 0 || url.IndexOf("qq.") > 0)
                {
                    encod = "gb2312";
                }
                else
                {
                    encod = "utf-8";
                }
                StreamReader sr = new StreamReader(resultstrs.GetResponseStream(), System.Text.Encoding.GetEncoding(encod));


                content = sr.ReadToEnd();
                sr.Close();


            }
            catch (Exception ex)
            {
                content = null;
            }
            return content;
        }




        /// <summary>
        /// 返回优酷视频：图片地址、标题、链接地址
        /// </summary>
        /// <param name="content">页面源码字符串</param>
        /// <returns></returns>
        private string[] getMessage_youku(string content)
        {
            string[] message_youku = new string[3];
            try
            {
                #region 获取页面标题信息
                string startString = "<title>";
                string endString = "</title>";
                string gettitlestrs = "";
                Regex start_strs = new Regex(startString);  //定义开始字符串的与此同时表达式
                Regex end_strs = new Regex(endString);  //定义结束字符串的与此同时表达式

                Match sm = start_strs.Match(content);
                Match em = end_strs.Match(content);
                if (sm.Success & em.Success)
                {
                    gettitlestrs = content.Substring(sm.Index + startString.Length, em.Index - sm.Index - startString.Length);//--------------
                    message_youku[0] = gettitlestrs;
                }
                #endregion

                #region 获取缩略图
                string patrn = @"id=""s_baidu1"" href=""(?<pic>[\s\S]*?)""";

                string picStr = searchExp_youku(content, patrn, "pic");
                //string y_content = getHtmlString(picStr);
                //string new_str = y_content.Substring(y_content.IndexOf("scope.picLst ="));
                //int last_index = new_str.IndexOf("\"]");
                //string ss = new_str.Substring(0, last_index);
                //string new_picStr = ss.Substring(ss.IndexOf("http:"));
                message_youku[1] = picStr;
                #endregion

                #region 获取视频播放地址
                string patrn1 = @"id=""link2"" value=""(?<http>[\s\S]*?)""";
                message_youku[2] = searchExp_youku(content, patrn1, "http");
                #endregion
            }
            catch
            {
                message_youku = null;
            }
            return message_youku;
        }

        /// <summary>
        /// 优酷正则解析
        /// </summary>
        /// <param name="strng">网页源码字符串</param>
        /// <param name="patrn">正则表达式</param>
        /// <param name="_pi">解析部分</param>
        /// <returns></returns>
        private string searchExp_youku(string strng, string patrn, string _pi)
        {


            string picStr = "";

            try
            {
                Regex re = new Regex(@patrn);
                MatchCollection mc = re.Matches(strng);
                string fileUrl = "";
                foreach (Match m in mc)
                {
                    fileUrl = m.Groups[_pi].ToString();
                }

                if (_pi != "http")
                {
                    picStr = fileUrl.Substring(fileUrl.IndexOf("pic=") + 4);

                }
                else
                {
                    picStr = fileUrl;
                }
            }
            catch
            {
                picStr = null;
            }



            return picStr;
        }
        #endregion


        /// <summary>
        /// 返回音悦台视频：图片地址、标题、链接地址
        /// </summary>
        /// <param name="content">页面源码字符串</param>
        /// <returns></returns>
        private string[] getMessage_yinyuetai(string content)
        {
            string[] message_yinyuetai = new string[3];
            try
            {
                #region 获取页面标题信息
                string patrn = @"property=""og:title"" content=""([^<]*)""";
                message_yinyuetai[0] = searchExp_yinyuetai_wuliu(content, patrn);
                #endregion

                #region 获取缩略图
                //string aa, bb, jpg;
                string patrn1 = @"property=""og:image"" content=""([^<]*)""";
                string c_url = searchExp_yinyuetai_wuliu(content, patrn1);
                //int index = content.IndexOf("Fartists");
                //aa = content.Substring(index + 11, content.IndexOf(".jpg"));
                //bb = aa.Substring(aa.IndexOf("%") + 3);
                //jpg = c_url.Substring(c_url.IndexOf(".jpg"), c_url.IndexOf(".jpg") + 4);
                //c_url = c_url.Substring(0, c_url.LastIndexOf("/") + 1);
                message_yinyuetai[1] = c_url;
                #endregion

                #region 获取视频播放地址
                string patrn2 = @"property=""og:videosrc"" content=""([^<]*)""";
                message_yinyuetai[2] = searchExp_yinyuetai_wuliu(content, patrn2);
                #endregion
            }
            catch
            {
                message_yinyuetai = null;
            }
            return message_yinyuetai;
        }

        /// <summary>
        /// 返回56视频：图片地址、标题、链接地址
        /// </summary>
        /// <param name="content">页面源码字符串</param>
        /// <returns></returns>
        private string[] getMessage_wuliu(string content, string url)
        {
            string[] message_wuliu = new string[3];
            try
            {
                #region 获取页面标题信息
                string patrn = @"name=""description"" content=""([^<]*)""";
                message_wuliu[0] = searchExp_yinyuetai_wuliu(content, patrn);
                #endregion

                #region 获取缩略图
                string pat = @"""img"":""([^<]*)""";
                Regex re = new Regex(@pat);
                MatchCollection mc = re.Matches(content);
                string fileUrl = "";
                foreach (Match m in mc)
                {
                    fileUrl = m.Groups[0].ToString();
                }
                string aa = fileUrl.Substring(fileUrl.IndexOf("http"));
                aa = aa.Split(',')[0].Replace("\"","");
                string imgUrl = aa.Replace("\\","");

                message_wuliu[1] = imgUrl.Trim();
                #endregion

                #region 获取视频播放地址
                string str_v = url;
                string videoStr = str_v.Substring(str_v.LastIndexOf("/") + 1);
                string videoId = videoStr.Substring(0, videoStr.Length - 5);//得到56视频特殊后缀码
                message_wuliu[2] = "http://player.56.com/" + videoId + ".swf";
                #endregion
            }
            catch
            {
                message_wuliu = null;
            }
            return message_wuliu;
        }

        /// <summary>
        /// 音悦台、56网 正则解析规则
        /// </summary>
        /// <param name="strng">网页源码字符串</param>
        /// <param name="patrn">正则表达式</param>
        /// <returns></returns>
        public string searchExp_yinyuetai_wuliu(string strng, string patrn)
        {
            string fileUrl = "";
            string picStr = "";
            try
            {
                strng = Regex.Replace(strng, @"\s+", " ");
                Regex re = new Regex(@patrn);

                MatchCollection mc = re.Matches(strng);




                foreach (Match m in mc)
                {

                    fileUrl = m.Groups[0].ToString();
                }

                picStr = fileUrl.Substring(fileUrl.IndexOf("content=") + 8);
                int lastIndex = picStr.Length - 2;
                picStr = picStr.Substring(1, lastIndex);
            }
            catch
            {
                picStr = null;
            }
            return picStr;
        }


        /// <summary>
        /// 获取土豆视频信息
        /// </summary>
        /// <param name="url">视频地址</param>
        /// <returns></returns>
        private string[] getMessage_tudou(string url)
        {
            string[] message_tudou = new string[3];
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("http://api.tudou.com/v3/gw?method=repaste.info.get&appKey=myKey&format=xml&url=" + url);

                XmlNode node = doc.FirstChild;  //获取文档的第一个节点

                foreach (XmlNode n in node.ChildNodes)
                {
                    if (n.Name == "itemInfo")
                    {
                        foreach (XmlElement x in n.ChildNodes)
                        {

                            if (x.Name == "title")
                            {
                                message_tudou[0] = x.InnerText;
                            }
                            if (x.Name == "picUrl")
                            {
                                message_tudou[1] = x.InnerText;
                            }
                            if (x.Name == "outerPlayerUrl")
                            {
                                message_tudou[2] = x.InnerText;
                            }
                        }
                    }

                }
            }
            catch {
                message_tudou = null;
            }
            return message_tudou;
        }


        /// <summary>
        /// 获取ku6视频信息
        /// </summary>
        /// <param name="url">视频地址</param>
        /// <returns></returns>
        private string[] getMessage_ku6(string url)
        {
            string[] message_ku6 = new string[3];
            try
            {
                //XmlDocument doc = new XmlDocument();
                //doc.Load("http://v.ku6.com/repaste.htm?url=" + url);
                XElement data = XElement.Load("http://v.ku6.com/repaste.htm?url=" + url);
                var query = from x in data.Descendants("result")
                            // from item in x.Elements("coverurl")
                            select new
                            {
                                coverurl = x.Element("coverurl").Value,
                                flash = x.Element("flash").Value,
                                title = x.Element("title").Value
                            };
                foreach (var item in query)
                {
                    message_ku6[0] = item.title;
                    message_ku6[1] = item.coverurl;
                    message_ku6[2] = item.flash;
                }

            }
            catch {
                message_ku6 = null;
            }
            return message_ku6;
        }


        ///// <summary>
        ///// 获取奇艺网视频信息
        ///// </summary>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //private string[] getMessage_qiyi(string content, string url)
        //{
        //    //url : "http://www.qiyi.com/marketing/20110905/fd5ae657140ba910.html",//页面url
        //    //   title : "富士施乐小小环保举动改变地球",//页面标题
        //    //   pid : "13444",//父对象id
        //    //   ptype : "2",//父对象类型
        //    //   navigation : "&lt;a href=&quot;http://www.qiyi.com/index.html&quot;&gt;首页&lt;/a&gt; &gt; &lt;strong&gt;富士施乐小小环保举动改变地球&lt;/strong&gt;",//页面导航
        //    //   videoId : "c0487651039a4c82ac2e15a168b06582",//视频id
        //    //   albumId : "129771",//专辑id
        //    //   tvId : "125958",//剧集id

        //    string[] message_qiyi = new string[3];
        //    string date, qiyi_class, swfN, title, pid, ptype, videoid, albumid, tvid;
        //    try
        //    {
        //        string[] qi = url.Split(new char[] { '/', '/', '/', '/', '/' });
        //        date = qi[4];
        //        qiyi_class = qi[3];
        //        swfN = qi[5].Substring(0, qi[5].IndexOf(".html"));

        //        string q_a = content.Substring(content.IndexOf("title : ") + 8);
        //        title = q_a.Substring(0, q_a.IndexOf(",//页面标题") - 1);

        //        string q_b = content.Substring(content.IndexOf("pid :") + 7);
        //        pid = q_b.Substring(0, q_b.IndexOf(",//父对象") - 1);

        //        string q_c = content.Substring(content.IndexOf("ptype :") + 9);
        //        ptype = q_c.Substring(0, q_c.IndexOf(",//父对象类型") - 1);

        //        string q_d = content.Substring(content.IndexOf("videoId :") + 11);
        //        videoid = q_d.Substring(0, q_d.IndexOf(",//视频id") - 1);

        //        string q_e = content.Substring(content.IndexOf("albumId :") + 11);
        //        albumid = q_e.Substring(0, q_e.IndexOf(",//专辑id") - 1);

        //        string q_f = content.Substring(content.IndexOf("tvId :") + 8);
        //        tvid = q_f.Substring(0, q_f.IndexOf(",//剧集id") - 1);


        //        message_qiyi[0] = title;
        //        message_qiyi[1] = "http://www.qiyipic.com/thumb/" + date + "/v" + tvid + ".jpg";
        //        message_qiyi[2] = "http://player.video.qiyi.com/" + videoid + "/0/3000/" + qiyi_class + "/" + date + "/fd5ae657140ba910.swf-pid=" + pid + "-ptype=" + ptype + "-albumId=" + albumid + "-tvId=" + tvid;

        //    }
        //    catch
        //    {
        //        throw new Exception("不支持的视频地址！");
        //    }
        //    return message_qiyi;
        //}

    }
}