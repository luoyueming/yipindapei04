using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using System.Data;
using NewXzc.DBUtility;
using System.Text;
using NewXzc.Common;
using NewXzc.Web.Common.uhelper;

namespace NewXzc.Web.templatecs.Article.newarticle
{
    public class dapei : Base.BasePage
    {

        NewXzc.Web.templatecs.Head_Article head = new Head_Article();

        BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

        int type_parent = 1;
        int type = 0;
        int stype = 0;
        string typename = "上衣";
        string typeurl = "";

        string description = "";

        string prname = "";
        string srname = "";

        string type_shouzimu = "";

        StringBuilder sbr = new StringBuilder();
        DataSet ds = null;

        //被要求显示的季节ID，1：秋天,2：夏天,3：冬天,4：四季,5：春天
        string season_require_id = "";

        string season_require_id_linshi = "";
        //四季ID
        string season_siji_id = "4";

        string season_siji_id_linshi = ",4,";

        //根据季节查询二级和三级
        string son_where = "";


        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title

            string title_val = "";
            string keywords_val = title_val;


            #endregion


            #region  热点

            //context.Put("hotarticle", ArticleHelper.Get_Hot_Articel());

            #endregion

            try
            {
                season_require_id = DbHelperSQL.GetSingle("select top 1 season_id from HRENH_SET_ARTICLE_SHOW_SEASON order by id desc").ToString();
            }
            catch (Exception ex)
            {

            }

            season_require_id_linshi = "," + season_require_id + ",";


            //当type_mark不为空时，用于作为当前子级的重写地址
            string linshi_srname = "";

            type = String_Manage.Return_Request_Int("type", 0);

            //父级
            prname = String_Manage.Return_Request_Str("prname");

            linshi_srname = prname;

            //子级
            srname = String_Manage.Return_Request_Str("srname");

            if (type == 370)
            {
                prname = "dapei";
            }

            if (prname == "")
            {
                context.Put("redirecturl", "/404");
            }
            else
            {

                if (type < 1)
                {
                    type = ArticleHelper.Get_Dianpu_Type_Id(prname, 0);
                }

                //通过汉字获取该汉字的首字母
                if (type < 370)
                {
                    type_shouzimu = Get_Hanzi_Shouzimu.GetSpellCode(type_bll.GetModel(type).TypeName);
                }

                prname = ArticleHelper.Get_Type_Rname(type);

                if (type == 370)
                {
                    linshi_srname = prname;
                }

                if (srname != "")
                {
                    //typeurl = "/"+prname+"/"+srname+"/";
                    typeurl = "/" + prname + "/" + srname + ".html";
                    stype = ArticleHelper.Get_Dianpu_Type_Id(srname, 1);
                }
                else
                {
                    if (linshi_srname != "")
                    {
                        if (prname != linshi_srname)
                        {
                            stype = ArticleHelper.Get_Dianpu_Type_Id(linshi_srname, 1);
                        }
                    }
                    //typeurl = "/" + prname + type_shouzimu + "/";
                    typeurl = "/" + linshi_srname + type_shouzimu + "/";
                }
            }

            context.Put("typepurl", typeurl);


            head.Init_Head(context, type);

            #region  加载二级和三级

            if (stype > 0)
            {
                typename = ArticleHelper.Get_Type_Name(stype, type_parent);
                typename = typename.Replace(",", "_");
            }
            else
            {
                typename = ArticleHelper.Get_Type_Name(type);
            }

            title_val = typename + "";

            if (type == 370)
            {
                title_val = "穿衣搭配|女装|服装搭配|衣服搭配-衣品搭配网";
            }

            context.Put("title", title_val);

            #region  获取keywords和description
            try
            {
                int type_linshi = type;

                if (stype > 0)
                {
                    type_linshi = stype;
                }

                DataSet kdds = DbHelperSQL.Query("select keywords,description from hrenh_article_type where id=" + type_linshi + "");
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

            if (type == 370)
            {
                context.Put("keywords", "女装,搭配,女装搭配,穿衣搭配,服装搭配,搭配技巧,韩国服饰,穿衣搭配网");
                context.Put("keywords_cname", "搭配");

                context.Put("description", "搭配涵盖多种类型的服装搭配，例如韩范、日系以及欧美等风格，用大量精致的服装搭配图片展现搭配的魅力。衣品搭配网为您推荐最全面的搭配信息。");
            }
            else
            {
                context.Put("keywords", title_val.Replace("_", "，").Replace("|", "，"));
                context.Put("keywords_cname", keywords_val.Replace("红人", ""));

                context.Put("description", description);
            }

            if (type < 370)
            {
                Get_Second_Third(context);
            }

            #endregion

            // 加载文章列表
            GetList(context);

        }


