using System;
namespace NewXzc.Model
{
	/// <summary>
	/// RED_USER:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class RED_USER
	{
		public RED_USER()
		{}
        #region Model
        private int _id;
        private int _userid;
        private string _username;
        private string _pwd;
        private string _tel;
        private int _tel_state;
        private string _user_head;
        private int? _sex = 0;
        private string _email;
        private int _user_type = 0;
        private int _iscomplete;
        private string _province;
        private string _city;
        private string _area;
        private string _people_identity;
        private string _occupation;
        private string _personality;
        private string _introduce;
        private int? _identification_state;
        private string _same_hobby_people;
        private int? _exp;
        private int? _score;
        private string _realm_name;
        private DateTime? _addtime;
        private int _is_red = 0;
        private int _state = 0;
        private string _remark;
        private string _person_desc = "";
        private string _person_nickname = "";
        private int? _porder = 0;
        private DateTime? _updatetime = DateTime.Now;
        private string _plat_val = "";
        private string _specialty_val;
        private string _ipurl = "";
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 注册用户ID
        /// </summary>
        public int USERID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户昵称（姓名）
        /// </summary>
        public string USERNAME
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string PWD
        {
            set { _pwd = value; }
            get { return _pwd; }
        }
        /// <summary>
        /// 手机号
        /// </summary>
        public string TEL
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 手机认证状态，0：未认证，1：已认证
        /// </summary>
        public int TEL_STATE
        {
            set { _tel_state = value; }
            get { return _tel_state; }
        }
        /// <summary>
        /// 用户头像地址
        /// </summary>
        public string USER_HEAD
        {
            set { _user_head = value; }
            get { return _user_head; }
        }
        /// <summary>
        /// 用户性别，1：男，2：女
        /// </summary>
        public int? SEX
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 邮箱（备用）
        /// </summary>
        public string EMAIL
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 用户注册途径，0：红人议会，1：红人汇，2：红人汇手机端，3：微吧，4：微吧手机端，5：红人爱品
        /// </summary>
        public int USER_TYPE
        {
            set { _user_type = value; }
            get { return _user_type; }
        }
        /// <summary>
        /// 用户信息是否已经完善，0：否，1：是
        /// </summary>
        public int ISCOMPLETE
        {
            set { _iscomplete = value; }
            get { return _iscomplete; }
        }
        /// <summary>
        /// 用户所在地区，省
        /// </summary>
        public string PROVINCE
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 用户所在地区，市
        /// </summary>
        public string CITY
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 用户所在地区，区/县
        /// </summary>
        public string AREA
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 用户身份，可多选，以“,”间隔
        /// </summary>
        public string PEOPLE_IDENTITY
        {
            set { _people_identity = value; }
            get { return _people_identity; }
        }
        /// <summary>
        /// 用户职业，可多选，以“,”间隔
        /// </summary>
        public string OCCUPATION
        {
            set { _occupation = value; }
            get { return _occupation; }
        }
        /// <summary>
        /// 用户个性，可多选，以“,”间隔
        /// </summary>
        public string PERSONALITY
        {
            set { _personality = value; }
            get { return _personality; }
        }
        /// <summary>
        /// 个人介绍
        /// </summary>
        public string INTRODUCE
        {
            set { _introduce = value; }
            get { return _introduce; }
        }
        /// <summary>
        /// 用户认证状态，0：未认证，1：已认证
        /// </summary>
        public int? IDENTIFICATION_STATE
        {
            set { _identification_state = value; }
            get { return _identification_state; }
        }
        /// <summary>
        /// 感兴趣的人
        /// </summary>
        public string SAME_HOBBY_PEOPLE
        {
            set { _same_hobby_people = value; }
            get { return _same_hobby_people; }
        }
        /// <summary>
        /// 经验
        /// </summary>
        public int? EXP
        {
            set { _exp = value; }
            get { return _exp; }
        }
        /// <summary>
        /// 积分
        /// </summary>
        public int? SCORE
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 域名
        /// </summary>
        public string REALM_NAME
        {
            set { _realm_name = value; }
            get { return _realm_name; }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime? ADDTIME
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 是否红人，0：否，1：是
        /// </summary>
        public int IS_RED
        {
            set { _is_red = value; }
            get { return _is_red; }
        }
        /// <summary>
        /// 用户状态，0：正常，1：被删除
        /// </summary>
        public int STATE
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string REMARK
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 个人详细介绍
        /// </summary>
        public string Person_Desc
        {
            set { _person_desc = value; }
            get { return _person_desc; }
        }
        /// <summary>
        /// 用户头衔
        /// </summary>
        public string Person_NickName
        {
            set { _person_nickname = value; }
            get { return _person_nickname; }
        }
        /// <summary>
        /// 红人序号
        /// </summary>
        public int? Porder
        {
            set { _porder = value; }
            get { return _porder; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 平台，可多选，以“,”间隔
        /// </summary>
        public string PLAT_VAL
        {
            set { _plat_val = value; }
            get { return _plat_val; }
        }
        /// <summary>
        /// 红人特长，可多选，以“,”间隔
        /// </summary>
        public string SPECIALTY_VAL
        {
            set { _specialty_val = value; }
            get { return _specialty_val; }
        }
        /// <summary>
        /// 红人爱品地址
        /// </summary>
        public string IPURL
        {
            set { _ipurl = value; }
            get { return _ipurl; }
        }
        #endregion Model

	}
}

