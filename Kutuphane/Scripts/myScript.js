function myToastr(data) {
    if (data.success == 0) {
        toastr.success(data.message, "", { extendedTimeOut: true, timeOut: 4000, progressBar: true, showMethod: 'slideDown', hideMethod: 'slideUp' });
    }
    else if (data.success == 1) {
        toastr.error(data.message, "", { extendedTimeOut: true, timeOut: 4000, progressBar: true, showMethod: 'slideDown', hideMethod: 'slideUp' });
    }
    else if (data.success == 2) {
        toastr.info(data.message, "", { extendedTimeOut: true, timeOut: 7000, progressBar: true, showMethod: 'slideDown', hideMethod: 'slideUp' });
    }
    else if (data.success == 3) {
        toastr.warning(data.message, "", { extendedTimeOut: true, timeOut: 7000, progressBar: true, showMethod: 'slideDown', hideMethod: 'slideUp' });
    }
}