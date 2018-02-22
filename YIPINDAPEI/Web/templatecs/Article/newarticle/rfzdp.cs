using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Web.Common;
using NewXzc.DBUtility;
using System.Data;

namespace NewXzc.Web.templatecs.Article.newarticle
{
    public class rfzdp:Base.BasePage
    {
        public override void Page_Load(ref NVelocity.VelocityContext context)
        {
            base.Page_Load(ref context);

            //获取文章详情
            GetDetail(context);
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="context"></param>
        private void GetDetail(NVelocity.VelocityContext context)
        {

            int articleid = String_Manage.Return_Request_Int("id", 0);

            string ishave_rcode = "0";

            string result = "ok";

            if (articleid == 0)
            {
                context.Put("redirecturl", "/404");
            }
            else
            {
                try
                {
                    DataSet ds = DbHelperSQL.Query("select top 1 New_Zixun_Tbk_Url from hrenh_article a where id=" + articleid + " ");// and a.pub_state=0

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = ds.Tables[0].Rows[i];

                            #region  文章详情的信息

                            string New_Zixun_Tbk_Url = dr["New_Zixun_Tbk_Url"].ToString();

                            #endregion

                            #region  加载文章详情

                            context.Put("redirecturl",New_Zixun_Tbk_Url);

                            #endregion
                        }
                    }
                    else
                    {
                        context.Put("redirecturl", "/404");
                    }
                }
                catch (Exception ex)
                {
                    context.Put("redirecturl", "/404");
                }

            }

        }
    }
}