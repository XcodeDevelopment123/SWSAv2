$(function () {
    let queryId = getQueryParam("id");
    if (queryId && !isNaN(queryId) && parseInt(queryId) > 0) {
        loadClientData(parseInt(queryId));
    }

    $(document).on("click", ".cp-item", function () {
        const id = $(this).data("id");
        loadClientData(id);
    });

    function loadClientData(id) {
        if ($("#clientId")?.val() == id) {
            const loadedName = $("#detailCompanyName").val() || $("#individualName").val();
            if (loadedName && loadedName.trim() !== "") {
                Toast_Fire(ICON_INFO, "Client already selected");
                $(".cp-item").removeClass("table-primary active");
                $(`.cp-item[data-id="${id}"]`).addClass("table-primary active");
                return;
            }
        }

        $(".cp-item").removeClass("table-primary active");
        $(`.cp-item[data-id="${id}"]`).addClass("table-primary active");

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
        "columnDefs": [{
            targets: [1],
            orderable: false
        }]
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
        "columnDefs": [{ targets: 0, visible: false }]
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
        "columnDefs": [{ targets: 0, visible: false }]
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
        "columnDefs": [{ targets: 0, visible: false }]
    });

    // Load options for detail panel dropdowns
    function loadDetailOptions(clientType) {
        $.ajax({
            url: `${urls.client}/options`,
            method: "POST",
            data: {
                req: {
                    clientType: clientType,
                    includeGroups: true,
                    includeReferrals: true
                }
            },
            success: function (res) {
                if (!res) return;
                if (res.groups && res.groups.length > 0) {
                    const $sel = $('#detailGroup');
                    $sel.find('option:not(:first)').remove();
                    $.each(res.groups, function (i, item) {
                        if (item && item.trim() !== "")
                            $sel.append(`<option value="${item}">${item}</option>`);
                    });
                    $sel.trigger('change');
                }
                if (res.referrals && res.referrals.length > 0) {
                    const $sel = $('#detailReferral');
                    $sel.find('option:not(:first)').remove();
                    $.each(res.referrals, function (i, item) {
                        if (item && item.trim() !== "")
                            $sel.append(`<option value="${item}">${item}</option>`);
                    });
                    $sel.trigger('change');
                }
            }
        });
    }

    var serviceSelectedValues = [];
    var apptEngData = {};

    function updateClientDetails(data) {
        $("#clientId").val(data.id);

        if (data.clientType !== "Individual") {
            // Populate dropdowns with loaded options first
            loadDetailOptions(data.clientType);
            // Need slight delay for options to load before setting values
            setTimeout(function () {
                $('#detailGroup').val(data.group || '').trigger('change');
                $('#detailReferral').val(data.referral || '').trigger('change');
            }, 300);

            $('#detailFileNo').val(data.fileNo || '');
            $('#detailCompanyName').val(data.name || '');
            if (data.companyType) {
                const companyTypeMap = {
                    "Sdn Bhd": "SdnBhd",
                    "LLP": "LLP",
                    "Partnership": "Partnership",
                    "Sole Proprietorships": "SoleProprietorships",
                    "Bhd": "Bhd",
                    "Enterprise": "Enterprise",
                    "Advocate": "Advocate",
                    "Resign": "Resign"
                };
                const mappedType = companyTypeMap[data.companyType] || data.companyType;
                $('#detailCompanyType').val(mappedType).trigger('change');
            } else {
                $('#detailCompanyType').val('').trigger('change');
            }
            $('#detailRegNumber').val(data.registrationNumber || '');
            $('#detailENumber').val(data.employerNumber || '');
            $('#detailTinNumber').val(data.taxIdentificationNumber || '');

            if (data.incorporationDate) {
                const incFp = document.getElementById("detailIncorpDate")?._flatpickr;
                if (incFp) {
                    incFp.setDate(ConvertTimeFormat(data.incorporationDate, "YYYY-MM-DD"), true);
                } else {
                    $('#detailIncorpDate').val(ConvertTimeFormat(data.incorporationDate, "YYYY-MM-DD"));
                }
            }
            if (data.yearEndMonth) {
                const monthNum = getMonthNumber(data.yearEndMonth);
                if (monthNum) {
                    const now = new Date();
                    const year = now.getFullYear();
                    const lastDay = new Date(year, monthNum, 0);
                    const fp = document.getElementById("detailYearEnd")?._flatpickr;
                    if (fp) {
                        fp.setDate(lastDay, true);
                    } else {
                        $('#detailYearEnd').val(ConvertTimeFormat(lastDay, "YYYY-MM-DD"));
                    }
                }
            }

            if (data.activitySize !== undefined && data.activitySize !== null) {
                const activitySizeMap = {
                    "D": "D",
                    "1": "One",
                    "2": "Two",
                    "3": "Three",
                    "4": "Four",
                    "5": "Five",
                    "6": "Six",
                    "One": "One",
                    "Two": "Two",
                    "Three": "Three",
                    "Four": "Four",
                    "Five": "Five",
                    "Six": "Six"
                };
                const mappedVal = activitySizeMap[data.activitySize.toString()] || data.activitySize.toString();
                $('#detailActivitySize').val(mappedVal).trigger('change');
            }
            if (data.companyStatus !== undefined && data.companyStatus !== null) {
                const companyStatusMap = {
                    "Active": "Active",
                    "Dormant": "Dormant",
                    "StrikeOff": "StrikeOff",
                    "Strike-off": "StrikeOff",
                    "Liquidation": "Liquidation",
                    "Resign": "Resign"
                };
                const statusVal = companyStatusMap[data.companyStatus.toString()] || data.companyStatus.toString();
                $('#detailCompanyStatus').val(statusVal).trigger('change');
            }
            $('#detailCompanyStatusReason').val(data.companyStatusReason || '');
            if (data.creditRating !== undefined && data.creditRating !== null) {
                $('#detailCreditRating').val(data.creditRating.toString()).trigger('change');
            }

            $('#detailBusinessNature').val(data.businessNature || '');

            serviceSelectedValues = [];
            if (data.serviceSelected) {
                serviceSelectedValues = data.serviceSelected.split(',').map(s => s.trim()).filter(s => s);
                updateServiceSelectedText();
            }

            $('#detailPrincipalActivity').val(data.principalActivity || '');
            if (data.foreignOwned === true) {
                $('#detailForeignOwned').val('true');
            } else if (data.foreignOwned === false) {
                $('#detailForeignOwned').val('false');
            } else {
                $('#detailForeignOwned').val('');
            }
            if (data.appointmentEngagementData) {
                try {
                    apptEngData = typeof data.appointmentEngagementData === 'string' ? JSON.parse(data.appointmentEngagementData) : data.appointmentEngagementData;
                } catch (e) {
                    console.error("Failed to parse appointmentEngagementData", e);
                    apptEngData = {};
                }
            }
            if (!apptEngData) {
                apptEngData = {};
            }
            updateApptEngText();
            updateNoteReferences(data);
            updateMsicCodeTable(data.msicCodes);
            updateOwnerTable(data.owners);
        } else {
            $("#individualName").val(data.name);
            $("#icOrPassport").val(data.icOrPassportNumber);
            $("#profession").val(data.profession);
        }

        $("#grouping").val(data.group);
        $("#referral").val(data.referral);
        $("#yearEnd").val(formatMonthLabel(data.yearEndMonth));
        $("#tinNumber").val(data.taxIdentificationNumber);
        updateOfficialTable(data.officialContacts);
        updateCommTable(data.communicationContacts);

        openClientDetails();
    }

    function getMonthNumber(monthName) {
        const months = {
            'January': 1, 'February': 2, 'March': 3, 'April': 4, 'May': 5, 'June': 6,
            'July': 7, 'August': 8, 'September': 9, 'October': 10, 'November': 11, 'December': 12
        };
        return months[monthName] || null;
    }

    function updateNoteReferences(data) {
        $('#noteClientRating').text(data.clientRating || '-');
        $('#noteCompanyStatus').text(data.companyStatus ? data.companyStatus.toString() : '-');
        $('#noteCreditRating').text(data.creditRating ? data.creditRating.toString() : '-');
        $('#noteBusinessNature').text(data.businessNature || '-');
        $('#noteServiceSelected').text(data.serviceSelected || '-');
    }

    function updateMsicCodeTable(data) {
        msicCodeTable.clear();
        if (data && data.length > 0) {
            $.each(data, function (i, item) {
                const code = item.msicCode;
                if (code) {
                    msicCodeTable.row.add([code.code || '', code.description || '']);
                }
            });
        }
        msicCodeTable.draw();
    }

    function updateOwnerTable(data) {
        ownerDatatable.clear();
        if (data && data.length > 0) {
            $.each(data, function (i, item) {
                ownerDatatable.row.add({
                    DT_RowClass: "owner-item",
                    DT_RowAttr: { 'data-id': item.id },
                    0: item.id,
                    1: item.namePerIC,
                    2: item.icOrPassportNumber,
                    3: item.position,
                    4: item.taxReferenceNumber,
                    5: item.phoneNumber,
                    6: item.email,
                    7: `<div class="btn-group" role="group">
                            <button type="button" class="btn btn-sm btn-warning btn-edit-owner mr-2" data-id="${item.id}" data-name="${item.namePerIC}" title="Edit"><i class="fa fa-edit"></i></button>
                            <button type="button" class="btn btn-sm btn-danger btn-delete-owner" data-id="${item.id}" data-name="${item.namePerIC}"><i class="fa fa-trash"></i></button>
                        </div>`
                });
            });
        }
        ownerDatatable.draw();
    }

    function updateOfficialTable(data) {
        officialCtDatatable.clear();
        if (data && data.length > 0) {
            $.each(data, function (i, item) {
                officialCtDatatable.row.add({
                    DT_RowClass: "official-item",
                    DT_RowAttr: { 'data-id': item.id },
                    0: item.id,
                    1: item.address,
                    2: item.officeTel,
                    3: item.email,
                    4: item.remark,
                    5: `<div class="btn-group" role="group">
                            <button type="button" class="btn btn-sm btn-warning btn-edit-official-ct mr-2" data-id="${item.id}" data-name="${item.officeTel}" title="Edit"><i class="fa fa-edit"></i></button>
                            <button type="button" class="btn btn-sm btn-danger btn-delete-official-ct" data-id="${item.id}" data-name="${item.officeTel}"><i class="fa fa-trash"></i></button>
                        </div>`
                });
            });
        }
        officialCtDatatable.draw();
    }

    function updateCommTable(data) {
        communicationCtDatatable.clear();
        if (data && data.length > 0) {
            $.each(data, function (i, item) {
                communicationCtDatatable.row.add({
                    DT_RowClass: "comm-item",
                    DT_RowAttr: { 'data-id': item.id },
                    0: item.id,
                    1: item.contactName,
                    2: item.position,
                    3: item.whatsApp,
                    4: item.email,
                    5: item.remark,
                    6: `<div class="btn-group" role="group">
                            <button type="button" class="btn btn-sm btn-warning btn-edit-communication-ct mr-2" data-id="${item.id}" data-name="${item.contactName}" title="Edit"><i class="fa fa-edit"></i></button>
                            <button type="button" class="btn btn-sm btn-danger btn-delete-communication-ct" data-id="${item.id}" data-name="${item.contactName}"><i class="fa fa-trash"></i></button>
                        </div>`
                });
            });
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

    // Service Selected Modal
    $("#btnSelectService").on('click', function () {
        $('.service-checkbox').prop('checked', false);
        $.each(serviceSelectedValues, function (i, val) {
            $(`#service${val}`).prop('checked', true);
        });
        $('#serviceSelectedModal').modal('show');
    });

    $('#serviceSelectedModal').on('click', '.close, [data-dismiss="modal"]', function () {
        $('#serviceSelectedModal').modal('hide');
    });

    $("#btnConfirmService").on('click', function () {
        serviceSelectedValues = [];
        $('.service-checkbox:checked').each(function () {
            serviceSelectedValues.push($(this).val());
        });
        updateServiceSelectedText();
        $('#serviceSelectedModal').modal('hide');
    });

    function updateServiceSelectedText() {
        if (serviceSelectedValues.length === 0) {
            $('#serviceSelectedText').text('None');
        } else {
            $('#serviceSelectedText').text(serviceSelectedValues.join(', '));
        }
    }

    // Appointment & Engagement Modal
    $("#btnAppointmentEngagement").on('click', function () {
        populateApptEngModal(apptEngData);
        $('#apptEngModal').modal('show');
    });

    $('#apptEngModal').on('click', '.close, [data-dismiss="modal"]', function () {
        $('#apptEngModal').modal('hide');
    });

    $("#btnConfirmApptEng").on('click', function () {
        apptEngData = {
            appointments: {
                Sec: { appointmentLetter: $('#apptSecFile').val().split('\\').pop(), appointmentDate: $('#apptSecDate').val() },
                Tax: { taxWork: $('#apptTaxWork').val(), appointmentLetter: $('#apptTaxFile').val().split('\\').pop(), appointmentDate: $('#apptTaxDate').val() },
                Audit: { appointmentLetter: $('#apptAuditFile').val().split('\\').pop(), appointmentDate: $('#apptAuditDate').val() },
                Acc: { appointmentLetter: $('#apptAccFile').val().split('\\').pop(), appointmentDate: $('#apptAccDate').val() },
                FormE: { appointmentLetter: $('#apptFormEFile').val().split('\\').pop(), appointmentDate: $('#apptFormEDate').val() },
                HR: { appointmentLetter: $('#apptHRFile').val().split('\\').pop(), appointmentDate: $('#apptHRDate').val() }
            },
            linkE1: {
                auditExemption: $('#apptLinkE1Exempt').val() === 'true' ? true : ($('#apptLinkE1Exempt').val() === 'false' ? false : null),
                who: $('#apptLinkE1Who').val(),
                appointmentDate: $('#apptLinkE1Date').val()
            },
            linkE22: {
                coSecretary: $('#apptLinkE22CoSec').val() === 'true' ? true : ($('#apptLinkE22CoSec').val() === 'false' ? false : null),
                who: $('#apptLinkE22Who').val()
            }
        };
        updateApptEngText();
        $('#apptEngModal').modal('hide');
    });

    function updateApptEngText() {
        var services = Object.keys(apptEngData.appointments || {}).filter(function (k) {
            return apptEngData.appointments[k] && apptEngData.appointments[k].appointmentDate;
        });
        if (services.length === 0) {
            $('#apptEngText').text('None');
        } else {
            $('#apptEngText').text(services.join(', '));
        }
    }

    function populateApptEngModal(data) {
        if (data.appointments) {
            $.each(data.appointments, function (service, vals) {
                var cap = service.charAt(0).toUpperCase() + service.slice(1);
                if (vals.appointmentDate) $(`#appt${cap}Date`).val(vals.appointmentDate);
                if (vals.taxWork) $(`#appt${cap}Work`).val(vals.taxWork);
            });
        }
        if (data.linkE1) {
            if (data.linkE1.auditExemption === true) $('#apptLinkE1Exempt').val('true');
            else if (data.linkE1.auditExemption === false) $('#apptLinkE1Exempt').val('false');
            else $('#apptLinkE1Exempt').val('');
            $('#apptLinkE1Who').val(data.linkE1.who || '');
            $('#apptLinkE1Date').val(data.linkE1.appointmentDate || '');
        }
        if (data.linkE22) {
            if (data.linkE22.coSecretary === true) $('#apptLinkE22CoSec').val('true');
            else if (data.linkE22.coSecretary === false) $('#apptLinkE22CoSec').val('false');
            else $('#apptLinkE22CoSec').val('');
            $('#apptLinkE22Who').val(data.linkE22.who || '');
        }
    }

    // Save Details
    $("#btnSaveDetails").on('click', function () {
        const id = $("#clientId").val();
        if (!id) {
            Toast_Fire(ICON_INFO, "No client selected");
            return;
        }

        var yearEndMonth = null;
        const yearEndVal = $('#detailYearEnd').val();
        if (yearEndVal) {
            const parsedNum = parseInt(yearEndVal);
            if (!isNaN(parsedNum) && parsedNum >= 1 && parsedNum <= 12 && !yearEndVal.includes("-")) {
                yearEndMonth = parsedNum;
            } else {
                const d = new Date(yearEndVal);
                if (!isNaN(d)) {
                    yearEndMonth = d.getMonth() + 1;
                }
            }
        }

        var incorpDate = $('#detailIncorpDate').val() || null;

        const data = {
            clientId: parseInt(id),
            companyName: $('#detailCompanyName').val() || '',
            registrationNumber: $('#detailRegNumber').val() || '',
            employerNumber: $('#detailENumber').val() || '',
            taxIdentificationNumber: $('#detailTinNumber').val() || '',
            yearEndMonth: yearEndMonth,
            incorporationDate: incorpDate,
            activitySize: $('#detailActivitySize').val() || 'D',
            companyType: $('#detailCompanyType').val() || null,
            companyStatus: $('#detailCompanyStatus').val() || null,
            companyStatusReason: $('#detailCompanyStatusReason').val() || '',
            creditRating: $('#detailCreditRating').val() || null,
            businessNature: $('#detailBusinessNature').val() || '',
            serviceSelected: serviceSelectedValues.join(', '),
            principalActivity: $('#detailPrincipalActivity').val() || '',
            foreignOwned: $('#detailForeignOwned').val() === 'true' ? true : ($('#detailForeignOwned').val() === 'false' ? false : null),
            appointmentEngagementData: JSON.stringify(apptEngData),
            categoryInfo: {
                group: $('#detailGroup').val() || '',
                referral: $('#detailReferral').val() || '',
                fileNo: $('#detailFileNo').val() || ''
            }
        };

        $('#btnSaveDetails').prop('disabled', true).html('<i class="fas fa-spinner fa-spin mr-1"></i>Saving...');

        $.ajax({
            url: `${urls.edit_client}/company`,
            method: "POST",
            data: { req: data },
            success: function (res) {
                $('#btnSaveDetails').prop('disabled', false).html('<i class="fas fa-save mr-1"></i> Save Changes');
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Saved", "Company details updated successfully.");
                }
            },
            error: function () {
                $('#btnSaveDetails').prop('disabled', false).html('<i class="fas fa-save mr-1"></i> Save Changes');
                Toast_Fire(ICON_ERROR, "Error", "Failed to save changes.");
            }
        });
    });

    // Flatpickr for Year End and Incorporation Date
    flatpickr("#detailYearEnd", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m",
                altFormat: "F"
            })
        ],
        altInput: true,
        onChange: function (selectedDates, dateStr, instance) {
            if (!selectedDates.length) return;
            const selectedDate = selectedDates[0];
            var lastDay = getLastDay(selectedDate);
            if (selectedDate.getTime() === lastDay.getTime()) return;
            instance.setDate(lastDay, true);
        },
        onReady: function (selectedDates, dateStr, instance) {
            const monthsEl = instance.calendarContainer.querySelector(".flatpickr-months");
            if (monthsEl) {
                monthsEl.style["display"] = "none";
            }
            if (!selectedDates.length) return;
            const selectedDate = selectedDates[0];
            var lastDay = getLastDay(selectedDate);
            if (selectedDate.getTime() === lastDay.getTime()) return;
            instance.setDate(lastDay, true);
        }
    });

    flatpickr("#detailIncorpDate", {
        allowInput: true
    });

    // Company Status change to show/hide reason
    $('#detailCompanyStatus').on('change', function () {
        const val = $(this).val();
        if (val === 'StrikeOff' || val === 'Liquidation') {
            $('#detailCompanyStatusReason').closest('.form-group').show();
        } else {
            $('#detailCompanyStatusReason').closest('.form-group').hide();
        }
    });
    $('#detailCompanyStatus').trigger('change');

    initSelect2();
});
