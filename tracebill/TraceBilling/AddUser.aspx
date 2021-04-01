<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="TraceBilling.AddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <div><h3>
              <asp:Label runat="server" ID="lbluser" Visible="true" Text="NEW USER DETAILS" Font-Bold="true"></asp:Label>
              <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>
               </h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>
         <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="200px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to View User list" onclick="btnReturn_Click" />
          </div>
        <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <table>
                <tr>
                    <td style="width: 502px"><label for="firstname">First Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtfirstname" placeholder="Enter FirstName"/></td>
                </tr>
                 <tr>
                    <td style="width: 502px"><label for="lastname">Last Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtlastname" placeholder="Enter LastName" AutoPostBack="True" OnTextChanged="txtlastname_TextChanged"/></td>
                </tr>
                 <tr>
                    <td style="width: 502px"><label for="othername">Other Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtothername" placeholder="Enter OtherName"/></td>
                </tr>
                 <tr>
                    <td style="width: 302px"><label for="username">Preferred User Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtusername" placeholder="Enter username" ReadOnly="true"/>
<asp:checkbox runat="server" CssClass="form-control" ID="chkuser" Text="Tick if changing username" AutoPostBack="true" OnCheckedChanged="chkuserChanged" ></asp:checkbox>
                    </td>
                     
                </tr>
                 <tr>
                    <td style="width: 502px"><label for="phonenumber">Phone Number 1</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtphone" placeholder="Enter Contact Number 1"/></td>
                </tr>
                     <tr>
                    <td style="width: 502px"><label for="phonenumber2">Phone Number 2</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtphone2" placeholder="Enter Contact Number 2"/></td>
                </tr>
                 <tr>
                    <td style="width: 502px"><label for="email">Email</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtemail" placeholder="Enter Email address"/></td>
                </tr>
                 <tr>
                    <td style="width: 502px"><label for="designation">Designation</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtdesignation" placeholder="Enter Job Designation"/></td>
                </tr>                      
           <tr>
                    <td style="width: 502px">
                        <label runat="server" for="reason" id="reason">Reason for adding user</label>
               <asp:TextBox runat="server" CssClass="form-control" ID="txtreason" placeholder="Enter any reason" Rows="3" TextMode="MultiLine"/>
                    </td>
                </tr>

                
                <tr>
                    <td style="width: 502px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <br />
                    </td>
                </tr>
              
             
            </table>
                      
             
            </div>
          <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <table>
                  
                  <tr>
                    <td style="width: 502px">
                         <asp:Label runat="server" Text="Role" ID="role" Visible="true" Font-Bold="true"></asp:Label>
                  <asp:DropDownList ID="role_list" CssClass="form-control" runat="server"  OnDataBound="role_list_DataBound" Visible="true">
                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                                   <asp:Label runat="server" Text="Country" ID="countryid" Visible="true" Font-Bold="true"></asp:Label>
                         <asp:DropDownList ID="country_list" CssClass="form-control" runat="server" AutoPostBack="True" OnDataBound="country_list_DataBound"
                           OnSelectedIndexChanged="country_list_SelectedIndexChanged" Visible="true">
                       </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Operation Area" ID="lbloparea" Visible="true" Font-Bold="true"></asp:Label>
            <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" >
                </asp:DropDownList>
                    </td>
                </tr>
                 
                      <tr>
                    <td style="width: 502px">
                         <asp:Label runat="server" Text="Operation Branch" ID="txtbranch" Visible="true" Font-Bold="true"></asp:Label>
                  <asp:DropDownList ID="branch_list" CssClass="form-control" runat="server"  OnDataBound="branch_list_DataBound" Visible="true">
                </asp:DropDownList>
                    </td>
                </tr>
               
                 <tr>
                    <td style="width: 502px"><label for="txtactive">Active</label>
                        <asp:checkbox runat="server" CssClass="form-control" ID="chkactive"></asp:checkbox>
                    </td>
                </tr>   
            </table>
             
            
            
                 
            
            </div>
        <hr />
          
            <div class="form-group col-sm-12 col-md-12 col-lg-12">
                <center>
               <asp:Button ID="btnSave" runat="server" Text="Save User" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  </div>
          <hr />            
        
                 
    </form>

    <br /><br />
	</div>
       

</div>
</asp:Content>
