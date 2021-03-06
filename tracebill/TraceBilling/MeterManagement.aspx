<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MeterManagement.aspx.cs" Inherits="TraceBilling.MeterManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>METER ACTIVITY MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
              </center>
         <%--      <div>
                 
                   <table width="100%">
    <tr>
  
         <th class="datepicker-inline" style="width: 226px"> Area</th>
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
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button3_Click" />
                      
           
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
                           AutoPostBack="true"
                              OnSelectedIndexChanged="ddloperationarea_SelectedIndexChanged" 
                    >
                        </asp:DropDownList>
          </div>
           <div class="col-sm-3">Branch
               <asp:DropDownList ID="ddlbranch" 
                                    DataTextField="BranchName"
                                     DataValueField="BranchId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlbranch_DataBound" Visible="true">
                        </asp:DropDownList>
          </div>
              
           <div class="col-sm-3">
           Customer Reference
              <asp:TextBox ID="txtsearch" 
                               runat="server" CssClass="form-control" ></asp:TextBox>
         </div>
          <div class="col-sm-3">
           Property Reference
              <asp:TextBox ID="txtpropref" 
                               runat="server" CssClass="form-control" ></asp:TextBox>
         </div>
                <div class="col-sm-3">  
        
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary round_btn form-control"
                                    runat="server" Text="Search" onclick="Button3_Click"  />
                           
           </div>

        <br />
         
        <div class="form-group col-sm-12 col-md-12 col-lg-12">
              <div id="btnlinks" runat="server" visible="true">
              <center>
              <asp:Button ID="btninventory" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btninventory_Click" Text="METER INVENTORY" cssclass ="btn-primary"
                                        Width="153px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnservicing" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnservicing_Click" Text="METER SERVICING" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnreplacement" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreplacement_Click" Text="METER REPLACEMENT" cssclass ="btn-primary"
                                        Width="159px" />&nbsp;&nbsp;&nbsp;&nbsp;                 
                                  
                                    <%--<asp:Button ID="btntransfer" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btntransfer_Click" Text="METER TRANSFER" cssclass ="btn-primary"
                                        Width="160px" />&nbsp;&nbsp;&nbsp;&nbsp; --%>
                                     <asp:Button ID="btnapprove" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnapprove_Click" Text="APPROVE REQUESTS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
               <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to metering list" onclick="btnReturn_Click" />
          </div>
             <div id="searchdisplay" runat="server" visible="false">
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
               
             <asp:BoundField DataField="custref" HeaderText="CustRef" NullDisplayText="-"/> 

             <asp:BoundField DataField="name" HeaderText="CustName" NullDisplayText="-" /> 
                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" /> 
            
       
<%--                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> --%>
             <asp:BoundField DataField="propertyref" HeaderText="PropertyRef" NullDisplayText="-"/>              
                
                 
          
                       <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        REPLACE
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="replaceButton"
                                runat="server"
                                CommandName="RowReplace" 
                    CommandArgument='<%#Eval("custref") %>'
                               
                                Text="select" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>   
                    <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        SERVICE
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="serviceButton"
                                runat="server"
                                CommandName="RowService" 
                    CommandArgument='<%#Eval("custref") %>'
                               
                                Text="select" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField> 
                    <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        TRANSFER
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="transferButton"
                                runat="server"
                                CommandName="RowTransfer" 
                    CommandArgument='<%#Eval("custref") %>'
                               
                                Text="select" />
                 
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
          <br />
       <div id="meterinventory" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Inventory</legend>
                 
                   <br< />
                   <div id="inventorydisplay" runat="server" visible="true">
