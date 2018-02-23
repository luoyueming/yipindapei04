using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using NewXzc.DBUtility;
using Maticsoft.Web.Common;

namespace NewXzc.Web.Common
{
    public class PageClass
    {
        public PageClass()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 验证当前字符串是否为数字
        /// </summary>
        /// <param name="num">需要验证的字符串</param>
        /// <returns></returns>
        public bool IsNum(string num)
        {
            Regex reg = new Regex("^-?\\d+$");
            bool f = false;
            if (reg.IsMatch(num))
            {
                f = true;
            }
            return f;
        }

        /// <summary>
        /// 宋明亮-----------------------分页方法（新加Field：需要被显示的字段名称，如：,count(id),max(id)），最终返回结果（ds.tables[0].Rows[0][0]：总的记录数，ds.tables[1].Rows[0][0]：总的页数，ds.tables[2]：查询后的数据列表）
        /// </summary>
        /// <param name="key">根据字段计算数据量，如*</param>
        /// <param name="Order">排序字段及排序方式</param>
        /// <param name="tabName">需要查询的表的名字</param>
        /// <param name="Where">查询条件，需要加and</param>
        /// <param name="Page">当前页，从0开始</param>
        /// <param name="Val">每页显示数量</param>
        /// <param name="Field">需要被显示的字段名称，如：id,addtime</param>
        /// <returns></returns>
        public DataSet TagList_New(string key, string Order, string tabName, string Where, int Page, int Val, string Field)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Key", SqlDbType.VarChar,50),
                    new SqlParameter("@Col", SqlDbType.VarChar,50),
                    new SqlParameter("@Cols", SqlDbType.VarChar,2000),
                    new SqlParameter("@TabName", SqlDbType.VarChar,2000),
                    new SqlParameter("@Where", SqlDbType.VarChar,2000),
                    new SqlParameter("@Page", SqlDbType.Int),
                    new SqlParameter("@Val", SqlDbType.Int),
                    new SqlParameter("@Field", SqlDbType.VarChar,2000)
                                        };
            parameters[0].Value = key;
            parameters[1].Value = Order;
            parameters[2].Value = Field;
            parameters[3].Value = tabName;
            parameters[4].Value = Where;
            parameters[5].Value = Page;
            parameters[6].Value = Val;
            parameters[7].Value = "*";

            string _connectionString = ConfigurationManager.ConnectionStrings["XzcPro"].ToString();

