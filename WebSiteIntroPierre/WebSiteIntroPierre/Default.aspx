<%@ Page Language="C#" Src="~/CodeMetier.asax" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Page sans titre</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <% Tty t1 = new Tty(); t1.Name = "Test1"; Response.Write(t1.Name); %>
    </div>
    </form>
</body>
</html>
