
var peo = {};

peo.original_array = [];

$(document).ready(function () {
    $("#editable_form  :input").
                    each(function () { peo.original_array.push($(this).val()) });

    /////////////////////

    $.ajax({
        type: 'post',
        url: '/home/getediteddiv',
        data: {},
        success: function (result) {
            $('body').append(result);
        }
    });

});


//window.onunload(alert("bye now"));

function peo_doc_changed() {
    var new_array = [];
    $("#editable_form  :input").
                    each(function () { new_array.push($(this).val()) });
    var res = false;

    for (var i = 0; i < peo.original_array.length; i++) {
        if (peo.original_array[i] != new_array[i]) {
            res = true;
        }
    }
    return res;
}

function go_for_return_to_list_of(cn) {
    // if something is changed
    var changed = peo_doc_changed();
    if (changed) {
        $("#dialog-confirm").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                " حواسم هست ": function () {
                    $(this).dialog("close");
                        document.location = '/basicdata/' + cn + '/list';
                },
                " خوب شد گفتی ": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    // no change
    else {
            document.location = '/basicdata/' + cn + '/list';
    }
}