<%--                 <p>This is meter inventory</p>--%>
                                   <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Meter Type</label>
             <asp:DropDownList ID="cboType" runat="server" OnDataBound="cboType_DataBound" Width="80%">
                       </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Meter Serial</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtserial" placeholder="Enter serial" AutoPostBack="True" OnTextChanged="txtserial_TextChanged" />
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                         <label>Meter Dials</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtdials" placeholder="Enter dials" ReadOnly="true" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
               <tr>
                    <td style="width: 502px">
                         <label>Initial Reading</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtreading" placeholder="Enter initial reading" ReadOnly="true" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
                                                              <tr>
                    <td style="width: 502px">
                         <label>Meter Life/Duration</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtlife" placeholder="Enter meter life" ReadOnly="true" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 502px">
                         <label>Manufacturing Date</label>
            <asp:TextBox runat="server" CssClass="manufacturedDate" ID="txtmanufacturedate" placeholder="Enter manufacture date" ReadOnly="true" Width="228px"/>
                    </td>
                </tr>
                    <tr>
                    <td style="width: 502px">
                         <label>Meter Condition</label>
            <asp:TextBox ID="txtcondition" runat="server" Height="40px" TextMode="MultiLine" Width="50%" BackColor="LightGreen" Font-Bold="True" ForeColor="Maroon" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                                          <tr>
                    <td style="width: 502px">
                         <label>Meter Status</label>
            <asp:CheckBox ID="chkactive" runat="server"  Text="Tick if meter is active" />
                    </td>
                </tr>
            </table>
 <br />
              <asp:Button ID="btninventorysave" runat="server" Text="Save" cssclass ="btn-primary" OnClick="btninventorysave_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btninventorycancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btninventorycancel_Click" />
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="meterservice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Servicing</legend>
                 
                   <br< />
                   <div id="servicingisplay" runat="server" visible="true">
                <%-- <p>This is meter servicing</p>--%>
                       <table style="width: 100%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 60%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                               <%-- &nbsp;<asp:TextBox ID="txtCustName" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" Width="90%" style="text-align: center"></asp:TextBox>--%>
                                                 <asp:Label ID="lblservice" runat="server" Text="." Font-Bold="true" ForeColor="DarkRed"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 1px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 48%; text-align: right">
                                    <table align="right" cellpadding="0" cellspacing="0" style="width: 70%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Meter Ref</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <asp:TextBox ID="txtViewMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Prop Ref</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtViewPropRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Serial</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtViewSerial" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Dails</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtViewDial" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Type/Make</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtViewType" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Size</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtViewSize" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Pre Reading</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtViewPreRdg" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 12px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 2%; height: 2px">
                                </td>
                                <td style="vertical-align: top; width: 48%; text-align: left">
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 70%">
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Pre Read Date</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtPreRdgDate" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reading</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtrdg" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="Maroon" Width="90%" AutoPostBack="True" OnTextChanged="txtReading_TextChanged" onkeypress="return NumberOnly()"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reading Date</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtRdgDate" runat="server" BackColor="LightGreen" CssClass="rdgDateservice"
                                                    Font-Bold="True" ForeColor="Maroon" Width="90%" Height="22px"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:CheckBox ID="chkEstimated" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                    Text="Tick If Reading Is Estimated"/></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Consumption</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConsumption" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reason for Servicing</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px"><asp:DropDownList ID="cboReason" runat="server" OnDataBound="cboReason_DataBound"
                                                Width="90%">
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 12px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table> 
                                           <table style="width: 100%">
                        <tr>
                            <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td class="InterfaceHeaderLabel">
                                            <b>NEW METER INITIALISATION</b></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="vertical-align: top; width: 100%; height: 1px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 48%; text-align: right">
                                <table align="right" cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                            Comment</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:TextBox ID="txtInitialReason" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Height="39px" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                                    </tr>
                                     <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 9px">
                                            Serviced By</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 9px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 9px">
                                            <asp:TextBox ID="txtservedby" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%; height: 2px">
                            </td>
                            <td style="vertical-align: top; width: 48%; text-align: left">
                                <table align="left" cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Reading
                                        </td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtInitialReading" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" onkeypress="return NumberOnly()"
                                                Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Reading Date</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtInitialRdgDate" runat="server" BackColor="LightGreen" CssClass="rdgDateserviceinit"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                   </div>
                   <center>
                                 
                <asp:Button ID="btnservice" runat="server" Text="Submit for approval" cssclass ="btn-primary" OnClick="btnservice_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btnreturn2" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturn2_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
              
                   </center>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="meterreplacement" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Replacement</legend>
                 
                   <br< />
                   <div id="replacementdisplay" runat="server" visible="true">
                 <%--<p>This is meter replacement</p>--%>
                                               <table style="width: 100%">
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 60%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <%--&nbsp;<asp:TextBox ID="txtnamereplace" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" Width="90%" style="text-align: center"></asp:TextBox>--%>
                                                 <asp:Label ID="lblreplace" runat="server" Text="." Font-Bold="true" ForeColor="DarkRed"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; height: 1px; text-align: center">
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 48%; text-align: right">
                                    <table align="right" cellpadding="0" cellspacing="0" style="width: 70%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Meter Ref</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <asp:TextBox ID="txtmeterefrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Prop Ref</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtproprefrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Serial</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtserialrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Dails</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtdialsrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Type/Make</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtmakerep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Size</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtsizerep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Pre Reading</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtprerdgrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 12px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 2%; height: 2px">
                                </td>
                                <td style="vertical-align: top; width: 48%; text-align: left">
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 70%">
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Pre Read Date</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtprerdgdtrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reading</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtrdgreplace" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="Maroon" Width="90%" AutoPostBack="True" OnTextChanged="txtrdgreplace_TextChanged" onkeypress="return NumberOnly()"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reading Date</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtrdgdtreplace" runat="server" BackColor="LightGreen" CssClass="rdgDatereplace"
                                                    Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:CheckBox ID="chkreplace" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                    Text="Tick If Reading Is Estimated"/></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Consumption</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtconsumptionrep" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reason for Replacement</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px"><asp:DropDownList ID="cboReasonrep" runat="server" OnDataBound="cboReasonrep_DataBound"
                                                Width="90%" >
                                            </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 12px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>     
                                           <table style="width: 100%">
                        <tr>
                            <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td class="InterfaceHeaderLabel">
                                            <b>NEW METER DETAILS</b></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="vertical-align: top; width: 100%; height: 1px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 48%; text-align: right">
                                <table align="right" cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Serial</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtNewSerial" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Size</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:DropDownList ID="cboMeterSize" runat="server" OnDataBound="cboMeterSize_DataBound"
                                                Width="90%">
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Type/Make</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:DropDownList ID="cboType2" runat="server" OnDataBound="cboType2_DataBound" Width="90%" >
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Dails</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtnewdials" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" MaxLength="2" onkeypress="return NumberOnly()"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Manufactured Date</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtManufacturedDate" runat="server" BackColor="lightgreen" CssClass="manufactureDaterep"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%; height: 2px">
                            </td>
                            <td style="vertical-align: top; width: 48%; text-align: left">
                                <table align="left" cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Life(Years)</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            &nbsp;</td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtnewliferep" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" MaxLength="2" onkeypress="return NumberOnly()"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Reading</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtNewReading" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" onkeypress="return NumberOnly()"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 9px">
                                            Install Date</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 9px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 9px">
                                            <asp:TextBox ID="txtNewRdgDate" runat="server" BackColor="lightgreen" CssClass="installDate"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 9px">
                                            Installed By</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 9px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 9px">
                                            <asp:TextBox ID="txtInstalledBy" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                     <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                             Comment</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:TextBox ID="txtcommentrep" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Height="39px" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                   </div>
                   <center>
                                 
                <asp:Button ID="btnreplace" runat="server" Text="Submit for approval" cssclass ="btn-primary" OnClick="btnreplace_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="Button2" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturnreplace_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
              
                   </center>
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                         <div id="metertransfer" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Transfer</legend>
                 
                   <br< />
                   <div id="transferdisplay" runat="server" visible="true">
                 <p>This is meter transfer</p>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
           <div id="meterapproval" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Approval Requests</legend>
                 
                   <br< />
                   <div id="Div2" runat="server" visible="true">
                 <%--<p>This is approval requests</p>--%>
                                                    <asp:GridView ID="gv_approvalview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="90%"
                                  AutoGenerateColumns="False" 
                                 OnRowCommand="gv_approvalview_RowCommand"
                                  OnRowDataBound="gv_approvalview_RowDataBound"   
                                 onselectedindexchanging="gv_approvalview_SelectedIndexChanging"
                                  onselectedindexchanged="gv_approvalview_SelectedIndexChanged"                                                             
                                 >
             <Columns>
               
             <asp:BoundField DataField="custref" HeaderText="CustRef" NullDisplayText="-"/> 

             <asp:BoundField DataField="name" HeaderText="CustName" NullDisplayText="-" /> 
                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" /> 
            
       
