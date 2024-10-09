<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalInsp.aspx.cs" Inherits="FinalInsp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SJ CLassic | Inprocess & Final Inspection Checklist - RD</title>
    <link rel="shortcut icon" href="images/Shutter.png" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
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
<script language="javascript" type="text/javascript">
    history.go(1); /* undo user navigation (ex: IE Back Button) */
</script>
<body onunload="HandleClose()" style="background-color: #eeeeee; width: 1024px; margin: 10px 0px 0px 0px;">
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                    border-bottom: blue thin solid; border-width: 0px" align="center">
                    <table border="0" width="1340px" align="center">
                        <tr>
                            <td align="center" valign="top" style="border-style: solid">
                                <ul id="Ul1" class="nfMain nfPure">
                                    <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                                       { %>
                                    <li class="nfItem"><a class="nfLink" href="#" style="font-size: 14">
                                        <%= dtMenuItems.Rows[a][0].ToString()  %>
                                        <img src="Images/menuarrow.png" height="14" width="18" /></a>
                                        <div class="nfSubC nfSubS">
                                            <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                               int aa;
                                               for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                               { %>
                                            <div class="nfItem" style="color: Blue; background-color: #eeeeee">
                                                <a class="nfLink" id='A1' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>' style="color: Black;
                                                    background-color: #eeeeee">
                                                    <%= dtSubMenuItems.Rows[aa][2].ToString()%></a>
                                            </div>
                                            <% } %>
                                        </div>
                                    </li>
                                    <% } %>
                                    <li class="nfItem"><a class="nfLink" href="Login.aspx" style="border-right-width: 0px;
                                        font-size: 14;">LOGOUT</a></li>
                                    <li style="border-style: none; border-width: 0px 0px 0px 1px; border-color: #333;
                                        padding: 6px 30px 6px 15px; font-family: Arial; font-size: 16px; color: Yellow;
                                        text-decoration: none; background-color: transparent; text-align: right; font-weight: bold;
                                        height: 17px;">
                                        <div>
                                            <asp:Label ID="lblname" runat="server" Font-Names="Tahoma" /></div>
                                    </li>
                                </ul>
                            </td>
                        </tr>
                    </table>
                    <table id="Table2" border="0" runat="server" align="center" style="width: 100%">
                        <tr>
                            <td style="width: 100%">
                                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                    <Scripts>
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                    </Scripts>
                                </telerik:RadScriptManager>
                                <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManager>
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                    text-align: center; font-weight: bold; height: 17px; color: Black;" class="mycss">
                                    Improcess & Final Inspection Checklist - RD
                                </div>
                                <br />
                            </td>
                        </tr>
                    </table>
                    <table id="Table3" border="0" runat="server">
                        <tr>
                            <td style="font-weight: bold">
                                Order Form No:
                            </td>
                            <td align="left">
                                <telerik:RadComboBox ID="cboOrderNo" runat="server" Height="190px" Width="200px"
                                    OnSelectedIndexChanged="cboOrderNo_SelectedChanged" AutoPostBack="true" DropDownWidth="300"
                                    AppendDataBoundItems="True" EmptyMessage="Select">
                                    <HeaderTemplate>
                                        <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 70px;">
                                                    OrderNo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td style="width: 180px;">
                                                    DealerName
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td style="width: 70px;" align="left">
                                                    <%# DataBinder.Eval(Container, "Attributes['OrderNo']")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td style="width: 180px;" align="left">
                                                    <%# DataBinder.Eval(Container, "Attributes['DealerName']")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </telerik:RadComboBox>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">
                                <div style="height: 20px;">
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <div style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid White;
                        width: 90%">
                        <table runat="server" align="center" width="100%" rules="all" style="border-style: groove;
                            font-weight: bold">
                            <tr align="center">
                                <td style="width: 8%">
                                    Serial No
                                </td>
                                <td style="width: 12%">
                                    Process
                                </td>
                                <td style="width: 70%">
                                    Check Item
                                </td>
                                <%--<td style="width: 2%">
                                    Result
                                </td>
                                <td style="width: 4%">
                                    Person in Charge
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="center">
                                    1)
                                </td>
                                <td style="width: 5%" align="center">
                                    Cutting
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="100%" style="font-weight: bold">
                                        <tr>
                                            <td>
                                                Color 1:
                                            </td>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtColor1" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Height 1:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="50px" ID="txtheight1" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="40px" ID="txtColorQty1" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Qty,
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                Color 2:
                                            </td>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtColor2" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Height 2:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="50px" ID="txtheight2" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="40px" ID="txtColorQty2" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Qty,
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Color 3:
                                            </td>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtColor3" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Height 3:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="50px" ID="txtheight3" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="40px" ID="txtColorQty3" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Qty,
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                Color 4:
                                            </td>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtColor4" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Height 4:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="50px" ID="txtheight4" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="40px" ID="txtColorQty4" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Qty,
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Color 5:
                                            </td>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtColor5" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Height 5:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="50px" ID="txtheight5" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="40px" ID="txtColorQty5" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Qty,
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                Color 6:
                                            </td>
                                            <td align="left">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtColor6" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Height 6:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="50px" ID="txtheight6" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="40px" ID="txtColorQty6" Enabled="false" ForeColor="Black" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Qty,
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--  <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboCuttingResult" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargeCut" runat="server" Width="100px" EmptyMessage="Select"  AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="center">
                                    2)
                                </td>
                                <td style="width: 5%" align="center">
                                    Punch
                                </td>
                                <td style="width: 90%" align="center">
                                    <table width="90%">
                                        <tr>
                                            <td>
                                                A) Lock :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPunchLock" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <%--<asp:CheckBox ID="chkPunchLock" runat="server" />--%>
                                            </td>
                                            <td>
                                                B) Other Lock :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtOtherLock" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <%--<asp:CheckBox ID="chkPunchLock" runat="server" />--%>
                                            </td>
                                            <td>
                                                C) Vent Hole :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPunchVenthole" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <%-- <asp:CheckBox ID="chkPunchVent" runat="server" />--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                D) Vent Row :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPunchVentRow" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                                <%-- <asp:CheckBox ID="CheckBox1" runat="server" />--%>
                                            </td>
                                            <td>
                                                E) Letter Box :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPunchLetterbox" runat="server" Width="100px" Enabled="false"
                                                    ForeColor="Black"></asp:TextBox>
                                                <%--<asp:CheckBox ID="chkPunchLetterbox" runat="server" />--%>
                                            </td>
                                            <td>
                                                F) Door Oreintation :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDoorOrentation" runat="server" Width="100px" Enabled="false"
                                                    ForeColor="Black"></asp:TextBox>
                                                <%-- <asp:CheckBox ID="lblDoorOrentation" runat="server" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%-- <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboPunchResult" runat="server" Width="60px" EmptyMessage="Select"  AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargePunch" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="center">
                                    3)
                                </td>
                                <td style="width: 5%" align="center">
                                    Assembled 1
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="90%">
                                        <tr>
                                            <td>
                                                A) Lock :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtass1Lock" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                            <td>
                                                B) Other Lock :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtass1otherlock" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                            <td>
                                                C) Letter Box :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtass1Letterbox" runat="server" Width="100px" Enabled="false" ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                            <td>
                                                B) America Lock
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAssemAmericalock" runat="server" />
                                            </td>
                                            <td>
                                                D) Hidding Lock
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="chkAssemHiddinglock" runat="server" />
                                            </td>
                                        </tr>--%>
                                    </table>
                                </td>
                                <%-- <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboAssemresult" runat="server" Width="60px" AppendDataBoundItems="True" 
                                        Height="40px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargeAss1" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="center">
                                    4)
                                </td>
                                <td style="width: 5%" align="center">
                                    Assembled 2
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="90%">
                                        <tr>
                                            <td>
                                                A) Door Board :
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtAssem2DoorBoard" runat="server" Width="100px" Enabled="false"
                                                    ForeColor="Black" />Pcs = 1 Door
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                B) Aluminium Bottom Bar :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAluminiumBottomBar" runat="server" Width="100px" Enabled="false"
                                                    ForeColor="Black"></asp:TextBox>
                                            </td>
                                            <td>
                                                C) Nylon Polystrip :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtNylonPolystrip" runat="server" Width="100px" Enabled="false"
                                                    ForeColor="Black"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--<td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboAssem2Result" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargeAss2" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="center">
                                    5)
                                </td>
                                <td style="width: 5%" align="center">
                                    Assembled 3
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="80%">
                                        <tr>
                                        </tr>
                                        <tr>
                                            <td>
                                                A) Spring 1:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtSpring1" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="50px" ID="txtSpring1pcs" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td style="width: 10%">
                                                pcs,
                                            </td>
                                            <td style="width: 15%">
                                                B) Spring 2:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtSpring2" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="50px" ID="txtSpring2pcs" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                pcs
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                C) Spring 3:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtSpring3" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="50px" ID="txtSpring3pcs" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                pcs,
                                            </td>
                                            <td>
                                                D) Spring 4:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtSpring4" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox Width="50px" ID="txtSpring4pcs" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                pcs
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                E) Pulley:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtPulleypcs" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td colspan="2">
                                                pcs
                                            </td>
                                            <td>
                                                F) Motor:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtMotor" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="80%">
                                        <tr>
                                            <td>
                                                ControlBox:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtControlBox" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                Voltage:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtVoltage" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                Current:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtCurrent" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                ManualOverride:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtManualOverride" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                RemoteBox:
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtRemoteBox" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                UPSBattery
                                            </td>
                                            <td>
                                                <asp:TextBox Width="100px" ID="txtUPSBattery" runat="server" Enabled="false" ForeColor="Black" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%-- <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboAssem3result" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargeAss3" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            </tr>
                            <%-- <tr>
                                <td style="width: 5%" align="center">
                                    6)
                                </td>
                                <td style="width: 5%" align="center">
                                    Assembled 4
                                </td>
                                <td style="width: 60%" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                Assembled - 2
                                            </td>
                                            <td>
                                                +
                                            </td>
                                            <td>
                                                Assembled - 3
                                            </td>
                                            <td>
                                                &nbsp;
                                                <telerik:RadComboBox ID="cboAssembled4" runat="server" Width="100px" EmptyMessage="Select"
                                                    AppendDataBoundItems="True" AllowCustomText="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                        <telerik:RadComboBoxItem Text="No" Value="2" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboAssem4result" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargeAss4" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 5%" align="center">
                                    6)
                                </td>
                                <td style="width: 5%" align="center">
                                    QC
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="60%">
                                        <tr>
                                            <td>
                                                Width :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQCWidth" runat="server" Width="100px" Enabled="false" ForeColor="Black" />mm,
                                            </td>
                                            <td align="left">
                                            </td>
                                            <td>
                                                Height :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtQCHeight" runat="server" Width="100px" Enabled="false" ForeColor="Black" />
                                                mm
                                            </td>
                                            <td align="left">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <%-- <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboQCResult" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargeQC" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            <%--   <tr>
                                <td style="width: 5%" align="center">
                                    8)
                                </td>
                                <td style="width: 5%" align="center">
                                    Packing
                                </td>
                                <td style="width: 60%" align="center">
                                    <table>
                                        <tr>
                                            <td>
                                                A)
                                            </td>
                                            <td>
                                                Wrapping
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboWrapping" runat="server" Width="100px" EmptyMessage="Select"
                                                    AppendDataBoundItems="True" AllowCustomText="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                        <telerik:RadComboBoxItem Text="No" Value="2" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                                B)
                                            </td>
                                            <td>
                                                Span :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPackingSpan" runat="server" Width="100px" />
                                                Pcs
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboPackingResult" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInchargePack" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                     <Items>
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>--%>
                            <tr>
                                <td style="width: 5%" align="center">
                                    7)
                                </td>
                                <td style="width: 5%" align="center">
                                    Installation
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="80%">
                                        <tr>
                                            <td>
                                                Installation Date :
                                            </td>
                                            <td colspan="3">
                                                <telerik:RadDatePicker ID="dtpInstallDate" runat="server" PopupDirection="BottomRight"
                                                    Enabled="false" Width="200" DateInput-EmptyMessage="Select  Date">
                                                    <Calendar ID="Calendar3" runat="server" ShowRowHeaders="true">
                                                    </Calendar>
                                                    <DateInput ID="DateInput3" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                                    </DateInput>
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Man Installation :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInstallMan1" runat="server" Width="150px" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInstallMan2" runat="server" Width="150px" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInstallMan3" runat="server" Width="150px" Enabled="false" ForeColor="Black" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInstallMan4" runat="server" Width="150px" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInstallMan5" runat="server" Width="150px" Enabled="false" ForeColor="Black" />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtInstallMan6" runat="server" Width="150px" Enabled="false" ForeColor="Black" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--<td style="width: 10%" align="center">
                                    <telerik:RadComboBox ID="cboInstallResult" runat="server" Width="60px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="OK" Value="1" />
                                            <telerik:RadComboBoxItem Text="NG" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                                <td style="width: 10%" align="center">
                                   <telerik:RadComboBox ID="cboInchargeInstall" runat="server" Width="100px" EmptyMessage="Select" AllowCustomText="true">
                                        <Items>
                                            
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>--%>
                            </tr>
                            <tr>
                                <td style="width: 5%" align="center">
                                    8)
                                </td>
                                <td style="width: 5%" align="center">
                                    Status
                                </td>
                                <td style="width: 60%" align="center">
                                    <table width="50%">
                                        <tr>
                                            <td>
                                                Order Process Status :
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="cboOrderProcessStatus" runat="server" Width="150px" EmptyMessage="Select"
                                                    AllowCustomText="true">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="Processing" Value="1" />
                                                        <telerik:RadComboBoxItem Text="Completed" Value="2" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btnSave" runat="server" Text="  Save  " BackColor="Black" ForeColor="White"
                                                    OnClick="btnSave_Click" />
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
                    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                        SelectCommand="select * from OrderForm where deleted=0 ORDER BY [OrderAutoID]">
                    </asp:SqlDataSource>--%>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
