//判断当前用户是否已经登录
function Validate_IsLogin() {

    //同步验证
    var flag = false;
    $.ajax({
        type: "post",
        async: false,
        data: { "action": "GetLoginState", "type": 1 },
        url: "/templatecs/Handle/LoginHandle.ashx",
        success: function (data) {

            if (data != "ok") {//还未登录
                window.location.href = data; //+ "?" + escape(window.location.href) + "";
            }
            else {//已经登录
                flag = true;
            }
        }
    });

    return flag;
}


//判断当前用户是否已经登录，没有返回值，直接跳转
function validate_login() {

    //同步验证
    var flag = false;
    $.ajax({
        type: "post",
        async: false,
        data: { "action": "GetLoginState", "type": 1 },
        url: "/templatecs/Handle/LoginHandle.ashx",
        success: function (data) {

            if (data != "ok") {//还未登录
                window.location.href = data;// +"?" + escape(window.location.href) + "";
            }
            else {//已经登录
                flag = true;
            }
        }
    });

    //return flag;
}

//判断当前用户是否已经登录
function Validate_IsLogin_Alert() {

    //同步验证
    var flag = false;
    $.ajax({
        type: "post",
        async: false,
        data: { "action": "GetLoginState" },
        url: "/templatecs/Handle/LoginHandle.ashx",
        success: function (data) {

            if (data != "ok") {//还未登录
                FunMsg("您还未登录");
            }
            else {//已经登录
                flag = true;
            }
        }
    });

    return flag;
}

