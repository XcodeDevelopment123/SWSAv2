// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
const urls = {
    "auth": "/Auth",
    "companies": "/companies",
    "company_owner": "/companies/owner",
    "company_official_contact": "/companies/official-contact",
    "company_cm_contact": "/companies/cm-contact",
    "company_compliance_date": "/companies/compliance-date",
    "company_documents": "/companies/docs",
    "company_works": "/companies/works",
    "company_work_progress": "/companies/works/progress",
    "company_handle_user": "/companies/handle-users",
    "schedule_job": "/scheduler-jobs",
    "system_audit_log": "/sys-audit-logs",
    "secretary_dept_submission":"/secretary-dept/submissions",
    "users": "/users",
};

// example use
/**
 *  
 * $.ajax({
    url: "/api/example",
    method: "POST",
    manualLoading: true, // declare, self control loading
    beforeSend: function () {
        startLoading(); // self control, start timing
    },
    complete: function () {
        stopLoading(); // self control, end timing
    },
    success: function (res) {
        Toast_Fire(ICON_SUCCESS, "Done", "Task completed.");
    }
});
 * 
 */
$.ajaxSetup({
    beforeSend: function (xhr, settings) {
        if (!settings.manualLoading) {
            startLoading();
        }
    },
    complete: function (xhr, status) {
        if (!this.manualLoading) {
            stopLoading();
        }
    }
});

$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    if (jqxhr.handledError) return;
    //jqxhr.status === httpstatus code
    stopLoading();
    console.error(jqxhr.responseJSON);
    Toast_Fire(ICON_ERROR, "Something went wrong", "Please try again later.");

});

document.addEventListener('DOMContentLoaded', function () {
    // Initialize all tooltips
    loadToolTip();
});

function extractNumbers(value) {
    const match = value.match(/\d+/g);
    return match ? match.join('') : null;
}

function initSelect2() {
    $('.select2').select2({
        theme: 'bootstrap4'
    })
}

function getFormData(formInputs) {
    const formData = {};

    for (const key in formInputs) {
        if (!formInputs.hasOwnProperty(key)) continue;

        const $input = formInputs[key];

        if ($input instanceof jQuery) {
            // select[multiple]
            if ($input.is('select[multiple]')) {
                formData[key] = $input.val() || [];
            }
            // file input
            else if ($input.is('input[type="file"]')) {
                const file = $input[0].files[0];
                formData[key] = file ?? null;
            }
            //  input/select
            else {
                formData[key] = $input.val();
            }
        }

        else if (typeof $input === 'object' && $input !== null) {
            formData[key] = getFormData($input);
        }
    }

    return formData;
}

function startLoading() {
    const $overlay = $("#globalLoadingOverlay");
    $overlay.removeClass("d-none");

    $('body').addClass('no-pointer-events');
}

function stopLoading() {
    const $overlay = $("#globalLoadingOverlay");
    $overlay.addClass("d-none");

    $('body').removeClass('no-pointer-events');
}

function loadToolTip() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}


function getOrdinal(day) {
    const suffixes = ["th", "st", "nd", "rd"];
    const v = day % 100;
    return day + (suffixes[(v - 20) % 10] || suffixes[v] || suffixes[0]);
}


function getQueryParam(param) {
    const urlParams = new URLSearchParams(window.location.search);
    return urlParams.get(param);
}

function ConvertTimeFormat(dateString, format = "DD-MM-YYYY hh:mm A") {
    if (!dateString)
        return null;

    var momentDate = moment(dateString);
    var formattedDate = momentDate.format(format);

    return formattedDate;
}