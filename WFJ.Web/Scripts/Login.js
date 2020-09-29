
$("#backToLogin").click(function () {
    $("#login-text").val("");
    $(".login-form").removeClass("d-none");
    $(".forget-password").addClass("d-none");
});
$("#goToforgotPassword").click(function () {
    $("#login-text").val("");
    $(".login-form").addClass("d-none");
    $(".forget-password").removeClass("d-none");
});
