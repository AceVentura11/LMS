using System;
using System.Data;
using System.IO;
using System.Web.UI;
using ClosedXML.Excel;

public partial class MIS : Page
{
    public encrypt en = new encrypt();
    public DataRow[] foundRow;
    DataTable dt = new DataTable("Report");
    Controller controller = new Controller();
    public string table;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
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
        DataSet ds = controller.GetDataSet("SELECT * FROM [Login] WHERE ID IN (2,5,10,12,16,17,19,25,123,33,34,38,124,58,62,64,73,74,95,96,106,107,108,112) ORDER BY USERNAME");

        if (txt_fromdate.Value != "" && txt_todate.Value != "")
        {
            DataSet ds4 = controller.GetDataSet("SELECT TeamHead, COUNT([UID]) AS [Total] FROM " + ddl_option1.SelectedValue.ToString() + " WHERE (LOGDATE >= convert(datetime,'" + txt_fromdate.Value + "', 103) AND LOGDATE <= DATEADD(DAY, 1, convert(datetime,'" + txt_todate.Value + "', 103))) GROUP BY TeamHead");
            DataTable dt4 = ds4.Tables[0];
            table = "<table class='pure-table pure-table-bordered' style='margin-top: 0px;'><tr><td style='width:50%; background-color:#CCC;'>Team Head</td><td style='width:50%; background-color:#CCC;'>Count</td></tr>";
            dt.Clear();
            dt.Columns.Add("Team Head");
            dt.Columns.Add("Count");
            int total = 0;
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string row4 = Find_row(ds.Tables[0].Rows[i]["USERNAME"].ToString(), dt4);
                    table += "<tr><td>" + ds.Tables[0].Rows[i]["USERNAME"].ToString() + "</td><td>" + row4 + "</td></tr>";
                    total = total + Convert.ToInt32(row4);
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables[0].Rows[i]["USERNAME"].ToString();
                    dr[1] = Convert.ToInt32(row4);
                    dt.Rows.Add(dr);
                }
                table += "<tr><td style='background-color:#CCC;'>Total</td><td style='background-color:#CCC;'>" + total + "</td></tr>";
                table += "</table>";
                DataRow dr1 = dt.NewRow();
                dr1[0] = "Total";
                dr1[1] = total;
                dt.Rows.Add(dr1);
                Session.Add("DT", dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                txt_todate.Value = "";
                txt_fromdate.Value = "";
            }
        }
        else
        {
            DataSet ds1 = controller.GetDataSet("SELECT TeamHead, COUNT([UID]) AS [Total], DATENAME(m, LOGDATE) AS [MONTH] FROM " + ddl_option1.SelectedValue.ToString() + " WHERE DATEPART(m, LOGDATE) = DATEPART(m, DATEADD(m, -2, getdate())) AND DATEPART(yyyy, LOGDATE) = DATEPART(yyyy, DATEADD(m, -2, getdate())) GROUP BY TeamHead, DATENAME(m, LOGDATE);");
            DataSet ds2 = controller.GetDataSet("SELECT TeamHead, COUNT([UID]) AS [Total], DATENAME(m, LOGDATE) AS [MONTH] FROM " + ddl_option1.SelectedValue.ToString() + " WHERE DATEPART(m, LOGDATE) = DATEPART(m, DATEADD(m, -1, getdate())) AND DATEPART(yyyy, LOGDATE) = DATEPART(yyyy, DATEADD(m, -1, getdate())) GROUP BY TeamHead, DATENAME(m, LOGDATE);");
            DataSet ds3 = controller.GetDataSet("SELECT TeamHead, COUNT([UID]) AS [Total], DATENAME(m, LOGDATE) AS [MONTH] FROM " + ddl_option1.SelectedValue.ToString() + " WHERE DATEPART(m, LOGDATE) = DATEPART(m, DATEADD(m, 0, getdate())) AND DATEPART(yyyy, LOGDATE) = DATEPART(yyyy, DATEADD(m, 0, getdate())) GROUP BY TeamHead, DATENAME(m, LOGDATE);");
            DataTable dt1 = ds1.Tables[0];
            DataTable dt2 = ds2.Tables[0];
            DataTable dt3 = ds3.Tables[0];
            table = "<table class='pure-table pure-table-bordered' style='margin-top: 0px;'><tr><td style='width:20%; background-color:#CCC;'>Team Head</td><td style='width:30%; background-color:#CCC;'>" + dt1.Rows[0]["MONTH"].ToString() + "</td><td style='width:30%; background-color:#CCC;'>" + dt2.Rows[0]["MONTH"].ToString() + "</td><td style='width:30%; background-color:#CCC;'>" + dt3.Rows[0]["MONTH"].ToString() + "</td></tr>";
            dt.Clear();
            dt.Columns.Add("Team Head");
            dt.Columns.Add(dt1.Rows[0]["MONTH"].ToString());
            dt.Columns.Add(dt2.Rows[0]["MONTH"].ToString());
            dt.Columns.Add(dt3.Rows[0]["MONTH"].ToString());
            int total1 = 0;
            int total2 = 0;
            int total3 = 0;
            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string row1 = Find_row(ds.Tables[0].Rows[i]["USERNAME"].ToString(), dt1);
                    string row2 = Find_row(ds.Tables[0].Rows[i]["USERNAME"].ToString(), dt2);
                    string row3 = Find_row(ds.Tables[0].Rows[i]["USERNAME"].ToString(), dt3);

                    table += "<tr><td>" + ds.Tables[0].Rows[i]["USERNAME"].ToString() + "</td><td>" + row1 + "</td><td>" + row2 + "</td><td>" + row3 + "</td></tr>";
                    DataRow dr = dt.NewRow();
                    dr[0] = ds.Tables[0].Rows[i]["USERNAME"].ToString();
                    dr[1] = row1;
                    dr[2] = row2;
                    dr[3] = row3;
                    total1 = total1 + Convert.ToInt32(row1);
                    total2 = total2 + Convert.ToInt32(row2);
                    total3 = total3 + Convert.ToInt32(row3);
                    dt.Rows.Add(dr);
                }
                table += "<tr><td style='background-color:#CCC;'>Total</td><td style='background-color:#CCC;'>" + total1 + "</td><td style='background-color:#CCC;'>" + total2 + "</td><td style='background-color:#CCC;'>" + total3 + "</td></tr>";
                table += "</table>";
                DataRow dr2 = dt.NewRow();
                dr2[0] = "Total";
                dr2[1] = total1;
                dr2[2] = total2;
                dr2[3] = total3;
                dt.Rows.Add(dr2);
                Session.Add("DT", dt);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                
            }
        }
    }

    public string Find_row(string find, DataTable dtfind)
    {
        foundRow = dtfind.Select("TeamHead=\'" + find + "\'");
        if (foundRow.Length > 0)
        {
            return foundRow[0][1].ToString();
        }
        else
        {
            return "0";
        }
    }

    protected void btn_download_Click(object sender, EventArgs e)
    {
        dt = (DataTable)Session["DT"];
        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dt);
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            string value = "attachment;filename=LeadTracker" + filename + ".xlsx";
            Response.AddHeader("content-disposition", value);
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}