<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.UserListViewModel>" %>
<div id="user_list">
<% if (Model.list == null || Model.list.Count == 0) { %>
   <div id="no_user">
   还没有用户
   </div>
<% }      
else { %>
<table id="user_table" style="text-align:center;">
<tr id="Tr1" style="background-color:#DDE6F7;">
<td style="width:70px;">用户名</td>
<td style="width:120px;">邮箱</td>
<td style="width:120px;">手机</td>
<td style="width:200px;">地址</td>
<td style="width:120px;">注册时间</td>
</tr>
<% foreach (iFinance.Models.UserViewModel m in Model.list)
   { %>
<tr id="Tr2">
<td><%=m.UserName%></td>
<td><%=m.Email%></td>
<td><%=m.Phone%></td>
<td><%=m.Address%></td>
<td><%=m.RegisterTime.ToString("yyyy/MM/dd")%></td>
</tr>
<% } %>
</table>
<% } %>
</div>

    <div style="margin-top:60px; margin-bottom:10px; width:750px; border-bottom:1px dashed #D4D4D4;"></div>