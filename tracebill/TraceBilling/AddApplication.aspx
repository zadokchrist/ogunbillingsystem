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
            
            <div class="form-group col-lg-6 col-md-12  col-sm-12 col-xs-12">
            <table>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Applicant Details" Font-Bold="true" ></asp:Label>
                 
                    </td>
                </tr>
               <%-- <tr>
                    <td style="width: 50%">
             <asp:Label runat="server" Text="Application Number" ID="appnumber" Visible="false" Font-Bold="true"></asp:Label>
                        </td>
                <td style="width: 50%">
                     <asp:TextBox runat="server" CssClass="form-control" ID="txtApplicationNo" ReadOnly="true" Visible="false"/>
                     </td>
                </tr>--%>
                 <tr>
                    <td style="width: 50%">
                        <label for="service">Proposed Customer Type</label>
                                <asp:Label ID="l1" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                     
                    </td>
                      <td style="width: 50%">
                          <asp:RadioButtonList ID="customertype_list" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="false">                       
                   </asp:RadioButtonList>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="First Name"  Font-Bold="true"></asp:Label>
                          <asp:Label ID="l2" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtfirstname"  placeholder="Enter First Name"/>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Last Name" Font-Bold="true" ></asp:Label>
                       <asp:Label ID="l3" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtlastname"  placeholder="Enter Last Name"/>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Other Name" Font-Bold="true"></asp:Label>
                        
                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtothername"  placeholder="Enter Other Name"/>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Email Address" Font-Bold="true" ></asp:Label>
                 
                    </td>
                     <td style="width: 50%">
                          <asp:TextBox runat="server" CssClass="form-control" ID="txtemail"  placeholder="Enter Email Address"/>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Occupation" Font-Bold="true"></asp:Label>
                 
                        
                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtoccupation"  placeholder="Enter Ocupation"/>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Phone Number" Font-Bold="true"></asp:Label>
                         <asp:Label ID="l4" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                        
                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtphone"  placeholder="Enter Phone number"  onkeypress="return NumberOnly()" MaxLength="12"/>
                         <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtphone" ID="revphone" ValidationExpression = "^[\s\S]{10,15}$" runat="server"
                 ErrorMessage="Minimum 9 and Maximum 12 characters required." ForeColor="Red"></asp:RegularExpressionValidator>
                     </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <label for="address">Street/Road</label>
                      <asp:Label ID="l5" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtaddress" placeholder="Enter street address" Rows="2" TextMode="MultiLine"/>
                     </td>
                </tr>
                 <tr>
                    <td style="width: 50%">
                        <label for="landmark">Nearest Landmark</label>
                      <asp:Label ID="l5x" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                     <td style="width: 50%">
                         <asp:TextBox runat="server" CssClass="form-control" ID="txtlandmark" placeholder="Enter known land mark" Rows="2" TextMode="MultiLine"/>
                     </td>
                </tr>
               <tr>
                    <td style="width: 50%">
                        <label for="Personal ID">Personal Identity</label>
                         <asp:Label ID="l6" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                        </td>
                    <td style="width: 50%">
                              <table style="width: 96%">
                            <tr>
                                
                                <td style="width: 251px">
                       
                                    <asp:DropDownList ID="cboID" CssClass="form-control" runat="server"  OnDataBound="cboID_DataBound">
                </asp:DropDownList></td>
                                <td style="width: 251px"><asp:TextBox runat="server" CssClass="form-control" ID="txtidnumber" placeholder="Enter ID number" Width="91%" /></td>
                            </tr>
                            </table>
                     </td>
                   </tr>
                 
                
                <tr>
                    <td style="width: 50%">
                        <label for="uploads">Attach Files (Maximum file size 3 Mbs)</label>
                         <table align="center" cellpadding="0" cellspacing="0" style="border-right: #a4a2ca 1px solid;
                                                    border-top: #a4a2ca 1px solid; border-left: #a4a2ca 1px solid; width: 75%; border-bottom: #a4a2ca 1px solid;
                                                    background-color: #ffffff; height: 10px;">
                                                    <tr>
                                                        <td style="height: 19px; width: 354px;">
                                                            <br />
                                                            <p id="upload-area">
                                                                <input id="FileField" runat="server" size="60" type="file" onclick="setUploadButtonState()"/>
                                                                
                                                            </p>
                                                            <p>
                                                               
                                                                <label for="Delete">
  <input id="fileinput"  onclick="removefile()" type="reset" value="Delete" />
</label>
                                                            </p>
                                                            <p>
                                                                <input id="Button1" onclick="addFileUploadBox()" type="button" value="Add a file" />
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                    </td>
                </tr>
          
                
             
            </table>
             
            </div>
          <div class="form-group col-lg-6 col-md-12  col-sm-12 col-xs-12">
            <table>
                 <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Location Details" Font-Bold="true" ></asp:Label>
                 
                    </td>
                </tr>
               <%--    <tr>
                    <td style="width: 50%">
                        <label for="country">Country</label>
            <asp:DropDownList ID="country_list" CssClass="form-control" runat="server"  OnDataBound="country_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged">
                </asp:DropDownList>
                    </td>
                </tr>--%>
                <%-- <tr>
                    <td style="width: 50%">
                        <label for="country">Administrative Area</label>
                                 <asp:Label ID="l8" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                      <td style="width: 50%">
                          <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" AutoPostBack="True"
                 >
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
                <tr><td style="width: 50%">
                         <asp:Label runat="server" Text="Branch" ID="txtbranch" Visible="true" Font-Bold="true"></asp:Label>
