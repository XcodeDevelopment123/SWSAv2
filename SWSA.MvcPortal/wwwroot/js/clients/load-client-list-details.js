$(function () {

    $(document).on("click", ".cp-item", function () {
        const id = $(this).data("id");

        if ($("#clientId")?.val() == id) {
            Toast_Fire(ICON_INFO, "Client already selected");
            return
        };

        $.ajax({
            url: `${urls.client}/${id}`,
            method: "GET",
            success: function (res) {
                if (res) {
                    updateClientDetails(res);
                }
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Client not found", `Client Id with ${id} is not exist`);
            }
        })
    })

    const msicCodeTable = $("#msicCodeDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "columnDefs": [
            {
                targets: [1],
                orderable: false
            }
        ]
    });

    const workAllocationDatatable = $("#workAllocationDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });

    const ownerDatatable = $("#ownerDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });

    const officialCtDatatable = $("#officialCtDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });

    const communicationCtDatatable = $("#communicationCtDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });

    function updateClientDetails(data) {
        if (data.clientType !== "Individual") {
            $("#fileNo").val(data.fileNo);
            $("#companyName").val(data.name);
            $("#companyType").val(data.companyType);
            $("#companyNumber").val(data.registrationNumber);
            $("#incorpDate").val(ConvertTimeFormat(data.incorporationDate, "YYYY-MM-DD"));
            $("#eNumber").val(data.employerNumber);
            updateMsicCodeTable(data.msicCodes);
            updateOwnerTable(data.owners);
        } else {

        }

        $("#clientId").val(data.id);
        $("#grouping").val(data.group);
        $("#referral").val(data.referral);
        $("#yearEnd").val(formatMonthLabel(data.yearEndMonth));
        $("#tinNumber").val(data.taxIdentificationNumber);
        updateWorkAllocTable(data.workAllocations);
        updateOfficialTable(data.officialContacts);
        updateCommTable(data.communicationContacts);

        openClientDetails();
    }

    function updateMsicCodeTable(data) {

        msicCodeTable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                console.log(item);
                msicCodeTable.add([
                    item.code,
                    item.descriptions
                ])
            })
        }
        msicCodeTable.draw();
    }

    function updateWorkAllocTable(data) {
        workAllocationDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                console.log(item);
                workAllocationDatatable.add([

                ])
            })
        }
        workAllocationDatatable.draw();
    }

    function updateOwnerTable(data) {
        ownerDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                console.log(item);
                ownerDatatable.add([

                ])
            })
        }
        ownerDatatable.draw();
    }

    function updateOfficialTable(data) {
        officialCtDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                console.log(item);
                officialCtDatatable.add([

                ])
            })
        }
        officialCtDatatable.draw();
    }

    function updateCommTable(data) {
        communicationCtDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                console.log(item);
                communicationCtDatatable.add([

                ])
            })
        }
        communicationCtDatatable.draw();
    }

    function openClientDetails() {
        const card = $(".detail-header").parent();
        if (card && card.hasClass("collapsed-card")) {
            const toggleBtn = card.find(".btn-tool");
            toggleBtn.trigger("click");
        }
    }



})