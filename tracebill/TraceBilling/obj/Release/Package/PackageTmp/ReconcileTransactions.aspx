<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ReconcileTransactions.aspx.cs" Inherits="TraceBilling.ReconcileTransactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>RECONCILIATION PROCESS MANAGEMENT</h3></div>
    
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
          
        <th class="modal-sm" style="width: 236px">Country</th>
         <th class="modal-sm" style="width: 226px">Operation Area</th>
         <th class="datepicker-inline" style="width: 226px">Start Date</th>
         <th class="datepicker-inline" style="width: 226px">End Date</th>
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
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged" Width="203px">
                </asp:DropDownList>
                    </td>
             
                           
             <td class="modal-sm" style="width: 226px" >
                      
            <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
             <td class="datepicker-inline" style="width: 250px">
                           <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control" Width="187px" ></asp:TextBox>
                     
                          </td>
             <td class="datepicker-inline" style="width: 250px">
                           <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control" Width="183px" ></asp:TextBox>
                     
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
              <asp:Button ID="btnreturn" Width="202px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to transaction list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View payment transaction Logs</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
            <div id="right" style="padding: 10px;float:right;">
                <asp:CheckBox ID="chkTransactions" runat="server" AutoPostBack="True" Font-Bold="True"
                            Font-Names="Cambria" Font-Size="8pt" OnCheckedChanged="chkTransactions_CheckedChanged"
                            Text="Select All Transactions" />
            </div>
                                   <center>
                      <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                                                Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                                                Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="Horizontal"
                                                                                HorizontalAlign="Center" OnItemCommand="DataGrid1_ItemCommand" PageSize="50"
                                                                                Style="text-align: justify; font: menu;" Width="98%" AllowPaging="True" >
                                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                        <EditItemStyle BackColor="#999999" />
                                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                                        <ItemStyle ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        <Columns>
                                                
                                                            <asp:BoundColumn DataField="No" HeaderText="No" >
                                                                <HeaderStyle Width="2%" />
                                                            </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="recordId" HeaderText="TransID" Visible="false" >
                                                                <HeaderStyle Width="5%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="custRef" HeaderText="custRef" >
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="custName" HeaderText="custName">
                                                                <HeaderStyle Width="15%" />
                                                                </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="country" HeaderText="country">
                                                                <HeaderStyle Width="10%" />
                                                               </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="area" HeaderText="area">
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="transactionAmount" HeaderText="Amount">
                                                                <HeaderStyle Width="15%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="paymentDate" HeaderText="PaymentDate">
                                                                <HeaderStyle Width="15%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="vendorTransactionRef" HeaderText="TransactionRef">
                                                                <HeaderStyle Width="10%" />
                                                                </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="vendorIdentity" HeaderText="vendorID">
                                                                <HeaderStyle Width="10%" />
                                                                </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="paymentCode" HeaderText="paymentCode" Visible="false">
                                                                <HeaderStyle Width="5%" />
                                                                </asp:BoundColumn>
                                                             <asp:BoundColumn DataField="IsReconciled" HeaderText="IsReconciled">
                                                                <HeaderStyle Width="10%" />
                                                                </asp:BoundColumn>
                                                             
                                                            <%--<asp:ButtonColumn CommandName="btnPrint" HeaderText="Print" Text="Print">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" ForeColor="Blue" />
                                                            </asp:ButtonColumn>--%>
                                                              <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server"   Width="40px"  AutoPostBack="true"/>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    </asp:DataGrid>
                      <br />
              <asp:Button ID="btnreconcile" runat="server" Text="Reconcile Transaction(s)" cssclass ="btn-primary" OnClick="btnreconcile_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnreconcancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btnreconcancel_Click" />
                           &nbsp;&nbsp;&nbsp;&nbsp;
                           <asp:Button ID="btnreconexport" runat="server" Text="Export" cssclass ="btn-primary" OnClick="btnreconexport_Click" />
                  </center>

               
           <%-- </asp:View>
                </asp:MultiView>--%>
                      
             
            </div>
          <br />
       
         <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblconnectionId" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblarea" runat="server" Text="." Visible="False"></asp:Label>
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
