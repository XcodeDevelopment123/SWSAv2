$(function () {
    const $companyId = $("#companyId");

    //#region Company Form 
    const $companyForm = $("#companyForm");
    const companyFormInputs = {
        companyName: $companyForm.find('input[name="companyName"]'),
        registrationNumber: $companyForm.find('input[name="regisNumber"]'),
        employerNumber: $companyForm.find('input[name="eNumber"]'),
        taxIdentificationNumber: $companyForm.find('input[name="tinNumber"]'),
        yearEndMonth: $companyForm.find('input[name="yearEndMonth"]'),
        incorporationDate: $companyForm.find('input[name="incorpDate"]'),
        companyType: $companyForm.find('select[name="companyType"]'),
        msicCodeIds: $companyForm.find('select[name="msicCodesIds"]'),
        companyStatus: $companyForm.find('select[name="companyStatus"]'),
        companyActivityLevel: $companyForm.find('select[name="companyActivityLevel"]'),
    }

    $companyForm.validate({
        rules: {
            companyName: {
                required: true
            },
            regisNumber: {
                required: true
            },
            eNumber: {
                required: true
            },
            tinNumber: {
                required: true
            },
            incorpDate: {
                required: true
            },
            yearEndMonth: {
                required: true
            },
            companyType: {
                required: true
            },
            msicCodesIds: {
                required: true
            },
            companyStatus: {
                required: true
            },
            companyActivityLevel: {
                required: true
            },
        },
        messages: {
            companyName: {
                required: "Company Name is required."
            },
            regisNumber: {
                required: "Registration Number is required."
            },
            eNumber: {
                required: "Employer Number is required."
            },
            tinNumber: {
                required: "Tax Identification Number is required."
            },
            incorpDate: {
                required: "Incorporation Date is required."
            },
            yearEndMonth: {
                required: "Year End Month is required."
            },
            companyType: {
                required: "Company Type is required."
            },
            companyStatus: {
                required: "Company Status is required."
            },
            msicCodesIds: {
                required: "Please select at least one MSIC code."
            },
            companyStatus: {
                required: "Please select a company status"
            },
            companyActivityLevel: {
                required: "Company activity level is required."
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });

    $companyForm.on('submit', function (e) {
        e.preventDefault();
        if (!$companyForm.valid()) {
            return;
        }

        const companyData = getFormData(companyFormInputs);
        companyData.yearEndMonth = extractNumbers(companyData.yearEndMonth);
        companyData.companyId = $companyId.val();
        $.ajax({
            url: `${urls.companies}/update`,
            method: "POST",
            data: companyData,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Success", "Company updated successfully.");
            },
            error: (res) => {
                Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
            }
        })
    })
    //#endregion

    //#region Compliance Date
    const $complianceDateForm = $("#complianceDateForm");
    const complianceDateFormInputs = {
        firstYearAccountStart: $complianceDateForm.find('input[name="firstYearAccountStart"]'),
        agmDate: $complianceDateForm.find('input[name="agmDate"]'),
        accountDueDate: $complianceDateForm.find('input[name="accountDueDate"]'),
        anniversaryDate: $complianceDateForm.find('input[name="anniversaryDate"]'),
        annualReturnDueDate: $complianceDateForm.find('input[name="annualReturnDueDate"]'),
        notes: $complianceDateForm.find('input[name="notes"]')
    };

    $complianceDateForm.on('submit', function (e) {
        e.preventDefault();

        const dateData = getFormData(complianceDateFormInputs);
        dateData.companyId = $companyId.val();
        $.ajax({
            url: `${urls.company_compliance_date}/update`,
            method: "POST",
            data: { req: dateData },
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Success", "Compliance Date saved successfully.");
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        });
    })

    //#endregion

    //#region Owner Form 
    const $ownerForm = $("#ownerForm");
    const $btnAddOwner = $("#btnAddOwner");
    const $btnCancelOwner = $("#btnCancelOwner");
    const ownerFormInputs = {
        namePerIC: $ownerForm.find('input[name="ownerNamePerIc"]'),
        icOrPassportNumber: $ownerForm.find('input[name="ownerIcOrPassword"]'),
        email: $ownerForm.find('input[name="ownerEmail"]'),
        phoneNumber: $ownerForm.find('input[name="ownerPhone"]'),
        position: $ownerForm.find('select[name="ownerPosition"]'),
        ownershipType: $ownerForm.find('select[name="ownership"]'),
        taxReferenceNumber: $ownerForm.find('input[name="ownerTaxRefer"]')
    };

    $ownerForm.validate({
        rules: {
            ownerNamePerIc: {
                required: true
            },
            ownerIcOrPassword: {
                required: true
            },
            ownerEmail: {
                required: true,
                email: true
            },
            ownerPhone: {
                required: true
            },
            ownerPosition: {
                required: true
            },
            ownership: {
                required: true
            },
            ownerTaxRefer: {
                required: true
            }
        },
        messages: {
            ownerNamePerIc: {
                required: "Name Per IC is required."
            },
            ownerIcOrPassword: {
                required: "IC or Passport Number is required."
            },
            ownerEmail: {
                required: "Email is required.",
                email: "Please enter a valid email address."
            },
            ownerPhone: {
                required: "Phone Number is required."
            },
            ownerPosition: {
                required: "Position is required."
            },
            ownership: {
                required: "Ownership Type is required."
            },
            ownerTaxRefer: {
                required: "Tax Reference Number is required."
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

    $ownerForm.on('submit', function (e) {
        e.preventDefault();
        if (!$ownerForm.valid()) {
            return;
        }

        const ownerData = getFormData(ownerFormInputs);
        ownerData.companyId = $companyId.val();
        ownerData.ownerId = editOwner.id;
        const apiUrl = editOwner.isEdit ? `${urls.company_owner}/update` : `${urls.company_owner}/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: ownerData },
            success: function (res) {

                ownerData.ownerId = editOwner.isEdit ? editOwner.id : res;
                addOrUpdateOwnerRow(ownerData);

                if (!editOwner.isEdit) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Owner added successfully.");
                } else {
                    Toast_Fire(ICON_SUCCESS, "Success", "Owner saved successfully.");

                }
                $btnCancelOwner.trigger("click");
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        });
    });

    let editOwner = {
        isEdit: false,
        index: -1,
        id: -1
    }

    $(document).on("click", ".btn-edit-owner", function () {
        const row = ownerTable.row($(this).closest("tr"));
        const rowData = row.data();
        row.select();

        editOwner.isEdit = true;
        editOwner.id = $(this).data("id");
        editOwner.index = row.index();

        const ownership = rowData[5];

        ownerFormInputs.namePerIC.val(rowData[0]);
        ownerFormInputs.icOrPassportNumber.val(rowData[1]);
        ownerFormInputs.email.val(rowData[2]);
        ownerFormInputs.phoneNumber.val(rowData[3]);
        ownerFormInputs.position.val(rowData[4]).trigger('change');
        ownerFormInputs.ownershipType.val(ownership.replace(" ", "")).trigger('change');
        ownerFormInputs.taxReferenceNumber.val(rowData[6]);

        $btnAddOwner.text("Save");
        $btnCancelOwner.show();
    })

    $btnCancelOwner.on("click", function () {
        editOwner.isEdit = false;
        editOwner.id = -1;
        editOwner.index = -1;

        ownerFormInputs.namePerIC.val("");
        ownerFormInputs.icOrPassportNumber.val("");
        ownerFormInputs.email.val("");
        ownerFormInputs.phoneNumber.val("");
        ownerFormInputs.position.val("").trigger('change');
        ownerFormInputs.ownershipType.val("").trigger('change');
        ownerFormInputs.taxReferenceNumber.val("");
        $btnCancelOwner.hide();
        $btnAddOwner.text("Add");
        ownerTable.rows().deselect();
    })

    $(document).on("click", ".btn-delete-owner", function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = ownerTable.row($(this).closest("tr"));


        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.company_owner}/${id}/delete`,
                method: "DELETE",
                success: function (res) {
                    Toast_Fire(ICON_SUCCESS, "Success", "Owner deleted successfully.");
                    row.remove().draw(false);

                    if (editOwner.isEdit) {
                        $btnCancelOwner.trigger("click");
                    }

                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })

        }
    })

    //#endregion

    //#region Official Contact Form 
    const $officialContactForm = $("#officialContactForm");
    const $btnAddOfficialContact = $("#btnAddOfficialContact");
    const $btnCancelOfficialContact = $("#btnCancelOfficialContact");
    const officialContactFormInputs = {
        address: $officialContactForm.find('input[name="officialAddress"]'),
        officeTel: $officialContactForm.find('input[name="officeTel"]'),
        email: $officialContactForm.find('input[name="officialEmail"]'),
        remark: $officialContactForm.find('input[name="officialRemark"]')
    };

    let editOfficialContact = {
        isEdit: false,
        index: -1,
        id: -1
    };

    $officialContactForm.validate({
        rules: {
            officialAddress: {
                required: true
            },
            officeTel: {
                required: true
            },
            officialEmail: {
                required: true,
                email: true
            },
            officialRemark: {
                required: false
            }
        },
        messages: {
            officialAddress: {
                required: "Address is required."
            },
            officeTel: {
                required: "Office Tel is required."
            },
            officialEmail: {
                required: "Email is required.",
                email: "Please enter a valid email address."
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

    $officialContactForm.on('submit', function (e) {
        e.preventDefault();

        if (!$officialContactForm.valid()) {
            return;
        }

        const contactData = getFormData(officialContactFormInputs);
        contactData.companyId = $companyId.val();
        contactData.contactId = editOfficialContact.id;
        const apiUrl = editOfficialContact.isEdit ? `${urls.company_official_contact}/update` : `${urls.company_official_contact}/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: contactData },
            success: function (res) {
                contactData.contactId = editOfficialContact.isEdit ? editOfficialContact.id : res;
                addOrUpdateOfficialContactRow(contactData);

                Toast_Fire(ICON_SUCCESS, "Success", editOfficialContact.isEdit ? "Contact saved successfully." : "Contact added successfully.");
                $btnCancelOfficialContact.trigger("click");
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        });
    });

    $(document).on("click", ".btn-edit-official-ct", function () {
        const row = officialContactTable.row($(this).closest("tr"));
        const rowData = row.data();
        row.select();

        editOfficialContact.isEdit = true;
        editOfficialContact.id = $(this).data("id");
        editOfficialContact.index = row.index();

        officialContactFormInputs.address.val(rowData[0]);
        officialContactFormInputs.officeTel.val(rowData[1]);
        officialContactFormInputs.email.val(rowData[2]);
        officialContactFormInputs.remark.val(rowData[3]);

        $btnAddOfficialContact.text("Save");
        $btnCancelOfficialContact.show();
    });

    $btnCancelOfficialContact.on("click", function () {
        editOfficialContact.isEdit = false;
        editOfficialContact.index = -1;
        editOfficialContact.id = -1;

        officialContactFormInputs.address.val("");
        officialContactFormInputs.officeTel.val("");
        officialContactFormInputs.email.val("");
        officialContactFormInputs.remark.val("");

        $btnAddOfficialContact.text("Add");
        $btnCancelOfficialContact.hide();
        officialContactTable.rows().deselect();
    });

    $(document).on("click", ".btn-delete-official-ct", function () {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const row = officialContactTable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.company_official_contact}/${id}/delete`,
                method: "DELETE",
                success: function () {
                    Toast_Fire(ICON_SUCCESS, "Success", "Official contact deleted successfully.");
                    row.remove().draw(false);

                    if (editOfficialContact.isEdit) {
                        $btnCancelOfficialContact.trigger("click");
                    }
                },
                error: function () {
                    Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
                }
            });
        }
    });

    //#endregion

    //#region Communication Contact Form 
    const $communicationContactForm = $("#communicationContactForm");
    const $btnAddCommunication = $("#btnAddCommunicationContact");
    const $btnCancelCommunication = $("#btnCancelCommunicationContact");

    const communicationContactFormInputs = {
        contactName: $communicationContactForm.find('input[name="contactName"]'),
        whatsApp: $communicationContactForm.find('input[name="contactWhatsapp"]'),
        email: $communicationContactForm.find('input[name="contactEmail"]'),
        remark: $communicationContactForm.find('input[name="contactRemark"]'),
        position: $communicationContactForm.find('select[name="contactPosition"]')
    };

    let editCommunicationContact = {
        isEdit: false,
        index: -1,
        id: ""
    };

    $communicationContactForm.validate({
        rules: {
            contactName: {
                required: true
            },
            contactWhatsapp: {
                required: true
            },
            contactEmail: {
                required: true,
                email: true
            },
            contactRemark: {
                required: false
            },
            contactPosition: {
                required: true
            }
        },
        messages: {
            contactName: {
                required: "Contact Name is required."
            },
            contactWhatsapp: {
                required: "WhatsApp number is required."
            },
            contactEmail: {
                required: "Email is required.",
                email: "Please enter a valid email address."
            },
            contactPosition: {
                required: "Position is required."
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

    $communicationContactForm.on('submit', function (e) {
        e.preventDefault();

        if (!$communicationContactForm.valid()) {
            return;
        }

        const communicationData = getFormData(communicationContactFormInputs);
        communicationData.companyId = $companyId.val();
        communicationData.contactId = editCommunicationContact.id;

        const apiUrl = editCommunicationContact.isEdit ? `${urls.company_cm_contact}/update` : `${urls.company_cm_contact}/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: communicationData },
            success: function (res) {

                communicationData.contactId = editCommunicationContact.isEdit ? editCommunicationContact.id : res;
                addOrUpdateCommunicationContactRow(communicationData);
                Toast_Fire(ICON_SUCCESS, "Success", editCommunicationContact.isEdit ? "Staff saved successfully." : "Staff added successfully.");
                $btnCancelCommunication.trigger("click");
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        });
    });

    $(document).on("click", ".btn-edit-communication-ct", function () {
        const row = communicationContactTable.row($(this).closest("tr"));
        const rowData = row.data();
        row.select();

        editCommunicationContact.isEdit = true;
        editCommunicationContact.id = $(this).data("id");
        editCommunicationContact.index = row.index();

        communicationContactFormInputs.contactName.val(rowData[0]);
        communicationContactFormInputs.whatsApp.val(rowData[1]);
        communicationContactFormInputs.email.val(rowData[2]);
        communicationContactFormInputs.position.val(rowData[3]).trigger("change");
        communicationContactFormInputs.remark.val(rowData[4]);

        $btnAddCommunication.text("Save");
        $btnCancelCommunication.show();
    });

    $btnCancelCommunication.on("click", function () {
        editCommunicationContact = {
            isEdit: false,
            index: -1,
            id: ""
        };

        communicationContactFormInputs.contactName.val("");
        communicationContactFormInputs.whatsApp.val("");
        communicationContactFormInputs.email.val("");
        communicationContactFormInputs.remark.val("");
        communicationContactFormInputs.position.val("").trigger("change");

        $btnAddCommunication.text("Add");
        $btnCancelCommunication.hide();
        communicationContactTable.rows().deselect();
    });

    $(document).on("click", ".btn-delete-communication-ct", function () {
        const id = $(this).data("id");
        const name = $(this).data("name");
        const row = communicationContactTable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.company_cm_contact}/${id}/delete`,
                method: "DELETE",
                success: function () {
                    Toast_Fire(ICON_SUCCESS, "Success", "Communication contact deleted successfully.");
                    row.remove().draw(false);

                    if (editCommunicationContact.isEdit) {
                        $btnCancelCommunication.trigger("click");
                    }
                },
                error: function () {
                    Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
                }
            });
        }
    });

    //#endregion

    //#region Handle User Form 
    const $handleUserForm = $("#handleUserForm");
    const $btnAddHandleUser = $("#btnAddHandleUser");
    const $btnCancelHandleUser = $("#btnCancelHandleUser");

    const handleUserFormInputs = {
        handleStaffId: $handleUserForm.find('select[name="handleStaffId"]'),
        userDepartments: $handleUserForm.find('select[name="userDepartments"]'),
    };

    let editHandleUser = {
        isEdit: false,
        index: -1,
        staffId: ""
    };


    $handleUserForm.validate({
        rules: {
            handleStaffId: {
                required: true
            },
            userDepartments: {
                required: true
            },
        },
        messages: {
            handleStaffId: {
                required: "User is required."
            },
            userDepartments: {
                required: "Select at least one department"
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

    $handleUserForm.on('submit', function (e) {
        e.preventDefault();

        if (!$handleUserForm.valid()) {
            return;
        }

        const staffId = editHandleUser.isEdit ? editHandleUser.staffId : handleUserFormInputs.handleStaffId.val(); // staff ID
        const staffName = handleUserFormInputs.handleStaffId.find('option:selected').text(); // staff 名字

        const departmentNames = handleUserFormInputs.userDepartments.find('option:selected')
            .map(function () { return $(this).text(); })
            .get(); // array of names

        const staffData = {
            staffId: staffId,
            name: `${staffName}`,
            departments: departmentNames,
            companyId: $companyId.val()
        };

        const apiUrl = editHandleUser.isEdit
            ? `${urls.companies}/${$companyId.val()}/handle-users/update`
            : `${urls.companies}/${$companyId.val()}/handle-users/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: staffData },
            success: function (res) {
                staffData.staffId = editHandleUser.isEdit ? editHandleUser.staffId : res;
                addOrUpdateHandleUserRow(staffData);
                Toast_Fire(ICON_SUCCESS, "Success", editCommunicationContact.isEdit ? "handle user saved successfully." : "handle user added successfully.");
                handleUserFormInputs.handleStaffId.find(`option[value="${staffId}"]`).prop('disabled', true);

                $btnCancelHandleUser.trigger("click");
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        });
    });

    $(document).on("click", ".btn-edit-handle-user", function () {
        const row = handleUserTable.row($(this).closest("tr"));
        const rowData = row.data();
        row.select();

        editHandleUser.isEdit = true;
        editHandleUser.staffId = $(this).data("staff-id");
        editHandleUser.index = row.index();

        handleUserFormInputs.handleStaffId.prop("disabled", true).val($(this).data("staff-id")).trigger("change");
        handleUserFormInputs.userDepartments.val($(this).data("departments")).trigger("change");

        $btnAddHandleUser.text("Save");
        $btnCancelHandleUser.show();
    });

    $btnCancelHandleUser.on("click", function () {
        editHandleUser = {
            isEdit: false,
            index: -1,
            staffId: ""
        };

        handleUserFormInputs.handleStaffId.prop("disabled", false).val("").trigger("change");
        handleUserFormInputs.userDepartments.val("").trigger("change");

        $btnAddHandleUser.text("Add");
        $btnCancelHandleUser.hide();
        handleUserTable.rows().deselect();
    });

    $(document).on("click", ".btn-delete-handle-user", function () {
        const name = $(this).data("name");
        const staffId = $(this).data("staff-id");
        const row = handleUserTable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.companies}/${$companyId.val()}/handle-users/${staffId}/delete`,
                method: "DELETE",
                success: function () {
                    handleUserFormInputs.handleStaffId.find(`option[value="${staffId}"]`).prop('disabled', false);

                    Toast_Fire(ICON_SUCCESS, "Success", "Handle User delete successfully.");
                    row.remove().draw(false);

                    if (editHandleUser.isEdit) {
                        $btnCancelHandleUser.trigger("click");
                    }
                },
                error: function () {
                    Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
                }
            });
        }
    });

    //#endregion

    //#region Table
    const handleUserTable = $('#handleUserTable').DataTable({
        select: {
            style: 'single',
            selector: 'td .btn-edit-handle-user'
        },
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

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
    //#endregion


    initSelect2();

    flatpickr("#incorpDate,#firstYearAccountStart, #agmDate, #accountDueDate, #anniversaryDate, #annualReturnDueDate", {
        allowInput: true
    });
    flatpickr("#yearEndMonth", {

        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m (F)",
                altFormat: "m (F)",
            })
        ]
    });

    $(document).on('click', '.btn-delete-row', function () {
        const table = $(this).closest('table').DataTable();
        table.row($(this).closest('tr')).remove().draw(false);
    });

    function addOrUpdateOwnerRow(data) {
        let html =
            `
   <div class="btn-group" role="group">
       <button type="button" class="btn btn-sm btn-warning btn-edit-owner mr-2" data-id="${data.ownerId}" data-name="${data.namePerIC}" title="Edit">
           <i class="fa fa-edit"></i>
       </button>
       <button type="button" class="btn btn-sm btn-danger btn-delete-owner" data-id="${data.ownerId}" data-name="${data.namePerIC}"><i class="fa fa-trash"></i></button>
   </div>
               `;


        if (editOwner.isEdit) {
            ownerTable.row(editOwner.index).data([
                data.namePerIC,
                data.icOrPassportNumber,
                data.email,
                data.phoneNumber,
                data.position,
                data.ownershipType,
                data.taxReferenceNumber,
                html
            ]).draw(false);
        } else {
            ownerTable.row.add([
                data.namePerIC,
                data.icOrPassportNumber,
                data.email,
                data.phoneNumber,
                data.position,
                data.ownershipType,
                data.taxReferenceNumber,
                html
            ]).draw(false);

        }
    }

    function addOrUpdateCommunicationContactRow(data) {
        let html =
            `
   <div class="btn-group" role="group">
       <button type="button" class="btn btn-sm btn-warning btn-edit-communication-ct mr-2" data-id="${data.contactId}" data-name="${data.contactName}" title="Edit">
           <i class="fa fa-edit"></i>
       </button>
       <button type="button" class="btn btn-sm btn-danger btn-delete-communication-ct" data-id="${data.contactId}" data-name="${data.contactName}"><i class="fa fa-trash"></i></button>
   </div>
               `;
        if (editCommunicationContact.isEdit) {
            communicationContactTable.row(editCommunicationContact.index).data([
                data.contactName,
                data.whatsApp,
                data.email,
                data.position,
                data.remark,
                html
            ]).draw(false);
        }
        else {
            communicationContactTable.row.add([
                data.contactName,
                data.whatsApp,
                data.email,
                data.position,
                data.remark,
                html
            ]).draw(false);
        }

    }

    function addOrUpdateOfficialContactRow(data) {
        let html =
            `
   <div class="btn-group" role="group">
       <button type="button" class="btn btn-sm btn-warning btn-edit-official-ct mr-2" data-id="${data.contactId}" data-name="${data.officeTel}" title="Edit">
           <i class="fa fa-edit"></i>
       </button>
       <button type="button" class="btn btn-sm btn-danger btn-delete-official-ct" data-id="${data.contactId}" data-name="${data.officeTel}"><i class="fa fa-trash"></i></button>
   </div>
               `;


        if (editOfficialContact.isEdit) {

            officialContactTable.row(editOfficialContact.index).data([
                data.address,
                data.officeTel,
                data.email,
                data.remark,
                html
            ]).draw(false);
        }
        else {
            officialContactTable.row.add([
                data.address,
                data.officeTel,
                data.email,
                data.remark,
                html
            ]).draw(false);
        }

    }

    function addOrUpdateHandleUserRow(data) {
        const rowHtml = [
            `<td>
                ${data.name}
            </td>`,
            `<td>
                ${data.departments.join(', ')}
            </td>`,
            `<td>
				<div class="btn-group" role="group">
			        <button type="button" class="btn btn-sm btn-warning btn-edit-handle-user mr-2"
						    data-staff-id="${data.staffId}"
						    data-departments="[${data.departments}]"
						    title="Edit">
						<i class="fa fa-edit"></i>
					</button>
					<button type="button" class="btn btn-sm btn-danger btn-delete-handle-user"
					        data-name="${data.name}"
					        data-staff-id="${data.staffId}">
					    <i class="fa fa-trash"></i>
					</button>
		    	</div>
			</td>`
        ];



        if (editHandleUser.isEdit) {
            handleUserTable.row(editHandleUser.index).data(rowHtml).draw(false);
        }
        else {
            handleUserTable.row.add(rowHtml).draw(false);
        }

    }
})
