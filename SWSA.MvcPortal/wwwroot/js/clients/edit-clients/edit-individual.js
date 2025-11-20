$(function () {
    //#region Admin Form
    const $adminForm = $("#adminForm");
    const adminFormInputs = {
        groupId: $adminForm.find('select[name="groupId"]'),  // ⭐ 改成 select
        referral: $adminForm.find('input[name="referral"]')
    };
    //#endregion

    //#region Individual Form 
    const $individualForm = $("#individualForm");
    const individualFormInputs = {
        clientId: $("#clientId"),
        individualName: $individualForm.find('input[name="name"]'),
        taxIdentificationNumber: $individualForm.find('input[name="tinNumber"]'),
        professions: $individualForm.find('input[name="professions"]'),
        icOrPassportNumber: $individualForm.find('input[name="icOrPassport"]'),
        yearEndMonth: $individualForm.find('input[name="yearEndMonth"]'),
        clientType: $individualForm.find('select[name="clientType"]')
    };

    $individualForm.validate({
        rules: {
            name: {
                required: true
            },
            tinNumber: {
                required: true
            },
            professions: {
                required: true
            },
            icOrPassport: {
                required: true
            },
            yearEndMonth: {
                required: true
            },
            clientType: {
                required: true
            }
        },
        messages: {
            name: {
                required: "Individual Name is required."
            },
            tinNumber: {
                required: "Tax Identification Number is required."
            },
            professions: {
                required: "Profession is required."
            },
            icOrPassport: {
                required: "IC or Passport Number is required."
            },
            yearEndMonth: {
                required: "Year End Month is required."
            },
            clientType: {
                required: "Client Type is required."
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
        highlight: function (element, errorClass, validClass) {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).removeClass('is-invalid');
        }
    });
    //#endregion
    initSelect2();

    loadGroupsForEdit();

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

    $("#btnSubmitRequest").on("click", function () {
        if (!$individualForm.valid()) {
            return;
        }
        var adminInfo = getFormData(adminFormInputs);
        const data = getFormData(individualFormInputs);
        data.yearEndMonth = extractNumbers(data.yearEndMonth);
        data.categoryInfo = adminInfo;
        $.ajax({
            url: `${urls.edit_client}/individual`,
            method: "POST",
            data: {
                req: data
            },
            success: function (res) {
                if (res) {
                    Toast_Fire(ICON_SUCCESS, "Updated", "Individual Client saved successfully.");
                    window.location.href = `${urls.client}/individual?id=${res.id}`;
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