        /// <summary>
        /// 获取二级和三级
        /// </summary>
        private void Get_Second_Third(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            int pageid = 2200;
            int cid = 1001;

            string navurl_list = "";

            switch (type)
            {
                case 49:
                    cid = 1001;
                    break;
                case 50:
                    cid = 1002;
                    break;
                case 51:
                    cid = 1003;
                    break;
                case 52:
                    cid = 1004;
                    break;
                case 53:
                    cid = 1005;
                    break;
                case 54:
                    cid = 1006;
                    break;
                case 55:
                    cid = 1007;
                    break;
                case 370:
                    cid = 1008;
                    break;
            }

            try
            {
                DataSet adds = DbHelperSQL.Query("select v3,v4,url from red_recommend where pageid=" + pageid + " and cid=" + cid + " order by sort asc,id desc");

                if (adds != null && adds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder adsbr = new StringBuilder();

                    foreach (DataRow addr in adds.Tables[0].Rows)
                    {
                        string imgurl = ImgHelper.GetCofigShowUrl() + addr["v3"].ToString();
                        string httpurl = String_Manage.Return_Str(addr["url"].ToString(), "");
                        //链接打开方式，1：在本页面打，2：在新页面打开
                        int openstyle = String_Manage.Return_Int(addr["v4"].ToString(), 0);

                        adsbr.AppendFormat("<a class=\"right_img\" {0} title=\"女装搭配\">", Return_HttpURL.Return_Url(httpurl, openstyle));
                        adsbr.AppendFormat("<img src=\"{0}\" alt=\"女装搭配\">", imgurl);
                        adsbr.AppendFormat("</a>");
                    }

                    context.Put("adimgs", adsbr.ToString());
                }

            }
            catch (Exception ex)
            {

            }

            //不是搭配栏目
            if (type < 370)
            {
                //当前大类型下的第一类型的ID
                int first_type_id = 0;

                try
                {
                    first_type_id = Convert.ToInt32(DbHelperSQL.GetSingle("select top 1 id from HRENH_ARTICLE_TYPE where pid=" + type + " order by id asc").ToString());
                }
                catch (Exception ex)
                {

                }

                try
                {
                    string son_sql = "select a.id,a.typename,a.lvl,(select top 1 TypeImg from HRENH_ARTICLE_TYPE where id=a.id) as typeimg from return_article_all_son_typelist(" + type + "," + type_parent + ") a where a.lvl=1";


                    ds = DbHelperSQL.Query(son_sql);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int f = 0; f < ds.Tables[0].Rows.Count; f++)
                        {
                            DataRow dr = ds.Tables[0].Rows[f];

                            string id = dr["id"].ToString();
                            string typename = dr["typename"].ToString();
                            string lvl = dr["lvl"].ToString();
                            string typeimg = dr["typeimg"].ToString();

                            if (typeimg != "")
                            {
                                typeimg = ImgHelper.Get_UploadImgUrl(typeimg, 1);
                            }

                            string article_url = " href='javascript:void(0)' title='" + typename + "' ";

                            //sbr.AppendFormat("<div class=\"resumeWrap\">");

                            //sbr.AppendFormat("<div class=\"catalogInfo\">");
                            //sbr.AppendFormat("<a {0}>", article_url);
                            //sbr.AppendFormat("<p class=\"title\">{0}</p>", typename);
                            //sbr.AppendFormat("</a>");
                            //sbr.AppendFormat("<a class=\"imgWrap\" {0}>", article_url);
                            //sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", typeimg, typename);
                            //sbr.AppendFormat("</a>");
                            //sbr.AppendFormat("</div>");

                            sbr.AppendFormat("<dl class=\"type_section first\">");
                            sbr.AppendFormat("<dt><a {0}>{1}</a></dt>", article_url, typename);
                            sbr.AppendFormat("<dd>");
                            sbr.AppendFormat("<a class=\"cat_img\" {0}>", article_url);
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", typeimg, typename);
                            sbr.AppendFormat("</a>");
                            sbr.AppendFormat("<ul>");

                            #region  加载三级

                            string third_sql = "select a.id,a.typename,(select TypeRename from hrenh_article_type where id=a.id) as rname from return_article_all_son_typelist(" + id + "," + type_parent + ") a where a.lvl=1";


                            DataSet sonds = DbHelperSQL.Query(third_sql);


                            if (sonds != null && sonds.Tables[0].Rows.Count > 0)
                            {
                                int total_cnt = sonds.Tables[0].Rows.Count;

                                int column = 4;
                                int rows = 0;

                                if (total_cnt % column == 0)
                                {
                                    rows = total_cnt / column;
                                }
                                else
                                {
                                    rows = total_cnt / column + 1;
                                }

                                for (int i = 0; i < rows; i++)
                                {
                                    //sbr.AppendFormat("<div class=\"catalogChoice\">");
                                    //sbr.AppendFormat("<ul>");

                                    for (int j = i * column; j < (i + 1) * column; j++)
                                    {
                                        if (j < total_cnt)
                                        {
                                            DataRow sdr = sonds.Tables[0].Rows[j];
                                            id = sdr["id"].ToString();
                                            typename = sdr["typename"].ToString();
                                            string rname = sdr["rname"].ToString();

                                            //是否是被热推的类型
                                            string cur_type_hot = "";

                                            if (f == 0)
                                            {
                                                //cur_type_hot = "hot";
                                                typeurl = "/" + rname + type_shouzimu + "/";
                                            }
                                            else
                                            {
                                                //typeurl = "/" + prname + "/" + rname + "/";
                                                typeurl = "/" + prname + "/" + rname + ".html";
                                            }

                                            article_url = " href='" + typeurl + "' title='" + typename + "' ";

                                            navurl_list += "http://www.ypindapei.com" + typeurl + "|" + typename;

                                            //sbr.AppendFormat("<li><a {0}>{1}</a></li>", article_url, typename);
                                            sbr.AppendFormat("<li>");
                                            sbr.AppendFormat("<a {0} class=\"pagani_log_link {2}\" >{1}</a>", article_url, typename, cur_type_hot);
                                            sbr.AppendFormat("</li>");

                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                    //sbr.AppendFormat("</ul>");
                                    //sbr.AppendFormat("</div>");
                                }


                            }

                            #endregion

                            //sbr.AppendFormat("</div>");

                            sbr.AppendFormat("</ul>");
                            sbr.AppendFormat("</dd>");
                            sbr.AppendFormat("</dl>");

                        }
                    }


                    context.Put("Second_Third", sbr.ToString());

                    context.Put("redirecturl_error", navurl_list);
                }
                catch (Exception ex)
                {
                    //context.Put("redirecturl","/404");
                    context.Put("redirecturl_error", ex.ToString());
                }
            }

        }


