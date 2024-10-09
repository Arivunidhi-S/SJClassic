<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SJ Classic | Main Page</title>
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="description" content="Made with WOW Slider - Create beautiful, responsive image sliders in a few clicks. Awesome skins and animations. Jquery carousel" />
    <link rel="shortcut icon" href="images/Shutter.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <!-- Start section -->
    <link rel="stylesheet" type="text/css" href="themes/engine1/style.css" />
    <script type="text/javascript" src="themes/engine1/jquery.js"></script>
    <!-- End section -->
    <%--<script type="text/javascript" src="js/api.js"></script>--%>
    <!--Custom Styles-->
    <style type="text/css">
        .myTitle
        {
            color: #333;
            font-family: arial;
            font-weight: normal;
            font-size: 10px;
            margin: 20px 0px 5px 0px;
        }
        .myTitleTop
        {
            margin: 5px 0px;
        }
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
    <script type="text/javascript">
        var myclose = false;

        function ConfirmClose() {
            if (event.clientY < 0) {
                event.returnValue = 'You have closed the browser. Do you want to logout from your application?';
                setTimeout('myclose=false', 10);
                myclose = true;
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
  //<![CDATA[
        function HandleClose() {
            alert("Killing the session on the server!!");
            PageMethods.AbandonSession();
        }
   //]]>
    </script>
</head>
<body onunload="HandleClose()" style="background-color: #eeeeee; width: 1024px; margin: 10px 0px 0px 0px;">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <table>
        <tr>
            <td>
                <table border="0" width="1355px" align="center">
                    <tr>
                        <td align="center" valign="middle" style="border-style: solid">
                            <ul id="myMenu" class="nfMain nfPure">
                                <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                                   { %>
                                <li class="nfItem"><a class="nfLink" href="#" style="font-size: 12">
                                    <%= dtMenuItems.Rows[a][0].ToString()  %>
                                    <img src="Images/menuarrow.png" height="14" width="18" /></a>
                                    <div class="nfSubC nfSubS">
                                        <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                           int aa;
                                           for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                           { %>
                                        <div class="nfItem" style="color: Blue; background-color: #eeeeee; font-size: 12">
                                            <a class="nfLink" id='<%= dtSubMenuItems.Rows[aa][0].ToString() %>' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>'
                                                style="color: Black; background-color: #eeeeee; font-size: 12">
                                                <%= dtSubMenuItems.Rows[aa][2].ToString()%></a>
                                        </div>
                                        <% } %>
                                    </div>
                                </li>
                                <% } %>
                                <li class="nfItem"><a class="nfLink" href="Login.aspx" style="border-right-width: 0px;
                                    font-size: 9;">LOGOUT</a></li>
                                <%-- <li style="border-style: none; border-width: 0px 0px 0px 1px; border-color: #333;
                                    padding: 6px 30px 6px 15px; font-family: Arial; font-size: 16px; color: Yellow;
                                    text-decoration: none; background-color: transparent; 
                                    text-align: center; font-weight: bold; height: 17px;">
                                    <div>
                                        <asp:Label ID="lblname" runat="server" Font-Bold="true" /></div>
                                </li>--%>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStatus" runat="server" Font-Bold="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="DivHeader" runat="server" Style="text-align:center">
                                <asp:Label Style="text-shadow: 1px 3px 5px rgba(0,0,0,0.3); font-size: 40px; background: -webkit-linear-gradient(#000 ,#999);
                                    -webkit-background-clip: text; -webkit-text-fill-color: transparent; text-outline: 2px 2px #FFFF00 ;"
                                    ID="lblname" runat="server" Font-Bold="true" Font-Size="XX-Large" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <!-- Start section-->
                            <div id="wowslider-container1">
                                <div class="ws_images">
                                    <ul>
                                        <li>
                                            <img src="themes/data1/images/sjclassicimage.jpg" alt="SJClassic" title="SJClassic"
                                                id="wows1_0" /></li>
                                        <li>
                                            <img src="themes/data1/images/shutter.jpg" alt="SJClassic" title="SJClassic" id="wows1_1" /></li>
                                        <li>
                                            <img src="themes/data1/images/shutter1.jpg" alt="SJClassic" title="SJClassic" id="wows1_2" /></li>
                                        <li>
                                            <img src="themes/data1/images/shutter2.jpg" alt="SJClassic" title="SJClassic" id="wows1_3" /></li>
                                        <li>
                                            <img src="themes/data1/images/shutter3.jpg" alt="SJClassic" title="SJClassic" id="wows1_4" /></a></li>
                                        <li>
                                            <img src="themes/data1/images/shutter4.jpg" alt="SJClassic" title="SJClassic" id="wows1_5" /></li>
                                    </ul>
                                </div>
                                <div class="ws_bullets">
                                    <div>
                                        <a href="#" title="sjclassicimage">
                                            <img src="themes/data1/tooltips/sjclassicimage.jpg" alt="SJClassic" />1</a>
                                        <a href="#" title="Shutter">
                                            <img src="themes/data1/tooltips/shutter.jpg" alt="SJClassic" />2</a> <a href="#"
                                                title="Shutter1">
                                                <img src="themes/data1/tooltips/shutter1.jpg" alt="SJClassic" />3</a> <a href="#"
                                                    title="Shutter2">
                                                    <img src="themes/data1/tooltips/shutter2.jpg" alt="SJClassic" />4</a>
                                        <a href="#" title="Shutter3">
                                            <img src="themes/data1/tooltips/shutter3.jpg" alt="SJClassic" />5</a> <a href="#"
                                                title="Shutter4">
                                                <img src="themes/data1/tooltips/shutter4.jpg" alt="SJClassic" />6</a>
                                    </div>
                                </div>
                               <%-- <span class="wsl"></span>--%>
                                <%--<div class="ws_shadow">
                                </div>--%>
                            </div>
                            <script type="text/javascript" src="themes/engine1/wowslider.js"></script>
                            <script type="text/javascript" src="themes/engine1/script.js"></script>
                            <!-- End section -->
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
