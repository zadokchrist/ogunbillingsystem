<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PaymentInvoice.aspx.cs" Inherits="TraceBilling.PaymentInvoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>PAYMENT INVOICE MANAGEMENT</h3></div>
    
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
           <th class="modal-sm" style="width: 250px">Application Number</th>
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
                           <asp:TextBox ID="txtappnumber" runat="server" CssClass="form-control" Width="217px" ></asp:TextBox>
                     
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
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to invoice list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View payment invoice Logs</h5>
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
                 <asp:BoundField DataField="JobNumber" HeaderText="Job#" NullDisplayText="-" /> 
             <asp:BoundField DataField="ApplicationNumber" HeaderText="Application#" NullDisplayText="-"/> 
             
             <asp:BoundField DataField="ApplicantName" HeaderText="Name" NullDisplayText="-" /> 
             <%--<asp:BoundField DataField="Location" HeaderText="Address" NullDisplayText="-" /> --%>
            
             
                
      
                  <asp:BoundField DataField="Area" HeaderText="Area" NullDisplayText="-" /> 
              <%--   <asp:BoundField DataField="countryId" HeaderText="CountryID" NullDisplayText="-"  Visible="false"/> 
                  <asp:BoundField DataField="areaId" HeaderText="AreaID" NullDisplayText="-" Visible="false" /> --%>
                 <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" NullDisplayText="-" />
                 <asp:BoundField DataField="Amount" HeaderText="Amount" NullDisplayText="-" /> 
               <%--  <asp:ButtonField ButtonType="Button" CommandName="btnJobCard" HeaderText="Job Card"
            Text="Print" ItemStyle-ForeColor="Green" />--%>
                 <asp:ButtonField ButtonType="link" CommandName="btnSelect" HeaderText="Select"
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
          <div class="form-group col-sm-12 col-md-12 col-lg-12">
              <div id="btnlinks" runat="server" visible="false">
              <center>
              <asp:Button ID="btngenerate" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btngenerate_Click" Text="GENERATE INVOICE" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                <%--  <asp:Button ID="btnapprove" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnapprove_Click" Text="APPROVE INVOICE" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                  <asp:Button ID="btnconfirm" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnconfirm_Click" Text="CONFIRM INVOICE" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                                    <asp:Button ID="btnreconcile" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreconcile_Click" Text="RECONCILE INVOICE" cssclass ="btn-primary"
                                        Width="160px" />&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
       <div id="generateinvoice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Generate Invoice</legend>
                 
                   <br< />
                   <div id="invoicedisplay" runat="server" visible="true">
                       <%--<h3>hi customer</h3>--%>
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            
                                       <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Application Number</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtappcode" placeholder="Enter Code" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Application Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtname" placeholder="Enter name" ReadOnly="true"/>
                    </td>
                </tr>                              
            </table>
                           <br />
                           <table style="width: 491px">
                                <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 42%">
                                    New Connection Fee</td>
                                <td class="InterFaceTableMiddleRowUp">
                                </td>
                                <td class="InterFaceTableRightRowUp" style="width: 40%">
                                    <asp:TextBox ID="txtNew" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" ForeColor="Maroon" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                    <td class="InterFaceTableRightRowUp" style="width: 20%; background-color: white;
                                    text-align: left">
                                   <%-- <asp:CheckBox ID="chkNew" runat="server" Font-Bold="True" Text="Tick" Checked="true" />--%>

                                    </td>
                               
                            </tr>
                                 <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 42%">
                                    New Connection with VAT</td>
                                <td class="InterFaceTableMiddleRowUp">
                                </td>
                                <td class="InterFaceTableRightRowUp" style="width: 40%">
                                    <asp:TextBox ID="txtgross" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" ForeColor="Maroon" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                    <td class="InterFaceTableRightRowUp" style="width: 20%; background-color: white;
                                    text-align: left">
                                   <%-- <asp:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" Text="Tick" Checked="true" />--%>

                                    </td>
                               
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 42%">
                                    Deposit Fee</td>
                                <td class="InterFaceTableMiddleRowUp">
                                </td>
                                <td class="InterFaceTableRightRowUp" style="width: 40%">
                                    <asp:TextBox ID="txtDeposit" runat="server" BackColor="LightGreen" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" ForeColor="Maroon" Width="80%" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox></td>
                               <td class="InterFaceTableRightRowUp" style="width: 20%; background-color: white;
                                    text-align: left">
                                    <%--<asp:CheckBox ID="chkDeposit" runat="server" Font-Bold="True" Text="Tick" />--%>

                               </td>
                            </tr>
                           
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 42%">
                                    Total (Vat inclusive)</td>
                                <td class="InterFaceTableMiddleRowUp">
                                </td>
                                <td class="InterFaceTableRightRowUp" style="width: 40%">
                                    <asp:TextBox ID="txtTotalFee" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" ForeColor="Maroon" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                <td class="InterFaceTableRightRowUp" style="width: 64%; background-color: white;
                                    text-align: center">
                                </td>
                            </tr>
                           </table>

                           </div>
                           
                       <br />
                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                       
                      
                      <br />
              <asp:Button ID="btninvoicegeneration" runat="server" Text="Generate" cssclass ="btn-primary" OnClick="btninvoicegeneration_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btninvoicecancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btninvoicecancel_Click" />
                  
                </div>
                   </div>
           
                   </fieldset>
               
               <br />
                
              
              
            </div>
                          
                
       </div>
        <%--  <div id="approveinvoice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Approve Invoice</legend>
                 
                   <br< />
                   <div id="Div2" runat="server" visible="true">
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <p>this is approve invoice</p>
                           </div>
                           
                       <br />
                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                       <center>
                      
                      <br />
              <asp:Button ID="btninvoiceapprove" runat="server" Text="Approve Invoice" cssclass ="btn-primary" OnClick="btninvoiceapprove_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                  </center>
                </div>
                   </div>
           
                   </fieldset>
               
               <br />
                
                           
            </div>
                          
                
       </div>--%>
          <div id="confirminvoice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Confirm Invoice</legend>
                 
                   <br< />
                   <div id="div3" runat="server" visible="true">
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
           <%-- <p>this is confirm invoice</p>--%>
                           
                           </div>
                           
                       <br />
                        <div class="form-group col-sm-12 col-md-12 col-lg-12">
                       <center>
                       <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                                                Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                                                Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="Horizontal"
                                                                                HorizontalAlign="Center" OnItemCommand="DataGrid1_ItemCommand" PageSize="15"
                                                                                Style="text-align: justify; font: menu;" Width="98%" AllowPaging="True" >
                                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                        <EditItemStyle BackColor="#999999" />
                                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                                        <ItemStyle ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        <Columns>
                                                           
                                                            <asp:BoundColumn DataField="applicationNumber" HeaderText="application#" >
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="paymentRef" HeaderText="PaymentRef" >
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="paymentCode" HeaderText="PaymentCode">
                                                                <HeaderStyle Width="5%" />
                                                                </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="amountInvoiced" HeaderText="AmountInvoiced">
                                                                <HeaderStyle Width="10%" />
                                                               </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="amountPaid" HeaderText="AmountPaid">
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="balance" HeaderText="Balance">
                                                                <HeaderStyle Width="15%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="paymentDate" HeaderText="PaymentDate">
                                                                <HeaderStyle Width="15%" />
                                                            </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="Ispaid" HeaderText="Paid">
                                                                <HeaderStyle Width="10%" />
                                                                </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="IsConfirmed" HeaderText="Confirmed">
                                                                <HeaderStyle Width="10%" />
                                                                </asp:BoundColumn>
                                                            <asp:ButtonColumn CommandName="btnView" HeaderText="View" Text="View">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" ForeColor="Blue" />
                                                            </asp:ButtonColumn>
                                                            
                                                        </Columns>
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    </asp:DataGrid>
                      <br />
              <asp:Button ID="btninvoiceconfirm" runat="server" Text="Confirm Invoice" cssclass ="btn-primary" OnClick="btninvoiceconfirm_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btninvoicecancel2" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btninvoicecancel2_Click" />
                  </center>
                </div>
                   </div>
           
                   </fieldset>
               
               <br />
                
              
              
            </div>
                          
                
       </div>
          <div id="reconcileinvoice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Reconcile Invoice</legend>
                 
                   <br< />
                   <div id="Div4" runat="server" visible="true">
                       <%--<h3>hi customer</h3>--%>
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <%--<p>this is reconcile invoice</p>--%>
                           <table>
		<tr>
			<td  style="width: 277px">Total Invoiced</td>
			<td style="width: 292px">Total Paid</td>
		</tr>
		<tr>
			<td  style="width: 277px">
                <asp:TextBox ID="txtinvoice1" runat="server" Height="53px" TextMode="MultiLine" Width="87%" BackColor="LightGreen" Font-Bold="True" ForeColor="Maroon" ReadOnly="true"></asp:TextBox>
			</td>
			<td style="width: 292px">
                <asp:TextBox ID="txtpaid1" runat="server" Height="53px" TextMode="MultiLine" Width="87%" BackColor="LightGreen" Font-Bold="True" ForeColor="Maroon" ReadOnly="true"></asp:TextBox>
			</td>
		</tr>
       <%--        <tr>
                   <td class="modal-sm" style="width: 277px">Remark:</td>
                   <td style="width: 292px"> <asp:Label ID="lblremark" runat="server" Text="." Visible="False"></asp:Label></td>
               </tr>--%>
		</table>
		<div id ="remark" runat="server">
            <asp:Label runat="server" Text="Remark:" Font-Bold="true" ></asp:Label>
            <asp:Label ID="lblremark" runat="server" Text="." Visible="False" Font-Size="Small" Font-Bold="true" ForeColor="Red"></asp:Label>
		</div>
		<%--<table style="display: inline-block; border: 1px solid; ">
		<tr>
			<td>2-11</td>
			<td>2-12</td>
		</tr>
		<tr>
			<td>2-21</td>
			<td>2-22</td>
		</tr>
		
		</table>--%>
                           </div>
                           
                       <br />
                        
                   </div>
           
                   </fieldset>
               
               <br />
                
              
              
            </div>
                          
                
       </div>
         <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblconnectionId" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblarea" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblpaid" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblInvoiced" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblappid" runat="server" Text="0" Visible="False"></asp:Label>
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
