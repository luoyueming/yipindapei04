using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace NewXzc.Web.Common
{
    public class CPA_Helper
    {
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
        public static int Add_CPA_Info(string ip, int article_id, int ad_id, int ad_type)
        {
            SqlParameter[] parameters = {
					new SqlParameter("@regip", SqlDbType.VarChar,50),
                    new SqlParameter("@article_id", SqlDbType.Int),
                    new SqlParameter("@ad_id", SqlDbType.Int),
                    new SqlParameter("@ad_type", SqlDbType.Int)
                                        };
            parameters[0].Value = ip;
            parameters[1].Value = article_id;
            parameters[2].Value = ad_id;
            parameters[3].Value = ad_type;

            string _connectionString = ConfigurationManager.ConnectionStrings["XzcPro"].ToString();

            int cnt = 0;

            try
            {
                cnt = SqlHelper.ExecuteNonQuery(_connectionString, CommandType.StoredProcedure, "add_cpa_register_info", parameters);
            }
            catch (Exception ex)
            {

            }
            //DataSet ds = SqlHelper.ExecuteDataset(_connectionString, CommandType.StoredProcedure, "add_cpa_register_info", parameters);

            return cnt;
        }
    }
}