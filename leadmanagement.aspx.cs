using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leadmanagement : System.Web.UI.Page
{
    #region DECLARATION

    encrypt en = new encrypt();
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
    DataSet ds = new DataSet();

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
                BindTeamHeadDropdown();
                BindDropdown();
                BindCCDropdown();
                BindViaDropdown();
                BindData(Request.QueryString["id"].ToString());
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
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            using (con)
            {
                string id = Request.QueryString["id"].ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERTLEADMANAGEMENT", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactID", id);
                cmd.Parameters.AddWithValue("@ProposalName", txt_proposalname.Value);
                cmd.Parameters.AddWithValue("@ProposalDueDate", proposalduedate.Value);
                cmd.Parameters.AddWithValue("@Dateofreceivinglead", txt_dateofreceivinglead.Value);
                cmd.Parameters.AddWithValue("@Organisation", ddl_organisation.Value);
                cmd.Parameters.AddWithValue("@ClientContact", txt_clientcontact.Value);
                cmd.Parameters.AddWithValue("@Location", txt_location.Value);
                cmd.Parameters.AddWithValue("@Designation", txt_designation.Value);
                //DC 24-05-2019 cmd.Parameters.AddWithValue("@IndustryVertical", ddl_industryvertical.Value);
                cmd.Parameters.AddWithValue("@IndustryVertical", txt_industryvertical.Value);
                cmd.Parameters.AddWithValue("@LeadGenerator", txt_leadgenerator.Value);
                cmd.Parameters.AddWithValue("@Via", txt_via.Value);
                cmd.Parameters.AddWithValue("@Allotedto", txt_allotedto.Value);
                cmd.Parameters.AddWithValue("@TeamHead", txt_teamhead.Value);
                cmd.Parameters.AddWithValue("@EstimateValue", txt_estimatevalue.Value);
                cmd.Parameters.AddWithValue("@ImportantClient", ddl_importantclient.Value);
                cmd.Parameters.AddWithValue("@Relationship", ddl_relationship.Value);
                cmd.Parameters.AddWithValue("@BriefQuality", ddl_briefquality.Value);
                cmd.Parameters.AddWithValue("@Comments", txt_comments.Value);
                cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Lead details saved successfully.'); window.location.href = 'list.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Error saving lead details.'); window.location.href = 'list.aspx'", true);
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

    #region Functions

    protected void BindTeamHeadDropdown()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
            using (con)
            {
                DataSet ds = new DataSet();
                con.Open();
                //DC 16-06-2020 SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEEMASTER WHERE employeetype = 'TeamHead' order by employeename", con);
                SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEEMASTER WHERE employeetype = 'TeamHead' and employeename!='Anjan Ghosh' order by employeename", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                txt_teamhead.Items.Add(new ListItem("Select", ""));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_teamhead.Items.Add(new ListItem(ds.Tables[0].Rows[i]["employeename"].ToString(), ds.Tables[0].Rows[i]["employeename"].ToString()));
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

    protected void BindViaDropdown()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
            using (con)
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEEMASTER WHERE Employmenttype = 'Permanent' ORDER BY employeename; SELECT * FROM EMPLOYEEMASTER WHERE employeetype = 'TeamHead' ORDER BY employeename", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                txt_leadgenerator.Items.Add(new ListItem("Select", ""));
                txt_allotedto.Items.Add(new ListItem("Select", ""));
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_leadgenerator.Items.Add(new ListItem(ds.Tables[0].Rows[i]["employeename"].ToString(), ds.Tables[0].Rows[i]["employeename"].ToString()));
                }
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    txt_allotedto.Items.Add(new ListItem(ds.Tables[1].Rows[i]["employeename"].ToString(), ds.Tables[1].Rows[i]["employeename"].ToString()));
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

    protected void BindData(string ID)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        DataSet ds = new DataSet();
        using (con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM ContactSheet WHERE ID = '" + ID + "'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_organisation.Value = ID;
                    txt_clientcontact.Value = ds.Tables[0].Rows[0]["ClientContact"].ToString();
                    txt_location.Value = ds.Tables[0].Rows[0]["BranchLocation"].ToString();
                    txt_designation.Value = ds.Tables[0].Rows[0]["Designation"].ToString();
                    //DC start 04-07-2019
                    
                    string tempIP = GetClientDetails1(ds.Tables[0].Rows[0]["ClientName"].ToString());
                    switch (tempIP)
                    {
                        case "AD":
                            txt_industryvertical.Value = "Ad Agency";
                            break;
                        case "AGRI":
                            txt_industryvertical.Value = "Agricultural";
                            break;
                        case "AUTO":
                            txt_industryvertical.Value = "Automobile";
                            break;
                        case "B2B":
                            txt_industryvertical.Value = "B2B";
                            break;
                        case "BMED":
                            txt_industryvertical.Value = "Broadcast Media";
                            break;
                        case "BFSI":
                            txt_industryvertical.Value = "BFSI";
                            break;
                        case "DURA":
                            txt_industryvertical.Value = "Durables";
                            break;
                        case "FINA":
                            txt_industryvertical.Value = "Financial Services";
                            break;
                        case "FMCG":
                            txt_industryvertical.Value = "FMCG";
                            break;
                        case "PSU":
                            txt_industryvertical.Value = "Govt / PSU";
                            break;
                        case "IT":
                            txt_industryvertical.Value = "IT";
                            break;
                        case "LOGO":
                            txt_industryvertical.Value = "Logistics";
                            break;
                        case "MNE":
                            txt_industryvertical.Value = "Media & Entertainment";
                            break;
                        case "PM":
                            txt_industryvertical.Value = "Pharmaceuticals / Medical / Healthcare";
                            break;
                        case "MEDI":
                            txt_industryvertical.Value = "Print Media";
                            break;
                        case "RCA":
                            txt_industryvertical.Value = "Research / Consulting Agency";
                            break;
                        case "RETI":
                            txt_industryvertical.Value = "Retail";
                            break;
                        case "SOC":
                            txt_industryvertical.Value = "Social Research";
                            break;
                        case "TELE":
                            txt_industryvertical.Value = "Telecom";
                            break;
                        case "TNH":
                            txt_industryvertical.Value = "Travel & Hospitality";
                            break;
                        case "OSW":
                            txt_industryvertical.Value = "Outsourced Work";
                            break;
                        case "OW":
                            txt_industryvertical.Value = "Outsourced Work";
                            break;
                        case "OTHE":
                            txt_industryvertical.Value = "Other Services";
                            break;
                        case "OTHS":
                            txt_industryvertical.Value = "Others";
                            break;
                        case "HI":
                            txt_industryvertical.Value = "Heavy Industries";
                            break;
                        case "NM":
                            txt_industryvertical.Value = "New Media";
                            break;
                        default:
                            txt_industryvertical.Value = "";
                            break;




                    }
                     
                                            
                    //DC End
                }
            }
        }
    }

    protected void BindDropdown()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT ClientName, ID FROM ContactSheet ORDER BY ClientName", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                ddl_organisation.Items.Add(new ListItem("Select", ""));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddl_organisation.Items.Add(new ListItem(ds.Tables[0].Rows[i]["ClientName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString()));
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

    protected void BindCCDropdown()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            using (con)
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT ClientContact FROM ContactSheet ORDER BY ClientContact", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                txt_clientcontact.Items.Add(new ListItem("Select", ""));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_clientcontact.Items.Add(new ListItem(ds.Tables[0].Rows[i]["ClientContact"].ToString(), ds.Tables[0].Rows[i]["ClientContact"].ToString()));
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            con.Close();
        }
    }

    public string GetClientDetails1(string type)
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
    #endregion

    #region WEBSERVICE

    [System.Web.Services.WebMethod]
    public static string GetContactDetails(string ID)
    {
        string d = "";

        return d;
    }

   

    #endregion
}