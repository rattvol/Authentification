//var login = Array();
//$(document).ready(function () {
//    $.ajax({
//        type: 'GET',
//        url: 'api/getLogin/',
//        success: function (data) {
//            logins = data.Array;   
//        }
//    });
//    LoginValidate();
//});
function LoginValidate(obj) {
    var loginMessage = document.querySelector("#loginMessage");
    var validateField = obj.value;
    $.ajax({
        type: 'GET',
        url: '/api/getLogin',
        data: { login : validateField },
        success: function (data) {
            if (data == true) loginMessage.textContent = "Этот логин занят";
            else loginMessage.textContent = "";
        }
    });
}