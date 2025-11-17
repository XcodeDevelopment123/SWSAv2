    $(function () {

        initSelect2();

        flatpickr("#startDate,#completeDate,#ssmPenaltiesAppealDate,#ssmPenaltiesPaymentDate,#ssmDocSentDate,#ssmSubmissionDate", {
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
            companyNo: $taskForm.find('input[name="companyNo"]'),
            incorpDate: $taskForm.find('input[name="incorpDate"]'),
            yearEndDate: $taskForm.find('input[name="yearEndDate"]'),
            startDate: $taskForm.find('input[name="startDate"]'),
            completeDate: $taskForm.find('input[name="completeDate"]'),
            doneByUserId: $taskForm.find('select[name="doneByUserId"]'),
            penaltiesAmount: $taskForm.find('input[name="penaltiesAmount"]'),
            revisedPenaltiesAmount: $taskForm.find('input[name="revisedPenaltiesAmount"]'),
            ssmPenaltiesAppealDate: $taskForm.find('input[name="ssmPenaltiesAppealDate"]'),
            ssmPenaltiesPaymentDate: $taskForm.find('input[name="ssmPenaltiesPaymentDate"]'),
            ssmDocSentDate: $taskForm.find('input[name="ssmDocSentDate"]'),
            ssmSubmissionDate: $taskForm.find('input[name="ssmSubmissionDate"]'),
            remarks: $taskForm.find('textarea[name="remarks"]')
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
            let url = `${urls.secretary_dept_strike_off}`;

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
                            taskData.companyNo,
                            taskData.incorpDate,
                            taskData.yearEndDate,

                            ConvertTimeFormat(res.startDate, "YYYY-MM-DD"),
                            ConvertTimeFormat(res.completeDate, "YYYY-MM-DD"),
                            res.doneByUser?.fullName || '',
                            ConvertTimeFormat(res.completeDate, "YYYY-MM-DD"),
                            res.penaltiesAmount || '',
                            res.revisedPenaltiesAmount || '',
                            ConvertTimeFormat(res.ssmPenaltiesAppealDate, "YYYY-MM-DD"),
                            ConvertTimeFormat(res.ssmPenaltiesPaymentDate, "YYYY-MM-DD"),
                            ConvertTimeFormat(res.ssmDocSentDate, "YYYY-MM-DD"),
                            ConvertTimeFormat(res.ssmSubmissionDate, "YYYY-MM-DD"),
                            res.remarks,
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
                url: `${urls.secretary_dept_strike_off}/${taskId}`,
                method: "GET",
                success: function (res) {
                    if (res) {
                        editId = taskId;
                        dataRow = row;
                        $taskForm.find('select[name="clientSelect"]').prop("disabled", true).val(res.clientId).trigger('change');
                        $taskForm.find('input[name="companyNo"]').val(res.companyNo);
                        $taskForm.find('input[name="incorpDate"]').val(ConvertTimeFormat(res.incorpDate, "YYYY-MM-DD"));
                        $taskForm.find('input[name="yearEndDate"]').val(ConvertTimeFormat(res.yearEndDate, "YYYY-MM-DD"));
                        $taskForm.find('input[name="startDate"]').val(ConvertTimeFormat(res.startDate, "YYYY-MM-DD"));
                        $taskForm.find('input[name="completeDate"]').val(ConvertTimeFormat(res.completeDate, "YYYY-MM-DD"));
                        $taskForm.find('select[name="doneByUserId"]').val(res.doneByUserId).trigger('change');
                        $taskForm.find('input[name="penaltiesAmount"]').val(res.penaltiesAmount);
                        $taskForm.find('input[name="revisedPenaltiesAmount"]').val(res.revisedPenaltiesAmount);
                        $taskForm.find('input[name="ssmPenaltiesAppealDate"]').val(ConvertTimeFormat(res.ssmPenaltiesAppealDate, "YYYY-MM-DD"));
                        $taskForm.find('input[name="ssmPenaltiesPaymentDate"]').val(ConvertTimeFormat(res.ssmPenaltiesPaymentDate, "YYYY-MM-DD"));
                        $taskForm.find('input[name="ssmDocSentDate"]').val(ConvertTimeFormat(res.ssmDocSentDate, "YYYY-MM-DD"));
                        $taskForm.find('input[name="ssmSubmissionDate"]').val(ConvertTimeFormat(res.ssmSubmissionDate, "YYYY-MM-DD"));
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

        // Delete task
        $(document).on("click", ".delete-task", function () {
            const taskId = $(this).data("id");
            const row = taskDatatable.row($(this).closest("tr"));

            if (confirm("Are you sure you want to delete this task?")) {
                $.ajax({
                    url: `${urls.secretary_dept_strike_off}/${taskId}/delete`,
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
            $('#doneByUserId').val(null).trigger('change');
            $taskForm.find('select[name="clientSelect"]').prop("disabled", false);
            editId = 0;
            dataRow = null;
        }
    })