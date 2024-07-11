<%@ Page Language="C#" AutoEventWireup="true" CodeFile="leadmanagement.aspx.cs" Inherits="leadmanagement" %>

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
          <div class='nav' >
            <ul>
                <li>
                    <a style="background:#ffce54" href='Home.aspx'>
                        <img src="assets/images/logobird.png" style="width: 30px;height:27.5px;" />
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
                <%--<a href="ChangeSession.aspx?opt=S" class="facebook">Self</a>
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
           <%-- <header class="main-header">
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
            <aside class="main-sidebar">
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
           <div class="content-wrapper" style="text-align:left;margin-left:165px;margin-right:165px">
                <section class="content-header">
                    <h1>Add Lead Details</h1>
                    <%--<h1>Add Client Contact</h1>--%>
                    <%--<ol class="breadcrumb">
                        <li><a href="Home.aspx">Home</a></li>
                        <li class="active">Add Client Contact</li>
                    </ol>--%>
                </section>
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label>Topic of the Lead:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_proposalname" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Date of receiving lead</label>
                                        <div class="input-group date">
                                            <input type="text" runat="server" id="txt_dateofreceivinglead" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Proposal Due Date:</label>
                                        <div class="input-group date">
                                            <input type="text" runat="server" id="proposalduedate" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Organisation:</label>
                                        <select runat="server" id="ddl_organisation" class="form-control select2 optional" style="width: 100%;"></select>
                                    </div>
                                    <div class="form-group">
                                        <label>Client Contact:</label>
                                        <select runat="server" id="txt_clientcontact" class="form-control select2" style="width: 100%;"></select>
                                    </div>
                                    <div class="form-group">
                                        <label>Location:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_location" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group" style="display: none;">
                                        <label>Designation:</label>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_designation" autocomplete="off" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Industry Vertical:</label>
                                        <%--DC 24-05-2019--%>
                                        <div class="input-group">
                                            <input type="text" runat="server" id="txt_industryvertical" autocomplete="off" class="form-control" readonly="true"/>
                                        </div>
                                        <%--DC 24-05-2019--%>
                                        <%--<select runat="server" id="ddl_industryvertical" class="form-control select2 optional" style="width: 100%;" enableviewstate="true">
                                            <option value="">Select</option>
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
                                        </select>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Lead Generator:</label>
                                        <select runat="server" id="txt_leadgenerator" class="form-control select2" style="width: 100%;">
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Via</label>
                                        <select runat="server" id="txt_via" class="form-control select2 optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="Telephonic query">Telephonic query</option>
                                            <option value="Email query">Email query</option>
                                            <option value="Website query">Website query</option>
                                            <option value="Meeting (in person)">Meeting (in person)</option>
                                            <option value="Other">Other</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Alloted to:</label>
                                        <select runat="server" id="txt_allotedto" class="form-control select2" style="width: 100%;">
                                        </select>
                                    </div>                                    
                                    <div class="form-group">
                                        <label>Group Head:</label>
                                        <select runat="server" id="txt_teamhead" class="form-control"></select>
                                    </div>
                                    <div class="form-group">
                                        <label>Estimate Value</label>
                                        <select runat="server" id="txt_estimatevalue" class="form-control select2 optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="< 5">&lt; 5 Lacs</option>
                                            <option value="5-10">5-10 Lacs</option>
                                            <option value="10-20">10-20 Lacs</option>
                                            <option value="20-50">20-50 Lacs</option>
                                            <option value="50+">50 Lacs +</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Important Client?</label>
                                        <select runat="server" id="ddl_importantclient" class="form-control select2 optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="Strategic Client">Normal Client</option>
                                            <option value="Strategic Client">Strategic Client</option>
                                            <option value="High Value Client">High Value Client</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Relationship</label>
                                        <select runat="server" id="ddl_relationship" class="form-control select2 optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="Regular Client">Regular Client</option>
                                            <option value="One Off Client">One Off Client</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Brief Quality</label>
                                        <select runat="server" id="ddl_briefquality" class="form-control select2 optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="Vague">Vague</option>
                                            <option value="Verbal Brief">Verbal Brief</option>
                                            <option value="In Mail">In Mail</option>
                                            <option value="Detailed Brief Document">Detailed Brief Document</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Comments:</label>
                                        <div class="input-group">
                                            <textarea rows="3" runat="server" id="txt_comments" class="optional" style="width: 100%"></textarea>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                    <asp:Button runat="server" ID="btn_validate" CssClass="btn btn-primary" Text="Submit" OnClick="btn_submit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </div>
            <footer class="main-footer">
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
            $('#txt_clientcontact').select2();
            $('#ddl_organisation').select2();
            $('#ddl_industryvertical').select2();
            $('#txt_leadgenerator').select2();
            $('#txt_allotedto').select2();
            $('#txt_teamhead').select2();

            $.validator.addMethod("regex",
                function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                });

            $("#form1").validate({
                rules: {
                    txt_proposalname: { required: true },
                    txt_dateofreceivinglead: { required: true },
                    ddl_organisation: { required: true },
                    txt_clientcontact: { required: true },
                    txt_location: { required: true, minlength: 2, maxlength: 100, regex: /^[a-zA-Z \s]+$/ },
                    //txt_designation: { minlength: 2, maxlength: 150, regex: /^[a-zA-Z & ! @ ) ( , \-\ . _  \s]+$/ },
                    ddl_industryvertical: { required: true },
                    txt_leadgenerator: { required: true },
                    txt_estimatevalue: { required: true },
                    ddl_importantclient: { required: true },
                    ddl_relationship: { required: true },
                    ddl_briefquality: { required: true },
                    txt_comments: { minlength: 2, maxlength: 250, regex: /^[a-zA-Z & ! @ ) ( , \-\ . _ \s]+$/ }
                },
                messages: {
                    txt_proposalname: {
                        required: "Please enter Topic of the Lead"
                    },
                    txt_dateofreceivinglead: {
                        required: "Please enter Date of receiving lead"
                    },
                    ddl_organisation: {
                        required: "Please select Organisation"
                    },
                    txt_clientcontact: {
                        required: "Please enter Client Contact",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid numbers and special characters"
                    },
                    txt_location: {
                        required: "Please enter Location",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid numbers and special characters"
                    },
                    //txt_designation: {
                    //    required: "Please enter Designation",
                    //    minlength: "Please enter at least {0} characters",
                    //    maxlength: "Please enter not more than {0} characters",
                    //    regex: "Please avoid numbers and special characters"
                    //},
                    ddl_industryvertical: {
                        required: "Please select Industry Vertical"
                    },
                    txt_leadgenerator: {
                        required: "Please enter Lead Generator",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid numbers and special characters"
                    },
                    txt_estimatevalue: {
                        required: "Please enter Estimate Value",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid characters, spaces and special characters"
                    },
                    ddl_importantclient: {
                        required: "Please select Important Client"
                    },
                    ddl_relationship: {
                        required: "Please select Relationship"
                    },
                    ddl_briefquality: {
                        required: "Please select Brief Quality"
                    },
                    txt_comments: {
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    form_message: ""
                }
            });
        });

        $(document).ready(function () {
            $('#txt_dateofreceivinglead').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy'
            });

            $('#proposalduedate').datepicker({
                autoclose: true,
                format: 'dd/mm/yyyy'
            });

            $('#ddl_organisation').change(function () {
                var id = $('#ddl_organisation').val();
                $.ajax({
                    type: "POST",
                    url: "leadmanagement.aspx/GetContactDetails",
                    data: '{ID: "' + id + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {

                    }
                });

                function OnSuccess(response) {
                    var d = response.d.split('|');
                    $('#txt_clientcontact').val(d[0]);
                    $('#txt_location').val(d[1]);
                    $('#txt_designation').val(d[2]);
                }
            });
        })



      
    </script>
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
