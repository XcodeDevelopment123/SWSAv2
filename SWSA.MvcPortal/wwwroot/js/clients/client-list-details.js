
$(function () {
    const clientType = $("#clientType").val();

    $(document).on("click", '.btn-add-owner, .btn-add-comm, .btn-add-official, .btn-work-alloc', function () {
        const id = $("#clientId").val();
        if (!id) {
            Toast_Fire(ICON_INFO, "Select client to continue");
            return;
        }
        const tableId = $(this).data("table-id");
        scrollToTable(tableId);

        if ($(this).hasClass("btn-add-comm")) {
            $("#upsertCommModal").modal("show");
        } else if ($(this).hasClass("btn-add-official")) {
            $("#upsertOfficialModal").modal("show");
        }
        else if ($(this).hasClass("btn-add-owner")) {
            $("#upsertOwnerModal").modal("show");
        }
        else if ($(this).hasClass("btn-work-alloc")) {
            $("#upsertWorkAllocModal").modal("show");
        }
    });

    $(document).on("click", ".btn-nav-edit", function () {
        const id = $("#clientId").val();
        if (!id) {
            Toast_Fire(ICON_INFO, "Select client to continue");
            return;
        }
        window.location.href = `${urls.edit_client}/${toCleanLower(clientType)}/${id}`
    })

    $(document).on("click", ".modal-close", function () {
        const modalId = $(this).data("modal-id");
        $("#" + modalId).modal("hide");
    })

    function scrollToTable(tableId) {
        const $table = $(`#${tableId}`);
        if ($table.length === 0) return;

        const $label = $table.closest('.col-md-12').prevAll('.form-info-label').first();
        if ($label.length === 0) return;

        const top = $label.offset().top - 10;
        window.scrollTo({ top, behavior: 'smooth' });
    }
})