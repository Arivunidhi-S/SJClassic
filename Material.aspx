<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Material.aspx.cs" Inherits="Material" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SJ CLassic | Material Master</title>
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
                                    text-decoration: none; background-color: transparent; 
                                    text-align: right; font-weight: bold; height: 17px;">
                                    <div>
                                        <asp:Label ID="lblname" runat="server" Font-Names="Tahoma" /></div>
                                </li>
                                </ul>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%-- <div style="height: 20px;">
                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                                </div>--%>
                            </td>
                        </tr>
                        <tr style="background-color: transparent; height: 550px" valign="top">
                            <td align="center">
                                <table border="0" align="center" style="background-color: transparent;">
                                    <tr valign="top">
                                        <td align="center">
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
                                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                                            <telerik:AjaxUpdatedControl ControlID="txtSize" />
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                </AjaxSettings>
                                            </telerik:RadAjaxManager>
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                             <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                                        Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                                        <TargetControls>
                                                            <telerik:TargetInput ControlID="cbocustomerid" />
                                                            <telerik:TargetInput ControlID="txtMaterialcode" />
                                                            <telerik:TargetInput ControlID="txtMaterialName" />
                                                        </TargetControls>
                                                    </telerik:TextBoxSetting>
                                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
                                                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
                                                        <TargetControls>
                                                            <telerik:TargetInput ControlID="txtEmail" />
                                                        </TargetControls>
                                                    </telerik:RegExpTextBoxSetting>
                                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior3" ValidationExpression="[0-9]"
                                                        ErrorMessage="Enter more than 6 figures" IsRequiredFields="true" Validation-IsRequired="true">
                                                        <TargetControls>
                                                            <telerik:TargetInput ControlID="txtSize" />
                                                        </TargetControls>
                                                    </telerik:RegExpTextBoxSetting>
                                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior4" ValidationExpression="[0-9]{6,}"
                                                        ErrorMessage="Enter more than 6 figures">
                                                        <TargetControls>
                                                            <telerik:TargetInput ControlID="txtFax" />
                                                        </TargetControls>
                                                    </telerik:RegExpTextBoxSetting>
                                                </telerik:RadInputManager>
                                            <!-- content start -->
                                            <div id="Div10" runat="server" style="background-color: transparent; overflow: auto;"
                                                align="center">
                                               
                                                <div runat="server" style="font-size: x-large;
                                                   font-weight: bold; color: Black;" class="mycss">
                                                    <br />
                                                    MATERIAL MASTER DETAILS
                                                     <br />
                                                    <br />
                                                </div>
                                               
                                                <div>
                                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                </div>
                                                <br />
                                                <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                    OnItemCreated="RadGrid1_ItemCreated" AllowPaging="True" PagerStyle-AlwaysVisible="true"
                                                    OnItemDataBound="RadGrid1_ItemDataBound" PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand"
                                                    OnInsertCommand="RadGrid1_InsertCommand" OnUpdateCommand="RadGrid1_UpdateCommand"
                                                    AllowAutomaticUpdates="true" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                                    AllowAutomaticDeletes="true" Width="90%" AllowSorting="true" AllowFilteringByColumn="true"
                                                    Skin="Black">
                                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="MaterialAutoid" CommandItemDisplay="Top"
                                                        CommandItemSettings-AddNewRecordText="Add New Material Details">
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <Columns>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                                <HeaderStyle Width="5%" />
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridBoundColumn DataField="MaterialAutoid " DataType="System.Int64" HeaderText="ID"
                                                                ReadOnly="True" SortExpression="MaterialAutoid" UniqueName="MaterialAutoid" AllowFiltering="false"
                                                                AllowSorting="false" Visible="false">
                                                            </telerik:GridBoundColumn>
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
                                                            <telerik:GridBoundColumn DataField="Quantity" DataType="System.String" HeaderText="Quantity"
                                                                SortExpression="Quantity" UniqueName="Quantity">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="MnfDate" DataType="System.String" HeaderText="Stock Date"
                                                                DataFormatString="{0:dd/MMM/yyyy}" SortExpression="MnfDate" UniqueName="MnfDate">
                                                                <HeaderStyle Width="10%" />
                                                            </telerik:GridBoundColumn>
                                                            <%--<telerik:GridBoundColumn DataField="Expiredate" DataType="System.String" HeaderText="Expiredate"
                                                                SortExpression="Expiredate" UniqueName="Expiredate">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>--%>
                                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                                ConfirmText="Are you sure want to delete?">
                                                                <HeaderStyle Width="5%" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <EditFormSettings EditFormType="Template">
                                                            <EditColumn UniqueName="EditCommandColumn1">
                                                            </EditColumn>
                                                            <FormTemplate>
                                                                <table cellspacing="2" cellpadding="1" width="90%" border="0">
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            <b>ID:
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("MaterialAutoid")%>' />
                                                                            </b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Material Type:
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="cboMaterialType" runat="server" Height="40px" Width="200px"
                                                                              Text='<%# Bind("MaterialType") %>'  AppendDataBoundItems="True"  EmptyMessage="Select">
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Spring" Value="1" />
                                                                                    <telerik:RadComboBoxItem Text="Pulley" Value="2" />
                                                                                </Items>
                                                                            </telerik:RadComboBox>
                                                                        </td>
                                                                        <td>
                                                                            Material Code/SI.no:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Width="200px" ID="txtMaterialcode" MaxLength="30" ToolTip="Maximum Length: 30" Text='<%# Bind("Materialcode") %>'
                                                                                runat="server" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            MaterialName:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Width="200px" ID="txtMaterialName" MaxLength="150" ToolTip="Maximum Length: 150" Text='<%# Bind("MaterialName") %>'
                                                                                runat="server"  />
                                                                        </td>
                                                                        <td>
                                                                            Size:
                                                                        </td>
                                                                        <td colspan="2"><table> 
                                                                        <tr>
                                                                        <td>
                                                                         <telerik:RadTextBox Width="200px" ID="txtSize" MaxLength="100" ToolTip="Maximum Length: 100" Text='<%# Bind("Size") %>'
                                                                                runat="server"  />
                                                                        </td>
                                                                        <td>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red"
                                                                                EnableClientScript="true" runat="server" ErrorMessage="Enter Number Only!....."
                                                                                ControlToValidate="txtSize" ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        </tr>
                                                                        </table>
                                                                           
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Stock Date:
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadDatePicker ID="txtmanfdate" runat="server" Width="200px" DbSelectedDate='<%# Bind("MnfDate") %>'
                                                                                PopupDirection="BottomRight" DateInput-EmptyMessage="Select Stock Date">
                                                                                <Calendar ID="Calendar1" runat="server" ShowRowHeaders="true">
                                                                                </Calendar>
                                                                                <DateInput ID="DateInput1" runat="server" Enabled="true" DateFormat="dd/MMM/yyyy">
                                                                                </DateInput>
                                                                            </telerik:RadDatePicker>
                                                                        </td>
                                                                        <td>
                                                                            Quantity:
                                                                        </td>
                                                                        <td colspan="2">
                                                                            <telerik:RadTextBox Width="200px" ID="txtQuantity" MaxLength="100" ToolTip="Maximum Length: 100"
                                                                                runat="server" Text='<%# Bind("Quantity") %>' /><asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                                    ForeColor="Red" EnableClientScript="true" runat="server" ErrorMessage="Enter Number Only!....."
                                                                                    ControlToValidate="txtQuantity" ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <%--Expiry Date:--%>
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadDateTimePicker ID="txtexpdate" runat="server" Width="200px" DbSelectedDate='<%# Bind("Expiredate") %>'
                                                                                PopupDirection="BottomRight" DateInput-EmptyMessage="Select Expiry Date" Visible="false">
                                                                                <Calendar ID="Calendar2" runat="server" ShowRowHeaders="true">
                                                                                </Calendar>
                                                                                <DateInput ID="DateInput2" runat="server" Enabled="true" DateFormat="d/MMM/yyyy hh:mm tt">
                                                                                </DateInput>
                                                                            </telerik:RadDateTimePicker>
                                                                        </td>
                                                                    </tr>
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
                                                        </EditFormSettings>
                                                    </MasterTableView>
                                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                                </telerik:RadGrid>
                                            </div>
                                            <asp:SqlDataSource ID="Sqlcustomer" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                                                SelectCommand="select customerid,customername from customer where deleted=0 ORDER BY [customerid]">
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
