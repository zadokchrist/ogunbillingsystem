<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ViewCustomers.aspx.cs" Inherits="TraceBilling.ViewCustomers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>CUSTOMER MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
          </center>
          

             <%--  <div>
                 
                   <table width="100%">
    <tr>
  
         <th class="datepicker-inline" style="width: 226px">Operation Area</th>
        <th class="modal-sm" style="width: 236px">CustRef</th>
        <th></th>
        </tr>
        <tr>

             
                           
             <td class="datepicker-inline" style="width: 226px" >
                      
            <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
                <td class="modal-sm" style="width: 236px">
<asp:TextBox ID="txtcustref" runat="server"></asp:TextBox>
                </td>
                 
                          <td>
                          <asp:Button ID="Button3" Width="150" Height="30" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button3_Click"  />
                      
           
                          </td>
                                                 
                          </tr>
                          </table>
          </div>--%>
            <div class="col-sm-3">Operation Area
               <asp:DropDownList ID="ddloperationarea" 
                                    DataTextField="operationAreaName"
                                     DataValueField="operationId" 
                                    CssClass="form-control" runat="server"
                                    OnDataBound="ddloperationarea_DataBound" Visible="true"
                             >
                        </asp:DropDownList>
          </div>
              <div class="col-sm-3">Customer Type
               <asp:DropDownList ID="ddlcusttype" 
                                    DataTextField="typeName"
                                     DataValueField="custTypeId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlcusttype_DataBound" Visible="true">
                        </asp:DropDownList>
          </div>
           <div class="col-sm-3">
           Customer Reference
              <asp:TextBox ID="txtsearch" 
                               runat="server" CssClass="form-control" ></asp:TextBox>
         </div>
                <div class="col-sm-3">  
        
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary round_btn form-control"
                                    runat="server" Text="Search" onclick="Button3_Click"  />
                           
           </div>

        <br />
         
        <div class="form-group col-sm-12 col-md-12 col-lg-12">
              <div id="btnlinks" runat="server" visible="false">
              <center>
              <asp:Button ID="btncustdetails" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btncustdetails_Click" Text="DETAILS" cssclass ="btn-primary"
                                        Width="153px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnreadingdetails" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreadingdetails_Click" Text="READINGS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnbilldetails" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnbilldetails_Click" Text="BILLS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;                 
                                  
                                    <asp:Button ID="btntransactiondetails" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btntransactiondetails_Click" Text="TRANSACTIONS" cssclass ="btn-primary"
                                        Width="160px" />&nbsp;&nbsp;&nbsp;&nbsp; 
                  <asp:Button ID="btnpaymentdetails" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnpaymentdetails_Click" Text="PAYMENTS" cssclass ="btn-primary"
                                        Width="149px" />&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
          <br />
             <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to customer list" onclick="btnReturn_Click" />
          </div>
       <div id="customerdisplay" runat="server" visible="false">
                     <div class="col-sm-12 home card" id="maindisplay" runat="server">

                   <h5 class="inline">View customer Logs</h5>
             
              
                             <asp:GridView ID="gv_customerview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" AllowPaging="True"
                                 PageSize="100"
                                  OnPageIndexChanged="gv_customerview_PageIndexChanged"
                                 OnPageIndexChanging="gv_customerview_PageIndexChanging"
                                 OnRowCommand="gv_customerview_RowCommand"
                                  OnRowDataBound="gv_customerview_RowDataBound"   
                                 onselectedindexchanging="gv_customerview_SelectedIndexChanging"
                                  onselectedindexchanged="gv_customerview_SelectedIndexChanged"                                                             
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
             <asp:BoundField DataField="custref" HeaderText="CustRef" NullDisplayText="-"/> 
               <asp:BoundField DataField="applicationId" HeaderText="Application#" NullDisplayText="-"/> 

             <asp:BoundField DataField="name" HeaderText="CustName" NullDisplayText="-" /> 
