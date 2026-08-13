$(function () {

    initSelect2();

    flatpickr("#dateBilled,#auditStartDate,#asAtDate,#firstReviewSendDate,#firstReviewEndDate,#dateOfReport,#dateOfDirectorsReport,#directorDateSent,#directorFollowUpDate,#directorDateReceived,#directorCommOfOathsDate,#taxDueDate,#secSSMDueDate,#datePassToSecDept,#targetTaxWorkDate,#datePassToTaxDept,#postAuditDateBinded", {
        allowInput: true
    });

    // Auto calculate YE + 7 months for SSM Due Date & Target Tax Work Date
    function addMonths(date, months) {
        const d = new Date(date);
        d.setMonth(d.getMonth() + months);
        return d;
    }

    function formatDate(d) {
        if (!d || isNaN(d.getTime())) return "";
        const year = d.getFullYear();
        const month = String(d.getMonth() + 1).padStart(2, '0');
        const day = String(d.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    }

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

            if (selectedDate.getTime() !== lastDay.getTime()) {
                instance.setDate(lastDay, true);
                return;
            }

            // YE + 7 months
            const ye7m = addMonths(lastDay, 7);
            const ye7mStr = formatDate(ye7m);

            $("#secSSMDueDate").val(ye7mStr);
            $("#targetTaxWorkDate").val(ye7mStr);
            calculateMetric();
        },
    });

    const params = new URLSearchParams();
    params.append('type', 'SdnBhd');
    params.append('type', 'LLP');

    let clientDataMap = {};

    $.ajax({
        method: "GET",
        manualLoading: true,
        url: `${urls.client}/selections?${params.toString()}`,
        success: function (res) {
            let html = "";
            $.each(res, function (index, item) {
                clientDataMap[item.clientId] = item;
                html += `<option value="${item.clientId}">${item.name}</option>`;
            });

            $("#clientSelect").append(html);
        }
    });

    // Autofill client details when client is selected
    $("#clientSelect").on("change", function () {
        const clientId = $(this).val();
        if (clientId && clientDataMap[clientId]) {
            const client = clientDataMap[clientId];
            $("#activity").val(client.activitySize || "");
            if (client.financialYearEnd) {
                $("#yearEnd").val(ConvertTimeFormat(client.financialYearEnd, "DD-MM-YYYY"));
            } else {
                $("#yearEnd").val("");
            }
            if (client.auditExemption !== undefined && client.auditExemption !== null) {
                $("#auditExemption").val(client.auditExemption.toString()).trigger('change');
            }
            $("#status").val("Pending").trigger('change');
        }
    });

    // Dynamic field calculations
    function calculateDays() {
        const startStr = $("#auditStartDate").val();
        const asAtStr = $("#asAtDate").val();
        let cDays = 0;

        if (startStr && asAtStr) {
            const startDate = new Date(startStr);
            const asAtDate = new Date(asAtStr);
            if (!isNaN(startDate) && !isNaN(asAtDate)) {
                const diffTime = asAtDate - startDate;
                cDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
                $("#numberOfDays").val(cDays >= 0 ? cDays : 0);
            }
        } else {
            $("#numberOfDays").val("");
        }

        calculateTotalFieldWorkDays(cDays);
    }

    function calculateReviewDays() {
        const sendStr = $("#firstReviewSendDate").val();
        const endStr = $("#firstReviewEndDate").val();
        let rcDays = 0;

        if (sendStr && endStr) {
            const sendDate = new Date(sendStr);
            const endDate = new Date(endStr);
            if (!isNaN(sendDate) && !isNaN(endDate)) {
                const diffTime = endDate - sendDate;
                rcDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
                $("#kuchingReviewDays").val(rcDays >= 0 ? rcDays : 0);
            }
        } else {
            $("#kuchingReviewDays").val("");
        }

        const cDays = parseInt($("#numberOfDays").val()) || 0;
        calculateTotalFieldWorkDays(cDays);
    }

    function calculateTotalFieldWorkDays(cDays) {
        const rcDays = parseInt($("#kuchingReviewDays").val()) || 0;
        $("#totalFieldWorkDays").val(cDays + rcDays);
    }

    function calculateMetric() {
        const passTaxStr = $("#datePassToTaxDept").val();
        const targetTaxStr = $("#targetTaxWorkDate").val();

        if (passTaxStr && targetTaxStr) {
            const passTaxDate = new Date(passTaxStr);
            const targetTaxDate = new Date(targetTaxStr);
            if (!isNaN(passTaxDate) && !isNaN(targetTaxDate)) {
                const diffTime = passTaxDate - targetTaxDate;
                const metricDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
                $("#timelinessMetric").val(`${metricDays} days`);
            }
        } else {
            $("#timelinessMetric").val("");
        }
    }

    $("#auditStartDate, #asAtDate").on("change input", calculateDays);
    $("#firstReviewSendDate, #firstReviewEndDate").on("change input", calculateReviewDays);
    $("#datePassToTaxDept, #targetTaxWorkDate").on("change input", calculateMetric);

    $.ajax({
        method: "GET",
        manualLoading: true,
        url: `${urls.users}/pic`,
        success: function (res) {
            let html = "";
            $.each(res, function (index, item) {
                html += `<option value="${item.id}">${item.name} (Dept: ${item.department})</option>`;
            });

            $("#personInCharge").append(html);
        }
    });

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
        auditExemption: $taskForm.find('select[name="auditExemption"]'),
        reportType: $taskForm.find('select[name="reportType"]'),
        signingFirm: $taskForm.find('select[name="signingFirm"]'),
        revenue: $taskForm.find('input[name="revenue"]'),
        profit: $taskForm.find('input[name="profitLoss"]'),
        auditFee: $taskForm.find('input[name="auditFee"]'),
        dateBilled: $taskForm.find('input[name="dateBilled"]'),
        auditStartDate: $taskForm.find('input[name="auditStartDate"]'),
        auditEndDate: $taskForm.find('input[name="asAtDate"]'),
        totalFieldWorkDays: $taskForm.find('input[name="totalFieldWorkDays"]'),
        firstReviewSendDate: $taskForm.find('input[name="firstReviewSendDate"]'),
        firstReviewEndDate: $taskForm.find('input[name="firstReviewEndDate"]'),
        kualaLumpurOfficeReportDate: $taskForm.find('input[name="dateOfReport"]'),
        kualaLumpurOfficeDirectorsReportDate: $taskForm.find('input[name="dateOfDirectorsReport"]'),
        directorDateSent: $taskForm.find('input[name="directorDateSent"]'),
        directorFollowUpDate: $taskForm.find('input[name="directorFollowUpDate"]'),
        directorDateReceived: $taskForm.find('input[name="directorDateReceived"]'),
        directorCommOfOathsDate: $taskForm.find('input[name="directorCommOfOathsDate"]'),
        taxDueDate: $taskForm.find('input[name="taxDueDate"]'),
        secSSMDueDate: $taskForm.find('input[name="secSSMDueDate"]'),
        datePassToSecDept: $taskForm.find('input[name="datePassToSecDept"]'),
        targetTaxWorkDate: $taskForm.find('input[name="targetTaxWorkDate"]'),
        datePassToTaxDept: $taskForm.find('input[name="datePassToTaxDept"]'),
        isPostAuditBinded: $taskForm.find('select[name="isPostAuditBinded"]'),
        postAuditDateBinded: $taskForm.find('input[name="postAuditDateBinded"]')
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
            url: `${urls.aex_template}/${taskId}`,
            method: "GET",
            success: function (res) {
                if (res) {
                    editId = taskId;
                    dataRow = row;

                    taskFormInputs.clientId.prop("disabled", true).val(res.clientId).trigger('change');
                    taskFormInputs.database.val(res.database).trigger('change');
                    yearEndInstances.setDate(ConvertTimeFormat(res.yearEndToDo, "MMMM YYYY"), true);
                    taskFormInputs.quarterToDo.val(res.quarterToDo).trigger('change');
                    taskFormInputs.personInChargeId.val(res.personInChargeId).trigger('change');

                    if (res.auditExemption !== undefined && res.auditExemption !== null) {
                        taskFormInputs.auditExemption.val(res.auditExemption.toString()).trigger('change');
                    }
                    taskFormInputs.reportType.val(res.reportType).trigger('change');
                    taskFormInputs.signingFirm.val(res.signingFirm).trigger('change');

                    taskFormInputs.revenue.val(res.revenue);
                    taskFormInputs.profit.val(res.profit);
                    taskFormInputs.auditFee.val(res.auditFee);
                    taskFormInputs.dateBilled.val(ConvertTimeFormat(res.dateBilled, "YYYY-MM-DD"));
                    taskFormInputs.auditStartDate.val(ConvertTimeFormat(res.auditStartDate, "YYYY-MM-DD"));
                    taskFormInputs.auditEndDate.val(ConvertTimeFormat(res.auditEndDate, "YYYY-MM-DD"));
                    calculateDays();

                    if (res.status === "Work In Progress") {
                        taskFormInputs.status.val("Audit_WIP").trigger('change');
                    } else if (res.status === "First Review") {
                        taskFormInputs.status.val("Audit_FirstReview").trigger('change');
                    } else {
                        taskFormInputs.status.val(res.status).trigger('change');
                    }

                    taskFormInputs.firstReviewSendDate.val(ConvertTimeFormat(res.firstReviewSendDate, "YYYY-MM-DD"));
                    taskFormInputs.firstReviewEndDate.val(ConvertTimeFormat(res.firstReviewEndDate, "YYYY-MM-DD"));
                    calculateReviewDays();

                    taskFormInputs.kualaLumpurOfficeReportDate.val(ConvertTimeFormat(res.kualaLumpurOfficeReportDate, "YYYY-MM-DD"));
                    taskFormInputs.kualaLumpurOfficeDirectorsReportDate.val(ConvertTimeFormat(res.kualaLumpurOfficeDirectorsReportDate, "YYYY-MM-DD"));

                    taskFormInputs.directorDateSent.val(ConvertTimeFormat(res.directorDateSent, "YYYY-MM-DD"));
                    taskFormInputs.directorFollowUpDate.val(ConvertTimeFormat(res.directorFollowUpDate, "YYYY-MM-DD"));
                    taskFormInputs.directorDateReceived.val(ConvertTimeFormat(res.directorDateReceived, "YYYY-MM-DD"));
                    taskFormInputs.directorCommOfOathsDate.val(ConvertTimeFormat(res.directorCommOfOathsDate, "YYYY-MM-DD"));

                    taskFormInputs.taxDueDate.val(ConvertTimeFormat(res.taxDueDate, "YYYY-MM-DD"));
                    taskFormInputs.secSSMDueDate.val(ConvertTimeFormat(res.secSSMDueDate, "YYYY-MM-DD"));
                    taskFormInputs.datePassToSecDept.val(ConvertTimeFormat(res.datePassToSecDept, "YYYY-MM-DD"));
                    taskFormInputs.targetTaxWorkDate.val(ConvertTimeFormat(res.targetTaxWorkDate, "YYYY-MM-DD"));
                    taskFormInputs.datePassToTaxDept.val(ConvertTimeFormat(res.datePassToTaxDept, "YYYY-MM-DD"));
                    calculateMetric();

                    if (res.isPostAuditBinded !== undefined && res.isPostAuditBinded !== null) {
                        taskFormInputs.isPostAuditBinded.val(res.isPostAuditBinded.toString()).trigger('change');
                    }
                    taskFormInputs.postAuditDateBinded.val(ConvertTimeFormat(res.postAuditDateBinded, "YYYY-MM-DD"));

                    $('#taskModal').modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching task data:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to fetch task data. Please try again.");
            }
        });
    });

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
        $taskForm.find('select[name="auditExemption"]').val(null).trigger('change');
        $taskForm.find('select[name="reportType"]').val(null).trigger('change');
        $taskForm.find('select[name="signingFirm"]').val(null).trigger('change');
        $taskForm.find('select[name="isPostAuditBinded"]').val(null).trigger('change');
        $("#activity,#yearEnd,#numberOfDays,#kuchingReviewDays,#totalFieldWorkDays,#timelinessMetric").val('');
        editId = 0;
        dataRow = null;
    }

    //#endregion
});