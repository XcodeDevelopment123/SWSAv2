$(function () {
    const $submissionForm = $("#submissionForm");

    const submissionFormInputs = {
        companyId: $("#companyId"),
        taskId: $("#workAssignmentId"),
        ssmExtensionDateForAcc: $submissionForm.find('input[name="ssmExtensionDateForAcc"]'),
        arDueDate: $submissionForm.find('input[name="arDueDate"]'),
        accountSubmitDate: $submissionForm.find('input[name="accountSubmitDate"]'),
        arSubmitDate: $submissionForm.find('input[name="arSubmitDate"]'),
        dateSentToClient: $submissionForm.find('input[name="dateSentToClient"]'),
        dateReturnedByClient: $submissionForm.find('input[name="dateReturnedByClient"]'),
        remarks: $submissionForm.find('input[name="remarks"]'),
    };


    $submissionForm.on("submit", function (e) {
        e.preventDefault();
        if (!$submissionForm.valid()) return;

        const data = getFormData(submissionFormInputs);
        $.ajax({
            url: `${urls.company_works}/llp/update`,
            method: "POST",
            data: data,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Saved", "Work updated successfully.");
            },
            error: function (jqxhr) {
            }
        });
    });

    flatpickr("#ssmExtensionDateForAcc,#arDueDate,#accountSubmitDate,#arSubmitDate,#dateSentToClient,#dateReturnedByClient", {
        allowInput: true
    });
})