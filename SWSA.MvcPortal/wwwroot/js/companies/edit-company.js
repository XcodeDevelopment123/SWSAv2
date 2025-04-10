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
        companyTypeId: $companyForm.find('select[name="companyType"]'),
        status: $companyForm.find('select[name="companyStatus"]'),
        msicCodeIds: $companyForm.find('select[name="msicCodesIds"]')
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
            companyStatus: {
                required: true
            },
            msicCodesIds: {
                required: true
            }
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

    $companyForm.on('submit', function (e) {
        e.preventDefault();
        if (!$companyForm.valid()) {
            return;
        }

        const companyData = getFormData(companyFormInputs);
        companyData.yearEndMonth = extractNumbers(companyData.yearEndMonth);
        companyData.companyId = $companyId.val();
        console.log(companyData)
        $.ajax({
            url: `${urls.companies}/edit`,
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
        const apiUrl = editOwner.isEdit ? `${urls.company_owner}/edit` : `${urls.company_owner}/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: ownerData },
            success: function (res) {
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

        ownerFormInputs.namePerIC.val(rowData[0]);
        ownerFormInputs.icOrPassportNumber.val(rowData[1]);
        ownerFormInputs.email.val(rowData[2]);
        ownerFormInputs.phoneNumber.val(rowData[3]);
        ownerFormInputs.position.val(rowData[4]).trigger('change');
        ownerFormInputs.ownershipType.val(rowData[5]).trigger('change');
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
        const apiUrl = editOfficialContact.isEdit ? `${urls.company_official_contact}/edit` : `${urls.company_official_contact}/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: contactData },
            success: function () {
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

    //#region Staff Contact Form 
    const $staffForm = $("#staffContactForm");
    const $btnAddStaff = $("#btnAddStaffContact");
    const $btnCancelStaff = $("#btnCancelStaffContact");

    const staffFormInputs = {
        contactName: $staffForm.find('input[name="staffName"]'),
        whatsApp: $staffForm.find('input[name="staffWhatsapp"]'),
        email: $staffForm.find('input[name="staffEmail"]'),
        remark: $staffForm.find('input[name="staffRemark"]'),
        position: $staffForm.find('select[name="staffPosition"]')
    };

    let editStaff = {
        isEdit: false,
        index: -1,
        id: -1
    };

    $staffForm.validate({
        rules: {
            staffName: {
                required: true
            },
            staffWhatsapp: {
                required: true
            },
            staffEmail: {
                required: true,
                email: true
            },
            staffRemark: {
                required: false
            },
            staffPosition: {
                required: true
            }
        },
        messages: {
            staffName: {
                required: "Contact Name is required."
            },
            staffWhatsapp: {
                required: "WhatsApp number is required."
            },
            staffEmail: {
                required: "Email is required.",
                email: "Please enter a valid email address."
            },
            staffPosition: {
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

    $staffForm.on('submit', function (e) {
        e.preventDefault();

        if (!$staffForm.valid()) {
            return;
        }

        const staffData = getFormData(staffFormInputs);
        staffData.companyId = $companyId.val();
        staffData.contactId = editStaff.id;

        const apiUrl = editStaff.isEdit ? `${urls.company_communication_contact}/edit` : `${urls.company_communication_contact}/create`;

        $.ajax({
            url: apiUrl,
            method: "POST",
            data: { req: staffData },
            success: function () {
                addOrUpdateStaffContactRow(staffData);
                Toast_Fire(ICON_SUCCESS, "Success", editStaff.isEdit ? "Contact saved successfully." : "Contact added successfully.");
                $btnCancelStaff.trigger("click");
            },
            error: function () {
                Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");
            }
        });
    });

    $(document).on("click", ".btn-edit-staff-ct", function () {
        const row = staffContactTable.row($(this).closest("tr"));
        const rowData = row.data();
        row.select();

        editStaff.isEdit = true;
        editStaff.id = $(this).data("id");
        editStaff.index = row.index();

        staffFormInputs.contactName.val(rowData[0]);
        staffFormInputs.whatsApp.val(rowData[1]);
        staffFormInputs.email.val(rowData[2]);
        staffFormInputs.remark.val(rowData[3]);
        staffFormInputs.position.val(rowData[4]).trigger("change");

        $btnAddStaff.text("Save");
        $btnCancelStaff.show();
    });

    $btnCancelStaff.on("click", function () {
        editStaff = {
            isEdit: false,
            index: -1,
            id: -1
        };

        staffFormInputs.contactName.val("");
        staffFormInputs.whatsApp.val("");
        staffFormInputs.email.val("");
        staffFormInputs.remark.val("");
        staffFormInputs.position.val("").trigger("change");

        $btnAddStaff.text("Add");
        $btnCancelStaff.hide();
        staffContactTable.rows().deselect();
    });

    $(document).on("click", ".btn-delete-staff-ct", function () {
        const id = $(this).data("id");
        const name = $(this).data("name");
        const row = staffContactTable.row($(this).closest("tr"));

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.company_communication_contact}/${id}/delete`,
                method: "DELETE",
                success: function () {
                    Toast_Fire(ICON_SUCCESS, "Success", "Communication contact deleted successfully.");
                    row.remove().draw(false);

                    if (editStaff.isEdit) {
                        $btnCancelStaff.trigger("click");
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

    const staffContactTable = $('#staffContactTable').DataTable({
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

    flatpickr("#incorpDate");
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

    function addOrUpdateStaffContactRow(data) {
        let html =
            `
   <div class="btn-group" role="group">
       <button type="button" class="btn btn-sm btn-warning btn-edit-staff-ct mr-2" data-id="${data.contactId}" data-name="${data.contactName}" title="Edit">
           <i class="fa fa-edit"></i>
       </button>
       <button type="button" class="btn btn-sm btn-danger btn-delete-staff-ct" data-id="${data.contactId}" data-name="${data.contactName}"><i class="fa fa-trash"></i></button>
   </div>
               `;
        if (editStaff.isEdit) {
            staffContactTable.row(editStaff.index).data([
                data.contactName,
                data.whatsApp,
                data.email,
                data.remark,
                data.position,
                html
            ]).draw(false);
        }
        else {
            staffContactTable.row.add([
                data.contactName,
                data.whatsApp,
                data.email,
                data.remark,
                data.position,
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
})
