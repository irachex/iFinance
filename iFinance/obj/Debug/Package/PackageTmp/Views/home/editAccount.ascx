<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.AccountViewModel>" %>

<div id="edit_account">
<div style="color:#1DACD6; font-size:14px;">编辑  · · · · · · </div>
   <br />
   <% using (Html.BeginForm("editAccount", "home", FormMethod.Post, new { id="account_form"}))
      { %>
    <div class="item">
      <%=Html.RadioButtonFor(m=>m.Type,false,new {id="outcome"}) %>
      <label for="outcome">支出</label>
      <%=Html.RadioButtonFor(m=>m.Type,true,new {id="income"}) %>
      <label for="income">收入</label>
    </div>
    <div class="item">
       <label for="Money">金额</label>
       <%=Html.TextBoxFor(m => m.Money)%>
    </div>
    <div class="item">
       <label for="Time">时间</label>
       <%=Html.TextBoxFor(m => m.Time, new { value = Model.Time.ToString("yyyy-MM-dd") })%>
    </div>
    <div class="item">
        <label for="Tag">标签</label>
        <%=Html.TextBoxFor(m => m.Tag)%>
        <div id="tag_cloud">
            <% 
                foreach (iFinance.Models.Tag t in (IEnumerable)ViewData["TagCloud"])
                { %>
                <% if (!Model.Tag.Contains(t.Name))
                   {%>
                    <a href="javascript:void(0)" id="tag_select_<%=t.Id %>" onclick="tagSelect(<%=t.Id %>)"><%=t.Name%></a>
                    <%}
                   else
                   {%>
                    <a href="javascript:void(0)" id="tag_select_<%=t.Id %>" onclick="tagSelect(<%=t.Id %>)"  class="tagselected" style="color:#BBB;"><%=t.Name%></a>
                    <%} %>
                   
             <%} %>
        </div>
    </div>
    <div class="item">
      <label for="Info" class="item_label">备注</label>
      <%=Html.TextAreaFor(m => m.Info)%>
    </div>
    <%=Html.HiddenFor(m=>m.Id) %>
    <%=Html.HiddenFor(m=>m.State) %>
    <%=Html.HiddenFor(m=>m.SubmitTime) %>
    <%=Html.HiddenFor(m=>m.UserId) %>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <a href="javascript:void(0)" onclick="editAccount()">写好了</a> &nbsp;&nbsp; &nbsp;  &nbsp;&nbsp;<a href="javascript:void(0)" onclick="cancelEditAccount()">取消</a>
    
    <% } %>
</div>