// placeholder 占位符 兼容 ie9 -
var placeholder = {

	_support: function(){
		return 'placeholder' in document.createElement('input');  // 检测是否支持 placeholder特性
	},

	init: function(o){

		if(!this._support()){
			
			$(o).each(function(){

				if($(this).attr('type') === 'text'){

					if(!$(this).val() || $(this).val() === $(this).data('placeholder')){
						$(this).val($(this).data('placeholder')).addClass('placeholder');
					}
					placeholder.bindText(this);

				}else if($(this).attr('type') === 'password'){

					var $pwd = $('<input>').attr({type: 'text'}).addClass('placeholder');

					if(!$(this).val() || $(this).val() === $(this).data('placeholder')){

						$(this).after($pwd).hide();
						$pwd.val($(this).data('placeholder'));
					}
					placeholder.bindPwd($pwd);
				}
			});
		}

	},

	bindText: function(o){

		$(o).on('focus', function(){
			if($(this).val() === $(this).data('placeholder')){
				$(this).removeClass('placeholder').val('');
			}else{
				$(this).removeClass('placeholder');
			}
		});

		$(o).on('blur', function(){
			if(!$(this).val()){
				$(this).addClass('placeholder').val($(this).data('placeholder'));
			}
		});


	},

	bindPwd: function(o){

		$(o).on('focus', function(){
			$(this).hide().prev('input').val('').show().focus();
		});

		$(o).prev('input').on('blur', function(){
			if(!$(this).val()){
				$(this).hide().next('input').show();
			}
		});

	}

};

placeholder.init($('input'));


//跳转到登录页面
function returnlogin() {
    window.location.href = "/login_1?urls=" + window.location.href;
}


//登录弹层
function Login_TC() {

//    var html = '<div class="login-tip" id="login_tc">';
//    html += '<div class="login-tip-head">';
//    html += '<em>登录</em>';
//    html += '<button type="button" id="login-tip-close">✕</button>';
//    html += '</div>';
//    html += '<div class="login-tip-content">';
//    html += '<p><input type="text" name="" id="login_tel" placeholder="手机" data-placeholder="手机" maxlength="11"></p>';
//    html += '<p><input type="password" name="" id="login_pwd" placeholder="密码" data-placeholder="密码" maxlength="16"></p>';
//    html += '<button type="button" onclick="login_islogin(2)">登录</button>';
//    html += '<p><span>还没有注册？<a href="/register" title="快速注册">快速注册</a></span><a href="/findpwd" title="忘记密码？">忘记密码？</a></p>';
//    html += '</div>';
//    html += '</div>';

//    $('.login-tip').size() ? $('.login-tip').show() : $('body').append(html);

//    $('#login-tip-close').click(function () {
//        $(this).parents('.login-tip').hide();
    //    });

    returnlogin();

}

//通过回车进行登录

$(function () {
    $(document).on('keypress', '#login_tc input', function (e) {
        var keyCode = e.keyCode ? e.keyCode : e.which ? e.which : e.charCode;
        if (keyCode == 13) {
            login_islogin(2); //通过登录弹框进入
        }
    });

});