<%--              <asp:BoundField DataField="country" HeaderText="Country" NullDisplayText="-" /> 
                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" /> 
            
       
                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> --%>
             <asp:BoundField DataField="propertyref" HeaderText="PropertyRef" NullDisplayText="-"/>              
                
   <asp:BoundField DataField="balance" HeaderText="Outstanding Bal." NullDisplayText="-" /> 
              
                  <asp:BoundField DataField="creationdate" HeaderText="DateCreated" NullDisplayText="-" />
          
                       <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Details
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="viewButton"
                                runat="server"
                                CommandName="RowView" 
                    CommandArgument='<%#Eval("custref") %>'
                               
                                Text="View" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>          
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

       
                      
             
            </div>

                 
       </div>
               
               <div id="custdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Customer Details</legend>
                 
                   <br< />
                 <%--<p>This is customer display</p>--%>
                   <div class="row">
    <div class="col-md-6">

         <table style="width: 80%">
             <tr>                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                </td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                <asp:Label ID="lblcustdetails" runat="server" Text="Customer Details" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Application Number</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtappnumber" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Creation Date</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtcreationdate" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
              <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Customer Ref</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtcustomer" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        ReadOnly="True" Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox><br />
                                                                                    
                                                                                </td>
                                                                            </tr>
             <%--<tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Ref</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        ReadOnly="True" Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox><br />
                                                                                    
                                                                                </td>
                                                                            </tr>--%>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Customer Name</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtcustname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
            

             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Customer Contact </td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtcontact" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="true" ></asp:TextBox>
                                                                            </td>
                                                                        </tr>

             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Email Address</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>


                                                                            
                                                                         
                                                                            
                                                                           
             <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    Physical Address</td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtaddress" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Height="40px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Latitude</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtlatitude" runat="server"  CssClass="InterfaceTextboxLongReadOnly" ReadOnly="true"
                                                                                         Width="80%"></asp:TextBox>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Longitude</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtlongitude" runat="server"  CssClass="InterfaceTextboxLongReadOnly" ReadOnly="true"
                                                                                         Width="80%"></asp:TextBox></td>
                                                                            </tr>
             <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Territory</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtterritory" runat="server"  CssClass="InterfaceTextboxLongReadOnly"
                                                                                         Width="80%"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" style="height: 12px">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
    </div>
    <div class="col-md-6">
       <!--table2 -->
         <%--<table><tr><td>yyyy</td></tr></table>--%>
        <table style="width: 80%">
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:Label ID="lblcustbilling" runat="server" Text="Billing Details" ForeColor="Blue" Font-Bold="true"></asp:Label> 
                                      
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Customer Type</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtcusttype" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray" ></asp:TextBox>
                                                                                  
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Area</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtarea" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Branch/Zone</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtzone" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox></td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    PropertyRef</td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtproperty" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    Meter &nbsp;Number</td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtmeterNumber" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter make</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtmetermake" runat="server" CssClass="InterfaceTextboxLongReadOnly" ReadOnly="true"
                                                                                        Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Size</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtmetersize" runat="server" CssClass="InterfaceTextboxLongReadOnly" ReadOnly="true"
                                                                                        Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>

            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Tariff</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txttariff" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ReadOnly="true"></asp:TextBox>
                                                                                  
                                                                                </td>
                                                                            </tr>
                      <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Category</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtcategory" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ReadOnly="true"></asp:TextBox>
                                                                                  
                                                                                </td>
                                                                            </tr>
                        <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Outstanding Balance</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtbalance" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
           
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                 
                                                                                    <asp:CheckBox ID="chkactive" runat="server"  Text="Is Suppressed/Inactive?" Enabled="False" Font-Bold="True"/>
                                                                                </td>
                                                                            </tr>
<%--            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                 
                                                                                    <asp:CheckBox ID="chksewer" runat="server"   Text="Has Sewer" Enabled="False" Font-Bold="True"/>
                                                                                </td>
                                                                            </tr>--%>
             <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                 
                                                                                    <asp:CheckBox ID="chkclosed" runat="server"  Text="Is Closed?" Enabled="False" Font-Bold="True" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" style="height: 12px">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
    </div>
