$(function () {
    const $btnSubmitRequest = $("#btnSubmitRequest");
    const $documentForm = $("#documentForm");

    const documentFormInputs = {
        companyDepartmentId: $documentForm.find('select[name="companyDepartmentId"]'),
        documentDate: $documentForm.find('input[name="documentDate"]'),
        flowType: $documentForm.find('select[name="flowType"]'),
        documentType: $documentForm.find('select[name="docType"]'),
        bagOrBoxCount: $documentForm.find('input[name="bagOrBoxCount"]'),
        handledByStaffId: $documentForm.find('select[name="handledByStaffId"]'),
        remark: $documentForm.find('input[name="remark"]'),
        attachmentFile: $documentForm.find('input[name="attachmentFile"]'),
        attachmentFileName: $documentForm.find('input[name="attachmentFileName"]')
    };

    $documentForm.validate({
        rules: {
            companyDepartmentId: {
                required: true
            },
            documentDate: {
                required: true,
                date: true
            },
            flowType: {
                required: true
            },
            docType: {
                required: true
            },
            bagOrBoxCount: {
                required: true,
                digits: true,
                min: 1
            },
            handledByStaffId: {
                required: true
            },
            attachmentFile: {
                required: true,
                filetype: "pdf|jpg|jpeg|png|doc|docx|xls|xlsx"
            },
            attachmentFileName: {
                required: true
            }
        },
        messages: {
            companyDepartmentId: {
                required: "Company/Department is required."
            },
            documentDate: {
                required: "Document date is required.",
                date: "Please enter a valid date."
            },
            flowType: {
                required: "Flow type is required."
            },
            docType: {
                required: "Document type is required."
            },
            bagOrBoxCount: {
                required: "Bag/Box count is required.",
                digits: "Please enter a valid number.",
                min: "Minimum count is 1."
            },
            handledByStaffId: {
                required: "Handled by staff is required."
            },
            attachmentFile: {
                required: "Attachment File is required.",
                filetype: "Only the following file types are allowed: PDF, JPG, PNG, DOC, XLS."
            },
            attachmentFileName: {
                required: "Attachment file name is required."
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

    $documentForm.on("submit", function (e) {
        e.preventDefault();

        if (!$documentForm.valid()) {
            return
        }

        const departmentName = documentFormInputs.companyDepartmentId.find('option:selected').text();
        const handledByStaffName = documentFormInputs.handledByStaffId.find('option:selected').text();

        const $fileContainer = $('<div>').text(documentFormInputs.attachmentFileName.val());
        const $hiddenInput = $('<input type="file" class="d-none stored-file" />');

        const file = documentFormInputs.attachmentFile[0].files[0];
        const dataTransfer = new DataTransfer();
        dataTransfer.items.add(file);
        $hiddenInput[0].files = dataTransfer.files;

        $fileContainer.append($hiddenInput);

        const rowNode = documentTable.row.add([
            `<span data-id="${documentFormInputs.companyDepartmentId.val()}">${departmentName}</span>`,
            documentFormInputs.documentDate.val(),
            documentFormInputs.flowType.val(),
            documentFormInputs.bagOrBoxCount.val(),
            `<span data-id="${documentFormInputs.handledByStaffId.val()}">${handledByStaffName}</span>`,
            documentFormInputs.remark.val(),
            '', // placeholder
            '<button type="button" class="btn btn-sm btn-danger btn-delete-row"><i class="fa fa-trash"></i></button>'
        ]).draw(false).node(); // get node

        // 将真实 fileContainer 插入到第6栏（文件名）
        $(rowNode).find('td').eq(6).append($fileContainer);

        Toast_Fire(ICON_SUCCESS, "Document added successfully.");
    })

    const documentTable = $('#documentTable').DataTable({
        paging: true,
        lengthChange: false,
        searching: true,
        ordering: true,
        info: true,
        autoWidth: false,
        responsive: true
    });

    documentFormInputs.attachmentFile.on('change', function () {
        var fileName = $(this).val().split('\\').pop();

        $(this).next('.custom-file-label').html(fileName);
        documentFormInputs.attachmentFileName.val(fileName);
    });

    flatpickr("#documentDate", {
        allowInput: true
    });

    $btnSubmitRequest.on("click", function () {
        const data = getDocumentTableData();

        const formData = new FormData();

        data.forEach((item, index) => {
            formData.append(`Documents[${index}].CompanyId`, item.companyId);
            formData.append(`Documents[${index}].CompanyName`, item.companyName);
            formData.append(`Documents[${index}].DepartmentName`, item.departmentName);
            formData.append(`Documents[${index}].CompanyDepartmentId`, item.companyDepartmentId);
            formData.append(`Documents[${index}].DocumentDate`, item.documentDate);
            formData.append(`Documents[${index}].FlowType`, item.flowType);
            formData.append(`Documents[${index}].BagOrBoxCount`, item.bagOrBoxCount);
            formData.append(`Documents[${index}].HandledByStaffId`, item.handledByStaffId);
            formData.append(`Documents[${index}].Remark`, item.remark || "");
            formData.append(`Documents[${index}].AttachmentFileName`, item.attachmentFileName || "");

            // Upload files[i] separately, matched by index
            if (item.attachmentFile) {
                formData.append('files', item.attachmentFile); // 不需要加索引
            }
        });

        $.ajax({
            url: `${urls.company_documents}/create`,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Create successfullly", "Documents has been created");
                window.location.href = `${urls.company_documents}`;
            },
            error: function (err) {
            }
        });


    })


    $(document).on('click', '.btn-delete-row', function () {
        const table = $(this).closest('table').DataTable();
        table.row($(this).closest('tr')).remove().draw(false);
    });

    function getDocumentTableData() {
        const data = [];

        documentTable.rows().every(function () {
            const row = this.node();
            const $row = $(row);

            const file = $row.find('input.stored-file')[0]?.files?.[0] ?? null;

            data.push({
                companyId: $("#companyId").val(),
                companyName: $("#companyName").val(),
                departmentName: $row.find('td').eq(0).find('span').text(),
                companyDepartmentId: $row.find('td').eq(0).find('span').data('id'),
                documentDate: $row.find('td').eq(1).text(),
                flowType: $row.find('td').eq(2).text(),
                bagOrBoxCount: $row.find('td').eq(3).text(),
                handledByStaffId: $row.find('td').eq(4).find('span').data('id'),
                remark: $row.find('td').eq(5).text(),
                attachmentFileName: $row.find('td').eq(6).text().trim(),
                attachmentFile: file
            });
        });


        return data;
    }
})

