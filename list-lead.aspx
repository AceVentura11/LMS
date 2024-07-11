<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list-lead.aspx.cs" Inherits="list_lead" EnableEventValidation="false" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
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
                <%--<a href="RightsExchange.aspx" class="youtube">Rights Exchange</a>--%>
            </div>
            <%
                    }
                }
            %>
        </div>
        <table class="pure-table pure-table-horizontal" style="border: 0;">
            <tr>
                <td style="width: 39%;"></td>
                <td style="width:10%;">
                    <asp:DropDownList runat="server" ID="ddl_Result" CssClass="form-control pull-right" Style="width: 140px">
                        <asp:ListItem Value="">Result</asp:ListItem>
                        <asp:ListItem Value="NOT NULL">Proposal sent</asp:ListItem>
                        <asp:ListItem Value="NULL">Proposal not sent</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:10%;">
                    <asp:DropDownList runat="server" ID="ddl_Location" CssClass="form-control pull-right" Style="width: 140px">
                        <asp:ListItem Value="">Location</asp:ListItem>
                        <asp:ListItem Value="Banglore">Bangalore</asp:ListItem>
                        <asp:ListItem Value="Delhi">Delhi</asp:ListItem>
                        <asp:ListItem Value="Mumbai">Mumbai</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:10%;">
                    <asp:DropDownList runat="server" ID="ddl_NatureofStudy" CssClass="form-control pull-right" Style="width: 140px">
                    </asp:DropDownList>
                </td>
                <td style="width:10%;">
                    <asp:DropDownList runat="server" ID="ddl_IndustryVertical" CssClass="form-control pull-right" Style="width: 140px">
                        <asp:ListItem Value="">Industry Vertical</asp:ListItem>
                        <asp:ListItem Value="AD">Ad Agency</asp:ListItem>
                        <asp:ListItem Value="AGRI">Agricultural</asp:ListItem>
                        <asp:ListItem Value="AUTO">Automotive</asp:ListItem>
                        <asp:ListItem Value="B2B">B2B</asp:ListItem>
                        <asp:ListItem Value="BMED">Broadcast Media</asp:ListItem>
                        <asp:ListItem Value="BFSI">BFSI</asp:ListItem>
                        <asp:ListItem Value="DURA">Durables</asp:ListItem>
                        <asp:ListItem Value="FINA">Financial Services</asp:ListItem>
                        <asp:ListItem Value="FMCG">FMCG</asp:ListItem>
                        <asp:ListItem Value="PSU">Govt / PSU</asp:ListItem>
                        <asp:ListItem Value="IT">IT</asp:ListItem>
                        <asp:ListItem Value="LOGO">Logistics</asp:ListItem>
                        <asp:ListItem Value="MNE">Media & Entertainment</asp:ListItem>
                        <asp:ListItem Value="PM">Pharmaceuticals / Medical / Healthcare</asp:ListItem>
                        <asp:ListItem Value="MEDI">Print Media</asp:ListItem>
                        <asp:ListItem Value="RETI">Retail</asp:ListItem>
                        <asp:ListItem Value="SOC">Social Research</asp:ListItem>
                        <asp:ListItem Value="TELE">Telecom</asp:ListItem>
                        <asp:ListItem Value="TNH">Travel &amp; Hospitality</asp:ListItem>
                        <asp:ListItem Value="OSW">Outsourced Work</asp:ListItem>
                        <asp:ListItem Value="OTHS">Other Services</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width:10%;">
                    <asp:DropDownList runat="server" ID="ddl_unit" CssClass="form-control pull-right" Style="width: 140px">
                        <%--DC 24-05-2019 <asp:ListItem Value="">Unit</asp:ListItem>--%>
                        <asp:ListItem Value="">Unit Head</asp:ListItem>
                        <asp:ListItem Value="QN">Quantitative</asp:ListItem>
                        <asp:ListItem Value="QL">Qualitative</asp:ListItem>
                        <asp:ListItem Value="MD">Media</asp:ListItem>
                        <asp:ListItem Value="IBD">IBD</asp:ListItem>
                        <asp:ListItem Value="IDF">IDF</asp:ListItem>
                        <asp:ListItem Value="B2B">B2B</asp:ListItem>
                        <asp:ListItem Value="ICI">ICICI</asp:ListItem>
                        <%--DC 04-06-2020--%>
                        <asp:ListItem Value="CX">CX</asp:ListItem>
                        <%--DC 04-06-2020--%>
                    </asp:DropDownList>
                </td>
                <td style="width:5%;">
                    <asp:Button runat="server" ID="btn_validate" CssClass="btn button-success pure-button" Text="Search" Style="margin: 0 5px 0 0; border-radius: 3px;" OnClick="btn_validate_Click" />
                </td>
                <td style="width:6%;">
                    <asp:Button runat="server" ID="btn_download" CssClass="btn button-success pure-button" Text="Download Excel" Style="margin: 0; border-radius: 3px;" OnClick="download_Click" />
                </td>
            </tr>
        </table>
        <asp:GridView Style="margin-top: 0px;" EmptyDataText="No leads available currently" runat="server" ID="gv_list" GridLines="Both" AutoGenerateColumns="false" PageSize="10" AllowPaging="true" OnPageIndexChanging="gv_list_PageIndexChanging" CssClass="pure-table pure-table-bordered">
            <Columns>
                <asp:TemplateField HeaderText="Client Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ClientName" Text='<%# Eval("ClientName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Client Contact">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ClientContact" Text='<%# Eval("ClientContact") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Topic of the lead">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Topicofthelead" Text='<%# Eval("ProposalName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date of receiving lead">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Dateofreceivinglead" Text='<%# Eval("Dateofreceivinglead") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal Due Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ProposalDueDate" Text='<%# Eval("ProposalDueDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Allocated To">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LeadGenerator" Text='<%# Eval("Allotedto") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Result">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Result" Text='<%# Eval("RESULT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal Sent Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Dateofsendingproposal" Text='<%# Eval("Dateofsendingproposal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal Result">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ProposalResult" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
           <%--     <asp:TemplateField HeaderText="Proposal Link">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_proposal" Text="Add / View Proposal" OnCommand="btn_response_Command" CssClass="button-success pure-button" CommandArgument='<%# Eval("LID") %>' CommandName='<%# Eval("CID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_edit" Text="Edit Proposal" OnCommand="btn_response_Command" CssClass="button-warning pure-button" CommandArgument='<%# Eval("LID") %>' CommandName='<%# Eval("CID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_view" Text="View Proposal" OnCommand="btn_response_Command1" CssClass="button-success pure-button" CommandArgument='<%# Eval("LID") %>' CommandName='<%# Eval("CID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Add">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_Add" Text="Add Proposal" OnCommand="btn_response_Command2" CssClass="button-secondary pure-button" CommandArgument='<%# Eval("LID") %>' CommandName='<%# Eval("CID") %>'  />
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
