$(function () {

    initSelect2();

    flatpickr("#arDueDate,#arSubmittedDate,#arSentToClientDate,#arReturnedDate,#adSubmittedDate,#adSentToClientDate,#adReturnedDate", {
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

    $("#clientSelect").on("change", function () {
        const id = $(this).val();
        if (!id) return;
        $.ajax({
            method: "GET",
            manualLoading: true,
            url: `${urls.client}/${id}/simple-info`,
            success: function (res) {
                $("#companyType").val(res.clientType);
                $("#yearEndDate").val(res.yearEndMonth).trigger("change");
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


    //#region Task Form
    const $taskForm = $("#taskForm");
    const taskFormInputs = {
        clientId: $taskForm.find('select[name="clientSelect"]'),
        companyType: $taskForm.find('input[name="companyType"]'),
        yearEnd: $taskForm.find('input[name="yearEndDate"]'),
        arDueDate: $taskForm.find('input[name="arDueDate"]'),
        arSubmitDate: $taskForm.find('input[name="arSubmittedDate"]'),
        arSendToClientDate: $taskForm.find('input[name="arSentToClientDate"]'),
        arReturnByClientDate: $taskForm.find('input[name="arReturnedDate"]'),
        adSubmitDate: $taskForm.find('input[name="adSubmittedDate"]'),
        adSendToClientDate: $taskForm.find('input[name="adSentToClientDate"]'),
        adReturnByClientDate: $taskForm.find('input[name="adReturnedDate"]'),
        remarks: $taskForm.find('textarea[name="remarks"]')
    };
    let editId = 0;
    let dataRow = null;
    $taskForm.validate({
        rules: {
            clientSelect: {
                required: true
            }

        },
        messages: {
            clientSelect: {
                required: "Please select a client."
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

    $("#saveBtn").on("click", function () {
        if (!$taskForm.valid()) {
            return;
        }

        const taskData = getFormData(taskFormInputs);
        let url = `${urls.secretary_dept_template}`;

        if (editId > 0) {
            url += `/${editId}/update`
        } else {
            url += `/create`
        }

        $.ajax({
            url: url,
            method: "POST",
            data: {
                req: taskData
            },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Created", "Task created successfully.");

                    if (editId > 0) {
                        dataRow.remove();
                    }

                    taskDatatable.row.add([
                        res.client.fileNo,
                        res.client.name,
                        res.client.companyType,
                        res.client.yearEndMonth,
                        ConvertTimeFormat(res.arDueDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.arSubmitDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.arSentToClientDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.arReturnByClientDate, "YYYY-MM-DD"),
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
                    $taskForm.find('input[name="companyType"]').val(res.companyType);
                    $taskForm.find('input[name="yearEndDate"]').val(res.yearEnd);
                    $taskForm.find('input[name="arDueDate"]').val(ConvertTimeFormat(res.arDueDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="arSubmittedDate"]').val(ConvertTimeFormat(res.arSubmitDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="arSentToClientDate"]').val(ConvertTimeFormat(res.arSentToClientDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="arReturnedDate"]').val(ConvertTimeFormat(res.arReturnByClientDate, "YYYY-MM-DD"));
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
                    row.remove().draw(false);

                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })

        }

    })

    $('#taskModal').on('hide.bs.modal', function () {

        resetTaskForm();
    });

    function resetTaskForm() {
        $taskForm[0].reset();
        $taskForm.find('.is-invalid').removeClass('is-invalid');
        $taskForm.find('.invalid-feedback').remove();
        $('#clientSelect').val(null).trigger('change');
        $taskForm.find('select[name="clientSelect"]').prop("disabled", false);
        editId = 0;
        dataRow = null;
    }
})