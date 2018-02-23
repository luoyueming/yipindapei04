var Prompt = function () {
    this.options = {
        title: '提示',   // 提示标题
        content: '我是alert提示！', // 提示内容
        type: 'alert',  // 提示类型
        timer: 2,  // 提示类型 必须为alert
        btn1: '确定', // 提示类型 必须为confirm
        btn2: '取消',  // 提示类型 必须为confirm
        fn: function () { }
    };
};
var p = {

    wrap: '',
    time: null,

    html: function () {

        var w = $('<div class="prompt-box">');

        var html = '<div class="prompt-head">';
        html += '<em>' + this.options.title + '</em>';
        html += '<button class="prompt-closeBtn">✕</button>';
        html += '</div>';
        html += '<div class="prompt-content">';
        html += '<p>' + this.options.content + '</p>';
        html += this.options.type === 'alert' ? '' : '<div class="prompt-btns"><button class="btn1">' + (this.options.btn1 ? this.options.btn1 : '') + '</button><button class="btn2 prompt-closeBtn">' + (this.options.btn2 ? this.options.btn2 : '') + '</button></div>'
        html += '</div>';

        return w.html(html), this.wrap = w;
    },
    position: function (o) {
        var w = $(o).outerWidth(),
			h = $(o).outerHeight();

        $(o).css({
            'marginLeft': parseInt(w / 2) * -1,
            'marginTop': parseInt(h / 2) * -1
        });
    },
    close: function (fn) {
        clearTimeout(this.time);
        fn && fn();
        this.wrap.remove();
        return;
    },
    delay: function (t, fn) {
        var _this = this;
        clearTimeout(this.time);
        this.time = setTimeout(function () {
            _this.close(fn);
        }, parseInt(t) * 1000);
    },
    init: function (json) {

        var _this = this;

        $.extend(this.options, json);

        $('body').append(this.html());
        this.position(this.wrap);

        if (this.options.type === 'alert' && this.options.timer) {
            this.delay(this.options.timer, this.options.fn);
        }

        $(document).on('click', '.prompt-closeBtn', function () {
            _this.close();
            return false;
        });

        $(document).on('click', '.btn1', function () {
            _this.options.fn();
            _this.close();
            return false;
        });

    }
};
Prompt.prototype = p;

var prompt = {
    use: function (json) {
        var p = new Prompt();
        p.init(json);
        return p;
    }
};


//显示消息，3秒后自动消失
function FunMsg(msg,href) {
    prompt.use({
        content: msg,
        timer: 2,//设置过期时间，单位：秒
        fn: function () {
            if (href != "" && href!=undefined) {
                window.location.href = href;
            }
        }

    });
}
