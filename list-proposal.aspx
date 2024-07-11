<%@ Page Language="C#" AutoEventWireup="true" CodeFile="list-proposal.aspx.cs" Inherits="list_proposal" %>

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
                    <%--<a class='logo' href='Home.aspx'>
                        <img src="assets/images/logobird.png" style="width: 100%;" />
                    </a>--%>
                    <a style="background: #ffce54" href='Home.aspx'>
                        <img src="assets/images/logobird.png" style="width: 30px; height: 27.5px;" />
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
                <a href="ProposalStatusTracking.aspx" class="youtube">Proposal Status Tracking</a>
                
            </div>
            <%
                    }
                    
                    else
                    {%>
                     <div class="icon-bar">
            <a href="ProposalStatusTracking.aspx" class="youtube">Proposal Status Tracking</a>
                </div>
            <%
            }
                }
            %>
         
        </div>
        <%--<table class="pure-table pure-table-horizontal" style="border: 0;">--%>
        <table class="pure-table pure-table-horizontal">
            <tr>
                <%--<td style="width: 20%;"></td>--%>
                <td>
                    <select runat="server" id="ddl_status" style="width: 180px;" data-placeholder="Status" multiple class="chosen-select">
                        <%-- class="form-control pull-right">--%>
                       <%-- <option value="">Status</option>--%>
                        <option value="Sent to client / awaiting response">Sent to client / awaiting response</option>
                        <option value="Reworking">Reworking</option>
                        <option value="Negotiating Cost">Negotiating Cost</option>
                        <option value="Commissioned">Commissioned</option>
                        <option value="Lost">Lost</option>
                    </select>
                </td>
                <td>
                    <select runat="server" id="ddl_originatingcentre" style="width: 180px;" data-placeholder="Location" multiple class="chosen-select">
                        <%-- class="form-control pull-right">--%>
                        <%--<option value="">Location</option>--%>
                        <option value="Banglore">Bangalore</option>
                        <option value="Delhi">Delhi</option>
                        <option value="Mumbai">Mumbai</option>
                    </select>

                </td>
                <td>
                    <select runat="server" id="ddl_natureofstudy" style="width: 180px;" data-placeholder="Select Client Name" multiple class="chosen-select">
                        <%-- class="form-control pull-right">--%>
                    </select>
                </td>
                <td>
                    <select runat="server" id="ddl_clientcategory" style="width: 180px;" data-placeholder="Industry Vertical" multiple class="chosen-select">
                        <%-- class="form-control pull-right">--%>
                      <%--  <option value="">Industry Vertical</option>--%>
                        <option value="AD">Ad Agency</option>
                        <option value="AGRI">Agricultural</option>
                        <option value="AUTO">Automotive</option>
                        <option value="B2B">B2B</option>
                        <option value="BMED">Broadcast Media</option>
                        <option value="BFSI">BFSI</option>
                        <option value="DURA">Durables</option>
                        <option value="FINA">Financial Services</option>
                        <option value="FMCG">FMCG</option>
                        <option value="PSU">Govt / PSU</option>
                        <option value="IT">IT</option>
                        <option value="LOGO">Logistics</option>
                        <option value="MNE">Media & Entertainment</option>
                        <option value="PM">Pharmaceuticals / Medical / Healthcare</option>
                        <option value="MEDI">Print Media</option>
                        <option value="RETI">Retail</option>
                        <option value="SOC">Social Research</option>
                        <option value="TELE">Telecom</option>
                        <option value="TNH">Travel &amp; Hospitality</option>
                        <option value="OSW">Outsourced Work</option>
                        <option value="OTHS">Other Services</option>
                    </select>
                </td>
                <td>
                    <asp:Button runat="server" ID="btn_validate" CssClass="btn button-success pure-button" Text="Search" Style="margin: 0 5px 0 0; border-radius: 3px;" OnClick="btn_search_Click" />
                </td>
                <%--<td></td>--%>
                <%--<td></td>--%>
            </tr>
            <tr>
                <td>
                    <select data-placeholder="Unit Head" runat="server" id="ddl_unit" style="width: 180px;" multiple class="chosen-select">
                        <%-- class="form-control pull-right">--%>
                        <%--<option value="">Unit Head</option>--%>
                        <option value="QN">Quantitative</option>
                        <option value="QL">Qualitative</option>
                        <option value="MD">Media</option>
                        <option value="IBD">IBD</option>
                        <option value="IDF">IDF</option>
                        <option value="B2B">B2B</option>
                        <option value="ICI">ICICI</option>
                        <%--dc 04-06-2020--%>
                        <option value="CX">CX</option>
                        <%--dc 04-06-2020--%>
                    </select>
                </td>
                <td>
                    <select runat="server" id="ddl_study" style="width: 180px;" data-placeholder="Type of Study" multiple class="chosen-select">
                        <%-- class="form-control pull-right">--%>
                        <%--<option value="">Type of Study</option>--%>
                        <option value="Accessibility study">Accessibility study</option>
                        <option value="Ad recall">Ad recall</option>
                        <option value="Ad test">Ad test</option>
                        <option value="Ad tracking">Ad tracking</option>
                        <option value="Adex">Adex</option>
                        <option value="Blend test">Blend test</option>
                        <option value="Brand perception">Brand perception</option>
                        <option value="Brand Track">Brand Track</option>
                        <option value="Brand Endorser">Brand Endorser</option>
                        <option value="Car Clinic">Car Clinic</option>
                        <option value="Catchment Area profiling">Catchment Area profiling</option>
                        <option value="CLT">CLT</option>
                        <option value="Competition pricing assessment study">Competition pricing assessment study</option>
                        <option value="Concept Test">Concept Test</option>
                        <option value="Consumer mood assessment study">Consumer mood assessment study</option>
                        <option value="Consumer Profiling">Consumer Profiling</option>
                        <option value="Consumer understanding / psyhographic profiling">Consumer understanding / psyhographic profiling</option>
                        <option value="Content Evaluation">Content Evaluation</option>
                        <option value="Corporate image study">Corporate image study</option>
                        <option value="Customer Satisfaction">Customer Satisfaction</option>
                        <option value="Day after recall">Day after recall</option>
                        <option value="Dealer Satisfaction">Dealer Satisfaction</option>
                        <option value="Diwali Monitor">Diwali Monitor</option>
                        <option value="Employee Satisfaction">Employee Satisfaction</option>
                        <option value="Entry Strategy study">Entry Strategy study</option>
                        <option value="Exit Poll">Exit Poll</option>
                        <option value="Feasibility Study">Feasibility Study</option>
                        <option value="Hansabus">Hansabus</option>
                        <option value="Idea generation / screening">Idea generation / screening</option>
                        <option value="Impact assessment study">Impact assessment study</option>
                        <option value="IPLomania">IPLomania</option>
                        <option value="Knowledge dissemination">Knowledge dissemination</option>
                        <option value="Launch assessment study">Launch assessment study</option>
                        <option value="Migration research">Migration research</option>
                        <option value="Modelling studies">Modelling studies</option>
                        <option value="Mystery Shopping">Mystery Shopping</option>
                        <option value="Name Test">Name Test</option>
                        <option value="Observational researches">Observational researches</option>
                        <option value="Opinion Poll">Opinion Poll</option>
                        <option value="Packaging tests">Packaging tests</option>
                        <option value="Population Census">Population Census</option>
                        <option value="Potential assessment study">Potential assessment study</option>
                        <option value="Price perception study">Price perception study</option>
                        <option value="Pricing Study">Pricing Study</option>
                        <option value="Print readership study">Print readership study</option>
                        <option value="Process effectiveness measurement">Process effectiveness measurement</option>
                        <option value="Product test">Product test</option>
                        <option value="Program evaluation">Program evaluation</option>
                        <option value="Ranking studies">Ranking studies</option>
                        <option value="Retail Audit">Retail Audit</option>
                        <option value="Retail Census">Retail Census</option>
                        <option value="Service Tracking study">Service Tracking study</option>
                        <option value="Sports related study">Sports related study</option>
                        <option value="Television viewership study">Television viewership study</option>
                        <option value="Transaction Audit Study">Transaction Audit Study</option>
                        <option value="U&A Study">U&A Study</option>
                        <option value="Viewership diagnostics">Viewership diagnostics</option>
                        <option value="Youth Survey">Youth Survey</option>
                        <%--DC 04-06-2020--%>
                        <option value="Relationship Customer Satisfaction">Relationship Customer Satisfaction</option>
                        <option value="Transaction Customer Satisfaction">Transaction Customer Satisfaction</option>
                        <option value="Mystery /Price Audits">Mystery /Price Audits</option>
                        <option value="NPS">NPS</option>
                        <option value="Lost customer assessment">Lost customer assessment</option>
                        <option value="Customer journey mapping">Customer journey mapping</option>
                        <option value="CX analytics">CX analytics</option>
                        <option value="Deployment workshops">Deployment workshops</option>
                        <%--DC 04-06-2020--%>
                        <option value="Others">Others</option>
                    </select>
                </td>
                <td>
                    <select runat="server" id="ddl_TeamHead" style="width: 180px;" data-placeholder="Select Team Head Name" multiple class="chosen-select">
                        <%--class="form-control pull-right">--%>
                    </select>
                </td>
                <td>
                    <%--  <select runat="server" id="ddl_dates" style="width: 140px;" class="form-control pull-right">
                        <option value="">Dates</option>
                        <option value="Proposal Sent date">Proposal Sent date</option>
                        <option value="Date of receipt of brief">Date of receipt of brief</option>
                        <option value="Date of sending proposal">Date of sending proposal</option>
                    </select>--%>
                    <%--<asp:DropDownList runat="server" ID="ddl_dates" Style="width: 140px;" class="form-control pull-right" OnSelectedIndexChanged="ddl_dates_SelectedIndexChanged" AutoPostBack="true">--%>
                    <asp:DropDownList runat="server" ID="ddl_dates" Style="width: 180px;" OnSelectedIndexChanged="ddl_dates_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Text="Select Date"></asp:ListItem>
                        <asp:ListItem Text="Proposal Sent date" Value="Proposal Sent date"></asp:ListItem>
                        <asp:ListItem Text="Date of receipt of brief" Value="Date of receipt of brief"></asp:ListItem>
                        <asp:ListItem Text="Date of sending proposal" Value="Date of sending proposal"></asp:ListItem>

                    </asp:DropDownList>
                </td>
                <%--   <td>
                    <asp:Button runat="server" ID="btn_validate" CssClass="btn button-success pure-button" Text="Search" Style="margin: 0 5px 0 0; border-radius: 3px;" OnClick="btn_search_Click" />
                </td>--%>
                <td>
                    <asp:Button runat="server" ID="btn_download" CssClass="btn button-success pure-button" Text="Download Excel" Style="margin: 0; border-radius: 3px;" OnClick="download_Click" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>

                <td>

                    <div class="input-group date">
                        <%--<input type="text" runat="server" id="fromdate" style="width: 140px;" class="form-control pull-right" autocomplete="off" readonly="readonly" visible="false" />--%>
                        <input type="text" runat="server" id="fromdate" style="width: 180px;" autocomplete="off" readonly="readonly" visible="false" />

                    </div>


                </td>
                <td>

                    <div class="input-group date">
                        <%--<input type="text" runat="server" id="Todate" style="width: 140px;" class="form-control pull-left" autocomplete="off" readonly="readonly" visible="false" />--%>
                        <input type="text" runat="server" id="Todate" style="width: 180px;" autocomplete="off" readonly="readonly" visible="false" />
                    </div>

                </td>

            </tr>

        </table>

        <asp:GridView Style="margin-top: 0px;" EmptyDataText="No proposals found...." runat="server" ID="gv_list" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_list_PageIndexChanging" CssClass="pure-table pure-table-bordered" OnRowDataBound="gv_list_RowDataBound">

            <Columns>
                <asp:TemplateField HeaderText="UID" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="UID" Text='<%# Eval("UID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LOCKED" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LOCKED" Text='<%# Eval("LOCKED") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--DC 16-06-2020--%>
                <asp:TemplateField HeaderText="Client Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="clientname" Text='<%# Eval("ClientName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--DC 16-06-2020--%>
                <asp:TemplateField HeaderText="Proposal No">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Date" Text='<%# Eval("PROPOSALID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Team Head">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Receivedby" Text='<%# Eval("TeamHead") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Researcher">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Form" Text='<%# Eval("Researcher") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Outcome" Text='<%# Eval("ProposalName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date of receipt of brief">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Reasons" Text='<%# Eval("Dateofreceiptofbrief") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date of sending proposal">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Comments" Text='<%# Eval("Dateofsendingproposal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Result">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Status" Text='<%# Eval("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal Sent Date">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="LOGDATE" Text='<%# Eval("Dateofsendingproposal") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--DC 18-08-2020--%>
                <asp:TemplateField HeaderText="Study Value">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="studyvalue" Text='<%# Eval("StudyValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--DC 18-08-2020--%>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_proposal" Text="Edit" OnCommand="btn_response_Command" CssClass="button-secondary pure-button" CommandArgument='<%# Eval("LID") %>' CommandName='<%# Eval("CID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_d" Text="Del" OnClientClick="return confirm('Are you sure you want to delete this proposal?');" OnCommand="btn_response_Command" CssClass="button-error pure-button" CommandArgument='<%# Eval("ID") %>' CommandName="D" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Add JCF">
                    <ItemTemplate>
                        <asp:Button runat="server" ID="btn_add_jcf" Text="Add JCF" OnCommand="btn_response_Command" CssClass="button-success pure-button" CommandArgument='<%# Eval("PROPOSALID") %>' CommandName="A" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle CssClass="pagination-ys" />
        </asp:GridView>
    </form>
    <script src="assets/js/jquery.min.js"></script>
    <script>
        $(".nav li").hover(function () {
            $(this).children("ul").stop().delay(200).animate({ height: "toggle", opacity: "toggle" }, 200);
        });
    </script>
    <%--            <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>


    <link rel="stylesheet" href="assets/bower_components/bootstrap/dist/css/bootstrap.min.css" />


    <link rel="stylesheet" href="assets/bower_components/bootstrap-daterangepicker/daterangepicker.css" />
    <link rel="stylesheet" href="assets/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <script src="assets/bower_components/jquery/dist/jquery.min.js"></script>
    <script src="assets/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="assets/bower_components/jquery-slimscroll/jquery.slimscroll.min.js"></script>
    <script src="assets/bower_components/fastclick/lib/fastclick.js"></script>

    <script src="assets/bower_components/select2/dist/js/select2.full.min.js"></script>
    <script src="assets/plugins/input-mask/jquery.inputmask.js"></script>
    <script src="assets/plugins/input-mask/jquery.inputmask.date.extensions.js"></script>
    <script src="assets/plugins/input-mask/jquery.inputmask.extensions.js"></script>
    <script src="assets/bower_components/moment/min/moment.min.js"></script>
    <script src="assets/bower_components/bootstrap-daterangepicker/daterangepicker.js"></script>
    <script src="assets/bower_components/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js"></script>
    <script src="assets/dist/js/adminlte.min.js"></script>
    <script src="assets/js/jquery.validate.min.js"></script>
    <script src="assets/js/additional-methods.js"></script>
    <script>
        $(document).ready(function () {

            $('#fromdate').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy'
            });

            $('#Todate').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy'
            });
        })
    </script>
    <%--   <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <script src="https://cdn.rawgit.com/harvesthq/chosen/gh-pages/chosen.jquery.min.js"></script>
    <link href="https://cdn.rawgit.com/harvesthq/chosen/gh-pages/chosen.min.css" rel="stylesheet" />

    <script>
        $(".chosen-select").chosen({
            no_results_text: "Oops, nothing found!"
        })
      
    </script>

</body>
</html>
