<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ViewApplications.aspx.cs" Inherits="TraceBilling.ViewApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>VIEW APPLICATIONS INITIATED</h3></div>
    
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
<%--        <th class="modal-sm" style="width: 236px">Country</th>--%>
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
                         <%-- <td class="modal-sm" style="width: 236px" >
                      
            <asp:DropDownList ID="country_list" CssClass="form-control" runat="server"  OnDataBound="country_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged" Width="235px">
                </asp:DropDownList>
                    </td>--%>
             
                           
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
              <asp:Button ID="btnreturn" Width="253px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to application view list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View application Logs</h5>
             
              
                             <asp:GridView ID="gv_applicationview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" 
                                 OnRowCommand="gv_applicationview_RowCommand"
                                  OnRowDataBound="gv_applicationview_RowDataBound"   
                                 onselectedindexchanging="gv_applicationview_SelectedIndexChanging"
                                  onselectedindexchanged="gv_applicationview_SelectedIndexChanged"                                                             
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
             <asp:BoundField DataField="applicationNumber" HeaderText="Application#" NullDisplayText="-"/> 
               <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" NullDisplayText="-" Visible="false"/> 

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
                  <asp:BoundField DataField="areaId" HeaderText="AreaID" NullDisplayText="-" Visible="false" />
              <%--   <asp:BoundField DataField="countryId" HeaderText="CountryID" NullDisplayText="-"  Visible="false"/> 
                  --%>
                 <asp:BoundField DataField="assignedTo" HeaderText="AssignedTo" NullDisplayText="-" /> 
             <%--    <asp:ButtonField ButtonType="Button" CommandName="btnPrint" HeaderText="Foam"
            Text="Print" ItemStyle-ForeColor="Green" />
                <asp:CommandField ShowSelectButton="true" ButtonType="Button" SelectText="View"   HeaderText="Details"/>--%>
          <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Foam
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="PrintButton"
                                runat="server"
                                CommandName="RowPrint" 
                     CommandArgument='<%#Eval("applicationNumber") + ";" +Eval("areaId") %>'
                               
                                Text="Print" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
                       <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Logs
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="viewButton"
                                runat="server"
                                CommandName="RowView" 
                    CommandArgument='<%#Eval("applicationNumber") %>'
                               
                                Text="View" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>    
                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Panel
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="viewButtonPanel"
                                runat="server"
                                CommandName="RowPanel" 
                    CommandArgument='<%#Eval("applicationNumber") + ";" +Eval("fullName") + ";" +Eval("ApplicationID") %>'
                               
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

               
           <%-- </asp:View>
                </asp:MultiView>--%>
                      
             
            </div>
          <br />
        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                  <%--<center>--%>
               <div id="statuslogdisplay" style="width:800px; margin:0 auto;  text-align: center;"  runat="server" visible="false">
                <asp:Label runat="server" Text="View Application status logs" id="lbllogmsg" Visible="true" Font-Bold="true" ForeColor="Blue"></asp:Label>
               <asp:GridView ID="gvlogdisplay" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False"
                              
                   >
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="applicationNumber" HeaderText="application#" NullDisplayText="-" />   
             <asp:BoundField DataField="statusName" HeaderText="Status"  NullDisplayText="-"/>                
             <asp:BoundField DataField="LoggedBy" HeaderText="Logged By" NullDisplayText="-"/> 
             <asp:BoundField DataField="LogDate" HeaderText="log Date" NullDisplayText="-" />                         

        
              
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
          <%--</center>--%>
            <br />
            <center>
               <asp:Button ID="btnreturn3" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturn3_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
               
          </center>
                  </div>
          <div class="form-group col-sm-12 col-md-12 col-lg-12">
                             <div id="paneldisplay" style="width:800px; margin:0 auto;  text-align: center;"  runat="server" visible="false">

                                                  <table align="center" cellpadding="0" cellspacing="0" class="style12" width="85%">
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; width: 100%; height: 5px; text-align: center">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tbody>
                                                        <tr>
                                                            <td class="InterfaceHeaderLabel3" style="height: 18px">
                                                               <b> View options for printing</b></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                      
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" colspan="3" style="vertical-align: top; width: 100%;
                                                height: 10px; text-align: center">
                                               <%-- <asp:TextBox ID="txtCustName" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Style="text-align: center"
                                                    Width="60%"></asp:TextBox>--%>
                                                 <asp:Label ID="lblname" runat="server" Text="." Visible="true" ForeColor="Maroon" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 10px; text-align: left">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align: top; width: 48%; height: 10px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%">
                                                            Applicant Details</td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 2%">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 30px">
                                                            <asp:Button ID="btnAppDetails" runat="server" Font-Bold="False" OnClick="btnAppDetails_Click"
                                                                Text="View Details" Width="75%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 29px">
                                                            Documents</td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 2%; height: 29px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 29px">
                                                            <asp:Button ID="btnDocuments" runat="server" Font-Bold="False" OnClick="btnDocuments_Click"
                                                                Text="View Document(s)" Width="75%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 29px">
                                                            Job Card</td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 2%; height: 29px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 29px">
                                                            <asp:Button ID="btnJobCard" runat="server" Font-Bold="False" OnClick="btnJobCard_Click"
                                                                Text="View Job Card" Width="75%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 29px">
                                                            Invoice</td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 2%; height: 29px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 29px">
                                                            <asp:Button ID="Button1" runat="server" Font-Bold="False" OnClick="Button1_Click"
                                                                Text="View Invoice" Width="75%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 29px">
                                                            Audit Trail</td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 2%; height: 29px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 29px">
                                                            <asp:Button ID="btnAudit" runat="server" Font-Bold="False" OnClick="btnAudit_Click"
                                                                Text="View Audit Trail" Width="75%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 2px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="vertical-align: top; width: 2%; height: 10px; text-align: center">
                                            </td>
                                            <td style="vertical-align: top; width: 48%; height: 10px; text-align: left">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 98%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 30px">
                                                            Payment Slip(s)</td>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 30px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 30px">
                                                            <asp:Button ID="btnSlips" runat="server" Font-Bold="False" OnClick="btnSlips_Click"
                                                                Text="View Slips" Width="75%" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 30px">
                                                            Expense</td>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 30px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 30px">
                                                            <asp:Button ID="btnExpense" runat="server" Font-Bold="False" OnClick="btnExpense_Click"
                                                                Text="View Expenses" Width="75%" />&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 28px">
                                                            Trench Details</td>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 28px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 28px">
                                                            <asp:Button ID="btnTrench" runat="server" Font-Bold="False" OnClick="btnTrench_Click"
                                                                Text="View Trench Details" Width="75%" />&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 31px">
                                                            Docket</td>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 31px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: blue; height: 31px">
                                                            <asp:Button ID="btnDocket" runat="server" Font-Bold="False" OnClick="btnDocket_Click"
                                                                Text="View Docket" Width="75%" /></td>
                                                    </tr>
                                                   <%-- <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; height: 31px">
                                                            None</td>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 31px">
                                                        </td>
                                                        <td class="InterFaceTableLeftRowUp" style="width: 50%; color: red; height: 31px">
                                                            <asp:Button ID="btnNone" runat="server" Font-Bold="False" OnClick="btnNone_Click"
                                                                Text="None" Width="75%" Enabled="False" /></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td colspan="3" style="height: 2px">
                                                        </td>
                                                    </tr>
                                                </table>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; width: 100%; height: 10px; text-align: center">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" colspan="3" style="vertical-align: top; width: 100%;
                                                height: 10px; text-align: center">
                                                <%--<asp:Button ID="Button5" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Size="9pt"
                            Height="23px" OnClick="Button5_Click" Text="Return" Width="141px" />--%>
                                        <asp:Button ID="btnreturnpanel" runat="server" Text="Return" Font-Bold="true" cssclass ="btn-primary" OnClick="btnreturnpanel_Click" Width="81px" />

                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; height: 2px; text-align: center">
                                            </td>
                                        </tr>
                                    </table>

          </div>
        </div>
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
