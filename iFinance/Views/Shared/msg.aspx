<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<iFinance.Models.MsgViewModel>" %>

<!DOCTYPE html> 
<html lang="zh-CN"> 
<head runat="server">
    <title><%=Model.msg%></title>
</head>
<body>
    <div id="msg">
        <%=Model.msg %>
    </div>
    <% if (Model.url != null)
       {%>
    <script type="text/javascript">
        window.location.href = "<%=Model.url %> ";
    </script>
    <%} %>
</body>
</html>
