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

namespace NewXzc.Web.templatecs.Article.newarticle
{
    public class dianpu_detail : Base.BasePage
    {

        BLL.HRENH_ARTICLE_REVIEW_REPLY review_bll = new BLL.HRENH_ARTICLE_REVIEW_REPLY();
        BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

        BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();

        StringBuilder sbr = new StringBuilder();
        DataSet ds = null;
        int articleid = 0;
        int type = 0;
        string navurl = "";

        string type_shouzimu = "";

        string typename = "";

        NewXzc.Web.templatecs.Head_Article head = new Head_Article();

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title

            string title_val = "";
            string keywords_val = title_val;

            context.Put("title", title_val);
            context.Put("keywords", keywords_val);
            context.Put("keywords_cname", keywords_val.Replace("红人", ""));


            head.Init_Head(context, 0);

            #endregion


            articleid = String_Manage.Return_Request_Int("id", 0);


            Model.HRENH_ARTICLE article_model = new BLL.HRENH_ARTICLE().GetModel(articleid);

            if (article_model != null)
            {
                type =Convert.ToInt32(article_model.TYPES);

                typename = type_bll.GetModel(type).TypeName;


                context.Put("parent_nav", typename);

                //通过汉字获取该汉字的首字母
                type_shouzimu = Get_Hanzi_Shouzimu.GetSpellCode(typename);

                string typeurl = "/"+ArticleHelper.Get_Type_Rname(type) + type_shouzimu + "/";

                if (type == 370)
                {
                    typeurl = "/" + ArticleHelper.Get_Type_Rname(type) + "/";
                }

                context.Put("typepurl", typeurl);

                navurl = typeurl + "show_";

                int pageid = 2100;

                #region  详情页相关阅读上方广告图

                //context.Put("topad", ArticleHelper.Get_Tj_Img(1, pageid, 1008));

                #endregion

                #region  详情页相关阅读轮播图

                //context.Put("topadlb", ArticleHelper.Get_Tj_Img(0, pageid, 1009));

                #endregion


                #region  获取最新排行的列表

                //context.Put("zxph", ArticleHelper.Get_Zxph(10));

                #endregion

                #region  今日热点

                context.Put("jrrd",Get_Jrrd(context));

                #endregion


                #region  每日精选

                context.Put("mrjx_ad", ArticleHelper.Get_Tj_Img(0, pageid, 1010));

                #endregion

                #region  大家都在搜

                context.Put("djdzs", ArticleHelper.Get_Tj_Img(0, pageid, 1004));

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
                    rows = total / cols+1;
                }

                for (int i = 0; i < rows; i++)
                {
                    sbr.AppendFormat("<div class=\"today_div\">");

                    for (int j = i*cols; j < (i+1)*cols; j++)
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
                string sql = "select top 6 id,title,types,isimg,img_url from hrenh_article where isend=0 and types=" + type + " and pub_state=0 and id<>"+articleid+"";

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
                        string article_url = "href=\"" + navurl + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";
                        int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                        string article_img = "/template/img/nocontent.png";

                        if (isimg == 1)
                        {
                            article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                        }

                        sbr.AppendFormat("<li>");
                        sbr.AppendFormat("<a {0}>{1}", article_url, show_article_title);
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
                        string addtime = "";
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
                                    addtime = TimeParser.ReturnCurTime(adr["addtime"].ToString(), 4);
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
                        sbr.AppendFormat("<span>{0}</span>", addtime);
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
                    ds = DbHelperSQL.Query("select top 1 id,title,isimg,img_url,isnull((select contents from hrenh_article_html where article_id=a.id),'') as contents,edittime,types,isnull(keywords,'文章详情') as keywords,isnull(description,'文章详情') as description,isnull((select top 1 uname from hrenh_article_author where id=a.AuthorId),'') as author,isnull((select top 1 USER_HEAD from hrenh_article_author where id=a.AuthorId),'') as uhead,New_Zixun_Tbk_Url,New_Zixun_Price,New_Zixun_Intro_Short,New_Zixun_Idlist,isnull((select product_name from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platname,isnull((select Produce_Img from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platimg from hrenh_article a where id=" + articleid + " ");// and a.pub_state=0

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
                                context.Put("title", StringHelper.ReturnNumStr(article_title, 1, 0) + "_" + ArticleHelper.Get_Type_Name(type));
                            }
                            else
                            {
                                context.Put("title", StringHelper.ReturnNumStr(article_title, 0, 0) + "_" + typename);
                            }

