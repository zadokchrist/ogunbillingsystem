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
        <div class="form-group col-lg-6 col-md-12  col-sm-12 col-xs-12" >
            <table>
                <tr>
                    <td style="width: 50%"><label for="firstname">First Name</label>
             <asp:Label ID="l1" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtfirstname" placeholder="Enter FirstName"/>
                     </td>
                </tr>
                 <tr>
                    <td style="width: 50%"><label for="lastname">Last Name</label>
               <asp:Label ID="l2" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtlastname" placeholder="Enter LastName" AutoPostBack="True" OnTextChanged="txtlastname_TextChanged"/>
                     </td>
                </tr>
                 <tr>
                    <td style="width: 50%"><label for="othername">Other Name</label>
           </td>
                <td style="width: 50%">
                     <asp:TextBox runat="server" CssClass="form-control" ID="txtothername" placeholder="Enter OtherName"/>
                     </td>
                 </tr>
                 <tr>
                    <td style="width: 302px"><label for="username">Preferred User Name</label>
              <asp:Label ID="l3" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                    <td style="width: 50%">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtusername" placeholder="Enter username" ReadOnly="true"/>
<asp:checkbox runat="server" CssClass="form-control" ID="chkuser" Text="Tick if changing username" AutoPostBack="true" OnCheckedChanged="chkuserChanged" ></asp:checkbox>
                     </td> 
                </tr>
                 <tr>
                    <td style="width: 50%"><label for="phonenumber">Phone Number 1</label>
               <asp:Label ID="l4" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            </td>
                <td style="width: 50%">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtphone" placeholder="Enter Contact Number 1" onkeypress="return NumberOnly()" MaxLength="12"/>
                     <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtphone" ID="revphone" ValidationExpression = "^[\s\S]{10,15}$" runat="server"
                 ErrorMessage="Minimum 9 and Maximum 12 characters required." ForeColor="Red"></asp:RegularExpressionValidator>
                     </td>
                 </tr>
                     <tr>
                    <td style="width: 50%"><label for="phonenumber2">Phone Number 2</label>
            </td>
               <td style="width: 50%">
                   <asp:TextBox runat="server" CssClass="form-control" ID="txtphone2" placeholder="Enter Contact Number 2"/>
                     </td>
                          </tr>
                                    
           

                             
             
            </table>
                      
             
            </div>
         <div class="form-group col-lg-6 col-md-12  col-sm-12 col-xs-12" >
            <table>
                   <tr>
                    <td style="width: 50%"><label for="email">Email</label>
           <asp:Label ID="l5" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

           </td>
                <td style="width: 50%">
                     <asp:TextBox runat="server" CssClass="form-control" ID="txtemail" placeholder="Enter Email address"/>
                     </td>
                   </tr>
                <%-- <tr>
                    <td style="width: 50%"><label for="designation">Designation</label>
            </td>
               <td style="width: 50%">
                   <asp:TextBox runat="server" CssClass="form-control" ID="txtdesignation" placeholder="Enter Job Designation"/>
                     </td>
                      </tr>--%>  
                  <tr>
                    <td style="width: 50%">
                         <asp:Label runat="server" Text="Designation" ID="role" Visible="true" Font-Bold="true"></asp:Label>
                     <asp:Label ID="l6" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                      <td style="width: 50%">
                          <asp:DropDownList ID="role_list" CssClass="form-control" runat="server"  OnDataBound="role_list_DataBound" Visible="true">
                </asp:DropDownList>
                     </td>
                </tr>
             <%--   <tr>
                    <td style="width: 50%">
                                   <asp:Label runat="server" Text="Country" ID="countryid" Visible="true" Font-Bold="true"></asp:Label>
                         <asp:DropDownList ID="country_list" CssClass="form-control" runat="server" AutoPostBack="True" OnDataBound="country_list_DataBound"
                           OnSelectedIndexChanged="country_list_SelectedIndexChanged" Visible="true">
                       </asp:DropDownList>
                    </td>
                </tr>--%>
              <%--  <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Administrative Area" ID="lbloparea" Visible="true" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lbx" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                    <td style="width: 50%">
                         <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" OnSelectedIndexChanged="operation_area_SelectedIndexChanged" AutoPostBack="true" Visible="true" >
                </asp:DropDownList>
                     </td>
                </tr>--%>
                                 <tr><td style="width: 50%">
                         <asp:Label runat="server" Text="Operation Area" ID="Label1" Visible="true" Font-Bold="true"></asp:Label>
                  <asp:Label ID="l9x" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                 </td>
                     <td style="width: 50%">
                          <asp:DropDownList ID="ddloperationarea" 
                                    DataTextField="operationAreaName"
                                     DataValueField="operationId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddloperationarea_DataBound" Visible="true" AutoPostBack="true"
                              OnSelectedIndexChanged="ddloperationarea_SelectedIndexChanged">
                        </asp:DropDownList>
                     </td>
                    </tr>

                 
                      <tr>
                    <td style="width: 50%">
                         <asp:Label runat="server" Text="Operation Branch" ID="txtbranch" Visible="true" Font-Bold="true"></asp:Label>
                   <asp:Label ID="lbx" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                          <td style="width: 50%">
 <asp:DropDownList ID="branch_list" CssClass="form-control" runat="server"  OnDataBound="branch_list_DataBound" Visible="true"
     AutoPostBack="true"  OnSelectedIndexChanged="branch_list_SelectedIndexChanged">
                </asp:DropDownList>
                     </td>
                </tr>
                 <tr>
                    <td style="width: 50%">
                        <label for="territory">Territory</label>
           
                    </td>
                 <td style="width: 50%">
                     <asp:DropDownList ID="ddlterritory" 
                                    DataTextField="territory"
                                     DataValueField="territoryId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlterritory_DataBound" Visible="true"
                             >
                        </asp:DropDownList>
                     </td>
                </tr>
               <tr>
                    <td style="width: 50%">
                        <label runat="server" for="reason" id="reason">Reason for adding user</label>
                  <asp:Label ID="l7" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                   <td style="width: 50%">
                       <asp:TextBox runat="server" CssClass="form-control" ID="txtreason" placeholder="Enter any reason" Rows="2" TextMode="MultiLine"/>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%"><label for="txtstatus">Status</label>
                 <asp:Label ID="l8" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>
    
                    </td>
                     <td style="width: 50%">
                          <asp:DropDownList ID="ddlstatus"
                                      DataTextField="status"
                                     DataValueField="recordId" 
                                     CssClass="form-control" runat="server" 
                                     OnDataBound="ddlstatus_DataBound" Visible="true">
                        </asp:DropDownList>
                     </td>
                </tr>
                <%-- <tr>
                    <td style="width: 50%"><label for="txtactive">Active</label>
                        
                    </td>
                     <td style="width: 50%">
                         <asp:checkbox runat="server" CssClass="form-control" ID="chkactive"></asp:checkbox>
                     </td>
                </tr>   --%>
            </table>
     
            </div>
          
            <div class="form-group col-sm-12 col-md-12 col-lg-12">
                <center>
               <asp:Button ID="btnSave" runat="server" Text="Save User" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  </div>
                
        
                 
    </form>

    <br /><br />
	</div>
       

</div>
     <script type="text/javascript">
     
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>  
</asp:Content>
