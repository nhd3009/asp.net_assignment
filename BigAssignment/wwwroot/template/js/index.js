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

[class*= "nicon-"]{ width: 17px; height: 20px; display: inline - block; opacity: .4; margin - bottom: -3px; }
.nicon - 1{
    background: url('../images/iconcatv2.png') 0 - 1px;
}
.nicon - 2{
    background: url('../images/iconcatv2.png') - 20px - 8px;
}
.nicon - 3{
    background: url('../images/iconcatv2.png') - 40px - 8px;
}
.nicon - 4{
    background: url('../images/iconcatv2.png') - 85px - 8px;
}
.nicon - 5{
    background: url('../images/iconcatv2.png') - 105px - 8px;
}
.nicon - 6{
    background: url('../images/iconcatv2.png') - 125px - 8px;
}
.nicon - 7{
    background: url('../images/iconcatv2.png') - 146px - 8px;
}
.nicon - 8{
    background: url('../images/iconcatv2.png') - 170px - 8px;
}
.nicon - 9{
    background: url('../images/iconcatv2.png') - 193px - 8px;
}
.nicon - 10{
    background: url('../images/iconcatv2.png') - 215px - 8px;
}
.nicon - 11{
    background: url('../images/iconcatv2.png') - 234px - 8px;
}
.nicon - 12{
    background: url('../images/iconcatv2.png') - 250px - 8px;
}
.check - cat {
    width: 17px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 209px 0;
}
.ucheck - cat {
    width: 17px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 229px 0;
}
.ucheck - cat {
    width: 17px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 229px 0;
}
.list - icon {
    width: 25px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 119px 0px;
}
.grid - icon {
    width: 25px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 90px 0px;
}
.list - icon - active {
    width: 25px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 119px - 28px;
}

.grid - icon - active {
    width: 25px;
    height: 20px;
    display: inline - block;
    margin - bottom: -4px;
    background: url('../images/iconIdN.png') 90px - 28px;
}

$(document).ready(function () {
    $('#thanh-toan').click(function (e) {
        e.preventDefault();
        if ($('#email').val() == null || $('#email').val() == '') {
            Swal.fire('Vui lòng nhập email!');
            return false;
        } else if (($('#address').val().trim() == null || $('#address').val().trim() == '')) {
            Swal.fire('Vui lòng nhập địa chỉ nhân hàng!');
            return false;
        }
        $.ajax({
            url: "/cart/checkauth",
            type: 'POST',
            success: function (f) {
                if (f.f == 0) {
                    WIYN();
                } else {
                    $.ajax({
                        url: "/cart/payment",
                        type: 'POST',
                        data: {
                            Email: $('input[name=Email]').val(),
                            Phone: $('input[name=Phone]').val(),
                            Address: $('input[name=Address]').val(),
                            FullName: $('input[name=FullName]').val(),
                        },
                        dataType: 'json',
                        success: function (pig) {
                            if (!pig) {
                                console.log(pig);
                            } else {
                                window.location.href = "account/order";
                            }
                        }
                    })
                }
            }
        })
    })
});
