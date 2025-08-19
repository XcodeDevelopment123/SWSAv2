$(function () {

    initSelect2();
    // Initialize flatpickr for date inputs
    flatpickr("#penaltiesAppealDate,#penaltiesPaymentDate,#formESubmitDate,#formCSubmitDate,#invoiceDate", {
        allowInput: true
    });

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
                $("#incorpDate").val(ConvertTimeFormat(res.incorporationDate, "YYYY-MM-DD"));
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
        yearEndDate: $taskForm.find('input[name="yearEndDate"]'),
        irbPenaltiesAmount: $taskForm.find('input[name="irbPenaltiesAmount"]'),
        penaltiesAppealDate: $taskForm.find('input[name="penaltiesAppealDate"]'),
        penaltiesPaymentDate: $taskForm.find('input[name="penaltiesPaymentDate"]'),
        remarks: $taskForm.find('textarea[name="remarks"]'),
        isAccountWorkComplete: $taskForm.find('input[name="isAccountWorkComplete"]'),
        formESubmitDate: $taskForm.find('input[name="formESubmitDate"]'),
        formCSubmitDate: $taskForm.find('input[name="formCSubmitDate"]'),
        invoiceDate: $taskForm.find('input[name="invoiceDate"]'),
        invoiceAmount: $taskForm.find('input[name="invoiceAmount"]'),
        isClientCopySent: $taskForm.find('input[name="isClientCopySent"]')
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
        let url = `${urls.tax_strike_off}`;

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
                        Toast_Fire(ICON_SUCCESS, "Create", "Task create successfully.");
                    }
                    taskDatatable.row.add([
                        res.client.referral,
                        res.client.name,
                        res.client.yearEndMonth || '',
                        res.irbPenaltiesAmount || '',
                        ConvertTimeFormat(res.penaltiesAppealDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.penaltiesPaymentDate, "YYYY-MM-DD"),
                        res.remarks || '',
                        res.isAccountWorkComplete ? "✓" : "",
                        ConvertTimeFormat(res.formESubmitDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.formCSubmitDate, "YYYY-MM-DD"),
                        ConvertTimeFormat(res.invoiceDate, "YYYY-MM-DD"),
                        res.invoiceAmount || '',
                        res.isClientCopySent ? "✓" : "",
                        `<button class="btn btn-sm btn-primary edit-task" data-id="${res.id}">Edit</button>
                     <button class="btn btn-sm btn-danger delete-task" data-id="${res.id}">Delete</button>`
                    ]).draw();

                    $('#taskModal').modal('hide');
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
            url: `${urls.tax_strike_off}/${taskId}`,
            method: "GET",
            success: function (res) {
                if (res) {
                    editId = taskId;
                    dataRow = row;
                    $taskForm.find('select[name="clientSelect"]').prop("disabled", true).val(res.clientId).trigger('change');
                    $taskForm.find('input[name="yearEndDate"]').val(ConvertTimeFormat(res.yearEndDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="irbPenaltiesAmount"]').val(res.irbPenaltiesAmount);
                    $taskForm.find('input[name="penaltiesAppealDate"]').val(ConvertTimeFormat(res.penaltiesAppealDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="penaltiesPaymentDate"]').val(ConvertTimeFormat(res.penaltiesPaymentDate, "YYYY-MM-DD"));
                    $taskForm.find('textarea[name="remarks"]').val(res.remarks);
                    $taskForm.find('input[name="isAccountWorkComplete"]').prop('checked', res.isAccountWorkComplete);
                    $taskForm.find('input[name="formESubmitDate"]').val(ConvertTimeFormat(res.formESubmitDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="formCSubmitDate"]').val(ConvertTimeFormat(res.formCSubmitDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="invoiceDate"]').val(ConvertTimeFormat(res.invoiceDate, "YYYY-MM-DD"));
                    $taskForm.find('input[name="invoiceAmount"]').val(res.invoiceAmount);
                    $taskForm.find('input[name="isClientCopySent"]').prop('checked', res.isClientCopySent);
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
                url: `${urls.tax_strike_off}/${taskId}/delete`,
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
    //#endregion
})