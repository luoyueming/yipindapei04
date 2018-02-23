using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using NewXzc.Web.Common;
using NewXzc.DBUtility;
using NewXzc.Common;
using NewXzc.Web.Common.uhelper;

namespace NewXzc.Web.templatecs.Article
{
    public class Article_Subject_Detail : Base.BasePage
    {
        BLL.HRENH_ARTICLE_REVIEW_REPLY review_bll = new BLL.HRENH_ARTICLE_REVIEW_REPLY();
        BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

        BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();

        StringBuilder sbr = new StringBuilder();
        DataSet ds = null;
        int articleid = 0;
        int type = -1;
        int stype = 0;
        int types_pid = 0;
        int tpid = 0;

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.Head_Article head = new Head_Article();
            head.Init_Head(context, 0);

            context.Put("title", "文章详情_衣品搭配");

            #endregion

            articleid = String_Manage.Return_Request_Int("id", 0);


            Model.HRENH_ARTICLE article_model = new BLL.HRENH_ARTICLE().GetModel(articleid);

            if (article_model != null)
            {

                stype =Convert.ToInt32(article_model.TYPES);
                types_pid = article_model.TYPES_PID;

                #region  加载子级栏目
                //Get_Son_Type(context);
                #endregion


                #region  获取面包屑导航

                int ctypes = type;

                if (types_pid > 0)
                {
                    ctypes = types_pid;
                }
                else
                {
                    ctypes = stype;
                    stype = -1;
                }

                string mbxnav = ArticleHelper.Get_Article_Nav(ctypes, stype, 1);
                context.Put("mbxnav", mbxnav);

                #endregion

                int pageid = 2100;

                #region  详情页相关阅读上方广告图

                //context.Put("topad", ArticleHelper.Get_Tj_Img(1, pageid, 1008));

                #endregion

                #region  详情页相关阅读轮播图

                //context.Put("topadlb", ArticleHelper.Get_Tj_Img(0, pageid, 1009));

                #endregion

                #region  详情页热门推荐轮播图

                //context.Put("lunbo1", ArticleHelper.Get_Tj_Img(0, pageid, 1005));

                #endregion

                #region  获取最新排行的列表

                //context.Put("zxph", ArticleHelper.Get_Zxph(10));

                #endregion

                #region  详情页论坛精华

                //context.Put("ltjh_ad", ArticleHelper.Get_Tj_Img(3, pageid, 1006));

                #endregion


                #region  今日热点

                context.Put("jrrd", Get_Jrrd(context));

                #endregion

                #region  每日精选

                //context.Put("mrjx_ad", ArticleHelper.Get_Tj_Img(0, pageid, 1007));

                context.Put("mrjx_ad", ArticleHelper.Get_Tj_Img(0, pageid, 1010));

                #endregion

                #region  大家都在搜

                context.Put("djdzs", ArticleHelper.Get_Tj_Img(0, pageid, 1004));

                #endregion


                #region  推荐位
                ////最新娱乐热点
                //context.Put("newzx", ArticleHelper.Get_New_ZX(8, "", 10));

                #region  推荐公共项
                //NewXzc.Web.templatecs.Article.Article_Search_Common article_common = new Article_Search_Common();

                ////头部广告
                //article_common.Init_Common_Ad(context);

                ////最火娱乐资讯
                //article_common.Init_Zhylzx(context);

                ////相关搜索上方广告
                //Init_Search_Ad(context);

                ////相关搜索
                //Init_Xgss(context);

                ////为您推荐
                //article_common.Init_Wntj(context);

                ////右侧推荐
                //article_common.Init_Rgiht_Common_Ad(context);

                ////相关阅读
                //Init_Xgyd(context);

                #endregion

                #endregion

                //获取文章详情
                GetDetail(context);
            }
            else
            {
                context.Put("redirecturl", "/404");
            }
        }


