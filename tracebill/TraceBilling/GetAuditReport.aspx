<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="GetAuditReport.aspx.cs" Inherits="TraceBilling.GetAuditReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>VIEW AUDIT REPORT</h3></div>
    
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
         <th class="modal-sm" style="width: 226px">User Name</th>
         <th class="datepicker-inline" style="width: 226px">Start Date</th>
         <th class="datepicker-inline" style="width: 226px">End Date</th>
        <th></th>
        </tr>
        <tr>
             
                           
             <td class="modal-sm" style="width: 226px" >
                      
            <asp:TextBox ID="username" runat="server" CssClass="form-control" Width="187px" ></asp:TextBox>
                    </td>
             <td class="datepicker-inline" style="width: 250px">
                           <asp:TextBox ID="txtstartdate" runat="server" CssClass="form-control paymentdate" Width="187px" ></asp:TextBox>
                     
                          </td>
             <td class="datepicker-inline" style="width: 250px">
                           <asp:TextBox ID="txtenddate" runat="server" CssClass="form-control paymentdate" Width="183px" ></asp:TextBox>
                     
                          </td>
                
                 
                          <td>
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button3_Click" />
                      
           
                          </td>
                                                 
                          </tr>
                          </table>
                          </div>
          <hr />
        <%--  <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="202px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to transaction list" onclick="btnReturn_Click" />
          </div>--%>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View Audit Report</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
            
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
                                                
                                                            <asp:BoundColumn DataField="RecordId" HeaderText="No" >
                                                                <HeaderStyle Width="2%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Username" HeaderText="Username" >
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="ActivityDone" HeaderText="Activity Done">
                                                                </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="RecordDate" HeaderText="Date of Activity">
                                                               </asp:BoundColumn>
                                                  
                                                        </Columns>
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    </asp:DataGrid>
                      <br />
                           <asp:Button ID="btnreconexport" runat="server" Text="Export" cssclass ="btn-primary" OnClick="btnreconexport_Click" />
                  </center>
                      
             
            </div>
          <br />
       
         <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblconnectionId" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblarea" runat="server" Text="." Visible="False"></asp:Label>
    </form>

    <br /><br />
        
	</div>
 
</div>
    <script type="text/javascript">
        $(function () {
            $(".paymentdate").datepicker({
                dateFormat: "yyyy-MM-dd"
            });
        });
    </script>
    <script type ="text/javascript">

         function Comma(Num) {
             Num += '';
             Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
             Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
             x = Num.split('.');
             x1 = x[0];
             x2 = x.length > 1 ? '.' + x[1] : '';
             var rgx = /(\d+)(\d{3})/;
             while (rgx.test(x1))
                 x1 = x1.replace(rgx, '$1' + ',' + '$2');
             return x1 + x2;
         } 
    
    </script>>
</asp:Content>
