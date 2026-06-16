$(function () {

    initSelect2();

    flatpickr("#arSubmittedDate,#arSentToClientDate,#arReturnedDate,#adSubmittedDate,#adSentToClientDate,#adReturnedDate", {
        allowInput: true
    });

    const params = new URLSearchParams();
    params.append('type', 'SdnBhd');
    params.append('type', 'LLP');

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

    function calcAnniversaryDate(incorpDate) {
        if (!incorpDate) return null;
        const d = new Date(incorpDate);
        if (isNaN(d.getTime())) return null;
        const now = new Date();
        let due = new Date(now.getFullYear(), d.getMonth(), d.getDate());
        if (due < now) due.setFullYear(due.getFullYear() + 1);
        return due;
    }

    function addMonths(date, months) {
        if (!date) return null;
        const d = new Date(date);
        if (isNaN(d.getTime())) return null;
        d.setMonth(d.getMonth() + months);
        return d;
    }

    function formatDate(date) {
        if (!date) return '';
        const d = new Date(date);
        if (isNaN(d.getTime())) return '';
        const y = d.getFullYear();
        const m = String(d.getMonth() + 1).padStart(2, '0');
        const day = String(d.getDate()).padStart(2, '0');
        return `${y}-${m}-${day}`;
    }

    function toDateInputValue(date) {
        if (!date) return '';
        const d = new Date(date);
        if (isNaN(d.getTime())) return '';
        const y = d.getFullYear();
        const m = String(d.getMonth() + 1).padStart(2, '0');
        const day = String(d.getDate()).padStart(2, '0');
        return `${y}-${m}-${day}`;
    }

    function autoCalculate(incorpDateStr) {
        if (!incorpDateStr) {
            $('#incorpDate').val('');
            $('#first18Months').val('');
            $('#arDueDate').val('');
            $('#adDueDate').val('');
            return;
        }
        const incorp = new Date(incorpDateStr);
        if (isNaN(incorp.getTime())) return;

        $('#incorpDate').val(formatDate(incorp));

        const first18 = addMonths(incorp, 18);
        $('#first18Months').val(formatDate(first18));

        const arDue = calcAnniversaryDate(incorp);
        $('#arDueDate').val(formatDate(arDue));

        $('#adDueDate').val(formatDate(first18));
    }

    $("#clientSelect").on("change", function () {
        const id = $(this).val();
        if (!id) return;
        $.ajax({
            method: "GET",
            manualLoading: true,
            url: `${urls.client}/${id}/simple-info`,
            success: function (res) {
                $("#companyType").val(res.clientType).trigger('change');
                $("#yearEndMonth").val(res.yearEndMonth).trigger('change');
                if (res.incorporationDate) {
                    const d = new Date(res.incorporationDate);
                    const local = new Date(d.getTime() + d.getTimezoneOffset() * 60000);
                    autoCalculate(local);
                } else {
                    autoCalculate(null);
                }
            }
        })
    })

    const taskDatatable = $("#taskDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
    });

    taskDatatable.buttons().container().appendTo('#taskDatatable_wrapper .col-md-6:eq(0)');

    tableResizeEventListener();

    const $taskForm = $("#taskForm");
    const taskFormInputs = {
        clientId: $taskForm.find('select[name="clientSelect"]'),
        companyType: $taskForm.find('select[name="companyType"]'),
        yearEnd: $taskForm.find('select[name="yearEndMonth"]'),
        yearInput: $taskForm.find('select[name="yearInput"]'),
        incorpDate: $taskForm.find('input[name="incorpDate"]'),
        first18Months: $taskForm.find('input[name="first18Months"]'),
        arDueDate: $taskForm.find('input[name="arDueDate"]'),
        arSubmitDate: $taskForm.find('input[name="arSubmittedDate"]'),
        arSendToClientDate: $taskForm.find('input[name="arSentToClientDate"]'),
        arReturnByClientDate: $taskForm.find('input[name="arReturnedDate"]'),
        adDueDate: $taskForm.find('input[name="adDueDate"]'),
        adSubmitDate: $taskForm.find('input[name="adSubmittedDate"]'),
        adSendToClientDate: $taskForm.find('input[name="adSentToClientDate"]'),
        adReturnByClientDate: $taskForm.find('input[name="adReturnedDate"]'),
        remarks: $taskForm.find('textarea[name="remarks"]')
    };
    let editId = 0;
    let dataRow = null;
    $taskForm.validate({
        rules: {
            clientSelect: { required: true }
        },
        messages: {
            clientSelect: { required: "Please select a client." }
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

    $("#saveBtn").on("click", function () {
        if (!$taskForm.valid()) { return; }

        const formData = getFormData(taskFormInputs);
        formData.clientId = parseInt(formData.clientId);
        delete formData.first18Months;
        delete formData.incorpDate;

        formData.arDueDate = formData.arDueDate ? new Date(formData.arDueDate).toISOString() : null;
        formData.adDueDate = formData.adDueDate ? new Date(formData.adDueDate).toISOString() : null;

        let url = `${urls.secretary_dept_template}`;
        if (editId > 0) {
            url += `/${editId}/update`
        } else {
            url += `/create`
        }

        $.ajax({
            url: url,
            method: "POST",
            data: { req: formData },
            success: function (res) {
                if (res) {
                    if (editId > 0) {
                        Toast_Fire(ICON_SUCCESS, "Save", "Task saved successfully.");
                        dataRow.remove();
                    } else {
                        Toast_Fire(ICON_SUCCESS, "Create", "Task create successfully.");
                    }

                    const incorpDate = res.client && res.client.incorporationDate
                        ? ConvertTimeFormat(res.client.incorporationDate, "YYYY-MM-DD") : '';

                    taskDatatable.row.add([
                        res.client ? res.client.fileNo : '',
                        res.client ? res.client.name : '',
                        res.client ? res.client.companyType : '',
                        res.client ? res.client.yearEndMonth : '',
                        incorpDate,
                        ConvertTimeFormat(res.arDueDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.arSubmitDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.arSentToClientDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.arReturnByClientDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.adDueDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.adSubmitDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.adSentToClientDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.adReturnByClientDate, "YYYY-MM-DD"),
                        res.remarks,
                        `<button class="btn btn-sm btn-primary edit-task" data-id="${res.id}">Edit</button>
                         <button class="btn btn-sm btn-danger delete-task" data-id="${res.id}">Delete</button>`
                    ]).draw();

                    $('#taskModal').modal('hide');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error creating task:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to create task. Please try again.");
            }
        });
    });

    $(document).on("click", ".edit-task", function () {
        const taskId = $(this).data("id");
        editId = taskId;
        dataRow = taskDatatable.row($(this).closest("tr"));
        $.ajax({
            url: `${urls.secretary_dept_template}/${taskId}`,
            method: "GET",
            success: function (res) {
                if (res) {
                    $taskForm.find('select[name="clientSelect"]').prop("disabled", true).val(res.clientId).trigger('change');
                    $taskForm.find('select[name="companyType"]').val(res.client ? res.client.clientType : '').trigger('change');
                    $taskForm.find('select[name="yearEndMonth"]').val(res.client ? res.client.yearEndMonth : '').trigger('change');
                    $taskForm.find('select[name="yearInput"]').val(res.yearInput || '').trigger('change');
                    $taskForm.find('input[name="incorpDate"]').val(res.client && res.client.incorporationDate
                        ? ConvertTimeFormat(res.client.incorporationDate, "YYYY-MM-DD") : '');
                    if (res.client && res.client.incorporationDate) {
                        const d = new Date(res.client.incorporationDate);
                        const local = new Date(d.getTime() + d.getTimezoneOffset() * 60000);
                        autoCalculate(local);
                    }
                    $taskForm.find('input[name="arDueDate"]').val(ConvertTimeFormat(res.arDueDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="arSubmittedDate"]').val(ConvertTimeFormat(res.arSubmitDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="arSentToClientDate"]').val(ConvertTimeFormat(res.arSentToClientDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="arReturnedDate"]').val(ConvertTimeFormat(res.arReturnByClientDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="adDueDate"]').val(ConvertTimeFormat(res.adDueDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="adSubmittedDate"]').val(ConvertTimeFormat(res.adSubmitDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="adSentToClientDate"]').val(ConvertTimeFormat(res.adSentToClientDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="adReturnedDate"]').val(ConvertTimeFormat(res.adReturnByClientDate, "YYYY-MM-DD"));
                    $taskForm.find('textarea[name="remarks"]').val(res.remarks);
                    $('#taskModal').modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching task data:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to fetch task data. Please try again.");
            }
        });
    });

    $(document).on("click", ".delete-task", function () {
        const taskId = $(this).data("id");
        const row = taskDatatable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete the task?`)) {
            $.ajax({
                url: `${urls.secretary_dept_template}/${taskId}/delete`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Task deleted successfully.");
                    row.remove();
                    taskDatatable.draw(false);
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
                }
            })
        }
    })

    $('#taskModal').on('hide.bs.modal', function () {
        resetTaskForm();
    });

    $('#taskModal').on('show.bs.modal', function () {
        if (editId === 0) {
            $('#modalTitle').text('New Record');
        } else {
            $('#modalTitle').text('Update Record');
        }
    });

    function resetTaskForm() {
        $taskForm[0].reset();
        $taskForm.find('.is-invalid').removeClass('is-invalid');
        $taskForm.find('.invalid-feedback').remove();
        $('#clientSelect').val(null).trigger('change');
        $taskForm.find('select[name="clientSelect"]').prop("disabled", false);
        $('#incorpDate').val('');
        $('#first18Months').val('');
        $('#arDueDate').val('');
        $('#adDueDate').val('');
        editId = 0;
        dataRow = null;
    }
})