        /// <summary>
        /// 加载子级栏目
        /// </summary>
        /// <param name="context"></param>
        private void Get_Son_Type(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            if (types_pid == 0)
            {
                ds = type_bll.GetList(" pid=" + stype + " and state=1 ");
                tpid = stype;
            }
            else
            {
                ds = type_bll.GetList(" pid=" + types_pid + " and state=1 ");
                tpid = types_pid;
            }

            context.Put("tpid", tpid);

            string type_url_name = "";


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int sid = 0;
                string sname = "";
                string surl = "";
                

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sid = Convert.ToInt32(dr["id"].ToString());
                    sname = dr["typename"].ToString();

                    type_url_name = ArticleHelper.Get_Nav_Type_Name(tpid);

                    //if (tpid == 2 || tpid == 5)
                    //{
                    //    surl = type_url_name + "_1_" + sid;
                    //}
                    //else
                    //{
                        surl = type_url_name + "_1_" + sid;
                    //}
                    

                    sbr.AppendFormat("<li>");
                    if (sid == stype)
                    {
                        sbr.AppendFormat("<a class=\"active\" href=\"{0}\" title=\"{1}\">{1}</a>", surl, sname);
                    }
                    else
                    {
                        sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", surl, sname);
                    }
                    sbr.AppendFormat("</li>");
                }
            }
            else
            {

                type_url_name = ArticleHelper.Get_Nav_Type_Name(tpid);

                sbr.AppendFormat("<li>");
                sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", type_url_name, ArticleHelper.Get_Nav_Type_Name(tpid,tpid));
                sbr.AppendFormat("</li>");
            }


            context.Put("stypelist", sbr.ToString());
        }

        /// <summary>
        /// 相关搜索上方广告
        /// </summary>
        /// <param name="context"></param>
        private void Init_Search_Ad(NVelocity.VelocityContext context)
        {
            StringBuilder sbr = new StringBuilder();
            DataSet ds = null;
            int pid = 1300;

            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(0, " pageid=" + pid + " and cid=1009 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 1, 10);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"90\" width=\"640\"";


                    //if (openstyle != 1 && openstyle != 2)
                    //{
                    //    sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">", title);
                    //    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                    //    sbr.AppendFormat("</a>");
                    //}
                    //else
                    //{
                    //    sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                    //    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                    //    sbr.AppendFormat("</a>");
                    //}

                    string hittpurl = " href=\"javascript:;\" ";
                    show_title = StringHelper.ReturnNumStr(title, 1, 0);
                    width_height = "height=\"128\" width=\"128\"";

                    //<div class="nn" style="position: relative;"><a href="javascript:;"><img src="$img/01.jpg"><p class="tuceng4"><span class="mone">￥54.00</span></p></a></div>

                    sbr.AppendFormat("<div class=\"nn\" style=\"position: relative;\">");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                    }
                    else
                    {
                        sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                    }

                    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, show_title, width_height);
                    sbr.AppendFormat("<p class=\"tuceng4\"><span class=\"mone\">￥{0}</span></p>", Recruit_Job.Return_Money(desc, 2));
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("</div>");

                }
            }

            context.Put("Search_Ad", sbr.ToString());
        }

        /// <summary>
        /// 相关搜索
        /// </summary>
        /// <param name="context"></param>
        public void Init_Xgss(NVelocity.VelocityContext context)
        {
            StringBuilder sbr = new StringBuilder();
            DataSet ds = null;

            try
            {
                string sql = "select top 6 id,title,types,isimg,img_url from hrenh_article where isend=0 and types in(1,3,4,5) and pub_state=0";

                sql += " order by read_cnt desc,id desc";

                ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];

                        int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                        string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                        string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 10);
                        string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + article_title + "\"";

                        article_url = "href=\"/" + ArticleHelper.Get_Nav_Type_Name(Convert.ToInt32(dr["types"].ToString())) + "/detail_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";
                        int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                        string article_img = "/template/img/nocontent.png";

                        if (isimg == 1)
                        {
                            article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                        }

                        sbr.AppendFormat("<li>");
                        sbr.AppendFormat("<a {0}>{1}", article_url,show_article_title);
                        //sbr.AppendFormat("<img src=\"{0}\" height=\"80\" width=\"140\" alt=\"{1}\">", article_img, article_title);
                        //sbr.AppendFormat("<p>{0}</p>", show_article_title);
                        //sbr.AppendFormat("<i class=\"vedioIcon\"></i>");
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</li>");

                    }
                }
                else
                {
                    sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
                }
            }
            catch (Exception ex)
            {
                sbr.AppendFormat(ex.ToString());
            }

            context.Put("xgss", sbr.ToString());
        }


        /// <summary>
        /// 相关阅读
        /// </summary>
        /// <param name="context"></param>
        public void Init_Xgyd(NVelocity.VelocityContext context)
        {
            StringBuilder sbr = new StringBuilder();
            DataSet ds = null;
            int pid = 1300;

            try
            {
                ds = recommend_bll.GetList_NewIndex(4, " pageid=" + pid + " and cid=1010 ");

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataRow dr = ds.Tables[0].Rows[i];


                        string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);

                        string title = "";
                        int isimg = 0;
                        string imgurl = "/template/img/nocontent.png";
                        string addtime="";
                        string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                        //链接打开方式，1：在本页面打，2：在新页面打开
                        int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                        try
                        {
                            DataSet ads = DbHelperSQL.Query("select top 1 title,isimg,img_url,addtime from hrenh_article where id=" + desc + " and pub_state=0");

                            if (ads != null && ads.Tables[0].Rows.Count > 0)
                            {
                                for (int j = 0; j < ads.Tables[0].Rows.Count; j++)
                                {
                                    DataRow adr = ads.Tables[0].Rows[j];
                                    title = adr["title"].ToString();
                                    isimg = String_Manage.Return_Int(adr["isimg"].ToString(), 0);
                                    if (isimg == 1)
                                    {
                                        imgurl = ImgHelper.Get_UploadImgUrl(adr["img_url"].ToString(), 1);
                                    }
                                    addtime=TimeParser.ReturnCurTime(adr["addtime"].ToString(),4);
                                }
                            }

                        }
                        catch (Exception ex)
                        {

                        }

                        title = StringHelper.ReturnNumStr(title, 0, 0);
                        string show_title = StringHelper.ReturnNumStr(title, 1, 38);

                        string width_height = "height=\"60\" width=\"90\"";

                        sbr.AppendFormat("<li>");
                        sbr.AppendFormat("<em></em>");
                        sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("{0}", show_title);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("<span>{0}</span>",addtime);
                        sbr.AppendFormat("</li>");


                    }
                }
                else
                {
                    sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
                }
            }
            catch (Exception ex)
            {
                sbr.AppendFormat(ex.ToString());
            }

            context.Put("xgyd", sbr.ToString());
        }



        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="context"></param>
        private void GetDetail(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            context.Put("id", articleid);

            string ishave_rcode = "0";

            string result = "ok";

            if (articleid == 0)
            {
                context.Put("redirecturl", "/404");
            }
            else
            {
                try
                {
                    ds = DbHelperSQL.Query("select top 1 id,title,isimg,img_url,isnull((select contents from hrenh_article_html where article_id=a.id),'') as contents,(isnull(read_cnt,0)+FALSH_READ_CNT) as read_cnt,isnull((select count(*) from hrenh_article_REVIEW_REPLY where article_id=a.id and reply_id=0),0) as review_cnt,edittime,types,isnull(vurl,'') as video_url,isnull(keywords,'文章详情') as keywords,isnull(description,'文章详情') as description,isnull((select top 1 uname from hrenh_article_author where id=a.AuthorId),'') as author,isnull((select top 1 USER_HEAD from hrenh_article_author where id=a.AuthorId),'') as uhead from hrenh_article a where id=" + articleid + " ");// and a.pub_state=0

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DbHelperSQL.ExecuteSql("update hrenh_article set read_cnt=read_cnt+1 where id=" + articleid + "");

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];

                            #region  文章详情的信息
                            int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                            string article_title = dr["title"].ToString();

                            context.Put("atitle", article_title);

                            type = String_Manage.Return_Int(dr["types"].ToString(), -1);

                            if (type == 6)
                            {
                                context.Put("title", StringHelper.ReturnNumStr(article_title, 1, 0) + "-衣品搭配网|女装穿衣搭配");
                            }
                            else
                            {
                                context.Put("title", StringHelper.ReturnNumStr(article_title, 0, 0) + "-衣品搭配网|女装穿衣搭配");
                            }

                            //context.Put("navurl", "/" + ArticleHelper.Get_Nav_Type_Name(type));
                            //context.Put("navnames", ArticleHelper.Get_Type_Name(type));

                            string article_contents = dr["contents"].ToString();
                            string article_addtime = TimeParser.ReturnCurTime(dr["edittime"].ToString(), 0);
                            int read_cnt = String_Manage.Return_Int(dr["read_cnt"].ToString(), 0) + 1;
                            int review_cnt = String_Manage.Return_Int(dr["review_cnt"].ToString(), 0);
                            //视频地址
                            string video_url = String_Manage.Return_Str(dr["video_url"].ToString(), "");

                            context.Put("keywords", dr["keywords"].ToString());
                            context.Put("description", dr["description"].ToString());

                            string author=String_Manage.Return_Str(dr["author"].ToString(),"");

                            #endregion

                            #region  加载文章详情


                            sbr.AppendFormat("<div class=\"portrait-header newportrait\">");
                            sbr.AppendFormat("<h1>{0}</h1>", article_title);
                            sbr.AppendFormat("<p class=\"clearfix\">");
                            if (author != "")
                            {
                                sbr.AppendFormat("<em class=\"pic\"><img src=\"{0}\" alt=\"{1}\"></em>", ImgHelper.GetCofigShowUrl() + dr["uhead"].ToString(), author);
                                sbr.AppendFormat("<em>编辑：<i>{0}</i></em>", author);
                                //sbr.AppendFormat("<em style=\"margin-left:53px\">{0}</em>", article_addtime);
                            }
                            //else
                            //{
                            //    sbr.AppendFormat("<em style=\"margin-left:0\">{0}</em>", article_addtime);
                            //}

                            sbr.AppendFormat("<span class=\"org\"><i>{0}</i>来源于：<a href=\"http://www.ypindapei.com/\" title=\"衣品搭配\" target=\"_blank\">衣品搭配</a></span>", article_addtime);


                            
                            sbr.AppendFormat("<span>");
                            //sbr.AppendFormat("<span class=\"comment-sum\"><b>{0}</b> 评论</span>", review_cnt);
                            //sbr.AppendFormat("<span><b></b>分享</span>");

                            sbr.AppendFormat("<span>");
                            sbr.AppendFormat("分享：<span class=\"bdsharebuttonbox\" id=\"share\" style=\"float: right;\"><a href=\"#\" class=\"bds_weixin\" data-cmd=\"weixin\" title=\"分享到微信\"></a><a href=\"#\" class=\"bds_tsina\" data-cmd=\"tsina\" title=\"分享到新浪微博\"></a><a href=\"#\" class=\"bds_qzone\" data-cmd=\"qzone\" title=\"分享到QQ空间\"></a></span>");
                            sbr.AppendFormat("</span>");

                            sbr.AppendFormat("</span>");
                            sbr.AppendFormat("</p>");
                            //sbr.AppendFormat("</div>");
                            //sbr.AppendFormat("<div class=\"hren-header\">");
                            //sbr.AppendFormat("<span class=\"cbhren\">唱吧红人</span>");
                            //sbr.AppendFormat("<span class=\"hrenID\">ID：<em>SHL-123456789</em></span>");
                            //sbr.AppendFormat("<span class=\"hrenfans\">唱吧粉丝：<em>33万</em></span>");
                            //sbr.AppendFormat("<span class=\"hrentx\"><img src=\"http://e.hiphotos.baidu.com/baike/w%3D268%3Bg%3D0/sign=52775554b6003af34dbadb660d11a161/d50735fae6cd7b89faf58fde0f2442a7d8330ed5.jpg\"></span>");
                            //sbr.AppendFormat("<span class=\"hrenfonc\">关注：34</span>");
                            //sbr.AppendFormat("<span class=\"hrenfc\">粉丝：16334</span>");
                            //sbr.AppendFormat("<span class=\"hrenName\">魏道道</span>");
                            //sbr.AppendFormat("<span class=\"hrenloc\">来自：<em>北京</em><em>北京</em><em>朝阳区</em></span>");
                            //sbr.AppendFormat("<a class=\"hrenfasixin\"><img src=\"/template/img/fasixin.jpg\"></a>");
                            //sbr.AppendFormat("<a class=\"hrenjiaguanzhu\"><img src=\"template/img/jiaguanzhu.jpg\"></a>");
                            sbr.AppendFormat("</div>");


                            sbr.AppendFormat("<div class=\"main-content-body\">");
                            //sbr.AppendFormat("<p class=\"abstract\">[<strong>摘要</strong>]她尝试了各种事情，也尝试了和王子约会，然后发现，还是做女王大人更有趣。</p>");

                            if (video_url != "")
                            {
                                if (video_url.Contains("swf"))
                                {
                                    sbr.AppendFormat("<p style=\"text-align: center;\"><embed width=\"600\" height=\"390\" allowfullscreen=\"true\" allowscriptaccess=\"never\" menu=\"false\" loop=\"false\" play=\"true\" wmode=\"transparent\" src=\"{0}\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" class=\"edui-faked-video\" type=\"application/x-shockwave-flash\"></p>", video_url);
                                }
                                else
                                {
                                    sbr.AppendFormat("<object id=\"CuPlayerVideo_video_object\" width=\"100%\" height=\"400\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\"><param name=\"movie\" value=\"/Flash/player.swf?v=2.5\"><param name=\"flashvars\" value=\"JcScpFile=/Flash/CuSunV2set.xml&amp;JcScpVideoPath={0}&amp;JcScpImg=/Flash/start.jpg\"><param name=\"allowFullScreen\" value=\"true\"><param name=\"allowScriptAccess\" value=\"always\"><param name=\"wmode\" value=\"Transparent\"><embed id=\"CuPlayerVideo_video_embed\" src=\"/Flash/player.swf?v=2.5\" type=\"application/x-shockwave-flash\" allowscriptaccess=\"always\" allowfullscreen=\"true\" wmode=\"Transparent\" width=\"100%\" height=\"400\" flashvars=\"JcScpFile=/Flash/CuSunV2set.xml&amp;JcScpVideoPath={0}&amp;JcScpImg=/Flash/start.jpg\"></object>", video_url);
                                }
                            }

                            if (article_contents.Contains("欲查看更多精彩内容请扫描下方二维码"))
                            {
                                ishave_rcode = "1";
                            }


                            sbr.AppendFormat("{0}", article_contents.Replace("<img", "<img alt='衣品搭配'").Replace("undefined", ""));

                            sbr.AppendFormat("</div>");


                            #endregion
                        }
                    }
                    else
                    {
                        result = "no";
                        sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
                    }
                }
                catch (Exception ex)
                {
                    sbr.Append(ex.ToString());
                }

                if (type == 6)
                {
                    result = "no";
                }

                context.Put("detail", sbr.ToString());
                context.Put("result", result);

                context.Put("ishave_rcode", ishave_rcode);

                ////获取评论、回复内容列表
                //GetReviewContent(context);

                context.Put("type", type);

                //获取指南栏目下最新的10条记录
                GetList(context);

                //获取上衣、裤装、裙装、女鞋、包包、配饰、美妆等栏目下的最新的16条记录
                GetList_fuzhuang(context);
            }

        }


        /// <summary>
        /// 获取第一页的评论的内容
        /// </summary>
        /// <returns></returns>
        private void GetReviewContent(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            PageClass pc = new PageClass();
            DataSet reply_ds = null;

            int record_cnt = 0;
            int page_cnt = 0;
            int curpage = 0;
            int pagesize = 10;

            ds = pc.TagList_New("*", " review_addtime desc ", " hrenh_review_reply_article ", " and reply_id=0 and islike=0 and article_id=" + articleid + " ", curpage, pagesize, " id,userid,review_uname,review_user_head,review_contents,review_addtime,like_cnt ");

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                record_cnt = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                page_cnt = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[2].Rows[i];

                    #region  评论内容的信息
                    int pid = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    int puserid = String_Manage.Return_Int(dr["userid"].ToString(), 0);
                    string uname = String_Manage.Return_Str(dr["review_uname"].ToString(), "");
                    string show_name = StringHelper.ReturnNumStr(uname, 0, 20);
                    string user_head = ImgHelper.Return_User_Head(dr["review_user_head"].ToString(), 2);
                    string contents = String_Manage.Return_Str(dr["review_contents"].ToString(), "");
                    string addtime = dr["review_addtime"].ToString();
                    addtime = TimeParser.ReturnCurTime(addtime, 1);

                    string is_href = "href=\"javascript:void(0)\"";
                    string width_height = "height=\"48\" width=\"48\"";

                    int like_cnt = String_Manage.Return_Int(dr["like_cnt"].ToString(), 0);
                    int is_like_own = review_bll.GetRecordCount(" reply_id=" + pid + " and userid=" + UserID_HongRenHui + " and islike=1 ");

                    #endregion

                    #region  加载评论及回复的内容
                    sbr.AppendFormat("<div class=\"activity-details-discuss-item clearfix\">");
                    sbr.AppendFormat("<a class=\"item-photo\" {0} title=\"{1}\">", is_href, uname);
                    sbr.AppendFormat("<img src=\"{0}\" {1} alt=\"{2}\">", user_head, width_height, uname);
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("<div class=\"item-content\">");
                    sbr.AppendFormat("<h3>");
                    sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", is_href, uname, show_name);
                    sbr.AppendFormat("<em>{0}</em>", addtime);
                    sbr.AppendFormat("</h3>");
                    sbr.AppendFormat("<p>{0}</p>", contents);
                    sbr.AppendFormat("<p class=\"item-btns\">");
                    if (is_like_own == 0)
                    {
                        sbr.AppendFormat("<a class=\"laud laud_up\" href=\"javascript: void(0);\">{0}</a>", like_cnt);
                    }
                    else
                    {
                        sbr.AppendFormat("<a class=\"laud laud_up lauded\" href=\"javascript: void(0);\">{0}</a>", like_cnt);
                    }

                    if (UserID_HongRenHui == puserid)
                    {
                        sbr.AppendFormat("<a class=\"reply_report\" href=\"javascript: void(0);\">举报</a>");
                        sbr.AppendFormat("<a class=\"reply_del\" href=\"javascript: void(0);\">删除</a>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a class=\"reply_report\" href=\"javascript: void(0);\">举报</a>");
                        sbr.AppendFormat("<a class=\"reply_article\" href=\"javascript: void(0);\">回复</a>");
                    }

                    sbr.AppendFormat("</p>");

                    #region  加载回复的列表

                    reply_ds = DbHelperSQL.Query("select * from hrenh_review_reply_article where review_pid=" + pid + " and islike=0 ");

                    if (reply_ds != null & reply_ds.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < reply_ds.Tables[0].Rows.Count; j++)
                        {
                            DataRow rdr = reply_ds.Tables[0].Rows[j];

                            #region  回复的内容的信息
                            int id = String_Manage.Return_Int(rdr["id"].ToString(), 0);
                            int reply_userid = String_Manage.Return_Int(rdr["userid"].ToString(), 0);
                            string reply_uname = String_Manage.Return_Str(rdr["review_uname"].ToString(), "");
                            string reply_show_name = StringHelper.ReturnNumStr(reply_uname, 0, 20);
                            user_head = ImgHelper.Return_User_Head(rdr["review_user_head"].ToString(), 2);
                            string reply_contents = String_Manage.Return_Str(rdr["review_contents"].ToString(), "");
                            string reply_addtime = rdr["review_addtime"].ToString();

                            reply_addtime = TimeParser.ReturnCurTime(reply_addtime, 1);
                            like_cnt = String_Manage.Return_Int(rdr["like_cnt"].ToString(), 0);
                            is_like_own = review_bll.GetRecordCount(" reply_id=" + id + " and userid=" + UserID_HongRenHui + " and islike=1 ");
                            #endregion

                            sbr.AppendFormat("<div class=\"activity-details-discuss-item clearfix\">");
                            sbr.AppendFormat("<a class=\"item-photo\" {0} title=\"{1}\"s>", is_href, reply_uname);
                            sbr.AppendFormat("<img src=\"{0}\" {1} alt=\"{2}\">", user_head, width_height, reply_uname);
                            sbr.AppendFormat("</a>");
                            sbr.AppendFormat("<div class=\"item-content\">");
                            sbr.AppendFormat("<h3>");
                            sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", is_href, reply_uname, reply_show_name);
                            sbr.AppendFormat("<em>{0}</em>", reply_addtime);
                            sbr.AppendFormat("</h3>");
                            sbr.AppendFormat("<p>{0}</p>", reply_contents);
                            sbr.AppendFormat("<p class=\"item-btns\">");
                            if (is_like_own == 0)
                            {
                                sbr.AppendFormat("<a class=\"laud laud_under\" href=\"javascript: void(0);\">{0}</a>", like_cnt);
                            }
                            else
                            {
                                sbr.AppendFormat("<a class=\"laud laud_under lauded\" href=\"javascript: void(0);\">{0}</a>", like_cnt);
                            }


                            if (UserID_HongRenHui == reply_userid)
                            {
                                sbr.AppendFormat("<a class=\"reply_under_report\" href=\"javascript: void(0);\">举报</a>");
                                sbr.AppendFormat("<a class=\"reply_under_del\" href=\"javascript: void(0);\">删除</a>");
                            }
                            else
                            {
                                sbr.AppendFormat("<a class=\"reply_under_report\" href=\"javascript: void(0);\">举报</a>");
                                sbr.AppendFormat("<a class=\"reply_under_article\" href=\"javascript: void(0);\">回复</a>");
                            }

                            sbr.AppendFormat("</p>");
                            sbr.AppendFormat("<input type=\"hidden\" class=\"hidval\" data-id=\"{0}\" data-type=\"{1}\" data-pid=\"{2}\">", id, type, pid);
                            sbr.AppendFormat("</div>");
                            sbr.AppendFormat("</div>");

                        }
                    }
                    #endregion

                    sbr.AppendFormat("<input type=\"hidden\" class=\"hidval_first\" data-id=\"{0}\" data-type=\"{1}\" data-pid=\"{2}\">", pid, type, pid);
                    sbr.AppendFormat("</div>");
                    sbr.AppendFormat("</div>");


                    #endregion
                }
            }
            else
            {
                //sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
                sbr.Append("");
            }

            int cpage = curpage + 1;

            if (cpage > page_cnt)
            {
                cpage = page_cnt;
            }

            context.Put("reviewcontent", sbr.ToString());
            context.Put("listPage", NewXzc.Common.GenerPage.pageHtml(record_cnt, pagesize, cpage));
        }


        /// <summary>
        /// 获取指南栏目下最新的10条记录
        /// </summary>
        /// <param name="context"></param>
        private void GetList(NVelocity.VelocityContext context)
        {
            StringBuilder listsbr = new StringBuilder();

            int pagesize = 10;

            int zhinan_type = 354;

            string where = " (types=" + zhinan_type + " or types_pid="+zhinan_type+") and isend=0 and pub_state=0 ";

            where += " and id<>"+articleid;

            string sql = "select top " + pagesize + " id,title,isimg,img_url,contents,edittime,(isnull(read_cnt,0)+FALSH_READ_CNT) as read_cnt from hrenh_article where " + where + "order by istop desc,edittime desc";

            ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 34);
                    int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                    string article_img = "/template/img/nocontent.png";

                    if (isimg == 1)
                    {
                        article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                    }

                    string article_contents = StringHelper.ReturnNumStr(dr["contents"].ToString(), 1, 102);
                    string article_contents_noimg = StringHelper.ReturnNumStr(dr["contents"].ToString(), 1, 170);
                    string article_addtime = TimeParser.ReturnCurTime(dr["edittime"].ToString(), 1);

                    string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + article_title + "\"";

                    article_url = "href=\"/zhinan/detail_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    int read_cnt = String_Manage.Return_Int(dr["read_cnt"].ToString(), 0);

                    #endregion

                    #region  加载文章列表

                    show_article_title = StringHelper.ReturnNumStr(article_title, 1, 18);
                    listsbr.AppendFormat("<dl class=\"claint-main-list\">");
                    listsbr.AppendFormat("<dt><a {0}><img src=\"{1}\" alt=\"{2}\"></a></dt>", article_url, article_img, article_title);
                    listsbr.AppendFormat("<dd>");
                    listsbr.AppendFormat("<h3><a {0}>{1}</a></h3>", article_url, show_article_title);
                    listsbr.AppendFormat("<p><a {0}>{1}</a></p>", article_url, article_contents);
                    //listsbr.AppendFormat("<span><i class=\"mar0\">网络红人</i><i>国家运动员</i><i>网络红人</i></span>");
                    listsbr.AppendFormat("<span style=\"border:0;margin-left:0px;color:#999;font-size:12px;\" >{0}<i  style=\"position:absolute; right: -20px;top: -3px;color:#999;border:none;\">{1}人阅</i></span>", article_addtime, read_cnt);
                    //listsbr.AppendFormat("<i  style=\"position:absolute; right: -20px;top: 13px;\">{0}人阅</i>", read_cnt);
                    listsbr.AppendFormat("</dd>");
                    listsbr.AppendFormat("</dl>");


                    #endregion
                }
            }
            else
            {
                //listsbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            context.Put("list_more", listsbr.ToString());
        }


        /// <summary>
        /// 获取上衣、裤装、裙装、女鞋、包包、配饰、美妆等栏目下的最新的16条记录
        /// </summary>
        /// <param name="context"></param>
        private void GetList_fuzhuang(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            StringBuilder listsbr = new StringBuilder();

            int pagesize = 8;

            string where = " types between 49 and 55 and isend=0 and pub_state=0 ";

            where = " types=370 and isend=0 and pub_state=0 ";

            where += " and id<>" + articleid;

            string sql = "select top " + pagesize + " id,title,types from hrenh_article where " + where + "order by edittime desc"; //"order by istop desc,edittime desc";

            ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 17);

                    int fuzhuang_type = Convert.ToInt32(dr["types"].ToString());

                    //string fuzhuang_type_parname = "";

                    //switch (fuzhuang_type)
                    //{
                    //    case 49:
                    //        fuzhuang_type_parname = "shangyisy";
                    //        break;
                    //    case 50:
                    //        fuzhuang_type_parname = "kuzhuangkz";
                    //        break;
                    //    case 51:
                    //        fuzhuang_type_parname = "qunzhuangqz";
                    //        break;
                    //    case 52:
                    //        fuzhuang_type_parname = "nvxienx";
                    //        break;
                    //    case 53:
                    //        fuzhuang_type_parname = "baobaobb";
                    //        break;
                    //    case 54:
                    //        fuzhuang_type_parname = "peiships";
                    //        break;
                    //    case 55:
                    //        fuzhuang_type_parname = "meizhuangmz";
                    //        break;
                    //}

                    //string article_url = "href=\"/"+fuzhuang_type_parname+"/show_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";


                    string article_url = "href=\"/dapei/show_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    #endregion

                    #region  加载文章列表

                    if (i % 2 == 0)
                    {
                        sbr.AppendFormat("<li><a {0}>{1}</a></li>",article_url,show_article_title);
                    }
                    else
                    {
                        listsbr.AppendFormat("<li><a {0}>{1}</a></li>", article_url, show_article_title);
                    }
                    #endregion
                }
            }
            else
            {
                //listsbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            context.Put("list_fuzhuang_left", sbr.ToString());
            context.Put("list_fuzhuang_right", listsbr.ToString());
        }


        /// <summary>
        /// 今日热点
        /// </summary>
        /// <param name="context"></param>
        private string Get_Jrrd(NVelocity.VelocityContext context)
        {
            StringBuilder sbr = new StringBuilder();
            DataSet ds = null;
            int pid = 2100;

            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(24, " pageid=" + pid + " and cid=1012 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int total = ds.Tables[0].Rows.Count;

                int cols = 6;

                int rows = 0;

                if (total % cols == 0)
                {
                    rows = total / cols;
                }
                else
                {
                    rows = total / cols + 1;
                }

                for (int i = 0; i < rows; i++)
                {
                    sbr.AppendFormat("<div class=\"today_div\">");

                    for (int j = i * cols; j < (i + 1) * cols; j++)
                    {
                        if (j < total)
                        {
                            DataRow dr = ds.Tables[0].Rows[j];

                            string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                            string show_title = StringHelper.ReturnNumStr(title, 1, 17);
                            string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                            //链接打开方式，1：在本页面打，2：在新页面打开
                            int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                            sbr.AppendFormat("<div class=\"fcut\">");

                            sbr.AppendFormat("<div class=\"mask\">");

                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                            sbr.AppendFormat("{0}</a>", show_title);

                            sbr.AppendFormat("</div>");

                            sbr.AppendFormat("</div>");
                        }
                        else
                        {
                            break;
                        }

                    }

                    sbr.AppendFormat("</div>");
                }
            }

            return sbr.ToString();
        }
    }
}