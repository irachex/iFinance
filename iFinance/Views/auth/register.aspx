<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<iFinance.Models.RegisterViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	注册
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="HeaderContent" runat="server">
<span style="margin-left:30px;">注册</span>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="width:600px; height:300px; margin-left:30px;">
   
    <% using (Html.BeginForm("register", "auth", FormMethod.Post))
           { %>
           <div style="float:left;">
           <b>必填</b>
           <div class="register_item">
               <label for="UserName">用户名</label><br />
                <%=Html.TextBoxFor(m => m.UserName)%>
           </div>
           <div class="register_item">
               <label for="Password">密码</label><br/>
               <%=Html.PasswordFor(m => m.Password1)%>
           </div>
           <div class="register_item">
               <label for="Password">确认密码</label><br />
               <%=Html.PasswordFor(m => m.Password2)%>
           </div>
           </div>

           <div style="float:left; margin-left:80px;">
           <b>可选</b>
           <div class="register_item">
               <label for="Email">邮箱</label><br />
               <%=Html.TextBoxFor(m => m.Email)%>
           </div>
           <div class="register_item">
               <label for="Phone">电话</label><br />
               <%=Html.TextBoxFor(m => m.Phone)%>
           </div>
           <div class="register_item">
               <label for="Address">地址</label><br />
               <%=Html.TextBoxFor(m => m.Address)%>
           </div>
           </div>
          <div style="clear:both;"></div>
            <input type="submit" value="注册" style="margin-left:20px;" />
           <% if (!String.IsNullOrEmpty((string)ViewData["error"]))
              { %>
            &nbsp; &nbsp;   &nbsp; &nbsp; <span style="color:Red;"><%=ViewData["error"] %></span>
           <% } %>
         
           <div style="height:40px; border-bottom:1px dashed #D4D4D4;"></div>

           已有帐号？<%=Html.ActionLink("立即登陆","login","auth") %>
        
        <% } %>
    
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="NavContent" runat="server">
</asp:Content>
