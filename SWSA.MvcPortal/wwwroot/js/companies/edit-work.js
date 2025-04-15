$(function () {
    //#region Task
    const $taskForm = $("#taskForm");

    const taskFormInputs = {
        taskId: $("#taskId"),
        workType: $taskForm.find('select[name="workType"]'),
        serviceScope: $taskForm.find('select[name="serviceScope"]'),
        companyActivityLevel: $taskForm.find('select[name="companyActivityLevel"]'),
        yearToDo: $taskForm.find('select[name="yearToDo"]'),
        monthToDo: $taskForm.find('input[name="monthToDo"]'),
        assignedStaffId: $taskForm.find('select[name="handledByStaffId"]'),
        internalNote: $taskForm.find('input[name="internalNote"]'),
        isCompleted: $taskForm.find('select[name="isCompleted"]'),
        completedDate: $taskForm.find('input[name="completedDate"]'),
        companyId: $("#companyId")
    };

    taskFormInputs.isCompleted.on("change", function () {
        const value = $(this).val();
        taskFormInputs.completedDate.prop("disabled", value !== "true");
        taskFormInputs.completedDate.removeClass("is-invalid");
        taskFormInputs.completedDate.parent().next().remove();
        taskFormInputs.completedDate.val("").trigger("change");
    })


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
            completedDate: {
                required: {
                    depends: function () {
                        return taskFormInputs.isCompleted.val() === "true";
                    }
                },
            }
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
            },
            completedDate: {
                required: "Completed date is require if is completed",
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
        //Css update

        if (!$taskForm.valid()) return;

        const taskData = getFormData(taskFormInputs);
        taskData.monthToDo = extractNumbers(taskData.monthToDo);
        console.log(taskData)

        $.ajax({
            url: `${urls.company_works}/edit`,
            method: "POST",
            data: taskData,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Saved", "Work save successfully.");
            },
            error: function (jqxhr) {
            }
        });
    });
    //#endregion

    //#region Progress Form
    const $progressForm = $("#progressForm");

    const progressFormInputs = {
        progressId: $("#progressId"),
        startDate: $progressForm.find('input[name="startDate"]'),
        endDate: $progressForm.find('input[name="endDate"]'),
        timeTakenInDays: $progressForm.find('input[name="timeTakenInDays"]'),
        status: $progressForm.find('select[name="status"]'),
        progressNote: $progressForm.find('input[name="progressNote"]')
    };

    progressFormInputs.startDate.add(progressFormInputs.endDate).on("change", function () {
        const start = new Date(progressFormInputs.startDate.val());
        const end = new Date(progressFormInputs.endDate.val());

        if (!isNaN(start) && !isNaN(end) && end >= start) {
            const diffTime = end - start;
            const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1;
            progressFormInputs.timeTakenInDays.val(diffDays);
        } else {
            progressFormInputs.timeTakenInDays.val('');
        }
    });

    $progressForm.validate({
        rules: {
            startDate: {
                required: true,
                date: true
            },
            endDate: {
                required: true,
                date: true
            },
            status: {
                required: true
            }
        },
        messages: {
            startDate: {
                required: "Start date is required."
            },
            endDate: {
                required: "End date is required."
            },
            status: {
                required: "Please select a status."
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

    $progressForm.on("submit", function (e) {
        e.preventDefault();
        if (!$progressForm.valid()) return;

        const progressData = getFormData(progressFormInputs);


        $.ajax({
            url: `${urls.company_work_progress}/edit`,
            method: "POST",
            data: progressData,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Saved", "Progress updated successfully.");
            },
            error: function (jqxhr) {
            }
        });
    });
    //#endregion

    initSelect2();

    flatpickr("#completedDate,#startDate,#endDate", {
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