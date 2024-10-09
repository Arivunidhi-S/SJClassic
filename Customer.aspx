<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Customer.aspx.cs" Inherits="Customer" %>

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
                                    <%= dtMenuItems.Rows[a][0].ToString()  %> <img src="Images/menuarrow.png" / height="14" width="18"></a>
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
                         
                           <br />
                        </td>
                    </tr>
                      <tr style="background-color: transparent; height: 550px; width:100%" valign="top">
            <td align="center">
                <table border="0"   align="center" style="background-color: transparent;">
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
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                            <!-- content start -->
                            <div id="Div10" runat="server" style="  background-color: transparent;
                                overflow: auto;" align="center">
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                        Validation-IsRequired="true" ErrorMessage="Mandatory Fields">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtName" />
                                            <telerik:TargetInput ControlID="txtAddress1" />
                                            
                                        </TargetControls>
                                    </telerik:TextBoxSetting>
                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" Validation-IsRequired="true"
                                        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" ErrorMessage="Invalid Email">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtEmail" />
                                        </TargetControls>
                                    </telerik:RegExpTextBoxSetting>
                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior3" ValidationExpression="[0-9]{6,}"
                                        ErrorMessage="Enter more than 6 figures" IsRequiredFields="true" Validation-IsRequired="true">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtPhone" />
                                        </TargetControls>
                                    </telerik:RegExpTextBoxSetting>
                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior4" ValidationExpression="[0-9]{6,}"
                                        ErrorMessage="Enter more than 6 figures">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtFax" />
                                        </TargetControls>
                                    </telerik:RegExpTextBoxSetting>
                                </telerik:RadInputManager>
                                <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                    text-align: center; font-weight: bold; height: 17px; color: Black;" class="mycss">
                                    DEALER MASTER DETAILS
                                </div>
                                <br />
                                 <div>
                                 <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                               
                                    
                            </div>
                                <br />
                                <telerik:RadGrid ID="RadGrid1" runat="server" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    OnItemCreated="RadGrid1_ItemCreated" AllowPaging="True" PagerStyle-AlwaysVisible="true"
                                    PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand" OnInsertCommand="RadGrid1_InsertCommand"
                                    OnUpdateCommand="RadGrid1_UpdateCommand" AllowAutomaticUpdates="true" AllowAutomaticInserts="true"
                                    PagerStyle-Mode="NextPrevNumericAndAdvanced" AllowAutomaticDeletes="true" Width="100%"
                                    AllowSorting="true" AllowFilteringByColumn="true" Skin="Black">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="Customerid" CommandItemDisplay="Top"
                                        CommandItemSettings-AddNewRecordText="Add New Dealer Details">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton">
                                                <HeaderStyle Width="5%" />
                                            </telerik:GridEditCommandColumn>
                                            <telerik:GridBoundColumn DataField="Customerid" DataType="System.Int64" HeaderText="ID"
                                                ReadOnly="True" SortExpression="Customerid" UniqueName="Customerid" AllowFiltering="false"
                                                AllowSorting="false" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Customername" DataType="System.String" HeaderText="Dealer Name"
                                                SortExpression="Customername" UniqueName="Customername">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Address1" DataType="System.String" HeaderText="Address"
                                                SortExpression="Address1" UniqueName="Address1">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="City" DataType="System.String" HeaderText="City"
                                                SortExpression="City" UniqueName="City">
                                                <HeaderStyle Width="6%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="State" DataType="System.String" HeaderText="State"
                                                SortExpression="State" UniqueName="State">
                                                <HeaderStyle Width="6%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Contactno" DataType="System.String" HeaderText="Phone"
                                                SortExpression="Contactno" UniqueName="Contactno">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Email" DataType="System.String" HeaderText="Email"
                                                SortExpression="Email" UniqueName="Email">
                                                <HeaderStyle Width="10%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Website" DataType="System.String" HeaderText="Website"
                                                SortExpression="Website" UniqueName="Website">
                                                <HeaderStyle Width="10%" />
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
                                                        <td >
                                                            <b>Dealer ID:</b>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Bind("Customerid")%>' Visible="false" />
                                                                 </td>
                                                        <td colspan="3">
                                                               <b> <asp:Label ID="lblCustomerID" runat="server" /><b>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Dealer Name:
                                                            <asp:Label ID="Label11" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                width: 1px;" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="197px" ID="txtName" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" Text='<%# Bind("Customername") %>' />
                                                        </td>
                                                             <td>
                                                            Address 1:
                                                            <asp:Label ID="Label1" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                width: 1px;" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="197px" ID="txtAddress1" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" Text='<%# Bind("Address1") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                  
                                                        <td>
                                                            Address 2:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtAddress2" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" Text='<%# Bind("Address2") %>' />
                                                        </td>
                                                         <td>
                                                            State:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtState" MaxLength="30" ToolTip="Maximum Length: 30"
                                                                runat="server" Text='<%# Bind("State") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                       
                                                         <td>
                                                            City:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtCity" MaxLength="30" ToolTip="Maximum Length: 30"
                                                                runat="server" Text='<%# Bind("City") %>' />
                                                        </td>
                                                        <td>
                                                            Country:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtCountry" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Country") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Mobile No: <asp:Label ID="Label3" Text="*" runat="server" Style="color: Red; font-size: smaller;
                                                                width: 1px;" />
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtPhone" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("Contactno") %>' />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red" EnableClientScript="true"
                                                                                    runat="server" ErrorMessage="Enter Number Only!....." ControlToValidate="txtPhone" ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Postcode:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtPostcode" MaxLength="10" ToolTip="Maximum Length: 10"
                                                                runat="server" Text='<%# Bind("Postcode") %>' />
                                                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" EnableClientScript="true"
                                                                                    runat="server" ErrorMessage="Enter Number Only!....." ControlToValidate="txtPostcode" ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtFax" MaxLength="20" ToolTip="Maximum Length: 20"
                                                                runat="server" Text='<%# Bind("Faxno") %>' />  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ForeColor="Red" EnableClientScript="true"
                                                                                    runat="server" ErrorMessage="Enter Number Only!....." ControlToValidate="txtFax" ValidationExpression="^\d+"></asp:RegularExpressionValidator>
                                                        </td>
                                                        <td>
                                                            Email:
                                                           
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtEmail" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Email") %>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Website:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtWebsite" MaxLength="50" ToolTip="Maximum Length: 50"
                                                                runat="server" Text='<%# Bind("Website") %>' />
                                                        </td>
                                                        <td>
                                                            Description:
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="200px" ID="txtDesc" MaxLength="200" ToolTip="Maximum Length: 200"
                                                             TextMode="MultiLine" runat="server" Text='<%# Bind("Description") %>' />
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
                                                        <td align ="center">  <asp:Label ID="lblStatus1" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </FormTemplate>
                                        </EditFormSettings>
                                    </MasterTableView>
                                    <PagerStyle Mode="NumericPages"></PagerStyle>
                                </telerik:RadGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
                </table>
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
