
$(function () {
    const communicationCtDatatable = $("#communicationCtDatatable").DataTable();

    const $upsertCommForm = $("#upsertCommForm");
    const commformInputs = {
        clientId: $("#clientId"),
        id: $upsertCommForm.find('input[name="Id"]'),
        name: $upsertCommForm.find('input[name="Name"]'),
        position: $upsertCommForm.find('select[name="Position"]'),
        phone: $upsertCommForm.find('input[name="Phone"]'),
        email: $upsertCommForm.find('input[name="Email"]'),
        remark: $upsertCommForm.find('textarea[name="Remark"]'),
    };
    let comm_row = null;

    $upsertCommForm.validate({
        rules: {
            Name: {
                required: true,
                maxlength: 100
            },
            Position: {
                required: true
            },
            Phone: {
                required: true,
                minlength: 7,
                maxlength: 20
            },
            Email: {
                required: true,
                email: true,
                maxlength: 100
            }
        },
        messages: {
            Name: {
                required: "Contact Name is required",
                maxlength: "Contact Name must not exceed 100 characters"
            },
            Position: {
                required: "Please select a Position"
            },
            Phone: {
                required: "Whatsapp number is required",
                minlength: "Phone number must be at least 7 digits",
                maxlength: "Phone number must not exceed 20 digits"
            },
            Email: {
                required: "Email is required",
                email: "Please enter a valid email address",
                maxlength: "Email must not exceed 100 characters"
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

    $(document).on("click", "#comm_submitBtn", function (e) {
        const data = getFormData(commformInputs);

        if (!$upsertCommForm.valid()) {
            return;
        }

        $.ajax({
            url: `${urls.client_comm}`,
            method: "POST",
            data: {
                req: data
            },
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Success", "Contact saved successfully.");
                closeCommModal();
                if (comm_row !== null) {
                    comm_row.remove();
                    comm_row = null;
                }

                communicationCtDatatable.row.add({
                    DT_RowClass: "comm-item",
                    DT_RowAttr: {
                        'data-id': res.id
                    },
                    0: res.id,
                    1: res.contactName,
                    2: res.position,
                    3: res.whatsApp,
                    4: res.email,
                    5: res.remark,
                    6: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-communication-ct mr-2" data-id="${res.id}" data-name="${res.contactName}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-communication-ct" data-id="${res.id}" data-name="${res.contactName}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
                communicationCtDatatable.draw();
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        })



    })

    $("#upsertCommModal").on("hide.bs.modal", function () {
        $upsertCommForm[0].reset();
    });

    $(document).on("click", ".btn-edit-communication-ct", function () {
        const id = $(this).data("id");
        comm_row = communicationCtDatatable.row($(this).closest("tr"));
        $.ajax({
            url: `${urls.client_comm}/${id}`,
            method: 'GET',
            success: function (res) {
                if (res) {
                    $("#comm_id").val(res.id);
                    $("#comm_contactName").val(res.contactName);
                    $("#comm_position").val(res.position).trigger("change");
                    $("#comm_whatsapp").val(res.whatsApp);
                    $("#comm_Email").val(res.email);
                    $("#comm_remark").val(res.remark);
                    openCommModal();
                }
            }
        })
    })

    $(document).on("click", ".btn-delete-communication-ct", function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = communicationCtDatatable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.client_comm}/${id}`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Communication Contact deleted successfully.");
                    row.remove().draw(false);
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })

        }
    })

    function openCommModal() {
        $("#upsertCommModal").modal("show");
    }
    function closeCommModal() {
        $("#upsertCommModal").modal("hide");
    }

})
