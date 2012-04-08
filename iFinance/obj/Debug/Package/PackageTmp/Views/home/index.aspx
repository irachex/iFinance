<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=((iFinance.Models.User)( Session["user"])).UserName %>的账户
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
      <%=Html.Action("accountList", "home") %>
     
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <span style="margin-left:30px;"><%=((iFinance.Models.User)( Session["user"])).UserName %>的账户</span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("注销","logout","auth") %></span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("记事本","index","note") %></span>
    <span style="float:right;margin-right:20px;"><%=Html.ActionLink("账户","index","home") %></span>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="NavContent" runat="server">
            <div id="left_nav">
                <div id="date_filter">
                    <ul>
                        <li id="date0" class="dactive" ><a  href="javascript:void(0)" onclick="dateClick(0)">全部</a></li>
                        <li id="date1" ><a href="javascript:void(0)" onclick="dateClick(1)">今天</a></li>
                        <li id="date2"><a href="javascript:void(0)" onclick="dateClick(2)">本周</a></li>
                        <li id="date3"><a href="javascript:void(0)" onclick="dateClick(3)">本月</a></li>
                        <li id="date4"><a href="javascript:void(0)" onclick="dateClick(4)">今年</a></li>
                        <li id="date5"><a  href="javascript:void(0)" onclick="showDatePicker()">翻日历</a></li>
                      </ul>
                      <div id="nowdate"></div>
                </div>
                <div id="menu">
                </div>
            </div> 
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="TopNavContent" runat="server">
<div id="top_nav">
<%=Html.Action("taglist","home") %>
</div>
</asp:Content>
