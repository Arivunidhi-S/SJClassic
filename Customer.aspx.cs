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

public partial class Customer : System.Web.UI.Page
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
            //SqlConnection connSave = BusinessTier.getConnection();
            //connSave.Open();
            //int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
            //connSave.Close();
            Response.Redirect("Login.aspx");
        }

    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection connSave = BusinessTier.getConnection();
            connSave.Open();
            //int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
            connSave.Close();
            Response.Redirect("Login.aspx");
        }
        catch (Exception ex)
        {
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
                Label lblStatus1 = (Label)editedItem.FindControl("lblStatus1");
                Label lblCustomerID = (Label)editedItem.FindControl("lblCustomerID");
             
                string sql1 = "select max(Customerid)as maxval from Customer";
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

                lblCustomerID.Text ="SJ_DLR/"+ DateTime.Today.ToString("yyMM/" + val);
                BusinessTier.DisposeReader(rd1);
               }
            else if (e.Item is GridEditableItem && e.Item.IsInEditMode)
            {
                GridEditFormItem editedItem = (GridEditFormItem)e.Item;
                Label lblStatus1 = (Label)editedItem.FindControl("lblStatus1");
                //RadTextBox txtPhone = (RadTextBox)editedItem.FindControl("txtPhone");
               
                TextBox txtName = editedItem.FindControl("txtName") as TextBox;
                TextBoxSetting textBoxSetting = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                textBoxSetting.TargetControls.Add(new TargetInput(txtName.UniqueID, true));

                TextBox txtAddress1 = editedItem.FindControl("txtAddress1") as TextBox;
                TextBoxSetting textBoxSetting1 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                textBoxSetting1.TargetControls.Add(new TargetInput(txtAddress1.UniqueID, true));

                //RadTextBox txtPhone = editedItem.FindControl("txtPhone") as RadTextBox;
                //TextBoxSetting textBoxSetting2 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("RagExpBehavior3");
                //textBoxSetting2.TargetControls.Add(new TargetInput(txtPhone.UniqueID, true));

                TextBox txtPhone = editedItem.FindControl("txtPhone") as TextBox;
                TextBoxSetting textBoxSetting3 = (TextBoxSetting)RadInputManager1.GetSettingByBehaviorID("TextBoxBehavior1");
                textBoxSetting3.TargetControls.Add(new TargetInput(txtPhone.UniqueID, true));

                Label lblCustomerID = (Label)editedItem.FindControl("lblCustomerID");
                string lblID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Customerid"].ToString().Trim();
                RadTextBox txtno = (RadTextBox)editedItem.FindControl("txtno");
                string sql1 = "select * from Customer where deleted=0 and Customerid='" + lblID.ToString().Trim() + "'";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();

                if (rd1.Read())
                {
                    lblCustomerID.Text = rd1["CustomerAutoid"].ToString();
                }
                      }
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
          // lblStatus.Text = "Please provide required details which are highlighted";
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
        string sql = "select * FROM Customer WHERE Deleted=0 order by Customername";
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
            GridEditFormItem editedItem = e.Item as GridEditFormItem;
           // Label lblStatus1 = (Label)editedItem.FindControl("lblStatus");
            //string strTransID = "0";
            int flg = 0;
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Customerid"].ToString().Trim();
            
            flg = BusinessTier.SaveCustomer(conn, "", "", "", "", "", "", "", "", "", "", "", "", "", Session["sesUserID"].ToString(), "D", ID.ToString().Trim());
            BusinessTier.DisposeConnection(conn);
            if (flg >= 1)
            {
                lblStatus.Text = "Dealer Info Deleted Successfully";
            }

          }
        catch (Exception ex)
        {
            lblStatus.Text = "Unable to delete, Please try again/Contact your administrator";
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
           }
    }

    protected void RadGrid1_InsertCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormItem editedItem = e.Item as GridEditFormItem;
        Label lblStatus1 = (Label)editedItem.FindControl("lblStatus1");
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            
            TextBox txtName = (TextBox)editedItem.FindControl("txtName");
            RadTextBox txtCity = (RadTextBox)editedItem.FindControl("txtCity");
            TextBox txtAddress1 = (TextBox)editedItem.FindControl("txtAddress1");
            RadTextBox txtAddress2 = (RadTextBox)editedItem.FindControl("txtAddress2");
            RadTextBox txtState = (RadTextBox)editedItem.FindControl("txtState");
            RadTextBox txtCountry = (RadTextBox)editedItem.FindControl("txtCountry");
            TextBox txtPhone = editedItem.FindControl("txtPhone") as TextBox;
            RadTextBox txtPostcode = (RadTextBox)editedItem.FindControl("txtPostcode");
            RadTextBox txtFax = (RadTextBox)editedItem.FindControl("txtFax");
            Label lblCustomerID = (Label)editedItem.FindControl("lblCustomerID");
            RadTextBox txtEmail = (RadTextBox)editedItem.FindControl("txtEmail");
            RadTextBox txtWebsite = (RadTextBox)editedItem.FindControl("txtWebsite");
            RadTextBox txtDesc = (RadTextBox)editedItem.FindControl("txtDesc");
            TextBox txtShortForm = (TextBox)editedItem.FindControl("txtShortForm");

            if (Convert.ToInt32(txtName.Text.Length.ToString().Trim()) <= 5)
            {
                lblStatus1.Text = "Dealer name is required for atleast 5 characters";
                 return;
            }

          
            int flg = BusinessTier.SaveCustomer(conn, lblCustomerID.Text.ToString(), txtName.Text.ToString().Trim(), txtAddress1.Text.ToString().Trim(), txtAddress2.Text.ToString().Trim(), txtCity.Text.ToString().Trim(), txtState.Text.ToString().Trim(), txtCountry.Text.ToString().Trim(), txtPostcode.Text.ToString().Trim(), txtDesc.Text.ToString().Trim(), txtPhone.Text.ToString().Trim(), txtFax.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtWebsite.Text.ToString().Trim(), Session["sesUserID"].ToString(), "N", "0");
            BusinessTier.DisposeConnection(conn);
            RadGrid1.Rebind();
            if (flg >= 1)
            {
                lblStatus1.Text = "Dealer Info Inserted Successfully";
            }
           }
        catch (Exception ex)
        {
            lblStatus1.Text = "Unable to insert, Please try again/Contact your administrator";
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
             }

        //m_datatable.Rows.Add(newRow);
       
    }

    protected void RadGrid1_UpdateCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        GridEditFormItem editedItem = e.Item as GridEditFormItem;
        Label lblStatus1 = (Label)editedItem.FindControl("lblStatus1");
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            Label lblID = (Label)editedItem.FindControl("lblID");
            TextBox txtName = (TextBox)editedItem.FindControl("txtName");
            RadTextBox txtCity = (RadTextBox)editedItem.FindControl("txtCity");
            TextBox txtAddress1 = (TextBox)editedItem.FindControl("txtAddress1");
            RadTextBox txtAddress2 = (RadTextBox)editedItem.FindControl("txtAddress2");
            RadTextBox txtState = (RadTextBox)editedItem.FindControl("txtState");
            RadTextBox txtCountry = (RadTextBox)editedItem.FindControl("txtCountry");
            TextBox txtPhone = editedItem.FindControl("txtPhone") as TextBox;
            RadTextBox txtPostcode = (RadTextBox)editedItem.FindControl("txtPostcode");
            RadTextBox txtFax = (RadTextBox)editedItem.FindControl("txtFax");
            Label lblCustomerID = (Label)editedItem.FindControl("lblCustomerID");
            RadTextBox txtEmail = (RadTextBox)editedItem.FindControl("txtEmail");
            RadTextBox txtWebsite = (RadTextBox)editedItem.FindControl("txtWebsite");
            RadTextBox txtDesc = (RadTextBox)editedItem.FindControl("txtDesc");
            TextBox txtShortForm = (TextBox)editedItem.FindControl("txtShortForm");

            if (Convert.ToInt32(txtName.Text.Length.ToString().Trim()) <= 5)
            {
                lblStatus.Text = "Dealer name is required for atleast 5 characters";
                return;
            }

           
            int flg = BusinessTier.SaveCustomer(conn, lblCustomerID.Text.ToString(), txtName.Text.ToString().Trim(), txtAddress1.Text.ToString().Trim(), txtAddress2.Text.ToString().Trim(), txtCity.Text.ToString().Trim(), txtState.Text.ToString().Trim(), txtCountry.Text.ToString().Trim(), txtPostcode.Text.ToString().Trim(), txtDesc.Text.ToString().Trim(), txtPhone.Text.ToString().Trim(), txtFax.Text.ToString().Trim(), txtEmail.Text.ToString().Trim(), txtWebsite.Text.ToString().Trim(), Session["sesUserID"].ToString(), "U", lblID.Text.ToString().Trim());
            BusinessTier.DisposeConnection(conn);
           
            if (flg >= 1)
            {
                lblStatus.Text = "Dealer Info Updated Successfully";
                //string display = "Dealer Info Updated Successfully";
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + display + "');", true);
             }
          }
        catch (Exception ex)
        {
            lblStatus1.Text = "Unable to Updated, Please try again/Contact your administrator";
            e.Canceled = true;
            BusinessTier.DisposeConnection(conn);
        }
        RadGrid1.Rebind();
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
