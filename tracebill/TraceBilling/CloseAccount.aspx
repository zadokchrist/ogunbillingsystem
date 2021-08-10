<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="CloseAccount.aspx.cs" Inherits="TraceBilling.CloseAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>CUSTOMER ACCOUNT CLOSURE </h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
               <div>
                 
                   <table width="100%">
    <tr>
        <th class="modal-sm" style="width: 236px">Country</th>
         <th class="datepicker-inline" style="width: 226px">Operation Area</th>
        <th class="modal-sm" style="width: 236px">CustRef</th>
        <th></th>
        </tr>
        <tr>
          
                          <td class="modal-sm" style="width: 236px" >
                      
            <asp:DropDownList ID="country_list" CssClass="form-control" runat="server"  OnDataBound="country_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged" Width="235px">
                </asp:DropDownList>
                    </td>
             
                           
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
          </div>
            </center>

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
                  &nbsp;&nbsp;&nbsp;&nbsp; 
                  <asp:Button ID="Button1" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btncloseaccount_Click" Text="CLOSE ACCOUNT" cssclass ="btn-primary"
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
                 &nbsp;&nbsp;&nbsp;&nbsp;
          </div>
       <div id="customerdisplay" runat="server" visible="false">
                     <div class="col-sm-12 home card" id="maindisplay" runat="server" style="left: 9px; top: 0px; width: 786px" >

                   <h5 class="inline">View customer Logs</h5>
             
              
                             <asp:GridView ID="gv_customerview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" 
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
              <asp:BoundField DataField="country" HeaderText="Country" NullDisplayText="-" /> 
                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" /> 
            
       
                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> 
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
             <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Ref</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        ReadOnly="True" Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox><br />
                                                                                    
                                                                                </td>
                                                                            </tr>
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
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                 
                                                                                    <asp:CheckBox ID="chksewer" runat="server"   Text="Has Sewer" Enabled="False" Font-Bold="True"/>
                                                                                </td>
                                                                            </tr>
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
                              AutoGenerateColumns="False" PageSize="50">

                              <Columns>
                                  <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-" />
                                  <asp:BoundField DataField="custRef" HeaderText="CustRef" NullDisplayText="-" />
                                  <asp:BoundField DataField="billNumber" HeaderText="billNumber" NullDisplayText="-" />
                                  <asp:BoundField DataField="period" HeaderText="Period" NullDisplayText="-" />
                                  <asp:BoundField DataField="readingType" HeaderText="Rdg Type" NullDisplayText="-" />
                                  <asp:BoundField DataField="curReadingDate" HeaderText="CurRdgDate" NullDisplayText="-" />
                                  <asp:BoundField DataField="curReading" HeaderText="CurRdg" NullDisplayText="-" />
                                  <asp:BoundField DataField="preReading" HeaderText="PrevRdg" NullDisplayText="-" />
                                  <asp:BoundField DataField="preReadingDate" HeaderText="PreRdgDate" NullDisplayText="-" />
                                  <asp:BoundField DataField="consumption" HeaderText="Consumption" NullDisplayText="-" />
                                  <asp:BoundField DataField="Comment" HeaderText="Comment" NullDisplayText="-" />
                                  <asp:BoundField DataField="isbilled" HeaderText="Isbilled" NullDisplayText="-" />
                                  <asp:BoundField DataField="RdgStatus" HeaderText="RdgStatus" NullDisplayText="-" />
                                  <asp:BoundField DataField="latitude" HeaderText="latitude" NullDisplayText="-" />
                                  <asp:BoundField DataField="longitude" HeaderText="longitude" NullDisplayText="-" />


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

                      <div id="capturereading" runat="server" visible="false">
                          <div class="form-group col-sm-12 col-md-12 col-lg-12">
                              <fieldset class="panel panel-primary" runat="server">
                                  <legend class="w-auto">&nbsp;Capture Readings</legend>
                                  <br />
                                  <div id="capturedisplay" runat="server" visible="true">

                                      <%-- <p>this is reading capture</p>--%>
                                      <%--<label for="rdgoptions">Reading Options</label>
                                   <asp:RadioButtonList ID="rdgoptions" runat="server" RepeatDirection="Horizontal" Width="80%" AutoPostBack="true" OnSelectedIndexChanged="rdgoptions_SelectedIndexChanged">
                                       <asp:ListItem Value="1">One By One</asp:ListItem>
                                       <asp:ListItem Value="2">Bulk Upload</asp:ListItem>
                                   </asp:RadioButtonList>--%>
                                      <%--                 <div runat="server" class="align:margin: auto;  width: 50%;  border: 3px solid green;  padding: 10px;">--%>
                                      <table align="center" cellpadding="0" cellspacing="0" style="width: 79%">
                                          <tr>
                                              <td>
                                                  <table>
                                                      <tr>
                                                          <td style="width: 400px">
                                                              <label>Area</label>
                                                              <asp:TextBox ID="area_list3" runat="server" Enabled="false" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox>
                                                              <%--<asp:DropDownList ID="" CssClass="form-control" runat="server" OnDataBound="area_list3_DataBound" Visible="true"
                                                               OnSelectedIndexChanged="area_list3_SelectedIndexChanged" AutoPostBack="true">
                                                           </asp:DropDownList>--%>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td style="width: 400px">
                                                              <label>Current Period</label>
                                                              <asp:TextBox runat="server" CssClass="form-control" ID="txtcurrentperiod" placeholder="Enter period" ReadOnly="true" />
                                                          </td>
                                                      </tr>
                                                  </table>
                                              </td>
                                              <td></td>
                                          </tr>
                                      </table>
                                      <div id="onebyonedisplay" runat="server" visible="false">

                                          <table>
                                              <tr>
                                                  <td style="vertical-align: middle; height: 20px; text-align: center; width: 641px;">
                                                      <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                                          <tr>
                                                              <td class="row">
                                                                  <b>SINGLE
                                    READINGS CAPTURE</b></td>
                                                          </tr>
                                                      </table>

                                                  </td>
                                              </tr>

                                          </table>
                                          <table align="center" cellpadding="0" cellspacing="0" style="width: 85%">
                                              <tr>

                                                  <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 30%; height: 18px; text-align: center">

                                                      <label>PROPERTY REF</label>
                                                  </td>
                                                  <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 20%; height: 18px; text-align: center">

                                                      <label>CUST REF</label>
                                                  </td>
                                                  <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 10%; height: 18px; text-align: center"></td>
                                              </tr>

                                              <tr>

                                                  <td colspan="2" style="vertical-align: middle; width: 30%; height: 23px; text-align: center">
                                                      <asp:TextBox ID="txtInquirePropRef" runat="server" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                          Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                                  <td colspan="1" style="vertical-align: middle; width: 20%; height: 23px; text-align: center">
                                                      <asp:TextBox ID="txtInquireCustRef" runat="server" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                          Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                                  <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center">&nbsp;<asp:Button ID="btnInquire" runat="server" BorderStyle="Inset" Font-Bold="True"
                                                      Font-Size="9pt" OnClick="btnInquire_Click" OnClientClick="changeButtonText(this);"
                                                      Text="Inquire" Width="95%" /></td>
                                              </tr>

                                              <tr>
                                                  <td colspan="5" style="vertical-align: middle; width: 100%; height: 2px; text-align: center">
                                                      <asp:TextBox ID="TextBox1" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                          Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Style="text-align: center"
                                                          Width="98%"></asp:TextBox></td>
                                              </tr>
                                          </table>


                                          <div class="row">
                                              <div class="col-md-6">
                                                  <!--table1 -->
                                                  <%-- <table><tr><td>xxxxx</td></tr></table>--%>
                                                  <table style="width: 50%">
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 82px;">Meter Ref</td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px; width: 6px;"></td>
                                                          <td class="InterFaceTableRightRowUp" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="TextBox2" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="166%"></asp:TextBox></td>
                                                      </tr>

                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">Prop Ref</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">&nbsp;</td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="txtPropRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="153%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">Pre Reading</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;"></td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="txtPreReading" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="156%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">Pre Read Date</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;"></td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="txtPreReadDate" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="151%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">Consumption</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;"></td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="txtConsumption" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="154%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">Avg Cons.</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;"></td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="txtAvgConsumption" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="152%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">Meter Dials</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;"></td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                              <asp:TextBox ID="txtdials" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="152%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td colspan="3" style="height: 12px"></td>
                                                      </tr>
                                                  </table>
                                              </div>
                                              <div class="col-md-6">
                                                  <!--table2 -->
                                                  <%--<table><tr><td>yyyy</td></tr></table>--%>
                                                  <table style="width: 50%;">
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">Type</td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                              <asp:TextBox ID="txtType" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">Is Billed</td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                              <asp:TextBox ID="txtIsBilled" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">Reading</td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                              <asp:TextBox ID="txtReading" runat="server" BackColor="lightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="Maroon" Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
                                                              <%--<asp:RangeValidator ID="RangeValidator1" Type="Integer"  ControlToValidate="txtReading" runat="server" ErrorMessage="enter only numbers" ForeColor="Red"></asp:RangeValidator>--%>
                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">Read Date</td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                              <asp:TextBox ID="txtReadDate" runat="server" BackColor="lightBlue" CssClass="readdate"
                                                                  Font-Bold="True" ForeColor="Maroon" Width="80%"></asp:TextBox></td>
                                                      </tr>
                                                      <%--<tr>
                                                       <td class="InterFaceTableLeftRow" style="height: 10px; width: 156px;">Reader</td>
                                                       <td class="InterFaceTableMiddleRow" style="height: 10px">&nbsp;</td>
                                                       <%--<td class="InterFaceTableRightRow" style="height: 10px">
                                                           <asp:DropDownList ID="reader_list" runat="server" OnDataBound="reader_list_DataBound"
                                                               Width="80%">
                                                           </asp:DropDownList>

                                                       </td>--%>
                                                      <%--</tr>--%>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">Other Reader</td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                              <asp:TextBox ID="txtotherReader" runat="server" BackColor="lightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                  Font-Bold="True" ForeColor="Maroon" Width="80%"></asp:TextBox></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRow" style="height: 10px; width: 156px;">Comment</td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px"></td>
                                                          <td class="InterFaceTableRightRow" style="height: 10px">
                                                              <asp:DropDownList ID="comment_list" runat="server" OnDataBound="comment_list_DataBound"
                                                                  Width="80%">
                                                              </asp:DropDownList>

                                                          </td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;"></td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                              <asp:CheckBox ID="chkEstimate" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                                  Text="Tick If Reading Is An Estimate" Width="190px" /></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;"></td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                              <asp:CheckBox ID="ChkMeterReset" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                                  Text="Tick only If meter was Reset!" Width="190px" /></td>
                                                      </tr>
                                                      <tr>
                                                          <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;"></td>
                                                          <td class="InterFaceTableMiddleRowUp" style="height: 10px"></td>
                                                          <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                              <asp:CheckBox ID="ChkConsumption" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                                  Text="Tick only If expected consumption is genuinely negative!" Width="190px" /></td>
                                                      </tr>
                                                      <tr>
                                                          <td colspan="3" style="height: 12px"></td>
                                                      </tr>
                                                  </table>
                                              </div>
                                          </div>
                                          <div class="form-group col-sm-12 col-md-12 col-lg-12">
                                              <center>
               <asp:Button ID="btnSave" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>

                                          </div>

                                          <%--</div>--%>
                                      </div>
                                      <div id="bulkdisplay" runat="server" visible="false">
                                          <p>bulk uploading</p>
                                      </div>
                                  </div>
                              </fieldset>

                          </div>

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
                              
                   >
               
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="custRef" HeaderText="CustRef" NullDisplayText="-" />   
             <asp:BoundField DataField="billNumber" HeaderText="billNumber"  NullDisplayText="-"/>                
             <asp:BoundField DataField="period" HeaderText="period" NullDisplayText="-"/> 
             <asp:BoundField DataField="billType" HeaderText="billType" NullDisplayText="-" />                         
