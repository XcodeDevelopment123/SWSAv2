
$(function () {
    const $taskForm = $("#taskForm");

    const taskFormInputs = {
        companyId: $("#companyId"),
        companyStatus: $taskForm.find('select[name="companyStatus"]'),
        workType: $taskForm.find('select[name="workType"]'),
        isYearEndTask: $taskForm.find('select[name="isYearEndTask"]'),
        serviceScope: $taskForm.find('select[name="serviceScope"]'),
        companyActivityLevel: $taskForm.find('select[name="companyActivityLevel"]'),
        internalNote: $taskForm.find('input[name="internalNote"]'),
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

    $("#btnSubmitRequest").on("click", function (e) {
        if (!$taskForm.valid()) return;

        const taskData = getFormData(taskFormInputs);

        taskData.auditUsers = getAuditUserData();
        taskData.accountUsers = getAccountUserData();

        if (taskData.auditUser !== null &&
            (taskData.auditUsers.months.length === 0 || taskData.auditUsers.staffIds.length === 0)
        ) {
            var missingLabel = taskData.auditUsers.months.length === 0 ? "month(s)" : "user(s)";
            Toast_Fire(ICON_ERROR, "Invalid action", `Please select ${missingLabel} for audit work. `);
            return;
        }
        if (taskData.accountUsers !== null &&
            (taskData.accountUsers.months.length === 0 || taskData.accountUsers.staffIds.length === 0)) {

            var missingLabel = taskData.accountUsers.months.length === 0 ? "month(s)" : "user(s)";
            Toast_Fire(ICON_ERROR, "Invalid action", `Please select ${missingLabel} for account work. `);
            return;
        }


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

    flatpickr("#dueDate", {
        allowInput: true
    });

    $(document).on('click', '.btn-delete-row', function () {
        const table = $(this).closest('table').DataTable();
        table.row($(this).closest('tr')).remove().draw(false);
    });

    $(document).on('click', '#workUserForm .btn-delete-row', function () {
        const id = $(this).val();
        workUserFormInputs.staffId.find(`option[value="${id}"]`).prop('disabled', false);
    });

    $(document).on('click', '#accountUserForm .btn-delete-row', function () {
        const id = $(this).val();
        accountUserFormInputs.staffId.find(`option[value="${id}"]`).prop('disabled', false);
    });

    //#region Audit Work Form
    const $workUserForm = $("#workUserForm");
    const workUserFormInputs = {
        staffId: $workUserForm.find('select[name="staffId"]'),
        monthToDo: $workUserForm.find('select[name="monthToDo"]'),
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

        const data = {
            staffId: staffId,
            name: `${staffName}`,
        };

        const rowHtml = [
            `<td data-staff-id="${data.staffId}">
                ${data.name}
            </td>`,
            `<td>
            <button type="button" class="btn btn-sm btn-danger btn-delete-row" value="${data.staffId}">
                <i class="fa fa-trash"></i>
            </button>
        </td>`
        ];

        auditUserTable.row.add(rowHtml).draw(false);

        workUserFormInputs.staffId.find(`option[value="${staffId}"]`).prop('disabled', true);
        workUserFormInputs.staffId.val("").trigger('change');
    });

    //#endregion

    //#region Account Work Form
    const $accountUserForm = $("#accountUserForm");
    const accountUserFormInputs = {
        staffId: $accountUserForm.find('select[name="staffId"]'),
        monthToDo: $accountUserForm.find('select[name="monthToDo"]'),
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

        const data = {
            staffId: staffId,
            name: `${staffName}`,
        };

        const rowHtml = [
            `<td data-staff-id="${data.staffId}">
                ${data.name}
            </td>`,
            `<td>
            <button type="button" class="btn btn-sm btn-danger btn-delete-row" value="${data.staffId}">
                <i class="fa fa-trash"></i>
            </button>
        </td>`
        ];

        accountUserTable.row.add(rowHtml).draw(false);

        accountUserFormInputs.staffId.find(`option[value="${staffId}"]`).prop('disabled', true);
        accountUserFormInputs.staffId.val("").trigger('change');
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

    function getAuditUserData() {
        const months = workUserFormInputs.monthToDo.find('option:selected')
            .map(function () { return $(this).text(); })
            .get(); // array of names
        const data = {
            months: months,
            staffIds: [], // staff ID
        };
        auditUserTable.rows().every(function () {
            const rowData = this.data();

            const staffCell = $('<div>').html(rowData[0]).find('td, *[data-staff-id]');
            const staffId = staffCell.data('staff-id');

            data.staffIds.push(staffId);
        });

        if (data.months.length === 0 && data.staffIds.length === 0) {
            return null;
        }

        return data;
    }

    function getAccountUserData() {
        const months = accountUserFormInputs.monthToDo.find('option:selected')
            .map(function () { return $(this).text(); })
            .get(); // array of names
        const data = {
            months: months,
            staffIds: [], // staff ID
        };
        accountUserTable.rows().every(function () {
            const rowData = this.data();

            const staffCell = $('<div>').html(rowData[0]).find('td, *[data-staff-id]');
            const staffId = staffCell.data('staff-id');

            data.staffIds.push(staffId);
        });

        if (data.months.length === 0 && data.staffIds.length === 0) {
            return null;
        }
        return data;
    }
})

//flatpickr("#dueDate", {
//    plugins: [
//        new monthSelectPlugin({
//            shorthand: false,
//            dateFormat: "m (F)",
//            altFormat: "m (F)",
//        })
//    ],
//    onReady: function (selectedDates, dateStr, instance) {
//        instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
//    }
//});