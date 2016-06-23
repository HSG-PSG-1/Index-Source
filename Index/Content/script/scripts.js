var winWidthForMasonry = 1768;
jQuery(window).load(function () {
    if ($(window).width() >= winWidthForMasonry) {
		$('#category-lists').css('visibility','visible');
	}
});






				
// Document Ready
$(function() {
  
  // Cache selectors
  var $windowWidth = $(window).innerWidth(),
  		$header = $('#page-header'),
  		$content = $('#content'),
  		$main = $('#main'),
  		$catList = $('#category-lists', $main),
  		$footer = $('#page-footer');
  

	/* header */
	$('#total-number', $header).text($('.category-content li').length);


	/* main */
	 $('.category-content', $catList).each(function() {
    $(this).find('.hidden-link').css({ display: 'block' });
		$.data(this, 'moreHeight', $(this).height());

		if(!$('body').hasClass('logged-in')) {
			$(this).find('.hidden-link:not(".paid-link")').css({ display: 'none' });
		}
		
	});

	$catList.find('.toggle-more').toggle(
		function() {
	    var $catContent = $(this).parents('.category-block').find('.category-content');
	    
	    $catContent.find('.hidden-link').show();
	    
	    if ($windowWidth >= winWidthForMasonry) {
        $catList.masonry('reload')
	    } else {
	      $catContent.velocity({height: $catContent.data('moreHeight')});
	    }
			
			$(this).find('.more').hide().delay(500).siblings().fadeIn();
		},
		function() {
	    var $catContent = $(this).parents('.category-block').find('.category-content');
	    
	    if ($windowWidth >= winWidthForMasonry) {
        $catContent.find('.hidden-link:not(".paid-link")').hide();
        $catList.masonry('reload');
	    } else {
  		  $catContent.velocity({height: $catContent.data('openedHeight')});
	    }

	    $(this).find('.less').hide().delay(500).siblings().fadeIn();
		}
	);
	

   
	mobileOnly();
	
	tabletUp();

	

	/* footer */
	if ( $.browser != $.browser.msie && parseInt($.browser.version, 10) > 9 ) {
    $('#index-categories').dropkick();
	}
	$('a:not(.btn-grey)', $catList).click(function() {
	  $(this).attr('target', '_blank');
	}); 
	
	
	
	
	
	
	/* any device up to a tablet's portrait view */
  function mobileOnly() {
      if ($windowWidth <= winWidthForMasonry-1) {
    	
  
  		$('.category-content', $catList).each(function() {
    		$.data(this, 'openedHeight', $(this).height());
    	}).css({ display: 'block', overflow: 'hidden', height: '0' });
  
  
  		$('.submit-company .submit-btn', $header).colorbox({inline:true, width:"300px", close: ''});
  
  
  		$('.category-header', $main).toggle(
  			function() {
          var $categoryList = $(this).siblings('.category-content');
  
          $(this).siblings('.category-footer').addClass('active');
  			
  				if ($(this).siblings('.category-footer').find('.more').is(':visible')) {
  	    		$categoryList.velocity({height: $categoryList.data('openedHeight')});
  				} else {
  	    		$categoryList.velocity({height: $categoryList.data('moreHeight')});
  				}
  			},
  			function() {    
  				$(this).siblings('.category-footer').removeClass('active');
  
    			$(this).siblings('.category-content').velocity({height: 0});
  			}
  		);
  
  	};
  }
  
  
  /* any device larger then a tablet's portrait view */
  function tabletUp() {
      if ($windowWidth >= winWidthForMasonry) {
  
  		$('.submit-company .submit-btn', $header).colorbox({inline:true, width:"420px", close: ''});
    	
  
      $catList.masonry({
        itemSelector: '.category-block',
  			isResizable: true
      });
  
      $catList.disableSelection();
  
  	}
  }
	


}); // end Document Ready


$(window).on("debouncedresize", function( event ) {
	
	$windowWidth =	$(window).innerWidth();
  
	if ($windowWidth >= winWidthForMasonry) {
    $catList.masonry('option', { 
	    columnWidth: function( containerWidth ) {
	    	if ($windowWidth < 1024) {
	    		//log('hello')
	    		return containerWidth / 3;
	    	} else {
	    		//log('goobye')
	    		return containerWidth / 4;
	    	}
	  	} 
		});

		//$catList.masonry('reload');
	};
});














