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
    public class jingxuan : Base.BasePage
    {

        NewXzc.Web.templatecs.Head_Article head = new Head_Article();

        BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

        int type_parent = 1;
        int type = 1001;
        int stype = 0;
        string typename = "精选";
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


            prname = "jingxuan";

            typeurl = "/" + prname + "";

            context.Put("typepurl", typeurl);


            head.Init_Head(context, type);

            #region  加载二级和三级

            title_val = typename + "";

            title_val = "精选|穿衣搭配|女装精选-衣品搭配网|穿衣搭配网";

            context.Put("title", title_val);

            context.Put("keywords", "精选,穿衣搭配,服装搭配,服饰搭配");
            context.Put("keywords_cname", "搭配");

            context.Put("description", "精选栏目汇聚众多小编精挑细选出来的服装搭配图片以及搭配技巧，每篇文章都会让你体学到新的搭配知识。");


            #endregion

            // 加载文章列表
            GetList(context);

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

                string where = " and types=370 and isend=0 and a.pub_state=0 and type_parent=" + type_parent + " and New_Zixun_Is_Tbk=0 and istop=1  ";


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

                ds = pc.TagList_New("*", " edittime desc ", " hrenh_article a ", where, page, pagesize, " id,title,isimg,img_url,edittime,New_Zixun_Price,New_Zixun_intro_short,isnull((select product_name from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platname,isnull((select Produce_Img from HRENH_PRODUCT_PLATINFO where id=a.product_plat_id),'') as platimg ");

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

                        #endregion

                        #region  加载文章列表


                        sbr.AppendFormat("<dl class=\"shopone\">");
                        sbr.AppendFormat("<a {0}>", article_url);
                        sbr.AppendFormat("<dt><img src=\"{0}\" alt=\"{1}\"></dt>", article_img, article_title);
                        //sbr.AppendFormat("<dt><img src=\"{0}\" alt=\"{1}\"><img class=\"tb\" src=\"{2}\" alt=\"{3}\"></dt>", article_img, article_title, platimg, platname + "有售");

                        sbr.AppendFormat("<dd>");
                        sbr.AppendFormat("<h4>{0}</h4>", show_article_title);
                        //if (platname != "")
                        //{
                        //    sbr.AppendFormat("<b title=\"{1}购买直通车\"><i class=\"sppt\"><img src=\"{2}\" alt=\"{1}\"></i><i>￥</i>{0}</b>", prices, platname, platimg);
                        //}
                        //else
                        //{
                        //    sbr.AppendFormat("<b><i>￥</i>{0}</b>",prices);
                        //}
                        ////sbr.AppendFormat("<div class=\"fav\"><em class=\"fav-i\"></em>  <span class=\"fav-n\">1228</span>  </div>");
                        sbr.AppendFormat("</dd>");
                        sbr.AppendFormat("</a>");
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