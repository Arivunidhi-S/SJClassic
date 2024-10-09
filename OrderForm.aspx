<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderForm.aspx.cs" Inherits="OrderForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SJ CLassic | ORDER FORM</title>
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
    <script language="javascript" type="text/javascript">
        function CleartextBoxes(sType) {
            a = document.getElementsByTagName("input");
            for (i = 0; i < a.length; i++) {
                if (a[i].type == sType) {
                    a[i].value = "";
                }
            }
        }
        function functionx(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Allow Only Numbers");
                return false;
            }
        }
    </script>
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
                    <table id="Table2" border="0" runat="server" align="center" style="background-color: transparent;
                        width: 90%">
                        <tr>
                            <td style="width: 100%">
                                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                    <Scripts>
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                        <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                    </Scripts>
                                </telerik:RadScriptManager>
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                    text-align: center; font-weight: bold; height: 17px; color: Black;" class="mycss">
                                    ORDER FORM
                                </div>
                                <br />
                                <div>
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" ActiveTabIndex="0" BackColor="#C0D7E8"
                                    Height="270px">
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabStep1" HeaderText="Step 1:">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;
                                                width: 99%" id="Div1" runat="server">
                                                <table width="100%" style="font-weight: bold">
                                                    <tr>
                                                        <td align="left">
                                                            <telerik:RadButton ID="btnAddNew" runat="server" Text="Add" OnClick="btnclear_Click">
                                                                <Icon SecondaryIconCssClass="rbAdd" SecondaryIconRight="4" SecondaryIconTop="3">
                                                                </Icon>
                                                            </telerik:RadButton>
                                                            <%--  <asp:Button ID="Button1" runat="server" Text="Add New" OnClick="btnclear_Click" />--%>
                                                        </td>
                                                        <td colspan="5">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 17%" align="right">
                                                            Door Type:
                                                        </td>
                                                        <td style="width: 17%">
                                                            <telerik:RadComboBox ID="cboDoorType" runat="server" Height="60px" Width="200px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="PPGL" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Polycarbonate" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="Aluminium" Value="3" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 17px" align="right">
                                                            Serial No:
                                                        </td>
                                                        <td style="width: 17%">
                                                            <asp:TextBox Width="200px" ID="txtdono" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" />
                                                        </td>
                                                        <td style="width: 17px" align="right">
                                                            Man Installation 1:
                                                        </td>
                                                        <td style="width: 17px">
                                                            <telerik:RadComboBox ID="cboman1" runat="server" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Order No :
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtno" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                ForeColor="Black" runat="server" Enabled="false" />
                                                        </td>
                                                        <td align="right">
                                                            Order Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtdate" runat="server" Width="200px" PopupDirection="BottomRight"
                                                                DateInput-EmptyMessage="Select  Date">
                                                                <Calendar ID="Calendar2" runat="server" ShowRowHeaders="true">
                                                                </Calendar>
                                                                <DateInput ID="DateInput2" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td align="right">
                                                            Man Installation 2:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboman2" runat="server" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Order Status:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtstatus" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" ForeColor="Black" Enabled="false" />
                                                        </td>
                                                        <td align="right">
                                                            Shipping Date:
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtdeldate" runat="server" Width="200px" PopupDirection="BottomRight"
                                                                DateInput-EmptyMessage="Select  Date">
                                                                <Calendar ID="Calendar1" runat="server" ShowRowHeaders="true">
                                                                </Calendar>
                                                                <DateInput ID="DateInput1" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                                                </DateInput>
                                                            </telerik:RadDatePicker>
                                                        </td>
                                                        <td align="right">
                                                            Man Installation 3:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboman3" runat="server" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            DealerID:
                                                            <asp:Label ID="lblID" runat="server" Visible="false" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboCustomerID" runat="server" Height="300px" Width="200px"
                                                                AutoPostBack="true" DropDownWidth="460" AppendDataBoundItems="True" EmptyMessage="Select"
                                                                OnSelectedIndexChanged="cboCustomerID_SelectedIndexChanged">
                                                                <HeaderTemplate>
                                                                    <table style="width: 450px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 130px;">
                                                                                DealerID
                                                                            </td>
                                                                            <td style="width: 320px;">
                                                                                DealerName
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 400px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 130px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['CustomerAutoID']")%>
                                                                            </td>
                                                                            <td style="width: 320px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['CustomerName']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td align="right">
                                                            Dealer Name:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtname" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" ForeColor="Black" Enabled="false" />
                                                        </td>
                                                        <td align="right">
                                                            Man Installation 4:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboman4" runat="server" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Mobile No:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txttel" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" ForeColor="Black" Enabled="false" />
                                                        </td>
                                                        <td align="right">
                                                            Method of Delivery:
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cboDelivery" runat="server" Height="60px" Width="200px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="By Self" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="By Dealer" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="By Installer" Value="3" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td align="right">
                                                            Man Installation 5:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboman5" runat="server" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" align="right">
                                                            Address:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtaddress" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                TextMode="MultiLine" Height="60px" runat="server" Wrap="true" ForeColor="Black"
                                                                Enabled="false" />
                                                        </td>
                                                        <td valign="top " align="right">
                                                            Remarks:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtremarks" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                TextMode="MultiLine" Height="60px" runat="server" Wrap="true" />
                                                        </td>
                                                        <td align="right" valign="top">
                                                            Man Installation 6:
                                                        </td>
                                                        <td valign="top">
                                                            <telerik:RadComboBox ID="cboman6" runat="server" Height="100px" Width="150px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabStep2" HeaderText="Step 2:">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;
                                                width: 99%" id="Div3" runat="server">
                                                <table width="70%" style="font-weight: bold">
                                                    <tr>
                                                        <td align="right">
                                                            Height:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox Width="160px" ID="txtpriceheight" runat="server" onkeypress="return functionx(event)" />
                                                                    </td>
                                                                    <td align="left">
                                                                        mm
                                                                    </td>
                                                                    <%--<td>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" ForeColor="Red"
                                                                            EnableClientScript="true" runat="server" ErrorMessage="*" ControlToValidate="txtpriceheight"
                                                                            ValidationExpression="^\d+"> </asp:RegularExpressionValidator>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            Width:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox Width="160px" ID="txtpricewidth" runat="server" onkeypress="return functionx(event)" />
                                                                    </td>
                                                                    <td align="left">
                                                                        mm
                                                                    </td>
                                                                    <%-- <td>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" ForeColor="Red"
                                                                            EnableClientScript="true" runat="server" ErrorMessage="*" ControlToValidate="txtpricewidth"
                                                                            ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Wall Type:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbopriceWallType" runat="server" OnSelectedIndexChanged="cbopriceWallType_SelectedIndexChanged"
                                                                AutoPostBack="true" Height="120px" Width="200px" EmptyMessage="Select" DropDownWidth="260px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Adjustable Wall" Value="0" />
                                                                    <telerik:RadComboBoxItem Text="Non-Adjustable Wall" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Adjustable & Non-Adjustable Wall" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="Adjustable Wall (Separate)" Value="3" />
                                                                    <telerik:RadComboBoxItem Text="Non-Adjustable Wall (Separate)" Value="4" />
                                                                    <telerik:RadComboBoxItem Text="Adjustable & Non-Adjustable Wall (Separate)" Value="5" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td align="right">
                                                            Unit:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox Width="160px" ID="txtunit" runat="server" Enabled="false" ForeColor="Black" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label Text="sqft" runat="server" ID="lblsqft"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Unit Quantity:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox Width="200px" ID="txtUnitQuantity" runat="server" onkeypress="return functionx(event)" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            Base Price:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="txtBasePrice" MinValue="0" MaxValue="999999999999999999999999"
                                                                            AutoPostBack="true" MaxLength="20" Type="Number" runat="server">
                                                                            <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                        </telerik:RadNumericTextBox>
                                                                        <%--   <asp:TextBox Width="90px" ID="txtBasePrice" runat="server" onkeypress="return functionx(event)"
                                                                            Numeric="True"  />--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label Text="RM" runat="server" ID="Label4"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            Tax :
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="cboTax" runat="server" Height="100px" Width="100px" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="1" Value="0" />
                                                                                <telerik:RadComboBoxItem Text="2" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="3" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="4" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="5" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="6" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="7" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="8" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="9" Value="8" />
                                                                                <telerik:RadComboBoxItem Text="10" Value="9" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        %
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            UnitPrice:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadNumericTextBox ID="txtUnitPriceNotax" MinValue="0" MaxValue="999999999999999999999999"
                                                                            AutoPostBack="true" Enabled="false" MaxLength="20" Type="Number" runat="server" ForeColor="Black">
                                                                            <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                        </telerik:RadNumericTextBox>
                                                                        <%--    <asp:TextBox Width="170px" ID="txtUnitPriceNotax" runat="server" Enabled="false" />--%>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label Text="RM" runat="server" ID="Label7"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            UnitPrice with Tax:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtUnitPrice" MinValue="0" MaxValue="999999999999999999999999"
                                                                                AutoPostBack="true" Enabled="false" MaxLength="20" Type="Number" runat="server" ForeColor="Black">
                                                                                <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                            </telerik:RadNumericTextBox>
                                                                            <%--  <asp:TextBox Width="170px" ID="txtUnitPrice" runat="server" Enabled="false" />--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label Text="RM" runat="server" ID="Label3"></asp:Label>
                                                                        </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            Total Amount:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <td>
                                                                            <telerik:RadNumericTextBox ID="txtTotalAmount" MinValue="0" MaxValue="999999999999999999999999" ForeColor="Black"
                                                                                AutoPostBack="true" Enabled="false" MaxLength="20" Type="Number" runat="server" BorderStyle="None">
                                                                                <NumberFormat GroupSeparator="" DecimalDigits="2" />
                                                                            </telerik:RadNumericTextBox>
                                                                            <%-- <asp:TextBox Width="120px" ID="txtTotalAmount" runat="server" Enabled="false" />--%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label Text="RM" runat="server" ID="Label5"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Button ID="btnCalc" OnClick="btnCalc_Click" runat="server" Text="Calc" />
                                                                        </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            Remarks:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtpriceRemarks" runat="server" TextMode="MultiLine"
                                                                Height="60px" />
                                                        </td>
                                                        <td align="right" valign="top">
                                                            Special Specifications:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="textSpecialSpec" runat="server" TextMode="MultiLine"
                                                                Height="60px" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabStep3" HeaderText="Step 3:">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;
                                                width: 99%" id="Div4" runat="server">
                                                <br />
                                                <table width="100%" style="font-weight: bold" height="230px">
                                                    <tr>
                                                        <td>
                                                            Total Height 1:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox Width="80px" ID="txtheightcal" runat="server" Enabled="false" ForeColor="Black" />
                                                                    </td>
                                                                    <td>
                                                                        mm
                                                                    </td>
                                                                     <%--<td>
                                                                        Height 2:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="80px" ID="txtheightcal2" runat="server" Enabled="false" ForeColor="Black" />
                                                                    </td>
                                                                     <td>
                                                                        mm
                                                                    </td>--%>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            Total Width 1:
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox Width="80px" ID="txtwidthcal" runat="server" Enabled="false" ForeColor="Black" />
                                                                    </td>
                                                                     <td>
                                                                        mm,
                                                                    </td>
                                                                     <td>
                                                                        Width 2:
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="80px" ID="txtwidthcal2" runat="server" Enabled="false" ForeColor="Black" />
                                                                    </td>
                                                                     <td>
                                                                        mm
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        Balance Height :&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblbalheight" runat="server" ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Color 1:
                                                        </td>
                                                        <td align="left">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="cboColor1" runat="server" Height="160px" Width="100px" AppendDataBoundItems="True"
                                                                            DropDownWidth="150px" EmptyMessage="Color 1">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Ivory White (SJIV01)" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Natural Yellow (SJSY02)" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Yellow (SJY03)" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Brown (SJTB04)" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Grey (SJMG05)" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Green (SJFG06)" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Jaguar Grey (SJJG07)" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Orange (SJSO08)" Value="8" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Height 1:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="CboColorheight1" runat="server" Height="40px" Width="70px"
                                                                            AppendDataBoundItems="True" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="705" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="310" Value="2" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="40px" ID="txtColorQty1" runat="server" onkeypress="return functionx(event)"
                                                                            OnTextChanged="txtColorQty1_TextChange" AutoPostBack="true" />
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
                                                                        <telerik:RadComboBox ID="cboColor2" runat="server" Height="160px" Width="100px" AppendDataBoundItems="True"
                                                                            DropDownWidth="150px" EmptyMessage="Color 2">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Ivory White (SJIV01)" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Natural Yellow (SJSY02)" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Yellow (SJY03)" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Brown (SJTB04)" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Grey (SJMG05)" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Green (SJFG06)" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Jaguar Grey (SJJG07)" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Orange (SJSO08)" Value="8" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Height 2:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="CboColorheight2" runat="server" Height="40px" Width="70px"
                                                                            AppendDataBoundItems="True" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="705" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="310" Value="2" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="40px" ID="txtColorQty2" runat="server" onkeypress="return functionx(event)"
                                                                            OnTextChanged="txtColorQty2_TextChange" AutoPostBack="true" />
                                                                    </td>
                                                                    <td>
                                                                        Qty
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td rowspan="3">
                                                            <img src="images/Color.jpg" width="250px" height="175px" alt="Colors in Door" />
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
                                                                        <telerik:RadComboBox ID="cboColor3" runat="server" Height="160px" Width="100px" AppendDataBoundItems="True"
                                                                            DropDownWidth="150px" EmptyMessage="Color 3">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Ivory White (SJIV01)" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Natural Yellow (SJSY02)" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Yellow (SJY03)" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Brown (SJTB04)" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Grey (SJMG05)" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Green (SJFG06)" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Jaguar Grey (SJJG07)" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Orange (SJSO08)" Value="8" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Height 3:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="CboColorheight3" runat="server" Height="40px" Width="70px"
                                                                            AppendDataBoundItems="True" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="705" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="310" Value="2" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="40px" ID="txtColorQty3" runat="server" onkeypress="return functionx(event)"
                                                                            OnTextChanged="txtColorQty3_TextChange" AutoPostBack="true" />
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
                                                                        <telerik:RadComboBox ID="cboColor4" runat="server" Height="160px" Width="100px" AppendDataBoundItems="True"
                                                                            DropDownWidth="150px" EmptyMessage="Color 4">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Ivory White (SJIV01)" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Natural Yellow (SJSY02)" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Yellow (SJY03)" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Brown (SJTB04)" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Grey (SJMG05)" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Green (SJFG06)" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Jaguar Grey (SJJG07)" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Orange (SJSO08)" Value="8" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Height 4:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="CboColorheight4" runat="server" Height="40px" Width="70px"
                                                                            AppendDataBoundItems="True" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="705" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="310" Value="2" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="40px" ID="txtColorQty4" runat="server" onkeypress="return functionx(event)"
                                                                            OnTextChanged="txtColorQty4_TextChange" AutoPostBack="true" />
                                                                    </td>
                                                                    <td>
                                                                        Qty
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
                                                                        <telerik:RadComboBox ID="cboColor5" runat="server" H Height="160px" Width="100px"
                                                                            AppendDataBoundItems="True" DropDownWidth="150px" EmptyMessage="Color 5">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Ivory White (SJIV01)" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Natural Yellow (SJSY02)" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Yellow (SJY03)" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Brown (SJTB04)" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Grey (SJMG05)" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Green (SJFG06)" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Jaguar Grey (SJJG07)" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Orange (SJSO08)" Value="8" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Height 5:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="CboColorheight5" runat="server" Height="40px" Width="70px"
                                                                            AppendDataBoundItems="True" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="705" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="310" Value="2" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="40px" ID="txtColorQty5" runat="server" onkeypress="return functionx(event)"
                                                                            OnTextChanged="txtColorQty5_TextChange" AutoPostBack="true" />
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
                                                                        <telerik:RadComboBox ID="cboColor6" runat="server" Height="160px" Width="100px" AppendDataBoundItems="True"
                                                                            DropDownWidth="150px" EmptyMessage="Color 6">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="Ivory White (SJIV01)" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="Natural Yellow (SJSY02)" Value="2" />
                                                                                <telerik:RadComboBoxItem Text="Yellow (SJY03)" Value="3" />
                                                                                <telerik:RadComboBoxItem Text="Brown (SJTB04)" Value="4" />
                                                                                <telerik:RadComboBoxItem Text="Grey (SJMG05)" Value="5" />
                                                                                <telerik:RadComboBoxItem Text="Green (SJFG06)" Value="6" />
                                                                                <telerik:RadComboBoxItem Text="Jaguar Grey (SJJG07)" Value="7" />
                                                                                <telerik:RadComboBoxItem Text="Orange (SJSO08)" Value="8" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        Height 6:
                                                                    </td>
                                                                    <td>
                                                                        <telerik:RadComboBox ID="CboColorheight6" runat="server" Height="40px" Width="70px"
                                                                            AppendDataBoundItems="True" EmptyMessage="Select">
                                                                            <Items>
                                                                                <telerik:RadComboBoxItem Text="705" Value="1" />
                                                                                <telerik:RadComboBoxItem Text="310" Value="2" />
                                                                            </Items>
                                                                        </telerik:RadComboBox>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox Width="40px" ID="txtColorQty6" runat="server" onkeypress="return functionx(event)"
                                                                            OnTextChanged="txtColorQty6_TextChange" AutoPostBack="true" />
                                                                    </td>
                                                                    <td>
                                                                        Qty
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabStep4" HeaderText="Step 4:">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;
                                                width: 99%" id="Div5" runat="server">
                                                <br />
                                                <table width="100%" style="font-weight: bold" height="230px">
                                                    <%--<tr>
                                                        <td colspan="8" align="center" class="mycss" style="color: Black;">
                                                            <b>Accessories Form</b>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td style="width: 15%">
                                                            Ventilation Holes:
                                                        </td>
                                                        <td align="left" style="width: 10%">
                                                            <telerik:RadComboBox ID="cboVentilationHole" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                            Ventilation Rows:
                                                        </td>
                                                        <td style="width: 10%">
                                                            <asp:TextBox Width="100px" ID="txtVentilationRows" runat="server" />
                                                        </td>
                                                        <td style="width: 15%">
                                                            Letter Box:
                                                        </td>
                                                        <td style="width: 10%">
                                                            <telerik:RadComboBox ID="cboLetterBox" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Name Plates:
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="cboNamePlates" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Lock Type:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboLockType" runat="server" Height="100px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select" DropDownWidth="170px">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Hidding Lock" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Galvanized America Lock" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="StainlessSteel America Lock" Value="3" />
                                                                    <telerik:RadComboBoxItem Text="Central Lock" Value="4" />
                                                                    <telerik:RadComboBoxItem Text="None" Value="5" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Other Lock Types:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="100px" ID="txtOtherLockTypes" runat="server" />
                                                        </td>
                                                        <td>
                                                            Motor:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboMotor" runat="server" Height="60px" Width="100px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select" AutoPostBack="true" OnSelectedIndexChanged="cboMotor_SelectedIndexChanged">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="None" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Left" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="Right" Value="3" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                            Control Box:
                                                        </td>
                                                        <td style="width: 10%">
                                                            <telerik:RadComboBox ID="cboControlBox" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="9701" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="1201" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Voltage:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboVoltage" runat="server" Height="40px" Width="100px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="220v" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="110v" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Current:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboCurrent" runat="server" Height="60px" Width="100px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="5A" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="8A" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="10A" Value="3" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Manual Override:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboManualOverride" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="One Side" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Two Side" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Remote Box:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboRemoteBox" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            UPS Battery:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboUPSBattery" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Door Orientation:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboDoorOrientation" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Indoor" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Outdoor" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Pull Handle:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboPullHandle" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Pull Hook:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboPullHook" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Packing:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboPacking" runat="server" Height="60px" Width="100px" AppendDataBoundItems="True"
                                                                EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Pack  All" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="Small" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="Large" Value="3" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Warranty on Door Panel:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboWarrantyDoor" runat="server" Height="100px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1 Year" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="2 Years" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="3 Years" Value="3" />
                                                                    <telerik:RadComboBoxItem Text="4 Years" Value="4" />
                                                                    <telerik:RadComboBoxItem Text="5 Years" Value="5" />
                                                                    <telerik:RadComboBoxItem Text="6 Years" Value="6" />
                                                                    <telerik:RadComboBoxItem Text="7 Years" Value="7" />
                                                                    <telerik:RadComboBoxItem Text="8 Years" Value="8" />
                                                                    <telerik:RadComboBoxItem Text="9 Years" Value="9" />
                                                                    <telerik:RadComboBoxItem Text="10 Years" Value="10" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Warranty on Motor:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboWarrantyMotor" runat="server" Height="100px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="1 Year" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="2 Years" Value="2" />
                                                                    <telerik:RadComboBoxItem Text="3 Years" Value="3" />
                                                                    <telerik:RadComboBoxItem Text="4 Years" Value="4" />
                                                                    <telerik:RadComboBoxItem Text="5 Years" Value="5" />
                                                                    <telerik:RadComboBoxItem Text="6 Years" Value="6" />
                                                                    <telerik:RadComboBoxItem Text="7 Years" Value="7" />
                                                                    <telerik:RadComboBoxItem Text="8 Years" Value="8" />
                                                                    <telerik:RadComboBoxItem Text="9 Years" Value="9" />
                                                                    <telerik:RadComboBoxItem Text="10 Years" Value="10" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Special Remarks
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="125px" ID="txtsplremarks" TextMode="MultiLine" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 15%">
                                                            AluminiumBottomBar:
                                                        </td>
                                                        <td style="width: 10%">
                                                            <telerik:RadComboBox ID="cboAluminiumBottomBar" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td style="width: 15%">
                                                            Nylon Polystrip:
                                                        </td>
                                                        <td style="width: 10%">
                                                            <telerik:RadComboBox ID="cboNylonPolystrip" runat="server" Height="40px" Width="100px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            Send Mail:
                                                            <asp:Label ID="Label2" runat="server" Visible="false" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbostaffid" runat="server" Height="100px" Width="150px"
                                                                DropDownWidth="300" OnSelectedIndexChanged="cbostaffid_SelectedIndexChanged"
                                                                AutoPostBack="true" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 70px;">
                                                                                Designation&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td style="width: 180px;">
                                                                                Staff Name
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 70px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Designation']")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td style="width: 180px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Name']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="175px" ID="txtStaffMail" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" ForeColor="Black" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <%--<asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />--%>
                                                            <telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click">
                                                                <Icon SecondaryIconCssClass="rbSave" SecondaryIconRight="4" SecondaryIconTop="3">
                                                                </Icon>
                                                            </telerik:RadButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabStep5" HeaderText="Step 5:">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;
                                                width: 99%" id="Div2" runat="server">
                                                <br />
                                                <table width="50%" style="font-weight: bold" height="230px">
                                                    <tr>
                                                        <td>
                                                            Approval:
                                                        </td>
                                                        <td>
                                                            <asp:CheckBox ID="chkApproval" runat="server" OnCheckedChanged="chkApproval_CheckedChange"
                                                                AutoPostBack="true" />
                                                            <%-- <telerik:RadComboBox ID="cboApproval" runat="server" Height="40px" Width="150px"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <Items>
                                                                    <telerik:RadComboBoxItem Text="Yes" Value="1" />
                                                                    <telerik:RadComboBoxItem Text="No" Value="2" />
                                                                </Items>
                                                            </telerik:RadComboBox>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Send Mail:
                                                            <asp:Label ID="Label1" runat="server" Visible="false" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbostaffid2" runat="server" Height="100px" Width="200px"
                                                                AutoPostBack="true" OnSelectedIndexChanged="cbostaffid2_SelectedIndexChanged"
                                                                DropDownWidth="300" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 70px;">
                                                                                StaffID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td style="width: 180px;">
                                                                                Staff Name
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 70px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['StaffID']")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td style="width: 180px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Name']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtsendmail2" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" ForeColor="Black" Enabled="false" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" />--%>
                                                            <telerik:RadButton ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click">
                                                                <Icon SecondaryIconCssClass="rbUpload" SecondaryIconRight="4" SecondaryIconTop="3">
                                                                </Icon>
                                                            </telerik:RadButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                    <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="tabStep6" HeaderText="Step 6:">
                                        <ContentTemplate>
                                            <div class="text" style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;
                                                width: 99%" id="Div6" runat="server">
                                                <table width="70%" style="font-weight: bold" height="245px">
                                                    <tr>
                                                        <td style="width: 25%">
                                                            Pulley:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="150px" ID="txtPulleypcs" runat="server" />
                                                        </td>
                                                        <td colspan="6">
                                                            pcs
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Spring 1:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboSpring1" runat="server" Height="160px" Width="150px"
                                                                DropDownWidth="230px" AutoPostBack="true" OnSelectedIndexChanged="cboSpring1_SelectedIndexChanged"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 210px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Spring Name
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                Qty
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 200px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                            </td>
                                                                            <td style="width: 50px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Quantity']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="65px" ID="txtSpring1" runat="server" onkeypress="return functionx(event)"
                                                                OnTextChanged="txtSpring1_TextChange" AutoPostBack="true" />
                                                        </td>
                                                        <td style="width: 10%">
                                                            pcs,
                                                        </td>
                                                        <td style="width: 25%">
                                                            Spring 2:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboSpring2" runat="server" Height="160px" Width="150px"
                                                                DropDownWidth="230px" AutoPostBack="true" OnSelectedIndexChanged="cboSpring2_SelectedIndexChanged"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 210px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Spring Name
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                Qty
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 200px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                            </td>
                                                                            <td style="width: 50px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Quantity']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="65px" ID="txtSpring2" runat="server" onkeypress="return functionx(event)"
                                                                OnTextChanged="txtSpring2_TextChange" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            pcs
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Spring 3:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboSpring3" runat="server" Height="160px" Width="150px"
                                                                DropDownWidth="230px" AutoPostBack="true" OnSelectedIndexChanged="cboSpring3_SelectedIndexChanged"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 210px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Spring Name
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                Qty
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 200px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                            </td>
                                                                            <td style="width: 50px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Quantity']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="65px" ID="txtSpring3" runat="server" onkeypress="return functionx(event)"
                                                                OnTextChanged="txtSpring3_TextChange" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            pcs,
                                                        </td>
                                                        <td>
                                                            Spring 4:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboSpring4" runat="server" Height="160px" Width="150px"
                                                                DropDownWidth="230px" AutoPostBack="true" OnSelectedIndexChanged="cboSpring4_SelectedIndexChanged"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 210px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Spring Name
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                Qty
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 200px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                            </td>
                                                                            <td style="width: 50px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Quantity']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="65px" ID="txtSpring4" runat="server" onkeypress="return functionx(event)"
                                                                OnTextChanged="txtSpring4_TextChange" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            pcs
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Spring 5:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboSpring5" runat="server" Height="160px" Width="150px"
                                                                DropDownWidth="230px" AutoPostBack="true" OnSelectedIndexChanged="cboSpring5_SelectedIndexChanged"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 210px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Spring Name
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                Qty
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 200px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                            </td>
                                                                            <td style="width: 50px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Quantity']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="65px" ID="txtSpring5" runat="server" onkeypress="return functionx(event)"
                                                                OnTextChanged="txtSpring5_TextChange" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            pcs,
                                                        </td>
                                                        <td>
                                                            Spring 6:
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cboSpring6" runat="server" Height="160px" Width="150px"
                                                                DropDownWidth="230px" AutoPostBack="true" OnSelectedIndexChanged="cboSpring6_SelectedIndexChanged"
                                                                AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 210px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;">
                                                                                Spring Name
                                                                            </td>
                                                                            <td style="width: 50px;">
                                                                                Qty
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 200px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 150px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                            </td>
                                                                            <td style="width: 50px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Quantity']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="65px" ID="txtSpring6" runat="server" onkeypress="return functionx(event)"
                                                                OnTextChanged="txtSpring6_TextChange" AutoPostBack="true" />
                                                        </td>
                                                        <td>
                                                            pcs
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Send Mail:
                                                            <asp:Label ID="Label6" runat="server" Visible="false" />
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cbostaffid3" runat="server" Height="100px" Width="150px"
                                                                DropDownWidth="300" OnSelectedIndexChanged="cbostaffid3_SelectedIndexChanged"
                                                                AutoPostBack="true" AppendDataBoundItems="True" EmptyMessage="Select">
                                                                <HeaderTemplate>
                                                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 70px;">
                                                                                StaffID&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td style="width: 180px;">
                                                                                Staff Name
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <table style="width: 250px; font-size: small" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td style="width: 70px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['StaffID']")%>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td style="width: 180px;" align="left">
                                                                                <%# DataBinder.Eval(Container, "Attributes['Name']")%>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox Width="175px" ID="txtsendmail3" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" ForeColor="Black" Enabled="false" />
                                                        </td>
                                                        <td>
                                                            <%-- <asp:Button ID="btnUpdateSpring" runat="server" Text="Save" OnClick="btnSave_Click" />--%>
                                                            <telerik:RadButton ID="btnUpdateSpring" runat="server" Text="Update" OnClick="btnUpdateSpring_Click">
                                                                <Icon SecondaryIconCssClass="rbUpload" SecondaryIconRight="4" SecondaryIconTop="3">
                                                                </Icon>
                                                            </telerik:RadButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                            </div>
                                        </ContentTemplate>
                                    </ajaxToolkit:TabPanel>
                                </ajaxToolkit:TabContainer>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table4" border="0" runat="server" align="center" style="width: 100%">
                                    <tr align="center">
                                        <td>
                                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                                <AjaxSettings>
                                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                                        <UpdatedControls>
                                                            <%--Step 1:--%>
                                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtno" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtname" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboCustomerID" />
                                                            <telerik:AjaxUpdatedControl ControlID="txttel" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtaddress" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtdate" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtdeldate" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtdono" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtstatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboDelivery" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboDoorType" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtremarks" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboman1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboman2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboman3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboman4" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboman5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboman6" />
                                                            <%--Step 2:--%>
                                                            <telerik:AjaxUpdatedControl ControlID="cbopriceWallType" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtpriceheight" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtpricewidth" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtunit" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtUnitQuantity" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtBasePrice" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboTax" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtUnitPriceNotax" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtUnitPrice" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtTotalAmount" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtpriceRemarks" />
                                                            <telerik:AjaxUpdatedControl ControlID="textSpecialSpec" />
                                                            <%--Step 3:--%>
                                                            <telerik:AjaxUpdatedControl ControlID="txtheightcal" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtwidthcal" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtwidthcal2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboColor1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboColor2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboColor3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboColor4" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboColor5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboColor6" />
                                                            <telerik:AjaxUpdatedControl ControlID="CboColorheight1" />
                                                            <telerik:AjaxUpdatedControl ControlID="CboColorheight2" />
                                                            <telerik:AjaxUpdatedControl ControlID="CboColorheight3" />
                                                            <telerik:AjaxUpdatedControl ControlID="CboColorheight4" />
                                                            <telerik:AjaxUpdatedControl ControlID="CboColorheight5" />
                                                            <telerik:AjaxUpdatedControl ControlID="CboColorheight6" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty1" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty2" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty3" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty4" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty5" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                            <%--Step 4:--%>
                                                            <telerik:AjaxUpdatedControl ControlID="cboVentilationHole" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtVentilationRows" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboLetterBox" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboControlBox" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboLockType" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtOtherLockTypes" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboVoltage" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboCurrent" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboUPSBattery" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboNamePlates" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboMotor" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboManualOverride" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboRemoteBox" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboDoorOrientation" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboPullHandle" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboPullHook" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboPacking" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboWarrantyDoor" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboWarrantyMotor" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtsplremarks" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboAluminiumBottomBar" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboNylonPolystrip" />
                                                            <telerik:AjaxUpdatedControl ControlID="cbostaffid" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtStaffMail" />
                                                            <%--Step 5:--%>
                                                            <telerik:AjaxUpdatedControl ControlID="chkApproval" />
                                                            <telerik:AjaxUpdatedControl ControlID="cbostaffid2" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtsendmail2" />
                                                            <%--Step 6:--%>
                                                            <telerik:AjaxUpdatedControl ControlID="txtPulleypcs" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring4" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSpring4" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="cbostaffid3" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtsendmail3" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtColorQty1">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty1" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtColorQty2">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty2" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtColorQty3">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty3" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtColorQty4">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty4" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtColorQty5">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty5" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtColorQty6">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtColorQty6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblbalheight" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboMotor">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboControlBox" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboVoltage" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboCurrent" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboManualOverride" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboRemoteBox" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboUPSBattery" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboWarrantyMotor" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cbostaffid3">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtsendmail3" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboSpring1">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring4" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboSpring2">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring4" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboSpring3">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring4" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboSpring4">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboSpring5">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring6" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboSpring6">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring2" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring3" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring1" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring5" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboSpring4" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="chkApproval">
                                                        <UpdatedControls>
                                                            <%--<telerik:AjaxUpdatedControl ControlID="btnUpdate" />--%>
                                                            <telerik:AjaxUpdatedControl ControlID="cbostaffid2" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtsendmail2" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cboCustomerID">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtname" />
                                                            <telerik:AjaxUpdatedControl ControlID="cboCustomerID" />
                                                            <telerik:AjaxUpdatedControl ControlID="txttel" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtaddress" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cbopriceWallType">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtunit" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtheightcal" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtwidthcal" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtwidthcal2" />
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cbostaffid">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtStaffMail" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="cbostaffid2">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="txtsendmail2" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtSpring1">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtSpring2">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtSpring3">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtSpring4">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtSpring5">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                    <telerik:AjaxSetting AjaxControlID="txtSpring6">
                                                        <UpdatedControls>
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                </AjaxSettings>
                                            </telerik:RadAjaxManager>
                                            <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                OnDeleteCommand="RadGrid1_DeleteCommand" OnItemCommand="RadGrid1_ItemCommand"
                                                AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                                PageSize="5" AllowMultiRowEdit="false" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                                PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowAutomaticDeletes="true" Width="100%"
                                                AllowSorting="true" AllowFilteringByColumn="true" Skin="Black">
                                                <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                                    <Selecting AllowRowSelect="true" />
                                                </ClientSettings>
                                                <MasterTableView AutoGenerateColumns="false" DataKeyNames="OrderAutoID" CommandItemDisplay="None">
                                                    <%--CommandItemSettings-AddNewRecordText="Add New Order Details"--%>
                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                    <Columns>
                                                        <%--<telerik:GridEditCommandColumn ButtonType="ImageButton">
                                    <HeaderStyle Width="5%" />
                                </telerik:GridEditCommandColumn>--%>
                                                        <telerik:GridBoundColumn DataField="OrderAutoID" DataType="System.Int64" HeaderText="OrderAutoID"
                                                            ReadOnly="True" SortExpression="OrderAutoID" UniqueName="OrderAutoID" AllowFiltering="false"
                                                            AllowSorting="false" Visible="false">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OrderNo" DataType="System.String" HeaderText="OrderNo"
                                                            SortExpression="OrderNo" UniqueName="OrderNo">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DoorType" DataType="System.String" HeaderText="Door Type"
                                                            SortExpression="DoorType" UniqueName="DoorType" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DealerName" DataType="System.String" HeaderText="Dealer Name"
                                                            SortExpression="DealerName" UniqueName="DealerName">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Date" DataType="System.DateTime" HeaderText="Order Date"
                                                            DataFormatString="{0:dd/MMM/yyyy}" SortExpression="Date" UniqueName="Date">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="OrdStatus" DataType="System.String" HeaderText="OrderStatus"
                                                            SortExpression="OrdStatus" UniqueName="OrdStatus" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="DeliveryType" DataType="System.String" HeaderText="Delivery Type"
                                                            SortExpression="DeliveryType" UniqueName="DeliveryType" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Walltype" DataType="System.String" HeaderText="Walltype"
                                                            SortExpression="Walltype" UniqueName="Walltype" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Height" DataType="System.String" HeaderText="Height"
                                                            SortExpression="Height" UniqueName="Height" AllowFiltering="false">
                                                            <HeaderStyle Width="5%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Width" DataType="System.String" HeaderText="Width"
                                                            SortExpression="Width" UniqueName="Width" AllowFiltering="false">
                                                            <HeaderStyle Width="5%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="LockType" DataType="System.DateTime" HeaderText="Lock Type"
                                                            SortExpression="LockType" UniqueName="LockType" AllowFiltering="false">
                                                            <HeaderStyle Width="10%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ClerkName" DataType="System.DateTime" HeaderText="Created By"
                                                            SortExpression="ClerkName" UniqueName="ClerkName" AllowFiltering="false">
                                                            <HeaderStyle Width="30%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="WrntyDoor" DataType="System.String" HeaderText="Door Warranty"
                                                            SortExpression="WrntyDoor" UniqueName="WrntyDoor" AllowFiltering="false">
                                                            <HeaderStyle Width="350%" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                            ConfirmText="Are you sure want to delete?">
                                                            <HeaderStyle Width="5%" />
                                                        </telerik:GridButtonColumn>
                                                    </Columns>
                                                    <%--<EditFormSettings EditFormType="Template">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                      
                                        <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                            <tr>
                                                <td colspan="2">
                                                  <asp:Button ID="Button1" runat="server" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'
                                                        CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                    </asp:Button>
                                                    <asp:Button ID="Button2" runat="server" Text="Cancel" CausesValidation="false" CommandName="Cancel">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                </FormTemplate>
                            </EditFormSettings>--%>
                                                    <EditFormSettings EditFormType="Template">
                                                        <EditColumn UniqueName="EditCommandColumn1">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                </MasterTableView></telerik:RadGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:SqlDataSource ID="Sqlcustomer" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                        SelectCommand="select customerid,customername from customer where deleted=0 ORDER BY [customerid]">
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                        SelectCommand="select MaterialName from MaterialMaster where deleted=0 ORDER BY [MaterialAutoid]">
                    </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
