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

namespace NewXzc.Web.templatecs
{
    public class Index : Base.BasePage
    {
        StringBuilder sbr = new StringBuilder();
        DataSet ds = null;
        BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            //NewXzc.Web.templatecs.Head head = new Head();
            //head.Init_Head(context, 1);

            //context.Put("title", "衣品搭配_中国首家网络红人门户|微博红人|网络红人|网络红人排行榜");
            //context.Put("keywords", "红人,衣品搭配,红人网,红人门户,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜");
            //context.Put("description", "衣品搭配,专注发掘不一样的红人神咖.衣品搭配是红人,衣品搭配,红人网,网络红人,微博红人,爱拍红人,红人服装,网络红人排行榜等综合红人报道平台.衣品搭配网络红人聚集平台，跟踪报道红人资讯,最新红人作品等等,第一时间满足粉丝需求.");

            NewXzc.Web.templatecs.head_index head = new head_index();
            head.Init_Head(context);

            #endregion

            #region  加载底部

            NewXzc.Web.templatecs.foot_index foot = new foot_index();
            foot.Page_Load(ref context);
            #endregion


            ////解密
            //string pwd = NewXzc.Common.DEncrypt.DESEncrypt.Decrypt("8B272BD28B1CC0A3461F9CEF28E120D9");



            ////首页轮播图
            //GetTopImg(context);

            ////红人资讯
            //int xw = 1;
            ////context.Put("xwhttp","/article_"+xw);
            //context.Put("xwhttp", ArticleHelper.Get_Nav_Type_Name(xw));
            //context.Put("xw", ArticleHelper.Get_New_ZX(5, " (types=" + xw+" or types_pid="+xw+") "));
            //context.Put("xwi",GetItemImg(1002));

            ////红人作品
            //int zp = 3;
            ////context.Put("zphttp", "/article_" + zp);
            //context.Put("zphttp", ArticleHelper.Get_Nav_Type_Name(zp));
            //context.Put("zp", ArticleHelper.Get_New_ZX(5, " (types=" + zp + " or types_pid=" + zp + ") "));
            //context.Put("zpi", GetItemImg(1003));

            ////红人专访
            //int bh = 4;
            ////context.Put("bhhttp", "/article_" + bh);
            //context.Put("bhhttp", ArticleHelper.Get_Nav_Type_Name(bh));
            //context.Put("bh", ArticleHelper.Get_New_ZX(5, " (types=" + bh + " or types_pid=" + bh + ") "));
            //context.Put("bhi", GetItemImg(1004));

            ////红人专题
            //int hrg = 5;
            ////context.Put("hrghttp", "/article_" + hrg);
            //context.Put("hrghttp", ArticleHelper.Get_Nav_Type_Name(hrg));
            //context.Put("hrg", ArticleHelper.Get_New_ZX(5, " (types=" + hrg + " or types_pid=" + hrg + ") "));
            //context.Put("hrg", ArticleHelper.Get_New_ZX(5, " subject_id>0 "));
            //context.Put("hrgi", GetItemImg(1005));

            ////红人写真
            //int xz = 2;
            ////context.Put("xzhttp", "/article_" + xz);
            //context.Put("xzhttp", ArticleHelper.Get_Nav_Type_Name(xz));
            //GetXZImg(context);

            ////产品推荐
            //GetProductImg(context);

            ////活动公告
            //int gg = 6;
            ////context.Put("gghttp", "/article_" + gg);
            //context.Put("gghttp", ArticleHelper.Get_Nav_Type_Name(gg));
            //context.Put("gginfo", ArticleHelper.Get_New_GG(1, " (types=" + gg + " or types_pid=" + gg + ") "));


            ////新闻热点
            //context.Put("news_hot",Get_News_Hren(1008));


            ////红人专访
            //context.Put("hren_zf", Get_News_Hren(1009));
        }

