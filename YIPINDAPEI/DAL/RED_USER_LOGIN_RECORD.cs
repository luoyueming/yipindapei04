using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using NewXzc.DBUtility;//Please add references
namespace NewXzc.DAL
{
	/// <summary>
	/// 数据访问类:RED_USER_LOGIN_RECORD
	/// </summary>
	public partial class RED_USER_LOGIN_RECORD
	{
		public RED_USER_LOGIN_RECORD()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "RED_USER_LOGIN_RECORD"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from RED_USER_LOGIN_RECORD");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(NewXzc.Model.RED_USER_LOGIN_RECORD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into RED_USER_LOGIN_RECORD(");
			strSql.Append("USERID,Login_Time,Login_IP,SessionID,Remark,Login_Type)");
			strSql.Append(" values (");
			strSql.Append("@USERID,@Login_Time,@Login_IP,@SessionID,@Remark,@Login_Type)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@Login_Time", SqlDbType.DateTime),
					new SqlParameter("@Login_IP", SqlDbType.NVarChar,50),
					new SqlParameter("@SessionID", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@Login_Type", SqlDbType.Int,4)};
			parameters[0].Value = model.USERID;
			parameters[1].Value = model.Login_Time;
			parameters[2].Value = model.Login_IP;
			parameters[3].Value = model.SessionID;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.Login_Type;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(NewXzc.Model.RED_USER_LOGIN_RECORD model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update RED_USER_LOGIN_RECORD set ");
			strSql.Append("USERID=@USERID,");
			strSql.Append("Login_Time=@Login_Time,");
			strSql.Append("Login_IP=@Login_IP,");
			strSql.Append("SessionID=@SessionID,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("Login_Type=@Login_Type");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@Login_Time", SqlDbType.DateTime),
					new SqlParameter("@Login_IP", SqlDbType.NVarChar,50),
					new SqlParameter("@SessionID", SqlDbType.VarChar,100),
					new SqlParameter("@Remark", SqlDbType.NVarChar,50),
					new SqlParameter("@Login_Type", SqlDbType.Int,4),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.USERID;
			parameters[1].Value = model.Login_Time;
			parameters[2].Value = model.Login_IP;
			parameters[3].Value = model.SessionID;
			parameters[4].Value = model.Remark;
			parameters[5].Value = model.Login_Type;
			parameters[6].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RED_USER_LOGIN_RECORD ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from RED_USER_LOGIN_RECORD ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		public NewXzc.Model.RED_USER_LOGIN_RECORD GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,USERID,Login_Time,Login_IP,SessionID,Remark,Login_Type from RED_USER_LOGIN_RECORD ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			NewXzc.Model.RED_USER_LOGIN_RECORD model=new NewXzc.Model.RED_USER_LOGIN_RECORD();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["USERID"]!=null && ds.Tables[0].Rows[0]["USERID"].ToString()!="")
				{
					model.USERID=int.Parse(ds.Tables[0].Rows[0]["USERID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Login_Time"]!=null && ds.Tables[0].Rows[0]["Login_Time"].ToString()!="")
				{
					model.Login_Time=DateTime.Parse(ds.Tables[0].Rows[0]["Login_Time"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Login_IP"]!=null && ds.Tables[0].Rows[0]["Login_IP"].ToString()!="")
				{
					model.Login_IP=ds.Tables[0].Rows[0]["Login_IP"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SessionID"]!=null && ds.Tables[0].Rows[0]["SessionID"].ToString()!="")
				{
					model.SessionID=ds.Tables[0].Rows[0]["SessionID"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Login_Type"]!=null && ds.Tables[0].Rows[0]["Login_Type"].ToString()!="")
				{
					model.Login_Type=int.Parse(ds.Tables[0].Rows[0]["Login_Type"].ToString());
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,USERID,Login_Time,Login_IP,SessionID,Remark,Login_Type ");
			strSql.Append(" FROM RED_USER_LOGIN_RECORD ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,USERID,Login_Time,Login_IP,SessionID,Remark,Login_Type ");
			strSql.Append(" FROM RED_USER_LOGIN_RECORD ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM RED_USER_LOGIN_RECORD ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from RED_USER_LOGIN_RECORD T ");
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
			parameters[0].Value = "RED_USER_LOGIN_RECORD";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method

        #region  自定义



        /// <summary>
        /// 得到一个对象实体，0：红人议会，1：红人汇，2：红人汇手机端
        /// </summary>
        /// <param name="userid">当前登录人用户ID</param>
        /// <param name="login_type">0：红人议会，1：红人汇，2：红人汇手机端</param>
        /// <returns></returns>
        public NewXzc.Model.RED_USER_LOGIN_RECORD GetModel(int userid, int login_type)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,USERID,Login_Time,Login_IP,SessionID,Remark,Login_Type from RED_USER_LOGIN_RECORD ");
            strSql.Append(" where USERID=@USERID and Login_Type=@Login_Type");
            SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
                    new SqlParameter("@Login_Type", SqlDbType.Int,4)
			};
            parameters[0].Value = userid;
            parameters[1].Value = login_type;

            NewXzc.Model.RED_USER_LOGIN_RECORD model = new NewXzc.Model.RED_USER_LOGIN_RECORD();
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
                if (ds.Tables[0].Rows[0]["Login_Time"] != null && ds.Tables[0].Rows[0]["Login_Time"].ToString() != "")
                {
                    model.Login_Time = DateTime.Parse(ds.Tables[0].Rows[0]["Login_Time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Login_IP"] != null && ds.Tables[0].Rows[0]["Login_IP"].ToString() != "")
                {
                    model.Login_IP = ds.Tables[0].Rows[0]["Login_IP"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SessionID"] != null && ds.Tables[0].Rows[0]["SessionID"].ToString() != "")
                {
                    model.SessionID = ds.Tables[0].Rows[0]["SessionID"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Remark"] != null && ds.Tables[0].Rows[0]["Remark"].ToString() != "")
                {
                    model.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Login_Type"] != null && ds.Tables[0].Rows[0]["Login_Type"].ToString() != "")
                {
                    model.Login_Type = int.Parse(ds.Tables[0].Rows[0]["Login_Type"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        #endregion
	}
}

