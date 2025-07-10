$(function () {
    const selectedDocId = getQueryParam("docId");
    
    const documentTable = $("#documentTable").DataTable({
        columnDefs: [
            {
                targets: [0],
                visible: false,
            },
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
         initComplete: function () {
            if (selectedDocId) {
                const api = this.api();
                $('.dataTables_filter input[type="search"]').val(selectedDocId).trigger('input');
                api.search(`DocId-${selectedDocId}`).draw();
            }
        }
    }).buttons().container().appendTo('#documentTable_wrapper .col-md-6:eq(0)');

    $(document).on('click', '.btn-delete', function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = $(this).closest('table').DataTable().row($(this).closest('tr'))

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.documents}/${id}/delete`,
                method: "DELETE",
                success: function (res) {
                    if (res) {
                        Toast_Fire(ICON_SUCCESS, "Deleted", "Document deleted successfully.");
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