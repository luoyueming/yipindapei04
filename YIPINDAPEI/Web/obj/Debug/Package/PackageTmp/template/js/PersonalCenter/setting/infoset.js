//当前logo的隐藏域
var hidelogo = "#hidlogo";
//显示logo的img标签的ID
var show_img_id = "#logo_img";

$(".err").html("");


//存储企业信息
function Save_Info() {

    var logo_id = $("#piwd");
    var uname_id = $("#uname");

    var comlogo = $.trim($(hidelogo).val());
    var uname = $.trim($(uname_id).val());

    if (comlogo == "") {
        show_error_msg(logo_id, "用户头像不能为空!");
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

    //判断是否添加成功
    $.ajax({
        type: "post",
        url: "/templatecs/Handle/PersonalHandle.ashx",
        data: "action=Update_User_Info&user_logo=" + escape(comlogo) + "&uname=" + escape(uname),
        async: false, //同步执行
        success: (function (msg) {
            if (msg == "nologin") {
                window.location.href = "/login";
            }
            else if (msg == "nohead") {
                show_error_msg(logo_id, "用户头像不能为空!");
            }
            else if (msg == "noname") {
                show_error_msg(uname_id, "请输入昵称");
            }
            else if (msg == "nouser") {
                window.location.href = "/404";
            }
            else if (msg == "repname") {
                show_error_msg(uname_id, "该昵称已存在，请重新输入");
            }
            else if (msg == "no") {
                FunMsg("提交失败!");
            }
            else {
                FunMsg("提交成功", "/people_c");
            }

        })
    })

}

//显示错误信息
function show_error_msg(id, html) {
    $(id).parents(".b").find(".err").html(html);
    $(".box input").blur();
    $(id).focus();

}