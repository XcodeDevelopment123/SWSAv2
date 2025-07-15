$(function () {
    $(document).on("click", '.btn-add-owner, .btn-add-comm, .btn-add-official, .btn-work-alloc', function () {
        const id = $("#clientId").val();
        if (!id) {
            Toast_Fire(ICON_INFO, "Select client to continue");
            return;
        }
        const tableId = $(this).data("table-id");
        scrollToTable(tableId);
    });

    function scrollToTable(tableId) {
        const $table = $(`#${tableId}`);
        if ($table.length === 0) return;

        const $label = $table.closest('.col-md-12').prevAll('.form-info-label').first();
        if ($label.length === 0) return;

        const top = $label.offset().top - 10;
        window.scrollTo({ top, behavior: 'smooth' });
    }

})