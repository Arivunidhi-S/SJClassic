using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

using System.Globalization;
using System.Collections;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class MasterStaff : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();
    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblStatus.Text = "";
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
        lblname.Text = Session["Name"].ToString() + ", " + Session["Designation"].ToString();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {

            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);
                BusinessTier.DisposeConnection(connMenu);
            }
            else
            {
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void RadGrid1_ItemCreated(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
             if (e.Item is GridEditFormInsertItem && e.Item.OwnerTableView.IsItemInserted)//if the item is in insert mode
            {

                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                TextBox txtStaffID = (TextBox)editedItem.FindControl("txtStaffID");

                string sql1 = "select max(StaffAutoID)as maxval from MasterStaff";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();
                int val = 0;
                rd1.Read();
                if (string.IsNullOrEmpty(rd1["maxval"].ToString()))
                {
                    val = 1;
                }
                else
                {
                    val = Convert.ToInt32(rd1["maxval"].ToString()) + 1;
                }

                txtStaffID.Text = "SJ_ST/" + DateTime.Today.ToString("yyMM/" + val);
                BusinessTier.DisposeReader(rd1);
            }
                  else if (e.Item is GridEditFormItem && e.Item.IsInEditMode)//if the item is in edit mode
                     {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
               

                //TextBox txtStaffName = editedItem.FindControl("txtStaffName") as TextBox;
                //TextBoxSetting textBoxSetting1 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                //textBoxSetting1.TargetControls.Add(new TargetInput(txtStaffName.UniqueID, true));

                //RadTextBox txtMobileNo = editedItem.FindControl("txtMobileNo") as RadTextBox;
                //TextBoxSetting textBoxSetting2 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                //textBoxSetting1.TargetControls.Add(new TargetInput(txtMobileNo.UniqueID, true));

                TextBox txtStaffID = (TextBox)editedItem.FindControl("txtStaffID");
                string lblID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["StaffAutoID"].ToString().Trim();
                string sql1 = "select * from MasterStaff where deleted=0 and StaffAutoID='" + lblID.ToString().Trim() + "'";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();

                if (rd1.Read())
                {
                    txtStaffID.Text = rd1["StaffID"].ToString();
                }
               
            }
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Please provide required details which are highlighted";
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
        }

    }

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {

        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();

        string sql = "select * FROM MasterStaff WHERE Deleted=0 order by StaffAutoID ";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        try
        {
            int flg = 0;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["StaffAutoID"].ToString().Trim();
            if (ID == "3")
            {
                lblStatus.Text = "Cannot be deleted Administrator";
                return;
            }
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();

            flg = BusinessTier.SaveStaff(conn, "", "", "", "", "", "", Session["sesUserID"].ToString(), "D", ID.ToString().Trim());

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Staff Info Deleted Successfully";
                lblStatus.ForeColor = Color.Red;
            }
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Unable to delete, Please try again/Contact your administrator";
         }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;

            TextBox txtStaffID = (TextBox)editedItem.FindControl("txtStaffID");
            TextBox txtStaffName = (TextBox)editedItem.FindControl("txtStaffName");
            TextBox txtMobileNo = (TextBox)editedItem.FindControl("txtMobileNo");
            RadComboBox txtDesignation = (RadComboBox)editedItem.FindControl("txtDesignation");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtAddress = (TextBox)editedItem.FindControl("txtAddress");

            if ((string.IsNullOrEmpty(txtStaffName.Text.ToString())))
            {
                lblStatus.Text = "Enter StaffName";
                return;
            }

            if ((string.IsNullOrEmpty(txtMobileNo.Text.ToString())))
            {
                lblStatus.Text = "Enter MobileNo";
                return;
            }
            
            int flg = BusinessTier.SaveStaff(conn, txtStaffID.Text.ToString().Trim(), txtStaffName.Text.ToString().Trim(),txtMobileNo.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtAddress.Text.ToString().Trim(), Session["sesUserID"].ToString(), "N","0");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Staff Info Inserted Successfully";
                lblStatus.ForeColor = Color.Green;
            }

        }
        catch (Exception ex)
        {
            //lblStatus.Text = "Unable to insert, Please try again/Contact your administrator";
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
        }
        RadGrid1.Rebind();
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            Label lblID = (Label)editedItem.FindControl("lblID");

            TextBox txtStaffID = (TextBox)editedItem.FindControl("txtStaffID");
            TextBox txtStaffName = (TextBox)editedItem.FindControl("txtStaffName");
            TextBox txtMobileNo = (TextBox)editedItem.FindControl("txtMobileNo");
            RadComboBox txtDesignation = (RadComboBox)editedItem.FindControl("txtDesignation");
            TextBox txtEmail = (TextBox)editedItem.FindControl("txtEmail");
            TextBox txtAddress = (TextBox)editedItem.FindControl("txtAddress");

            if ((string.IsNullOrEmpty(txtStaffName.Text.ToString())))
            {
                lblStatus.Text = "Enter StaffName";
                return;
            }

            if ((string.IsNullOrEmpty(txtMobileNo.Text.ToString())))
            {
                lblStatus.Text = "Enter MobileNo";
                return;
            }
           
           int flg = BusinessTier.SaveStaff(conn, txtStaffID.Text.ToString().Trim(), txtStaffName.Text.ToString().Trim(),txtMobileNo.Text.ToString().Trim(), txtDesignation.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtAddress.Text.ToString().Trim(), Session["sesUserID"].ToString(), "U",lblID.Text.ToString().Trim());
            BusinessTier.DisposeConnection(conn);

            if (flg >= 1)
            {
                lblStatus.Text = "Staff Info Updated Successfully";
                lblStatus.ForeColor = Color.Green;

            }
            RadGrid1.Rebind();

        }
        catch (Exception ex)
        {
            lblStatus.Text = "Unable to Updated, Please try again/Contact your administrator";
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
        }

    }

    //protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    //{
    //    SqlConnection conn = BusinessTier.getConnection();
    //    conn.Close();

    //    try
    //    {

    //        GridEditFormItem editedItem = e.Item as GridEditFormItem;
    //        TextBox txtStaffID = (TextBox)editedItem.FindControl("txtStaffID");
    //        string lblID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["StaffAutoID"].ToString().Trim();
    //        string sql1 = "select * from MasterStaff where deleted=0 and StaffAutoID='" + lblID.ToString().Trim() + "'";
    //        SqlCommand cmd1 = new SqlCommand(sql1, conn);
    //        SqlDataReader rd1 = cmd1.ExecuteReader();

    //        if (rd1.Read())
    //        {
    //            txtStaffID.Text = rd1["StaffID"].ToString();
    //        }
    //        BusinessTier.DisposeConnection(conn);
    //    }
    //    catch (Exception ex)
    //    {
    //        BusinessTier.DisposeConnection(conn);
    //    }
    //}

    private void ShowMessage(int errorNo)
    {
        lblStatus.Text = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Message"].ToString();
        System.Drawing.ColorConverter colConvert = new ColorConverter();
        string strColor = BusinessTier.g_ErrorMessagesDataTable.Rows[errorNo - 1]["Color"].ToString();
        lblStatus.ForeColor = (System.Drawing.Color)colConvert.ConvertFromString(strColor);
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}