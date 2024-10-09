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

public partial class Material : System.Web.UI.Page
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
        try
        {
            if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = e.Item as GridEditFormItem;

                TextBox txtMaterialcode = editedItem.FindControl("txtMaterialcode") as TextBox;
                TextBoxSetting textBoxSetting1 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                textBoxSetting1.TargetControls.Add(new TargetInput(txtMaterialcode.UniqueID, true));

                TextBox txtMaterialName = editedItem.FindControl("txtMaterialName") as TextBox;
                TextBoxSetting textBoxSetting2 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                textBoxSetting1.TargetControls.Add(new TargetInput(txtMaterialName.UniqueID, true));
                }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Please provide required details which are highlighted";
            e.Canceled = true;
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

        string sql = "select * FROM MaterialMaster WHERE Deleted=0 order by materialname ";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
           
            int flg = 0;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["MaterialAutoid"].ToString().Trim();

            string sql2 = "select * from IncomingMaterial where deleted=0 and MaterialAutoid='" + ID.ToString().Trim() + "' ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();

            if (rd2.Read())
            {
                lblStatus.Text = "Sorry you cannot be deleted because this material already keyin Incoming Material";
                lblStatus.ForeColor = Color.Red;
                rd2.Close();
                return;
            }
            rd2.Close();

            flg = BusinessTier.SaveMaterial(conn,"", "", "", "", "", "", "", Session["sesUserID"].ToString(), "D", ID.ToString().Trim());

            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Material Info Deleted Successfully";
                lblStatus.ForeColor = Color.Red;
            }
            RadGrid1.Rebind();
          }
        catch (Exception ex)
        {
            //lblStatus.Text = "Unable to delete, Please try again/Contact your administrator";
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Material", "Delete", ex.ToString(), "Audit");
            e.Canceled = true;

         }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            
            TextBox txtMaterialcode = (TextBox)editedItem.FindControl("txtMaterialcode");
            TextBox txtMaterialName = (TextBox)editedItem.FindControl("txtMaterialName");
            RadTextBox txtSize = (RadTextBox)editedItem.FindControl("txtSize");
            RadComboBox cboMaterialType = (RadComboBox)editedItem.FindControl("cboMaterialType");
            RadDatePicker txtmanfdate = editedItem.FindControl("txtmanfdate") as RadDatePicker;
            RadDateTimePicker txtexpdate = editedItem.FindControl("txtexpdate") as RadDateTimePicker;

            RadTextBox txtQuantity = (RadTextBox)editedItem.FindControl("txtQuantity");

            String stockdt = txtmanfdate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(stockdt);
            stockdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

            int flg = BusinessTier.SaveMaterial(conn, txtMaterialcode.Text.ToString().Trim(), txtMaterialName.Text.ToString().Trim(), txtSize.Text.ToString().Trim(), cboMaterialType.Text.ToString().Trim(), stockdt.ToString().Trim(), txtexpdate.SelectedDate.ToString().Trim(), txtQuantity.Text.ToString().Trim(), Session["sesUserID"].ToString(), "N", "0");
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {            
                lblStatus.Text = "Material Info Inserted Successfully";
                lblStatus.ForeColor = Color.Green;
            }
        
        }
        catch (Exception ex)
        {
           // lblStatus.Text = "Unable to insert, Please try again/Contact your administrator";
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Material", "Insert", ex.ToString(), "Audit");
            e.Canceled = true;
             }

        //m_datatable.Rows.Add(newRow);
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

            TextBox txtMaterialcode = (TextBox)editedItem.FindControl("txtMaterialcode");
            TextBox txtMaterialName = (TextBox)editedItem.FindControl("txtMaterialName");
            RadTextBox txtSize = (RadTextBox)editedItem.FindControl("txtSize");
            RadComboBox cboMaterialType = (RadComboBox)editedItem.FindControl("cboMaterialType");
            RadDatePicker txtmanfdate = editedItem.FindControl("txtmanfdate") as RadDatePicker;
            RadDateTimePicker txtexpdate = editedItem.FindControl("txtexpdate") as RadDateTimePicker;

            RadTextBox txtQuantity = (RadTextBox)editedItem.FindControl("txtQuantity");

            String stockdt = txtmanfdate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(stockdt);
            stockdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

            string sql2 = "select * from IncomingMaterial where deleted=0 and MaterialAutoid='" + lblID.Text.ToString().Trim() + "' ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();

            if (rd2.Read())
            {
                lblStatus.Text = "Sorry you cannot be modified because this material already keyin Incoming Material";
               lblStatus.ForeColor = Color.Red;
                return;
            }
            rd2.Close();

            int flg = BusinessTier.SaveMaterial(conn, txtMaterialcode.Text.ToString().Trim(), txtMaterialName.Text.ToString().Trim(), txtSize.Text.ToString().Trim(), cboMaterialType.Text.ToString().Trim(), stockdt.ToString().Trim(), txtexpdate.SelectedDate.ToString().Trim(), txtQuantity.Text.ToString().Trim(), Session["sesUserID"].ToString(), "U", lblID.Text.ToString().Trim());
            BusinessTier.DisposeConnection(conn);
            RadGrid1.Rebind();
            if (flg >= 1)
            {
                lblStatus.Text = "Material Info Updated Successfully";
                lblStatus.ForeColor = Color.Green;
            }
          }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Material", "Update", ex.ToString(), "Audit");
            e.Canceled = true;
        }

    }

    protected void RadGrid1_ItemDataBound(object source, Telerik.Web.UI.GridItemEventArgs e)
    {
        try
        {
            
                GridEditFormItem editedItem = e.Item as GridEditFormItem;
                Label lblID = (Label)editedItem.FindControl("lblID");
                TextBox txtMaterialcode = (TextBox)editedItem.FindControl("txtMaterialcode");
                TextBox txtMaterialName = (TextBox)editedItem.FindControl("txtMaterialName");
                RadComboBox cboMaterialType = (RadComboBox)editedItem.FindControl("cboMaterialType");
                RadDatePicker txtmanfdate = editedItem.FindControl("txtmanfdate") as RadDatePicker;
                RadTextBox txtSize = (RadTextBox)editedItem.FindControl("txtSize");

                if (!(string.IsNullOrEmpty(lblID.Text.ToString())))
                {
                    cboMaterialType.Enabled = false;
                    txtMaterialcode.Enabled = false;
                    txtMaterialName.Enabled = false;
                    txtSize.Enabled = false;
                    //txtmanfdate.Enabled = false;

                }
             
        }
        catch (Exception ex)
        {
           
        }
    }
     
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