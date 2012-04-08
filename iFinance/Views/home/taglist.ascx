<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.TagListViewModel>" %>

<ul>
<li> <a id="notag" href="javascript:void(0)" onclick="notag()" class="active">所有</a></li>
<% foreach (iFinance.Models.Tag tag in Model.list)
   { %>
     <li> <a id="tag<%=tag.Id %>" href="javascript:void(0)" onclick="tagClick(<%=tag.Id %>)"><%= tag.Name %></a></li>
<% } %>
</ul>
<div class="edit_tag_list" style="float:right;margin-right:80px;">
<a href="javascript:void(0)" onclick="showEditTagList()">编辑标签</a>
</div>