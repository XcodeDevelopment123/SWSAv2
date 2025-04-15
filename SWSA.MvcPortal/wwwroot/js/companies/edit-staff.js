$(function () {
    //#region Staff Contact Form 
    const $staffForm = $("#staffContactForm");
    const staffFormInputs = {
        staffId: $staffForm.find('input[name="staffId"]'),
        contactName: $staffForm.find('input[name="staffName"]'),
        whatsApp: $staffForm.find('input[name="staffWhatsapp"]'),
        email: $staffForm.find('input[name="staffEmail"]'),
        remark: $staffForm.find('input[name="staffRemark"]'),
        position: $staffForm.find('select[name="staffPosition"]')
    };

    $staffForm.validate({
        rules: {
            staffName: {
                required: true
            },
            staffWhatsapp: {
                required: true
            },
            staffEmail: {
                required: true,
                email: true
            },
            staffRemark: {
                required: false
            },
            staffPosition: {
                required: true
            }
        },
        messages: {
            staffName: {
                required: "Contact Name is required."
            },
            staffWhatsapp: {
                required: "WhatsApp number is required."
            },
            staffEmail: {
                required: "Email is required.",
                email: "Please enter a valid email address."
            },
            staffPosition: {
                required: "Position is required."
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

    $staffForm.on('submit', function (e) {
        e.preventDefault();

        if (!$staffForm.valid()) {
            return;
        }

        const staffData = getFormData(staffFormInputs);
        staffData.companyId = $("#companyId").val(); 
        $.ajax({
            url: `${urls.company_staffs}/edit`,
            method: "POST",
            data: { req: staffData },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Updated", "Staff info updated successfully.");
                }
            },
            error: function (jqxhr) {
                // optional error handling
            }
        });
    });

    //#endregion


    //#region Staff Login Profile Form
    const $staffLoginForm = $("#staffLoginProfile");
    const hasPassword = $("#hasPassword").val() === "true";

    const staffLoginFormInputs = {
        enableLogin: $staffLoginForm.find('select[name="enableLogin"]'),
        username: $staffLoginForm.find('input[name="username"]'),
        password: $staffLoginForm.find('input[name="password"]'),
        rePassword: $staffLoginForm.find('input[name="rePassword"]'),
    };


    staffLoginFormInputs.enableLogin.on('change', function () {

        const isEnable = $(this).val() === "true";
        if (isEnable) {
            staffLoginFormInputs.username.prop("disabled", false);
            staffLoginFormInputs.password.prop("disabled", false);
            staffLoginFormInputs.rePassword.prop("disabled", false);
            staffLoginFormInputs.rePassword.addClass('prevent-typing');
        } else {
            staffLoginFormInputs.username.prop("disabled", true);
            staffLoginFormInputs.password.prop("disabled", true).val('');
            staffLoginFormInputs.rePassword.prop("disabled", true).val('');
            staffLoginFormInputs.rePassword.addClass('prevent-typing');
        }
    })

    staffLoginFormInputs.rePassword.addClass('prevent-typing').val('');

    staffLoginFormInputs.password.on('input', function () {
        const hasPassword = $(this).val().length > 0;

        if (hasPassword) {
            staffLoginFormInputs.rePassword.removeClass('prevent-typing');
        } else {
            staffLoginFormInputs.rePassword.addClass('prevent-typing').val('');
        }
    });

    $staffLoginForm.validate({
        rules: {
            enableLogin: {
                required: true
            },
            username: {
                required: {
                    depends: function () {
                        return staffLoginFormInputs.enableLogin.val() === "true" && !hasPassword;
                    }
                }
            },
            password: {
                required: {
                    depends: function () {
                        return staffLoginFormInputs.enableLogin.val() === "true" && !hasPassword;
                    }
                }
            },
            rePassword: {
                required: {
                    depends: function () {
                        if (hasPassword && staffLoginFormInputs.password.val() === "••••••") {
                            return false;
                        }

                        return staffLoginFormInputs.password.val().length > 0;
                    }
                },
                equalTo: {
                    depends: function () {
                        if (hasPassword && staffLoginFormInputs.password.val() === "••••••") {
                            return false;
                        }

                        return staffLoginFormInputs.password.val().length > 0 && !hasPassword;
                    },
                    param: 'input[name="password"]'
                }
            }
        },
        messages: {
            enableLogin: {
                required: "Please select login option."
            },
            username: {
                required: "Username is required."
            },
            password: {
                required: "Password is required."
            },
            rePassword: {
                required: "Please re-enter your password.",
                equalTo: "Passwords do not match."
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

    $staffLoginForm.on('submit', function (e) {
        e.preventDefault();

        if (!$staffLoginForm.valid()) {
            return;
        }

        const loginData = getFormData(staffLoginFormInputs);
        loginData.staffId = $("#staffId").val();
        $.ajax({
            url: `${urls.company_staffs}/edit-login-profile`,
            method: "POST",
            data: { req: loginData },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Updated", "Login profile updated successfully.");
                }
            },
            error: function (jqxhr) {
                // optional error handling
            }
        });
    });
    //#endregion

})