using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using NewXzc.DBUtility;//Please add references
namespace NewXzc.DAL
{
    /// <summary>
    /// 数据访问类:HRENH_ARTICLE_REVIEW_REPLY
    /// </summary>
    public partial class HRENH_ARTICLE_REVIEW_REPLY
    {
        public HRENH_ARTICLE_REVIEW_REPLY()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "HRENH_ARTICLE_REVIEW_REPLY");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HRENH_ARTICLE_REVIEW_REPLY");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HRENH_ARTICLE_REVIEW_REPLY(");
            strSql.Append("ID,USERID,REPLY_ID,PUB_USERID,REVIEW_PID,CONTENTS,ISLIKE,TYPES,ARTICLE_ID,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@ID,@USERID,@REPLY_ID,@PUB_USERID,@REVIEW_PID,@CONTENTS,@ISLIKE,@TYPES,@ARTICLE_ID,@ADDTIME)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@REPLY_ID", SqlDbType.Int,4),
					new SqlParameter("@PUB_USERID", SqlDbType.Int,4),
					new SqlParameter("@REVIEW_PID", SqlDbType.Int,4),
					new SqlParameter("@CONTENTS", SqlDbType.NVarChar,1000),
					new SqlParameter("@ISLIKE", SqlDbType.Int,4),
					new SqlParameter("@TYPES", SqlDbType.Int,4),
					new SqlParameter("@ARTICLE_ID", SqlDbType.Int,4),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.USERID;
            parameters[2].Value = model.REPLY_ID;
            parameters[3].Value = model.PUB_USERID;
            parameters[4].Value = model.REVIEW_PID;
            parameters[5].Value = model.CONTENTS;
            parameters[6].Value = model.ISLIKE;
            parameters[7].Value = model.TYPES;
            parameters[8].Value = model.ARTICLE_ID;
            parameters[9].Value = model.ADDTIME;

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
        public bool Update(NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HRENH_ARTICLE_REVIEW_REPLY set ");
            strSql.Append("USERID=@USERID,");
            strSql.Append("REPLY_ID=@REPLY_ID,");
            strSql.Append("PUB_USERID=@PUB_USERID,");
            strSql.Append("REVIEW_PID=@REVIEW_PID,");
            strSql.Append("CONTENTS=@CONTENTS,");
            strSql.Append("ISLIKE=@ISLIKE,");
            strSql.Append("TYPES=@TYPES,");
            strSql.Append("ARTICLE_ID=@ARTICLE_ID,");
            strSql.Append("ADDTIME=@ADDTIME");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@REPLY_ID", SqlDbType.Int,4),
					new SqlParameter("@PUB_USERID", SqlDbType.Int,4),
					new SqlParameter("@REVIEW_PID", SqlDbType.Int,4),
					new SqlParameter("@CONTENTS", SqlDbType.NVarChar,1000),
					new SqlParameter("@ISLIKE", SqlDbType.Int,4),
					new SqlParameter("@TYPES", SqlDbType.Int,4),
					new SqlParameter("@ARTICLE_ID", SqlDbType.Int,4),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.USERID;
            parameters[1].Value = model.REPLY_ID;
            parameters[2].Value = model.PUB_USERID;
            parameters[3].Value = model.REVIEW_PID;
            parameters[4].Value = model.CONTENTS;
            parameters[5].Value = model.ISLIKE;
            parameters[6].Value = model.TYPES;
            parameters[7].Value = model.ARTICLE_ID;
            parameters[8].Value = model.ADDTIME;
            parameters[9].Value = model.ID;

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
        public bool Delete(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HRENH_ARTICLE_REVIEW_REPLY ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

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
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from HRENH_ARTICLE_REVIEW_REPLY ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
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
        public NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,USERID,REPLY_ID,PUB_USERID,REVIEW_PID,CONTENTS,ISLIKE,TYPES,ARTICLE_ID,ADDTIME from HRENH_ARTICLE_REVIEW_REPLY ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY model = new NewXzc.Model.HRENH_ARTICLE_REVIEW_REPLY();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USERID"] != null && ds.Tables[0].Rows[0]["USERID"].ToString() != "")
                {
                    model.USERID = int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["REPLY_ID"] != null && ds.Tables[0].Rows[0]["REPLY_ID"].ToString() != "")
                {
                    model.REPLY_ID = int.Parse(ds.Tables[0].Rows[0]["REPLY_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PUB_USERID"] != null && ds.Tables[0].Rows[0]["PUB_USERID"].ToString() != "")
                {
                    model.PUB_USERID = int.Parse(ds.Tables[0].Rows[0]["PUB_USERID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["REVIEW_PID"] != null && ds.Tables[0].Rows[0]["REVIEW_PID"].ToString() != "")
                {
                    model.REVIEW_PID = int.Parse(ds.Tables[0].Rows[0]["REVIEW_PID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["CONTENTS"] != null && ds.Tables[0].Rows[0]["CONTENTS"].ToString() != "")
                {
                    model.CONTENTS = ds.Tables[0].Rows[0]["CONTENTS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ISLIKE"] != null && ds.Tables[0].Rows[0]["ISLIKE"].ToString() != "")
                {
                    model.ISLIKE = int.Parse(ds.Tables[0].Rows[0]["ISLIKE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TYPES"] != null && ds.Tables[0].Rows[0]["TYPES"].ToString() != "")
                {
                    model.TYPES = int.Parse(ds.Tables[0].Rows[0]["TYPES"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ARTICLE_ID"] != null && ds.Tables[0].Rows[0]["ARTICLE_ID"].ToString() != "")
                {
                    model.ARTICLE_ID = int.Parse(ds.Tables[0].Rows[0]["ARTICLE_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ADDTIME"] != null && ds.Tables[0].Rows[0]["ADDTIME"].ToString() != "")
                {
                    model.ADDTIME = DateTime.Parse(ds.Tables[0].Rows[0]["ADDTIME"].ToString());
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
            strSql.Append("select ID,USERID,REPLY_ID,PUB_USERID,REVIEW_PID,CONTENTS,ISLIKE,TYPES,ARTICLE_ID,ADDTIME ");
            strSql.Append(" FROM HRENH_ARTICLE_REVIEW_REPLY ");
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
            strSql.Append(" ID,USERID,REPLY_ID,PUB_USERID,REVIEW_PID,CONTENTS,ISLIKE,TYPES,ARTICLE_ID,ADDTIME ");
            strSql.Append(" FROM HRENH_ARTICLE_REVIEW_REPLY ");
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
            strSql.Append("select count(1) FROM HRENH_ARTICLE_REVIEW_REPLY ");
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
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from HRENH_ARTICLE_REVIEW_REPLY T ");
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
            parameters[0].Value = "HRENH_ARTICLE_REVIEW_REPLY";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method
    }
}

