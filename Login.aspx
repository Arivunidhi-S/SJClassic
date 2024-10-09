<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SJ Classic | Login</title>
    <link rel="shortcut icon" href="images/Shutter.png" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/styles_green.css" type="text/css" />
      <script type='text/javascript'>
          var txt = "SJClassic Manufacturing ......   ";
          var espera = 200;
          var refresco = null;
          function rotulo_title() {
              document.title = txt;
              txt = txt.substring(1, txt.length) + txt.charAt(0); refresco = setTimeout("rotulo_title()", espera);
          }
          rotulo_title();
    </script>
    <style type="text/css">
        .box
        {
            box-shadow: 0.2px 0.2px 8px 0.2px;
        }
        body
        {
            background-image: url(images/bluebackground.jpg); /*You will specify your image path here.*/
            -moz-background-size: cover;
            -webkit-background-size: cover;
            background-size: cover;
            background-position: top center !important;
            background-repeat: no-repeat !important;
            background-attachment: fixed;
        }
    </style>
</head>
<body class="body">
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Visible="true" EnableEmbeddedSkins="false"/>
    <telerik:RadAjaxManager ID="RadAjaxManagergraph" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel2">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadTabStripGraph">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="UpdatePanel1" />
                    <telerik:AjaxUpdatedControl ControlID="up1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <%--=====back ground=========--%>
    <div align="center">
        <br />
       
        <%--<img src="Images/Banner.png" width="1000" height="100" alt="RSS" />--%></div>
    <%--=====Title Bar=========--%>
    <%--<div id="menu">
        <ul class="tabs">
            <li>
                <h4>
                    <a href="#">In the blog &raquo;</a></h4>
            </li>
            <li class="hasmore"><a href="#"><span>DownLoad</span></a>
                <ul class="dropdown">
                    <li><a href="#">Menu item 1</a></li>
                    <li><a href="#">Menu item 2</a></li>
                    <li><a href="#">Menu item 3</a></li>
                    <li class="last"><a href="#">Menu item 6</a></li>
                </ul>
            </li>
            <li class="hasmore"><a href="#"><span>UpLoad</span></a>
                <ul class="dropdown">
                    <li><a href="#">Topic 1</a></li>
                    <li><a href="#">Topic 2</a></li>
                    <li><a href="#">Topic 3</a></li>
                    <li class="last"><a href="#">Topic 4</a></li>
                </ul>
            </li>
            <li><a href="#"><span><strong>
                <img src="images/feed-icon-14x14.png" width="14" height="14" alt="RSS" />
                Subscribe to RSS</strong></span></a></li>
            <li>
                <h4>
                    <a href="#">Elsewhere &raquo;</a></h4>
            </li>
            <li><a href="#"><span>About</span></a></li>
            <li class="hasmore"><a href="/about/#networks"><span>ViewReport</span></a>
                <ul class="dropdown">
                    <li><a href="#">Twitter</a></li>
                    <li><a href="#">posterous</a></li>
                    <li><a href="#">SpeakerSite</a></li>
                    <li><a href="#">LinkedIn</a></li>
                    <li class="last"><a href="#">See more&hellip;</a></li>
                </ul>
            </li>
            <li><a href="#"><span>Bookmarks</span></a></li>
        </ul>
    </div>
    <div id="form">
    </div>
    <script type="text/javascript" src="fancydropdown.js"></script>--%>
    <%--<div id="menu">
        <ul class="tabs">
        </ul>
    </div>
    <script type="text/javascript" src="fancydropdown.js"></script>--%>
    <div style="margin-top: 7px;">
        <h4 align="center"><asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></h4>
    </div>
    <%--=====Welcome Bar=========--%>
    <div class="panel">
        <h1 align="center" class="mycss">
            Welcome User!</h1>
        <div style="margin-top: -25px;">
            <a href="#login_form" id="login_pop">Log In</a>
        </div>
    </div>
    
    <%--======popup1--%>
    <a href="#x" class="overlay" id="login_form"></a>
    <div class="popup">
        <h2 align="center" style="margin-top: 34px;">
            Sign In Here</h2>
        <div align="center">
        </div>
        <div style="margin-top: 33.5px; margin-left: 15px;">
            <div>
                <label for="login">
                    Login ID</label>
                <asp:TextBox Width="300px" ID="LoginTxt" BackColor="Transparent" runat="server" />
            </div>
            <div style="margin-top: 18px;">
                <label for="password">
                    Password</label>
                <asp:TextBox Width="300px" ID="PwdTxt" BackColor="Transparent" runat="server" TextMode="Password" />
            </div>
            <asp:Button ID="LoginBtn" Text="Log In" runat="server" BackColor="Transparent" ForeColor="Black"
                OnClick="LoginBtn_Click" Style="margin-top: 10px; margin-left: 420px; border: 0;" />
        </div>
        <a class="close" href="#close"></a>
    </div>
    <%--======popup2--%>
    <a href="#x" class="overlay" id="join_form"></a>
    <div class="popup" style="color: White">
        <h2>
            Sign Up</h2>
        <p>
            Please enter your details here</p>
        <div>
            <label for="email">
                Login (Email)</label>
            <input type="text" id="email" value="" />
        </div>
        <div>
            <label for="pass">
                Password</label>
            <input type="password" id="pass" value="" />
        </div>
        <div>
            <label for="firstname">
                First name</label>
            <input type="text" id="firstname" value="" />
        </div>
        <div>
            <label for="lastname">
                Last name</label>
            <input type="text" id="lastname" value="" />
        </div>
        <input type="button" value="Sign Up" />&nbsp;&nbsp;&nbsp;or&nbsp;&nbsp;&nbsp;<a href="#login_form"
            id="A1">Log In</a> <a class="close" href="#close"></a>
    </div>
    <br />
    <br />
    <br />
    <div runat="server" align="center" style="width: 100%; height: 250px">
       <%-- <asp:UpdatePanel ID="up1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:Timer ID="Timer1" Interval="2000" runat="server" />
                <asp:AdRotator ID="AdRotator2" runat="server" AdvertisementFile="~/XMLFile.xml"
                    Height="200px" Target="_top" Width="600px" />
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    </form>
</body>
</html>
