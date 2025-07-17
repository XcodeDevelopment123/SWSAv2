$(function () {
    const workAllocationDatatable = $("#workAllocationDatatable").DataTable();
    const clientType = $("#clientType").val();
    const $upsertWorkAllocForm = $("#upsertWorkAllocForm");
    const workAllocInputs = {
        clientId: $("#clientId"),
        id: $upsertWorkAllocForm.find('input[name="Id"]'),
        service: $upsertWorkAllocForm.find('select[name="Service"]'),
        activitySize: $upsertWorkAllocForm.find('select[name="ActivitySize"]'),
        auditCpStatus: $upsertWorkAllocForm.find('select[name="AuditCompanyStatus"]'),
        auditStatus: $upsertWorkAllocForm.find('select[name="AuditStatus"]'),
        remarks: $upsertWorkAllocForm.find('textarea[name="Remark"]'),
    };
    let workAlloc_row = null;

    $upsertWorkAllocForm.validate({
        rules: {
            Service: {
                required: true
            },
            ActivitySize: {
                required: {
                    depends: function () {
                        return clientType !== "Individual"
                    }
                }
            },
            AuditCompanyStatus: {
                required: {
                    depends: function () {
                        return clientType === "SdnBhd"
                    }
                }
            },
            AuditStatus: {
                required: {
                    depends: function () {
                        return clientType === "SdnBhd"
                    }
                }
            }
        },
        messages: {
            Service: {
                required: "Service scope is required"
            },
            ActivitySize: {
                required: "Activity / Co Size is required for non-individual clients"
            },
            AuditCompanyStatus: {
                required: "Company status is required for Sdn Bhd"
            },
            AuditStatus: {
                required: "Audit status is required for Sdn Bhd"
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

    $(document).on("click", "#work_alloc_submitBtn", function (e) {
        const data = getFormData(workAllocInputs);

        if (!$upsertWorkAllocForm.valid()) {
            return;
        }

        $.ajax({
            url: `${urls.client_work_alloc}`,
            method: "POST",
            data: {
                req: data
            },
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Success", "Work allocation saved successfully.");
                closeWorkAllocModal();
                if (workAlloc_row !== null) {
                    workAlloc_row.remove();
                    workAlloc_row = null;
                }

                workAllocationDatatable.row.add({
                    DT_RowClass: "work-alloc-item",
                    DT_RowAttr: {
                        'data-id': res.id
                    },
                    0: res.serviceScope,
                    1: res.companyStatus,
                    2: res.auditStatus,
                    3: res.companyActivityLevel,
                    4: res.remarks,
                    5: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-work-alloc mr-2" data-id="${res.id}" data-name="${res.serviceScope}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-work-alloc" data-id="${res.id}" data-name="${res.serviceScope}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
                workAllocationDatatable.draw();
            },
            error: function (jqxhr) {
                jqxhr.handledError = true;
                const res = jqxhr.responseJSON;
                const message = res?.message || "Something went wrong";
                const error = res?.error || jqxhr.statusText || "Unknown error";

                console.error("AJAX Error:", res);
                Toast_Fire(ICON_ERROR, message, error);
            }
        })
    })

    $("#upsertWorkAllocModal").on("hide.bs.modal", function () {
        $upsertWorkAllocForm[0].reset();
        $("#work_alloc_service,#work_alloc_activity,#work_alloc_cp_status,#work_alloc_audit_status").val("").trigger("change")
    });

    $(document).on("click", ".btn-edit-work-alloc", function () {
        const id = $(this).data("id");
        workAlloc_row = workAllocationDatatable.row($(this).closest("tr"));
        $.ajax({
            url: `${urls.client_work_alloc}/${id}`,
            method: 'GET',
            success: function (res) {
                if (res) {
                    $("#work_alloc_id").val(res.id);
                    selectByDisplayText("work_alloc_service", res.serviceScope);
                    selectByDisplayText("work_alloc_activity", res.companyActivityLevel);
                    selectByDisplayText("work_alloc_cp_status", res.companyStatus);
                    selectByDisplayText("work_alloc_audit_status", res.auditStatus);
                    $("#work_alloc_remark").val(res.remark);
                    openWorkAllocModal();
                }
            }
        })
    })

    $(document).on("click", ".btn-delete-work-alloc", function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = workAllocationDatatable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.client_work_alloc}/${id}`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Work allocation deleted successfully.");
                    row.remove().draw(false);
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })

        }
    })


    function openWorkAllocModal() {
        $("#upsertWorkAllocModal").modal("show");
    }
    function closeWorkAllocModal() {
        $("#upsertWorkAllocModal").modal("hide");
    }
})