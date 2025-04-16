$(function () {
    const userDatatable = $("#userDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        buttons: [
            {
                extend: "copy",
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: "csv",
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: "excel",
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: "pdf",
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            {
                extend: "print",
                exportOptions: {
                    columns: ':visible:not(:last-child)'
                }
            },
            "colvis" 
        ]
    }).buttons().container().appendTo('#companyDatatable_wrapper .col-md-6:eq(0)');

    $(document).on('click', '.btn-delete', function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = $(this).closest('table').DataTable().row($(this).closest('tr'))
        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.users}/${id}/delete`,
                method: "DELETE",
                success: function (res) {
                    if (res) {
                        Toast_Fire(ICON_SUCCESS, "Deleted", "User deleted successfully.");
                       
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