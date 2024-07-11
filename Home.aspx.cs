using System;
using System.Web;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["logname"] == null)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            Session["url_referer"] = url;
            Response.Redirect("Default.aspx");
        }
    }
}