//邮箱
var reg_email = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/i;

//判断是否是正整数的正则表达式
var reg_num = /^\d*$/;

//判断是否是非0正整数的正则表达式
var reg_non_negative_integer = /^[1-9][0-9]{0,}$/

//判断是否是正整数（QQ）的正则表达式
var reg_qq = /^[1-9]\d{4,8}$/;

//判断是否是正整数（邮编）的正则表达式
var reg_ycode = /^[0-9][0-9]{5}$/;

//判断座机号格式是否正确的正则表达式
var reg_tel = /^[+]{0,1}(\d){1,4}[ ]{0,1}([-]{0,1}((\d)|[ ]){1,12})+$/;

//验证带有国家区号的座机号格式，不包含分机号
var reg_country_tel = /^((\d){3,}-){0,1}(\d){1,4}[ ]{0,1}(-(\d){7,8})$/;

//验证国家区号以0打头，的3-4位数字
var reg_country_area = /^(([0\+]\d{2,3})?(0\d{2,3}))$/;

//验证带有国家区号的座机号格式，包含分机号
var reg_country = /^(([0\+]\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/;

//判断手机号格式是否正确的正则表达式
var reg_phone = /^(13[0-9]{9})|(15[0-9]{9})|(170[0-9]{8})|(176[0-9]{8})|(177[0-9]{8})|(178[0-9]{8})|(18[0-9]{9})$/;

//验证传真
var reg_fax = /^(\d{3,4}-)?\d{7,8}$/;

//判断手机号格式是否正确的正则表达式（支付宝账户支持绑定的大陆手机号段有哪些？）
var reg_phone_zhifubao = /^(13[0-9]{9})|(145[0-9]{8})|(147[0-9]{8})|(150[0-9]{8})|(151[0-9]{8})|(152[0-9]{8})|(153[0-9]{8})|(155[0-9]{8})|(156[0-9]{8})|(157[0-9]{8})|(158[0-9]{8})|(159[0-9]{8})|(17[0-9]{9})|(180[0-9]{8})|(181[0-9]{8})|(182[0-9]{8})|(183[0-9]{8})|(185[0-9]{8})|(186[0-9]{8})|(187[0-9]{8})|(188[0-9]{8})|(189[0-9]{8})$/;

//汉字
var reg_hanzi = /^[\u4e00-\u9fa5]+$/; 

//判断是否包含字母、数字
var reg_validate = /^[0-9a-zA-Z]+$/;  // /^[0-9a-zA-Z_]+$/;（判断是否包含字母、数字、下划线）


//验证特殊字符
var strss = "',=,@,$,%,^,&,*,(,),!,~,>,<,?,{,},|,【,】,#";


//去掉字符串头尾空格   
function trim(str) {
    return str.replace(/(^\s*)|(\s*$)/g, "");
}


//添加需要弹出的消息框内容
function Alert_Val(val) {
    alert(val);
//    $('body').fytips({
//        title: "信息",
//        times: 2000,
//        con: val
//    });
}

//判断是否为空，如果是空则返回false,否则返回true
function validate_str_empty(val) {
    var v = $.trim(val);
    if (v == "" || val == null || val == undefined) {
        v = "";
    }

    var f = true;
    if (v == "" || v == "省份" || v == "城市" || v == "区县") {
        f = false;
    }
    return f;
}


//判断是否为空，如果是空则返回true,否则返回false
function validate_str_empty_new(val) {
    var f = false;
    var v = $.trim(val);
    if (v == "" || val == null || val == undefined) {
        f = true;
    }
    return f;
}

//比较当前日期
function checkdate(y, m, d) {

    var curdate = new Date();
    var selectdate = new Date(y, m - 1, d);
    if (selectdate > curdate)
        return false;
    else
        return true;
}

//比较当前日期
function checkdate_new(date1, date2) {

//    var curdate = new Date();
//    var years = (curdate.getFullYear()) * 1;
//    var months = (curdate.getMonth()) * 1 + 1;

//    y = y * 1;
//    m = m * 1;

//    if (years == y) {//年份相等
//        if (months == m)//月份相等
//        {
//            return true;
//        }
//        else if (months < m) {//当前月份小于被选中的日期
//            return false;
//        }
//        else {//当前月份大于被选中的日期
//            return true;
//        }
//    }
//    else if (years < y) {//当前年份小于被选中的日期
//        return false;
//    }
//    else {//当前年份大于被选中的日期
//        return true;
    //    }

    date1 = date1.replace(/\-/gi, "/");
    date2 = date2.replace(/\-/gi, "/");
    var time1 = new Date(date1).getTime();
    var time2 = new Date(date2).getTime();
    if (time1 > time2) {
        return 1;
    } else if (time1 == time2) {
        return 2;
    } else {
        return 3;
    }


}



//返回当前字符串的长度（一个汉字按2个字节算），str：需要被截取的字符串
function GetCharLength(str) {
    var iLength = 0;  //记录字符的字节数
    var hfleng = 1; //记录数字和字母的
    for (var i = 0; i < str.length; i++) {
        if (str.charCodeAt(i) > 255) {   //如果当前字符的编码大于255
            iLength += 1;    //所占字节数加1
        } else {
            hfleng += 1;   //否则数字字节数加1
        }
        if (hfleng == 2) {//两个数字或字母加1字节数
            iLength += 1;
            hfleng = 0;
        }
    }
    return iLength;
}

//javascript有一个函数，验证数字，包含小数和负数，去除负数和0
function IsNum(num) {
    var f = true;
    if (!isNaN(num)) {
        //数字
        if (num*1 > 0) {
            f = true;
        }
        else {
            f = false;
        }
    }
    else {
        //非数字
        f = false;
    }
    return f;
}
//javascript有一个函数，验证数字，包含小数和负数，去除负数
function IsNum_All(num) {
    var f = true;
    if (!isNaN(num)) {
        //数字
        if (num * 1 >= 0) {
            f = true;
        }
        else {
            f = false;
        }
    }
    else {
        //非数字
        f = false;
    }
    return f;
}


//比较两个数字的大小，如果num1大于num2，返回true，反之返回false;
function CompanyNum(num1,num2) {
    var f = true;
    if (num1*1>num2*1) {
        f = false;
    }
    return f;
}

//正则判断，需要判断的值，代表正则表达式的变量，val：需要被验证的字符串，reg：正则表达式的变量名称
function validate_reg(val, reg) {
    var f = true;
    if (!reg.test(val)) {
        f = false;
    }

//    //验证汉字
//    if (reg == "reg_hanzi") {
//        if (reg.test(val)) {
//            f = false;
//        }
//    }

    return f;
}

// 字符限制，可使两个英文按一个位置计算
function limit(obj, num) {
    var flag = false;
    var Limit = $.trim($(obj).val().replace(/[^\x00-\xff]/g, "**"));
    if (Limit.length > num * 2) {
        flag = true;
    }
    return flag;
}

//获取复选框下被选中的数据的列表，name：复选框下所有选项的name值
function Return_Check_Val(name) {
    var vals = "";
    $("input[name='" + name + "']").each(function () {
        if ($(this).attr("checked") != undefined)         {
            vals += $(this).val() + ",";
        }
    });

    if (validate_str_empty(vals)) {
        if (vals.indexOf(",") >= 0) {
            vals = vals.substring(0, vals.length - 1);
        }
    }

    return vals;
}

//给复选框下被选中的数据的列表赋值，name：复选框下所有选项的name值，vals：复选框下所有选项的value值
function Assignment_Check_Val(name, vals) {
    $("input[name='" + name + "']").attr("checked", false);

    if (validate_str_empty(vals)) {
        if (vals.indexOf(",") >= 0) {
            var arr = vals.split(",");
            $("input[name='" + name + "']").each(function () {
                var v = $(this).val();
                for (var i = 0; i < arr.length; i++) {
                    if (v == arr[i]) {
                        $(this).attr("checked", "checked");
                    }
                }
            });
        }
        else {
            $("input[name='" + name + "']").each(function () {
                if ($(this).val() == vals) {
                    $(this).attr("checked", "checked");
                }
            });
        }
    }

}

//获取复选框下被选中的数据的列表，获取根据显示的label下的数据，name：复选框下所有选项的name值，isval：当前被选中的复选框的值的列表，以","隔开，ischeck：是否进行数据的对比（true：用户isval进行选中值的对比，false：与之相反）
function Return_Check_Html(name, isval, ischeck) {
    var vals = "";
    $("input[name='" + name + "']").each(function () {
        var v = $(this).val();
        if (ischeck) {
            var arr = isval.split(",");
            for (var i = 0; i < arr.length; i++) {
                if (arr[i] == v) {
                    vals += $(this).parent().find("label").text() + ",";
                }
            }
        }
        else {
            if ($(this).attr("checked") != undefined) {
                vals += $(this).parent().find("label").text() + ",";
            }
        }
    });

    if (validate_str_empty(vals)) {
        if (vals.indexOf(",") >= 0) {
            vals = vals.substring(0, vals.length - 1);
        }
    }

    return vals;
}

//获取单选框下被选中的数据，name：单选框下所有选项的name值
function Return_Radio_Val(name) {
    var vals = "";
    $("input[name='" + name + "']").each(function () {
        if ($(this).attr("checked") != undefined)         {
            vals += $(this).val();
        }
    });

    return vals;
}


//获取下拉列表框下被选中的数据（即VALUE：通过optoin value=**，value中的数据），name：下拉列表框的ID
function Return_Select_Val(ID) {
    var vals = $("#" + ID + " option:selected").val();
    return vals;
}

//获取下拉列表框下被选中的数据（即HTML：能被看到显示在外面的数据），name：下拉列表框的ID
function Return_Select_Html(ID) {
    var vals = $("#" + ID + " option:selected").html();
    if (vals == "省份" || vals == "城市" || vals == "区县") {
        vals = "";
    }
    return vals;
}

//给指定的标签赋值，val：当前标签需要被赋予的值，cz_type：需要被操作的是标签的id还是class（id：代表标签的ID；不填或c代表标签的class）；cz_val：代表具体的ID或Class（如btnok）；text_html：代表需要被以哪种方式赋值text：代表.text()的形式，html：代表.html()的形式；
function Get_Html(val,cz_type,cz_val,text_html) {
    if (cz_type == "id") {//ID
        if (text_html == "text") {
            $("#" + cz_val).text(val);
        }
        else {
            $("#" + cz_val).html(val);
        }
    }
    else {//Class
        if (text_html == "text") {
            $("." + cz_val).text(val);
        }
        else {
            $("." + cz_val).html(val);
        }
    }
}

/**  
* 身份证15位编码规则：dddddd yymmdd xx p   
* dddddd：地区码   
* yymmdd: 出生年月日   
* xx: 顺序类编码，无法确定   
* p: 性别，奇数为男，偶数为女  
* <p />  
* 身份证18位编码规则：dddddd yyyymmdd xxx y   
* dddddd：地区码   
* yyyymmdd: 出生年月日   
* xxx:顺序类编码，无法确定，奇数为男，偶数为女   
* y: 校验码，该位数值可通过前17位计算获得  
* <p />  
* 18位号码加权因子为(从右到左) Wi = [ 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2,1 ]  
* 验证位 Y = [ 1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2 ]   
* 校验位计算公式：Y_P = mod( ∑(Ai×Wi),11 )   
* i为身份证号码从右往左数的 2...18 位; Y_P为脚丫校验码所在校验码数组位置  
*   
*/

var Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1]; // 加权因子   
var ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2]; // 身份证验证位值.10代表X   

