/**
 * Bulk import clients from Excel
 * Works across B1 (Sdn Bhd), B2 (LLP), and B3 (Enterprise).
 * Parses the Excel file in the browser (SheetJS), previews the rows,
 * then posts them to /clients/create/{type}/import.
 */
$(function () {
    if (typeof XLSX === "undefined") {
        console.error("SheetJS (XLSX) not loaded - Excel import disabled.");
        $("#importCard").find("button, label").prop("disabled", true).addClass("disabled");
        return;
    }

    // Determine current client profile from URL
    const path = window.location.pathname.toLowerCase();
    let clientTypeKey = "sdnbhd";
    let clientTypeName = "Sdn Bhd";
    let exampleCompanyName = "ABC Trading Sdn. Bhd.";

    if (path.includes("/llp")) {
        clientTypeKey = "llp";
        clientTypeName = "LLP";
        exampleCompanyName = "ABC PLT";
    } else if (path.includes("/enterprise")) {
        clientTypeKey = "enterprise";
        clientTypeName = "Enterprise";
        exampleCompanyName = "ABC Enterprise";
    }

    // Template columns (header text -> request field)
    const TEMPLATE_COLUMNS = [
        { header: "Company Name*", field: "companyName", example: exampleCompanyName },
        { header: "Registration Number*", field: "registrationNumber", example: "202501012345" },
        ...(clientTypeKey === "enterprise"
            ? [{ header: "Company Type", field: "companyType", example: "Sole Proprietorships" }]
            : []),
        { header: "Employer Number", field: "employerNumber", example: "E 1234567890" },
        { header: "TIN", field: "taxIdentificationNumber", example: "C 12345678901" },
        { header: "Incorporation Date", field: "incorporationDate", example: "2025-01-15" },
        { header: "Year End Month", field: "yearEndMonth", example: "12" },
        { header: "Activity Size", field: "activitySize", example: "D" },
        { header: "MSIC Codes", field: "msicCodes", example: "46100, 47190" },
        { header: "Group", field: "group", example: "" },
        { header: "Referral", field: "referral", example: "" },
        { header: "File No", field: "fileNo", example: "F-001" },
    ];

    // Accept common header variations (normalized: lowercase, letters/digits only)
    const HEADER_ALIASES = {
        companyName: ["companyname", "company", "name", "clientname"],
        registrationNumber: [
            "registrationnumber", "registrationno", "regno", "regnumber", "regisnumber",
            "ssmno", "ssm", "ssmnumber", "rocno", "roc",
            "companyno", "companynumber", "compno", "compnumber", "coyno", "cono", "companyregno", "companyregistrationno", "companyregistrationnumber"
        ],
        companyType: ["companytype", "type", "businesstype"],
        employerNumber: ["employernumber", "employerno", "enumber", "eno"],
        taxIdentificationNumber: ["tin", "taxidentificationnumber", "taxno", "incometaxno", "tinnumber"],
        incorporationDate: ["incorporationdate", "incorpdate", "dateofincorporation", "doi"],
        yearEndMonth: ["yearendmonth", "yearend", "fye", "financialyearend", "yem"],
        activitySize: ["activitysize", "activity", "activitylevel", "size"],
        msicCodes: ["msiccodes", "msiccode", "msic"],
        group: ["group", "groupname"],
        referral: ["referral", "referralname", "referredby"],
        fileNo: ["fileno", "filenumber", "file"],
    };

    const normalize = (s) => String(s ?? "").toLowerCase().replace(/[^a-z0-9]/g, "");
    const escapeHtml = (s) => String(s ?? "")
        .replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;").replace(/'/g, "&#39;");

    const headerToField = {};
    Object.entries(HEADER_ALIASES).forEach(([field, aliases]) =>
        aliases.forEach(a => headerToField[a] = field));

    const $file = $("#importFile");
    const $fileName = $("#importFileName");
    const $btnImport = $("#btnStartImport");
    const $btnClear = $("#btnClearImport");
    const $count = $("#importCount");
    const $summary = $("#importSummary");
    const $previewWrap = $("#importPreviewWrap");
    const $tbody = $("#importPreviewTable tbody");

    let parsedRows = [];

    // ===== Download template =====
    $("#btnDownloadTemplate").on("click", function () {
        const wb = XLSX.utils.book_new();

        const data = [
            TEMPLATE_COLUMNS.map(c => c.header),
            TEMPLATE_COLUMNS.map(c => c.example),
        ];
        const ws = XLSX.utils.aoa_to_sheet(data);
        ws["!cols"] = TEMPLATE_COLUMNS.map(c => ({ wch: Math.max(c.header.length, 18) }));
        const sheetName = clientTypeName.replace(/\s+/g, "");
        XLSX.utils.book_append_sheet(wb, ws, sheetName);

        const notes = [
            ["Column", "Required", "Notes"],
            ["Company Name", "Yes", "Full company name"],
            ["Registration Number", "Yes", "SSM registration number. Format cell as Text to keep leading zeros."],
            ...(clientTypeKey === "enterprise"
                ? [["Company Type", "No", "Sole Proprietorships or Partnership (defaults to Sole Proprietorships)"]]
                : []),
            ["Employer Number", "No", ""],
            ["TIN", "No", "Tax Identification Number"],
            ["Incorporation Date", "No", "Excel date, or yyyy-mm-dd / dd/mm/yyyy"],
            ["Year End Month", "No", "1-12, month name (Dec / December / Mac) or a date (31/12/2025)"],
            ["Activity Size", "No", "D, 1, 2, 3, 4, 5 or 6 (default D)"],
            ["MSIC Codes", "No", "Up to 3 codes, separated by comma. e.g. 46100, 47190"],
            ["Group", "No", "Must match an existing ACTIVE Group name exactly"],
            ["Referral", "No", ""],
            ["File No", "No", ""],
            [],
            [`Rows with the same Company Name + Registration Number as an existing ${clientTypeName} client will be skipped.`],
        ];
        const wsNotes = XLSX.utils.aoa_to_sheet(notes);
        wsNotes["!cols"] = [{ wch: 22 }, { wch: 10 }, { wch: 80 }];
        XLSX.utils.book_append_sheet(wb, wsNotes, "Instructions");

        XLSX.writeFile(wb, `${sheetName}_Import_Template.xlsx`);
    });

    // ===== Choose file =====
    $file.on("change", function () {
        const file = this.files && this.files[0];
        if (!file) return;

        $fileName.text(file.name).removeClass("text-muted");
        $summary.addClass("d-none").empty();

        const reader = new FileReader();
        reader.onload = (e) => {
            try {
                const wb = XLSX.read(new Uint8Array(e.target.result), { type: "array" });
                const ws = wb.Sheets[wb.SheetNames[0]];
                const matrix = XLSX.utils.sheet_to_json(ws, { header: 1, raw: true, defval: "", blankrows: false });
                parsedRows = mapRows(matrix);

                if (parsedRows.length === 0) {
                    Popup_Warning("No data", "No data rows found. Please check the file uses the template headers.");
                    resetImport(false);
                    return;
                }
                renderPreview(parsedRows);
            } catch (err) {
                console.error(err);
                Popup_Error("Invalid file", "Unable to read this file. Please upload a valid Excel file.");
                resetImport();
            }
        };
        reader.readAsArrayBuffer(file);
    });

    function mapRows(matrix) {
        if (!matrix || matrix.length < 2) return [];

        let headerIdx = matrix.findIndex(r => r.some(c => headerToField[normalize(c)] === "companyName"));
        if (headerIdx < 0) headerIdx = 0;

        const colMap = {};
        matrix[headerIdx].forEach((h, i) => {
            const field = headerToField[normalize(h)];
            if (field && colMap[field] === undefined) colMap[field] = i;
        });

        if (colMap.companyName === undefined) {
            Popup_Warning("Header not found", "Cannot find the 'Company Name' column. Please use the template.");
            return [];
        }

        const rows = [];
        for (let r = headerIdx + 1; r < matrix.length; r++) {
            const line = matrix[r];
            const row = { rowNumber: r + 1 };
            let hasValue = false;
            Object.keys(HEADER_ALIASES).forEach(field => {
                const idx = colMap[field];
                let v = idx === undefined ? "" : line[idx];
                v = (v === null || v === undefined) ? "" : String(v).trim();
                if (v) hasValue = true;
                row[field] = v;
            });
            if (hasValue) rows.push(row);
        }
        return rows;
    }

    function displayDate(v) {
        if (/^\d+(\.\d+)?$/.test(v) && Number(v) > 59 && Number(v) < 2958466) {
            const d = XLSX.SSF.parse_date_code(Number(v));
            if (d) return `${d.y}-${String(d.m).padStart(2, "0")}-${String(d.d).padStart(2, "0")}`;
        }
        return v;
    }

    function renderPreview(rows) {
        $tbody.empty();
        rows.forEach(r => {
            const missing = !r.companyName || !r.registrationNumber;
            $tbody.append(`
                <tr data-row="${r.rowNumber}" class="${missing ? "table-warning" : ""}">
                    <td>${r.rowNumber}</td>
                    <td class="import-status">${missing
                    ? '<span class="badge badge-warning">Missing required</span>'
                    : '<span class="badge badge-secondary">Pending</span>'}</td>
                    <td>${escapeHtml(r.companyName)}</td>
                    <td>${escapeHtml(r.registrationNumber)}</td>
                    <td>${escapeHtml(r.employerNumber)}</td>
                    <td>${escapeHtml(r.taxIdentificationNumber)}</td>
                    <td>${escapeHtml(displayDate(r.incorporationDate))}</td>
                    <td>${escapeHtml(r.yearEndMonth.length > 2 ? displayDate(r.yearEndMonth) : r.yearEndMonth)}</td>
                    <td>${escapeHtml(r.activitySize)}</td>
                    <td>${escapeHtml(r.msicCodes)}</td>
                    <td>${escapeHtml(r.group)}</td>
                    <td>${escapeHtml(r.referral)}</td>
                    <td>${escapeHtml(r.fileNo)}</td>
                </tr>`);
        });

        $count.text(rows.length);
        $previewWrap.removeClass("d-none");
        $btnImport.removeClass("d-none").prop("disabled", false);
        $btnClear.removeClass("d-none");
    }

    // ===== Import =====
    $btnImport.on("click", async function () {
        if (!parsedRows.length) return;

        const confirm = await Popup_Questions(
            `Import ${clientTypeName} clients?`,
            `${parsedRows.length} row(s) will be imported as ${clientTypeName} clients.`,
            true, "Yes, import", "Cancel");
        if (!confirm.isConfirmed) return;

        const $btn = $(this);
        const original = $btn.html();
        $btn.prop("disabled", true).html('<i class="fas fa-spinner fa-spin mr-1"></i> Importing...');
        $btnClear.prop("disabled", true);

        function safeStopLoading() {
            if (typeof stopLoading === "function") {
                stopLoading();
            }
            $("#globalLoadingOverlay").addClass("d-none");
            $("body").removeClass("no-pointer-events");
        }

        $.ajax({
            url: `${urls.new_client}/${clientTypeKey}/import`,
            method: "POST",
            contentType: "application/json",
            data: JSON.stringify({ rows: parsedRows }),
            success: function (res) {
                safeStopLoading();
                renderResults(res);
                $btn.addClass("d-none").prop("disabled", false).html(original);
                if (res.failedCount === 0) {
                    Popup_Success("Import completed", `${res.successCount} ${clientTypeName} client(s) created successfully.`);
                } else {
                    Popup_Warning("Import completed with errors",
                        `${res.successCount} created, ${res.failedCount} failed. Check the table for details.`);
                }
            },
            error: function (xhr) {
                safeStopLoading();
                console.error("Import error:", xhr);
                const msg = xhr.responseJSON?.message || xhr.responseText || "Import failed.";
                Popup_Error("Import failed", msg);
                $btn.prop("disabled", false).html(original);
            },
            complete: function () {
                safeStopLoading();
                $btnClear.prop("disabled", false);
            }
        });
    });

    function renderResults(res) {
        (res.results || []).forEach(r => {
            const $tr = $tbody.find(`tr[data-row="${r.rowNumber}"]`);
            $tr.removeClass("table-warning table-success table-danger")
                .addClass(r.success ? "table-success" : "table-danger");

            const $status = $tr.find(".import-status");
            if (r.success) {
                $status.html('<span class="badge badge-success">Created</span>');
            } else {
                $status.html(`
                    <span class="badge badge-danger">Failed</span>
                    <div class="small text-danger mt-1">${escapeHtml(r.message || "Failed")}</div>
                `);
            }
        });

        const alertClass = res.failedCount === 0 ? "alert-success" : "alert-warning";
        $summary.removeClass("d-none")
            .html(`
                <div class="alert ${alertClass} mb-0">
                    <strong>Import result:</strong> Total ${res.total} |
                    <span class="text-success font-weight-bold">Success: ${res.successCount}</span> |
                    <span class="text-danger font-weight-bold">Failed: ${res.failedCount}</span>
                </div>
            `);
    }

    // ===== Clear / Reset =====
    $btnClear.on("click", function () {
        resetImport(true);
    });

    function resetImport(clearInput = true) {
        parsedRows = [];
        if (clearInput) $file.val("");
        $fileName.text("No file selected").addClass("text-muted");
        $tbody.empty();
        $previewWrap.addClass("d-none");
        $btnImport.addClass("d-none").prop("disabled", false);
        $btnClear.addClass("d-none");
        $summary.addClass("d-none").empty();
        $count.text("0");
    }
});
