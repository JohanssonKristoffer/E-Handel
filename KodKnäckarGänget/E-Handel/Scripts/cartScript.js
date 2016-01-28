$(function () {
    $("#openCart").click(function (evt) {
        evt.preventDefault();
        if ($('#CartPanel').is(":hidden")) {
            $("#CartPanel").slideDown("fast");
        } else {
            $("#CartPanel").slideUp("fast");
        }
    });

    $(function () {
            $("#Link4").click(function(evt) {
                evt.preventDefault();
                if ($('#panelText').is(":hidden")) {
                    $("#panelText").slideDown("fast");
                } else {
                    $("#panelText").slideUp("fast");
                }
            });
        });

    /*
    $(function () {
        $("#categoryDropdownMenu").click(function (evt) {
            evt.preventDefault();
            if ($('#CartPanel').is(":hidden")) {
                $("#CartPanel").slideDown("fast");
            } else {
                $("#CartPanel").slideUp("fast");
            }
        });
        */

$(document).ready(function () {
    // Activate Carousel
    $(".carousel").carousel({ interval: 3000 });

    // Enable Carousel Indicators
    $(".item").click(function () {
        $(".carousel").carousel(0);
    });
    $(".item").click(function () {
        $(".carousel").carousel(1);
    });
    $(".item").click(function () {
        $(".carousel").carousel(2);
    });
    $(".item").click(function () {
        $(".carousel").carousel(3);
    });
    $(".item").click(function () {
        $(".carousel").carousel(4);
    });

    // Enable Carousel Controls
    $(".left").click(function () {
        $(".carousel").carousel("prev");
    });
    $(".right").click(function () {
        $(".carousel").carousel("next");
    });
});
})

$(".dropdown-toggle").dropdown();