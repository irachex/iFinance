var fsdate="";
var fedate="";
var ftag="";
function dateClick(d) {
    var sdate, edate;
    if (d == 0) {
        fsdate = '1980-01-01';
        fedate = '2100-01-01';
    }
    else if (d == 1) {
        edate = new Date();
        sdate = new Date();
        fsdate = sdate.getFullYear() + "-" + (sdate.getMonth() + 1) + "-" + sdate.getDate();
        fedate = edate.getFullYear() + "-" + (edate.getMonth() + 1) + "-" + edate.getDate();
    }
    else if (d == 2) {
        edate = new Date();
        sdate = new Date();
        sdate.setDate(new Date().getDate() - 7);
        fsdate = sdate.getFullYear() + "-" + (sdate.getMonth() + 1) + "-" + sdate.getDate();
        fedate = edate.getFullYear() + "-" + (edate.getMonth() + 1) + "-" + edate.getDate();
     }
    else if (d == 3) {
        edate = new Date();
        sdate = new Date()
        sdate.setDate(1);
        edate.setMonth(edate.getMonth() + 1);
        edate.setDate(1);
        fsdate = sdate.getFullYear() + "-" + (sdate.getMonth() + 1) + "-" + sdate.getDate();
        fedate = edate.getFullYear() + "-" + (edate.getMonth() + 1) + "-" + edate.getDate();
    }
    else if (d == 4) {
        edate = new Date();
        sdate = new Date();
        sdate.setMonth(0);
        sdate.setDate(1);
        edate.setMonth(11);
        edate.setDate(31);
        fsdate = sdate.getFullYear() + "-" + (sdate.getMonth() + 1) + "-" + sdate.getDate();
        fedate = edate.getFullYear() + "-" + (edate.getMonth() + 1) + "-" + edate.getDate();
    }
    $("#date0").removeClass("dactive");
    $("#date1").removeClass("dactive");
    $("#date2").removeClass("dactive");
    $("#date3").removeClass("dactive");
    $("#date4").removeClass("dactive");
    $("#date5").removeClass("dactive");

    $("#date" + d).addClass("dactive");

    filter();
}

function notag() {
    ftag = "";
    $("#top_nav .active").removeClass("active");
    $("#notag").addClass("active");
    
    filter();
}
function tagClick(id) {
    $("#notag").removeClass("active");
    var tagactive = $("#tag" + id).hasClass("active");
    if (!tagactive) {
        ftag += id + " ";
        $("#tag" + id).addClass("active");
    }
    else {
        $("#tag" + id).removeClass("active");
        ftag= ftag.replace(id, "");
    }
    filter();
}

function filter() {
    $.ajax({
        url: rootPath + "accountList?tags=" + ftag + "&starttime=" + fsdate + "&endtime=" + fedate,
        type: "GET",
        success: function (content) {
            $("#content_main").hide();
            $("#content_main").html(content);
            $("#content_main").fadeIn(300);
        }
    });
}