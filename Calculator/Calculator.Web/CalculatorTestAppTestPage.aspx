<%@ Page Language="c#" AutoEventWireup="true" %>

<%@ Register Assembly="System.Web.Silverlight" Namespace="System.Web.UI.SilverlightControls"
    TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CalculatorTestApp</title>
    <style type="text/css">
    html, body {
	    height: 100%;
	    overflow: auto;
    }
    body {
	    padding: 0;
	    margin: 0;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="height:100%;">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="height:320px; width: 440px">
            <asp:Silverlight ID="Silverlight1" runat="server" Source="~/ClientBin/CalculatorTestApp.xap" MinimumVersion="3.0.40226.0" Width="100%" Height="100%" Windowless="true" />
        </div>
    </form>
</body>
</html>