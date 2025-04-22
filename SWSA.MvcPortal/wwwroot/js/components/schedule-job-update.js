$(function () {
    const $jobTriggerSettingForm = $("#jobTriggerSettingForm");
    const jobTriggerSettingForm = {
        jobKey: $("input[name='jobKey']"),
        scheduleType: $("select[name='scheduleType']"),
        cronExpression: $("input[name='cronExpression']"),
        triggerTime: $("input[name='dateTimePicker']"),
        isEnabled: $("select[name='isEnabled']")
    };
    let clickedButton = null;
    jobTriggerSettingForm.scheduleType.on("change", function () {
        const value = $(this).val();

        switch (value) {
            case "Once":
                $("#dateTimePickerContainer").show();
                $("#timePickerContainer,#datePickerContainer,#weekPickerContainer,#cronInputContainer").hide();
                break;
            case "Daily":
                $("#timePickerContainer").show();
                $("#dateTimePickerContainer,#datePickerContainer,#weekPickerContainer,#cronInputContainer").hide();
                break;
            case "Weekly":
                $("#timePickerContainer,#weekPickerContainer").show();
                $("#dateTimePickerContainer,#datePickerContainer,#cronInputContainer").hide();
                break;
            case "Monthly":
                $("#timePickerContainer,#datePickerContainer").show();
                $("#dateTimePickerContainer,#weekPickerContainer,#cronInputContainer").hide();
                break;
            case "Cron":
                $("#cronInputContainer").show();
                $("#timePickerContainer,#datePickerContainer,#weekPickerContainer,#dateTimePickerContainer").hide();
                break;
            default:
                console.error("Type not found, Type:", value);
                return;
        }
    })

    $jobTriggerSettingForm.validate({
        rules: {
            workType: {
                required: true
            },
            serviceScope: {
                required: true
            },
            companyActivityLevel: {
                required: true
            },
            dueDate: {
                required: true
            },
            handledByStaffId: {
                required: true
            },
        },
        messages: {
            workType: {
                required: "Work type is required."
            },
            serviceScope: {
                required: "Service scope is required."
            },
            companyActivityLevel: {
                required: "Company activity level is required."
            },
            dueDate: {
                required: "Due date is required."
            },
            handledByStaffId: {
                required: "Please select a staff."
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

    $jobTriggerSettingForm.find("button[type=submit]").on("click", function () {
        clickedButton = $(this).val();
    });

    $jobTriggerSettingForm.on("submit", function (e) {
        e.preventDefault();
        if (!$jobTriggerSettingForm.valid()) return;

        const cronFields = {
            dayOfWeek: $('select[name="weekPicker"]').val(),
            dayOfMonth: $('input[name="datePicker"]').val(),
            time: $('input[name="timePicker"]').val()
        }
        const triggerData = getFormData(jobTriggerSettingForm);
        triggerData.cronFields = cronFields;
        console.log(triggerData)

        switch (clickedButton) {
            case "trigger":
                $.ajax({
                    url: `${urls.schedule_job}/${triggerData.jobKey}/execute`,
                    method: "POST",
                    success: function (res) {
                        Toast_Fire(ICON_SUCCESS, "The Job is execute success");
                    }
                })
                return; //End method
            case "schedule":
            case "schedule_trigger":
                triggerData.executeNow = clickedButton === "schedule_trigger";
                break;
        }


        $.ajax({
            url: `${urls.schedule_job}/schedule`,
            method: "POST",
            data: {req:triggerData},
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "The job schedule setting has been save");
            }
        })

    });



    initSelect2()

    flatpickr("#dateTimePicker", {
        allowInput: true,
        enableTime: true,
        time_24hr: true
    });

    flatpickr("#timePicker", {
        enableTime: true,
        noCalendar: true,
        dateFormat: "H:i",
        time_24hr: true
    });

    flatpickr("#datePicker", {
        dateFormat: "H:i",
    });

    flatpickr("#datePicker", {
        enableTime: false,
        dateFormat: "Y-m-d",
        altInput: true,
        altFormat: "d",
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
        },
        onChange: updateOrdinal
    });


    function updateOrdinal(selectedDates, dateStr, instance) {
        if (!selectedDates.length) return;
        const date = selectedDates[0];
        const dayOrdinal = getOrdinal(date.getDate());
        const month = instance.l10n.months.longhand[date.getMonth()];
        const year = date.getFullYear();
        instance.altInput.value = `${dayOrdinal}`;
    }
})