                            //context.Put("navurl", "/" + ArticleHelper.Get_Nav_Type_Name(type));
                            //context.Put("navnames", ArticleHelper.Get_Type_Name(type));

                            string article_contents = dr["contents"].ToString();
                            string article_addtime = TimeParser.ReturnCurTime(dr["edittime"].ToString(), 0);
                            string New_Zixun_Tbk_Url = dr["New_Zixun_Tbk_Url"].ToString();
                            string New_Zixun_Price = dr["New_Zixun_Price"].ToString();
                            New_Zixun_Price = Recruit_Job.Return_Money(New_Zixun_Price, 1);

                            context.Put("keywords", dr["keywords"].ToString());
                            context.Put("description", dr["description"].ToString());

                            string author = String_Manage.Return_Str(dr["author"].ToString(), "");

                            string New_Zixun_Intro_Short = String_Manage.Return_Str(dr["New_Zixun_Intro_Short"].ToString(), "");

                            string New_Zixun_Idlist = String_Manage.Return_Str(dr["New_Zixun_Idlist"].ToString(), "");

                            if (New_Zixun_Idlist != "")
                            {
                                Get_type_New_Zixun__name(New_Zixun_Idlist, context);
                            }

                            string platname = String_Manage.Return_Str(dr["platname"].ToString(), "");
                            string platimg = String_Manage.Return_Str(dr["platimg"].ToString(), "");
                            if (platimg != "")
                            {
                                platimg = ImgHelper.Get_UploadImgUrl(platimg, 1);
                            }

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

                            #region  加载新说明

                            if (New_Zixun_Intro_Short != "")
                            {
                                sbr.AppendFormat("<div class=\"double_quotes\">");
                                sbr.AppendFormat("<div>{0}</div>", New_Zixun_Intro_Short);
                                sbr.AppendFormat("</div>");
                            }

                            #endregion


                            article_contents = article_contents.Replace("<img", "<img alt='衣品搭配'").Replace("undefined", "");

                            //将A标签下的href替换成javascript:;
                            article_contents = Get_A_Href.get_a_info(article_contents);

                            sbr.AppendFormat("{0}", article_contents);

                            sbr.AppendFormat("</div>");

                            //sbr.AppendFormat("<p class=\"gobuy\"><b>￥{0}</b></p>", New_Zixun_Price);

                            if (New_Zixun_Tbk_Url != "")
                            {
                                New_Zixun_Tbk_Url = "/rfzdp/" + articleid + ".html";

                                if (platname != "")
                                {
                                    //sbr.AppendFormat("<p class=\"gobuy\"><b>￥{0}</b><span><a href=\"{1}\" title='购买' target='_blank' rel=\"nofollow\">点击这里去“<strong>{3}</strong>”购买</a><i class=\"sppt\"><img src=\"{2}\" alt=\"{3}\"></i></span></p>", New_Zixun_Price, New_Zixun_Tbk_Url, platimg, platname);
                                    sbr.AppendFormat("<p class=\"gobuy\"><b>￥{0}</b><span><a href=\"{1}\" title='购买' target='_blank' rel=\"nofollow\">点击这里去购买</a><i class=\"sppt\"><img src=\"{2}\" alt=\"{3}\"></i></span></p>", New_Zixun_Price, New_Zixun_Tbk_Url, platimg, platname);
                                }
                                else
                                {
                                    sbr.AppendFormat("<p class=\"gobuy\"><b>￥{0}</b><a href=\"{1}\" title='购买' target='_blank' rel=\"nofollow\">点击这里去购买</a></p>", New_Zixun_Price, New_Zixun_Tbk_Url);
                                }
                            }

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

                ////获取当前文章所在类型的最新的几篇文章
                //GetList(context);

                //获取相关搭配推荐
                Get_Top10_Zixun(context);

                //获取去这里选更多
                Get_Top5_Zixun(context);

