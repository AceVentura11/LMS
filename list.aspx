<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list.aspx.cs" Inherits="list" %>

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
    <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet" />
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
              <%--  <a href="RightsExchange.aspx" class="youtube">Rights Exchange</a>--%>
            </div>
            <%
                    }
                }
            %>
        </div>
        <div>
            <table class="pure-table pure-table-horizontal" style="border: 0;">
                <tr>
                    <td style="width:10%;"> <asp:Button runat="server" ID="Button1" CssClass="btn button-success pure-button" Text="Add Client Contact" Style="margin: 0 5px 0 0;" OnClick="Button1_Click"/></td>
                     
                        <td style="width:69%;"> <asp:Button runat="server" ID="Button2" CssClass="btn button-success pure-button" Text="User Registration" Style="margin: 0 5px 0 0;" OnClick="Button2_Click"/>
               </td>
                 
                    <td style="width:10%;">
                        <asp:DropDownList Style="width: 300px" runat="server" ID="ddl_client" CssClass="form-control pull-right"></asp:DropDownList>
                    </td>
                    <td style="width:5%;">
                        <asp:Button runat="server" ID="btn_validate" CssClass="btn button-success pure-button" Text="Submit" Style="margin: 0 5px 0 0;" OnClick="btn_validate_Click" />
                    </td>
                    <td style="width:6%;">
                        <asp:Button runat="server" ID="btn_download" CssClass="btn button-success pure-button" Text="Download Excel" Style="margin: 0; border-radius: 3px;" OnClick="btn_download_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:GridView style="margin-top: 0px;" runat="server" ID="gv_contact" GridLines="Both" AllowPaging="true" CssClass="pure-table pure-table-bordered" OnPageIndexChanging="gv_contact_PageIndexChanging" AutoGenerateColumns="false" PageSize="10" OnRowDataBound="gv_contact_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="UID" Text='<%# Eval("UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Client Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ClientName" Text='<%# Eval("ClientName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Office">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Office" Text='<%# Eval("Office") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Division">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Division" Text='<%# Eval("Division") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Branch Location">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="BranchLocation" Text='<%# Eval("BranchLocation") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Client Contact">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ClientContact" Text='<%# Eval("ClientContact") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               <%--DC 24-05-2019 <asp:TemplateField HeaderText="Email Id">--%>
                 <asp:TemplateField HeaderText="Email Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="EmailID" Text='<%# Eval("EmailID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_proposal" Text="Edit" OnCommand="btn_response_Command" CssClass="button-warning pure-button" CommandArgument='<%# Eval("ID") %>' CommandName="response" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View Lead">
                    <ItemTemplate>
                      <%--  <a href='list-lead.aspx?id=<%# Eval("ID") %>'><span class="button-success pure-button">View</span></a>--%>
                         <asp:Button runat="server" ID="btn_view" Text="View" OnCommand="btn_response_Command1" CssClass="button-success pure-button" CommandArgument='<%# Eval("ID") %>' CommandName="response1" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Add Lead">
                    <ItemTemplate>
                        <a href='leadmanagement.aspx?id=<%# Eval("ID") %>'><span class="button-secondary pure-button">Add</span></a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
        <script src="assets/js/jquery.min.js"></script>
        <script>
            $(".nav li").hover(function () {
                $(this).children("ul").stop().delay(200).animate({ height: "toggle", opacity: "toggle" }, 200);
            });
        </script>
    </form>
</body>
</html>
