$(function () {
    //#region Admin Form
    const $adminForm = $("#adminForm");
    const adminFormInputs = {
        groupId: $adminForm.find('select[name="groupId"]'),  // ⭐ 改成 select
        referral: $adminForm.find('input[name="referral"]'),
        fileNo: $adminForm.find('input[name="fileNo"]'),
    };
    //#endregion

    //#region Company Form 
    const $companyForm = $("#companyForm");
    const companyFormInputs = {
        clientId: $("#clientId"),
        companyName: $companyForm.find('input[name="companyName"]'),
        registrationNumber: $companyForm.find('input[name="regisNumber"]'),
        employerNumber: $companyForm.find('input[name="eNumber"]'),
        taxIdentificationNumber: $companyForm.find('input[name="tinNumber"]'),
        incorporationDate: $companyForm.find('input[name="incorpDate"]'),
        clientType: $companyForm.find('select[name="clientType"]'),
        yearEndMonth: $companyForm.find('input[name="yearEndMonth"]'),
        msicCodeIds: $companyForm.find('select[name="msicCodesIds"]'),
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
            msicCodesIds: {
                required: true,
                maxlength: 3
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
            msicCodesIds: {
                required: "Please select at least one MSIC code.",
                maxlength: "You can select up to 3 MSIC codes only."
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    //#endregion

    // 初始化 Select2
    initSelect2();

    // ⭐ 新增：加载 Groups（在 initSelect2 之后）
    loadGroupsForEdit();

    // Flatpickr 初始化
    flatpickr("#incorpDate", {
        allowInput: true
    });

    flatpickr("#yearEndMonth", {
        plugins: [
            new monthSelectPlugin({
                shorthand: false,
                dateFormat: "m",
                altFormat: "d-m (F)",
            })
        ],
        altInput: true,
        onChange: function (selectedDates, dateStr, instance) {
            if (!selectedDates.length) return;
            const selectedDate = selectedDates[0];
            var lastDay = getLastDay(selectedDate);
            if (selectedDate.getTime() === lastDay.getTime()) return;
            instance.setDate(lastDay, true);
        },
        onReady: function (selectedDates, dateStr, instance) {
            instance.calendarContainer.querySelector(".flatpickr-months").style["display"] = "none";

            if (!selectedDates.length) return;
            const selectedDate = selectedDates[0];
            var lastDay = getLastDay(selectedDate);
            if (selectedDate.getTime() === lastDay.getTime()) return;
            instance.setDate(lastDay, true);
        },
    });

    // 提交按钮
    $("#btnSubmitRequest").on("click", function () {
        if (!$companyForm.valid()) {
            return;
        }

        var adminInfo = getFormData(adminFormInputs);
        const companyData = getFormData(companyFormInputs);
        companyData.yearEndMonth = extractNumbers(companyData.yearEndMonth);
        companyData.categoryInfo = adminInfo;

        console.log('Submitting update data:', companyData);

        $.ajax({
            url: `${urls.edit_client}/company`,
            method: "POST",
            data: {
                req: companyData
            },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Updated", "Company Client saved successfully.");
                    window.location.href = `${urls.client}/${toCleanLower(res.clientType)}?id=${res.id}`;
                }
            },
            error: (res) => {
                console.error('Error updating client:', res);
                Toast_Fire(ICON_ERROR, "Error", "Failed to update client.");
            }
        });
    });

    // ⭐ 新增：加载 Groups 并预选当前 Group
    function loadGroupsForEdit() {
        const $groupSelect = $('select[name="groupId"]');
        const currentGroupId = $groupSelect.data('current-group-id');

        console.log('Loading groups for edit, current GroupId:', currentGroupId);

        $.ajax({
            url: '/api/groups/options',
            type: 'GET',
            success: function (res) {
                if (res && res.success && res.data) {
                    const groups = res.data;

                    // 清空现有选项（保留第一个空选项）
                    $groupSelect.find('option:not(:first)').remove();

                    // 添加 Groups
                    groups.forEach(function (group) {
                        const option = new Option(group.text, group.value, false, false);
                        $groupSelect.append(option);
                    });

                    // ⭐ 预选当前的 GroupId
                    if (currentGroupId) {
                        $groupSelect.val(currentGroupId).trigger('change');
                        console.log('Pre-selected GroupId:', currentGroupId);
                    } else {
                        // 如果没有 GroupId，触发一次 change 确保 Select2 更新
                        $groupSelect.trigger('change');
                    }

                    console.log('Loaded ' + groups.length + ' groups');
                } else {
                    console.error('Failed to load groups:', res?.message);
                    Toast_Fire(ICON_WARNING, "Warning", "Failed to load groups.");
                }
            },
            error: function (xhr) {
                console.error('Error loading groups:', xhr);
                Toast_Fire(ICON_ERROR, "Error", "Error loading groups: " + (xhr.statusText || 'Network error'));
            }
        });
    }
});