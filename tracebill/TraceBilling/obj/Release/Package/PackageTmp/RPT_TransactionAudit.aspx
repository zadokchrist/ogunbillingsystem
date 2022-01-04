<%@ Page Title="Transaction Audit Report" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="RPT_TransactionAudit.aspx.cs" Inherits="TraceBilling.RPT_TransactionAudit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <form role="form" runat="server"> 
    
    <div class="container">
          <div class="row filters_div white_bg left_text">
  

         <div class=" col-sm-12">
                <div class="col-sm-12">
                     <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
          
             </div>     
             
  <div class="col-sm-3">Operation Area
              <asp:DropDownList ID="ddloperationarea" 
                                    DataTextField="operationAreaName"
                                     DataValueField="operationId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddloperationarea_DataBound" Visible="true" AutoPostBack="true"
                              OnSelectedIndexChanged="ddloperationarea_SelectedIndexChanged">
                        </asp:DropDownList>
          </div>
      
          <div class="col-sm-3">Branch
               <asp:DropDownList ID="ddlbranch" 
                                    DataTextField="branchName"
                                     DataValueField="branchId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlbranch_DataBound" Visible="true">
                        </asp:DropDownList>
          </div>
        
          <div class="col-sm-3">Period

               <asp:TextBox ID="txtperiod" runat="server" CssClass="form-control"
                              ></asp:TextBox>
          </div>
              <div class="col-sm-3">Transaction Code
               <asp:DropDownList ID="ddtranscode" 
                                    DataTextField="transName"
                                     DataValueField="transCode" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddtranscode_DataBound" Visible="true">
                        </asp:DropDownList>
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
                   <h5 class="inline">Transaction Audit report</h5>
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
          
             <asp:BoundField DataField="No" HeaderText="#" NullDisplayText="-"/> 
                 <asp:BoundField DataField="branch" HeaderText="Branch" NullDisplayText="-"/> 
              <asp:BoundField DataField="customerRef" HeaderText="CustomerRef" NullDisplayText="-" />            
             <asp:BoundField DataField="customerName" HeaderText="CustomerName" NullDisplayText="-"/> 
             <asp:BoundField DataField="territory" HeaderText="Territory" NullDisplayText="-" /> 
              <asp:BoundField DataField="transName" HeaderText="TransName" NullDisplayText="-" /> 
               <asp:BoundField DataField="chargeType" HeaderText="ChargeType" NullDisplayText="-" /> 
               <asp:BoundField DataField="period" HeaderText="Period" NullDisplayText="-" />     
            <asp:BoundField DataField="transValue" HeaderText="TransValue" NullDisplayText="-" /> 
                  <asp:BoundField DataField="vatValue" HeaderText="VatValue" NullDisplayText="-" /> 
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

