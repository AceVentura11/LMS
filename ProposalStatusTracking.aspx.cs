using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Web.UI;
using ClosedXML.Excel;

public partial class ProposalStatusTracking : System.Web.UI.Page
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
            DateTime date = DateTime.Now.AddMonths(-2);
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            string Year = date.Year.ToString();
            string Sd = month + "/" + day + "/" + Year;


            DateTime date1 = DateTime.Now;
            string day1 = date1.Day.ToString();
            string month1 = date1.Month.ToString();
            string Year1 = date1.Year.ToString();
            string Ed = month1 + "/" + day1 + "/" + Year1;

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
                
                if (Session["usertype"].ToString() == "IDF Head")
                {
                    cond += " AND P.Unit = 'IDF'";
                }
                

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

                


                DataSet ds = new DataSet();
                using (con)
                {
                    cond += " order by  convert(datetime,P.Dateofsendingproposal,103) desc";
                    con.Open();
                    SqlCommand cmd = new SqlCommand();

                    string Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal,P.StudyValue, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) and P.status not in ('Lost','Commissioned')  AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') not between  '" + Sd + "' and  '" + Ed + "'" + cond;
                   
                    if (Session["uid"] != null)
                    {
                        if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                        {
                            if (Session["AccessUid"].ToString() != "")
                            {

                                Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal,P.StudyValue, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID in ('" + Session["AccessUid"].ToString() + "') and P.status not in ('Lost','Commissioned') AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') not between  '" + Sd + "' and  '" + Ed + "'" + cond;
                            }
                            else
                            {

                                Query = "SELECT P.ID, P.[UID], P.ClientCompany AS CID, L.ID AS LID,c.ClientName, P.PROPOSALID, P.TeamHead, P.Researcher, P.ProposalName, P.Dateofreceiptofbrief, P.Dateofsendingproposal, P.Status, P.Dateofsendingproposal,P.StudyValue, P.LOCKED FROM ProposalTracker P LEFT JOIN LeadDetails L ON P.LeadID = CAST(L.ID as varchar(20)) LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID = '" + Session["uid"].ToString() + "'  and P.status not in ('Lost','Commissioned') AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') not between  '" + Sd + "' and  '" + Ed + "'" + cond;
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
    protected void btn_validate_Click(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void btn_download_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        string cond = "WHERE P.ID <> 0 and (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) and P.status not in ('Lost','Commissioned')";

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
        
        if (Session["usertype"].ToString() == "IDF Head")
        {
            cond += " AND P.Unit = 'IDF'";
        }
       

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

       


        DataSet ds = new DataSet();
        using (con)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand();
           
           
            string Query = "SELECT L.USERNAME AS [USER], LD.ProposalName AS [Topic of the Lead], P.Unit, P.OriginatingCentre, P.TeamHead, P.Researcher, C.ClientName, P.ClientCategory, P.ClientContact, P.DirectLine, P.Mobile, P.Email, P.ProposalName, convert(datetime, P.Dateofreceiptofbrief, 103) AS Dateofreceiptofbrief, convert(datetime, P.Dateofsendingproposal, 103) AS Dateofsendingproposal, P.ProjectType, P.StudyType, P.NatureofStudy, P.StudyMethodology, P.DataCollectionMethodology, P.StudyDetails,p.OnlineOption,p.OnlineOptionSource,p.OnlineOptionOtherSource,p.OnlineOptionReason, P.SampleSize, P.StudyValue, P.OPE, P.Status, P.ReasonsforLoss, P.DetailedReasons, P.Comment, P.PROPOSALID, P.LOGDATE, P.LOCKED FROM ProposalTracker P LEFT JOIN ContactSheet C ON P.ClientCompany = C.ID LEFT JOIN[LOGIN] L ON L.ID = P.UID LEFT JOIN LeadDetails LD ON LD.ID = P.LeadID " + cond + " order by  convert(datetime,P.Dateofsendingproposal,103) desc";

            if (Session["uid"] != null)
            {
                if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                {
                    cond += " AND P.UID = '" + Session["uid"].ToString() + "'";
                    
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
    protected void gv_list_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_list.PageIndex = e.NewPageIndex;
        LoadData();
    }
    protected void gv_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
          
            Label UID = e.Row.FindControl("UID") as Label;
            Button EditButton = e.Row.FindControl("btn_proposal") as Button;

            Label Status = e.Row.FindControl("Status") as Label;
          

            Label TeamHead = e.Row.FindControl("Receivedby") as Label;
            Label LOCKED = e.Row.FindControl("LOCKED") as Label;

          

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
            if (LOCKED.Text == "Y")
            {
                
                EditButton.Enabled = false;
                
            }
        }
    }
    protected void btn_proposal_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string id = (String)e.CommandArgument;
            string name = (String)e.CommandName;
            id = en.Encryption(id);
            name = en.Encryption(name);
            string type = "Edit";
            
            Response.Redirect("edit-proposal.aspx?id=" + id + "&cid=" + name + "&type=" + type);
        }
        catch (Exception)
        {
            
            //throw;
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
                   
                    cmd = new SqlCommand("SELECT DISTINCT TeamHead FROM ProposalTracker where TeamHead!='Anjan Ghosh' ORDER BY TeamHead", con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    ddl_TeamHead.DataSource = ds.Tables[0];
                    ddl_TeamHead.DataTextField = "TeamHead";
                    ddl_TeamHead.DataValueField = "TeamHead";
                    ddl_TeamHead.DataBind();
                    
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