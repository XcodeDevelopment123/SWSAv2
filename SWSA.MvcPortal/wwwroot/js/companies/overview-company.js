$(function () {
    //#region Table

    const ownerTable = $('#ownerTable').DataTable({
        select: {
            style: 'single',
            selector: 'td .btn-edit-owner'
        },
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    const officialContactTable = $('#officialContactTable').DataTable({
        select: {
            style: 'single',
            selector: 'td .btn-edit-official-ct'
        },
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    const communicationContactTable = $('#communicationContactTable').DataTable({
        select: {
            style: 'single',
            selector: 'td .btn-edit-staff-ct'
        },
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    const workDatatable = $('#workDatatable').DataTable({
        select: {
            style: 'single',
            selector: 'td .btn-edit-staff-ct'
        },
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });
    //#endregion

})
