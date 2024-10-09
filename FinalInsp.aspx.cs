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

using System.Net;
using System.Net.Mail;
using System.Threading;
using System.IO;
using System.Text;


public partial class FinalInsp : System.Web.UI.Page
{
    public DataTable dtMenuItems = new DataTable();

    public DataTable dtSubMenuItems = new DataTable();

    public static string flag = string.Empty;

    public static string MName = string.Empty;
    public static string MMail = string.Empty;
    public static string CName = string.Empty;
    public static string CMail = string.Empty;

    public static int totalqty = 0;

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
                // lblStatus.Text = "";
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
            cboOrderNo_Load();
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }

    }

    protected void cboOrderNo_Load()
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {
            string sql2 = "select * from OrderForm where deleted=0 ORDER BY [OrderAutoID] desc ";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql2, conn);
            DataTable dataTable2 = new DataTable();
            adapter2.Fill(dataTable2);
            cboOrderNo.Items.Clear();
            foreach (DataRow row in dataTable2.Rows)
            {
                RadComboBoxItem item1 = new RadComboBoxItem();
                item1.Text = row["OrderNo"].ToString();
                item1.Value = row["OrderAutoID"].ToString();
                item1.Attributes.Add("OrderNo", row["OrderNo"].ToString());
                item1.Attributes.Add("DealerName", row["DealerName"].ToString());
                //item1.Attributes.Add("DealerName", row["DealerName"].ToString());
                cboOrderNo.Items.Add(item1);
                item1.DataBind();
            }


            BusinessTier.DisposeConnection(conn);
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "FinalInspection", "cboOrderNo_load", ex.ToString(), "Audit");
        }
    }

    protected void cboOrderNo_SelectedChanged(object sender, EventArgs e)
    {
        lblStatus.Text = string.Empty;
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        try
        {

            string sql = "Select * from VW_FinalInspection where OrderAutoID='" + cboOrderNo.SelectedValue.ToString() + "' and deleted=0";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                //ClearInputs();
                flag = "U";
                txtColor1.Text = (rd["Color1"].ToString());
                txtColor2.Text = (rd["Color2"].ToString());
                txtColor3.Text = (rd["Color3"].ToString());
                txtColor4.Text = (rd["Color4"].ToString());
                txtColor5.Text = (rd["Color5"].ToString());
                txtColor6.Text = (rd["Color6"].ToString());

                txtheight1.Text = (rd["Hgt1"].ToString());
                txtheight2.Text = (rd["Hgt2"].ToString());
                txtheight3.Text = (rd["Hgt3"].ToString());
                txtheight4.Text = (rd["Hgt4"].ToString());
                txtheight5.Text = (rd["Hgt5"].ToString());
                txtheight6.Text = (rd["Hgt6"].ToString());

                txtColorQty1.Text = (rd["Qty1"].ToString());
                txtColorQty2.Text = (rd["Qty2"].ToString());
                txtColorQty3.Text = (rd["Qty3"].ToString());
                txtColorQty4.Text = (rd["Qty4"].ToString());
                txtColorQty5.Text = (rd["Qty5"].ToString());
                txtColorQty6.Text = (rd["Qty6"].ToString());

                int totalqty = 0, qty1 = 0, qty2 = 0, qty3 = 0, qty4 = 0, qty5 = 0, qty6 = 0;
                //-------------------Calculation Quantity1--------------------//
                if (rd["Qty1"].ToString() == "")
                {
                    qty1 = 0;
                }
                else
                {
                    qty1 = Convert.ToInt32(rd["Qty1"].ToString());
                }
                //-------------------Calculation Quantity2--------------------//
                if (rd["Qty2"].ToString() == "")
                {
                    qty2 = 0;
                }
                else
                {
                    qty2 = Convert.ToInt32(rd["Qty2"].ToString());
                }
                //-------------------Calculation Quantity3--------------------//
                if (rd["Qty3"].ToString() == "")
                {
                    qty3 = 0;
                }
                else
                {
                    qty3 = Convert.ToInt32(rd["Qty3"].ToString());
                }
                //-------------------Calculation Quantity4--------------------//
                if (rd["Qty4"].ToString() == "")
                {
                    qty4 = 0;
                }
                else
                {
                    qty4 = Convert.ToInt32(rd["Qty4"].ToString());
                }
                //-------------------Calculation Quantity5--------------------//
                if (rd["Qty5"].ToString() == "")
                {
                    qty5 = 0;
                }
                else
                {
                    qty5 = Convert.ToInt32(rd["Qty5"].ToString());
                }
                //-------------------Calculation Quantity6--------------------//
                if (rd["Qty6"].ToString() == "")
                {
                    qty6 = 0;
                }
                else
                {
                    qty6 = Convert.ToInt32(rd["Qty6"].ToString());
                }

                totalqty = qty1 + qty2 + qty3 + qty4 + qty5 + qty6;

                txtAssem2DoorBoard.Text = (totalqty.ToString());

                txtAluminiumBottomBar.Text = (rd["AluBottomBar"].ToString());
                txtNylonPolystrip.Text = (rd["NylonPoly"].ToString());

                txtQCWidth.Text = (rd["WidthCal"].ToString());
                txtQCHeight.Text = (rd["HeightCal"].ToString());

                dtpInstallDate.DbSelectedDate = rd["ShippingDate"].ToString();
                txtInstallMan1.Text = (rd["InstallMan1"].ToString());
                txtInstallMan2.Text = (rd["InstallMan2"].ToString());
                txtInstallMan3.Text = (rd["InstallMan3"].ToString());
                txtInstallMan4.Text = (rd["InstallMan4"].ToString());
                txtInstallMan5.Text = (rd["InstallMan5"].ToString());
                txtInstallMan6.Text = (rd["InstallMan6"].ToString());

                txtPunchVenthole.Text = (rd["Venthole"].ToString());
                txtPunchVentRow.Text = (rd["VentRow"].ToString());
                txtPunchLetterbox.Text = (rd["Letterbox"].ToString());
                txtPunchLock.Text = (rd["LockType"].ToString());
                txtOtherLock.Text = (rd["OtherLckTyp"].ToString());
                txtDoorOrentation.Text = (rd["DoorOrein"].ToString());

                txtass1Letterbox.Text = (rd["Letterbox"].ToString());
                txtass1Lock.Text = (rd["LockType"].ToString());
                txtass1otherlock.Text = (rd["OtherLckTyp"].ToString());

                txtPulleypcs.Text = (rd["Pulley"].ToString());
                txtSpring1.Text = (rd["Spring1"].ToString());
                txtSpring2.Text = (rd["Spring2"].ToString());
                txtSpring3.Text = (rd["Spring3"].ToString());
                txtSpring4.Text = (rd["Spring4"].ToString());
                txtMotor.Text = (rd["Motor"].ToString());

                txtSpring1pcs.Text = (rd["Spring1pcs"].ToString());
                txtSpring2pcs.Text = (rd["Spring2pcs"].ToString());
                txtSpring3pcs.Text = (rd["Spring3pcs"].ToString());
                txtSpring4pcs.Text = (rd["Spring4pcs"].ToString());

                txtControlBox.Text = (rd["ControlBox"].ToString());
                txtVoltage.Text = (rd["Voltage"].ToString());
                txtCurrent.Text = (rd["Currnt"].ToString());
                txtManualOverride.Text = (rd["ManualOver"].ToString());
                txtRemoteBox.Text = (rd["RemoteBox"].ToString());
                txtUPSBattery.Text = (rd["UPSBattery"].ToString());

                MName = (rd["SendName"].ToString());
                MMail = (rd["SendMail"].ToString());
                CName = (rd["ClerkName"].ToString());
                CMail = (rd["ClerkMail"].ToString());

                cboOrderProcessStatus.Text = (rd["Status"].ToString());

                BusinessTier.DisposeReader(rd);
            }

            else
            {
                BusinessTier.DisposeReader(rd);
                string sql1 = "Select * from OrderForm where OrderAutoID='" + cboOrderNo.SelectedValue.ToString() + "' and deleted=0";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                SqlDataReader rd1 = cmd1.ExecuteReader();

                if (rd1.Read())
                {
                    //  ClearInputs();
                    flag = "N";
                    txtColor1.Text = (rd1["Color1"].ToString());
                    txtColor2.Text = (rd1["Color2"].ToString());
                    txtColor3.Text = (rd1["Color3"].ToString());
                    txtColor4.Text = (rd1["Color4"].ToString());
                    txtColor5.Text = (rd1["Color5"].ToString());
                    txtColor6.Text = (rd1["Color6"].ToString());

                    txtheight1.Text = (rd1["Hgt1"].ToString());
                    txtheight2.Text = (rd1["Hgt2"].ToString());
                    txtheight3.Text = (rd1["Hgt3"].ToString());
                    txtheight4.Text = (rd1["Hgt4"].ToString());
                    txtheight5.Text = (rd1["Hgt5"].ToString());
                    txtheight6.Text = (rd1["Hgt6"].ToString());

                    txtColorQty1.Text = (rd1["Qty1"].ToString());
                    txtColorQty2.Text = (rd1["Qty2"].ToString());
                    txtColorQty3.Text = (rd1["Qty3"].ToString());
                    txtColorQty4.Text = (rd1["Qty4"].ToString());
                    txtColorQty5.Text = (rd1["Qty5"].ToString());
                    txtColorQty6.Text = (rd1["Qty6"].ToString());

                    int qty1 = 0, qty2 = 0, qty3 = 0, qty4 = 0, qty5 = 0, qty6 = 0;
                    //-------------------Calculation Quantity1--------------------//
                    if (rd1["Qty1"].ToString() == "")
                    {
                        qty1 = 0;
                    }
                    else
                    {
                        qty1 = Convert.ToInt32(rd1["Qty1"].ToString());
                    }
                    //-------------------Calculation Quantity2--------------------//
                    if (rd1["Qty2"].ToString() == "")
                    {
                        qty2 = 0;
                    }
                    else
                    {
                        qty2 = Convert.ToInt32(rd1["Qty2"].ToString());
                    }
                    //-------------------Calculation Quantity3--------------------//
                    if (rd1["Qty3"].ToString() == "")
                    {
                        qty3 = 0;
                    }
                    else
                    {
                        qty3 = Convert.ToInt32(rd1["Qty3"].ToString());
                    }
                    //-------------------Calculation Quantity4--------------------//
                    if (rd1["Qty4"].ToString() == "")
                    {
                        qty4 = 0;
                    }
                    else
                    {
                        qty4 = Convert.ToInt32(rd1["Qty4"].ToString());
                    }
                    //-------------------Calculation Quantity5--------------------//
                    if (rd1["Qty5"].ToString() == "")
                    {
                        qty5 = 0;
                    }
                    else
                    {
                        qty5 = Convert.ToInt32(rd1["Qty5"].ToString());
                    }
                    //-------------------Calculation Quantity6--------------------//
                    if (rd1["Qty6"].ToString() == "")
                    {
                        qty6 = 0;
                    }
                    else
                    {
                        qty6 = Convert.ToInt32(rd1["Qty6"].ToString());
                    }

                    totalqty = qty1 + qty2 + qty3 + qty4 + qty5 + qty6;

                    txtAssem2DoorBoard.Text = (totalqty.ToString());

                    txtAluminiumBottomBar.Text = (rd1["AluBottomBar"].ToString());
                    txtNylonPolystrip.Text = (rd1["NylonPoly"].ToString());
                    txtQCWidth.Text = (rd1["WidthCal"].ToString());
                    txtQCHeight.Text = (rd1["HeightCal"].ToString());

                    dtpInstallDate.DbSelectedDate = rd1["ShippingDate"].ToString();
                    txtInstallMan1.Text = (rd1["InstallMan1"].ToString());
                    txtInstallMan2.Text = (rd1["InstallMan2"].ToString());
                    txtInstallMan3.Text = (rd1["InstallMan3"].ToString());
                    txtInstallMan4.Text = (rd1["InstallMan4"].ToString());
                    txtInstallMan5.Text = (rd1["InstallMan5"].ToString());
                    txtInstallMan6.Text = (rd1["InstallMan6"].ToString());

                    txtPunchVenthole.Text = (rd1["Venthole"].ToString());
                    txtPunchVentRow.Text = (rd1["VentRow"].ToString());
                    txtPunchLetterbox.Text = (rd1["Letterbox"].ToString());
                    txtPunchLock.Text = (rd1["LockType"].ToString());
                    txtOtherLock.Text = (rd1["OtherLckTyp"].ToString());
                    txtDoorOrentation.Text = (rd1["DoorOrein"].ToString());

                    txtass1Letterbox.Text = (rd1["Letterbox"].ToString());
                    txtass1Lock.Text = (rd1["LockType"].ToString());
                    txtass1otherlock.Text = (rd1["OtherLckTyp"].ToString());

                    txtPulleypcs.Text = (rd1["Pulley"].ToString());
                    txtSpring1.Text = (rd1["Spring1"].ToString());
                    txtSpring2.Text = (rd1["Spring2"].ToString());
                    txtSpring3.Text = (rd1["Spring3"].ToString());
                    txtSpring4.Text = (rd1["Spring4"].ToString());
                    txtMotor.Text = (rd1["Motor"].ToString());

                    txtSpring1pcs.Text = (rd1["Spring1pcs"].ToString());
                    txtSpring2pcs.Text = (rd1["Spring2pcs"].ToString());
                    txtSpring3pcs.Text = (rd1["Spring3pcs"].ToString());
                    txtSpring4pcs.Text = (rd1["Spring4pcs"].ToString());

                    txtControlBox.Text = (rd1["ControlBox"].ToString());
                    txtVoltage.Text = (rd1["Voltage"].ToString());
                    txtCurrent.Text = (rd1["Currnt"].ToString());
                    txtManualOverride.Text = (rd1["ManualOver"].ToString());
                    txtRemoteBox.Text = (rd1["RemoteBox"].ToString());
                    txtUPSBattery.Text = (rd1["UPSBattery"].ToString());

                    MName = (rd1["SendName"].ToString());
                    MMail = (rd1["SendMail"].ToString());
                    CName = (rd1["ClerkName"].ToString());
                    CMail = (rd1["ClerkMail"].ToString());
                }
                BusinessTier.DisposeReader(rd1);
            }
            BusinessTier.DisposeConnection(conn);
            lblStatus.Text = "";
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "OrderForm", "ItemClick", ex.ToString(), "Audit");
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtSpring1.Text.ToString()))
        {
            lblStatus.Text = "Please Enter the Spring details in OrderForm";
            return;
        }
        if (string.IsNullOrEmpty(cboOrderProcessStatus.Text.ToString()))
        {
            lblStatus.Text = "Select Order Status";
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();

        try
        {
            string sql1 = "Select * from VW_FinalInspection where OrderAutoID='" + cboOrderNo.SelectedValue.ToString() + "' and deleted=0";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            SqlDataReader rd1 = cmd1.ExecuteReader();

            if (rd1.Read())
            {
                MName = rd1["SendName"].ToString();
                MMail = rd1["SendMail"].ToString();
                CName = rd1["ClerkName"].ToString();
                CMail = rd1["ClerkMail"].ToString();
            }
            rd1.Close();

            if (flag == "N")
            {
                //string sql6 = "INSERT INTO [SJClassic].[dbo].[InspectionTbl]([OrderAutoid],[CuttingAwidth],[CuttingALength],[CuttingAtotal],[CuttingBwidth],[CuttingBLength],[CuttingBtotal],[CuttingResult],[CuttingIncharge],[PunchResult],[PunchIncharge],[Assemresult],[AssemIncharge],[Assem2DoorBoard],[Assem2AluBottomBar],[Assem2NylonPoly],[Assem2Result],[Assem2Incharge],[Assem3result],[Assem3Incharge],[Assem4],[Assem4result],[Assem4Incharge],[QCWidth],[QCHeight],[QCResult],[QCIncharge],[PackingWrap],[PackingSpan],[PackingResult],[PackingIncharge],[Installdate],[InstallMan1],[InstallMan2],[InstallMan3],[InstallMan4],[InstallResult],[InstallIncharge],[StickerSJCResult],[createdby],[createddate]) VALUES ('" + cboOrderNo.SelectedValue.ToString() + "' ,'" + txtCuttingAwidth.Text.ToString() + "','" + txtCuttingALength.Text.ToString() + "','" + txtCuttingAtotal.Text.ToString() + "','" + txtCuttingBwidth.Text.ToString() + "','" + txtCuttingBLength.Text.ToString() + "','" + txtCuttingBtotal.Text.ToString() + "','" + cboCuttingResult.Text.ToString() + "','" + cboInchargeCut.Text.ToString() + "','" + cboPunchResult.Text.ToString() + "','" + cboInchargePunch.Text.ToString() + "','" + cboAssemresult.Text.ToString() + "','" + cboInchargeAss1.Text.ToString() + "','" + txtAssem2DoorBoard.Text.ToString() + "','" + cboAluminiumBottomBar.Text.ToString() + "','" + cboNylonPolystrip.Text.ToString() + "','" + cboAssem2Result.Text.ToString() + "','" + cboInchargeAss2.Text.ToString() + "','" + cboAssem3result.Text.ToString() + "','" + cboInchargeAss3.Text.ToString() + "','" + cboAssembled4.Text.ToString() + "','" + cboAssem4result.Text.ToString() + "','" + cboInchargeAss4.Text.ToString() + "','" + txtQCWidth.Text.ToString() + "','" + txtQCHeight.Text.ToString() + "','" + cboQCResult.Text.ToString() + "','" + cboInchargeQC.Text.ToString() + "','" + cboWrapping.Text.ToString() + "','" + txtPackingSpan.Text.ToString() + "','" + cboPackingResult.Text.ToString() + "','" + cboInchargePack.Text.ToString() + "','" + Convert.ToDateTime(dtpInstallDate.SelectedDate.ToString()).ToString("MM/dd/yyyy") + "','" + txtInstallMan1.Text.ToString() + "','" + txtInstallMan2.Text.ToString() + "','" + txtInstallMan3.Text.ToString() + "','" + txtInstallMan4.Text.ToString() + "','" + cboInstallResult.Text.ToString() + "','" + cboInchargeInstall.Text.ToString() + "','" + cboStickerSJCResult.Text.ToString() + "','" + Session["sesUserID"].ToString() + "','" + DateTime.Now.ToString() + "')";
                //string sql6 = "insert into FinalInspection(OrderAutoid,Status,DoorBoard,createdby,createddate) values('" + cboOrderNo.SelectedValue.ToString() + "','" + cboOrderProcessStatus.Text.ToString() + "','" + txtAssem2DoorBoard.Text.ToString() + "','" + Session["sesUserID"].ToString() + "','" + DateTime.Now.ToString() + "')";
                //SqlCommand cmd6 = new SqlCommand(sql6, conn);
                //cmd6.ExecuteNonQuery();
                int flg = BusinessTier.FinalInspection(conn, cboOrderNo.SelectedValue.ToString(), cboOrderProcessStatus.Text.ToString(), txtAssem2DoorBoard.Text.ToString(), Session["sesUserID"].ToString(), "I");
                if (flg >= 1)
                {
                    lblStatus.Text = "Final Inspection Value Successfully Inserted";
                    lblStatus.ForeColor = Color.Maroon;
                }
            }
            else
            {
                //string sql6 = "update [SJClassic].[dbo].[InspectionTbl] set [CuttingAwidth]='" + txtCuttingAwidth.Text.ToString() + "',[CuttingALength]='" + txtCuttingALength.Text.ToString() + "',[CuttingAtotal]='" + txtCuttingAtotal.Text.ToString() + "',[CuttingBwidth]='" + txtCuttingBwidth.Text.ToString() + "',[CuttingBLength]='" + txtCuttingBLength.Text.ToString() + "',[CuttingBtotal]='" + txtCuttingBtotal.Text.ToString() + "',[CuttingResult]='" + cboCuttingResult.Text.ToString() + "',[CuttingIncharge]='" + cboInchargeCut.Text.ToString() + "',[PunchResult]='" + cboPunchResult.Text.ToString() + "',[PunchIncharge]='" + cboInchargePunch.Text.ToString() + "',[Assemresult]='" + cboAssemresult.Text.ToString() + "',[AssemIncharge]='" + cboInchargeAss1.Text.ToString() + "',[Assem2DoorBoard]='" + txtAssem2DoorBoard.Text.ToString() + "',[Assem2AluBottomBar]='" + cboAluminiumBottomBar.Text.ToString() + "',[Assem2NylonPoly]='" + cboNylonPolystrip.Text.ToString() + "',[Assem2Result]='" + cboAssem2Result.Text.ToString() + "',[Assem2Incharge]='" + cboInchargeAss2.Text.ToString() + "',[Assem3result]='" + cboAssem3result.Text.ToString() + "',[Assem3Incharge]='" + cboInchargeAss3.Text.ToString() + "',[Assem4]='" + cboAssembled4.Text.ToString() + "',[Assem4result]='" + cboAssem4result.Text.ToString() + "',[Assem4Incharge]='" + cboInchargeAss4.Text.ToString() + "',[QCWidth]='" + txtQCWidth.Text.ToString() + "',[QCHeight]='" + txtQCHeight.Text.ToString() + "',[QCResult]='" + cboQCResult.Text.ToString() + "',[QCIncharge]='" + cboInchargeQC.Text.ToString() + "',[PackingWrap]='" + cboWrapping.Text.ToString() + "',[PackingSpan]='" + txtPackingSpan.Text.ToString() + "',[PackingResult]='" + cboPackingResult.Text.ToString() + "',[PackingIncharge]='" + cboInchargePack.Text.ToString() + "',[Installdate]='" + Convert.ToDateTime(dtpInstallDate.SelectedDate.ToString()).ToString("MM/dd/yyyy") + "',[InstallMan1]='" + txtInstallMan1.Text.ToString() + "',[InstallMan2]='" + txtInstallMan2.Text.ToString() + "',[InstallMan3]='" + txtInstallMan3.Text.ToString() + "',[InstallMan4]='" + txtInstallMan4.Text.ToString() + "',[InstallResult]='" + cboInstallResult.Text.ToString() + "',[InstallIncharge]='" + cboInchargeInstall.Text.ToString() + "',[StickerSJCResult]='" + cboStickerSJCResult.Text.ToString() + "',modifiedby='" + Session["sesUserID"].ToString() + "' , modifydate = '" + DateTime.Now.ToString() +"' where deleted=0 and orderautoid='" + cboOrderNo.SelectedValue.ToString() + "'";
                //string sql6 = "update FinalInspection set Status='" + cboOrderProcessStatus.Text.ToString() + "',DoorBoard='" + txtAssem2DoorBoard .Text.ToString() + "',modifiedby='" + Convert.ToInt32(Session["sesUserID"].ToString()) + "' , modifydate = '" + DateTime.Now.ToString() + "' where deleted=0 and orderautoid='" + Convert.ToInt32(cboOrderNo.SelectedValue.ToString()) + "'";
                //SqlCommand cmd6 = new SqlCommand(sql6, conn);
                //cmd6.ExecuteNonQuery();
                int flg = BusinessTier.FinalInspection(conn, cboOrderNo.SelectedValue.ToString(), cboOrderProcessStatus.Text.ToString(), txtAssem2DoorBoard.Text.ToString(), Session["sesUserID"].ToString(), "U");

                // BusinessTier.DisposeConnection(conn);
                if (flg >= 1)
                {
                    lblStatus.Text = "Final Inspection Value Successfully Updated";
                    lblStatus.ForeColor = Color.Maroon;
                }
            }
            string sql = "update OrderForm set OrdStatus='" + cboOrderProcessStatus.Text.ToString() + "' where deleted=0 and orderautoid='" + cboOrderNo.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            BusinessTier.DisposeConnection(conn);
            if (CName != "")
            {
                SendMail(CName, CMail);
            }
            if (MName != "")
            {
                SendMail(MName, MMail);
                lblStatus.Text = "Final Inspection Info Inserted Successfully & Successfully e-mail sent!";
                lblStatus.ForeColor = Color.Green;
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = ex.Message;
            BusinessTier.DisposeConnection(conn);
        }
        ClearInputs(Page.Controls);
        //cboOrderNo.ClearSelection();
    }

    protected void SendMail(string Name, string Mail)
    {
        if (Session["Name"].ToString().Trim() == "")
        {
            Response.Redirect("Login.aspx");
        }

        MailMessage message1 = new MailMessage();
        message1.From = new MailAddress(ConfigurationManager.AppSettings["MailAddress"].ToString());
        // message1.From = new MailAddress(Session["Email"].ToString().Trim());
        message1.To.Add(new MailAddress(Mail.ToString().Trim()));

        message1.Subject = "Request for OrderForm Confirmation";
        string msg = string.Empty;
        msg = "Dear " + Name.ToString() + ",\n\n" + "The Order No : " + cboOrderNo.Text.ToString() + " is Now " + cboOrderProcessStatus.Text.ToString() + ".\n\nby, \n" + Session["Name"].ToString();
        message1.Body = msg;
        SmtpClient client1 = new SmtpClient(ConfigurationManager.AppSettings["Webserver"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()));
        client1.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
        client1.DeliveryMethod = SmtpDeliveryMethod.Network;
        client1.EnableSsl = true;
        client1.Send(message1);

    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        //window.open('default.aspx','name','width=200,height=200');
        Response.Write("<script language=javascript>child=window.open('Report.aspx'),'Report',width=700,height=350,left=430,top=23;</script>");
    }

    private void InsertLogAuditTrail(string userid, string module, string activity, string result, string flag)
    {
        SqlConnection connLog = BusinessTier.getConnection();
        connLog.Open();
        BusinessTier.InsertLogAuditTrial(connLog, userid, module, activity, result, flag);
        BusinessTier.DisposeConnection(connLog);
    }

    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            if (ctrl is RadComboBox)
                ((RadComboBox)ctrl).ClearSelection();
            ClearInputs(ctrl.Controls);
        }
        dtpInstallDate.Clear();
        cboOrderProcessStatus.ClearSelection();
        cboOrderProcessStatus.Text = string.Empty;

    }

}