//邮箱
var reg_email = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/i;

//特殊字符
var strss = "',=,$,%,^,&,*,(,),!,~,>,<,?,{,},|,【,】,#";

//产生随机数
function RndNum(n) { var rnd = ""; for (var i = 0; i < n; i++) rnd += Math.floor(Math.random() * 10); return rnd; }

//验证码随机数
var strCode = "";

//是否第一次登录错误
var first_error = true;

//点击换一张的时候切换验证码
function verc() {
    strCode = RndNum(4);
    document.getElementById("verifyimage").src = "/Handle/VerifyIMGt.aspx?id=" + strCode;
    $("#verifyimage").attr("data_code", strCode);
}

verc();

//验证码
$("#captcha").val("");

//判断是否登录成功（正式）
function find_step1() {

    var tel_id = "#tel";
    var code_id = "#captcha";

    //手机号
    var tel = $.trim($(tel_id).val());
    //输入框中的验证码
    var yz_code = $.trim($(code_id).val());
//    //输入框中的验证码
//    var hid_yz_code = $.trim($("#verifyimage").attr("data_code"));

    if (tel == "") {
        show_error_msg(tel_id, "请输入手机号");
        return false;
    }

    if (!validate_reg(tel, reg_phone)) {
        show_error_msg(tel_id, "手机号格式不正确！");
        return false;
    }

    if (tel.length > 11) {
        show_error_msg(tel_id, "手机号格式错误");
        return false;
    }

    var flag = true;
    var tel_msg = "";

    $.ajax({
        type: "post",
        url: "/templatecs/Handle/FindPwdHandle.ashx",
        data: "action=ValidatePhone&tel=" + escape(tel) + "",
        async: false,
        success: (function (msg) {
            if (msg == "nouser") {
                tel_msg = "该账户不存在";
                flag = false;
            }
            else if (msg == "notel") {
                tel_msg = "请输入手机号";
                flag = false;
            }
            else {
                tel_msg = "";
                flag = true;
            }
        })
    })

    if (!flag) {
        show_error_msg(tel_id, tel_msg);
        return false;
    }

    if (yz_code == "" || yz_code == "验证码") {
        show_error_msg(code_id, "请输入验证码");
        $("#captcha").focus();

        return false;
    }

//    if (yz_code != hid_yz_code) {
//        show_error_msg(code_id, "验证码错误");
//        $("#captcha").focus();
//        return false;
//    }

    //关闭弹层
    close_a();

    //发送手机验证码
    $.post("/templatecs/Handle/FindPwdHandle.ashx", { action: "SendPhoneVerifyCode", send_code_yzm: yz_code }, function (data) {


        if (data == "noyzm") {
            show_error_msg(code_id, "请输入验证码");
            $("#captcha").focus();

            return false;
        }
        else if (data == "error_yzm") {
            show_error_msg(code_id, "验证码错误");
            $("#captcha").focus();
            return false;
        }
        else if (data == "1") {
            show_error_msg(tel_id, "发送失败，请重新发送！");
            //让按钮可用，倒计时清0
            huifuable();

            return false;
        }
        else if (data == "nologin") {
            window.location.href = "/findpwd";
        }
        else if (data == "5") {
            show_error_msg(tel_id, "同一个手机号每天只能发5次验证码");

            return false;
        }
        else if (data == "10") {
            show_error_msg(tel_id, "同一个IP每天只能发10次验证码");

            return false;
        }
        else if (data == "one") {
            show_error_msg(tel_id, "同一个手机号一分钟之内只能发送一次");


            return false;
        }
        else {
            window.location.href = "/findpwd/step2_" + yz_code;
        }

    });
    
}



//通过回车进行登录

$(function () {

    $(".pwdrecovery-box input").keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            find_step1();
        }
    });

});

