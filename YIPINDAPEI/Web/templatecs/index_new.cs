using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using NewXzc.Common;
using NewXzc.Web.Common;
using NewXzc.Web.Common.uhelper;
using NewXzc.DBUtility;

namespace NewXzc.Web.templatecs
{
    public class index_new : Base.BasePage
    {
        StringBuilder sbr = new StringBuilder();
        DataSet ds = null;
        BLL.RED_RECOMMEND recommend_bll = new BLL.RED_RECOMMEND();

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载头部及Title
            NewXzc.Web.templatecs.head_index head = new head_index();
            head.Init_Head(context);
            #endregion


            // 获取推荐内容
            Get_Recommend_Html(context);

        }


        /// <summary>
        /// 获取推荐内容
        /// </summary>
        /// <param name="context"></param>
        private void Get_Recommend_Html(NVelocity.VelocityContext context)
        {
            #region  首页头部广告图
            context.Put("head_ad",Get_RHtml(1001,0,context));
            #endregion

            #region  首页头部轮播图
            context.Put("head_lb", Get_RHtml(1002, 5,context));
            #endregion

            #region  首页头部轮播图下方最左侧两张小图
            context.Put("head_lb_two", Get_RHtml(1003, 2,context));
            #endregion
            
            #region  首页头部轮播图下方最左侧一张背景图
            context.Put("head_lb_left_one", Get_RHtml(1004, 1,context));
            #endregion

            #region  首页头部轮播图下方最左侧一张背景图上的文章
            context.Put("head_lb_left_one_article", Get_RHtml(1013, 5, context));
            #endregion

            #region  首页头部今日热点下方一张图
            context.Put("head_jrrd_img", Get_RHtml(1005, 1, context));
            #endregion

            #region  首页头部VIDEO精彩视频
            context.Put("head_video", Get_RHtml(1006, 1, context));
            #endregion

            #region  首页红人资讯大图
            context.Put("head_hrzx", Get_RHtml(1007, 1, context));
            #endregion

            #region  首页红人资讯大图右侧轮播图
            context.Put("head_hrzx_lb", Get_RHtml(1008, 3, context));
            #endregion

            #region  首页红人资讯大图下方左侧两张小图
            context.Put("head_hrzx_left_two", Get_RHtml(1010, 2, context));
            #endregion

            #region  首页红人资讯大图下方右侧一张大图
            context.Put("head_hrzx_right_one", Get_RHtml(1011, 1, context));
            #endregion

            #region  首页红人资讯大图下方右侧一张大图下方的文章
            context.Put("head_hrzx_right_one_article", Get_RHtml(1012, 5, context));
            #endregion

            #region  首页红人资讯大图右侧轮播图下方文章下方的文章下方的图片
            context.Put("head_hrzx_right_one_article_img", Get_RHtml(1014, 1, context));
            #endregion

            #region  首页红人作品大图
            context.Put("head_hrzp", Get_RHtml(1015, 1, context));
            #endregion

            #region  首页红人作品大图右侧图片
            context.Put("head_hrzp_img", Get_RHtml(1016, 1, context));
            #endregion

            #region  首页红人作品大图下方四张小图
            context.Put("head_hrzp_img_four", Get_RHtml(1017, 4, context));
            #endregion

            #region  首页红人作品大图下方四张小图右侧轮播图
            context.Put("head_hrzp_img_four_lb", Get_RHtml(1019, 5, context));
            #endregion

            #region  首页红人专访大图
            context.Put("head_hrzf", Get_RHtml(1020, 1, context));
            #endregion

            #region  首页红人专访大图右侧一张图
            context.Put("head_hrzf_img", Get_RHtml(1021, 1, context));
            #endregion

            #region  首页红人专访大图下方三张图
            context.Put("head_hrzf_img_three", Get_RHtml(1022, 3, context));
            #endregion

            #region  首页红人专访大图右侧一张图下方图片
            context.Put("head_hrzf_img_right_img", Get_RHtml(1023, 1, context));
            #endregion

            #region  首页红人写真轮播图
            context.Put("head_hrxz_lb", Get_RHtml(1024, 5, context));
            #endregion

            #region  首页红人写真轮播图右侧三张大图
            context.Put("head_hrxz_lb_img_three", Get_RHtml(1025, 3, context));
            #endregion

            #region  首页红人写真轮播图右侧两张小图
            context.Put("head_hrxz_lb_img_right_two", Get_RHtml(1026, 2, context));
            #endregion

            #region  首页红人微吧二级栏目
            context.Put("head_weiba_son", Get_RHtml(1036, 0, context));
            #endregion

            #region  首页红人微吧下方三张图
            context.Put("head_weiba_img_three", Get_RHtml(1027, 3, context));
            #endregion

            #region  首页红人微吧下方三张图下方微吧
            context.Put("head_weiba_img_three_wb_four", Get_RHtml(1035, 4, context));
            #endregion

            #region  首页红人微吧轮播图
            context.Put("head_weiba_lb", Get_RHtml(1028, 5, context));
            #endregion

            #region  首页红人爱品二级栏目
            context.Put("head_hrip_son", Get_RHtml(1037, 0, context));
            #endregion

            #region  首页红人爱品轮播图
            context.Put("head_hrip_lb", Get_RHtml(1030, 5, context));
            #endregion

            #region  首页红人爱品轮播图右侧六张图
            context.Put("head_hrip_lb_img_six", Get_RHtml(1031, 6, context));
            #endregion

            #region  首页人物推荐上方广告图
            context.Put("head_rwtj_ad", Get_RHtml(1032, 0, context));
            #endregion

            #region  首页人物推荐
            context.Put("head_rwtj", Get_RHtml(1033, 5, context));
            #endregion

            #region  首页聚合阅读下方微吧
            context.Put("head_jhrd_wb", Get_RHtml(1034, 0, context));
            #endregion

            #region  首页聚合阅读下方红人汇

            //红人
            context.Put("hren_html",Get_JHYD(1));
            //资讯
            context.Put("zx_html", Get_JHYD(2));
            //潮范
            context.Put("cf_html", Get_JHYD(3));

            #endregion

            #region  今日热点
            context.Put("today_hot", Get_News_Hren(1,6));
            #endregion

            #region  红人资讯
            context.Put("today_hrzx", Get_News_Hren(2, 5));
            #endregion

            #region  红人作品
            context.Put("today_hrzf", Get_News_Hren(3, 5));
            #endregion

            #region  加载二级栏目

            //红人资讯
            context.Put("son_hrzx", Get_Son_Type(1));

            //红人作品
            context.Put("son_hrzp", Get_Son_Type(3));

            //红人写真
            context.Put("son_hrxz", Get_Son_Type(2));

            #endregion
        }

