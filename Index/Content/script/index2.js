$(function() {

  pushBody();


  // headroom
  $("#page-header").headroom({
    offset: 145,
    onTop : function() {
      $('.sticky-parked').removeClass('sticky-parked');
    },
  });
  
  
  // add favicons next to links
  var googleFavIcos = 'http://www.google.com/s2/favicons?domain=';
  $('.category-content li').each(function() {
    var url = $(this).find('a').attr('href');
    $(this).find('h5').before('<img class="fav-img" src="'+ googleFavIcos + url +'">');
  });
  

  //$('.category-list-container').height($(window).outerHeight() - $("#page-header").outerHeight() - $('#page-footer').outerHeight());
  $('.category-list-container').css('top', $("#page-header").outerHeight()).css('bottom', '0');
    
  
  
  $(".nano").nanoScroller({preventPageScrolling: true });
  
 
  
  
  $('.scroll-top').click(function () {
    $("html, body").animate({
        scrollTop: 0
    }, 600);
    return false; 
  });

var topMenuHeight = 50; // HT: Custom

  // sticky header
  function updateTableHeaders() {
     $(".persist-area").each(function() {
     
       var el             = $(this),
           offset         = el.offset(),
           scrollTop = $(window).scrollTop() + topMenuHeight, //125,
           sectionHeader = $(".section-header", this),
           sectionHeaderWrapper = $(".section-header-wrapper", this),
           sectionHeaderWrapperOffset = sectionHeaderWrapper.offset(),
           sectionContent = $(".section-content", this),
           sectionContentOffset = sectionContent.offset();
       
       if ((scrollTop > sectionHeaderWrapperOffset.top)) {
          sectionHeader.addClass("sticky");

          if ((scrollTop < sectionContentOffset.top + sectionContent.outerHeight() - sectionHeader.height())) {
            sectionHeader.removeClass("sticky-parked");
            sectionHeader.addClass("sticky-active");
          } else {
            sectionHeader.removeClass("sticky-active");
            sectionHeader.addClass("sticky-parked");
          }
       } else {
          sectionHeader.removeClass("sticky-active");
          sectionHeader.removeClass("sticky");
       }

     });
  }

  updateTableHeaders();
  $(window).scroll(updateTableHeaders);




  // Scroll spy
  // Cache selectors
  var lastId,
      topMenu = $(".category-list-wrapper > ul"),
      //topMenuHeight = 125,
      // All list items
      menuItems = topMenu.find("a"),
      // Anchors corresponding to menu items
      scrollItems = menuItems.map(function(){
        var item = $($(this).attr("href"));
        if (item.length) { return item; }
      });
  // Bind click handler to menu items
  // so we can get a fancy scroll animation
  menuItems.click(function(e){
    var href = $(this).attr("href"),
        offsetTop = href === "#" ? 0 : $(href).offset().top-topMenuHeight+1;
    $('html, body').stop().animate({ 
        scrollTop: offsetTop
    }, 300);

    //console.log($(href))
    e.preventDefault();
  });
  // Bind to scroll
  $(window).scroll(function(){
     // Get container scroll position
     var fromTop = $(this).scrollTop()+topMenuHeight;
     
     // Get id of current scroll item
     var cur = scrollItems.map(function(){
       if ($(this).offset().top < fromTop)
         return this;
     });
     // Get the id of the current element
     cur = cur[cur.length-1];
     var id = cur && cur.length ? cur[0].id : "";
     
     if (lastId !== id) {
         lastId = id;
         // Set/remove active class
         menuItems
           .parent().removeClass("active")
           .end().filter("[href=#"+id+"]").parent().addClass("active");
     }                   
  });


});
// Fire after 2 seconds of resizing browser window
$(window).on("debouncedresize", function( event ) {
  pushBody();
});

// Reload page after each major breakpoint
enquire.register("screen and (max-width: 767px)", {
  unmatch : function() {
    reloadPage();
  }
});
enquire.register("screen and (min-width: 768px) and (max-width: 991px)", {
  unmatch : function() {
    reloadPage();
  }
});
enquire.register("screen and (min-width: 992px)", {
  unmatch : function() {
    reloadPage();
  }
});
enquire.register("screen and (min-width: 1200px)", {
  unmatch : function() {
    reloadPage();
  }
});
function reloadPage() {
  location.reload(false);
}


// Header magic for fixed headers
function pushBody(includeSubHeader) {
  if($(window).width() >= 768) {
    var topNavHeight = $('.fixed-header').outerHeight();

    if(includeSubHeader === undefined) {
      topNavHeight += $('.sub-header').outerHeight();
    }
    
    $('body').css({paddingTop: topNavHeight});
  }
}



$(window).on("scroll", function() {
	var scrollHeight = $(document).height();
	var scrollPosition = $(window).height() + $(window).scrollTop();
	var footerHeight = parseInt($('#page-footer').height());
	//var scrollIcon = $('.scroll-top').height();
	//var negZone = scrollPosition - (scrollHeight - footerHeight - scrollIcon);
	
	if ((scrollHeight - scrollPosition) / scrollHeight === 0) { // when scroll to bottom of the page
	  $('.page-body').addClass('footer-scroll-to-top'); 
	} else if ((scrollHeight - scrollPosition) <= footerHeight) {
    $('.page-body').removeClass('footer-scroll-to-top');
    //$('.scroll-top').css('margin-bottom', footerHeight - scrollIcon)
	}
});

