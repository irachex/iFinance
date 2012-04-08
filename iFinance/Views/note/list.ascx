<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.NoteListViewModel>" %>


<div id="note_list">
<% if (Model.list == null || Model.list.Count == 0) { %>
   <div id="no_note">
   还没有记事
   </div>
<% }      
else { %>
<table id="note_table" style="text-align:center;">
<tr id="thead" style="background-color:#DDE6F7;">
<td style="width:200px;">标题</td>
<td style="width:300px;">内容</td>
<td style="width:120px;">时间</td>
<td style="width:30px;">编辑</td>
<td style="width:30px;">删除</td>
</tr>
<% foreach (iFinance.Models.Note m in Model.list)
   { %>
<tr id="note_item_<%=m.Id %>">
<td><%=m.Name%></td>
<td><%=m.Content%></td>
<td><%=m.SubmitTime.ToString("yyyy-MM-dd")%></td>
<td style="font-size:20px;"><a href="javascript:void(0)" class="btn_edit" onclick="showEditNote(<%=m.Id %>)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>
<td style="font-size:20px;"><a href="javascript:void(0)" class="btn_delete" onclick="showDeleteNote(<%=m.Id %>)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>
</tr>
<% } %>
</table>
<% } %>
</div>

    <div style="margin-top:60px; margin-bottom:10px; width:750px; border-bottom:1px dashed #D4D4D4;"></div>
   
<div id="bottom_menu" style="margin-left:20px; padding-bottom:30px;">
  <a href="javascript:void(0)" onclick="showAddNote()">添加</a>
</div>