<%--                  <asp:Label ID="l9" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>--%>

                 </td>
                     <td style="width: 50%">
                          <asp:DropDownList ID="branch_list" CssClass="form-control" runat="server"  OnDataBound="branch_list_DataBound" Visible="true"
                              AutoPostBack="true"
                              OnSelectedIndexChanged="branch_list_SelectedIndexChanged">
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
                              OnSelectedIndexChanged="ddlterritory_SelectedIndexChanged">
                        </asp:DropDownList>
                     </td>
                </tr>
               <%--  <tr>
                    <td style="width: 50%">
                        <label for="zone">Zone</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtzone" placeholder="Enter zone"/>
                    </td>
                </tr>--%>
                 <tr>
                    <td style="width: 50%">
                        <label for="village">Sub Territory</label>
            
                    </td>
                      <td style="width: 50%">
                          <%--<asp:TextBox runat="server" CssClass="form-control" ID="txtvillage" placeholder="Enter sub territory"/>--%>
                     <asp:DropDownList ID="ddlsubterritory" 
                                    DataTextField="subTerritory"
                                     DataValueField="subTerritoryId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlsubterritory_DataBound" Visible="true"
                              >
                        </asp:DropDownList>
                      </td>
                </tr>
                 <tr>
                    <td style="width: 50%">
                        <label for="plot">Plot</label>
           
                    </td>
                      <td style="width: 50%">
 <asp:TextBox runat="server" CssClass="form-control" ID="txtplot" placeholder="Enter Plot"/>
                     </td>
                </tr>
                
                 <%--<tr>
                    <td style="width: 50%">
                        <label for="service">Service Type</label>
           
                    </td>
                      <td style="width: 50%">
                          <asp:RadioButtonList ID="rtnServicetype" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true" OnSelectedIndexChanged="rtnServicetype_SelectedIndexChanged">
                        <asp:ListItem Value="1" Selected="True">Water</asp:ListItem>
                        <asp:ListItem Value="2">Sewerage</asp:ListItem>
                        <asp:ListItem Value="3">Water and Sewerage</asp:ListItem>
                   </asp:RadioButtonList>
                     </td>
                </tr>--%>
                
                <tr>
                    <td style="width: 50%">
                        <label for="tariff">Proposed Category</label>
                                <asp:Label ID="l7" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                    </td>
                     <td style="width: 50%">
                       <%--  <asp:RadioButtonList ID="rtncategory" runat="server" RepeatDirection="Vertical" Width="65%"  AutoPostBack="false" OnSelectedIndexChanged="rtnTariff_SelectedIndexChanged">
                        <asp:ListItem Value="1">Domestic</asp:ListItem>
                        <asp:ListItem Value="2">Commercial</asp:ListItem>
                        <asp:ListItem Value="3">Institutional/Government</asp:ListItem>
                         <asp:ListItem Value="2">Community Stand</asp:ListItem>
                   </asp:RadioButtonList>--%>
                           <asp:RadioButtonList ID="rtncategory" runat="server"
                               DataTextField="className"
                                     DataValueField="classID" 
                                RepeatDirection="Vertical" Width="80%"  AutoPostBack="false">                       
                   </asp:RadioButtonList>
                     </td>
                </tr>
                  <tr>
                    <td style="width: 50%">
                     
                                                <label for="uploads"> Application Requirement CheckList(All those with Star are Compulsory)</label>
                                                <asp:CheckBoxList ID="chkBoxRequired" runat="server" Font-Bold="True" RepeatDirection="vertical"
                                                    Width="98%" style="font: menu" Font-Names="Arial Narrow">
                                                </asp:CheckBoxList>
                                        
                    </td>
                   
                </tr>
                 
            </table>
                               
                                <div id="documentdisplay" runat="server" visible="true">
                               <asp:Label ID="lbldocumets" runat="server" Text="View Attachments" ForeColor="Blue" Font-Bold="true" ></asp:Label><br />

                                            <asp:GridView ID="gvdocuments" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False"
                                  OnRowDataBound="gvdocuments_RowDataBound" 
                                 onselectedindexchanging="gvdocuments_SelectedIndexChanging"
                                  onselectedindexchanged="gvdocuments_SelectedIndexChanged"
                                 OnRowCommand="gvdocuments_RowCommand">
             <Columns>   
                   
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                 <asp:BoundField DataField="FileID" HeaderText="FileID" NullDisplayText="-" Visible="false"/> 
                 
             <asp:BoundField DataField="FilePath" HeaderText="FilePath"  NullDisplayText="-"/>                
                <asp:BoundField DataField="FileName" HeaderText="FileName"  NullDisplayText="-"/>   
          
                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Delete
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="DeleteButton"
                                runat="server"
                                CommandName="RowDelete" 
                    CommandArgument='<%#Eval("FileID") %>'
                               
                                Text="Remove" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
               
  
              
             </Columns>
            </asp:GridView>

                </div>      
            
            </div>
                 
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
                  <center>
               <asp:Button ID="btnSave" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  </div>
          <hr />  
        </div>
  <asp:Label ID="lblapplication" runat="server" Text="0" Visible="false"></asp:Label>

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
    function removefile()
    {
        //$('#fileinput').val("")
        document.getElementById("fileinput").value = null;
    }
    function setUploadButtonState() {

        var maxFileSize = 2048000 //2mb @2000-- 4MB -> 4000 * 1024....4096000
        var fileUpload = $('#fileUpload');

        if (fileUpload.val() == '') {
            return false;
        }
        else {
            if (fileUpload[0].files[0].size < maxFileSize) {
                $('#button_fileUpload').prop('disabled', false);
                return true;
            } else {
                $('#lbl_uploadMessage').text('File too big !')
                return false;
            }
        }
    }
    function NumberOnly() {
        var AsciiValue = event.keyCode
        if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
            event.returnValue = true;
        else
            event.returnValue = false;
    }
</script>
</asp:Content>
