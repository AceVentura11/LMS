<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

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
    <body >

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

        <div class="wrapper" >
       <%--     <header class="main-header" style="display: block;">
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

                                        <p><% if (Session["logname"] != null) { Response.Write(Session["logname"].ToString()); } else { Response.Write(Session["logname"].ToString()); } %></p>
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
            </header>--%>
       <%--     <aside class="main-sidebar" style="display: none;">
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
                    <h1>Add Client Contact</h1>
                   <%-- <ol class="breadcrumb">
                        <li><a href="Home.aspx">Close</a></li>
                        <li class="active">Add Client Contact</li>
                    </ol>--%>
                </section>
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="box">
                                <div class="box-body">
                                    <div class="form-group">
                                        <label>
                                            <input runat="server" id="rb_currentclient" type="radio" name="r1" class="minimal" checked="true" onchange="check()" />
                                            Current Client
                                        </label>
                                        <label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                                        <label>
                                            <input runat="server" id="rb_newclient" type="radio" name="r1" class="minimal" onchange="uncheck()" />
                                            Potential New Cient
                                        </label>
                                    </div>
                                    <div class="form-group" >
                                        <label>Client Name:</label>
                                        <select runat="server" id="txt_clientname" class="form-control select2" style="width: 100%;" onchange="getclient1('1')">
                                        </select>
                                        <input style="margin-top: 10px;" type="text" class="form-control optional" runat="server" id="new_client" autocomplete="off" />
                                    </div>
                                    <div class="form-group">
                                        <label>Office</label>
                                        <select runat="server" id="ddl_office" class="form-control select2" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="Head Office">Head Office</option>
                                            <option value="Branch Office">Branch Office</option>
                                        </select>
                                    </div>
                                    <div class="form-group">
                                        <label>Division:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control optional" runat="server" id="txt_division" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <br />
                                        <label>Client Office Location (City):</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control optional" runat="server" id="txt_branchlocation" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Client Contact:</label>
                                        <select runat="server" id="ddl_clientcontact" class="form-control select2" style="width: 100%;">
                                        </select><br />
                                        <br />
                                        <input type="text" runat="server" class="form-control" id="txt_clientcontact_new" autocomplete="off" />
                                        <input type="hidden" runat="server" id="txt_clientcontact" />
                                    </div>
                                    <div class="form-group">
                                        <label>Boardline:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" runat="server" id="txt_boardline" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Direct Line:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" runat="server" id="txt_directline" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Mobile:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" runat="server" id="txt_mobile" autocomplete="off" data-inputmask="'mask': ['+99 9999999999']" data-mask="" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Email ID:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" runat="server" id="txt_emailid" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Function</label>
                                        <select runat="server" id="txt_function" class="form-control select2 optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="HR">HR</option>
                                            <option value="Marketing">Marketing</option>
                                            <option value="Sales">Sales</option>
                                            <option value="Accounting and Finance">Accounting and Finance</option>
                                            <option value="Research and Development">Research and Development</option>
                                            <option value="Manufacturing or Production">Manufacturing / Production</option>
                                            <option value="Procurement">Procurement</option>
                                            <option value="Logistics">Logistics</option>
                                            <option value="Analytics">Analytics</option>
                                            <option value="Customer Service">Customer Service</option>
                                            <option value="Consumer Insights">Consumer Insights</option>
                                            <%--Dhhanaji 03-06-20--%>
                                            <option value="Real Estate">Real Estate</option>
                                            <option value="Ecommerce">Ecommerce</option>
                                            <option value="Hospitality">Hospitality</option>
                                            <%--Dhhanaji 03-06-20--%>
                                            <option value="Others">Others</option>
                                        </select><br />
                                        <input type="text" runat="server" class="form-control" id="txt_function_others" autocomplete="off" />
                                    </div>
                                    <div class="form-group">
                                        <label>Designation:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control" runat="server" id="txt_designation" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>Gender</label>
                                        <select runat="server" id="ddl_gender" class="form-control optional" style="width: 100%;">
                                            <option value="">Select</option>
                                            <option value="Male">Male</option>
                                            <option value="Female">Female</option>
                                        </select>
                                    </div>
                                    <div class="form-group">

                                        <label>Industry Vertical</label>
                                        <select runat="server" id="ddl_industryvertical" class="form-control select2 optional" style="width: 100%;" disabled="true" >
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

                                            <%-- DC 24/05/2019 --%>
                                            <%--    <option value="">Select</option>
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
                                            <option value="OTHS">Other Services</option>--%>
                                        </select>
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

    <script src="assets/js/jquery-ui.js"></script>
    <script src="assets/js/jquery.validate.min.js"></script>
    <script src="assets/js/additional-methods.js"></script>
    <script>
        $().ready(function () {
            $('[data-mask]').inputmask();
            $('#ddl_clientcontact').select2();
            $('#txt_clientname').select2();
            $("#txt_clientcontact_new").hide();

            $("#ddl_clientcontact").change(function (event) {
                txt_clientcontact.value = $(this).val();
                if ($(this).val() == 'Other') {
                    $("#txt_clientcontact_new").show();
                }
                else {
                    $("#txt_clientcontact_new").hide();
                }
            });

            $.validator.addMethod("email", function (value, element) {
                return this.optional(element) || /^[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+\.[a-zA-Z.]{2,5}$/i.test(value);
            }, "Please enter a valid email address.");

            $.validator.addMethod("regex",
                function (value, element, regexp) {
                    var re = new RegExp(regexp);
                    return this.optional(element) || re.test(value);
                });
            $("#txt_function_others").hide();
            $("#txt_function").change(function () {
                if ($('option:selected', this).text() == "Others") {
                    $("#txt_function_others").show();
                }
                else {
                    $("#txt_function_others").val("");
                    $("#txt_function_others").hide();
                }
            });

            $("#new_client").hide();
            $("#txt_clientname").change(function () {
                console.log(this);
                if ($('option:selected', this).val() == "New") {
                    $("#new_client").show();
                }
                else {
                    $("#new_client").val("");
                    $("#new_client").hide();
                }
            });

            $("#form1").validate({
                rules: {
                    txt_clientname: { required: true }, // , minlength: 2, maxlength: 150, regex: /^[a-zA-Z0-9 & ! @ ) ( , \-\ . _ \s]+$/
                    ddl_office: { required: true },
                    txt_division: { minlength: 2, maxlength: 100, regex: /^[a-zA-Z0-9 \s]+$/ },
                    txt_branchlocation: { minlength: 2, maxlength: 100, regex: /^[a-zA-Z0-9 \s]+$/ },
                    ddl_clientcontact: { required: true },
                    txt_clientcontact_new: {
                        required: function (element) {
                            return $("#ddl_clientcontact").val() == "Other";
                        }, minlength: 3, maxlength: 100, regex: /^[A-Za-z0-9 ! @ ) ( , \-\ . _  \s]+$/
                    },
                    txt_boardline: {
                        required: function (element) {
                            return $("#txt_directline").val() == "" && $("#txt_mobile").val() == "";
                        }, minlength: 8, maxlength: 15, regex: /^[0-9 \s]+$/
                    },
                    txt_directline: {
                        required: function (element) {
                            return $("#txt_boardline").val() == "" && $("#txt_mobile").val() == "";
                        }, minlength: 8, maxlength: 15, regex: /^[0-9 \s]+$/
                    },
                    txt_mobile: {
                        required: function (element) {
                            return $("#txt_directline").val() == "" && $("#txt_boardline").val() == "";
                        }, minlength: 14, maxlength: 14, regex: /^[0-9 + ]+$/
                    },
                    txt_emailid: { required: true, maxlength: 80 },
                    //txt_function: { required: true },
                    //txt_function_others: {
                    //    required: function (element) {
                    //        return $("#txt_function").val() == "Others";
                    //    }, minlength: 2, maxlength: 100, regex: /^[a-zA-Z0-9 \s]+$/
                    //},
                    txt_designation: { minlength: 2, maxlength: 100, regex: /^[a-zA-Z0-9 ! & @ ) ( , \-\ . _ \s]+$/ },
                    ddl_gender: { required: true },
                    ddl_industryvertical: { required: true }
                },
                messages: {
                    txt_clientname: {
                        required: "Please enter Client Name"
                        //minlength: "Please enter at least {0} characters",
                        //maxlength: "Please enter not more than {0} characters",
                        //regex: "Please avoid spaces and special characters"
                    },
                    ddl_office: {
                        required: "Please select Office"
                    },
                    txt_division: {
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_branchlocation: {
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_clientcontact: {
                        required: "Please enter Client Contact other",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_clientcontact_new: {
                        required: "Please enter Client Contact",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid spaces and special characters"
                    },
                    txt_boardline: {
                        required: "Please enter either Boardline, Direct Line or Mobile",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid characters, spaces and special characters"
                    },
                    txt_directline: {
                        required: "Please enter either Boardline, Direct Line or Mobile",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid characters, spaces and special characters"
                    },
                    txt_mobile: {
                        required: "Please enter either Boardline, Direct Line or Mobile",
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid characters, spaces and special characters"
                    },
                    txt_emailid: {
                        required: "Please enter Email ID",
                        maxlength: "Please enter not more than {0} characters",
                        email: "Your email address must be in the format name@domain.com"
                    },
                    txt_function: {
                        required: "Please select Function"
                    },
                    txt_function_others: {
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    txt_designation: {
                        minlength: "Please enter at least {0} characters",
                        maxlength: "Please enter not more than {0} characters",
                        regex: "Please avoid special characters"
                    },
                    ddl_gender: {
                        required: "Please select Gender",
                    },
                    ddl_industryvertical: {
                        required: "Please select Industry vertical",
                    },
                    form_message: ""
                }
            });
            $('input[type=radio][id=rb_currentclient]').change(function () {
                $("#new_client").hide();
                getclient('current');
            });

            $('input[type=radio][id=rb_newclient]').change(function () {
                $("#new_client").hide();
                getclient('new');
            });

        });

        function getclient(type) {
            $.ajax({
                type: "POST",
                url: "Index.aspx/GetClientDetails",
                data: '{type: "' + type + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {

                }
            });

            function OnSuccess(response) {
                $("#txt_clientname").html(response.d);
            }
           
        }

        function getclient1() {            
            $.ajax({
                type: "POST",
                url: "Index.aspx/GetClientDetails1",
                data: '{type: "' + $("#txt_clientname").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {

                }
            });

            function OnSuccess(response) {
                $("#ddl_industryvertical").val(response.d);

            }

               // javascript: __doPostBack('LinkButton1', '')
            getclientcontactdetails();
                       
        }
        function getclientcontactdetails()
        {
            $.ajax({
                type: "POST",
                url: "Index.aspx/GetClientContactDetails",
                data: '{type: "' + $("#txt_clientname").val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {

                }
            });

            function OnSuccess(response) {
                $("#ddl_clientcontact").html(response.d);

            }
        }
    </script>
    <script>
        function check() {
            $("#ddl_industryvertical").val("");
            document.getElementById("ddl_industryvertical").disabled = true;
            
        }
        function uncheck() {
            $("#ddl_industryvertical").val("");
            document.getElementById("ddl_industryvertical").disabled = false;
            
        }
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
