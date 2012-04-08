function showAddAccount() {
    $.ajax({
        type: "GET",
        url: rootPath + "addAccount",
        success: function (content) {
            $("#float_box").html(content);
            $("#dialog").fadeIn(300);
            $("#Time").datepicker({
                dateFormat: 'yy/mm/dd'
            });


        }
    });
}

function showEditAccount(id) {
    $.ajax({
        type: "GET",
        url: rootPath + "editAccount/" + id,
        success: function (content) {
            $("#float_box").html(content);
            $("#dialog").fadeIn(300);
            $("#Time").datepicker({
                dateFormat: 'yy/mm/dd'
            });
        }
    });

}

function addAccount() {
    var data = "";
    var type = $("#account_form input:checked").val();
    var money = $("#Money").val();
    var time = $("#Time").val();
    var info = $("#Info").val();
    var tag = $("#Tag").val();
    data += "Type=" + type;
    data += "&Money=" + money;
    data += "&Time=" + time;
    data += "&Tag=" + tag;
    data += "&Info=" + info;
    $.ajax({
        url: rootPath + "addAccount",
        type: "POST",
        data: data,
        success: function (content) {
            $(content).insertAfter("#thead");
        }
    });
    cancelAddAccount();
    //showAccount(type, money, time, info, tag,-1);
}

function editAccount() {
    var data = "";
    var type = $("#account_form input:checked").val();
    var money = $("#Money").val();
    var time = $("#Time").val();
    var info = $("#Info").val();
    var tag = $("#Tag").val();
    var id = $("#Id").val();
    var uid= $("#UserId").val();
    data += "Id=" + id;
    data += "&Type=" + type;
    data += "&Money=" + money;
    data += "&Time=" + time;
    data += "&Tag=" + tag;
    data += "&Info=" + info;
    data += "&UserId=" + uid;
    $.ajax({
        url: rootPath + "editAccount",
        type: "POST",
        data: data,
        success: function (content) {
            $("#account_item_" + id).html(content);
        }
    });
    cancelEditAccount();
    //showAccount(type, money, time, info, tag, id);
}

function cancelAddAccount() {
    $("#dialog").fadeOut(300);
    $("#add_account").remove();
}

function cancelEditAccount() {
    $("#dialog").fadeOut(300);
    $("#edit_account").remove();
}


function showAccount(type, money, time, info, tag, id) {
    if (id==-1) {
    $("#account_table").prepend('<tr><td>' + type + '</td><td>' + money + '</td><td>' + time + '</td><td>' + info + '</td><td>' + tag + '</td></tr><td style="width:24px;"><a href="javascript:void(0)" class="btn_edit" onclick="showEditAccount(<%=account.Id %>)"<span style="font-size:24px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></a></td><td style="width:24px;"><a href="javascript:void(0)" class="btn_delete" onclick="showDeleteAccount(<%=account.Id %>)"><span style="font-size:24px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></a></td>');
    }
    else {
    $("#account_item_"+id).html("<td>" + type + "</td><td>" + money + "</td><td>" + time + "</td><td>" + info + "</td><td>" + tag + "</td>");
    }
}

function showDeleteAccount(id) {
    if (window.confirm("确认删除?") == true) {
        deleteAccount(id);
    }
}

function deleteAccount(id) {
    $.ajax({
        url: rootPath + "deleteAccount/" + id,
        type: "POST",
        success: function (content) {
            filter();
        }
    });
}

function tagSelect(id) {
    var tagname = $("#tag_select_" + id).html();
    var tag=" "+$("#Tag").val();
    if ($("#tag_select_" + id).hasClass("tagselected")) {
        tag = tag.replace(" "+tagname, "");
        $("#Tag").val(tag.trim());
        $("#tag_select_" + id).removeClass("tagselected");
        $("#tag_select_" + id).css("color", "#1398b0");
    }
    else {
        tag += " " + tagname;
        $("#Tag").val(tag.trim());
        $("#tag_select_" + id).addClass("tagselected");
        $("#tag_select_" + id).css("color", "#BBB");
    }
}