//登录操作
//判断是否登录成功（正式）
function login_islogin(num) {

    var login_tel_id = "#login_tel";
    var login_upwd_id = "#login_pwd";
    var remmber = 0;

    //手机号
    var login_tel = $.trim($(login_tel_id).val());
    //密码
    var login_upwd = $.trim($(login_upwd_id).val());

    if (login_tel == "") {
        show_error_msg(login_tel_id, "请输入手机号");
        return false;
    }

    if (!validate_reg(login_tel, reg_phone)) {
        show_error_msg(login_tel_id, "手机号格式不正确！");
        return false;
    }

    if (login_tel.length > 11) {
        show_error_msg(login_tel_id, "手机号格式错误");
        return false;
    }

    var login_flag = true;
    var login_tel_msg = "";

    $.ajax({
        type: "post",
        url: "/templatecs/Handle/LoginHandle.ashx",
        data: "action=ValidatePhone&tel=" + escape(login_tel) + "",
        async: false,
        success: (function (msg) {
            if (msg == "nouser") {
                login_tel_msg = "该账户不存在";
                login_flag = false;
            }
            else if (msg == "notel") {
                login_tel_msg = "请输入手机号";
                login_flag = false;
            }
            else {
                login_tel_msg = "";
                login_flag = true;
            }
        })
    })

    if (!login_flag) {
        show_error_msg(login_tel_id, login_tel_msg);
        return false;
    }


    if (login_upwd == "") {
        show_error_msg(login_upwd_id, "请输入密码");
        return false;
    }

    //关闭弹层
    close_a();

    $.post("/templatecs/Handle/LoginHandle.ashx", { action: "Is_Login", tel: escape(login_tel), upwd: escape(login_upwd), remmber: remmber }, function (data) {

        if (data == "nouser") {
            show_error_msg(login_tel_id, "该账户不存在");
        }
        else if (data == "notel") {

            show_error_msg(login_tel_id, "请输入手机号");
        }
        else if (data == "no") {

            show_error_msg(login_tel_id, "账号或密码错误");
        }
        else {
            //重新加载父级页面
            window.parent.location.reload(true);
        }
    });
}


//举报

function ToRrport(reply_id, type, pid, col, reid) {
    var html = '  <div id="report">';
    html += '<form action="#" method="post">';
    html += '<dl class="ck_sets">';
    html += ' <dt>选择举报类型：</dt>';
    html += '<dd>';
    html += ' <span class="ck_substitute_checked"></span><input type="checkbox" checked="checked" name="ck_set" value="广告欺诈" id="ck1"/><label for="ck1">广告欺诈</label></dd>';
    html += '<dd>';
    html += ' <span class="" ></span><input type="checkbox" name="ck_set" value="反动政治" id="ck2"/><label for="ck2">反动政治</label></dd>';
    html += '<dd>';
    html += ' <span class="" ></span><input type="checkbox" name="ck_set" value="色情暴力" id="ck3"/><label for="ck3">色情暴力</label></dd>';
    html += '<dd>';
    html += ' <span  class=""></span><input type="checkbox" name="ck_set" value="恶意注水" id="ck4"/><label for="ck4">恶意注水</label></dd>';
    html += '<dd>';
    html += ' <span class="" ></span><input type="checkbox" name="ck_set" value="人身攻击" id="ck5"/><label for="ck5">人身攻击</label></dd>';
    html += '<dd>';
    html += ' <span class="" ></span><input type="checkbox" name="ck_set" value="非法虚假信息" id="ck6"/><label for="ck6">非法虚假信息</label></dd>';
    html += '<dd>';
    html += ' <span class="" ></span><input type="checkbox" name="ck_set" value="其他" id="ck7"/><label for="ck7">其他</label></dd>';
    html += '</dl>';
    html += ' 	<dl class="expain">';
    html += '<dt>举报说明：</dt>';
    html += '<dd><textarea></textarea></dd>';
    html += '</dl>';
    html += '<input type="button" value="提交" class="report_subBtn" />';
    html += '</form>';
    html += '</div>';
    html += '';

    $("body").append(html);
    //simulate_ck();
   
    var i = $.layer({
        type: 1,
        title: '举报',
        closeBtn: [0, true],
        border: [0],
        shade: false,
        shadeClose: true,
        area: ['516px', '402px'],
        page: { dom: '#report' },
        zIndex: 10
    });

    //关闭
    $(".xubox_close0").on('click', function () {
        $("#report").remove();
    });

    //选择举报项

    //通过复选框
    var report_check_obj = $("#report .ck_sets input[type=checkbox]");

    $(document).on('click', '#report .ck_sets input[type=checkbox]', function () {
        var checked_report = $(this).parents("dd").find("input").attr("checked");

        if (checked_report == "checked") {
            $(this).parents("dd").find("span").addClass("ck_substitute_checked");
        }
        else {
            $(this).parents("dd").find("span").removeClass("ck_substitute_checked");
        }
    });


    $("#report").find(".report_subBtn").click(function () {
        var SelCon = $("#report .ck_sets").find(".ck_substitute_checked");

        var typecon = "";
        $(SelCon).each(function () {
            typecon += ($(this).next().val()) + ',';
        });

        var con = $("#report .expain").find("textarea").val();
        if (typecon == "" && con == "") {
            FunMsg("请选择或者填写举报内容");
            return false;
        }


        var report_function = "InsertReport";
        var rurl = window.location.href.toLowerCase();

        if (rurl.indexOf("hrxz_detail_") >= 0) {
            report_function += "_Xz";
        }

        $.post(
            "/templatecs/handle/ArticleHandle.ashx",
            { Action: report_function, reply_id: reply_id, type: type, ArticleId: pid, TypeCon: escape(typecon), Con: escape(con), Col: col, Reid: reid },
            function (data) {
                if (data == "nologin") {
                    Login_TC();
                }
                else if (data == "no") {
                    FunMsg("系统繁忙，请稍后重试");
                }
                else if (data == "repet") {
                    FunMsg("您已经举报过了");
                }
                else {
                    $("#xubox_layer1").hide();
                    $("#report .expain").find("textarea").val("");
                    $("#report .ck_sets").find("span").removeClass("ck_substitute_checked");
                    $("#report .ck_sets").find("span:first").addClass("ck_substitute_checked");
                    $(".xubox_close0").click();
                    $("#report").remove();
                    FunMsg("举报成功");
                }
            }
        );
    });

}

