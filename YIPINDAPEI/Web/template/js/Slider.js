/*
 * Class Slider
 * @param  {[type]} opts [description]
 * @author : Zhong Yuan 2013.6.26
 * @version : v0.99 beta
 */
var Slider = function (opts) {
	var init = function (opts) {
		this.conf = this._extends({
				wrapId : 'slider',
				itemClass : 'slider_item',
				sameSize : true,
				startOn : 0,
				// startOnPos : 300,
				scrollBy : 1,
				// scrollDis : 400,
				speed : 6,
				isVertical : false,
				isLoop : true,
				autoPlay : true,
				autoInterval : 3,
				absoluteAni : false,
				onReady : function (states) {
					// body...
				},
				onAniStart : function (slideOut, slideIn) {
					// body...
				},
				onAniEnd : function (slideOut, slideIn) {
					// body...
				},
				onEdge : function (isLeft) {
					// body...
				}
			}, opts);
		this._initDoms();
		this._initStates();
		this._initEvents();
		this._launch();
	};
	init.prototype = {
		// utils
		_id : function (id) {
			return document.getElementById(id);
		},
		_class : function (searchClass, node, tag) {
			var classElements = [],
			els,
			elsLen,
			pattern;
			if (node == null)
				node = document.body;
			if (tag == null)
				tag = '*';
			if (node.getElementsByClassName) {
				return node.getElementsByClassName(searchClass);
			}
			if (node.querySelectorAll) {
				return node.querySelectorAll('.' + searchClass);
			}
			els = node.getElementsByTagName(tag);
			elsLen = els.length;
			pattern = new RegExp("(^|\\s)" + searchClass + "(\\s|$)");
			for (i = 0, j = 0; i < elsLen; i++) {
				if (pattern.test(els[i].className)) {
					classElements[j] = els[i];
					j++;
				}
			}
			return classElements;
		},
		_extends : function (destination, source) {
			for (property in source) {
				destination[property] = source[property];
			}
			return destination;
		},
		_prefix : function () {
			var transitionEnd;
			if (this.css3Key) {
				return this.css3Key;
			}
			var dummyStyle = document.createElement('div').style,
			vendor = (function () {
				var vendors = 't,webkitT,MozT,msT,OT'.split(','),
				t,
				i = 0,
				l = vendors.length;

				for (; i < l; i++) {
					t = vendors[i] + 'ransform';
					if (t in dummyStyle) {
						return vendors[i].substr(0, vendors[i].length - 1);
					}
				}

				return false;
			})();

			transitionEnd = (vendor == '') ? 'transitionend' : vendor + 'TransitionEnd';

			this.css3Key = {
				vendor : vendor,
				cssVendor : vendor ? '-' + vendor.toLowerCase() + '-' : '',
				transform : prefixStyle('transform'),
				transition : prefixStyle('transition'),
				transitionProperty : prefixStyle('transitionProperty'),
				transitionDuration : prefixStyle('transitionDuration'),
				transformOrigin : prefixStyle('transformOrigin'),
				transitionTimingFunction : prefixStyle('transitionTimingFunction'),
				transitionDelay : prefixStyle('transitionDelay'),
				transitionEnd : transitionEnd
			};
			return this.css3Key;

			function prefixStyle(style) {
				if (vendor === '')
					return style;

				style = style.charAt(0).toUpperCase() + style.substr(1);
				return vendor + style;
			}
		},
		_easeOut : function (t, b, c, d) {
			return -c * (t /= d) * (t - 2) + b;
		},
		_addEvent : function (o, type, fn) {
			o.attachEvent ? o.attachEvent('on' + type, fn) : o.addEventListener(type, fn, false);
		},

		_initDoms : function (opts) {
			var that = this,
			c = this.conf,
			wrap = that._id(c.wrapId),
			inner = wrap.innerHTML,
			wrapInner,
			aniWrap,
			items;

			if (c.isLoop) {
				wrapInner = '\
										<div style="' + (c.isVertical ? 'height' : 'width') + ':30000px;zoom:1;">\
											<div style="float:left;">' + inner + '</div>\
											<div style="float:left;">' + inner + '</div>\
										</div>';
			} else {
				wrapInner = '<div style="' + (c.isVertical ? 'height' : 'width') + ':30000px;zoom:1;">' + inner + '</div>';
			}

			wrap.innerHTML = wrapInner;
			aniWrap = wrap.getElementsByTagName('div')[0];
			aniWrap.style.position = 'relative';
			items = this._class(c.itemClass, wrap, 'div');

			for (var i = 0, itemsLen = items.length; i < itemsLen; i++) {
				items[i].style['float'] = c.isVertical ? 'none' : 'left';
			}

			this.doms = {
				wrap : wrap,
				aniWrap : aniWrap,
				items : items,
				oInnerHTML : inner
			};
		},
		_initStates : function (opts) {
			var that = this,
			c = this.conf,
			d = this.doms,
			items = d.items,
			css3Key = this._prefix(),
			scrollDis,
			scrollBy,
			scrollType,
			total,
			wrapSize,
			duration,
			touchDev,
			cssProterty,
			now,
			fromPos,
			transitionAni,
			onTransitionEnd;

			scrollType = c.isVertical ? 'v' : 'h';
			wrapSize = c.isVertical ? d.wrap.clientHeight : d.wrap.clientWidth;
			if (wrapSize > 10000) {
				wrapSize = c.isVertical ? parseInt(d.wrap.currentStyle.height, 10) : parseInt(d.wrap.currentStyle.width, 10);
			}
			total = c.scrollDis ? (c.isLoop ? Math.ceil(d.wrap.getElementsByTagName('div')[0].clientWidth * 2 / c.scrollDis) : Math.ceil(d.wrap.getElementsByTagName('div')[0].clientWidth) / c.scrollDis) : items.length;
			duration = c.speed / 10;
			scrollDis = d.wrap.clientWidth * c.scrollBy;

			touchDev = 'touchstart' in window;

			cssProterty = c.isVertical ? (c.absoluteAni ? 'top' : 'marginTop') : (c.absoluteAni ? 'left' : 'marginLeft');

			if (css3Key.vendor == '') {
				transitionAni = 'transform ' + duration + 's ease-out';
			} else if (css3Key.vendor) {
				transitionAni = '-' + css3Key.vendor + '-transform ' + duration + 's ease-out';
			}

			onTransitionEnd = function () {
				var c = that.conf,
				d = that.doms,
				s = that.states,
				css3Key = that._prefix();
				c.onAniEnd && c.onAniEnd.call(that);
				s.animating = false;

				d.aniWrap.removeEventListener(css3Key.transitionEnd, arguments.callee, false);
			}

			this.states = {
				curIndex : c.startOn,
				scrollType : scrollType,
				wrapSize : wrapSize,
				total : total,
				scrollDis : scrollDis,
				duration : duration,
				touchDev : touchDev,
				cssProterty : cssProterty,
				to : null,
				now : 0,
				fromPos : null,
				movedPos : null,
				animating : false,
				transitionAni : transitionAni,
				onTransitionEnd : onTransitionEnd
			};
		},
		_initEvents : function (opts) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			wrap = d.wrap;
			this._addEvent(wrap, 'mouseover', function () {
				clearInterval(that._auto);
				that._auto = null;
			});
			this._addEvent(wrap, 'mouseout', function () {
				that.autoSlide();
			});

			if ('ontouchstart' in window) {
				/*this._addEvent(wrap, 'touchstart', function(e){if(!s.animating) that._touchstart(e);});
				this._addEvent(wrap, 'touchmove', function(e){if(s.touched) that._touchmove(e);});
				this._addEvent(wrap, 'touchend', function(e){if(s.touched) that._touchend(e);});*/
				this._addEvent(wrap, 'touchstart', function (e) {
					that._touchstart(e);
				});
				this._addEvent(wrap, 'touchmove', function (e) {
					that._touchmove(e);
				});
				this._addEvent(wrap, 'touchend', function (e) {
					that._touchend(e);
				});
			}
		},
		_launch : function () {
			var c = this.conf,
			d = this.doms,
			s = this.states;

			if (c.startOn) {
				this._moveToEle(c.startOn);
				s.fromPos = -s.wrapSize * c.startOnPos;
			} else if (c.startOnPos) {
				this._moveByPx(c.startOnPos, true);
				s.fromPos = -c.startOnPos;
			}

			c.onReady && c.onReady.call(this);

			if (c.autoPlay) {
				this.autoSlide();
			}
		},
		_touchstart : function (e) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			css3Key = this._prefix();
			// document.querySelector('#cl').innerHTML += ' start';
			if (e.touches.length !== 1) {
				return;
			}

			if (c.isLoop) {
				if (s.curIndex == 0) {
					s.curIndex += s.total / 2;
				}
				if (s.curIndex == s.total - 1) {
					s.curIndex -= s.total / 2;
				}
				d.aniWrap.style[css3Key.transition] = 'all 0s ease-out';
				this._moveToEle(s.curIndex);
			}

			clearInterval(this._auto);
			this._auto = null;

			this.touchInitPos = e.touches[0].pageX;
			this.touchInitPosY = e.touches[0].pageY;
			this.deltaX1 = this.touchInitPos;
			this.startPos =  - s.curIndex * s.scrollDis;
			s.touched = true;
		},
		_touchmove : function (e) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			css3Key = this._prefix(),
			finalDis;
			if (e.touches.length !== 1) {
				return;
			}

			var cX = e.touches[0].pageX;
			var cY = e.touches[0].pageY;
			var dX = cX - this.touchInitPos;
			var dY = cY - this.touchInitPosY;
			if (Math.abs(dY) === 0 || Math.abs(dX) / Math.abs(dY) >= 1) {
				e.preventDefault();
			}

			this.deltaX2 = cX - this.deltaX1; //记录当次移动的偏移量
			var finalDis = this.startPos + dX;
			var pos = finalDis + 'px,0';
			var toPosStr = c.isVertical ? ('translate3d(0,' + finalDis + 'px,0)') : ('translate3d(' + finalDis + 'px,0,0)');
			d.aniWrap.style[css3Key.transform] = 'translate3d(' + pos + ', 0)';
		},
		_touchend : function (e) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			css3Key = this._prefix(),
			toPos;

			if (this.deltaX2 < -30) {
				this.next();
				this.deltaX2 = 0;
			} else if (this.deltaX2 > 30) {
				this.prev();
				this.deltaX2 = 0;
			} else {
				this._slideToCss3(s.curIndex);
			}
			s.touched = false;
		},
		_moveToEle : function (to, isOffset, onMoveEnd) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			curIndex = s.curIndex,
			toIndex = isOffset ? curIndex + to : to,
			aniWrap = d.aniWrap,
			items = d.items,
			toItem = items[toIndex],
			css3Key = this._prefix(),
			toPos,
			toPosStr;

			if (to != curIndex) {
				c.onIndexChanged && c.onIndexChanged.call(this, to);
			}

			toPos = c.isVertical ?  - toItem.offsetTop :  - toItem.offsetLeft;
			/*if(c.isLoop && toIndex > s.total / 2 - 1){
			toPos = toPos - s.scrollDis * s.total / 2;
			}*/
			toPosStr = c.isVertical ? ('translate3d(0,' + toPos + 'px,0)') : ('translate3d(' + toPos + 'px,0,0)');

			if (css3Key.vendor !== false) {
				// if(false){
				if (s.animating) {
					aniWrap.removeEventListener(css3Key.transitionEnd, s.onTransitionEnd, false);
				}

				aniWrap.style[css3Key.transition] = 'all 0s ease-out';
				aniWrap.style[css3Key.transform] = toPosStr;
				s.curIndex = toIndex;
				setTimeout(function () {
					onMoveEnd && onMoveEnd.call(that);
				}, 20);
			} else {
				aniWrap.style[s.cssProterty] = toPos + 'px';
				s.curIndex = to;
				onMoveEnd && onMoveEnd.call(this);
			}

		},
		_slideToCss3 : function (fIndex) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			items = d.items,
			aniWrap = d.aniWrap,
			css3Key = this._prefix(),
			toPos,
			toPosStr;

			aniWrap.style[css3Key.transition] = s.transitionAni;

			toPos = c.isVertical ?  - items[fIndex].offsetTop :  - items[fIndex].offsetLeft;
			toPosStr = c.isVertical ? ('translate3d(0,' + toPos + 'px,0)') : ('translate3d(' + toPos + 'px,0,0)');

			aniWrap.style[css3Key.transform] = toPosStr;

			if (s.animating) {
				aniWrap.removeEventListener(css3Key.transitionEnd, s.onTransitionEnd, false);
			}

			aniWrap.style[css3Key.transform] = toPosStr;
			aniWrap.addEventListener(css3Key.transitionEnd, s.onTransitionEnd, false);

			s.curIndex = fIndex;
		},
		_slideToTdt : function (to) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			aniWrap = d.aniWrap,
			cssProterty = s.cssProterty,
			duration = s.duration * 1000,
			now = s.now,
			wrapSize = s.wrapSize,
			movedPos;
			if (s.to === null || s.to == to) {
				s.fromPos =  - s.curIndex * wrapSize;
			} else {
				s.fromPos = s.movedPos;
				s.now = 0;
			}

			if (duration - now < 20) {
				// aniWrap.style[cssProterty] = -to * wrapSize + 'px';
				aniWrap.style[cssProterty] = -to * wrapSize + 'px';
				s.curIndex = to;

				clearTimeout(this.aniInterval);
				this.aniInterval = null;

				s.now = 0;
				s.to = null;
				s.fromPos =  - s.curIndex * wrapSize;
				s.movedPos = null;

				c.onAniEnd && c.onAniEnd.call(this);
				s.animating = false;
				return;
			}

			movedPos = this._easeOut(now, s.fromPos, -to * wrapSize - s.fromPos, duration);
			aniWrap.style[cssProterty] = movedPos + 'px';
			s.now += 20;
			s.movedPos = movedPos;

			this.aniInterval = setTimeout(function () {
					that._slideToTdt(to);
					// that._tranditionSlide.apply(that, args.slice(0, 6).concat([later]));
				}, 20);
		},
		prev : function () {
			var c = this.conf,
			d = this.doms,
			s = this.states,
			curIndex = s.curIndex,
			toIndex;
			// if(s.animating) return;
			if (c.isLoop && curIndex == 0) {
				this._moveToEle(s.total / 2, false, function () {
					toIndex = s.curIndex - c.scrollBy;
					this.slideTo(toIndex);
					this.autoSlide();
				});
			} else {
				toIndex = s.curIndex - c.scrollBy;
				this.slideTo(toIndex);
				this.autoSlide();
			}
		},
		next : function () {
			var c = this.conf,
			d = this.doms,
			s = this.states,
			curIndex = s.curIndex,
			toIndex;
			// if(s.animating) return;
			if (c.isLoop && curIndex == s.total - 1) {
				this._moveToEle(s.total / 2 - 1, false, function () {
					toIndex = s.curIndex + c.scrollBy;
					this.slideTo(toIndex);
					this.autoSlide();
				});
			} else {
				toIndex = s.curIndex + c.scrollBy;
				this.slideTo(toIndex);
				this.autoSlide();
			}
		},
		slideByEle : function (offset) {
			var toIndex = this.states.curIndex + offset;
			this.slideTo(toIndex);
		},
		slideTo : function (i) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			curIndex = s.curIndex,
			total = s.total,
			css3Key = this._prefix(),
			fnlIndex,
			cssProterty;

			fnlIndex = (i > total - 1) ? i % total : (i < 0 ? (total + i % total) : i);
			/*if(c.isLoop && Math.abs(fnlIndex - curIndex) > total / 2){
			fnlIndex = fnlIndex < curIndex ? fnlIndex + total / 2 : fnlIndex - total / 2;
			}*/
			if (fnlIndex == curIndex) {
				return;
			}

			c.onIndexChanged && c.onIndexChanged.call(this, fnlIndex);

			s.animating = true;

			if (c.autoPlay) {
				this.autoSlide();
			}

			c.onAniStart && c.onAniStart.call(this);
			// css3 animation
			if (css3Key.vendor !== false) {
				// if(false){
				this._slideToCss3(fnlIndex);
			}
			// traditional animation
			else {
				this._slideToTdt(fnlIndex);
			}
		},
		autoSlide : function () {
			var that = this;
			if (this._auto) {
				clearInterval(this._auto);
				this._auto = null;
			}
			this._auto = setInterval(function () {
					that.next();
				}, this.conf.autoInterval * 1000);
		},
		disableAuto : function () {
			if (this._auto) {
				clearInterval(this._auto);
				this._auto = null;
			}
		},
		insertItem : function (item) {
			// body...
		},
		refresh : function (conf) {
			var that = this,
			c = this.conf,
			d = this.doms,
			s = this.states,
			css3Key = this._prefix(),
			wrapSize,
			scrollDis;

			wrapSize = (conf && conf.width) ? conf.width : c.isVertical ? d.wrap.clientHeight : d.wrap.clientWidth;

			s.scrollDis = wrapSize * c.scrollBy;
			if (s.animating) {
				s.onTransitionEnd();
			}
			d.aniWrap.style[css3Key.transition] = 'all 0s ease-out';
			this._moveToEle(s.curIndex);
		},
		destroy : function () {
			// body...
		}
	};
	return init;
}
();

