using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using NewXzc.DBUtility;
using NewXzc.Web.Common;
using NewXzc.Common;

namespace NewXzc.Web.templatecs
{
    public class head_index:Base.BasePage
    {
        Model.RED_USER user_model = new Model.RED_USER();
        BLL.RED_USER user_bll = new BLL.RED_USER();

        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);
        }

        /// <summary>
        /// 加载首页头部
        /// </summary>
        /// <param name="context"></param>
        public void Init_Head(NVelocity.VelocityContext context)
        {
            //context.Put("title", "网红，网络红人，网红经济，衣品搭配");
            //context.Put("keywords", "网红,网络红人，网红经济,网红直播,网红营销，红人，衣品搭配");
            //context.Put("description", "衣品搭配是中国首家网络红人门户,有丰富的网红资讯是网络红人综合业务报道和网红推广的平台，可以满足广告主的投放诉求和网红的变现诉求实现网红经济化，提供红薯直播平台让主播们的分成更多，还能成就不少网红的明星梦。");

            context.Put("title", "穿衣搭配网|女装搭配|服装搭配|衣服搭配-衣品搭配网");
            context.Put("keywords", "女装,穿衣搭配,服装搭配,服饰搭配,韩国服饰,穿衣搭配网");
            context.Put("description", "衣品搭配网是最专业最权威的服装搭配网站。除了展示各类女装及配饰还有最专业最潮流的穿衣搭配指南，让时尚变的触手可及！");


            ////加载头部导航
            //context.Put("nav_list", Get_Top_Nav(context));

            //获取微信登录信息
            string wcode = String_Manage.Return_Request_Str("code");

            if (wcode != "")
            {
                context.Put("redirecturl","/userlogin/wechat_return_url.aspx?code="+wcode);
            }

            //Save_Img_Http.Save_Img_WebRequest("http://wx.qlogo.cn/mmopen/Q3auHgzwzM6Gv0vqjANHBUX7npN7tQGicuXxIkelKa3ibP8P5EBOPcb9yqougxLicZqcL9bwmo9M56QM8ibDPgXHia7M7kKEt7CNKK4PAPjNHiaxw/0", "jpg", 0);

            //string pwd = NewXzc.Common.DEncrypt.DESEncrypt.Encrypt("608");//给密码加密
        }

        /// <summary>
        /// 加载头部导航
        /// </summary>
        /// <param name="context"></param>
        private string Get_Top_Nav(NVelocity.VelocityContext context)
        {
            StringBuilder weiba_sbr = new StringBuilder();

            int topnum = 3;//5

            #region  加载导航名称
            string sql = "select top "+topnum+" id,pid,typename from hrenh_article_type where state=1 order by id asc";//旧版本
            sql = "select * from (";
            sql += "select top " + topnum + " id,pid,typename from hrenh_article_type where state=1 ";
            sql += "union ";
            //sql += "select id,pid,typename from hrenh_article_type where (id=35 or pid=35) and state=1";
            sql += "select id,pid,typename from hrenh_article_type where (id=44 or pid=44) and state=1";
            sql += ") a ";

            DataSet type_ds = DbHelperSQL.Query(sql);
            StringBuilder type_sbr = new StringBuilder();

            //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f20\">{1}</a>", "http://www.ypindapei.com", "首页");
            //type_sbr.AppendFormat("<span></span>");

            if (type_ds != null && type_ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < type_ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = type_ds.Tables[0].Rows[i];

                    int typeids = String_Manage.Return_Int(dr["id"].ToString(), 0);
                    int pid = String_Manage.Return_Int(dr["pid"].ToString(), 0);

                    string nav_http_url = "/article_" + typeids;
                    string nav_http_name = dr["typename"].ToString();

                    nav_http_url = "/" + ArticleHelper.Get_Nav_Type_Name(typeids);
                    int curtid = Convert.ToInt32(dr["id"].ToString());

                    if (i > topnum)
                    {
                        nav_http_url += "_1_" + curtid;
                    }


                    if (i < topnum)
                    {
                        nav_http_name = nav_http_name.Replace("红人","");
                    }

                    if (i != topnum)
                    {
                        //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18\">{1}</a>", nav_http_url, nav_http_name);

                        if (pid == 0 && typeids != 35)
                        {
                            type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18 active\">{1}</a>", nav_http_url, nav_http_name);
                        }
                        else
                        {
                            if (pid == 35)
                            {
                                switch (typeids)
                                {
                                    case 36:
                                        nav_http_url = "http://weiba.ypindapei.com/found_148";
                                        break;
                                    case 37:
                                        nav_http_url = "http://weiba.ypindapei.com/found_143";
                                        break;
                                    case 38:
                                        nav_http_url = "http://weiba.ypindapei.com/found_142";
                                        break;
                                    case 39:
                                        nav_http_url = "http://weiba.ypindapei.com/found_129";
                                        break;
                                }

                                weiba_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18\" target=\"_blank\">{1}</a>", nav_http_url, nav_http_name);
                            }
                            else
                            {
                                type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f18\">{1}</a>", nav_http_url, nav_http_name);
                            }
                        }

                    }

                    //if (i == topnum)
                    //{

                    //    nav_http_url = "http://weiba.ypindapei.com";

                    //    weiba_sbr.AppendFormat("<span></span>");
                    //    weiba_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f22\" target=\"_blank\">{1}</a>", nav_http_url, nav_http_name);
                    //}

                }
            }
            //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f20\" target=\"_blank\">{1}</a>", "http://weiba.ypindapei.com", "红人微吧");
            //type_sbr.AppendFormat("<span></span>");
            //type_sbr.AppendFormat("<a href=\"{0}\" title=\"{1}\" class=\"f20\">{1}</a>", "/hongren", "网络红人");

            
            #endregion

            context.Put("weiba",weiba_sbr.ToString());

            return type_sbr.ToString();
        }



    }
}