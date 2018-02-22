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
    public class subject_index : Base.BasePage
    {

        NewXzc.Web.templatecs.Head_Article head = new Head_Article();

        BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

        int type = 1000;
        int stype = 0;
        string typename = "上衣";
        string typeurl = "";

        string description = "";

        string prname = "";

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

            head.Init_Head(context, type);

            #region  加载二级和三级

            title_val = "穿衣搭配专题|女装搭配专题|服装搭配专题-衣品搭配网|衣服搭配专题";

            context.Put("title", title_val);

            context.Put("keywords", "专题,穿衣搭配,服装搭配,服饰搭配");
            context.Put("keywords_cname", keywords_val.Replace("红人", ""));

            context.Put("description", "专题栏目涵盖所有和服装搭配相关的内容，在专题页你可以全面了解和学习关于服装搭配的知识和技巧。");

            Get_Second_Third(context);

            #endregion


            int pageid = 2300;

            #region  列表页最新排行上方轮播图

            context.Put("topadimg", ArticleHelper.Get_Tj_Img(1, pageid, 1001));

            #endregion

        }


        /// <summary>
        /// 获取二级和三级
        /// </summary>
        private void Get_Second_Third(NVelocity.VelocityContext context)
        {
            sbr.Clear();


            try
            {
                string son_sql = "select a.id,a.typename,a.lvl,(select top 1 TypeImg from HRENH_ARTICLE_TYPE where id=a.id) as typeimg from return_article_all_son_typelist(370,1) a where a.lvl=1 and (select state from hrenh_article_type where id=a.id)=1";


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

                        string typename_desc = "";

                        switch (typename)
                        {
                            case "上衣":
                                typename = "上衣 Clothes";
                                typename_desc = "全面展示各种属于上衣分类的精致单品";
                                break;
                            case "裤装":
                                typename = "裤装 Trousers";
                                typename_desc = "所有属于裤装类型的全部分类应有尽有";
                                break;
                            case "裙装":
                                typename = "裙装 Skirt";
                                typename_desc = "女生衣橱必不可少的各种裙装分类总有属于你的款";
                                break;
                            case "气质风格":
                                typename = "气质风格 Style";
                                typename_desc = "根据搭配习惯问题，每个人都有属于自己的气质风格";
                                break;
                            case "特定人群":
                                typename = "特定人群 Populations";
                                typename_desc = "根据不同人的体貌特征把人群大致分为以下七个类型";
                                break;
                            case "潮流元素":
                                typename = "潮流元素 Tidal Element";
                                typename_desc = "时装界每年都会有代表今年流行趋势的的潮流元素";
                                break;
                        }

                        string article_url = " href='javascript:void(0)' title='" + typename + "' ";
                        sbr.AppendFormat("<div class=\"g-title\">");
                        sbr.AppendFormat("<div class=\"title\">");
                        sbr.AppendFormat("<strong class=\"clothes\"></strong>");
                        sbr.AppendFormat("<span><a href=\"javascript:;\" title=\"{0}\">{0}</a></span>",typename);
                        sbr.AppendFormat("</div>");

                        sbr.AppendFormat("<div class=\"links\">");
                        sbr.AppendFormat("<!--描述-->");
                        sbr.AppendFormat("<p>{0}</p>", typename_desc);
                        sbr.AppendFormat("</div>");

                        sbr.AppendFormat("</div>");


                        #region  加载三级

                        //string third_sql = "select a.id,a.typename,(select TypeRename from hrenh_article_type where id=a.id) as rname,(select top 1 TypeImg from HRENH_ARTICLE_TYPE where id=a.id) as typeimg from return_article_all_son_typelist(" + id + ",1) a where a.lvl=1";
                        string third_sql = "select a.id,a.typename,a.TypeRename as rname,a.TypeImg as typeimg from hrenh_article_type a where state=1 and id in(select id from return_article_all_son_typelist(" + id + ",1) where lvl=1) and charindex(','+convert(varchar,SEASON_ID,10)+',','" + season_require_id_linshi + "')>0";

                        DataSet sonds = DbHelperSQL.Query(third_sql);


                        if (sonds != null && sonds.Tables[0].Rows.Count > 0)
                        {

                            sbr.AppendFormat("<div class=\"g-content m-second\">");
                            sbr.AppendFormat("<div class=\"second clearfix\">");
                            sbr.AppendFormat("<div class=\"smore\"><ul>");

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

                            for (int j = 0; j < sonds.Tables[0].Rows.Count; j++)
                            {
                                if (j < total_cnt)
                                {
                                    DataRow sdr = sonds.Tables[0].Rows[j];
                                    id = sdr["id"].ToString();
                                    typename = sdr["typename"].ToString();
                                    string rname = sdr["rname"].ToString();

                                    typeurl = "/zhuanti/" + rname + ".html";

                                    article_url = " href='" + typeurl + "' title='" + typename + "' target='_blank' ";
                                    typeimg = sdr["typeimg"].ToString();

                                    typeimg = ImgHelper.GetCofigShowUrl() + typeimg;


                                    sbr.AppendFormat("<li><a {0}><img src=\"{1}\" alt=\"{2}\"><i>{2}</i></a></li>",article_url,typeimg,typename);


                                }
                                else
                                {
                                    break;
                                }
                            }

                            sbr.AppendFormat("</ul></div>");
                            sbr.AppendFormat("</div>");
                            sbr.AppendFormat("</div>");

                        }

                        #endregion

                    }
                }


                context.Put("Second_Third", sbr.ToString());

            }
            catch (Exception ex)
            {
                //context.Put("redirecturl","/404");
                context.Put("redirecturl_error", ex.ToString());
            }

        }



    }
}