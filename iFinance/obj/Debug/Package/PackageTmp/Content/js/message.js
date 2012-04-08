function showAddNote() {
    $.ajax({
        type: "GET",
        url: rootPath + "note/add",
        success: function (content) {
            $("#float_box").html(content);
            $("#dialog").fadeIn(300);
        }
    });
}

function showEditNote(id) {
    $.ajax({
        type: "GET",
        url: rootPath + "note/edit/" + id,
        success: function (content) {
            $("#float_box").html(content);
            $("#dialog").fadeIn(300);
        }
    });

}

function addNote() {
    var data = "";
    var name = $("#Name").val();
    var content = $("#Content").val();
    data += "Name=" + name;
    data += "&Content=" + content;
    $.ajax({
        url: rootPath + "note/add",
        type: "POST",
        data: data,
        success: function (content) {
            $(content).insertAfter("#thead");
        }
    });
    cancelAddNote();
    //showAccount(type, money, time, info, tag,-1);
}

function editNote() {
    var data = "";
    var name = $("#Name").val();
    var content = $("#Content").val();
    var id = $("#Id").val();
    var uid = $("#UserId").val();
    data += "Id=" + id;
    data += "&Name=" + name;
    data += "&Content=" + content;
    data += "&UserId=" + uid;
    $.ajax({
        url: rootPath + "note/edit",
        type: "POST",
        data: data,
        success: function (content) {
            $("#note_item_" + id).html(content);
        }
    });
    cancelEditNote();
    //showAccount(type, money, time, info, tag, id);
}

function cancelAddNote() {
    $("#dialog").fadeOut(300);
    $("#add_note").remove();
}

function cancelEditNote() {
    $("#dialog").fadeOut(300);
    $("#edit_note").remove();
}


function showDeleteNote(id) {
    if (window.confirm("确认删除?") == true) {
        deleteNote(id);
    }
}

function deleteNote(id) {
    $.ajax({
        url: rootPath + "note/delete/" + id,
        type: "POST",
        success: function (content) {
            $("#note_item_" + id).remove();
        }
    });
}