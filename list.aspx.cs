using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class list : System.Web.UI.Page
{
    public encrypt en = new encrypt();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "K" || Session["usertype"].ToString() == "D") { btn_download.Visible = true; Button2.Visible = true; } else { btn_download.Visible = false; Button2.Visible = false; }
            if (!IsPostBack)
            {
                LoadData();
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
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    if (Session["uid"] != null)
                    {
                        //DC 04-09-2019
                        //if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                        //{
                        //    if (Session["AccessUid"].ToString()!="")
                        //    {
                        //        cmd = new SqlCommand("SELECT * FROM ContactSheet WHERE UID in ('" + Session["AccessUid"].ToString() + "') ORDER BY ClientName, BranchLocation", con);
                        //    }
                        //    else
                        //    {
                        //        cmd = new SqlCommand("SELECT * FROM ContactSheet WHERE UID = '" + Session["uid"].ToString() + "' ORDER BY ClientName, BranchLocation", con);
                        //    }
                        //}
                        //else
                        //{
                            cmd = new SqlCommand("SELECT * FROM ContactSheet ORDER BY ClientName, BranchLocation", con);
                        //}
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT * FROM ContactSheet ORDER BY ClientName, BranchLocation", con);
                    }
                    
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    if (ddl_client.SelectedValue.ToString().Length > 2)
                    {
                        ds.Tables[0].DefaultView.RowFilter = "ClientName = '" + ddl_client.SelectedValue.ToString() + "'";
                        DataTable dt = (ds.Tables[0].DefaultView).ToTable();
                        gv_contact.DataSource = dt;
                        gv_contact.DataBind();
                    }
                    else
                    {
                        gv_contact.DataSource = ds.Tables[0];
                        gv_contact.DataBind();

                        DataView view = new DataView(ds.Tables[0]);
                        DataTable distinctValues = view.ToTable(true, "ClientName");

                        ddl_client.DataSource = distinctValues;
                        ddl_client.DataTextField = "ClientName";
                        ddl_client.DataValueField = "ClientName";
                        ddl_client.DataBind();
                        ddl_client.Items.Insert(0, new ListItem("Select Client Name", "0"));
                    }
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

    protected void btn_validate_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void gv_contact_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_contact.PageIndex = e.NewPageIndex;
        LoadData();
    }

    protected void btn_response_Command(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
        id = en.Encryption(id);
        Response.Redirect("edit-contact.aspx?id=" + id);
    }

    protected void btn_response_Command1(object sender, CommandEventArgs e)
    {
        string id = (String)e.CommandArgument;
       // id = en.Encryption(id);
        Response.Redirect("list-lead.aspx?id=" + id);
    }

    protected void gv_contact_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label UID = e.Row.FindControl("UID") as Label;
            Button EditButton = e.Row.FindControl("btn_proposal") as Button;
            Button viewbutton = e.Row.FindControl("btn_view") as Button;
            if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "D")
            {

            }
            else
            {
                if (Session["uid"].ToString() == UID.Text)
                {
                    EditButton.Enabled = true;
                    viewbutton.Enabled = true;

                }
                else
                {
                    EditButton.Enabled = false;
                    viewbutton.Enabled = false;
                }
            }
        }
    }

    protected void btn_download_Click(object sender, EventArgs e)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        string id = "";
        string cond = "";
        if (ddl_client.SelectedValue.ToString() != "0")
        {
            cond += " AND C.ClientName = '" + ddl_client.SelectedValue.ToString() + "'";
        }

        DataSet ds = new DataSet();
        using (con)
        {
            string Query = "SELECT C.ID, L.USERNAME AS [ADDED BY], ClientName, Office, Division, BranchLocation, ClientContact, Boardline, DirectLine, Mobile, C.EmailID, [Function], FunctionOthers, Designation, C.Gender, LOGDATE FROM ContactSheet C LEFT JOIN [Login] L ON C.UID = CONVERT(varchar(10), L.ID) WHERE C.ACTIVE = 'Y' " + cond;
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
        }

        //Create a dummy GridView
        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();

        Response.Clear();
        Response.Buffer = true;
        string filename = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        Response.AddHeader("content-disposition", "attachment;filename=Client Contact Tracker " + filename + ".xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridView1.Rows[i].Attributes.Add("class", "textmode");
        }
        GridView1.RenderControl(hw);

        //style to format numbers to string
        string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        Response.Write(style);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Server.Transfer("Index.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        Server.Transfer("Ragistration.aspx");
    }
}