using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.DBUtility;
using System.Data;
using System.Text;

namespace NewXzc.Web.Common
{
    public class TypeHelper
    {

        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="id">当前类型ID，实现被选中状态</param>
        /// <param name="pid">需要被查询的子级列表的父级类型ID</param>
        /// <param name="selid">当前被显示的Select下拉列表框的ID</param>
        /// <returns></returns>
        public static string Get_Tyle_List(int id, int pid, string selid)
        {
            Model.HRENH_ARTICLE_TYPE party_model = new Model.HRENH_ARTICLE_TYPE();
            BLL.HRENH_ARTICLE_TYPE party_bll = new BLL.HRENH_ARTICLE_TYPE();

            StringBuilder sbr = new StringBuilder();
            DataSet ds = party_bll.GetList(" pid=" + pid + " and type_parent=0 ");

            sbr.AppendFormat("<select id=\"{0}\">", selid);
            sbr.AppendFormat("<option value=\"-1\">请选择</option>");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    DataRow dr = ds.Tables[0].Rows[k];
                    int cid = Convert.ToInt32(dr["id"].ToString());
                    string tname = dr["typename"].ToString();

                    if (cid == id)
                    {
                        sbr.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", cid, tname);
                    }
                    else
                    {
                        sbr.AppendFormat("<option value=\"{0}\">{1}</option>", cid, tname);
                    }
                }


            }

            sbr.AppendFormat("</select>");

            return sbr.ToString();
        }



        /// <summary>
        /// 获取类型列表
        /// </summary>
        /// <param name="id">当前类型ID，实现被选中状态</param>
        /// <param name="pid">需要被查询的子级列表的父级类型ID</param>
        /// <param name="selid">当前被显示的Select下拉列表框的ID</param>
        /// <param name="type">0：父级，1：子级</param>
        /// <returns></returns>
        public static string Get_Tyle_List(int id, int pid, string selid, int type)
        {
            Model.HRENH_ARTICLE_TYPE party_model = new Model.HRENH_ARTICLE_TYPE();
            BLL.HRENH_ARTICLE_TYPE party_bll = new BLL.HRENH_ARTICLE_TYPE();

            StringBuilder sbr = new StringBuilder();
            DataSet ds = null;

            if (type == 0)
            {
                ds = party_bll.GetList(" pid=" + pid + " and id in(2,5) ");
            }
            else
            {
                ds = party_bll.GetList(" pid=" + pid + " ");
            }

            sbr.AppendFormat("<select id=\"{0}\">", selid);
            sbr.AppendFormat("<option value=\"-1\">请选择</option>");

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    DataRow dr = ds.Tables[0].Rows[k];
                    int cid = Convert.ToInt32(dr["id"].ToString());
                    string tname = dr["typename"].ToString();

                    if (cid == id)
                    {
                        sbr.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", cid, tname);
                    }
                    else
                    {
                        sbr.AppendFormat("<option value=\"{0}\">{1}</option>", cid, tname);
                    }
                }


            }

            sbr.AppendFormat("</select>");

            return sbr.ToString();
        }


        /// <summary>
        /// 获取类型或平台的名字
        /// </summary>
        /// <param name="idlist">类型或平台的ID</param>
        /// <returns></returns>
        public static string Get_type_plat_name(string idlist)
        {
            string result = "";

            if (idlist != "")
            {
                string sql = "select typename from HRENH_HATTR_TYPE where id in(" + idlist + ")";
                DataSet ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        result += "," + dr["typename"].ToString();
                    }
                }

                if (result != "")
                {
                    result = result.Substring(1);
                }
            }

            return result;
        }


        /// <summary>
        /// 获取类型列表，新资讯类型
        /// </summary>
        /// <param name="id">当前类型ID，实现被选中状态</param>
        /// <param name="pid">需要被查询的子级列表的父级类型ID</param>
        /// <param name="selid">当前被显示的Select下拉列表框的ID</param>
        /// <param name="selid">0：默认，1：店铺 </param>
        /// <returns></returns>
        public static string Get_Tyle_List_New_Zixun(int id, int pid, string selid, int type_parent = 0)
        {
            Model.HRENH_ARTICLE_TYPE party_model = new Model.HRENH_ARTICLE_TYPE();
            BLL.HRENH_ARTICLE_TYPE party_bll = new BLL.HRENH_ARTICLE_TYPE();

            StringBuilder sbr = new StringBuilder();

            string where = " pid=" + pid + " ";
            where += "and type_parent=" + type_parent + "";

            DataSet ds = party_bll.GetList(where);


            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                sbr.AppendFormat("&nbsp;<select id=\"{0}\">", selid);
                sbr.AppendFormat("<option value=\"0\">请选择</option>");

                for (int k = 0; k < ds.Tables[0].Rows.Count; k++)
                {
                    DataRow dr = ds.Tables[0].Rows[k];
                    int cid = Convert.ToInt32(dr["id"].ToString());
                    string tname = dr["typename"].ToString();

                    if (cid == id)
                    {
                        sbr.AppendFormat("<option value=\"{0}\" selected=\"selected\">{1}</option>", cid, tname);
                    }
                    else
                    {
                        sbr.AppendFormat("<option value=\"{0}\">{1}</option>", cid, tname);
                    }
                }

                sbr.AppendFormat("</select>");
            }



            return sbr.ToString();
        }


        /// <summary>
        /// 获取类型的名字
        /// </summary>
        /// <param name="idlist">类型或平台的ID</param>
        /// <returns></returns>
        public static string Get_type_New_Zixun__name(string idlist)
        {
            string result = "";

            if (idlist != "")
            {
                string sql = "select typename from HRENH_ARTICLE_TYPE where id in(" + idlist + ")";
                DataSet ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        result += "," + dr["typename"].ToString();
                    }
                }

                if (result != "")
                {
                    result = result.Substring(1);
                }
            }

            return result;
        }

    }
}