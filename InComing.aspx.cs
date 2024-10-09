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

using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Threading;
using System.IO;
using System.Text;

public partial class InComing : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();
    public DataTable dtSubMenuItems = new DataTable();

    public static string g_strUserID;
    public static Int32 IncomeQty;

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

    protected void cboMaterialType_SelectChange(object sender, EventArgs e)
    {
        cboMaterialCode.Text = string.Empty;
        txtMaterialName.Text = string.Empty;
        txtSize.Text = string.Empty;
        SqlConnection connMenu = BusinessTier.getConnection();
        connMenu.Open();
        try
        {
            string sql1 = "select * FROM MaterialMaster WHERE Deleted=0 and MaterialType='" + cboMaterialType.SelectedItem.Text.ToString() + "' order by MaterialName ";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, connMenu);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboMaterialCode.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Materialcode"].ToString();
                item.Value = row["MaterialAutoid"].ToString();
                item.Attributes.Add("Materialcode", row["Materialcode"].ToString());
                item.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item.Attributes.Add("Size", row["Size"].ToString());
                cboMaterialCode.Items.Add(item);
                item.DataBind();
            }
            BusinessTier.DisposeConnection(connMenu);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(connMenu);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "IncomingMaterial", "cboMaterialCode_SelectChange", ex.ToString(), "Audit");
        }
    }

    protected void cboMaterialCode_SelectChange(object sender, EventArgs e)
    {
        txtMaterialName.Text = string.Empty;
        txtSize.Text = string.Empty;
        txtMaterialName.Text = cboMaterialCode.SelectedItem.Attributes["MaterialName"].ToString();
        txtSize.Text = cboMaterialCode.SelectedItem.Attributes["Size"].ToString();
    }

    protected void Items_Clear()
    {
        lblID.Text = string.Empty;
        txtMaterialName.Text = string.Empty;
        txtSize.Text = string.Empty;
        txtQuantity.Text = string.Empty;
        cboMaterialCode.Text = string.Empty;
        cboMaterialType.Text = string.Empty;
        cboMaterialCode.ClearSelection();
        cboMaterialType.ClearSelection();
        dtpStockDate.Clear(); 
        }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if ((string.IsNullOrEmpty(cboMaterialType.Text.ToString())))
        {
            lblStatus.Text = "Select MaterialType";
           lblStatus.ForeColor = Color.Red;
            return;
        }
        if ((string.IsNullOrEmpty(cboMaterialCode.Text.ToString())))
        {
            lblStatus.Text = "Select MaterialCode";
           lblStatus.ForeColor = Color.Red;
            return;
        }

        if ((string.IsNullOrEmpty(dtpStockDate.SelectedDate.ToString())))
        {
            lblStatus.Text = "Select StockDate";
           lblStatus.ForeColor = Color.Red;
            return;
        }

        if ((string.IsNullOrEmpty(txtQuantity.Text.ToString())))
        {
            lblStatus.Text = "Enter Quantity";
           lblStatus.ForeColor = Color.Red;
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            String StockDate = dtpStockDate.SelectedDate.ToString();
            DateTime pdt = DateTime.Parse(StockDate);
            StockDate = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

            if (lblID.Text == "")
            {
                string sql2 = "select * from IncomingMaterial where deleted=0 and MaterialAutoid='" + cboMaterialCode.SelectedValue.ToString().Trim() + "' and StockDate='" + StockDate.ToString().Trim() + "'  ";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlDataReader rd2 = cmd2.ExecuteReader();

                if (rd2.Read())
                {
                    lblStatus.Text = "This Mateial & StockDate values are alreaady Exist try another Mateial or StockDate";
                   lblStatus.ForeColor = Color.Red;
                    return;
                }
                rd2.Close();
                int flg = BusinessTier.SaveIncoming(conn, cboMaterialCode.SelectedValue.ToString().Trim(), cboMaterialType.Text.ToString().Trim(), cboMaterialCode.Text.ToString().Trim(), txtMaterialName.Text.ToString().Trim(), txtSize.Text.ToString().Trim(), StockDate.ToString().Trim(), txtQuantity.Text.ToString().Trim(), Session["sesUserID"].ToString(), "N","0");
               
                if (flg >= 1)
                {
                    lblStatus.Text = "Incoming Material Info Inserted Successfully";
                }
               
                string sql = "select Quantity from MaterialMaster where deleted=0 and MaterialAutoid='" + cboMaterialCode.SelectedValue.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Convert.ToInt32(txtQuantity.Text.ToString().Trim()));
                    rd.Close();
                    string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and MaterialAutoid='" + cboMaterialCode.SelectedValue.ToString().Trim() + "'";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();

                }
                else
                {
                    rd.Close();
                }
             
            }
            else
            {
                int flg = BusinessTier.SaveIncoming(conn, cboMaterialCode.SelectedValue.ToString().Trim(), cboMaterialType.Text.ToString().Trim(), cboMaterialCode.Text.ToString().Trim(), txtMaterialName.Text.ToString().Trim(), txtSize.Text.ToString().Trim(), StockDate.ToString().Trim(), txtQuantity.Text.ToString().Trim(), Session["sesUserID"].ToString(), "U", lblID.Text.ToString());

                if (flg >= 1)
                {
                    lblStatus.Text = "Incoming Material Info Updated Successfully";
                }
                string sql = "select Quantity from MaterialMaster where deleted=0 and MaterialAutoid='" + cboMaterialCode.SelectedValue.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    int qty = (Convert.ToInt32(rd["Quantity"].ToString()) - IncomeQty) + Convert.ToInt32(txtQuantity.Text.ToString().Trim());
                    rd.Close();
                    string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and MaterialAutoid='" + cboMaterialCode.SelectedValue.ToString().Trim() + "'";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();

                }
                else
                {
                    rd.Close();
                }
            }

           lblStatus.ForeColor = Color.Red;
            BusinessTier.DisposeConnection(conn);
            Items_Clear();
            RadGrid1.Rebind(); 
            cboMaterialType.Enabled = true;
            cboMaterialCode.Enabled = true;
            
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Incoming Material", "Save", ex.ToString(), "Audit");
        }
    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
        //ClearInputs(Page.Controls);
        //try
        //{
        //    MailMessage message1 = new MailMessage();
            
        //    message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
        //    message1.To.Add(new MailAddress("system@sj-classic.com"));
        //    message1.Subject = "Request OrderForm Confirmation";
        //    message1.Body = "Thank you for contacting us. We will be in touch with you very soon.";
        //    SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
        //    client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
            
        //    client1.Send(message1);
        //    lblStatus.Text = "Successfully e-mail sent!";
        //    lblStatus.ForeColor = System.Drawing.Color.Yellow;
        //}
        //catch (Exception ex)
        //{
        //    lblStatus.Text = ex.Message;
        //}
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            cboMaterialType.Enabled = false;
            cboMaterialCode.Enabled = false;
            e.Item.Selected = true;
            string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IncomingAutoid"]).ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                g_strUserID = strTakenId.ToString();   //Assign userID to class variable for update.
                string sql2 = "select * from IncomingMaterial where deleted=0 and IncomingAutoid='" + strTakenId.ToString().Trim() + "'";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.Read())
                {
                    lblID.Text = (rd2["IncomingAutoid"].ToString());
                    cboMaterialType.Text = (rd2["MaterialType"].ToString());
                    cboMaterialCode.Text = (rd2["Materialcode"].ToString());
                    cboMaterialCode.SelectedValue = (rd2["MaterialAutoid"].ToString());
                    txtMaterialName.Text = (rd2["MaterialName"].ToString());
                    txtSize.Text = (rd2["Size"].ToString());
                    txtQuantity.Text = (rd2["Quantity"].ToString());
                    IncomeQty =Convert.ToInt32(rd2["Quantity"].ToString());
                    dtpStockDate.SelectedDate = Convert.ToDateTime(rd2["StockDate"].ToString());
                         }
                BusinessTier.DisposeReader(rd2);
                BusinessTier.DisposeConnection(conn);
                lblStatus.Text = "";
            }
            catch (Exception ex)
            {
                BusinessTier.DisposeConnection(conn);
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "Incoming Material", "RadGrid_ItemClick", ex.ToString(), "Audit");
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
            string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["IncomingAutoid"].ToString();
            string incomeqty = string.Empty, incomeid = string.Empty;
            string sql2 = "select Quantity,MaterialAutoid from IncomingMaterial where deleted=0 and IncomingAutoid='" + ID.ToString().Trim() + "'";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlDataReader rd2 = cmd2.ExecuteReader();
                if (rd2.Read())
                {
                    incomeqty = rd2["Quantity"].ToString();
                    incomeid = rd2["MaterialAutoid"].ToString();
                }
                rd2.Close();

                string sql = "select Quantity from MaterialMaster where deleted=0 and MaterialAutoid='" + incomeid.ToString().Trim() + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read())
                {
                    int qty = (Convert.ToInt32(rd["Quantity"].ToString())) - Convert.ToInt32(incomeqty.ToString().Trim());
                    rd.Close();
                    string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and MaterialAutoid='" + incomeid.ToString().Trim() + "'";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);
                    cmd1.ExecuteNonQuery();

                }
                else
                {
                    rd.Close();
                }

            string sql5 = "UPDATE [SJClassic].[dbo].[IncomingMaterial] SET [Modifiedby] = '" + Session["sesUserID"].ToString() + "' ,[Modifieddate] = '" + DateTime.Now + "',[Deleted] = 1  WHERE IncomingAutoid =" + ID.ToString() + "";
            SqlCommand cmd5 = new SqlCommand(sql5, conn);
            cmd5.ExecuteNonQuery();
            lblStatus.Text = "Successfully Value Deleted";
            lblStatus.ForeColor = Color.Red;
            BusinessTier.DisposeConnection(conn);
            Items_Clear();
            RadGrid1.Rebind();
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "Incoming Material", "Delete", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        int delval = 0;
        string sql = "select * FROM IncomingMaterial WHERE Deleted='" + delval + "' order by IncomingAutoid desc";
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