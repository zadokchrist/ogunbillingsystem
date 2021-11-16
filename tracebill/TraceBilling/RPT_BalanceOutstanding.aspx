<%@ Page Title="Balance Outstanding" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="RPT_BalanceOutstanding.aspx.cs" Inherits="TraceBilling.RPT_BalanceOutstanding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <form role="form" runat="server"> 
    
    <div class="container">
          <div class="row filters_div white_bg left_text">
      

         <div class=" col-sm-12">
                <div class="col-sm-12">
                     <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
          
             </div>     
             
  
      
          <div class="col-sm-3">Branch
               <asp:DropDownList ID="ddlbranch" 
                                    DataTextField="branchName"
                                     DataValueField="branchId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlbranch_DataBound" Visible="true">
                        </asp:DropDownList>
          </div>
            <div class="col-sm-3">From Date
                  <asp:TextBox ID="txtfromdatesrc" runat="server" CssClass="mydate"
                               ></asp:TextBox>
                      

            </div>
         <div class="col-sm-3">To Date
               <asp:TextBox ID="txttodatesrc" runat="server" CssClass="mydate" 
                              ></asp:TextBox>
                         
         </div>
          <div class="col-sm-3">Filter by Amount

               <asp:TextBox ID="txtamount" runat="server" CssClass="form-control"
                              ></asp:TextBox>
          </div>
             
        <div class="col-sm-3">  
        Search 
                          <asp:Button ID="Button1" Width="150" Height="40" CssClass="btn-primary round_btn form-control"
                                    runat="server" Text="Search" onclick="Button3_Click"  />
                           
           </div>
             
        </div>
        </div>

        <div class="row">
	      <div class="col-sm-12 white_bg card_border" id="maindisplay" runat="server">
                 <div id ="exportexcel" align="left">
                 <asp:ImageButton ID="Imageexcel"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/excel.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imageexcel_Click" /><br />
                 <b> Export</b>
            </div>
                   <h5 class="inline">Balance outstanding report</h5>
                <hr />
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
                             <asp:GridView ID="GridViewIssue" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False"
                                  OnRowDataBound="GridViewIssue_RowDataBound" 
                                 onselectedindexchanging="GridViewIssue_SelectedIndexChanging"
                                  onselectedindexchanged="GridViewIssue_SelectedIndexChanged">
             <Columns>              
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
             <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-"/> 
              <asp:BoundField DataField="customerRef" HeaderText="CustRef" NullDisplayText="-" />            
             <asp:BoundField DataField="customerName" HeaderText="Name" NullDisplayText="-"/> 
             <asp:BoundField DataField="propertyRef" HeaderText="PropertyRef" NullDisplayText="-" /> 
              <asp:BoundField DataField="address" HeaderText="Address" NullDisplayText="-" /> 
               <asp:BoundField DataField="territory" HeaderText="Territory" NullDisplayText="-" /> 
               <asp:BoundField DataField="meterNumber" HeaderText="Meter Number" NullDisplayText="-" /> 
                <asp:BoundField DataField="contact" HeaderText="Contact" NullDisplayText="-" /> 
             <asp:BoundField DataField="category" HeaderText="Category" NullDisplayText="-" /> 
                  <asp:BoundField DataField="tariff" HeaderText="Tariff" NullDisplayText="-" /> 
                    <asp:BoundField DataField="custtype" HeaderText="Type" NullDisplayText="-" /> 
                  <asp:BoundField DataField="payments" HeaderText="Payments" NullDisplayText="-" /> 
                  <asp:BoundField DataField="adjustment" HeaderText="Adjustment" NullDisplayText="-" />   
            <asp:BoundField DataField="outstandingbalance" HeaderText="Outstanding Bal." NullDisplayText="-" />      

             </Columns>
             
             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
             <EditRowStyle BackColor="#2461BF" />
             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" Font-Underline="false" ForeColor="#333333" />
             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
             <AlternatingRowStyle BackColor="White" CssClass="GridRows" HorizontalAlign="Left" />
             <RowStyle BackColor="#EFF3FB" CssClass="GridRows" HorizontalAlign="Left" />
             <HeaderStyle CssClass="cecilia davido" Font-Bold="True" BackColor="#3c8dbc" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
             </asp:GridView>

                         
             
            </div>
          </div>
	</div>
       </form>
  <script type="text/javascript">
      $(function () {
          $(".mydate").datepicker({
              dateFormat: "dd-M-yy"
          });
      });
      </script>
  
</asp:Content>

