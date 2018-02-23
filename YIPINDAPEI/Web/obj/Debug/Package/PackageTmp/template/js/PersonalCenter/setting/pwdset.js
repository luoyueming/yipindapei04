//特殊字符（除！!@#$￥%&*.以外的字符）
var strss_pwd = "',=,^,(,),~,>,<,?,{,},|,【,】";
strss_pwd = "！!@#$￥%&*";

//特殊字符
var strss_real_name = "！!@#$￥%&*";

//初始化加载项
$(function () {
    //通过回车进行注册
    $(".modify-box input").keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            Update_Pwd();
        }
    });
});


//密码
var old_id = "#old_pwd";
//密码
var upwd_id = "#upwd";
//确认密码
var re_pwd_id = "#uqpwd";

$(".err").html("");

//修改密码
function Update_Pwd() {

    //原密码
    var oldpwd = $.trim($(old_id).val());
    //密码
    var upwd = $.trim($(upwd_id).val());
    //确认密码
    var re_pwd = $.trim($(re_pwd_id).val());

    show_error_msg(old_id, "");
    show_error_msg(upwd_id, "");
    show_error_msg(re_pwd_id, "");


    if (!validate_str_empty(oldpwd)) {
        show_error_msg(old_id, "请输入原密码");
        return false;
    }

    var old_msg = "";
    var old_flag = false;

    //判断是否是原密码
    $.ajax({
        type: "post",
        url: "/templatecs/Handle/PersonalHandle.ashx",
        data: "action=ValidateOldPwd&pwd=" + escape(oldpwd) + "",
        async: false, //同步执行
        success: (function (msg) {

            if (msg == "nologin") {
                window.location.href = "/login";
            }
            else if (msg == "noold") {
                old_msg = "请输入原密码";
            }
            else if (msg == "no") {
                old_msg="原密码错误";
            }
            else {
                old_flag = true;
            }
        })
    })

    if (!old_flag) {
        show_error_msg(old_id, old_msg);
        return false;
    }

    if (!validate_str_empty(upwd)) {
        show_error_msg(upwd_id, "请输入新密码");
        return false;
    }

    if (isNumsAny(upwd, strss_pwd)) {
        show_error_msg(upwd_id, "非法输入");
        return false;
    }

    if (upwd.length < 6 || upwd.length > 16) {
        show_error_msg(upwd_id, "请输入6-16位数字,字母或常用符号,字母区分大小写");
        return false;
    }

    if (!validate_str_empty(re_pwd)) {
        show_error_msg(re_pwd_id, "请再次输入密码");
        return false;
    }

    if (upwd != re_pwd) {
        show_error_msg(re_pwd_id, "两次密码输入不一致");
        return false;
    }

    //判断是否修改成功
    $.ajax({
        type: "post",
        url: "/templatecs/Handle/PersonalHandle.ashx",
        data: "action=Update_User_Pwd&pwd=" + escape(upwd) + "",
        async: false, //同步执行
        success: (function (msg) {

            if (msg == "nologin") {
                window.location.href = "/login";
            }
            else if (msg == "nopwd") {
                show_error_msg(upwd_id, "请输入新密码");
            }
            else if (msg == "no") {
                show_error_msg(phone_id, "系统繁忙，请稍后重试");
            }
            else {
                FunMsg("修改成功", "/people_c/pwd_set");
            }
        })
    })
}


//显示错误信息
function show_error_msg(id, msg) {
    $(".err").html("");
    $("input").blur;
    $(id).parents(".b").find(".err").html(msg);
    $(id).focus();
}