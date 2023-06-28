$('.admin-update-btn').on('click', function () {
    var tr = $(this).closest('.admin-display-row');
    /*tr.hide();
    tr.next().show();*/
    tr.css("display", "none");
    tr.next().css("display", "table-row");
})

$('.update-cancel-btn').on('click', function () {
    var tr = $(this).closest('.admin-update-row');
    tr.css("display", "none");
    tr.prev().css("display", "table-row");
})

$('#admin-create-btn').on('click', function () {
    $('#create-form').show();
})

$('#admin-create-cancel-btn').on('click', function () {
    $('#create-form').hide();
})