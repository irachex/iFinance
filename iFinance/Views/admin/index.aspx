<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	管理用户
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      
      <%=Html.Action("list", "admin") %>
      
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <span style="margin-left:30px;">管理用户</span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("注销","logout","auth") %></span>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server"> 
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="TopNavContent" runat="server">
</asp:Content>