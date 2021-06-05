<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MeterManagement.aspx.cs" Inherits="TraceBilling.MeterManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>METER ACTIVITY MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
               <div>
                 
                   <table width="100%">
    <tr>
    <%--<th>New</th>--%>
        <th class="modal-sm" style="width: 236px">Country</th>
         <th class="datepicker-inline" style="width: 226px"> Area</th>
        <th class="modal-sm" style="width: 236px">CustRef</th>
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
                  <td class="modal-sm" style="width: 236px">
<asp:TextBox ID="txtcustref" runat="server"></asp:TextBox>
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
              <asp:Button ID="btninventory" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btninventory_Click" Text="METER INVENTORY" cssclass ="btn-primary"
                                        Width="153px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnservicing" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnservicing_Click" Text="METER SERVICING" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnreplacement" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreplacement_Click" Text="METER REPLACEMENT" cssclass ="btn-primary"
                                        Width="159px" />&nbsp;&nbsp;&nbsp;&nbsp;                 
                                  
                                    <asp:Button ID="btntransfer" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btntransfer_Click" Text="METER TRANSFER" cssclass ="btn-primary"
                                        Width="160px" />&nbsp;&nbsp;&nbsp;&nbsp; 
                                     <asp:Button ID="btnapprove" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnapprove_Click" Text="APPROVE REQUESTS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

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
            
       
                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> 
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
                                                &nbsp;<asp:TextBox ID="txtCustName" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" Width="90%" style="text-align: center"></asp:TextBox>
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
                                                    Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
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
                                            Reason</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:TextBox ID="txtInitialReason" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                Font-Bold="True" ForeColor="Maroon" Height="39px" TextMode="MultiLine" Width="90%"></asp:TextBox></td>
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
                                                &nbsp;<asp:TextBox ID="txtnamereplace" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                    Font-Bold="True" ForeColor="DarkRed" Width="90%" style="text-align: center"></asp:TextBox>
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
                                            <asp:TextBox ID="TextBox14" runat="server" BackColor="lightgreen" CssClass="InterfaceTextboxLongReadOnly"
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
                 <p>This is approval requests</p>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

      <asp:Label ID="lblcustref" runat="server" Text="." Visible="False"></asp:Label>   
        
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
</asp:Content>

