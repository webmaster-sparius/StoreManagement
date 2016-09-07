

var page_id = 0;

// crud by comet:  these function do not actually do the change, they just update the page
// NOTE: all these functions recieve a list of partial views

function add_by_comet(pv_list) {
    for (elem in pv_list) {
        $('.table').append(elem);
    }
}

// sending requests

function send_add_comet_request() {
    $.ajax({
        type: 'post',
        url: '/comet/notifyonadd',
        data: {
            client_id: page_id,
            entity_name: '@entityType.Name',
        },
        success: function (result) {
            add_by_comet(result);
            send_add_comet_request();
        }
    });
}

function send_edit_comet_request() {
    $.ajax({
        type: 'post',
        url: '/comet/notifyonedit',
        data: {
            client_id: page_id,
            entity_name: '@entityType.Name',
        },
        success: function (result) {
            edit_by_comet(result);
            send_edit_comet_request();
        }
    });
}

function send_delete_comet_request() {
    $.ajax({
        type: 'post',
        url: '/comet/notifyondelete',
        data: {
            client_id: page_id,
            entity_name: '@entityType.Name',
        },
        success: function (result) {
            delete_by_comet(result);
            send_delete_comet_request();
        }
    });
}


function start_comet_mechanism() {      // we seperated actions so that we are enabled to
    send_add_comet_request();           // write different success functions for them
    send_edit_comet_request();          // which eliminates the need for parsing the result
    send_delete_comet_request();
}

$(document).ready(function () {
    $.ajax({
        type: 'post',
        url: '/comet/sendclientid',
        data: {},
        success: function (result) {
            page_id = result;
            start_comet_mechanism();
        }
    });
});