                //获取更多穿搭推荐
                Get_Top20_Zixun(context);
            }

        }


        /// <summary>
        /// 获取类型的名字
        /// </summary>
        /// <param name="idlist">类型或平台的ID</param>
        /// <returns></returns>
        private void Get_type_New_Zixun__name(string idlist,NVelocity.VelocityContext context)
        {
            if (idlist != "")
            {
                string sql = "select id,typename,TypeRename from HRENH_ARTICLE_TYPE where id in(" + idlist + ")";
                DataSet ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string dpid = "";

                    for (int d = 0; d < ds.Tables[0].Rows.Count;d++ )
                    {
                        string did=ds.Tables[0].Rows[d]["id"].ToString();
                        string dtypename = ds.Tables[0].Rows[d]["typename"].ToString();
                        string dTypeRename = ds.Tables[0].Rows[d]["TypeRename"].ToString();
                        if (d == 0)
                        {
                            //context.Put("son_type",dtypename);

                            dpid = did;
                        }
                        else
                        {
                            bool f = false;

                            try
                            {
                                DataSet find_son = DbHelperSQL.Query("select typename,TypeRename from HRENH_ARTICLE_TYPE where pid=" + dpid + " and id=" + did + "");

                                if (find_son != null && find_son.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow ddr in find_son.Tables[0].Rows)
                                    {
                                        dtypename = ddr["typename"].ToString();
                                        dTypeRename = ddr["TypeRename"].ToString();

                                        context.Put("third_type", dtypename);
                                        context.Put("third_type_url", "/" + dTypeRename + type_shouzimu + "/");
                                        f = true;
                                    }
                                }

                            }
                            catch (Exception ex)
                            {

                            }

                            if (f)
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 获取当前文章所在类型的最新的几篇文章
        /// </summary>
        /// <param name="context"></param>
        private void GetList(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            StringBuilder listsbr = new StringBuilder();

            int pagesize = 10;

            string where = " types=" + type + " and isend=0 and pub_state=0 ";

            where += " and id<>" + articleid;

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

                    string article_url = "href=\"" + navurl + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    int read_cnt = String_Manage.Return_Int(dr["read_cnt"].ToString(), 0);

                    string platname = String_Manage.Return_Str(dr["platname"].ToString(), "");
                    string platimg = String_Manage.Return_Str(dr["platimg"].ToString(), "");
                    if (platimg != "")
                    {
                        platimg = ImgHelper.Get_UploadImgUrl(platimg, 1);
                    }

                    #endregion

                    #region  加载文章列表

                    if (i < 3)
                    {
                        if (i == 0)
                        {
                            sbr.AppendFormat("<li class=\"mar0\">");
                        }
                        else
                        {
                            sbr.AppendFormat("<li>");
                        }
                        sbr.AppendFormat("<span>{0}</span>", article_addtime);
                        sbr.AppendFormat("<a {0}>{1}</a>", article_url, show_article_title);
                        sbr.AppendFormat("</li>");
                    }
                    else
                    {
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
                    }


                    #endregion
                }
            }
            else
            {
                sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            context.Put("list3", sbr.ToString());
            context.Put("list_more", listsbr.ToString());
        }


        /// <summary>
        /// 获取相关搭配推荐
        /// </summary>
        /// <param name="context"></param>
        private void Get_Top10_Zixun(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            //被要求显示的季节ID
            string season_require_id = "";


            try
            {
                season_require_id = DbHelperSQL.GetSingle("select top 1 season_id from HRENH_SET_ARTICLE_SHOW_SEASON order by id desc").ToString();
            }
            catch (Exception ex)
            {

            }

            StringBuilder listsbr = new StringBuilder();

            int pagesize = 10;

            string where = " types=" + type + " and isend=0 and pub_state=0 ";
            if (season_require_id != "")
            {

                where += " and New_Zixun_Season in(" + season_require_id + ")";
            }

            where += " and id<>" + articleid;

            string sql = "select top " + pagesize + " id,title,types,New_Zixun_intro_short,isimg,img_url,New_Zixun_Price,isnull((select product_name from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platname,isnull((select Produce_Img from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platimg from hrenh_article a where " + where + "order by istop desc,edittime desc";

            

            ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sbr.AppendFormat("<h4 class=\"lineread\">相关搭配推荐</h4>");
                sbr.AppendFormat("<div class=\"shop\" style=\"margin-left:-20px;\">");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 29);
                    int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                    string article_img = "/template/img/nocontent.png";

                    if (isimg == 1)
                    {
                        article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                    }


                    int atype = Convert.ToInt32(dr["types"].ToString());

                    if (atype != 370)
                    {
                        article_title = String_Manage.Return_Str(dr["New_Zixun_intro_short"].ToString(), "");
                    }

                    string article_url = "href=\"" + navurl + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    string New_Zixun_Price = dr["New_Zixun_Price"].ToString();
                    New_Zixun_Price = Recruit_Job.Return_Money(New_Zixun_Price, 1);

                    string platname = String_Manage.Return_Str(dr["platname"].ToString(), "");
                    string platimg = String_Manage.Return_Str(dr["platimg"].ToString(), "");
                    if (platimg != "")
                    {
                        platimg = ImgHelper.Get_UploadImgUrl(platimg, 1);
                    }

                    #endregion

                    #region  加载文章列表

                    sbr.AppendFormat("<dl class=\"shopone\">");
                    sbr.AppendFormat("<a {0}>",article_url);
                    sbr.AppendFormat("<dt>");
                    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">",article_img,article_title);
                    sbr.AppendFormat("</dt>");
                    sbr.AppendFormat("<dd>");

                    sbr.AppendFormat("<h4>{0}</h4>", show_article_title);

                    //if (platname != "")
                    //{
                    //    sbr.AppendFormat("<b title=\"{1}购买直通车\"><i class=\"sppt\"><img src=\"{2}\" alt=\"{1}\"></i><i>￥</i>{0}</b>", New_Zixun_Price, platname, platimg);
                    //}
                    //else
                    //{
                    //    sbr.AppendFormat("<b><i>￥</i>{0}</b>", New_Zixun_Price);
                    //}
                    
                    sbr.AppendFormat("</dd>");
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("</dl>");


                    #endregion
                }

                sbr.AppendFormat("</div>");

            }

            context.Put("xgdptj", sbr.ToString());
        }


        /// <summary>
        /// 获取去这里选更多
        /// </summary>
        /// <param name="context"></param>
        private void Get_Top5_Zixun(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            StringBuilder listsbr = new StringBuilder();

            int pagesize = 5;

            string sql = "select b.*,(select TypeRename from HRENH_ARTICLE_TYPE where ID=b.parents_id) as prname,(select TypeName from HRENH_ARTICLE_TYPE where ID=b.parents_id) as ptypename,(select min(id) from HRENH_ARTICLE_TYPE where pid=b.parents_id) as first_type_id from ";
            sql+="(";
            sql+="select top "+pagesize+" id,TypeImg,TypeName,TypeRename,PID,TypeOrder,Addtime,(select PID from HRENH_ARTICLE_TYPE where ID=a.PID) as parents_id from HRENH_ARTICLE_TYPE a where ISTOP=1 and Type_parent=1";
            sql+=") b order by TypeOrder asc,Addtime desc";



            ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sbr.AppendFormat("<h4 class=\"lineread\">去这里选更多</h4>");
                sbr.AppendFormat("<div class=\"smore\">");
                sbr.AppendFormat("<ul>");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息

                    string article_title = String_Manage.Return_Str(dr["TypeName"].ToString(), "");
                    string article_img = "/template/img/nocontent.png";

                    article_img = ImgHelper.Get_UploadImgUrl(dr["TypeImg"].ToString(), 1);

                    string prname = dr["prname"].ToString();
                    string son_rname = dr["TypeRename"].ToString();


                    string navurl = "";

                    if (dr["pid"].ToString() == dr["first_type_id"].ToString())
                    {
                        //通过汉字获取该汉字的首字母
                        string type_shouzimu = Get_Hanzi_Shouzimu.GetSpellCode(dr["ptypename"].ToString());
                        navurl = "/" + son_rname + type_shouzimu + "/";
                    }
                    else
                    {
                        navurl = "/" + prname + "/" + son_rname + ".html";
                    }

                    string article_url = "href=\"" + navurl + "\" target=\"_blank\" title=\"" + article_title + "\"";

                    #endregion

                    #region  加载文章列表

                    sbr.AppendFormat("<li>");
                    sbr.AppendFormat("<a {0}>",article_url);
                    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">",article_img,article_title);
                    sbr.AppendFormat("<i>{0}</i>",article_title);
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("</li>");

                    #endregion
                }

                sbr.AppendFormat("</ul>");
                sbr.AppendFormat("</div>");

            }

            context.Put("qzlxgd", sbr.ToString());
        }


        /// <summary>
        /// 获取更多穿搭推荐
        /// </summary>
        /// <param name="context"></param>
        private void Get_Top20_Zixun(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            StringBuilder listsbr = new StringBuilder();

            int pagesize = 20;

            string where = " isend=0 and pub_state=0 and ishot=1 ";

            string sql = "select top " + pagesize + " id,title,New_Zixun_intro_short,isimg,img_url,New_Zixun_Price,types,isnull((select product_name from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platname,isnull((select Produce_Img from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platimg from Hrenh_Article a where " + where + "order by istop desc,edittime desc";



            ds = DbHelperSQL.Query(sql);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sbr.AppendFormat("<h4 class=\"lineread\">更多穿搭推荐</h4>");
                sbr.AppendFormat("<div class=\"shop\" style=\"margin-left:-20px;\">");

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    #region  文章列表的信息
                    int article_id = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    string article_title = String_Manage.Return_Str(dr["title"].ToString(), "");
                    
                    string show_article_title = StringHelper.ReturnNumStr(article_title, 1, 29);
                    int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                    string article_img = "/template/img/nocontent.png";

                    if (isimg == 1)
                    {
                        article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                    }

                    int atype = Convert.ToInt32(dr["types"].ToString());

                    if (atype != 370)
                    {
                        article_title = String_Manage.Return_Str(dr["New_Zixun_intro_short"].ToString(), "");
                    }

                    //string atypename = type_bll.GetModel(type).TypeName;

                    string typeurl = "";

                    //if (type == 370)
                    //{
                    //    typeurl = "/" + ArticleHelper.Get_Type_Rname(type) + "/";
                    //}
                    //else
                    //{
                    //    //通过汉字获取该汉字的首字母
                    //    string type_shouzimu = Get_Hanzi_Shouzimu.GetSpellCode(typename);

                    //    typeurl = "/" + ArticleHelper.Get_Type_Rname(type) + type_shouzimu + "/";
                    //}

                    if (atype == 370)
                    {
                        typeurl = "/" + ArticleHelper.Get_Type_Rname(type) + "/";
                    }
                    else
                    {

                        typename = type_bll.GetModel(atype).TypeName;

                        //通过汉字获取该汉字的首字母
                        string type_shouzimu = Get_Hanzi_Shouzimu.GetSpellCode(typename);

                        typeurl = "/" + ArticleHelper.Get_Type_Rname(atype) + type_shouzimu + "/";
                    }


                    string navurl = typeurl + "show_";

                    string article_url = "href=\"" + navurl + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    string New_Zixun_Price = dr["New_Zixun_Price"].ToString();
                    New_Zixun_Price = Recruit_Job.Return_Money(New_Zixun_Price, 1);

                    string platname = String_Manage.Return_Str(dr["platname"].ToString(), "");
                    string platimg = String_Manage.Return_Str(dr["platimg"].ToString(), "");
                    if (platimg != "")
                    {
                        platimg = ImgHelper.Get_UploadImgUrl(platimg, 1);
                    }

                    #endregion

                    #region  加载文章列表

                    sbr.AppendFormat("<dl class=\"shopone\">");
                    sbr.AppendFormat("<a {0}>", article_url);
                    sbr.AppendFormat("<dt>");
                    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", article_img, article_title);
                    sbr.AppendFormat("</dt>");
                    sbr.AppendFormat("<dd>");

                    sbr.AppendFormat("<h4>{0}</h4>", show_article_title);

                    //if (platname != "")
                    //{
                    //    sbr.AppendFormat("<b title=\"{1}购买直通车\"><i class=\"sppt\"><img src=\"{2}\" alt=\"{1}\"></i><i>￥</i>{0}</b>", New_Zixun_Price, platname, platimg);
                    //}
                    //else
                    //{
                    //    sbr.AppendFormat("<b><i>￥</i>{0}</b>", New_Zixun_Price);
                    //}

                    sbr.AppendFormat("</dd>");
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("</dl>");


                    #endregion
                }

                sbr.AppendFormat("</div>");

            }

            context.Put("gdcdtj", sbr.ToString());
        }

    }
}