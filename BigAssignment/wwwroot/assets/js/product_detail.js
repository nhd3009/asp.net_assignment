//product image slider
$(document).ready(function() {
    $('.product-detail-img__slider').owlCarousel({
        loop: true,
        margin: 16,
        dots: false,
        items:5
    });

    $(".product-detail-img-slide__item").click(function() {
        let pathImg = $(this).children().attr("src");
        $('.product-detail-img__represent').attr("src", pathImg);
    });
});

// Tab
$(".tab-navbar__item").eq(0).click(function() {
    $(".tab-navbar__item").eq(1).removeClass("active");
    $(".tab-content-item").eq(1).removeClass("active");
    $(this).addClass("active");
    $(".tab-content-item").eq(0).addClass("active");
});
$(".tab-navbar__item").eq(1).click(function() {
    $(".tab-navbar__item").eq(0).removeClass("active");
    $(".tab-content-item").eq(0).removeClass("active");
    $(this).addClass("active");
    $(".tab-content-item").eq(1).addClass("active");
});

// Slider
$('.product-slider').owlCarousel({
    loop: true,
    margin: 30,
    dots: false,
    responsive:{
        0:{
            items: 1
        },
        576:{
            items: 2
        },
        768:{
            items:3
        },
        992:{
            items:4
        }
    }
});