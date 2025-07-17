$(function () {
    const officialCtDatatable = $("#officialCtDatatable").DataTable();
    const $upsertOfficialForm = $("#upsertOfficialForm");

    const officialFormInputs = {
        clientId: $("#clientId"),
        id: $upsertOfficialForm.find('input[name="Id"]'),
        address: $upsertOfficialForm.find('textarea[name="Address"]'),
        phone: $upsertOfficialForm.find('input[name="Phone"]'),
        email: $upsertOfficialForm.find('input[name="Email"]'),
        remark: $upsertOfficialForm.find('textarea[name="Remark"]')
    };
    let official_row = null;

    $upsertOfficialForm.validate({
        rules: {
            Address: {
                required: true,
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
            Address: {
                required: "Address is required",
            },
            Phone: {
                required: "Official telephone is required",
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

    $(document).on("click", "#official_submitBtn", function (e) {
        const data = getFormData(officialFormInputs);

        if (!$upsertOfficialForm.valid()) {
            return;
        }

        $.ajax({
            url: `${urls.client_official}`,
            method: "POST",
            data: {
                req: data
            },
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Success", "Contact saved successfully.");
                closeOfficialModal();
                if (official_row !== null) {
                    official_row.remove();
                    official_row = null;
                }

                officialCtDatatable.row.add({
                    DT_RowClass: "official-item",
                    DT_RowAttr: {
                        'data-id': res.id
                    },
                    0: res.id,
                    1: res.address,
                    2: res.officeTel,
                    3: res.email,
                    4: res.remark,
                    5: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-official-ct mr-2" data-id="${res.id}" data-name="${res.officeTel}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-official-ct" data-id="${res.id}" data-name="${res.officeTel}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
                officialCtDatatable.draw();
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        })



    })

    $("#upsertOfficialModal").on("hide.bs.modal", function () {
        $upsertOfficialForm[0].reset();
    });

    $(document).on("click", ".btn-edit-official-ct", function () {
        const id = $(this).data("id");
        official_row = officialCtDatatable.row($(this).closest("tr"));
        $.ajax({
            url: `${urls.client_official}/${id}`,
            method: 'GET',
            success: function (res) {
                if (res) {
                    $("#official_id").val(res.id);
                    $("#official_address").val(res.address);
                    $("#official_telephone").val(res.officeTel);
                    $("#official_Email").val(res.email);
                    $("#official_remark").val(res.remark);
                    openOfficialModal();
                }
            }
        })
    })

    $(document).on("click", ".btn-delete-official-ct", function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = officialCtDatatable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.client_official}/${id}`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Official Contact deleted successfully.");
                    row.remove().draw(false);
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })

        }
    })

    function openOfficialModal() {
        $("#upsertOfficialModal").modal("show");
    }
    function closeOfficialModal() {
        $("#upsertOfficialModal").modal("hide");
    }

})
