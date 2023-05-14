
$(function () {
    $('.close').on('click', function () {
        $(this).parent().remove();
    });
});
//$(function () {
//    $('.time').timepicker({
//        timeFormat: 'HH:mm'
//    });
//});
//$(function () {
//    $("#ReleaseDate").datepicker({
//        dateFormat: "yy-mm-dd",
//        minDate: new Date(1900, 1 - 1, 1),
//        maxDate: new Date()
//    });
//$(function () {
//    $("#ReleaseDate").datepicker({
//        dateFormat: "yyyy-mm-dd",
//        minDate: new Date(1900, 1 - 1, 1),
//        maxDate: new Date(),
//        });
//}); 
$('#trailerModal').on('hide.bs.modal', function () {
    var iframe = document.getElementsByTagName("iframe")[0];
    iframe.contentWindow.postMessage('{"event":"command","func":"pauseVideo","args":""}', '*');
});

