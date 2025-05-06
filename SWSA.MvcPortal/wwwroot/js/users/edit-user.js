$(function () {
    initSelect2();
    const $userForm = $("#userForm");
    const userFormInputs = {
        fullName: $userForm.find('input[name="fullName"]'),
        phoneNumber: $userForm.find('input[name="phoneNumber"]'),
        email: $userForm.find('input[name="email"]'),
        password: $userForm.find('input[name="password"]'),
        rePassword: $userForm.find('input[name="rePassword"]'),
        isActive: $userForm.find('select[name="isActive"]'),
        role: $userForm.find('select[name="role"]')
    };

    userFormInputs.rePassword.addClass('prevent-typing').val('');

    userFormInputs.password.on('input', function () {
        const hasPassword = $(this).val().length > 0;

        if (hasPassword) {
            userFormInputs.rePassword.removeClass('prevent-typing');
        } else {
            userFormInputs.rePassword.addClass('prevent-typing').val('');
        }
    });

    $userForm.validate({
        rules: {
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
                required: false 
            },
            rePassword: {
                required: {
                    depends: function () {
                        return userFormInputs.password.val().length > 0;
                    }
                },
                equalTo: {
                    depends: function () {
                        return userFormInputs.password.val().length > 0;
                    },
                    param: 'input[name="password"]'
                }
            },
            isActive: {
                required: true
            },
            role: {
                required: true
            }
        },
        messages: {
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
        userData.staffId = $("#staffId").val();
        $.ajax({
            url: `${urls.users}/edit`, 
            method: "POST",
            data: userData,
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Updated", "User updated successfully.");
                    setTimeout(() => {
                      //  window.location.href = `${urls.users}/${userData.staffId}/overview`;
                    }, 200);
                }
            },
            error: function (jqxhr) {
            }
        });
    });
});
