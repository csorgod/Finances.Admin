$(function() {
	"use strict";
	skinChanger();
    initSparkline();
    
    setTimeout(function() {
        $('.page-loader-wrapper').fadeOut();
	}, 50);
	
	var values2 = getRandomValues();
	var paramsBar = {
        type: 'bar',
        barWidth: 3,
        height: 15,
        barColor: '#0E9BE2'
    };
    $('#mini-bar-chart1').sparkline(values2[0], paramsBar);
    paramsBar.barColor = '#7CAC25';
    $('#mini-bar-chart2').sparkline(values2[1], paramsBar);
    paramsBar.barColor = '#FF4402';
    $('#mini-bar-chart3').sparkline(values2[2], paramsBar);
	paramsBar.barColor = '#ff9800';

	function getRandomValues() {
        // data setup
        var values = new Array(20);

        for (var i = 0; i < values.length; i++) {
            values[i] = [5 + randomVal(), 10 + randomVal(), 15 + randomVal(), 20 + randomVal(), 30 + randomVal(),
                35 + randomVal(), 40 + randomVal(), 45 + randomVal(), 50 + randomVal()
            ];
        }

        return values;
    }

    function randomVal() {
        return Math.floor(Math.random() * 80);
	}
});

// Sparkline
function initSparkline() {
	$(".sparkline").each(function() {
		var $this = $(this);
		$this.sparkline('html', $this.data());
	});

	// Bar charts using inline values
	$('.sparkbar').sparkline('html', { type: 'bar' });	
}

//Skin changer
function skinChanger() {
	$('.choose-skin li').on('click', function() {
	    var $body = $('body');
	    var $this = $(this);
  
	    var existTheme = $('.choose-skin li.active').data('theme');

	    $('.choose-skin li').removeClass('active');
	    $body.removeClass('theme-' + existTheme);
	    $this.addClass('active');
	    var newTheme = $('.choose-skin li.active').data('theme');
	    $body.addClass('theme-' + $this.data('theme'));
		
		var darkLogo = "../assets/images/icon.svg";
	    var lightLogo = "../assets/images/icon.svg";
	    if(newTheme == 'orange' || newTheme == 'purple' || newTheme == 'green'){
			$('#left-sidebar .navbar-brand .logo').attr('src', darkLogo);
	    }
	    else{
			$('#left-sidebar .navbar-brand .logo').attr('src', lightLogo);    
	    }
	});
}

$(document).ready(function() {

	// sidebar navigation
	$('#main-menu').metisMenu();

	// sidebar nav scrolling
	$('#left-sidebar .sidebar-scroll').slimScroll({
		height: 'calc(100vh - 65px)',
		wheelStep: 10,
		touchScrollStep: 50,
		color: '#1c222c',
		size: '2px',
		borderRadius: '3px',
		alwaysVisible: false,
		position: 'right',
	});

	// toggle fullwidth layout
	$('.btn-toggle-fullwidth').on('click', function() {
		if(!$('body').hasClass('layout-fullwidth')) {
			$('body').addClass('layout-fullwidth');
			$(this).find(".fa").toggleClass('fa-arrow-left fa-arrow-right');

		} else {
			$('body').removeClass('layout-fullwidth');
			$(this).find(".fa").toggleClass('fa-arrow-left fa-arrow-right');
		}
	});

	// off-canvas menu toggle
	$('.btn-toggle-offcanvas').on('click', function() {
		$('body').toggleClass('offcanvas-active');
	});

	$('#main-content').on('click', function() {
		$('body').removeClass('offcanvas-active');
	});

	$('.right_toggle, .overlay').on('click', function() {
		$('#rightbar').toggleClass('open');
		$('.overlay').toggleClass('open');
	});

	$('.megamenu_toggle').on('click', function() {
		$('#megamenu').toggleClass('open');
	});

	// navbar search form
	$('.navbar-form.search-form input[type="text"]')
	.on('focus', function() {
		$(this).animate({
			width: '+=50px'
		}, 300);
	})
	.on('focusout', function() {
		$(this).animate({
			width: '-=50px'
		}, 300);
	});

	// right chat bar 
	$(".rightbar .right_chat li a, .rightbar .back_btn").on('click', function(){
        $("#rightbar").toggleClass("detail");
    });

	// Bootstrap tooltip init
	if($('[data-toggle="tooltip"]').length > 0) {
		$('[data-toggle="tooltip"]').tooltip();
	}

	if($('[data-toggle="popover"]').length > 0) {
		$('[data-toggle="popover"]').popover();
	}

	$(window).on('load', function() {
		// for shorter main content
		if($('#main-content').height() < $('#left-sidebar').height()) {
			$('#main-content').css('min-height', $('#left-sidebar').innerHeight() - $('footer').innerHeight());
		}
	});

	$(window).on('load resize', function() {
		if($(window).innerWidth() < 420) {
			$('.navbar-brand logo.svg').attr('src', '../assets/images/icon.svg');
		} else {
			$('.navbar-brand icon-light.svg').attr('src', '../assets/images/logo.svg');
		}
	});
    
    // Full screen class 
	$('.full-screen').on('click', function() {
		$(this).parents('.card').toggleClass('fullscreen');
	});

	// progress bars
    $('.progress .progress-bar').progressbar({
		display_text: 'none'
	});

	// Theme Setting 
	$('.themesetting .theme_btn').on('click', function() {
		$('.themesetting').toggleClass('open');
	});

	// header dropdown add new class
	$('.header-dropdown .dropdown-toggle').on('click', function() {
		$('.header-dropdown li .dropdown-menu').toggleClass('vivify fadeIn');
	});

	// Select all checkbox
	$('.check-all').on('click',function(){
	
		if(this.checked){
			$(this).parents('.check-all-parent').find('.checkbox-tick').each(function(){
			this.checked = true;
			});
		}else{
			$(this).parents('.check-all-parent').find('.checkbox-tick').each(function(){
			this.checked = false;
			});
		}
	});

	$('.checkbox-tick').on('click',function(){   
		if($(this).parents('.check-all-parent').find('.checkbox-tick:checked').length == $(this).parents('.check-all-parent').find('.checkbox-tick').length){
			$(this).parents('.check-all-parent').find('.check-all').prop('checked',true);
		}else{
			$(this).parents('.check-all-parent').find('.check-all').prop('checked',false);
		}
	});

	// inbox star
	$('a.mail-star').on('click', function () {
		$(this).toggleClass('active')
	});

	// todo list js
	$('.todo_list .todo-delete').on('click', function() {
		$(this).parents("li:first").toggleClass('delete');
	});
});

// toggle function
$.fn.clickToggle = function( f1, f2 ) {
	return this.each( function() {
		var clicked = false;
		$(this).bind('click', function() {
			if(clicked) {
				clicked = false;
				return f2.apply(this, arguments);
			}

			clicked = true;
			return f1.apply(this, arguments);
		});
	});
};

// When the user scrolls the page, execute myFunction 
window.onscroll = function() {myFunction()};
    
function myFunction() {
  var winScroll = document.body.scrollTop || document.documentElement.scrollTop;
  var height = document.documentElement.scrollHeight - document.documentElement.clientHeight;
  var scrolled = (winScroll / height) * 100;
  document.getElementById("myBar").style.width = scrolled + "%";
}