var common = {
    init: function () {
        common.registerEvents();
    },
    registerEvents: function () {

        $('#customer_login').validate({
            rules: {
                username: "required",
                password: "required"
            },
            messages: {
                username: "Nhập tài khoản",
                password: "Nhập mật khẩu"
            }
        });


        $('#login-submit').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#customer_login').valid();
            if (isValid) {
                common.createLogin();
            }
        });

        $('#create_customer').validate({
            rules: {
                name: "required",
                address: "required",
                phone: "required",
                email: {
                    required: true,
                    email: true
                },
                userName: "required",
                password: {
                    required: true,
                    minlength: 6
                },
                repassword: {
                    equalTo: "#register-form-password"
                }
            },
            messages: {
                name: "Nhập họ tên",
                address: "Nhập địa chỉ",
                phone: "Nhập số điện thoại",
                userName: "Nhập tài khoản",
                password: {
                    required: "Nhập mật khẩu",
                    minlength: "Mật khẩu ít nhất 6 ký tự"
                },
                email: {
                    required: "Bạn cần nhập email",
                    email: "Định dạng email chưa đúng"
                },
                repassword: "Mật khẩu nhập lại không đúng"
            }
        });

        $('#register-submit').off('click').on('click', function (e) {
            e.preventDefault();
            var isValid = $('#create_customer').valid();
            if (isValid) {
                common.register();
            }
        });

    },
    createLogin: function () {
        var userName = $('#login-form-username').val();
        var passWord = $('#login-form-password').val();
        var urlredirect = $('#urlredirect').val();
        $.ajax({
            url: '/Account/Login',
            data: {
                userName: userName,
                passWord: passWord
            },
            type: 'POST',
            dataType: 'json',
            success: function (response) {
                if (response.status) {
                    window.location.href = urlredirect;
                }
                else {
                    setTimeout(function () {
                        $('.errors').show();
                        $('#login-form-username').val(''),
                        $('#login-form-password').val('')
                    });
                }
            }
        });
    },
    register: function () {
        var register = {
            UserName: $('#register-form-userName').val(),
            Password: $('#register-form-password').val(),
            Name: $('#register-form-name').val(),
            Address: $('#register-form-address').val(),
            Email: $('#register-form-email').val(),
            Phone: $('#register-form-phone').val()
        };
        $.ajax({
            url: '/Account/Register',
            type: 'POST',
            dataType: 'json',
            data: {
                accountViewModel: JSON.stringify(register)
            },
            success: function (response) {

                if (response.status) {
                    alert('Đăng ký thành công.');
                    window.location.href = "/dang-nhap.html";
                }
                else {
                    setTimeout(function () {
                        $('#existsResult').text(response.message);
                        $('.errors').show();
                    });
                }
            }
        });
    }
}
common.init();