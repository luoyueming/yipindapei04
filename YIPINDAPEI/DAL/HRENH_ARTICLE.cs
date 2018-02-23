using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using NewXzc.DBUtility;//Please add references
namespace NewXzc.DAL
{
    /// <summary>
    /// 数据访问类:HRENH_ARTICLE
    /// </summary>
    public partial class HRENH_ARTICLE
    {
        public HRENH_ARTICLE()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("ID", "HRENH_ARTICLE");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from HRENH_ARTICLE");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.HRENH_ARTICLE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into HRENH_ARTICLE(");
            strSql.Append("ID,TITLE,ISIMG,IMG_URL,CONTENTS,TYPES,TYPES_PID,SUBJECT_ID,ZP_ZX_ID,READ_CNT,FALSH_READ_CNT,ISHOT,ISEND,EDITTIME,ADDTIME)");
            strSql.Append(" values (");
            strSql.Append("@ID,@TITLE,@ISIMG,@IMG_URL,@CONTENTS,@TYPES,@TYPES_PID,@SUBJECT_ID,@ZP_ZX_ID,@READ_CNT,@FALSH_READ_CNT,@ISHOT,@ISEND,@EDITTIME,@ADDTIME)");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4),
					new SqlParameter("@TITLE", SqlDbType.NVarChar,1000),
					new SqlParameter("@ISIMG", SqlDbType.Int,4),
					new SqlParameter("@IMG_URL", SqlDbType.NVarChar,100),
					new SqlParameter("@CONTENTS", SqlDbType.NVarChar),
					new SqlParameter("@TYPES", SqlDbType.Int,4),
					new SqlParameter("@TYPES_PID", SqlDbType.Int,4),
					new SqlParameter("@SUBJECT_ID", SqlDbType.Int,4),
					new SqlParameter("@ZP_ZX_ID", SqlDbType.Int,4),
					new SqlParameter("@READ_CNT", SqlDbType.Int,4),
					new SqlParameter("@FALSH_READ_CNT", SqlDbType.Int,4),
					new SqlParameter("@ISHOT", SqlDbType.Int,4),
					new SqlParameter("@ISEND", SqlDbType.Int,4),
					new SqlParameter("@EDITTIME", SqlDbType.DateTime),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TITLE;
            parameters[2].Value = model.ISIMG;
            parameters[3].Value = model.IMG_URL;
            parameters[4].Value = model.CONTENTS;
            parameters[5].Value = model.TYPES;
            parameters[6].Value = model.TYPES_PID;
            parameters[7].Value = model.SUBJECT_ID;
            parameters[8].Value = model.ZP_ZX_ID;
            parameters[9].Value = model.READ_CNT;
            parameters[10].Value = model.FALSH_READ_CNT;
            parameters[11].Value = model.ISHOT;
            parameters[12].Value = model.ISEND;
            parameters[13].Value = model.EDITTIME;
            parameters[14].Value = model.ADDTIME;

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
        public bool Update(NewXzc.Model.HRENH_ARTICLE model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update HRENH_ARTICLE set ");
            strSql.Append("TITLE=@TITLE,");
            strSql.Append("ISIMG=@ISIMG,");
            strSql.Append("IMG_URL=@IMG_URL,");
            strSql.Append("CONTENTS=@CONTENTS,");
            strSql.Append("TYPES=@TYPES,");
            strSql.Append("TYPES_PID=@TYPES_PID,");
            strSql.Append("SUBJECT_ID=@SUBJECT_ID,");
            strSql.Append("ZP_ZX_ID=@ZP_ZX_ID,");
            strSql.Append("READ_CNT=@READ_CNT,");
            strSql.Append("FALSH_READ_CNT=@FALSH_READ_CNT,");
            strSql.Append("ISHOT=@ISHOT,");
            strSql.Append("ISEND=@ISEND,");
            strSql.Append("EDITTIME=@EDITTIME,");
            strSql.Append("ADDTIME=@ADDTIME");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@TITLE", SqlDbType.NVarChar,1000),
					new SqlParameter("@ISIMG", SqlDbType.Int,4),
					new SqlParameter("@IMG_URL", SqlDbType.NVarChar,100),
					new SqlParameter("@CONTENTS", SqlDbType.NVarChar),
					new SqlParameter("@TYPES", SqlDbType.Int,4),
					new SqlParameter("@TYPES_PID", SqlDbType.Int,4),
					new SqlParameter("@SUBJECT_ID", SqlDbType.Int,4),
					new SqlParameter("@ZP_ZX_ID", SqlDbType.Int,4),
					new SqlParameter("@READ_CNT", SqlDbType.Int,4),
					new SqlParameter("@FALSH_READ_CNT", SqlDbType.Int,4),
					new SqlParameter("@ISHOT", SqlDbType.Int,4),
					new SqlParameter("@ISEND", SqlDbType.Int,4),
					new SqlParameter("@EDITTIME", SqlDbType.DateTime),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
            parameters[0].Value = model.TITLE;
            parameters[1].Value = model.ISIMG;
            parameters[2].Value = model.IMG_URL;
            parameters[3].Value = model.CONTENTS;
            parameters[4].Value = model.TYPES;
            parameters[5].Value = model.TYPES_PID;
            parameters[6].Value = model.SUBJECT_ID;
            parameters[7].Value = model.ZP_ZX_ID;
            parameters[8].Value = model.READ_CNT;
            parameters[9].Value = model.FALSH_READ_CNT;
            parameters[10].Value = model.ISHOT;
            parameters[11].Value = model.ISEND;
            parameters[12].Value = model.EDITTIME;
            parameters[13].Value = model.ADDTIME;
            parameters[14].Value = model.ID;

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
            strSql.Append("delete from HRENH_ARTICLE ");
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
            strSql.Append("delete from HRENH_ARTICLE ");
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
        public NewXzc.Model.HRENH_ARTICLE GetModel(int ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,TITLE,ISIMG,IMG_URL,CONTENTS,TYPES,TYPES_PID,SUBJECT_ID,ZP_ZX_ID,READ_CNT,FALSH_READ_CNT,ISHOT,ISEND,EDITTIME,ADDTIME from HRENH_ARTICLE ");
            strSql.Append(" where ID=@ID ");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)			};
            parameters[0].Value = ID;

            NewXzc.Model.HRENH_ARTICLE model = new NewXzc.Model.HRENH_ARTICLE();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["ID"] != null && ds.Tables[0].Rows[0]["ID"].ToString() != "")
                {
                    model.ID = int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TITLE"] != null && ds.Tables[0].Rows[0]["TITLE"].ToString() != "")
                {
                    model.TITLE = ds.Tables[0].Rows[0]["TITLE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ISIMG"] != null && ds.Tables[0].Rows[0]["ISIMG"].ToString() != "")
                {
                    model.ISIMG = int.Parse(ds.Tables[0].Rows[0]["ISIMG"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IMG_URL"] != null && ds.Tables[0].Rows[0]["IMG_URL"].ToString() != "")
                {
                    model.IMG_URL = ds.Tables[0].Rows[0]["IMG_URL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CONTENTS"] != null && ds.Tables[0].Rows[0]["CONTENTS"].ToString() != "")
                {
                    model.CONTENTS = ds.Tables[0].Rows[0]["CONTENTS"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TYPES"] != null && ds.Tables[0].Rows[0]["TYPES"].ToString() != "")
                {
                    model.TYPES = int.Parse(ds.Tables[0].Rows[0]["TYPES"].ToString());
                }
                if (ds.Tables[0].Rows[0]["TYPES_PID"] != null && ds.Tables[0].Rows[0]["TYPES_PID"].ToString() != "")
                {
                    model.TYPES_PID = int.Parse(ds.Tables[0].Rows[0]["TYPES_PID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SUBJECT_ID"] != null && ds.Tables[0].Rows[0]["SUBJECT_ID"].ToString() != "")
                {
                    model.SUBJECT_ID = int.Parse(ds.Tables[0].Rows[0]["SUBJECT_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ZP_ZX_ID"] != null && ds.Tables[0].Rows[0]["ZP_ZX_ID"].ToString() != "")
                {
                    model.ZP_ZX_ID = int.Parse(ds.Tables[0].Rows[0]["ZP_ZX_ID"].ToString());
                }
                if (ds.Tables[0].Rows[0]["READ_CNT"] != null && ds.Tables[0].Rows[0]["READ_CNT"].ToString() != "")
                {
                    model.READ_CNT = int.Parse(ds.Tables[0].Rows[0]["READ_CNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["FALSH_READ_CNT"] != null && ds.Tables[0].Rows[0]["FALSH_READ_CNT"].ToString() != "")
                {
                    model.FALSH_READ_CNT = int.Parse(ds.Tables[0].Rows[0]["FALSH_READ_CNT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ISHOT"] != null && ds.Tables[0].Rows[0]["ISHOT"].ToString() != "")
                {
                    model.ISHOT = int.Parse(ds.Tables[0].Rows[0]["ISHOT"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ISEND"] != null && ds.Tables[0].Rows[0]["ISEND"].ToString() != "")
                {
                    model.ISEND = int.Parse(ds.Tables[0].Rows[0]["ISEND"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EDITTIME"] != null && ds.Tables[0].Rows[0]["EDITTIME"].ToString() != "")
                {
                    model.EDITTIME = DateTime.Parse(ds.Tables[0].Rows[0]["EDITTIME"].ToString());
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
            strSql.Append("select ID,TITLE,ISIMG,IMG_URL,CONTENTS,TYPES,TYPES_PID,SUBJECT_ID,ZP_ZX_ID,READ_CNT,FALSH_READ_CNT,ISHOT,ISEND,EDITTIME,ADDTIME ");
            strSql.Append(" FROM HRENH_ARTICLE ");
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
            strSql.Append(" ID,TITLE,ISIMG,IMG_URL,CONTENTS,TYPES,TYPES_PID,SUBJECT_ID,ZP_ZX_ID,READ_CNT,FALSH_READ_CNT,ISHOT,ISEND,EDITTIME,ADDTIME ");
            strSql.Append(" FROM HRENH_ARTICLE ");
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
            strSql.Append("select count(1) FROM HRENH_ARTICLE ");
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
            strSql.Append(")AS Row, T.*  from HRENH_ARTICLE T ");
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
            parameters[0].Value = "HRENH_ARTICLE";
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

