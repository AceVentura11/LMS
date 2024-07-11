using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

public partial class forgotpassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            string password = GenerateOTP();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
            DataSet ds = new DataSet();
            con.Open();
            SqlCommand cmd = new SqlCommand("RESETPASSWORD", con);
            cmd.Parameters.AddWithValue("@username", txt_uname.Value);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
            Email em = new Email();
            string path = HttpContext.Current.Server.MapPath("~/emailers/resetpassword.html");
            string content = System.IO.File.ReadAllText(path);
            content = content.Replace("{{USER}}", ds.Tables[0].Rows[0]["USERNAME"].ToString());
            content = content.Replace("{{Password}}", password);
          em.Mail("New Password", content, ds.Tables[0].Rows[0]["EMAILID"].ToString(), "", "");
          
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Forgot Password", "alert('New password has been mailed to your registered email id.');", true);
            txt_uname.Value = "";
        }
        catch
        {

        }
    }

    protected string GenerateOTP()
    {
        string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
        string numbers = "1234567890";

        string characters = numbers;
        characters += alphabets + small_alphabets + numbers;
        int length = int.Parse("8");
        string otp = string.Empty;
        for (int i = 0; i < length; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, characters.Length);
                character = characters.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        return otp;
    }
}