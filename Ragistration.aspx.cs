using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Resources;
using System.Text.RegularExpressions;

public partial class Ragistration : System.Web.UI.Page
{
    
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           loaddata();
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (txtUserName.Value!="" && txtUserID.Value!="" && txtEmailID.Value!="")
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(txtEmailID.Value);
            if (match.Success)
            {

                if (ddlUsType.Value != "" && ddlTeam.Value != "" && ddl_gender.Value != "")
                {
                    string password = GenerateOTP();

                    SqlCommand obj = new SqlCommand("spRagistration", con);
                    obj.CommandType = CommandType.StoredProcedure;

                    obj.Parameters.AddWithValue("@Type", "Insert");
                    obj.Parameters.AddWithValue("@USERNAME    ", txtUserName.Value);
                    obj.Parameters.AddWithValue("@USERID      ", txtUserID.Value);
                    obj.Parameters.AddWithValue("@EMAILID     ", txtEmailID.Value);
                    obj.Parameters.AddWithValue("@PASSWORD    ", password);
                    obj.Parameters.AddWithValue("@OLDPASSWORD ", "null");
                    obj.Parameters.AddWithValue("@USERTYPE    ", ddlUsType.Value);
                    obj.Parameters.AddWithValue("@TEAM        ", ddlTeam.Value);
                    obj.Parameters.AddWithValue("@ACTIVE      ", "Y");
                    obj.Parameters.AddWithValue("@CREATEDDATE ", DateTime.Now);
                    obj.Parameters.AddWithValue("@MODIFIEDDATE", "");
                    obj.Parameters.AddWithValue("@FLAG        ", "N");
                    obj.Parameters.AddWithValue("@GENDER      ", ddl_gender.Value);
                    con.Open();

                    try
                    {
                        if (usercheck() == "false")
                        {
                            obj.ExecuteNonQuery();
                            string email = txtEmailID.Value;
                            string USERNAME = txtUserName.Value;
                            Email em = new Email();
                            string path = HttpContext.Current.Server.MapPath("~/emailers/resetpassword.html");
                            string content = System.IO.File.ReadAllText(path);
                            content = content.Replace("{{USER}}", USERNAME);
                            content = content.Replace("{{Password}}", password);
                            em.Mail("New Password", content, email, "", "");

                            con.Close();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Data saved successfully.');", true);
                            clear();
                        }


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    if (ddlUsType.Value == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select User Type');", true);

                    }
                    else if (ddlTeam.Value == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select Team Name');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Select Gender');", true);

                    }

                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Invalid Email Address');", true);            
            }
        }
        else
        {
            if(txtUserName.Value=="")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Enter User Name');", true);
            }
            else if(txtUserID.Value=="")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Enter User ID');", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Please Enter User Email ID');", true);

            }

        }


    }
    protected string usercheck()
    {
        string temp = "false";
        DataSet ds = new DataSet();
        //con.Open();
        try
        {

            SqlCommand obj = new SqlCommand("Select * from Login where USERID=@USERID", con);
            obj.CommandType = CommandType.Text;
            obj.Parameters.AddWithValue("@USERID", txtUserID.Value);
            SqlDataAdapter da = new SqlDataAdapter(obj);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                temp = "true";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('This UserID alredy Used..!');", true);
            }
            
        }
        catch (Exception ex)
        {

            throw ex;
        }
       
        return temp;
    }
    private void clear()
    {
        try
        {
            txtUserName.Value = "";
            txtUserID.Value = "";
            txtEmailID.Value = "";
            ddlUsType.SelectedIndex = 0;
            ddlTeam.SelectedIndex = 0;
            ddl_gender.SelectedIndex = 0;
        }
        catch (Exception)
        {

            // throw;
        }

    }

    protected void loaddata()
    {
        try
        {


            DataSet ds = new DataSet();

            con.Open();
            SqlCommand cmd = new SqlCommand();
           // cmd = new SqlCommand("SELECT USERID,ID FROM login where USERID!='' and USERTYPE='T' and ACTIVE='Y' ORDER BY USERID", con);
            //DC 05/11/2020 cmd = new SqlCommand("SELECT * FROM login where USERID!='' and USERTYPE='T' and ACTIVE='Y' or USERID in ('Piyali Chatterjee','Shaleena') ORDER BY USERID", con);
           //DC 31-12-2021 cmd = new SqlCommand("SELECT * FROM login where USERID!='' and USERTYPE='T' and ACTIVE='Y' or USERID in ('Piyali Chatterjee','Shaleena','Anil Rathod') ORDER BY USERID", con);
            cmd = new SqlCommand("SELECT * FROM login where USERID!='' and USERTYPE='T' and ACTIVE='Y' or USERID in ('Piyali Chatterjee','Shaleena','Anil Rathod','Ashish Karnad') ORDER BY USERID", con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            ddlTeam.DataSource = ds;
            ddlTeam.DataTextField = "USERID";
            ddlTeam.DataValueField = "ID";
            ddlTeam.DataBind();
            ddlTeam.Items.Insert(0, new ListItem("Select Team Name", ""));
        }
        catch (Exception)
        {

            //throw;
        }

        try
        {
            //DataSet ds = new DataSet();

            //con.Open();
            //SqlCommand cmd = new SqlCommand();
            //cmd = new SqlCommand("SELECT distinct Usertype FROM login where Usertype!='' ORDER BY Usertype", con);
            //cmd.CommandType = CommandType.Text;
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(ds);
            //con.Close();

            //ddlUsType.DataSource = ds;
            //ddlUsType.DataTextField = "Usertype";
            //ddlUsType.DataValueField = "Usertype";
            //ddlUsType.DataBind();
            ddlUsType.Items.Insert(0, new ListItem("Select User type", ""));
            ddlUsType.Items.Insert(1, new ListItem("D", "D"));
            ddlUsType.Items.Insert(2, new ListItem("K", "K"));
            ddlUsType.Items.Insert(3, new ListItem("M", "M"));
            ddlUsType.Items.Insert(4, new ListItem("R", "R"));
            ddlUsType.Items.Insert(5, new ListItem("T", "T"));
            ddlUsType.Items.Insert(6, new ListItem("New Qnt Head", "New Qnt Head"));
            ddlUsType.Items.Insert(7, new ListItem("New Qual Head", "New Qual Head"));
            ddlUsType.Items.Insert(8, new ListItem("IBD Head", "IBD Head"));
            ddlUsType.Items.Insert(9, new ListItem("CX", "CX"));
            ddlUsType.Items.Insert(10, new ListItem("IDF Head", "IDF Head"));
            ddlUsType.Items.Insert(11, new ListItem("Media", "Media"));//DC 31-12-2021

            
        }
        catch (Exception)
        {

            //  throw;
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
