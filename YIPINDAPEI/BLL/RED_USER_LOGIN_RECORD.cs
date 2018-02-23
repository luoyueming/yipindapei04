using System;
using System.Data;
using System.Collections.Generic;
using NewXzc.Common;
using NewXzc.Model;
namespace NewXzc.BLL
{
	/// <summary>
	/// RED_USER_LOGIN_RECORD
	/// </summary>
	public partial class RED_USER_LOGIN_RECORD
	{
		private readonly NewXzc.DAL.RED_USER_LOGIN_RECORD dal=new NewXzc.DAL.RED_USER_LOGIN_RECORD();
		public RED_USER_LOGIN_RECORD()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
			return dal.GetMaxId();
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(NewXzc.Model.RED_USER_LOGIN_RECORD model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(NewXzc.Model.RED_USER_LOGIN_RECORD model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public NewXzc.Model.RED_USER_LOGIN_RECORD GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public NewXzc.Model.RED_USER_LOGIN_RECORD GetModelByCache(int ID)
		{
			
			string CacheKey = "RED_USER_LOGIN_RECORDModel-" + ID;
			object objModel = NewXzc.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
						int ModelCache = NewXzc.Common.ConfigHelper.GetConfigInt("ModelCache");
						NewXzc.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (NewXzc.Model.RED_USER_LOGIN_RECORD)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<NewXzc.Model.RED_USER_LOGIN_RECORD> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<NewXzc.Model.RED_USER_LOGIN_RECORD> DataTableToList(DataTable dt)
		{
			List<NewXzc.Model.RED_USER_LOGIN_RECORD> modelList = new List<NewXzc.Model.RED_USER_LOGIN_RECORD>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				NewXzc.Model.RED_USER_LOGIN_RECORD model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new NewXzc.Model.RED_USER_LOGIN_RECORD();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["USERID"]!=null && dt.Rows[n]["USERID"].ToString()!="")
					{
						model.USERID=int.Parse(dt.Rows[n]["USERID"].ToString());
					}
					if(dt.Rows[n]["Login_Time"]!=null && dt.Rows[n]["Login_Time"].ToString()!="")
					{
						model.Login_Time=DateTime.Parse(dt.Rows[n]["Login_Time"].ToString());
					}
					if(dt.Rows[n]["Login_IP"]!=null && dt.Rows[n]["Login_IP"].ToString()!="")
					{
					model.Login_IP=dt.Rows[n]["Login_IP"].ToString();
					}
					if(dt.Rows[n]["SessionID"]!=null && dt.Rows[n]["SessionID"].ToString()!="")
					{
					model.SessionID=dt.Rows[n]["SessionID"].ToString();
					}
					if(dt.Rows[n]["Remark"]!=null && dt.Rows[n]["Remark"].ToString()!="")
					{
					model.Remark=dt.Rows[n]["Remark"].ToString();
					}
					if(dt.Rows[n]["Login_Type"]!=null && dt.Rows[n]["Login_Type"].ToString()!="")
					{
						model.Login_Type=int.Parse(dt.Rows[n]["Login_Type"].ToString());
					}
					modelList.Add(model);
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

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

            return dal.GetModel(userid, login_type);
        }

        #endregion
	}
}

