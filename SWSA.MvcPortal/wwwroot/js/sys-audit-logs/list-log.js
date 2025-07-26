$(function () {
    const logsDatatable = $("#logsDatatable").DataTable({
        order: [[0, 'desc']],
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
    });
    logsDatatable.buttons().container().appendTo('#logsDatatable_wrapper .col-md-6:eq(0)');

    flatpickr("#performedAt");

    var auditLogDatatable = $("#auditLogSummaryTable").DataTable({
        pageLength: 5,
        lengthChange: false
    });

    $(document).on("click", ".view-log-btn", function () {
        const id = $(this).data("id");

        $.ajax({
            url: `${urls.system_audit_log}/${id}`,
            method: "GET",
            success: function (res) {
                $("#auditLogModal").modal("show");
                $("#systemAuditLogForm").find("input[name='performedAt']").val(ConvertTimeFormat(res.performedAt,"YYYY-MM-DD hh:mm A"));
                $("#systemAuditLogForm").find("input[name='performedBy']").val(res.performedBy);
                $("#systemAuditLogForm").find("input[name='logId']").val(res.logId);
                $("#systemAuditLogForm").find("input[name='message']").val(res.message);
                $("#systemAuditLogForm").find("input[name='actionType']").val(res.actionType);

                auditLogDatatable.rows().clear().draw();
                $.each(res.changeSummaries, function (index, item) {
                    auditLogDatatable.row.add([item.fieldName, item.oldValue, item.newValue]).draw();
                })

            }
        })
    })
})
