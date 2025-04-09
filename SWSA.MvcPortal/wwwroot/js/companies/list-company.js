$(function () {
   const companyTable =  $("#companyDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#companyDatatable_wrapper .col-md-6:eq(0)');


    $(document).on('click', '.btn-delete', function () {
        const id = $(this).data('id');
        const name = $(this).data('name');

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            console.log(id);
            $.ajax({
                url: `${urls.companies}/${id}/delete`,
                method: "DELETE",
                success: function (res) {
                    const table = $(this).closest('table').DataTable();
                    table.row($(this).closest('tr')).remove().draw(false);
                    console.log(res);

                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })


        }
    });
})