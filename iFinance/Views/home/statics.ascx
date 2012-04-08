<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.StaticsViewModel>" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<div id="statics">
<div id="chartholder" style="width:500px;height:360px;float:left; "></div>
<div id="pieholder" style="width:280px;height:280px;float:left;"></div>
<div class="clear"></div>

<div style="margin-top:-60px; margin-bottom:10px; width:750px; border-bottom:1px dashed #D4D4D4;"></div>
   
<div id="bottom_menu" style="margin-left:20px;">
  <a href="javascript:void(0)" onclick="filter()">返回列表</a> 
 </div>
    <% 
        var dtstr = "";
        foreach (string k in Model.tagout.Keys)
        {
            dtstr = ",['" + k + "'," + Model.tagout[k] + "]"+dtstr;
        }
        if (dtstr.Length>1) dtstr = dtstr.Substring(1);
   
        var distr = "";
        foreach (long k in Model.ichart.Keys)
       {
           distr = ",[" + k + "," + (-Model.ichart[k]) + "]"+distr; 
       }
        if (distr.Length > 1)  distr = distr.Substring(1);
        var dostr = "";
        var kl = Model.ochart.Keys;
        foreach (long k in Model.ochart.Keys)
        {
            dostr = ",[" + k + "," + (Model.ochart[k]) + "]"+dostr;
        }
        if (dostr.Length > 1) dostr = dostr.Substring(1);
        var dsstr = "";
        foreach (long k in Model.schart.Keys)
        {
            dsstr = ",[" + k + "," +( Model.schart[k] )+ "]" +dsstr;
        }
        if (dsstr.Length > 1) dsstr = dsstr.Substring(1);
     %>
<script type="text/javascript" src="<%=Url.Content("~/Content/js/highcharts.js")%>"></script> 
<script type="text/javascript">
var chart;
$(document).ready(function () {
   piechart=new Highcharts.Chart({
       chart: {
           renderTo: 'pieholder',
           defaultSeriesType: 'pie'
       },
       title : {
           text:'各标签支出统计'
       },
       tooltip: {
						formatter: function() {
							return this.point.name +' : '+ this.y;
						}
					},
       plotOptions: {
						pie: {
							allowPointSelect: true,
							cursor: 'pointer',
							dataLabels: {
								enabled: true,
								color: '#000',
								connectorColor: '#333',
								formatter: function() {
									return  this.point.name;
								}
							}
						}
					},
        credits: {
            enabled: false
        },
           series:[{
            name: '标签',
            data: [<%=dtstr %>],
            type:'pie',
            size:160
        }]
   });

    chart = new Highcharts.Chart({
        chart: {
            renderTo: 'chartholder',
            defaultSeriesType: 'column',
            zoomType: "xy"
        },
        title: {
            text: '总收支：<%=Model.income %>(收) - <%=Model.outcome %>(支) = <%=Model.income-Model.outcome %>'
        },
        xAxis: {
            type: 'datetime',

        },
        yAxis: {
            title: {
                text: 'Money'					
            }
        },
        tooltip: {
            formatter: function () {
                return '' +
								 this.series.name + ': ' + this.y + '';
            }
        },
        credits: {
            enabled: false
        },
        series: [
         {
            name: '收入',
            data: [<%=dostr %>]
        },{
            name: '支出',
            data: [<%=distr %>]
        }, {
            name: '收支',
            data: [<%=dsstr %>],
            type: 'line'
        }]
    });

});
</script>
</div>