<%--                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> --%>
             <asp:BoundField DataField="propertyRef" HeaderText="PropertyRef" NullDisplayText="-"/>              
                 <asp:BoundField DataField="requestType" HeaderText="Request Type" NullDisplayText="-"/> 
                  <asp:BoundField DataField="period" HeaderText="Period" NullDisplayText="-"/> 
                  <asp:BoundField DataField="recordId" HeaderText="Id#" Visible="true" NullDisplayText="-"/> 
          
                       <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        APPROVE
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="approbalButton"
                                runat="server"
                                CommandName="RowApprove" 
                      CommandArgument='<%#Eval("custref") + ";" +Eval("recordId") + ";" +Eval("name")%>'              
                                Text="select" />
                 
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
                   <div id="confirmdisplay" runat="server" visible="false">
                       <hr />
                       <table style="width: 100%">
                          <tr>
                                <td colspan="3" style="vertical-align: top; width: 100%; text-align: center">
                                    <table align="center" cellpadding="0" cellspacing="0" style="width: 60%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Name</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <%--&nbsp;<asp:TextBox ID="txtnamereplace" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" Width="90%" style="text-align: center"></asp:TextBox>--%>
                                                 <asp:Label ID="lblapproval" runat="server" Text="." Font-Bold="true" ForeColor="DarkRed"></asp:Label>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="vertical-align: top; width: 48%; text-align: right">
                                    <table align="right" cellpadding="0" cellspacing="0" style="width: 70%">

                                         <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Request Type</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmreqtype" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Request Number</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtconfirmid" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox>&nbsp;</td>
                                        </tr>
                                         <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Customer Ref</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <asp:TextBox ID="txtconfirmcustref" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Meter Ref</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                       
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Serial</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmSerial" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Dials</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmOldDials" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Type/Make</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmType" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Size</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmSize" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 12px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 2%; height: 2px">
                                </td>
                                <td style="vertical-align: top; width: 48%; text-align: left">
                                    <table align="left" cellpadding="0" cellspacing="0" style="width: 70%">
                                        <tr>
                                            <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                Pre Reading</td>
                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmPreReading" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Pre Read Date</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmPreRdgDate" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reading</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmCurReading" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reading Date</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmCurRdgDate" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:CheckBox ID="chkConfirmEstimated" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                    Text="Tick If Reading Is Estimated" Enabled="False" /></td>
                                        </tr>
                                        <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Consumption</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            </td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                <asp:TextBox ID="txtConfirmConsumption" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                        </tr>
                                         <tr>
                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                Reason</td>
                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                &nbsp;</td>
                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                               
                                                <asp:TextBox ID="txtConfirmreason" runat="server" BackColor="Lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Height="39px" TextMode="MultiLine" Width="90%" ReadOnly="true"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="height: 12px">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%">
                        <tr>
                            <td colspan="3" style="vertical-align: top; width: 100%; height: 2px; text-align: center">
                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                    <tr>
                                        <td class="InterfaceHeaderLabel">
                                            <b>NEW METER DETAILS</b></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="vertical-align: top; width: 100%; height: 1px; text-align: center">
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 48%; text-align: right">
                                <table align="right" cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Serial</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmnewserial" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Size</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmnewsize" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Type/Make</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                             <asp:TextBox ID="txtconfirmnewmake" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Dails</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmnewdials" runat="server" BackColor="lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" readonly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Manufactured Date</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmmanufacturedate" runat="server" BackColor="lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                     <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Required Action</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                              <asp:dropdownlist ID="cboaction" runat="server"  Width="87%" >
                        <asp:ListItem Value="0">--select action--</asp:ListItem>
                        <asp:ListItem Value="1">Approve</asp:ListItem>
                        <asp:ListItem Value="2">Reject</asp:ListItem>                       
                   </asp:dropdownlist></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%; height: 2px">
                            </td>
                            <td style="vertical-align: top; width: 48%; text-align: left">
                                <table align="left" cellpadding="0" cellspacing="0" style="width: 70%">
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Life(Years)</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                            &nbsp;</td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmnewlife" runat="server" BackColor="lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" readonly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                            Reading</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmnewreading" runat="server" BackColor="lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" readonly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 9px">
                                            Reading Date</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 9px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 9px">
                                            <asp:TextBox ID="txtconfirminstalldate" runat="server" BackColor="lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRow" style="height: 9px">
                                            Installed By</td>
                                        <td class="InterFaceTableMiddleRow" style="height: 9px">
                                        </td>
                                        <td class="InterFaceTableRightRow" style="height: 9px">
                                            <asp:TextBox ID="txtconfirminstalledby" runat="server" BackColor="lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                     <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                             Comment</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:TextBox ID="txtconfirmcomment" runat="server" BackColor="Lightgray" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Height="39px" TextMode="MultiLine" Width="90%" ReadOnly="true"></asp:TextBox></td>
                                    </tr>
                                     <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                             Approver Comment</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:TextBox ID="txtapprovercomment" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Height="39px" TextMode="MultiLine" Width="90%" ></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                        <center>
                                 
                <asp:Button ID="btnsave" runat="server" Text="Save" cssclass ="btn-primary" OnClick="btnsave_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btncancel" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btncancel_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
              
                   </center>
                   </div>
                    
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

      <asp:Label ID="lblcustref" runat="server" Text="0" Visible="False"></asp:Label> 
            <asp:Label ID="lblarea" runat="server" Text="0" Visible="False"></asp:Label> 
          <asp:Label ID="lblbranch" runat="server" Text="0" Visible="False"></asp:Label> 
          <asp:Label ID="lblperiod" runat="server" Text="0" Visible="False"></asp:Label> 
          <asp:Label ID="lblsizeid" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lbltypeid" runat="server" Text="0" Visible="False"></asp:Label>
        
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
        $(function () {
            $(".uploaddate").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
        $(function () {
            $(".manufacturedDate").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        $(function () {
            $(".rdgDateservice").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        $(function () {
            $(".rdgDateserviceinit").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        $(function () {
            $(".rdgDatereplace").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        $(function () {
            $(".manufactureDaterep").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        $(function () {
            $(".installDate").datepicker({
                dateFormat: "dd-M-yy"
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
                   </div>
</asp:Content>

