
$(document).ready(function () {
    for (var i = 0; i < fields.length; i++) {
        original[fields[i]] = $('#' + fields[i] + '_box').val();
    }
});

function doc_changed() {
    var res = false;
    for (var x in original) {      // js for loops over keys not values,   nice :)
        if (original[x] != $('#' + x + '_box').val()) {
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