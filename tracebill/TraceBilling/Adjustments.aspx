<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Adjustments.aspx.cs" Inherits="TraceBilling.Adjustments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>ADJUSTMENT MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
              </center>
        <%--       <div>
                 
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
                             >
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
         
      
           <div id="adjustmentdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Adjustment Panel</legend>
                 
                   <br< />
                   <div id="Div2" runat="server" visible="true">
               <%--  <p>This is adjustment capture</p>--%>
                         <table style="width: 100%">
                                        <tr>
                                            <td colspan="3" style="vertical-align: top; width: 100%; text-align: center">
                                                <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                                                   
                                                    <tr>
                                                        <td colspan="4" style="vertical-align: middle; width: 100%; height: 2px; text-align: center">
                                                            <asp:TextBox ID="txtCustName" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Style="text-align: center"
                                                                Width="98%"></asp:TextBox></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 48%; text-align: right; vertical-align: top;">
                                                <table align="right" cellpadding="0" cellspacing="0" style="width: 60%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                            Cust Ref</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtcustomer" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                            Prop Ref</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtPropRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%"></asp:TextBox></td>
                                                    </tr>
                                 
                                                     <tr>
                                    <td class="InterFaceTableLeftRow" style="height: 10px">
                                        Transaction Code</td>
                                    <td class="InterFaceTableMiddleRow" style="height: 10px">
                                    </td>
                                    <td class="InterFaceTableRightRow" style="width: 66%; height: 10px">
                                        <asp:DropDownList ID="cbotranscode" runat="server" CssClass="InterfaceDropdownList"  
                                            OnDataBound="cbotranscode_DataBound" Width="90%">
                                        </asp:DropDownList></td>
                                </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                                            Document Number
                            <asp:Label ID="l1" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                        </td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                            <asp:TextBox ID="txtDocNo" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" Width="90%" ></asp:TextBox>&nbsp;</td>
                                                    </tr>
                                                     <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                                            Reason To Adjust
                               <asp:Label ID="l2" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                        </td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                            <asp:TextBox runat="server"  ID="txtcomment" placeholder="Enter  comment" Font-Bold="true"
                                                                Rows="3" TextMode="MultiLine" Width="170px" BackColor="LightGreen" ForeColor="Maroon"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="height: 12px">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 2%; height: 2px">
                                            </td>
                                            <td style="width: 48%; text-align: left; vertical-align: top;">
                                                <table align="left" cellpadding="0" cellspacing="0" style="width: 60%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                            Effective Date</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtEffectiveDate" runat="server" BackColor="LightGreen" CssClass="effectivedate"
                                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                            Amount (+ / -)</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtAmount" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="Maroon" Width="90%" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                                            <!--VAT--></td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px">
                                                            Total</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                            <asp:TextBox ID="txtTotal" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="90%" ></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                    <td class="InterFaceTableLeftRow" style="height: 10px">
                                                            Attach Evidence Document</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                           <asp:FileUpload ID="FileUpload1" runat="server" CssClass="InterfaceDropdownList"
                                                Width="80%" /></td>
                                                        </tr>
                                                    <%--<tr>
                                                        <td colspan="3" style="height: 12px">
                                                        </td>
                                                    </tr>--%>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                   </div>
                  
            <center>
                               
                <asp:Button ID="btnadd" runat="server" Text="Add Adjustment" cssclass ="btn-primary" OnClick="btnadd_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btnreturn" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturn_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
              
                   </center>
                   </fieldset>                                                      
              
            </div>
                 
       </div>
           <div id="adjustmentlog" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;View Adjustment</legend>
                 
                   <br< />
                   <div id="myadjustment" runat="server" visible="true">
                <%-- <p>This is adjustment log</p>--%>
                                    <div id="searchdisplay" runat="server" visible="true">
                 <h5 class="inline">View customer Logs</h5>
             
              
                             <asp:GridView ID="gv_customerview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="90%"
                                  AutoGenerateColumns="False" 
                                 OnRowCommand="gv_customerview_RowCommand"
                                  OnRowDataBound="gv_customerview_RowDataBound"   
                                 onselectedindexchanging="gv_customerview_SelectedIndexChanging"
                                  onselectedindexchanged="gv_customerview_SelectedIndexChanged"                                                                                          
                                 >
             <Columns>
               
             <asp:BoundField DataField="custref" HeaderText="CustRef" NullDisplayText="-"/> 

            <%-- <asp:BoundField DataField="name" HeaderText="CustName" NullDisplayText="-" />--%> 
                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" />            
       
                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> 
             <asp:BoundField DataField="transcode" HeaderText="Trans Code" NullDisplayText="-"/>              
                 <asp:BoundField DataField="DocumentNo" HeaderText="Doc No." NullDisplayText="-" /> 
                  <asp:BoundField DataField="effectivedate" HeaderText="Effective Date" NullDisplayText="-" /> 
                  <asp:BoundField DataField="amount" HeaderText="Amount" NullDisplayText="-" /> 
                  <asp:BoundField DataField="vat" HeaderText="Vat" NullDisplayText="-" /> 
                  <asp:BoundField DataField="total" HeaderText="Total" NullDisplayText="-" /> 
                  <asp:BoundField DataField="comment" HeaderText="Reason" NullDisplayText="-" /> 
          
                      <%-- <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Remove
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="replaceButton"
                                runat="server"
                                CommandName="RowRemove" 
                    CommandArgument='<%#Eval("custref") %>'
                               
                                Text="select" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>   --%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server"
                                OnClick="LinkButton1_Click">Remove</asp:LinkButton>
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
              <center>
                       <asp:Label ID="lblAdjustmentTotal" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                        Text="0" Visible="False" ForeColor="Green"></asp:Label>    <br />        
                <asp:Button ID="btnsubmit" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnsubmit_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btncancel_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                                      <asp:Button ID="btnprint" runat="server" Text="Print Adjustments" cssclass ="btn-primary" OnClick="btnprint_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                   </center>
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                    <%--    <div id="approvaldisplay" runat="server" visible="false">
        <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;View Adjustment Approval</legend>
                 
                   <br< />
                   <div id="Div3" runat="server" visible="true">
                                    <div id="Div4" runat="server" visible="true">
                 <h5 class="inline">View Approval Logs</h5>
             
              
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

                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" />            
       
                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-" /> 
             <asp:BoundField DataField="transcode" HeaderText="Trans Code" NullDisplayText="-"/>              
                 <asp:BoundField DataField="DocumentNo" HeaderText="Doc No." NullDisplayText="-" /> 
                  <asp:BoundField DataField="effectivedate" HeaderText="Effective Date" NullDisplayText="-" /> 
                  <asp:BoundField DataField="amount" HeaderText="Amount" NullDisplayText="-" /> 
                  <asp:BoundField DataField="vat" HeaderText="Vat" NullDisplayText="-" /> 
                  <asp:BoundField DataField="total" HeaderText="Total" NullDisplayText="-" /> 
                  <asp:BoundField DataField="comment" HeaderText="Reason" NullDisplayText="-" /> 
           <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Select
                    </HeaderTemplate>
            <ItemTemplate>
        
                 <asp:CheckBox ID="CheckBox1" runat="server"   Width="40px"  AutoPostBack="false"/>
                 
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
                                         <div>
                           <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True"
                            OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" />&nbsp;
                      </div>
                 </div>

                   </div>
              <center>
                       <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                        Text="0" Visible="False" ForeColor="Green"></asp:Label>    <br /> 
                  <asp:RadioButtonList ID="rbAction" runat="server" RepeatDirection="Horizontal" Width="100%" Font-Italic="True" AutoPostBack="true" OnSelectedIndexChanged="rbAction_SelectedIndexChanged">
                                                    <asp:ListItem Value="1">APPROVE</asp:ListItem>
                                                    <asp:ListItem Value="2">REJECT</asp:ListItem>
                                                   
                                                </asp:RadioButtonList><br />   
                  <asp:Label runat="server" Text="Reason" id="lblreason" Font-Bold="true"> </asp:Label>&nbsp;&nbsp;&nbsp;
               <asp:TextBox runat="server"  ID="txtapprovalcomment" placeholder="Enter comment" Rows="3" TextMode="MultiLine" Width="322px"/><br />  <br />  
                <asp:Button ID="btnsaveapproval" runat="server" Text="Save" cssclass ="btn-primary" OnClick="btnsaveapproval_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="btncancelapproval" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btncancelapproval_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                                      <asp:Button ID="btnprintapproval" runat="server" Text="Print Adjustments" cssclass ="btn-primary" OnClick="btnprintapproval_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp; 
                   </center>
                   </fieldset>                                                      
              
            </div>--%>
                 
      


      <asp:Label ID="lblcustref" runat="server" Text="." Visible="False"></asp:Label>   
        
    </form> 

	</div>

    <br /><br />
        
	</div>
 

    
    <script type="text/javascript">
        $(function () {
            $(".effectivedate").datepicker({
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

