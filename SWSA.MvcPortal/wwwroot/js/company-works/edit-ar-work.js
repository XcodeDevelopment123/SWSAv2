$(function () {
    const $submissionForm = $("#submissionForm");

    const submissionFormInputs = {
        companyId: $("#companyId"),
        taskId: $("#workAssignmentId"),
        anniversaryDate: $submissionForm.find('input[name="anniversaryDate"]'),
        arDueDate: $submissionForm.find('input[name="arDueDate"]'),
        targetedARDate: $submissionForm.find('input[name="targetedARDate"]'),
        dateOfAnnualReturn: $submissionForm.find('input[name="dateOfAnnualReturn"]'),
        dateSubmitted: $submissionForm.find('input[name="dateSubmitted"]'),
        dateSentToClient: $submissionForm.find('input[name="dateSentToClient"]'),
        dateReturnedByClient: $submissionForm.find('input[name="dateReturnedByClient"]'),
        remarks: $submissionForm.find('input[name="remarks"]'),
    };


    $submissionForm.on("submit", function (e) {
        e.preventDefault();
        if (!$submissionForm.valid()) return;

        const data = getFormData(submissionFormInputs);
        $.ajax({
            url: `${urls.company_works}/annual-return/update`,
            method: "POST",
            data: data,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Saved", "Work updated successfully.");
            },
            error: function (jqxhr) {
            }
        });
    });

    flatpickr("#anniversaryDate,#arDueDate,#targetedARDate,#dateOfAnnualReturn,#dateSubmitted,#dateSentToClient,#dateReturnedByClient", {
        allowInput: true
    });
})