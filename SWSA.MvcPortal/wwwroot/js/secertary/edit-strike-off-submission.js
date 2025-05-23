$(function () {
    const $submissionForm = $("#submissionForm");

    const submissionFormInputs = {
        companyId: $("#companyId"),
        submissionId: $("#submissionId"),
        completeDate: $submissionForm.find('input[name="completeDate"]'),
        ssmSubmissionDate: $submissionForm.find('input[name="ssmSubmissionDate"]'),
        irbSubmissionDate: $submissionForm.find('input[name="irbSubmissionDate"]'),
        ssmStrikeOffDate: $submissionForm.find('input[name="ssmStrikeOffDate"]'),
        remarks: $submissionForm.find('input[name="remarks"]'),
    };


    $submissionForm.on("submit", function (e) {
        e.preventDefault();
        if (!$submissionForm.valid()) return;

        const data = getFormData(submissionFormInputs);
        $.ajax({
            url: `${urls.secretary_dept_submission}/company-strike-off/update`,
            method: "POST",
            data: data,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Saved", "Submission updated successfully.");
            },
            error: function (jqxhr) {
            }
        });
    });

    flatpickr("#ssmStrikeOffDate,#irbSubmissionDate,#ssmSubmissionDate,#completeDate,#startDate", {
        allowInput: true
    });
})