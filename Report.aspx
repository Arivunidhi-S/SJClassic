<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<%@ Register Assembly="Stimulsoft.Report.Web, Version=2011.2.1100.0, Culture=neutral, PublicKeyToken=ebe6666cba19647a"
    Namespace="Stimulsoft.Report.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SJ CLassic | Customer Master</title>
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
    <form id="server" runat="server">
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
                    <tr>
                    </tr>
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
                    <tr style="background-color: transparent" valign="top">
                        <td align="center">
                            <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                text-align: center; font-weight: bold; color: Black;" class="mycss">
                                Reports
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <div>
                                <asp:Label ID="lblStatus" runat="server" ForeColor="White" Font-Bold="true"></asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="Reports :"></asp:Label>
                            &nbsp; &nbsp;
                            <telerik:RadComboBox ID="cboReports" runat="server" Height="180px" Width="150px"
                                AutoPostBack="true" DropDownWidth="150px" AppendDataBoundItems="True" EmptyMessage="Select"
                                OnSelectedIndexChanged="cboReports_Select_Change">
                                <Items>
                                    <telerik:RadComboBoxItem Text="OrderForm" Value="1" />
                                    <telerik:RadComboBoxItem Text="Final Inspection" Value="2" />
                                    <telerik:RadComboBoxItem Text="Customer Details" Value="3" />
                                    <telerik:RadComboBoxItem Text="Material Details" Value="4" />
                                    <telerik:RadComboBoxItem Text="Incoming Material" Value="7" />
                                    <telerik:RadComboBoxItem Text="Staff Details" Value="5" />
                                    <telerik:RadComboBoxItem Text="Warranty Sticker" Value="6" />
                                    <telerik:RadComboBoxItem Text="Spring Details" Value="8" />
                                    <telerik:RadComboBoxItem Text="Sales Report" Value="9" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="lblOrderid" runat="server" Font-Bold="true" Text="OrderNo :"></asp:Label>
                            &nbsp; &nbsp;
                            <telerik:RadComboBox ID="cboOrderID" runat="server" Height="160px" Width="150px" DropDownWidth="150px"
                              OnSelectedIndexChanged="cboOrderID_Select_Change" AutoPostBack="true"  AppendDataBoundItems="True" EmptyMessage="Select">
                                <Items>
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="lblMaterialNo" runat="server" Font-Bold="true" Text="MaterialNo :"></asp:Label>
                            &nbsp; &nbsp;
                            <telerik:RadComboBox ID="cboMatreialNo" runat="server" Height="110px" Width="150px"
                                DropDownWidth="250" AppendDataBoundItems="True" EmptyMessage="Select">
                                <HeaderTemplate>
                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 70px;">
                                                MatreialNo&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td style="width: 180px;">
                                                MatreialName
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 70px;" align="left">
                                                <%# DataBinder.Eval(Container, "Attributes['Materialcode']")%>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp&nbsp;
                                            </td>
                                            <td style="width: 180px;" align="left">
                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </telerik:RadComboBox>
                            &nbsp; &nbsp;
                            <asp:Label ID="lblDateStart" runat="server" Font-Bold="true" Text="StartDate :"></asp:Label>
                            &nbsp; &nbsp;
                            <telerik:RadDatePicker ID="dtStartDate" runat="server" Width="150px" PopupDirection="BottomRight"
                                DateInput-EmptyMessage="Select  Date" OnSelectedDateChanged="dtStartDate_Select_Change" AutoPostBack="true">
                                <Calendar ID="Calendar2" runat="server" ShowRowHeaders="true">
                                </Calendar>
                                <DateInput ID="DateInput2" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                            &nbsp; &nbsp;
                            <asp:Label ID="lblDateEnd" runat="server" Font-Bold="true" Text="EndDate :"></asp:Label>
                            &nbsp; &nbsp;
                            <telerik:RadDatePicker ID="dtEndDate" runat="server" Width="150px" PopupDirection="BottomRight"
                                DateInput-EmptyMessage="Select  Date">
                                <Calendar ID="Calendar1" runat="server" ShowRowHeaders="true">
                                </Calendar>
                                <DateInput ID="DateInput1" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                </DateInput>
                            </telerik:RadDatePicker>
                            &nbsp; &nbsp;
                            <asp:Button ID="btn_Report_Submit" runat="server" Text="  Submit  " BackColor="Black"
                                ForeColor="White" OnClick="btn_Report_Submit_Click" />
                            &nbsp; &nbsp;
                            <%-- <asp:Button ID="btn_Report_Print" runat="server" Text="  Print  " BackColor="Blue" ForeColor="White" OnClick="btn_Report_Print_Click"/>--%>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <div id="divrepo" class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0);
                                border: 4px solid Black; width: 70%; overflow: auto; box-shadow: 3px 5px 6px rgba(0,0,0,0.5);">
                                <br />
                                <cc1:StiWebViewer ID="StiWebViewer1" runat="server" RenderMode="UseCache" ScrollBarsMode="true"
                                    Width="900px" Height="700px" />
                                <br />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 100%;">
                            <div>
                                <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                    SelectCommand="select * from OrderForm where deleted=0 ORDER BY [OrderAutoID]">
                                </asp:SqlDataSource>--%>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