//通过span
var report_span_obj = $("#report .ck_sets span");

$(document).on('click', '#report .ck_sets span', function () {

    var checked_report = $(this).attr("class");

    if (checked_report == "ck_substitute_checked") {
        $(this).parents("dd").find("input").attr("checked", false);
        $(this).parents("dd").find("span").removeClass("ck_substitute_checked");
    }
    else {
        $(this).parents("dd").find("input").attr("checked", true);
        $(this).parents("dd").find("span").addClass("ck_substitute_checked");
    }
    return;
});

// 验证码
function verifyCode() {

    var newcode = RndNum_register(4);

    var html = '<div class="tip-wrap"><div class="login-tip">\
        <div class="login-tip-head"><em>验证码</em><button type="button" id="login-tip-close">✕</button></div>\
        <div class="login-tip-content">\
            <h2>请输入验证码</h2>\
            <p class="verify-p">\
                <input type="text" name="" id="captcha_register" placeholder="输入验证码" data-placeholder="输入验证码" maxlength="4"><img id="verifyimage" src="/Handle/VerifyIMGt.aspx?id="+newcode+"" data_code=""+newcode+"" width="100" height="42" /><a href="javascript: void(0);" onclick="verc_register();">看不清<br />换一组</a>\
            </p>\
            <div class="tip-err"></div>\
            <button type="button" onclick="validate_register_code()">确定</button>\
        </div>\
    </div>\
    <div class="tip-mask"></div></div>';

    $('.tip-wrap').size() ? $('.login-tip').show().siblings('.tip-mask').show() : $('body').append(html);


    verc_register();


    $('#login-tip-close').click(function () {
        $(this).parents('.login-tip').hide().siblings('.tip-mask').hide();
    });

}


    
//点击换一张的时候切换验证码
//产生随机数
function RndNum_register(n) { var rnd = ""; for (var i = 0; i < n; i++) rnd += Math.floor(Math.random() * 10); return rnd; }

//验证码随机数
var strCode_register = "";

function verc_register() {
    strCode_register = RndNum_register(4);
    document.getElementById("verifyimage").src = "/Handle/VerifyIMGt.aspx?id=" + strCode_register;
    $("#verifyimage").attr("data_code", strCode_register);
}


