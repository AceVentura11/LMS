<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="stylesheet" href="assets/bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/bower_components/font-awesome/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/bower_components/Ionicons/css/ionicons.min.css" />
    <link rel="stylesheet" href="assets/dist/css/AdminLTE.min.css" />
    <link rel="stylesheet" href="assets/plugins/iCheck/square/blue.css" />
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700,300italic,400italic,600italic" />
</head>
<body class="hold-transition login-page" style="background-color:#FFFFFF;">
    <div class="login-box" style="float: right; margin-right: 20%;">
        <div class="login-logo">
            <a>
                <img src="assets/images/logo.png" />
            </a>
        </div>
        <div class="login-box-body" style="background-color:#d2d6de;">
            <p class="login-box-msg">Sign in to start your session</p>
            <form id="form1" runat="server">
                <div class="form-group has-feedback">
                    <input type="text" class="form-control" placeholder="User Name" id="txt_uname" runat="server"/>
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" class="form-control" placeholder="Old Password" id="txt_oldpassword" runat="server" />
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <input type="password" class="form-control" placeholder="New Password" id="txt_password" runat="server" />
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <div class="checkbox icheck">
                            &nbsp;
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <asp:Button runat="server" ID="btn_submit" CssClass="btn btn-primary btn-block btn-flat" Text="Submit" OnClick="btn_submit_Click" />
                    </div>
                </div>
            </form>
            <a href="list.aspx">Back</a>
        </div>
    </div>
    <div class="login-box" style="width: 300px; margin: 7% auto; float: right; margin-right: 20%;">
        <img src="assets/images/login.png" style="width: 100%;" />
    </div>
    <script src="assets/bower_components/jquery/dist/jquery.min.js"></script>
    <script src="assets/bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
    <script src="assets/plugins/iCheck/icheck.min.js"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%'
            });
        });
    </script>
    <script src="assets/js/jquery-ui.js"></script>
    <script src="assets/js/jquery.validate.min.js"></script>
    <script src="assets/js/loginvalidate.js"></script>
</body>
</html>