/*验证身份证号*/
function IdCardValidate(idCard) {
    idCard = trim(idCard.replace(/ /g, ""));
    if (idCard.length == 15) {
        return isValidityBrithBy15IdCard(idCard);
    } else if (idCard.length == 18) {
        var a_idCard = idCard.split(""); // 得到身份证数组   
        if (isValidityBrithBy18IdCard(idCard) && isTrueValidateCodeBy18IdCard(a_idCard)) {
            return true;
        } else {
            return false;
        }
    } else {
        return false;
    }
}
/**  
* 判断身份证号码为18位时最后的验证位是否正确  
* @param a_idCard 身份证号码数组  
* @return  
*/
function isTrueValidateCodeBy18IdCard(a_idCard) {
    var sum = 0; // 声明加权求和变量   
    if (a_idCard[17].toLowerCase() == 'x') {
        a_idCard[17] = 10; // 将最后位为x的验证码替换为10方便后续操作   
    }
    for (var i = 0; i < 17; i++) {
        sum += Wi[i] * a_idCard[i]; // 加权求和   
    }
    valCodePosition = sum % 11; // 得到验证码所位置   
    if (a_idCard[17] == ValideCode[valCodePosition]) {
        return true;
    } else {
        return false;
    }
}
/**  
* 通过身份证判断是男是女  
* @param idCard 15/18位身份证号码   
* @return 'female'-女、'male'-男  
*/
function maleOrFemalByIdCard(idCard) {
    idCard = trim(idCard.replace(/ /g, "")); // 对身份证号码做处理。包括字符间有空格。   
    if (idCard.length == 15) {
        if (idCard.substring(14, 15) % 2 == 0) {
            return 'female';
        } else {
            return 'male';
        }
    } else if (idCard.length == 18) {
        if (idCard.substring(14, 17) % 2 == 0) {
            return 'female';
        } else {
            return 'male';
        }
    } else {
        return null;
    }
    //  可对传入字符直接当作数组来处理   
    // if(idCard.length==15){   
    // alert(idCard[13]);   
    // if(idCard[13]%2==0){   
    // return 'female';   
    // }else{   
    // return 'male';   
    // }   
    // }else if(idCard.length==18){   
    // alert(idCard[16]);   
    // if(idCard[16]%2==0){   
    // return 'female';   
    // }else{   
    // return 'male';   
    // }   
    // }else{   
    // return null;   
    // }   
}
/**  
* 验证18位数身份证号码中的生日是否是有效生日  
* @param idCard 18位书身份证字符串  
* @return  
*/
function isValidityBrithBy18IdCard(idCard18) {
    var year = idCard18.substring(6, 10);
    var month = idCard18.substring(10, 12);
    var day = idCard18.substring(12, 14);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 这里用getFullYear()获取年份，避免千年虫问题   
    if (temp_date.getFullYear() != parseFloat(year)
          || temp_date.getMonth() != parseFloat(month) - 1
          || temp_date.getDate() != parseFloat(day)) {
        return false;
    } else {
        return true;
    }
}
/**  
* 验证15位数身份证号码中的生日是否是有效生日  
* @param idCard15 15位书身份证字符串  
* @return  
*/
function isValidityBrithBy15IdCard(idCard15) {
    var year = idCard15.substring(6, 8);
    var month = idCard15.substring(8, 10);
    var day = idCard15.substring(10, 12);
    var temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
    // 对于老身份证中的你年龄则不需考虑千年虫问题而使用getYear()方法   
    if (temp_date.getYear() != parseFloat(year)
              || temp_date.getMonth() != parseFloat(month) - 1
              || temp_date.getDate() != parseFloat(day)) {
        return false;
    } else {
        return true;
    }
}


