using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Web.UI;
using ClosedXML.Excel;

public partial class list_lead : System.Web.UI.Page
{
    public encrypt en = new encrypt();
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
                if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "K" || Session["usertype"].ToString() == "D") { btn_download.Visible = true; } else { btn_download.Visible = false; }
                LoadData();
                if (!IsPostBack)
                {
                    LoadClientData();
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
            if (Session["logid"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                DataSet ds = new DataSet();
                using (con)
                {
                    string id = "";
                    string cond = "";
                    if (ddl_unit.SelectedValue.ToString() != "")
                    {
                        cond += " AND P.Unit = '" + ddl_unit.SelectedValue.ToString() + "'";
                    }

                    if (ddl_IndustryVertical.SelectedValue.ToString() != "")
                    {
                        cond += " AND L.IndustryVertical = '" + ddl_IndustryVertical.SelectedValue.ToString() + "'";
                    }

                    if (ddl_NatureofStudy.SelectedValue.ToString() != "")
                    {
                        cond += " AND C.ClientName = '" + ddl_NatureofStudy.SelectedValue.ToString() + "'";
                    }

                    if (ddl_Location.SelectedValue.ToString() != "")
                    {
                        cond += " AND L.Location = '" + ddl_Location.SelectedValue.ToString() + "'";
                    }

                    if (ddl_Result.SelectedValue.ToString() != "")
                    {
                        cond += " AND P.[Status] IS " + ddl_Result.SelectedValue.ToString() + "";
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

                    //string Query = "SELECT C.ClientName, L.ClientContact, P.ClientCompany AS CID,L.ID AS LID, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status], P.ID FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                    //string Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, P.ClientCompany AS CID, L.ID AS LID, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status], P.ID FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = L.ID LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                    string Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, L.ContactID AS CID, L.ID AS LID, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status], P.ID FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
                    if (Session["uid"] != null)
                    {
                        if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                        {
                            if (Session["AccessUid"].ToString() != "")
                            {
                                Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, L.ContactID AS CID, L.ID AS LID, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status], P.ID FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND L.UID  in ('" + Session["AccessUid"].ToString() + "') " + cond;
                            }
                            else
                            {
                                Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, L.ContactID AS CID, L.ID AS LID, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status], P.ID FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND L.UID = '" + Session["uid"].ToString() + "' " + cond;
                            }
                        }
                    }
                    
                    if (Request.QueryString["id"] != null)
                    {
                        id = Request.QueryString["id"].ToString();
                    }
                    else
                    {

                    }
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (id != "")
                    {
                        cmd = new SqlCommand(Query + " AND ContactID = " + id, con);
                    }
                    else
                    {
                        cmd = new SqlCommand(Query, con);
                    }
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                    gv_list.DataSource = ds.Tables[0];
                    gv_list.DataBind();
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

    protected void gv_list_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_list.PageIndex = e.NewPageIndex;
        LoadData();
    }

    protected void btn_response_Command(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
        string page = (String)e.CommandName;
        id = en.Encryption(id);
        page = en.Encryption(page);
        string type = "Edit";
        Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + page+"&type="+type);
    }

    protected void btn_response_Command1(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
        string page = (String)e.CommandName;
        id = en.Encryption(id);
        page = en.Encryption(page);
        string type = "View";
        Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + page + "&type=" + type);
    }
    protected void btn_response_Command2(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
        string page = (String)e.CommandName;
        id = en.Encryption(id);
        page = en.Encryption(page);
        string type = "Add";
        Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + page + "&type=" + type);

        //  string id = (String)e.CommandArgument;
        // Response.Redirect("leadmanagement.aspx?id=" + id );
    }
    protected void btn_followup_Command(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
        string page = (String)e.CommandName;
        id = en.Encryption(id);
        page = en.Encryption(page);
        Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + page);
    }

    protected void btn_validate_Click(object sender, EventArgs e)
    {

    }

    protected void download_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        string id = "";
        string cond = "";
        if (ddl_unit.SelectedValue.ToString() != "")
        {
            cond += " AND P.Unit = '" + ddl_unit.SelectedValue.ToString() + "'";
        }

        if (ddl_IndustryVertical.SelectedValue.ToString() != "")
        {
            cond += " AND L.IndustryVertical = '" + ddl_IndustryVertical.SelectedValue.ToString() + "'";
        }

        if (ddl_NatureofStudy.SelectedValue.ToString() != "")
        {
            cond += " AND C.ClientName = '" + ddl_NatureofStudy.SelectedValue.ToString() + "'";
        }

        if (ddl_Location.SelectedValue.ToString() != "")
        {
            cond += " AND L.Location = '" + ddl_Location.SelectedValue.ToString() + "'";
        }

        if (ddl_Result.SelectedValue.ToString() != "")
        {
            cond += " AND P.[Status] = " + ddl_Result.SelectedValue.ToString() + "";
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
            //string Query = "SELECT C.ClientName, L.ClientContact, L.ProposalName, L.Dateofreceivinglead, L.ProposalDueDate, L.Allotedto, L.LeadGenerator, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, P.Dateofsendingproposal, P.[Status] FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
            //string Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, convert(datetime, L.Dateofreceivinglead, 103) AS Dateofreceivinglead, convert(datetime, L.ProposalDueDate, 103) AS ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.[Status] FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
            //string Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, convert(datetime, L.Dateofreceivinglead, 103) AS Dateofreceivinglead, convert(datetime, L.ProposalDueDate, 103) AS ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.[Status] FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;

            string Query = "SELECT C.ClientName, C.ClientContact, L.ProposalName, convert(datetime, L.Dateofreceivinglead, 103) AS Dateofreceivinglead, convert(datetime, L.ProposalDueDate, 103) AS ProposalDueDate, L.Allotedto, L.IndustryVertical, P.NatureofStudy, L.Location, P.Unit, CASE WHEN P.[Status] IS NOT NULL THEN 'Proposal sent' ELSE 'Proposal not sent' END AS RESULT, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.[Status] FROM LeadDetails L LEFT JOIN ProposalTracker P ON L.ID = P.LeadID LEFT JOIN ContactSheet C ON L.Organisation = C.ID WHERE(P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) " + cond;
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"].ToString();
            }
            else
            {

            }
            con.Open();
            SqlCommand cmd = new SqlCommand();
            if (Session["logname"].ToString() == "Ashok Das" || Session["logname"].ToString() == "Srinivasan Raman" || Session["usertype"].ToString() == "D")
            {
                if (id != "")
                {
                    cmd = new SqlCommand(Query + " AND ContactID = " + id, con);
                }
                else
                {
                    cmd = new SqlCommand(Query, con);
                }
            }
            else
            {
                if (id != "")
                {
                    cmd = new SqlCommand(Query + " AND (L.[UID] = '" + Session["uid"].ToString() + "'  OR L.[UID] IN (" + Session["team"].ToString() + ")) AND ContactID = " + id, con);
                }
                else
                {
                    cmd = new SqlCommand(Query + " AND (L.[UID] = '" + Session["uid"].ToString() + "'  OR L.[UID] IN (" + Session["team"].ToString() + "))", con);
                }
            }
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
            Response.AddHeader("content-disposition", "attachment;filename=LeadTracker - " + filename + ".xlsx");
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

                    ddl_NatureofStudy.DataSource = ds.Tables[0];
                    ddl_NatureofStudy.DataTextField = "ClientName";
                    ddl_NatureofStudy.DataValueField = "ClientName";
                    ddl_NatureofStudy.DataBind();
                    ddl_NatureofStudy.Items.Insert(0, new ListItem("Select Client Name", ""));
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
}