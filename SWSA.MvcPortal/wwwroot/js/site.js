// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.
const urls = {
    "auth": "/Auth",
    "client": "/clients",
    "client_comm": "/clients/comm-contact",
    "client_official": "/clients/official-contact",
    "client_cp_owner": "/clients/company-owner",
    "client_work_alloc": "/clients/work-alloc",

    "new_client": "/clients/create",
    "edit_client": "/clients/edit",
    "companies": "/companies",
    "company_owner": "/companies/owner",
    "company_official_contact": "/companies/official-contact",
    "company_cm_contact": "/companies/cm-contact",
    "company_compliance_date": "/companies/compliance-date",
    "documents": "/documents",
    "company_works": "/companies/works",
    "company_work_progress": "/companies/works/progress",
    "company_handle_user": "/companies/handle-users",
    "schedule_job": "/scheduler-jobs",
    "system_audit_log": "/sys-audit-logs",
    "secretary_dept": "/secretary-dept",
    "secretary_dept_template": "/secretary-dept/template",
    "secretary_dept_strike_off": "/secretary-dept/strike-off-template",
    "secretary_dept_submission": "/secretary-dept/submissions",
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
            } else if ($input.is('input[type="checkbox"]')) {
                formData[key] = $input.prop("checked");
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

function getLastDay(date) {
    const year = date.getFullYear();
    const month = date.getMonth() + 1;

    const lastDay = new Date(year, month, 0);

    return lastDay;
}

function ConvertTimeFormat(dateString, format = "DD-MM-YYYY hh:mm A") {
    if (!dateString)
        return null;

    var momentDate = moment(dateString);
    var formattedDate = momentDate.format(format);

    return formattedDate;
}

function formatMonthLabel(monthNumber) {
    const month = parseInt(monthNumber);
    if (isNaN(month) || month < 1 || month > 12) {
        return monthNumber;
    }

    const monthNames = ['January', 'February', 'March', 'April', 'May', 'June',
        'July', 'August', 'September', 'October', 'November', 'December'];

    const date = new Date(1990, monthNumber - 1, 1);
    const lastDay = getLastDay(date);

    const monthName = monthNames[month - 1];

    return `${ConvertTimeFormat(lastDay, 'DD')} ${monthName}`;
}

function throttle(fn, wait) {
    let last = 0, timeout;
    return function (...args) {
        const now = Date.now();
        if (now - last > wait) {
            last = now;
            fn.apply(this, args);
        } else {
            clearTimeout(timeout);
            timeout = setTimeout(() => {
                last = Date.now();
                fn.apply(this, args);
            }, wait);
        }
    };
}

function debounce(fn, delay) {
    let timer = null;
    return function (...args) {
        clearTimeout(timer);
        timer = setTimeout(() => fn.apply(this, args), delay);
    };
}

function adjustResponsiveTables() {
    $('.table-responsive').each(function () {

        const $thead = $(this).find('thead').first();
        const theadWidth = $thead.length ? $thead.get(0).getBoundingClientRect().width : 0;

        const $card = $(this).closest('.card');
        if ($card.length === 0) return;
        const cardWidth = $card.get(0).getBoundingClientRect().width;

        if (cardWidth > theadWidth) {
            $(this).css('display', 'table');
        } else {
            $(this).css('display', 'block');
        }
    });
}

function reorderSelect2OptionsByText(selectElement) {
    const $select = $(selectElement);
    const options = $select.find('option').detach().get();

    const firstOption = options.shift();

    const enabledSorted = options
        .filter(opt => !opt.disabled)
        .sort((a, b) => a.text.localeCompare(b.text));

    const disabledSorted = options
        .filter(opt => opt.disabled)
        .sort((a, b) => a.text.localeCompare(b.text));

    $select.append([firstOption, ...enabledSorted, ...disabledSorted]);
    $select.trigger('change.select2');
}

function tableResizeEventListener() {
    window.addEventListener('resize', throttle(adjustResponsiveTables, 150));
    window.addEventListener('resize', debounce(adjustResponsiveTables, 250));
    window.addEventListener('load', adjustResponsiveTables);
}


function selectByDisplayText(selectId, displayText) {
    const option = $(`#${selectId} option`).filter(function () {
        return $(this).text().trim() === displayText;
    }).first();

    if (option.length) {
        $(`#${selectId}`).val(option.val()).trigger("change");
    }
}

function toCleanLower(str) {
    return String(str).toLowerCase().replace(/\s+/g, '');
}