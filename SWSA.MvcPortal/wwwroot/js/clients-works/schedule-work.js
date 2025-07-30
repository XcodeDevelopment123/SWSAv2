$(function () {
    let currentStep = 1;
    const totalSteps = 2;
    let selectedYear = null;
    let clientServices = [];
    let picList = [];
    let completedServices = new Set();

    // Service type definitions with reminder templates
    const serviceTypes = {
        Sec: {
            name: "Secretarial Services",
            code: "sec",
            icon: "bi-file-text",
            reminderTemplate: [
                {
                    label: "Anniversary Month Reminder",
                    method: "Email",
                    daysBefore: 7,
                    type: "AnniversaryReminder" // 替换原来的 "email"
                },
                {
                    label: "AGM & AFS Due Reminder",
                    method: "Email",
                    daysBefore: 180,
                    type: "AGMAFSDue" 
                },
            ],
        },
        Audit: {
            name: "Audit Services",
            code: "audit",
            icon: "bi-clipboard-check",
            reminderTemplate: [
                {
                    label: "Month 1 Email",
                    method: "Email",
                    daysBefore: 30,
                    type: "EmailNotification"
                },
                {
                    label: "Month 4 Reminder PIC",
                    method: "Call/Reminder",
                    daysBefore: 120,
                    type: "PhoneCallReminder"
                },
                {
                    label: "Month 6 Text PIC",
                    method: "Text",
                    daysBefore: 180,
                    type: "TextMessage"
                },
            ],
        },
        Tax: {
            name: "Tax Services",
            code: "tax",
            icon: "bi-calculator",
            reminderTemplate: [
                {
                    label: "Tax Planning Review",
                    method: "Email",
                    daysBefore: 7,
                    type: "TaxPlanning"
                },
                {
                    label: "Document Collection",
                    method: "Call/Email",
                    daysBefore: 3,
                    type: "DocumentCollection"
                },
                {
                    label: "Filing Preparation",
                    method: "Text/Call",
                    daysBefore: 1,
                    type: "FilingPreparation"
                },
                {
                    label: "Start Tax Work",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "StartWorkPreparation"
                },
            ],
        },
        Acc: {
            name: "Accounting Services",
            code: "acc",
            icon: "bi-journal-bookmark",
            reminderTemplate: [
                {
                    label: "Month 1 Email",
                    method: "Email",
                    daysBefore: 30,
                    type: "EmailNotification"
                },
                {
                    label: "Month 3 Reminder PIC",
                    method: "Call/Reminder",
                    daysBefore: 90,
                    type: "ProgressFollowUp"
                },
                {
                    label: "Month 4 Text PIC",
                    method: "Text",
                    daysBefore: 120,
                    type: "TextMessage"
                },
            ],
        },
        "Form E": {
            name: "Form E",
            code: "forme",
            icon: "bi-file-earmark-text",
            reminderTemplate: [
                {
                    label: "Form E Preparation",
                    method: "Email",
                    daysBefore: 90,
                    type: "FilingPreparation"
                },
                {
                    label: "Final Review",
                    method: "Call",
                    daysBefore: 1,
                    type: "FinalReview"
                },
                {
                    label: "Submit Form E",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
        "Form BE": {
            name: "Form BE",
            code: "formbe",
            icon: "bi-file-earmark-ruled",
            reminderTemplate: [
                {
                    label: "Form BE Preparation",
                    method: "Email",
                    daysBefore: 3,
                    type: "FilingPreparation"
                },
                {
                    label: "Final Review",
                    method: "Call",
                    daysBefore: 1,
                    type: "FinalReview"
                },
                {
                    label: "Submit Form BE",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
        "Form B & P": {
            name: "Form B & P",
            code: "formbp",
            icon: "bi-file-earmark-plus",
            reminderTemplate: [
                {
                    label: "Form B & P Preparation",
                    method: "Email",
                    daysBefore: 3,
                    type: "FilingPreparation"
                },
                {
                    label: "Final Review",
                    method: "Call",
                    daysBefore: 1,
                    type: "FinalReview"
                },
                {
                    label: "Submit Form B & P",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
        "PCB": {
            name: "PCB",
            code: "pcb",
            icon: "bi-building",
            reminderTemplate: [
                {
                    label: "PCB Setup",
                    method: "Email",
                    daysBefore: 5,
                    type: "StartWorkPreparation"
                },
                {
                    label: "Documentation Review",
                    method: "Call",
                    daysBefore: 2,
                    type: "DocumentCollection"
                },
                {
                    label: "PCB Processing",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
        "Monthly E Invoicing": {
            name: "Monthly E Invoicing",
            code: "einvoice",
            icon: "bi-receipt",
            reminderTemplate: [
                {
                    label: "E-Invoice Setup",
                    method: "Email",
                    daysBefore: 3,
                    type: "StartWorkPreparation"
                },
                {
                    label: "Monthly Processing",
                    method: "Call",
                    daysBefore: 1,
                    type: "ProgressFollowUp"
                },
                {
                    label: "E-Invoice Generation",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
        "Not Sec Filing Only": {
            name: "Not Sec Filing Only",
            code: "notsec",
            icon: "bi-file-minus",
            reminderTemplate: [
                {
                    label: "Filing Preparation",
                    method: "Email",
                    daysBefore: 3,
                    type: "FilingPreparation"
                },
                {
                    label: "Document Review",
                    method: "Call",
                    daysBefore: 1,
                    type: "DocumentCollection"
                },
                {
                    label: "Submit Filing",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
        "Review & Tax": {
            name: "Review & Tax",
            code: "reviewtax",
            icon: "bi-search",
            reminderTemplate: [
                {
                    label: "Review Planning",
                    method: "Email",
                    daysBefore: 7,
                    type: "TaxPlanning"
                },
                {
                    label: "Tax Assessment",
                    method: "Meeting",
                    daysBefore: 3,
                    type: "MeetingReminder"
                },
                {
                    label: "Final Review",
                    method: "Call",
                    daysBefore: 1,
                    type: "FinalReview"
                },
                {
                    label: "Complete Review",
                    method: "SystemNotification",
                    daysBefore: 0,
                    type: "SystemNotification"
                },
            ],
        },
    };
    function getClientServices(year) {
        return [
            {
                id: `sec_${year}`,
                name: "Annual Company Secretary Services",
                type: "Sec",
                description: `${year} secretarial compliance and filing`,
                departments: ["Secretary"],
            },
            {
                id: `audit_${year}`,
                name: "Annual Financial Audit",
                type: "Audit",
                description: `${year} statutory audit engagement`,
                departments: ["Audit"],
            },
            {
                id: `tax_${year}`,
                name: "Corporate Tax Services",
                type: "Tax",
                description: `${year} corporate tax compliance and planning`,
                departments: ["Tax"],
            },
            {
                id: `acc_${year}`,
                name: "Monthly Accounting Services",
                type: "Acc",
                description: `${year} bookkeeping and financial reporting`,
                departments: ["Account"],
            },
            {
                id: `forme_${year}`,
                name: "Form E Submission",
                type: "Form E",
                description: `${year} annual return filing`,
                departments: ["Secretary"],
            },
            {
                id: `formbe_${year}`,
                name: "Form BE Submission",
                type: "Form BE",
                description: `${year} Form BE filing`,
                departments: ["Secretary"],
            },
            {
                id: `formbp_${year}`,
                name: "Form B & P Submission",
                type: "Form B & P",
                description: `${year} Form B & P filing`,
                departments: ["Secretary"],
            },
            {
                id: `pcb_${year}`,
                name: "PCB Services",
                type: "PCB",
                description: `${year} PCB compliance and processing`,
                departments: ["Tax", "Account"],
            },
            {
                id: `einvoice_${year}`,
                name: "Monthly E Invoicing",
                type: "Monthly E Invoicing",
                description: `${year} electronic invoicing services`,
                departments: ["Account", "Tax"],
            },
            {
                id: `notsec_${year}`,
                name: "Not Sec Filing Only",
                type: "Not Sec Filing Only",
                description: `${year} non-secretarial filing services`,
                departments: ["Account", "Audit"],
            },
            {
                id: `reviewtax_${year}`,
                name: "Review & Tax",
                type: "Review & Tax",
                description: `${year} comprehensive review and tax services`,
                departments: ["Audit", "Tax"],
            },
        ];
    }

    function getPICList() {
        return $.ajax({
            url: `${urls.users}/pic`,
            method: "GET",
        });
    }

    function getWorkAllocs(id) {
        return $.ajax({
            url: `${urls.client}/${id}/work-allocs`,
            method: "GET",
        });
    }

    // Initialize modal
    $(document).on("click", ".btn-start-schedule", function () {
        initializeModal();
        $("#scheduleWorkModal").modal("show");
    });


    function initializeModal() {
        currentStep = 1;
        selectedYear = null;
        completedServices.clear();

        // Reset step indicator
        $(".step").removeClass("active completed");
        $('.step[data-step="1"]').addClass("active");

        // Show first step
        $(".step-content").removeClass("active");
        $("#step1").addClass("active");

        // Reset year selection
        $(".year-card").removeClass("selected");

        updateButtons();
    }

    // Year selection
    $(".year-card").on("click", function () {
        $(".year-card").removeClass("selected");
        $(this).addClass("selected");
        selectedYear = $(this).data("year");
    });

    // Step navigation
    $("#nextBtn").on("click", function () {
        if (validateCurrentStep()) {
            changeStep(1);
        }
    });

    $("#prevBtn").on("click", function () {
        changeStep(-1);
    });

    async function changeStep(direction) {
        // Update step indicator
        $(`.step[data-step="${currentStep}"]`).removeClass("active");
        if (direction === 1) {
            $(`.step[data-step="${currentStep}"]`).addClass("completed");
        }

        currentStep += direction;

        // Show new step
        $(".step-content").removeClass("active");
        $(`#step${currentStep}`).addClass("active");
        $(`.step[data-step="${currentStep}"]`).addClass("active");

        $("#nextBtn, #submitBtn").prop("disabled", false);
        // Load data for step 2 only forward
        if (currentStep === 2 && direction === 1) {
            await loadServicesForYear();
            $("#nextBtn, #submitBtn").prop("disabled", true);
        }
        updateButtons();
    }

    function validateCurrentStep() {
        switch (currentStep) {
            case 1:
                if (!selectedYear) {
                    alert("Please select an engagement year");
                    return false;
                }
                break;
            case 2:
                if (completedServices.size === 0) {
                    alert("Please configure at least one service engagement");
                    return false;
                }
                break;
        }
        return true;
    }

    function updateButtons() {
        $("#prevBtn").toggle(currentStep > 1);
        $("#nextBtn").toggle(currentStep < totalSteps);
        $("#submitBtn").toggle(currentStep === totalSteps);
    }

    async function loadServicesForYear() {
        const id = $("#clientId").val();

        try {
            const template = getClientServices(selectedYear);
            const data = await getWorkAllocs(id);
            const requiredService = data.map(x => x.serviceScope);
            clientServices = template.filter(item => requiredService.includes(item.type));
            picList = await getPICList();
            renderServices();
            updateProgress();

        } catch (err) {
            console.error("获取工作分配失败:", err);
            alert("获取数据失败，请稍后重试。");
        }
    }

    function renderServices() {
        const container = $("#servicesContainer");
        container.empty();

        clientServices.forEach((service, index) => {
            const serviceType = serviceTypes[service.type];
            const serviceHtml = createServiceCard(service, serviceType, index);
            container.append(serviceHtml);
        });

        $("#totalCount").text(clientServices.length);
        bindServiceEvents();
        initSelect2();

        flatpickr(".service-start-date", {
            allowInput: true,
            dateFormat: "Y-m-d",
            defaultDate: "today",
            minDate: `${selectedYear}-01-01`,
            maxDate: `${selectedYear}-12-31`,
        });
    }

    function createServiceCard(service, serviceType, index) {
        // Filter PICs based on service departments
        const filteredPics = picList.filter(pic =>
            service.departments.includes(pic.department) || pic.department === "Full"
        );
        const picOptions = filteredPics
            .map((pic) => {
                return `
                        <div class="col-lg-4 col-md-6 mb-3">
                            <div class="pic-selector" data-pic="${pic.staffId}" data-service="${service.id}" data-department="${pic.department}">
                                <div class="d-flex align-items-center">
                                    <div class="pic-avatar me-3">${pic.avatar}</div>
                                    <div class="flex-grow-1">
                                        <div class="fw-bold">${pic.name}</div>
                                        <div class="text-muted small">${pic.title}</div>
                                        <div class="text-muted small">${pic.experience} | ${pic.department.charAt(0).toUpperCase() + pic.department.slice(1)}</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    `;
            })
            .join("");

        // Add message if no PICs available for this service
        const noPicsMessage = filteredPics.length === 0 ?
            '<div class="col-12"><div class="alert alert-warning">No personnel available for this service type</div></div>' : '';
        return `
                    <div class="service-setup-card" data-service-id="${service.id}">
                        <div class="service-header ${serviceType.code}">
                            <div class="complete-indicator">
                                <i class="bi bi-check"></i>
                            </div>
                            <div class="d-flex align-items-center">
                                <div class="service-icon me-4">
                                    <i class="${serviceType.icon}"></i>
                                </div>
                                <div>
                                    <h5 class="mb-1">${service.name}</h5>
                                    <p class="mb-0 opacity-75">${service.description}</p>
                                </div>
                            </div>
                        </div>
                        
                        <div class="card-body service-item">
                            <div class="row spacing-y" style="padding: 0 1rem;">
                                <div class="col-md-8">
                                    <h6><i class=" bi bi-person-check me-2"></i>Assign Person-in-Charge</h6>
                                    <div class="mb-2 small text-muted">
                                        <i class="bi bi-info-circle me-1"></i>
                                        Eligible departments: ${service.departments.map(d => d.charAt(0).toUpperCase() + d.slice(1)).join(', ')}
                                    </div>
                                    <div class="row" id="pic-container-${service.id}">
                                        ${picOptions}
                                        ${noPicsMessage}
                                    </div>
                                </div>
                                
                                <div class="col-md-4" >
                                    <h6><i class="bi bi-calendar-event me-2"></i>Schedule Details</h6>
                                    <div class="mb-3">
                                        <label class="form-label">Target Start Date</label>
                                        <input type="date" class="form-control service-start-date" data-service="${service.id}">
                                    </div>
                                    <div class="mb-3">
                                        <label class="form-label">Priority</label>
                                        <select class="form-select service-priority select2" data-service="${service.id}">
                                            <option value="None">None</option>
                                            <option value="Low">Low</option>
                                            <option value="Medium" selected>Medium</option>
                                            <option value="High">High</option>
                                            <option value="Urgent">Urgent</option>
                                        </select>
                                    </div>

                                           <div class="mb-3">
                                        <label class="form-label">Remarks</label>
                                        <input type="text" class="form-control service-remarks" placeholder="(optional)" data-service="${service.id}">
                                    </div>
                                </div>
                                
                                <div class="col-md-12">
                                    <h6><i class="bi bi-bell me-2"></i>Reminder Timeline (preview)</h6>
                                    <div class="reminder-preview">
                                        <div class="reminder-preview-content" id="preview-${service.id}">
                                            <div class="text-muted small">Set target start date to preview reminder timeline</div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            
                            <!-- Completion Status -->
                            <div class="spacing-y">
                                <div class="d-flex justify-content-between align-items-center p-3 bg-light rounded">
                                    <div>
                                        <span class="text-muted">Status: </span>
                                        <span class="config-status fw-bold" data-service="${service.id}">Not Configured</span>
                                    </div>
                                    <button type="button" class="btn btn-outline-success mark-complete" 
                                            data-service="${service.id}" disabled>
                                        <i class="bi bi-check-circle me-2"></i>Mark as Complete
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                `;
    }

    function bindServiceEvents() {
        // PIC selection
        $(".pic-selector").on("click", function () {
            const serviceId = $(this).data("service");
            const picDepartment = $(this).data("department");
            const picId = $(this).data("pic");

            // Find the service to validate department
            const service = clientServices.find(s => s.id === serviceId);

            if (!service.departments.includes(picDepartment) == picDepartment === "Full") {
                alert(`This person is from ${picDepartment} department and cannot be assigned to this service.\nEligible departments: ${service.departments.join(', ')}`);
                return;
            }

            $(`#pic-container-${serviceId} .pic-selector`).removeClass("selected");
            $(this).addClass("selected");

            updateServiceStatus(serviceId);
            updateReminderPreview(serviceId);
        });

        // Date and other changes
        $(".service-start-date, .service-priority").on("change", function () {
            const serviceId = $(this).data("service");
            updateServiceStatus(serviceId);
            updateReminderPreview(serviceId);
        });

        // Mark complete
        $(".mark-complete").on("click", function () {
            const serviceId = $(this).data("service");
            const $card = $(`.service-setup-card[data-service-id="${serviceId}"]`);
            if (completedServices.has(serviceId)) {
                completedServices.delete(serviceId);
                $card.removeClass("completed");
                $(this).html('<i class="bi bi-check-circle me-2"></i>Mark as Complete');
                $(this).removeClass("btn-success").addClass("btn-outline-success");
                $(`.service-start-date[data-service='${serviceId}'],.service-remarks[data-service='${serviceId}']`).prop("disabled", false);
                $(`.service-priority[data-service='${serviceId}']`).prop("disabled", false).trigger("change");
            } else {
                completedServices.add(serviceId);
                $card.addClass("completed");
                $(this).html('<i class="bi bi-check-circle-fill me-2"></i>Completed');
                $(this).removeClass("btn-outline-success").addClass("btn-success");
                $(`.service-start-date[data-service='${serviceId}'],.service-remarks[data-service='${serviceId}']`).prop("disabled", true);
                $(`.service-priority[data-service='${serviceId}']`).prop("disabled", true).trigger("change");
                // Auto scroll to next uncompleted service
                scrollToNextService(serviceId);
            }
            updateProgress();

            if (completedServices.size === clientServices.length) {
                setTimeout(() => {
                    $("#nextBtn, #submitBtn").prop("disabled", false);
                }, 500);
            } else {

                $("#nextBtn, #submitBtn").prop("disabled", true);
            }
        });
    }

    function scrollToNextService(currentServiceId) {
        // Find the next service that is not completed
        const currentIndex = clientServices.findIndex((s) => s.id === currentServiceId);
        if (currentIndex === -1) return; // Current service not found


        const nextIndex = currentIndex + 1 >= clientServices.length ? clientServices.length - 1 : currentIndex + 1;
        const nextServiceId = clientServices[nextIndex].id;

        if (!completedServices.has(nextServiceId)) {
            const $nextCard = $(`.service-setup-card[data-service-id="${nextServiceId}"]`);
            if ($nextCard.length > 0) {
                // Smooth scroll to the next service with some offset
                const $modalBody = $("#scheduleWorkModal .modal-body");
                const scrollTop = $nextCard.position().top + $modalBody.scrollTop();

                $modalBody.animate(
                    {
                        scrollTop: scrollTop,
                    },
                    600,
                    "swing"
                );

                // Optional: Add a subtle highlight effect
                $nextCard.addClass("border-primary");
                setTimeout(() => {
                    $nextCard.removeClass("border-primary");
                }, 1500);

                return;
            }
        }
    }

    function updateServiceStatus(serviceId) {
        const picSelected = $(`#pic-container-${serviceId} .pic-selector.selected`).length > 0;
        const dateSet = $(`.service-start-date[data-service="${serviceId}"]`).val() !== "";

        const isConfigured = picSelected && dateSet;
        const $button = $(`.mark-complete[data-service="${serviceId}"]`);
        const $status = $(`.config-status[data-service="${serviceId}"]`);

        if (isConfigured) {
            $button.prop("disabled", false);
            $status.text("Configured").addClass("text-success").removeClass("text-danger");
        } else {
            $button.prop("disabled", true);
            $status.text("Not Configured").addClass("text-danger").removeClass("text-success");

            // If it was completed but now not configured, remove from completed
            if (completedServices.has(serviceId)) {
                completedServices.delete(serviceId);
                $(`.service-setup-card[data-service-id="${serviceId}"]`).removeClass("completed");
                $button.html('<i class="bi bi-check-circle me-2"></i>Mark as Complete');
                $button.removeClass("btn-success").addClass("btn-outline-success");
                updateProgress();
            }
        }
    }

    function updateReminderPreview(serviceId) {
        const startDate = $(`.service-start-date[data-service="${serviceId}"]`).val();
        const $preview = $(`#preview-${serviceId}`);

        if (!startDate) {
            $preview.html('<div class="text-muted small">Set start date to preview reminder timeline</div>');
            return;
        }

        const service = clientServices.find((s) => s.id === serviceId);
        const serviceType = serviceTypes[service.type];
        const start = new Date(startDate);

        let previewHtml = "";

        // Generate service-specific reminders based on template
        serviceType.reminderTemplate.forEach((reminder, index) => {
            const reminderDate = new Date(start);
            reminderDate.setDate(start.getDate() - reminder.daysBefore);

            const dateStr = reminderDate.toLocaleDateString("en-GB");
            const timeStr = "09:00"; // Default time

            previewHtml += `
              <div class="reminder-item ${reminder.type}">
                <div class="d-flex justify-content-between align-items-center" style="width:100%">
                  <div>
                    <div class="reminder-label">${reminder.label}</div>
                    <div class="reminder-method">${reminder.method}</div>
                  </div>
                  <div class="text-muted large">
                    <i class="bi bi-calendar me-1"></i>${dateStr} ${timeStr}
                  </div>
                </div>
              </div>
            `;
        });

        $preview.html(previewHtml);
    }

    function updateProgress() {
        const completed = completedServices.size;
        const total = clientServices.length;
        const percentage = total > 0 ? (completed / total) * 100 : 0;

        $(".progress-bar").css("width", `${percentage}%`);
        $("#completedCount").text(completed);
    }

    // Submit all engagements
    $("#submitBtn").on("click", function () {
        const results = [];

        completedServices.forEach((serviceId) => {
            const service = clientServices.find((s) => s.id === serviceId);
            const serviceType = serviceTypes[service.type];
            const picId = $(`#pic-container-${serviceId} .pic-selector.selected`).data("pic");
            const pic = picList.find((p) => p.staffId === picId);
            const startDate = $(`.service-start-date[data-service="${serviceId}"]`).val();
            const priority = $(`.service-priority[data-service="${serviceId}"]`).val();
            const remarks = $(`.service-remarks[data-service="${serviceId}"]`).val();
            results.push({
                //   serviceId: serviceId,
                //   serviceName: service.name,
                serviceScope: serviceType.name,
                //  pic: pic.name,
                picId: pic.staffId,
                startDate: startDate,
                priority: priority,
                reminders: serviceType.reminderTemplate,
                remarks: remarks
            });
        });

        const req = {
            year: selectedYear,
            engagements: results,
            clientId: $("#clientId").val(),
        };
        console.log("Submitted engagement data:", req);

        $.ajax({
            url: `${urls.client_work_alloc}/schedule`,
            method: "POST",
            data: JSON.stringify(req),
            contentType:"application/json",
            success: function (response) {
                console.log(response)
                Toast_Fire(ICON_SUCCESS, `Work for year ${req.year} scheduled successfully!`, "success");
                $("#scheduleWorkModal").modal("hide");
            },
            error: function () { }
        })


    });

    // Reset modal on close
    $("#scheduleWorkModal").on("hidden.bs.modal", function () {
        initializeModal();
        $("#nextBtn, #submitBtn").prop("disabled", false);
        $("#globalCustomReminders").empty();
    });

    // Set default date to tomorrow
    function setDefaultDates() {
        const tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        const defaultDate = tomorrow.toISOString().split("T")[0];
        $(".service-start-date").val(defaultDate);
    }

    setDefaultDates();

});
