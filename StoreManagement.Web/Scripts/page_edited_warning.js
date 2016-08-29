var original_array = [];

$(document).ready(function () {
    $("#editable_form div.form-group :input").
                    each(function () { original_array.push($(this).val()) });
});

function doc_changed() {
    var new_array = [];
    $("#editable_form div.form-group :input").
                    each(function () { new_array.push($(this).val()) });
    var res = false;

    for (var i = 0; i < original_array.length; i++) {
        if (original_array[i] != new_array[i]) {
            res = true;
        }
    }
    return res;
}


function go_for_return() {
    // if something is changed
    var changed = doc_changed();
    if (changed) {
        $("#dialog-confirm").dialog({
            resizable: false,
            height: "auto",
            width: 400,
            modal: true,
            buttons: {
                " حواسم هست ": function () {
                    $(this).dialog("close");
                    document.location = '../list';
                },
                " خوب شد گفتی ": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    else {
        document.location = '../list';
    }
}