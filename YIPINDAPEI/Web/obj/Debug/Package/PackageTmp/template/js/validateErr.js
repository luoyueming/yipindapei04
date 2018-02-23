var validateErr = {

    init: function (o, s) {

        var html = '<div class="validate-tips">';
        html += '<a class="tips-close" href="javascript: void(0);">✕</a>';
        html += '<p class="tips-msg">';
        html += s;
        html += '</p>';
        html += '<span class="poptip-arrow poptip-arrow-bottom"><em>◆</em><i>◆</i></span>';
        html += '</div>';

        if ($('.validate-tips').size() > 0) {
            $('.tips-msg').text(s);
            $('.validate-tips').show();
        } else {
            $('body').append(html);
        }

        var w = $(o).outerWidth(),
			h = $('.validate-tips').outerHeight(),
			t = $(o).offset().top,
			l = $(o).offset().left;

        $('.validate-tips').css({
            width: w - 25,
            left: l,
            top: t - h - 6
        });

        $(document).on('click', '.tips-close', function () {
            $(this).parents('.validate-tips').hide();
        });

        return this;
    },
    close: function () {
        $('.validate-tips').hide();
    }
};


//显示错误信息
var a;

function show_error_msg(id, html) {
    a = validateErr.init(id, html);
    $(id).focus();
}

//关闭所有的错误提示
function close_a() {
    if (a != null && a != undefined) {
        a.close();
    }
}

