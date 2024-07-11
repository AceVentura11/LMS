using System;

public partial class logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["logname"] = "";
        Session["logname"] = null;
        Session.Abandon();
        Response.Redirect("Default.aspx");
    }
}