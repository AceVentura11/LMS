using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class edit_proposal : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    BindTeamHeadDropdown();
                    BindResearcherDropdown();
                    BindDropdown();
                    BindData();
                    if (Request.QueryString["type"].ToString() == "View")
                        btn_validate.Enabled = false;
                }
            }
        }
        catch
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void BindData()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        try
        {
            if (Session["logid"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                DataSet ds = new DataSet();
                string id = Request.QueryString["id"].ToString();
                hf_id.Value = id;
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    string query = "SELECT * FROM ProposalTracker WHERE LeadID = '" + en.Decryption(id) + "' AND ACTIVE = 'Y'";
                    cmd = new SqlCommand(query, con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ddl_unit.Value = ds.Tables[0].Rows[0]["Unit"].ToString();
                    ddl_originatingcentre.Value = ds.Tables[0].Rows[0]["OriginatingCentre"].ToString();
                    txt_teamhead.Value = ds.Tables[0].Rows[0]["TeamHead"].ToString();
                    txt_researcher.Value = ds.Tables[0].Rows[0]["Researcher"].ToString();
                    ddl_clientcompany.Value = ds.Tables[0].Rows[0]["ClientCompany"].ToString();
                    ddl_clientcategory.Value = ds.Tables[0].Rows[0]["ClientCategory"].ToString();
                    txt_clientcontact.Value = ds.Tables[0].Rows[0]["ClientContact"].ToString();
                    txt_directline.Value = ds.Tables[0].Rows[0]["DirectLine"].ToString();
                    txt_mobile.Value = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    txt_email.Value = ds.Tables[0].Rows[0]["Email"].ToString();
                    txt_proposalname.Value = ds.Tables[0].Rows[0]["ProposalName"].ToString();
                    txt_dateofreceiptofbrief.Value = ds.Tables[0].Rows[0]["Dateofreceiptofbrief"].ToString();
                    txt_dateofsendingproposal.Value = ds.Tables[0].Rows[0]["Dateofsendingproposal"].ToString();
                    ddl_projecttype.Value = ds.Tables[0].Rows[0]["ProjectType"].ToString();
                    txt_studytype.Value = ds.Tables[0].Rows[0]["StudyType"].ToString();
                    ddl_natureofstudy.Value = ds.Tables[0].Rows[0]["NatureofStudy"].ToString();
                    hf_studymethodology.Value = ds.Tables[0].Rows[0]["StudyMethodology"].ToString();
                    hf_datacollectionmethod.Value = ds.Tables[0].Rows[0]["DataCollectionMethodology"].ToString();
                    txt_studydetails.Value = ds.Tables[0].Rows[0]["StudyDetails"].ToString();
                    txt_samplesize.Value = ds.Tables[0].Rows[0]["SampleSize"].ToString();
                    txt_studyvalue.Value = ds.Tables[0].Rows[0]["StudyValue"].ToString();
                    txt_ope.Value = ds.Tables[0].Rows[0]["OPE"].ToString();
                    ddl_status.Value = ds.Tables[0].Rows[0]["Status"].ToString();
                    txt_reasonforloss.Value = ds.Tables[0].Rows[0]["ReasonsforLoss"].ToString();
                    txt_detailedreason.Value = ds.Tables[0].Rows[0]["DetailedReasons"].ToString();
                    txt_comment.Value = ds.Tables[0].Rows[0]["Comment"].ToString();
                    hf_uid.Value = ds.Tables[0].Rows[0]["UID"].ToString();

                    //DC 10-12-2019
                    string OnlineOption = ds.Tables[0].Rows[0]["OnlineOption"].ToString();
                    if (OnlineOption.Trim() == "Yes")
                    {
                        rb_Onloptyes.Checked = true;
                        ddl_onlinesource.Value= ds.Tables[0].Rows[0]["OnlineOptionSource"].ToString();
                        if (ddl_onlinesource.Value == "Other sources")
                        {
                            
                            txtonloptionsothersource.Value = ds.Tables[0].Rows[0]["OnlineOptionOtherSource"].ToString();
                        }


                    }
                    else
                    {
                        rb_Onloptno.Checked = true;
                        

                        txtonloptreason.Value = ds.Tables[0].Rows[0]["OnlineOptionReason"].ToString();
                       
                    }
                    //

                    if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "D")
                    {

                    }
                    else
                    {
                        if (Session["uid"].ToString() == ds.Tables[0].Rows[0]["UID"].ToString() || Session["logname"].ToString() == ds.Tables[0].Rows[0]["TeamHead"].ToString())
                        {
                            btn_validate.Enabled = true;
                        }
                        else
                        {
                            btn_validate.Enabled = false;
                        }
                    }
                    if (ddl_clientcategory.Value == "")
                    {
                        SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());

                        string query1 = "SELECT IPG FROM clientmaster where clientname='" + getclientname() + "'";
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
                                    ddl_clientcategory.Value = ds1.Tables[0].Rows[0]["IPG"].ToString();
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
                else
                {
                    string cid = en.Decryption(Request.QueryString["cid"].ToString());
                    string lid = en.Decryption(Request.QueryString["id"].ToString());
                    hf_uid.Value = Session["uid"].ToString();
                    BindFormData(cid, lid);
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

    public string getclientname()
    {
        string tempname = null;
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            DataSet ds1 = new DataSet();
            using (con)
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT DISTINCT ClientName from ContactSheet where ID='" + ddl_clientcompany.Value + "'", con);
                cmd1.CommandType = CommandType.Text;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(ds1);
                con.Close();

                if (ds1.Tables[0].Rows.Count > 0)
                {
                    tempname = ds1.Tables[0].Rows[0]["ClientName"].ToString();

                }



            }
            return tempname;
        }
        catch
        {

            return tempname;
        }
        finally
        {
            con.Close();

        }
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SP_PROPOSALTRACKER", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@unit", ddl_unit.Value);
                cmd.Parameters.AddWithValue("@originatingcentre", ddl_originatingcentre.Value);
                cmd.Parameters.AddWithValue("@teamhead", txt_teamhead.Value);
                cmd.Parameters.AddWithValue("@researcher", txt_researcher.Value);
                cmd.Parameters.AddWithValue("@clientcompany", ddl_clientcompany.Value);
                cmd.Parameters.AddWithValue("@clientcategory", ddl_clientcategory.Value);
                cmd.Parameters.AddWithValue("@clientcontact", txt_clientcontact.Value);
                cmd.Parameters.AddWithValue("@directline", txt_directline.Value);
                cmd.Parameters.AddWithValue("@mobile", txt_mobile.Value);
                cmd.Parameters.AddWithValue("@email", txt_email.Value);
                cmd.Parameters.AddWithValue("@proposalname", txt_proposalname.Value);
                cmd.Parameters.AddWithValue("@dateofreceiptofbrief", txt_dateofreceiptofbrief.Value);
                cmd.Parameters.AddWithValue("@dateofsendingproposal", txt_dateofsendingproposal.Value);
                cmd.Parameters.AddWithValue("@projecttype", ddl_projecttype.Value);
                cmd.Parameters.AddWithValue("@studytype", txt_studytype.Value);
                cmd.Parameters.AddWithValue("@natureofstudy", ddl_natureofstudy.Value);
                cmd.Parameters.AddWithValue("@studymethodology", hf_studymethodology.Value);
                cmd.Parameters.AddWithValue("@datacollectionmethod", hf_datacollectionmethod.Value);
                cmd.Parameters.AddWithValue("@studydetails", txt_studydetails.Value);
                cmd.Parameters.AddWithValue("@samplesize", txt_samplesize.Value);
                cmd.Parameters.AddWithValue("@studyvalue", txt_studyvalue.Value);
                cmd.Parameters.AddWithValue("@ope", txt_ope.Value);
                cmd.Parameters.AddWithValue("@status", ddl_status.Value);
                cmd.Parameters.AddWithValue("@reasonforloss", txt_reasonforloss.Value);
                cmd.Parameters.AddWithValue("@detailedreason", txt_detailedreason.Value);
                cmd.Parameters.AddWithValue("@comment", txt_comment.Value);
                cmd.Parameters.AddWithValue("@LeadID", en.Decryption(hf_id.Value));
                cmd.Parameters.AddWithValue("@UID", hf_uid.Value);

                //DC 10-12-2019
                if (rb_Onloptyes.Checked == true)
                {
                    cmd.Parameters.AddWithValue("@OnlineOption", "Yes");
                    cmd.Parameters.AddWithValue("@OnlineOptionReason", "");
                    cmd.Parameters.AddWithValue("@OnlineOptionSource", ddl_onlinesource.Value);
                    if (string.IsNullOrEmpty(txtonloptionsothersource.Value) && ddl_onlinesource.Value == "Other sources")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Please Enter other source.')", true);
                        goto a;
                    }
                    else
                        cmd.Parameters.AddWithValue("@OnlineOptionOtherSource", txtonloptionsothersource.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@OnlineOption", "No");
                    if(!string.IsNullOrEmpty(txtonloptreason.Value))
                    cmd.Parameters.AddWithValue("@OnlineOptionReason", txtonloptreason.Value);
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Please Enter Reason.')", true);
                        goto a;
                    }
                    cmd.Parameters.AddWithValue("@OnlineOptionSource", "");
                    cmd.Parameters.AddWithValue("@OnlineOptionOtherSource", "");
                }

                //

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Proposal saved successfully.'); window.location.href = 'list-proposal.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Error saving proposal.'); window.location.href = 'list-proposal.aspx'", true);
                }
            a: { }
            }
        }
        catch
        {
            Response.Redirect("Default.aspx");
        }
        finally
        {
            //con.Close();
        }
    }

    #endregion

    #region FUNCTIONS

    protected void BindFormData(string ID, string LID)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            DataSet ds = new DataSet();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT *, (SELECT Dateofreceivinglead FROM LeadDetails WHERE ID = '" + LID + "') as DD FROM ContactSheet WHERE ID = '" + ID + "'", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ddl_clientcompany.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                        txt_clientcontact.Value = ds.Tables[0].Rows[0]["ClientContact"].ToString();
                        ddl_originatingcentre.Value = ds.Tables[0].Rows[0]["BranchLocation"].ToString();
                        txt_directline.Value = ds.Tables[0].Rows[0]["DirectLine"].ToString();
                        txt_mobile.Value = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        txt_email.Value = ds.Tables[0].Rows[0]["EmailID"].ToString();
                        txt_dateofreceiptofbrief.Value = ds.Tables[0].Rows[0]["DD"].ToString();
                    }
                }
            }
        }
        catch
        {

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
                ddl_clientcompany.Items.Add(new ListItem("Select", ""));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    ddl_clientcompany.Items.Add(new ListItem(ds.Tables[0].Rows[i]["ClientName"].ToString(), ds.Tables[0].Rows[i]["ID"].ToString()));
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

    protected void BindTeamHeadDropdown()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
            using (con)
            {
                DataSet ds = new DataSet();
                con.Open();
               //Dc 16-06-20202 SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEEMASTER WHERE employeetype = 'TeamHead' order by employeename", con);
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

    protected void BindResearcherDropdown()
    {
        try
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
            using (con)
            {
                DataSet ds = new DataSet();
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM EMPLOYEEMASTER WHERE employeetype = 'Researcher' order by employeename", con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                txt_researcher.Items.Add(new ListItem("Select", ""));

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    txt_researcher.Items.Add(new ListItem(ds.Tables[0].Rows[i]["employeename"].ToString(), ds.Tables[0].Rows[i]["employeename"].ToString()));
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
    public static string GetContactDetails(string ID)
    {
        string d = "";
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
                    d = GetJson(ds.Tables[0]);
                }
            }
        }
        return d;
    }

    public static string GetJson(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer Jserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rowsList = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rowsList.Add(row);
        }
        return Jserializer.Serialize(rowsList);
    }

    #endregion
}