<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AddApplication.aspx.cs" Inherits="TraceBilling.ApplicationForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
      <div id="applicationdisplay" runat="server">
            <div><h3>New Connection Application Form</h3></div>
            
            <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>
            
            <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <table>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Applicant Details" Font-Bold="true" ></asp:Label>
                 
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px"><label for="ApplicationNumber">Application Number</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtApplicationNo" ReadOnly="true"/></td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                        <label for="service">Proposed Customer Type</label>
           <asp:RadioButtonList ID="customertype_list" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true">                       
                   </asp:RadioButtonList>
                     
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="First Name"  Font-Bold="true"></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtfirstname"  placeholder="Enter First Name"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Last Name" Font-Bold="true" ></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtlastname"  placeholder="Enter Last Name"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Other Name" Font-Bold="true"></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtothername"  placeholder="Enter Other Name"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Email Address" Font-Bold="true" ></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtemail"  placeholder="Enter Email Address"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Occupation" Font-Bold="true"></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtoccupation"  placeholder="Enter Ocupation"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Phone Number" Font-Bold="true"></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtphone"  placeholder="Enter Phone number"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <label for="address">Address</label>
               <asp:TextBox runat="server" CssClass="form-control" ID="txtaddress" placeholder="Enter physical address" Rows="3" TextMode="MultiLine"/>
                    </td>
                </tr>
               <tr>
                    <td style="width: 502px">
                        <label for="Personal ID">Personal Identity</label>
                        <table style="width: 96%">
                            <tr>
                                
                                <td style="width: 251px"><asp:dropdownlist ID="cboID" runat="server"  Width="87%" >
                        <asp:ListItem Value="0">--select ID Type--</asp:ListItem>
                        <asp:ListItem Value="1">National ID</asp:ListItem>
                        <asp:ListItem Value="2">Passport Number</asp:ListItem>
                        <asp:ListItem Value="3">Business Registration</asp:ListItem>
                   </asp:dropdownlist></td>
                                <td style="width: 251px"><asp:TextBox runat="server" CssClass="form-control" ID="txtidnumber" placeholder="Enter ID number" Width="91%" /></td>
                            </tr>
                            </table>
                        </td>
                   </tr>
                 
                
                <tr>
                    <td style="width: 502px">
                        <label for="uploads">Attach Files</label>
                         <table align="center" cellpadding="0" cellspacing="0" style="border-right: #a4a2ca 1px solid;
                                                    border-top: #a4a2ca 1px solid; border-left: #a4a2ca 1px solid; width: 75%; border-bottom: #a4a2ca 1px solid;
                                                    background-color: #ffffff; height: 10px;">
                                                    <tr>
                                                        <td style="height: 19px; width: 354px;">
                                                            <br />
                                                            <p id="upload-area">
                                                                <input id="FileField" runat="server" size="60" type="file" />
                                                            </p>
                                                            <p>
                                                                <input id="Button1" onclick="addFileUploadBox()" type="button" value="Add a file" />
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                     
                                                <label for="uploads"> Application Requirement CheckList(All those with Star are Compulsory)</label>
                                                <asp:CheckBoxList ID="chkBoxRequired" runat="server" Font-Bold="True" RepeatDirection="Horizontal"
                                                    Width="98%" style="font: menu" Font-Names="Arial Narrow">
                                                </asp:CheckBoxList>
                                        
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                           

                    </td>
                </tr>
             
            </table>
                      
             
            </div>
          <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <table>
                 <tr>
                    <td style="width: 502px">
                        <asp:Label runat="server" Text="Location Details" Font-Bold="true" ></asp:Label>
                 
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                        <label for="country">Country</label>
            <asp:DropDownList ID="country_list" CssClass="form-control" runat="server"  OnDataBound="country_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged">
                </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                        <label for="country">Operation Area</label>
            <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="area_list_SelectedIndexChanged">
                </asp:DropDownList>
                    </td>
                </tr>
               <%-- <tr>
                    <td style="width: 502px">
                         <label for="city">City</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcity" placeholder="Enter city"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                        <label for="state">State</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtstate" placeholder="Enter state"/>
                    </td>
                </tr>--%>
                <%-- <tr>
                    <td style="width: 502px">
                        <label for="street">Street</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtstreet" placeholder="Enter street"/>
                    </td>
                </tr>--%>
                 <tr>
                    <td style="width: 502px">
                        <label for="division">Division</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtdivision" placeholder="Enter division"/>
                    </td>
                </tr>
               <%--  <tr>
                    <td style="width: 502px">
                        <label for="zone">Zone</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtzone" placeholder="Enter zone"/>
                    </td>
                </tr>--%>
                 <tr>
                    <td style="width: 502px">
                        <label for="village">Village</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtvillage" placeholder="Enter Village"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                        <label for="plot">Plot</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtplot" placeholder="Enter Plot"/>
                    </td>
                </tr>
                
                 <tr>
                    <td style="width: 502px">
                        <label for="service">Service Type</label>
           <asp:RadioButtonList ID="rtnServicetype" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true" OnSelectedIndexChanged="rtnServicetype_SelectedIndexChanged">
                        <asp:ListItem Value="1">Water</asp:ListItem>
                        <asp:ListItem Value="2">Sewerage</asp:ListItem>
                        <asp:ListItem Value="3">Water and Sewerage</asp:ListItem>
                   </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                        <label for="tariff">Proposed Category</label>
           <asp:RadioButtonList ID="rtncategory" runat="server" RepeatDirection="Vertical" Width="65%"  AutoPostBack="true" OnSelectedIndexChanged="rtnTariff_SelectedIndexChanged">
                        <asp:ListItem Value="1">Dommestic</asp:ListItem>
                        <asp:ListItem Value="2">Commercial</asp:ListItem>
                        <asp:ListItem Value="3">Institutional/Government</asp:ListItem>
                         <asp:ListItem Value="2">Community Stand</asp:ListItem>
                   </asp:RadioButtonList>
                    </td>
                </tr>
                 
            </table>
                               
                 
            
            </div>
                 
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
                  <center>
               <asp:Button ID="btnSave" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  </div>
          <hr />  
        </div>
    </form>
    

    <br /><br />
	</div>
</div>
    <script type="text/javascript">
   function addFileUploadBox()
   {
   if (!document.getElementById || !document.createElement)
   return false;
   
   var uploadArea = document.getElementById("upload-area");
   if (!uploadArea)
   return;
   
   var newline = document.createElement("br");
   uploadArea.appendChild(newline);
   
   var newUploadBox = document.createElement("input");
   newUploadBox.type= "file";
   newUploadBox.size = "60";
   if (!addFileUploadBox.lastAssignedId)
   addFileUploadBox.lastAssignedId = 100;
   
   newUploadBox.setAttribute("id", "FileField" + addFileUploadBox.lastAssignedId);
   newUploadBox.setAttribute("name", "FileField" + addFileUploadBox.lastAssignedId);
   uploadArea.appendChild(newUploadBox);
   addFileUploadBox.lastAssignedId++;
   }
    function changeButtonText(button) {

        button.value = "Processing";

    }

</script>
</asp:Content>
