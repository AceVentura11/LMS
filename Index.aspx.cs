
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    #region DECLARATION

    encrypt en = new encrypt();

    #endregion

    #region METHODS

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["logname"] == null)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["url_referer"] = url;
                Response.Redirect("Default.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    //DC 04-07-2019 BindDropdown();
                    BindClientDropdown();
                }

            }
        }
        catch
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            DataSet ds = new DataSet();
            string check = CheckClientName(txt_clientname.Value, txt_emailid.Value, txt_clientcontact.Value);
            if (check == "Not Exists")
            {
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERTCONTACT", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (new_client.Value != "")
                    { cmd.Parameters.AddWithValue("@ClientName", new_client.Value); }
                    else if (txt_clientname.Value != "")
                    { cmd.Parameters.AddWithValue("@ClientName", txt_clientname.Value); }
                    else
                    {goto a;
                    }
                    if (rb_currentclient.Checked == true)
                    { cmd.Parameters.AddWithValue("@DbType", "C"); }
                    else
                    { cmd.Parameters.AddWithValue("@DbType", "N"); }
                    cmd.Parameters.AddWithValue("@Office", ddl_office.Value);
                    cmd.Parameters.AddWithValue("@Division", txt_division.Value);
                    cmd.Parameters.AddWithValue("@BranchLocation", txt_branchlocation.Value);
                    if (txt_clientcontact.Value == "Other")
                    { cmd.Parameters.AddWithValue("@ClientContact", txt_clientcontact_new.Value); }
                    else
                    { cmd.Parameters.AddWithValue("@ClientContact", txt_clientcontact.Value); }
                    cmd.Parameters.AddWithValue("@Boardline", txt_boardline.Value);
                    cmd.Parameters.AddWithValue("@DirectLine", txt_directline.Value);
                    cmd.Parameters.AddWithValue("@Mobile", txt_mobile.Value);
                    cmd.Parameters.AddWithValue("@EmailID", txt_emailid.Value);
                    cmd.Parameters.AddWithValue("@Function", txt_function.Value);
                    cmd.Parameters.AddWithValue("@FunctionOthers", txt_function_others.Value);
                    cmd.Parameters.AddWithValue("@Designation", txt_designation.Value);
                    cmd.Parameters.AddWithValue("@Gender", ddl_gender.Value);
                    cmd.Parameters.AddWithValue("@IndustryVertical", ddl_industryvertical.Value);
                    cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Contact saved successfully.'); window.location.href = 'list.aspx'", true);
                        goto b;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Error saving contact.'); window.location.href = 'list.aspx'", true);
                        goto b;
                    }
                    a:{
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Please select Add new Client then Enter new Client Name.');", true);
                    }
                    b:{
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Client Name already exists');", true);
            }
        }
        catch
        {

        }
    }

    protected string CheckClientName(string clientname, string emailid, string contactname)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        DataSet ds = new DataSet();
        string clientvalidate = "Not Exists";
        using (con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("CHECKCLIENTNAME", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ClientName", clientname);
            cmd.Parameters.AddWithValue("@ClientEmail", emailid);
            cmd.Parameters.AddWithValue("@ContactName", contactname);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                clientvalidate = "Exists";
            }
        }
        return clientvalidate;
    }


    #endregion

    #region FUNCTIONS

    protected void BindDropdown()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        try
        {
            DataSet ds = new DataSet();
            using (con)
            {
                con.Open();
                //SqlCommand cmd = new SqlCommand("SELECT DISTINCT ClientContact FROM ContactSheet ORDER BY ClientContact", con);
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT ClientContact FROM ContactSheet where ClientName='" + txt_clientname.Value + "'  ORDER BY ClientContact", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                ddl_clientcontact.Items.Clear();
                ddl_clientcontact.Items.Add(new ListItem("Select", ""));
                ddl_clientcontact.Items.Add(new ListItem("Add New Contact", "Other"));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddl_clientcontact.Items.Add(new ListItem(ds.Tables[0].Rows[i]["ClientContact"].ToString(), ds.Tables[0].Rows[i]["ClientContact"].ToString()));
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

    protected void BindClientDropdown()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
        try
        {
            DataSet ds = new DataSet();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT clientname FROM clientmaster ORDER BY clientname", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                txt_clientname.Items.Add(new ListItem("Select", ""));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_clientname.Items.Add(new ListItem(ds.Tables[0].Rows[i]["clientname"].ToString(), ds.Tables[0].Rows[i]["clientname"].ToString()));
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


    #endregion

    #region WEBSERVICE

    [System.Web.Services.WebMethod]
    public static string GetClientDetails(string type)
    {
        string d = "", query = "";
        d += "<option value=''>Select</option>";
        SqlConnection con = new SqlConnection();
        if (type == "new")
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            query = "SELECT DISTINCT(ClientName) as clientname FROM ContactSheet ORDER BY ClientName";
            d += "<option value='New'>Add New Client</option>";
        }
        else
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
            query = "SELECT clientname FROM clientmaster ORDER BY clientname";
        }


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
                    d += "<option value='" + ds.Tables[0].Rows[i]["clientname"].ToString() + "'>" + ds.Tables[0].Rows[i]["clientname"].ToString() + "</option>";
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

        return d;
    }
    [System.Web.Services.WebMethod]
    public static string GetClientDetails1(string type)
    {

        string d = "";

        if (HttpContext.Current != null)
        {


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());

            string query = "SELECT IPG FROM clientmaster where clientname='" + type + "'";
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

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        d = ds.Tables[0].Rows[0]["IPG"].ToString();
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

    [System.Web.Services.WebMethod]
    public static string GetClientContactDetails(string type)
    {
        string d = "", query = "";
        d += "<option value=''>Select</option>";
        SqlConnection con = new SqlConnection();

        con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        query = "SELECT DISTINCT ClientContact FROM ContactSheet where ClientName='" + type + "'  ORDER BY ClientContact";
        d += "<option value='Other'>Add New Contact</option>";


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
                    d += "<option value='" + ds.Tables[0].Rows[i]["ClientContact"].ToString() + "'>" + ds.Tables[0].Rows[i]["ClientContact"].ToString() + "</option>";
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

        return d;
    }

    #endregion

}