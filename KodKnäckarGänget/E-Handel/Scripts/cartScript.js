$(function () {
    $("#textlink").click(function (evt) {
        evt.preventDefault();
        if ($('#CartPanel').is(":hidden")) {
            $("#CartPanel").slideDown("fast");
        } else {
            $("#CartPanel").slideUp("fast");
        }
    });
});