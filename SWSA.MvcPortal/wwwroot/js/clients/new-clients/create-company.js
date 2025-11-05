$(function () {
    //#region Admin Form
    const $adminForm = $("#adminForm");
    const adminFormInputs = {
        group: $adminForm.find('input[name="group"]'),
        referral: $adminForm.find('input[name="referral"]'),
        fileNo: $adminForm.find('input[name="fileNo"]'),
    };
    //#endregion

    //#region Company Form 
    const $companyForm = $("#companyForm");
    const companyFormInputs = {
        companyName: $companyForm.find('input[name="companyName"]'),
        registrationNumber: $companyForm.find('input[name="regisNumber"]'),
        employerNumber: $companyForm.find('input[name="eNumber"]'),
        taxIdentificationNumber: $companyForm.find('input[name="tinNumber"]'),
        incorporationDate: $companyForm.find('input[name="incorpDate"]'),
        companyType: $companyForm.find('select[name="companyType"]'),
        clientType: $companyForm.find('select[name="clientType"]'),
        yearEndMonth: $companyForm.find('input[name="yearEndMonth"]'),
        activitySize: $companyForm.find('select[name="activitySizes"]'),
        msicCodeIds: $companyForm.find('select[name="msicCodesIds"]'),
    }

    $companyForm.validate({
        rules: {
            companyName: {
                required: true
            },
            regisNumber: {
                required: true
            },
            eNumber: {
                required: true
            },
            tinNumber: {
                required: true
            },
            incorpDate: {
                required: true
            },
            yearEndMonth: {
                required: true
            },
            msicCodesIds: {
                required: true,
                maxlength: 3
            },
            companyType: {
                required: true
            },
            activitySizes: {
                required: true
            }
        },
        messages: {
            companyName: {
                required: "Company Name is required."
            },
            regisNumber: {
                required: "Registration Number is required."
            },
            eNumber: {
                required: "Employer Number is required."
            },
            tinNumber: {
                required: "Tax Identification Number is required."
            },
            incorpDate: {
                required: "Incorporation Date is required."
            },
            yearEndMonth: {
                required: "Year End Month is required."
            },
            companyType: {
                required: "Company Type is required."
            },
            msicCodesIds: {
                required: "Please select at least one MSIC code.",
                maxlength: "You can select up to 3 MSIC codes only."
            },
            activitySizes: {
                required: "Please select activity size.",
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    //#endregion
    initSelect2();

    flatpickr("#incorpDate", {
        allowInput: true
    });

    flatpickr("#yearEndMonth", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m",
                altFormat: "d-m (F)",
            })
        ],
        altInput: true,
        onChange: function (selectedDates, dateStr, instance) {
            if (!selectedDates.length) return;

            const selectedDate = selectedDates[0];

            var lastDay = getLastDay(selectedDate);

            if (selectedDate.getTime() === lastDay.getTime()) return;

            instance.setDate(lastDay, true);
        },
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
        },
    });

    $("#btnSubmitRequest").on("click", function () {
        if (!$companyForm.valid()) {
            return;
        }

        var adminInfo = getFormData(adminFormInputs);
        const companyData = getFormData(companyFormInputs);
        companyData.yearEndMonth = extractNumbers(companyData.yearEndMonth);
        companyData.categoryInfo = adminInfo;
        alert(`${urls.new_client}/company`);

        $.ajax({
            url: `${urls.new_client}/company`,
            method: "POST",
            data: {
                req: companyData
            },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Created", "Company Client created successfully.");
                    window.location.href = `${urls.client}/${toCleanLower(res.clientType)}?id=${res.id}`;
                }
            },
            error: (res) => {
            }
        })

    })
})