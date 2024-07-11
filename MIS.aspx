<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MIS.aspx.cs" Inherits="MIS" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lead Management System</title>
    <link rel="apple-touch-icon" sizes="57x57" href="assets/favicon/apple-icon-57x57.png" />
    <link rel="apple-touch-icon" sizes="60x60" href="assets/favicon/apple-icon-60x60.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="assets/favicon/apple-icon-72x72.png" />
    <link rel="apple-touch-icon" sizes="76x76" href="assets/favicon/apple-icon-76x76.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="assets/favicon/apple-icon-114x114.png" />
    <link rel="apple-touch-icon" sizes="120x120" href="assets/favicon/apple-icon-120x120.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="assets/favicon/apple-icon-144x144.png" />
    <link rel="apple-touch-icon" sizes="152x152" href="assets/favicon/apple-icon-152x152.png" />
    <link rel="apple-touch-icon" sizes="180x180" href="assets/favicon/apple-icon-180x180.png" />
    <link rel="icon" type="image/png" sizes="192x192" href="assets/favicon/android-icon-192x192.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="assets/favicon/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="96x96" href="assets/favicon/favicon-96x96.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="assets/favicon/favicon-16x16.png" />
    <link rel="manifest" href="assets/favicon/manifest.json" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="msapplication-TileImage" content="assets/favicon/ms-icon-144x144.png" />
    <meta name="theme-color" content="#ffffff" />
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />


    <link rel="stylesheet" href="assets/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/bower_components/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/bower_components/Ionicons/css/ionicons.min.css" />
    <link rel="stylesheet" href="assets/dist/css/AdminLTE.min.css" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />

    <link href="https://unpkg.com/purecss@1.0.0/build/pure-min.css" rel="stylesheet" />
    <link href="assets/menu.css" rel="stylesheet" />
    <link href="assets/main.css" rel="stylesheet" />
    <link href="assets/table.css" rel="stylesheet" />
    <link href="assets/sticky.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class='nav'>
            <ul>
                <li>
                    <a class='logo' href='Home.aspx'>
                        <img src="assets/images/logobird.png" style="width: 100%;" />
                    </a>
                </li>
                <li>
                    <a href='Home.aspx'>Lead Management System</a>
                </li>
                <li>
                    <a href="list.aspx">Client Contact Tracker</a>
                </li>
                <li>
                    <a href="list-lead.aspx">Lead Tracker</a>
                </li>
                <li>
                    <a href="list-proposal.aspx">Proposal Tracker</a>
                </li>
                <li>
                    <%
                        if (Session["usertype"] != null)
                        {
                            if (Session["usertype"].ToString() == "M" || Session["usertype"].ToString() == "D")
                            {
                    %>
                    <a href="MIS.aspx">MIS Report</a>
                    <%
                        }
                        else
                        {
                    %>
                    <a style="width: 50px;"></a>
                    <%
                            }
                        }
                        else
                        {
                            Response.Redirect("Default.aspx");
                        }
                    %>                    
                </li>
                <li>
                    <a href='logout.aspx'>
                        <div class='fa fa-sign-out font22'></div>
                    </a>
                </li>
            </ul>
            <%
                if (Session["flag"] != null)
                {
                    if (Session["flag"].ToString() != "R" && Session["flag"].ToString() != "K" && Session["flag"].ToString() != "M" && Session["flag"].ToString() != "A")
                    {
            %>
            <div class="icon-bar">
                <a href="ChangeSession.aspx?opt=S" class="facebook">Self</a>
                <a href="ChangeSession.aspx?opt=A" class="twitter">All</a>
               <%-- <a href="RightsExchange.aspx" class="youtube">Rights Exchange</a>--%>
            </div>
            <%
                    }
                }
            %>
        </div>
        <div>
            <table class="pure-table pure-table-horizontal" style="border: 0;">
                <tr>
                    <td style="width: 6%;">
                        <span style="font-size: 12px; margin-top: 18px;">From Date :</span>
                    </td>
                    <td style="width: 10%;">
                        <input type="text" style="width: 140px; height: 10px;" runat="server" id="txt_fromdate" autocomplete="off" class="form-control pull-right" />
                    </td>
                    <td style="width: 5%;">
                        <span style="font-size: 12px; margin-top: 18px;">To Date :</span>
                    </td>
                    <td style="width: 6%;">
                        <input type="text" style="width: 140px; height: 10px;" runat="server" id="txt_todate" autocomplete="off" class="form-control pull-right" />
                    </td>
                    <td style="width: 10%;">
                        <asp:DropDownList runat="server" ID="ddl_option1" CssClass="form-control pull-right" Style="width: 140px; height: 25px; padding: 1px 12px;">
                            <asp:ListItem Value="LeadDetails">Leads</asp:ListItem>
                            <asp:ListItem Value="ProposalTracker">Proposal</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 6%;">
                        <asp:Button runat="server" ID="Button1" CssClass="btn button-success pure-button" Text="Search" Style="margin: 0; border-radius: 3px; padding: 3px 12px;" OnClick="Button1_Click" />
                    </td>
                    <td style="text-align: right;">
                        <asp:Button runat="server" ID="btn_download" CssClass="btn button-success pure-button" Text="Download Excel" Style="margin: 0; border-radius: 3px; padding: 3px 12px;" OnClick="btn_download_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <%= table %>
        <script src="assets/bower_components/jquery/dist/jquery.min.js"></script>
        <script src="assets/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
        <script src="assets/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
        <script src="assets/bower_components/fastclick/lib/fastclick.js"></script>
        <script src="assets/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
        <script src="assets/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
        <script src="assets/dist/js/adminlte.min.js"></script>
        <script>
            $(".nav li").hover(function () {
                $(this).children("ul").stop().delay(200).animate({ height: "toggle", opacity: "toggle" }, 200);
            });

            $(document).ready(function () {
                $('#txt_fromdate').datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy'
                });

                $('#txt_todate').datepicker({
                    autoclose: true,
                    format: 'dd/mm/yyyy'
                });
            });
        </script>
    </form>
</body>
</html>
