// Banner Slide
setInterval(function() {
    let currentIndexSlide = $(".carousel-indicators a.active").index();
    currentIndexSlide = (currentIndexSlide + 1) % 3;
    $(".carousel-indicators").children().removeClass("active");
    $(".carousel-indicators a").eq(currentIndexSlide).addClass("active");
    $(".banner-left").children().removeClass("active");
    $(".banner-left-item").eq(currentIndexSlide).addClass("active");
}, 10000);

// Trademark slide
$('.owl-carousel').owlCarousel({
    loop:true,
    margin:30,
    dots: false,
    responsive:{
        0:{
            items: 2
        },
        576:{
            items: 3
        },
        768:{
            items:4
        },
        992:{
            items:5
        },
        1200:{
            items:6
        }
    }
})