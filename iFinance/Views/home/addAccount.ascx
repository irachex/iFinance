<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<iFinance.Models.AccountViewModel>" %>

<div id="add_account">
    <div style="color:#1DACD6; font-size:14px;">添加  · · · · · · </div>
    <br />
   <% using (Html.BeginForm("addAccount", "home", FormMethod.Post, new { id="account_form"}))
      { %>
    <div class="item">
      <input id="outcome" name="Type" type="radio" checked="checked" value="false" /> 
      <label for="outcome">支出</label>
      <input id="income" name="Type" type="radio" value="true"/> 
      <label for="income">收入</label>
    </div>
    <div class="item">
        <span class="item_label"><label for="Money">金额</label></span>
       <%=Html.TextBoxFor(m => m.Money)%>
    </div>
    <div class="item">
        <span class="item_label"><label for="Time">时间</label></span>
            <%=Html.TextBoxFor(m=>m.Time) %>
    </div>
    <div class="item">
         <span class="item_labe;"><label for="Tag">标签</label></span>
        <%=Html.TextBoxFor(m=>m.Tag) %>
        <div id="tag_cloud">
            <% 
                foreach (iFinance.Models.Tag t in (IEnumerable)ViewData["TagCloud"])
                { %>
                    <a href="javascript:void(0)" id="tag_select_<%=t.Id %>" onclick="tagSelect(<%=t.Id %>)"><%=t.Name%></a>   
             <%} %>
        </div>
    </div>
    <div class="item">
      <span ><label for="Info" class="item_label">备注</label></span>
      <%=Html.TextAreaFor(m => m.Info)%>
    </div>
    <br />
     &nbsp;&nbsp;&nbsp;&nbsp;
    <a href="javascript:void(0)" onclick="addAccount()">写好了</a> &nbsp;&nbsp; &nbsp;   &nbsp;&nbsp;<a href="javascript:void(0)" onclick="cancelAddAccount()">取消</a>
    
    <% } %>
</div>