            DataSet ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "PROC_Page_More_New", parameters);
            return ds;
        }



        /// <summary>
        /// 获取分页数据集合
        /// </summary>
        /// <param name="key">获取总的数据数目的查询的关键字，可为*</param>
        /// <param name="Order">排序的关键字及方式,如：id desc</param>
        /// <param name="colums">查询更多的列</param>
        /// <param name="tabName">需要查询的表的名称</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="Pages">当前页，以0开始</param>
        /// <param name="Val">每页显示多少条记录</param>
        /// <param name="fields">在查询外需要查询的字段名称</param>
        /// <returns></returns>
        public DataSet GetList(string key, string Order, string colums, string tabName, string strwhere, int Pages, int Val, string fields)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Key", SqlDbType.VarChar,50),
                    new SqlParameter("@Col", SqlDbType.VarChar,100),
                    new SqlParameter("@Cols", SqlDbType.VarChar,2000),
                    new SqlParameter("@TabName", SqlDbType.VarChar,2000),
                    new SqlParameter("@Where", SqlDbType.VarChar,2000),
                    new SqlParameter("@Page", SqlDbType.Int,4),
                    new SqlParameter("@Val", SqlDbType.Int),
                    new SqlParameter("@Field", SqlDbType.NVarChar,2000)};
            parameters[0].Value = key;
            parameters[1].Value = Order;
            parameters[2].Value = colums;
            parameters[3].Value = tabName;
            parameters[4].Value = strwhere;
            parameters[5].Value = Pages;
            parameters[6].Value = Val;
            parameters[7].Value = fields;

            string _connectionString = PubConstant.ConnectionString;

            DataSet ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "PROC_Page_More_New", parameters);
            return ds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Order"></param>
        /// <param name="tabName"></param>
        /// <param name="Where"></param>
        /// <param name="Page">当前页</param>
        /// <param name="Val">每页显示多少条数据</param>
        /// <returns></returns>
        public DataSet TagList(string key, string Order, string tabName, string Where, int Page, int Val)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@Key", SqlDbType.VarChar,50),
                    new SqlParameter("@Col", SqlDbType.VarChar,50),
                    new SqlParameter("@Cols", SqlDbType.VarChar,100),
                    new SqlParameter("@TabName", SqlDbType.VarChar,100),
                    new SqlParameter("@Where", SqlDbType.VarChar,1000),
                    new SqlParameter("@Page", SqlDbType.Int),
                    new SqlParameter("@Val", SqlDbType.Int)};
            parameters[0].Value = key;
            parameters[1].Value = Order;
            parameters[2].Value = "*";
            parameters[3].Value = tabName;
            parameters[4].Value = Where;
            parameters[5].Value = Page;
            parameters[6].Value = Val;

            //ConfigurationManager.AppSettings["SQLConnString1"];
            //DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
            //                                      CommandType.StoredProcedure, "pr_Page", parameters);

            //DataSet ds = SqlHelper.ExecuteDataset(ConfigurationManager.AppSettings["ConnectionString"],
            //                                      CommandType.StoredProcedure, "pr_Page", parameters);




            string _connectionString = ConfigurationManager.ConnectionStrings["XzcPro"].ToString();

            DataSet ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "pr_Page", parameters);
            return ds;
        }

        public static string NumberToChinese(string numberStr)
        {
            string numStr = "1234567890";
            string chineseStr = "一二三四五六七八九十";
            char[] c = numberStr.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                int index = numStr.IndexOf(c[i]);
                if (index != -1)
                    c[i] = chineseStr.ToCharArray()[index];
            }
            numStr = null;
            chineseStr = null;
            return new string(c);
        }

        #region 分页存储过程（李彪）
        /// <summary>
        /// 分页存储过程（李彪）
        /// </summary>
        /// <param name="strTable">表名</param>
        /// <param name="strColumn">按该列来进行排序分页</param>
        /// <param name="intOrder">排序,0-顺序,1-倒序</param>
        /// <param name="strColumnlist">要查询出的字段列表,*表示全部字段</param>
        /// <param name="intPageSize">每页记录数</param>
        /// <param name="intPageNum">指定页</param>
        /// <param name="strWhere">查询条件</param>
        /// <param name="intPageCount">总页数</param>
        /// <returns></returns>
        public static DataSet Paging(string strTable, string strColumn, int intOrder, string strColumnlist, int intPageSize, int intPageNum, string strWhere, out int intPageCount)
        {
            SqlParameter[] parm = new SqlParameter[] { 
                new SqlParameter("@strTable",SqlDbType.NVarChar),
                new SqlParameter("@strColumn",SqlDbType.NVarChar),
                new SqlParameter("@intOrder",SqlDbType.Int),
                new SqlParameter("@strColumnlist",SqlDbType.NVarChar),
                new SqlParameter("@intPageSize",SqlDbType.Int),
                new SqlParameter("@intPageNum",SqlDbType.Int),
                new SqlParameter("@strWhere",SqlDbType.NVarChar),
                new SqlParameter("@intPageCount",SqlDbType.Int)
            };
            parm[0].Value = strTable;
            parm[1].Value = strColumn;
            parm[2].Value = intOrder;
            parm[3].Value = strColumnlist;
            parm[4].Value = intPageSize;
            parm[5].Value = intPageNum;
            parm[6].Value = strWhere;
            parm[7].Direction = ParameterDirection.Output;
            DataSet ds= DbHelperSQL.RunProcedure("Paging", parm, "table");

            intPageCount = Convert.ToInt32(parm[7].Value);

            return ds;
        }
        #endregion
    }
}