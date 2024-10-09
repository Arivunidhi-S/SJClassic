<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterStaff.aspx.cs" Inherits="MasterStaff" %>

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
                                        <asp:Label ID="lblname" runat="server" Font-Names="Tahoma"/></div>
                                </li>
                                </ul>
                            </td>
                        </tr>
                        
                        
                    </table>

                </td>
            </tr>
        </table>
          <table width="1340px">
                                    <tr>
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
                                                        </UpdatedControls>
                                                    </telerik:AjaxSetting>
                                                </AjaxSettings>
                                            </telerik:RadAjaxManager>
                                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                                            <!-- content start -->
                                            <div id="Div10" runat="server" style="background-color: transparent; width:90%"
                                                align="center">
                                                
                                                 <br /> 
                                                <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                                    text-align: center; font-weight: bold; height: 17px; color: Black;" class="mycss">
                                                    STAFF MASTER DETAILS
                                                </div>
                                                <br />
                                                <div style="height: 20px;">
                                                    <asp:Label ID="lblStatus" runat="server" ForeColor="Blue" Font-Bold="true" />
                                                </div>
                                                 <br /> 
                                                <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                                                    OnItemCreated="RadGrid1_ItemCreated" AllowPaging="True" PagerStyle-AlwaysVisible="true"
                                                    PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
                                                    OnUpdateCommand="RadGrid1_UpdateCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                                    PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowAutomaticDeletes="true" Width="100%"
                                                    AllowSorting="true" AllowFilteringByColumn="true" Skin="Black">
                                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="StaffAutoID" CommandItemDisplay="Top"
                                                        CommandItemSettings-AddNewRecordText="Add New Staff Details">
                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                                        <Columns>
                                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                                <HeaderStyle Width="5%" />
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridBoundColumn DataField="StaffAutoID " DataType="System.Int64" HeaderText="StaffAutoID"
                                                                ReadOnly="True" SortExpression="StaffAutoID" UniqueName="StaffID" AllowFiltering="false"
                                                                AllowSorting="false" Visible="false">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="StaffID" DataType="System.String" HeaderText="StaffID"
                                                                SortExpression="Materialcode" UniqueName="StaffID">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Name" DataType="System.String" HeaderText="StaffName"
                                                                SortExpression="MaterialName" UniqueName="StaffName">
                                                                <HeaderStyle Width="25%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="MobileNo" DataType="System.Int32" HeaderText="MobileNo"
                                                                SortExpression="MobileNo" UniqueName="MobileNo">
                                                                <HeaderStyle Width="15%" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="Designation" DataType="System.String" HeaderText="Designation"
                                                                SortExpression="Designation" UniqueName="Designation">
                                                                <HeaderStyle Width="20%" />
                                                            </telerik:GridBoundColumn>
                                                             <telerik:GridBoundColumn DataField="Email" DataType="System.String" HeaderText="Email"
                                                                SortExpression="Email" UniqueName="Email">
                                                                <HeaderStyle Width="25%" />
                                                            </telerik:GridBoundColumn>
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
                                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("StaffAutoID")%>' />
                                                                            </b>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Staff ID:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Width="200px" ID="txtStaffID" MaxLength="30" Enabled="false" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            Staff Name:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Width="200px" ID="txtStaffName" MaxLength="150" runat="server" Text='<%# Bind("Name") %>' />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            Mobile No:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Width="200px" ID="txtMobileNo" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                                runat="server" Text='<%# Bind("MobileNo") %>' /><asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                                                    ForeColor="Red" EnableClientScript="true" runat="server" ErrorMessage="Enter Number Only!....."
                                                                                    ControlToValidate="txtMobileNo" ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                                        </td>
                                                                        <td>
                                                                            Designation:
                                                                        </td>
                                                                        <td>
                                                                            <telerik:RadComboBox ID="txtDesignation" runat="server" Height="80px" Width="200px"
                                                                                EmptyMessage="Select" Text='<%# Bind("Designation") %>'>
                                                                                <Items>
                                                                                    <telerik:RadComboBoxItem Text="Manager" Value="0" />
                                                                                    <telerik:RadComboBoxItem Text="Clerk" Value="1" />
                                                                                    <telerik:RadComboBoxItem Text="Engineer" Value="2" />
                                                                                    <telerik:RadComboBoxItem Text="Technician" Value="3" />
                                                                                </Items>
                                                                            </telerik:RadComboBox>
                                                                            <%-- <asp:TextBox Width="200px" ID="txtDesignation" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                                runat="server" Text='<%# Bind("Designation") %>' />--%>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            Email:
                                                                        </td>
                                                                        <td valign="top">
                                                                            <asp:TextBox Width="200px" ID="txtEmail" MaxLength="150" runat="server" Text='<%# Bind("Email") %>' />
                                                                        </td>
                                                                        <td valign="top">
                                                                            Address:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox Width="200px" ID="txtAddress" Height="50" TextMode="MultiLine" runat="server"
                                                                                Text='<%# Bind("Address") %>' />
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
                                                SelectCommand="select * from MasterStaff where deleted=0 ORDER BY [StaffAutoID]">
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
    </div>
    </form>
</body>
</html>
