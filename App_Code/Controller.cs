using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Controller
/// </summary>
public class Controller
{
    public DataSet GetDataSet(string Query)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["proposaltrachercs"].ToString());
        DataSet ds = new DataSet();
        using (con)
        {            
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
        }
        return ds;
    }

    public DataSet GetDataSetFin(string Query)
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["TeamHeadcs"].ToString());
        DataSet ds = new DataSet();
        using (con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();
        }
        return ds;
    }
}