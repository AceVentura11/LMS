using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class edit_contact : System.Web.UI.Page
{
    #region DECLARATION

    encrypt en = new encrypt();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
    DataSet ds = new DataSet();

    #endregion

    #region METHODS

    protected void Page_Load(object sender, EventArgs e)
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
                con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
                contactid.Value = Request.QueryString["id"].ToString();
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM ContactSheet WHERE ID = " + en.Decryption(contactid.Value), con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                }
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string DbType = ds.Tables[0].Rows[0]["DbType"].ToString();
                        BindClientDropdown(DbType);
                        BindDropdown();
                        if (DbType == "N") { rb_newclient.Checked = true; } else { rb_currentclient.Checked = true; }
                        txt_clientname.Value = ds.Tables[0].Rows[0]["ClientName"].ToString();
                        ddl_office.Value = ds.Tables[0].Rows[0]["Office"].ToString();
                        txt_division.Value = ds.Tables[0].Rows[0]["Division"].ToString();
                        txt_branchlocation.Value = ds.Tables[0].Rows[0]["BranchLocation"].ToString();
                        txt_clientcontact.Value = ds.Tables[0].Rows[0]["ClientContact"].ToString();
                        txt_boardline.Value = ds.Tables[0].Rows[0]["Boardline"].ToString();
                        txt_directline.Value = ds.Tables[0].Rows[0]["DirectLine"].ToString();
                        txt_mobile.Value = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        txt_emailid.Value = ds.Tables[0].Rows[0]["EmailID"].ToString();
                        txt_function.Value = ds.Tables[0].Rows[0]["Function"].ToString();
                        txt_function_others.Value = ds.Tables[0].Rows[0]["FunctionOthers"].ToString();
                        txt_designation.Value = ds.Tables[0].Rows[0]["Designation"].ToString();
                        ddl_gender.Value = ds.Tables[0].Rows[0]["Gender"].ToString();
                        ddl_industryvertical.Value = ds.Tables[0].Rows[0]["IndustryVertical"].ToString();
                        if(ddl_industryvertical.Value=="")
                        {
                            SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());

                            string query1 = "SELECT IPG FROM clientmaster where clientname='" + txt_clientname.Value + "'";
                            try
                            {
                                DataSet ds1 = new DataSet();
                                using (con1)
                                {
                                    con1.Open();
                                    SqlCommand cmd1 = new SqlCommand(query1, con1);
                                    cmd1.CommandType = CommandType.Text;
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    da1.Fill(ds1);
                                    con1.Close();

                                    if (ds1.Tables[0].Rows.Count > 0)
                                    {
                                        ddl_industryvertical.Value = ds1.Tables[0].Rows[0]["IPG"].ToString();
                                    }
                                    else
                                    {
                                        ddl_industryvertical.Disabled = false;

                                    }
                                }
                            }
                            catch
                            {

                            }
                            finally
                            {
                                con1.Close();
                            }
                        }
                    }
                }
            }
        }
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        using (con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATECONTACT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            if (new_client.Value != "") { cmd.Parameters.AddWithValue("@ClientName", new_client.Value); } else { cmd.Parameters.AddWithValue("@ClientName", txt_clientname.Value); }
            if (rb_currentclient.Checked == true) { cmd.Parameters.AddWithValue("@DbType", "C"); } else { cmd.Parameters.AddWithValue("@DbType", "N"); }
            cmd.Parameters.AddWithValue("@Office", ddl_office.Value);
            cmd.Parameters.AddWithValue("@Division", txt_division.Value);
            cmd.Parameters.AddWithValue("@BranchLocation", txt_branchlocation.Value);
            if (txt_clientcontact.Value == "Other") { cmd.Parameters.AddWithValue("@ClientContact", txt_clientcontact_new.Value); } else { cmd.Parameters.AddWithValue("@ClientContact", txt_clientcontact.Value); }
            cmd.Parameters.AddWithValue("@Boardline", txt_boardline.Value);
            cmd.Parameters.AddWithValue("@DirectLine", txt_directline.Value);
            cmd.Parameters.AddWithValue("@Mobile", txt_mobile.Value);
            cmd.Parameters.AddWithValue("@EmailID", txt_emailid.Value);
            cmd.Parameters.AddWithValue("@Function", txt_function.Value);
            cmd.Parameters.AddWithValue("@FunctionOthers", txt_function_others.Value);
            cmd.Parameters.AddWithValue("@Designation", txt_designation.Value);
            cmd.Parameters.AddWithValue("@Gender", ddl_gender.Value);
            cmd.Parameters.AddWithValue("@IndustryVertical", ddl_industryvertical.Value);
            cmd.Parameters.AddWithValue("@ID", en.Decryption(contactid.Value));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Contact updated successfully.'); window.location.href = 'list.aspx'", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Error saving contact.'); window.location.href = 'list.aspx'", true);
            }
        }
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
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT ClientContact FROM ContactSheet ORDER BY ClientContact", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
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

    protected void BindClientDropdown(string DbType)
    {

        string query = "";
        SqlConnection con = new SqlConnection();
        txt_clientname.Items.Add(new ListItem("Select", ""));
        if (DbType == "N")
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            query = "SELECT DISTINCT(ClientName) as clientname FROM ContactSheet ORDER BY ClientName";
            txt_clientname.Items.Add(new ListItem("Add New Client", "New"));
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

    #endregion
}