var slide_00 = new Slider({
    wrapId: 'hd_slider',
    itemClass: 'item',
    sameSize: true,
    startOn: 1,
    // startOnPos : 300,
    scrollBy: 1,
    // scrollDis : 400,
    speed: 3,
    isVertical: false,
    isLoop: true,
    autoPlay: true,
    autoInterval: 5,
    absoluteAni: true,
    onReady: function () {
        var that = this;
        var s = this.states;
        var dots = document.getElementById('hd_slider_dot');
        var btn_prev = document.getElementById('hd_slider_prev');
        var btn_next = document.getElementById('hd_slider_next');
        if ($('.item').size() < 4) return;
        var str = '';
        for (var i = 0; i < this.states.total / 2; i++) {
            if (i == 0) {
                str += '<span class="dot_list current" onmouseover="slide_00.slideToNearBy(' + i + ')"></span>';
            }
            else {
                str += '<span class="dot_list" onmouseover="slide_00.slideToNearBy(' + i + ')"></span>';
            }
        }
        dots.innerHTML = str;
        this.doms.dots = dots.getElementsByTagName('span');
        btn_prev.onclick = function () {
            that.prev();
        };
        btn_next.onclick = function () {
            that.next();
        };
        this.slideToNearBy = function (i) {
            var curIndex = s.curIndex;
            if (curIndex - i == s.total / 2) {
                return;
            }
            if (that.css3Key.vendor) {
                if (Math.abs(curIndex - i) > Math.abs(curIndex - i - s.total / 2)) {
                    i += s.total / 2;
                }
            }

            if (i == 0) {
                i += s.total / 2;
            }
            else if (i == s.total - 1) {
                i -= s.total / 2;
            }
            that.slideTo(i);
        }
    },
    onAniStart: function () {
        // body...
    },
    onAniEnd: function () {
        // body...
    },
    onIndexChanged: function (index) {
        var s = this.states;
        index = index % (s.total / 2);
        if (this.doms.dots) {
            for (var i = 0; i < this.states.total / 2; i++) {
                $(this.doms.dots[i]).removeClass('current');
            }
            $(this.doms.dots[index]).addClass('current');
        }
    },
    onEdge: function (isLeft) {
        // body...
    }
});

