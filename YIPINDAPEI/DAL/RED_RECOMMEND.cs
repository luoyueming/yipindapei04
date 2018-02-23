using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using NewXzc.DBUtility;//Please add references
namespace NewXzc.DAL
{
    /// <summary>
    /// 数据访问类:RED_RECOMMEND
    /// </summary>
    public partial class RED_RECOMMEND
    {
        public RED_RECOMMEND()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("Id", "RED_RECOMMEND");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RED_RECOMMEND");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.RED_RECOMMEND model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RED_RECOMMEND(");
            strSql.Append("Id,PageId,Cid,Sort,Url,V1,V2,V3,v4,AddTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@PageId,@Cid,@Sort,@Url,@V1,@V2,@V3,@v4,@AddTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@PageId", SqlDbType.Int,4),
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@V1", SqlDbType.NVarChar,500),
					new SqlParameter("@V2", SqlDbType.NVarChar,500),
					new SqlParameter("@V3", SqlDbType.NVarChar,500),
					new SqlParameter("@v4", SqlDbType.NVarChar,500),
					new SqlParameter("@AddTime", SqlDbType.DateTime)};
            parameters[0].Value = model.Id;
            parameters[1].Value = model.PageId;
            parameters[2].Value = model.Cid;
            parameters[3].Value = model.Sort;
            parameters[4].Value = model.Url;
            parameters[5].Value = model.V1;
            parameters[6].Value = model.V2;
            parameters[7].Value = model.V3;
            parameters[8].Value = model.v4;
            parameters[9].Value = model.AddTime;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(NewXzc.Model.RED_RECOMMEND model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RED_RECOMMEND set ");
            strSql.Append("PageId=@PageId,");
            strSql.Append("Cid=@Cid,");
            strSql.Append("Sort=@Sort,");
            strSql.Append("Url=@Url,");
            strSql.Append("V1=@V1,");
            strSql.Append("V2=@V2,");
            strSql.Append("V3=@V3,");
            strSql.Append("v4=@v4,");
            strSql.Append("AddTime=@AddTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@PageId", SqlDbType.Int,4),
					new SqlParameter("@Cid", SqlDbType.Int,4),
					new SqlParameter("@Sort", SqlDbType.Int,4),
					new SqlParameter("@Url", SqlDbType.NVarChar,500),
					new SqlParameter("@V1", SqlDbType.NVarChar,500),
					new SqlParameter("@V2", SqlDbType.NVarChar,500),
					new SqlParameter("@V3", SqlDbType.NVarChar,500),
					new SqlParameter("@v4", SqlDbType.NVarChar,500),
					new SqlParameter("@AddTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.Int,4)};
            parameters[0].Value = model.PageId;
            parameters[1].Value = model.Cid;
            parameters[2].Value = model.Sort;
            parameters[3].Value = model.Url;
            parameters[4].Value = model.V1;
            parameters[5].Value = model.V2;
            parameters[6].Value = model.V3;
            parameters[7].Value = model.v4;
            parameters[8].Value = model.AddTime;
            parameters[9].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RED_RECOMMEND ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from RED_RECOMMEND ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public NewXzc.Model.RED_RECOMMEND GetModel(int Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,PageId,Cid,Sort,Url,V1,V2,V3,v4,AddTime from RED_RECOMMEND ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)			};
            parameters[0].Value = Id;

            NewXzc.Model.RED_RECOMMEND model = new NewXzc.Model.RED_RECOMMEND();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PageId"] != null && ds.Tables[0].Rows[0]["PageId"].ToString() != "")
                {
                    model.PageId = int.Parse(ds.Tables[0].Rows[0]["PageId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Cid"] != null && ds.Tables[0].Rows[0]["Cid"].ToString() != "")
                {
                    model.Cid = int.Parse(ds.Tables[0].Rows[0]["Cid"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Sort"] != null && ds.Tables[0].Rows[0]["Sort"].ToString() != "")
                {
                    model.Sort = int.Parse(ds.Tables[0].Rows[0]["Sort"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Url"] != null && ds.Tables[0].Rows[0]["Url"].ToString() != "")
                {
                    model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["V1"] != null && ds.Tables[0].Rows[0]["V1"].ToString() != "")
                {
                    model.V1 = ds.Tables[0].Rows[0]["V1"].ToString();
                }
                if (ds.Tables[0].Rows[0]["V2"] != null && ds.Tables[0].Rows[0]["V2"].ToString() != "")
                {
                    model.V2 = ds.Tables[0].Rows[0]["V2"].ToString();
                }
                if (ds.Tables[0].Rows[0]["V3"] != null && ds.Tables[0].Rows[0]["V3"].ToString() != "")
                {
                    model.V3 = ds.Tables[0].Rows[0]["V3"].ToString();
                }
                if (ds.Tables[0].Rows[0]["v4"] != null && ds.Tables[0].Rows[0]["v4"].ToString() != "")
                {
                    model.v4 = ds.Tables[0].Rows[0]["v4"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AddTime"] != null && ds.Tables[0].Rows[0]["AddTime"].ToString() != "")
                {
                    model.AddTime = DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,PageId,Cid,Sort,Url,V1,V2,V3,v4,AddTime ");
            strSql.Append(" FROM RED_RECOMMEND ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,PageId,Cid,Sort,Url,V1,V2,V3,v4,AddTime ");
            strSql.Append(" FROM RED_RECOMMEND ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM RED_RECOMMEND ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from RED_RECOMMEND T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "RED_RECOMMEND";
            parameters[1].Value = "Id";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region  推荐操作  宋明亮 2014-12-05
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="top">获取指定的数目，可为0</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataSet GetList_NewIndex(int top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (top > 0)
            {
                strSql.Append("select top " + top + " Id,PageId,Cid,Sort,Url,V1,V2,V3,isnull(V4,'') as V4 ");
            }
            else
            {
                strSql.Append("select Id,PageId,Cid,Sort,Url,V1,V2,V3,isnull(V4,'') as V4 ");
            }
            strSql.Append(" FROM RED_RECOMMEND ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort asc,id asc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <param name="top">获取指定的数目，可为0</param>
        /// <param name="tabname">需要被查询的表</param>
        /// <param name="coloum">需要显示哪些列，此处是列名，如（,username,user_head），提示：此处的列名已有一部分固定PageId,Cid,Sort,Url,V1,V2,V3,isnull(V4,'') as V4，只要将需要显示的其他的列名在其后加","显示即可</param>
        /// <param name="strWhere">查询条件</param>
        /// <returns></returns>
        public DataSet GetList_NewIndex(int top, string tabname, string coloum, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            if (top > 0)
            {
                strSql.Append("select top " + top + " PageId,Cid,Sort,Url,V1,V2,V3,isnull(V4,'') as V4 ");
            }
            else
            {
                strSql.Append("select PageId,Cid,Sort,Url,V1,V2,V3,isnull(V4,'') as V4 ");
            }
            strSql.Append(coloum);
            strSql.Append(" from " + tabname);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort asc,id asc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        #endregion
    }
}

