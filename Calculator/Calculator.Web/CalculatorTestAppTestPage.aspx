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
    
    /*
     * CSS to resize the IronRuby Console
     */ 

    /* top resize handle */
    .ui-resizable-n {
      cursor: n-resize;
      width: 100%;
      height: 7px;
      left: 0;
      top: -5px;
    }
    .ui-resizable-handle {
      display: block;
      position: absolute;
      z-index: 99999;
    }

    #silverlightDlrRepl { 
      position: relative;
    }
    </style>
    <script type="text/javascript" src="jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="jquery-ui-1.7.1.custom.min.js"></script>
</head>
<body>
    <form id="form1" runat="server"> 
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="height:320px; width: 440px; margin-left: auto; margin-right: auto; margin-top: 10px;">
            <asp:Silverlight ID="Silverlight1" runat="server" Source="~/ClientBin/CalculatorTestApp.xap" MinimumVersion="3.0.40226.0" Width="100%" Height="100%" Windowless="true" />
        </div>
    </form>

    <script type="text/javascript">
      var previous_top = null;
      
      //
      // Makes the silverlightDlrRepl vertically resizable.
      // Depends on jquery-ui's "resizable" method
      //
      resizable_repl = function() {
        $('#silverlightDlrWindow').resizable({

          // Resize handle is only on the top
          handles: 'n', 

          // During resizing, also resize the repl div, but by
          // 10px less than the window.
          resize: function(event, ui) {
            $('#silverlightDlrWindow').css('top', 'inherit') 
            $('#silverlightDlrRepl').css('height', 
              ($('#silverlightDlrWindow').css('height').split('px')[0] - 10) + 'px')
          },

          // When the resize is finished, make sure the top is the only 
          // one that moved.
          stop: function(event, ui) {
            previous_top = $('#silverlightDlrWindow').css('top')
            $('#silverlightDlrWindow')
              .css('left', '10px')
              .css('right', '10px')
              .css('bottom', '25px')
              .css('top', 'inherit')
              .css('width', 'inherit')
              .css('height', 'inherit')
          }
        });
      }
    </script>
</body>
</html>
