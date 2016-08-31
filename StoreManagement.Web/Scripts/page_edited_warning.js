var original_array = [];

$(function () {
    $("#editable_form div.form-group :input").
                    each(function () { original_array.push($(this).val()) });
});


//window.onunload(alert("bye now"));

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


function go_for_return(cn,vn) {     // controller name , view name
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
                    if(vn == 'create'){
                        document.location = '/basicdata/' + cn + '/list';
                    }
                    else {
                        document.location = '../list';
                    }
                },
                " خوب شد گفتی ": function () {
                    $(this).dialog("close");
                }
            }
        });
    }
    else {
        if (vn == 'create') {
            document.location = '/basicdata/' + cn + '/list';
        }
        else {
            document.location = '../list';
        }
    }
}

function go_for_return_to_list_of(cn) {
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