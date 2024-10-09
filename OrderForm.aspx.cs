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
using System.Threading;
using System.IO;
using System.Text;


public partial class OrderForm : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    public static string desig = string.Empty;

    public static int tax = 0;

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
            SqlConnection connMenu = BusinessTier.getConnection();
            connMenu.Open();

            if (!(string.IsNullOrEmpty(Session["sesUserID"].ToString())))
            {

                SqlDataReader readerMenu = BusinessTier.getMenuList(connMenu, Session["sesUserID"].ToString().Trim(), Session["sesUserType"].ToString().Trim());
                dtMenuItems.Load(readerMenu);
                BusinessTier.DisposeReader(readerMenu);

            }
            else
            {
                SqlConnection connSave = BusinessTier.getConnection();
                connSave.Open();
                int iflg = BusinessTier.InsertLogTable(connSave, Session["sesUserID"].ToString(), "UpdateVal");
                connSave.Close();
                Response.Redirect("Login.aspx");
            }

            string sql = "select Designation from MasterUser where deleted=0 and StaffAutoID='" + Session["stid"] + "'";
            SqlCommand cmd1 = new SqlCommand(sql, connMenu);
            SqlDataReader rd1 = cmd1.ExecuteReader();
            if (rd1.Read())
            {
                if (rd1["Designation"].ToString() == "Manager")
                {

                    tabStep5.Visible = true;
                    tabStep6.Enabled = false;
                }
                else if (rd1["Designation"].ToString() == "Engineer")
                {
                    EnableInputs(Page.Controls);
                    btnSave.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnAddNew.Enabled = false;
                    chkApproval.Enabled = false;
                    txtdeldate.Enabled = false;
                    txtdate.Enabled = false;
                    tabStep6.Visible = true;

                    txtPulleypcs.Enabled = true;

                    cboSpring1.Enabled = true;
                    cboSpring2.Enabled = true;
                    cboSpring3.Enabled = true;
                    cboSpring4.Enabled = true;
                    cboSpring5.Enabled = true;
                    cboSpring6.Enabled = true;

                    txtSpring1.Enabled = true;
                    txtSpring2.Enabled = true;
                    txtSpring3.Enabled = true;
                    txtSpring4.Enabled = true;
                    txtSpring5.Enabled = true;
                    txtSpring6.Enabled = true;

                    cbostaffid3.Enabled = true;
                    txtsendmail3.Enabled = true;

                }
                else if (rd1["Designation"].ToString() == "Admin")
                {
                    tabStep5.Enabled = true;
                    tabStep6.Enabled = true;
                }
                else
                {
                    tabStep5.Enabled = false;
                    tabStep6.Enabled = false;
                }
            }
            rd1.Close();
            BusinessTier.DisposeConnection(connMenu);
            Enabled(false);
            Cbo_Spring_load();
            Cbo_Customer_load();

        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    // ---------------- %% ** !! Enable Function !! ** %% ---------------- //

    void EnableInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Enabled = false;
            if (ctrl is RadComboBox)
                ((RadComboBox)ctrl).Enabled = false;
            EnableInputs(ctrl.Controls);
        }
    }

    protected void Enabled(Boolean torf)
    {
        cbostaffid2.Enabled = torf;
        txtsendmail2.Enabled = torf;
        //btnUpdate.Enabled = torf;

    }

    protected void Enabledstep6(Boolean torf)
    {
        txtPulleypcs.Enabled = torf;

        cboSpring1.Enabled = torf;
        cboSpring2.Enabled = torf;
        cboSpring3.Enabled = torf;
        cboSpring4.Enabled = torf;
        cboSpring5.Enabled = torf;
        cboSpring6.Enabled = torf;

        txtSpring1.Enabled = torf;
        txtSpring2.Enabled = torf;
        txtSpring3.Enabled = torf;
        txtSpring4.Enabled = torf;
        txtSpring5.Enabled = torf;
        txtSpring6.Enabled = torf;

        cbostaffid3.Enabled = torf;
        txtsendmail3.Enabled = torf;

    }

    protected void EnabledMotor(Boolean torf)
    {
        cboControlBox.Text = string.Empty;
        cboVoltage.Text = string.Empty;
        cboCurrent.Text = string.Empty;
        cboManualOverride.Text = string.Empty;
        cboRemoteBox.Text = string.Empty;
        cboUPSBattery.Text = string.Empty;
        cboWarrantyMotor.Text = string.Empty;

        cboControlBox.Enabled = torf;
        cboVoltage.Enabled = torf;
        cboCurrent.Enabled = torf;
        cboManualOverride.Enabled = torf;
        cboRemoteBox.Enabled = torf;
        cboUPSBattery.Enabled = torf;
        cboWarrantyMotor.Enabled = torf;

    }

    // ---------------- %% ** !! Load Function !! ** %% ---------------- //

    protected void Cbo_Customer_load()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            txtdate.SelectedDate = DateTime.Now.Date;
            string sql = "select max(OrderAutoID)as maxval from OrderForm";
            SqlCommand cmd1 = new SqlCommand(sql, conn);
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

            txtno.Text = "OrdNo/" + DateTime.Today.ToString("yyMMdd/" + val);
            BusinessTier.DisposeReader(rd1);
            string sql1 = "select * FROM Customer WHERE Deleted=0 order by CustomerID ";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboCustomerID.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["CustomerAutoID"].ToString();
                item.Attributes.Add("CustomerAutoID", row["CustomerAutoID"].ToString());
                item.Attributes.Add("CustomerName", row["CustomerName"].ToString());
                cboCustomerID.Items.Add(item);
                item.DataBind();
            }

            string sql2 = "select * FROM MasterStaff WHERE Deleted=0 order by StaffAutoID ";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            cbostaffid.Items.Clear();
            cbostaffid2.Items.Clear();
            cbostaffid3.Items.Clear();
            cboman1.Items.Clear();
            cboman2.Items.Clear();
            cboman3.Items.Clear();
            cboman4.Items.Clear();
            cboman5.Items.Clear();
            cboman6.Items.Clear();
            foreach (DataRow row in dataTable2.Rows)
            {
                if (row["Designation"].ToString() == "Manager" || row["Designation"].ToString() == "Engineer")
                {
                    RadComboBoxItem item1 = new RadComboBoxItem();
                    item1.Text = row["Name"].ToString();
                    item1.Value = row["StaffAutoID"].ToString();
                    item1.Attributes.Add("Designation", row["Designation"].ToString());
                    item1.Attributes.Add("Name", row["Name"].ToString());
                    cbostaffid.Items.Add(item1);
                    item1.DataBind();
                }
                if (row["Designation"].ToString() == "Engineer")
                {
                    RadComboBoxItem item2 = new RadComboBoxItem();
                    item2.Text = row["Name"].ToString();
                    item2.Value = row["StaffAutoID"].ToString();
                    item2.Attributes.Add("StaffID", row["StaffID"].ToString());
                    item2.Attributes.Add("Name", row["Name"].ToString());
                    cbostaffid2.Items.Add(item2);
                    item2.DataBind();
                }
                if (row["Designation"].ToString() == "Technician")
                {
                    RadComboBoxItem item3 = new RadComboBoxItem();
                    item3.Text = row["Name"].ToString();
                    item3.Value = row["StaffAutoID"].ToString();
                    item3.Attributes.Add("StaffID", row["StaffID"].ToString());
                    item3.Attributes.Add("Name", row["Name"].ToString());
                    cbostaffid3.Items.Add(item3);
                    item3.DataBind();

                    RadComboBoxItem item4 = new RadComboBoxItem();
                    item4.Text = row["Name"].ToString();
                    cboman1.Items.Add(item4);
                    item4.DataBind();

                    RadComboBoxItem item5 = new RadComboBoxItem();
                    item5.Text = row["Name"].ToString();
                    cboman2.Items.Add(item5);
                    item5.DataBind();

                    RadComboBoxItem item6 = new RadComboBoxItem();
                    item6.Text = row["Name"].ToString();
                    cboman3.Items.Add(item6);
                    item6.DataBind();

                    RadComboBoxItem item7 = new RadComboBoxItem();
                    item7.Text = row["Name"].ToString();
                    cboman4.Items.Add(item7);
                    item7.DataBind();

                    RadComboBoxItem item8 = new RadComboBoxItem();
                    item8.Text = row["Name"].ToString();
                    cboman5.Items.Add(item8);
                    item8.DataBind();

                    RadComboBoxItem item9 = new RadComboBoxItem();
                    item9.Text = row["Name"].ToString();
                    cboman6.Items.Add(item9);
                    item9.DataBind();
                }
            }

            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Cbo_Customer_load", ex.ToString(), "Audit");
        }
    }

    protected void Cbo_Spring_load()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql = "select * from MaterialMaster where deleted=0 and materialtype='Spring' ORDER BY [MaterialAutoid]";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboSpring1.Items.Clear();
            cboSpring2.Items.Clear();
            cboSpring3.Items.Clear();
            cboSpring4.Items.Clear();
            cboSpring5.Items.Clear();
            cboSpring6.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                RadComboBoxItem item1 = new RadComboBoxItem();
                RadComboBoxItem item2 = new RadComboBoxItem();
                RadComboBoxItem item3 = new RadComboBoxItem();
                RadComboBoxItem item4 = new RadComboBoxItem();
                RadComboBoxItem item5 = new RadComboBoxItem();

                item.Text = row["MaterialName"].ToString();

                item.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item.Attributes.Add("Quantity", row["Quantity"].ToString());

                item1.Text = row["MaterialName"].ToString();

                item1.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item1.Attributes.Add("Quantity", row["Quantity"].ToString());

                item2.Text = row["MaterialName"].ToString();

                item2.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item2.Attributes.Add("Quantity", row["Quantity"].ToString());

                item3.Text = row["MaterialName"].ToString();

                item3.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item3.Attributes.Add("Quantity", row["Quantity"].ToString());

                item4.Text = row["MaterialName"].ToString();

                item4.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item4.Attributes.Add("Quantity", row["Quantity"].ToString());

                item5.Text = row["MaterialName"].ToString();

                item5.Attributes.Add("MaterialName", row["MaterialName"].ToString());
                item5.Attributes.Add("Quantity", row["Quantity"].ToString());

                cboSpring1.Items.Add(item);
                cboSpring2.Items.Add(item1);
                cboSpring3.Items.Add(item2);
                cboSpring4.Items.Add(item3);
                cboSpring5.Items.Add(item4);
                cboSpring6.Items.Add(item5);
                item.DataBind();
                item1.DataBind();
                item2.DataBind();
                item3.DataBind();
                item4.DataBind();
                item5.DataBind();
            }

            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
        }
    }

    // ---------------- %% ** !! Selected Index Changed !! ** %% ---------------- //

    protected void cboSpring1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSpring1.Text.ToString() == cboSpring2.Text.ToString() || cboSpring1.Text.ToString() == cboSpring3.Text.ToString() || cboSpring1.Text.ToString() == cboSpring4.Text.ToString() || cboSpring1.Text.ToString() == cboSpring5.Text.ToString() || cboSpring1.Text.ToString() == cboSpring6.Text.ToString())
        {
            lblStatus.Text = "The Selected Spring is already Selected";
            cboSpring1.Text = string.Empty;
            cboSpring1.ClearSelection();

        }
        else
        {
            lblStatus.Text = "";

        }
    }

    protected void cboSpring2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSpring2.Text.ToString() == cboSpring1.Text.ToString() || cboSpring2.Text.ToString() == cboSpring3.Text.ToString() || cboSpring2.Text.ToString() == cboSpring4.Text.ToString() || cboSpring2.Text.ToString() == cboSpring5.Text.ToString() || cboSpring2.Text.ToString() == cboSpring6.Text.ToString())
        {
            lblStatus.Text = "The Selected Spring is already Selected";
            cboSpring2.Text = string.Empty;
            cboSpring2.ClearSelection();

        }
        else
        {
            lblStatus.Text = "";

        }
    }

    protected void cboSpring3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSpring3.Text.ToString() == cboSpring1.Text.ToString() || cboSpring3.Text.ToString() == cboSpring5.Text.ToString() || cboSpring3.Text.ToString() == cboSpring6.Text.ToString() || cboSpring3.Text.ToString() == cboSpring2.Text.ToString() || cboSpring3.Text.ToString() == cboSpring4.Text.ToString())
        {
            lblStatus.Text = "The Selected Spring is already Selected";
            cboSpring3.Text = string.Empty;
            cboSpring3.ClearSelection();

        }
        else
        {
            lblStatus.Text = "";

        }
    }

    protected void cboSpring4_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSpring4.Text.ToString() == cboSpring2.Text.ToString() || cboSpring4.Text.ToString() == cboSpring5.Text.ToString() || cboSpring4.Text.ToString() == cboSpring6.Text.ToString() || cboSpring4.Text.ToString() == cboSpring3.Text.ToString() || cboSpring4.Text.ToString() == cboSpring1.Text.ToString())
        {
            lblStatus.Text = "The Selected Spring is already Selected";
            cboSpring4.Text = string.Empty;
            cboSpring4.ClearSelection();

        }
        else
        {
            lblStatus.Text = "";

        }
    }

    protected void cboSpring5_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSpring5.Text.ToString() == cboSpring2.Text.ToString() || cboSpring5.Text.ToString() == cboSpring4.Text.ToString() || cboSpring5.Text.ToString() == cboSpring6.Text.ToString() || cboSpring5.Text.ToString() == cboSpring3.Text.ToString() || cboSpring5.Text.ToString() == cboSpring1.Text.ToString())
        {
            lblStatus.Text = "The Selected Spring is already Selected";
            cboSpring4.Text = string.Empty;
            cboSpring4.ClearSelection();

        }
        else
        {
            lblStatus.Text = "";

        }
    }

    protected void cboSpring6_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboSpring6.Text.ToString() == cboSpring2.Text.ToString() || cboSpring6.Text.ToString() == cboSpring4.Text.ToString() || cboSpring6.Text.ToString() == cboSpring5.Text.ToString() || cboSpring6.Text.ToString() == cboSpring3.Text.ToString() || cboSpring6.Text.ToString() == cboSpring1.Text.ToString())
        {
            lblStatus.Text = "The Selected Spring is already Selected";
            cboSpring4.Text = string.Empty;
            cboSpring4.ClearSelection();

        }
        else
        {
            lblStatus.Text = "";

        }
    }

    protected void cbostaffid_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "select * FROM MasterStaff WHERE Deleted=0 and StaffAutoID='" + cbostaffid.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                txtStaffMail.Text = rd["Email"].ToString();
            }
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Cbo_Customer_load", ex.ToString(), "Audit");
        }
    }

    protected void cbostaffid2_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "select * FROM MasterStaff WHERE Deleted=0 and StaffAutoID='" + cbostaffid2.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                txtsendmail2.Text = rd["Email"].ToString();
            }
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Cbo_Customer_load", ex.ToString(), "Audit");
        }
    }

    protected void cbostaffid3_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql = "select * FROM MasterStaff WHERE Deleted=0 and StaffAutoID='" + cbostaffid3.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                txtsendmail3.Text = rd["Email"].ToString();
            }
            rd.Close();
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Cbo_Customer_load", ex.ToString(), "Audit");
        }
    }

    protected void cboCustomerID_SelectedIndexChanged(object sender, EventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql2 = "select * from Customer where deleted=0 and CustomerAutoid='" + cboCustomerID.SelectedItem.Attributes["CustomerAutoID"].ToString() + "' ORDER BY [Customerid]";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            if (rd2.Read())
            {
                txtname.Text = rd2["Customername"].ToString();
                txttel.Text = rd2["Contactno"].ToString();
                string addrs = string.Empty;
                if (rd2["Address1"].ToString() != "")
                {
                    addrs = rd2["Address1"].ToString() + ", ";
                }
                if (rd2["Address2"].ToString() != "")
                {
                    addrs = addrs + rd2["Address2"].ToString() + ", ";
                }
                if (rd2["City"].ToString() != "")
                {
                    addrs = addrs + rd2["City"].ToString() + ", ";
                }
                if (rd2["State"].ToString() != "")
                {
                    addrs = addrs + rd2["State"].ToString() + ", ";
                }
                if (rd2["Postcode"].ToString() != "")
                {
                    addrs = addrs + rd2["Postcode"].ToString() + ", ";
                }
                if (rd2["Country"].ToString() != "")
                {
                    addrs = addrs + rd2["Country"].ToString() + ".";
                }

                txtaddress.Text = addrs.ToString();
            }

            BusinessTier.DisposeReader(rd2);
            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "CustomerID_SelectedIndexChanged", ex.ToString(), "Audit");
        }
    }

    protected void cboMotor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboMotor.Text.ToString() == "None")
        {
            EnabledMotor(false);
        }
        else
        {
            EnabledMotor(true);
        }
    }

    protected void chkApproval_CheckedChange(object sender, EventArgs e)
    {
        if (chkApproval.Checked == true)
        {
            Enabled(true);
        }
        else
        {
            Enabled(false);
            cbostaffid2.ClearSelection();
            txtsendmail2.Text = "";
        }
    }

    protected void cbopriceWallType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (txtpriceheight.Text.ToString() == "")
        {
            lblStatus.Text = "Enter Height";
            return;
        }
        if (txtpricewidth.Text.ToString() == "")
        {
            lblStatus.Text = "Enter Width";
            return;
        }

        lblStatus.Text = "";
        try
        {
            double hval = Convert.ToDouble(txtpriceheight.Text.ToString());

            double calhg = hval + 1300;
            txtheightcal.Text = calhg.ToString();

            //txtheightcal.Text = hval.ToString();
            double wval = Convert.ToDouble(txtpricewidth.Text.ToString());
            double total = 0.00;
            if (cbopriceWallType.Text == "Adjustable Wall")
            {
                total = ((hval + 750) * (wval + 240) / 1000000) * 10.764;
                txtunit.Text = Math.Round(total).ToString();

                double calwd = wval + 100;
                txtwidthcal.Text = calwd.ToString();
                txtwidthcal2.Text = string.Empty;
            }
            else if (cbopriceWallType.Text == "Non-Adjustable Wall")
            {
                total = ((hval + 750) * (wval) / 1000000) * 10.764;
                txtunit.Text = Math.Round(total).ToString();

                double calwd = (wval - 240) + 100;
                txtwidthcal.Text = calwd.ToString();
                txtwidthcal2.Text = string.Empty;
            }
            else if (cbopriceWallType.Text == "Adjustable & Non-Adjustable Wall")
            {

                total = ((hval + 750) * (wval + 120) / 1000000) * 10.764;
                txtunit.Text = Math.Round(total).ToString();

                double calwd = (wval - 120) + 100;
                txtwidthcal.Text = calwd.ToString();
                txtwidthcal2.Text =string.Empty;

            }
            else if (cbopriceWallType.Text == "Adjustable Wall (Separate)")
            {
                total = ((hval + 750) * (wval + 240) / 1000000) * 10.764;
                txtunit.Text = Math.Round(total).ToString();

                double sepwd = wval / 2;

                double calwd = (sepwd - 40) + 50;
                txtwidthcal.Text = calwd.ToString();
                txtwidthcal2.Text = calwd.ToString();
            }
            else if (cbopriceWallType.Text == "Non-Adjustable Wall (Separate)")
            {
                total = ((hval + 750) * (wval + 240) / 1000000) * 10.764;
                txtunit.Text = Math.Round(total).ToString();

                double sepwd = wval / 2;

                double calwd = ((sepwd - 120) + 50)-40;
                txtwidthcal.Text = calwd.ToString();
                txtwidthcal2.Text = calwd.ToString();
            }

            else if (cbopriceWallType.Text == "Adjustable & Non-Adjustable Wall (Separate)")
            {
                total = ((hval + 750) * (wval + 240) / 1000000) * 10.764;
                txtunit.Text = Math.Round(total).ToString();

                double sepwd = wval / 2;

                double calwd = (sepwd +50) - 40;
               txtwidthcal.Text = calwd.ToString();
                //txtwidthcal2.Text = calwd.ToString();

                double calwd2 = ((sepwd - 120) + 50) - 40;
                //txtwidthcal.Text = calwd.ToString();
                txtwidthcal2.Text = calwd2.ToString();
            }
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Height & Width Calculation", ex.ToString(), "Audit");
        }
    }

    // ---------------- %% ** !! Text Changed !! ** %% ---------------- //

    protected void txtColorQty1_TextChange(object sender, EventArgs e)
    {
        if (cboColor1.Text == "")
        {
            lblStatus.Text = "Select Color1";
            return;
        }

        if (CboColorheight1.Text == "")
        {
            lblStatus.Text = "Select Height1";
            return;
        }

        int balance = 0, total = 0, colhgt1 = 0, colhgt2 = 0, colhgt3 = 0, colhgt4 = 0, colhgt5 = 0, colhgt6 = 0, colqty1 = 0, colqty2 = 0, colqty3 = 0, colqty4 = 0, colqty5 = 0, colqty6 = 0;

        if (CboColorheight1.Text == "")
        {
            colhgt1 = 0;
        }
        else
        {
            colhgt1 = Convert.ToInt32(CboColorheight1.Text);
        }
        //-------------------------------------------
        if (CboColorheight2.Text == "")
        {
            colhgt2 = 0;
        }
        else
        {
            colhgt2 = Convert.ToInt32(CboColorheight2.Text);
        }
        //-------------------------------------------
        if (CboColorheight3.Text == "")
        {
            colhgt3 = 0;
        }
        else
        {
            colhgt3 = Convert.ToInt32(CboColorheight3.Text);
        }
        //-------------------------------------------
        if (CboColorheight4.Text == "")
        {
            colhgt4 = 0;
        }
        else
        {
            colhgt4 = Convert.ToInt32(CboColorheight4.Text);
        }
        //-------------------------------------------
        if (CboColorheight5.Text == "")
        {
            colhgt5 = 0;
        }
        else
        {
            colhgt5 = Convert.ToInt32(CboColorheight5.Text);
        }
        //-------------------------------------------
        if (CboColorheight6.Text == "")
        {
            colhgt6 = 0;
        }
        else
        {
            colhgt6 = Convert.ToInt32(CboColorheight6.Text);
        }
        //-------------------------------------------


        if (txtColorQty1.Text == "")
        {
            colqty1 = 0;
        }
        else
        {
            colqty1 = Convert.ToInt32(txtColorQty1.Text);
        }
        //-------------------------------------------
        if (txtColorQty2.Text == "")
        {
            colqty2 = 0;
        }
        else
        {
            colqty2 = Convert.ToInt32(txtColorQty2.Text);
        }
        //-------------------------------------------
        if (txtColorQty3.Text == "")
        {
            colqty3 = 0;
        }
        else
        {
            colqty3 = Convert.ToInt32(txtColorQty3.Text);
        }
        //-------------------------------------------
        if (txtColorQty4.Text == "")
        {
            colqty4 = 0;
        }
        else
        {
            colqty4 = Convert.ToInt32(txtColorQty4.Text);
        }
        //-------------------------------------------
        if (txtColorQty5.Text == "")
        {
            colqty5 = 0;
        }
        else
        {
            colqty5 = Convert.ToInt32(txtColorQty5.Text);
        }
        //-------------------------------------------
        if (txtColorQty6.Text == "")
        {
            colqty6 = 0;
        }
        else
        {
            colqty6 = Convert.ToInt32(txtColorQty6.Text);
        }
        //-------------------------------------------

        total = (colhgt1 * colqty1) + (colhgt2 * colqty2) + (colhgt3 * colqty3) + (colhgt4 * colqty4) + (colhgt5 * colqty5) + (colhgt6 * colqty6);
        balance = Convert.ToInt32(txtheightcal.Text) - total;
        if (0 >= balance)
        {
            lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Green;
        }

    }

    protected void txtColorQty2_TextChange(object sender, EventArgs e)
    {
        if (cboColor2.Text == "")
        {
            lblStatus.Text = "Select Color2";
            return;
        }

        if (CboColorheight2.Text == "")
        {
            lblStatus.Text = "Select Height2";
            return;
        }

        int balance = 0, total = 0, colhgt1 = 0, colhgt2 = 0, colhgt3 = 0, colhgt4 = 0, colhgt5 = 0, colhgt6 = 0, colqty1 = 0, colqty2 = 0, colqty3 = 0, colqty4 = 0, colqty5 = 0, colqty6 = 0;

        if (CboColorheight1.Text == "")
        {
            colhgt1 = 0;
        }
        else
        {
            colhgt1 = Convert.ToInt32(CboColorheight1.Text);
        }
        //-------------------------------------------
        if (CboColorheight2.Text == "")
        {
            colhgt2 = 0;
        }
        else
        {
            colhgt2 = Convert.ToInt32(CboColorheight2.Text);
        }
        //-------------------------------------------
        if (CboColorheight3.Text == "")
        {
            colhgt3 = 0;
        }
        else
        {
            colhgt3 = Convert.ToInt32(CboColorheight3.Text);
        }
        //-------------------------------------------
        if (CboColorheight4.Text == "")
        {
            colhgt4 = 0;
        }
        else
        {
            colhgt4 = Convert.ToInt32(CboColorheight4.Text);
        }
        //-------------------------------------------
        if (CboColorheight5.Text == "")
        {
            colhgt5 = 0;
        }
        else
        {
            colhgt5 = Convert.ToInt32(CboColorheight5.Text);
        }
        //-------------------------------------------
        if (CboColorheight6.Text == "")
        {
            colhgt6 = 0;
        }
        else
        {
            colhgt6 = Convert.ToInt32(CboColorheight6.Text);
        }
        //-------------------------------------------


        if (txtColorQty1.Text == "")
        {
            colqty1 = 0;
        }
        else
        {
            colqty1 = Convert.ToInt32(txtColorQty1.Text);
        }
        //-------------------------------------------
        if (txtColorQty2.Text == "")
        {
            colqty2 = 0;
        }
        else
        {
            colqty2 = Convert.ToInt32(txtColorQty2.Text);
        }
        //-------------------------------------------
        if (txtColorQty3.Text == "")
        {
            colqty3 = 0;
        }
        else
        {
            colqty3 = Convert.ToInt32(txtColorQty3.Text);
        }
        //-------------------------------------------
        if (txtColorQty4.Text == "")
        {
            colqty4 = 0;
        }
        else
        {
            colqty4 = Convert.ToInt32(txtColorQty4.Text);
        }
        //-------------------------------------------
        if (txtColorQty5.Text == "")
        {
            colqty5 = 0;
        }
        else
        {
            colqty5 = Convert.ToInt32(txtColorQty5.Text);
        }
        //-------------------------------------------
        if (txtColorQty6.Text == "")
        {
            colqty6 = 0;
        }
        else
        {
            colqty6 = Convert.ToInt32(txtColorQty6.Text);
        }
        //-------------------------------------------

        total = (colhgt1 * colqty1) + (colhgt2 * colqty2) + (colhgt3 * colqty3) + (colhgt4 * colqty4) + (colhgt5 * colqty5) + (colhgt6 * colqty6);
        balance = Convert.ToInt32(txtheightcal.Text) - total;
        if (0 >= balance)
        {
            lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Green;
        }
    }

    protected void txtColorQty3_TextChange(object sender, EventArgs e)
    {
        if (cboColor3.Text == "")
        {
            lblStatus.Text = "Select Color3";
            return;
        }

        if (CboColorheight3.Text == "")
        {
            lblStatus.Text = "Select Height3";
            return;
        }

        int balance = 0, total = 0, colhgt1 = 0, colhgt2 = 0, colhgt3 = 0, colhgt4 = 0, colhgt5 = 0, colhgt6 = 0, colqty1 = 0, colqty2 = 0, colqty3 = 0, colqty4 = 0, colqty5 = 0, colqty6 = 0;

        if (CboColorheight1.Text == "")
        {
            colhgt1 = 0;
        }
        else
        {
            colhgt1 = Convert.ToInt32(CboColorheight1.Text);
        }
        //-------------------------------------------
        if (CboColorheight2.Text == "")
        {
            colhgt2 = 0;
        }
        else
        {
            colhgt2 = Convert.ToInt32(CboColorheight2.Text);
        }
        //-------------------------------------------
        if (CboColorheight3.Text == "")
        {
            colhgt3 = 0;
        }
        else
        {
            colhgt3 = Convert.ToInt32(CboColorheight3.Text);
        }
        //-------------------------------------------
        if (CboColorheight4.Text == "")
        {
            colhgt4 = 0;
        }
        else
        {
            colhgt4 = Convert.ToInt32(CboColorheight4.Text);
        }
        //-------------------------------------------
        if (CboColorheight5.Text == "")
        {
            colhgt5 = 0;
        }
        else
        {
            colhgt5 = Convert.ToInt32(CboColorheight5.Text);
        }
        //-------------------------------------------
        if (CboColorheight6.Text == "")
        {
            colhgt6 = 0;
        }
        else
        {
            colhgt6 = Convert.ToInt32(CboColorheight6.Text);
        }
        //-------------------------------------------


        if (txtColorQty1.Text == "")
        {
            colqty1 = 0;
        }
        else
        {
            colqty1 = Convert.ToInt32(txtColorQty1.Text);
        }
        //-------------------------------------------
        if (txtColorQty2.Text == "")
        {
            colqty2 = 0;
        }
        else
        {
            colqty2 = Convert.ToInt32(txtColorQty2.Text);
        }
        //-------------------------------------------
        if (txtColorQty3.Text == "")
        {
            colqty3 = 0;
        }
        else
        {
            colqty3 = Convert.ToInt32(txtColorQty3.Text);
        }
        //-------------------------------------------
        if (txtColorQty4.Text == "")
        {
            colqty4 = 0;
        }
        else
        {
            colqty4 = Convert.ToInt32(txtColorQty4.Text);
        }
        //-------------------------------------------
        if (txtColorQty5.Text == "")
        {
            colqty5 = 0;
        }
        else
        {
            colqty5 = Convert.ToInt32(txtColorQty5.Text);
        }
        //-------------------------------------------
        if (txtColorQty6.Text == "")
        {
            colqty6 = 0;
        }
        else
        {
            colqty6 = Convert.ToInt32(txtColorQty6.Text);
        }
        //-------------------------------------------

        total = (colhgt1 * colqty1) + (colhgt2 * colqty2) + (colhgt3 * colqty3) + (colhgt4 * colqty4) + (colhgt5 * colqty5) + (colhgt6 * colqty6);
        balance = Convert.ToInt32(txtheightcal.Text) - total;
        if (0 >= balance)
        {
            lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Green;
        }
    }

    protected void txtColorQty4_TextChange(object sender, EventArgs e)
    {
        if (cboColor4.Text == "")
        {
            lblStatus.Text = "Select Color4";
            return;
        }

        if (CboColorheight4.Text == "")
        {
            lblStatus.Text = "Select Height4";
            return;
        }

        int balance = 0, total = 0, colhgt1 = 0, colhgt2 = 0, colhgt3 = 0, colhgt4 = 0, colhgt5 = 0, colhgt6 = 0, colqty1 = 0, colqty2 = 0, colqty3 = 0, colqty4 = 0, colqty5 = 0, colqty6 = 0;

        if (CboColorheight1.Text == "")
        {
            colhgt1 = 0;
        }
        else
        {
            colhgt1 = Convert.ToInt32(CboColorheight1.Text);
        }
        //-------------------------------------------
        if (CboColorheight2.Text == "")
        {
            colhgt2 = 0;
        }
        else
        {
            colhgt2 = Convert.ToInt32(CboColorheight2.Text);
        }
        //-------------------------------------------
        if (CboColorheight3.Text == "")
        {
            colhgt3 = 0;
        }
        else
        {
            colhgt3 = Convert.ToInt32(CboColorheight3.Text);
        }
        //-------------------------------------------
        if (CboColorheight4.Text == "")
        {
            colhgt4 = 0;
        }
        else
        {
            colhgt4 = Convert.ToInt32(CboColorheight4.Text);
        }
        //-------------------------------------------
        if (CboColorheight5.Text == "")
        {
            colhgt5 = 0;
        }
        else
        {
            colhgt5 = Convert.ToInt32(CboColorheight5.Text);
        }
        //-------------------------------------------
        if (CboColorheight6.Text == "")
        {
            colhgt6 = 0;
        }
        else
        {
            colhgt6 = Convert.ToInt32(CboColorheight6.Text);
        }
        //-------------------------------------------


        if (txtColorQty1.Text == "")
        {
            colqty1 = 0;
        }
        else
        {
            colqty1 = Convert.ToInt32(txtColorQty1.Text);
        }
        //-------------------------------------------
        if (txtColorQty2.Text == "")
        {
            colqty2 = 0;
        }
        else
        {
            colqty2 = Convert.ToInt32(txtColorQty2.Text);
        }
        //-------------------------------------------
        if (txtColorQty3.Text == "")
        {
            colqty3 = 0;
        }
        else
        {
            colqty3 = Convert.ToInt32(txtColorQty3.Text);
        }
        //-------------------------------------------
        if (txtColorQty4.Text == "")
        {
            colqty4 = 0;
        }
        else
        {
            colqty4 = Convert.ToInt32(txtColorQty4.Text);
        }
        //-------------------------------------------
        if (txtColorQty5.Text == "")
        {
            colqty5 = 0;
        }
        else
        {
            colqty5 = Convert.ToInt32(txtColorQty5.Text);
        }
        //-------------------------------------------
        if (txtColorQty6.Text == "")
        {
            colqty6 = 0;
        }
        else
        {
            colqty6 = Convert.ToInt32(txtColorQty6.Text);
        }
        //-------------------------------------------

        total = (colhgt1 * colqty1) + (colhgt2 * colqty2) + (colhgt3 * colqty3) + (colhgt4 * colqty4) + (colhgt5 * colqty5) + (colhgt6 * colqty6);
        balance = Convert.ToInt32(txtheightcal.Text) - total;
        if (0 >= balance)
        {
            lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Green;
        }
    }

    protected void txtColorQty5_TextChange(object sender, EventArgs e)
    {
        if (cboColor5.Text == "")
        {
            lblStatus.Text = "Select Color5";
            return;
        }

        if (CboColorheight5.Text == "")
        {
            lblStatus.Text = "Select Height5";
            return;
        }

        int balance = 0, total = 0, colhgt1 = 0, colhgt2 = 0, colhgt3 = 0, colhgt4 = 0, colhgt5 = 0, colhgt6 = 0, colqty1 = 0, colqty2 = 0, colqty3 = 0, colqty4 = 0, colqty5 = 0, colqty6 = 0;

        if (CboColorheight1.Text == "")
        {
            colhgt1 = 0;
        }
        else
        {
            colhgt1 = Convert.ToInt32(CboColorheight1.Text);
        }
        //-------------------------------------------
        if (CboColorheight2.Text == "")
        {
            colhgt2 = 0;
        }
        else
        {
            colhgt2 = Convert.ToInt32(CboColorheight2.Text);
        }
        //-------------------------------------------
        if (CboColorheight3.Text == "")
        {
            colhgt3 = 0;
        }
        else
        {
            colhgt3 = Convert.ToInt32(CboColorheight3.Text);
        }
        //-------------------------------------------
        if (CboColorheight4.Text == "")
        {
            colhgt4 = 0;
        }
        else
        {
            colhgt4 = Convert.ToInt32(CboColorheight4.Text);
        }
        //-------------------------------------------
        if (CboColorheight5.Text == "")
        {
            colhgt5 = 0;
        }
        else
        {
            colhgt5 = Convert.ToInt32(CboColorheight5.Text);
        }
        //-------------------------------------------
        if (CboColorheight6.Text == "")
        {
            colhgt6 = 0;
        }
        else
        {
            colhgt6 = Convert.ToInt32(CboColorheight6.Text);
        }
        //-------------------------------------------


        if (txtColorQty1.Text == "")
        {
            colqty1 = 0;
        }
        else
        {
            colqty1 = Convert.ToInt32(txtColorQty1.Text);
        }
        //-------------------------------------------
        if (txtColorQty2.Text == "")
        {
            colqty2 = 0;
        }
        else
        {
            colqty2 = Convert.ToInt32(txtColorQty2.Text);
        }
        //-------------------------------------------
        if (txtColorQty3.Text == "")
        {
            colqty3 = 0;
        }
        else
        {
            colqty3 = Convert.ToInt32(txtColorQty3.Text);
        }
        //-------------------------------------------
        if (txtColorQty4.Text == "")
        {
            colqty4 = 0;
        }
        else
        {
            colqty4 = Convert.ToInt32(txtColorQty4.Text);
        }
        //-------------------------------------------
        if (txtColorQty5.Text == "")
        {
            colqty5 = 0;
        }
        else
        {
            colqty5 = Convert.ToInt32(txtColorQty5.Text);
        }
        //-------------------------------------------
        if (txtColorQty6.Text == "")
        {
            colqty6 = 0;
        }
        else
        {
            colqty6 = Convert.ToInt32(txtColorQty6.Text);
        }
        //-------------------------------------------

        total = (colhgt1 * colqty1) + (colhgt2 * colqty2) + (colhgt3 * colqty3) + (colhgt4 * colqty4) + (colhgt5 * colqty5) + (colhgt6 * colqty6);
        balance = Convert.ToInt32(txtheightcal.Text) - total;
        if (0 >= balance)
        {
            lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Green;
        }
    }

    protected void txtColorQty6_TextChange(object sender, EventArgs e)
    {
        if (cboColor6.Text == "")
        {
            lblStatus.Text = "Select Color6";
            return;
        }

        if (CboColorheight6.Text == "")
        {
            lblStatus.Text = "Select Height6";
            return;
        }

        int balance = 0, total = 0, colhgt1 = 0, colhgt2 = 0, colhgt3 = 0, colhgt4 = 0, colhgt5 = 0, colhgt6 = 0, colqty1 = 0, colqty2 = 0, colqty3 = 0, colqty4 = 0, colqty5 = 0, colqty6 = 0;

        if (CboColorheight1.Text == "")
        {
            colhgt1 = 0;
        }
        else
        {
            colhgt1 = Convert.ToInt32(CboColorheight1.Text);
        }
        //-------------------------------------------
        if (CboColorheight2.Text == "")
        {
            colhgt2 = 0;
        }
        else
        {
            colhgt2 = Convert.ToInt32(CboColorheight2.Text);
        }
        //-------------------------------------------
        if (CboColorheight3.Text == "")
        {
            colhgt3 = 0;
        }
        else
        {
            colhgt3 = Convert.ToInt32(CboColorheight3.Text);
        }
        //-------------------------------------------
        if (CboColorheight4.Text == "")
        {
            colhgt4 = 0;
        }
        else
        {
            colhgt4 = Convert.ToInt32(CboColorheight4.Text);
        }
        //-------------------------------------------
        if (CboColorheight5.Text == "")
        {
            colhgt5 = 0;
        }
        else
        {
            colhgt5 = Convert.ToInt32(CboColorheight5.Text);
        }
        //-------------------------------------------
        if (CboColorheight6.Text == "")
        {
            colhgt6 = 0;
        }
        else
        {
            colhgt6 = Convert.ToInt32(CboColorheight6.Text);
        }
        //-------------------------------------------


        if (txtColorQty1.Text == "")
        {
            colqty1 = 0;
        }
        else
        {
            colqty1 = Convert.ToInt32(txtColorQty1.Text);
        }
        //-------------------------------------------
        if (txtColorQty2.Text == "")
        {
            colqty2 = 0;
        }
        else
        {
            colqty2 = Convert.ToInt32(txtColorQty2.Text);
        }
        //-------------------------------------------
        if (txtColorQty3.Text == "")
        {
            colqty3 = 0;
        }
        else
        {
            colqty3 = Convert.ToInt32(txtColorQty3.Text);
        }
        //-------------------------------------------
        if (txtColorQty4.Text == "")
        {
            colqty4 = 0;
        }
        else
        {
            colqty4 = Convert.ToInt32(txtColorQty4.Text);
        }
        //-------------------------------------------
        if (txtColorQty5.Text == "")
        {
            colqty5 = 0;
        }
        else
        {
            colqty5 = Convert.ToInt32(txtColorQty5.Text);
        }
        //-------------------------------------------
        if (txtColorQty6.Text == "")
        {
            colqty6 = 0;
        }
        else
        {
            colqty6 = Convert.ToInt32(txtColorQty6.Text);
        }
        //-------------------------------------------

        total = (colhgt1 * colqty1) + (colhgt2 * colqty2) + (colhgt3 * colqty3) + (colhgt4 * colqty4) + (colhgt5 * colqty5) + (colhgt6 * colqty6);
        balance = Convert.ToInt32(txtheightcal.Text) - total;
        if (0 >= balance)
        {
            lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Red;
        }
        else
        {
            lblStatus.Text = string.Empty;
            lblbalheight.Text = balance.ToString();
            lblbalheight.ForeColor = Color.Green;
        }
    }

    protected void txtSpring1_TextChange(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboSpring1.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 1";
            txtSpring1.Text = string.Empty;
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring1.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string qty = string.Empty;
            if (rd.Read())
            {
                qty = rd["Quantity"].ToString();

            }
            rd.Close();
            if (Convert.ToInt32(qty.ToString()) <= Convert.ToInt32(txtSpring1.Text.ToString()))
            {
                lblStatus.Text = "The given Quantity is higher than Stocked Quantity";
                txtSpring1.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "";
            }
            txtSpring1.Text =(Convert.ToInt32(txtSpring1.Text.ToString()) * Convert.ToInt32(txtUnitQuantity.Text.ToString())).ToString();
        }
        catch (Exception ex)
        {

        }

    }

    protected void txtSpring2_TextChange(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboSpring2.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 2";
            txtSpring2.Text = string.Empty;
            return;
        }


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring2.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string qty = string.Empty;
            if (rd.Read())
            {
                qty = rd["Quantity"].ToString();

            }
            rd.Close();
            if (Convert.ToInt32(qty.ToString()) <= Convert.ToInt32(txtSpring2.Text.ToString()))
            {
                lblStatus.Text = "The given Quantity is higher than Stocked Quantity";
                txtSpring2.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "";
            }
            txtSpring2.Text = (Convert.ToInt32(txtSpring2.Text.ToString()) * Convert.ToInt32(txtUnitQuantity.Text.ToString())).ToString();
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtSpring3_TextChange(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboSpring3.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 3";
            txtSpring3.Text = string.Empty;
            return;
        }


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring3.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string qty = string.Empty;
            if (rd.Read())
            {
                qty = rd["Quantity"].ToString();

            }
            rd.Close();
            if (Convert.ToInt32(qty.ToString()) <= Convert.ToInt32(txtSpring3.Text.ToString()))
            {
                lblStatus.Text = "The given Quantity is higher than Stocked Quantity";
                txtSpring3.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "";
            }
            txtSpring3.Text = (Convert.ToInt32(txtSpring3.Text.ToString()) * Convert.ToInt32(txtUnitQuantity.Text.ToString())).ToString();
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtSpring4_TextChange(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboSpring4.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 4";
            txtSpring4.Text = string.Empty;
            return;
        }


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring4.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string qty = string.Empty;
            if (rd.Read())
            {
                qty = rd["Quantity"].ToString();

            }
            rd.Close();
            if (Convert.ToInt32(qty.ToString()) <= Convert.ToInt32(txtSpring4.Text.ToString()))
            {
                lblStatus.Text = "The given Quantity is higher than Stocked Quantity";
                txtSpring4.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "";
            }
            txtSpring4.Text = (Convert.ToInt32(txtSpring4.Text.ToString()) * Convert.ToInt32(txtUnitQuantity.Text.ToString())).ToString();
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtSpring5_TextChange(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboSpring5.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 5";
            txtSpring5.Text = string.Empty;
            return;
        }


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring5.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string qty = string.Empty;
            if (rd.Read())
            {
                qty = rd["Quantity"].ToString();

            }
            rd.Close();
            if (Convert.ToInt32(qty.ToString()) <= Convert.ToInt32(txtSpring5.Text.ToString()))
            {
                lblStatus.Text = "The given Quantity is higher than Stocked Quantity";
                txtSpring5.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "";
            }
            txtSpring5.Text = (Convert.ToInt32(txtSpring5.Text.ToString()) * Convert.ToInt32(txtUnitQuantity.Text.ToString())).ToString();
        }
        catch (Exception ex)
        {

        }
    }

    protected void txtSpring6_TextChange(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(cboSpring6.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 6";
            txtSpring6.Text = string.Empty;
            return;
        }


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring6.Text.ToString().Trim() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            string qty = string.Empty;
            if (rd.Read())
            {
                qty = rd["Quantity"].ToString();

            }
            rd.Close();
            if (Convert.ToInt32(qty.ToString()) <= Convert.ToInt32(txtSpring6.Text.ToString()))
            {
                lblStatus.Text = "The given Quantity is higher than Stocked Quantity";
                txtSpring6.Text = string.Empty;
            }
            else
            {
                lblStatus.Text = "";
            }
            txtSpring6.Text = (Convert.ToInt32(txtSpring6.Text.ToString()) * Convert.ToInt32(txtUnitQuantity.Text.ToString())).ToString();
        }
        catch (Exception ex)
        {

        }
    }

    // ---------------- %% ** !! RadGrid Functions !! ** %% ---------------- //

    protected void RadGrid1_NeedDataSource(object source, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        try
        {
            RadGrid1.DataSource = DataSourceHelper();
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "GridLoad", ex.ToString(), "Audit");
        }
    }

    public DataTable DataSourceHelper()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Close();
        string sql = "select * FROM orderform WHERE Deleted=0 order by orderautoid desc";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, conn);
        DataTable g_datatable = new DataTable();
        sqlDataAdapter.Fill(g_datatable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
        BusinessTier.DisposeConnection(conn);
        return g_datatable;
    }

    protected void RadGrid1_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        if (e.CommandName == "RowClick" && e.Item is GridDataItem)
        {
            e.Item.Selected = true;
            string strTakenId = (e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OrderAutoID"]).ToString();
            SqlConnection conn = BusinessTier.getConnection();
            conn.Open();
            try
            {
                string sql = "Select * from OrderForm where OrderAutoID='" + strTakenId.ToString() + "' and deleted=0";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {

                    ClearInputs(Page.Controls);
                    // cboCustomerID.ClearSelection();
                    lblID.Text = (rd["OrderAutoID"].ToString());

                    txtno.Text = (rd["OrderNo"].ToString());
                    txtdono.Text = (rd["SINo"].ToString());
                    cboDoorType.Text = (rd["DoorType"].ToString());
                    txtdate.DbSelectedDate = (rd["Date"].ToString());
                    txtstatus.Text = (rd["OrdStatus"].ToString());
                    txtdeldate.DbSelectedDate = (rd["ShippingDate"].ToString());
                    cboCustomerID.Text = (rd["DealerID"].ToString());
                    txtname.Text = (rd["DealerName"].ToString());
                    txttel.Text = (rd["Tel"].ToString());
                    txtaddress.Text = (rd["Address"].ToString());
                    cboDelivery.Text = (rd["DeliveryType"].ToString());
                    txtremarks.Text = (rd["Remark1"].ToString());

                    cboman1.Text = (rd["InstallMan1"].ToString());
                    cboman2.Text = (rd["InstallMan2"].ToString());
                    cboman3.Text = (rd["InstallMan3"].ToString());
                    cboman4.Text = (rd["InstallMan4"].ToString());
                    cboman5.Text = (rd["InstallMan5"].ToString());
                    cboman6.Text = (rd["InstallMan6"].ToString());

                    txtpriceheight.Text = (rd["Height"].ToString());
                    txtpricewidth.Text = (rd["Width"].ToString());
                    cbopriceWallType.Text = (rd["Walltype"].ToString());
                    txtunit.Text = (rd["Unit"].ToString());
                    txtUnitQuantity.Text = (rd["UnitQty"].ToString());
                    txtBasePrice.Text = (rd["BasePrice"].ToString());
                    txtUnitPrice.Text = (rd["UnitPrice"].ToString());
                    cbopriceWallType.SelectedValue = (rd["TaxPrice"].ToString());

                    cboTax.Text = (rd["Tax"].ToString());
                    txtUnitPriceNotax.Text = (rd["UnitPriceNoTax"].ToString());

                    txtTotalAmount.Text = (rd["TotalAmt"].ToString());
                    txtpriceRemarks.Text = (rd["Remark2"].ToString());
                    textSpecialSpec.Text = (rd["SplSpec"].ToString());

                    txtheightcal.Text = (rd["HeightCal"].ToString());
                    txtwidthcal.Text = (rd["WidthCal"].ToString());
                    txtwidthcal2.Text = (rd["WidthCal2"].ToString());
                    cboColor1.Text = (rd["Color1"].ToString());
                    cboColor2.Text = (rd["Color2"].ToString());
                    cboColor3.Text = (rd["Color3"].ToString());
                    cboColor4.Text = (rd["Color4"].ToString());
                    cboColor5.Text = (rd["Color5"].ToString());
                    cboColor6.Text = (rd["Color6"].ToString());

                    CboColorheight1.Text = (rd["Hgt1"].ToString());
                    CboColorheight2.Text = (rd["Hgt2"].ToString());
                    CboColorheight3.Text = (rd["Hgt3"].ToString());
                    CboColorheight4.Text = (rd["Hgt4"].ToString());
                    CboColorheight5.Text = (rd["Hgt5"].ToString());
                    CboColorheight6.Text = (rd["Hgt6"].ToString());

                    txtColorQty1.Text = (rd["Qty1"].ToString());
                    txtColorQty2.Text = (rd["Qty2"].ToString());
                    txtColorQty3.Text = (rd["Qty3"].ToString());
                    txtColorQty4.Text = (rd["Qty4"].ToString());
                    txtColorQty5.Text = (rd["Qty5"].ToString());
                    txtColorQty6.Text = (rd["Qty6"].ToString());

                    lblbalheight.Text = (rd["balheight"].ToString());
                    int balance = Convert.ToInt32(rd["balheight"].ToString());
                    if (0 >= balance)
                    {
                        lblStatus.Text = "The Extra Height is " + Math.Abs(balance).ToString() + "mm";
                        lblbalheight.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblStatus.Text = string.Empty;
                        lblbalheight.ForeColor = Color.Green;
                    }

                    cboVentilationHole.Text = (rd["Venthole"].ToString());
                    txtVentilationRows.Text = (rd["VentRow"].ToString());
                    cboLetterBox.Text = (rd["Letterbox"].ToString());
                    cboControlBox.Text = (rd["ControlBox"].ToString());
                    cboLockType.Text = (rd["LockType"].ToString());

                    txtOtherLockTypes.Text = (rd["OtherLckTyp"].ToString());
                    cboVoltage.Text = (rd["Voltage"].ToString());
                    cboCurrent.Text = (rd["Currnt"].ToString());
                    cboUPSBattery.Text = (rd["UPSBattery"].ToString());
                    cboNamePlates.Text = (rd["NamePlate"].ToString());

                    cboMotor.Text = (rd["Motor"].ToString());
                    cboManualOverride.Text = (rd["ManualOver"].ToString());
                    cboRemoteBox.Text = (rd["RemoteBox"].ToString());
                    cboDoorOrientation.Text = (rd["DoorOrein"].ToString());
                    cboPullHandle.Text = (rd["PullHandle"].ToString());
                    cboPullHook.Text = (rd["PullHook"].ToString());

                    cboPacking.Text = (rd["Packing"].ToString());
                    cboWarrantyDoor.Text = (rd["WrntyDoor"].ToString());
                    cboWarrantyMotor.Text = (rd["WrntyMotor"].ToString());
                    txtsplremarks.Text = (rd["SplRemark"].ToString());
                    cboAluminiumBottomBar.Text = (rd["AluBottomBar"].ToString());
                    cboNylonPolystrip.Text = (rd["NylonPoly"].ToString());
                    cbostaffid.Text = (rd["SendName"].ToString());
                    txtStaffMail.Text = (rd["SendMail"].ToString());

                    if (rd["Approval"].ToString() == "True")
                    {
                        chkApproval.Checked = true;

                        if (Session["stid"].ToString() == "Manager")
                            Enabled(true);
                        else
                            Enabled(false);
                    }
                    else
                    {
                        Enabled(false);
                        chkApproval.Checked = false;
                    }


                    //if (chkApproval.Checked == true)
                    //{
                    //    Enabledstep6(true);

                    //}
                    //else
                    //{
                    //    Enabledstep6(false);

                    //}


                    cbostaffid2.Text = (rd["SendName1"].ToString());
                    txtsendmail2.Text = (rd["SendMail1"].ToString());

                    txtPulleypcs.Text = (rd["Pulley"].ToString());

                    cboSpring1.Text = (rd["Spring1"].ToString());
                    cboSpring2.Text = (rd["Spring2"].ToString());
                    cboSpring3.Text = (rd["Spring3"].ToString());
                    cboSpring4.Text = (rd["Spring4"].ToString());
                    cboSpring5.Text = (rd["Spring5"].ToString());
                    cboSpring6.Text = (rd["Spring6"].ToString());

                    txtSpring1.Text = (rd["Spring1pcs"].ToString());
                    txtSpring2.Text = (rd["Spring2pcs"].ToString());
                    txtSpring3.Text = (rd["Spring3pcs"].ToString());
                    txtSpring4.Text = (rd["Spring4pcs"].ToString());
                    txtSpring5.Text = (rd["Spring5pcs"].ToString());
                    txtSpring6.Text = (rd["Spring6pcs"].ToString());

                    cbostaffid3.Text = (rd["SendName3"].ToString());
                    txtsendmail3.Text = (rd["SendMail3"].ToString());
                }
                BusinessTier.DisposeReader(rd);
                BusinessTier.DisposeConnection(conn);
                lblStatus.Text = "";
            }
            catch (Exception ex)
            {
                //lblStatus.Text = ex.Message;
                BusinessTier.DisposeConnection(conn);
                InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "ItemClick", ex.ToString(), "Audit");
            }
        }
    }

    protected void RadGrid1_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            if (Session["Designation"].ToString() == "Engineer")
            {
                lblStatus.Text = "Access Denied, You cannot be permission to Delete";
                lblStatus.ForeColor = Color.Red;
            }
            else
            {

                string ID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["OrderAutoID"].ToString();

                int Pulleyqty = 0, Sp1 = 0, Sp2 = 0, Sp3 = 0, Sp4 = 0, Pull = 0;

                string sql20 = "select * from OrderForm where deleted=0 and OrderAutoID='" + ID.ToString() + "'";
                SqlCommand cmd20 = new SqlCommand(sql20, conn);
                SqlDataReader rd20 = cmd20.ExecuteReader();

                if (rd20.Read())
                {
                    string sprg1 = rd20["Spring1pcs"].ToString();
                    string sprg2 = rd20["Spring2pcs"].ToString();
                    string sprg3 = rd20["Spring3pcs"].ToString();
                    string sprg4 = rd20["Spring4pcs"].ToString();

                    string sprg1nm = rd20["Spring1"].ToString();
                    string sprg2nm = rd20["Spring2"].ToString();
                    string sprg3nm = rd20["Spring3"].ToString();
                    string sprg4nm = rd20["Spring4"].ToString();

                    string pulys = rd20["Pulley"].ToString();

                    rd20.Close();

                    if (sprg1 == "")
                    {
                        Sp1 = 0;
                    }
                    else
                    {
                        Sp1 = Convert.ToInt32(sprg1);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg1nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp1);
                            rd.Close();
                            string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg1nm.ToString().Trim() + "'";
                            SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg2 == "")
                    {
                        Sp2 = 0;
                    }
                    else
                    {
                        Sp2 = Convert.ToInt32(sprg2);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg2nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp2);
                            rd.Close();
                            string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg2nm.ToString().Trim() + "'";
                            SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg3 == "")
                    {
                        Sp3 = 0;
                    }
                    else
                    {
                        Sp3 = Convert.ToInt32(sprg3);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg3nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp3);
                            rd.Close();
                            string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg3nm.ToString().Trim() + "'";
                            SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg4 == "")
                    {
                        Sp4 = 0;
                    }
                    else
                    {
                        Sp4 = Convert.ToInt32(sprg4);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg4nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp4);
                            rd.Close();
                            string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg4nm.ToString().Trim() + "'";
                            SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            cmd1.ExecuteNonQuery();
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (pulys == "")
                    {
                        Pull = 0;
                    }
                    else
                    {
                        Pull = Convert.ToInt32(pulys);
                    }



                }
                else
                {
                    rd20.Close();
                }


                //--------------------------Pulley Update---------------------
                if (txtPulleypcs.Text.ToString() != "")
                {
                    string sql2 = "select Quantity from MaterialMaster where deleted=0 and materialtype='Pulley'";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    SqlDataReader rd2 = cmd2.ExecuteReader();

                    if (rd2.Read())
                    {
                        Pulleyqty = Convert.ToInt32(rd2["Quantity"].ToString());
                        int qty = (Pulleyqty + Pull);
                        rd2.Close();
                        string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialtype='Pulley'";
                        SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        rd2.Close();
                    }
                }




                string sql5 = "UPDATE [SJClassic].[dbo].[OrderForm] SET [Modifiedby] = '" + Session["sesUserID"].ToString() + "' ,[modifydate] = '" + DateTime.Now + "',[Deleted] = 1  WHERE OrderAutoID =" + ID.ToString() + "";
                SqlCommand cmd5 = new SqlCommand(sql5, conn);
                cmd5.ExecuteNonQuery();
                lblStatus.Text = "Successfully Value Deleted";
                lblStatus.ForeColor = Color.Red;
                BusinessTier.DisposeConnection(conn);
                RadGrid1.Rebind();
            }
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Delete", ex.ToString(), "Audit");
        }
    }

    // ---------------- %% ** !! Button Click !! ** %% ---------------- //

    protected void btnCalc_Click(object sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrEmpty(txtunit.Text))
            {
                lblStatus.Text = "Calculate Unit sqft";
                return;
            }
            if (string.IsNullOrEmpty(txtUnitQuantity.Text))
            {
                lblStatus.Text = "Enter UnitQuantity";
                return;
            }
            if (string.IsNullOrEmpty(txtBasePrice.Text))
            {
                lblStatus.Text = "Enter BasePrice";
                return;
            }
            if (string.IsNullOrEmpty(cboTax.Text))
            {
                lblStatus.Text = "Select Tax";
                return;
            }
            lblStatus.Text = "";
            double calprice = 0.00, tax = 0.00;
            calprice = (Convert.ToDouble(txtunit.Text) * Convert.ToDouble(txtBasePrice.Text));
            txtUnitPriceNotax.Text = calprice.ToString();
            tax = (calprice * Convert.ToDouble(cboTax.Text)) / 100;
            cbopriceWallType.SelectedValue = tax.ToString();
            txtUnitPrice.Text = Math.Round((calprice + tax)).ToString();
            txtTotalAmount.Text = Math.Round((Convert.ToDouble(txtUnitQuantity.Text) * Convert.ToDouble(txtUnitPrice.Text))).ToString();
        }
        catch (Exception ex)
        {

            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Height & Width Calculation", ex.ToString(), "Audit");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Session["Name"].ToString().Trim() == "")
        {
            Response.Redirect("Login.aspx");
        }
        if (string.IsNullOrEmpty(cboDoorType.Text.ToString()))
        {
            lblStatus.Text = "Select Step 1: DoorType";
            return;
        }
        if (string.IsNullOrEmpty(txtdeldate.SelectedDate.ToString()))
        {
            lblStatus.Text = "Select Step 1: Delivary Date";
            return;
        }
        if (string.IsNullOrEmpty(cboCustomerID.Text.ToString()))
        {
            lblStatus.Text = "Select Step 1: DealerID";
            return;
        }

        if (string.IsNullOrEmpty(cboDelivery.Text.ToString()))
        {
            lblStatus.Text = "Select Step 1: Method of Delivery";
            return;
        }
        if (string.IsNullOrEmpty(txtpriceheight.Text.ToString()))
        {
            lblStatus.Text = "Enter Step 2: Height";
            return;
        }
        if (string.IsNullOrEmpty(txtpricewidth.Text.ToString()))
        {
            lblStatus.Text = "Enter Step 2: Width";
            return;
        }

        if (string.IsNullOrEmpty(cbopriceWallType.Text.ToString()))
        {
            lblStatus.Text = "Select Step 2: WallType";
            return;
        }
        if (string.IsNullOrEmpty(txtUnitQuantity.Text.ToString()))
        {
            lblStatus.Text = "Enter Step 2: UnitQuantity";
            return;
        }
        if (string.IsNullOrEmpty(txtUnitPrice.Text.ToString()))
        {
            lblStatus.Text = "Click Step 2: Calc Button";
            return;
        }
        if (string.IsNullOrEmpty(txtBasePrice.Text.ToString()))
        {
            lblStatus.Text = "Enter Step 2: BasePrice";
            return;
        }

        if (string.IsNullOrEmpty(cboColor1.Text.ToString()) && string.IsNullOrEmpty(cboColor2.Text.ToString()) && string.IsNullOrEmpty(cboColor3.Text.ToString()) && string.IsNullOrEmpty(cboColor4.Text.ToString()) && string.IsNullOrEmpty(cboColor5.Text.ToString()) && string.IsNullOrEmpty(cboColor6.Text.ToString()))
        {
            lblStatus.Text = "Select atleast any one Step 3: color and height";
            return;
        }
        if (string.IsNullOrEmpty(txtColorQty1.Text.ToString()))
        {
            lblStatus.Text = "Enter Step 3: Color1 Quantity";
            return;
        }

        if (string.IsNullOrEmpty(cboVentilationHole.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: VentilationHole";
            return;
        }

        if (string.IsNullOrEmpty(cboLetterBox.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: LetterBox";
            return;
        }
        //if (string.IsNullOrEmpty(cboControlBox.Text.ToString()))
        //{
        //    lblStatus.Text = "Select Step 4: ControlBox";
        //    return;
        //}
        if (string.IsNullOrEmpty(cboLockType.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: LockType";
            return;
        }

        if (string.IsNullOrEmpty(cboNamePlates.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: NamePlates";
            return;
        }
        if (string.IsNullOrEmpty(cboMotor.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: Motor";
            return;
        }
        //if (string.IsNullOrEmpty(cboManualOverride.Text.ToString()))
        //{
        //    lblStatus.Text = "Select Step 4: ManualOverride";
        //    return;
        //}
        //if (string.IsNullOrEmpty(cboRemoteBox.Text.ToString()))
        //{
        //    lblStatus.Text = "Select Step 4: RemoteBox";
        //    return;
        //}
        if (string.IsNullOrEmpty(cboDoorOrientation.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: DoorOrientation";
            return;
        }
        //if (string.IsNullOrEmpty(cboUPSBattery.Text.ToString()))
        //{
        //    lblStatus.Text = "Select Step 4: UPSBattery";
        //    return;
        //}
        if (string.IsNullOrEmpty(cboPullHandle.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: PullHandle";
            return;
        }
        if (string.IsNullOrEmpty(cboPullHook.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: PullHook";
            return;
        }
        if (string.IsNullOrEmpty(cboPacking.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: Packing";
            return;
        }
        if (string.IsNullOrEmpty(cboWarrantyDoor.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: Warranty on Door Panel";
            return;
        }
        //if (string.IsNullOrEmpty(cboWarrantyMotor.Text.ToString()))
        //{
        //    lblStatus.Text = "Select Step 4: Warranty on Motor";
        //    return;
        //}

        if (string.IsNullOrEmpty(cboAluminiumBottomBar.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: AluminiumBottomBar";
            return;
        }
        if (string.IsNullOrEmpty(cboNylonPolystrip.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: Nylon Polystrip";
            return;
        }
        if (string.IsNullOrEmpty(cbostaffid.Text.ToString()))
        {
            lblStatus.Text = "Select Step 4: Send Mail";
            return;
        }


        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {
            string sql21 = "select OrderAutoID from OrderForm where deleted=0 and OrderNo='" + txtno.Text.ToString() + "'";
            SqlCommand cmd21 = new SqlCommand(sql21, conn);
            SqlDataReader rd21 = cmd21.ExecuteReader();

            if (rd21.Read())
            {
                lblID.Text = rd21["OrderAutoID"].ToString();
                rd21.Close();
                String orddt = txtdate.SelectedDate.ToString();
                DateTime pdt = DateTime.Parse(orddt);
                orddt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year + " 00:00:00";

                String shipdt = txtdeldate.SelectedDate.ToString();
                DateTime idt = DateTime.Parse(shipdt);
                shipdt = idt.Month + "/" + idt.Day + "/" + idt.Year + " 00:00:00";

                //string sql6 = "Update [SJClassic].[dbo].[OrderForm] set [OrderNo]='" + txtno.Text.ToString() + "',[SINo]='" + txtdono.Text.ToString() + "',[DoorType]='" + cboDoorType.Text.ToString() + "',[Date]='" + orddt.ToString() + "',[OrdStatus]='" + txtstatus.Text.ToString() + "',[ShippingDate]='" + shipdt.ToString() + "',[DealerID]='" + cboCustomerID.Text.ToString() + "',[DealerName]='" + txtname.Text.ToString() + "',[Tel]='" + txttel.Text.ToString() + "',[Address]='" + txtaddress.Text.ToString() + "',[DeliveryType]='" + cboDelivery.Text.ToString() + "',[Remark1]='" + txtremarks.Text.ToString() + "',[Height]='" + txtpriceheight.Text.ToString() + "',[Width]='" + txtpricewidth.Text.ToString() + "',[Walltype]='" + cbopriceWallType.Text.ToString() + "',[Unit]='" + txtunit.Text.ToString() + "',[UnitQty]='" + txtUnitQuantity.Text.ToString() + "',[BasePrice]='" + txtBasePrice.Text.ToString() + "',[UnitPrice]='" + txtUnitPrice.Text.ToString() + "',[TotalAmt]='" + txtTotalAmount.Text.ToString() + "',[Remark2]='" + txtpriceRemarks.Text.ToString() + "',[SplSpec]='" + textSpecialSpec.Text.ToString() + "',[HeightCal]='" + txtheightcal.Text.ToString() + "',[WidthCal]='" + txtwidthcal.Text.ToString() + "',[Color1]='" + cboColor1.Text.ToString() + "',[Color2]='" + cboColor2.Text.ToString() + "',[Color3]='" + cboColor3.Text.ToString() + "',[Color4]='" + cboColor4.Text.ToString() + "',[Color5]='" + cboColor5.Text.ToString() + "',[Color6]='" + cboColor6.Text.ToString() + "',[Hgt1]='" + CboColorheight1.Text.ToString() + "',[Hgt2]='" + CboColorheight2.Text.ToString() + "',[Hgt3]='" + CboColorheight3.Text.ToString() + "',[Hgt4]='" + CboColorheight4.Text.ToString() + "',[Hgt5]='" + CboColorheight5.Text.ToString() + "',[Hgt6]='" + CboColorheight6.Text.ToString() + "', [Qty1]='" + txtColorQty1.Text.ToString() + "', [Qty2]='" + txtColorQty2.Text.ToString() + "', [Qty3]='" + txtColorQty3.Text.ToString() + "', [Qty4]='" + txtColorQty4.Text.ToString() + "', [Qty5]='" + txtColorQty5.Text.ToString() + "', [Qty6]='" + txtColorQty6.Text.ToString() + "',[Venthole]='" + cboVentilationHole.Text.ToString() + "',[VentRow]='" + txtVentilationRows.Text.ToString() + "',[Letterbox]='" + cboLetterBox.Text.ToString() + "', [ControlBox]='" + cboControlBox.Text.ToString() + "',[LockType]='" + cboLockType.Text.ToString() + "',[OtherLckTyp]='" + txtOtherLockTypes.Text.ToString() + "',[Voltage]='" + cboVoltage.Text.ToString() + "',[Currnt]='" + cboCurrent.Text.ToString() + "',[NamePlate]='" + cboNamePlates.Text.ToString() + "',[Motor]='" + cboMotor.Text.ToString() + "',[ManualOver]='" + cboManualOverride.Text.ToString() + "',[RemoteBox]='" + cboRemoteBox.Text.ToString() + "',[DoorOrein]='" + cboDoorOrientation.Text.ToString() + "',[UPSBattery]='" + cboUPSBattery.Text.ToString() + "',[PullHandle]='" + cboPullHandle.Text.ToString() + "',[PullHook]='" + cboPullHook.Text.ToString() + "',[Packing]='" + cboPacking.Text.ToString() + "',[WrntyDoor]='" + cboWarrantyDoor.Text.ToString() + "',[WrntyMotor]='" + cboWarrantyMotor.Text.ToString() + "',[SplRemark]='" + txtsplremarks.Text.ToString() + "',[SendName]='" + cbostaffid.Text.ToString() + "',[SendMail]='" + txtStaffMail.Text.ToString() + "',modifiedby='" + Session["sesUserID"].ToString() + "',modifydate='" + DateTime.Now.ToString() + "' ,InstallMan1='" + cboman1.Text.ToString() + "',InstallMan2='" + cboman2.Text.ToString() + "',InstallMan3='" + cboman3.Text.ToString() + "',InstallMan4='" + cboman4.Text.ToString() + "',InstallMan5='" + cboman5.Text.ToString() + "',InstallMan6='" + cboman6.Text.ToString() + "',AluBottomBar='" + cboAluminiumBottomBar.Text.ToString() + "',NylonPoly='" + cboNylonPolystrip.Text.ToString() + "',Tax='" + cboTax.Text.ToString() + "',TaxPrice='" + cbopriceWallType.SelectedValue.ToString() + "',UnitPriceNoTax='" + txtUnitPriceNotax.Text.ToString() + "' ,balheight='" + lblbalheight.Text.ToString() + "' where OrderAutoID='" + lblID.Text.ToString() + "'";


                //SqlCommand cmd6 = new SqlCommand(sql6, conn);
                //cmd6.ExecuteNonQuery();
                int flg = BusinessTier.SaveOrderForm(conn, txtno.Text.ToString(), txtdono.Text.ToString(), cboDoorType.Text.ToString(), orddt.ToString(), txtstatus.Text.ToString(), shipdt.ToString(), cboCustomerID.Text.ToString(), txtname.Text.ToString(), txttel.Text.ToString(), txtaddress.Text.ToString(), cboDelivery.Text.ToString(), txtremarks.Text.ToString(), cboman1.Text.ToString(), cboman2.Text.ToString(), cboman3.Text.ToString(), cboman4.Text.ToString(), cboman5.Text.ToString(), cboman6.Text.ToString(), txtpriceheight.Text.ToString(), txtpricewidth.Text.ToString(), cbopriceWallType.Text.ToString(), txtunit.Text.ToString(), txtUnitQuantity.Text.ToString(), txtBasePrice.Text.ToString(), txtUnitPrice.Text.ToString(), txtTotalAmount.Text.ToString(), txtpriceRemarks.Text.ToString(), textSpecialSpec.Text.ToString(), txtheightcal.Text.ToString(), txtwidthcal.Text.ToString(), txtwidthcal2.Text.ToString(), lblbalheight.Text.ToString(), cboColor1.Text.ToString(), cboColor2.Text.ToString(), cboColor3.Text.ToString(), cboColor4.Text.ToString(), cboColor5.Text.ToString(), cboColor6.Text.ToString(), CboColorheight1.Text.ToString(), CboColorheight2.Text.ToString(), CboColorheight3.Text.ToString(), CboColorheight4.Text.ToString(), CboColorheight5.Text.ToString(), CboColorheight6.Text.ToString(), txtColorQty1.Text.ToString(), txtColorQty2.Text.ToString(), txtColorQty3.Text.ToString(), txtColorQty4.Text.ToString(), txtColorQty5.Text.ToString(), txtColorQty6.Text.ToString(), cboVentilationHole.Text.ToString(), txtVentilationRows.Text.ToString(), cboLetterBox.Text.ToString(), cboControlBox.Text.ToString(), cboLockType.Text.ToString(), txtOtherLockTypes.Text.ToString(), cboVoltage.Text.ToString(), cboCurrent.Text.ToString(), cboNamePlates.Text.ToString(), cboMotor.Text.ToString(), cboManualOverride.Text.ToString(), cboRemoteBox.Text.ToString(), cboDoorOrientation.Text.ToString(), cboUPSBattery.Text.ToString(), cboPullHandle.Text.ToString(), cboPullHook.Text.ToString(), cboPacking.Text.ToString(), cboWarrantyDoor.Text.ToString(), cboWarrantyMotor.Text.ToString(), txtsplremarks.Text.ToString(), cboAluminiumBottomBar.Text.ToString(), cboNylonPolystrip.Text.ToString(), cbostaffid.Text.ToString(), txtStaffMail.Text.ToString(), cboTax.Text.ToString(), cbopriceWallType.SelectedValue.ToString(), txtUnitPriceNotax.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), Convert.ToInt32(lblID.Text.ToString()), "U", Session["Name"].ToString(), Session["Email"].ToString());
                lblStatus.Text = "OrderForm Info Updated Successfully But e-mail cannot be sent!";
                lblStatus.ForeColor = Color.Maroon;
            }

            else
            {
                rd21.Close();
                String orddt = txtdate.SelectedDate.ToString();
                DateTime pdt = DateTime.Parse(orddt);
                orddt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

                String shipdt = txtdeldate.SelectedDate.ToString();
                DateTime idt = DateTime.Parse(shipdt);
                shipdt = idt.Month + "/" + idt.Day + "/" + idt.Year;

                //  string sql6 = "INSERT INTO [SJClassic].[dbo].[OrderForm]([OrderNo],[SINo],[DoorType],[Date],[OrdStatus],[ShippingDate],[DealerID],[DealerName],[Tel],[Address],[DeliveryType],[Remark1],[Height],[Width],[Walltype],[Unit],[UnitQty],[BasePrice],[UnitPrice],[TotalAmt],[Remark2],[SplSpec],[HeightCal],[WidthCal],[Color1],[Color2],[Color3],[Color4],[Color5],[Color6],[Hgt1],[Hgt2],[Hgt3],[Hgt4],[Hgt5],[Hgt6],[Qty1],[Qty2],[Qty3],[Qty4],[Qty5],[Qty6],[Venthole],[VentRow],[Letterbox],[ControlBox],[LockType],[OtherLckTyp],[Voltage],[Currnt],[NamePlate],[Motor],[ManualOver],[RemoteBox],[DoorOrein],[UPSBattery],[PullHandle],[PullHook],[Packing],[WrntyDoor],[WrntyMotor],[SplRemark],[SendName],[SendMail],[createdby],[createddate],InstallMan1,InstallMan2,InstallMan3,InstallMan4,InstallMan5,InstallMan6,AluBottomBar,NylonPoly,Tax,TaxPrice,UnitPriceNoTax,balheight) VALUES ('" + txtno.Text.ToString() + "','" + txtdono.Text.ToString() + "','" + cboDoorType.Text.ToString() + "','" + orddt.ToString() + "','" + txtstatus.Text.ToString() + "','" + shipdt.ToString() + "','" + cboCustomerID.Text.ToString() + "','" + txtname.Text.ToString() + "','" + txttel.Text.ToString() + "','" + txtaddress.Text.ToString() + "','" + cboDelivery.Text.ToString() + "','" + txtremarks.Text.ToString() + "','" + txtpriceheight.Text.ToString() + "','" + txtpricewidth.Text.ToString() + "','" + cbopriceWallType.Text.ToString() + "','" + txtunit.Text.ToString() + "','" + txtUnitQuantity.Text.ToString() + "','" + txtBasePrice.Text.ToString() + "','" + txtUnitPrice.Text.ToString() + "','" + txtTotalAmount.Text.ToString() + "','" + txtpriceRemarks.Text.ToString() + "','" + textSpecialSpec.Text.ToString() + "','" + txtheightcal.Text.ToString() + "','" + txtwidthcal.Text.ToString() + "','" + cboColor1.Text.ToString() + "','" + cboColor2.Text.ToString() + "','" + cboColor3.Text.ToString() + "','" + cboColor4.Text.ToString() + "','" + cboColor5.Text.ToString() + "','" + cboColor6.Text.ToString() + "','" + CboColorheight1.Text.ToString() + "','" + CboColorheight2.Text.ToString() + "','" + CboColorheight3.Text.ToString() + "','" + CboColorheight4.Text.ToString() + "','" + CboColorheight5.Text.ToString() + "','" + CboColorheight6.Text.ToString() + "','" + txtColorQty1.Text.ToString() + "','" + txtColorQty2.Text.ToString() + "','" + txtColorQty3.Text.ToString() + "','" + txtColorQty4.Text.ToString() + "','" + txtColorQty5.Text.ToString() + "','" + txtColorQty6.Text.ToString() + "','" + cboVentilationHole.Text.ToString() + "','" + txtVentilationRows.Text.ToString() + "','" + cboLetterBox.Text.ToString() + "','" + cboControlBox.Text.ToString() + "','" + cboLockType.Text.ToString() + "','" + txtOtherLockTypes.Text.ToString() + "','" + cboVoltage.Text.ToString() + "','" + cboCurrent.Text.ToString() + "','" + cboNamePlates.Text.ToString() + "','" + cboMotor.Text.ToString() + "','" + cboManualOverride.Text.ToString() + "','" + cboRemoteBox.Text.ToString() + "','" + cboDoorOrientation.Text.ToString() + "','" + cboUPSBattery.Text.ToString() + "','" + cboPullHandle.Text.ToString() + "','" + cboPullHook.Text.ToString() + "','" + cboPacking.Text.ToString() + "','" + cboWarrantyDoor.Text.ToString() + "','" + cboWarrantyMotor.Text.ToString() + "','" + txtsplremarks.Text.ToString() + "','" + cbostaffid.Text.ToString() + "','" + txtStaffMail.Text.ToString() + "'," + Convert.ToInt32(Session["sesUserID"].ToString()) + ",'" + DateTime.Now.ToString() + "','" + cboman1.Text.ToString() + "','" + cboman2.Text.ToString() + "','" + cboman3.Text.ToString() + "','" + cboman4.Text.ToString() + "','" + cboman5.Text.ToString() + "','" + cboman6.Text.ToString() + "','" + cboAluminiumBottomBar.Text.ToString() + "','" + cboNylonPolystrip.Text.ToString() + "','" + cboTax.Text.ToString() + "','" + cbopriceWallType.SelectedValue.ToString() + "','" + txtUnitPriceNotax.Text.ToString() + "','" + lblbalheight.Text.ToString() + "')";
                //   string i = Session["Name"].ToString();
                //string j=Session["Email"].ToString();
                //,'" +  + "')";
                int flg = BusinessTier.SaveOrderForm(conn, txtno.Text.ToString(), txtdono.Text.ToString(), cboDoorType.Text.ToString(), orddt.ToString(), txtstatus.Text.ToString(), shipdt.ToString(), cboCustomerID.Text.ToString(), txtname.Text.ToString(), txttel.Text.ToString(), txtaddress.Text.ToString(), cboDelivery.Text.ToString(), txtremarks.Text.ToString(), cboman1.Text.ToString(), cboman2.Text.ToString(), cboman3.Text.ToString(), cboman4.Text.ToString(), cboman5.Text.ToString(), cboman6.Text.ToString(), txtpriceheight.Text.ToString(), txtpricewidth.Text.ToString(), cbopriceWallType.Text.ToString(), txtunit.Text.ToString(), txtUnitQuantity.Text.ToString(), txtBasePrice.Text.ToString(), txtUnitPrice.Text.ToString(), txtTotalAmount.Text.ToString(), txtpriceRemarks.Text.ToString(), textSpecialSpec.Text.ToString(), txtheightcal.Text.ToString(), txtwidthcal.Text.ToString(), txtwidthcal2.Text.ToString(), lblbalheight.Text.ToString(), cboColor1.Text.ToString(), cboColor2.Text.ToString(), cboColor3.Text.ToString(), cboColor4.Text.ToString(), cboColor5.Text.ToString(), cboColor6.Text.ToString(), CboColorheight1.Text.ToString(), CboColorheight2.Text.ToString(), CboColorheight3.Text.ToString(), CboColorheight4.Text.ToString(), CboColorheight5.Text.ToString(), CboColorheight6.Text.ToString(), txtColorQty1.Text.ToString(), txtColorQty2.Text.ToString(), txtColorQty3.Text.ToString(), txtColorQty4.Text.ToString(), txtColorQty5.Text.ToString(), txtColorQty6.Text.ToString(), cboVentilationHole.Text.ToString(), txtVentilationRows.Text.ToString(), cboLetterBox.Text.ToString(), cboControlBox.Text.ToString(), cboLockType.Text.ToString(), txtOtherLockTypes.Text.ToString(), cboVoltage.Text.ToString(), cboCurrent.Text.ToString(), cboNamePlates.Text.ToString(), cboMotor.Text.ToString(), cboManualOverride.Text.ToString(), cboRemoteBox.Text.ToString(), cboDoorOrientation.Text.ToString(), cboUPSBattery.Text.ToString(), cboPullHandle.Text.ToString(), cboPullHook.Text.ToString(), cboPacking.Text.ToString(), cboWarrantyDoor.Text.ToString(), cboWarrantyMotor.Text.ToString(), txtsplremarks.Text.ToString(), cboAluminiumBottomBar.Text.ToString(), cboNylonPolystrip.Text.ToString(), cbostaffid.Text.ToString(), txtStaffMail.Text.ToString(), cboTax.Text.ToString(), cbopriceWallType.SelectedValue.ToString(), txtUnitPriceNotax.Text.ToString(), Convert.ToInt32(Session["sesUserID"].ToString()), 0, "I", Session["Name"].ToString(), Session["Email"].ToString());

                //SqlCommand cmd6 = new SqlCommand(sql6, conn);
                //cmd6.ExecuteNonQuery();

                lblStatus.Text = "OrderForm Info Inserted Successfully But e-mail cannot be sent!";
                lblStatus.ForeColor = Color.Maroon;
                RadGrid1.Rebind();
            }
            //if (Session["Designation"].ToString() == "Clerk")
            //{
            //string sql1 = "update OrderForm set ClerkName='" + Session["Name"].ToString() + "',ClerkMail='" + Session["Email"].ToString() + "' where deleted=0 and OrderNo='" + txtno.Text.ToString().Trim() + "'";
            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
            //cmd1.ExecuteNonQuery();
            //}

            string sql20 = "select Designation from MasterStaff where deleted=0 and Email='" + txtStaffMail.Text.ToString() + "'";
            SqlCommand cmd20 = new SqlCommand(sql20, conn);
            SqlDataReader rd20 = cmd20.ExecuteReader();

            if (rd20.Read())
            {
                desig = rd20["Designation"].ToString();
            }
            rd20.Close();
            string msg = string.Empty;
            if (desig == "Manager")
            {
                msg = "Dear " + cbostaffid.Text.ToString() + ",\n\n" + "Please Confirm the Order No : " + txtno.Text.ToString() + ".\n\nby \n" + Session["Name"].ToString();
            }
            else
            {
                msg = "Dear " + cbostaffid.Text.ToString() + ",\n\n" + "Now you can enter the materials for Order No : " + txtno.Text.ToString() + ".\n\nby \n" + Session["Name"].ToString();
            }
            MailMessage message1 = new MailMessage();
            message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
            //message1.From = new MailAddress(Session["Email"].ToString().Trim());
            message1.To.Add(new MailAddress(txtStaffMail.Text.ToString().Trim()));
            message1.Subject = "OrderForm Request";
            message1.Body = msg;
            SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
            client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
            client1.DeliveryMethod = SmtpDeliveryMethod.Network;
            client1.EnableSsl = true;
            client1.Send(message1);
            lblStatus.Text = "OrderForm Info Inserted Successfully & Successfully e-mail sent!";
            lblStatus.ForeColor = Color.Green;

        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            // lblStatus.Text = ex.Message;
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Save/Update", ex.ToString(), "Audit");
        }


    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (Session["Name"].ToString().Trim() == "")
        {
            Response.Redirect("Login.aspx");
        }
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {
            if (lblID.Text != "")
            {
                int flg = BusinessTier.OrderFormUpdate(conn, chkApproval.Checked.ToString(), cbostaffid2.Text.ToString().Trim(), txtsendmail2.Text.ToString().Trim(), Session["sesUserID"].ToString(), lblID.Text.ToString());
                BusinessTier.DisposeConnection(conn);
                if (flg >= 1)
                {
                    lblStatus.Text = "OrderForm Info Updated Successfully But e-mail cannot be sent!";
                    lblStatus.ForeColor = Color.Maroon;
                }
                //ClearInputs(Page.Controls);
                RadGrid1.Rebind();
                MailMessage message1 = new MailMessage();
                message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
                //message1.From = new MailAddress(Session["Email"].ToString().Trim());
                message1.To.Add(new MailAddress(txtsendmail2.Text.ToString().Trim()));

                message1.Subject = "OrderForm Request";
                string msg = string.Empty;
                msg = "Dear " + cbostaffid2.Text.ToString() + ",\n\n" + "I Approved to this Order No : " + txtno.Text.ToString() + ". Now you can enter Materials.\n\nby, \n" + Session["Name"].ToString();
                message1.Body = msg;
                SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
                client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                client1.DeliveryMethod = SmtpDeliveryMethod.Network;
                client1.EnableSsl = true;
                client1.Send(message1);
                lblStatus.Text = "OrderForm Info Updated Successfully & Successfully e-mail sent!";
                lblStatus.ForeColor = Color.Green;
            }
            else
            {
                lblStatus.Text = "Please Select any Order number";
            }


        }
        catch (Exception ex)
        {
            //lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "Update", ex.ToString(), "Audit");
        }
    }

    protected void btnUpdateSpring_Click(object sender, EventArgs e)
    {
        if (Session["Name"].ToString().Trim() == "")
        {
            Response.Redirect("Login.aspx");
        }
        if (string.IsNullOrEmpty(cboDoorType.Text.ToString()))
        {
            lblStatus.Text = "Please Click any OrderNo in Data Grid";
            return;
        }
        if (string.IsNullOrEmpty(txtPulleypcs.Text.ToString()))
        {
            lblStatus.Text = "Enter Pulley pcs";
            return;
        }
        if (string.IsNullOrEmpty(txtSpring1.Text.ToString()))
        {
            lblStatus.Text = "Select Spring 1 and Enter Spring 1 pcs";
            return;
        }

        if (string.IsNullOrEmpty(txtsendmail3.Text.ToString()))
        {
            lblStatus.Text = "Select Send Mail";
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            if (lblID.Text != "")
            {
                int Pulleyqty = 0, Sp1 = 0, Sp2 = 0, Sp3 = 0, Sp4 = 0, Sp5 = 0, Sp6 = 0, Pull = 0;

                string sql20 = "select * from OrderForm where deleted=0 and OrderAutoID='" + lblID.Text.ToString() + "'";
                SqlCommand cmd20 = new SqlCommand(sql20, conn);
                SqlDataReader rd20 = cmd20.ExecuteReader();

                if (rd20.Read())
                {
                    string sprg1 = rd20["Spring1pcs"].ToString();
                    string sprg2 = rd20["Spring2pcs"].ToString();
                    string sprg3 = rd20["Spring3pcs"].ToString();
                    string sprg4 = rd20["Spring4pcs"].ToString();
                    string sprg5 = rd20["Spring5pcs"].ToString();
                    string sprg6 = rd20["Spring6pcs"].ToString();

                    string sprg1nm = rd20["Spring1"].ToString();
                    string sprg2nm = rd20["Spring2"].ToString();
                    string sprg3nm = rd20["Spring3"].ToString();
                    string sprg4nm = rd20["Spring4"].ToString();
                    string sprg5nm = rd20["Spring5"].ToString();
                    string sprg6nm = rd20["Spring6"].ToString();

                    string pulys = rd20["Pulley"].ToString();

                    rd20.Close();

                    if (sprg1 == "")
                    {
                        Sp1 = 0;
                    }
                    else
                    {
                        Sp1 = Convert.ToInt32(sprg1);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg1nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp1);
                            rd.Close();
                            //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg1nm.ToString().Trim() + "'";
                            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            //cmd1.ExecuteNonQuery();
                            int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), sprg1nm.ToString().Trim(), Session["sesUserID"].ToString());
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg2 == "")
                    {
                        Sp2 = 0;
                    }
                    else
                    {
                        Sp2 = Convert.ToInt32(sprg2);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg2nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp2);
                            rd.Close();
                            //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg2nm.ToString().Trim() + "'";
                            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            //cmd1.ExecuteNonQuery();
                            int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), sprg2nm.ToString().Trim(), Session["sesUserID"].ToString());
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg3 == "")
                    {
                        Sp3 = 0;
                    }
                    else
                    {
                        Sp3 = Convert.ToInt32(sprg3);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg3nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp3);
                            rd.Close();
                            //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg3nm.ToString().Trim() + "'";
                            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            //cmd1.ExecuteNonQuery();
                            int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), sprg3nm.ToString().Trim(), Session["sesUserID"].ToString());
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg4 == "")
                    {
                        Sp4 = 0;
                    }
                    else
                    {
                        Sp4 = Convert.ToInt32(sprg4);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg4nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp4);
                            rd.Close();
                            //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg4nm.ToString().Trim() + "'";
                            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            //cmd1.ExecuteNonQuery();
                            int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), sprg4nm.ToString().Trim(), Session["sesUserID"].ToString());
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg5 == "")
                    {
                        Sp5 = 0;
                    }
                    else
                    {
                        Sp5 = Convert.ToInt32(sprg5);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg5nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp5);
                            rd.Close();
                            //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg5nm.ToString().Trim() + "'";
                            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            //cmd1.ExecuteNonQuery();
                            int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), sprg5nm.ToString().Trim(), Session["sesUserID"].ToString());
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (sprg6 == "")
                    {
                        Sp6 = 0;
                    }
                    else
                    {
                        Sp6 = Convert.ToInt32(sprg6);
                        string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + sprg6nm.ToString().Trim() + "'";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        SqlDataReader rd = cmd.ExecuteReader();

                        if (rd.Read())
                        {
                            int qty = (Convert.ToInt32(rd["Quantity"].ToString()) + Sp6);
                            rd.Close();
                            //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + sprg6nm.ToString().Trim() + "'";
                            //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            //cmd1.ExecuteNonQuery();
                            int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), sprg6nm.ToString().Trim(), Session["sesUserID"].ToString());
                        }
                        else
                        {
                            rd.Close();
                        }
                    }

                    if (pulys == "")
                    {
                        Pull = 0;
                    }
                    else
                    {
                        Pull = Convert.ToInt32(pulys);
                    }



                }
                else
                {
                    rd20.Close();
                }


                //--------------------------Pulley Update---------------------
                if (txtPulleypcs.Text.ToString() != "")
                {
                    string sql2 = "select Quantity from MaterialMaster where deleted=0 and materialtype='Pulley'";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    SqlDataReader rd2 = cmd2.ExecuteReader();

                    if (rd2.Read())
                    {
                        Pulleyqty = Convert.ToInt32(rd2["Quantity"].ToString());
                        int qty = (Pulleyqty + Pull) - Convert.ToInt32(txtPulleypcs.Text.ToString());
                        rd2.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialtype='Pulley'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), "Pulley", Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd2.Close();
                    }
                }
                //--------------------------Spring Update---------------------

                //--------------------------Spring1---------
                if (txtSpring1.Text.ToString() != "")
                {
                    string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring1.Text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        int qty = Convert.ToInt32(rd["Quantity"].ToString()) - Convert.ToInt32(txtSpring1.Text.ToString());
                        rd.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + cboSpring1.Text.ToString() + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), cboSpring1.Text.ToString().Trim(), Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd.Close();
                    }
                }
                //--------------------------Spring2------------
                if (txtSpring2.Text.ToString() != "")
                {
                    string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring2.Text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        int qty = Convert.ToInt32(rd["Quantity"].ToString()) - Convert.ToInt32(txtSpring2.Text.ToString());
                        rd.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + cboSpring2.Text.ToString() + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), cboSpring2.Text.ToString().Trim(), Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd.Close();
                    }
                }
                //--------------------------Spring3------------
                if (txtSpring3.Text.ToString() != "")
                {
                    string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring3.Text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        int qty = Convert.ToInt32(rd["Quantity"].ToString()) - Convert.ToInt32(txtSpring3.Text.ToString());
                        rd.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + cboSpring3.Text.ToString() + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), cboSpring3.Text.ToString().Trim(), Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd.Close();
                    }
                }
                //--------------------------Spring4------------
                if (txtSpring4.Text.ToString() != "")
                {
                    string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring4.Text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        int qty = Convert.ToInt32(rd["Quantity"].ToString()) - Convert.ToInt32(txtSpring4.Text.ToString());
                        rd.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + cboSpring4.Text.ToString() + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), cboSpring4.Text.ToString().Trim(), Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd.Close();
                    }
                }

                //--------------------------Spring5---------
                if (txtSpring5.Text.ToString() != "")
                {
                    string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring5.Text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        int qty = Convert.ToInt32(rd["Quantity"].ToString()) - Convert.ToInt32(txtSpring5.Text.ToString());
                        rd.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + cboSpring5.Text.ToString() + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), cboSpring5.Text.ToString().Trim(), Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd.Close();
                    }
                }

                //--------------------------Spring6---------
                if (txtSpring6.Text.ToString() != "")
                {
                    string sql = "select Quantity from MaterialMaster where deleted=0 and materialname='" + cboSpring6.Text.ToString() + "'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rd = cmd.ExecuteReader();

                    if (rd.Read())
                    {
                        int qty = Convert.ToInt32(rd["Quantity"].ToString()) - Convert.ToInt32(txtSpring6.Text.ToString());
                        rd.Close();
                        //string sql1 = "update MaterialMaster set Quantity='" + qty.ToString() + "' where deleted=0 and materialname='" + cboSpring6.Text.ToString() + "'";
                        //SqlCommand cmd1 = new SqlCommand(sql1, conn);
                        //cmd1.ExecuteNonQuery();
                        int flg = BusinessTier.SpringUpdate(conn, qty.ToString(), cboSpring6.Text.ToString().Trim(), Session["sesUserID"].ToString());
                    }
                    else
                    {
                        rd.Close();
                    }
                }
                //string sql6 = "Update [SJClassic].[dbo].[OrderForm] set [Pulley]='" + txtPulleypcs.Text.ToString() + "',[Spring1]='" + cboSpring1.Text.ToString() + "',[Spring1pcs]='" + txtSpring1.Text.ToString() + "',[Spring2]='" + cboSpring2.Text.ToString() + "',[Spring2pcs]='" + txtSpring2.Text.ToString() + "',[Spring3]='" + cboSpring3.Text.ToString() + "',[Spring3pcs]='" + txtSpring3.Text.ToString() + "',[Spring4]='" + cboSpring4.Text.ToString() + "',[Spring4pcs]='" + txtSpring4.Text.ToString() + "',[Spring5]='" + cboSpring5.Text.ToString() + "',[Spring5pcs]='" + txtSpring5.Text.ToString() + "',[Spring6]='" + cboSpring6.Text.ToString() + "',[Spring6pcs]='" + txtSpring6.Text.ToString() + "',[SendName3]='" + cbostaffid3.Text.ToString() + "',[SendMail3]='" + txtsendmail3.Text.ToString() + "',modifiedby='" + Session["sesUserID"].ToString() + "',modifydate='" + DateTime.Now.ToString() + "' where OrderAutoID='" + lblID.Text.ToString() + "'";
                BusinessTier.OrderFormSpringUpdate(conn, txtPulleypcs.Text.ToString(), cboSpring1.Text.ToString(), txtSpring1.Text.ToString(), cboSpring2.Text.ToString(), txtSpring2.Text.ToString(), cboSpring3.Text.ToString(), txtSpring3.Text.ToString(), cboSpring4.Text.ToString(), txtSpring4.Text.ToString(), cboSpring5.Text.ToString(), txtSpring5.Text.ToString(), cboSpring6.Text.ToString(), txtSpring6.Text.ToString(), cbostaffid3.Text.ToString(), txtsendmail3.Text.ToString(), Convert.ToInt32(lblID.Text.ToString()), Session["sesUserID"].ToString());
                //SqlCommand cmd6 = new SqlCommand(sql6, conn);
                //cmd6.ExecuteNonQuery();
                lblStatus.Text = "OrderForm Info Updated Successfully But e-mail cannot be sent!";
                lblStatus.ForeColor = Color.Maroon;

                RadGrid1.Rebind();

                MailMessage message1 = new MailMessage();
                message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
                //message1.From = new MailAddress(Session["Email"].ToString().Trim());
                message1.To.Add(new MailAddress(txtsendmail3.Text.ToString().Trim()));

                message1.Subject = "OrderForm Request";
                string msg = string.Empty;
                msg = "Dear " + cbostaffid3.Text.ToString() + ",\n\n" + "I entered material details for this Order No : " + txtno.Text.ToString() + ". Now you do final inspection.\n\nby, \n" + Session["Name"].ToString();
                message1.Body = msg;
                SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
                client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                client1.DeliveryMethod = SmtpDeliveryMethod.Network;
                client1.EnableSsl = true;
                client1.Send(message1);
                lblStatus.Text = "OrderForm Info Updated Successfully & Successfully e-mail sent!";
                lblStatus.ForeColor = Color.Green;
                ClearInputs(Page.Controls);
                Cbo_Spring_load();
            }

            else
            {
                // lblStatus.Text = "Please Select any Order number";
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "UpdateSpring", ex.ToString(), "Audit");
        }

    }

    protected void btnclear_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.PathAndQuery, true);
        //ClearInputs(Page.Controls);
    }

    void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            if (ctrl is RadComboBox)
                ((RadComboBox)ctrl).Text = string.Empty;
            if (ctrl is RadComboBox)
                ((RadComboBox)ctrl).ClearSelection();
            ClearInputs(ctrl.Controls);
        }
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }
}