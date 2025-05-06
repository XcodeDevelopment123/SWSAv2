$(function () {
    initSelect2();

    const $userForm = $("#userForm");
    const userFormInputs = {
        username: $userForm.find('input[name="username"]'),
        fullName: $userForm.find('input[name="fullName"]'),
        phoneNumber: $userForm.find('input[name="phoneNumber"]'),
        email: $userForm.find('input[name="email"]'),
        password: $userForm.find('input[name="password"]'),
        rePassword: $userForm.find('input[name="rePassword"]'),
        isActive: $userForm.find('select[name="isActive"]'),
        role: $userForm.find('select[name="role"]')
    };

    $userForm.validate({
        rules: {
            username: {
                required: true
            },
            fullName: {
                required: true
            },
            phoneNumber: {
                required: true
            },
            email: {
                required: true,
                email: true
            },
            password: {
                required: true
            },
            rePassword: {
                required: true,
                equalTo: 'input[name="password"]'
            },
            isActive: {
                required: true
            },
            role: {
                required: true
            }
        },
        messages: {
            username: {
                required: "Username is required."
            },
            fullName: {
                required: "Full name is required."
            },
            phoneNumber: {
                required: "Phone number is required."
            },
            email: {
                required: "Email is required.",
                email: "Please enter a valid email address."
            },
            password: {
                required: "Password is required."
            },
            rePassword: {
                required: "Please re-enter your password.",
                equalTo: "Passwords do not match."
            },
            isActive: {
                required: "Please select an active status."
            },
            role: {
                required: "Please select an role."
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            if (element.hasClass('select2-hidden-accessible')) {
                element.next('.select2-container').after(error);
            } else {
                element.closest('.form-group').append(error);
            }
        },
        highlight: function (element) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).removeClass('is-invalid');
        }
    });

    $userForm.on('submit', function (e) {
        e.preventDefault();

        if (!$userForm.valid()) {
            return;
        }

        const userData = getFormData(userFormInputs);

        $.ajax({
            url: `${urls.users}/create`,
            method: "POST",
            data: userData,
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Created", "User created successfully.");
                    setTimeout(() => {
                        window.location.href = `${urls.users}/${res}/overview`;
                    },200)
           
                }
            },
            error: (jqxhr) => {
                jqxhr.handledError = true; // mark for manual handling

                stopLoading();
                const responseJson = jqxhr.responseJSON;
                Toast_Fire(ICON_ERROR, responseJson.message, responseJson.error);
            }
        });
    });

})