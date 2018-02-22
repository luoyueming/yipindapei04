

//通过回车进行登录

$(function () {

    $(".pwdrecovery-box input").keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            checkPhone();
        }
    });

});

//----------------------------------------------------------------发送验证码，验证手机验证码----------

//发送验证码
var TimerFly = "";
var max_sec = 60; //每隔60秒就重新发送一次

//发送验证码
$(document).on('click', '.send', function () {
    setCode();
});

var tel_code_id = "#captcha";

function setCode() {

    max_sec = -1;

    $.post("/templatecs/Handle/FindPwdHandle.ashx", { action: "SendPhoneVerifyCode", send_code_yzm: $.trim($("#hidcode").val()) }, function (data) {


        if (data == "noyzm") {
            window.location.href = "/findpwd";
        }
        else if (data == "error_yzm") {
            window.location.href = "/findpwd";
        }
        else if (data == "1") {
            show_error_msg(tel_code_id, "发送失败，请重新发送！");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else if (data == "nologin") {
            window.location.href = "/findpwd";
        }
        else if (data == "5") {
            show_error_msg(tel_code_id, "同一个手机号每天只能发5次验证码");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else if (data == "10") {
            show_error_msg(tel_code_id, "同一个IP每天只能发10次验证码");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else if (data == "one") {
            show_error_msg(tel_code_id, "同一个手机号一分钟之内只能发送一次");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else {

            //关闭弹层
            close_a();

            max_sec = 60;

            //                    $("#backtime").text(max_sec);
            TimerFly = window.setInterval(SetRemainTime, 1000);
            //                    $("#recode").text("秒后可再次发送");

            $("#send_codes").removeClass("send").addClass("gray");
            return false;
        }

    });

}

//验证手机号
function checkPhone() {
    var codenum = $.trim($(tel_code_id).val());

    //判断验证码
    if (!validate_str_empty(codenum)) {
        show_error_msg(tel_code_id, "验证码不能为空，请重新输入！");
        //让按钮可用，倒计时清0
        huifuable();
    }
    else {

        $.ajax({
            type: "post",
            url: "/templatecs/Handle/FindPwdHandle.ashx",
            data: "action=VerifyCodeIsExists&Vc=" + codenum + "",
            async: false, //同步执行
            success: (function (data) {

                if (data == "0") {
                    show_error_msg(tel_code_id, "验证码错误，请重新输入！");
                    $(tel_code_id).focus();
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else if (data == "nologin") {
                    window.location.href = "/findpwd";
                }
                else {

                    window.location.href = "/findpwd/step3";
                }
            })
        })
    }
}


//60秒后自动发送验证码

function SetRemainTime() {

    if (max_sec > 0) {
        $("#send_codes").html("" + (max_sec - 1) + "秒后再次发送");

        max_sec--;

        if (max_sec == 0) {

            //让按钮可用，倒计时清0
            huifuable();
            //重置初始值
            max_sec = 61;

            TimerFly = window.clearInterval(TimerFly); //清空计时器，停止调用函数
        }
    }
}


//点击关闭按钮，同时关掉计时器
function closeTime() {
    TimerFly = window.clearInterval(TimerFly); //清空计时器，停止调用函数
}

//让按钮可用，倒计时清0
function huifuable() {

    //关掉计时器
    closeTime();

    $("#send_codes").removeClass("gray").addClass("send").html("重新获取");

    TimerFly = window.clearInterval(TimerFly); //清空计时器，停止调用函数
}
