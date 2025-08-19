$(function () {

    initSelect2();

    $("#yearToDo").yearpicker()
    $("#yearToDo").on('change', function () {
        $(this).valid();
    })

    const params = new URLSearchParams();
    params.append('type', 'SdnBhd');
    params.append('type', 'LLP');
    params.append('type', 'Enterprise');

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

            $("#doneByUserId").append(html);
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
                $("#companyNo").val(res.registrationNumber);
                $("#activitySize").val(res.activitySize);
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
        quarterToDoAudit: $taskForm.find('select[name="quarter"]'),
        activitySize: $taskForm.find('input[name="activitySize"]'),
        yearEndDate: $taskForm.find('input[name="yearEndDate"]'),
        yearToDo: $taskForm.find('input[name="yearToDo"]'),
        reasonForBacklog: $taskForm.find('textarea[name="reason"]')
    };

    let editId = 0;
    let dataRow = null;

    // Form validation
    $taskForm.validate({
        rules: {
            clientSelect: {
                required: true
            },
        },
        messages: {
            clientSelect: {
                required: "Please select a client."
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    // Save button click
    $("#saveBtn").on("click", function () {
        if (!$taskForm.valid()) {
            return;
        }

        const taskData = getFormData(taskFormInputs);
        let url = `${urls.backlog_aex}`;

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
                        Toast_Fire(ICON_SUCCESS, "Save", "Log saved successfully.");
                        dataRow.remove();
                    } else {
                        Toast_Fire(ICON_SUCCESS, "Create", "Log create successfully.");
                    }
                    taskDatatable.row.add([
                        res.client.group,
                        res.client.name,
                        `Q${res.quarterToDoAudit}`,
                        res.client.activitySize,
                        res.client.yearEndMonth,
                        res.yearToDo,
                        '',
                        '',
                        '',
                        res.reasonForBacklog,
                        `<button class="btn btn-sm btn-primary edit-task" data-id="${res.id}">Edit</button>
                     <button class="btn btn-sm btn-danger delete-task" data-id="${res.id}">Delete</button>`
                    ]).draw();

                    $('#taskModal').modal('hide');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error saving task:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to save log. Please try again.");
            }
        });
    });

    // Edit task
    $(document).on("click", ".edit-task", function () {
        const taskId = $(this).data("id");
        const row = taskDatatable.row($(this).closest("tr"));

        $.ajax({
            url: `${urls.backlog_aex}/${taskId}`,
            method: "GET",
            success: function (res) {
                if (res) {
                    editId = taskId;
                    dataRow = row;
                    taskFormInputs.clientId.prop("disabled", true).val(res.clientId).trigger('change');
                    taskFormInputs.quarterToDoAudit.val(res.quarterToDoAudit);
                    taskFormInputs.activitySize.val(res.activitySize);
                    taskFormInputs.yearEndDate.val(ConvertTimeFormat(res.yearEndDate, "YYYY-MM-DD"));
                    taskFormInputs.yearToDo.val(res.yearToDo).trigger("change");
                    taskFormInputs.reasonForBacklog.val(res.reasonForBacklog);
                    $('#taskModal').modal('show');
                }
            },
            error: function (xhr, status, error) {
                console.error('Error fetching task data:', error);
                Toast_Fire(ICON_ERROR, "Error", "Failed to fetch log data. Please try again.");
            }
        });
    });

    // Delete task
    $(document).on("click", ".delete-task", function () {
        const taskId = $(this).data("id");
        const row = taskDatatable.row($(this).closest("tr"));

        if (confirm("Are you sure you want to delete this log?")) {
            $.ajax({
                url: `${urls.backlog_aex}/${taskId}/delete`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Log deleted successfully.");
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
        editId = 0;
        dataRow = null;
    }
})