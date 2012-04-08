<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.AccountListViewModel>" %>

<div id="account_list">
<% if (Model.list == null || Model.list.Count == 0) { %>
   <div id="no_account">
   还没有记账记录
   </div>
<% }      
else { %>
<table id="account_table" style="text-align:center;">
<tr id="thead" style="background-color:#DDE6F7;">
<td style="width:60px;">类型</td>
<td style="width:80px;">金额</td>
<td style="width:120px;">时间</td>
<td style="width:180px;">备注</td>
<td style="width:140px;">标签</td>
<td style="width:30px;">编辑</td>
<td style="width:30px;">删除</td>
</tr>
<% foreach (iFinance.Models.AccountViewModel account in Model.list)
   { %>
<tr id="account_item_<%=account.Id %>">
<td><%=account.Type?"收入":"支出"%></td>
<td><%=account.Money%></td>
<td><%=account.Time.ToString("yyyy-MM-dd")%></td>
<td><%=account.Info%></td>
<td><%=account.Tag%></td>
<td style="font-size:20px;"><a href="javascript:void(0)" class="btn_edit" onclick="showEditAccount(<%=account.Id %>)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>
<td style="font-size:20px;"><a href="javascript:void(0)" class="btn_delete" onclick="showDeleteAccount(<%=account.Id %>)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>
</tr>
<% } %>
</table>
<% } %>
</div>

    <div style="margin-top:60px; margin-bottom:10px; width:750px; border-bottom:1px dashed #D4D4D4;"></div>
   
<div id="bottom_menu" style="margin-left:20px;">
  <a href="javascript:void(0)" onclick="showAddAccount()">添加</a>     &nbsp;&nbsp; | &nbsp;&nbsp;
  <a href="javascript:void(0)" onclick="showStatics()">统计</a> 
 </div>