        /// <summary>
        /// 加载文章列表
        /// </summary>
        /// <param name="context"></param>
        private void GetList(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            try
            {

                PageClass pc = new PageClass();

                int record_cnt = 0;
                int page_cnt = 0;
                int curpage = 1;
                int pagesize = 49;

                curpage = String_Manage.Return_Request_Int("page", 1);

                int page = 0;

                if (curpage > 0)
                {
                    page = curpage - 1;
                }

                string where = " and types=" + type + " and isend=0 and a.pub_state=0 and type_parent=" + type_parent + " and New_Zixun_Is_Tbk=0 ";


                if (stype > 0)
                {
                    where = " and charindex('," + stype + ",',','+New_Zixun_Idlist+',')>0 ";
                }


                #region  根据季节显示文章列表
                string season_where = "";

                if (season_require_id_linshi != "" && season_require_id_linshi != ",,")
                {
                    season_where = " and (";
                    string[] sarr = season_require_id_linshi.Split(',');
                    for (int s = 0; s < sarr.Length; s++)
                    {
                        string nseasonid = sarr[s];
                        if (nseasonid != "")
                        {
                            season_where += "or charindex('," + nseasonid + ",',','+New_Zixun_Season+',')>0 ";
                        }
                    }
                    season_where = season_where.Replace("(or", "(");
                    season_where += ")";
                }

                if (season_where.IndexOf("charindex") > 0)
                {
                    where += season_where;
                }
                #endregion

                //context.Put("redirecturl_error",page+"@@@@@@"+ where);

                ds = pc.TagList_New("*", " edittime desc ", " hrenh_article a ", where, page, pagesize, " id,title,isimg,img_url,edittime,New_Zixun_Price,New_Zixun_intro_short,isnull((select product_name from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platname,isnull((select Produce_Img from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platimg,New_Zixun_Idlist ");

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
                        string short_intro = String_Manage.Return_Str(dr["New_Zixun_intro_short"].ToString(), "");

                        if (type != 370)
                        {
                            if (short_intro != "")
                            {
                                article_title = short_intro;
                            }
                        }

                        string show_article_title = StringHelper.ReturnNumStr(article_title, 0, 29);
                        int isimg = String_Manage.Return_Int(dr["isimg"].ToString(), 0);
                        string article_img = "/template/img/nocontent.png";

                        if (isimg == 1)
                        {
                            article_img = ImgHelper.Get_UploadImgUrl(dr["img_url"].ToString(), 1);
                        }

                        string article_addtime = TimeParser.ReturnCurTime(dr["edittime"].ToString(), 1);
                        string prices = String_Manage.Return_Str(dr["New_Zixun_Price"].ToString(), "0");
                        prices = Recruit_Job.Return_Money(prices, 1);

                        //搭配栏目
                        if (type == 370)
                        {
                            type_shouzimu = "";
                        }

                        string article_url = " href='/" + prname + type_shouzimu + "/show_" + article_id + ".html' target='_blank' title='" + article_title + "' ";

                        string platname = String_Manage.Return_Str(dr["platname"].ToString(), "");
                        string platimg = String_Manage.Return_Str(dr["platimg"].ToString(), "");
                        if (platimg != "")
                        {
                            platimg = ImgHelper.Get_UploadImgUrl(platimg, 1);
                        }



                        #region  获取当前文章类型下的（特点人群下的第一类和气质风格下的第一类）
                        string typeidlist = dr["New_Zixun_Idlist"].ToString();

                        typeidlist = Set_Season_Ypdp_Typename.Get_Typename_list(typeidlist);

                        #endregion


                        #endregion

                        #region  加载文章列表


                        sbr.AppendFormat("<dl class=\"shopone\">");
                        //sbr.AppendFormat("<a {0}>", article_url);
                        //sbr.AppendFormat("<dt><img src=\"{0}\" alt=\"{1}\"><p>{2}</p></dt>", article_img,article_title, typeidlist);
                        ////sbr.AppendFormat("<dt><img src=\"{0}\" alt=\"{1}\"><img class=\"tb\" src=\"{2}\" alt=\"{3}\"></dt>", article_img, article_title, platimg, platname + "有售");

                        //sbr.AppendFormat("<dd>");
                        //sbr.AppendFormat("<h4>{0}</h4>", show_article_title);
                        ////if (platname != "")
                        ////{
                        ////    sbr.AppendFormat("<b title=\"{1}购买直通车\"><i class=\"sppt\"><img src=\"{2}\" alt=\"{1}\"></i><i>￥</i>{0}</b>", prices, platname, platimg);
                        ////}
                        ////else
                        ////{
                        ////    sbr.AppendFormat("<b><i>￥</i>{0}</b>",prices);
                        ////}
                        //////sbr.AppendFormat("<div class=\"fav\"><em class=\"fav-i\"></em>  <span class=\"fav-n\">1228</span>  </div>");
                        //sbr.AppendFormat("</dd>");
                        //sbr.AppendFormat("</a>");


                        sbr.AppendFormat("<dt>");
                        sbr.AppendFormat("<a {0}>", article_url);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", article_img, article_title);
                        sbr.AppendFormat("</a>");

                        sbr.AppendFormat("<p><a {0}>{1}</a></p>", article_url, typeidlist);

                        sbr.AppendFormat("</dt>");
                        sbr.AppendFormat("<dd>");
                        sbr.AppendFormat("<a {0}>", article_url);
                        sbr.AppendFormat("<h4>{0}</h4>", show_article_title);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</dd>");
                        


                        sbr.AppendFormat("</dl>");


                        #endregion
                    }
                }
                else
                {
                    sbr.Append("<div class=\"nodata\"></div>");
                }

                if (curpage > page_cnt)
                {
                    curpage = page_cnt;
                }

                if (curpage < page_cnt)
                {
                    sbr.AppendFormat("<dl class=\"shopone\">");
                    sbr.AppendFormat("<a href=\"javascript:;\" title=\"下一页\" class=\"cat-page-next trans200\" onclick='GoPage(" + (curpage + 1) + ")'></a>");
                    sbr.AppendFormat("</dl>");
                }


                context.Put("list", sbr.ToString());
                context.Put("listPage", NewXzc.Common.GenerPage.pageHtml(record_cnt, pagesize, curpage, "GoPage"));
            }
            catch (Exception ex)
            {
                context.Put("redirecturl", "/404");
            }
        }


    }
}