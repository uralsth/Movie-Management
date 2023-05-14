function AjaxCall(config) {
    config = $.extend({
        async: false,
        cache: false,
        type: 'POST',
        data: {},
        dataType: 'json',
        contentType : "application/json; charset=utf-8",
        url: '',        
        success: function (data, textStatus, jqXHR) {
            console.log(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error(errorThrown);
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());
        }
    }, config);
    $.ajax(config);
}