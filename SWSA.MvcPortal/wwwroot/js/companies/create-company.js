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
        status: $companyForm.find('select[name="companyStatus"]'),
        msicCodeIds: $companyForm.find('select[name="msicCodesIds"]'),
        departmentsIds: $companyForm.find('select[name="departmentsIds"]')
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
            },
            departmentsIds: {
                required: false
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

    //#region Staff Contact Form 
    const $staffForm = $("#staffContactForm");
    const staffFormInputs = {
        contactName: $staffForm.find('input[name="staffName"]'),
        whatsApp: $staffForm.find('input[name="staffWhatsapp"]'),
        email: $staffForm.find('input[name="staffEmail"]'),
        remark: $staffForm.find('input[name="staffRemark"]'),
        position: $staffForm.find('select[name="staffPosition"]')
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
        addStaffContactRow(staffData)
    });

    //#endregion

    //#region Handle User Form 
    const $handleUserForm = $("#handleUserForm");
    const handleUserFormInputs = {
        handleStaffId: $handleUserForm.find('select[name="handleStaffId"]'),
        userDepartmentIds: $handleUserForm.find('select[name="userDepartmentIds"]'),
    };


    $handleUserForm.validate({
        rules: {
            handleStaffId: {
                required: true
            },
            userDepartmentIds: {
                required: true
            },
        },
        messages: {
            handleStaffId: {
                required: "User is required."
            },
            userDepartmentIds: {
                required: "Select at least one departments"
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

        const departmentIds = handleUserFormInputs.userDepartmentIds.val(); // array of IDs
        const departmentNames = handleUserFormInputs.userDepartmentIds.find('option:selected')
            .map(function () { return $(this).text(); })
            .get(); // array of names

        const staffData = {
            staffId: staffId,
            name: `${staffName}`, 
            departmentIds: departmentIds,
            departmentNames: departmentNames
        };

        addHandleUserRow(staffData)

        handleUserFormInputs.handleStaffId.find(`option[value="${staffId}"]`).prop('disabled', true);
        handleUserFormInputs.userDepartmentIds.val("").trigger('change'); 
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

    const staffContactTable = $('#staffContactTable').DataTable({
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
            return;
        }

        const companyData = getFormData(companyFormInputs);
        companyData.yearEndMonth = extractNumbers(companyData.yearEndMonth);
        companyData.companyOwners = getOwnerTableData();
        companyData.staffsContact = getStaffContactTableData();
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
            `<td data-department-ids="${data.departmentIds.join(',')}">
                ${data.departmentNames.join(',')}
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

    function addStaffContactRow(data) {
        staffContactTable.row.add([
            data.contactName,
            data.whatsApp,
            data.email,
            data.remark,
            data.position,
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

    function getStaffContactTableData() {
        const data = [];

        staffContactTable.rows().every(function () {
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
            const departmentCell = $('<div>').html(rowData[1]).find('td, *[data-department-ids]');

            const staffId = staffCell.data('staff-id');
            const departmentIds = departmentCell.data('department-ids');

            data.push({
                staffId: staffId,
                departmentIds: (departmentIds || '').toString().split(',') // string => array
            });
        });

        return data;
    }

})
