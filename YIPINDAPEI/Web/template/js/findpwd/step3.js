//特殊字符（除！!@#$￥%&*.以外的字符）
var strss_pwd = "',=,^,(,),~,>,<,?,{,},|,【,】";
strss_pwd = "！!@#$￥%&*";

//特殊字符
var strss_real_name = "！!@#$￥%&*";

//初始化加载项
$(function () {
    //通过回车进行注册
    $(".pwdrecovery-box input").keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            Update_Pwd();
        }
    });
});



//密码
var upwd_id = "#upwd";
//确认密码
var re_pwd_id = "#uqpwd";

//修改密码
function Update_Pwd() {

    //密码
    var upwd = $.trim($(upwd_id).val());
    //确认密码
    var re_pwd = $.trim($(re_pwd_id).val());

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

    //关闭弹层
    close_a();

    //判断是否注册成功
    $.ajax({
        type: "post",
        url: "/templatecs/Handle/FindPwdHandle.ashx",
        data: "action=Update_User_Pwd&pwd=" + escape(upwd) + "",
        async: false, //同步执行
        success: (function (msg) {

            if (msg == "nologin") {
                window.location.href = "/findpwd";
            }
            else if (msg == "notel") {
                window.location.href = "/findpwd/step2";
            }
            else if (msg == "nopwd") {
                show_error_msg(upwd_id, "请输入新密码");
                //让按钮可用，倒计时清0
                huifuable();
            }
            else if (msg == "nouser") {
                show_error_msg(uname_id, "请输入昵称");
                //让按钮可用，倒计时清0
                huifuable();
            }
            else if (msg == "no") {
                show_error_msg(phone_id, "系统繁忙，请稍后重试");
                //让按钮可用，倒计时清0
                huifuable();
            }
            else {
                window.location.href = "/findpwd/step4";
            }
        })
    })
}
