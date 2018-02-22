using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using NewXzc.DBUtility;
using NewXzc.Common;

namespace NewXzc.Web.Common
{
    public class Company_Money_Helper
    {
        /// <summary>
        /// 获取企业用户的总金额、余额，0：总金额，1：已花费金额，2：余额，3：可开具发票的金额
        /// </summary>
        /// <param name="userid">企业用户ID</param>
        /// <param name="types">0：总金额，1：已花费金额，2：余额</param>
        /// <returns></returns>
        public static int Get_Company_Money(int userid, int types)
        {
            int money = 0;

            string sql = "";

            if (types == 0)
            {
                sql = "select isnull(sum(money),0) as total_money from invite_company_recharge where userid=@userid and state=1";
            }
            else if (types == 1)
            {
                //sql = "select isnull(sum(moneys),0) as look_money from Invite_Look_Resume_LinkStyle where userid=@userid and seltype=0";
                sql = "select isnull(sum(moneys),0) as look_money from Invite_Look_Resume_LinkStyle where userid=@userid";
            }
            else if (types == 2)
            {
                //sql = "select ((select isnull(sum(money),0) as total_money from invite_company_recharge where userid=@userid and state=1)-(select isnull(sum(moneys),0) as look_money from Invite_Look_Resume_LinkStyle where userid=@userid and seltype=0)) as balance_money ";
                sql = "select ((select isnull(sum(money),0) as total_money from invite_company_recharge where userid=@userid and state=1)-(select isnull(sum(moneys),0) as look_money from Invite_Look_Resume_LinkStyle where userid=@userid)) as balance_money ";
            }
            else if (types == 3)
            {
                //sql = "select ((select isnull(sum(moneys),0) as look_money from Invite_Look_Resume_LinkStyle where userid=@userid and seltype=0)-(select isnull(sum(moneys),0) from Invite_Invoice where userid=@userid)) as balance_money";
                sql = "select ((select isnull(sum(moneys),0) as look_money from Invite_Look_Resume_LinkStyle where userid=@userid)-(select isnull(sum(moneys),0) from Invite_Invoice where userid=@userid)) as balance_money";
            }

            SqlParameter[] para = { new SqlParameter("@userid",SqlDbType.Int) };

            para[0].Value = userid;

            object obj=DbHelperSQL.GetSingle(sql, para);

            if (obj != null)
            {
                money =Convert.ToInt32(Recruit_Job.Return_Money(obj.ToString(), 2));
            }

            return money;
        }
    }
}