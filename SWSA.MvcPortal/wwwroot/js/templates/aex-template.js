$(function () {

    initSelect2();

    flatpickr("#dateBilled,#auditStartDate,#asAtDate,#firstReviewSendDate,#firstReviewEndDate,#secondReviewSendDate,#secondReviewEndDate,#secondReviewFinal,#dateSentToKK,#dateReceivedAR,#dateOfReport,#dateOfDirectorsReport,#directorDateSent,#directorFollowUpDate,#directorDateReceived,#directorCommOfOathsDate,#taxDueDate,#datePassToTaxDept,#secSSMDueDate,#datePassToSecDept,#postAuditDateBinded,#postAuditDespatchDateToClient", {
        allowInput: true
    });

    const yearEndInstances = flatpickr("#yearEndToDo", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                altFormat: "d-m-Y",
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
    });

    const params = new URLSearchParams();
    params.append('type', 'SdnBhd');
    params.append('type', 'LLP');

    let modal_AuditWorkProgress = 0;
    const modal_AuditTotalProgress = $(".audit-wip-prog").length;
    const modal_AuditWorkProgressStep = 100 / modal_AuditTotalProgress;

    $.ajax({
        method: "GET",
        manualLoading: true,
        url: `${urls.client}/selections?${params.toString()}`,
        success: function (res) {
            let html = "";
            $.each(res, function (index, item) {
                html += `<option value="${item.clientId}">${item.name}</option>`;
            })

            $("#clientSelect").append(html);
        }
    })

    $.ajax({
        method: "GET",
        manualLoading: true,
        url: `${urls.users}/pic`,
        success: function (res) {
            let html = "";
            $.each(res, function (index, item) {
                html += `<option value="${item.id}">${item.name} (Dept: ${item.department})</option>`;
            })

            $("#personInCharge").append(html);
        }
    })

    $(".audit-wip-prog").on("change", function () {
        const val = $(this).prop("checked");
        if (val) {
            modal_AuditWorkProgress += modal_AuditWorkProgressStep;
        } else {
            modal_AuditWorkProgress -= modal_AuditWorkProgressStep;
        }

        $("#totalPercent").val(`${modal_AuditWorkProgress}%`)

    })

    const taskDatatable = $("#taskDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "responsive": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],

    });

    taskDatatable.buttons().container().appendTo('#taskDatatable_wrapper .col-md-6:eq(0)');

    tableResizeEventListener();


    //#region Task Form
    const $taskForm = $("#taskForm");
    const taskFormInputs = {
        clientId: $taskForm.find('select[name="clientSelect"]'),
        database: $taskForm.find('select[name="database"]'),
        yearEndToDo: $taskForm.find('input[name="yearEndToDo"]'),
        quarterToDo: $taskForm.find('select[name="quarterToDo"]'),
        personInChargeId: $taskForm.find('select[name="personInCharge"]'),
        status: $taskForm.find('select[name="status"]'),
        revenue: $taskForm.find('input[name="revenue"]'),
        profit: $taskForm.find('input[name="profitLoss"]'),
        auditFee: $taskForm.find('input[name="auditFee"]'),
        dateBilled: $taskForm.find('input[name="dateBilled"]'),
        auditStartDate: $taskForm.find('input[name="auditStartDate"]'),
        auditEndDate: $taskForm.find('input[name="asAtDate"]'),
        totalFieldWorkDays: $taskForm.find('input[name="totalFieldWorkDays"]'),
        auditWIPResult: $taskForm.find('select[name="auditWIPResult"]'),
        isAccSetupComplete: $taskForm.find('input[name="isAccSetupComplete"]'),
        isAccSummaryComplete: $taskForm.find('input[name="isAccSummaryComplete"]'),
        isAuditPlanningComplete: $taskForm.find('input[name="isAuditPlanningComplete"]'),
        isAuditExecutionComplete: $taskForm.find('input[name="isAuditExecutionComplete"]'),
        isExecutionAuditComplete: $taskForm.find('input[name="isExecutionAuditComplete"]'),
        firstReviewSendDate: $taskForm.find('input[name="firstReviewSendDate"]'),
        firstReviewEndDate: $taskForm.find('input[name="firstReviewEndDate"]'),
        firstReviewResult: $taskForm.find('select[name="firstReviewResult"]'),
        secondReviewSendDate: $taskForm.find('input[name="secondReviewSendDate"]'),
        secondReviewEndDate: $taskForm.find('input[name="secondReviewEndDate"]'),
        secondReviewResult: $taskForm.find('select[name="secondReviewResult"]'),
        secondReviewFinal: $taskForm.find('input[name="secondReviewFinal"]'),
        kualaLumpurOfficeDateSent: $taskForm.find('input[name="dateSentToKK"]'),
        kualaLumpurOfficeAuditReportReceivedDate: $taskForm.find('input[name="dateReceivedAR"]'),
        kualaLumpurOfficeReportDate: $taskForm.find('input[name="dateOfReport"]'),
        kualaLumpurOfficeDirectorsReportDate: $taskForm.find('input[name="dateOfDirectorsReport"]'),
        directorDateSent: $taskForm.find('input[name="directorDateSent"]'),
        directorFollowUpDate: $taskForm.find('input[name="directorFollowUpDate"]'),
        directorDateReceived: $taskForm.find('input[name="directorDateReceived"]'),
        directorCommOfOathsDate: $taskForm.find('input[name="directorCommOfOathsDate"]'),
        taxDueDate: $taskForm.find('input[name="taxDueDate"]'),
        datePassToTaxDept: $taskForm.find('input[name="datePassToTaxDept"]'),
        secSSMDueDate: $taskForm.find('input[name="secSSMDueDate"]'),
        datePassToSecDept: $taskForm.find('input[name="datePassToSecDept"]'),
        postAuditDateBinded: $taskForm.find('input[name="postAuditDateBinded"]'),
        postAuditDespatchDateToClient: $taskForm.find('input[name="postAuditDespatchDateToClient"]')
    };

    let editId = 0;
    let dataRow = null;

    // Form validation
    $taskForm.validate({
        rules: {
            clientSelect: {
                required: true
            },
            database: {
                required: true
            },
            status: {
                required: true
            },
            yearEndToDo: {
                required: true
            },
            quarterToDo: {
                required: true
            },
            auditStartDate: {
                required: true
            },
            asAtDate: {
                required: true
            }
        },
        messages: {
            clientSelect: {
                required: "Please select a client."
            },
            database: {
                required: "Please select a database."
            },
            status: {
                required: "Please select a status."
            },
            yearEndToDo: {
                required: "Please select year end to do date."
            },
            quarterToDo: {
                required: "Please select a quarter."
            },
            auditStartDate: {
                required: "Please select audit start date."
            },
            asAtDate: {
                required: "Please select audit end date."
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

    // Save button click
    $("#saveBtn").on("click", function () {
        const ye = taskFormInputs.yearEndToDo.val();
        if (!$taskForm.valid()) {
            Toast_Fire(ICON_ERROR, "Validation Error", "Please fill in all required fields.");
            return;
        }
        if (ye?.trim() === "") {
            Toast_Fire(ICON_ERROR, "Validation Error", "Please select a date for YE.");
            return;
        }

        const taskData = getFormData(taskFormInputs);
        taskData.yearEndToDo = convertMonthStringToLastDay(ye);
        let url = `${urls.aex_template}`;

        if (editId > 0) {
            url += `/${editId}/update`;
        } else {
            url += `/create`;
        }

        $.ajax({
            url: url,
            method: "POST",
            data: {
                req: taskData
            },
            success: function (res) {
                if (res) {
                    if (editId > 0) {
                        Toast_Fire(ICON_SUCCESS, "Save", "Task saved successfully.");
                        dataRow.remove();
                    } else {
                        Toast_Fire(ICON_SUCCESS, "Create", "Task created successfully.");
                    }

                    $('#taskModal').modal('hide');
                    window.location.reload();
                }
            },
            error: function (xhr, status, error) {
                console.error('Error saving task:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to save task. Please try again.");
            }
        });
    });

    // Edit task
    $(document).on("click", ".edit-task", function () {
        const taskId = $(this).data("id");
        const row = taskDatatable.row($(this).closest("tr"));

        $.ajax({
            url: `${urls.aex_template}/${taskId}`, // Update with your actual URL
            method: "GET",
            success: function (res) {
                if (res) {
                    editId = taskId;
                    dataRow = row;
                    // Populate form fields using taskFormInputs object
                    taskFormInputs.clientId.prop("disabled", true).val(res.clientId).trigger('change');
                    taskFormInputs.database.val(res.database).trigger('change');
                    yearEndInstances.setDate(ConvertTimeFormat(res.yearEndToDo, "MMMM YYYY"), true);
                    taskFormInputs.quarterToDo.val(res.quarterToDo).trigger('change');
                    taskFormInputs.personInChargeId.val(res.personInChargeId).trigger('change');
                    taskFormInputs.revenue.val(res.revenue);
                    taskFormInputs.profit.val(res.profit);
                    taskFormInputs.auditFee.val(res.auditFee);
                    taskFormInputs.dateBilled.val(ConvertTimeFormat(res.dateBilled, "YYYY-MM-DD"));
                    taskFormInputs.auditStartDate.val(ConvertTimeFormat(res.auditStartDate, "YYYY-MM-DD"));
                    taskFormInputs.auditEndDate.val(ConvertTimeFormat(res.auditEndDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="numberOfDays"]').val(res.numberOfDays);
                    taskFormInputs.totalFieldWorkDays.val(res.totalFieldWorkDays);

                    if (res.status === "Work In Progress") {
                        taskFormInputs.status.val("Audit_WIP").trigger('change');
                    } else if (res.status === "First Review") {
                        taskFormInputs.status.val("Audit_FirstReview").trigger('change');
                    } else {
                        taskFormInputs.status.val(res.status).trigger('change');
                    }

                    taskFormInputs.auditWIPResult.val(res.auditWIPResult).trigger('change');
                    // Set checkboxes
                    taskFormInputs.isAccSetupComplete.prop('checked', res.isAccSetupComplete);
                    taskFormInputs.isAccSummaryComplete.prop('checked', res.isAccSummaryComplete);
                    taskFormInputs.isAuditPlanningComplete.prop('checked', res.isAuditPlanningComplete);
                    taskFormInputs.isAuditExecutionComplete.prop('checked', res.isAuditExecutionComplete);
                    taskFormInputs.isExecutionAuditComplete.prop('checked', res.isExecutionAuditComplete);

                    // Calculate and set total percentage
                    calculateTotalPercent();

                    // Set review dates
                    taskFormInputs.firstReviewSendDate.val(ConvertTimeFormat(res.firstReviewSendDate, "YYYY-MM-DD"));
                    taskFormInputs.firstReviewEndDate.val(ConvertTimeFormat(res.firstReviewEndDate, "YYYY-MM-DD"));
                    taskFormInputs.firstReviewResult.val(res.firstReviewResult).trigger('change');
                    taskFormInputs.secondReviewSendDate.val(ConvertTimeFormat(res.secondReviewSendDate, "YYYY-MM-DD"));
                    taskFormInputs.secondReviewEndDate.val(ConvertTimeFormat(res.secondReviewEndDate, "YYYY-MM-DD"));
                    taskFormInputs.secondReviewResult.val(res.secondReviewResult).trigger('change');
                    taskFormInputs.secondReviewFinal.val(res.secondReviewFinal).trigger('change');
                    $taskForm.find('input[name="totalReviewDays"]').val(res.totalReviewDays);

                    // Set AT3.3 dates
                    taskFormInputs.kualaLumpurOfficeDateSent.val(ConvertTimeFormat(res.kualaLumpurOfficeDateSent, "YYYY-MM-DD"));
                    taskFormInputs.kualaLumpurOfficeAuditReportReceivedDate.val(ConvertTimeFormat(res.kualaLumpurOfficeAuditReportReceivedDate, "YYYY-MM-DD"));
                    taskFormInputs.kualaLumpurOfficeReportDate.val(ConvertTimeFormat(res.kualaLumpurOfficeReportDate, "YYYY-MM-DD"));
                    taskFormInputs.kualaLumpurOfficeDirectorsReportDate.val(ConvertTimeFormat(res.kualaLumpurOfficeDirectorsReportDate, "YYYY-MM-DD"));

                    // Set AT3.4 dates
                    taskFormInputs.directorDateSent.val(ConvertTimeFormat(res.directorDateSent, "YYYY-MM-DD"));
                    taskFormInputs.directorFollowUpDate.val(ConvertTimeFormat(res.directorFollowUpDate, "YYYY-MM-DD"));
                    taskFormInputs.directorDateReceived.val(ConvertTimeFormat(res.directorDateReceived, "YYYY-MM-DD"));
                    taskFormInputs.directorCommOfOathsDate.val(ConvertTimeFormat(res.directorCommOfOathsDate, "YYYY-MM-DD"));

                    // Set Tax dept dates
                    taskFormInputs.taxDueDate.val(ConvertTimeFormat(res.taxDueDate, "YYYY-MM-DD"));
                    taskFormInputs.datePassToTaxDept.val(ConvertTimeFormat(res.datePassToTaxDept, "YYYY-MM-DD"));

                    // Set Sec dept dates
                    taskFormInputs.secSSMDueDate.val(ConvertTimeFormat(res.secSSMDueDate, "YYYY-MM-DD"));
                    taskFormInputs.datePassToSecDept.val(ConvertTimeFormat(res.datePassToSecDept, "YYYY-MM-DD"));

                    // Set Post Audit Work dates
                    taskFormInputs.postAuditDateBinded.val(ConvertTimeFormat(res.postAuditDateBinded, "YYYY-MM-DD"));
                    taskFormInputs.postAuditDespatchDateToClient.val(ConvertTimeFormat(res.postAuditDespatchDateToClient, "YYYY-MM-DD"));

                    $('#taskModal').modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching task data:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to fetch task data. Please try again.");
            }
        });
    });

    function calculateTotalPercent() {
        var checkboxes = $(".audit-wip-prog:checked").length;
        modal_AuditWorkProgress = checkboxes * modal_AuditWorkProgressStep;
        $("#totalPercent").val(`${modal_AuditWorkProgress}%`)
    }

    // Delete task
    $(document).on("click", ".delete-task", function () {
        const taskId = $(this).data("id");
        const row = taskDatatable.row($(this).closest("tr"));

        if (confirm("Are you sure you want to delete this task?")) {
            $.ajax({
                url: `${urls.aex_template}/${taskId}/delete`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Task deleted successfully.");
                    row.remove();
                    taskDatatable.draw(false);
                },
                error: function (res) {
                    Toast_Fire(ICON_ERROR, "Error", "Something went wrong. Please try again later.");
                }
            });
        }
    });

    $('#taskModal').on('hide.bs.modal', function () {
        resetTaskForm();
    });

    $('#taskModal').on('show.bs.modal', function () {
        if (editId === 0) {
            $('#modalTitle').text('New Audit Task');
        } else {
            $('#modalTitle').text('Update Audit Task');
        }
    });

    function resetTaskForm() {
        $taskForm[0].reset();
        $taskForm.find('.is-invalid').removeClass('is-invalid');
        $taskForm.find('.invalid-feedback').remove();
        $taskForm.find('select[name="clientSelect"]').prop("disabled", false).val(null).trigger('change');
        $taskForm.find('select[name="database"]').val(null).trigger('change');
        $taskForm.find('select[name="quarterToDo"]').val(null).trigger('change');
        $taskForm.find('select[name="personInCharge"]').val(null).trigger('change');
        $taskForm.find('select[name="status"]').val(null).trigger('change');
        $taskForm.find('select[name="auditWIPResult"]').val(null).trigger('change');
        $taskForm.find('select[name="firstReviewResult"]').val(null).trigger('change');
        $taskForm.find('select[name="secondReviewResult"]').val(null).trigger('change');
        $('#totalPercent').val('');
        modal_AuditWorkProgress = 0;
        editId = 0;
        dataRow = null;
    }

    //#endregion
})