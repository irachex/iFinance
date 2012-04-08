<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iFinance.Models.LoginViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	登陆
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<span style="margin-left:30px;">登陆</span>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <div style="width:400px; height:300px; margin-left:30px;">
        <% using (Html.BeginForm("login", "auth", FormMethod.Post))
           { %>
           <div class="item">
               <label for="UserName">用户名</label><br />
                <%=Html.TextBoxFor(m => m.UserName)%>
           </div>
           <div class="item">
               <label for="Password">密码</label><br />
               <%=Html.PasswordFor(m => m.Password)%>
           </div>
           
           <input type="submit" value="登录" />
            <% if (!String.IsNullOrEmpty((string)ViewData["error"]))
              { %>
            &nbsp; &nbsp;   &nbsp; &nbsp; <span style="color:Red;"><%=ViewData["error"] %></span>
           <% } %>
           <div style="height:60px; border-bottom:1px dashed #D4D4D4;"></div>

           还没有帐号？<%=Html.ActionLink("立即注册","register","auth") %>
        <% } %>
    </div>
</asp:Content>
