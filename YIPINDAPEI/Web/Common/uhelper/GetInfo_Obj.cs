using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewXzc.DBUtility;

namespace NewXzc.Web.Common.uhelper
{
    public class GetInfo_Obj
    {
        /// <summary>
        /// 根据查询条件获取需要获得的值
        /// </summary>
        /// <param name="tabname">表名</param>
        /// <param name="fileds">列（字段）名</param>
        /// <param name="where">查询条件</param>
        /// <param name="type">返回结果，1：DataSet，2：int类型数值，3：字符串</param>
        /// <returns></returns>
        public static object GetInfo(string tabname, string fileds, string where, int type)
        {
            object obj = null;

            string sql = "select " + fileds + " from " + tabname + " where " + where;

            try
            {
                if (type == 1)
                {
                    obj = DbHelperSQL.Query(sql);
                }
                else if (type == 2)
                {
                    obj = Convert.ToInt32(DbHelperSQL.GetSingle(sql).ToString());
                }
                else if (type == 3)
                {
                    obj = DbHelperSQL.GetSingle(sql).ToString();
                }
            }
            catch (Exception ex)
            {
                if (type == 1)
                {
                    obj = null;
                }
                else if (type == 2)
                {
                    obj = 0;
                }
                else if (type == 3)
                {
                    obj = "";
                }
            }

            return obj;
        }
    }
}