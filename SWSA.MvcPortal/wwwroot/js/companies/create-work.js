$(function () {
    const $taskForm = $("#taskForm");

    const taskFormInputs = {
        workType: $taskForm.find('select[name="workType"]'),
        serviceScope: $taskForm.find('select[name="serviceScope"]'),
        companyActivityLevel: $taskForm.find('select[name="companyActivityLevel"]'),
        yearToDo: $taskForm.find('select[name="yearToDo"]'),
        monthToDo: $taskForm.find('input[name="monthToDo"]'),
        assignedStaffId: $taskForm.find('select[name="handledByStaffId"]'),
        internalNote: $taskForm.find('input[name="internalNote"]'),    
        companyId:$("#companyId")
    };


    $taskForm.validate({
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
            yearToDo: {
                required: true
            },
            monthToDo: {
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
            yearToDo: {
                required: "Please select a year."
            },
            monthToDo: {
                required: "Please select a month."
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

    $taskForm.on("submit", function (e) {
        e.preventDefault();
        if (!$taskForm.valid()) return;

        const taskData = getFormData(taskFormInputs);
        taskData.monthToDo = extractNumbers(taskData.monthToDo);

        $.ajax({
            url: `${urls.company_works}/create`,
            method: "POST",
            data: taskData,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Created", "Work created successfully.");
                setTimeout(() => {
                    window.location.href = `${urls.company_works}`;
                }, 200);
            },
            error: function (jqxhr) {
            }
        });
    });


    initSelect2();

    flatpickr("#id", {
        allowInput: true
    });

    flatpickr("#monthToDo", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m (F)",
                altFormat: "m (F)",
            })
        ],
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
        }
    });
})