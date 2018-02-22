using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using NewXzc.DBUtility;
using NewXzc.Web.Common;
using NewXzc.Common;

namespace NewXzc.Web.templatecs
{
    public class Error404 : Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            HttpContext.Current.Response.Status = "404 Not Found";

            GetList(context);
        }



        /// <summary>
        /// 获取议会活动列表
        /// </summary>
        /// <param name="context"></param>
        private void GetList(NVelocity.VelocityContext context)
        {
            StringBuilder sbr = new StringBuilder();

            int pagesize = 2;
            int type = 1;

            string where = " (types=" + type + " or types_pid=" + type + ") and isend=0 and a.pub_state=0 ";

            DataSet ds = DbHelperSQL.Query("select top "+pagesize+" id,title,isimg,img_url,contents,(isnull(read_cnt,0)+FALSH_READ_CNT) as read_cnt,edittime from hrenh_article a where"+where);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];

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

                    string article_url = "href=\"/article_detail_" + article_id + "\" target=\"_blank\" title=\"" + article_title + "\"";

                    article_url = "href=\"/zixun/detail_" + article_id + ".html\" target=\"_blank\" title=\"" + article_title + "\"";

                    #endregion

                    #region  加载文章列表


                    sbr.AppendFormat("<dl class=\"mar0 listone\">");
                    sbr.AppendFormat("<dt style=\"float:left;\">");
                    sbr.AppendFormat("<a {0}>",article_url);
                    sbr.AppendFormat("<img src=\"{0}\" alt=\"{1}\" width=\"300\" height=\"200\">",article_img,article_title);
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("</dt>");
                    sbr.AppendFormat("<dd style=\"float: left;padding-left:19px; \">");
                    sbr.AppendFormat("<h3>");
                    sbr.AppendFormat("<a {0} style=\" color: #333;font-size: 20px;font-weight: 900; \">{1}</a>",article_url,show_article_title);
                    sbr.AppendFormat("</h3>");
                    sbr.AppendFormat("<h3 class=\"hh2\">");
                    sbr.AppendFormat("<p class=\"content\"\">");
                    sbr.AppendFormat("<a {0}>{1}",article_url,article_contents);
                    sbr.AppendFormat("</a>");
                    sbr.AppendFormat("</p>");
                    sbr.AppendFormat("<p class=\"main-share\">");
                    sbr.AppendFormat("<span class=\"clearfix\" style=\"margin-top:4px;\">");
                    sbr.AppendFormat("<span class=\"list-time\">发布时间：{0}</span>", article_addtime);
                    sbr.AppendFormat("<span class=\"list-view-data\">");
                    sbr.AppendFormat("</span>");
                    sbr.AppendFormat("</span>");
                    sbr.AppendFormat("</p>");
                    sbr.AppendFormat("</h3>");
                    sbr.AppendFormat("</dd>");
                    sbr.AppendFormat("</dl>");


                    #endregion
                }
            }
            else
            {
                sbr.Append("<div class=\"nodata\">没有找到相关数据</div>");
            }

            context.Put("list", sbr.ToString());

        }

    }
}