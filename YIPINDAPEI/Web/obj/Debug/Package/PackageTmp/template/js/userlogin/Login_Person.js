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

var cur_url_s = window.location.href.toLowerCase();

if (cur_url_s.indexOf("userlogin") >= 0) {
    verc();

    //验证码
    $("#captcha").val("");
}

//判断是否登录成功（正式）
function new_islogin(num) {

    var tel_id = "#tel";
    var upwd_id = "#pwd";
    var code_id = "#captcha";
    var remmber = 0;

    //手机号
    var tel = $.trim($(tel_id).val());
    //密码
    var upwd = $.trim($(upwd_id).val());
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
        url: "/templatecs/Handle/LoginHandle.ashx",
        data: "action=ValidatePhone&tel=" + escape(tel) + "",
        async: false,
        success: (function (msg) {
            if (msg == "nouser") {
                tel_msg="该账户不存在";
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


    if (upwd == "") {
        show_error_msg(upwd_id, "请输入密码");
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

    if ($("#remember-pwd").attr("checked") == "checked" || $("#remember-pwd").attr("checked") == true) {
        remmber = 1;
    }

    //关闭弹层
    close_a();

    $.post("/templatecs/Handle/LoginHandle.ashx", { action: "Is_Login", tel:escape(tel), upwd:escape(upwd),remmber:remmber,send_code_yzm:yz_code }, function (data) {

        if (data == "error_yzm") {
            show_error_msg(code_id, "验证码错误");
            $("#captcha").focus();
            return false;
        }
        else if (data == "nouser") {
            show_error_msg(tel_id, "该账户不存在");
        }
        else if (data == "notel") {

            show_error_msg(tel_id, "请输入手机号");
        }
        else if (data == "no") {

            show_error_msg(tel_id, "账号或密码错误");
        }
        else {
            if (num == 1) {
                window.location.href = data;
            }
            else {
                //重新加载父级页面
                window.parent.location.reload(true);
            }
        }
    });
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
        if (strss.indexOf(kstr) > 0) {
            f = true;
            break;
        }
    }
    return f;
}


//判断当前字符串是否全为数字或全为特殊字符
function isNums(strVal) {
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
            if (strss.indexOf(kstr) > 0) {
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


//通过回车进行登录

$(function () {

    $(".login-form input").keypress(function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            var urls = $.trim($(".quickDl").html());
            if (urls == "") {
                new_islogin(1); //通过登录页面进入
            }
            else {
                new_islogin(2); //通过登录弹框进入
            }
        }
    });

});

