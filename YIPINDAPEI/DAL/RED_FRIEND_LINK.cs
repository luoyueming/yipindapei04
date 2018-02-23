using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using NewXzc.DBUtility;//Please add references
namespace NewXzc.DAL
{
    /// <summary>
    /// 数据访问类:RED_FRIEND_LINK
    /// </summary>
    public partial class RED_FRIEND_LINK
    {
        public RED_FRIEND_LINK()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "RED_FRIEND_LINK");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RED_FRIEND_LINK");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.RED_FRIEND_LINK model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RED_FRIEND_LINK(");
            strSql.Append("ID,LINK_NAME,LINK_URL,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@ID,@LINK_NAME,@LINK_URL,@ADDTIME)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@LINK_NAME", SqlDbType.NVarChar,100),
					new SqlParameter("@LINK_URL", SqlDbType.NVarChar,100),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.LINK_NAME;
            parameters[2].Value = model.LINK_URL;
            parameters[3].Value = model.ADDTIME;

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
        public bool Update(NewXzc.Model.RED_FRIEND_LINK model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RED_FRIEND_LINK set ");
            strSql.Append("LINK_NAME=@LINK_NAME,");
            strSql.Append("LINK_URL=@LINK_URL,");
            strSql.Append("ADDTIME=@ADDTIME");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@LINK_NAME", SqlDbType.NVarChar,100),
					new SqlParameter("@LINK_URL", SqlDbType.NVarChar,100),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.LINK_NAME;
            parameters[1].Value = model.LINK_URL;
            parameters[2].Value = model.ADDTIME;
            parameters[3].Value = model.ID;

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
            strSql.Append("delete from RED_FRIEND_LINK ");
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
            strSql.Append("delete from RED_FRIEND_LINK ");
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
        public NewXzc.Model.RED_FRIEND_LINK GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,LINK_NAME,LINK_URL,ADDTIME from RED_FRIEND_LINK ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            NewXzc.Model.RED_FRIEND_LINK model = new NewXzc.Model.RED_FRIEND_LINK();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LINK_NAME"] != null && ds.Tables[0].Rows[0]["LINK_NAME"].ToString() != "")
                {
                    model.LINK_NAME = ds.Tables[0].Rows[0]["LINK_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LINK_URL"] != null && ds.Tables[0].Rows[0]["LINK_URL"].ToString() != "")
                {
                    model.LINK_URL = ds.Tables[0].Rows[0]["LINK_URL"].ToString();
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
            strSql.Append("select ID,LINK_NAME,LINK_URL,ADDTIME ");
            strSql.Append(" FROM RED_FRIEND_LINK ");
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
            strSql.Append(" ID,LINK_NAME,LINK_URL,ADDTIME ");
            strSql.Append(" FROM RED_FRIEND_LINK ");
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
            strSql.Append("select count(1) FROM RED_FRIEND_LINK ");
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
            strSql.Append(")AS Row, T.*  from RED_FRIEND_LINK T ");
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
            parameters[0].Value = "RED_FRIEND_LINK";
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