<asp:BoundField DataField="period" HeaderText="period" NullDisplayText="-"/> 
                 <asp:BoundField DataField="billdate" HeaderText="billdate" NullDisplayText="-"/> 
                 <asp:BoundField DataField="openbalance" HeaderText="Bal B/F" NullDisplayText="-" DataFormatString="{0:n}"/> 
                  <asp:BoundField DataField="subtotal" HeaderText="BillAmount" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                 <asp:BoundField DataField="closingBalance" HeaderText="Bal C/F" NullDisplayText="-"  DataFormatString="{0:n}"/> 
                 <asp:BoundField DataField="invoiceNumber" HeaderText="invoiceNumber" NullDisplayText="-"/> 
                 <asp:BoundField DataField="capturedby" HeaderText="capturedby" NullDisplayText="-"/> 
        
              
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
          <div id="closeaccountdisplay" runat="server" visible="false">
              <div class="row">
                  <div class="form-group col-sm-3 col-md-3 col-lg-3">
                      <label>Customer Relations Management Refrence</label>
                      <asp:TextBox ID="crmreference" runat="server" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                          Font-Bold="True" ForeColor="Maroon"></asp:TextBox>
                  </div>
              </div>
              <div class="row">
                  <div class="form-group col-sm-3 col-md-3 col-lg-3">
                      <label>Reason for Closure</label>
                      <asp:TextBox ID="reason" runat="server" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                          Font-Bold="True" ForeColor="Maroon"></asp:TextBox>
                  </div>
              </div>
              <div class="row">
                  <div class=" form-group col-sm-3 col-md-3 col-lg-3">
                      <asp:Button ID="Button2" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                          Font-Underline="False" OnClick="btndeactivateaccount_Click" Text="DEACTIVATE ACCOUNT" CssClass="btn-primary"
                       />
                  </div>
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

