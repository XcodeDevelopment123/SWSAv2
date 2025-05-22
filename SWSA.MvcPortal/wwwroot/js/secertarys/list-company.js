$(function () {
    //#region init table and date
    const msicTable = $("#msicDataTable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    })
    msicTable.buttons().container().appendTo('#msicDataTable_wrapper .col-md-6:eq(0)');

    const ownerTable = $("#ownerTable").DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": true,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"],
        "pageLength": 5
    });
    ownerTable.buttons().container().appendTo('#ownerTable_wrapper .col-md-6:eq(0)');

    flatpickr("#incorpDate", {
        allowInput: true,
    });
    //#endregion init table and date

    //#region scroll event handle
    const $card = $('.select-company-card');
    const $parent = $card.parent();
    const offsetTop = $card.offset().top;

    $(window).on('scroll resize', function () {
        const scrollTop = $(window).scrollTop();
        const screenWidth = $(window).width();

        if (screenWidth < 768) {
            $card.removeClass('fixed').css('width', '');
            return;
        }

        if (scrollTop >= offsetTop) {
            if (!$card.hasClass('fixed')) {
                $card.addClass('fixed');
            }
            $card.css('width', $parent.width() + 'px');
        } else {
            $card.removeClass('fixed');
            $card.css('width', '');
        }
    });
    //#endregion scroll event handle

    let currentSelectCompanyId = 0;

    $(document).on('click', '.company-item', function (e) {
        const companyId = $(this).data("id");

        if (companyId === currentSelectCompanyId) {
            return
        }

        $(".company-item.active").removeClass("active");
        $(this).addClass("active");

        currentSelectCompanyId = companyId;

        startLoading();
        $.ajax({
            type: "GET",
            url: `${urls.companies}/${companyId}/secretary`,
            success: function (res) {

                console.log(res);
                //Load details
                stopLoading();

                $("#companyName").val(res.name);
                $("#companyType").val(res.companyType);
                $("#yearEndMonth").val(res.yearEndMonth);

                $("#incorpDate").val(ConvertTimeFormat(res.incorporationDate, "DD-MM-YYYY")).trigger("change");
                $("#regisNumber").val(res.registrationNumber);
                $("#eNumber").val(res.employerNumber);
                $("#tinNumber").val(res.taxIdentificationNumber);
                reloadMsic(res.msicCodes);
                reloadOwner(res.owners);
            },
            error: function () {
                $(".company-item.active").removeClass("active");
                currentSelectCompanyId = 0;
                stopLoading();
            }

        })

    })

    function reloadMsic(msicCodes) {
        msicTable.clear();
        msicCodes.forEach(item => {
            msicTable.row.add([
                item.code,
                item.description
            ]);
        });
        msicTable.draw();
    }

    function reloadOwner(owners) {
        ownerTable.clear();
        owners.forEach((item, index) => {
            ownerTable.row.add([
                index + 1,
                item.namePerIC,
                item.icOrPassportNumber,
                item.taxReferenceNumber,
                `<div>
                  <p class="m-0">${item.phoneNumber}</p>
                  <p class="m-0">${item.email}</p>
                </div>`
            ]);
        });
        ownerTable.draw();
    }

})
