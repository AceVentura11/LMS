<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit-proposal.aspx.cs" Inherits="edit_proposal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="stylesheet" href="assets/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/bower_components/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/bower_components/Ionicons/css/ionicons.min.css" />

    <link rel="stylesheet" href="assets/bower_components/bootstrap-daterangepicker/daterangepicker.css" />
    <link rel="stylesheet" href="assets/bower_components/bootstrap-datepicker/dist/css/bootstrap-datepicker.min.css" />
    <link rel="stylesheet" href="assets/plugins/iCheck/all.css" />
    <link rel="stylesheet" href="assets/bower_components/select2/dist/css/select2.min.css" />
    <link rel="stylesheet" href="assets/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="assets/dist/css/skins/_all-skins.min.css" />

    <link href="assets/menu.css" rel="stylesheet" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<%--<body class="hold-transition skin-blue sidebar-mini">--%>
<body>
    <form id="form1" runat="server">
        <div class='nav'>
            <ul>
                <li>
                    <a style="background: #ffce54" href='Home.aspx'>
                        <img src="assets/images/logobird.png" style="width: 30px; height: 27.5px;" />
                        <%--<a class='logo' href='Home.aspx' style="width:30px;height:30px;">--%>
                        <%--<img src="assets/images/logobird.png" style="width: 100%;" />--%>
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
                <%-- <a href="ChangeSession.aspx?opt=S" class="facebook">Self</a>
                <a href="ChangeSession.aspx?opt=A" class="twitter">All</a>
                <a href="RightsExchange.aspx" class="youtube">Rights Exchange</a>--%>
            </div>
            <%
                    }
                }
            %>
        </div>
        <div>&nbsp</div>
        <div class="wrapper">
            <%-- <header class="main-header" style="display:none;">
                <a href="list.aspx" class="logo">
                    <span class="logo-lg">
                        <img src="assets/images/logo.jpg" style="width: 50%;" />
                    </span>
                </a>
                <nav class="navbar navbar-static-top">
                    <a href="Home.aspx" class="sidebar-toggle">Lead Management System
                    </a>
                    <div class="navbar-custom-menu">
                        <ul class="nav navbar-nav">
                            <li class="dropdown user user-menu">
                                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Welcome, <span class="hidden-xs"><% if (Session["logname"] != null) { Response.Write(Session["logname"].ToString()); } else { Response.Redirect("Default.aspx"); } %></span>
                                </a>
                                <ul class="dropdown-menu">
                                    <li class="user-header">
                                        <% 
                                            if (Session["GENDER"] != null)
                                            {
                                                if (Session["GENDER"].ToString() == "M")
                                                {
                                        %>
                                        <img src="assets/dist/img/avatar5.png" class="img-circle" alt="User Image" />
                                        <% 
                                                }
                                                else
                                                {
                                        %>                                        
                                        <img src="assets/dist/img/avatar3.png" class="img-circle" alt="User Image" />
                                        <% 
                                                }
                                            }
                                            else
                                            {
                                                Response.Redirect("Default.aspx");
                                            }
                                        %>
                                        
                                        <p><% if (Session["logname"] != null) { Response.Write(Session["logname"].ToString()); } else { Response.Redirect("Default.aspx"); } %></p>
                                    </li>
                                    <li class="user-footer">
                                        <div class="pull-left">
                                            <a href="changepassword.aspx" class="btn btn-default btn-flat">Change Password</a>
                                        </div>
                                        <div class="pull-right">
                                            <a href="logout.aspx" class="btn btn-default btn-flat">Sign out</a>
                                        </div>
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                </nav>
            </header>
            <aside class="main-sidebar" style="display:none;">
                <section class="sidebar">
                    <ul class="sidebar-menu" data-widget="tree">
                        <li class="header">&nbsp;</li>
                        <li>
                            <a href="list.aspx"><i class="fa fa-circle-o text-red"></i><span>Client Contact Tracker</span></a>
                        </li>
                        <li>
                            <a href="list-lead.aspx"><i class="fa fa-circle-o text-yellow"></i><span>Lead Tracker</span></a>
                        </li>
                        <li>
                            <a href="list-proposal.aspx"><i class="fa fa-circle-o text-aqua"></i><span>Proposal Tracker</span></a>
                        </li>
                        <%
                            if (Session["usertype"] != null)
                            {
                                if (Session["usertype"].ToString() == "M")
                                {
                        %>
                        <li>
                            <a href="MIS.aspx"><i class="fa fa-circle-o text-aqua"></i><span>MIS</span></a>
                        </li>
                        <%
                                }
                            }
                            else
                            {
                                Response.Redirect("login.aspx");
                            }
                        %>
                    </ul>
                </section>
            </aside>--%>
            <div class="content-wrapper" style="text-align: left; margin-left: 165px; margin-right: 165px">
                <section class="content-header">
                    <h1>Proposal Tracker</h1>
                    <%--<ol class="breadcrumb">
                        <li><a href="list-lead.aspx">Close</a></li>
                    </ol>--%>
                </section>
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label>Unit:</label>
                                        <select runat="server" id="ddl_unit" class="form-control">
                                            <option value="">Select</option>
                                            <option value="QN">Quantitative</option>
                                            <option value="QL">Qualitative</option>
                                            <option value="MD">Media</option>
                                            <option value="IBD">IBD</option>
                                            <option value="IDF">IDF</option>
                                            <option value="B2B">B2B</option>
                                            <option value="ICI">ICICI</option>
                                            <%--AMAN--%>
                                            <option value="Cheetah Mobile Survey">Cheetah Mobile Survey</option>
                                            <option value="Social">Social</option>
                                            <option value="Syndicate">Syndicate</option>
                                            <%--Dhanaji 04-06-2020--%>
                                            <option value="CX">CX</option>
                                            <%--Dhanaji 04-06-2020--%>
                                        </select>
                                        <input type="hidden" runat="server" id="hf_uid" />
                                    </div>
                                    <div class="form-group">
                                        <label>Originating Centre</label>
                                        <select runat="server" id="ddl_originatingcentre" class="form-control">
                                            <option value="">Select</option>
                                            <option value="Banglore">Banglore</option>
                                            <option value="Delhi">Delhi</option>
                                            <option value="Mumbai">Mumbai</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Team Head:</label>
                                        <select runat="server" id="txt_teamhead" class="form-control"></select>
                                    </div>
                                    <div class="form-group">
                                        <label>Researcher:</label>
                                        <select runat="server" id="txt_researcher" class="form-control"></select>
                                    </div>
                                    <div class="form-group">
                                        <label>Client Company:</label>
                                        <select runat="server" id="ddl_clientcompany" class="form-control"></select>
                                    </div>
                                    <div class="form-group">
                                        <label>Client Contact:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_clientcontact" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Direct Line:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_directline" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Mobile:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_mobile" autocomplete="off" class="form-control" data-inputmask="'mask': ['+99 9999999999']" data-mask="" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Email ID:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_email" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Client Category</label>
                                        <select runat="server" id="ddl_clientcategory" class="form-control" disabled="disabled">
                                            <%--<option value="NA" selected="selected">Select</option>
                                            <option value="Agriculture">Agriculture</option>
                                            <option value="FMCG">FMCG</option>
                                            <option value="Automobiles">Automobiles</option>
                                            <option value="Auto Ancilliaries">Auto Ancilliaries</option>
                                            <option value="Heavy Manufacturing">Heavy Manufacturing</option>
                                            <option value="Chemicals">Chemicals</option>
                                            <option value="Pharmaceutical">Pharmaceutical</option>
                                            <option value="Government">Government</option>
                                            <option value="BFSI">BFSI</option>
                                            <option value="Telecom">Telecom</option>
                                            <option value="Services">Services</option>
                                            <option value="B2B Services">B2B Services</option>
                                            <option value="Metals">Metals</option>
                                            <option value="Construction / Real Estate">Construction / Real Estate</option>--%>

                                            <option value="">Select</option>
                                            <option value="AD">Ad Agency</option>
                                            <option value="AGRI">Agricultural</option>
                                            <option value="AUTO">Automobile</option>
                                            <option value="B2B">B2B</option>
                                            <option value="BMED">Broadcast Media</option>
                                            <option value="BFSI">BFSI</option>
                                            <option value="DURA">Durables</option>
                                            <option value="FMCG">FMCG</option>
                                            <option value="PSU">Govt / PSU</option>
                                            <option value="IT">IT</option>
                                            <option value="LOGO">Logistics</option>
                                            <option value="PM">Pharmaceuticals / Medical / Healthcare</option>
                                            <option value="MEDI">Print Media</option>
                                            <option value="RCA">Research / Consulting Agency</option>
                                            <option value="RETI">Retail</option>
                                            <option value="SOC">Social Research</option>
                                            <option value="TELE">Telecom</option>
                                            <option value="TNH">Travel & Hospitality</option>
                                            <option value="OW">Outsourced Work</option>
                                            <option value="OTHE">Other Services</option>
                                            <option value="OTHS">Others</option>
                                            <option value="HI">Heavy Industries</option>
                                            <option value="NM">New Media</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Proposal Name:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_proposalname" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Date of receipt of brief:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_dateofreceiptofbrief" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Due date of sending proposal:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_dateofsendingproposal" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none;">
                                        <label>Team</label>
                                        <select runat="server" id="ddl_projecttype" class="form-control select2">
                                            <option value="">Select</option>
                                            <option value="B2B-AGRI">B2B-AGRI</option>
                                            <option value="BARC">BARC</option>
                                            <option value="CHAR">Charting</option>
                                            <option value="IBD">IBD</option>
                                            <option value="IDF">IDF</option>
                                            <option value="Quantitative">Quantitative</option>
                                            <option value="Qualitative">Qualitative</option>
                                            <option value="Social">Social</option>
                                            <option value="Syndicate">Syndicate</option>
                                            <option value="SB">SB</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Nature of Study</label>
                                        <select runat="server" id="ddl_natureofstudy" class="form-control select2">
                                            <option value="">Select</option>
                                            <option value="Customised Adhoc">Customized - Adhoc</option>
                                            <option value="Customised Track">Customized - Track</option>
                                            <option value="Syndicated Adhoc">Syndicated - Adhoc</option>
                                            <option value="Syndicated Track">Syndicated - Track</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Study Type:</label>
                                        <select runat="server" id="ddl_studytype" class="form-control select2"></select>
                                        <input type="hidden" runat="server" id="txt_studytype" />
                                    </div>
                                    <div class="form-group">
                                        <label>Study Methodology:</label>
                                        <select runat="server" id="ddl_studymethodology" class="form-control select2">
                                            <option value="Quantitative">Quantitative</option>
                                            <option value="Qualitative">Qualitative</option>
                                            <option value="Mixed">Mixed</option>
                                        </select>
                                        <input type="hidden" runat="server" id="hf_studymethodology" />
                                    </div>
                                    <div class="form-group">
                                        <label>Data Collection Methodology:</label>
                                        <select runat="server" id="ddl_datacollectionmethod" multiple="true" class="form-control select2">
                                            <option value="CAPI">CAPI</option>
                                            <option value="CATI">CATI</option>
                                            <option value="CLT">CLT</option>
                                            <option value="Consumer Contact Program">Consumer Contact Program</option>
                                            <option value="DIs">DI's</option>
                                            <option value="GDs">GD's</option>
                                            <option value="DIADS">DIADS</option>
                                            <option value="HansaCheetah">Hansa Cheetah</option>
                                            <option value="Mobile Survey">Mobile Survey</option>
                                            <option value="Mystery Audits">Mystery Audits</option>
                                            <option value="Observation/Ethnography">Observation/Ethnography</option>
                                            <option value="PAPI">PAPI</option>
                                            <option value="Secondary / Desk Research">Secondary / Desk Research</option>
                                            <option value="TRIADS">TRIADS</option>
                                            <option value="WEB">WEB</option>
                                            <option value="Others">Others</option>
                                        </select>
                                        <input type="hidden" runat="server" id="hf_datacollectionmethod" />
                                    </div>
                                    <div class="form-group">
                                        <label>Study Details:</label>
                                        <div class="input-group">
                                            <textarea rows="3" runat="server" id="txt_studydetails" class="form-control"></textarea>
                                        </div>
                                    </div>

                                     <%--DC Created 10-12-2019--%>
                                       <%--DC Updated 03-03-2020--%>
                                    <div class="form-group">
                                        <label>Have you given <q>Online Option</q> in the Proposal?</label>
                                        <div class="input-group">
                                            <label>
                                                <input runat="server" id="rb_Onloptyes" type="radio" name="r1" class="minimal" checked="true" onchange="check()" />
                                                Yes
                                            </label>
                                            <label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                            <label>
                                                <input runat="server" id="rb_Onloptno" type="radio" name="r1" class="minimal" onchange="uncheck()" />
                                                No
                                            </label>
                                        </div>
                                    </div>
                                    <div class="form-group" id="onloptions">
                                         <label>Please specify the source</label>
                                        <select runat="server" id="ddl_onlinesource" class="form-control select2">
                                            <option value="Hansa Cheetah (App & Online)">Hansa Cheetah (App & Online)</option>
                                            <option value="Client survey Link">Client survey Link</option>
                                            <option value="Other sources">Other sources</option>
                                        </select>
                                    </div>
                                    <div class="form-group" id="onloptionsothersource" hidden>
                                        <label>Please specify other source.</label>
                                        <div class="input-group">
                                            <textarea rows="1" runat="server" id="txtonloptionsothersource" class="optional" style="width:100%"  ></textarea>
                                        </div>
                                    </div>
                                    <div class="form-group" id="onloptreason">
                                        <label>What has stopped you from giving the online option? Please specify in detail.</label>
                                        <div class="input-group">
                                            <textarea rows="3" runat="server" id="txtonloptreason" class="optional" style="width:100%" placeholder="Your response will help us in analysing the gaps in the Online platform. So, therefore, please avoid short responses." ></textarea>
                                        </div>
                                    </div>
                                    <%--//--%>

                                    <div class="form-group">
                                        <label>Sample Size:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_samplesize" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Study Value (if multiple options, put most likely):</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_studyvalue" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>OPE (specify value in Rs.):</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_ope" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Status:</label>
                                        <select runat="server" id="ddl_status" class="form-control select2">
                                            <option value="">Select</option>
                                            <option value="Sent to client / awaiting response">Sent to client / awaiting response</option>
                                            <option value="Reworking">Reworking</option>
                                            <option value="Reworked version sent to client/awaiting response">Reworked version sent to client/awaiting response</option>
                                            <option value="Negotiating Cost">Negotiating Cost</option>
                                            <option value="Commissioned">Commissioned</option>
                                            <option value="Lost">Lost</option>
                                        </select>
                                    </div>
                                    <div class="form-group" id="lossreason">
                                        <label>Reasons for Loss:</label>
                                        <select runat="server" id="txt_reasonforloss" class="form-control select2">
                                            <option value="">Select</option>
                                            <option value="Price">Price</option>
                                            <option value="Client decided not to do the project">Client decided not to do the project</option>
                                            <option value="Competition had superior / more appropriate methodology">Competition had superior / more appropriate methodology</option>
                                            <option value="Competition experience in the category">Competition experience in the category</option>
                                            <option value="Competition experience in the method">Competition experience in the method</option>
                                            <option value="Lost to competition (don’t know reasons)">Lost to competition (don’t know reasons)</option>
                                            <option value="Others">Others (specify in Detailed Response)</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Detailed Reasons:</label>
                                        <div class="input-group">
                                            <textarea rows="3" runat="server" id="txt_detailedreason" class="form-control"></textarea>
                                        </div>
                                    </div>                                                                     

                                    <div class="form-group">
                                        <label>Comment:</label>
                                        <div class="input-group">
                                            <textarea rows="3" runat="server" id="txt_comment" class="form-control"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <input type="hidden" id="hf_id" runat="server" />
                                    <asp:Button runat="server" ID="btn_validate" CssClass="btn btn-primary" Text="Submit" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <footer class="main-footer" style="display: none;">
                <div class="pull-right hidden-xs">
                    &nbsp;   
                </div>
                <strong>Copyright &copy; 2017-2018 <a href="http://hansaresearch.com/">Hansa Research Group Private Limited</a>.</strong> All rights reserved. 
            </footer>
            <div class="control-sidebar-bg"></div>
        </div>
    </form>
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
        $().ready(function () {
            $.validator.addMethod("email", function (value, element) {
                return this.optional(element) || /^[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z.]{2,5}$/i.test(value);
            }, "Please enter a valid email address.");

            $.validator.addMethod("regex",
                function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                });

            $("#form1").validate({
                rules: {
                    ddl_unit: { required: true },
                    ddl_originatingcentre: { required: true },
                    txt_teamhead: { required: true },
                    txt_researcher: { required: true },
                    ddl_clientcompany: { required: true },
                    txt_clientcontact: { required: true, minlength: 2, maxlength: 50, regex: /^[a-zA-Z \s]+$/ },
                    txt_directline: {
                        required: function (element) {
                            return $("#txt_mobile").val() == "";
                        }, minlength: 8, maxlength: 15, regex: /^[0-9]+$/
                    },
                    txt_mobile: {
                        required: function (element) {
                            return $("#txt_directline").val() == "";
                        }, minlength: 14, maxlength: 14, regex: /^[0-9 + ]+$/
                    },
                    txt_email: { required: true, minlength: 10, maxlength: 80 },
                    ddl_clientcategory: { required: true },
                    txt_proposalname: { required: true, minlength: 0, maxlength: 250, regex: /^[a-zA-Z0-9 ,.)( \-\ \s]+$/ },
                    txt_dateofreceiptofbrief: { required: true },
                    txt_dateofsendingproposal: { required: true },
                    //ddl_projecttype: { required: true },
                    ddl_natureofstudy: { required: true },
                    ddl_studymethodology: { required: true },
                    ddl_datacollectionmethod: { required: true },
                    txt_studydetails: { required: true, minlength: 2, maxlength: 250, regex: /^[a-zA-Z 0-9 ,.)( \s]+$/ },
                    txt_samplesize: { required: true, minlength: 0, maxlength: 10, regex: /^[0-9.]+$/ },
                    txt_studyvalue: { required: true, minlength: 0, maxlength: 10, regex: /^[0-9.]+$/ },
                    txt_ope: { required: true, minlength: 0, maxlength: 10, regex: /^[0-9.]+$/ },
                    ddl_status: { required: true },
                    txt_reasonforloss: {
                        required: function (element) {
                            return $("#ddl_status").val() == "Lost";
                        }
                    },
                    txt_detailedreason: {
                        required: function (element) {
                            return $("#ddl_status").val() == "Lost";
                        }, minlength: 2, maxlength: 250, regex: /^[a-zA-Z 0-9 ,.)( \s]+$/
                    },
                    txt_comment: { minlength: 2, maxlength: 250, regex: /^[a-zA-Z 0-9 ,.)( \s]+$/ }
                },
                messages: {
                    ddl_unit: {
                        required: "Please select Unit"
                    },
                    ddl_originatingcentre: {
                        required: "Please select Originating Centre"
                    },
                    txt_teamhead: {
                        required: "Please enter Team Head",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid numbers and special characters"
                    },
                    txt_researcher: {
                        required: "Please enter Researcher",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid numbers and special characters"
                    },
                    ddl_clientcompany: {
                        required: "Please select Client Company"
                    },
                    txt_clientcontact: {
                        required: "Please enter Client Contact",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid numbers and special characters"
                    },
                    txt_directline: {
                        required: "Please enter Direct Line",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid alphabates and special characters"
                    },
                    txt_mobile: {
                        required: "Please enter Mobile",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid alphabates and special characters"
                    },
                    txt_email: {
                        required: "Please enter Email",
                        maxlength: "Please enter not more than {0} characters",
                        email: "Your email address must be in the format name@domain.com"
                    },
                    ddl_clientcategory: {
                        required: "Please select Client Category"
                    },
                    txt_proposalname: {
                        required: "Please enter Proposal Name",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_dateofreceiptofbrief: {
                        required: "Please enter Date of receipt of brief"
                    },
                    txt_dateofsendingproposal: {
                        required: "Please enter Date of sending proposal"
                    },
                    //ddl_projecttype: {
                    //    required: "Please select Project Type"
                    //},
                    ddl_natureofstudy: {
                        required: "Please select Nature of Study"
                    },
                    hf_studymethodology: {
                        required: "Please select Study Methodology"
                    },
                    hf_datacollectionmethod: {
                        required: "Please select Data Collection Methodology"
                    },
                    txt_studydetails: {
                        required: "Please enter Study Details",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_samplesize: {
                        required: "Please enter Sample Size",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid alphabates and special characters"
                    },
                    txt_studyvalue: {
                        required: "Please enter Study Value",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid alphabates and special characters"
                    },
                    txt_ope: {
                        required: "Please enter OPE",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid alphabates and special characters"
                    },
                    ddl_status: {
                        required: "Please select Status"
                    },
                    txt_reasonforloss: {
                        required: "Please enter Reasons for Loss",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_detailedreason: {
                        required: "Please enter Detailed Reasons",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_comment: {
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    }
                }
            });
        });

        $(document).ready(function () {
            $('#ddl_studymethodology').select2();
            $('#ddl_datacollectionmethod').select2();
            $('#ddl_unit').select2();
            $('#txt_teamhead').select2();
            $('#ddl_clientcompany').select2();
            $('#ddl_projecttype').select2();
            $('#ddl_studytype').select2();
            $('#txt_researcher').select2();
            $('[data-mask]').inputmask();

            $("#ddl_studymethodology").change(function (event) {
                hf_studymethodology.value = $(this).val();
            });

            $("#ddl_studytype").change(function (event) {
                $("#txt_studytype").val($(this).val());
            });

            $("#ddl_status").change(function (event) {
                if ($("#ddl_status").val() == "Lost") {
                    $('#lossreason').show();
                }
                else {
                    $('#lossreason').hide();
                }
            });
            $('#ddl_status').trigger('change');

             $("#ddl_onlinesource").change(function (event) {
                if ($("#ddl_onlinesource").val() == "Other sources") {
                    $('#onloptionsothersource').show();
                }
                else {
                    $('#onloptionsothersource').hide();
                }
            });

            $('#ddl_onlinesource').trigger('change');

            $("#ddl_natureofstudy").change(function (event) {
                var ddl = '<option value="">Select</option>';
                ddl += '<option value="Accessibility study">Accessibility study</option>';
                ddl += '<option value="Ad recall">Ad recall</option>';
                ddl += '<option value="Ad test">Ad test</option>';
                ddl += '<option value="Ad tracking">Ad tracking</option>';
                ddl += '<option value="Adex">Adex</option>';
                ddl += '<option value="Blend test">Blend test</option>';
                ddl += '<option value="Brand perception">Brand perception</option>';
                ddl += '<option value="Brand Track">Brand Track</option>';
                ddl += '<option value="Car Clinic">Car Clinic</option>';
                ddl += '<option value="Catchment Area profiling">Catchment Area profiling</option>';
                ddl += '<option value="CLT">CLT</option>';
                ddl += '<option value="Competition pricing assessment study">Competition pricing assessment study</option>';
                ddl += '<option value="Concept Test">Concept Test</option>';
                ddl += '<option value="Consumer mood assessment study">Consumer mood assessment study</option>';
                ddl += '<option value="Consumer Profiling">Consumer Profiling</option>';
                ddl += '<option value="Consumer understanding / psyhographic profiling">Consumer understanding / psyhographic profiling</option>';
                ddl += '<option value="Content Evaluation">Content Evaluation</option>';
                ddl += '<option value="Corporate image study">Corporate image study</option>';
                ddl += '<option value="Customer Satisfaction">Customer Satisfaction</option>';
                ddl += '<option value="Day after recall">Day after recall</option>';
                ddl += '<option value="Dealer Satisfaction">Dealer Satisfaction</option>';
                ddl += '<option value="Employee Satisfaction">Employee Satisfaction</option>';
                ddl += '<option value="Entry Strategy study">Entry Strategy study</option>';
                ddl += '<option value="Exit Poll">Exit Poll</option>';
                ddl += '<option value="Feasibility Study">Feasibility Study</option>';
                ddl += '<option value="Idea generation / screening">Idea generation / screening</option>';
                ddl += '<option value="Impact assessment study">Impact assessment study</option>';
                ddl += '<option value="Knowledge dissemination">Knowledge dissemination</option>';
                ddl += '<option value="Launch assessment study">Launch assessment study</option>';
                ddl += '<option value="Migration research">Migration research</option>';
                ddl += '<option value="Modelling studies">Modelling studies</option>';
                ddl += '<option value="Mystery Shopping">Mystery Shopping</option>';
                ddl += '<option value="Name Test">Name Test</option>';
                ddl += '<option value="Observational researches">Observational researches</option>';
                ddl += '<option value="Opinion Poll">Opinion Poll</option>';
                ddl += '<option value="Packaging tests">Packaging tests</option>';
                ddl += '<option value="Population Census">Population Census</option>';
                ddl += '<option value="Potential assessment study">Potential assessment study</option>';
                ddl += '<option value="Price perception study">Price perception study</option>';
                ddl += '<option value="Pricing Study">Pricing Study</option>';
                ddl += '<option value="Print readership study">Print readership study</option>';
                ddl += '<option value="Process effectiveness measurement">Process effectiveness measurement</option>';
                ddl += '<option value="Product test">Product test</option>';
                ddl += '<option value="Program evaluation">Program evaluation</option>';
                ddl += '<option value="Ranking studies">Ranking studies</option>';
                ddl += '<option value="Retail Audit">Retail Audit</option>';
                ddl += '<option value="Retail Census">Retail Census</option>';
                ddl += '<option value="Service Tracking study">Service Tracking study</option>';
                ddl += '<option value="Sports related study">Sports related study</option>';
                ddl += '<option value="Television viewership study">Television viewership study</option>';
                ddl += '<option value="Transaction Audit Study">Transaction Audit Study</option>';
                ddl += '<option value="U&A Study">U&A Study</option>';
                ddl += '<option value="Viewership diagnostics">Viewership diagnostics</option>';               
                //DC 04-06-2020
                ddl += '<option value="Relationship Customer Satisfaction">Relationship Customer Satisfaction</option>';
                ddl += '<option value="Transaction Customer Satisfaction">Transaction Customer Satisfaction</option>';
                ddl += '<option value="Mystery /Price Audits">Mystery /Price Audits</option>';
                ddl += '<option value="NPS">NPS</option>';
                ddl += '<option value="Lost customer assessment">Lost customer assessment</option>';
                ddl += 'option value="Customer journey mapping"<>Customer journey mapping</option>';
                ddl += '<option value="CX analytics">CX analytics</option>';
                ddl += '<option value="Deployment workshops">Deployment workshops</option>';
                //DC 04-06-2020
                ddl += '<option value="Others">Others</option>';

                var ddl1 = '<option value="">Select</option>';
                ddl1 += '<option value="Brand Endorser">Brand Endorser</option>';
                ddl1 += '<option value="Youth Survey">Youth Survey</option>';
                ddl1 += '<option value="IPLomania">IPLomania</option>';
                ddl1 += '<option value="Hansabus">Hansabus</option>';
                ddl1 += '<option value="Diwali Monitor">Diwali Monitor</option>';
                ddl1 += '<option value="Others">Others</option>';

                $("#ddl_studytype").html('');
                if ($(this).val() == "Customised Adhoc" || $(this).val() == "Customised Track") {
                    $("#ddl_studytype").html(ddl);
                }
                else if ($(this).val() == "Syndicated Adhoc" || $(this).val() == "Syndicated Track") {
                    $("#ddl_studytype").html(ddl1);
                }
                $("#ddl_studytype").select2("destroy").select2();
                $('#ddl_studytype').val($("#txt_studytype").val()).trigger('change');
            });

            var array1 = $('#hf_studymethodology').val().split(",");
            $('#ddl_studymethodology').val(array1).trigger('change');

            $("#ddl_datacollectionmethod").change(function (event) {
                hf_datacollectionmethod.value = $(this).val();
            });

            var array2 = $('#hf_datacollectionmethod').val().split(",");
            $('#ddl_datacollectionmethod').val(array2).trigger('change');

            var date = new Date();
            date.setDate(date.getDate());
            $("#txt_dateofreceiptofbrief").datepicker({
                format: 'dd/mm/yyyy',
                //endDate: '+0d',                
                startDate: date,
                autoclose: true
            });
           

            $("#txt_dateofsendingproposal").datepicker({
                format: 'dd/mm/yyyy',
                startDate: date,                
                autoclose: true
            });
            $('#ddl_clientcompany').change(function () {
                var id = $('#ddl_clientcompany').val();
                $.ajax({
                    type: "POST",
                    url: "proposal.aspx/GetContactDetails",
                    data: '{ID: "' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {

                    }
                });

                function OnSuccess(response) {
                    var obj = jQuery.parseJSON(response.d);
                    $('#txt_clientcontact').val(obj[0]["ClientContact"]);
                    $('#txt_directline').val(obj[0]["DirectLine"]);
                    $('#txt_mobile').val(obj[0]["Mobile"]);
                    $('#txt_email').val(obj[0]["EmailID"]);
                }
            });

            $('#ddl_natureofstudy').trigger('change');

            var radioValue = $("input[name='r1']:checked"). val();
            if (radioValue == "rb_Onloptyes") {
                check();
            }
            else {
                uncheck();
            }

            

        });
    </script>
    <%--//DC Created 10-12-2019--%>
    <%--//DC Updated 10-12-2019--%>
    <script>
        function check() {
            
            $('#onloptreason').hide();    
            $('#onloptions').show(); 
            
            
        }
        function uncheck() {
            
            $('#onloptreason').show(); 
            $('#onloptions').hide();
            $('#onloptionsothersource').hide();
            
        }
    </script>
    <%--//--%>
    <style>
        em {
            color: red;
            font-size: 12px;
        }

        .input-group {
            width: 100%;
        }

        .sidebar-toggle {
            transition: width .3s ease-in-out;
            display: block;
            float: left;
            height: 50px;
            font-size: 20px;
            line-height: 20px;
            text-align: center;
            font-weight: 300;
        }

        .main-header .sidebar-toggle:before {
            content: "";
        }

        .skin-blue .main-header .navbar .sidebar-toggle:hover {
            background-color: #3c8dbc;
        }
    </style>
</body>
</html>
