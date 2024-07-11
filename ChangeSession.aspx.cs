using System;
using System.Web;

public partial class ChangeSession : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Request.UrlReferrer.ToString();
        if (Session["flag"] != null)
        {
            //DC 18-09-2019 if (Session["flag"].ToString() == "D" || Session["flag"].ToString() == "T")
            if (Session["flag"].ToString() == "D" || Session["flag"].ToString() == "T" || Session["flag"].ToString() == "New Qnt Head" || Session["flag"].ToString() == "IBD Head" || Session["flag"].ToString() == "New Qual Head")
            {
                string opt = Request.QueryString["opt"].ToString();
                if (opt == "S")
                    Session["usertype"] = "R";
                else
                    Session["usertype"] = Session["flag"];
            }
            
        }
        Response.Redirect(url);
    }
}