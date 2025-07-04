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

    $("#yearpicker").yearpicker()
    $("#yearpicker").on('change', function () {
        $(this).valid();
    })

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
    $(document).on("click","#searchBtn", function () {
        const searchValue = $("#searchInput").val();
        const cpCards = $(".company-selection-container .company-item");

        const result = cpCards.filter(function () {
            const name = $(this).find(".company-name").text().toLowerCase();
            return name.includes(searchValue.toLowerCase());
        });

        cpCards.hide();
        result.show();
    })


    $(document).on('click', '.company-item', function (e) {
        const companyId = $(this).data("id");

        if (companyId === currentSelectCompanyId) {
            return;
        }

        $(".company-item.active").removeClass("active");
        $(this).addClass("active");

        currentSelectCompanyId = companyId;

        startLoading();
        $.ajax({
            type: "GET",
            url: `${urls.companies}/${companyId}`,
            success: function (res) {
                //Load details
                updateWorkType(res);
                $("#companyName").val(res.name);
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

                $submissionForm.validate().resetForm();
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
            const staffsName = item.assignedUsers
                .map(userMap => {
                    var user = userMap.user;

                    return ` <span class="mr-1">
                            <a href="/users/${user.staffId}/overview">
                                ${user.fullName}
                            </a>
                        </span>`;
                });

            workTable.row.add([
                `<a href="/companies/works/${item.id}/edit">${item.workType}</a>`,
                item.companyStatus,
                item.companyActivityLevel,
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
            var code = item.msicCode;

            msicTable.row.add([
                code.code,
                code.description
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

    function updateWorkType(cp) {
        console.log(cp)
        const workTypeSelect = submissionFormInputs.workType;
        const annualReturn = workTypeSelect.find('option[value="AnnualReturn"]');
        const audit = workTypeSelect.find('option[value="Audit"]');
        const strikeOff = workTypeSelect.find('option[value="StrikeOff"]');
        const llp = workTypeSelect.find('option[value="LLP"]');

        submittedWorks = cp.workAssignments.map(w => ({
            workType: w.workType.replaceAll(' ',''),
            year: w.forYear
        }));


        if (cp.companyType !== "Sdn Bhd") {
            annualReturn.text(annualReturn.text().split("(")[0] + ` (Not support for ${cp.companyType})`).prop("disabled", true);
            audit.text(audit.text().split("(")[0] + ` (Not support for ${cp.companyType})`).prop("disabled", true);
            llp.text(llp.text().split("(")[0] + ` (Not support for ${cp.companyType})`).prop("disabled", true);

            if (cp.companyType === "Individual") {
                strikeOff.text(strikeOff.text().split("(")[0] + ` (Not support for ${cp.companyType})`).prop("disabled", true);
            } else if (cp.companyType === "LLP") {
                if (cp.strikeOffStatus !== "Not Applied") {
                    strikeOff.text(strikeOff.text().split("(")[0] + ` (Applied)`).prop("disabled", true);
                } else {
                    strikeOff.text(strikeOff.text().split("(")[0]).prop("disabled", false);
                }

                llp.text(llp.text().split("(")[0]).prop("disabled", false);
            }
        }
        else {
            const strikeOffWork = cp.workAssignments.filter(w => w.workType === "Company Strike Off");

            if (strikeOffWork.length === 0) {
                strikeOff.text(strikeOff.text().split("(")[0]).prop("disabled", false);
            } else {
                strikeOff.text(strikeOff.text().split("(")[0] + ` (Applied)`).prop("disabled", true);
            }

            annualReturn.text(annualReturn.text().split("(")[0]).prop("disabled", false);
            audit.text(audit.text().split("(")[0]).prop("disabled", false);
            llp.text(llp.text().split("(")[0] + ` (Not support for ${cp.companyType})`).prop("disabled", true);
        }


        //if (cp.isStrikedOff && cp.strikeOffStatus !== "Not Applied") {
        //    $("#requestStrikeOffSubmission").prop("disabled", true);
        //    $("#strikeOffContainer").removeClass("d-none");
        //    $("#strikeOffStatus").val(cp.strikeOffStatus);
        //    $("#strikeOffEffectiveDate").val(ConvertTimeFormat(cp.strikeOffEffectiveDate, "DD-MM-YYYY")).trigger("change");

        //    const strikeOffText = submissionFormInputs.workType.find('option[value="StrikeOff"]').text();
        //    submissionFormInputs.workType.val("").trigger("change");
        //    submissionFormInputs.year.val("").trigger("change");
        //    submissionFormInputs.workType.find('option[value="StrikeOff"]').prop("disabled", true).text(`${strikeOffText} (Applied)`);
        //} else {
        //    $("#strikeOffStatus").val("");
        //    $("#strikeOffEffectiveDate").val("").trigger("change");
        //    $("#strikeOffContainer").addClass("d-none");
        //    $("#requestStrikeOffSubmission").prop("disabled", false);
        //    submissionFormInputs.workType.find('option[value="StrikeOff"]').prop("disabled", false);
        //}

        reorderSelect2OptionsByText(workTypeSelect);
        workTypeSelect.val("");

    }
    //#endregion

    //#region handle submission
    let currentSelectCompanyId = 0;
    let submittedWorks = []

    const $submissionForm = $("#submissionForm");
    const submissionFormInputs = {
        workType: $submissionForm.find('select[name="submissionSelect"]'),
        year: $submissionForm.find('input[name="yearpicker"]'),
    };

    const unsupportedWorkTypes = ["Accounting", "SdnBhd", "Enterprise", "Partnership", "FormBE", "FormB", "Trust",];

    submissionFormInputs.workType.find('option').each(function () {
        const optionValue = $(this).val();
        if (unsupportedWorkTypes.includes(optionValue)) {
            var text = $(this).text();
            $(this).prop('disabled', true).text(`${text} (Not support yet)`);
        }
    });

    $.validator.addMethod("yearIsValid", function (value, element) {
        const workType = submissionFormInputs.workType.val();
        const isDuplicate = submittedWorks.some(w => w.workType === workType && w.year == value);
        return !isDuplicate;
    }, "This year has already been submitted for the selected work type.");

    $submissionForm.validate({
        rules: {
            submissionSelect: {
                required: true
            },
            yearpicker: {
                required: true,
                yearIsValid: true
            },
        },
        messages: {
            submissionSelect: {
                required: "Work Type are required"
            },
            yearpicker: {
                required: "Year are required",
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
                data: { companyId: currentSelectCompanyId, type: data.workType, year: data.year },
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Request submitted successfully.");
                    setTimeout(() => {
                        window.location.href = res;
                    }, 1000)
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


    tableResizeEventListener();
})
