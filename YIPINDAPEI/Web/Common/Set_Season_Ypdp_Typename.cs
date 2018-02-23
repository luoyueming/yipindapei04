using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.DBUtility;

namespace NewXzc.Web.Common
{
    public class Set_Season_Ypdp_Typename
    {
        public static string Get_Typename_list(string typeidlist)
        {
            string result = "冬季";//冬季&nbsp;+&nbsp;短腿&nbsp;+&nbsp;韩系

            string typename = "";

            string sql = "";

            string sel_type_id_ypdp = "404";

            //特定人群  404
            try
            {
                sql = "select top 1 typename from hrenh_article_type where id in(" + typeidlist + ") and pid in(" + sel_type_id_ypdp + ")";

                typename = DbHelperSQL.GetSingle(sql).ToString();

                if (typename != "")
                {
                    result += "&nbsp;+&nbsp;"+typename+"";
                }
            }
            catch (Exception ex)
            {

            }

            //气质风格  403
            sel_type_id_ypdp = "403";
            try
            {
                sql = "select top 1 typename from hrenh_article_type where id in(" + typeidlist + ") and pid in(" + sel_type_id_ypdp + ")";

                typename = DbHelperSQL.GetSingle(sql).ToString();

                if (typename != "")
                {
                    result += "&nbsp;+&nbsp;" + typename;
                }
            }
            catch (Exception ex)
            {

            }

            result += "&nbsp;&nbsp;搭配";

            return result;
        }
    }
}