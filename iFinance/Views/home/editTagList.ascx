<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.TagListViewModel>" %>

<div id="edit_tag_list">
<div style="color:#1DACD6; font-size:14px;">编辑标签  · · · · · · </div>
<br />
   
<ul>
<% foreach (iFinance.Models.Tag tag in Model.list)
   { %>
     <li id="tag_item_<%=tag.Id %>"> <span class="tag_name"><%=tag.Name %>  </span><a href="javascript:void(0)" onclick="showEditTag(<%=tag.Id %>)">编辑</a> | <a href="javascript:void(0)" onclick="showDeleteTag(<%=tag.Id %>)">删除</a></li>
<% } %>
</ul>
<div id="add_tag_input">

</div>
<br />
<br />
<a href="javascript:void(0)" onclick="showAddTag()">添加标签</a>
 &nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0)" onclick="cancelEdit()">取消</a>
</div>

