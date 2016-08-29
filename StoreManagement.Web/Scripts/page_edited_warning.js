

var temp = {};

temp.original = {};
temp.fields = [];

$(document).ready(function () {
    for (var i = 0; i < temp.fields.length; i++) {
        temp.original[temp.fields[i]] = $('#' + temp.fields[i] + '_box').val();
    }
});

function set_fields(param) {
    for (var x in param) {
        temp.fields[x] = param[x];
    }
}

function doc_changed() {
    var res = false;
    for (var x in temp.original) {      // js for loops over keys not values,   nice :)
        if (temp.original[x] != $('#' + x + '_box').val()) {
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