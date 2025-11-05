$(function () {
    let queryId = getQueryParam("id");
    if (queryId) {
        loadClientData(queryId);
    }

    $(document).on("click", ".cp-item", function () {
        const id = $(this).data("id");
        loadClientData(id);
    });

    function loadClientData(id) {
        if ($("#clientId")?.val() == id) {
            Toast_Fire(ICON_INFO, "Client already selected");
            return;
        }

        $.ajax({
            url: `${urls.client}/${id}`,
            method: "GET",
            success: function (res) {
                console.log(res);
                if (res) {
                    updateClientDetails(res);
                }
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Client not found", `Client Id with ${id} is not exist`);
            }
        });
    }

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
        order: [[0, 'asc']],

    });

    const ownerDatatable = $("#ownerDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "pageLength": 5,
        order: [[0, 'desc']],
        "columnDefs": [
            {
                targets: 0,
                visible: false,
            }
        ]
    });

    const officialCtDatatable = $("#officialCtDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "pageLength": 5,
        order: [[0, 'desc']],
        "columnDefs": [
            {
                targets: 0,
                visible: false,
            }
        ]
    });

    const communicationCtDatatable = $("#communicationCtDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "pageLength": 5,
        order: [[0, 'desc']],
        "columnDefs": [
            {
                targets: 0,
                visible: false,
            }
        ]
    });

    function updateClientDetails(data) {
        if (data.clientType !== "Individual") {
            $("#fileNo").val(data.fileNo);
            $("#companyName").val(data.name);
            $("#companyType").val(data.companyType);
            $("#companyNumber").val(data.registrationNumber);
            $("#incorpDate").val(ConvertTimeFormat(data.incorporationDate, "YYYY-MM-DD"));
            $("#eNumber").val(data.employerNumber);
            $("#activitySize").val(data.activitySize);
            updateMsicCodeTable(data.msicCodes);
            updateOwnerTable(data.owners);
        } else {
            $("#individualName").val(data.name);
            $("#icOrPassport").val(data.icOrPassportNumber);
            $("#profession").val(data.profession);
        }

        $("#clientId").val(data.id);
        $("#grouping").val(data.group);
        $("#referral").val(data.referral);
        $("#yearEnd").val(formatMonthLabel(data.yearEndMonth));
        $("#tinNumber").val(data.taxIdentificationNumber);
   //     updateWorkAllocTable(data.workAllocations);
        updateOfficialTable(data.officialContacts);
        updateCommTable(data.communicationContacts);

        openClientDetails();
    }

    function updateMsicCodeTable(data) {

        msicCodeTable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                const code = item.msicCode;
                msicCodeTable.row.add([
                    code.code,
                    code.description
                ])
            })
        }
        msicCodeTable.draw();
    }

    function updateWorkAllocTable(data) {
        workAllocationDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                workAllocationDatatable.row.add({
                    DT_RowClass: "work-alloc-item",
                    DT_RowAttr: {
                        'data-id': item.id
                    },
                    0: item.serviceScope,
                    1: item.companyStatus,
                    2: item.auditStatus,
                    3: item.companyActivityLevel,
                    4: item.remarks,
                    5: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-work-alloc mr-2" data-id="${item.id}" data-name="${item.serviceScope}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-work-alloc" data-id="${item.id}" data-name="${item.serviceScope}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
            })
        }
        workAllocationDatatable.draw();
    }

    function updateOwnerTable(data) {
        ownerDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                ownerDatatable.row.add({
                    DT_RowClass: "owner-item",
                    DT_RowAttr: {
                        'data-id': item.id
                    },
                    0: item.id,
                    1: item.namePerIC,
                    2: item.requiresFormBESubmission ? "Yes" : "No",
                    3: item.icOrPassportNumber,
                    4: item.position,
                    5: item.taxReferenceNumber,
                    6: item.phoneNumber,
                    7: item.email,
                    8: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-owner mr-2" data-id="${item.id}" data-name="${item.namePerIC}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-owner" data-id="${item.id}" data-name="${item.namePerIC}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
            })
        }
        ownerDatatable.draw();
    }

    function updateOfficialTable(data) {
        officialCtDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                officialCtDatatable.row.add({
                    DT_RowClass: "official-item",
                    DT_RowAttr: {
                        'data-id': item.id
                    },
                    0: item.id,
                    1: item.address,
                    2: item.officeTel,
                    3: item.email,
                    4: item.remark,
                    5: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-official-ct mr-2" data-id="${item.id}" data-name="${item.officeTel}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-official-ct" data-id="${item.id}" data-name="${item.officeTel}"><i class="fa fa-trash"></i></button>
					</div>
                    `
                })
            })
        }
        officialCtDatatable.draw();
    }

    function updateCommTable(data) {
        communicationCtDatatable.clear();
        if (data.length > 0) {
            $.each(data, function (i, item) {
                communicationCtDatatable.row.add({
                    DT_RowClass: "comm-item",
                    DT_RowAttr: {
                        'data-id': item.id
                    },
                    0: item.id,
                    1: item.contactName,
                    2: item.position,
                    3: item.whatsApp,
                    4: item.email,
                    5: item.remark,
                    6: `
                    <div class="btn-group" role="group">
						<button type="button" class="btn btn-sm btn-warning btn-edit-communication-ct mr-2" data-id="${item.id}" data-name="${item.contactName}" title="Edit">
							<i class="fa fa-edit"></i>
						</button>
						<button type="button" class="btn btn-sm btn-danger btn-delete-communication-ct" data-id="${item.id}" data-name="${item.contactName}"><i class="fa fa-trash"></i></button>
					</div>`
                })
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
    initSelect2();

})