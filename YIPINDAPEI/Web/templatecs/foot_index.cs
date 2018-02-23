using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using NewXzc.Web.Common.uhelper;
using NewXzc.Common;
using NewXzc.Web.Common;

namespace NewXzc.Web.templatecs
{
    public class foot_index:Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            #region  加载首页友情链接

            StringBuilder sbr_foot = new StringBuilder();
            DataSet ds_foot = null;
            BLL.RED_RECOMMEND recommend_bll_foot = new BLL.RED_RECOMMEND();

            ds_foot = recommend_bll_foot.GetList_NewIndex(0, " pageid=2000 and cid=1001 ");

            if (ds_foot != null && ds_foot.Tables[0].Rows.Count > 0)
            {
                sbr_foot.AppendFormat("<div class=\"friendLink\">");
                sbr_foot.AppendFormat("<div class=\"box\">");
                sbr_foot.AppendFormat("<div>");
                sbr_foot.AppendFormat("<h2>友情链接</h2>");

                for (int i = 0; i < ds_foot.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds_foot.Tables[0].Rows[i];

                    string title = StringHelper.ReturnNumStr(dr["v1"].ToString(), 3, 0);
                    string httpurl = String_Manage.Return_Str(dr["url"].ToString(), "");
                    //链接打开方式，1：在本页面打，2：在新页面打开
                    int openstyle = String_Manage.Return_Int(dr["v4"].ToString(), 0);

                    sbr_foot.AppendFormat("<a {1} title=\"{0}\">{0}</a>", title, Return_HttpURL.Return_Url(httpurl, openstyle));
                    if (i < ds_foot.Tables[0].Rows.Count-1)
                    {
                        sbr_foot.AppendFormat("<i>|</i>");
                    }

                }

                sbr_foot.AppendFormat("</div>");
			    sbr_foot.AppendFormat("</div>");
		        sbr_foot.AppendFormat("</div>");

            }

            context.Put("foot_link", sbr_foot.ToString());
            #endregion

        }
    }
}