<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=((iFinance.Models.User)( Session["user"])).UserName %>的记事本
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      
      <%=Html.Action("list", "note") %>
      
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <span style="margin-left:30px;"><%=((iFinance.Models.User)( Session["user"])).UserName %>的记事本</span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("注销","logout","auth") %></span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("记事本","index","note") %></span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("账户","index","home") %></span>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server"> 
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="TopNavContent" runat="server">
</asp:Content>
