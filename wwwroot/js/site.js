// --------------- Admin/* pages ---------------

// Toggles on edit mode so the admin can update database values
$('.admin-update-btn').on('click', function () {
    var tr = $(this).closest('.admin-display-row');
    tr.css("display", "none");
    tr.next().css("display", "table-row");
})

// Toggles off admin edit mode
$('.update-cancel-btn').on('click', function () {
    var tr = $(this).closest('.admin-update-row');
    tr.css("display", "none");
    tr.prev().css("display", "table-row");
})

// Show the create menu on any admin page
$('#admin-create-btn').on('click', function () {
    $('#create-form').show();
})

// Hide the create menu on any admin page 
$('#admin-create-cancel-btn').on('click', function () {
    $('#create-form').hide();
})



// --------------- Scoreboard page ---------------

// Add new team to scoreboard
$('#add-team-btn').on('click', function () {
    $('#create-form').show();
})

// Cancel adding a new team to the scoreboard
$('#add-team-cancel-btn').on('click', function () {
    $('#create-form').hide();
})

// Toggles on edit mode so the user can edit various scoreboard properties
$('#edit-scoreboard-btn').on('click', function () {
    $('.edit-show').show();
    $('.edit-hide').hide();
})

// Toggles off scoreboard edit mode
$('#edit-scoreboard-cancel-btn').on('click', function () {
    $('.edit-show').hide();
    $('.edit-hide').show();
})



// --------------- Index page ---------------

// Create new scoreboard for user
$('#add-scoreboard-btn').on('click', function () {
    $('#create-form').show();
})

// Cancel creating new scoreboard for user
$('#add-scoreboard-cancel-btn').on('click', function () {
    $('#create-form').hide();
})

// When creating a new scoreboard, add new text input for adding users
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