        /// <summary>
        /// 首页轮播图
        /// </summary>
        /// <param name="context"></param>
        private void GetTopImg(NVelocity.VelocityContext context)
        {
            sbr.Clear();
            StringBuilder html = new StringBuilder();
            int cnt = 0;

            ds = recommend_bll.GetList_NewIndex(0, " pageid=1000 and cid=1001 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                cnt = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 22);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"350\" width=\"680\"";


                    //sbr.AppendFormat("<div>");

                    //if (openstyle != 1 && openstyle != 2)
                    //{
                    //    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                    //    sbr.AppendFormat("<span class=\"slides-descride\">");
                    //    sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{1}\">{0}</a>",show_title,title);
                    //    sbr.AppendFormat("</span>");
                    //}
                    //else
                    //{
                    //    sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" {3}></a>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle), width_height);
                    //    sbr.AppendFormat("<span class=\"slides-descride\">");
                    //    sbr.AppendFormat("<a {2} title=\"{1}\">{0}</a>", show_title, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                    //    sbr.AppendFormat("</span>");
                    //}
                    //sbr.AppendFormat("</div>");


                    sbr.AppendFormat("<div class=\"item imgwrap\">");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, title);
                        //sbr.AppendFormat("<span class=\"slides-descride\">");
                        //sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{1}\">{0}</a>", show_title, title);
                        //sbr.AppendFormat("</span>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        //sbr.AppendFormat("<span class=\"slides-descride\">");
                        //sbr.AppendFormat("<a {2} title=\"{1}\">{0}</a>", show_title, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        //sbr.AppendFormat("</span>");
                    }
                    sbr.AppendFormat("</div>");

