using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewXzc.Common
{
    public class Recruit_Job
    {

        #region  表情
        public static string[,] os_face = new string[,]{

            {"[呲牙]","<img src=\"/template/images/emotion/qq/q_01.gif\" title=\"呲牙\" alt=\"呲牙\"/>"},
            {"[调皮]","<img src=\"/template/images/emotion/qq/q_02.gif\" title=\"调皮\" alt=\"调皮\"/>"},
            {"[流汗]","<img src=\"/template/images/emotion/qq/q_03.gif\" title=\"流汗\" alt=\"流汗\"/>"},
            {"[偷笑]","<img src=\"/template/images/emotion/qq/q_04.gif\" title=\"偷笑\" alt=\"偷笑\"/>"},
            {"[再见]","<img src=\"/template/images/emotion/qq/q_05.gif\" title=\"再见\" alt=\"再见\"/>"},
            {"[敲打]","<img src=\"/template/images/emotion/qq/q_06.gif\" title=\"敲打\" alt=\"敲打\"/>"},
            {"[闭嘴]","<img src=\"/template/images/emotion/qq/q_07.gif\" title=\"闭嘴\" alt=\"闭嘴\"/>"},
            {"[no]","<img src=\"/template/images/emotion/qq/q_08.gif\" title=\"no\" alt=\"no\"/>"},
            {"[玫瑰]","<img src=\"/template/images/emotion/qq/q_09.gif\" title=\"玫瑰\" alt=\"玫瑰\"/>"},
            {"[乒乓]","<img src=\"/template/images/emotion/qq/q_10.gif\" title=\"乒乓\" alt=\"乒乓\"/>"},
            {"[大哭]","<img src=\"/template/images/emotion/qq/q_11.gif\" title=\"大哭\" alt=\"大哭\"/>"},
            {"[嘘]","<img src=\"/template/images/emotion/qq/q_12.gif\" title=\"嘘\" alt=\"嘘\"/>"},
            {"[色]","<img src=\"/template/images/emotion/qq/q_13.gif\" title=\"色\" alt=\"色\"/>"},
            {"[酷]","<img src=\"/template/images/emotion/qq/q_14.gif\" title=\"酷\" alt=\"酷\"/>"},
            {"[折磨]","<img src=\"/template/images/emotion/qq/q_15.gif\" title=\"折磨\" alt=\"折磨\"/>"},
            {"[委屈]","<img src=\"/template/images/emotion/qq/q_16.gif\" title=\"委屈\" alt=\"委屈\"/>"},
            {"[便便]","<img src=\"/template/images/emotion/qq/q_17.gif\" title=\"便便\" alt=\"便便\"/>"},
            {"[炸弹]","<img src=\"/template/images/emotion/qq/q_18.gif\" title=\"炸弹\" alt=\"炸弹\"/>"},
            {"[菜刀]","<img src=\"/template/images/emotion/qq/q_19.gif\" title=\"菜刀\" alt=\"菜刀\"/>"},
            {"[可爱]","<img src=\"/template/images/emotion/qq/q_20.gif\" title=\"可爱\" alt=\"可爱\"/>"},
            {"[害羞]","<img src=\"/template/images/emotion/qq/q_21.gif\" title=\"害羞\" alt=\"害羞\"/>"},
            {"[得意]","<img src=\"/template/images/emotion/qq/q_22.gif\" title=\"得意\" alt=\"得意\"/>"},
            {"[吐]","<img src=\"/template/images/emotion/qq/q_23.gif\" title=\"吐\" alt=\"吐\"/>"},
            {"[微笑]","<img src=\"/template/images/emotion/qq/q_24.gif\" title=\"微笑\" alt=\"微笑\"/>"},
            {"[发怒]","<img src=\"/template/images/emotion/qq/q_25.gif\" title=\"发怒\" alt=\"发怒\"/>"},
           {"[尴尬]","<img src=\"/template/images/emotion/qq/q_26.gif\" title=\"尴尬\" alt=\"尴尬\"/>"},
            {"[惊恐]","<img src=\"/template/images/emotion/qq/q_27.gif\" title=\"惊恐\" alt=\"惊恐\"/>"},
            {"[冷汗]","<img src=\"/template/images/emotion/qq/q_28.gif\" title=\"冷汗\" alt=\"冷汗\"/>"},
            {"[爱心]","<img src=\"/template/images/emotion/qq/q_29.gif\" title=\"爱心\" alt=\"爱心\"/>"},
            {"[示爱]","<img src=\"/template/images/emotion/qq/q_30.gif\" title=\"示爱\" alt=\"示爱\"/>"},
            {"[白眼]","<img src=\"/template/images/emotion/qq/q_31.gif\" title=\"白眼\" alt=\"白眼\"/>"},
            {"[傲慢]","<img src=\"/template/images/emotion/qq/q_32.gif\" title=\"傲慢\" alt=\"傲慢\"/>"},
            {"[难过]","<img src=\"/template/images/emotion/qq/q_33.gif\" title=\"难过\" alt=\"难过\"/>"},
            {"[惊讶]","<img src=\"/template/images/emotion/qq/q_34.gif\" title=\"惊讶\" alt=\"惊讶\"/>"},
            {"[疑问]","<img src=\"/template/images/emotion/qq/q_35.gif\" title=\"疑问\" alt=\"疑问\"/>"},
            {"[睡]","<img src=\"/template/images/emotion/qq/q_36.gif\" title=\"睡\" alt=\"睡\"/>"},
            {"[亲亲]","<img src=\"/template/images/emotion/qq/q_37.gif\" title=\"亲亲\" alt=\"亲亲\"/>"},
            {"[憨笑]","<img src=\"/template/images/emotion/qq/q_38.gif\" title=\"憨笑\" alt=\"憨笑\"/>"},
            {"[衰]","<img src=\"/template/images/emotion/qq/q_39.gif\" title=\"衰\" alt=\"衰\"/>"},
            {"[撇嘴]","<img src=\"/template/images/emotion/qq/q_40.gif\" title=\"撇嘴\" alt=\"撇嘴\"/>"},
            {"[阴险]","<img src=\"/template/images/emotion/qq/q_41.gif\" title=\"阴险\" alt=\"阴险\"/>"},
            {"[奋斗]","<img src=\"/template/images/emotion/qq/q_42.gif\" title=\"奋斗\" alt=\"奋斗\"/>"},
            {"[发呆]","<img src=\"/template/images/emotion/qq/q_43.gif\" title=\"发呆\" alt=\"发呆\"/>"},
            {"[右哼哼]","<img src=\"/template/images/emotion/qq/q_44.gif\" title=\"右哼哼\" alt=\"右哼哼\"/>"},
            {"[拥抱]","<img src=\"/template/images/emotion/qq/q_45.gif\" title=\"拥抱\" alt=\"拥抱\"/>"},
            {"[坏笑]","<img src=\"/template/images/emotion/qq/q_46.gif\" title=\"坏笑\" alt=\"坏笑\"/>"},
            {"[鄙视]","<img src=\"/template/images/emotion/qq/q_47.gif\" title=\"鄙视\" alt=\"鄙视\"/>"},
            {"[晕]","<img src=\"/template/images/emotion/qq/q_48.gif\" title=\"晕\" alt=\"晕\"/>"},
            {"[大兵]","<img src=\"/template/images/emotion/qq/q_49.gif\" title=\"大兵\" alt=\"大兵\"/>"},
            {"[可怜]","<img src=\"/template/images/emotion/qq/q_50.gif\" title=\"可怜\" alt=\"可怜\"/>"},
            {"[强]","<img src=\"/template/images/emotion/qq/q_51.gif\" title=\"强\" alt=\"强\"/>"},
            {"[握手]","<img src=\"/template/images/emotion/qq/q_52.gif\" title=\"握手\" alt=\"握手\"/>"},
            {"[篮球]","<img src=\"/template/images/emotion/qq/q_53.gif\" title=\"篮球\" alt=\"篮球\"/>"},
            {"[抱拳]","<img src=\"/template/images/emotion/qq/q_54.gif\" title=\"抱拳\" alt=\"抱拳\"/>"},
            {"[凋谢]","<img src=\"/template/images/emotion/qq/q_55.gif\" title=\"凋谢\" alt=\"凋谢\"/>"},
            {"[饭]","<img src=\"/template/images/emotion/qq/q_56.gif\" title=\"饭\" alt=\"饭\"/>"},
            {"[蛋糕]","<img src=\"/template/images/emotion/qq/q_57.gif\" title=\"蛋糕\" alt=\"蛋糕\"/>"},
            {"[西瓜]","<img src=\"/template/images/emotion/qq/q_58.gif\" title=\"西瓜\" alt=\"西瓜\"/>"},
            {"[啤酒]","<img src=\"/template/images/emotion/qq/q_59.gif\" title=\"啤酒\" alt=\"啤酒\"/>"},
            {"[瓢虫]","<img src=\"/template/images/emotion/qq/q_60.gif\" title=\"瓢虫\" alt=\"瓢虫\"/>"},
            {"[勾引]","<img src=\"/template/images/emotion/qq/q_61.gif\" title=\"勾引\" alt=\"勾引\"/>"},
            {"[ok]","<img src=\"/template/images/emotion/qq/q_62.gif\" title=\"ok\" alt=\"ok\"/>"},
            {"[爱你]","<img src=\"/template/images/emotion/qq/q_63.gif\" title=\"爱你\" alt=\"爱你\"/>"},
            {"[咖啡]","<img src=\"/template/images/emotion/qq/q_64.gif\" title=\"咖啡\" alt=\"咖啡\"/>"},
            {"[月亮]","<img src=\"/template/images/emotion/qq/q_65.gif\" title=\"月亮\" alt=\"月亮\"/>"},
            {"[刀]","<img src=\"/template/images/emotion/qq/q_66.gif\" title=\"刀\" alt=\"刀\"/>"},
            {"[差劲]","<img src=\"/template/images/emotion/qq/q_67.gif\" title=\"差劲\" alt=\"差劲\"/>"},
            {"[拳头]","<img src=\"/template/images/emotion/qq/q_68.gif\" title=\"拳头\" alt=\"拳头\"/>"},
            {"[心碎]","<img src=\"/template/images/emotion/qq/q_69.gif\" title=\"心碎\" alt=\"心碎\"/>"},
            {"[太阳]","<img src=\"/template/images/emotion/qq/q_70.gif\" title=\"太阳\" alt=\"太阳\"/>"},
            {"[礼物]","<img src=\"/template/images/emotion/qq/q_71.gif\" title=\"礼物\" alt=\"礼物\"/>"},
            {"[足球]","<img src=\"/template/images/emotion/qq/q_72.gif\" title=\"足球\" alt=\"足球\"/>"},
            {"[骷髅]","<img src=\"/template/images/emotion/qq/q_73.gif\" title=\"骷髅\" alt=\"骷髅\"/>"},
            {"[闪电]","<img src=\"/template/images/emotion/qq/q_74.gif\" title=\"闪电\" alt=\"闪电\"/>"},
            {"[饥饿]","<img src=\"/template/images/emotion/qq/q_75.gif\" title=\"饥饿\" alt=\"饥饿\"/>"},
            {"[困]","<img src=\"/template/images/emotion/qq/q_76.gif\" title=\"困\" alt=\"困\"/>"},
            {"[吓]","<img src=\"/template/images/emotion/qq/q_77.gif\" title=\"吓\" alt=\"吓\"/>"},
            {"[弱]","<img src=\"/template/images/emotion/qq/q_78.gif\" title=\"弱\" alt=\"弱\"/>"},
            {"[抠鼻]","<img src=\"/template/images/emotion/qq/q_79.gif\" title=\"抠鼻\" alt=\"抠鼻\"/>"},
            {"[鼓掌]","<img src=\"/template/images/emotion/qq/q_80.gif\" title=\"鼓掌\" alt=\"鼓掌\"/>"},
            {"[糗大了]","<img src=\"/template/images/emotion/qq/q_81.gif\" title=\"糗大了\" alt=\"糗大了\"/>"},
            {"[左哼哼]","<img src=\"/template/images/emotion/qq/q_82.gif\" title=\"左哼哼\" alt=\"左哼哼\"/>"},
            {"[哈欠]","<img src=\"/template/images/emotion/qq/q_83.gif\" title=\"哈欠\" alt=\"哈欠\"/>"},
            {"[快哭了]","<img src=\"/template/images/emotion/qq/q_84.gif\" title=\"快哭了\" alt=\"快哭了\"/>"},
            
            
            
            
            

            {"[kiss]","<img src=\"/template/images/emotion/no/n_01.gif\" title=\"kiss\" alt=\"kiss\"//>"},
            {"[mua]","<img src=\"/template/images/emotion/no/n_02.gif\" title=\"mua\" alt=\"mua\"/>"},
            {"[新年快乐]","<img src=\"/template/images/emotion/no/n_03.gif\" title=\"新年快乐\" alt=\"新年快乐\"/>"},
            {"[打哈欠]","<img src=\"/template/images/emotion/no/n_04.gif\" title=\"打哈欠\" alt=\"打哈欠\"/>"},
            {"[大礼包]","<img src=\"/template/images/emotion/no/n_05.gif\" title=\"大礼包\" alt=\"大礼包\"/>"},
            {"[放屁]","<img src=\"/template/images/emotion/no/n_06.gif\" title=\"放屁\" alt=\"放屁\"/>"},
            {"[尴尬]","<img src=\"/template/images/emotion/no/n_07.gif\" title=\"尴尬\" alt=\"尴尬\"/>"},
            {"[害羞]","<img src=\"/template/images/emotion/no/n_08.gif\" title=\"害羞\" alt=\"害羞\"/>"},
            {"[娇羞]","<img src=\"/template/images/emotion/no/n_09.gif\" title=\"娇羞\" alt=\"娇羞\"/>"},
            {"[开心]","<img src=\"/template/images/emotion/no/n_10.gif\" title=\"开心\" alt=\"kiss\"/>"},
            {"[看不见我]","<img src=\"/template/images/emotion/no/n_11.gif\" title=\"看不见我\" alt=\"看不见我\"/>"},
            {"[哭]","<img src=\"/template/images/emotion/no/n_12.gif\" title=\"哭\" alt=\"哭\"/>"},
            {"[来呀来呀]","<img src=\"/template/images/emotion/no/n_13.gif\" title=\"来呀来呀\" alt=\"来呀来呀\"/>"},
            {"[卖帅]","<img src=\"/template/images/emotion/no/n_14.gif\" title=\"卖帅\" alt=\"卖帅\"/>"},
            {"[扭]","<img src=\"/template/images/emotion/no/n_15.gif\" title=\"扭\" alt=\"扭\"/>"},
            {"[抛小球]","<img src=\"/template/images/emotion/no/n_16.gif\" title=\"抛小球\" alt=\"抛小球\"/>"},
            {"[跑步]","<img src=\"/template/images/emotion/no/n_17.gif\" title=\"跑步\" alt=\"跑步\"/>"},
            {"[生病]","<img src=\"/template/images/emotion/no/n_18.gif\" title=\"生病\" alt=\"生病\"/>"},
            {"[圣诞节]","<img src=\"/template/images/emotion/no/n_19.gif\" title=\"圣诞节\" alt=\"圣诞节\"/>"},
            {"[水汪汪]","<img src=\"/template/images/emotion/no/n_20.gif\" title=\"水汪汪\" alt=\"水汪汪\"/>"},
            {"[睡觉]","<img src=\"/template/images/emotion/no/n_21.gif\" title=\"睡觉\" alt=\"睡觉\"/>"},     
            {"[挑逗]","<img src=\"/template/images/emotion/no/n_22.gif\" title=\"挑逗\" alt=\"挑逗\"/>"},
            {"[跳舞]","<img src=\"/template/images/emotion/no/n_23.gif\" title=\"跳舞\" alt=\"跳舞\"/>"},
            {"[无语]","<img src=\"/template/images/emotion/no/n_24.gif\" title=\"无语\" alt=\"无语\"/>"},
            {"[心心眼]","<img src=\"/template/images/emotion/no/n_25.gif\" title=\"心心眼\" alt=\"心心眼\"/>"},
            {"[星星眼]","<img src=\"/template/images/emotion/no/n_26.gif\" title=\"星星眼\" alt=\"星星眼\"/>"},
            {"[摇手指]","<img src=\"/template/images/emotion/no/n_27.gif\" title=\"摇手指\" alt=\"摇手指\"/>"},
            {"[悠哉狗]","<img src=\"/template/images/emotion/no/n_28.gif\" title=\"悠哉狗\" alt=\"悠哉狗\"/>"},
            {"[晕]","<img src=\"/template/images/emotion/no/n_29.gif\" title=\"晕\" alt=\"晕\"/>"},
            {"[眨眼]","<img src=\"/template/images/emotion/no/n_30.gif\" title=\"眨眼\" alt=\"眨眼\"/>"},
            {"[转圈圈]","<img src=\"/template/images/emotion/no/n_31.gif\" title=\"转圈圈\" alt=\"转圈圈\"/>"},
            {"[巴掌]","<img src=\"/template/images/emotion/xs/x_01.gif\" title=\"巴掌\" alt=\"巴掌\"/>"},
            {"[奔跑]","<img src=\"/template/images/emotion/xs/x_02.gif\" title=\"奔跑\" alt=\"奔跑\"/>"},
            {"[扯]","<img src=\"/template/images/emotion/xs/x_03.gif\" title=\"扯\" alt=\"扯\"/>"},
            {"[出浴]","<img src=\"/template/images/emotion/xs/x_04.gif\" title=\"出浴\" alt=\"出浴\"/>"},
            {"[弹跳]","<img src=\"/template/images/emotion/xs/x_05.gif\" title=\"弹跳\" alt=\"弹跳\"/>"},
            {"[瞪眼]","<img src=\"/template/images/emotion/xs/x_06.gif\" title=\"瞪眼\" alt=\"瞪眼\"/>"},
            {"[飞吻]","<img src=\"/template/images/emotion/xs/x_07.gif\" title=\"飞吻\" alt=\"飞吻\"/>"},
            {"[好饱]","<img src=\"/template/images/emotion/xs/x_08.gif\" title=\"好饱\" alt=\"好饱\"/>"},
            {"[嘿哈]","<img src=\"/template/images/emotion/xs/x_09.gif\" title=\"嘿哈\" alt=\"嘿哈\"/>"},
            {"[举哑铃]","<img src=\"/template/images/emotion/xs/x_10.gif\" title=\"举哑铃\" alt=\"举哑铃\"/>"},
            {"[练腰]","<img src=\"/template/images/emotion/xs/x_11.gif\" title=\"练腰\" alt=\"练腰\"/>"},
            {"[凌乱]","<img src=\"/template/images/emotion/xs/x_12.gif\" title=\"凌乱\" alt=\"凌乱\"/>"},
            {"[挠痒]","<img src=\"/template/images/emotion/xs/x_13.gif\" title=\"挠痒\" alt=\"挠痒\"/>"},
            {"[拍脸]","<img src=\"/template/images/emotion/xs/x_14.gif\" title=\"拍脸\" alt=\"拍脸\"/>"},
            {"[拍手]","<img src=\"/template/images/emotion/xs/x_15.gif\" title=\"拍手\" alt=\"拍手\"/>"},
            {"[跑]","<img src=\"/template/images/emotion/xs/x_16.gif\" title=\"跑\" alt=\"跑\"/>"},
            {"[皮]","<img src=\"/template/images/emotion/xs/x_17.gif\" title=\"皮\" alt=\"皮\"/>"},
            {"[飘忽]","<img src=\"/template/images/emotion/xs/x_18.gif\" title=\"飘忽\" alt=\"飘忽\"/>"},
            {"[揉眼]","<img src=\"/template/images/emotion/xs/x_19.gif\" title=\"揉眼\" alt=\"揉眼\"/>"},
            {"[撒娇]","<img src=\"/template/images/emotion/xs/x_20.gif\" title=\"撒娇\" alt=\"撒娇\"/>"},
            {"[踏步]","<img src=\"/template/images/emotion/xs/x_21.gif\" title=\"踏步\" alt=\"踏步\"/>"},
            {"[跳]","<img src=\"/template/images/emotion/xs/x_22.gif\" title=\"跳\" alt=\"跳\"/>"},
            {"[兴奋]","<img src=\"/template/images/emotion/xs/x_23.gif\" title=\"兴奋\" alt=\"兴奋\"/>"},
            {"[仰卧起坐]","<img src=\"/template/images/emotion/xs/x_24.gif\" title=\"仰卧起坐\" alt=\"仰卧起坐\"/>"},
            {"[转圈]","<img src=\"/template/images/emotion/xs/x_25.gif\" title=\"转圈\" alt=\"转圈\"/>"},
            {"[啦啦啦]","<img src=\"/template/images/emotion/xs/x_26.gif\" title=\"啦啦啦\" alt=\"啦啦啦\"/>"},
            {"[地球一小时]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/dc/earth1r_thumb.gif\" title=\"地球一小时\" alt=\"地球一小时\"/>"},
            {"[许愿]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/87/lxhxuyuan_thumb.gif\" title=\"许愿\" alt=\"许愿\"/>"},
            {"[泪流满面]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/64/lxhtongku_thumb.gif\" title=\"泪流满面\" alt=\"泪流满面\"/>"},
            {"[马到成功]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/30/madaochenggong_thumb.gif\" title=\"马到成功\" alt=\"马到成功\"/>"},
            {"[江南style]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/67/gangnamstyle_thumb.gif\" title=\"江南style\" alt=\"江南style\"/>"},
            {"[偷乐]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/fa/lxhtouxiao_thumb.gif\" title=\"偷乐\" alt=\"偷乐\"/>"},
            {"[得意地笑]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/d4/lxhdeyidixiao_thumb.gif\" title=\"得意地笑\" alt=\"得意地笑\"/>"},
            {"[炸鸡和啤酒]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/f4/zhaji_thumb.gif\" title=\"炸鸡和啤酒\" alt=\"炸鸡和啤酒\"/>"},
            {"[xkl转圈]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/f4/xklzhuanquan_thumb.gif\" title=\"xkl转圈\" alt=\"xkl转圈\"/>"},
            {"[lt切克闹]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/73/ltqiekenao_thumb.gif\" title=\"lt切克闹\" alt=\"lt切克闹\"/>"},
            {"[din推撞]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/dd/dintuizhuang_thumb.gif\" title=\"din推撞\" alt=\"din推撞\"/>"},
            {"[风扇]","<img src=\"http://img.t.sinajs.cn/t4/appstyle/expression/ext/normal/92/fan.gif\" title=\"风扇\" alt=\"风扇\"/>"},
            {"[啦啦啦]","<img src=\"/template/images/emotion/xs/x_26.gif\" title=\"啦啦啦\" alt=\"啦啦啦\"/>"},






       
        };
        #endregion


        /// <summary>
        /// 定义薪资待遇数组
        /// </summary>
        public static string[] CompanyScaleArry = new string[]{
        "面议","2K以下","2K-4K","4K-6K","6K-10K","10K-15K","15K-20K","20K-30K","30K以上"
        };
        /// <summary>
        /// 工作性质
        /// </summary>
        public static string[] WorkType = new string[]{
        "全职","兼职","实习"
        };
        /// <summary>
        /// 求职状态
        /// </summary>
        public static string[] Job_StateArry = new string[]{
         "我目前离职，可随时到岗","我目前在职，考虑换个环境，1个月之内到岗","暂无跳槽打算","我是应届毕业生"
        };
        /// <summary>
        /// 定义职位性质数组
        /// </summary>
        public static string[] PropertyArry = new string[]{
         "全职","兼职","实习"
        };
        /// <summary>
        /// 工作经验类别数组 
        /// </summary>
        public static string[] ExpArry = new string[] {
         "不限","1年以下","1-3年","3-5年","5-10年","10年以上"
        };
        /// <summary>
        /// 学历数组
        /// </summary>
        public static string[] RecuritArry = new string[] {
         "不限","初中","高中","专科","本科","硕士","博士及以上","MBA","EMBA","其他"
        };
        /// <summary>
        /// 定义企业性质数组
        /// </summary>
        public static string[] CompanyPropertyArry = new string[]{
         "外资(欧美)","外资(非欧美)","合资(欧美)","合资(非欧美)","国企","民营公司","外企代表处","政府机关","事业单位","非营利机构","股份制企业","上市公司","国家机关","其他性质"
        };
        /// <summary>
        /// 定义企业规模数组
        /// </summary>
        public static string[] CompanyScopeArry = new string[]{
         "20人以下","20-99人","100-499人","500-999人","1000-9999人","10000人以上"
        };
        /// <summary>
        /// 定义平均工资数组
        /// </summary>
        public static string[] CompanyAvgScaleArry = new string[]{
         "保密","2K以下","2K-3K","3K-5K","5K-7K","7K-10K","10K-15K","15K-20K","20K以上"
        };
        /// <summary>
        /// 投递时间数组
        /// </summary>
        public static string[] SendTimeArry = new string[] {
         "全部","最近三天","一周以内","一个月以内","三个月以内"
        };
        /// <summary>
        /// 定义性别数组
        /// </summary>
        public static string[] SexArry = new string[]{
         "不限","男","女"
        };
        /// <summary>
        /// 简历状态数组
        /// </summary>
        public static string[] Resume_StateArry = new string[]{
            "发送面试函","已阅","已发面试函","面试中","面试通过","面试未通过"
        };


        /// <summary>
        /// 个人等级名称对应的简称
        /// </summary>
        public static string[] PersonLvArry = new string[]{
            "瓷碗会员","铁碗会员","铜碗会员","银碗会员","金碗会员"
        };

        /// <summary>
        /// 个人等级名称对应的全称
        /// </summary>
        public static string[] PersonAllLvArry = new string[]{
            "瓷饭碗会员","铁饭碗会员","铜饭碗会员","银饭碗会员","金饭碗会员"
        };

        /// <summary>
        /// 企业等级名称对应的简称
        /// </summary>
        public static string[] CompanyLvArry = new string[]{
            "男爵企业","子爵企业","伯爵企业","候爵企业","公爵企业"
        };

        /// <summary>
        /// 企业等级名称对应的全称
        /// </summary>
        public static string[] CompanyAllLvArry = new string[]{
            "男爵企业","子爵企业","伯爵企业","候爵企业","公爵企业"
        };

        /// <summary>
        /// 猎头等级名称对应的简称
        /// </summary>
        public static string[] HuntLvArry = new string[]{
            "伯乐新人","专业伯乐","资深伯乐","顶级伯乐","骨灰伯乐"
        };

        /// <summary>
        /// 猎头等级名称对应的全称
        /// </summary>
        public static string[] HuntAllLvArry = new string[]{
            "伯乐新人","专业伯乐","资深伯乐","顶级伯乐","骨灰伯乐"
        };

        /// <summary>
        /// 个人隐私下的简历的可见状态
        /// </summary>
        public static string[] PersonSecretArry = new string[]{
            "完全保密","仅互粉好友可见"
        };

        /// <summary>
        /// 职位发布状态
        /// </summary>
        public static string[] JobFbStateArry = new string[]{
            "全部职位","未发布职位","已暂停职位","发布结束职位","未通过审核职位"
        };

        /// <summary>
        /// 面试结果
        /// </summary>
        public static string[] JobResultArry = new string[]{
            "确定通过","感觉靠谱","感觉没戏","确定未通过"
        };

        /// <summary>
        /// 面试难度
        /// </summary>
        public static string[] JobHardArry = new string[]{
            "很容易","一般","有难度","困难","巨难"
        };

        /// <summary>
        /// 面试感受
        /// </summary>
        public static string[] JobFeelArry = new string[]{
            "不好","一般","很好"
        };


        /// <summary>
        /// 公司发展阶段
        /// </summary>
        public static string[] RecruitStageArry = new string[]{
            "未融资","天使轮","A轮","B轮","C轮","D轮及以上","上市公司","不需要融资"
        };

        /// <summary>
        /// 企业评价普通简历或多维简历的状态
        /// </summary>
        public static string[] RecruitReviewArry = new string[]{
            "未评价","已评价"
        };

        /// <summary>
        /// 企业创建服务的招聘周期
        /// </summary>
        public static string[] RecruitTimeArry = new string[]{
            "一周","二周","三周","一个月"
        };

        /// <summary>
        /// 红人议会中需要添加列表的文章（红人动态、议会活动、公告）
        /// </summary>
        public static string[] ArticleTypeArry = new string[]{
            "红人动态","议会活动","公告"
        };


        /// <summary>
        /// 根据所传类型获取职位的性质或经验
        /// </summary>
        /// <param name="Rtype">资源类型（1:职位性质,2:工作经验，3：公司性质，4:企业规模，5：平均工资，6：性别，7：企业薪资待遇，8：学历，9：简历状态），10：个人隐私下的简历的可见状态，11：面试结果（0：确定通过，1：感觉靠谱，2：感觉没戏，3：确定未通过），12：面试难度（0：很容易，1：一般，2：有难度，3：困难，4：巨难），13：面试感受（0：不好，1：一般，2：很好），14：公司发展阶段，15：企业评价普通简历或多维简历的状态，16：企业创建服务的招聘周期，17：红人议会中需要添加列表的文章（红人动态、议会活动、公告）</param>
        /// <param name="Num">性质或经验对应的数值</param>
        /// <returns>返回相关值</returns>
        public static string ReturnPropertyOrExp(int Rtype, int Num)
        {
            string value = "";
            if (Rtype == 1)//职位性质
            {
                if (Num < PropertyArry.Length && Num >= 0)
                {
                    value = PropertyArry[Num];
                }
            }
            else if (Rtype == 2)//工作经验
            {
                if (Num < ExpArry.Length && Num >= 0)
                {
                    value = ExpArry[Num];
                }

            }
            else if (Rtype == 3)//公司性质
            {
                if (Num < CompanyPropertyArry.Length && Num >= 0)
                {
                    value = CompanyPropertyArry[Num];
                }

            }
            else if (Rtype == 4)//企业规模
            {
                if (Num < CompanyScopeArry.Length && Num >= 0)
                {
                    value = CompanyScopeArry[Num];
                }

            }
            else if (Rtype == 5)//平均工资
            {
                if (Num < CompanyAvgScaleArry.Length && Num >= 0)
                {
                    value = CompanyAvgScaleArry[Num];
                }

            }
            else if (Rtype == 6)//性别
            {
                if (Num < SexArry.Length && Num >= 0)
                {
                    value = SexArry[Num];
                }

            }
            else if (Rtype == 7)//企业薪资待遇
            {
                if (Num < CompanyScaleArry.Length && Num >= 0)
                {
                    value = CompanyScaleArry[Num];
                }

            }
            else if (Rtype == 8)//学历
            {
                if (Num < RecuritArry.Length && Num >= 0)
                {
                    value = RecuritArry[Num];
                }

            }
            else if (Rtype == 9)//简历状态
            {
                if (Num < Resume_StateArry.Length && Num >= 0)
                {
                    value = Resume_StateArry[Num];
                }

            }
            else if (Rtype == 10)//个人隐私下的简历的可见状态
            {
                if (Num < PersonSecretArry.Length && Num >= 0)
                {
                    value = PersonSecretArry[Num];
                }

            }
            else if (Rtype == 11)//面试结果
            {
                if (Num < JobResultArry.Length && Num >= 0)
                {
                    value = JobResultArry[Num];
                }

            }
            else if (Rtype == 12)//面试难度
            {
                if (Num < JobHardArry.Length && Num >= 0)
                {
                    value = JobHardArry[Num];
                }

            }
            else if (Rtype == 13)//面试感受
            {
                if (Num < JobFeelArry.Length && Num >= 0)
                {
                    value = JobFeelArry[Num];
                }

            }
            else if (Rtype == 14)//公司发展阶段
            {
                if (Num < RecruitStageArry.Length && Num >= 0)
                {
                    value = RecruitStageArry[Num];
                }

            }
            else if (Rtype == 15)//企业评价普通简历或多维简历的状态
            {
                if (Num < RecruitReviewArry.Length && Num >= 0)
                {
                    value = RecruitReviewArry[Num];
                }

            }
            else if (Rtype == 16)//企业创建服务的招聘周期
            {
                if (Num < RecruitTimeArry.Length && Num >= 0)
                {
                    value = RecruitTimeArry[Num];
                }

            }
            else if (Rtype == 17)//红人议会中需要添加列表的文章（红人动态、议会活动、公告）
            {
                if (Num < ArticleTypeArry.Length && Num >= 0)
                {
                    value = ArticleTypeArry[Num];
                }

            }
            return value;
        }

        /// <summary>
        /// 获取当前类别的名称的简称
        /// </summary>
        /// <param name="lv_num">等级数值</param>
        /// <param name="lv_type">等级类别，1：个人，2：企业，3：猎头</param>
        /// <returns></returns>
        public static string ReturnLvShortName(int lv_num, int lv_type)
        {
            string lv_name = "";
            int lvnum = lv_num;

            if (lvnum < 1)
            {
                lvnum = 0;
            }
            else if (lvnum > 5)
            {
                lvnum = 4;
            }

            lvnum = lvnum - 1;

            if (lv_type == 1)
            {
                lv_name = PersonLvArry[lvnum];
            }
            else if (lv_type == 2)
            {
                lv_name = CompanyLvArry[lvnum];
            }
            else if (lv_type == 3)
            {
                lv_name = HuntLvArry[lvnum];
            }

            return lv_name;

        }


        /// <summary>
        /// 获取当前类别的名称的全称
        /// </summary>
        /// <param name="lv_num">等级数值</param>
        /// <param name="lv_type">等级类别，1：个人，2：企业，3：猎头</param>
        /// <returns></returns>
        public static string ReturnLvAllName(int lv_num, int lv_type)
        {
            string lv_name = "";
            int lvnum = lv_num;

            if (lvnum < 1)
            {
                lvnum = 0;
            }
            else if (lvnum > 5)
            {
                lvnum = 4;
            }

            lvnum = lvnum - 1;

            if (lv_type == 1)
            {
                lv_name = PersonAllLvArry[lvnum];
            }
            else if (lv_type == 2)
            {
                lv_name = CompanyAllLvArry[lvnum];
            }
            else if (lv_type == 3)
            {
                lv_name = HuntAllLvArry[lvnum];
            }

            return lv_name;

        }


        /// <summary>
        /// 返回用户的工作经验
        /// </summary>
        /// <param name="jobstarttime">开始工作时间</param>
        /// <returns></returns>
        public static string ReturnPersonWorkExp(string jobstarttime)
        {
            string timestr = "1个月";

            try
            {

                DateTime exrp = Convert.ToDateTime(jobstarttime);
                DateTime nowdt = DateTime.Now;
                TimeSpan ts = nowdt - exrp;
                //Response.Write("相差的天数"+ts.Days+"<br>");
                //Response.Write("相差的总时间 用天数表示"+ts.TotalDays + "<br>");


                int totaldays = Convert.ToInt32(ts.TotalDays);
                int numdays = 365;
                if (totaldays % numdays == 0)
                {
                    timestr = (totaldays / numdays).ToString();
                    if (timestr != "0")
                    {
                        timestr += "年";
                    }
                    else
                    {
                        timestr = "";
                    }
                }
                else
                {

                    timestr = (totaldays / numdays).ToString();
                    if (timestr != "0")
                    {
                        timestr += "年";
                    }
                    else
                    {
                        timestr = "";
                    }
                    int days = Convert.ToInt32(totaldays % numdays);
                    int monthnum = 30;
                    if (days % monthnum == 0)
                    {
                        if (timestr != "")
                        {
                            timestr += "零" + (days / monthnum).ToString() + "个月";
                        }
                        else
                        {
                            timestr = (days / monthnum).ToString() + "个月";
                        }

                    }
                    else
                    {
                        if (timestr != "")
                        {
                            timestr += "零" + (days / monthnum + 1).ToString() + "个月";
                        }
                        else
                        {
                            timestr = (days / monthnum + 1).ToString() + "个月";
                        }

                    }
                }

                if (timestr == "" || jobstarttime.IndexOf("1900") >= 0)
                {
                    timestr = "无";
                }
            }
            catch (Exception ex)
            {
                timestr = "无";
            }

            if (jobstarttime.IndexOf("1900") >= 0)
            {
                timestr = "无";
            }


            return timestr;
        }


        /// <summary>
        /// 获取money类型的数据去除多余的0之后的值，如：0.00
        /// </summary>
        /// <param name="money"></param>
        /// <param name="type">操作类型，1：如果当前值为0，则返回""，2：如果当前值为0，则返回0</param>
        /// <returns></returns>
        public static int Return_Money_Int(string money)
        {
            if (string.IsNullOrEmpty(money))
            {
                money = "";
            }
            else
            {
                if (money.IndexOf(".") >= 0)
                {
                    money = money.TrimEnd('0').TrimEnd('.');
                }
                
            }

            if (money == "")
            {
                money = "0";
            }

            if (money.IndexOf(".") >= 0)
            {
                money = money.Substring(0, money.IndexOf("."));
            }

            return Convert.ToInt32(money);
        }


        /// <summary>
        /// 获取money类型的数据去除多余的0之后的值，如：0.00
        /// </summary>
        /// <param name="money"></param>
        /// <param name="type">操作类型，1：如果当前值为0，则返回""，2：如果当前值为0，则返回0</param>
        /// <returns></returns>
        public static string Return_Money(string money, int type)
        {
            if (string.IsNullOrEmpty(money))
            {
                money = "";
            }
            else
            {
                if (money.IndexOf(".") >= 0)
                {
                    money = money.TrimEnd('0').TrimEnd('.');
                }
            }

            if (type == 1)
            {
                if (money == "0")
                {
                    money = "";
                }
            }
            else
            {
                if (money == "")
                {
                    money = "0";
                }
                else
                {
                    if(money.IndexOf(".")>=0)
                    {
                        string[] arr = money.Split('.');
                        if (arr[1].Length > 3)
                        {
                            arr[1] = arr[1].ToString().Substring(0,3);
                            money = arr[0].ToString() + "." + arr[1].ToString();
                        }
                    }
                    
                }
            }

            return money;
        }


        /// <summary>
        /// 获取money类型的数据去除多余的0之后的值，如：0.00
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string Return_Money(string money)
        {
            if (string.IsNullOrEmpty(money))
            {
                money = "";
            }
            else
            {
                if (money.IndexOf(".") >= 0)
                {
                    money = money.TrimEnd('0').TrimEnd('.');
                }
            }

            if (money == "0")
            {
                money = "";
            }

            return money;
        }


        /// <summary>
        /// 将money类型的数据后面多余的0去掉
        /// </summary>
        /// <param name="money">money类型的变量</param>
        /// <returns></returns>
        public static string GetMoey(string money)
        {
            if (string.IsNullOrEmpty(money))
            {
                money = "";
            }
            else
            {
                if (money.IndexOf(".") >= 0)
                {
                    money = money.TrimEnd('.').TrimEnd('0');

                    string[] arr = money.Split('.');
                    if (arr[1] == "")
                    {
                        money = money.Substring(0, money.Length - 1);
                    }
                }
            }

            return money;
        }


        /// <summary>
        /// 根据用户类型
        /// </summary>
        /// <param name="user_type">用户类型，1：个人，2：企业，3：猎头</param>
        /// <param name="realms_name">域名</param>
        /// <returns></returns>
        public static string GetHomePageUrl(int user_type, string realms_name)
        {
            string url = "";
            if (user_type > 0 && user_type != 2)
            {
                url = "/people_h/"+realms_name+"/trends";
            }
            else
            {
                url = "/company_h/" + realms_name ;
            }

            return url;
        }

    }
}
