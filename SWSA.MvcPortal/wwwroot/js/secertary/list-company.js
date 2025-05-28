$(function () {

    //#region init table and date
    const msicTable = $("#msicDataTable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    })
    msicTable.buttons().container().appendTo('#msicDataTable_wrapper .col-md-6:eq(0)');

    const ownerTable = $("#ownerTable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    });
    ownerTable.buttons().container().appendTo('#ownerTable_wrapper .col-md-6:eq(0)');

    const workTable = $("#workTable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    });
    workTable.buttons().container().appendTo('#workTable_wrapper .col-md-6:eq(0)');

    flatpickr("#incorpDate,#strikeOffEffectiveDate", {
        allowInput: true,
    });
    //#endregion init table and date

    //#region scroll event handle
    const $card = $('.select-company-card');
    const $parent = $card.parent();
    const offsetTop = $card.offset().top;

    $(window).on('scroll resize', function () {
        const scrollTop = $(window).scrollTop();
        const screenWidth = $(window).width();

        if (screenWidth < 768) {
            $card.removeClass('fixed').css('width', '');
            return;
        }

        if (scrollTop >= offsetTop) {
            if (!$card.hasClass('fixed')) {
                $card.addClass('fixed');
            }
            $card.css('width', $parent.width() + 'px');
        } else {
            $card.removeClass('fixed');
            $card.css('width', '');
        }
    });
    //#endregion scroll event handle

    //#region load company

    $(document).on('click', '.company-item', function (e) {
        const companyId = $(this).data("id");

        if (companyId === currentSelectCompanyId) {
            return
        }

        $(".company-item.active").removeClass("active");
        $(this).addClass("active");

        currentSelectCompanyId = companyId;

        startLoading();
        $.ajax({
            type: "GET",
            url: `${urls.companies}/${companyId}/secretary`,
            success: function (res) {
                //Load details
                if (res.isStrikedOff && res.strikeOffStatus !== "Not Applied") {
                    $("#requestStrikeOffSubmission").prop("disabled", true);
                    $("#strikeOffContainer").removeClass("d-none");
                    $("#strikeOffStatus").val(res.strikeOffStatus);
                    $("#strikeOffEffectiveDate").val(ConvertTimeFormat(res.strikeOffEffectiveDate, "DD-MM-YYYY")).trigger("change");

                    const strikeOffText = submissionFormInputs.workType.find('option[value="StrikeOff"]').text();
                    submissionFormInputs.workType.val("").trigger("change");
                    submissionFormInputs.workType.find('option[value="StrikeOff"]').prop("disabled", true).text(`${strikeOffText} (Applied)`);
                } else {
                    $("#strikeOffStatus").val("");
                    $("#strikeOffEffectiveDate").val("").trigger("change");
                    $("#strikeOffContainer").addClass("d-none");
                    $("#requestStrikeOffSubmission").prop("disabled", false);
                    submissionFormInputs.workType.find('option[value="StrikeOff"]').prop("disabled", false);
                }

                $("#companyName").val(res.companyName);
                $("#companyType").val(res.companyType);
                $("#yearEndMonth").val(res.yearEndMonth);

                $("#incorpDate").val(ConvertTimeFormat(res.incorporationDate, "DD-MM-YYYY")).trigger("change");
                $("#regisNumber").val(res.registrationNumber);
                $("#eNumber").val(res.employerNumber);
                $("#tinNumber").val(res.taxIdentificationNumber);
                reloadMsic(res.msicCodes);
                reloadOwner(res.owners);
                reloadScheduling(res.workAssignments);
                stopLoading();
            },
            error: function () {
                $(".company-item.active").removeClass("active");
                currentSelectCompanyId = 0;
                stopLoading();
            }

        })

    })

    function reloadScheduling(workAssignments) {
        workTable.clear();
        workAssignments.forEach(item => {
            const submission = item.submission;
            const staffsName = item.assignedUsers
                .map(user => {
                    return ` <span class="mr-1">
                            <a href="/users/${user.staffId}/overview">
                                ${user.staffName}
                            </a>
                        </span>`;
                }
                   );

            workTable.row.add([
                `<a href="/companies/works/${item.taskId}/edit">${item.workType}</a>`,
                item.companyStatus,
                item.activitySize,
                item.isYearEndTask ? "Yes" : "No",
                staffsName,
                ConvertTimeFormat(item.reminderDate, "DD-MM-YYYY"),
                item.progress.status,
                item.internalNote,
            ]);
        });
        workTable.draw();
    }

    function reloadMsic(msicCodes) {
        msicTable.clear();
        msicCodes.forEach(item => {
            msicTable.row.add([
                item.code,
                item.description
            ]);
        });
        msicTable.draw();
    }

    function reloadOwner(owners) {
        ownerTable.clear();
        owners.forEach((item, index) => {
            ownerTable.row.add([
                index + 1,
                item.namePerIC,
                item.icOrPassportNumber,
                item.taxReferenceNumber,
                `<div>
                  <p class="m-0">${item.phoneNumber}</p>
                  <p class="m-0">${item.email}</p>
                </div>`
            ]);
        });
        ownerTable.draw();
    }

    //#endregion

    //#region handle submission
    let currentSelectCompanyId = 0;

    const $submissionForm = $("#submissionForm");
    const submissionFormInputs = {
        workType: $submissionForm.find('select[name="submissionSelect"]'),
    };

    const unsupportedWorkTypes = ["Accounting", "SdnBhd", "Enterprise", "Partnership", "FormBE", "FormB", "Trust",];

    submissionFormInputs.workType.find('option').each(function () {
        const optionValue = $(this).val();
        if (unsupportedWorkTypes.includes(optionValue)) {
            var text = $(this).text();
            $(this).prop('disabled', true).text(`${text} (Not support yet)`);
        }
    });

    $submissionForm.validate({
        rules: {
            submissionSelect: {
                required: true
            }
        },
        messages: {
            submissionSelect: {
                required: "Work Type are required"
            },
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

    $submissionForm.on('submit', function (e) {
        e.preventDefault();
        if (currentSelectCompanyId === 0) {
            Toast_Fire(ICON_ERROR, "Error", "Please select a company first.");
            return;
        }

        if (!$submissionForm.valid()) {
            return;
        }

        const name = $("#companyName").val();
        const data = getFormData(submissionFormInputs);

        var typeText = submissionFormInputs.workType.find(`option[value="${data.workType}"]`).text();

        if (confirm(`Request ${typeText} work for company "${name}"?`)) {
            $.ajax({
                url: `${urls.secretary_dept_submission}/create`,
                type: "POST",
                data: { companyId: currentSelectCompanyId, type: data.workType },
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Request submitted successfully.");
                },
                error: (jqxhr) => {
                    jqxhr.handledError = true;
                    const errorMsg = jqxhr?.responseJSON?.error ?? "Please try again later.";
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", errorMsg);

                }
            })


        }
    });
    //#endregion
})
