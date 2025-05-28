$(function () {
    const $submissionForm = $("#submissionForm");

    const submissionFormInputs = {
        companyId: $("#companyId"),
        submissionId: $("#submissionId"),
        firstYearAccountStart: $submissionForm.find('input[name="firstYearAccountStart"]'),
        accDueDate: $submissionForm.find('input[name="accDueDate"]'),
        dateSubmitted: $submissionForm.find('input[name="dateSubmitted"]'),
        targettedCirculation: $submissionForm.find('input[name="targettedCirculation"]'),
        reasonForLate: $submissionForm.find('input[name="reasonForLate"]'),
    };


    $submissionForm.on("submit", function (e) {
        e.preventDefault();
        if (!$submissionForm.valid()) return;

        const data = getFormData(submissionFormInputs);
        $.ajax({
            url: `${urls.secretary_dept_submission}/audit-report/update`,
            method: "POST",
            data: data,
            success: function (res) {
                Toast_Fire(ICON_SUCCESS, "Saved", "Submission updated successfully.");
            },
            error: function (jqxhr) {
            }
        });
    });

    flatpickr("#firstYearAccountStart,#accDueDate,#dateSubmitted,#documentDate", {
        allowInput: true
    });

    flatpickr("#targettedCirculation", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m(F) Y",
                altFormat: "m(F) Y",
            })
        ],
        onReady: function (selectedDates, dateStr, instance) {
          //  instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";
        },
    });

    $("#accDueDate,#dateSubmitted").on("change", function () {
        const accDueDate = $("#accDueDate").val();
        const dateSubmitted = $("#dateSubmitted").val();

        if (dateSubmitted > accDueDate) {
            $("#isLateLabel").val("Late");
        } else {
            $("#isLateLabel").val("On Time"); 
        }
    })

})