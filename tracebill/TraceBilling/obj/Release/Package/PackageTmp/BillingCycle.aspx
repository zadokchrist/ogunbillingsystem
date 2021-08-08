<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="BillingCycle.aspx.cs" Inherits="TraceBilling.BillingCycle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>BILLING CYCLE MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
               <div>
                 
                   <table width="100%">
    <tr>
    <%--<th>New</th>--%>
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
            </center>

        <br />
         
        <div class="form-group col-sm-12 col-md-12 col-lg-12">
              <div id="btnlinks" runat="server" visible="true">
              <center>
              <asp:Button ID="btnschedulerequest" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnschedulerequest_Click" Text="SCHEDULE REQUEST" cssclass ="btn-primary"
                                        Width="153px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnviewrequest" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnviewrequest_Click" Text="VIEW REQUESTS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                 
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
          <br />
       <div id="billschedule" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Schedule Bill Request</legend>
                 
                   <br< />
                   <div id="scheduledisplay" runat="server" visible="true">
               <%--  <p>This is billing schedule</p>--%>
                       <table align="center" cellpadding="0" cellspacing="0" style="width: 79%">
                         <tr>
                             <td>
                                 <table>
                                      <tr>
                    <td style="width: 400px">
                         <label>Current Period</label>
             <asp:TextBox runat="server" CssClass="form-control" ID="txtcurrentperiod" placeholder="Enter period" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 400px">
                        <label>Area</label>
            <asp:DropDownList ID="area_list3" CssClass="form-control" runat="server"  OnDataBound="area_list3_DataBound" Visible="true"
                OnSelectedIndexChanged="area_list3_SelectedIndexChanged" AutoPostBack="true" >
                
                </asp:DropDownList>
                    </td>
                </tr>
                                     <tr>
                    <td style="width: 400px">
                        <label>Block</label>
            <asp:DropDownList ID="block_list" CssClass="form-control" runat="server"  OnDataBound="block_list_DataBound" Visible="true">               
                
                </asp:DropDownList>
                    </td>
                </tr>
               
                 <tr>
                    <td style="width: 400px">
                         <label>CustRef</label>
             <asp:TextBox runat="server" CssClass="form-control" ID="txtcustref" placeholder="Enter customer Ref"  Font-Bold="true" ForeColor="Maroon" BackColor="Khaki" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
                                     </table>
                             </td>
                             <td></td>
                             <td>
                                 <table>
                                    
                                     <tr>
                    <td style="width: 400px">
                         <label>Biller Incharge</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtuser" placeholder="Enter user" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 400px">
                        <label>Branch/Zone</label>
            <asp:DropDownList ID="branch_list1" CssClass="form-control" runat="server"  OnDataBound="branch_list1_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
                </tr>    
                                      <tr>
                    <td style="width: 400px">
        <asp:CheckBox ID="chkBillRequestNow" runat="server" Font-Bold="True" Font-Size="Small" Text="Run Bill Request Later" AutoPostBack="True" OnCheckedChanged="chkBillRequestNow_CheckedChanged" />

                    </td>
                </tr>                
                                     <tr>
                    <td style="width: 400px">
                         <label>Schedule Date</label>
             <asp:TextBox runat="server" CssClass="scheduledate" ID="txtscheduledate" placeholder="Enter schedule" Font-Bold="true" ForeColor="Maroon" BackColor="Khaki"/>
                    </td>
                </tr>
                              <%--        <tr>
                    <td style="width: 400px">
                         <label>Schedule Time</label>
             <asp:TextBox runat="server" CssClass="scheduledate" ID="form-control" placeholder="Enter schedule time" Font-Bold="true" ForeColor="Maroon" BackColor="Khaki"/>
                        <asp:TextBox ID="txtCallTime" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" ForeColor="DarkRed" Width="60%" ReadOnly="true"></asp:TextBox>&nbsp;
                 
                     <asp:ImageButton ID="ImageButtonedit"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="assets/dist/img/refresh.png" 
         CssClass="btn-default inline" Width="20" Height="20" OnClick="ImageButton1_Click" />
                    </td>
                </tr>--%>

                                     </table>
                             </td>
                         </tr>
                     </table>
                       <br />
                        <center>
               <asp:Button ID="btnbillrequest" runat="server" Text="REQUEST" cssclass ="btn-primary" OnClick="btnbillrequest_Click" />
          </center>
                   </div>

           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
              
               <div id="viewrequests" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;View Bill Request Logs</legend>
                 
                   <br< />
                   <div id="requestdisplay" runat="server" visible="true">
                 <p>This is request log</p>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
             
          <div id="requestsummary" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp; Bill Request Summary</legend>
                 
                   <br< />
                   <div id="summarydisplay" runat="server" visible="true">
                       <div id="qndisplay" runat="server" visible="true">
                           <center>
                           <asp:Label ID="lblqn" runat="server" Text="." Visible="true" Font-Bold="true" ForeColor="Blue"></asp:Label>
                           <br />
                           <asp:Button ID="btnYes" runat="server" Text="YES" cssclass ="btn-primary" OnClick="btnYes_Click" Width="70px" />&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnNo" runat="server" Text="NO" cssclass ="btn-primary" OnClick="btnNo_Click" Width="70px" />
                               </center>
                       </div>
                  <table align="center" cellpadding="0" cellspacing="0" style="width: 79%">
                         <tr>
                             <td>
                                 <table>
                                      <tr>
                    <td style="width: 400px">
                         <label>Billing Period</label>
             <asp:TextBox runat="server" CssClass="form-control" ID="txtbillperiod" placeholder="Enter period" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 400px">
                        <label>Area</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtarea" placeholder="Enter area" ReadOnly="true"/>
                    </td>
                </tr>
                                     <tr>
                    <td style="width: 400px">
                        <label>Branch</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtbranch" placeholder="Enter branch" ReadOnly="true"/>
                    </td>
                </tr>
               
                 
                                     </table>
                             </td>
                             <td></td>
                             <td>
                                 <table>
                                     <tr>
                    <td style="width: 400px">
                         <label>CustRef</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcustomerref" placeholder="Enter customer" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 400px">
                        <label>Block</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtblock" placeholder="Enter blocks" ReadOnly="true"/>
                    </td>
                </tr>
                                      <tr>
                    <td style="width: 400px">
                         <label>Customer Type</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txttype"  ReadOnly="true"/>
                    </td>
                </tr>
                
                                     </table>
                             </td>
                         </tr>
                     </table>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

         <asp:Label ID="lblarea" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblbranch" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblperiod" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblblock" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblcustref" runat="server" Text="." Visible="False"></asp:Label>
        
    </form>

    <br /><br />
        
	</div>
 
</div>
 <script type="text/javascript">
        $(function () {
            $(".scheduledate").datepicker({
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
