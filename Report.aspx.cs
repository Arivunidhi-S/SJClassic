using System;
using System.Collections.Generic;
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
using Stimulsoft.Report;
using Stimulsoft.Report.Web;
using Stimulsoft.Report.Viewer;
using Stimulsoft.Report.SaveLoad;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Print;
using Stimulsoft.Base;
using Stimulsoft.Controls;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report.Controls;
using System.IO;
using System.Web.SessionState;
using System.Runtime;
using System.Drawing.Printing;

public partial class Report : System.Web.UI.Page
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
                //lblStatus.Text = "";
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
            lblMaterialNo.Visible = false;
            cboOrderID.Visible = false;
            lblOrderid.Visible = false;
            dtStartDate.Visible = false;
            dtEndDate.Visible = false;
            lblDateStart.Visible = false;
            lblDateEnd.Visible = false;
            StiWebViewer1.Visible = false;
            cboMatreialNo.Visible = false;
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

    protected void cboOrderID_Select_Change(object sender, EventArgs e)
    {
        StiWebViewer1.Visible = false;
        lblStatus.Text = "";
        if (cboReports.SelectedItem.Text == "OrderForm" || cboReports.SelectedItem.Text == "Final Inspection" || cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details" || cboReports.SelectedItem.Text == "Sales Report")
        {
            dtStartDate.Clear();
            dtEndDate.Clear();
        }
    }

    protected void dtStartDate_Select_Change(object sender, EventArgs e)
    {
        StiWebViewer1.Visible = false;
        lblStatus.Text = "";
        if (cboReports.SelectedItem.Text == "OrderForm" || cboReports.SelectedItem.Text == "Final Inspection" || cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details" || cboReports.SelectedItem.Text == "Sales Report")
        {
            cboOrderID.ClearSelection();
            cboOrderID.Text = string.Empty;
        }
    }

    protected void cboReports_Select_Change(object sender, EventArgs e)
    {
        lblStatus.Text = "";
        cboOrderID.ClearSelection();
        cboOrderID.Text = string.Empty;
        cboMatreialNo.ClearSelection();
        cboMatreialNo.Text = string.Empty;
        dtStartDate.Clear();
        dtEndDate.Clear();
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        StiWebViewer1.Visible = false;
        if (cboReports.SelectedItem.Text == "OrderForm" || cboReports.SelectedItem.Text == "Sales Report")
        {
            if (cboReports.SelectedItem.Text == "OrderForm")
            {
                cboOrderID.Visible = true;
                lblOrderid.Visible = true;
            }
            else 
            {
                cboOrderID.Visible = false;
                lblOrderid.Visible = false;
            }
            dtStartDate.Visible = true;
            dtEndDate.Visible = true;
            lblDateStart.Visible = true;
            lblDateEnd.Visible = true;
            cboMatreialNo.Visible = false;
            lblMaterialNo.Visible = false;
            cboOrderID.Items.Clear();
            string sql2 = "select * from OrderForm where Deleted=0 ORDER BY [OrderAutoID] desc ";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            while (rd2.Read())
            {
                RadComboBoxItem item1 = new RadComboBoxItem();
                item1.Text = rd2["orderno"].ToString();
                cboOrderID.Items.Add(item1);
            }
            BusinessTier.DisposeReader(rd2);
        }
        else if (cboReports.SelectedItem.Text == "Final Inspection")
        {
            cboOrderID.Visible = true;
            lblOrderid.Visible = true;
            dtStartDate.Visible = true;
            dtEndDate.Visible = true;
            lblDateStart.Visible = true;
            lblDateEnd.Visible = true;
            cboMatreialNo.Visible = false;
            lblMaterialNo.Visible = false;
            cboOrderID.Items.Clear();
            string sql2 = "select * from VW_FinalInspection ORDER BY [OrderAutoID] desc";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            while (rd2.Read())
            {
                RadComboBoxItem item1 = new RadComboBoxItem();
                item1.Text = rd2["orderno"].ToString();
                cboOrderID.Items.Add(item1);
            }
            BusinessTier.DisposeReader(rd2);
        }
        if (cboReports.SelectedItem.Text == "Customer Details")
        {
            cboOrderID.Visible = false;
            lblOrderid.Visible = false;
            dtStartDate.Visible = false;
            dtEndDate.Visible = false;
            lblDateStart.Visible = false;
            lblDateEnd.Visible = false;
            cboMatreialNo.Visible = false;
            lblMaterialNo.Visible = false;
        }
        if (cboReports.SelectedItem.Text == "Material Details")
        {
            cboOrderID.Visible = false;
            lblOrderid.Visible = false;
            dtStartDate.Visible = false;
            dtEndDate.Visible = false;
            lblDateStart.Visible = false;
            lblDateEnd.Visible = false;
            cboMatreialNo.Visible = false;
            lblMaterialNo.Visible = false;

        }

        if (cboReports.SelectedItem.Text == "Staff Details")
        {
            cboOrderID.Visible = false;
            lblOrderid.Visible = false;
            dtStartDate.Visible = false;
            dtEndDate.Visible = false;
            lblDateStart.Visible = false;
            lblDateEnd.Visible = false;
            cboMatreialNo.Visible = false;
            lblMaterialNo.Visible = false;
        }

        if (cboReports.SelectedItem.Text == "Incoming Material")
        {
            lblOrderid.Visible = false;
            cboOrderID.Visible = false;
            lblMaterialNo.Visible = true;
            cboMatreialNo.Visible = true;
            dtStartDate.Visible = true;
            dtEndDate.Visible = true;
            lblDateStart.Visible = true;
            lblDateEnd.Visible = true;

            string sql1 = "select distinct(Materialcode),MaterialName FROM IncomingMaterial WHERE Deleted=0";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sql1, conn);
            DataTable dataTable1 = new DataTable();
            adapter1.Fill(dataTable1);
            cboMatreialNo.Items.Clear();
            foreach (DataRow row in dataTable1.Rows)
            {
                RadComboBoxItem item = new RadComboBoxItem();
                item.Text = row["Materialcode"].ToString();
                item.Attributes.Add("Materialcode", row["Materialcode"].ToString());
                item.Attributes.Add("MaterialName", row["MaterialName"].ToString());

                cboMatreialNo.Items.Add(item);
                item.DataBind();
            }

        }
        if (cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details")
        {
            lblMaterialNo.Visible = false;
            cboMatreialNo.Visible = false;
            cboOrderID.Visible = true;
            lblOrderid.Visible = true;
            dtStartDate.Visible = true;
            dtEndDate.Visible = true;
            lblDateStart.Visible = true;
            lblDateEnd.Visible = true;
            cboOrderID.Text = string.Empty;
            cboOrderID.Items.Clear();
            string sql2 = "select * from OrderForm where Deleted=0 ORDER BY [OrderAutoID] desc";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            SqlDataReader rd2 = cmd2.ExecuteReader();
            while (rd2.Read())
            {
                RadComboBoxItem item1 = new RadComboBoxItem();
                item1.Text = rd2["orderno"].ToString();
                cboOrderID.Items.Add(item1);
            }
            BusinessTier.DisposeReader(rd2);
        }
        BusinessTier.DisposeConnection(conn);
    }

    protected void btn_Report_Submit_Click(object sender, EventArgs e)
    {

        Stimulsoft.Report.StiReport stiReport1;
        string path = string.Empty;
        lblStatus.Text = string.Empty;
        if ((string.IsNullOrEmpty(cboReports.Text.ToString())))
        {
            lblStatus.Text = "Select Reports";
            return;
        }

        string con = BusinessTier.getConnection1();
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {

            string sqldatasource4 = string.Empty;
            DataSet ds4 = new DataSet();
            string sqldatasource5 = string.Empty;
            DataSet ds5 = new DataSet();

            if (cboReports.SelectedItem.Text == "OrderForm")
            {

                sqldatasource4 = "OrderForm";
                string sql4 = string.Empty;
                if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
                {
                    if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select Start Date";
                        return;
                    }
                    if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select End Date";
                        return;
                    }

                    String stdt = dtStartDate.SelectedDate.ToString();
                    DateTime pdt = DateTime.Parse(stdt);
                    stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

                    String enddt = dtEndDate.SelectedDate.ToString();
                    DateTime idt = DateTime.Parse(enddt);
                    enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

                    sql4 = "select *,CONVERT(VARCHAR(10), Date, 103) AS [Todate],CONVERT(VARCHAR(10), ShippingDate, 103) AS [InstallDate] from OrderForm where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
                }

                else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), Date, 103) AS [Todate],CONVERT(VARCHAR(10), ShippingDate, 103) AS [InstallDate] from OrderForm where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
                }
                else
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), Date, 103) AS [Todate],CONVERT(VARCHAR(10), ShippingDate, 103) AS [InstallDate] from OrderForm where deleted=0";
                }


                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\OrderForm.mrt";
                
            }

            if (cboReports.SelectedItem.Text == "Final Inspection")
            {

                sqldatasource4 = "VW_FinalInspection";
                string sql4 = string.Empty;
                if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
                {
                    if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select Start Date";
                        return;
                    }
                    if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select End Date";
                        return;
                    }

                    String stdt = dtStartDate.SelectedDate.ToString();
                    DateTime pdt = DateTime.Parse(stdt);
                    stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

                    String enddt = dtEndDate.SelectedDate.ToString();
                    DateTime idt = DateTime.Parse(enddt);
                    enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

                    sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from VW_FinalInspection where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
                }

                else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from VW_FinalInspection where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
                }
                else
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from VW_FinalInspection where deleted=0";
                }

                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Final Inspection.mrt";
               
            }
            if (cboReports.SelectedItem.Text == "Customer Details")
            {
              
                cboOrderID.Visible = false;
                lblOrderid.Visible = false;
                sqldatasource4 = "Customer";
                string sql4 = "select * from Customer where Deleted=0";
                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Customer.mrt";
            }

            if (cboReports.SelectedItem.Text == "Material Details")
            {
             
                sqldatasource4 = "MaterialMaster";
                string sql4 = "select *,CONVERT(VARCHAR(10), MnfDate, 103) as ShippingDate from MaterialMaster where Deleted=0";
                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Material.mrt";
            }

            if (cboReports.SelectedItem.Text == "Incoming Material")
            {
              
                sqldatasource4 = "IncomingMaterial";
                string sql4 = string.Empty;
                if (string.IsNullOrEmpty(cboMatreialNo.Text.ToString()))
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), StockDate, 103) as ShippingDate from IncomingMaterial where Deleted=0";
                }
                else if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
                {

                    String stdt = dtStartDate.SelectedDate.ToString();
                    DateTime pdt = DateTime.Parse(stdt);
                    stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

                    String enddt = dtEndDate.SelectedDate.ToString();
                    DateTime idt = DateTime.Parse(enddt);
                    enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

                    sql4 = "select *,CONVERT(VARCHAR(10), StockDate, 103) as ShippingDate from IncomingMaterial where Deleted=0 and Materialcode='" + cboMatreialNo.Text.ToString() + "' and StockDate between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
                }

                else if (!(string.IsNullOrEmpty(cboMatreialNo.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), StockDate, 103) as ShippingDate from IncomingMaterial where Deleted=0 and Materialcode='" + cboMatreialNo.Text.ToString() + "'";
                }

                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Incoming Material.mrt";
            }

            if (cboReports.SelectedItem.Text == "Staff Details")
            {
               
                sqldatasource4 = "MasterStaff";
                string sql4 = "select * from MasterStaff where Deleted=0";
                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Staff.mrt";
            }

            if (cboReports.SelectedItem.Text == "Warranty Sticker" || cboReports.SelectedItem.Text == "Spring Details")
            {

                string sql4 = string.Empty;
                sqldatasource4 = "OrderForm";
                if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
                {
                    if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select Start Date";
                        return;
                    }
                    if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select End Date";
                        return;
                    }

                    String stdt = dtStartDate.SelectedDate.ToString();
                    DateTime pdt = DateTime.Parse(stdt);
                    stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

                    String enddt = dtEndDate.SelectedDate.ToString();
                    DateTime idt = DateTime.Parse(enddt);
                    enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

                    sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from OrderForm where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
                }

                else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from OrderForm where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
                }
                else
                {
                    sql4 = "select *,CONVERT(VARCHAR(10), ShippingDate, 103) as InstallDate from OrderForm where deleted=0";
                }



                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds4.Tables.Add(sqldatasource4);
                ad4.Fill(ds4, sqldatasource4);
                if (cboReports.SelectedItem.Text == "Warranty Sticker")
                {
                    path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Warranty.mrt";
                }
                else
                {
                    path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\SpringDetails.mrt";
                }
            }

            if (cboReports.SelectedItem.Text == "Sales Report")
            {

                string sql4 = string.Empty, sql5 = string.Empty;
                sqldatasource4 = "OrderForm";
                sqldatasource5 = "DataSource1";
                if (!(string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (!(string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))))
                {
                    if (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select Start Date";
                        return;
                    }
                    if (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString()))
                    {
                        lblStatus.Text = "Select End Date";
                        return;
                    }

                    String stdt = dtStartDate.SelectedDate.ToString();
                    DateTime pdt = DateTime.Parse(stdt);
                    stdt = pdt.Month + "/" + pdt.Day + "/" + pdt.Year;

                    String enddt = dtEndDate.SelectedDate.ToString();
                    DateTime idt = DateTime.Parse(enddt);
                    enddt = idt.Month + "/" + idt.Day + "/" + idt.Year;

                    sql4 = "select * from OrderForm where Deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
                    sql5 = "SELECT SUM(CAST(unitqty as int)) AS Qty,SUM(CAST([TotalAmt] as int)) AS TotalAmount FROM [SJClassic].[dbo].[OrderForm] where deleted=0 and Date between '" + stdt.ToString() + "' and '" + enddt.ToString() + "'";
                }

                else if (!(string.IsNullOrEmpty(cboOrderID.Text.ToString())) || (string.IsNullOrEmpty(dtStartDate.SelectedDate.ToString())) || (string.IsNullOrEmpty(dtEndDate.SelectedDate.ToString())))
                {
                    sql4 = "select * from OrderForm where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
                    sql5 = "SELECT SUM(CAST(unitqty as int)) AS Qty,SUM(CAST([TotalAmt] as int)) AS TotalAmount FROM [SJClassic].[dbo].[OrderForm] where deleted=0 and orderno='" + cboOrderID.SelectedItem.Text.ToString().Trim() + "' and  Deleted=0";
                }
                else
                {
                    sql4 = "select * from OrderForm where deleted=0";
                    sql5 = "SELECT SUM(CAST(unitqty as int)) AS Qty,SUM(CAST([TotalAmt] as int)) AS TotalAmount FROM [SJClassic].[dbo].[OrderForm] where deleted=0";
                }


                SqlDataAdapter ad4 = new SqlDataAdapter(sql4, con);
                SqlDataAdapter ad5 = new SqlDataAdapter(sql5, con);

                ds4.DataSetName = "DynamicDataSource1";
                ds5.DataSetName = "DynamicDataSource1";

                ds4.Tables.Add(sqldatasource4);
                ds5.Tables.Add(sqldatasource5);

                ad4.Fill(ds4, sqldatasource4);
                ad5.Fill(ds5, sqldatasource5);

                path = "C:\\inetpub\\wwwroot\\SJClassic\\Reports\\Sales Report.mrt";
                
            }
            stiReport1 = new StiReport();
            stiReport1.Dictionary.DataStore.Clear();
            stiReport1.ClearAllStates();
            stiReport1.Load(path);
            stiReport1.Dictionary.Databases.Clear();
            stiReport1.Dictionary.Databases.Add(new StiSqlDatabase("Connection", con));
            stiReport1.Dictionary.DataSources.Clear();

            stiReport1.RegData(sqldatasource4, ds4);
            if (cboReports.SelectedItem.Text == "Sales Report")
            {
                stiReport1.RegData(sqldatasource5, ds5);
            }
            stiReport1.Dictionary.Synchronize();
            stiReport1.Compile();
            stiReport1.Render();
            StiWebViewer1.Report = stiReport1;
            StiWebViewer1.ViewMode = StiWebViewMode.WholeReport;
            //stiReport1.Print();
            stiReport1.Dispose();
            StiWebViewer1.Visible = true;
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            StiWebViewer1.Visible = false;
        }
    }

}