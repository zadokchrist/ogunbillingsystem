<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AddTransaction.aspx.cs" Inherits="TraceBilling.AddTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
      <div id="transactiondisplay" runat="server">
            <div><h3>New Transaction Processing</h3></div>
            
            <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>
            
            <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <table>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Customer Details" Font-Bold="true" ></asp:Label>
                 
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%"><label for="customerNumber">Payment Reference Number</label>
                      <asp:Label ID="l1" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            <asp:TextBox runat="server" CssClass="form-control" ID="txtcustrefNo" ReadOnly="false" AutoPostBack="True" OnTextChanged="txtcustrefNo_TextChanged"/></td>
                </tr>
               
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Full Name"  Font-Bold="true"></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtfullname"  placeholder="Enter Full Name" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Phone No." Font-Bold="true" ></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtcontact"  placeholder="Enter contact" ReadOnly="true"/>
                    </td>
                </tr>
               
                <%--<tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Country" Font-Bold="true" ></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtcounry"  placeholder="enter country" ReadOnly="true"/>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Operation Area" Font-Bold="true"></asp:Label>
                 
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtarea"  placeholder="Enter area" ReadOnly="true"/>
                    </td>
                </tr>
               
               
              
            
                <tr>
                    <td style="width: 50%">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                           

                    </td>
                </tr>
             
            </table>
                      
             
            </div>
          <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <table>
                 <tr>
                    <td style="width: 50%">
                        <asp:Label runat="server" Text="Payment Details" Font-Bold="true" ></asp:Label>
                 
                    </td>
                </tr>
                   <tr>
                    <td style="width: 50%">
                        <label for="country">Bank/Vendor</label>
 <asp:Label ID="l2" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            <asp:DropDownList ID="vendor_list" CssClass="form-control" runat="server"  OnDataBound="vendor_list_DataBound" Visible="true" AutoPostBack="false">
                 
                </asp:DropDownList>
                    </td>
                </tr>
            
                 <tr>
                    <td style="width: 50%">
                        <label for="transref">Transaction Reference</label>
       <asp:Label ID="l3" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            <asp:TextBox runat="server" CssClass="form-control" ID="txttransref" placeholder="Enter transaction ref."/>
                    </td>
                </tr>
           
                 <tr>
                    <td style="width: 50%">
                        <label for="amount">Amount</label>
    <asp:Label ID="l4" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            <asp:TextBox runat="server" CssClass="form-control" ID="txtamount" placeholder="Enter Amount" onkeyup = "javascript:this.value=Comma(this.value);" 
                                            Font-Bold="True" 
                                            Font-Size="14pt" 
                                            ForeColor="Firebrick"/>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 50%">
                        <label for="paymrntdate">Payment Date</label>
     <asp:Label ID="l5" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

            <%--<asp:TextBox runat="server" CssClass="form-control" ID="txtpaymetdate" placeholder="Enter Payment Date"/>--%>
<%--                        <div class="input-group date">--%>
                 <%-- <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>--%>
                    <%--<asp:TextBox runat="server" class="form-control pull-right" id="datepicker" ReadOnly="true" />--%>
                    <asp:TextBox ID="txtpaymentDate" CssClass="form-control"  runat="server" style="left: 0px; top: 0px"></asp:TextBox>
                           <%-- <asp:TextBox ID="datepicker" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                        Font-Bold="True" ForeColor="DarkRed" Width="80%"></asp:TextBox>

                                        <ajaxToolkit:CalendarExtender ID="datepicker_CalendarExtender" runat="server" TargetControlID="datepicker" Format="yyyy-MM-dd"/>--%>
<%--                    </div>--%>
                    </td>
                </tr>
               <%-- <tr>
                    <td></td>
                </tr>--%>
                 <tr>
                    <td style="width: 50%">
                        <label for="paymethod">Payment Method</label>
 <asp:Label ID="l6" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

           <asp:RadioButtonList ID="rtnpaymethod" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true" OnSelectedIndexChanged="rtnpaymethod_SelectedIndexChanged">
                        <asp:ListItem Value="1">CASH</asp:ListItem>
                        <asp:ListItem Value="2">CHEQUE</asp:ListItem>
                        <asp:ListItem Value="3">EFT</asp:ListItem>
                   </asp:RadioButtonList>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 50%">
                        <asp:Label ID="chqid" runat="server" Text="Check Number"  Font-Bold="true" ></asp:Label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcheque" placeholder="Enter check details"  readonly ="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 50%">
                        <label for="naration">Narration</label>
            <asp:TextBox ID="txtnaration" runat="server" Height="53px" TextMode="MultiLine" Width="97%" BackColor="LightGreen" Font-Bold="True" ForeColor="Maroon"></asp:TextBox>

                          </td>
                </tr> 
                 
            </table>
                               
                 
            
            </div>
                 
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
                  <center>
               <asp:Button ID="btnSave" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  </div>
          <hr />  
        </div>
    </form>
    

    <br /><br />
	</div>
</div>
      <script type="text/javascript">
        $(function () {
            $(".paymentdate").datepicker({
                dateFormat: "dd-M-yy"
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
    
   </script>
</asp:Content>
