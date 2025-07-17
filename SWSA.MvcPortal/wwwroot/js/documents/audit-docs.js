$(function () {
    initSelect2();
    flatpickr("#in_documentDate,#out_documentDate", {
        allowInput: true,
        dateFormat: "Y/m/d"
    });

    $(document).on('click', '.btn-delete-row', function () {
        const id = $(this).data('id');
        const row = inDocsTable.row($(this).parents('tr'));

        if (confirm(`Are you sure you want to delete document ID ${id}? This action cannot be undone.`)) {
            $.ajax({
                url: `${urls.documents}/${id}/delete`,
                method: "DELETE",
                success: function (res) {
                    if (res) {
                        Toast_Fire(ICON_SUCCESS, "Deleted", "The document record is deleted");
                        row.remove().draw();
                    }
                },
            })
        }
    })


    //#region In Document
    const $inDocForm = $("#inDocForm");
    const inDocFormInputs = {
        companyId: $inDocForm.find('select[name="in_client"]'),
        flowType: $inDocForm.find('select[name="in_flowType"]'),
        documentDate: $inDocForm.find('input[name="in_documentDate"]'),
        documentType: $inDocForm.find('select[name="in_docType"]'),
        bagOrBoxCount: $inDocForm.find('input[name="in_bagOrBoxCount"]'),
        uploadLetter: $inDocForm.find('input[name="in_UploadLetter"]'),
        remark: $inDocForm.find('input[name="in_remark"]')
    };

    $inDocForm.validate({
        rules: {
            in_client: {
                required: true
            },
            in_documentDate: {
                required: true
            },
            in_docType: {
                required: true
            },
            in_bagOrBoxCount: {
                required: true
            },
        },
        messages: {
            in_client: {
                required: "Please select a client"
            },
            in_documentDate: {
                required: "Please select a document date"
            },
            in_docType: {
                required: "Please select a document type"
            },
            in_bagOrBoxCount: {
                required: "Please enter the bag or box count"
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    const inDocsTable = $('#inDocsTable').DataTable({
        paging: true,
        lengthChange: false,
        pageLength: 5,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true,
        order: [[0, 'desc']],
        columnDefs: [
            {
                targets: [0, 1], //Hidden Id
                visible: false
            }
        ]
    });

    $inDocForm.on('submit', function (e) {
        e.preventDefault();

        if (!$inDocForm.valid()) {
            return;
        }

        const data = getFormData(inDocFormInputs);
        data.department = 'Audit';

        $.ajax({
            url: `${urls.documents}/submit-doc-record`,
            method: "POST",
            data: { req: data },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Successfully", "The document record is submitted");
                    const cp = res.company;
                    const user = res.handledByStaff;
                    inDocsTable.row.add([
                        res.id,
                        cp.id,
                        `<a href="/companies/${cp.id}/overview" title="company overview">
							${cp.name}
						</a>`,
                        formatMonthLabel(cp.yearEndMonth),
                        res.documentType,
                        ConvertTimeFormat(data.documentDate, "YYYY-MM-DD"),
                        res.bagOrBoxCount,
                        `<a href="/users/${user.id}/overview" title="staff overview">
						        ${user.fullName} (You)
						    </a>`,
                        res.uploadLetter,
                        res.remark,
                        `
                            <button type="button" class="btn btn-sm btn-danger btn-in-delete-row" data-id="${res.id}"><i class="fa fa-trash"></i></button>
                            `
                    ]).draw(false);
                }
            },
            error: function (jqxhr) {
                // optional error handling
            }
        });
    });

    $("#in_display_client").on('change', function () {
        const id = $(this).val() ?? "";
        $("#in_client").val(id).trigger("change");
        if (id) {
            $.ajax({
                url: `${urls.client}/${id}/simple-info`,
                method: "GET",
                success: function (res) {
                    currentCp = res;
                    $("#in_yearEndMonth").val(formatMonthLabel(res.yearEndMonth) ?? "-");
                    inDocsTable.column(1).search('^' + id + '$', true, false).draw();
                }
            })
        } else {
            inDocsTable.column(1).search('').draw();
            $("#in_yearEndMonth").val("-");
        }
    })

    //#endregion

    //#region Out Document

    const $outDocForm = $("#outDocForm");
    const outDocFormInputs = {
        companyId: $outDocForm.find('select[name="out_client"]'),
        flowType: $outDocForm.find('select[name="out_flowType"]'),
        documentDate: $outDocForm.find('input[name="out_documentDate"]'),
        documentType: $outDocForm.find('select[name="out_docType"]'),
        bagOrBoxCount: $outDocForm.find('input[name="out_bagOrBoxCount"]'),
        uploadLetter: $outDocForm.find('input[name="out_UploadLetter"]'),
        remark: $outDocForm.find('input[name="out_remark"]')
    };

    $outDocForm.validate({
        rules: {
            out_client: {
                required: true
            },
            out_documentDate: {
                required: true
            },
            out_docType: {
                required: true
            },
            out_bagOrBoxCount: {
                required: true
            },
        },
        messages: {
            out_client: {
                required: "Please select a client"
            },
            out_documentDate: {
                required: "Please select a document date"
            },
            out_docType: {
                required: "Please select a document type"
            },
            out_bagOrBoxCount: {
                required: "Please enter the bag or box count"
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    const outDocsTable = $('#outDocsTable').DataTable({
        paging: true,
        lengthChange: false,
        pageLength: 5,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true,
        order: [[0, 'desc']],
        columnDefs: [
            {
                targets: [0, 1], //Hidden Id
                visible: false
            }
        ]
    });

    $outDocForm.on('submit', function (e) {
        e.preventDefault();

        if (!$outDocForm.valid()) {
            return;
        }

        const data = getFormData(outDocFormInputs);
        data.department = 'Audit';

        $.ajax({
            url: `${urls.documents}/submit-doc-record`,
            method: "POST",
            data: { req: data },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Successfully", "The document record is submitted");
                    const cp = res.company;
                    const user = res.handledByStaff;
                    outDocsTable.row.add([
                        res.id,
                        cp.id,
                        `<a href="/companies/${cp.id}/overview" title="company overview">
							${cp.name}
						</a>`,
                        formatMonthLabel(cp.yearEndMonth),
                        res.documentType,
                        ConvertTimeFormat(data.documentDate, "YYYY-MM-DD"),
                        res.bagOrBoxCount,
                        `<a href="/users/${user.id}/overview" title="staff overview">
						        ${user.fullName} (You)
						    </a>`,
                        res.uploadLetter,
                        res.remark,
                        `
                            <button type="button" class="btn btn-sm btn-danger btn-delete-row" data-id="${res.id}"><i class="fa fa-trash"></i></button>
                            `
                    ]).draw(false);
                }
            },
            error: function (jqxhr) {
                // optional error handling
            }
        });
    });

    $("#out_display_client").on('change', function () {
        const id = $(this).val() ?? "";
        $("#out_client").val(id).trigger("change");
        if (id) {
            $.ajax({
                url: `${urls.companies}/${id}/simple-info`,
                method: "GET",
                success: function (res) {
                    currentCp = res;
                    $("#out_yearEndMonth").val(formatMonthLabel(res.yearEndMonth) ?? "-");
                    outDocsTable.column(1).search('^' + id + '$', true, false).draw();
                }
            })
        } else {
            outDocsTable.column(1).search('').draw();
            $("#out_yearEndMonth").val("-");
        }
    })

    //#endregion
})