//验证是否是MSN账号
function isMSN(str) {
    if (validate_reg(str, reg_email)) {
        if (str.indexOf("outlook.com") > 0) {
            return true;
        }
        if (str.indexOf("hotmail.com") > 0) {
            return true;
        }
        if (str.indexOf("live.cn") > 0) {
            return true;
        }
        if (str.indexOf("live.com") > 0) {
            return true;
        }
        return false;
    }
    else {
        return false;
    }
}

//验证身份证号
function Validate_IDCard(val) {
    var f = true;
    if (!IdCardValidate(val)) {
        f = false;
    }
    return f;
}

//判断当前字符串是否包含特殊字符
function isNumsAny(strVal) {
    var f = false;
    var regNum = /^\d*$/;
    //isNaN(num)方法判断是否为数字，如果是数字则返回false,否则返回true
    var numa = 0;
    for (var k = 0; k < strVal.length; k++) {
        var kstr = "";
        kstr = strVal.substr(k, 1);
        if (strss.indexOf(kstr) >= 0) {
            f = true;
            break;
        }
    }
    return f;
}


//判断当前字符串是否包含特殊字符
function isNumsAny(strVal,strss_new) {
    var f = false;
    var regNum = /^\d*$/;
    //isNaN(num)方法判断是否为数字，如果是数字则返回false,否则返回true
    var numa = 0;
    for (var k = 0; k < strVal.length; k++) {
        var kstr = "";
        kstr = strVal.substr(k, 1);
        if (strss_new.indexOf(kstr) >= 0) {
            f = true;
            break;
        }
    }
    return f;
}


