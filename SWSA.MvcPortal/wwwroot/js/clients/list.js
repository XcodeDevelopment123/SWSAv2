$(function () {
    let firstLoad = true;
    const clientDatatable = $("#clientDatatable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "columnDefs": [
            {
                targets: [0, 1, 2],
                orderable: false
            }
        ]
    });
    const clientType = $("#clientType").val();
    clientDatatable.buttons().container().appendTo('#clientDatatable_wrapper .col-md-6:eq(0)');
    const $filterForm = $("#filterForm");
    const filterFormInputs = {
        grouping: $filterForm.find('select[name="grouping"]'),
        referral: $filterForm.find('input[name="referral"]'),
        fileNo: $filterForm.find('input[name="fileNo"]'),
        name: $filterForm.find('input[name="companyName"]'),
        companyNumber: $filterForm.find('input[name="companyNumber"]'),
        incorpDateFrom: $filterForm.find('input[name="incorpDateFrom"]'),
        incorpDateTo: $filterForm.find('input[name="incorpDateTo"]'),
        individualName: $filterForm.find('input[name="individualName"]'),
        profession: $filterForm.find('select[name="profession"]'),
    };

    $.validator.addMethod("dateRange", function (value, element) {
        if (!value) return true;

        const fromDate = $("#incorpDateFrom").val();
        const toDate = $("#incorpDateTo").val();

        if (fromDate && toDate) {
            return new Date(fromDate) <= new Date(toDate);
        }
        return true;
    }, "End date must be after start date.");

    $filterForm.validate({
        rules: {
            grouping: {
                required: false
            },
            referral: {
                required: false,
                minlength: 2
            },
            fileNo: {
                required: false,
                minlength: 3
            },
            companyName: {
                required: false,
                minlength: 2
            },
            companyNumber: {
                required: false,
                minlength: 3
            },
            incorpDateFrom: {
                required: false,
                date: true
            },
            incorpDateTo: {
                required: false,
                date: true,
                dateRange: true
            }
        },
        messages: {
            referral: {
                minlength: "Referral must be at least 2 characters long."
            },
            fileNo: {
                minlength: "File No must be at least 3 characters long."
            },
            companyName: {
                minlength: "Company Name must be at least 2 characters long."
            },
            companyNumber: {
                minlength: "Company Number must be at least 3 characters long."
            },
            incorpDateFrom: {
                date: "Please enter a valid start date."
            },
            incorpDateTo: {
                date: "Please enter a valid end date."
            }
        },
        errorElement: 'span',
        errorPlacement: function (error, element) {
            error.addClass('invalid-feedback');
            if (element.hasClass('select2-hidden-accessible')) {
                element.next('.select2-container').after(error);
            } else {
                element.closest('.form-group').append(error);
            }
        },
        highlight: function (element) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element) {
            $(element).removeClass('is-invalid');
        }
    });

    $.ajax({
        url: `${urls.client}/options`,
        method: "POST",
        data: {
            req: getOptionRequest()
        },
        success: function (res) {
            if (res) {
                updateSelectOption(res);
            }

        }
    })

    flatpickr("#incorpDateFrom, #incorpDateTo", {
        allowInput: true,
        dateFormat: "Y-m-d"
    });

    $("#searchBtn").on('click', function (e) {
        e.preventDefault();
        if (!$filterForm.valid()) {
            return;
        }
        const filterData = getFormData(filterFormInputs);

        $.ajax({
            url: `${urls.client}/search?type=${clientType}`,
            method: "POST",
            data: { req: filterData },
            success: function (res) {
                if (res) {
                    updateSearchResults(res);
                    if (firstLoad) {
                        firstLoad = false;
                        return;
                    }
                    Toast_Fire(ICON_SUCCESS, "Search", "Filter applied successfully.");
                }
            },
            error: function (jqxhr) {
                Toast_Fire(ICON_ERROR, "Error", "Failed to apply filter.");
            }
        });
    });

    $("#resetBtn").on('click', function (e) {
        e.preventDefault();
        $filterForm[0].reset();
        $filterForm.find('.is-invalid').removeClass('is-invalid');
        $filterForm.find('.invalid-feedback').remove();

        $filterForm.find('select').val('').trigger('change');

        Toast_Fire(ICON_INFO, "Reset", "Filter has been reset.");
        $("#searchBtn").trigger("click");
    });

    $("#searchBtn").trigger("click");

    function updateSearchResults(data) {
        clientDatatable.clear().draw();
        if (!Array.isArray(data) || data.length === 0) {
            return;
        }

        $.each(data, function (i, item) {
            //Will only have 1 type of client
            if (item.clientType === "Individual") {
                clientDatatable.row.add({
                    DT_RowClass: "cp-item",
                    DT_RowAttr: {
                        'data-id': item.id
                    },
                    0: item.group,
                    1: item.referral,
                    2: item.name,
                    3: item.icOrPassportNumber,
                    4: formatMonthLabel(item.yearEndMonth),
                    5: item.taxIdentificationNumber,
                    6: item.profession
                });
            } else {
                clientDatatable.row.add({
                    DT_RowClass: "cp-item",
                    DT_RowAttr: {
                        'data-id': item.id
                    },
                    0: item.group,
                    1: item.referral,
                    2: item.fileNo,
                    3: item.name,
                    4: item.companyType,
                    5: formatMonthLabel(item.yearEndMonth),
                    6: ConvertTimeFormat(item.incorporationDate, "YYYY-MM-DD"),
                    7: item.registrationNumber,
                    8: item.employerNumber,
                    9: item.taxIdentificationNumber
                });
            }

        })

        clientDatatable.draw();
    }

    function getOptionRequest() {
        var req = {
            clientType: clientType,
        };

        switch (clientType) {
            case "Enterprise":
            case "LLP":
            case "SdnBhd": req.includeGroups = true;
                break;
            case "Individual":
                req.includeGroups = true;
                req.includeProfessions = true;
                break;
            default: break;
        }

        return req;
    }

    function updateSelectOption(res) {

        if (res.professions && res.professions.length > 0) {
            let html = '';
            $.each(res.professions, function (i, item) {
                if (item && item.trim() !== "")
                    html += `<option value="${item}">${item}</option>`
            });

            filterFormInputs.profession.append(html);
        }

        if (res.groups && res.groups.length > 0) {
            let html = '';
            $.each(res.groups, function (i, item) {
                if (item && item.trim() !== "")
                    html += `<option value="${item}">${item}</option>`
            });

            filterFormInputs.grouping.append(html);
        }
    }
})