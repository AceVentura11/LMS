using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
    public static string refurl = "";
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
           
            DataSet ds = new DataSet();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM [LOGIN] WHERE USERID = @uname AND [PASSWORD] = @password", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@uname", txt_uname.Value);
                cmd.Parameters.AddWithValue("@password", txt_password.Value);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["FLAG"].ToString() == "N")
                    {
                        Response.Redirect("changepassword.aspx");
                    }
                    else
                    {
                        
                        Session["finyear"] = "22-23";
                        Session["logid"] = ds.Tables[0].Rows[0]["userid"].ToString();
                        Session["uid"] = ds.Tables[0].Rows[0]["ID"].ToString();
                        Session["team"] = ds.Tables[0].Rows[0]["TEAM"].ToString();
                        Session["usertype"] = ds.Tables[0].Rows[0]["USERTYPE"].ToString();
                        Session["flag"] =  ds.Tables[0].Rows[0]["USERTYPE"].ToString();
                        Session["logname"] = txt_uname.Value;
                        Session["gender"] = ds.Tables[0].Rows[0]["GENDER"].ToString();
                        AccessUID();
                        if (Session["url_referer"] != null)
                        {
                            Response.Redirect(Session["url_referer"].ToString());

                        }
                        else
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

                            
                            string Query = "SELECT * FROM ProposalTracker P WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) and P.status not in ('Lost','Commissioned')  AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') not between  '" + Sd + "' and  '" + Ed + "'";
                            if (Session["uid"] != null)
                            {
                                if (Session["uid"].ToString() != "" && Session["usertype"].ToString() == "R")
                                {
                                    if (Session["AccessUid"].ToString() != "")
                                    {

                                        Query = "SELECT * FROM ProposalTracker P WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID in ('" + Session["AccessUid"].ToString() + "') and P.status not in ('Lost','Commissioned') AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') not between  '" + Sd + "' and  '" + Ed + "'";
                                    }
                                    else
                                    {

                                        Query = "SELECT * FROM ProposalTracker P WHERE (P.ACTIVE = 'Y' OR P.ACTIVE IS NULL) AND P.UID = '" + Session["uid"].ToString() + "'  and P.status not in ('Lost','Commissioned') AND PARSE(p.Dateofsendingproposal as date using 'AR-LB') not between  '" + Sd + "' and  '" + Ed + "'";
                                    }
                                }
                            }
                            con.Open();
                            DataSet ds1 = new DataSet();
                            SqlCommand cmd1 = new SqlCommand(Query, con);
                            cmd1.CommandType = CommandType.Text;                           
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(ds1);
                            con.Close();
                            if (ds1.Tables[0].Rows.Count > 0)
                            {
                                
                                Response.Redirect("ProposalStatusTracking.aspx");
                            }
                            else
                            {

                                Response.Redirect("Home.aspx");
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Invalid username or password.');", true);
                }
            }
        }
        catch
        {
            
        }
    }

    public void AccessUID()
    {
        try
        {
            con.Open();
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SELECT * FROM RightsExchange WHERE UID = @UID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UID", Session["uid"].ToString());
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                string tempAccessUid= Session["uid"].ToString() + "," + ds.Tables[0].Rows[0]["AccessedUID"].ToString();
                Session["AccessUid"] = tempAccessUid.Replace(",", "','");
                string a = Session["AccessUid"].ToString();
            }
            else
            {
                Session["AccessUid"] = "";
            }
           
        }
        catch (Exception)
        {

        }
    }
}