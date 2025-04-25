$(function () {
    const $jobTriggerSettingForm = $("#jobTriggerSettingForm");
    const jobTriggerSettingForm = {
        jobType: $("select[name='jobType']"),
        scheduleType: $("select[name='scheduleType']"),
        cronExpression: $("input[name='cronExpression']"),
        triggerTime: $("input[name='dateTimePicker']"),
        cronFields: {
            dayOfWeek: $('select[name="weekPicker"]'),
            dayOfMonth: $('input[name="datePicker"]'),
            time: $('input[name="timePicker"]')
        }
    };

    $jobTriggerSettingForm.validate({
        rules: {
            jobType: {
                required: true
            },
            scheduleType: {
                required: true
            },
            cronExpression: {
                required: {
                    depends: function () {
                        return $("select[name='scheduleType']").val() === "Cron";
                    }
                }
            },
            dateTimePicker: {
                required: {
                    depends: function () {
                        return $("select[name='scheduleType']").val() === "Once";
                    }
                },
                notEmptyOrNA: {
                    depends: function () {
                        return $("select[name='scheduleType']").val() === "Once";
                    }
                },
            },
            timePicker: {
                required: {
                    depends: function () {
                        const val = $("select[name='scheduleType']").val();
                        return val === "Daily" || val === "Weekly" || val === "Monthly";
                    }
                },
                notEmptyOrNA: {
                    depends: function () {
                        const val = $("select[name='scheduleType']").val();
                        return val === "Daily" || val === "Weekly" || val === "Monthly";
                    }
                }
            },
            weekPicker: {
                required: {
                    depends: function () {
                        return $("select[name='scheduleType']").val() === "Weekly";
                    }
                }
            },
            datePicker: {
                required: {
                    depends: function () {
                        return $("select[name='scheduleType']").val() === "Monthly";
                    }
                },
                notEmptyOrNA: {
                    depends: function () {
                        return $("select[name='scheduleType']").val() === "Monthly";
                    }
                }
            }
        },
        messages: {
            cronExpression: {
                required: "Cron expression is required."
            },
            dateTimePicker: {
                required: "Please select a date and time."
            },
            timePicker: {
                required: "Please select a time."
            },
            weekPicker: {
                required: "Please select a day of the week."
            },
            datePicker: {
                required: "Please select a date."
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

    $jobTriggerSettingForm.on("submit", function (e) {
        e.preventDefault();
        if (!$jobTriggerSettingForm.valid()) return;

        const triggerData = getFormData(jobTriggerSettingForm);
        console.log(triggerData);

        $.ajax({
            url: `${urls.schedule_job}/create`,
            method: "POST",
            data: { req: triggerData },
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "The job schedule setting has been create");
                window.location.href = `/scheduler-jobs/${res}/overview` 

            }
        })

    })


    initSelect2()

    flatpickr("#month", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m (F)",
                altFormat: "m (F)",
            })
        ],
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
        },
    });

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
        enableTime: false,
        dateFormat: "d",
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
            updateOrdinal(instance.selectedDates, instance.input.value, instance);
        },
        onChange: updateOrdinal
    });

    function updateOrdinal(selectedDates, dateStr, instance) {
        if (dateStr === 'N/A' || dateStr === '') {
            instance.input.value = "";
            return;
        }

        if (!selectedDates.length) return;

        const date = selectedDates[0];
        const dayOrdinal = getOrdinal(date.getDate());

        instance.input.value = `${dayOrdinal}`;
    }
});