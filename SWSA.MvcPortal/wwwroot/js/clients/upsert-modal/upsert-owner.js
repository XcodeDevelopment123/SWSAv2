$(function () {
    const ownerDatatable = $("#ownerDatatable").DataTable();

    const $upsertOwnerForm = $("#upsertOwnerForm");
    const ownerFormInputs = {
        clientId: $("#clientId"),
        id: $upsertOwnerForm.find('input[name="Id"]'),
        name: $upsertOwnerForm.find('input[name="Name"]'),
        icOrPassport: $upsertOwnerForm.find('input[name="IcPassport"]'),
        position: $upsertOwnerForm.find('select[name="Position"]'),
        taxRef: $upsertOwnerForm.find('input[name="Tax"]'),
        phoneNumber: $upsertOwnerForm.find('input[name="Phone"]'),
        email: $upsertOwnerForm.find('input[name="Email"]'),
        isRequireSubmitFormBE: $upsertOwnerForm.find('input[name="RequireFormBE"]')
    };
    let owner_row = null;

    $upsertOwnerForm.validate({
        rules: {
            Name: {
                required: true,
                maxlength: 100
            },
            IcPassport: {
                required: true,
                maxlength: 20
            },
            Position: {
                required: true
            },
            Tax: {
                required: true,
                maxlength: 30
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
                required: "Name is required",
                maxlength: "Name must not exceed 100 characters"
            },
            IcPassport: {
                required: "IC/Passport is required",
                maxlength: "IC/Passport must not exceed 20 characters"
            },
            Position: {
                required: "Position is required"
            },
            Tax: {
                required: "Tax Reference Number is required",
                maxlength: "Tax Reference Number must not exceed 30 characters"
            },
            Phone: {
                required: "Telephone is required",
                minlength: "Telephone number must be at least 7 digits",
                maxlength: "Telephone number must not exceed 20 digits"
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

    $(document).on("click", "#owner_submitBtn", function (e) {
        const data = getFormData(ownerFormInputs);

        if (!$upsertOwnerForm.valid()) {
            return;
        }

        $.ajax({
            url: `${urls.client_cp_owner}`,
            method: "POST",
            data: {
                req: data
            },
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Success", "Owner saved successfully.");
                closeOwnerModal();
                if (owner_row !== null) {
                    owner_row.remove();
                    owner_row = null;
                }

                ownerDatatable.row.add({
                    DT_RowClass: "owner-item",
                    DT_RowAttr: {
                        'data-id': res.id
                    },
                    0: res.id,
                    1: res.namePerIC,
                    2: res.requiresFormBESubmission ? "Yes" : "No",
                    3: res.icOrPassportNumber,
                    4: res.position,
                    5: res.taxReferenceNumber,
                    6: res.phoneNumber,
                    7: res.email,
                    8: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-owner mr-2" data-id="${res.id}" data-name="${res.namePerIC}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-owner" data-id="${res.id}" data-name="${res.namePerIC}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
                ownerDatatable.draw();
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        })
    })

    $("#upsertOwnerModal").on("hide.bs.modal", function () {
        $upsertOwnerForm[0].reset();
    });

    $(document).on("click", ".btn-edit-owner", function () {
        const id = $(this).data("id");
        owner_row = ownerDatatable.row($(this).closest("tr"));
        $.ajax({
            url: `${urls.client_cp_owner}/${id}`,
            method: 'GET',
            success: function (res) {
                if (res) {
                    $("#owner_id").val(res.id);
                    $("#owner_name").val(res.namePerIC);
                    $("#owner_icPassport").val(res.icOrPassportNumber);
                    $("#owner_position").val(res.position).trigger("change");
                    $("#owner_taxRef").val(res.taxReferenceNumber);
                    $("#owner_requireFormBE").prop("checked", res.requiresFormBESubmission);
                    $("#owner_telephone").val(res.phoneNumber);
                    $("#owner_Email").val(res.email);
                    openOwnerModal();
                }
            }
        })
    })

    $(document).on("click", ".btn-delete-owner", function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = ownerDatatable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.client_cp_owner}/${id}`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Owner deleted successfully.");
                    row.remove().draw(false);
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })

        }
    })

    function openOwnerModal() {
        $("#upsertOwnerModal").modal("show");
    }
    function closeOwnerModal() {
        $("#upsertOwnerModal").modal("hide");
    }

})
