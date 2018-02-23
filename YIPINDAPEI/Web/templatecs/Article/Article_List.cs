using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using NewXzc.Web.Common;
using NewXzc.Common;
using NewXzc.Web.Common.uhelper;
using NewXzc.DBUtility;

namespace NewXzc.Web.templatecs.Article
{
    public class Article_List : Base.BasePage
    {
        StringBuilder sbr = new StringBuilder();
        DataSet ds = null;
        BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();
        int type = 0;
        int stype = 0;

        string curnavnames = "";
        string title_val = "";
        string title_val_common = "";
        string keywords_val = "";
        string description = "";

        NewXzc.Web.templatecs.Head_Article head = new Head_Article();

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            //NewXzc.Web.templatecs.Head head = new Head();
            //head.Init_Head(context, 0);


            type = String_Manage.Return_Request_Int("type", -1);

            curnavnames = ArticleHelper.Get_Type_Name(type);


            int curtype = 0;
            description = "红人资讯主要介绍红人参与的活动、演出跟踪报道、娱乐八卦消息.";

            switch (type)
            {
                case 1:
                    title_val = "网红资讯-网红信息";
                    keywords_val = "网红资讯-网红资料";
                    description = "衣品搭配，专门分享报道网红最新资讯，有你想了解的一切网络红人相关资料";
                    curtype = 2;
                    break;
                case 2:
                    title_val = "红人写真";
                    keywords_val = "红人写真-红人图片_女装搭配|衣品搭配";
                    description = "红人写真主要是红人参与的一些影视照片、写真的综合集";
                    curtype = 3;
                    break;
                case 3:
                    title_val = "红人作品-演出作品";
                    keywords_val = "红人作品_女装搭配|衣品搭配";
                    description = "衣品搭配专注报道红人、红人作品、拍摄、演出作品汇集";
                    curtype = 4;
                    break;
                case 4:
                    title_val = "红人专访-网红专访";
                    keywords_val = "红人专访-网红专访";
                    description = "专访一些网络红人成长、求学、工作、成名经历,让粉丝进一步了解红人,更好亲近红人";
                    curtype = 5;
                    break;
                case 5:
                    description = "红人针对最新事件观点阐述,通过自己观点让粉丝进一步了解红人.";
                    curtype = 6;
                    break;
                case 35:
                    description = "红人针对最新事件观点阐述,通过自己观点让粉丝进一步了解红人.";
                    curtype = 8;
                    break;
                case 40:
                    title_val = "星网咖-网红-网络红人";
                    keywords_val = "星网咖-网红";
                    description = "衣品搭配针对红人最新事件观点阐述,通过红人的观点让粉丝进一步了解红人，帮红人拍摄红人自己的视频作品。";
                    curtype = 40;
                    break;
                case 41:
                    title_val = "衣品搭配网红营销的领导者_网红营销";
                    keywords_val = "网红营销，网红营销平台";
                    description = "衣品搭配这里是全套一站式的网红营销平台，轻松帮您达到意想不到的营销效果";
                    curtype = 0;
                    break;
                case 42:
                    title_val = "热点-热点话题-实时热点";
                    keywords_val = "热点-热点话题-实时热点";
                    description = "衣品搭配，热点栏目专门为您提供实时热点、红人经济热点和热点话题的深入报道和评论等内容。";
                    curtype = 42;
                    break;
                case 44:
                    title_val = "怎么成长为一名网红以及网红成长经历";
                    keywords_val = "网红成长，网红资料";
                    description = "介绍各个网红的成长经历以及他们的是怎么成为网红的的资讯。";
                    curtype = 44;
                    break;
                case 45:
                    title_val = "网红美女主播教你如何快速吸粉";
                    keywords_val = "网红主播，美女主播";
                    description = "衣品搭配汇聚各个网红主播大咖，教你如何成为网红如何快速吸粉。";
                    curtype = 45;
                    break;
                case 46:
                    title_val = "微博红人-微博营销-微博推广";
                    keywords_val = "微博营销,微博红人,微博达人,微博推广,自媒体营销";
                    description = "衣品搭配最大的网络红人服务平台!提供微博红人和各个大v资料、微博营销、自媒体营销。";
                    curtype = 46;
                    break;
                case 47:
                    title_val = "微信营销-微信推广";
                    keywords_val = "微信营销,微信推广,自媒体营销";
                    description = "衣品搭配最大的网络红人服务平台!提供微信营销、微信推广。";
                    curtype = 47;
                    break;
                case 354:
                    title_val = "穿衣搭配指南|女装搭配指南|服装搭配指南-衣品搭配网|衣服搭配指南";
                    keywords_val = "搭配,服装搭配,搭配指南,搭配技巧,女装搭配,风格搭配";
                    description = "服装搭配是每个女孩子的终身课题，搭配出适合自己的服饰无疑将展现自己各方面搭配技巧。衣品搭配网会为您展现最全面最专业的搭配指南，让每个爱美的女孩都能搭配出属于自己的风格。";
                    curtype = 354;
                    break;
            }

