using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RightsExchange : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
    public string AccessUId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loaddata();
        }
    }
    protected void loaddata()
    {
        try
        {


            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("SELECT USERNAME,ID FROM login where USERNAME!='' ORDER BY USERNAME", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            ddlusername1.DataSource = ds;
            ddlusername1.DataTextField = "USERNAME";
            ddlusername1.DataValueField = "ID";
            ddlusername1.DataBind();
            ddlusername1.Items.Insert(0, new ListItem("Select User Name", ""));
        }
        catch (Exception)
        {

            //throw;
        }

        try
        {


            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("SELECT USERNAME,ID FROM login where USERNAME!='' ORDER BY USERNAME", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            ddlusername2.DataSource = ds;
            ddlusername2.DataTextField = "USERNAME";
            ddlusername2.DataValueField = "ID";
            ddlusername2.DataBind();
            ddlusername2.Items.Insert(0, new ListItem("Select User Name", ""));
        }
        catch (Exception)
        {

            //throw;
        }

    }
    protected string usercheck()
    {
        string temp = "0";
        AccessUId = "";
        DataSet ds = new DataSet();
        //con.Open();
        try
        {

            SqlCommand obj = new SqlCommand("Select * from RightsExchange where UID=@USERID", con);
            obj.CommandType = CommandType.Text;
            obj.Parameters.AddWithValue("@USERID", ddlusername1.Value);
            SqlDataAdapter da = new SqlDataAdapter(obj);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                temp = ds.Tables[0].Rows[0]["ID"].ToString();
               AccessUId= ds.Tables[0].Rows[0]["AccessedUID"].ToString();
            }

        }
        catch (Exception ex)
        {

            throw ex;
        }

        return temp;
    }

    protected void ddlusername1_ServerChange(object sender, EventArgs e)
    {
        try
        {


            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("SELECT USERNAME,ID FROM login where USERNAME!='' and USERTYPE='R' ORDER BY USERNAME", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            ddlusername2.DataSource = ds;
            ddlusername2.DataTextField = "USERNAME";
            ddlusername2.DataValueField = "ID";
            ddlusername2.DataBind();
            ddlusername2.Items.Insert(0, new ListItem("Select User Name", ""));
        }
        catch (Exception)
        {

            //throw;
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlusername1.Value != "" && ddlusername2.Value != "")
            {
                if (ddlusername1.Value!= ddlusername2.Value)
                {
                    string  updateid = usercheck();
                    if (updateid == "0")
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("Insert Into RightsExchange(UID,AccessedUID,Createdby) values ('" + ddlusername1.Value + "','" + ddlusername2.Value + "','" + Session["uid"].ToString() + "')  ", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        con.Open();
                        string tempAccessUid = AccessUId + "," + ddlusername2.Value;
                        SqlCommand cmd1 = new SqlCommand("update RightsExchange set AccessedUID='"+tempAccessUid+"' ,Createdby='" + Session["uid"].ToString() + "' where ID='"+updateid+"' ", con);
                        cmd1.CommandType = CommandType.Text;
                        cmd1.ExecuteNonQuery();
                        con.Close();
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Data Saved successfully. ');", true);
                    loaddata();
                }
                else
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select different user names ');", true);
            }
            else
            {
                if (ddlusername1.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select first User Name ');", true);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select second User Name');", true);

                }

            }
        }
        catch (Exception ex)
        {

            //throw;
        }
    }

    [System.Web.Services.WebMethod]
    public static string fillusername2(string type)
    {

        string d = "", query = "";
        d += "<option value=''>Select</option>";
        SqlConnection con = new SqlConnection();
        
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            query = "SELECT USERNAME,ID FROM login where USERNAME!='' and USERTYPE='R' and ID!='"+type+"' ORDER BY USERNAME";

        if (type != "")
        {

            try
            {
                DataSet ds = new DataSet();
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        d += "<option value='" + ds.Tables[0].Rows[i]["ID"].ToString() + "'>" + ds.Tables[0].Rows[i]["USERNAME"].ToString() + "</option>";
                    }
                }
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
        }

        return d;
    }
}