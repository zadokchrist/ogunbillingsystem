<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="TraceBilling.ChangePassword" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <form role="form" runat="server">

     <div class="container">
	<div class="row">
        <div class="col-sm-12">
	 
          <div><h3>PASSWORD CHANGE MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        </div>
        <div class="col-sm-3"></div>
          <div class="col-sm-6 white_bg left_text div_space">     
                           <div id="biqdisplay" runat="server">
                
                               <table align="center" cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr class="hide">
                                                            <td colspan="3">
                                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 100%">
                                                                    <tr>
                                                                        <td class="form-control">                                                                         
                                                                            <asp:Label runat="server" Text="CHANGE YOUR SYSTEM PASSWORD" Font-Bold="true" ForeColor="Blue" ></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 20px">
                                                            </td>
                                                        </tr>
                                    <tr  class="hide">
                                                            <td style="width:50%">                                                                
                                                                <asp:Label runat="server" Text="Current UserName" Font-Bold="true" ></asp:Label>
                                                            </td>
                                                            
                                                            <td style="width:50%">
                                                                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control"
                                                                     Width="80%"  ReadOnly="true" BackColor="WhiteSmoke"></asp:TextBox>
                                                                    </td>
                                                        </tr>
                                                         <tr class="trow">
                                                            <td style="width:50%">                                                                
                                                                <asp:Label runat="server" Text="Current Password" Font-Bold="true" ></asp:Label>
                                                            </td>
                                                            
                                                            <td style="width:50%">
                                                                <asp:TextBox ID="txtcurrentpwd" runat="server" CssClass="form-control"
                                                                    TextMode="Password" Width="80%" ></asp:TextBox>
                                                                    <input type="checkbox" class="hide" id="pass1" onclick="showpass1(this);" /></td>
                                                        </tr>
                                                        <tr class="trow">
                                                            <td style="width:50%">                                                                
                                                                <asp:Label runat="server" Text="New Password" Font-Bold="true" ></asp:Label>
                                                            </td>
                                                           
                                                            <td style="width:50%">
                                                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="form-control"
                                                                    TextMode="Password" Width="80%" ></asp:TextBox>
                                                                    <input type="checkbox"  class="hide"  id="pass2" onclick="showpass2(this);" /></td>
                                                        </tr>
                                                        <tr class="trow">
                                                            <td style="width:50%">                                                                
                                                                <asp:Label runat="server" Text="Confirm New Password" Font-Bold="true" ></asp:Label>
                                                            </td>
                                                          
                                                            <td style="width:50%">
                                                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"
                                                                    TextMode="Password" Width="80%" ></asp:TextBox>
                                                                    <input type="checkbox" id="pass3" onclick="showpass3(this);" />Show</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="height: 21px">

                                                            </td>
                                                        </tr>
              <tr>
                  <td></td>
                                                            <td " style="height: 21px">
 <asp:Button ID="BtnSave" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px" CssClass="btn-success"
                                                                    OnClick="BtnSave_Click" Text="Save" Width="99px" />&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="btnCancel" CssClass="btn-warning" runat="server" Font-Bold="True" Font-Size="9pt" Height="23px"
                                                                    OnClick="btnCancel_Click" Text="Cancel" Width="90px" />
                                                            </td>
                                                        </tr>
             </table>

               </div>
              </div>
        
       <div class="col-sm-3"></div>
<asp:Label runat="server" Text="0" id="lbluserid" Visible="false"></asp:Label>
    <asp:Label runat="server" Text="." id="lblusername" Visible="false"></asp:Label>
    <asp:Label runat="server" Text="." id="lbloldpwd" Visible="false"></asp:Label>
    <asp:Label runat="server" Text="." id="lblmewpwd" Visible="false"></asp:Label>
           </div>
           
              <%--</div>--%>
                 
	</div>
          </form>
      
          <script type="text/javascript">
    function showpass1(check_box) {
        var spass = document.getElementById("txtcurrentpwd");
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
</asp:Content>