            head.Init_Head(context, curtype);


            //title_val = title_val + title_val_common;

            context.Put("title", title_val);
            context.Put("keywords_cname", curnavnames.Replace("红人", ""));

            #region  获取keywords和description
            try
            {
                DataSet kdds = DbHelperSQL.Query("select keywords,description from hrenh_article_type where id="+type+"");
                if (kdds != null && kdds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow kddr in kdds.Tables[0].Rows)
                    {
                        keywords_val = kddr["keywords"].ToString();
                        string description_val = kddr["description"].ToString();

                        if (keywords_val != "")
                        {
                            title_val = keywords_val;
                        }

                        if (description_val != "")
                        {
                            description = description_val;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            #endregion

            context.Put("keywords", title_val.Replace("_", "，").Replace("|", "，"));
            context.Put("description", description);


            context.Put("type",type);

            if (type < 1)
            {
                context.Put("redirecturl","/404");
            }

            #endregion

            ////获取顶部轮播图
            //context.Put("lunbo",GetLB());

            #region  右侧推荐位
            ////获取最新资讯，右侧
            //context.Put("newzx", ArticleHelper.Get_New_ZX(9,"",18));

            ////广告图
            //context.Put("ad",ArticleHelper.Get_Article_List_Ad());

            ////热门图片
            //context.Put("hot_img", ArticleHelper.Get_New_Img());

            ////热门文章
            //context.Put("hot_article", ArticleHelper.Get_New_Article(0));

            #endregion


            #region  加载子级栏目
            Get_Son_Type(context);
            #endregion


            #region  推荐位

            #region  推荐公共项

            //NewXzc.Web.templatecs.Article.Article_Search_Common article_common = new Article_Search_Common();

            ////头部广告
            //article_common.Init_Common_Ad(context);

            #endregion

            #endregion

            #region  热点

            //context.Put("hotarticle",ArticleHelper.Get_Hot_Articel());

            #endregion

            #region  今日热点

            context.Put("jrrd", Get_Jrrd(context));

            #endregion

            #region  每日精选

            context.Put("mrjx_ad", ArticleHelper.Get_Tj_Img(0, 2100, 1010));

            #endregion

            #region  获取面包屑导航

            string mbxnav=ArticleHelper.Get_Article_Nav(type,stype);
            string[] mbx=mbxnav.Split('|');

            context.Put("mbxnav", mbx[0]);
            context.Put("mbxnav_name", mbx[1]);

            #endregion

            int pageid = 2100;

            #region  列表页最新排行上方轮播图

            //context.Put("lunbo1", ArticleHelper.Get_Tj_Img(0, pageid, 1002));

            #endregion

            #region  获取最新排行的列表

            //context.Put("zxph", ArticleHelper.Get_Zxph(10));

            #endregion

            #region  列表页最新排行下方轮播图

            //context.Put("lunbo2", ArticleHelper.Get_Tj_Img(0, pageid, 1003));

            #endregion

            #region  大家都在搜

            context.Put("djdzs", ArticleHelper.Get_Tj_Img(0, pageid, 1004));

            #endregion


            //获取议会活动列表
            GetList(context);

            ////加载资讯列表友情链接
            //Get_FriendLink(context);
        }


        /// <summary>
        /// 加载子级栏目
        /// </summary>
        /// <param name="context"></param>
        private void Get_Son_Type(NVelocity.VelocityContext context)
        {
            sbr.Clear();
            BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

            int tpid = type;

            ds = type_bll.GetList(" pid=" + tpid + " and state=1 ");

            context.Put("tpid", tpid);

            string type_url_name = "";

            stype = String_Manage.Return_Request_Int("stype",0);

            if (tpid == 35 && stype>0)
            {
                head.Init_Head(context, stype);
            }

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int sid = 0;
                string sname = "";
                string surl = "";

                if (stype > 0)
                {
                    sbr.AppendFormat("<li><a href=\"/zhinan/\" title=\"全部\">全部</a></li>");
                }
                else
                {
                    sbr.AppendFormat("<li class=\"cur\"><a href=\"/zhinan/\" title=\"全部\">全部</a></li>");
                }

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

                    string keywords = "";

                    if ((sid >= 359 && sid <= 362) || sid==369)
                    {
                        surl = "/zhinan/";
                        switch (sid)
                        {
                            case 359:
                                surl += "clys";
                                title_val = "时尚女装|时尚潮流|时尚资讯|时尚服装|时尚搭配-衣品搭配网|穿衣搭配网";
                                keywords = "女装,时尚女装,潮流趋势,时尚潮流,时尚潮流女装";
                                description = "时尚永远是潮流人群竞相追逐的东西。在服装界每年都会有新的时尚潮流以及潮流指标，衣品搭配网会带给您最前沿的时尚服饰。";
                                break;
                            case 360:
                                surl += "fzdp";
                                title_val = "服装搭配技巧|穿衣搭配技巧|女装搭配技巧|服饰搭配技巧-衣品搭配网|穿衣搭配网";
                                keywords = "服装搭配,穿衣搭配,穿衣搭配技巧,韩国服装搭配,时尚服装搭配,冬季服装搭配";
                                description = "搭配技巧主要指在款式，颜色上相协调，整体上达到得体，大方的效果。衣品搭配网会针对不同人群推荐相应的服装搭配技巧，让每位用户都能找到适合自己的穿衣风格。";
                                break;
                            case 361:
                                surl += "hzjq";
                                break;
                            case 362:
                                surl += "hdth";
                                break;
                            case 369:
                                surl += "psdp";
                                break;
                        }
                        surl += "/";
                    }

                    

                    
                    if (sid == stype)
                    {

                        #region  获取keywords和description
                        //    try
                    //    {
                    //        DataSet kdds = DbHelperSQL.Query("select keywords,description from hrenh_article_type where id=" + sid + "");
                    //        if (kdds != null && kdds.Tables[0].Rows.Count > 0)
                    //        {
                    //            foreach (DataRow kddr in kdds.Tables[0].Rows)
                    //            {
                    //                keywords_val = kddr["keywords"].ToString();
                    //                string description_val = kddr["description"].ToString();

                    //                if (keywords_val != "")
                    //                {
                    //                    keywords = keywords_val;
                    //                }

                    //                if (description_val != "")
                    //                {
                    //                    description = description_val;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }
                        #endregion

                        //title_val = sname + "_" + curnavnames + title_val_common;

                        //context.Put("title", title_val);
                        //context.Put("keywords", title_val.Replace("_", "，").Replace("|", "，"));
                        context.Put("title", title_val);
                        context.Put("keywords", keywords);
                        context.Put("description", description);

                        sbr.AppendFormat("<li class=\"cur\">");

                        sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", surl, sname);
                    }
                    else
                    {
                        sbr.AppendFormat("<li>");
                        sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", surl, sname);
                    }
                    sbr.AppendFormat("</li>");
                }
            }
            else
            {

                type_url_name = ArticleHelper.Get_Nav_Type_Name(tpid);

                sbr.AppendFormat("<li>");
                sbr.AppendFormat("<a href=\"/{0}\" title=\"{1}\">{1}</a>", type_url_name, ArticleHelper.Get_Nav_Type_Name(tpid, tpid));
                sbr.AppendFormat("</li>");
            }


            context.Put("stypelist", sbr.ToString());
        }


