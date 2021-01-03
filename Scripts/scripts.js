/*!
    * Start Bootstrap - Creative v6.0.1 (https://startbootstrap.com/themes/creative)
    * Copyright 2013-2020 Start Bootstrap
    * Licensed under MIT (https://github.com/BlackrockDigital/startbootstrap-creative/blob/master/LICENSE)
    */
    (function($) {
  "use strict"; // Start of use strict

  // Smooth scrolling using jQuery easing
  $('a.js-scroll-trigger[href*="#"]:not([href="#"])').click(function() {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
      if (target.length) {
        $('html, body').animate({
          scrollTop: (target.offset().top - 110)
        }, 1000, "easeInOutExpo");
        return false;
      }
    }
  });

  // Closes responsive menu when a scroll trigger link is clicked
  $('.js-scroll-trigger').click(function() {
    $('.navbar-collapse').collapse('hide');
  });

  // Activate scrollspy to add active class to navbar items on scroll
  $('body').scrollspy({
    target: '#mainNav',
    offset: 200
  });

  // Collapse Navbar
  var navbarCollapse = function() {
    if ($("#mainNav").offset().top > 100) {
      $("#mainNav").addClass("navbar-scrolled");
    } else {
      $("#mainNav").removeClass("navbar-scrolled");
    }
  };
  // Collapse now if page is not at top
  navbarCollapse();
  // Collapse the navbar when page is scrolled
  $(window).scroll(navbarCollapse);

  // Magnific popup calls
  $('#portfolio').magnificPopup({
    delegate: 'a',
    type: 'image',
    tLoading: 'Loading image #%curr%...',
    mainClass: 'mfp-img-mobile',
    gallery: {
      enabled: true,
      navigateByImgClick: true,
      preload: [0, 1]
    },
    image: {
      tError: '<a href="%url%">The image #%curr%</a> could not be loaded.'
    }
  });

$("#form").validate({
        submitHandler: function (form) {
        if (grecaptcha.getResponse().length > 0) {
            $.ajax({
                type: "POST",
                url: '/Home/Index',
                data: $("#form").serialize(),
                beforeSend: function () {
                    $("#form").find(":submit").prop('disabled', true);
                    $("#form").removeData("grecaptcha");
                    $("#form").removeData("getResponse");
                },
                success: function () {
                    $("#form").css("display", "none");
                    $("#form").css("visibility", "hidden");
                    $("#formSubmitted").css("display", "block");
                    $("#formSubmitted").css("visibility", "visible");
                },
                error: function (e) {
                    console.log(e);
                }
            });
        } else {
            jQuery('#lblrecaptcha').html('Captcha verification is required');
            jQuery('#lblrecaptcha').css('color', "red");            
        }
        return false;
    }
});

})(jQuery); // End of use strict


