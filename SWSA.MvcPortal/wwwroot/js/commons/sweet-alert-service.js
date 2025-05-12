const ICON_SUCCESS = "success";
const ICON_ERROR = "error";
const ICON_INFO = "info";
const ICON_WARNING = "warning";
const ICON_QUESTION = "question";

const ConfirmBtnColor = " #566FC9";
const CancelBtnColor = "#d33";

const confirmButton = 'btn btn-primary me-3 waves-effect waves-light';
const cancelButton = 'btn btn-label-danger waves-effect waves-light';


const Toast = Swal.mixin({
    toast: true,
    position: "top-end",
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
    },
    didClose: (toast) => {
        $("#toast-block").hide();
    }
});

function Popup_Success(title, text, btnText) {
    return Swal.fire({
        title: title ?? "Successfully!",
        text: text ?? "The action has completed.",
        icon: ICON_SUCCESS,
        confirmButtonColor: ConfirmBtnColor,
        confirmButtonText: btnText ?? "Ok",
        allowOutsideClick: false,
        showCancelButton: false,
        focusConfirm: false,
    });
}

function Popup_Error(title, text, btnText) {
    return Swal.fire({
        title: title ?? "[Error]",
        text: text ?? "Something is error",
        icon: ICON_ERROR,
        customClass: {
            confirmButton: 'btn btn-primary waves-effect waves-light',
            title: "popup-title"
        },
        showCancelButton: false,
        confirmButtonColor: ConfirmBtnColor,
        confirmButtonText: btnText ?? "Ok",
        allowOutsideClick: false,
    });
}

function Popup_Warning(title, text, btnText) {
    return Swal.fire({
        title: title ?? "Warning!",
        text: text ?? "This is default Text",
        icon: ICON_WARNING,
        showCancelButton: false,
        confirmButtonColor: ConfirmBtnColor,
        confirmButtonText: btnText ?? "Ok",
        allowOutsideClick: false
    });
}

function Popup_Info(title, text, btnText) {
    return Swal.fire({
        title: title ?? "Infomations",
        text: text ?? "Sample Text....",
        icon: ICON_INFO,
        showCancelButton: false,
        confirmButtonColor: ConfirmBtnColor,
        confirmButtonText: btnText ?? "Ok",
        allowOutsideClick: false
    });
}

function Popup_Questions(title, text, cancelBtn, confirmBtnText, cancelBtnText) {
    return Swal.fire({
        title: title ?? "Are you sure?",
        text: text ?? "You won't be able to revert this!",
        icon: ICON_QUESTION,
        showCancelButton: cancelBtn ?? true,
        confirmButtonColor: ConfirmBtnColor,
        cancelButtonColor: CancelBtnColor,
        confirmButtonText: confirmBtnText ?? "Yes",
        cancelButtonText: cancelBtnText ?? "Cancel",
        allowOutsideClick: false
    });
}

function Toast_Fire(icon, title, text) {
    $("#toast-block").show();
    Toast.fire({
        icon: icon ?? ICON_QUESTION,
        title: title ?? "Toast Title here",
        html: text ?? ""
    });
}

function Frontend_Error_Popup(title, text) {
    return Swal.fire({
        title: title ?? "[Error]",
        text: text ?? "Something is error",
        icon: ICON_ERROR,
        showCancelButton: false,
        confirmButtonColor: ConfirmBtnColor,
        confirmButtonText: "Ok",
        allowOutsideClick: false,
    });
}

function Delete_Popup(title, text, html, confirmText) {
    return Swal.fire({
        title: title ?? 'Are you sure?',
        text: text ?? "Do you really want to delete this item?",
        icon: ICON_WARNING,
        html: html ?? "",
        showCancelButton: true,
        confirmButtonColor: CancelBtnColor,
        reverseButtons: true, 
        cancelButtonColor: "",
        confirmButtonText: confirmText ?? 'Yes, Delete it',
        cancelButtonText: 'Cancel',
        allowOutsideClick: false,

    }).then((result) => {
        return result.isConfirmed;
    });
}