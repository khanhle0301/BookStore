$(document).ready(function(){
	$("#tabanh1").click(function(){
		$("#infoxproduct").slideToggle();
	})
	$("#ptab2").click(function(){
		$("#infoxproduct1").slideToggle();
	})
	$("#tabanh3").click(function(){
		$("#infokhac").slideToggle();
	})
	
	
	$("#cloai").click(function(){
		$("#coll-loaisp").slideToggle();
	})
	$("#cnsx").click(function(){
		$("#menusx").slideToggle();
	})
	$("#cgia").click(function(){
		$("#menutheogia").slideToggle();
	})
	$("#ckt").click(function(){
		$("#kichthuoc").slideToggle();
	})
	
	
	$(".cong").click(function(){
		
var tong=parseInt($("#quantity").val())+1;
		$("#quantity").val(tong);

	
	
			


			
	})

	
	
	
	
	
	$(".image-lsp").show(500);
	
	$("#content-slider").lightSlider({
        loop:true,
        keyPress:true
    });
    $('#image-gallery').lightSlider({
        gallery:true,
        item:1,
        thumbItem:9,
        slideMargin: 0,
				
        speed:1000,
        auto:false,
        loop:true,
        onSliderLoad: function() {
            $('#image-gallery').removeClass('cS-hidden');
        }  
    });
























$(".tru").click(function(){
			
			var tong=parseInt($("#quantity").val())-1;
				if(tong<1){
				tong=1;
			
			}
			$("#quantity").val(tong);
		});


});