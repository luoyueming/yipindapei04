using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using NewXzc.DBUtility;//Please add references
namespace NewXzc.DAL
{
	/// <summary>
	/// 数据访问类:RED_USER
	/// </summary>
	public partial class RED_USER
	{
		public RED_USER()
		{}

        #region  Method
        
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="USERID">用户USERID</param>
        /// <returns></returns>
        public bool Exists(int USERID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from RED_USER");
            strSql.Append(" where USERID=@USERID");

            SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)
			};
            parameters[0].Value = USERID;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetMaxId()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 (max(userid)+1) as mmid FROM RED_USER ");

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(NewXzc.Model.RED_USER model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into RED_USER(");
            strSql.Append("USERID,USERNAME,PWD,TEL,TEL_STATE,USER_HEAD,SEX,EMAIL,USER_TYPE,ISCOMPLETE,PROVINCE,CITY,AREA,PEOPLE_IDENTITY,OCCUPATION,PERSONALITY,INTRODUCE,IDENTIFICATION_STATE,SAME_HOBBY_PEOPLE,EXP,SCORE,REALM_NAME,ADDTIME,IS_RED,STATE,REMARK,Person_Desc,Person_NickName,Porder,UpdateTime,PLAT_VAL,SPECIALTY_VAL,IPURL)");
            strSql.Append(" values (");
            strSql.Append("@USERID,@USERNAME,@PWD,@TEL,@TEL_STATE,@USER_HEAD,@SEX,@EMAIL,@USER_TYPE,@ISCOMPLETE,@PROVINCE,@CITY,@AREA,@PEOPLE_IDENTITY,@OCCUPATION,@PERSONALITY,@INTRODUCE,@IDENTIFICATION_STATE,@SAME_HOBBY_PEOPLE,@EXP,@SCORE,@REALM_NAME,@ADDTIME,@IS_RED,@STATE,@REMARK,@Person_Desc,@Person_NickName,@Porder,@UpdateTime,@PLAT_VAL,@SPECIALTY_VAL,@IPURL)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@USERNAME", SqlDbType.NVarChar,100),
					new SqlParameter("@PWD", SqlDbType.NVarChar,100),
					new SqlParameter("@TEL", SqlDbType.NVarChar,50),
					new SqlParameter("@TEL_STATE", SqlDbType.Int,4),
					new SqlParameter("@USER_HEAD", SqlDbType.NVarChar,100),
					new SqlParameter("@SEX", SqlDbType.Int,4),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,100),
					new SqlParameter("@USER_TYPE", SqlDbType.Int,4),
					new SqlParameter("@ISCOMPLETE", SqlDbType.Int,4),
					new SqlParameter("@PROVINCE", SqlDbType.NVarChar,50),
					new SqlParameter("@CITY", SqlDbType.NVarChar,50),
					new SqlParameter("@AREA", SqlDbType.NVarChar,50),
					new SqlParameter("@PEOPLE_IDENTITY", SqlDbType.NVarChar,200),
					new SqlParameter("@OCCUPATION", SqlDbType.NVarChar,200),
					new SqlParameter("@PERSONALITY", SqlDbType.NVarChar,200),
					new SqlParameter("@INTRODUCE", SqlDbType.NVarChar,1000),
					new SqlParameter("@IDENTIFICATION_STATE", SqlDbType.Int,4),
					new SqlParameter("@SAME_HOBBY_PEOPLE", SqlDbType.NVarChar,1000),
					new SqlParameter("@EXP", SqlDbType.Int,4),
					new SqlParameter("@SCORE", SqlDbType.Int,4),
					new SqlParameter("@REALM_NAME", SqlDbType.NVarChar,50),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime),
					new SqlParameter("@IS_RED", SqlDbType.Int,4),
					new SqlParameter("@STATE", SqlDbType.Int,4),
					new SqlParameter("@REMARK", SqlDbType.NVarChar,50),
					new SqlParameter("@Person_Desc", SqlDbType.NVarChar,3000),
					new SqlParameter("@Person_NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@Porder", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PLAT_VAL", SqlDbType.NVarChar,200),
					new SqlParameter("@SPECIALTY_VAL", SqlDbType.NVarChar,200),
					new SqlParameter("@IPURL", SqlDbType.NVarChar,200)};
            parameters[0].Value = model.USERID;
            parameters[1].Value = model.USERNAME;
            parameters[2].Value = model.PWD;
            parameters[3].Value = model.TEL;
            parameters[4].Value = model.TEL_STATE;
            parameters[5].Value = model.USER_HEAD;
            parameters[6].Value = model.SEX;
            parameters[7].Value = model.EMAIL;
            parameters[8].Value = model.USER_TYPE;
            parameters[9].Value = model.ISCOMPLETE;
            parameters[10].Value = model.PROVINCE;
            parameters[11].Value = model.CITY;
            parameters[12].Value = model.AREA;
            parameters[13].Value = model.PEOPLE_IDENTITY;
            parameters[14].Value = model.OCCUPATION;
            parameters[15].Value = model.PERSONALITY;
            parameters[16].Value = model.INTRODUCE;
            parameters[17].Value = model.IDENTIFICATION_STATE;
            parameters[18].Value = model.SAME_HOBBY_PEOPLE;
            parameters[19].Value = model.EXP;
            parameters[20].Value = model.SCORE;
            parameters[21].Value = model.REALM_NAME;
            parameters[22].Value = model.ADDTIME;
            parameters[23].Value = model.IS_RED;
            parameters[24].Value = model.STATE;
            parameters[25].Value = model.REMARK;
            parameters[26].Value = model.Person_Desc;
            parameters[27].Value = model.Person_NickName;
            parameters[28].Value = model.Porder;
            parameters[29].Value = model.UpdateTime;
            parameters[30].Value = model.PLAT_VAL;
            parameters[31].Value = model.SPECIALTY_VAL;
            parameters[32].Value = model.IPURL;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);

            bool f = true;

            if (obj == null)
            {
                f= false;
            }
            else
            {
                if (Convert.ToInt32(obj.ToString()) <= 0)
                {
                    f = false;
                }
            }

            return f;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="model">用户信息Model</param>
        /// <param name="type">执行操作类型，0：全部修改，1：修改用户头像和昵称，2：修改密码</param>
        /// <returns></returns>
        public bool Update(NewXzc.Model.RED_USER model,int type=0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RED_USER set ");
            strSql.Append("USERID=@USERID,");
            strSql.Append("USERNAME=@USERNAME,");
            strSql.Append("PWD=@PWD,");
            strSql.Append("TEL=@TEL,");
            strSql.Append("TEL_STATE=@TEL_STATE,");
            strSql.Append("USER_HEAD=@USER_HEAD,");
            strSql.Append("SEX=@SEX,");
            strSql.Append("EMAIL=@EMAIL,");
            strSql.Append("USER_TYPE=@USER_TYPE,");
            strSql.Append("ISCOMPLETE=@ISCOMPLETE,");
            strSql.Append("PROVINCE=@PROVINCE,");
            strSql.Append("CITY=@CITY,");
            strSql.Append("AREA=@AREA,");
            strSql.Append("PEOPLE_IDENTITY=@PEOPLE_IDENTITY,");
            strSql.Append("OCCUPATION=@OCCUPATION,");
            strSql.Append("PERSONALITY=@PERSONALITY,");
            strSql.Append("INTRODUCE=@INTRODUCE,");
            strSql.Append("IDENTIFICATION_STATE=@IDENTIFICATION_STATE,");
            strSql.Append("SAME_HOBBY_PEOPLE=@SAME_HOBBY_PEOPLE,");
            strSql.Append("EXP=@EXP,");
            strSql.Append("SCORE=@SCORE,");
            strSql.Append("REALM_NAME=@REALM_NAME,");
            strSql.Append("ADDTIME=@ADDTIME,");
            strSql.Append("IS_RED=@IS_RED,");
            strSql.Append("STATE=@STATE,");
            strSql.Append("REMARK=@REMARK,");
            strSql.Append("Person_Desc=@Person_Desc,");
            strSql.Append("Person_NickName=@Person_NickName,");
            strSql.Append("Porder=@Porder,");
            strSql.Append("UpdateTime=@UpdateTime,");
            strSql.Append("PLAT_VAL=@PLAT_VAL,");
            strSql.Append("SPECIALTY_VAL=@SPECIALTY_VAL,");
            strSql.Append("IPURL=@IPURL");
            strSql.Append(" where USERID=@USERID");
            //strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4),
					new SqlParameter("@USERNAME", SqlDbType.NVarChar,100),
					new SqlParameter("@PWD", SqlDbType.NVarChar,100),
					new SqlParameter("@TEL", SqlDbType.NVarChar,50),
					new SqlParameter("@TEL_STATE", SqlDbType.Int,4),
					new SqlParameter("@USER_HEAD", SqlDbType.NVarChar,100),
					new SqlParameter("@SEX", SqlDbType.Int,4),
					new SqlParameter("@EMAIL", SqlDbType.NVarChar,100),
					new SqlParameter("@USER_TYPE", SqlDbType.Int,4),
					new SqlParameter("@ISCOMPLETE", SqlDbType.Int,4),
					new SqlParameter("@PROVINCE", SqlDbType.NVarChar,50),
					new SqlParameter("@CITY", SqlDbType.NVarChar,50),
					new SqlParameter("@AREA", SqlDbType.NVarChar,50),
					new SqlParameter("@PEOPLE_IDENTITY", SqlDbType.NVarChar,200),
					new SqlParameter("@OCCUPATION", SqlDbType.NVarChar,200),
					new SqlParameter("@PERSONALITY", SqlDbType.NVarChar,200),
					new SqlParameter("@INTRODUCE", SqlDbType.NVarChar,1000),
					new SqlParameter("@IDENTIFICATION_STATE", SqlDbType.Int,4),
					new SqlParameter("@SAME_HOBBY_PEOPLE", SqlDbType.NVarChar,1000),
					new SqlParameter("@EXP", SqlDbType.Int,4),
					new SqlParameter("@SCORE", SqlDbType.Int,4),
					new SqlParameter("@REALM_NAME", SqlDbType.NVarChar,50),
					new SqlParameter("@ADDTIME", SqlDbType.DateTime),
					new SqlParameter("@IS_RED", SqlDbType.Int,4),
					new SqlParameter("@STATE", SqlDbType.Int,4),
					new SqlParameter("@REMARK", SqlDbType.NVarChar,50),
					new SqlParameter("@Person_Desc", SqlDbType.NVarChar,3000),
					new SqlParameter("@Person_NickName", SqlDbType.NVarChar,50),
					new SqlParameter("@Porder", SqlDbType.Int,4),
					new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@PLAT_VAL", SqlDbType.NVarChar,200),
					new SqlParameter("@SPECIALTY_VAL", SqlDbType.NVarChar,200),
					new SqlParameter("@IPURL", SqlDbType.NVarChar,200)
                    //,
                    //new SqlParameter("@ID", SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.USERID;
            parameters[1].Value = model.USERNAME;
            parameters[2].Value = model.PWD;
            parameters[3].Value = model.TEL;
            parameters[4].Value = model.TEL_STATE;
            parameters[5].Value = model.USER_HEAD;
            parameters[6].Value = model.SEX;
            parameters[7].Value = model.EMAIL;
            parameters[8].Value = model.USER_TYPE;
            parameters[9].Value = model.ISCOMPLETE;
            parameters[10].Value = model.PROVINCE;
            parameters[11].Value = model.CITY;
            parameters[12].Value = model.AREA;
            parameters[13].Value = model.PEOPLE_IDENTITY;
            parameters[14].Value = model.OCCUPATION;
            parameters[15].Value = model.PERSONALITY;
            parameters[16].Value = model.INTRODUCE;
            parameters[17].Value = model.IDENTIFICATION_STATE;
            parameters[18].Value = model.SAME_HOBBY_PEOPLE;
            parameters[19].Value = model.EXP;
            parameters[20].Value = model.SCORE;
            parameters[21].Value = model.REALM_NAME;
            parameters[22].Value = model.ADDTIME;
            parameters[23].Value = model.IS_RED;
            parameters[24].Value = model.STATE;
            parameters[25].Value = model.REMARK;
            parameters[26].Value = model.Person_Desc;
            parameters[27].Value = model.Person_NickName;
            parameters[28].Value = model.Porder;
            parameters[29].Value = model.UpdateTime;
            parameters[30].Value = model.PLAT_VAL;
            parameters[31].Value = model.SPECIALTY_VAL;
            parameters[32].Value = model.IPURL;
            //parameters[33].Value = model.ID;

            
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                #region  修改微吧用户信息
                try
                {
                    if (GetRecordCount(" tel='" + model.TEL + "' and userid<>"+model.USERID+" ") > 0)
                    {
                        strSql = new StringBuilder();

                        strSql.Append("update RED_USER set ");

                        ////1：修改用户头像和昵称
                        //if (type == 1)
                        //{
                        strSql.Append("USERNAME=@USERNAME,");
                        strSql.Append("USER_HEAD=@USER_HEAD,");
                        strSql.Append("PWD=@PWD");
                        //}//2：修改密码
                        //else if (type == 2)
                        //{
                        //    strSql.Append("PWD=@PWD");
                        //}

                        strSql.Append(" where TEL=@TEL and USERID<>@USERID");

                        ////1：修改用户头像和昵称
                        //if (type == 1)
                        //{
                        SqlParameter[] param = {
					            new SqlParameter("@USERID", SqlDbType.Int,4),
                                new SqlParameter("@TEL", SqlDbType.NVarChar,50),
					            new SqlParameter("@USERNAME", SqlDbType.NVarChar,100),
					            new SqlParameter("@USER_HEAD", SqlDbType.NVarChar,100),
                                new SqlParameter("@PWD", SqlDbType.NVarChar,100)
                            };

                        param[0].Value = model.USERID;
                        param[1].Value = model.TEL;
                        param[2].Value = model.USERNAME;
                        param[3].Value = model.USER_HEAD;
                        param[4].Value = model.PWD;

                        DbHelperSQL.ExecuteSql(strSql.ToString(), param);

                        //}//2：修改密码
                        //else if (type == 2)
                        //{
                        //    SqlParameter[] param = {
                        //        new SqlParameter("@USERID", SqlDbType.Int,4),
                        //        new SqlParameter("@TEL", SqlDbType.NVarChar,50),
                        //        new SqlParameter("@PWD", SqlDbType.NVarChar,100)
                        //    };

                        //    param[0].Value = model.USERID;
                        //    param[1].Value = model.TEL;
                        //    param[2].Value = model.PWD;

                        //    DbHelperSQL.ExecuteSql(strSql.ToString(), param);
                        //}

                    }
                }
                catch (Exception ex)
                {

                }
                #endregion

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
            strSql.Append("delete from RED_USER ");
            strSql.Append(" where ID=@ID");
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
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
            strSql.Append("delete from RED_USER ");
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
        /// <param name="USERID">用户USERID</param>
        /// <returns></returns>
        public NewXzc.Model.RED_USER GetModel(int USERID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 ID,USERID,USERNAME,PWD,TEL,TEL_STATE,USER_HEAD,SEX,EMAIL,USER_TYPE,ISCOMPLETE,PROVINCE,CITY,AREA,PEOPLE_IDENTITY,OCCUPATION,PERSONALITY,INTRODUCE,IDENTIFICATION_STATE,SAME_HOBBY_PEOPLE,EXP,SCORE,REALM_NAME,ADDTIME,IS_RED,STATE,REMARK,Person_Desc,Person_NickName,Porder,UpdateTime,PLAT_VAL,SPECIALTY_VAL,IPURL from RED_USER ");
            strSql.Append(" where USERID=@USERID order by userid asc");

            SqlParameter[] parameters = {
					new SqlParameter("@USERID", SqlDbType.Int,4)
			};
            parameters[0].Value = USERID;

            NewXzc.Model.RED_USER model = new NewXzc.Model.RED_USER();
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
                if (ds.Tables[0].Rows[0]["USERNAME"] != null && ds.Tables[0].Rows[0]["USERNAME"].ToString() != "")
                {
                    model.USERNAME = ds.Tables[0].Rows[0]["USERNAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PWD"] != null && ds.Tables[0].Rows[0]["PWD"].ToString() != "")
                {
                    model.PWD = ds.Tables[0].Rows[0]["PWD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TEL"] != null && ds.Tables[0].Rows[0]["TEL"].ToString() != "")
                {
                    model.TEL = ds.Tables[0].Rows[0]["TEL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["TEL_STATE"] != null && ds.Tables[0].Rows[0]["TEL_STATE"].ToString() != "")
                {
                    model.TEL_STATE = int.Parse(ds.Tables[0].Rows[0]["TEL_STATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["USER_HEAD"] != null && ds.Tables[0].Rows[0]["USER_HEAD"].ToString() != "")
                {
                    model.USER_HEAD = ds.Tables[0].Rows[0]["USER_HEAD"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SEX"] != null && ds.Tables[0].Rows[0]["SEX"].ToString() != "")
                {
                    model.SEX = int.Parse(ds.Tables[0].Rows[0]["SEX"].ToString());
                }
                if (ds.Tables[0].Rows[0]["EMAIL"] != null && ds.Tables[0].Rows[0]["EMAIL"].ToString() != "")
                {
                    model.EMAIL = ds.Tables[0].Rows[0]["EMAIL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["USER_TYPE"] != null && ds.Tables[0].Rows[0]["USER_TYPE"].ToString() != "")
                {
                    model.USER_TYPE = int.Parse(ds.Tables[0].Rows[0]["USER_TYPE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["ISCOMPLETE"] != null && ds.Tables[0].Rows[0]["ISCOMPLETE"].ToString() != "")
                {
                    model.ISCOMPLETE = int.Parse(ds.Tables[0].Rows[0]["ISCOMPLETE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PROVINCE"] != null && ds.Tables[0].Rows[0]["PROVINCE"].ToString() != "")
                {
                    model.PROVINCE = ds.Tables[0].Rows[0]["PROVINCE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["CITY"] != null && ds.Tables[0].Rows[0]["CITY"].ToString() != "")
                {
                    model.CITY = ds.Tables[0].Rows[0]["CITY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["AREA"] != null && ds.Tables[0].Rows[0]["AREA"].ToString() != "")
                {
                    model.AREA = ds.Tables[0].Rows[0]["AREA"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PEOPLE_IDENTITY"] != null && ds.Tables[0].Rows[0]["PEOPLE_IDENTITY"].ToString() != "")
                {
                    model.PEOPLE_IDENTITY = ds.Tables[0].Rows[0]["PEOPLE_IDENTITY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["OCCUPATION"] != null && ds.Tables[0].Rows[0]["OCCUPATION"].ToString() != "")
                {
                    model.OCCUPATION = ds.Tables[0].Rows[0]["OCCUPATION"].ToString();
                }
                if (ds.Tables[0].Rows[0]["PERSONALITY"] != null && ds.Tables[0].Rows[0]["PERSONALITY"].ToString() != "")
                {
                    model.PERSONALITY = ds.Tables[0].Rows[0]["PERSONALITY"].ToString();
                }
                if (ds.Tables[0].Rows[0]["INTRODUCE"] != null && ds.Tables[0].Rows[0]["INTRODUCE"].ToString() != "")
                {
                    model.INTRODUCE = ds.Tables[0].Rows[0]["INTRODUCE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IDENTIFICATION_STATE"] != null && ds.Tables[0].Rows[0]["IDENTIFICATION_STATE"].ToString() != "")
                {
                    model.IDENTIFICATION_STATE = int.Parse(ds.Tables[0].Rows[0]["IDENTIFICATION_STATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SAME_HOBBY_PEOPLE"] != null && ds.Tables[0].Rows[0]["SAME_HOBBY_PEOPLE"].ToString() != "")
                {
                    model.SAME_HOBBY_PEOPLE = ds.Tables[0].Rows[0]["SAME_HOBBY_PEOPLE"].ToString();
                }
                if (ds.Tables[0].Rows[0]["EXP"] != null && ds.Tables[0].Rows[0]["EXP"].ToString() != "")
                {
                    model.EXP = int.Parse(ds.Tables[0].Rows[0]["EXP"].ToString());
                }
                if (ds.Tables[0].Rows[0]["SCORE"] != null && ds.Tables[0].Rows[0]["SCORE"].ToString() != "")
                {
                    model.SCORE = int.Parse(ds.Tables[0].Rows[0]["SCORE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["REALM_NAME"] != null && ds.Tables[0].Rows[0]["REALM_NAME"].ToString() != "")
                {
                    model.REALM_NAME = ds.Tables[0].Rows[0]["REALM_NAME"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ADDTIME"] != null && ds.Tables[0].Rows[0]["ADDTIME"].ToString() != "")
                {
                    model.ADDTIME = DateTime.Parse(ds.Tables[0].Rows[0]["ADDTIME"].ToString());
                }
                if (ds.Tables[0].Rows[0]["IS_RED"] != null && ds.Tables[0].Rows[0]["IS_RED"].ToString() != "")
                {
                    model.IS_RED = int.Parse(ds.Tables[0].Rows[0]["IS_RED"].ToString());
                }
                if (ds.Tables[0].Rows[0]["STATE"] != null && ds.Tables[0].Rows[0]["STATE"].ToString() != "")
                {
                    model.STATE = int.Parse(ds.Tables[0].Rows[0]["STATE"].ToString());
                }
                if (ds.Tables[0].Rows[0]["REMARK"] != null && ds.Tables[0].Rows[0]["REMARK"].ToString() != "")
                {
                    model.REMARK = ds.Tables[0].Rows[0]["REMARK"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Person_Desc"] != null && ds.Tables[0].Rows[0]["Person_Desc"].ToString() != "")
                {
                    model.Person_Desc = ds.Tables[0].Rows[0]["Person_Desc"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Person_NickName"] != null && ds.Tables[0].Rows[0]["Person_NickName"].ToString() != "")
                {
                    model.Person_NickName = ds.Tables[0].Rows[0]["Person_NickName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Porder"] != null && ds.Tables[0].Rows[0]["Porder"].ToString() != "")
                {
                    model.Porder = int.Parse(ds.Tables[0].Rows[0]["Porder"].ToString());
                }
                if (ds.Tables[0].Rows[0]["UpdateTime"] != null && ds.Tables[0].Rows[0]["UpdateTime"].ToString() != "")
                {
                    model.UpdateTime = DateTime.Parse(ds.Tables[0].Rows[0]["UpdateTime"].ToString());
                }
                if (ds.Tables[0].Rows[0]["PLAT_VAL"] != null && ds.Tables[0].Rows[0]["PLAT_VAL"].ToString() != "")
                {
                    model.PLAT_VAL = ds.Tables[0].Rows[0]["PLAT_VAL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["SPECIALTY_VAL"] != null && ds.Tables[0].Rows[0]["SPECIALTY_VAL"].ToString() != "")
                {
                    model.SPECIALTY_VAL = ds.Tables[0].Rows[0]["SPECIALTY_VAL"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IPURL"] != null && ds.Tables[0].Rows[0]["IPURL"].ToString() != "")
                {
                    model.IPURL = ds.Tables[0].Rows[0]["IPURL"].ToString();
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
            strSql.Append("select ID,USERID,USERNAME,PWD,TEL,TEL_STATE,USER_HEAD,SEX,EMAIL,USER_TYPE,ISCOMPLETE,PROVINCE,CITY,AREA,PEOPLE_IDENTITY,OCCUPATION,PERSONALITY,INTRODUCE,IDENTIFICATION_STATE,SAME_HOBBY_PEOPLE,EXP,SCORE,REALM_NAME,ADDTIME,IS_RED,STATE,REMARK,Person_Desc,Person_NickName,Porder,UpdateTime,PLAT_VAL,SPECIALTY_VAL,IPURL ");
            strSql.Append(" FROM RED_USER ");
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
            strSql.Append(" ID,USERID,USERNAME,PWD,TEL,TEL_STATE,USER_HEAD,SEX,EMAIL,USER_TYPE,ISCOMPLETE,PROVINCE,CITY,AREA,PEOPLE_IDENTITY,OCCUPATION,PERSONALITY,INTRODUCE,IDENTIFICATION_STATE,SAME_HOBBY_PEOPLE,EXP,SCORE,REALM_NAME,ADDTIME,IS_RED,STATE,REMARK,Person_Desc,Person_NickName,Porder,UpdateTime,PLAT_VAL,SPECIALTY_VAL,IPURL ");
            strSql.Append(" FROM RED_USER ");
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
            strSql.Append("select count(1) FROM RED_USER ");
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
            strSql.Append(")AS Row, T.*  from RED_USER T ");
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
            parameters[0].Value = "HRENH_THINK_USER";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  Method

        #region 更新用户头像
        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="tel">用户手机号</param>
        /// <param name="logoPath">更新后的头像的路径</param>
        /// <returns></returns>
        public bool UpdateLogo(string tel, string logoPath)
        {
            if (GetRecordCount(" tel='" + tel + "' ") > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update RED_USER set ");
                strSql.Append("User_Head=@User_Head");
                strSql.Append(" where tel=@tel");

                SqlParameter[] parameters = {
					new SqlParameter("@tel", SqlDbType.NVarChar,50),
					new SqlParameter("@User_Head", SqlDbType.VarChar,100)};
                parameters[0].Value = tel;
                parameters[1].Value = logoPath;

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
            else
            {
                return false;
            }
        }
        #endregion

    }
}

