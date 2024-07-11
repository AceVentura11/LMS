<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ragistration.aspx.cs" Inherits="Ragistration" %>

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

    <form id="form2" runat="server">
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
                <a href="ChangeSession.aspx?opt=A" class="twitter">All</a>--%>
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
           
            <div class="content-wrapper" style="text-align:left;margin-left:300px;margin-right:300px">
                <section class="content-header">
                    <h1>User Registration</h1>
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
                                    <div class="form-group" >
                                        <label>User Name:</label>                                                                          
                                        <div class="input-group">
                                            <input type="text" class="form-control optional" runat="server" id="txtUserName" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>User ID</label>                                        
                                        <div class="input-group">
                                            <input type="text" class="form-control optional" runat="server" id="txtUserID" autocomplete="off" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label>User Email ID:</label>
                                        <div class="input-group">
                                            <input type="text" class="form-control optional" runat="server" id="txtEmailID" autocomplete="off" />
                                        </div>
                                    </div>
                                   
                                    <div class="form-group">
                                        <label>User Type:</label>
                                        <select runat="server" id="ddlUsType" class="form-control select2" style="width: 100%;">
                                                                                   
                                        </select>
                                    </div>                                   
                                    <div class="form-group">
                                        <label>Team:</label>
                                        <select runat="server" id="ddlTeam" class="form-control select2 optional" style="width: 100%;">
                                           
                                        </select>
                                        
                                    </div>
                                   
                                    <div class="form-group">
                                        <label>Gender :</label>
                                        <select runat="server" id="ddl_gender" class="form-control optional" style="width: 100%;">
                                            <option value="">Select Gender</option>
                                            <option value="Male">Male</option>
                                            <option value="Female">Female</option>
                                        </select>
                                    </div>
                                    
                                </div>
                                <div class="box-footer text-center">
                                    <asp:Button runat="server" ID="btn_validate" CssClass="btn btn-primary" Text="Submit" OnClick="btnOk_Click" />
                                   
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
