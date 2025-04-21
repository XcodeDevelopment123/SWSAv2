$(function () {
    const scheduledTaskDatatable = $("#scheduledTaskDatatable").DataTable({
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
    }).buttons().container().appendTo('#scheduledTaskDatatable_wrapper .col-md-6:eq(0)');
})