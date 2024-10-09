<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterUser.aspx.cs" Inherits="MasterUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Master</title>
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
                                    text-decoration: none; background-color: transparent; text-align: right; font-weight: bold;
                                    height: 17px;">
                                    <div>
                                        <asp:Label ID="lblname" runat="server" Font-Names="Tahoma"/></div>
                                </li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2" id="tdStatus" runat="server">
                            <br />
                            <div id="DivHeader" runat="server" style="background-color: transparent; font-size: x-large;
                                text-align: center; font-weight: bold; height: 17px; color: Black;" class="mycss">
                                USER MASTER DETAILS
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
                                <ajaxToolkit:TabPanel BackColor="#000" runat="server" ID="TabPanel1" HeaderText="User Information">
                                    <ContentTemplate>
                                        <div style="background: -webkit-linear-gradient(#dbf6e3,#f2e9b0); border: 4px solid Black;">
                                            <br />
                                            <asp:Label runat="server" CssClass="labelstyle_1" ID="lblID" Visible="false"></asp:Label>
                                            <table cellpadding="2" cellspacing="2" border="0" width="80%" style="font-weight: bold"  >
                                                <tr>
                                                    <td align="left" style="width:10%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblName1">Name:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width:40%">
                                                        <telerik:RadComboBox ID="cboName" runat="server" Height="110px" Width="220px" DataValueField="StaffID"
                                                            OnSelectedIndexChanged="cboName_SelectChange" AppendDataBoundItems="True" EmptyMessage="Select"
                                                            DataTextField="Name" AutoPostBack="true" DropDownWidth="300px" ForeColor="Black">
                                                            <HeaderTemplate>
                                                                <table style="width: 300px; font-size: small" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 150px;">
                                                                            StaffName&nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="width: 150px;">
                                                                            StaffID
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table style="width: 300px; font-size: small" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td style="width: 150px;" align="left">
                                                                            <%# DataBinder.Eval(Container, "Attributes['Name']")%>
                                                                        </td>
                                                                        <td style="width: 150px;" align="left">
                                                                            <%# DataBinder.Eval(Container, "Attributes['StaffID']")%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                        </telerik:RadComboBox>
                                                        <%--  <asp:RequiredFieldValidator runat="server" ID="NameValidator" ControlToValidate="txtName"
                                                            ErrorMessage="Name is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblDesignation">Designation:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadTextBox ID="txtDesignation" CssClass="textInput" runat="server" Width="220px"
                                                            Enabled="false" ForeColor="Black" MaxLength="100" ToolTip="Max Length:100" />
                                                        <%--<asp:RequiredFieldValidator runat="server" ID="DesignationValidator" ControlToValidate="txtDesignation"
                                                            ErrorMessage="Designation is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="width: 15%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblUserId" AssociatedControlID="txtUserID">User ID:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <telerik:RadTextBox ID="txtUserID" CssClass="textInput" runat="server" Width="220px"
                                                            MaxLength="50" ToolTip="Max Length:50">
                                                        </telerik:RadTextBox>
                                                        <%-- <asp:RequiredFieldValidator runat="server" ID="UserValidator" ControlToValidate="txtUserID"
                                                            ErrorMessage="UserID is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblPass" AssociatedControlID="txtPass">Password:</asp:Label>
                                                    </td>
                                                    <td align="left" style="width: 15%">
                                                        <telerik:RadTextBox ID="txtPass" CssClass="textInput" runat="server" Width="220px"
                                                            MaxLength="20">
                                                        </telerik:RadTextBox>
                                                        <%--<asp:RequiredFieldValidator runat="server" ID="passwordValidator" ControlToValidate="txtPass"
                                                            ErrorMessage="Password Needed" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:Label runat="server" CssClass="labelstyle_1" ID="lblType">UserType:</asp:Label>
                                                    </td>
                                                    <td align="left">
                                                        <telerik:RadComboBox ID="cboUserType" runat="server" Width="220px" EmptyMessage="Select">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Admin" Value="1" />
                                                                <telerik:RadComboBoxItem Text="Technician" Value="2" />
                                                                <telerik:RadComboBoxItem Text="Others" Value="3" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtDept"
                                                            ErrorMessage="Dept is required" ForeColor="Red" Text="*"></asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td>
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
                                <%--<asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Text=" Save " />--%>
                                <%-- <asp:Button runat="server" ID="btnClearItem" Text=" Clear " OnClick="btnclear_Click" />--%>
                                <%--OnClick="btnClearItem_Click"--%>
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
                                    <telerik:AjaxSetting AjaxControlID="cboName">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="txtDesignation" />
                                            <telerik:AjaxUpdatedControl ControlID="lblStatus" />
                                            <telerik:AjaxUpdatedControl ControlID="lblID" />
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
                            <div runat="server" style="width: 90%; background-color: transparent; overflow: auto;">
                                <telerik:RadGrid ID="RadGrid1" runat="server" AllowMultiRowEdit="false" OnNeedDataSource="RadGrid1_NeedDataSource"
                                    GridLines="Vertical" AllowPaging="True" PagerStyle-AlwaysVisible="true" PagerStyle-Position="Bottom"
                                    OnDeleteCommand="RadGrid1_DeleteCommand" PagerStyle-Mode="NextPrevNumericAndAdvanced"
                                    AllowAutomaticDeletes="true" AllowSorting="true" OnItemCommand="RadGrid1_ItemCommand"
                                    AllowFilteringByColumn="true" Skin="Black" PageSize="5">
                                    <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                                        <Selecting AllowRowSelect="true" />
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="false" AllowAutomaticUpdates="false" DataKeyNames="Id"
                                        CommandItemDisplay="Top" CommandItemSettings-AddNewRecordText="Add New User Details">
                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                        <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" DataType="System.Int64" HeaderText="ID" ReadOnly="True"
                                                SortExpression="Id" UniqueName="Id" AllowFiltering="false" AllowSorting="false"
                                                Visible="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UserName" DataType="System.String" HeaderText="Name"
                                                SortExpression="UserName" UniqueName="UserName">
                                                <HeaderStyle Width="25%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridHyperLinkColumn DataTextField="LoginId" DataType="System.String" HeaderText="User Id"
                                                SortExpression="LoginId" UniqueName="LoginId">
                                                <HeaderStyle Width="15" />
                                            </telerik:GridHyperLinkColumn>
                                            <telerik:GridBoundColumn DataField="Password" DataType="System.String" HeaderText="Password"
                                                SortExpression="Password" UniqueName="Password">
                                                <HeaderStyle Width="15%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Designation" DataType="System.String" HeaderText="Designation"
                                                SortExpression="Designation" UniqueName="Designation">
                                                <HeaderStyle Width="20%" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="UserType" DataType="System.String" HeaderText="UserType"
                                                SortExpression="UserType" UniqueName="UserType">
                                                <HeaderStyle Width="15%" />
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
