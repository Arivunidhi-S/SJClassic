<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InComing.aspx.cs" Inherits="InComing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Incoming Material</title>
    <link rel="shortcut icon" href="images/Shutter.png" />
    <link rel="stylesheet" href="css/menu_core.css" type="text/css" />
    <link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
    <link rel="stylesheet" href="css/modal.css" type="text/css" />
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
    <script type="text/javascript">
        function JSFunction() {
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
                <table border="0" width="100%">
                    <tr>
                        <td id="Td1" align="left" runat="server" colspan="2" style="border-style: solid">
                            <ul id="myMenu" class="nfMain nfPure">
                                <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                                   { %>
                                <li class="nfItem"><a class="nfLink" href="#">
                                    <%= dtMenuItems.Rows[a][0].ToString()  %>
                                    <img src="Images/menuarrow.png" height="14" width="18" /></a>
                                    <div class="nfSubC nfSubS">
                                        <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                           int aa;
                                           for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                           { %>
                                        <div class="nfItem" style="color: Blue; background-color: #eeeeee">
                                            <a class="nfLink" id='<%= dtSubMenuItems.Rows[aa][0].ToString() %>' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>'
                                                style="color: Black; background-color: #eeeeee">
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
                                    text-decoration: none; background-color: transparent; 
                                    text-align: right; font-weight: bold; height: 17px;">
                                    <div>
                                        <asp:Label ID="lblname" runat="server" Font-Names="Tahoma" /></div>
                                </li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" id="tdStatus" runat="server">
                            <br />
                            <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                text-align: center; font-weight: bold; height: 17px; color: Black;" class="mycss">
                                INCOMING MATERIAL DETAILS
                            </div>
                            <br />
                            <div>
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                            </div>
                            <telerik:RadScriptManager ID="ScriptManager" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                </Scripts>
                            </telerik:RadScriptManager>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td align="center">
                            <ajaxToolkit:TabContainer runat="server" ID="TabContainer1" ActiveTabIndex="0" Width="90%">
                                <ajaxToolkit:TabPanel BackColor="#C0D7E8" runat="server" ID="TabPanel1" HeaderText="Material Details">
                                    <ContentTemplate>
                                        <div style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0);border: 4px solid Black;">
                                            <br />
                                            <asp:Label runat="server" CssClass="labelstyle_1" ID="lblID" Visible="false"></asp:Label>
                                            <table cellpadding="2" cellspacing="2" border="0" width="80%" style="font-weight:bold">
                                                <tr>
                                                    <td align="left" style="width:15">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblName1">Material Type:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cboMaterialType" runat="server" Height="40px" Width="200px"
                                                            OnSelectedIndexChanged="cboMaterialType_SelectChange" AutoPostBack="true" AppendDataBoundItems="True"
                                                            Text='<%# Bind("MaterialType") %>' EmptyMessage="Select">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Spring" Value="1" />
                                                                <telerik:RadComboBoxItem Text="Pulley" Value="2" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblDesignation">Material Code/SI.no:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cboMaterialCode" runat="server" Height="110px" Width="220px"
                                                            OnSelectedIndexChanged="cboMaterialCode_SelectChange" AppendDataBoundItems="True"
                                                            EmptyMessage="Select" AutoPostBack="true" DropDownWidth="300px" ForeColor="Black">
                                                            <HeaderTemplate>
                                                                <table style="width: 300px; font-size: small" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 150px;">
                                                                            Material Code/SI.no&nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="width: 150px;">
                                                                            MaterialName
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table style="width: 300px; font-size: small" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 150px;" align="left">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Materialcode']")%>
                                                                        </td>
                                                                        <td style="width: 150px;" align="left">
                                                                            <%# DataBinder.Eval(Container, "Attributes['MaterialName']")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 15%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblUserId">MaterialName:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <telerik:RadTextBox ID="txtMaterialName" CssClass="textInput" runat="server" Width="220px"
                                                        ForeColor="Black" Enabled="false" MaxLength="50" ToolTip="Max Length:50">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblPass">Size:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <telerik:RadTextBox ID="txtSize" CssClass="textInput" runat="server" Width="220px" Enabled="false"
                                                          ForeColor="Black"  MaxLength="20">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblType">Stock Date:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadDatePicker ID="dtpStockDate" runat="server" Width="200px" PopupDirection="BottomRight"
                                                            DateInput-EmptyMessage="Select Stock Date">
                                                            <Calendar ID="Calendar1" runat="server" ShowRowHeaders="true">
                                                            </Calendar>
                                                            <DateInput ID="DateInput1" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                                            </DateInput>
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="Label1">Quantity:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <telerik:RadTextBox ID="txtQuantity" CssClass="textInput" runat="server" Width="220px" onkeypress="return functionx(event)"
                                                            MaxLength="20">
                                                        </telerik:RadTextBox>
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
                            <div class="text" style="text-align: center;">
                                <telerik:RadButton ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click">
                                    <Icon SecondaryIconCssClass="rbSave" SecondaryIconRight="4" SecondaryIconTop="3">
                                    </Icon>
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnClearItem" runat="server" Text="Add" OnClick="btnclear_Click">
                                    <Icon SecondaryIconCssClass="rbAdd" SecondaryIconRight="4" SecondaryIconTop="3">
                                    </Icon>
                                </telerik:RadButton>
                              
                            </div>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td align="center" style="width: 50%;">
                            <telerik:RadAjaxManager ID="RadAjaxManager10" runat="server">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            <telerik:AjaxUpdatedControl ControlID="TabContainer1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="lblID" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="cboMaterialType">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="cboMaterialCode" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                    <telerik:AjaxSetting AjaxControlID="cboMaterialCode">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="txtMaterialName" />
                                            <telerik:AjaxUpdatedControl ControlID="txtSize" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
                                    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
                                    <TargetControls>
                                        <telerik:TargetInput ControlID="txtEmail" />
                                    </TargetControls>
                                </telerik:RegExpTextBoxSetting>
                            </telerik:RadInputManager>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                            <!-- content start -->
                            <div id="Div1" runat="server" style="width:90%; background-color: transparent; overflow: auto;">
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                    OnDeleteCommand="RadGrid1_DeleteCommand" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    AllowAutomaticDeletes="true" AllowSorting="true" OnItemCommand="RadGrid1_ItemCommand"
                                    AllowFilteringByColumn="true" Skin="Black" PageSize="5">
                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" AllowAutomaticUpdates="false" DataKeyNames="IncomingAutoid"
                                        CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add New User Details">
                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="MaterialType" DataType="System.String" HeaderText="MaterialType"
                                                SortExpression="MaterialType" UniqueName="MaterialType">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Materialcode" DataType="System.String" HeaderText="Material Code"
                                                SortExpression="Materialcode" UniqueName="Materialcode">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MaterialName" DataType="System.String" HeaderText="Material Name"
                                                SortExpression="MaterialName" UniqueName="MaterialName">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="StockDate" DataType="System.String" HeaderText="Stock Date"
                                                DataFormatString="{0:dd/MMM/yyyy}" SortExpression="StockDate" UniqueName="StockDate">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Quantity" DataType="System.String" HeaderText="Quantity"
                                                SortExpression="Quantity" UniqueName="Quantity" AllowFiltering="false">
                                                <HeaderStyle Width="10%"/>
                                            </telerik:GridBoundColumn>
                                            
                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                ConfirmText="Are you sure want to delete?">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridButtonColumn>
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                        </EditFormSettings>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
    </form>
</body>
</html>
