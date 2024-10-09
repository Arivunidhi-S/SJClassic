using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using System.Net;

using System.Globalization;
using System.Collections;
using System.Data.OleDb;
using System.Drawing;

/// <summary>
/// Summary description for Class1
/// </summary>
public class BusinessTier
{
    public BusinessTier()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable g_ErrorMessagesDataTable;

    public static SqlConnection getConnection()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        SqlConnection conn = new SqlConnection(conString);
        return conn;
    }

    public static string getConnection1()
    {
        string conString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        //SqlConnection conn = new SqlConnection(conString);
        return conString;
    }


    public static void DisposeConnection(SqlConnection conn)
    {
        conn.Close();
        conn.Dispose();
    }

    public static void DisposeReader(SqlDataReader reader)
    {
        reader.Close();
        reader.Dispose();
    }

    public static void DisposeAdapter(SqlDataAdapter adapter)
    {
        adapter.Dispose();
    }

    public static int SaveOrderDetail(SqlConnection connSave, decimal price, Int32 menuid, decimal deduction, string strSaveFlag, string CurrUserId, Int64 orderid, Int32 qty)
    {
        if (connSave == null)
            connSave = getConnection();
        connSave.Open();
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_OrderDetail_Insert]";
        }
        else
        {
            sp_Name = "[sp_OrderDetail_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@orderid", orderid);
        }
        dCmd.Parameters.AddWithValue("@price", price);
        dCmd.Parameters.AddWithValue("@menuid", menuid);
        dCmd.Parameters.AddWithValue("@orderid", orderid);
        dCmd.Parameters.AddWithValue("@deduction", deduction);
        dCmd.Parameters.AddWithValue("@qty", qty);
        dCmd.Parameters.AddWithValue("@CurrUserId", CurrUserId);
        return dCmd.ExecuteNonQuery();
    }
    public static int SaveOrder(SqlConnection connSave, string tblno, decimal taxorig, decimal tax, decimal orderprice, decimal strtotal, string orderdate, string strSaveFlag, string CurrUserId, string orderid, decimal totdisc, decimal taxadj)
    {
        if (connSave == null)
            connSave = getConnection();
        connSave.Open();
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_Order_Insert]";
        }
        else
        {
            sp_Name = "[sp_Order_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@orderid", orderid);
        }
        dCmd.Parameters.AddWithValue("@tblno", tblno);
        dCmd.Parameters.AddWithValue("@taxorig", taxorig);
        dCmd.Parameters.AddWithValue("@tax", tax);
        dCmd.Parameters.AddWithValue("@taxadj", taxadj);
        dCmd.Parameters.AddWithValue("@orderprice", orderprice);
        dCmd.Parameters.AddWithValue("@total", strtotal);
        dCmd.Parameters.AddWithValue("@totdisc", totdisc);
        dCmd.Parameters.AddWithValue("@CurrUserId", CurrUserId);
        return dCmd.ExecuteNonQuery();
    }



    public static int SaveInstallation(SqlConnection connSave, string strInstId, string strInstCode, string strInstName, string strInstType, string strXC, string strYC, string strMC, string strDesc, string strUserid, string strSaveFlag, string pid1)
    {
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_Installation_Insert]";
        }
        else
        {
            sp_Name = "[sp_Installation_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@instid_p", strInstId);
        }
        dCmd.Parameters.AddWithValue("@instcode_p", strInstCode);
        dCmd.Parameters.AddWithValue("@instname_p", strInstName);
        dCmd.Parameters.AddWithValue("@insttype_p", strInstType);
        dCmd.Parameters.AddWithValue("@xc_p", strXC);
        dCmd.Parameters.AddWithValue("@yc_p", strYC);
        dCmd.Parameters.AddWithValue("@mc_p", strMC);
        dCmd.Parameters.AddWithValue("@descriptionp", strDesc);
        dCmd.Parameters.AddWithValue("@useridp", strUserid);
        dCmd.Parameters.AddWithValue("@pidp", pid1);
        return dCmd.ExecuteNonQuery();
    }


    public static int DeleteInstallation(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Installation_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@instid_p", id);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader VaildateUserLogin(SqlConnection connec, string Logind, string Password)
    {
        SqlCommand cmd = new SqlCommand("sp_Validate_UserLogin", connec);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Useridp", Logind);
        cmd.Parameters.AddWithValue("@Passp", Password);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;
    }

    public static SqlDataReader checkUserGroupId(SqlConnection connCheck, string strGroupId)
    {
        SqlCommand cmd = new SqlCommand("[sp_UserGroup_IsDuplicate]", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@group_idp", strGroupId);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }


    public static int DeleteUserGroupGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_UserGroup_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@group_aidp", id);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveUserGroup(SqlConnection connSave, string strgroupid, string strdesc, string strSaveFlag, string strCurrUserId)
    {
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_UserGroup_Insert]";
        }
        else
        {
            sp_Name = "[sp_UserGroup_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@group_aidp", strCurrUserId);
        }
        dCmd.Parameters.AddWithValue("@group_idp", strgroupid);
        dCmd.Parameters.AddWithValue("@descriptionp", strdesc);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getUserGroupById(SqlConnection conn, string strUserGroupId)
    {
        int delval = 0;
        string sql = "select * FROM UserGroup_tbl WHERE Deleted='" + delval + "' and group_id='" + strUserGroupId + "' ORDER BY group_id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getUserGroupByAID(SqlConnection conn, string strUserGroupAId)
    {
        int delval = 0;
        string sql = "select * FROM UserGroup_tbl WHERE Deleted='" + delval + "' and group_aid='" + strUserGroupAId + "' ORDER BY group_id";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveUserGroupModulePermission(SqlConnection connSave, long intgroupid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_UserGroupModulePermission_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@usergroup_aidp", intgroupid);
        dCmd.Parameters.AddWithValue("@moduleidp", intlinebyline);
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getMasterModulePermisnByUserGroupId(SqlConnection connModulePermission, string strUserGroupId)
    {
        int delval = 0;
        string sql = "select * FROM vw_MasterModulePermission_MasterModuleByModuleID WHERE Deleted='" + delval + "' and group_aid='" + strUserGroupId.ToString() + "'";
        SqlCommand cmd = new SqlCommand(sql, connModulePermission);
        SqlDataReader readerModulePermission = cmd.ExecuteReader();
        return readerModulePermission;
    }

    public static int SaveUserDetails(SqlConnection conn, string strID, string strName, string strPass, string strGroup, string strEmail, string strUserid, string strSaveFlag, string strStatus, string strContact, string contact_name, string contact_no, string designation, string department)
    {
        string sp_Name = "";
        if (strSaveFlag.ToString() == "Insert")
            sp_Name = "sp_user_tbl_insert";

        if (strSaveFlag.ToString() == "Update")
            sp_Name = "sp_user_tbl_update";

        if (strSaveFlag.ToString() == "Delete")
            sp_Name = "[sp_user_tbl_delete]";

        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if ((strSaveFlag.ToString() == "Update") || (strSaveFlag.ToString() == "Delete"))
        {
            dCmd.Parameters.AddWithValue("@user_aid", strID);
        }
        if ((strSaveFlag.ToString() == "Update") || (strSaveFlag.ToString() == "Insert"))
        {
            dCmd.Parameters.AddWithValue("@user_name", strName.Trim());
            dCmd.Parameters.AddWithValue("@user_password", strPass.Trim());
            dCmd.Parameters.AddWithValue("@user_group", strGroup.Trim());
            dCmd.Parameters.AddWithValue("@user_email", strEmail.Trim());
            dCmd.Parameters.AddWithValue("@user_active", strStatus.Trim());
            dCmd.Parameters.AddWithValue("@contact", strContact.Trim());
            dCmd.Parameters.AddWithValue("@contact_name", contact_name.Trim());
            dCmd.Parameters.AddWithValue("@contact_no", contact_no.Trim());
            dCmd.Parameters.AddWithValue("@designation", designation.Trim());
            dCmd.Parameters.AddWithValue("@department", department.Trim());
        }
        return dCmd.ExecuteNonQuery();
    }
    public static void InsertLogAuditTrial(SqlConnection connLog, string userid, string module, string activity, string result, string flag)
    {
        string sp_Name;
        if (flag.ToString() == "Log")
        {
            sp_Name = "[sp_Master_Insert_Log]";
        }
        else
        {
            sp_Name = "[sp_Master_Insert_AuditTrail]";
        }

        SqlCommand dCmd = new SqlCommand(sp_Name, connLog);

        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@modulep", module);
        dCmd.Parameters.AddWithValue("@activityp", activity);
        dCmd.Parameters.AddWithValue("@resultp", result);
        dCmd.ExecuteNonQuery();
    }
    public static SqlDataReader getMenuList(SqlConnection conn, string strUserId, string usertype)
    {
        string sql = "";
        if (usertype.ToString().Trim() == "Admin")
            sql = "select Category, seqCategory FROM MenuList group by Category,seqCategory order by seqCategory";

        else if (usertype.ToString().Trim() == "Technician")
        {
            sql = "select Category, seqCategory FROM MenuList where id in (6,7) order by seqCategory";
        }
        else
            sql = "select Category, seqCategory FROM MenuList group by Category,seqCategory order by seqCategory";
        // sql = "select Category, seqCategory from MenuList where UserId='" + strUserId + "' group by Category,seqCategory order by seqCategory";
        //sql = "   select Category, seqCategory FROM MenuList where id not in (3,13) group by Category,seqCategory order by seqCategory";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }
    //public static SqlDataReader getMenuList(SqlConnection conn, string Uid)
    //{
    //    string sql = "SELECT dbo.MenuList.Category, dbo.MenuList.SeqCategory FROM dbo.UserInfo_Permission INNER JOIN dbo.MenuList ON dbo.UserInfo_Permission.MenuId = dbo.MenuList.Id WHERE dbo.UserInfo_Permission.UserId = '" + Uid.ToString().Trim() + "' GROUP BY dbo.MenuList.Category, dbo.MenuList.SeqCategory";
    //    SqlCommand cmd = new SqlCommand(sql, conn);
    //    SqlDataReader reader = cmd.ExecuteReader();
    //    return reader;
    //}
    //LSB MODEL----------------------------------------------------------------------------
    public static DataTable getSubMenuItems(string category, string uid, string usertype)
    {
        DataTable ret = new DataTable();
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "";
        if (usertype.ToString().Trim() == "Admin")
        {
            sql = "select ModuleID, Href, modulename FROM MenuList where Category = '" + category + "' order by seqMenu";
        }
        else if (usertype.ToString().Trim() == "Technician")
        {
            sql = "select ModuleID, Href, modulename FROM MenuList where Category = '" + category + "' and id in (6,7) order by seqMenu";
        }
        else
        {
            // sql = "select ModuleID, Href, modulename FROM vw_MenuList_MasterModulePermission where Category = '" + category + "' and UserId='" + uid + "' and Deleted=0 order by seqMenu";
            sql = "select ModuleID, Href, modulename FROM MenuList where Category = '" + category + "' and id not in (3,13) order by seqMenu";
        }
        SqlCommand cmd = new SqlCommand(sql, conn);

        SqlDataReader reader = cmd.ExecuteReader();
        ret.Load(reader);
        BusinessTier.DisposeConnection(conn);
        return ret;
    }
    public static DataTable getSubMenuItemss(string category, string uid)
    {
        DataTable ret = new DataTable();
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "SELECT dbo.MenuList.ModuleID, dbo.MenuList.Href, dbo.MenuList.ModuleName FROM dbo.UserInfo_Permission INNER JOIN dbo.MenuList ON dbo.UserInfo_Permission.MenuId = dbo.MenuList.Id WHERE dbo.MenuList.Category = '" + category + "' and dbo.UserInfo_Permission.UserId = '" + uid.ToString().Trim() + "'  order by SeqMenu";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        ret.Load(reader);
        BusinessTier.DisposeConnection(conn);
        return ret;
    }
    public static int InsertLogTable(SqlConnection connSave, string userid, string param)
    {
        SqlCommand dCmd = new SqlCommand("[sp_Insert_LogTable]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@paramp", param);
        //'dCmd.Parameters.AddWithValue("@lgin", lgin);
        return dCmd.ExecuteNonQuery();
    }
    public static void BindErrorMessageDetails(SqlConnection connError)
    {
        string sql = "select * FROM MasterErrorMessage order by OrderNo";
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, connError);
        g_ErrorMessagesDataTable = new DataTable();
        sqlDataAdapter.Fill(g_ErrorMessagesDataTable);
        BusinessTier.DisposeAdapter(sqlDataAdapter);
    }

    //---------------------------------------------------------------------------------------------
    //--------------------------< Methods For Master Module >--------------------------------------
    public static int Savesupplier(SqlConnection conn, string name, string address1, string address2, string city, string state, string country, string postcode, string desc, string phone, string fax, string email, string website, string userid, string saveflag, string custid, string strShortForm)
    {
        string sp_Name = "";
        string RowValue = "0";
        if (saveflag.ToString() == "N")
            sp_Name = "sp_Customer_Ins";

        if (saveflag.ToString() == "U")
            sp_Name = "sp_Customer_Up";

        if (saveflag.ToString() == "D")
            sp_Name = "sp_Customer_Del";


        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@customeridp", custid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        if (saveflag.ToString() == "D")
            dCmd.Parameters.AddWithValue("@customeridp", custid);

        if (saveflag.ToString() == "N")
            dCmd.Parameters.AddWithValue("@customernamep", name);

        if ((saveflag.ToString() == "N") || (saveflag.ToString() == "U"))
        {
            dCmd.Parameters.AddWithValue("@address1p", address1);
            dCmd.Parameters.AddWithValue("@address2p", address2);
            dCmd.Parameters.AddWithValue("@cityp", city);
            dCmd.Parameters.AddWithValue("@statep", state);
            dCmd.Parameters.AddWithValue("@countryp", country);
            dCmd.Parameters.AddWithValue("@descriptionp", desc);
            dCmd.Parameters.AddWithValue("@contactnop", phone);
            dCmd.Parameters.AddWithValue("@faxnop", fax);
            dCmd.Parameters.AddWithValue("@emailp", email);
            dCmd.Parameters.AddWithValue("@websitep", website);
            dCmd.Parameters.AddWithValue("@postcodep", postcode);
            dCmd.Parameters.AddWithValue("@useridp", userid);
            dCmd.Parameters.AddWithValue("@shortformp", strShortForm);
        }
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveStaff(SqlConnection conn, string StaffID, string Name, string MobileNo, string Designation, string Email, string Address, string userid, string saveflag, string StaffAutoID)
    {
        string sp_Name = "";
        string RowValue = "0";
        if (saveflag.ToString() == "N")
            sp_Name = "sp_Staff_Ins";

        if (saveflag.ToString() == "U")
            sp_Name = "sp_Staff_Up";

        if (saveflag.ToString() == "D")
            sp_Name = "[sp_Staff_Del]";


        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@StaffAutoID", StaffAutoID);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        if (saveflag.ToString() == "D")
            dCmd.Parameters.AddWithValue("@StaffAutoID", StaffAutoID);

        if ((saveflag.ToString() == "N") || (saveflag.ToString() == "U"))
        {

            dCmd.Parameters.AddWithValue("@StaffID", StaffID);
            dCmd.Parameters.AddWithValue("@Name", Name);
            dCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            dCmd.Parameters.AddWithValue("@Designation", Designation);
            dCmd.Parameters.AddWithValue("@Email", Email);
            dCmd.Parameters.AddWithValue("@Address", Address);
            dCmd.Parameters.AddWithValue("@useridp", userid);
        }
        return dCmd.ExecuteNonQuery();
    }



    public static int SaveMaterial(SqlConnection conn, string Materialcode, string MaterialName, string SerialNo, string battery, string manfdate, string expdate, string Description, string userid, string saveflag, string matrid)
    {
        string sp_Name = "";
        string RowValue = "0";
        if (saveflag.ToString() == "N")
            sp_Name = "sp_Material_Ins";

        if (saveflag.ToString() == "U")
            sp_Name = "sp_Material_Up";

        if (saveflag.ToString() == "D")
            sp_Name = "sp_Material_Del";


        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@Materialidp", matrid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        if (saveflag.ToString() == "D")
            dCmd.Parameters.AddWithValue("@Materialidp", matrid);

        //if (saveflag.ToString() == "N")
        //    dCmd.Parameters.AddWithValue("@Materialcodep", Materialcode);

        if ((saveflag.ToString() == "N") || (saveflag.ToString() == "U"))
        {

            dCmd.Parameters.AddWithValue("@Materialcodep", Materialcode);
            dCmd.Parameters.AddWithValue("@MaterialNamep", MaterialName);
            dCmd.Parameters.AddWithValue("@Size", SerialNo);
            dCmd.Parameters.AddWithValue("@MaterialType", battery);
            dCmd.Parameters.AddWithValue("@manfdatep", manfdate);
            dCmd.Parameters.AddWithValue("@expdatep", expdate);
            dCmd.Parameters.AddWithValue("@Quantity", Description);
            dCmd.Parameters.AddWithValue("@useridp", userid);
        }
        return dCmd.ExecuteNonQuery();
    }
    public static int SaveCustomer(SqlConnection conn, string CustomerAutoid, string name, string address1, string address2, string city, string state, string country, string postcode, string desc, string phone, string fax, string email, string website, string userid, string saveflag, string custid)
    {
        string sp_Name = "";
        string RowValue = "0";
        if (saveflag.ToString() == "N")
            sp_Name = "sp_Customer_Ins";

        if (saveflag.ToString() == "U")
            sp_Name = "sp_Customer_Up";

        if (saveflag.ToString() == "D")
            sp_Name = "sp_Customer_Del";


        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@customeridp", custid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        if (saveflag.ToString() == "D")
            dCmd.Parameters.AddWithValue("@customeridp", custid);

        if (saveflag.ToString() == "N")
            dCmd.Parameters.AddWithValue("@CustomerAutoid", CustomerAutoid);

        if ((saveflag.ToString() == "N") || (saveflag.ToString() == "U"))
        {
            dCmd.Parameters.AddWithValue("@customernamep", name);
            dCmd.Parameters.AddWithValue("@address1p", address1);
            dCmd.Parameters.AddWithValue("@address2p", address2);
            dCmd.Parameters.AddWithValue("@cityp", city);
            dCmd.Parameters.AddWithValue("@statep", state);
            dCmd.Parameters.AddWithValue("@countryp", country);
            dCmd.Parameters.AddWithValue("@descriptionp", desc);
            dCmd.Parameters.AddWithValue("@contactnop", phone);
            dCmd.Parameters.AddWithValue("@faxnop", fax);
            dCmd.Parameters.AddWithValue("@emailp", email);
            dCmd.Parameters.AddWithValue("@websitep", website);
            dCmd.Parameters.AddWithValue("@postcodep", postcode);
            dCmd.Parameters.AddWithValue("@useridp", userid);
            //dCmd.Parameters.AddWithValue("@shortformp", strShortForm);
        }
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveOrderForm(SqlConnection conn, string @OrderNo, string @SINo, string @DoorType, string @Date, string @OrdStatus, string @ShippingDate, string @DealerID, string @DealerName, string @Tel, string @Address, string @DeliveryType, string @Remark1, string @InstallMan1, string @InstallMan2, string @InstallMan3, string @InstallMan4, string @InstallMan5, string @InstallMan6, string @Height, string @Width, string @Walltype, string @Unit, string @UnitQty, string @BasePrice, string @UnitPrice, string @TotalAmt, string @Remark2, string @SplSpec, string @HeightCal, string @WidthCal, string @WidthCal2, string @balheight, string @Color1, string @Color2, string @Color3, string @Color4, string @Color5, string @Color6, string @Hgt1, string @Hgt2, string @Hgt3, string @Hgt4, string @Hgt5, string @Hgt6, string @Qty1, string @Qty2, string @Qty3, string @Qty4, string @Qty5, string @Qty6, string @Venthole, string @VentRow, string @Letterbox, string @ControlBox, string @LockType, string @OtherLckTyp, string @Voltage, string @Currnt, string @NamePlate, string @Motor, string @ManualOver, string @RemoteBox, string @DoorOrein, string @UPSBattery, string @PullHandle, string @PullHook, string @Packing, string @WrntyDoor, string @WrntyMotor, string @SplRemark, string @AluBottomBar, string @NylonPoly, string @SendName, string @SendMail, string @Tax, string @TaxPrice, string @UnitPriceNoTax, int @createdby, int @OrderAutoID, string @flag, string @ClerkName, string @ClerkMail)
    {
        SqlCommand dCmd = new SqlCommand("[sp_OrderForm_Ins]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        //--------------Step:1--------------------

        dCmd.Parameters.AddWithValue("@OrderNo", @OrderNo);
        dCmd.Parameters.AddWithValue("@SINo", @SINo);
        dCmd.Parameters.AddWithValue("@DoorType", @DoorType);
        dCmd.Parameters.AddWithValue("@Date", @Date);
        dCmd.Parameters.AddWithValue("@OrdStatus", @OrdStatus);
        dCmd.Parameters.AddWithValue("@ShippingDate", @ShippingDate);
        dCmd.Parameters.AddWithValue("@DealerID", @DealerID);
        dCmd.Parameters.AddWithValue("@DealerName", @DealerName);
        dCmd.Parameters.AddWithValue("@Tel", @Tel);
        dCmd.Parameters.AddWithValue("@Address", @Address);
        dCmd.Parameters.AddWithValue("@DeliveryType", @DeliveryType);
        dCmd.Parameters.AddWithValue("@Remark1", @Remark1);
        dCmd.Parameters.AddWithValue("@InstallMan1", @InstallMan1);
        dCmd.Parameters.AddWithValue("@InstallMan2", @InstallMan2);
        dCmd.Parameters.AddWithValue("@InstallMan3", @InstallMan3);
        dCmd.Parameters.AddWithValue("@InstallMan4", @InstallMan4);
        dCmd.Parameters.AddWithValue("@InstallMan5", @InstallMan5);
        dCmd.Parameters.AddWithValue("@InstallMan6", @InstallMan6);

        //--------------Step:2--------------------

        dCmd.Parameters.AddWithValue("@Height", @Height);
        dCmd.Parameters.AddWithValue("@Width", @Width);
        dCmd.Parameters.AddWithValue("@Walltype", @Walltype);
        dCmd.Parameters.AddWithValue("@Unit", @Unit);
        dCmd.Parameters.AddWithValue("@UnitQty", @UnitQty);
        dCmd.Parameters.AddWithValue("@BasePrice", @BasePrice);
        dCmd.Parameters.AddWithValue("@UnitPrice", @UnitPrice);
        dCmd.Parameters.AddWithValue("@TotalAmt", @TotalAmt);
        dCmd.Parameters.AddWithValue("@Remark2", @Remark2);
        dCmd.Parameters.AddWithValue("@SplSpec", @SplSpec);

        //--------------Step:3--------------------

        dCmd.Parameters.AddWithValue("@HeightCal", @HeightCal);
        dCmd.Parameters.AddWithValue("@WidthCal", @WidthCal);
        dCmd.Parameters.AddWithValue("@WidthCal2", @WidthCal2);
        dCmd.Parameters.AddWithValue("@balheight", @balheight);
        dCmd.Parameters.AddWithValue("@Color1", @Color1);
        dCmd.Parameters.AddWithValue("@Color2", @Color2);
        dCmd.Parameters.AddWithValue("@Color3", @Color3);
        dCmd.Parameters.AddWithValue("@Color4", @Color4);
        dCmd.Parameters.AddWithValue("@Color5", @Color5);
        dCmd.Parameters.AddWithValue("@Color6", @Color6);
        dCmd.Parameters.AddWithValue("@Hgt1", @Hgt1);
        dCmd.Parameters.AddWithValue("@Hgt2", @Hgt2);
        dCmd.Parameters.AddWithValue("@Hgt3", @Hgt3);
        dCmd.Parameters.AddWithValue("@Hgt4", @Hgt4);
        dCmd.Parameters.AddWithValue("@Hgt5", @Hgt5);
        dCmd.Parameters.AddWithValue("@Hgt6", @Hgt6);
        dCmd.Parameters.AddWithValue("@Qty1", @Qty1);
        dCmd.Parameters.AddWithValue("@Qty2", @Qty2);
        dCmd.Parameters.AddWithValue("@Qty3", @Qty3);
        dCmd.Parameters.AddWithValue("@Qty4", @Qty4);
        dCmd.Parameters.AddWithValue("@Qty5", @Qty5);
        dCmd.Parameters.AddWithValue("@Qty6", @Qty6);


        //--------------Step:4--------------------

        dCmd.Parameters.AddWithValue("@Venthole", @Venthole);
        dCmd.Parameters.AddWithValue("@VentRow", @VentRow);
        dCmd.Parameters.AddWithValue("@Letterbox", @Letterbox);
        dCmd.Parameters.AddWithValue("@ControlBox", @ControlBox);
        dCmd.Parameters.AddWithValue("@LockType", @LockType);
        dCmd.Parameters.AddWithValue("@OtherLckTyp", @OtherLckTyp);
        dCmd.Parameters.AddWithValue("@Voltage", @Voltage);
        dCmd.Parameters.AddWithValue("@Currnt", @Currnt);
        dCmd.Parameters.AddWithValue("@NamePlate", @NamePlate);
        dCmd.Parameters.AddWithValue("@Motor", @Motor);
        dCmd.Parameters.AddWithValue("@ManualOver", @ManualOver);
        dCmd.Parameters.AddWithValue("@RemoteBox", @RemoteBox);
        dCmd.Parameters.AddWithValue("@DoorOrein", @DoorOrein);
        dCmd.Parameters.AddWithValue("@UPSBattery", @UPSBattery);
        dCmd.Parameters.AddWithValue("@PullHandle", @PullHandle);
        dCmd.Parameters.AddWithValue("@PullHook", @PullHook);
        dCmd.Parameters.AddWithValue("@Packing", @Packing);
        dCmd.Parameters.AddWithValue("@WrntyDoor", @WrntyDoor);
        dCmd.Parameters.AddWithValue("@WrntyMotor", @WrntyMotor);
        dCmd.Parameters.AddWithValue("@SplRemark", @SplRemark);
        dCmd.Parameters.AddWithValue("@AluBottomBar", @AluBottomBar);
        dCmd.Parameters.AddWithValue("@NylonPoly", @NylonPoly);
        dCmd.Parameters.AddWithValue("@SendName", @SendName);
        dCmd.Parameters.AddWithValue("@SendMail", @SendMail);
        dCmd.Parameters.AddWithValue("@Tax", @Tax);
        dCmd.Parameters.AddWithValue("@TaxPrice", @TaxPrice);
        dCmd.Parameters.AddWithValue("@UnitPriceNoTax", @UnitPriceNoTax);

        dCmd.Parameters.AddWithValue("@createdby", @createdby);
        dCmd.Parameters.AddWithValue("@OrderAutoID", @OrderAutoID);
        dCmd.Parameters.AddWithValue("@flag", @flag);

        dCmd.Parameters.AddWithValue("@ClerkName", @ClerkName);
        dCmd.Parameters.AddWithValue("@ClerkMail", @ClerkMail);

        return dCmd.ExecuteNonQuery();
    }

    public static int SpringUpdate(SqlConnection conn, string @Quantity, string @MaterialName, string @useridp)
    {

        SqlCommand dCmd = new SqlCommand("[sp_Spring_Up]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@Quantity", @Quantity);
        dCmd.Parameters.AddWithValue("@MaterialName", @MaterialName);
        dCmd.Parameters.AddWithValue("@useridp", @useridp);

        return dCmd.ExecuteNonQuery();
    }

    public static int OrderFormSpringUpdate(SqlConnection conn, string @Pulley, string @Spring1, string @Spring1pcs, string @Spring2, string @Spring2pcs, string @Spring3, string @Spring3pcs, string @Spring4, string @Spring4pcs, string @Spring5, string @Spring5pcs, string @Spring6, string @Spring6pcs, string @SendName3, string @SendMail3, int @OrderAutoID, string @useridp)
    {

        SqlCommand dCmd = new SqlCommand("[sp_OrderSpring_Up]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@Pulley", @Pulley);
        dCmd.Parameters.AddWithValue("@Spring1", @Spring1);
        dCmd.Parameters.AddWithValue("@Spring1pcs", @Spring1pcs);
        dCmd.Parameters.AddWithValue("@Spring2", @Spring2);
        dCmd.Parameters.AddWithValue("@Spring2pcs", @Spring2pcs);
        dCmd.Parameters.AddWithValue("@Spring3", @Spring3);
        dCmd.Parameters.AddWithValue("@Spring3pcs", @Spring3pcs);
        dCmd.Parameters.AddWithValue("@Spring4", @Spring4);
        dCmd.Parameters.AddWithValue("@Spring4pcs", @Spring4pcs);
        dCmd.Parameters.AddWithValue("@Spring5", @Spring5);
        dCmd.Parameters.AddWithValue("@Spring5pcs", @Spring5pcs);
        dCmd.Parameters.AddWithValue("@Spring6", @Spring6);
        dCmd.Parameters.AddWithValue("@Spring6pcs", @Spring6pcs);
        dCmd.Parameters.AddWithValue("@SendName3", @SendName3);
        dCmd.Parameters.AddWithValue("@SendMail3", @SendMail3);
        dCmd.Parameters.AddWithValue("@OrderAutoID", @OrderAutoID);
        dCmd.Parameters.AddWithValue("@useridp", @useridp);

        return dCmd.ExecuteNonQuery();
    }


    public static int SaveIncoming(SqlConnection conn, string MaterialAutoid, string MaterialType, string Materialcode, string MaterialName, string Size, string StockDate, string Quantity, string createdby, string Flag, string IncomingAutoid)
    {

        SqlCommand dCmd = new SqlCommand("[sp_IncomingMaterial]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@MaterialAutoid", MaterialAutoid);
        dCmd.Parameters.AddWithValue("@MaterialType", MaterialType);
        dCmd.Parameters.AddWithValue("@Materialcode", Materialcode);
        dCmd.Parameters.AddWithValue("@MaterialName", MaterialName);
        dCmd.Parameters.AddWithValue("@Size", Size);
        dCmd.Parameters.AddWithValue("@StockDate", StockDate);
        dCmd.Parameters.AddWithValue("@Quantity", Quantity);
        dCmd.Parameters.AddWithValue("@createdby", createdby);
        dCmd.Parameters.AddWithValue("@Flag", Flag);
        dCmd.Parameters.AddWithValue("@IncomingAutoid", IncomingAutoid);


        return dCmd.ExecuteNonQuery();
    }

    public static int FinalInspection(SqlConnection conn, string @OrderAutoid, string @Status, string @DoorBoard, string @useridp, string @flag)
    {

        SqlCommand dCmd = new SqlCommand("[sp_FinalInsp_Ins]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@OrderAutoid", @OrderAutoid);
        dCmd.Parameters.AddWithValue("@Status", @Status);
        dCmd.Parameters.AddWithValue("@DoorBoard", @DoorBoard);
        dCmd.Parameters.AddWithValue("@useridp", @useridp);
        dCmd.Parameters.AddWithValue("@flag", @flag);


        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getMasterModule(SqlConnection conn)
    {
        int delval = 0;
        string sql = "select * FROM MasterModule WHERE Deleted='" + delval + "' ORDER BY ModuleName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getMasterModuleById(SqlConnection connect, string strModuleId)
    {
        int delval = 0;
        string sql = "select * FROM MasterModule WHERE Deleted='" + delval + "' and ModuleId='" + strModuleId + "' ORDER BY ModuleName";
        SqlCommand cmd = new SqlCommand(sql, connect);
        SqlDataReader reader1 = cmd.ExecuteReader();
        return reader1;

    }

    public static int DeleteModuleGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("sp_MasterModule_Delete", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@moduleidp", id);
        return dCmd.ExecuteNonQuery();
    }


    public static SqlDataReader checkModuleName(SqlConnection connCheck, string name)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterModule_IsDuplicate", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@modulenamep", name);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static int SaveModuleMaster(SqlConnection conn, string name, string desc, string appflag, string userid, string saveflag, string modid)
    {
        string sp_Name;
        string RowValue = "0";
        if (saveflag.ToString() == "N")
        {
            sp_Name = "[sp_MasterModule_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterModule_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (saveflag.ToString() == "U")
        {
            dCmd.Parameters.AddWithValue("@idp", modid);
            dCmd.Parameters.AddWithValue("@Rowp", RowValue);
        }
        dCmd.Parameters.AddWithValue("@namep", name);
        dCmd.Parameters.AddWithValue("@descriptionp", desc);
        dCmd.Parameters.AddWithValue("@approvalflag", appflag);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        return dCmd.ExecuteNonQuery();
    }
    public static SqlDataReader checkUserApprovalByUserId(SqlConnection connectUserAprvl, long lnguserid)
    {
        SqlCommand cmd = new SqlCommand("sp_MasterUserApproval_CheckUserId", connectUserAprvl);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@useridp", lnguserid);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }
    public static SqlDataReader checkUserLoginId(SqlConnection connCheck, string strLoginId)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_IsDuplicate]", connCheck);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@loginidp", strLoginId);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }
    public static int DeleteUserGrid(SqlConnection conn, string id)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUser_Delete]", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@masteruseridp", id);
        return dCmd.ExecuteNonQuery();
    }
    public static SqlDataReader getMasterModulePermisnByUserId(SqlConnection connModulePermission, string strUserId)
    {
        int delval = 0;
        string sql = "select * FROM vw_MasterModulePermission_MasterModuleByModuleID WHERE Deleted='" + delval + "' and UserId='" + strUserId.ToString() + "' order by modulename";
        SqlCommand cmd = new SqlCommand(sql, connModulePermission);
        SqlDataReader readerModulePermission = cmd.ExecuteReader();
        return readerModulePermission;
    }
    public static SqlDataReader getMasterUserByFilter(SqlConnection conn, string approverType, string platformId, string struserid)
    {
        int delval = 0;
        string sql = "";
        string strAppFlag = "N";
        if (approverType.ToString() == "Platform")
        {
            sql = "select * FROM MasterUser WHERE ID<>'" + struserid.ToString() + "' and Deleted='" + delval + "' and UserType='" + approverType.ToString() + "' and IsApprovalRqrd='" + strAppFlag.ToString() + "'  and PlatformId='" + platformId + "' ORDER BY UserName";
        }
        else
        {
            sql = "select * FROM MasterUser WHERE ID<>'" + struserid.ToString() + "' and Deleted='" + delval + "' and IsApprovalRqrd='" + strAppFlag.ToString() + "' and UserType='" + approverType.ToString() + "' ORDER BY UserName";
        }
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }
    public static SqlDataReader getMasterUserApprovalByFilter(SqlConnection connMasterUserApproval, string struserid, string approvaltype)
    {
        int delval = 0;
        string sql = "select * FROM vw_MasterApproval_MasterUserByApprovarID WHERE Deleted='" + delval + "' and UserId='" + struserid.ToString() + "' and UserType='" + approvaltype.ToString() + "'";
        SqlCommand cmd = new SqlCommand(sql, connMasterUserApproval);
        SqlDataReader readerMasterUserApproval = cmd.ExecuteReader();
        return readerMasterUserApproval;
    }

    public static SqlDataReader getMasterUserByID(SqlConnection conn, string strID)
    {
        int delval = 0;
        string sql = "select * FROM MasterUser WHERE ID='" + strID + "' and  Deleted='" + delval + "' ORDER BY UserName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    public static SqlDataReader getUserNameByID(SqlConnection conn, string strID)
    {
        SqlCommand cmd = new SqlCommand("[sp_MasterUser_getUserName]", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@idp", strID);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }
    public static int SaveUserMasterModulePermission(SqlConnection connSave, long intloginid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUserModulePermission_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@loginidp", intloginid);
        dCmd.Parameters.AddWithValue("@moduleidp", intlinebyline);
        dCmd.Parameters.AddWithValue("@appflag", "Y");
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }
    public static int SaveUserMasterApproval(SqlConnection connSave, long intloginid, long intlinebyline, string struserid)
    {
        SqlCommand dCmd = new SqlCommand("[sp_MasterUserApproval_Save]", connSave);
        dCmd.CommandType = CommandType.StoredProcedure;

        dCmd.Parameters.AddWithValue("@loginidp", intloginid);
        dCmd.Parameters.AddWithValue("@approvalp", intlinebyline);
        dCmd.Parameters.AddWithValue("@useridp", struserid);
        return dCmd.ExecuteNonQuery();
    }
    public static int SaveUserMaster(SqlConnection connSave, string strloginid, string strpass, string strdesign, string strname, string strdept, string strmail, string strusertype, long strPlatformid, string strapprqrd, string strnotifyrqrd, string strCreatedByID, string strSaveFlag, string strCurrUserId)
    {
        string sp_Name;
        if (strSaveFlag.ToString() == "Insert")
        {
            sp_Name = "[sp_MasterUser_Insert]";
        }
        else
        {
            sp_Name = "[sp_MasterUser_Update]";
        }
        SqlCommand dCmd = new SqlCommand(sp_Name, connSave);
        dCmd.CommandType = CommandType.StoredProcedure;
        if (strSaveFlag.ToString() == "Update")
        {
            dCmd.Parameters.AddWithValue("@idp", strCurrUserId);
        }
        dCmd.Parameters.AddWithValue("@loginidp", strloginid);
        dCmd.Parameters.AddWithValue("@passp", strpass);
        dCmd.Parameters.AddWithValue("@designp", strdesign);
        dCmd.Parameters.AddWithValue("@namep", strname);
        dCmd.Parameters.AddWithValue("@deptp", strdept);
        dCmd.Parameters.AddWithValue("@mailp", strmail);
        dCmd.Parameters.AddWithValue("@usertypep", strusertype);
        dCmd.Parameters.AddWithValue("@platformidp", strPlatformid);
        dCmd.Parameters.AddWithValue("@isapprovalrqrdp", strapprqrd);
        dCmd.Parameters.AddWithValue("@isnotifyrqrd", strnotifyrqrd);
        dCmd.Parameters.AddWithValue("@useridp", strCreatedByID);
        return dCmd.ExecuteNonQuery();
    }

    public static int OrderFormUpdate(SqlConnection connLog, string Approval, string SendName1, string SendMail1, string useridp, string OrderAutoID)
    {
        SqlCommand dCmd = new SqlCommand("[sp_OrderForm_Up]", connLog);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@Approval", Approval);
        dCmd.Parameters.AddWithValue("@SendName1", SendName1);
        dCmd.Parameters.AddWithValue("@SendMail1", SendMail1);
        dCmd.Parameters.AddWithValue("@useridp", useridp);
        dCmd.Parameters.AddWithValue("@OrderAutoID", OrderAutoID);
        return dCmd.ExecuteNonQuery();
    }

    public static SqlDataReader getPlatformInfoById(SqlConnection getPlatformconn, string platformid)
    {
        int delval = 0;
        string sql = "select * FROM MasterPlatform WHERE PlatformId='" + platformid + "' and  Deleted='" + delval + "' ORDER BY PlatformName";
        SqlCommand cmd = new SqlCommand(sql, getPlatformconn);
        SqlDataReader getPlatformreader = cmd.ExecuteReader();
        return getPlatformreader;
    }
    public static SqlDataReader getMasterUserByLoginId(SqlConnection conn, string strLoginId)
    {
        int delval = 0;
        string sql = "select * FROM MasterUser WHERE Deleted='" + delval + "' and LoginId='" + strLoginId + "' ORDER BY UserName";
        SqlCommand cmd = new SqlCommand(sql, conn);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }
    //public static int SaveModuleMaster(SqlConnection conn, string name, string cfname, string desc, string appflag, string userid, string saveflag, string modid)
    //{
    //    string sp_Name;
    //    string RowValue = "0";
    //    if (saveflag.ToString() == "N")
    //    {
    //        sp_Name = "[sp_MasterModule_Insert]";
    //    }
    //    else
    //    {
    //        sp_Name = "[sp_MasterModule_Update]";
    //    }
    //    SqlCommand dCmd = new SqlCommand(sp_Name, conn);
    //    dCmd.CommandType = CommandType.StoredProcedure;
    //    if (saveflag.ToString() == "U")
    //    {
    //        dCmd.Parameters.AddWithValue("@idp", modid);
    //        dCmd.Parameters.AddWithValue("@Rowp", RowValue);
    //    }
    //    dCmd.Parameters.AddWithValue("@namep", name);
    //    dCmd.Parameters.AddWithValue("@cfnamep", cfname);
    //    dCmd.Parameters.AddWithValue("@descriptionp", desc);
    //    dCmd.Parameters.AddWithValue("@approvalflag", appflag);
    //    dCmd.Parameters.AddWithValue("@useridp", userid);
    //    return dCmd.ExecuteNonQuery();
    //}


    public static int SaveNewKitchen(SqlConnection conn, string kitchname, string descrp, int restid, int userid, string saveflag, int kitchid)
    {
        SqlCommand dCmd = new SqlCommand("sp_Kitchen_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@kitchnamep", kitchname);
        dCmd.Parameters.AddWithValue("@descrpp", descrp);
        dCmd.Parameters.AddWithValue("@restidp", restid);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@Flagp", saveflag);
        dCmd.Parameters.AddWithValue("@kitchidp", kitchid);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveNewtable(SqlConnection conn, string tblno, string descrp, int restid, int userid, string saveflag, int tblmstrid)
    {
        SqlCommand dCmd = new SqlCommand("sp_Table_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@tblnoep", tblno);
        dCmd.Parameters.AddWithValue("@descrpp", descrp);
        dCmd.Parameters.AddWithValue("@restidp", restid);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@Flagp", saveflag);
        dCmd.Parameters.AddWithValue("@tblmstridp", tblmstrid);
        return dCmd.ExecuteNonQuery();
    }



    public static int Savemenutable(SqlConnection conn, string name, string descrp, int price, int discount, int category, string aval, int userid, string saveflag, int tblmenuid, string guid, byte pic)
    {
        SqlCommand dCmd = new SqlCommand("sp_Menu_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@menunamep", name);
        dCmd.Parameters.AddWithValue("@descrp", descrp);
        dCmd.Parameters.AddWithValue("@pricep", price);
        dCmd.Parameters.AddWithValue("@discountp", discount);
        dCmd.Parameters.AddWithValue("@categoryp", category);
        dCmd.Parameters.AddWithValue("@avalp", aval);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@Flagp", saveflag);
        dCmd.Parameters.AddWithValue("@menumstridp", tblmenuid);
        dCmd.Parameters.AddWithValue("@guidp", guid);
        //dCmd.Parameters.AddWithValue("@picp", pic);
        dCmd.Parameters.Add("@Picture", SqlDbType.VarBinary);
        dCmd.Parameters["@Picture"].Value = pic;
        dCmd.Parameters.AddWithValue("@picp", pic);
        return dCmd.ExecuteNonQuery();
    }

    public static int SaveNewTax(SqlConnection conn, string taxname, int perct, int aplicab, int userid, string saveflag, int taxtblid)
    {
        SqlCommand dCmd = new SqlCommand("sp_Tax_Save", conn);
        dCmd.CommandType = CommandType.StoredProcedure;
        dCmd.Parameters.AddWithValue("@taxnamep", taxname);
        dCmd.Parameters.AddWithValue("@perctp", perct);
        dCmd.Parameters.AddWithValue("@aplicabp", aplicab);
        dCmd.Parameters.AddWithValue("@useridp", userid);
        dCmd.Parameters.AddWithValue("@Flagp", saveflag);
        dCmd.Parameters.AddWithValue("@taxtblidp", taxtblid);
        return dCmd.ExecuteNonQuery();
    }



    public static string getCCMailID(string strModule)
    {
        string strEmailFile = ConfigurationManager.AppSettings["Email_CC_FilePath"].ToString();
        //   string strMailCC = "Default@petronas.com.my";
        string strMailCC = "sara@e-serbadk.com";

        if (File.Exists(strEmailFile))
        {
            string strLine = "";
            string[] strLine1 = new string[1];
            int counter = 0;
            StreamReader reader = new StreamReader(strEmailFile);
            while ((strLine = reader.ReadLine()) != null)
            {
                if (counter == 0)
                {
                    strLine1 = strLine.Split(':');

                    if (strLine1[0].ToString().Trim() == strModule.ToString().Trim())
                    {
                        strMailCC = strLine1[1].ToString().Trim();
                        counter = 1;
                    }
                }
            }
            reader.Close();
            reader.Dispose();
        }
        return strMailCC.ToString().Trim();
    }

    public static void SendMail(string strSubject, string strBody, string strToAddress, string strApprovarMail, string strAttachmentFilename)
    {
        SmtpClient smtpClient = new SmtpClient();
        MailMessage message = new MailMessage();
        if (!(strAttachmentFilename.ToString().Trim() == "NoAttach"))
        {
            Attachment attachment = new Attachment(strAttachmentFilename.ToString().Trim());
            message.Attachments.Add(attachment);
        }
        MailAddress fromAddress = new MailAddress(ConfigurationManager.AppSettings["FromAddress"].ToString(), "Vessel Information System");
        smtpClient.Host = ConfigurationManager.AppSettings["ExchangeServer"].ToString();
        smtpClient.Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());

        message.Priority = MailPriority.High;
        message.From = fromAddress;
        message.Subject = strSubject.ToString();
        message.To.Add(strToAddress.ToString());
        message.CC.Add(strApprovarMail.ToString());
        message.Body = strBody;
        //smtpClient.EnableSsl = true;
        //smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["FromAddress"].ToString(), ConfigurationManager.AppSettings["Password"].ToString().Trim());
        //smtpClient.UseDefaultCredentials = true;
        smtpClient.Send(message);
        message.Dispose();
        smtpClient.Dispose();
        File.Delete(strAttachmentFilename.ToString().Trim());
    }

    public static SqlDataReader getCustMailbyID(SqlConnection conn1, int intCustID)
    {
        SqlCommand cmd = new SqlCommand("[sp_CustomerMail_CustID]", conn1);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@custidp", intCustID);
        SqlDataReader reader = cmd.ExecuteReader();
        return reader;
    }

    //-----------
    public static Boolean ExecuteInsertUpdateQry(string sql)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        SqlCommand comm = new SqlCommand(sql, conn);
        int ret = comm.ExecuteNonQuery();
        conn.Close();
        if (ret == 1)
        {
            return true;
        }
        {
            return false;
        }
    }
    public static Boolean ExecuteDeleteQry(string sql)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        SqlCommand comm = new SqlCommand(sql, conn);
        int ret = comm.ExecuteNonQuery();
        conn.Close();
        if (ret == 1)
        {
            return true;
        }
        {
            return false;
        }
    }
    public static ArrayList getPendingOrderIDsUsingUID(string UID, string dtpckrselect)
    {
        DateTime selectdt = Convert.ToDateTime(dtpckrselect);
        ArrayList al = new ArrayList();
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        string sql = "Select OrderID from dbo.OrderMaster where Table_ID = " + UID + " and Status = 'P' and Deleted='0' and REPLACE(CONVERT(VARCHAR,OrdDate,106),' ','/') = '" + selectdt.ToString("dd/MMM/yyyy") + "'";
        SqlCommand comm = new SqlCommand(sql, conn);
        SqlDataReader dr = comm.ExecuteReader();
        while (dr.Read())
        {
            al.Add(dr["OrderID"].ToString());
        }
        return al;
    }

    public static DataSet getOrderDetailsUsingOrderID(string OID)
    {
        SqlConnection conn = BusinessTier.getConnection();
        conn.Open();
        String sql = "Select MM.Name as Item, MM.Description as Descr, OD.Qty as Qty, OD.Price, OD.DetailID as DetailID, OD.Split as Split ";
        sql = sql + "from dbo.orderDetail OD, dbo.menuMaster MM ";
        sql = sql + "Where OD.OrderID = " + OID + " and OD.MenuID = MM.ID and OD.Deleted=0";
        SqlCommand comm = new SqlCommand(sql, conn);
        SqlDataAdapter da = new SqlDataAdapter(comm);
        DataSet ds = new DataSet();
        da.Fill(ds, "ORDERITEMS");
        conn.Close();
        return ds;
    }
    public static SqlCommand GetCommand(SqlConnection conn, String sql)
    {
        SqlCommand comm = new SqlCommand(sql, conn);
        return comm;
    }
    public static String GettotQty(string id)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "select Qty, OrderID from dbo.ORDERDETAIL  where deleted=0 and DetailID = " + id);
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        return dr["Qty"].ToString();
    }
    public static String GetUnitPrice(string id)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select UnitPrice from dbo.menuMaster where ID = " + id);
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        return dr["UnitPrice"].ToString();
    }
    public static double RoundUp(double figure, int precision)
    {
        double newFigure = Math.Round(figure / 5, precision) * 5;
        return newFigure;
    }
    public static String GetOrderDetailPrice(string id)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select Price from dbo.orderDetail where  deleted=0 and DetailID = " + id);
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        return dr["Price"].ToString();
    }

    public static Double GetTax(string ordid, double ordPrice)
    {
        Double ret, per;
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select sum(Percentage) as perc from dbo.taxMaster where deleted=0 and Flag = 'Y'");
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        per = Convert.ToDouble(dr["perc"].ToString());
        ret = (ordPrice * (per / 100));
        //Decimal rnd = 2;
        //ret = Convert.ToDouble(RoundUp(Convert.ToDecimal(ret), rnd));
        return ret;
    }

    public static Double GetDiscount(string menuID, double ordPrice)
    {
        Double ret, dis;
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select Discount from dbo.menuMaster where deleted=0 and ID = " + menuID);
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        if (dr["Discount"].ToString() != "")
            dis = Convert.ToDouble(dr["Discount"]);
        else
            dis = 0;
        ret = (ordPrice * (dis / 100));
        return ret;
    }

    public static Double GetOrderPrice(string id)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select sum(Price) as Price from dbo.orderDetail where deleted=0 and OrderID = " + id);
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        return Convert.ToDouble(dr["Price"]);
    }

    public static Double GetDeductionFromOrderDetail(string id)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select Deduction from dbo.orderDetail where deleted=0 and DetailID = " + id);
        SqlDataReader dr = comm.ExecuteReader();
        dr.Read();
        return Convert.ToDouble(dr["Deduction"]);
    }

    public static void UpdateTaxAdjustment(string taxAdj, string taxOrg, string OID)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "Update dbo.OrderMaster set TaxAdj = '" + taxAdj + "', TaxOrg = '" + taxOrg + "' Where OrderID = " + OID;
        SqlCommand comm = GetCommand(conn, sql);
        int ret = comm.ExecuteNonQuery();
        conn.Close();
    }
    public static void UpdateDeductionInOrderMaster(string OID, string TotDed)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        string sql = "Update dbo.OrderMaster set TotDed = " + TotDed + " Where OrderID = " + OID;
        SqlCommand comm = GetCommand(conn, sql);
        int ret = comm.ExecuteNonQuery();
        conn.Close();
    }

    public static DataSet getOrderRecieptDetails(string OID)
    {
        SqlConnection conn = getConnection();
        //String sql = "Select OM.*, UM.UID ";
        //sql = sql + "from dbo.orderMaster OM, dbo.userMaster UM ";    
        //sql = sql + "Where OM.OrderID = " + OID + " and cast(OM.UID as varchar) = UM.UID ";

        String sql = "Select OM.*, UM.user_aid ";
        sql = sql + "from dbo.orderMaster OM, dbo.Users_tbl UM ";
        sql = sql + "Where OM.OrderID = " + OID + " and OM.status='P' and OM.Deleted='0'";

        SqlCommand comm = GetCommand(conn, sql);
        SqlDataAdapter da = new SqlDataAdapter(comm);
        DataSet ds = new DataSet();
        da.Fill(ds, "ORDER");
        conn.Close();
        return ds;
    }
    public static Double GetDeductionFromOrderMaster(string id)
    {
        SqlConnection conn = getConnection();
        conn.Open();
        SqlCommand comm = GetCommand(conn, "Select TotDed from dbo.orderMaster where deleted=0 and OrderID = " + id);
        SqlDataReader dr = comm.ExecuteReader();
        if (dr.Read())
        {
            // dr.Read();
            if (dr["TotDed"] == "")
            {
                return Convert.ToDouble(0);
            }
            else
            {
                return Convert.ToDouble(dr["TotDed"].ToString());
            }
        }
        else
        {
            return Convert.ToDouble(0);
        }

    }


    public static int SaveOrderForm(SqlConnection conn, string p, string p_2, string p_3, string p_4, string p_5, string p_6, string p_7, string p_8, string p_9, string p_10, string p_11, string p_12, string p_13, string p_14, string p_15, string p_16, string p_17, string p_18, string p_19, string p_20, string p_21, string p_22, string p_23, string p_24, string p_25, string p_26, string p_27, string p_28, string p_29, string p_30, string p_31, string p_32, string p_33, string p_34, string p_35, string p_36, string p_37, string p_38, string p_39, string p_40, string p_41, string p_42, string p_43, string p_44, string p_45, string p_46, string p_47, string p_48, string p_49, string p_50, string p_51, string p_52, string p_53, string p_54, string p_55, string p_56, string p_57, string p_58, string p_59, string p_60, string p_61, string p_62, string p_63, string p_64, string p_65, string p_66, string p_67, string p_68, string p_69, string p_70, string p_71, string p_72, string p_73, int p_74, int p_75, string p_76)
    {
        throw new NotImplementedException();
    }
}