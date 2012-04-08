var rootPath = "/"

function showStatics() {
    $.ajax({
        url: rootPath + "statics?tags=" + ftag + "&starttime=" + fsdate + "&endtime=" + fedate,
        type: "GET",
        success: function (content) {
            $("#content_main").hide();
            $("#content_main").html(content);
            $("#content_main").fadeIn(300);
        }
    });
}

$(function () {
    
    
});
 
function showDatePicker() {
    $("#float_box").html('<div style="color:#1DACD6; font-size:14px;">查询时间段  · · · · · · </div><br/><div id="calendar"> 起始日期  <input type="text" id="start_time" /><br/>终止日期  <input type="text" id="end_time"/> <br/><br/><a href="javascript:void(0)" onclick="calendarClick()">查询</a>&nbsp;&nbsp;&nbsp;<a href="javascript:void(0)" onclick="cancelCalendar()">取消</a> </div>');
    $("#dialog").fadeIn(300);
    $("#start_time").datepicker({
        dateFormat: 'yy/mm/dd'
    });
    $("#end_time").datepicker({
        dateFormat: 'yy/mm/dd'
    });
}

function calendarClick() {
    fsdate = $("#start_time").val();
    fedate = $("#end_time").val();
    cancelCalendar();
    filter();
}

function cancelCalendar() {
    $("#calendar").remove();
    $("#dialog").fadeOut(300);
}