<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AuthorizeConnection.aspx.cs" Inherits="TraceBilling.AuthorizeConnection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>CONNECTION AUTHORIZATION</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        <br />
        <div class="form-group col-sm-12">
          <%--  <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">--%>
                          <table width="100%">
    <tr>
    <%--<th>New</th>--%>
           <th class="modal-sm" style="width: 250px">Application Name</th>
        <th class="modal-sm" style="width: 236px">Country</th>
         <th class="datepicker-inline" style="width: 226px">Operation Area</th>
        <th></th>
        </tr>
        <tr>
      <%--  <td>
        
        <asp:ImageButton ID="ImageButtonedit"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="assets/dist/img/add.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="ImageButton1_Click" />
        </td>--%>
           <td class="modal-sm" style="width: 250px">
                           <asp:TextBox ID="txtapplicationname" runat="server" CssClass="form-control" Width="217px" ></asp:TextBox>
                     
                          </td> 
                          <td class="modal-sm" style="width: 236px" >
                      
            <asp:DropDownList ID="country_list" CssClass="form-control" runat="server"  OnDataBound="country_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged" Width="235px">
                </asp:DropDownList>
                    </td>
             
                           
             <td class="datepicker-inline" style="width: 226px" >
                      
            <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
                
                 
                          <td>
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button3_Click" />
                      
           
                          </td>
                                                 
                          </tr>
                          </table>
                          </div>
          <hr />
          <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="171px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to application list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View application Logs</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
              
                             <asp:GridView ID="gv_surveyjobs" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" OnRowCommand="gv_surveyjobs_OnRowCommand"
                                  OnRowDataBound="gv_surveyjobs_RowDataBound"                                 
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
             <%--    <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" NullDisplayText="-" Visible="false"/> --%>
             <asp:BoundField DataField="applicationNumber" HeaderText="Application#" NullDisplayText="-"/> 
             
             <asp:BoundField DataField="fullName" HeaderText="Name" NullDisplayText="-" /> 
             <asp:BoundField DataField="address" HeaderText="Address" NullDisplayText="-" /> 
            
            <%-- <asp:BoundField DataField="contact" HeaderText="Contact" NullDisplayText="-" /> --%>
                
                
                 <asp:BoundField DataField="statusName" HeaderText="Status" NullDisplayText="-" /> 
             <asp:BoundField DataField="typeName" HeaderText="CustomerType" NullDisplayText="-">
              
                 </asp:BoundField> 
   <asp:BoundField DataField="className" HeaderText="className" NullDisplayText="-" /> 
                 <%-- <asp:BoundField DataField="serviceName" HeaderText="serviceName" NullDisplayText="-" /> --%>
               <asp:BoundField DataField="countryName" HeaderText="Country" NullDisplayText="-" /> 
                  <asp:BoundField DataField="areaName" HeaderText="Area" NullDisplayText="-" /> 
              <%--   <asp:BoundField DataField="countryId" HeaderText="CountryID" NullDisplayText="-"  Visible="false"/> 
                  <asp:BoundField DataField="areaId" HeaderText="AreaID" NullDisplayText="-" Visible="false" /> --%>
                 <asp:BoundField DataField="assignedTo" HeaderText="AssignedTo" NullDisplayText="-" /> 
                
                 <asp:ButtonField ButtonType="Button" CommandName="btnSelect" HeaderText="Select"
            Text="Select" ItemStyle-ForeColor="Blue"/>
            
                                 
             </Columns>
             
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <EditRowStyle BackColor="#2461BF" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Underline="false" ForeColor="#333333" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" CssClass="GridRows" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="GridRows" HorizontalAlign="Left" />
             <HeaderStyle CssClass="GridTopHeaderCell" Font-Bold="True" BackColor="#3c8dbc" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
             </asp:GridView>

               
           <%-- </asp:View>
                </asp:MultiView>--%>
                      
             
            </div>
          <br />
       <div id="authorizeapps" runat="server" visible="false">
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Application Details</legend>
            <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Application Code</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtappcode" placeholder="Enter Code" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Application Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtname" placeholder="Enter name" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                         <label>Customer Type</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txttype" placeholder="Enter customer type" ReadOnly="true"/>
                    </td>
                </tr>
            <tr>
                    <td style="width: 502px">
                         <label>Contact</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcontact" placeholder="Enter contact" ReadOnly="true"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                         <label>Occupation</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtoccupation" placeholder="Enter occupation" ReadOnly="true"/>
                    </td>
                </tr>
                
                 <tr>
                    <td style="width: 502px">
                         <label>Category</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcategory" placeholder="Enter category" ReadOnly="true"/>
                    </td>
                </tr>
               
            </table>
                   </fieldset>
               <br />
                       
            </div>

            <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Location Details</legend>
            <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Country</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcountry" placeholder="Enter country" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Operation Area</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtarea" placeholder="Enter area" ReadOnly="true"/>
                    </td>
                </tr>
            <tr>
                    <td style="width: 502px">
                         <label>Division</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtdivision" placeholder="Enter division" ReadOnly="true"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                         <label>Village</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtvillage" placeholder="Enter village" ReadOnly="true"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                         <label>Address</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtaddress" placeholder="Enter address" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Identity</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtidentity" placeholder="Enter identity" ReadOnly="true"/>
                    </td>
                </tr>
               
            </table>
                   </fieldset>
               <br />
                       
            </div>
       </div>
         
          <div id="connectionapps" runat="server" visible="false">
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Connection Details</legend>
            <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Pipe Diameter</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtdiameter" placeholder="Enter diameter" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Pipe Type</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtmaterial" placeholder="Enter material" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                         <label>Pipe Length</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtlength" placeholder="Enter length" ReadOnly="true"/>
                    </td>
                </tr>
            
               
            </table>
                   </fieldset>
                       
            </div>
              <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Invoice Details</legend>
            <table>
               <tr>
                    <td style="width: 502px">
                         <label>New Connection Fee</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtnewcon" placeholder="Enter connection fee" ReadOnly="true"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                         <label>VAT</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtvat" placeholder="Enter vat" ReadOnly="true"/>
                    </td>
                </tr>
                
                 <tr>
                    <td style="width: 502px">
                         <label>Invoice Total</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txttotal" placeholder="Enter total" ReadOnly="true"/>
                    </td>
                </tr>
               
            </table>
                   </fieldset>
              
                       
            </div>
                      <br />
                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                       <center>
                
              <asp:Button ID="btnapprove" runat="server" Text="Approve" cssclass ="btn-primary" OnClick="btnapprove_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnterminate" runat="server" Text="Terminate" cssclass ="btn-primary" OnClick="btnterminate_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnhold" runat="server" Text="Put on hold" cssclass ="btn-primary" OnClick="btnhold_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                  </center>
                </div>
              </div>
           <div id="approvecon" runat="server" visible="false">
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Action required</legend>
            <table>
              <%--     <tr>
                    <td style="width: 502px">
                        <label>Application Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtappname2" placeholder="Enter appname" ReadOnly="true"/>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 502px">                        
                        <asp:Label ID="lblaction" runat="server" Text="." Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                         <label>Remark/Comment</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" placeholder="Enter remark" TextMode="MultiLine" Height="51px" Width="385px" BackColor="LightGreen" ForeColor="Maroon"/>
                    </td>
                </tr>
                           
            </table>
                    <br />
              <asp:Button ID="btnsubmit" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnsubmit_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnapprovecancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btnapprovecancel_Click" />
                   </fieldset>
                       
            </div>
               </div>
        <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblconnectionId" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblarea" runat="server" Text="." Visible="False"></asp:Label>
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
