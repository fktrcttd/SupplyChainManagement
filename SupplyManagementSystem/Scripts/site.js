
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


    // No_results_text - текст, который будет отображаться, когда результаты не найдены;
    //
    // Placeholder_text_multiple - текст для отображения в качестве заполнителя, если для множественного выбора не выбрано ни одного параметра;
    //
    // Placeholder_text_single - текст, отображаемый в качестве заполнителя, если для одиночного выбора не выбрано ни одного параметра;


    $(".chosen-select").chosen({
         width: "100%",
        // disable_search: false,
        // disable_search_threshold: 5,
        // enable_split_word_search: false,
        // max_selected_options: 10,
        no_results_text: "Ничего не найдено",
        placeholder_text_multiple: "Выберите несколько параметров",
        // placeholder_text_single: "Выберите параметр",
        // search_contains: true,
        // display_disabled_options: false,
        // display_selected_options: false,
        // max_shown_results: Infinity
    });
});

