<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.Note>" %>

<div id="add_note">
    <div style="color:#1DACD6; font-size:14px;">添加  · · · · · · </div>
    <br />
   <% using (Html.BeginForm("add", "note", FormMethod.Post, new { id="note_form"}))
      { %>
    <div class="item">
        <span class="item_label"><label for="Name">标题</label></span>
       <%=Html.TextBoxFor(m => m.Name)%>
    </div>
    <div class="item">
        <span class="item_label"><label for="Content">内容</label></span>
            <%=Html.TextBoxFor(m=>m.Content) %>
    </div>
    <br />
     &nbsp;&nbsp;&nbsp;&nbsp;
    <a href="javascript:void(0)" onclick="addNote()">写好了</a> &nbsp;&nbsp; &nbsp;   &nbsp;&nbsp;<a href="javascript:void(0)" onclick="cancelAddNote()">取消</a>
    
    <% } %>
</div>