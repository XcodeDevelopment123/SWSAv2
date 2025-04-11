$(function () {

    var companyTargetUrl = null;

    $(document).on('click', '.open-company-modal', function (e) {
        e.preventDefault();
        companyTargetUrl = $(this).data('target-url');
        var urlName = $(this).data('url-name');

        $(".target-url-name").text(urlName);
        $('#companyModal').modal('show');
    });

    $("#companySelector").on("change", function () {
        const id = $(this).val();
        $(".select-company-btn").data("id", id);
    })

    $(document).on('click', '.select-company-btn', function () {
        var companyId = $(this).data('id');

        if (!companyId) {
            Toast_Fire(ICON_ERROR, "Something went wrong", "Please contact support.");
            return;
        }

        if (companyTargetUrl) {
            var finalUrl = companyTargetUrl.replace('{companyId}', companyId);
   
            window.location.href = finalUrl;
        }
    });

    $('.select2.modal').each(function () {
        $(this).select2({
            theme: 'bootstrap4',
            dropdownParent: $('#companyModal')
        });
    });

    $(".dismiss-btn-selector-company").on("click", function () {
        $('#companyModal').modal('hide');

    })

    $(document).on('select2:open', function (e) {
        const $select = $(e.target);

        if ($select.hasClass('modal')) {
            // 动态加 class 到 container
            const $container = $('.select2-container--open');
            $container.addClass('modal-select2');
        }
    });
})