<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterModule.aspx.cs" Inherits="MasterModule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server" />
</head>
<link rel="stylesheet" href="css/menu_core.css" type="text/css" />
<!--Style Skin (menu widget)-->
<link rel="stylesheet" href="css/skins/menu_simpleAnimated.css" type="text/css" />
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
</style>
<script language="javascript">
    history.go(1); /* undo user navigation (ex: IE Back Button) */
</script>
<body style="background-image: url('Images/aa.jpg'); margin: 10px 0px 0px 0px;">
    <form id="form1" runat="server">
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td style="border-right: blue thin solid; border-top: blue thin solid; border-left: blue thin solid;
                border-bottom: blue thin solid; border-width: 0px" align="center">
                <hr style="visibility: hidden;" />
                <hr style="visibility: hidden;" />
                <table border="0" width="100%">
                    <tr>
                        <td align="left" runat="server" colspan="2">
                            <ul id="myMenu" class="nfMain nfPure">
                                <% for (int a = 0; a < dtMenuItems.Rows.Count; a++)
                                   { %>
                                <li class="nfItem"><a class="nfLink" href="#">
                                    <%= dtMenuItems.Rows[a][0].ToString()  %></a>
                                    <div class="nfSubC nfSubS">
                                        <% dtSubMenuItems = BusinessTier.getSubMenuItems(dtMenuItems.Rows[a][0].ToString(), Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                                           int aa;
                                           for (aa = 0; aa < dtSubMenuItems.Rows.Count; aa++)
                                           { %>
                                        <div class="nfItem">
                                            <a class="nfLink" id='<%= dtSubMenuItems.Rows[aa][0].ToString() %>' href='<%= dtSubMenuItems.Rows[aa][1].ToString() %>'>
                                                <%= dtSubMenuItems.Rows[aa][2].ToString()%></a>
                                        </div>
                                        <% } %>
                                    </div>
                                </li>
                                <% } %>
                                <li class="nfItem"><a class="nfLink" href="Login.aspx" style="border-right-width: 1px;">
                                    LOGOUT</a></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div style="height: 20px;">
                                <asp:Label class="labelstyle" ID="lblStatus" runat="server" ForeColor="Red" Font-Bold="true" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 80%;">
                            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
                                </Scripts>
                            </telerik:RadScriptManager>
                            <script type="text/javascript">
                            </script>
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                                            <telerik:AjaxUpdatedControl ControlID="RadInputManager1" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="chkApproval" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" />
                            <!-- content start -->
                            <div id="Div1" runat="server" style="width: 100%; height: 400px; background-color: White;
                                overflow: auto;">
                                <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                    <telerik:TextBoxSetting BehaviorID="TextBoxBehavior1" InitializeOnClient="false"
                                        Validation-IsRequired="true">
                                    </telerik:TextBoxSetting>
                                    <telerik:RegExpTextBoxSetting BehaviorID="RagExpBehavior1" ValidationExpression="[Y,y,N,n]{1}"
                                        IsRequiredFields="true" Validation-IsRequired="true">
                                        <TargetControls>
                                            <telerik:TargetInput ControlID="txtApprovalflag" />
                                        </TargetControls>
                                    </telerik:RegExpTextBoxSetting>
                                </telerik:RadInputManager>
                                <div id="DivHeader" runat="server" style="background-color: transparent; font-size: medium;
                                    text-align: center; font-weight: bold; height: 17px;  
                                    color: Navy;">
                                    MODULE MASTER DETAILS
                                </div>
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" OnItemCreated="RadGrid1_ItemCreated"
                                    PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom" OnDeleteCommand="RadGrid1_DeleteCommand"
                                    AllowAutomaticUpdates="true" AllowAutomaticInserts="true" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    AllowAutomaticDeletes="true" OnInsertCommand="RadGrid1_InsertCommand" AllowSorting="true"
                                    AllowFilteringByColumn="true" OnUpdateCommand="RadGrid1_UpdateCommand">
                                    <MasterTableView AutoGenerateColumns="false" DataKeyNames="ModuleId" CommandItemDisplay="Top">
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                        <Columns>
                                            <telerik:GridEditCommandColumn ButtonType="ImageButton" Visible="false" />
                                            <telerik:GridBoundColumn DataField="ModuleId" DataType="System.Int64" HeaderText="ID"
                                                ReadOnly="True" SortExpression="ModuleId" UniqueName="ModuleId" AllowSorting="false"
                                                AllowFiltering="false" Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ModuleName" DataType="System.String" HeaderText="Name"
                                                SortExpression="ModuleName" UniqueName="ModuleName">
                                                <HeaderStyle Width="25%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Description" DataType="System.String" HeaderText="Description"
                                                SortExpression="Description" UniqueName="Description">
                                                <HeaderStyle Width="75%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridButtonColumn CommandName="Delete" UniqueName="DeleteColumn" ButtonType="ImageButton"
                                                ConfirmText="Are you sure want to delete?" Visible="false" />
                                        </Columns>
                                        <EditFormSettings EditFormType="Template">
                                            <EditColumn UniqueName="EditCommandColumn1">
                                            </EditColumn>
                                            <FormTemplate>
                                                <table cellspacing="2" cellpadding="1" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="2">
                                                            <b>ID:
                                                                <%# Eval("ModuleId")%>
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Name:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtName" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" Text='<%# Bind("ModuleName") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Description:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="200px" ID="txtDesc" TextMode="MultiLine" MaxLength="150" ToolTip="Maximum Length: 150"
                                                                runat="server" Text='<%# Bind("Description") %>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Is Approval Needed:
                                                        </td>
                                                        <td>
                                                            <asp:TextBox Width="15px" ID="txtApprovalflag" MaxLength="1" ToolTip="Maximum Length: 1"
                                                                runat="server" Text='<%# Bind("ApprovalNeeded") %>'></asp:TextBox>
                                                            <asp:Label Text="(Enter Y/N )" ForeColor="Red" runat="server" />
                                                            <%-- <% if ((Container.DataItem, "Approvalflag") == "True") { %>
                                                            <asp:CheckBox runat="server" ID="chkApproval" Width="50px" Text='<%# Bind("Approvalflag") %>' Visible="true" />
                                                            <% } %>--%>
                                                            <%--<asp:CheckBox runat="server" ID="chkApproval" Width="50px" Visible="true" />--%>
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
                        </td>
                        <td align="center" valign="top" style="width: 20%; height: 400px">
                            <div id="Div2" runat="server" style="color: Navy; background-color: transparent; font-size: large;
                                font-weight: bold; height: 400px;  ">
                                HELP
                                <div id="Div10" runat="server" style="color: Navy; width: 270px; font-weight: lighter;
                                    font-size: small; text-align: left">
                                    <ul style="text-align: left;">
                                        <li>Click "Refresh" button to refresh the Database.</li><hr style="border-color: transparent;
                                            height: 1px;" />
                                        <%--                                        <li>Click "Edit" button to change existing details.</li><hr style="border-color: transparent" />
                                        <li>Click "Delete" button to delete existing details.</li><hr style="border-color: transparent" />
                                        --%>
                                        <li>Click on "Filter" provided under each columns, to filter the information.</li><hr
                                            style="border-color: transparent" />
                                        <li>Click on "Next/Previous" or "First/Last" button to move to other pages.</li><hr
                                            style="border-color: transparent" />
                                        <li>Enter Page No and Click on "Go" button to goto particular page directly.</li><hr
                                            style="border-color: transparent" />
                                        <li>Enter the Number and Click on "Change" to change the number of items shown.</li>
                                    </ul>
                                </div>
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
