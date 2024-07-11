using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class changepassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            DataSet ds = new DataSet();
            using (con)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATEPASSWORD", con);
                cmd.Parameters.AddWithValue("@username", txt_uname.Value);
                cmd.Parameters.AddWithValue("@oldpassword", txt_oldpassword.Value);
                cmd.Parameters.AddWithValue("@newpassword", txt_password.Value);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            
        }
    }
}