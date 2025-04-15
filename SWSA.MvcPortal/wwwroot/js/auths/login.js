
$(function () {
    const $form = $("#login-form"),
        $signInBtn = $("#sign-in-button");

    $form.validate({
        rules: {
            username: {
                required: true,
            },
            password: {
                required: true,
            },
        },
        messages: {
            username: {
                required: "Username cannot be empty.",
            },
            password: {
                required: "Password cannot be empty.",
            },
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');

            element.parent().parent().append(error);

        },
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');

        }
    });

    $form.on('submit', function (e) {
        e.preventDefault();

        $('#login-error').remove();
        if (!$(this).valid()) {
            return;
        }

        $.ajax({
            url: `${urls.auth}/login`,
            method: "POST",
            data: {
                username: $form.find('input[name="username"]').val(),
                password: $form.find('input[name="password"]').val()
            },
            success: function (res) {
                console.log("login req:", res);
                if (!res.isSuccess) {
                    $form.append(`<span id="login-error" class="error invalid-feedback" style="display: block;">* Login Failed. ${res.message}</span>`);
                    return;
                }

                window.location.href = "/home";
            },
            error: (res) => {
                Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
            }
        })

    });
})