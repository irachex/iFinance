<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.Note>" %>

<div id="edit_note">
<div style="color:#1DACD6; font-size:14px;">编辑  · · · · · · </div>
   <br />
   <% using (Html.BeginForm("edit", "note", FormMethod.Post, new { id="note_form"}))
      { %>
    <div class="item">
       <label for="Name">标题</label>
       <%=Html.TextBoxFor(m => m.Name)%>
    </div>
    <div class="item">
       <label for="Content">内容</label>
       <%=Html.TextBoxFor(m => m.Content)%>
    </div>
    <%=Html.HiddenFor(m=>m.Id) %>
    <%=Html.HiddenFor(m=>m.SubmitTime) %>
    <%=Html.HiddenFor(m=>m.UserId) %>
    <%=Html.HiddenFor(m=>m.SubmitTime) %>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <a href="javascript:void(0)" onclick="editNote()">写好了</a> &nbsp;&nbsp; &nbsp;  &nbsp;&nbsp;<a href="javascript:void(0)" onclick="cancelEditNote()">取消</a>
    
    <% } %>
</div>