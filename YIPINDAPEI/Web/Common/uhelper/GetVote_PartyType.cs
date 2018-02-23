using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace NewXzc.Web.Common.uhelper
{
    public class GetVote_PartyType
    {
        /// <summary>
        /// 党派类型，1：兄弟党，2：美女党
        /// </summary>
        /// <param name="party_type"></param>
        /// <param name="type">获取数值类型，1：投票数，2：百分比</param>
        /// <returns></returns>
        public static double GetVote_Count(int party_type,int type)
        {
            double cnt = 0;
            string sql = "";
            DataSet ds = null;

            try
            {
                if (type == 1)
                {
                    sql = "select (isnull((select top 1 counts from RED_VOTE_INIT_COUNT where types=@types),0)+isnull((select count(*) from RED_VOTE_RECORD where types=@types),0)) as cnt";
                }
                else
                {
                    sql = "select (isnull((select top 1 counts from RED_VOTE_INIT_COUNT where types=@types),0)+isnull((select count(*) from RED_VOTE_RECORD where types=@types),0)) as cnt,(isnull((select count(*) from RED_VOTE_RECORD),0)+isnull((select sum(counts) from RED_VOTE_INIT_COUNT),0)) as total_cnt";
                }

                SqlParameter[] para = { 
                                        new SqlParameter("@types",SqlDbType.Int)
                                      };
                para[0].Value = party_type;

                if (type == 1)
                {
                    cnt = Convert.ToInt32(DbHelperSQL.GetSingle(sql, para).ToString());
                }
                else
                {
                    ds = DbHelperSQL.Query(sql, para);

                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        double t = Convert.ToDouble(dr["cnt"].ToString());
                        double total = Convert.ToDouble(dr["total_cnt"].ToString());

                        //用这个方法比较理想，能得到你想要的小数点后位数
                        if (Convert.ToInt32(total) == 0)
                        {
                            cnt = 0;
                        }
                        else
                        {
                            cnt = Convert.ToDouble(t) / Convert.ToDouble(total);

                            //string result = cnt.ToString("0%");//得到6%
                            //string result = cnt.ToString("0.000%");//得到5.882%
                        }
                        

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return cnt;
        }
    }
}