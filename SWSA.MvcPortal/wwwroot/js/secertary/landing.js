$(function () {
    let currentYear = new Date().getFullYear();

    return
    const companyTable = $("#companyDatatable").DataTable({
        ajax: {
            url: `${urls.companies}/companies?year=${currentYear}`,
            type: "GET",
            dataSrc: '',
            complete: function (jqXHR, textStatus) {
                const data = jqXHR.responseJSON;
                stopLoading();
            }
        },
        columns: [
            {
                data: "companyName",
                render: function (data, type, row) {
                    return `${data} (${row.registrationNumber})`;
                }
            },           // Name
            { data: "companyType" },           // Type
            {
                data: "yearEndMonth",
                render: function (data, type, row) {
                    return formatMonthLabel(data);
                }
            },          // Year End

            {
                data: "dateOfAnnualReturn",
                render: function (data, type, row) {
                    return ConvertTimeFormat(data, "DD-MM-YYYY");
                }
            },    // Date of Annual Return
            {
                data: "dateOfARDue",
                render: function (data, type, row) {
                    return ConvertTimeFormat(data, "DD-MM-YYYY");
                }
            },           // Date of AR Due
            {
                data: "dateOfARSubmitted",
                render: function (data, type, row) {
                    return ConvertTimeFormat(data, "DD-MM-YYYY");
                }
            },     // Date AR Submitted
            {
                data: "dateOfAccSubmitted",
                render: function (data, type, row) {
                    return ConvertTimeFormat(data, "DD-MM-YYYY");
                }
            },    // Date of Acc Submitted

            {
                data: "dateSentToClient",
                render: function (data, type, row) {
                    return ConvertTimeFormat(data, "DD-MM-YYYY");
                }
            },      // Date send to Client
            {
                data: "dateReturnedByClient",
                render: function (data, type, row) {
                    return ConvertTimeFormat(data, "DD-MM-YYYY");
                }
            },   // Date Returned
            { data: "remarks" }   // Remark
        ],

        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
    })

    $('#yearSelect').on('change', function () {
        const selectedYear = $(this).val();
        const newUrl = `${urls.secretary_dept}/landing/companies?year=${selectedYear}`;

        companyTable.ajax.url(newUrl).load();
    });

    $(`<div class="form-group d-flex align-items-center gap-2">
    <label for="yearpicker" class="mb-0">Year:</label>
    <input id="yearpicker" class="form-control" />
    </div>`)
        .appendTo('#companyDatatable_wrapper .col-md-6:eq(0)');

    $("#yearpicker").yearpicker({
        startYear: 2020,
        endYear: currentYear + 1,
        year: currentYear
    });

    $('#yearpicker').on('change', function () {
        const selectedYear = $(this).val();
        const newUrl = `${urls.secretary_dept}/landing/companies?year=${selectedYear}`;
        companyTable.ajax.url(newUrl).load();
    });
})