$(function () {
    //#region init table and date
    const companyDatatable = $("#companyDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    })
    companyDatatable.buttons().container().appendTo('#companyDatatable_wrapper .col-md-6:eq(0)');

    //#endregion init table and date

    $(document).on('click', '.btn-delete', function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = $(this).closest('table').DataTable().row($(this).closest('tr'))

        if (confirm(`Are you sure you want to delete strike off submission for "${name}"?`)) {
            $.ajax({
                url: `${urls.secretary_dept_submission}/company-strike-off/${id}`,
                method: "DELETE",
                success: function (res) {
                    if (res) {
                        Toast_Fire(ICON_SUCCESS, "Deleted", "Submission deleted successfully.");
                        row.remove().draw(false);
                    }
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })


        }
    });
})