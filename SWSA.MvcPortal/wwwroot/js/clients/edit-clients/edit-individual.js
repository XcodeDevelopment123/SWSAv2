$(function () {
    //#region Admin Form
    const $adminForm = $("#adminForm");
    const adminFormInputs = {
        group: $adminForm.find('input[name="group"]'),
        referral: $adminForm.find('input[name="referral"]')
    };
    //#endregion

    //#region Individual Form 
    const $individualForm = $("#individualForm");
    const individualFormInputs = {
        clientId: $("#clientId"),
        individualName: $individualForm.find('input[name="name"]'),
        taxIdentificationNumber: $individualForm.find('input[name="tinNumber"]'),
        professions: $individualForm.find('input[name="professions"]'),
        icOrPassportNumber: $individualForm.find('input[name="icOrPassport"]'),
        yearEndMonth: $individualForm.find('input[name="yearEndMonth"]'),
        clientType: $individualForm.find('select[name="clientType"]')
    };

    $individualForm.validate({
        rules: {
            name: {
                required: true
            },
            tinNumber: {
                required: true
            },
            professions: {
                required: true
            },
            icOrPassport: {
                required: true
            },
            yearEndMonth: {
                required: true
            },
            clientType: {
                required: true
            }
        },
        messages: {
            name: {
                required: "Individual Name is required."
            },
            tinNumber: {
                required: "Tax Identification Number is required."
            },
            professions: {
                required: "Profession is required."
            },
            icOrPassport: {
                required: "IC or Passport Number is required."
            },
            yearEndMonth: {
                required: "Year End Month is required."
            },
            clientType: {
                required: "Client Type is required."
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

            if (!selectedDates.length) return;
            const selectedDate = selectedDates[0];
            var lastDay = getLastDay(selectedDate);
            if (selectedDate.getTime() === lastDay.getTime()) return;

            instance.setDate(lastDay, true);
        },
    });

    $("#btnSubmitRequest").on("click", function () {
        if (!$individualForm.valid()) {
            return;
        }
        var adminInfo = getFormData(adminFormInputs);
        const data = getFormData(individualFormInputs);
        data.yearEndMonth = extractNumbers(data.yearEndMonth);
        data.categoryInfo = adminInfo;
        $.ajax({
            url: `${urls.edit_client}/individual`,
            method: "POST",
            data: {
                req: data
            },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Created", "Individual Client saved successfully.");
                    window.location.href = `${urls.client}/individual?id=${res.id}`;
                }
            },
            error: (res) => {
            }
        })

    })
})