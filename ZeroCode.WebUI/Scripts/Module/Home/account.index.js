/*! 
 * 鼠标经过二级菜单
 */
;(function($){

	'use strict';
	
	var Bmenu = function (element,option) {
		this.$element = $(element);
		this.options = $.extend({},Bmenu.DEFAULTS,option);
	}	
	
	Bmenu.VERSION  = '1.0.0';

	Bmenu.DEFAULTS = {
		duration: 400,
		menuActiveClass: 'frame_menu_active'
	};
	
	Bmenu.prototype.activeMenu = function(){
		this.$element.addClass(this.options.menuActiveClass);
	};

	Bmenu.prototype.passiveMenu = function(){
		this.$element.removeClass(this.options.menuActiveClass);
	};
	
	Bmenu.prototype.showSecondary = function(secondary){
		if(secondary.not(':animated')){
			secondary.slideDown(this.options.duration);
		}
	};
	
	Bmenu.prototype.hideSecondary = function(secondary){
		if(secondary.is(':animated')){
			secondary.stop(true,true)
		}
		secondary.hide();
	};
	
	function Plugin(option) {
		return this.each(function () {
			var $this = $(this);
			var data = $this.data('frame.bmenu')
			if(!data){
				$this.data('frame.bmenu', (data = new Bmenu(this,option)))
			};

			var secondary = $this.next('[data-menu="secondary"]');
			
			data.showSecondary(secondary);
			data.activeMenu();
			
			secondary.one('mouseenter.frame.bmenu',function(){
				$(this).show();
				data.activeMenu();
			}).one('mouseleave.frame.bmenu',function(){
				$(this).hide();
				data.passiveMenu();
			});
			
			$this.one('mouseenter.frame.bmenu',function(){
				data.showSecondary(secondary);
				data.activeMenu();
			}).one('mouseleave.frame.bmenu',function(){
				data.hideSecondary(secondary);
				data.passiveMenu();
			});
			
		});
	}
	
	var old = $.fn.bmenu;

	$.fn.bmenu             = Plugin;
	$.fn.bmenu.Constructor = Bmenu;

	$.fn.bmenu.noConflict = function () {
		$.fn.bmenu = old;
		return this;
	}

	$(document).on('mouseenter','[data-menu="board"]',function(){
		$(this).each(function(){
			var durationValue;
			if($(this).attr('data-board-duration')){
				durationValue = parseInt($(this).attr('data-board-duration'));
				Plugin.call($(this),{duration: durationValue});
			}else{
				Plugin.call($(this));
			}
		});
	});
	
})(jQuery);

/*! 
 * 轮播 切换效果
 */
var bannerTime;
function bannerToggle(){
	if(bannerTime){
		clearTimeout(bannerTime);
	}
	var bannerTime = setTimeout(bannerRoll,4500);
}
function bannerRoll(){
	var bannerNow = $('.login_wrapper').children('.login_banner_img.active');
	var bannerNext = $('.login_wrapper').children('.login_banner_img.active').next('.login_banner_img');
	if(bannerNext.length === 0){
		bannerNext = $('.login_wrapper').children('.login_banner_img').eq(0);
	}
	bannerNow.removeClass('active');
	bannerNext.addClass('active');
	bannerNow.animate({opacity: 0},800);
	bannerNext.animate({opacity: 1},800, function(){
		bannerToggle();
	});
}

/*! 
 * 展示区域控制
 */
function setBanner(){
	var winWidth = $(window).width();
	if(winWidth <= 1200){ 
		return; 
	}else {
		$('.login_banner_img').css('left',-(1920-winWidth)/2);
	}
}
$(document).ready(function(){
	setBanner();
});
$(window).resize(function(){
	setBanner();
});

/*! 浏览器兼容提示 **/
function browerWarning(){
	var body = $('body');
	var tipsBox = '<div class="login_tips_mask"></div>';
	tipsBox +=	'<div class="login_tips">'
					+'<div class="login_tips_dialog" style="top:'+ ($(window).height() - 170)/2 +'px; left: '+ ($(window).width() - 370)/2 +'px;">'
						+'<div class="login_tips_border"></div>'
						+'<div class="login_tips_cont">'
							+'<div class="login_tips_header">'
								+'<div class="login_tips_title">警告</div>'
							+'</div>'
							+'<div class="login_tips_body">'
								+'<p class="login_browser_tips">本系统目前只支持 <span class="login_browser_tips_em">IE内核浏览器、火狐、Opera、Safari浏览器，</span>请更换您的浏览器。</p>'
							+'</div>'
						+'</div>'
					+'</div>'
				+'</div>';
	body.append(tipsBox);
}