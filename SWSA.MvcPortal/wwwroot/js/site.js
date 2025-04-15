// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const urls = {
    "auth": "/Auth",
    "companies": "/companies",
    "company_owner": "/companies/owner",
    "company_official_contact": "/companies/official-contact",
    "company_staffs": "/companies/staffs",
    "company_compliance_date": "/companies/compliance-date",
    "company_documents": "/companies/docs",
    "company_works": "/companies/works",
    "company_work_progress": "/companies/works/progress",
    "users": "/users",
};

// example use
/**
 *  
 * $.ajax({
    url: "/api/example",
    method: "POST",
    manualLoading: true, // ✅ 明确声明：我自己控制 loading
    beforeSend: function () {
        startLoading(); // 手动控制开始
    },
    complete: function () {
        stopLoading(); // 手动控制结束
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

        // 针对 select[multiple] 处理数组，其它默认用 .val()
        if ($input.is('select[multiple]')) {
            formData[key] = $input.val() || []; // 确保 null 转为 []
        }
        else if ($input.is('input[type="file"]')) {
            const file = $input[0].files[0];
            formData[key] = file ?? null;
        }
        else {
            formData[key] = $input.val();
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
