<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DbModelApi.Views.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, minimum-scale=1, user-scalable=no">
    <meta name="format-detection" content="telephone=no">
    <link href="/Scripts/bootstrap-datepicker/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="/Scripts/bootstrap-datepicker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" />

    <link rel="stylesheet" type="text/css" href="/Content/base.css" />
    <link rel="stylesheet" type="text/css" href="/Scripts/select2/select2.min.css" />
    <script src="/Scripts/jquery/jquery-2.1.4.min.js"></script>
    <script src="/Scripts/jquery/jquery-ui.min.js"></script>
    <script src="/Scripts/jquery.cookie.js"></script>
    <script src="/Scripts/resize.js"></script>
    <script src="/Scripts/base.js"></script>


    <script src="/Scripts/select2/select2.full.min.js"></script>

    <script type="text/javascript" src="/Scripts/dataTables/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="/Scripts/dataTables/jquery.dataTables.bootstrap.js"></script>

    <script src="/Scripts/bootstrap-datepicker/bootstrap/js/bootstrap.min.js"></script>
    <script src="/Scripts/bootstrap-datepicker/js/bootstrap-datetimepicker.js"></script>
    <script src="/Scripts/bootstrap-datepicker/js/locales/bootstrap-datetimepicker.zh-CN.js"></script>
    <script src="/Scripts/layer/layer.js"></script>
    <!–[if lt IE9]> 
    <script src="/Scripts/html5.js"></script>
    <script src="/Scripts/respond.js"></script>
    <script src="/Scripts/angular.min.js"></script>
    <%--<script src="/Scripts/angular-1.0.1.min.js"></script>--%>
    <script src="/Scripts/app-ng.js"></script>
    <script src="/Scripts/jQuery.formatMoney.min.js"></script>
    <![endif]–>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    用户名：<input type="text" id="txtuname"/><br/>
        密码：<input type="password" id="txtpwd"/><br />
        <input type="button" id="btnlogin" value="登录" onclick="Login()"/>
    </div>
    </form>
</body>
</html>
<script type="text/javascript">
    function Login() {
        //if (!LoginCheck()) {
            //ShowLoading();
            var authInfo = {
                "username": $("#txtuname").val(),
                "password": $("#txtpwd").val()
            };
            var parameterJson = JSON.stringify(authInfo);
            $.ajax({
                type: "post",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                url: "/api/proxy/auth",
                data: parameterJson,
                success: function (result) {
                    alert("succ");
                    //$.ajax({
                    //    type: "get",
                    //    dataType: 'json',
                    //    contentType: "application/json; charset=utf-8",
                    //    url: "/api/proxy/currentuser",
                    //    success: function (result) {


                    //    }
                    //});
                },
                error: function (err) {
                    ErrorResponse(err);
                }
            });
        //}

    }
</script>
