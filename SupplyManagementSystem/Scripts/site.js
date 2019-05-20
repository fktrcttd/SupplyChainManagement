
$("#menu-toggle").click(function(e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

$(document).on("click",".linked-div",function() {

    var ref = $(this).data("href");
    $.get(ref, function (response) {
        $('#request-content').html(response);
    });

    $('#request_modal').modal({
        backdrop: 'static'}, 'show');
    return false;
    
    
});

$(document).ready(function () {
    $(".chosen-select").chosen();
});