                    if (i == 0)
                    {
                        html.AppendFormat("<span class=\"dot_list current\" onmouseover=\"slide_00.slideToNearBy({0})\"></span>",i);
                    }
                    else
                    {
                        html.AppendFormat("<span class=\"dot_list\" onmouseover=\"slide_00.slideToNearBy({0})\"></span>",i);
                    }

                }
            }

            context.Put("lunbo", sbr.ToString());
            context.Put("lunbo_2", html.ToString());
            context.Put("cnt", cnt);
        }


        /// <summary>
        /// 获取红人资讯、红人作品、红人专访、红人专题，这四项右侧的推荐图
        /// </summary>
        /// <param name="cid">对应位置编号</param>
        private string GetItemImg(int cid)
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(1, " pageid=1000 and cid="+cid+" ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 0, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 0, 14);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"138\" width=\"236\"";


                    sbr.AppendFormat("<div class=\"item-content-img\">");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">",title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>",imgurl,title,width_height);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("<span>");
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{1}\">{0}</a>",show_title,title);
                        sbr.AppendFormat("</span>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("<span>");
                        sbr.AppendFormat("<a {2} title=\"{1}\">{0}</a>", show_title, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("</span>");
                    }
                    sbr.AppendFormat("</div>");
                }
            }

            return sbr.ToString();

        }

        /// <summary>
        /// 红人写真
        /// </summary>
        /// <param name="context"></param>
        private void GetXZImg(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(5, " pageid=1000 and cid=1006 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 2, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 2, 22);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"405\" width=\"450\"";

                    switch (i)
                    { 
                        case 0:
                            width_height = "height=\"405\" width=\"450\"";
                            break;
                        case 1:
                            width_height = "height=\"200\" width=\"340\"";
                            break;
                        case 2:
                            width_height = "height=\"200\" width=\"200\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 12);
                            break;
                        case 3:
                            width_height = "height=\"200\" width=\"200\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 12);
                            break;
                        case 4:
                            width_height = "height=\"200\" width=\"340\"";
                            break;
                    }

                    sbr.AppendFormat("<div class=\"item-photo-box\">");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">",title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("<span>");
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{1}\">{0}</a>", show_title, title);
                        sbr.AppendFormat("</span>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("<span>");
                        sbr.AppendFormat("<a {1} title=\"{0}\">{2}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle),show_title);
                        sbr.AppendFormat("</span>");
                    }

                    sbr.AppendFormat("</div>");
                }
            }

            context.Put("xz", sbr.ToString());

        }


        /// <summary>
        /// 产品推荐
        /// </summary>
        /// <param name="context"></param>
        private void GetProductImg(NVelocity.VelocityContext context)
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(6, " pageid=1000 and cid=1007 ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 2, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 2, 22);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"180\" width=\"429\"";

                    switch (i)
                    {
                        case 0:
                            width_height = "height=\"180\" width=\"429\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 28);
                            context.Put("p1",GetProductContent(title,show_title,openstyle,httpurl,imgurl,width_height));
                            break;
                        case 1:
                            width_height = "height=\"365\" width=\"283\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 18);
                            context.Put("p2", GetProductContent(title, show_title, openstyle, httpurl, imgurl, width_height));
                            break;
                        case 2:
                            width_height = "height=\"180\" width=\"278\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 12);
                            context.Put("p3", GetProductContent(title, show_title, openstyle, httpurl, imgurl, width_height));
                            break;
                        case 3:
                            width_height = "height=\"180\" width=\"212\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 13);
                            context.Put("p4", GetProductContent(title, show_title, openstyle, httpurl, imgurl, width_height));
                            break;
                        case 4:
                            width_height = "height=\"180\" width=\"212\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 13);
                            context.Put("p5", GetProductContent(title, show_title, openstyle, httpurl, imgurl, width_height));
                            break;
                        case 5:
                            width_height = "height=\"180\" width=\"278\"";
                            show_title = StringHelper.ReturnNumStr(title, 2, 17);
                            context.Put("p6", GetProductContent(title, show_title, openstyle, httpurl, imgurl, width_height));
                            break;
                    }

                    
                }
            }

        }

        /// <summary>
        /// 获取产品推荐的内容
        /// </summary>
        /// <param name="title"></param>
        /// <param name="show_title"></param>
        /// <param name="openstyle"></param>
        /// <param name="httpurl"></param>
        /// <param name="imgurl"></param>
        /// <param name="width_height"></param>
        /// <returns></returns>
        private string GetProductContent(string title,string show_title,int openstyle,string httpurl, string imgurl, string width_height)
        {
            sbr.Clear();

            sbr.AppendFormat("<div class=\"item-productReco-box\">");

            if (openstyle != 1 && openstyle != 2)
            {
                sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">", title);
                sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                sbr.AppendFormat("</a>");
                sbr.AppendFormat("<span>");
                sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{1}\">{0}</a>", show_title, title);
                sbr.AppendFormat("</span>");
            }
            else
            {
                sbr.AppendFormat("<a {1} title=\"{0}\">", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                sbr.AppendFormat("</a>");
                sbr.AppendFormat("<span>");
                sbr.AppendFormat("<a {1} title=\"{0}\">{2}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle), show_title);
                sbr.AppendFormat("</span>");
            }

            sbr.AppendFormat("</div>");

            return sbr.ToString();
        }



        /// <summary>
        /// 新闻热点、红人专访
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        private string Get_News_Hren(int cid)
        {
            sbr.Clear();

            ds = recommend_bll.GetList_NewIndex(8, " pageid=1000 and cid="+cid+" ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = ArticleHelper.GetArticleTitle(dr["v2"].ToString());
                    title = StringHelper.ReturnNumStr(title, 2, 0);
                    string show_title = StringHelper.ReturnNumStr(title, 2, 18);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 0, 80);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"130\" width=\"300\"";


                    sbr.AppendFormat("<li>");

                    if (openstyle != 1 && openstyle != 2)
                    {
                        sbr.AppendFormat("<a href=\"javascript:void(0)\" title=\"{0}\">{1}", title,show_title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }
                    else
                    {
                        sbr.AppendFormat("<a {1} title=\"{0}\">{2}", title, Return_HttpURL.Return_Url(httpurl, openstyle),show_title);
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, title, width_height);
                        sbr.AppendFormat("</a>");
                    }

                    sbr.AppendFormat("</li>");
                }
            }

            return sbr.ToString();

        }


    }
}