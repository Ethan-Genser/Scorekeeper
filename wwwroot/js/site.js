$('.admin-update-btn').on('click', function () {
    var tr = $(this).closest('.admin-display-row');
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

$('#add-team-btn').on('click', function () {
    $('#create-form').show();
})

$('#add-team-cancel-btn').on('click', function () {
    $('#create-form').hide();
})

$('#add-scoreboard-btn').on('click', function () {
    $('#create-form').show();
})

$('#add-scoreboard-cancel-btn').on('click', function () {
    $('#create-form').hide();
})

// Scoreboard page: Add new text input when user clicks the add user button.
var counter = 0;
$('#add-user-btn').on('click', function () {
    var ul = document.getElementById('add-users-list');
    var li = document.createElement("li");
    ul.style.listStyle = "none";
    ul.style.padding = 0;
    ul.appendChild(li);
    li.innerHTML = '<input type="text" name="NewUsernames[' + counter + ']" />';
    counter++;
});