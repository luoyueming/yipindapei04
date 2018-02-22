/*
 *  author tiger
 */
(function(){
	
	function $(id,tag){
		var re=(id&&typeof id!="string") ? id : document.getElementById(id);
		if(!tag){
			return re;
		}else{
			return re.getElementsByTagName(tag);
		}
	}

	function getClass(sClass, oParent) {
		var aClass = [];
		var reClass = new RegExp("(^| )" + sClass + "( |$)");
		var aElem = $(oParent,"*");
		for (var i = 0; i < aElem.length; i++) reClass.test(aElem[i].className) && aClass.push(aElem[i]);
		return aClass
	}

	function getStyle(obj, attr){
		return parseFloat(obj.currentStyle ? obj.currentStyle[attr] : getComputedStyle(obj, null)[attr])	
	}

	function inputFocus(id,defaultValue){
		var obj = $(id),
			v = defaultValue;
		Event.add(obj,'focus',function(){
			if(obj.value == v){
				obj.value = '';
				Element.addClass(obj,'on');
			}
		})
		Event.add(obj,'blur',function(){
			if(obj.value == '' || obj.value == v){
				obj.value = v;
				Element.removeClass(obj,'on');
			}
		})
	}

	function dropMenu(id,tag,type){
		var obj = $(id),
			menu = $(obj,tag)[0],
			timer = null,
			state = false,
			type = type || 'click';

		if(!obj || !menu) return;
		Event.add(obj,type,function(e){
			clear();
			if(state && type == 'click'){
				Element.removeClass(menu,'on');
				state = false;
				return;
			}
			Element.addClass(menu,'on');
			state = true;
		})
		if(type == 'click'){
			Event.add(obj,'mouseover',function(){
				if(!state) return;
				clear();
				Element.addClass(menu,'on');
				state = true;
			})
		}
		Event.add(obj,'mouseout',function(){
			if(timer) return;
			timer = setTimeout(function(){
				clear();
				state = false;
				Element.removeClass(menu,'on');
			},100)
		})
		function clear(){
			if(timer){
				clearTimeout(timer);
				timer = null;
			}
		}
	}

	function search(id,defaultValue){
		var obj = $(id),
			state = false,
			timer = null;

		if(!obj) return;

		var input = obj.getElementsByTagName('input')[0],
			form = obj.getElementsByTagName('form')[0],
			defaultValue = encodeURIComponent(defaultValue);

		Event.add(obj,'click',function(e){
			var tar = Event.target(e);
			Event.stop(e);
			if(tar.nodeName == 'SPAN' && tar.className == 'icon_search'){
				Element.addClass(obj,'search_show');
				return;
			}
			if(tar.nodeName == 'DIV' && tar.className == 'close'){
				Element.removeClass(obj,'search_show');
				return;
			}
			if(tar.nodeName == 'DIV' && tar.className == 'btn'){
				submit();
			}
		})
		
		Event.add(input,'focus',function(){
			var v = encodeURIComponent(input.value);
			if(v == '' || v == defaultValue){
				input.value = '';
			}
			Element.addClass(input,'on');
			Event.add(document,'keydown',keydown_submit);
		})
		Event.add(input,'blur',function(){
			var v = encodeURIComponent(input.value);
			if(v == '' || v == defaultValue){
				input.value = decodeURIComponent(defaultValue);
				Element.removeClass(input,'on');
			}
			Event.remove(document,'keydown',keydown_submit);
		})
		function keydown_submit(e){
			if(e.keycode == 13){
				submit();
			}
		}
		function submit(e){
			var v = encodeURIComponent(input.value);
			if(v == '' || v == defaultValue){
				return;
			}
			form.submit();
		}
	}

	function upper(m){
		return m.replace(/^[a-z]/g,function(n){ return n.charAt(0).toUpperCase()});
	}


	function scroll(opt){
		if(!opt) return;
		var obj = $(opt.id);
		if (!obj) {return};
		var box = Element.getChild(obj,opt.boxTag)[0],
			list = Element.getChild(box,opt.singleTag),
			loop = opt.loop ? true : false,
			leftBtn = opt.leftBtn ? $(opt.leftBtn) : null,
			rightBtn = opt.rightBtn ? $(opt.rightBtn) : null,
			auto = opt.auto ? true : false,
			c = opt.current ? parseInt(opt.current) : 0,
			path = opt.path ? opt.path : 'left',
			callback = opt.callback ? opt.callback : null,
			after_complete = opt.after_complete ? opt.after_complete : null,
			step = opt.step ? parseInt(opt.step) : 1,
			anima = null,
			singleSize = 0,
			length = list.length,
			max = 0,
			time = opt.time ? parseInt(opt.time) : 5000,
			state = false,
			from = 0,
			to = 0,
			fix =0,
			range = [0,0],
			move = null;
		if(length <= 1) return;
		function init(){
			if(path == 'left' || path =='right'){
				singleSize = list[0]['offsetWidth'];
				singleSize = singleSize ? singleSize : getStyle(list[0],'width');
				fix = Math.round(obj['offsetWidth']/singleSize);
			}else{
				singleSize = list[0]['offsetHeight'];
				fix = Math.round(obj['offsetHeight']/singleSize);
			}
			fix = Math.max(1,fix);
			
			path = (path == 'left' || path == 'right') ? 'marginLeft' : 'marginTop';
			
			max = singleSize*length;
			anima = Anima(box,500);
			to = -step*singleSize;
			
			if(auto || loop){
				range = [0,length];
				box.innerHTML += box.innerHTML;
				if(auto) {
					loop = true;
					move = setTimeout(play,time);
				}
			}else{
				range = [0,length-fix];
			}
			reset(c==0 ? 0 : 1); //have bug, wait to fix
			if(callback) callback(c,range);
			handle();
		}
		function handle(){
			if(leftBtn){
				Event.add(leftBtn,'click',function(){
					change();
				})
			}
			if(rightBtn){
				Event.add(rightBtn,'click',function(){
					change(null,true);
				})
			}
			Event.add(box,'mouseover',function(){
				clear();
			})
			Event.add(box,'mouseout',function(){
				if(auto){
					move = setTimeout(play,time);
				}
			})
		}
		function change(m,type){
			
			clear();
			var n = type ? 1 : 0;
			if( (m||m==0) ){
				if(n){
					if(m<c){
						c = m+length;
					}else{
						c = m;
					}
				}else{
					if(m>c){
						resetPos(c+length);
					}
					c=m;
				}
			}else{
				if(state) return;

				if(n){
					if(c == range[1]){
						if(!auto && !loop) return;
						reset(1-n);
					}
					c += step*1;
				}else{
					if(c == range[0]){
						if(!auto && !loop) return;
						reset(1-n);
					}
					c -= step;
				}
			}
			to = -c*singleSize;
			state = true;
			var s = {};
			s[path] = to;
			if(callback) {
				if(auto && c == range[1]){
					callback(0,range,n)
				}else{
					callback(c>=length?c-length:c,range,n)
				}
			};
			anima.start(s);
			anima.complete(function(){
				state = false;
				if(c>=length){
					c -= length;
					resetPos(c);
				}
				if(loop){
					if (c >= range[1]){
						reset(1-n);
					}
				}
				if(auto){
					if(c== range[n]-1 && !loop) return;
					if (c== range[n]){
						reset(1-n);
					}
					move = setTimeout(play,time);
				}
				if(after_complete) after_complete();
			})
		}
		
		function play(){
			change(null,true);
		}
		function clear(){
			clearTimeout(move);
			move = null;
		}
		function reset(n){
			box.style[path] = -Math.abs(range[n]*singleSize) + 'px';
			c = range[n];
		}
		function resetPos(n){
			box.style[path] = -Math.abs(n*singleSize) + 'px';
		}
		init();
		function setChange(s,t){
			return change(s,t);
		}
		function getCurrent(){
			return c;
		}
		return {
			setChange : setChange,
			range : range,
			current : getCurrent,
			length : length,
			setposition : resetPos
		}
	}

	
	function focus(id,time,left,right,callbackFun){

		var obj = $(id),
			imgs = Element.getChild(Element.getChild(obj,'div')[0],'div'),
			_imgs = $(obj,'img'),
			dd = document.createElement('div'),
			lis = null,
			current = 0,
			oLeft = $(left)||null,
			oRight = $(right)||null,
			state = false,
			old = -1,
			time = time || 5000,
			autoTimer = null,
			lazyTimer = null,
			anima = [],
			ie6 = !window.XMLHttpRequest && window.ActiveXObject ? true : false,
			moving = false,
			imgState = [],
			timer = null;

		function init(){

			var str = '';
			dd.className = 'point';
			for(var i=0,l=imgs.length;i<l;i++){
				imgState[i] = false;
				changeState(i);
				str += '<span class="g-point"></span>';
				anima[i] = Anima(imgs[i]);
			}
			dd.innerHTML = str;
			obj.appendChild(dd);
			imgs[0].style.cssText += ';opacity:1;display:block;z-index:2;';
			handle();
			Event.add(obj,'mouseover',function(){
				clear();
			})
			Event.add(obj,'mouseout',function(){
				play();
			})
			if (oLeft&&oRight) {
				Event.add(obj,'mouseover',function(){
					Element.addClass(oLeft,'active');
					Element.addClass(oRight,'active');
				})
				Event.add(obj,'mouseout',function(){
					Element.removeClass(oLeft,'active');
					Element.removeClass(oRight,'active');
				})
				Event.add(oRight,'click',function(){
					clear();
					next();
				})
				Event.add(oLeft,'click',function(){
					clear();
					prev();
				})
				
			};
			//play();
			preLoad(0,play);
			if(callbackFun) callbackFun(0);
		}

		function preLoad(n,callback){
			if(_imgs[n].src == '' ) _imgs[n].src = _imgs[n].getAttribute('_src');
			callback ? changeState(n,callback) : changeState(n);
		}

		function changeState(n,callback){
			//imgState[n] = true;
			//if(callback) callback();
			
			if(_imgs[n].complete || _imgs[n].readyState == 'loaded' || _imgs[n].readyState == 'complete'){
				imgState[n] = true;
				if(callback) callback();
			}else{
				Event.add(_imgs[n],'load',function(){
					imgState[n] = true;
					if(callback) callback();
					Event.remove(_imgs[n],'load',arguments.callee);
				})
				Event.add(_imgs[n],'error',function(){
					imgState[n] = true;
					if(callback) callback();
					Event.remove(_imgs[n],'error',arguments.callee);
				})
			}
			
			var m = n+1;
			if( m <= _imgs.length-1 && !imgState[m]){
				preLoad(m);
			}
		}
		function auto(){
			var next = current;
			next++;
			if( next > imgs.length-1){
				next = 0;
			}
			if(!imgState[next]){
				clear();
				if(_imgs[next].src == ''){
					preLoad(next,auto);
					return;
				}
				lazyTimer = setTimeout(function(){
					lazyTimer = null;
					auto();
				},300);
				return;
			}
			change(next);
		}
		function next(){
			var n = current;
			n++;
			if (n > imgs.length-1) {
				n = 0;
			};
			change(n,true);
		}
		function prev(){
			var n = current;
			n--;
			if (n<0) {
				n = imgs.length-1;
			};
			change(n,true);
		}

		function change(c,btn){
			var btn = btn || false;
			moving = true;

			if(callbackFun) callbackFun(c);
			clear();
			if(old>-1){ 
				anima[old].cancel();
				imgs[old].style.cssText = 'display:none';
			}
			old = current;
			anima[old].cancel();
			imgs[old].style.cssText += ';display:block;opacity:1;z-index:1;';
			Element.removeClass(lis[old],'on');
			current = c;
			imgs[current].style.cssText += ';display:block;opacity:0;z-index:2;';
			Element.addClass(lis[current],'on');

			if(ie6){
				moving = false;
				if (!btn) {
					
				    play();
				};
			}else{
				anima[current].start({opacity:1});
				anima[current].complete(function(){
					moving = false;
					if (!btn) {
						
					    play();
					};
					
				});
			}
			
		}

		function handle(){
			lis = $(dd,'span');
			Element.addClass(lis[0],'on');
			for(var i=0,l=lis.length;i<l;i++){
				fun(i);
			}
			function fun(n){
				Event.add(lis[n],'mouseover',function(){
					clearTimeout(timer);
					timer = setTimeout(function(){
						contral(n);
					},300)
				})
			} 
		}
		function contral(n){
			if(moving) return;
			if(n==current) return;
			if(_imgs[n].src == '' && _imgs[n].getAttribute('_src') ) _imgs[n].src = _imgs[n].getAttribute('_src');
			change(n);
		}
		function getCurrent(){
			return current;
		}
		function clear(){
			clearTimeout(autoTimer);
			clearTimeout(lazyTimer);
			lazyTimer = null;
			autoTimer = null;
		}
		function play(){
			clear();
			autoTimer = setTimeout(auto,time);
		}
		function delDiv(){
			for(var i=0;i<imgs.length;i++){
				var oImg = imgs[i].getElementsByTagName('img')[0]||null;
				if (!oImg) {
					Element.getChild(obj,'div')[0].removeChild(imgs[i]);
				};
			}
			imgs = Element.getChild(Element.getChild(obj,'div')[0],'div');
			_imgs = $(obj,'img');
			if (imgs.length>1) {
				init();
			};
		}
		delDiv();
		
		return {
			change : contral,
			getCurrent : getCurrent,
			length : imgs.length
		}
	}
	


    
	function tagSwitch(tit,box,s,fn,time,show){
		tit=tit.split('/');
		box=box.split("/");
		!s&&(s="mouseover");
		!show&&(show=0);
		var ts=$(tit[0]),
			bs=$(box[0]);
		if (!ts || !bs) {return};
		var n=0,
			tx=tit[2],
			bx=box[2],
			now=-1,i,c,
			old=-1,
			current;
		ts = Element.getChild(ts,tit[1]);
		bs = Element.getChild(bs,box[1]);
		n=ts.length;

		for(i=0;i<n;i++){
			if( (' ' + ts[i].className + ' ').indexOf(' on ') > -1 ){
				current = i;
			}
			reg(ts[i],bs[i],i);
		};
		function reg(tv,bv,i){
			var timer = null;
			Element.removeClass(tv,'on');
			Element.removeClass(bv,'on');
			tv.old = tv.className || '';
			bv.old = bv.className || '';
			Event.add(tv,s,function(){
				if(timer) return;
				timer = setTimeout(function(){
					timer = null;
					clearInterval(c);
					init(i);	
				},50);
			});
			Event.add(tv,'mouseout',function(){
				if(timer){
					clearTimeout(timer);
					timer = null;
				}
			})
			if(show!=-1&&time){
				Event.add(bv,"mouseover",function(){clearInterval(c);});
				Event.add(bv,"mouseout",go);
				Event.add(tv,"mouseout",function(){
					if(timer){
						clearTimeout(timer);
						timer = null;
					}
					go();
				});
			}
			else if(show==-1&&s=="mouseover"){
				Event.add(tv,"mouseout",function(){init(0);});
			}
		}
		init(current);
		if(show!=-1&&time){c=setInterval(auto,time);}
		function go(){clearInterval(c);c=setInterval(auto,time);}
		function init(m){
			if(m==now) return;
			if(old>-1){
				Element.removeClass(ts[old],'old');
				Element.removeClass(bs[old],'old');
			}
			if(now>-1){
				Element.addClass(ts[now],'old');
				Element.addClass(bs[now],'old');
				Element.removeClass(ts[now],'on');
				Element.removeClass(bs[now],'on');
				old = now;
			}
			if(m>-1){
				Element.addClass(ts[m],'on');
				Element.addClass(bs[m],'on');
				now = m;
			}
			fn&&fn(ts[m],bs[m],m);
		}

		function auto(){
			var s = now;
			(s<n-1)?s++:s=0;
			init(s);
		};
	}
	
	function loadScript(url,callback,charset){
		var script = document.createElement('script');
			script.setAttribute('async',true);
			script.src = url;
			if(charset) script.charset = charset;
			script.readyState ? script.onreadystatechange = function(){
				if(script.readyState == 'loaded' || script.readyState =='complete'){
					setTimeout(function(){
						if(callback) callback();
					},100)

				}
			}:
			script.onload = function(){
				if(callback) callback();
			}
			document.getElementsByTagName('head')[0].appendChild(script);
	}


	function lazyLoad(id,tarid,type){
		if(!id || !tarid) return;
		var obj = document.getElementById(id),
			tar = document.getElementById(tarid),
			n = 0,
			inner = '',
			div = document.createElement('div');
		if(!obj || !tar) return;
		if(type){
			obj.appendChild(tar);
			return;
		}
		inner = tar.innerHTML.replace(/\<script[^\>]{1,}(src)[^\>]{1,}\>\<\/script\>/i,'');
		inner = inner.replace(/\<div\s{1,}adCount.*\<\/div\>/i,'');
		div.innerHTML = inner;
		obj.appendChild(div);
		//tar.innerHTML = ''; /** innerHTMl script style not run **/ 
		inner = null;
	}


	function rss(opt){
		var text = $(opt.emailText),
			btn = $(opt.rssBtn),
			obj = $(opt.rssSub),
			closeBtn = $(obj,'span')[0],
			title = [$(obj,'i')[0],$(obj,'em')[0]],
			success = $(opt.rssSuccess),
			exitBtn = $(success,'a')[0],
			exit = $(opt.rssExit),
			exitInput = $(exit,'input'),
			errorText = [$(opt.errorEmail),$(exit,'span')[0]],
			dl = $(exit,'dl')[0],
			exitSuccess = $(exit,'b')[0],
			reg = new RegExp('^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$'),
			url_sub = 'http://g.yoka.com/edm/index.php',
			head = $(document,'head')[0];

		Event.add(btn,'click',function(){
			if(text.value == ''){
				errorText[0].innerHTML = decodeURIComponent("%E8%AF%B7%E8%BE%93%E5%85%A5%E9%82%AE%E7%AE%B1%E5%9C%B0%E5%9D%80");
				errorText[0].style.display = 'block';
				return;
			}
			if(!/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(text.value)){
				errorText[0].innerHTML = decodeURIComponent("%E8%AF%B7%E8%BE%93%E5%85%A5%E6%AD%A3%E7%A1%AE%E7%9A%84Email%E5%9C%B0%E5%9D%80");
				errorText[0].style.display = 'block';
				return;
			}
			create(url_sub + '?email='+text.value + '&action=order&callback=yo.rss.sub')
		})
		Event.add(text,'focus',function(){
			errorText[0].innerHTML = '';
			errorText[0].style.display = 'none';
		})
		function sub(v){
			if(v==-2){
				alert(decodeURIComponent("%E8%AE%A2%E9%98%85%E5%A4%B1%E8%B4%A5"));
				return;
			}
			text.value = '';
			obj.style.display = 'block';
			success.style.display = 'block';
			title[0].style.display = 'block';

			title[1].style.display = 'none';
			exit.style.display = 'none';
		}
		Event.add(exitBtn,'click',function(e){
			Event.stop(e);
			
			success.style.display = 'none';
			title[0].style.display = 'none';

			exit.style.display = 'block';
			title[1].style.display = 'block';
			exit.style.display = 'block';
			dl.style.display = 'block';
			errorText[1].style.display = 'none';
			exitSuccess.style.display = 'none';
		})
		Event.add(exitInput[1],'click',function(){
			if(exitInput[0].value == ''){
				errorText[1].innerHTML = decodeURIComponent("%E8%AF%B7%E8%BE%93%E5%85%A5%E9%82%AE%E7%AE%B1%E5%9C%B0%E5%9D%80");
				errorText[1].style.display = 'block';
				return;
			}
			if(!/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/.test(exitInput[0].value)){
				errorText[1].innerHTML = decodeURIComponent("%E8%AF%B7%E8%BE%93%E5%85%A5%E6%AD%A3%E7%A1%AE%E7%9A%84Email%E5%9C%B0%E5%9D%80");
				errorText[1].style.display = 'block';
				return;
			}
			create(url_sub+'?email='+exitInput[0].value+'&action=unorder&callback=yo.rss.exitFn');
		})
		Event.add(closeBtn,'click',function(){
			obj.style.display = 'none';
			success.style.display = 'block';
			title[0].style.display = 'block';

			title[1].style.display = 'none';
			exit.style.display = 'none';
			dl.style.display = 'block';
			errorText[1].innerHTML = '';
			exitInput[0].value = '';
			errorText[1].style.display = 'none';
		})
		
		function exitFn(){
			exitInput[0].value = '';
			dl.style.display = 'none';
			errorText[1].style.display = 'none';
			exitSuccess.style.display = 'block';
		}
		function create(s){
			var script = document.createElement('script');
			script.src = s;
			head.appendChild(script);
		}

		window['yo']['rss']={
			sub : sub,
			exitFn : exitFn
		}
	}

	function shoppingShow(box){
		var oBox = document.getElementById(box);
		if (!oBox) {return};
		var obj = getClass('item',oBox);
		var len = obj.length;
		for(var i=0;i<len;i++){
			change(i);
		}
		function change(n){
			Event.add(obj[n],'mouseover',function(){
				Element.addClass(obj[n],'on');
			})
			Event.add(obj[n],'mouseout',function(){
				Element.removeClass(obj[n],'on');
			})
		}
	}

	function goTop(id){

		var oTop = document.getElementById(id);
		var screenh = parseInt(document.documentElement.clientHeight || document.body.clientHeight);
		var timer = null;
		var scrollTop;

		function scrollFn(){
			scrollTop = document.documentElement.scrollTop || document.body.scrollTop;
			if(scrollTop >= screenh){
				oTop.style.display='block';
			}else{
				oTop.style.display = 'none';
			}
			return scrollTop;
		}
		scrollFn();
		window.onscroll = scrollFn;
		oTop.onclick = function(){
			clearInterval(timer);
			timer = setInterval(function(){
				var now = scrollTop;
				var speed = (0 - now) / 10;
				speed = speed > 0 ? Math.ceil(speed) : Math.floor(speed);
				if(scrollTop == 0){
					clearInterval(timer);
				}
				document.documentElement.scrollTop = scrollTop + speed;
				document.body.scrollTop = scrollTop + speed;
			}, 30);
		};
	}

	function equalHeight(id){
		var box = document.getElementById(id);
		var list = Element.getChild(box, 'div');
		var max = -1;

		for(var i = 0, l = list.length; i < l; i++){
			max = Math.max(max, getStyle(list[i], 'height'));
		}

		for(var i = 0, l = list.length; i < l; i++){
			list[i].style.height = max + 'px';
		}
	}

	window['yo'] = {
		'search' : search,
		'scroll' : scroll,
		'focus' : focus,
		'tagSwitch' : tagSwitch,
		'inputFocus' : inputFocus,
		'rss' : rss,
		'loadScript' : loadScript,
		'lazyLoad' : lazyLoad,
		'dropMenu' : dropMenu,
		'shoppingShow' : shoppingShow,
		'goTop': goTop,
		'equalHeight': equalHeight
	}
	
})();