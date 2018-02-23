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
    public class subject_about : Base.BasePage
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

            if (type == 406)
            {
                prname = "zhuanti/hanxi";
            }
            else if (type == 407)
            {
                prname = "zhuanti/rixi";
            }
            else if (type == 408)
            {
                prname = "zhuanti/oumeiz";
            }
            else if (type == 417)
            {
                prname = "zhuanti/agez";
            }

            //if (prname == "")
            //{
            //    context.Put("redirecturl", "/404");
            //}
            //else
            //{

            //    if (type < 1)
            //    {
            //        type = ArticleHelper.Get_Dianpu_Type_Id(prname, 0);
            //    }

            //    //通过汉字获取该汉字的首字母
            //    if (type < 406)
            //    {
            //        type_shouzimu = Get_Hanzi_Shouzimu.GetSpellCode(type_bll.GetModel(type).TypeName);
            //    }

            //    prname = ArticleHelper.Get_Type_Rname(type);

            //    if (type==406 || type==407 || type==408)
            //    {
            //        linshi_srname = prname;
            //    }

            //    if (srname != "")
            //    {
            //        //typeurl = "/"+prname+"/"+srname+"/";
            //        typeurl = "/" + prname + "/" + srname + ".html";
            //        stype = ArticleHelper.Get_Dianpu_Type_Id(srname, 1);
            //    }
            //    else
            //    {
            //        if (linshi_srname != "")
            //        {
            //            if (prname != linshi_srname)
            //            {
            //                stype = ArticleHelper.Get_Dianpu_Type_Id(linshi_srname, 1);
            //            }
            //        }
            //        //typeurl = "/" + prname + type_shouzimu + "/";
            //        typeurl = "/" + linshi_srname + type_shouzimu + "/";
            //    }
            //}

            typeurl = "/" + prname;

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

            if (type == 406)
            {
                title_val = "韩国穿衣搭配|韩国女装搭配|韩国服饰图片-衣品搭配网|韩系穿衣搭配网";
            }
            else if (type == 407)
            {
                title_val = "日系风格|日系搭配|日系女装|日系服装搭配-衣品搭配网|日系衣服搭配";
            }
            else if (type == 408)
            {
                title_val = "欧美风搭配|欧美风图片|欧美风打扮-衣品搭配网|欧美衣服搭配";
            }
            else if (type == 417)
            {
                title_val = "矮个子女装|矮个子女生穿衣搭配|矮个子衣服的穿配法-衣品搭配网|矮个子衣服搭配";
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

            if (type == 406 || type == 407 || type == 408 || type == 417)
            {
                if (type == 406)
                {
                    context.Put("keywords", "韩国服饰,韩国服饰搭配,韩国街拍");
                    context.Put("description", "韩国服饰是亚洲范围内非常流行的流行服饰，也是最受穿搭潮人们喜爱的服饰类型！韩系服装现在是时尚圈无法阻挡的一股潮流，衣品搭配网将给您展示最新潮的韩式穿搭！");
                }
                else if (type == 407)
                {
                    context.Put("keywords", "日系,日系风格,日系服装搭配,日系服装特点,甜美日系风,日系秋装搭配");
                    context.Put("description", "日系是夹在欧美风和韩系风中的一种特立流行风格，突出可爱风格。日系和韩系都是越来越流行的服装类型，衣品搭配网将给您展示最适合您的日系搭配方法！");
                }
                else if (type == 408)
                {
                    context.Put("keywords", "欧美风,欧美风搭配,欧美风打扮,欧美风格图片");
                    context.Put("description", "欧美风，随性、简单，不同于以简约优雅著称的英伦风，更偏向于街头类型的纽约范，任何人穿都会变得更加自信、有范、洋气、大牌，让你彻底脱胎换骨、远离土里土气。");
                }
                else if (type == 417)
                {
                    context.Put("keywords", "矮个子,女装,矮个子女装,矮个子穿衣搭配图片,矮个子街拍,矮个子服装搭配图片");
                    context.Put("description", "矮个子女生常常因为身高问题影响穿衣搭配，通过衣品搭配网的专业推荐和分析一定能解决矮个子女生穿衣搭配的难题。");
                }

                context.Put("keywords_cname", "搭配");

            }
            else
            {
                context.Put("keywords", title_val.Replace("_", "，").Replace("|", "，"));
                context.Put("keywords_cname", keywords_val.Replace("红人", ""));

                context.Put("description", description);
            }

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
                DataSet tds = DbHelperSQL.Query("select typename,show_img_url,show_desc from hrenh_article_type where id=" + type + "");

                if (tds != null && tds.Tables[0].Rows.Count > 0)
                {
                    StringBuilder tsdr=new StringBuilder();

                    foreach (DataRow tdr in tds.Tables[0].Rows)
                    {
                        string ttypename = tdr["typename"].ToString();
                        string tshowimg = tdr["show_img_url"].ToString();
                        tshowimg = ImgHelper.GetCofigShowUrl() + tshowimg;
                        string tshowdesc = tdr["show_desc"].ToString();

                        tsdr.AppendFormat("<div class=\"detailtop\">");
                        tsdr.AppendFormat("<dl>");
                        tsdr.AppendFormat("<dt><a href=\"javascript:;\" title=\"{0}\"><img src=\"{1}\" alt=\"{0}\"></a></dt>",ttypename,tshowimg);
                        tsdr.AppendFormat("<dd>");
               
                        tsdr.AppendFormat("<h3>{0}</h3>",ttypename);

                        tsdr.AppendFormat("<div class=\"worddetail\">");
                        tsdr.AppendFormat("<div class=\"heightauto\" style=\"height:auto\">");
                        tsdr.AppendFormat("{0}",tshowdesc);
                        tsdr.AppendFormat("</div>");

                        tsdr.AppendFormat("</div>");
                        tsdr.AppendFormat("<span class=\"wordmore\" style=\"display:none;\">加载更多</span>");
                
                        tsdr.AppendFormat("</dd>");
                        tsdr.AppendFormat("</dl>");

                        tsdr.AppendFormat("</div>");
                    }

                    context.Put("typeshow",tsdr.ToString());
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

                if (type >= 406 && type <= 408 || type==417)
                {
                    stype = type;
                }

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

                        //if (type != 406 && type != 407 && type != 408)
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

                        //韩系、日系、欧美栏目
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