        /// <summary>
        /// 获取返回结果
        /// </summary>
        /// <param name="cid">模块ID</param>
        /// <param name="top">获取指定数目的数据，可为0（代表全部）</param>
        /// <returns></returns>
        private string Get_RHtml(int cid, int top,NVelocity.VelocityContext context)
        {
            sbr.Clear();
            StringBuilder html = new StringBuilder();
            StringBuilder weiba_article = new StringBuilder();
            int cnt = 0;
            int pageid = 1500;

            ds = recommend_bll.GetList_NewIndex(0, " pageid="+pageid+" and cid="+cid+" ");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                cnt = ds.Tables[0].Rows.Count;

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 1, 0);
                    string show_title = StringHelper.ReturnNumStr(title,1 , 0);
                    string desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 3, 55);
                    string imgurl = ImgHelper.GetCofigShowUrl() + dr["v3"].ToString();
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    string width_height = "height=\"128\" width=\"128\"";

                    string hittpurl = " href=\"javascript:void(0);\"";

                    if (cid == 1001)
                    {
                        #region  首页头部广告图
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
                            sbr.AppendFormat("<a {0} title=\"{1}\">",  Return_HttpURL.Return_Url(httpurl, openstyle),title);
                        }

                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, show_title,width_height);
                        sbr.AppendFormat("<p class=\"tuceng4\"><span class=\"mone\">￥{0}</span></p>",Recruit_Job.Return_Money(desc,2));
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1002)
                    {
                        #region  首页头部轮播图
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);

                        if (i == 0)
                        {
                            sbr.AppendFormat("<div class=\"item on\">");
                        }
                        else
                        {
                            sbr.AppendFormat("<div class=\"item\">");
                        }

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        }
                        sbr.AppendFormat("</div>");
                        #endregion
                    }
                    else if (cid == 1003)
                    {
                        #region  首页头部轮播图下方最左侧两张小图
                        show_title = StringHelper.ReturnNumStr(title, 1, 16);
                        width_height = "height=\"305\" width=\"300\"";

                        sbr.AppendFormat("<div class=\"ad_switch\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, show_title,width_height);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" {3}></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle),width_height);
                        }

                        sbr.AppendFormat("<div class=\"title\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\">{0}</a>", show_title,title,hittpurl);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\">{0}</a>", show_title, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        }
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1004)
                    {
                        #region  首页头部轮播图下方最左侧一张背景图
                        show_title = StringHelper.ReturnNumStr(title, 1, 16);
                        desc = StringHelper.ReturnNumStr(dr["v2"].ToString(), 1, 55);

                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);

                        StringBuilder goodsimg=new StringBuilder();

                        if (openstyle != 1 && openstyle != 2)
                        {
                            goodsimg.AppendFormat("<h2 class=\"f24\">");
                            goodsimg.AppendFormat("<a {0} class=\"white\" title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                            goodsimg.AppendFormat("</h2>");
                            goodsimg.AppendFormat("<p>{0}</p>",desc);
                        }
                        else
                        {
                            goodsimg.AppendFormat("<h2 class=\"f24\">");
                            goodsimg.AppendFormat("<a {0} class=\"white\" title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            goodsimg.AppendFormat("</h2>");
                            goodsimg.AppendFormat("<p>{0}</p>",desc);
                        }

                        context.Put("head_lb_left_one_title",goodsimg.ToString());
                        #endregion
                    }
                    else if (cid == 1013)
                    {
                        #region  首页头部轮播图下方最左侧一张背景图上的文章
                        show_title = StringHelper.ReturnNumStr(title, 1, 13);

                        sbr.AppendFormat("<li class=\"fcut\">");

                        string clname = "white";

                        if (i == 0)
                        {
                            clname = "orange";
                        }

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} class=\"{1}\" title=\"{2}\">{3}</a>",hittpurl,clname,title,show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} class=\"{1}\" title=\"{2}\">{3}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), clname,title, show_title);
                        }

                        sbr.AppendFormat("</li>");
                        #endregion
                    }
                    else if (cid == 1005)
                    {
                        #region  首页头部今日热点下方一张图
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);
                        width_height = "height=\"250\" width=\"300\" border=\"0\"";

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, show_title,width_height);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" {3}></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle),width_height);
                        }

                        #endregion
                    }
                    else if (cid == 1006)
                    {
                        #region  首页头部VIDEO精彩视频
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\">{1}</em><span class=\"video\"></span></a>", imgurl, show_title, hittpurl,title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\">{1}</em><span class=\"video\"></span></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle),title);
                        }

                        #endregion
                    }
                    else if (cid == 1007)
                    {
                        #region  首页红人资讯大图
                        show_title = StringHelper.ReturnNumStr(title, 1, 36);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, hittpurl, title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }
                        #endregion
                    }
                    else if (cid == 1008)
                    {
                        #region  首页红人资讯大图右侧轮播图
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);
                        width_height = "height=\"280\" width=\"300\" border=\"0\"";

                        sbr.AppendFormat("<div class=\"item\">");
                        sbr.AppendFormat("<div class=\"g-pic\">");

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\" {4}><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, hittpurl, title,width_height);

                            html.AppendFormat("<li><a {0} class=\"\" title=\"{1}\">{2}</a></li>",hittpurl,title,show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\" {4}><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle), title,width_height);

                            html.AppendFormat("<li><a {0} class=\"\" title=\"{1}\">{2}</a></li>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                        }

                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");

                        
                        #endregion
                    }
                    else if (cid == 1010)
                    {
                        #region  首页红人资讯大图下方左侧两张小图
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);

                        sbr.AppendFormat("<div class=\"g-pic\">");

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, hittpurl, title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }

                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1011)
                    {
                        #region  首页红人资讯大图下方右侧一张大图
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);

                        sbr.AppendFormat("<div class=\"g-pic\">");

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, hittpurl, title);

                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\"><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle), title);

                        }

                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1012)
                    {
                        #region  首页红人资讯大图下方右侧一张大图下方的文章
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);

                        if (i > 0)
                        {
                            sbr.AppendFormat("<li>");
                        }

                        if (openstyle != 1 && openstyle != 2)
                        {
                            if (i > 0)
                            {
                                sbr.AppendFormat("<a {0} class=\"\" title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                            }
                            else
                            {
                                html.AppendFormat("<a {0} class=\"\" title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                            }
                        }
                        else
                        {
                            if (i > 0)
                            {
                                sbr.AppendFormat("<a {0} class=\"\" title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            }
                            else
                            {
                                html.AppendFormat("<a {0} class=\"\" title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            }
                        }

                        if (i > 0)
                        {
                            sbr.AppendFormat("</li>");
                        }

                        if (i == 0)
                        {
                            context.Put("head_hrzx_right_one_article_title_one", html.ToString());
                        }

                        #endregion
                    }
                    else if (cid == 1014)
                    {
                        #region  首页红人资讯大图右侧轮播图下方文章下方的文章下方的图片
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);
                        width_height = "height=\"250\" width=\"300\" border=\"0\"";


                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" {2}>", imgurl, show_title,width_height);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" {3}></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle),width_height);
                        }

                        #endregion
                    }
                    else if (cid == 1015)
                    {
                        #region  首页红人作品大图
                        show_title = StringHelper.ReturnNumStr(title, 1, 18);
                        desc = StringHelper.ReturnNumStr(desc, 1, 18);

                        StringBuilder h2sbr = new StringBuilder();
                        StringBuilder h3sbr = new StringBuilder();

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                            h2sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                            h3sbr.AppendFormat("<a {0} title=\"{1}\">{1}</a>", hittpurl, desc);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                            h2sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            h3sbr.AppendFormat("<a {0} title=\"{1}\">{1}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), desc);
                        }

                        sbr.AppendFormat("<div class=\"line\"></div>");

                        sbr.AppendFormat("<div class=\"tit\">");
                        sbr.AppendFormat("<h2>");
                        sbr.AppendFormat(h2sbr.ToString());
                        sbr.AppendFormat("</h2>");
                        sbr.AppendFormat("<h3>");
                        sbr.AppendFormat(h3sbr.ToString());
                        sbr.AppendFormat("</h3>");
                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1016)
                    {
                        #region  首页红人作品大图右侧图片
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);

                        StringBuilder ddsbr = new StringBuilder();

                        sbr.AppendFormat("<dt>");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                            ddsbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                            ddsbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>",  Return_HttpURL.Return_Url(httpurl, openstyle),title,show_title);
                        }
                        sbr.AppendFormat("</dt>");
                        sbr.AppendFormat("<dd>");
                        sbr.AppendFormat(ddsbr.ToString());
                        sbr.AppendFormat("</dd>");

                        #endregion
                    }
                    else if (cid == 1017)
                    {
                        #region  首页红人作品大图下方四张小图
                        show_title = StringHelper.ReturnNumStr(title, 1, 12);

                        StringBuilder foursbr = new StringBuilder();


                        if (i < 3)
                        {
                            sbr.AppendFormat("<div class=\"g-pic w240\">");
                        }
                        else
                        {
                            foursbr.AppendFormat("<div class=\"g-pic w240\">");
                        }

                        if (openstyle != 1 && openstyle != 2)
                        {
                            if (i < 3)
                            {
                                sbr.AppendFormat("<a {0} class=\"white\" title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                            }
                            else
                            {
                                foursbr.AppendFormat("<a {0} class=\"white\" title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                            }
                        }
                        else
                        {
                            if (i < 3)
                            {
                                sbr.AppendFormat("<a {0} class=\"white\" title=\"{1}\">", hittpurl, title);
                            }
                            else
                            {
                                foursbr.AppendFormat("<a {0} class=\"white\" title=\"{1}\">", hittpurl, title);
                            }
                        }

                        if (i < 3)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                            sbr.AppendFormat("<em class=\"fcut\"><span>{0}</span></em>", show_title);
                            sbr.AppendFormat("</a>");

                            sbr.AppendFormat("</div>");
                        }
                        else
                        {
                            foursbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                            foursbr.AppendFormat("<em class=\"fcut\"><span>{0}</span></em>", show_title);
                            foursbr.AppendFormat("</a>");

                            foursbr.AppendFormat("</div>");
                        }

                        context.Put("foursbr", foursbr.ToString());

                        #endregion
                    }
                    else if (cid == 1019)
                    {
                        #region  首页红人作品大图下方四张小图右侧轮播图
                        show_title = StringHelper.ReturnNumStr(title, 1, 13);
                        desc = StringHelper.ReturnNumStr(desc, 1, 13);

                        sbr.AppendFormat("<div class=\"item\">");
                        
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle),title);
                        }

                        sbr.AppendFormat("<img alt=\"{1}\" src=\"{0}\">",imgurl,title);
                        sbr.AppendFormat("<dl>");
                        sbr.AppendFormat("<dt>{0}</dt>",show_title);
                        sbr.AppendFormat("<dd>{0}</dd>",desc);
                        sbr.AppendFormat("</dl>");
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</div>");
                        #endregion
                    }
                    else if (cid == 1020)
                    {
                        #region  首页红人专访大图
                        show_title = StringHelper.ReturnNumStr(title, 1, 18);
                        desc = StringHelper.ReturnNumStr(desc, 1, 18);

                        StringBuilder h2sbr = new StringBuilder();
                        StringBuilder h3sbr = new StringBuilder();

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                            h2sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                            h3sbr.AppendFormat("<a {0} title=\"{1}\">{1}</a>", hittpurl, desc);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                            h2sbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            h3sbr.AppendFormat("<a {0} title=\"{1}\">{1}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), desc);
                        }

                        sbr.AppendFormat("<div class=\"line\"></div>");

                        sbr.AppendFormat("<div class=\"tit\">");
                        sbr.AppendFormat("<h2>");
                        sbr.AppendFormat(h2sbr.ToString());
                        sbr.AppendFormat("</h2>");
                        sbr.AppendFormat("<h3>");
                        sbr.AppendFormat(h3sbr.ToString());
                        sbr.AppendFormat("</h3>");
                        sbr.AppendFormat("</div>");
                        #endregion
                    }
                    else if (cid == 1021)
                    {
                        #region  首页红人专访大图右侧一张图
                        show_title = StringHelper.ReturnNumStr(title, 1, 16);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                            sbr.AppendFormat("<p><a {0} title=\"{1}\">{2}</a></p>",hittpurl,title,show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                            sbr.AppendFormat("<p><a {0} title=\"{1}\">{2}</a></p>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                        }
                        
                        #endregion
                    }
                    else if (cid == 1022)
                    {
                        #region  首页红人专访大图下方三张图
                        show_title = StringHelper.ReturnNumStr(title, 1, 12);

                        sbr.AppendFormat("<div class=\"g-pic w240\">");

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} class=\"white\" title=\"{1}\">",hittpurl,title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} class=\"white\" title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }
                        
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">",imgurl,title);
                        sbr.AppendFormat("<em class=\"fcut\"><span>{0}</span></em>",show_title);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</div>");

                        

                        #endregion
                    }
                    else if (cid == 1023)
                    {
                        #region  首页红人专访大图右侧一张图下方图片
                        show_title = StringHelper.ReturnNumStr(title, 1, 16);

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"><span class=\"desc\">{3}</span></a>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle),show_title);
                        }

                        #endregion
                    }
                    else if (cid == 1024)
                    {
                        #region  首页红人写真轮播图
                        show_title = StringHelper.ReturnNumStr(title, 1, 16);
                        desc = StringHelper.ReturnNumStr(desc, 1, 16);

                        sbr.AppendFormat("<div class=\"item\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">",hittpurl,title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }
                        
                        sbr.AppendFormat("<img alt=\"{1}\" src=\"{0}\">",imgurl,title);
                        sbr.AppendFormat("<dl>");
                        sbr.AppendFormat("<dt>{0}</dt>",show_title);
                        sbr.AppendFormat("<dd>{0}</dd>",desc);
                        sbr.AppendFormat("</dl>");
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</div>");

                        

                        #endregion
                    }
                    else if (cid == 1025)
                    {
                        #region  首页红人写真轮播图右侧三张大图
                        show_title = StringHelper.ReturnNumStr(title, 1, 14);
                        desc = StringHelper.ReturnNumStr(desc, 1, 25);

                        StringBuilder sbr2 = new StringBuilder();

                        if (i < 2)
                        {
                            sbr.AppendFormat("<li>");
                            sbr.AppendFormat("<div class=\"item\">");

                            if (openstyle != 1 && openstyle != 2)
                            {
                                sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                            }
                            else
                            {
                                sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                            }

                            sbr.AppendFormat("<img alt=\"{1}\" src=\"{0}\">", imgurl, title);
                            sbr.AppendFormat("<dl>");
                            sbr.AppendFormat("<dt>{0}</dt>", show_title);
                            sbr.AppendFormat("<dd>{0}</dd>", desc);
                            sbr.AppendFormat("</dl>");
                            sbr.AppendFormat("</a>");
                            sbr.AppendFormat("</div>");
                            sbr.AppendFormat("</li>");
                        }
                        else if (i == 2)
                        {
                            sbr2.AppendFormat("<li>");
                            sbr2.AppendFormat("<div class=\"item\">");

                            if (openstyle != 1 && openstyle != 2)
                            {
                                sbr2.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                            }
                            else
                            {
                                sbr2.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                            }

                            sbr2.AppendFormat("<img alt=\"{1}\" src=\"{0}\">", imgurl, title);
                            sbr2.AppendFormat("<dl>");
                            sbr2.AppendFormat("<dt>{0}</dt>", show_title);
                            sbr2.AppendFormat("<dd>{0}</dd>", desc);
                            sbr2.AppendFormat("</dl>");
                            sbr2.AppendFormat("</a>");
                            sbr2.AppendFormat("</div>");
                            sbr2.AppendFormat("</li>");

                            context.Put("sbr2_html",sbr2.ToString());
                        }
                        

                        #endregion
                    }
                    else if (cid == 1026)
                    {
                        #region  首页红人写真轮播图右侧两张小图
                        show_title = StringHelper.ReturnNumStr(title, 1, 5);
                        desc = StringHelper.ReturnNumStr(desc, 1, 10);

                        if(i==0)
                        {
                            sbr.AppendFormat("<div class=\"item\">");
                        }
                        else
                        {
                            sbr.AppendFormat("<div class=\"item fright\">");
                        }

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }
                        sbr.AppendFormat("<img alt=\"{1}\" src=\"{0}\">",imgurl,title);
                        sbr.AppendFormat("<dl>");
                        sbr.AppendFormat("<dt>{0}</dt>",show_title);
                        sbr.AppendFormat("<dd>{0}</dd>",desc);
                        sbr.AppendFormat("</dl>");
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1036)
                    {
                        #region  首页红人微吧二级栏目
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);


                        sbr.AppendFormat("<a {1} title=\"{0}\">{0}</a>", show_title, Return_HttpURL.Return_Url(httpurl, openstyle));

                        if (i < ds.Tables[0].Rows.Count - 1)
                        {
                            sbr.AppendFormat("<span>|</span>");
                        }

                        #endregion
                    }
                    else if (cid == 1027)
                    {
                        #region  首页红人微吧下方三张图
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);

                        sbr.AppendFormat("<div class=\"ads-box\">");


                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" width=\"370\" height=\"110\" border=\"0\">", imgurl, show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\" width=\"370\" height=\"110\" border=\"0\"></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        }

                        sbr.AppendFormat("</div>");
                        #endregion
                    }
                    else if (cid == 1035)
                    {
                        #region  首页红人微吧下方三张图下方微吧
                        show_title = StringHelper.ReturnNumStr(title, 1, 6);
                        desc = StringHelper.ReturnNumStr(desc, 1, 9);

                        sbr.AppendFormat("<div class=\"sbox\">");
                        sbr.AppendFormat("<dl class=\"t\">");
                        sbr.AppendFormat("<dt><h3>{0}</h3></dt>",show_title);
                        sbr.AppendFormat("<dd>");
                        sbr.AppendFormat("<div class=\"btn\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<h5><a class=\"more\" {0} title=\"{1}\">更多 &gt;</a></h5>",hittpurl,title);
                        }
                        else
                        {
                            sbr.AppendFormat("<h5><a class=\"more\" {0} title=\"{1}\">更多 &gt;</a></h5>", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }
                        
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</dd>");
                        sbr.AppendFormat("</dl>");
                        sbr.AppendFormat("<div class=\"cont\"> ");
                        sbr.AppendFormat("<div class=\"shadow\">");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle), title);
                        }
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">",imgurl,desc);
                        sbr.AppendFormat("<h2>{0}</h2>",desc);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("<ul>");

                        ///sywb_1_30

                        try
                        {
                            string ggid = httpurl.Substring(httpurl.LastIndexOf('_') + 1);
                            DataSet wbds = DbHelperSQL.Query("select top 6 id,title from think_sns_db.dbo.THINK_GROUPTOPIC where topicid=0 and islock=0 and groupid in(select id from think_sns_db.dbo.THINK_GROUP where classid in(select id from think_sns_db.dbo.think_type_manage where pid in(" + ggid + ") and isuse=1)) order by id desc ");

                            if (wbds != null && wbds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow wdr in wbds.Tables[0].Rows)
                                {
                                    sbr.AppendFormat("<li><a href=\"http://weiba.ypindapei.com/pic_{0}.html\" title=\"{2}\" target=\"_blank\">{1}</a></li>", wdr["id"].ToString(), StringHelper.ReturnNumStr(wdr["title"].ToString(), 1, 0), StringHelper.ReturnNumStr(wdr["title"].ToString(), 1, 12));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            sbr.AppendFormat("错误：{0}",ex.ToString());
                        }

                        sbr.AppendFormat("</ul>");
                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");

                        #endregion
                    }
                    else if (cid == 1028)
                    {
                        #region  首页红人微吧轮播图
                        show_title = StringHelper.ReturnNumStr(title, 1, 20);
                        width_height = "height=\"250\" width=\"320\" border=\"0\"";

                        StringBuilder weiba_article_one = new StringBuilder();
                        

                        sbr.AppendFormat("<div class=\"item\">");
                        sbr.AppendFormat("<div class=\"g-pic\">");

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\" {4}><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, hittpurl, title, width_height);

                            //if (i == 0)
                            //{
                            //    weiba_article_one.AppendFormat("<li><a {0} class=\"\" title=\"{1}\">{2}</a></li>", hittpurl, title, show_title);
                            //}
                            //else
                            //{
                                weiba_article.AppendFormat("<li><a {0} class=\"\" title=\"{1}\">{2}</a></li>", hittpurl, title, show_title);
                            //}
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} class=\"white\" title=\"{3}\"><img src=\"{0}\" alt=\"{3}\" {4}><em class=\"fcut\"><span>{1}</span></em></a>", imgurl, show_title, Return_HttpURL.Return_Url(httpurl, openstyle), title, width_height);
                            //if (i == 0)
                            //{
                            //    weiba_article_one.AppendFormat("<li><a {0} class=\"\" title=\"{1}\">{2}</a></li>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            //}
                            //else
                            //{
                                weiba_article.AppendFormat("<li><a {0} class=\"\" title=\"{1}\">{2}</a></li>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                            //}
                        }

                        sbr.AppendFormat("</div>");
                        sbr.AppendFormat("</div>");

                        //if (i == 0)
                        //{
                        //    context.Put("weiba_article_one", weiba_article_one.ToString());
                        //}
                        #endregion
                    }
                    else if (cid == 1037)
                    {
                        #region  首页红人微吧二级栏目
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);


                        sbr.AppendFormat("<a {1} title=\"{0}\">{0}</a>", show_title, Return_HttpURL.Return_Url(httpurl, openstyle));

                        if (i < ds.Tables[0].Rows.Count - 1)
                        {
                            sbr.AppendFormat("<span>|</span>");
                        }

                        #endregion
                    }
                    else if (cid == 1030)
                    {
                        #region  首页红人爱品轮播图
                        show_title = StringHelper.ReturnNumStr(title, 1, 30);

                        StringBuilder ipsbr = new StringBuilder();

                        sbr.AppendFormat("<dl class=\"item\">");
                        sbr.AppendFormat("<dt>");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">", imgurl, title);
                            ipsbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", hittpurl, title,show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><img src=\"{0}\" alt=\"{1}\"></a>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                            ipsbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                        }
                        sbr.AppendFormat("</dt>");
                        sbr.AppendFormat("<dd>{0}</dd>",ipsbr.ToString());
                        sbr.AppendFormat("</dl>");

                        #endregion
                    }
                    else if (cid == 1031)
                    {
                        #region  首页红人爱品轮播图右侧六张图
                        show_title = StringHelper.ReturnNumStr(title, 1, 12);

                        StringBuilder ipsbr = new StringBuilder();


                        sbr.AppendFormat("<dl class=\"item\">");
                        sbr.AppendFormat("<dt>");
                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, title);
                            ipsbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", hittpurl, title, show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", Return_HttpURL.Return_Url(httpurl, openstyle),title);
                            ipsbr.AppendFormat("<a {0} title=\"{1}\">{2}</a>", Return_HttpURL.Return_Url(httpurl, openstyle), title, show_title);
                        }
                        sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\">",imgurl,title);
                        sbr.AppendFormat("</a>");
                        sbr.AppendFormat("</dt>");
                        sbr.AppendFormat("<dd>{0}</dd>",ipsbr);
                        sbr.AppendFormat("</dl>");

                        

                        #endregion
                    }
                    else if (cid == 1032)
                    {
                        #region  首页人物推荐上方广告图
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);
                        width_height = "height=\"128\" width=\"128\"";

                        sbr.AppendFormat("<div class=\"nn2\" style=\"position: relative;\">");

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

                        #endregion
                    }
                    else if (cid == 1033)
                    {
                        #region  首页人物推荐
                        show_title = StringHelper.ReturnNumStr(title, 1, 4);

                        sbr.AppendFormat("<li>");
						

                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><span><img src=\"{0}\" alt=\"{1}\"></span>", imgurl, title,hittpurl);
                            sbr.AppendFormat("<h2>{0}</h2>",show_title);
                            sbr.AppendFormat("<em>TA的推荐</em>");
                            sbr.AppendFormat("</a>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        }
                        else
                        {
                            sbr.AppendFormat("<a {2} title=\"{1}\"><span><img src=\"{0}\" alt=\"{1}\"></span>", imgurl, title, Return_HttpURL.Return_Url(httpurl, openstyle));
                            sbr.AppendFormat("<h2>{0}</h2>", show_title);
                            sbr.AppendFormat("<em>TA的推荐</em>");
                            sbr.AppendFormat("</a>");
                        }
                        sbr.AppendFormat("</li>");
                        #endregion
                    }
                    else if (cid == 1034)
                    {
                        #region  首页聚合阅读下方微吧
                        show_title = StringHelper.ReturnNumStr(title, 1, 0);


                        if (openstyle != 1 && openstyle != 2)
                        {
                            sbr.AppendFormat("<a {0} title=\"{1}\">", hittpurl, show_title);
                        }
                        else
                        {
                            sbr.AppendFormat("<a {1} title=\"{0}\">{0}</a>", show_title, Return_HttpURL.Return_Url(httpurl, openstyle));
                        }
                        #endregion
                    }
                    

                }

                if (cid == 1008)
                {
                    context.Put("head_hrzx_lb_three_article_title", html.ToString());
                }

                if (cid == 1028)
                {
                    context.Put("weiba_article", weiba_article.ToString());
                }
            }


            return sbr.ToString();

        }


        /// <summary>
        /// 获取聚合阅读（红人汇）
        /// </summary>
        /// <param name="type">1：红人，2：资讯，3：潮范</param>
        private string Get_JHYD(int type)
        {
            string result="";

            //红人
            string[] hren = { "Papi酱:45", "王思聪:49", "宋赫伦:1", "天佑:83", "穆雅斓:68", "小九:41", "魏道道:9", "游乐儿:22", "马启光:6", "黄灿灿:54", "破破:56", "李明霖:46", "张大奕:63", "黄景瑜:79", "南笙:70" };

            //资讯
            string[] zixun = { "网红", "主播", "纯吐槽", "身材/性感", "美女", "男神", "校花", "电影", "整容", "PS", "颜值", "国民", "耽美", "火爆", "演员" };

            //潮范
            string[] chaofan = { "唱吧", "说唱/rap", " 原创", "作品", "恶搞/搞怪", "奇葩/搞笑", "MV", "男神", "性感", "写真", "女神", "鲜肉", "模特", "娱乐", "主播" };

            StringBuilder jhyd = new StringBuilder();

            string hturl = ImgHelper.GetCofigHttpUrl()+"hren/";

            if (type == 1)
            {
                for (int i = 0; i < hren.Length; i++)
                {
                    string[] skey = hren[i].Split(':');
                    string search_url =hturl+ "search_1_" + skey[0] + "_hren" ;
                    jhyd.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", search_url, skey[0]);
                }

                result = jhyd.ToString();
            }
            else if (type == 2)
            {
                for (int i = 0; i < zixun.Length; i++)
                {
                    string search_url = hturl + "search_1_" + zixun[i] + "_zx";
                    jhyd.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", search_url, zixun[i]);
                }

                result = jhyd.ToString();
            }
            else if (type == 3)
            {
                for (int i = 0; i < chaofan.Length; i++)
                {
                    string search_url = hturl + "search_1_" + chaofan[i] + "_cf";
                    jhyd.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", search_url, chaofan[i]);
                }

                result = jhyd.ToString();
            }

            return result;
        }


        /// <summary>
        /// 今日热点、红人专访、红人作品
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string Get_News_Hren(int type,int top)
        {
            sbr.Clear();

            string result = "";

            if (type == 1)
            {
                result=ArticleHelper.Get_New_ZX(top, "", 16);
                result = result.Replace("<li>","<div class=\"fcut\">");
                result = result.Replace("</li>", "</div>");
            }
            else if (type == 2)
            {
                result = ArticleHelper.Get_New_ZX(top, "(types in(1) or types_pid in(1))", 22);
            }
            else if (type == 3)
            {
                result = ArticleHelper.Get_New_ZX(top, "(types in(3) or types_pid in(3))", 26);
            }

            sbr.AppendFormat(result);

            return sbr.ToString();

        }

        /// <summary>
        /// 获取二级栏目，红人资讯、红人作品、红人写真
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string Get_Son_Type(int type)
        {
            sbr.Clear();
            BLL.HRENH_ARTICLE_TYPE type_bll = new BLL.HRENH_ARTICLE_TYPE();

            int tpid = type;

            ds = type_bll.GetList(" pid=" + tpid + " and state=1 ");

            string type_url_name = "";

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                int sid = 0;
                string sname = "";
                string surl = "";
                int cnt = 0;

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sid = Convert.ToInt32(dr["id"].ToString());
                    sname = dr["typename"].ToString();

                    type_url_name = ArticleHelper.Get_Nav_Type_Name(tpid);

                    surl = type_url_name + "_1_" + sid;

                    sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\">{1}</a>", surl, sname);

                    if (cnt<ds.Tables[0].Rows.Count-1)
                    {
                        sbr.AppendFormat("<span>|</span>");
                    }

                    cnt++;
                }
            }

            return sbr.ToString();

        }

    }
}