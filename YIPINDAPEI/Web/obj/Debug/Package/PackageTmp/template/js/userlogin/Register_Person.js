//特殊字符（除！!@#$￥%&*.以外的字符）
var strss_pwd = "',=,^,(,),~,>,<,?,{,},|,【,】";
strss_pwd = "！!@#$￥%&*";

//特殊字符
var strss_real_name = "！!@#$￥%&*";

//初始化加载项
$(function () {
    //通过回车进行注册
    $(".login-form input").keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            IsRegister();
        }
    });
});

//手机号
var phone_id = "#tel";
//手机验证码
var tel_code_id = "#captcha";
//密码
var upwd_id = "#upwd";
//确认密码
var re_pwd_id = "#uqpwd";
//昵称
var uname_id = "#uname";

//判断是否注册成功
function IsRegister() {

    //手机号
    var tel = $.trim($(phone_id).val());
    //手机验证码
    var code = $.trim($(tel_code_id).val());
    //密码
    var upwd = $.trim($(upwd_id).val());
    //确认密码
    var re_pwd = $.trim($(re_pwd_id).val());
    //昵称
    var uname = $.trim($(uname_id).val());

//--------------------------------------------------------------手机-----------------------------------

    if (tel == "") {
        show_error_msg(phone_id, "手机号不能为空");
        //让按钮可用，倒计时清0
        huifuable();
        return false;
    }

    if (!validate_reg(tel, reg_phone)) {
        show_error_msg(phone_id, "手机号格式不正确！");
        //让按钮可用，倒计时清0
        huifuable();

        return false;
    }

    if (tel.length > 11) {
        show_error_msg(phone_id, "手机号格式不正确！");
        //让按钮可用，倒计时清0
        huifuable();

        return false;
    }

    //验证手机
    var phone_flag = true;
    var tel_msg = "";

    $.ajax({
        type: "post",
        url: "/templatecs/Handle/RegisterHandle.ashx",
        data: "action=ValidatePhone&tel=" + tel + "",
        async: false, //同步执行
        success: (function (msg) {
            if (msg == "no_repeat") {
                tel_msg = "已经有人绑定了该手机号，请重新输入！";
                phone_flag = false; //当前邮箱已经被注册过
            }
            else if (msg == "notel") {
                tel_msg = "手机号不能为空，请重新输入！";
                phone_flag = false; //当前邮箱已经被注册过
            }
            else {
                tel_msg = "";
                phone_flag = true; //当前邮箱还没有被注册过
            }
        })
    })

    if (!phone_flag) {
        show_error_msg(phone_id, tel_msg);
        //让按钮可用，倒计时清0
        huifuable();

        return false;
    }
    else {
        //让按钮可用，倒计时清0
        huifuable();
    }

    //验证验证码
    var now_test_flag = checkPhone();

    if (!now_test_flag) {
        return false;
    }
    else {//手机验证码已经通过

        if (!validate_str_empty(upwd)) {
            show_error_msg(upwd_id, "请输入密码");
            return false;
        }

        if (isNumsAny(upwd,strss_pwd)) {
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

        if (uname == "") {
            show_error_msg(uname_id, "请输入昵称");
            return false;
        }

        var hlen = GetCharLength(uname);

        if (hlen > 8) {
            show_error_msg(uname_id, "昵称的长度不能超过16个字符，其中一个汉字代表两个字符");
            return false;
        }

        //关闭弹层
        close_a();

        //判断是否注册成功
        $.ajax({
            type: "post",
            url: "/templatecs/Handle/RegisterHandle.ashx",
            data: "action=Is_Register&tel=" + escape(tel) + "&upwd=" + escape(upwd) + "&uname=" + escape(uname) + "",
            async: false, //同步执行
            success: (function (msg) {

                if (msg == "no_repeat") {
                    show_error_msg(phone_id, "已经有人绑定了该手机号，请重新输入！");
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else if (msg == "notel") {
                    show_error_msg(phone_id, "手机号不能为空，请重新输入！");
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else if (msg == "nopwd") {
                    show_error_msg(upwd_id, "请输入密码");
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else if (msg == "nouser") {
                    show_error_msg(uname_id, "请输入昵称");
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else if (msg == "repname") {
                    show_error_msg(uname_id, "该昵称已存在，请重新输入");
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else if (msg == "no") {
                    show_error_msg(phone_id, "系统繁忙，请稍后重试");
                    //让按钮可用，倒计时清0
                    huifuable();
                }
                else {
                    window.location.href = "/people_c";
                }
            })
        })
        

    }
}





//----------------------------------------------------------------发送验证码，验证手机验证码----------

//发送验证码
var TimerFly = "";
var max_sec = 60; //每隔60秒就重新发送一次

//发送验证码
$(document).on('click', '.send', function () {
    setCode();
});

function setCode() {

    max_sec = -1;

    var pnum = $.trim($(phone_id).val());


    if (!validate_str_empty(pnum)) {

        show_error_msg(phone_id, "请输入手机号！");
        //让按钮可用，倒计时清0
        huifuable();

        return false;
    }
    else {
        if (!validate_reg(pnum, reg_phone)) {
            show_error_msg(phone_id, "手机号格式不正确！");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else if (pnum.length > 11) {
            show_error_msg(phone_id, "手机号格式不正确！");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else {

            close_a();

            //显示验证码
            verifyCode();
        }
    }

}


//验证发送验证码
function validate_register_code() {
    var fr = true;

    var captcha_register = "#captcha_register";

    var code_num = $.trim($(captcha_register).val());
//    var code_data = $.trim($("#verifyimage").attr("data_code"));

    $(captcha_register).parents(".login-tip-content").find("div").html("");

    if (code_num == "") {
        $(captcha_register).parents(".login-tip-content").find("div").html("验证码不能为空！");
        fr = false;
    }
//    else if (code_num != code_data) {
//        $(captcha_register).parents(".login-tip-content").find("div").html("验证码不正确！");
//        fr = false;
//    }
//    else {
//        $('#login-tip-close').click();
//    }

    if (!fr) {
        return false;
    }
    else {

        //发送手机验证码

        var pnum = $.trim($(phone_id).val());

        $.post("/templatecs/Handle/RegisterHandle.ashx", { Phone: pnum, action: "SendPhoneVerifyCode", send_code_yzm: code_num }, function (data) {


            if (data == "noyzm") {
                $(captcha_register).parents(".login-tip-content").find("div").html("验证码不能为空！");
                fr = false;
            }
            else if (data == "error_yzm") {
                $(captcha_register).parents(".login-tip-content").find("div").html("验证码不正确！");
                fr = false;
            }
            else {
                //关闭弹层
                $('#login-tip-close').click();

                if (data == "1") {
                    show_error_msg(tel_code_id, "发送失败，请重新发送！");
                    //让按钮可用，倒计时清0
                    huifuable();

                    return false;
                }
                else if (data == "no_repeat") {
                    show_error_msg(phone_id, "已经有人绑定了该手机号，请重新输入！");
                    //让按钮可用，倒计时清0
                    huifuable();

                    return false;
                }
                else if (data == "notel") {
                    show_error_msg(phone_id, "手机号不能为空，请重新输入！");
                    //让按钮可用，倒计时清0
                    huifuable();

                    return false;
                }
                else if (data == "5") {
                    show_error_msg(phone_id, "同一个手机号每天只能发5次验证码");
                    //让按钮可用，倒计时清0
                    huifuable();

                    return false;
                }
                else if (data == "10") {
                    show_error_msg(phone_id, "同一个IP每天只能发10次验证码");
                    //让按钮可用，倒计时清0
                    huifuable();

                    return false;
                }
                else if (data == "one") {
                    show_error_msg(phone_id, "同一个手机号一分钟之内只能发送一次");
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
            }

        });

    }
}


//验证手机号
function checkPhone() {

    var pnum = $.trim($(phone_id).val());
    var codenum = $.trim($(tel_code_id).val());


    //最后验证的结果
    var error = false;

    if (!validate_str_empty(pnum)) {
        show_error_msg(phone_id, "请输入手机号！");

        //让按钮可用，倒计时清0
        huifuable();
    }
    else {
        if (!validate_reg(pnum, reg_phone)) {
            show_error_msg(phone_id, "手机格式错误请重新输入！");
            //让按钮可用，倒计时清0
            huifuable();
        }
        else {
            //show_error_msg("手机号输入成功！");

            //判断验证码
            if (!validate_str_empty(codenum)) {
                show_error_msg(tel_code_id, "验证码不能为空，请重新输入！");
                //让按钮可用，倒计时清0
                huifuable();
            }
            else {

                $.ajax({
                    type: "post",
                    url: "/templatecs/Handle/RegisterHandle.ashx",
                    data: "action=VerifyCodeIsExists&Phone=" + pnum + "&Vc=" + codenum + "",
                    async: false, //同步执行
                    success: (function (data) {

                        if (data == "0") {
                            show_error_msg(tel_code_id, "验证码错误，请重新输入！");
                            $(tel_code_id).focus();
                            //让按钮可用，倒计时清0
                            huifuable();
                        }
                        else if (data == "no_repeat") {
                            show_error_msg(phone_id, "已经有人绑定了该手机号，请重新输入！");
                            //让按钮可用，倒计时清0
                            huifuable();
                        }
                        else if (data == "notel") {
                            show_error_msg(phone_id, "手机号不能为空，请重新输入！");
                            //让按钮可用，倒计时清0
                            huifuable();
                        }
                        else {
                            //关闭弹层
                            close_a();
                            //让按钮可用，倒计时清0
                            huifuable();
                            error = true;
                        }
                    })
                })



            }

        }
    }

    return error;
}


//60秒后自动发送验证码

function SetRemainTime() {

    if (max_sec > 0) {
        $("#send_codes").html("" + (max_sec - 1) + "秒后再次发送");

        max_sec--;

        if (max_sec==0) {

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

    $("#send_codes").removeClass("gray").addClass("send").html("发送验证码");

    TimerFly = window.clearInterval(TimerFly); //清空计时器，停止调用函数
}



