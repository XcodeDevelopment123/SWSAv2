$(function () {
    const isSuperAdmin = $("#isSuperAdmin").val() === 'True';
    //#region Task
    const $taskForm = $("#taskForm");

    const taskFormInputs = {
        taskId: $("#taskId"),
        companyStatus: $taskForm.find('select[name="companyStatus"]'),
        workType: $taskForm.find('select[name="workType"]'),
        isYearEndTask: $taskForm.find('select[name="isYearEndTask"]'),
        serviceScope: $taskForm.find('select[name="serviceScope"]'),
        companyActivityLevel: $taskForm.find('select[name="companyActivityLevel"]'),
        internalNote: $taskForm.find('input[name="internalNote"]'),
        auditMonths: $taskForm.find('select[name="auditMonths"]'),
        accountMonths: $taskForm.find('select[name="accountMonths"]'),
        ssmExtensionDate: $taskForm.find('input[name="ssmExtensionDate"]'),
        agmDate: $taskForm.find('input[name="agmDate"]'),
        arDueDate: $taskForm.find('input[name="arDueDate"]'),
        reminderDate: $taskForm.find('input[name="reminderDate"]'),
    };

    $taskForm.validate({
        rules: {
            companyStatus: {
                required: true
            },
            workType: {
                required: true
            },
            isYearEndTask: {
                required: true
            },
            serviceScope: {
                required: true
            },
            companyActivityLevel: {
                required: true
            },
        },
        messages: {
            companyStatus: {
                required: "Please select a company status"
            },
            workType: {
                required: "Work type is required."
            },
            isYearEndTask: {
                required: "Please define is year end task or not"
            },
            serviceScope: {
                required: "Service scope is required."
            },
            companyActivityLevel: {
                required: "Company activity level is required."
            },
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
        const taskData = getFormData(taskFormInputs);
        if (!$taskForm.valid()) return;

        $.ajax({
            url: `${urls.company_works}/update`,
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

    flatpickr("#ssmExtensionDate,#agmDate,#arDueDate,#reminderDate,#completedDate,#startDate,#endDate", {
        allowInput: true
    });

    //#region Audit Work Form
    const $workUserForm = $("#workUserForm");
    const workUserFormInputs = {
        staffId: $workUserForm.find('select[name="staffId"]'),
    };
    const auditUserTable = $('#auditUserTable').DataTable({
        paging: true,
        lengthChange: false,
        searching: false,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    $workUserForm.validate({
        rules: {
            staffId: {
                required: true
            },
        },
        messages: {
            staffId: {
                required: "User is required."
            },
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

    $workUserForm.on('submit', function (e) {
        e.preventDefault();

        if (!$workUserForm.valid()) {
            return;
        }

        const staffId = workUserFormInputs.staffId.val(); // staff ID
        const staffName = workUserFormInputs.staffId.find('option:selected').text(); // staff 名字

        const req = {
            staffId: staffId,
            taskId: taskFormInputs.taskId.val(),
            department: "Audit"
        }

        $.ajax({
            url: `${urls.company_works}/add-user`,
            method: "POST",
            data: req,
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Add user to handle audit work");
                    const data = {
                        staffId: staffId,
                        name: `${staffName}`,
                    };

                    const rowHtml = [
                        `<td data-staff-id="${data.staffId}">
                ${data.name}
                    </td>`,
                        `<td>
                    <button type="button" class="${isSuperAdmin ? "btn-delete-user" : ""} btn btn-sm btn-danger btn-delete-row" 
                     data-bs-toggle="tooltip"
                     data-department="Audit"
                     data-bs-placement="top" title="Only super admin can remove" value="${data.staffId}">
                    
                     <i class="fa fa-trash"></i>
                    </button>
                </td>`
                    ];

                    auditUserTable.row.add(rowHtml).draw(false);

                    workUserFormInputs.staffId.find(`option[value="${staffId}"]`).prop('disabled', true);
                    workUserFormInputs.staffId.val("").trigger('change');
                }
            }
        });
    });

    //#endregion

    //#region Account Work Form
    const $accountUserForm = $("#accountUserForm");
    const accountUserFormInputs = {
        staffId: $accountUserForm.find('select[name="staffId"]'),
    };

    $accountUserForm.validate({
        rules: {
            staffId: {
                required: true
            },
        },
        messages: {
            staffId: {
                required: "User is required."
            },
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

    $accountUserForm.on('submit', function (e) {
        e.preventDefault();

        if (!$accountUserForm.valid()) {
            return;
        }

        const staffId = accountUserFormInputs.staffId.val(); // staff ID
        const staffName = accountUserFormInputs.staffId.find('option:selected').text(); // staff 名字

        const req = {
            staffId: staffId,
            taskId: taskFormInputs.taskId.val(),
            department: "Account"
        }

        $.ajax({
            url: `${urls.company_works}/add-user`,
            method: "POST",
            data: req,
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Add user to handle account work");
                    const data = {
                        staffId: staffId,
                        name: `${staffName}`,
                    };

                    const rowHtml = [
                        `<td data-staff-id="${data.staffId}">
                ${data.name}
                    </td>`,
                        `<td>
                    <button type="button" class="${isSuperAdmin ? "btn-delete-user" : ""} btn btn-sm btn-danger btn-delete-row" 
                     data-bs-toggle="tooltip" data-bs-placement="top" title="Only super admin can remove"
                     data-department="Account"
                     value="${data.staffId}">
                        <i class="fa fa-trash"></i>
                    </button>
                </td>`
                    ];

                    accountUserTable.row.add(rowHtml).draw(false);

                    accountUserFormInputs.staffId.find(`option[value="${staffId}"]`).prop('disabled', true);
                    accountUserFormInputs.staffId.val("").trigger('change');
                }
            }
        });
    });

    const accountUserTable = $('#accountUserTable').DataTable({
        paging: true,
        lengthChange: false,
        searching: false,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });
    //#endregion

    $(document).on("click", ".btn-delete-user", function () {
        const staffId = $(this).val();
        const department = $(this).data('department');
        const name = $(this).data('name');
        const req = {
            staffId: staffId,
            taskId: taskFormInputs.taskId.val(),
            department: department
        }
        const row = $(this).closest('table').DataTable().row($(this).closest('tr'))

        if (confirm(`Are you sure you want to remove "${name}" from work?`)) {
            $.ajax({
                url: `${urls.company_works}/remove-user`,
                data: req,
                method: "POST",
                success: function (res) {
                    if (res) {
                        Toast_Fire(ICON_SUCCESS, "Removed", "user removed successfully.");
                        if (department === "Audit") {
                            workUserFormInputs.staffId.find(`option[value="${staffId}"]`).prop('disabled', false);
                        } else if (department === "Account") {
                            accountUserFormInputs.staffId.find(`option[value="${staffId}"]`).prop('disabled', false);
                        }
                        row.remove().draw(false);
                    }
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })
        }
    })
})