</div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

                        <div id="readingdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Readings Details</legend>
                 
                   
                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
               
               <asp:GridView ID="gvreadingdisplay" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False" PageSize="50"
                              
                   >
            
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="custRef" HeaderText="CustRef" NullDisplayText="-" />   
             <asp:BoundField DataField="billNumber" HeaderText="billNumber"  NullDisplayText="-"/>                
             <asp:BoundField DataField="period" HeaderText="Period" NullDisplayText="-"/> 
             <asp:BoundField DataField="readingType" HeaderText="Rdg Type" NullDisplayText="-" />                         
            <asp:BoundField DataField="curReadingDate" HeaderText="CurRdgDate" NullDisplayText="-"/> 
                 <asp:BoundField DataField="curReading" HeaderText="CurRdg" NullDisplayText="-"/> 
                 <asp:BoundField DataField="preReading" HeaderText="PrevRdg" NullDisplayText="-"/> 
                 <asp:BoundField DataField="preReadingDate" HeaderText="PreRdgDate" NullDisplayText="-"/> 
                 <asp:BoundField DataField="consumption" HeaderText="Consumption" NullDisplayText="-"/> 
                 <asp:BoundField DataField="Comment" HeaderText="Comment" NullDisplayText="-"/> 
                 <asp:BoundField DataField="isbilled" HeaderText="Isbilled" NullDisplayText="-"/> 
                 <asp:BoundField DataField="RdgStatus" HeaderText="RdgStatus" NullDisplayText="-"/> 
                 <asp:BoundField DataField="latitude" HeaderText="latitude" NullDisplayText="-"/> 
                 <asp:BoundField DataField="longitude" HeaderText="longitude" NullDisplayText="-"/> 
        
              
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
                        
                  </div>
                   
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                         <div id="billdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Bill Details</legend>
                 
                   <br />
                                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
               
               <asp:GridView ID="gvbilldisplay" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False" PageSize="50"
                              OnRowCommand="gvbilldisplay_RowCommand"
                                  OnRowDataBound="gvbilldisplay_RowDataBound"   
                                 onselectedindexchanging="gvbilldisplay_SelectedIndexChanging"
                                  onselectedindexchanged="gvbilldisplay_SelectedIndexChanged" 
                   >
               
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="custRef" HeaderText="CustRef" NullDisplayText="-" />   
             <asp:BoundField DataField="billNumber" HeaderText="billNumber"  NullDisplayText="-"/>                
             <asp:BoundField DataField="period" HeaderText="period" NullDisplayText="-"/> 
             <asp:BoundField DataField="billType" HeaderText="billType" NullDisplayText="-" />                         
                 <asp:BoundField DataField="billdate" HeaderText="billdate" NullDisplayText="-"/> 
                 <asp:BoundField DataField="openbalance" HeaderText="Bal B/F" NullDisplayText="-" DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="subtotal" HeaderText="BillAmount" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                 <asp:BoundField DataField="closingBalance" HeaderText="Bal C/F" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                 <asp:BoundField DataField="invoiceNumber" HeaderText="invoiceNumber" NullDisplayText="-"/> 
                 <asp:BoundField DataField="capturedby" HeaderText="capturedby" NullDisplayText="-"/> 
                 <asp:BoundField DataField="areaId" HeaderText="areaid" NullDisplayText="-" Visible="false"/> 
        <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Invoice
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="PrintButton"
                                runat="server"
                                CommandName="RowPrint" 
                     CommandArgument='<%#Eval("custRef") + ";" +Eval("billNumber")+ ";" +Eval("period") + ";" +Eval("areaid")%>'
                               
                                Text="Print" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
              
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
                        
                  </div>

                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="transactiondisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Transaction Details</legend>
                 
                   <br/>
                                         <div class="form-group col-sm-12 col-md-12 col-lg-12">
               
               <asp:GridView ID="gvtransdisplay" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False"   PageSize="50"                        
                   >
                
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="custRef" HeaderText="custRef" NullDisplayText="-" />   
             <asp:BoundField DataField="transName" HeaderText="transName"  NullDisplayText="-"/>                
             <asp:BoundField DataField="chargeType" HeaderText="chargeType" NullDisplayText="-"/> 
             <asp:BoundField DataField="billNumber" HeaderText="billNumber" NullDisplayText="-" />                         
 <asp:BoundField DataField="period" HeaderText="period" NullDisplayText="-"/> 
                   <asp:BoundField DataField="basisConsumption" HeaderText="Quantity" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="transValue" HeaderText="transValue" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="vatValue" HeaderText="vatValue" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="unitCost" HeaderText="unitCost" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                 <asp:BoundField DataField="total" HeaderText="Total" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="vatCode" HeaderText="vatrate" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="postDate" HeaderText="Post Date" NullDisplayText="-"/>
                  <asp:BoundField DataField="invoiceNumber" HeaderText="Invoice#" NullDisplayText="-" />  
         <asp:BoundField DataField="capturedby" HeaderText="Posted By" NullDisplayText="-"/> 
              
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
                        
                  </div>

                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="paymentdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Payment Details</legend>
                 
                   <br />
                                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
               
               <asp:GridView ID="gvpaymentdisplay" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False" PageSize="50"
                              
                   >
             
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="custRef" HeaderText="CustRef" NullDisplayText="-" />   
             <asp:BoundField DataField="transName" HeaderText="Vendor"  NullDisplayText="-"/>                
             <asp:BoundField DataField="documentNumber" HeaderText="DocumentNumber" NullDisplayText="-"/> 
             <asp:BoundField DataField="chargeType" HeaderText="ChargeType" NullDisplayText="-" /> 
                 <asp:BoundField DataField="amount" HeaderText="Amount" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                 <asp:BoundField DataField="postDate" HeaderText="Post Date" NullDisplayText="-"/> 
                 <asp:BoundField DataField="capturedby" HeaderText="Posted By" NullDisplayText="-"/>                         

        
              
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
                        
                  </div>

                   
           
                   </fieldset>                                                      
              
            </div>
            
       </div>
        <asp:Label ID="lblcustref" runat="server" Text="0" Visible="False"></asp:Label>

        
    </form>

    <br /><br />
        
	</div>
 
</div>
    <script type="text/javascript">
        $(function () {
            $(".readdate").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
        
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
</asp:Content>

