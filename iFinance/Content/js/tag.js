function showEditTagList() {
    $.ajax({
        type: "GET",
        url: rootPath + "editTagList",
        success: function (content) {
            $("#float_box").html(content);
            $("#dialog").fadeIn(300);
        }
    });
}

function showEditTag(id) {
    var tagname= $("#tag_item_"+id +" .tag_name").html().trim();
    $("#tag_item_" + id).html("<input type='text' id='edit_tag_name'  name='edit_tag_name' value='" + tagname + "' /><a href='javascript:void(0)' onclick='editTag(" + id + ")'>保存</a>");
}

function editTag(id) {
    var tagname = $("#edit_tag_name").val();
    $.ajax({
        url: rootPath + "editTag",
        type: "POST",
        data: "Id="+id+"&Name="+tagname+"&UserId=0",
        success: function (content) {
        }
    });
    $("#tag_item_"+id ).html('<span class="tag_name">'+tagname+' </span><a href="javascript:void(0)" onclick="showEditTag('+id+')">编辑</a>');
}

function showAddTag(id) {
    $("#add_tag_input").html('<input type="text" name="add_tag_name" id="add_tag_name" value="" /><a  href="javascript:void(0)" onclick="addTag()">保存</a>');
}

function addTag() {
    var tagname = $("#add_tag_name").val();
    $.ajax({
        url: rootPath + "addTag",
        type: "POST",
        data: "Name=" + tagname,
        success: function (content) {
            $("#edit_tag_list ul").append(content);
        }
    });

    $("#add_tag_input").html('');

}

function showDeleteTag(id) {
    if (window.confirm("确认删除?") == true) {
        deleteTag(id);
    }
}

function deleteTag(id) {
    $.ajax({
        url: rootPath + "deleteTag/" + id,
        type: "POST",
        success: function (content) {
            $("#tag_item_" + id).remove();
        }
    });
}

function cancelEdit() {
    $("#dialog").fadeOut(300);
    $("#edit_tag_list").remove();
    filter();
}