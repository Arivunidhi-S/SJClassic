using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using MySql.Data.MySqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Runtime;
using System.Drawing;
using System.Data.SqlClient;
//using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Web.SessionState;



public partial class Login : System.Web.UI.Page


{
    public string qry = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        LoginTxt.Focus();
        Session["sesUserID"] = 0;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Session.Contents.Clear();
        LoginTxt.Text = "";
        PwdTxt.Text = "";
    }

    protected void LoginBtn_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(LoginTxt.Text.ToString()))
        {
            lblStatus.Text = "Enter your User ID";
            return;
        }
        if (string.IsNullOrEmpty(PwdTxt.Text.ToString()))
        {
            lblStatus.Text = "Enter your Password";
            return;
        }

        SqlConnection conn = BusinessTier.getConnection();
        SqlConnection connec = BusinessTier.getConnection();
        try
        {
            conn.Open();
            BusinessTier.BindErrorMessageDetails(conn);
            BusinessTier.DisposeConnection(conn);

            int flag = 0;
            string strId = "0";
            int intValidation = 0;
            string appPath = Request.PhysicalApplicationPath;
            connec.Open();
            SqlDataReader reader1 = BusinessTier.VaildateUserLogin(connec, LoginTxt.Text.ToString(), PwdTxt.Text.ToString());
            if (reader1.Read())
            {
                flag = 2;
                if (!(string.IsNullOrEmpty(reader1["ID"].ToString())))
                {
                    flag = 1;
                    strId = (reader1["ID"].ToString());
                    //Session["sesPlatformID"] = (reader1["PlatformId"].ToString());
                    Session["sesUserType"] = (reader1["UserType"].ToString());
                    Session["stid"] = (reader1["StaffAutoID"].ToString());
                    Session["Name"] = (reader1["UserName"].ToString());
                    Session["Designation"] = (reader1["Designation"].ToString());
                    qry = Session["stid"].ToString();
                }
                else
                {
                    intValidation = 1;
                    qry ="Nothing";

                }
                
            }
            else
            {
                Session["stid"] = "0";
            }
            BusinessTier.DisposeReader(reader1);

            string sql = "Select Email from MasterStaff where StaffAutoID='" + Session["stid"].ToString() + "' and deleted=0";
            SqlCommand cmd = new SqlCommand(sql, connec);
            SqlDataReader rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                Session["Email"] = rd["Email"].ToString();
                //lblStatus.Text = rd["Email"].ToString();
            }
            rd.Close();

            BusinessTier.DisposeConnection(connec);

            if (intValidation == 1)
            {
                ShowMessage(16);
                return;
            }

            if (flag >= 1)
            {
                Session["sesUserID"] = strId.ToString();
                Response.Redirect("Main.aspx", false);
            }
            else
            {
               lblStatus.Text = "Invalid UserID, Password";
                //lblStatus.Text = "Check" + flag.ToString();
                //lblStatus.Text = ex.ToString();
                //ShowMessage(16);
                return;
            }
            //InsertLogAuditTrial is used to insert the action into MasterLog table
           // InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterLogin", "SignIn", "Success", "Log");
        }
        catch (Exception ex)
        {
            BusinessTier.DisposeConnection(conn);
            BusinessTier.DisposeConnection(connec);
           // ShowMessage(5);
            //lblStatus.Text = "Invalid UserID, Password";
           lblStatus.Text = ex.ToString() + qry.ToString();
            InsertLogAuditTrail(Session["sesUserID"].ToString(), "MasterLogin", "SignIn", ex.ToString(), "Audit");
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

    private void MessageBox(string msg)
    {
        Label lbl = new Label();
        lbl.Text = "<script language='javascript'>" + Environment.NewLine + "window.alert('" + msg + "')</script>";
        Page.Controls.Add(lbl);
    }
}