        /// <summary>
        /// 获取列表页中头部的轮播图
        /// </summary>
        private string GetLB()
        {
            sbr.Clear();

            int cid = 0;
            switch (type)
            {
                case 1:
                    cid = 1001;
                    break;
                case 2:
                    cid = 1002;
                    break;
                case 3:
                    cid = 1003;
                    break;
                case 4:
                    cid = 1004;
                    break;
                case 5:
                    cid = 1005;
                    break;
                case 35:
                    cid = 1012;
                    break;
                case 40:
                    cid = 1012;
                    break;
                case 41:
                    cid = 1012;
                    break;
                case 42:
                    cid = 1012;
                    break;
            }

            ds = recommend_bll.GetList_NewIndex(6, " pageid=1100 and cid=" + cid + " ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 18);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"300\" width=\"640\"";

                    sbr.AppendFormat("<div>");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("<span class=\"slides-descride\">");
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{1}\">{0}</a>",show_title,title);
                        sbr.AppendFormat("</span>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" {3}></a>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle), width_height);
                        sbr.AppendFormat("<span class=\"slides-descride\">");
                        sbr.AppendFormat("<a {2} title=\"{1}\">{0}</a>", show_title, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("</span>");
                    }
                    sbr.AppendFormat("</div>");
                }
            }

            return sbr.ToString();
        }

        /// <summary>
        /// 获取议会活动列表
        /// </summary>
        /// <param name="context"></param>
        private void GetList(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            PageClass pc = new PageClass();

            int record_cnt = 0;
            int page_cnt = 0;
            int curpage = 1;
            int pagesize = 10;

            curpage = String_Manage.Return_Request_Int("page", 1);

            int page = 0;

            if (curpage > 0)
            {
                page = curpage - 1;
            }

            string where = " and (types=" + type + " or types_pid=" + type + ") and isend=0 and a.pub_state=0 ";

            if (stype > 0)
            {
                where = " and types="+stype+" and isend=0 ";
            }

            context.Put("stype",stype);

            ds = pc.TagList_New("*", " istop desc,edittime desc ", " hrenh_article a ", where, page, pagesize, " id,title,isimg,img_url,contents,(isnull(read_cnt,0)+FALSH_READ_CNT) as read_cnt,isnull((select count(*) from hrenh_article_REVIEW_REPLY where article_id=a.id and reply_id=0),0) as review_cnt,edittime ");

            if (ds != null && ds.Tables[2].Rows.Count > 0)
            {
                record_cnt = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                page_cnt = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[2].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 0, 36);
                    int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                    string article_img = "/template/img/nocontent.png";

                    if (isimg == 1)
                    {
                        article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                    }

                    string article_contents = StringHelper.ReturnNumStr(dr["contents"].ToString(), 1, 76);
                    string article_contents_noimg = StringHelper.ReturnNumStr(dr["contents"].ToString(), 1, 170);
                    string article_addtime = TimeParser.ReturnCurTime(dr["edittime"].ToString(), 1);
                    int read_cnt = String_Manage.Return_Int(dr["read_cnt"].ToString(), 0);
                    int review_cnt = String_Manage.Return_Int(dr["review_cnt"].ToString(), 0);

                    string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + article_title + "\"";

                    string parent_type_url = ArticleHelper.Get_Nav_Type_Name(type);

                    string surl = parent_type_url + "/detail";

                    //if (stype >= 359 && stype <= 362)
                    //{
                    //    surl = "zhinan/";
                    //    switch (stype)
                    //    {
                    //        case 359:
                    //            surl += "clys";
                    //            break;
                    //        case 360:
                    //            surl += "fzdp";
                    //            break;
                    //        case 361:
                    //            surl += "hzjq";
                    //            break;
                    //        case 362:
                    //            surl += "hdth";
                    //            break;
                    //        case 369:
                    //            surl += "psdp";
                    //            break;
                    //    }
                    //    surl += "/show";
                    //}
                    surl +=  "_"+ article_id + ".html";

                    article_url = "href=\"/"+surl+"\" target=\"_blank\" title=\"" + article_title + "\"";

                    #endregion

                    #region  加载文章列表


                    sbr.AppendFormat("<li>");
                    sbr.AppendFormat("<dl>");
                    //if (isimg == 1)
                    //{
                        sbr.AppendFormat("<dt><a {0}><img src=\"{1}\" alt=\"{2}\"></a></dt>",article_url,article_img,article_title);
                    //}
                    sbr.AppendFormat("<dd style=\"position:absolute;left:0;top:20px;left:336px;max-height:69px;\">");

                    string static_class = "";

                    //if (isimg != 1)
                    //{
                    //    //static_class = "style=\"position:static;\"";
                    //    article_contents = article_contents_noimg;
                    //}

                    sbr.AppendFormat("<h4 {2} style=\"position:static;\"><a {0}>{1}</a></h4>", article_url, show_article_title,static_class);
                    sbr.AppendFormat("<p class=\"list-content\" {2}  style=\"position:static;\"><a {0}> {1}</a></p>", article_url, article_contents, static_class);

                    sbr.AppendFormat("<p style=\"position:relative;background:url('/template/img/clock.png') no-repeat left center;\">");
                    //sbr.AppendFormat("<span  style=\"margin-left:0px;\">网络红人</span>");
                    //sbr.AppendFormat("<span class=\"mar12\">国家运动员</span>");
                    //sbr.AppendFormat("<span>网络红人</span>");
                    sbr.AppendFormat("<span style=\"border:0;margin-left:0px;color:#999;padding-left:20px;\" >{0}</span>", article_addtime);
                    sbr.AppendFormat("<i  style=\"position:absolute; right: -20px;top: 13px;\">{0}人阅</i>", read_cnt);
                    sbr.AppendFormat("</p>");
                    sbr.AppendFormat("</dd>");
                    sbr.AppendFormat("</dl>");
                    sbr.AppendFormat("</li>");



                    #endregion
                }
            }
            else
            {
                sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            if (curpage > page_cnt)
            {
                curpage = page_cnt;
            }

            context.Put("list", sbr.ToString());
            context.Put("listPage", NewXzc.Common.GenerPage.pageHtml(record_cnt, pagesize, curpage, "GoPage"));
        }


        /// <summary>
        /// 加载资讯列表友情链接
        /// </summary>
        /// <param name="context"></param>
        private void Get_FriendLink(NVelocity.VelocityContext context)
        {

            #region  加载资讯列表友情链接

            StringBuilder sbr_foot = new StringBuilder();
            DataSet ds_foot = null;
            BLL.RED_RECOMMEND recommend_bll_foot = new BLL.RED_RECOMMEND();

            ds_foot = recommend_bll_foot.GetList_NewIndex(0, " pageid=2000 and cid=1002 ");

            if (ds_foot != null && ds_foot.Tables[0].Rows.Count > 0)
            {
                sbr_foot.AppendFormat("<div class=\"friendLink\">");
                sbr_foot.AppendFormat("<div class=\"box\">");
                sbr_foot.AppendFormat("<div>");
                sbr_foot.AppendFormat("<h2>友情链接</h2>");

                for (int i = 0; i < ds_foot.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds_foot.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 3, 0);
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    sbr_foot.AppendFormat("<a {1} title=\"{0}\">{0}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                    if (i < ds_foot.Tables[0].Rows.Count - 1)
                    {
                        sbr_foot.AppendFormat("<i>|</i>");
                    }

                }

                sbr_foot.AppendFormat("</div>");
                sbr_foot.AppendFormat("</div>");
                sbr_foot.AppendFormat("</div>");

            }

            context.Put("foot_link", sbr_foot.ToString());
            #endregion
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