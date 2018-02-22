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
    public class subject : Base.BasePage
    {

        NewXzc.Web.templatecs.Head_Article head = new Head_Article();

        BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

        int type_parent = 1;
        int type = 1000;
        int stype = 0;
        string typename = "";
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


            //子级
            srname = String_Manage.Return_Request_Str("srname");

            if (srname != "")
            {
                stype = ArticleHelper.Get_Dianpu_Type_Id(srname, 1);
            }

            typeurl = "/zhuanti/" + srname + "";

            context.Put("typepurl", typeurl);
            context.Put("srname", srname);


            head.Init_Head(context, type);

            #region  加载二级和三级

            if (stype > 0)
            {
                typename = ArticleHelper.Get_Type_Name(stype, type_parent);
            }

            if (typename != "")
            {
                title_val = typename.Substring(0,typename.IndexOf(","));
                typename = title_val;
            }

            context.Put("title", title_val + "-衣品搭配网|"+typename+"衣服搭配");

            #region  获取keywords和description
            try
            {
                int type_linshi = type;

                if (stype > 0)
                {
                    type_linshi = stype;
                }

                DataSet kdds = DbHelperSQL.Query("select keywords,description,pid from hrenh_article_type where id=" + type_linshi + "");
                if (kdds != null && kdds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow kddr in kdds.Tables[0].Rows)
                    {
                        keywords_val = kddr["keywords"].ToString();
                        string description_val = kddr["description"].ToString();
                        int spid = Convert.ToInt32(kddr["pid"].ToString());

                        if (keywords_val != "")
                        {
                            title_val = keywords_val;
                        }

                        //if (description_val != "")
                        //{
                            //description = description_val;
                        //}

                        //上衣、裤装、裙装
                        if (spid >= 371 && spid <= 373)
                        {
                            description = "" + typename + "是每个女生衣橱里必备的服装单品。不论有多少款式的" + typename + "找到适合自己的款就能穿出属于自己的风格，适合自己的才能完美遮住自身缺点，这才是成功的" + typename + "搭配技巧。";
                        }
                        else if (spid == 403)//气质风格
                        {
                            description = "" + typename + "是目前时尚界较为流行的服装风格，受到很多漂亮MM的喜爱。根据自己的身高、体型、肤色等外在特点才能找到自己的穿衣风格，轻松变身时尚大咖。";
                        }
                        else if (spid == 404)//特定人群
                        {
                            description = "好的搭配能遮掉自身的很多缺点，适合自己的服装才能扬长避短，比如穿什么样的衣服能" + typename + "，一旦掌握了能" + typename + "的穿衣秘诀，无论是谁都能穿出明星范。";
                        }
                        else if (spid == 405)//潮流元素
                        {
                            description = "服装上的潮流元素每年都会不尽相同。例如" + typename + "原本是非常普遍的元素，也许哪年就会成为时下最流行的潮流元素，只有紧跟潮流元素的步伐才会离时尚越来越近。";
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
            #endregion

            if (title_val != "")
            {
                context.Put("title", title_val.Replace("，", ",").Replace(",", "|") + "-衣品搭配网|" + typename + "衣服搭配");
            }

            context.Put("keywords", keywords_val);
            context.Put("keywords_cname", keywords_val);

            context.Put("description", description);


            #endregion

            // 获取当前类型的封面图和描述
            Get_Type_Show_Dest(context);

            // 加载文章列表
            GetList(context);

        }


        /// <summary>
        /// 获取当前类型的封面图和描述
        /// </summary>
        /// <param name="context"></param>
        private void Get_Type_Show_Dest(NVelocity.VelocityContext context)
        {
            try
            {
                DataSet tds = DbHelperSQL.Query("select typename,show_img_url,show_desc from hrenh_article_type where id=" + stype + "");

                if (tds != null && tds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder tsdr = new StringBuilder();

                    foreach (DataRow tdr in tds.Tables[0].Rows)
                    {
                        string ttypename = tdr["typename"].ToString();
                        string tshowimg = tdr["show_img_url"].ToString();
                        tshowimg = ImgHelper.GetCofigShowUrl() + tshowimg;
                        string tshowdesc = tdr["show_desc"].ToString();

                        tsdr.AppendFormat("<div class=\"detailtop\">");
                        tsdr.AppendFormat("<dl>");
                        tsdr.AppendFormat("<dt><a href=\"javascript:;\" title=\"{0}\"><img src=\"{1}\" alt=\"{0}\"></a></dt>", ttypename, tshowimg);
                        tsdr.AppendFormat("<dd>");

                        tsdr.AppendFormat("<h3>{0}</h3>", ttypename);

                        tsdr.AppendFormat("<div class=\"worddetail\">");
                        tsdr.AppendFormat("<div class=\"heightauto\" style=\"height:auto\">");
                        tsdr.AppendFormat("{0}", tshowdesc);
                        tsdr.AppendFormat("</div>");

                        tsdr.AppendFormat("</div>");
                        tsdr.AppendFormat("<span class=\"wordmore\" style=\"display:none;\">加载更多</span>");

                        tsdr.AppendFormat("</dd>");
                        tsdr.AppendFormat("</dl>");

                        tsdr.AppendFormat("</div>");
                    }

                    context.Put("typeshow", tsdr.ToString());
                }

            }
            catch (Exception ex)
            {

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

                string where = " and types=370 and isend=0 and a.pub_state=0 and type_parent=" + type_parent + " and New_Zixun_Is_Tbk=0 ";


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
                    season_where = season_where.Replace("(or","(");
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

                        //if (type != 406 && type != 407 && type != 431)
                        //{
                        //    if (short_intro != "")
                        //    {
                        //        article_title = short_intro;
                        //    }
                        //}

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

                        //韩系、日系、欧美三个栏目下被推荐的文章
                        prname = "dapei";
                        type_shouzimu = "";

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