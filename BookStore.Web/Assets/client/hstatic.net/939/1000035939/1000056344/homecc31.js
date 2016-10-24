 // hover main menu
 $(function(){
    $(".dropdown").hover(
            function() {
                $('.dropdown-menu', this).stop( true, true ).fadeIn("fast");
                $(this).toggleClass('open');
                $('b', this).toggleClass("caret caret-up");
            },
            function() {
                $('.dropdown-menu', this).stop( true, true ).fadeOut("fast");
                $(this).toggleClass('open');
                $('b', this).toggleClass("caret caret-up");
            });
    });
$(function() {
			var $elem = $('#wrapper');

			$('#nav_up').fadeIn('slow');
			$('#nav_down').fadeIn('slow');  

			$(window).bind('scrollstart', function(){
				$('#nav_up,#nav_down').stop().animate({'opacity':'0.2'});
			});
			$(window).bind('scrollstop', function(){
				$('#nav_up,#nav_down').stop().animate({'opacity':'1'});
			});

			$('#nav_down').click(
				function (e) {
					$('html, body').animate({scrollTop: $elem.height()}, 800);
				}
			);
			$('#nav_up').click(
				function (e) {
					$('html, body').animate({scrollTop: '0px'}, 800);
					
				}
				
			);
		});

$(document).ready(function() {

					$("#owl-demo").owlCarousel({

						navigation : false, // Show next and prev buttons
						slideSpeed : 300,
						paginationSpeed : 400,
						singleItem:true,
						pagination : true,
						paginationNumbers: false,
						autoPlay : true,
						navigationText : false
						// "singleItem:true" is a shortcut for:
						// items : 1, 
						// itemsDesktop : false,
						// itemsDesktopSmall : false,
						// itemsTablet: false,
						// itemsMobile : false

					});

				});