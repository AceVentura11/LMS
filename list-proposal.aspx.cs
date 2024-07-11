using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Web.UI;
using ClosedXML.Excel;

public partial class list_proposal : System.Web.UI.Page
{
    public encrypt en = new encrypt();
    public Controller controller = new Controller();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["uid"] != null)
                {
                    if (Request.QueryString["uid"].ToString() != "")
                    {
                        string uid = en.Decryption(Request.QueryString["uid"].ToString());
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
                        DataSet ds = new DataSet();
                        using (con)
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("SELECT * FROM [LOGIN] WHERE USERNAME = '" + uid + "'", con);
                            cmd.CommandType = CommandType.Text;
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(ds);
                            con.Close();
                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                
                                Session["finyear"] = "21-22";
                                Session["logid"] = ds.Tables[0].Rows[0]["userid"].ToString();
                                Session["uid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                                Session["team"] = ds.Tables[0].Rows[0]["TEAM"].ToString();
                                Session["usertype"] = ds.Tables[0].Rows[0]["USERTYPE"].ToString();
                                Session["flag"] = ds.Tables[0].Rows[0]["USERTYPE"].ToString();
                                Session["logname"] = uid;
                                Session["gender"] = ds.Tables[0].Rows[0]["GENDER"].ToString();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Invalid username or password.');", true);
                            }
                        }
                    }
                }
            }

            if (Session["logname"] == null)
            {
                string url = HttpContext.Current.Request.Url.AbsoluteUri;
                Session["url_referer"] = url;
                Response.Redirect("Default.aspx");
            }
            else
            {
                // 22-09-2020 if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "K" || Session["usertype"].ToString() == "D") { btn_download.Visible = true; } else { btn_download.Visible = false; }
                if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "K" || Session["usertype"].ToString() == "D" || Session["usertype"].ToString() == "IDF Head") { btn_download.Visible = true; } else { btn_download.Visible = false; }
                if (!IsPostBack)
                {
                    LoadData();
                    LoadClientData();
                    LoadTeamHeadData();
                }
            }
        }
        catch
        {

        }
    }

    protected void LoadData()
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        try
        {
            string unithead = "";
            foreach (ListItem listItem in ddl_unit.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(unithead))
                        unithead += ",";
                    unithead += "'" + listItem.Value + "'";

                }
            }
            string clientcategory = "";
            foreach (ListItem listItem in ddl_clientcategory.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(clientcategory))
                        clientcategory += ",";
                    clientcategory += "'" + listItem.Value + "'";

                }
            }
            string originatingcentre = "";
            foreach (ListItem listItem in ddl_originatingcentre.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(originatingcentre))
                        originatingcentre += ",";
                    originatingcentre += "'" + listItem.Value + "'";

                }
            }

            string natureofstudy = "";
            foreach (ListItem listItem in ddl_natureofstudy.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(natureofstudy))
                        natureofstudy += ",";
                    natureofstudy += "'" + listItem.Value + "'";

                }
            }

            string status = "";
            foreach (ListItem listItem in ddl_status.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(status))
                        status += ",";
                    status += "'" + listItem.Value + "'";

                }
            }
            string study = "";
            foreach (ListItem listItem in ddl_study.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(study))
                        study += ",";
                    study += "'" + listItem.Value + "'";

                }
            }

            string TeamHead = "";
            foreach (ListItem listItem in ddl_TeamHead.Items)
            {
                if (listItem.Selected)
                {
                    if (!string.IsNullOrEmpty(TeamHead))
                        TeamHead += ",";
                    TeamHead += "'" + listItem.Value + "'";

                }
            }

            if (Session["logid"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                string cond = "";

                if (!string.IsNullOrEmpty(unithead))
                {

                    cond += " AND P.Unit in (" + unithead + ")";
                }

                if (!string.IsNullOrEmpty(clientcategory))
                {
                    cond += " AND P.ClientCategory in (" + clientcategory + ") ";
                }

                if (!string.IsNullOrEmpty(originatingcentre))
                {
                    cond += " AND P.OriginatingCentre in (" + originatingcentre + ")";
                }

                if (!string.IsNullOrEmpty(natureofstudy))
                {
                    cond += " AND C.ClientName in (" + natureofstudy + ") ";
                }

                if (!string.IsNullOrEmpty(status))
                {
                    cond += " AND P.Status in (" + status + ")";
                }

                if (!string.IsNullOrEmpty(study))
                {
                    cond += " AND P.StudyType in (" + study + ")";
                }
                if (!string.IsNullOrEmpty(TeamHead))
                {
                    cond += " AND P.TeamHead in (" + TeamHead + ")";
                }
                if (ddl_dates.Text != "Select Date")
                {
                    if (fromdate.Value != "From Date" && Todate.Value != "To Date")
                    {
                        string day = fromdate.Value.Substring(0, 2);
                        string month = fromdate.Value.Substring(3, 2);
                        string Year = fromdate.Value.Substring(6, 4);
                        string Sd = month + "/" + day + "/" + Year;
                        

                        string day1 = Todate.Value.Substring(0, 2);
                        string month1 = Todate.Value.Substring(3, 2);
                        string Year1 = Todate.Value.Substring(6, 4);
                        string Ed = month1 + "/" + day1 + "/" + Year1;
                        

                        
                        if (ddl_dates.Text == "Proposal Sent date")
                        {
                            //Dc 18-06-2020 cond += " AND CONVERT(datetime,P.Dateofsendingproposal,103) between  '" + fromdate.Value.ToString() + "' and '" + Todate.Value.ToString() + "'";
                           //Dc 17-08-2020 cond += " AND CONVERT(datetime,P.Dateofsendingproposal,103) between  '" + Sd + "' and  '" + Ed + "'";
                           //27-08-2020 cond += " AND CONVERT(varchar,P.Dateofsendingproposal,103) between  '" + Sd + "' and  '" + Ed + "'";
                            cond += " AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') between  '" + Sd + "' and  '" + Ed + "'";

                            
                        }
                        else if (ddl_dates.Text == "Date of receipt of brief")
                        {
                            //Dc 18-06-2020 cond += " AND CONVERT(datetime,P.Dateofreceiptofbrief,103) between '" + fromdate.Value.ToString() + "' and '" + Todate.Value.ToString() + "'";
                           //Dc 17-08-2020 cond += " AND CONVERT(datetime,P.Dateofreceiptofbrief,103) between '" + Sd + "' and  '" + Ed + "'";
                            //27-08-2020 cond += " AND CONVERT(varchar,P.Dateofreceiptofbrief,103) between '" + Sd + "' and  '" + Ed + "'";
                            cond += " AND PARSE(p.Dateofreceiptofbrief as date using 'AR-LB') between '" + Sd + "' and  '" + Ed + "'";
                        }
                        else if (ddl_dates.Text == "Date of sending proposal")
                        {
                            //Dc 18-06-2020 cond += " AND CONVERT(datetime,P.Dateofsendingproposal,103) between '" + fromdate.Value.ToString() + "' and '" + Todate.Value.ToString() + "'";
                            //cond += " AND CONVERT(datetime,P.Dateofsendingproposal,103) between '" + Sd + "' and  '" + Ed + "'";
                            // 27-08-2020 cond += " AND CONVERT(varchar,P.Dateofsendingproposal,103) between '" + Sd + "' and  '" + Ed + "'";
                            cond += " AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') between '" + Sd + "' and  '" + Ed + "'";
                        }
                    }
                    else
                    {
                        if (fromdate.Value == "From Date")
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Select From date');", true);
                        else
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Select To date');", true);
                    }
                }

                //DC 18-09-2019
                if (Session["usertype"].ToString() == "New Qnt Head")
                {
                    cond += " AND P.Unit = 'QN'";
                }
                if (Session["usertype"].ToString() == "IBD Head")
                {
                    cond += " AND P.Unit = 'IBD'";
                }
                if (Session["usertype"].ToString() == "New Qual Head")
                {   
                    cond += " AND P.Unit = 'QL'";
                }
                if (Session["usertype"].ToString() == "Media")
                {
                    cond += " AND P.Unit = 'MD'";
                }
                //
                //DC 22-09-2020
                if (Session["usertype"].ToString() == "IDF Head")
                {
                    cond += " AND P.Unit = 'IDF'";
                }
                //

                //DC 29-09-2021 Unit head wise changes 

                if (Session["logname"].ToString() == "Amarnath Pathak")
                {
                    cond += " AND P.Unit = 'B2B'";
                }
                else if (Session["logname"].ToString() == "Anjan Ghosh")
                {
                    cond += " AND (P.Unit = 'QN' or P.Unit = 'MD')";
                }
                else if (Session["logname"].ToString() == "Piyali Chatterjee")
                {
                    cond += " AND (P.Unit = 'CX' or P.Unit = 'ICI')";
                }
                else if (Session["logname"].ToString() == "Jiten")
                {
                    cond += " AND P.Unit = 'IDF'";
                }
                else if (Session["logname"].ToString() == "Shaleena")
                {
                    cond += " AND P.Unit = 'QL'";
                }
                else if (Session["logname"].ToString() == "Umesh Kumar Singh" || Session["logname"].ToString() == "Umesh Kumar")
                {
                    cond += " AND P.Unit = 'IBD'";
                }

                //


                DataSet ds = new DataSet();
                using (con)
                {
                    cond += " order by  convert(datetime,P.Dateofsendingproposal,103) desc";
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    //Dc 16-06-2020 string Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                   //DC 18-08-2020 string Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                    string Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal,P.StudyValue, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                    //string Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, L.ContactID AS CID, L.ID AS LID, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status], P.ID FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                    if (Session["uid"] != null)
                    {
                        if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                        {
                            if (Session["AccessUid"].ToString() != "")
                            {
                                //Dc 16-06-2020 Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID in ('" + Session["AccessUid"].ToString() + "') " + cond;
                                //Dc 18-06-2020 Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID in ('" + Session["AccessUid"].ToString() + "') " + cond;
                                Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal,P.StudyValue, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID in ('" + Session["AccessUid"].ToString() + "') " + cond;
                            }
                            else
                            {
                                //Dc 16-06-2020 Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID = '" + Session["uid"].ToString() + "' " + cond;
                                //Dc 18-08-2020 Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID = '" + Session["uid"].ToString() + "' " + cond;
                                Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal,P.StudyValue, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID = '" + Session["uid"].ToString() + "' " + cond;
                            }
                        }
                    }
                    cmd = new SqlCommand(Query, con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                    gv_list.DataSource = ds.Tables[0];
                    gv_list.DataBind();
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

    protected void gv_list_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_list.PageIndex = e.NewPageIndex;
        LoadData();
    }

    protected void btn_response_Command(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
        string name = (String)e.CommandName;
        string uid = Session["logname"].ToString();
        if (name == "D")
        {
            delete(id);
        }
        else if (name == "A")
        {
            DataSet DS1 = controller.GetDataSet("SELECT C.ClientName FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE PROPOSALID = '" + id + "' AND P.ACTIVE = 'Y'");
            if (DS1.Tables[0].Rows.Count > 0)
            {
                DataSet ds = controller.GetDataSetFin("SELECT * FROM clientmaster WHERE clientname = '" + DS1.Tables[0].Rows[0]["ClientName"].ToString() + "'");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["ClientName"] = DS1.Tables[0].Rows[0]["ClientName"].ToString();
                    string cname = Session["ClientName"].ToString();

                    // id = en.Encryption(id);
                    //Response.Redirect("http://192.168.0.27:83/Index.aspx?proposalid=" + id + "&uid=" + en.Encryption(uid));
                    Response.Redirect("http://192.168.2.56:99/Index.aspx?proposalid=" + id + "&uid=" + uid + "&cname=" + cname, true);
                    // Response.Redirect("http://192.168.0.27:8182/Index.aspx?proposalid=" + id + "&uid=" + uid + "&cname=" + cname, true);

                }
                else
                {
                    Response.Write("<script>var newWin = window.open('http://192.168.0.19:91/','_blank'); if(!newWin || newWin.closed || typeof newWin.closed=='undefined') { alert('Please allow pop up blocks.'); } </script>");
                }
            }
        }
        else
        {
            id = en.Encryption(id);
            name = en.Encryption(name);
            string type = "Edit";
            //Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + name);
            Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + name + "&type=" + type);
        }
    }

    protected void delete(string id)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            DataSet ds = new DataSet();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE ProposalTracker SET ACTIVE = 'D' WHERE ID = " + id, con);
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Proposal deleted successfully.'); window.location.href = 'list-proposal.aspx'", true);
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {

        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void download_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        string cond = "WHERE P.ID <> 0 and (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL)";

        string unithead = "";
        foreach (ListItem listItem in ddl_unit.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(unithead))
                    unithead += ",";
                unithead += "'" + listItem.Value + "'";

            }
        }
        string clientcategory = "";
        foreach (ListItem listItem in ddl_clientcategory.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(clientcategory))
                    clientcategory += ",";
                clientcategory += "'" + listItem.Value + "'";

            }
        }
        string originatingcentre = "";
        foreach (ListItem listItem in ddl_originatingcentre.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(originatingcentre))
                    originatingcentre += ",";
                originatingcentre += "'" + listItem.Value + "'";

            }
        }

        string natureofstudy = "";
        foreach (ListItem listItem in ddl_natureofstudy.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(natureofstudy))
                    natureofstudy += ",";
                natureofstudy += "'" + listItem.Value + "'";

            }
        }

        string status = "";
        foreach (ListItem listItem in ddl_status.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(status))
                    status += ",";
                status += "'" + listItem.Value + "'";

            }
        }
        string study = "";
        foreach (ListItem listItem in ddl_study.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(study))
                    study += ",";
                study += "'" + listItem.Value + "'";

            }
        }

        string TeamHead = "";
        foreach (ListItem listItem in ddl_TeamHead.Items)
        {
            if (listItem.Selected)
            {
                if (!string.IsNullOrEmpty(TeamHead))
                    TeamHead += ",";
                TeamHead += "'" + listItem.Value + "'";

            }
        }
        if (!string.IsNullOrEmpty(unithead))
        {
            cond += " AND P.Unit in (" + unithead + ") ";
        }

        if (!string.IsNullOrEmpty(clientcategory))
        {
            cond += " AND P.ClientCategory in (" + clientcategory + ")";
        }

        if (!string.IsNullOrEmpty(originatingcentre))
        {
            cond += " AND P.OriginatingCentre in (" + originatingcentre + ") ";
        }

        if (!string.IsNullOrEmpty(natureofstudy))
        {
            cond += " AND C.ClientName in (" + natureofstudy + ")";
        }

        if (!string.IsNullOrEmpty(status))
        {
            cond += " AND P.Status in (" + status + ")";
        }

        if (!string.IsNullOrEmpty(study))
        {
            cond += " AND P.StudyType in (" + study + ")";
        }
        if (!string.IsNullOrEmpty(TeamHead))
        {
            cond += " AND P.TeamHead in (" + TeamHead + ")";
        }
        if (ddl_dates.Text != "Select Date")
        {
            if (fromdate.Value != "From Date" && Todate.Value != "To Date")
            {
                string day = fromdate.Value.Substring(0, 2);
                string month = fromdate.Value.Substring(3, 2);
                string Year = fromdate.Value.Substring(6, 4);
                string Sd = month + "/" + day + "/" + Year;

                string day1 = Todate.Value.Substring(0, 2);
                string month1 = Todate.Value.Substring(3, 2);
                string Year1 = Todate.Value.Substring(6, 4);
                string Ed = month1 + "/" + day1 + "/" + Year1;

                if (ddl_dates.Text == "Proposal Sent date")
                {
                    //DC 16-08-2020 cond += " AND CONVERT(datetime,P.Dateofsendingproposal,103) between '" + Sd + "' and '" + Ed + "'";
                    //Dc 27-08-2020 cond += " AND CONVERT(varchar,P.Dateofsendingproposal,103) between '" + Sd + "' and '" + Ed + "'";
                    cond += " AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') between '" + Sd + "' and '" + Ed + "'";
                }
                else if (ddl_dates.Text == "Date of receipt of brief")
                {
                   //DC 16-08-2020 cond += " AND CONVERT(datetime,P.Dateofreceiptofbrief,103) between '" + Sd + "' and '" + Ed + "'";
                    //Dc 27-08-2020 cond += " AND CONVERT(varchar,P.Dateofreceiptofbrief,103) between '" + Sd + "' and '" + Ed + "'";
                    cond += " AND PARSE(p.Dateofreceiptofbrief as date using 'AR-LB') between '" + Sd + "' and '" + Ed + "'";
                }
                else if (ddl_dates.Text == "Date of sending proposal")
                {
                   //DC 16-08-2020 cond += " AND CONVERT(datetime,P.Dateofsendingproposal,103) between '" + Sd + "' and '" + Ed + "'";
                    //Dc 27-08-2020 cond += " AND CONVERT(varchar,P.Dateofsendingproposal,103) between '" + Sd + "' and '" + Ed + "'";
                    cond += " AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') between '" + Sd + "' and '" + Ed + "'";
                }
            }
            else
            {
                if (fromdate.Value == "From Date")
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Select From date');", true);
                else
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "alert('Select To date');", true);
            }
        }

        //DC 18-09-2019
        if (Session["usertype"].ToString() == "New Qnt Head")
        {
            cond += " AND P.Unit = 'QN'";
        }
        if (Session["usertype"].ToString() == "IBD Head")
        {
            cond += " AND P.Unit = 'IBD'";
        }
        if (Session["usertype"].ToString() == "New Qual Head")
        {
            cond += " AND P.Unit = 'QL'";
        }
        if (Session["usertype"].ToString() == "Media")
        {
            cond += " AND P.Unit = 'MD'";
        }
        //
        //DC 22-09-2020
        if (Session["usertype"].ToString() == "IDF Head")
        {
            cond += " AND P.Unit = 'IDF'";
        }
        //

        //DC 29-09-2021 Unit head wise changes 

        if (Session["logname"].ToString() == "Amarnath Pathak")
        {
            cond += " AND P.Unit = 'B2B'";
        }
        else if (Session["logname"].ToString() == "Anjan Ghosh")
        {
            cond += " AND (P.Unit = 'QN' or P.Unit = 'MD')";
        }
        else if (Session["logname"].ToString() == "Piyali Chatterjee")
        {
            cond += " AND (P.Unit = 'CX' or P.Unit = 'ICI')";
        }
        else if (Session["logname"].ToString() == "Jiten")
        {
            cond += " AND P.Unit = 'IDF'";
        }
        else if (Session["logname"].ToString() == "Shaleena")
        {
            cond += " AND P.Unit = 'QL'";
        }
        else if (Session["logname"].ToString() == "Umesh Kumar Singh" || Session["logname"].ToString() == "Umesh Kumar")
        {
            cond += " AND P.Unit = 'IBD'";
        }

        //


        DataSet ds = new DataSet();
        using (con)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();
            //Dc 06-01-20 string Query = "SELECT L.USERNAME AS [USER], LD.ProposalName AS [Topic of the Lead], P.Unit, P.OriginatingCentre, P.TeamHead, P.Researcher, C.ClientName, P.ClientCategory, P.ClientContact, P.DirectLine, P.Mobile, P.Email, P.ProposalName, convert(datetime, P.Dateofreceiptofbrief, 103) AS Dateofreceiptofbrief, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.ProjectType, P.StudyType, P.NatureofStudy, P.StudyMethodology, P.DataCollectionMethodology, P.StudyDetails, P.SampleSize, P.StudyValue, P.OPE, P.Status, P.ReasonsforLoss, P.DetailedReasons, P.Comment, P.PROPOSALID, P.LOGDATE, P.LOCKED FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID LEFT JOIN[LOGIN] L ON L.ID = P.UID LEFT JOIN LeadDetails LD ON LD.ID = P.LeadID " + cond + " order by  convert(datetime,P.Dateofsendingproposal,103) desc";
            //DC 06-01-20 string Query = "SELECT L.USERNAME AS [USER], LD.ProposalName AS [Topic of the Lead], P.Unit, P.OriginatingCentre, P.TeamHead, P.Researcher, C.ClientName, P.ClientCategory, P.ClientContact, P.DirectLine, P.Mobile, P.Email, P.ProposalName, convert(datetime, P.Dateofreceiptofbrief, 103) AS Dateofreceiptofbrief, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.ProjectType, P.StudyType, P.NatureofStudy, P.StudyMethodology, P.DataCollectionMethodology, P.StudyDetails,p.OnlineOption,p.OnlineOptionReason, P.SampleSize, P.StudyValue, P.OPE, P.Status, P.ReasonsforLoss, P.DetailedReasons, P.Comment, P.PROPOSALID, P.LOGDATE, P.LOCKED FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID LEFT JOIN[LOGIN] L ON L.ID = P.UID LEFT JOIN LeadDetails LD ON LD.ID = P.LeadID " + cond + " order by  convert(datetime,P.Dateofsendingproposal,103) desc";
            string Query = "SELECT L.USERNAME AS [USER], LD.ProposalName AS [Topic of the Lead], P.Unit, P.OriginatingCentre, P.TeamHead, P.Researcher, C.ClientName, P.ClientCategory, P.ClientContact, P.DirectLine, P.Mobile, P.Email, P.ProposalName, convert(datetime, P.Dateofreceiptofbrief, 103) AS Dateofreceiptofbrief, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.ProjectType, P.StudyType, P.NatureofStudy, P.StudyMethodology, P.DataCollectionMethodology, P.StudyDetails,p.OnlineOption,p.OnlineOptionSource,p.OnlineOptionOtherSource,p.OnlineOptionReason, P.SampleSize, P.StudyValue, P.OPE, P.Status, P.ReasonsforLoss, P.DetailedReasons, P.Comment, P.PROPOSALID, P.LOGDATE, P.LOCKED FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID LEFT JOIN[LOGIN] L ON L.ID = P.UID LEFT JOIN LeadDetails LD ON LD.ID = P.LeadID " + cond + " order by  convert(datetime,P.Dateofsendingproposal,103) desc";

            if (Session["uid"] != null)
            {
                if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                {
                    cond += " AND P.UID = '" + Session["uid"].ToString() + "'";
                    //DC 06-01-20 Query = "SELECT L.USERNAME AS [USER], LD.ProposalName AS [Topic of the Lead], P.Unit, P.OriginatingCentre, P.TeamHead, P.Researcher, C.ClientName, P.ClientCategory, P.ClientContact, P.DirectLine, P.Mobile, P.Email, P.ProposalName, convert(datetime, P.Dateofreceiptofbrief, 103) AS Dateofreceiptofbrief, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.ProjectType, P.StudyType, P.NatureofStudy, P.StudyMethodology, P.DataCollectionMethodology, P.StudyDetails, P.SampleSize, P.StudyValue, P.OPE, P.Status, P.ReasonsforLoss, P.DetailedReasons, P.Comment, P.PROPOSALID, P.LOGDATE, P.LOCKED FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID LEFT JOIN[LOGIN] L ON L.ID = P.UID LEFT JOIN LeadDetails LD ON LD.ID = P.LeadID " + cond + " order by  convert(datetime,P.Dateofsendingproposal,103) desc";
                    Query = "SELECT L.USERNAME AS [USER], LD.ProposalName AS [Topic of the Lead], P.Unit, P.OriginatingCentre, P.TeamHead, P.Researcher, C.ClientName, P.ClientCategory, P.ClientContact, P.DirectLine, P.Mobile, P.Email, P.ProposalName, convert(datetime, P.Dateofreceiptofbrief, 103) AS Dateofreceiptofbrief, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.ProjectType, P.StudyType, P.NatureofStudy, P.StudyMethodology, P.DataCollectionMethodology, P.StudyDetails,p.OnlineOption,p.OnlineOptionReason, P.SampleSize, P.StudyValue, P.OPE, P.Status, P.ReasonsforLoss, P.DetailedReasons, P.Comment, P.PROPOSALID, P.LOGDATE, P.LOCKED FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID LEFT JOIN[LOGIN] L ON L.ID = P.UID LEFT JOIN LeadDetails LD ON LD.ID = P.LeadID " + cond + " order by  convert(datetime,P.Dateofsendingproposal,103) desc";
                }
            }
            cmd = new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }

        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            {
                wb.Worksheets.Add(dt);
            }
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string filename = System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            Response.AddHeader("content-disposition", "attachment;filename=ProposalReport - " + filename + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void LoadClientData()
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
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("SELECT DISTINCT ClientName FROM ContactSheet ORDER BY ClientName", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    ddl_natureofstudy.DataSource = ds.Tables[0];
                    ddl_natureofstudy.DataTextField = "ClientName";
                    ddl_natureofstudy.DataValueField = "ClientName";
                    ddl_natureofstudy.DataBind();
                    //ddl_natureofstudy.Items.Insert(0, new ListItem("Select Client Name", ""));
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
    protected void LoadTeamHeadData()
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
                using (con)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    //Dc 24-06-20 cmd = new SqlCommand("SELECT DISTINCT TeamHead FROM ProposalTracker ORDER BY TeamHead", con);
                    cmd = new SqlCommand("SELECT DISTINCT TeamHead FROM ProposalTracker where TeamHead!='Anjan Ghosh' ORDER BY TeamHead", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    ddl_TeamHead.DataSource = ds.Tables[0];
                    ddl_TeamHead.DataTextField = "TeamHead";
                    ddl_TeamHead.DataValueField = "TeamHead";
                    ddl_TeamHead.DataBind();
                    //ddl_TeamHead.Items.Insert(0, new ListItem("Select Team Head Name", ""));
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

    protected void gv_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button DeleteButton = e.Row.FindControl("btn_d") as Button;
            Label UID = e.Row.FindControl("UID") as Label;
            Button EditButton = e.Row.FindControl("btn_proposal") as Button;

            Label Status = e.Row.FindControl("Status") as Label;
            Button AddJCF = e.Row.FindControl("btn_add_jcf") as Button;

            Label TeamHead = e.Row.FindControl("Receivedby") as Label;
            Label LOCKED = e.Row.FindControl("LOCKED") as Label;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Session["usertype"].ToString() == "M")
            {
                DeleteButton.Enabled = true;
            }
            else
            {
                DeleteButton.Enabled = false;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "D")
            {

            }
            else
            {
                if (Session["uid"].ToString() == UID.Text || Session["logname"].ToString() == TeamHead.Text)
                {
                    EditButton.Enabled = true;
                }
                else
                {
                    EditButton.Enabled = false;
                }
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Status.Text == "Commissioned")
            {
                AddJCF.Enabled = true;
            }
            else
            {
                AddJCF.Enabled = false;
            }

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (LOCKED.Text == "Y")
            {
                AddJCF.Enabled = false;
                EditButton.Enabled = false;
                DeleteButton.Enabled = false;
            }
        }
    }

    protected void ddl_dates_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddl_dates.Text != "Select Date")
            {
                fromdate.Visible = true;
                Todate.Visible = true;
                fromdate.Value = "From Date";
                Todate.Value = "To Date";

            }
            else
            {
                fromdate.Visible = false;
                Todate.Visible = false;
            }
        }
        catch (Exception)
        {

        }
    }


}