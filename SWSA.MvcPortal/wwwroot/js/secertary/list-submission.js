$(function () {
    //#region init table and date
    const companyDatatable = $("#companyDatatable").DataTable({
        columnDefs: [
            { targets: 0, orderable: false, searchable: false }
        ],
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": true,
        "responsive": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    })
    companyDatatable.buttons().container().appendTo('#companyDatatable_wrapper .col-md-6:eq(0)');

    //#endregion init table and date

    tableResizeEventListener();
})