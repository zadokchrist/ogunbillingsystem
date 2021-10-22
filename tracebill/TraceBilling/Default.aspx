<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TraceBilling.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
  <!--<meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge" />-->
   
   <title>Trace Billing</title>
  <link rel="icon" href="custom/img/Linkedin Logo.jpg" />
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">
  <!-- Font Awesome -->
  <!-- Theme style -->
  <link rel="stylesheet" href="assets/dist/css/AdminLTE.min.css">
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="assets/dist/css/skins/_all-skins.min.css" />
  <link rel="stylesheet" href="assets/plugins/datepicker/datepicker3.css" />
  <link rel="stylesheet" href="assets/plugins/bootstrap-timepicker/bootstrap-timepicker.min.css" />

    
    <style type="text/css">
        .auto-style1 {
            width: 87%;
        }
    </style>

    
</head>
<!-- <body class="hold-transition register-page" style="background-color:#eee;"> -->
<%-- <body class="hold-transition register-page" style="background:url('assets/dist/img/background1.jpg') no-repeat center center fixed;">--%>


    <p>
        <br />
    </p>

<form id="Form1" runat="server">
    <div class="register-box">

  <div class="register-box-body shadow">

      <div class="register-logo">
<%--        <img src="images/tracelogo.png" style="height:70px; width: 85px" />--%>
           <img src="images/ogunwater.png" style="height:70px; width: 85px" />
         <%-- <table class="auto-style1">
              <tr>
                  <td align="left"> <img src="images/tracebilllogo.png" style="height:70px; width: 85px" /></td>
                  <td align="right"> <img src="images/ogunwater.png" style="height:70px; width: 85px" /></td>
              </tr>
          </table>--%>

      </div>
      <h6><b><font color="black"><center>OGUN WATER BILLING SYSTEM</center></b></h6>
    
   
      <div>
      <div class="form-group has-feedback">
          <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="red"></asp:Label>
    
        <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="Username or Email" ></asp:TextBox>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" ></asp:TextBox>
       <input type="checkbox" id="pass1" onclick="showpass(this);" />Show
           <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
          <div class="form-group has-feedback">
             <asp:checkbox runat="server" id="ckremember"></asp:checkbox><label for="rememberme">Remember me</label>
        
       
      </div>
      </div>
      <div class="row">
        <!-- /.col -->
        <div class="col-xs-4">

        <center>  <asp:Button ID="Button1"
             CssClass="btn btn-primary btn-block btn-flat form-control" 
            runat="server" Text="LOGIN" OnClick="Button1_Click" Width="303px" /> </center>
               
        </div>
           
        
        <!-- /.col -->
      </div>
      <div class="form-group has-feedback">
            
               <a href="#" class="btn btn-default btn-flat">Forgot password?</a>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
                        
  </div>
  <!-- /.form-box -->
</div>
</form>



    <!-- jQuery 2.2.3 -->
<script src="assets/plugins/jQuery/jquery-2.2.3.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="assets/bootstrap/js/bootstrap.min.js"></script>
<!-- FastClick -->
<script src="assets/plugins/fastclick/fastclick.js"></script>
<!-- AdminLTE App -->
<script src="assets/dist/js/app.min.js"></script>
<!-- AdminLTE for demo purposes -->
<script src="assets/dist/js/demo.js"></script>
<script type="text/javascript" src="assets/plugins/jquery.maskedinput.js"></script>

</body>
    <script type="text/javascript">
    function showpass(check_box) {
        var spass = document.getElementById("txtpassword");
        if (check_box.checked) {
            spass.setAttribute("type", "text");
          
        }
        else {
            spass.setAttribute("type", "password");
            
        }
    }
        </script>
</html>

