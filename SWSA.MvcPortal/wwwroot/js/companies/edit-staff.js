$(function () {
    //#region Staff Contact Form 
    const $staffForm = $("#staffContactForm");
    const staffFormInputs = {
        staffId: $staffForm.find('input[name="staffId"]'),
        contactName: $staffForm.find('input[name="staffName"]'),
        whatsApp: $staffForm.find('input[name="staffWhatsapp"]'),
        email: $staffForm.find('input[name="staffEmail"]'),
        remark: $staffForm.find('input[name="staffRemark"]'),
        position: $staffForm.find('select[name="staffPosition"]'),
        companyDepartmentId: $staffForm.find('select[name="departmentId"]')
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
            url: `${urls.company_cm_contact}/edit`,
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
})