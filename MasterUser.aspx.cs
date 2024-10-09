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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;
using System.Drawing;

public partial class MasterUser : System.Web.UI.Page
{
    public static string g_strSaveFlag;
    public static string g_strUserID;
    public static string g_strLoginID;
    public static string g_strIsApproverAdded;

    public DataTable dtMenuItems = new DataTable();
    public DataTable dtSubMenuItems = new DataTable();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {
                //btnRegister.Text = "Save";
                //if (!(IsPostBack))
                //{
                //    //clearFields();
                //    g_strSaveFlag = "Insert";
                //    g_strUserID = "0";
                //    g_strLoginID = "0";
                //    g_strIsApproverAdded = "N";
                  
                //    lblStatus.Text = "";
                //}

                SqlConnection connMenu = BusinessTier.getConnection();
                connMenu.Open();
                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);

                string sql1 = "select * FROM MasterStaff WHERE Deleted=0 order by StaffAutoID ";
                SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, connMenu);
                DataTable dataTable1 = new DataTable();
                adapter1.Fill(dataTable1);
                cboName.Items.Clear();
                foreach (DataRow row in dataTable1.Rows)
                {
                        RadComboBoxItem item = new RadComboBoxItem();
                        item.Text = row["Name"].ToString();
                        item.Value = row["StaffAutoID"].ToString();
                        item.Attributes.Add("StaffID", row["StaffID"].ToString());
                        item.Attributes.Add("Name", row["Name"].ToString());
                        cboName.Items.Add(item);
                        item.DataBind();
                    }
           
                BusinessTier.DisposeConnection(connMenu);
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(Session["sesUserID"].ToString()))
            {
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

    protected void Items_Clear() 
    {
        lblID.Text = string.Empty;
        txtUserID.Text = string.Empty;
        txtPass.Text = string.Empty;
        txtDesignation.Text = string.Empty;
        cboName.Text = string.Empty;
        cboUserType.Text = string.Empty;
        cboName.ClearSelection();
        cboUserType.ClearSelection();
        }

    protected void cboName_SelectChange(object sender, EventArgs e) 
    {
                string id = cboName.SelectedItem.Attributes["StaffID"].ToString();
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                try
                {
                    string sql1 = "select * from MasterUser where StaffAutoID='" + cboName.SelectedValue.ToString() + "' and deleted=0 ";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    if (reader1.Read())
                    {
                        reader1.Close();
                        lblStatus.Text = "This user have already permission you cannot be added!";
                       lblStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        reader1.Close();
                        string sql = "select StaffID, Designation from MasterStaff where StaffID='" + id.ToString() + "' and deleted=0 ";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txtDesignation.Text = (reader["Designation"].ToString());
                        }
                        reader.Close();
                    }
                    BusinessTier.DisposeConnection(conn);
                }
                catch (Exception ex) 
                {
                    //lblStatus.Text = ex.Message;
                    BusinessTier.DisposeConnection(conn);
                    InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "cboName_SelectChange", ex.ToString(), "Audit");
                }
    }

   
    protected void btnSave_Click(object sender, EventArgs e) 
    {
        if ((string.IsNullOrEmpty(txtUserID.Text.ToString())))
        {
            lblStatus.Text = "Enter UserID";
           lblStatus.ForeColor = Color.Red;
            return;
        }
        if ((string.IsNullOrEmpty(txtPass.Text.ToString())))
        {
            lblStatus.Text = "Enter Password";
           lblStatus.ForeColor = Color.Red;
            return;
        }
        
        if ((string.IsNullOrEmpty(cboName.Text.ToString())))
        {
            lblStatus.Text = "Select Name";
           lblStatus.ForeColor = Color.Red;
            return;
        }

        if ((string.IsNullOrEmpty(cboUserType.Text.ToString())))
        {
            lblStatus.Text = "Select UserType";
           lblStatus.ForeColor = Color.Red;
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

           
                if (lblID.Text == "")
                {
                    string sql5 = "INSERT INTO [SJClassic].[dbo].[MasterUser]([LoginId],[Password],StaffAutoID,[UserName],[Designation],[UserType],[Createdby],[Createddate])VALUES ('" + txtUserID.Text.ToString() + "','" + txtPass.Text.ToString() + "','" + Convert.ToInt32(cboName.SelectedValue.ToString()) + "','" + cboName.Text.ToString() + "','" + txtDesignation.Text.ToString() + "','" + cboUserType.Text.ToString() + "','" + Session["sesUserID"].ToString() + "','" + DateTime.Now.ToString() + "')";
                    SqlCommand cmd5 = new SqlCommand(sql5, conn);
                    cmd5.ExecuteNonQuery();
                    lblStatus.Text = "Successfully Value Inserted";
                }
                else
                {
                    string sql5 = "UPDATE [SJClassic].[dbo].[MasterUser] SET [LoginId] =  '" + txtUserID.Text.ToString() + "' ,[Password] = '" + txtPass.Text.ToString() + "'  ,StaffAutoID = '" + Convert.ToInt32(cboName.SelectedValue.ToString()) + "',[UserName] =  '" + cboName.Text.ToString() + "'  ,[Designation] = '" + txtDesignation.Text.ToString() + "' ,[UserType] = '" + cboUserType.Text.ToString() + "' , [Modifiedby] = '" + Session["sesUserID"].ToString() + "' ,[Modifieddate] = '" + DateTime.Now.ToString() + "' WHERE id =" + lblID.Text.ToString() + " and deleted=0";
                    SqlCommand cmd5 = new SqlCommand(sql5, conn);
                    cmd5.ExecuteNonQuery();
                    lblStatus.Text = "Successfully Value Updated";
                }
          
           lblStatus.ForeColor = Color.Red;
            BusinessTier.DisposeConnection(conn);
            Items_Clear();
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "Save", ex.ToString(), "Audit");
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
        //ClearInputs(Page.Controls);
    }

   protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"]).ToString();
            try
            {
               
                g_strUserID = strTakenId.ToString();   //Assign userID to class variable for update.
                SqlConnection conn = BusinessTier.getConnection();
                conn.Open();
                SqlDataReader reader = BusinessTier.getMasterUserByID(conn, g_strUserID);
                if (reader.Read())
                {
                    lblID.Text = (reader["id"].ToString());
                    txtUserID.Text = (reader["LoginId"].ToString());
                    txtPass.Text = (reader["Password"].ToString());
                    cboName.Text = (reader["UserName"].ToString());
                    cboName.SelectedValue = (reader["StaffAutoID"].ToString());
                    txtDesignation.Text = (reader["Designation"].ToString());
                    cboUserType.Text = (reader["UserType"].ToString());
                    //cboName.SelectedValue = (reader["id"].ToString());
                    cboName.Enabled = false;
                      }
                BusinessTier.DisposeReader(reader);
                BusinessTier.DisposeConnection(conn);
                lblStatus.Text = "";
                           }
            catch (Exception ex)
            {
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "ItemClick", ex.ToString(), "Audit");
            }
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
            ShowMessage(9);
            //InsertLogAuditTrial is used to insert the action into MasterAuditTrail table
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "NeedDataSource", ex.ToString(), "Audit");
        }
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"].ToString();
            if (ID == "15")
            {
                lblStatus.Text = "Cannot be deleted Administrator";
                return;
            }
            string sql5 = "UPDATE [SJClassic].[dbo].[MasterUser] SET [Modifiedby] = '" + Session["sesUserID"].ToString() + "' ,[Modifieddate] = '" + DateTime.Now + "',[Deleted] = 1  WHERE id =" + ID.ToString() + "";
            SqlCommand cmd5 = new SqlCommand(sql5, conn);
            cmd5.ExecuteNonQuery();
            lblStatus.Text = "Successfully Value Deleted";
           lblStatus.ForeColor = Color.Red;
            BusinessTier.DisposeConnection(conn);
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterUser", "Delete", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        int delval = 0;
        string sql = "select * FROM MasterUser WHERE Deleted='" + delval + "' order by id desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
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