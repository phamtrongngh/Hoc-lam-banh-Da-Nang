$(document).ready(function () {

    $("#button-login").on("click", function () {
        var user = $("#userlogin").val();
        var password = $("#passlogin").val();
        var check = $("#remmember-password").prop("checked");
        check = check ? "1" : "0";
        $.ajax({
            type: "POST",
            url: "/Home/Login/",
            data: {
                userLogin: user,
                passwordLogin: password,
                remember : check
            },
            dataType: "json"
        }).done(function (result) {
            if (result == "") {
                location.reload();
            }
            else {
                $(".error-login").css("display", "block");
            }
        })
    })
    $("#submit-register").on("click",function () {
		var user = $("#userRegister").val();
		var fullname = $("#fullnameRegister").val();
        var email = $("#email").val();
        var password = $("#passRegister").val();
		var passwordConfirm = $("#passComfirm").val();
		var phone = $("#phoneNumber").val();
        var address = $("#address").val();
        var gender = $("input[name='gender']:checked").val();
        var dob = $("#datepicker").val();
        if (!/^[\w]{6,}$/.test(user)) {
            $(".error-user").css("display", "block");
            return false;
        }
        else {
            $(".error-user").css("display", "none");
        }
        if (fullname.length <= 0) {
            $(".error-fullname").css("display", "block");
            return false;
        }
        else {
            $(".error-fullname").css("display", "none");
        }
        if (!(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/).test(email)) {
            $(".error-email").css("display", "block");
            return false;
        }
        else {
            $(".error-email").css("display", "none");
        }
        if (!(/(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$/).test(password)) {
           
            $(".error-password").css("display", "block");
            return false;
        }
        else {
            $(".error-password").css("display", "none");
        }
        if (password != passwordConfirm) {
            $(".error-repassword").css("display", "block");
            return false;
        }
        else {
            $(".error-repassword").css("display", "none");
        }
        if (!(/^(0|\+84)(\s|\.)?((3[2-9])|(5[689])|(7[06-9])|(8[1-689])|(9[0-46-9]))(\d)(\s|\.)?(\d{3})(\s|\.)?(\d{3})$/).test(phone)) {
            $(".error-phone").css("display", "block"); 
            return false;
        }
        else {
            $(".error-phone").css("display", "none");
        }
        if (dob == "") {
            $(".error-date").css("display", "block");
            return false;
        }
        else {
            $(".error-date").css("display", "none");
        }
        $.ajax({
            type: "POST",
            url: "/Home/Register/",
            data: {
                userregister: user,
                passwordregister: password,
                emailregister: email,
                addressregister: address,
                phoneregister: phone,
                fullnameregister: fullname,
                gender: gender,
                dob:dob
            },
            dataType: "json"
        }).done(function (result) {
            if (result == "Success") {
                location.reload();
            };
            if (result == "existEmail") {
                $(".error-existEmail").css("display", "block");
            } else {
                $(".error-existEmail").css("display", "none");
            }
            if (result == "existUsername") {
                $(".error-existUser").css("display", "block");
            } else {
                $(".error-existUser").css("display", "none");
            }
        })
    })

    $("#button-toggler").click(function () {
        if ($(".pull-right").css("display") == "none") {
            $(".pull-right").css("display", "block");
        }
        else {
            $(".pull-right").css("display", "none");
        }
    })
    //--------------------------------------
    /* Set rates + misc */
    var taxRate = 0.05;
    var shippingRate = 15.00;
    var fadeTime = 300;


    /* Assign actions */
    $('.product-quantity input').change(function () {
        updateQuantity(this);
    });

    $('.product-removal button').click(function () {
        removeItem(this);
    });


    /* Recalculate cart */
    function recalculateCart() {
        var subtotal = 0;

        /* Sum up row totals */
        $('.product').each(function () {
            subtotal += parseFloat($(this).children('.product-line-price').text());
        });

        /* Calculate totals */
        var tax = subtotal * taxRate;
        var shipping = (subtotal > 0 ? shippingRate : 0);
        var total = subtotal + tax + shipping;

        /* Update totals display */
        $('.totals-value').fadeOut(fadeTime, function () {
            $('#cart-subtotal').html(subtotal.toFixed(2));
            $('#cart-tax').html(tax.toFixed(2));
            $('#cart-shipping').html(shipping.toFixed(2));
            $('#cart-total').html(total.toFixed(2));
            if (total == 0) {
                $('.checkout').fadeOut(fadeTime);
            } else {
                $('.checkout').fadeIn(fadeTime);
            }
            $('.totals-value').fadeIn(fadeTime);
        });
    }


    /* Update quantity */
    function updateQuantity(quantityInput) {
        /* Calculate line price */
        var productRow = $(quantityInput).parent().parent();
        var price = parseFloat(productRow.children('.product-price').text());
        var quantity = $(quantityInput).val();
        var linePrice = price * quantity;

        /* Update line price display and recalc cart totals */
        productRow.children('.product-line-price').each(function () {
            $(this).fadeOut(fadeTime, function () {
                $(this).text(linePrice.toFixed(2));
                recalculateCart();
                $(this).fadeIn(fadeTime);
            });
        });
    }


    /* Remove item from cart */
    function removeItem(removeButton) {
        /* Remove row from DOM and recalc cart total */
        var productRow = $(removeButton).parent().parent();
        productRow.slideUp(fadeTime, function () {
            productRow.remove();
            recalculateCart();
        });
    }
    
})
jQuery(document).ready(function ($) {
    if ($(".lentop").length > 0) {
        $(window).scroll(function () {
            var e = $(window).scrollTop();
            if (e > 300) {
                $(".lentop").show()
            } else {
                $(".lentop").hide()
            }
        });
        $(".lentop").click(function () {
            $('body,html').animate({
                scrollTop: 0
            })
        })
    }
});
jQuery(document).ready(function ($) {
    if ($(".lentop1").length > 0) {
        $(window).scroll(function () {
            var e = $(window).scrollTop();
            if (e > 300) {
                $(".lentop1").show()
            } else {
                $(".lentop1").hide()
            }
        });
        $(".lentop1").click(function () {
            $('body,html').animate({
                scrollTop: 0
            })
        })
    }
});