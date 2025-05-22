$(function () {
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
    //#endregion

    //#region Owner Form 
    const $ownerForm = $("#ownerForm");
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
        addOwnerRow(ownerData);
    });

    //#endregion

    //#region Official Contact Form 
    const $officialContactForm = $("#officialContactForm");
    const officialContactFormInputs = {
        address: $officialContactForm.find('input[name="officialAddress"]'),
        officeTel: $officialContactForm.find('input[name="officeTel"]'),
        email: $officialContactForm.find('input[name="officialEmail"]'),
        remark: $officialContactForm.find('input[name="officialRemark"]')
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

        const officialContactData = getFormData(officialContactFormInputs);
        addOfficialContactRow(officialContactData);
    });

    //#endregion

    //#region Communication Contact Form 
    const $communicationContactForm = $("#communicationContactForm");
    const communicationContactFormInputs = {
        contactName: $communicationContactForm.find('input[name="contactName"]'),
        whatsApp: $communicationContactForm.find('input[name="contactWhatsapp"]'),
        email: $communicationContactForm.find('input[name="contactEmail"]'),
        remark: $communicationContactForm.find('input[name="contactRemark"]'),
        position: $communicationContactForm.find('select[name="contactPosition"]')
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

        const contactData = getFormData(communicationContactFormInputs);
        addCommunicationContactRow(contactData)
    });

    //#endregion

    //#region Handle User Form 
    const $handleUserForm = $("#handleUserForm");
    const handleUserFormInputs = {
        handleStaffId: $handleUserForm.find('select[name="handleStaffId"]'),
        userDepartments: $handleUserForm.find('select[name="userDepartments"]'),
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

        const staffId = handleUserFormInputs.handleStaffId.val(); // staff ID
        const staffName = handleUserFormInputs.handleStaffId.find('option:selected').text(); // staff 名字
        const departmentNames = handleUserFormInputs.userDepartments.find('option:selected')
            .map(function () { return $(this).text(); })
            .get(); // array of names

        const staffData = {
            staffId: staffId,
            name: `${staffName}`, 
            departments: departmentNames
        };

        addHandleUserRow(staffData)

        handleUserFormInputs.handleStaffId.find(`option[value="${staffId}"]`).prop('disabled', true);
        handleUserFormInputs.userDepartments.val("").trigger('change');
        handleUserFormInputs.handleStaffId.val("").trigger('change'); 
        $handleUserForm[0].reset();
    });

    //#endregion


    //#region Table
    const handleUserTable = $('#handleUserTable').DataTable({
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    const ownerTable = $('#ownerTable').DataTable({
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    const officialContactTable = $('#officialContactTable').DataTable({
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    const communicationContactTable = $('#communicationContactTable').DataTable({
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
        ],
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
        },
    });

    $(document).on('click', '.btn-delete-row', function () {
        const table = $(this).closest('table').DataTable();
        table.row($(this).closest('tr')).remove().draw(false);
    });

    $(document).on('click', '#handleUserTable .btn-delete-row', function () {
        const id = $(this).val();
        handleUserFormInputs.handleStaffId.find(`option[value="${id}"]`).prop('disabled', false);
    });

    $("#btnSubmitRequest").on('click', function (e) {

        if (!$companyForm.valid()) {
            demoSubmitCompany()
            return;
        }

        const companyData = getFormData(companyFormInputs);
        companyData.yearEndMonth = extractNumbers(companyData.yearEndMonth);
        companyData.companyOwners = getOwnerTableData();
        companyData.communicationContacts = getCommunicationContactTableData();
        companyData.officialContacts = getOfficialContactTableData();
        companyData.handleUsers = getHandleUserTableData();
        companyData.complianceDate = getFormData(complianceDateFormInputs);

        $.ajax({
            url: `${urls.companies}/create`,
            method: "POST",
            data: companyData,
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Created", "Company created successfully.");
                    window.location.href = `${urls.companies}/${res}/overview`;
                }
            },
            error: (res) => {
            }
        })
    });

    function addHandleUserRow(data) {
        const rowHtml = [
            `<td data-staff-id="${data.staffId}">
                ${data.name}
            </td>`,
            `<td data-departments="${data.departments.join(',')}">
                ${data.departments.join(',')}
            </td>`,
            `<td>
            <button type="button" class="btn btn-sm btn-danger btn-delete-row" value="${data.staffId}">
                <i class="fa fa-trash"></i>
            </button>
        </td>`
        ];

        handleUserTable.row.add(rowHtml).draw(false);
    }

    function addOwnerRow(data) {
        ownerTable.row.add([
            data.namePerIC,
            data.icOrPassportNumber,
            data.email,
            data.phoneNumber,
            data.position,
            data.ownershipType,
            data.taxReferenceNumber,
            '<button type="button" class="btn btn-sm btn-danger btn-delete-row"><i class="fa fa-trash"></i></button>'
        ]).draw(false);
    }

    function addCommunicationContactRow(data) {
        communicationContactTable.row.add([
            data.contactName,
            data.whatsApp,
            data.email,
            data.position,
            data.remark,
            '<button type="button" class="btn btn-sm btn-danger btn-delete-row"><i class="fa fa-trash"></i></button>'
        ]).draw(false);
    }

    function addOfficialContactRow(data) {
        officialContactTable.row.add([
            data.address,
            data.officeTel,
            data.email,
            data.remark,
            '<button type="button" class="btn btn-sm btn-danger btn-delete-row"><i class="fa fa-trash"></i></button>'
        ]).draw(false);
    }

    function getOwnerTableData() {
        const data = [];

        ownerTable.rows().every(function () {
            const rowData = this.data();

            data.push({
                namePerIC: rowData[0],
                icOrPassportNumber: rowData[1],
                email: rowData[2],
                phoneNumber: rowData[3],
                position: rowData[4],
                ownershipType: rowData[5],
                taxReferenceNumber: rowData[6]
            });
        });

        return data;
    }

    function getCommunicationContactTableData() {
        const data = [];

        communicationContactTable.rows().every(function () {
            const rowData = this.data();

            data.push({
                contactName: rowData[0],
                whatsApp: rowData[1],
                email: rowData[2],
                remark: rowData[3],
                position: rowData[4]
            });
        });

        return data;
    }

    function getOfficialContactTableData() {
        const data = [];

        officialContactTable.rows().every(function () {
            const rowData = this.data();

            data.push({
                address: rowData[0],
                officeTel: rowData[1],
                email: rowData[2],
                remark: rowData[3]
            });
        });

        return data;
    }

    function getHandleUserTableData() {
        const data = [];

        handleUserTable.rows().every(function () {
            const rowData = this.data();

            const staffCell = $('<div>').html(rowData[0]).find('td, *[data-staff-id]');
            const departmentCell = $('<div>').html(rowData[1]).find('td, *[data-departments]');
            const staffId = staffCell.data('staff-id');
            const departmentNames = departmentCell.data('departments');

            data.push({
                staffId: staffId,
                departments: departmentNames.split(',')
            });
        });

        return data;
    }

    //Require to select user
    function demoSubmitCompany() {
        const companyData = {
            "CompanyName": "ABC Biz Solutions Sdn Bhd",
            "RegistrationNumber": "202401234567",
            "EmployerNumber": "E1234567890",
            "TaxIdentificationNumber": "TIN99887766",
            "YearEndMonth": "December",
            "IncorporationDate": "2022-07-15T00:00:00",
            "CompanyType": "SdnBhd",
            "ComplianceDate": {
                "FirstYearAccountStart": "2022-07-15T00:00:00",
                "AGMDate": "2023-09-01T00:00:00",
                "AccountDueDate": "2023-10-31T00:00:00",
                "AnniversaryDate": "2023-07-15T00:00:00",
                "AnnualReturnDueDate": "2023-11-15T00:00:00",
                "Notes": "First year compliance schedule confirmed"
            },
            "MsicCodeIds": [1001, 1002],
            "DepartmentsIds": [1, 2],
            "CompanyOwners": [
                {
                    "CompanyId": null,
                    "NamePerIC": "Tan Ah Kow",
                    "ICOrPassportNumber": "800101-01-1234",
                    "Position": "Director",
                    "TaxReferenceNumber": "SG12345678",
                    "Email": "tan.ah.kow@example.com",
                    "PhoneNumber": "+60123456789",
                    "OwnershipType": "IndividualOwner"
                }
            ],
            "CommunicationContacts": [
                {
                    "CompanyId": null,
                    "ContactName": "Alice Lee",
                    "WhatsApp": "+60123456789",
                    "Email": "alice@example.com",
                    "Remark": "Handles all audit queries",
                    "Position": "Manager"
                }
            ],
            "OfficialContacts": [
                {
                    "CompanyId": null,
                    "Address": "123, Jalan Example, 50450 Kuala Lumpur",
                    "OfficeTel": "03-12345678",
                    "Email": "admin@abcbiz.com",
                    "Remark": "Registered office"
                }
            ],
            "HandleUsers": getHandleUserTableData()
        }
        $.ajax({
            url: `${urls.companies}/create`,
            method: "POST",
            data: companyData,
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Created", "Company created successfully.");
                    window.location.href = `${urls.companies}/${res}/overview`;
                }
            },
            error: (res) => {
            }
        })
    }
})