//判断当前字符串是否全为数字或全为特殊字符
function isNums(strVal, strss_new) {
    var f = false;
    var regNum = /^\d*$/;
    //isNaN(num)方法判断是否为数字，如果是数字则返回false,否则返回true
    var numa = 0;
    //全都是数字的情况 
    if (regNum.test(strVal)) {
        f = true;
    }
    else {
        for (var k = 0; k < strVal.length; k++) {
            var kstr = "";
            kstr = strVal.substr(k, 1);
            if (strss_new.indexOf(kstr) >= 0) {
                numa++;
            }
        }
        if (numa == strVal.length) {
            f = true;
        }
    }
    //alert(numa);
    return f;
}

//方法：将json时间字符串转换成2011-4-4的时间格式的字符串

function ChangeDateFormat(jsondate) {
    jsondate = jsondate.replace("/Date(", "").replace(")/", "");

    if (jsondate.indexOf("+") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("+"));
    } else if (jsondate.indexOf("-") > 0) {
        jsondate = jsondate.substring(0, jsondate.indexOf("-"));
    }
    var date = new Date(parseInt(jsondate, 10));
    var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
    var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    month = date.getMonth() + 1;
    currentDate = date.getDate();
    return date.getFullYear() + "-" + month + "-" + currentDate; //+ " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
}