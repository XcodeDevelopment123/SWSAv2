$(function () {

    //#region Document
    const $documentForm = $("#documentForm");

    const documentFormInputs = {
        workAssignmentId: $("#workAssignmentId"),
        documentDate: $documentForm.find('input[name="documentDate"]'),
        flowType: $documentForm.find('select[name="flowType"]'),
        documentType: $documentForm.find('select[name="docType"]'),
        bagOrBoxCount: $documentForm.find('input[name="bagOrBoxCount"]'),
        remark: $documentForm.find('input[name="remark"]'),
        handledByStaffId: $documentForm.find('select[name="handledByStaffId"]'),
        attachmentFile: $documentForm.find('input[name="attachmentFile"]'),
        attachmentFileName: $documentForm.find('input[name="attachmentFileName"]')
    };

    $documentForm.validate({
        rules: {
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
            attachmentFile: {
                required: true,
                filetype: "pdf|jpg|jpeg|png|doc|docx|xls|xlsx"
            },
            attachmentFileName: {
                required: true
            }
        },
        messages: {
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

    $documentForm.on("submit", function (e) {
        e.preventDefault();

        if (!$documentForm.valid()) {
            return
        }

        const file = documentFormInputs.attachmentFile[0].files[0];
        const data = getFormData(documentFormInputs);
        const formData = new FormData();

        formData.append(`workAssignmentId`, data.workAssignmentId)
        formData.append(`documentDate`, data.documentDate);
        formData.append(`flowType`, data.flowType);
        formData.append(`documentType`, data.documentType);
        formData.append(`bagOrBoxCount`, data.bagOrBoxCount);
        formData.append(`remark`, data.remark || "");
        formData.append(`attachmentFileName`, data.attachmentFileName || "");

        formData.append('file', file);


        $.ajax({
            url: `${urls.documents}/create-work`,
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Submit successfullly", "Documents has been saved");
                setTimeout(() => {
                    window.location.reload();
                }, 1000)
            },
            error: function (err) {
            }
        });

    })

    $(".btn-delete").on("click", function () {
        const documentId = $(this).data("id");
        const name = $(this).data('name');
        const row = $(this).closest('table').DataTable().row($(this).closest('tr'))

        if (confirm(`Are you sure you want to delete "${name}"?`)) {
            $.ajax({
                url: `${urls.documents}/${documentId}/delete`,
                method: "DELETE",
                success: function (res) {
                    if (res) {
                        Toast_Fire(ICON_SUCCESS, "Deleted", "Document deleted successfully.");
                        row.remove().draw(false);
                    }
                },
                error: (res) => {
                    Toast_Fire(ICON_ERROR, "Somethign went wrong", "Please try again later.");
                }
            })


        }
    })
    //#endregion

    $("#documentForm").remove()
})