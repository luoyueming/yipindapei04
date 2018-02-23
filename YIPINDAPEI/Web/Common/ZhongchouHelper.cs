using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.Common;
using NewXzc.DBUtility;
using System.Data;

namespace NewXzc.Web.Common
{
    public class ZhongchouHelper
    {
        /// <summary>
        /// 更新当前众筹结果
        /// </summary>
        /// <param name="zcid">众筹ID</param>
        /// <param name="type">ID类型，0：众筹ID，1：支持ID</param>
        public static void Update_ZC_State(int zcid,int type)
        {
            string tabname = " HRENH_CROWDFUNDING_INFO a ";
            string coloum = " a.TOTAL_MONEY,a.ENDTIME,isnull((select SUM(MONEY) from HRENH_SUPPORT_USER_RECHARGE_RECORD where SUPPORT_ID in(select ID from HRENH_SUPPORT_TYPE_INFO where CROWD_ID=a.ID) and State=2),0) as get_money ";
            string where = " a.id="+zcid+" ";

            if (type == 1)
            {
                where = " a.id in(select top 1 crowd_id from HRENH_SUPPORT_TYPE_INFO where id="+zcid+") ";
            }


            string sql = "select"+coloum+"from "+tabname+" where"+where;

            try
            {
                DataSet ds = DbHelperSQL.Query(sql);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {

                        string endtime = TimeParser.ReturnCurTime(dr["endtime"].ToString(), 0);
                        string total_money = String_Manage.Return_Str(dr["total_money"].ToString(), "0");
                        string get_money = String_Manage.Return_Str(dr["get_money"].ToString(), "0");

                        DateTime now = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                        TimeSpan ts = Convert.ToDateTime(endtime) - now;

                        int results = 0;

                        int leave_days = 0;

                        decimal tmoney = Convert.ToDecimal(total_money);
                        decimal gmoney = Convert.ToDecimal(get_money);

                        if (ts.Days > 0)
                        {
                            if (gmoney >= tmoney)
                            {
                                results = 2;
                            }
                            else
                            {
                                results = 1;
                            }
                            endtime = ts.Days.ToString();
                            leave_days = ts.Days;
                        }
                        else
                        {
                            if (gmoney >= tmoney)
                            {
                                results = 2;
                            }
                            else
                            {
                                results = 3;
                            }
                        }

                        #region  更新当前众筹结果
                        try
                        {
                            if (results > 0)
                            {
                                DbHelperSQL.ExecuteSql("update HRENH_CROWDFUNDING_INFO set results=" + results + " where id=" + zcid);
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}