// // 获取当前页面宽度
// function getWidth(){
// 	var d = document, dd=d.documentElement;
// 	var w = dd.clientWidth||d.body.clientWidth;
// 	return w;
// }

// function getIEInfo(){
// 	var ie = false;
// 	var ua = navigator.userAgent.toLowerCase();
// 	if (window.ActiveXObject){
// 		ie = {}
// 		ie.b="ie";
// 		ie.v =parseInt(ua.match(/msie ([\d.]+)/)[1]);
// 	}
// 	return ie;
// }

// function resizeListener(){
// 	var w = getWidth();
// 	var slideW = w;
// 	if(slideW > 1000){
// 		slideW = 1000;
// 	}
// 	var hd_slider = $('#hd_slider')[0];
// 	var hd_slider_w = $('#hd_slider').parent()[0];
// 	var items = $('item', '#hd_slider'), itemsLen = items.length;

// 	hd_slider.style['width'] = slideW + 'px';
// 	hd_slider.style['height'] = slideW/2 + 'px';
// 	hd_slider_w.style['height'] = slideW/2 + 'px';
// 	for(var i = 0; i < itemsLen; i++){
// 		items[i].style['width'] = slideW + 'px';
// 		items[i].style['height'] = slideW/2 + 'px';
// 		items[i].getElementsByTagName('img')[0].style['width'] = slideW + 'px';
// 		items[i].getElementsByTagName('img')[0].style['height'] = slideW/2 + 'px';
// 	}

// 	if(window.slide_00 && window.slide_00._moveToEle){
// 		window.slide_00.refresh();
// 	}

// }


// // 监听resize事件
// var resizeTimeout = null;
// window.onload = window.onresize = function(){
// 	var ie = getIEInfo();
// 	if(ie && ie.v < 10){
// 		return;
// 	}
// 	window.clearTimeout(resizeTimeout);
// 	resizeTimeout = window.setTimeout(resizeListener,300);
// };