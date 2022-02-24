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
      <h6><b><font color="black"><center>OGUN WATER BILLING SYSTEM</center></b></h6><br />
              <asp:Label ID="lblmsg" runat="server" Text="" ForeColor="red"></asp:Label><br />

   <div id="logindisplay" runat="server" visible="true">
      <div class="form-group has-feedback">
    
        <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="Username" ></asp:TextBox>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" ></asp:TextBox>
       <input type="checkbox" id="pass1" onclick="showpass(this);" />Show
           <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
          <%--<div class="form-group has-feedback">
             <asp:checkbox runat="server" id="ckremember"></asp:checkbox><label for="rememberme">Remember me</label>
        
       
      </div>--%>
      <div class="row">
        <!-- /.col -->
        <div class="col-xs-4">

        <center>  <asp:Button ID="Button1"
             CssClass="btn btn-primary btn-block btn-flat form-control" 
            runat="server" Text="LOGIN" OnClick="Button1_Click" Width="303px" /> </center>
               
        </div>
           
        
        <!-- /.col -->
      </div>
      <div class="row">
            
               <%--<a href="#" class="btn btn-default btn-flat">Forgot password?</a>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>--%>
            <asp:LinkButton ID="linkGoSomewhere" runat="server" 
         Click="linkGoSomewhere_Click" onclick="linkGoSomewhere_Click">Forgot Password??...click here!!!</asp:LinkButton>
      </div>
         </div>  
      <div id="changedisplay"  runat="server" visible="false">
         <table align="center" cellpadding="0" cellspacing="0" style="width: 100%">
                                                       
                                         <asp:Label runat="server" Text="CHANGE YOUR SYSTEM PASSWORD" id="Label1" Font-Bold="true" ForeColor="Green"></asp:Label>

<%--              <tr>
                                                            <td class="InterFaceTableLeftRow">
                                                                Current Password</td>
                                                            <td class="InterFaceTableMiddleRow">
                                                            </td>
                                                            <td class="InterFaceTableRightRow">
                                                                <asp:TextBox ID="txtcurrentpwd" runat="server" CssClass="form-control"
                                                                    TextMode="Password" Width="60%"  onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                                                                    <input type="checkbox" id="cpwd" onclick="showpass2(this);" />Show Password</td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td class="auto-style3">
                                                                New Password</td>
                                                            
                                                            <td class="auto-style2">
                                                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control"
                                                                    TextMode="Password" Width="80%"  onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                                                                    <input type="checkbox" id="pass2" onclick="showpass2(this);" />Show</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style3">
                                                                Confirm New Password</td>
                                                            
                                                            <td class="auto-style2">
                                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"
                                                                    TextMode="Password" Width="80%"   onKeyDown="return DisableControlKey(event)" onMouseDown="return DisableControlKey(event)"></asp:TextBox>
                                                                    <input type="checkbox" id="pass3" onclick="showpass3(this);" />Show</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 21px">

                                                            </td>
                                                        </tr>
              <tr>
                                                            <td colspan="3" style="height: 21px">
 <asp:Button ID="BtnSave" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                                    OnClick="BtnSave_Click" Text="Save" Width="99px" />&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                                    OnClick="btnCancel_Click" Text="Cancel" Width="90px" />
                                                            </td>
                                                        </tr>
             </table>
      </div>      
      <div id="forgotpwddisplay" runat="server" visible="false">
                                                          <table align="center" cellpadding="0" cellspacing="0" style="width: 45%">
                                                       
                                                              <asp:Label runat="server" Text="FORGOT SYSTEM PASSWORD" id="lblf" Font-Bold="true" ForeColor="Green"></asp:Label>
                                                       
                                                        <tr>
                                                            <td class="auto-style2">
                                                                System Username</td>
                                                           
                                                            <td class="auto-style2">
                                                                <asp:TextBox ID="txtforgetusername" runat="server" CssClass="InterfaceTextboxLongReadOnly" AutoPostBack="true" OnTextChanged="txtforgetusername_TextChanged" 
                                                                
                                                                     Width="94%"></asp:TextBox>
                                                                     </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style2">
                                                                Full Name</td>
                                                            
                                                            <td class="auto-style2">
                                                                <asp:TextBox ID="txtFullName" runat="server" CssClass="InterfaceTextboxLongReadOnly" ReadOnly="true" BackColor="WhiteSmoke"
                                                                     Width="94%"></asp:TextBox>
                                                                     </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style2">
                                                                Email</td>
                                                           
                                                            <td class="auto-style2">
                                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="InterfaceTextboxLongReadOnly" ReadOnly="true" BackColor="WhiteSmoke"
                                                                     Width="94%"></asp:TextBox>
                                                                     </td>
                                                        </tr>
                                                      
                                                         <tr>
                                                            <td class="auto-style2">
                                                                Action</td>
                                                            
                                                            <td class="InterfaceInforFonts" style="vertical-align: top; text-align: center">
                                <asp:RadioButtonList ID="rbnAction" runat="server" BackColor="LemonChiffon" AutoPostBack="true"
                                    Font-Bold="True" OnSelectedIndexChanged="rbnAction_SelectedIndexChanged" RepeatDirection="Horizontal"
                                    Style="border-top-style: ridge; border-right-style: ridge; border-left-style: ridge;
                                    border-bottom-style: ridge" Width="92%">
                                    <asp:ListItem Value="0">Enable Account</asp:ListItem>
                                    <asp:ListItem Value="1">Reset password</asp:ListItem>                                    
                                    </asp:RadioButtonList></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 21px">
                                                            </td>
                                                        </tr>
                                                      
                                                                            <tr>
                                                                                <td class="auto-style2">
<asp:Button ID="Button3" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                                    OnClick="BtnForgotSubmit_Click" Text="Submit" Width="99px" />
                                                                                </td>
                                                            <td class="auto-style2">
                                                            &nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="Button4" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                                    OnClick="btnCancelSubmit_Click" Text="Cancel" Width="90px" />
                                                            </td>
                                                        </tr>
                                                   
                                                    </table>

      </div>               
  </div>
  <!-- /.form-box -->
</div>
      <asp:Label runat="server" Text="0" id="lbluserid" Visible="false"></asp:Label>
    <asp:Label runat="server" Text="." id="lblusername" Visible="false"></asp:Label>
    <asp:Label runat="server" Text="." id="lbloldpwd" Visible="false"></asp:Label>
    <asp:Label runat="server" Text="." id="lblmewpwd" Visible="false"></asp:Label>
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
    //new password
    function showpass2(check_box) {
        var spass = document.getElementById("txtNewPassword");
        if (check_box.checked) {
            spass.setAttribute("type", "text");

        }
        else {
            spass.setAttribute("type", "password");

        }
    }
    //confirm new password
    function showpass3(check_box) {
        var spass = document.getElementById("txtConfirmPassword");
        if (check_box.checked) {
            spass.setAttribute("type", "text");

        }
        else {
            spass.setAttribute("type", "password");

        }
    }
        </script>
</html>

