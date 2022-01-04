﻿<%@ Page Title="Meter Audit Report" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="RPT_MeterAudit.aspx.cs" Inherits="TraceBilling.RPT_MeterAudit" %>
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
        
          <div class="col-sm-3">Period

               <asp:TextBox ID="txtperiod" runat="server" CssClass="form-control"
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
                   <h5 class="inline">Meter Audit report</h5>
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
            <%-- <asp:BoundField DataField="propertyRef" HeaderText="PropertyRef" NullDisplayText="-" /> 
              <asp:BoundField DataField="address" HeaderText="Address" NullDisplayText="-" /> 
               <asp:BoundField DataField="territory" HeaderText="Territory" NullDisplayText="-" /> --%>
               <asp:BoundField DataField="meterNumber" HeaderText="MeterNumber" NullDisplayText="-" /> 
        <asp:BoundField DataField="preReading" HeaderText="Prev.Reading" NullDisplayText="-" /> 
          <asp:BoundField DataField="preReadingDate" HeaderText="PreReadingDate" NullDisplayText="-" /> 
               <asp:BoundField DataField="curReadingDate" HeaderText="CurReadingDate" NullDisplayText="-" /> 
               <asp:BoundField DataField="curReading" HeaderText="CurReading" NullDisplayText="-" /> 
               <asp:BoundField DataField="consumption" HeaderText="Consumption" NullDisplayText="-" /> 
               <asp:BoundField DataField="readStatus" HeaderText="Read Status" NullDisplayText="-" /> 
               <asp:BoundField DataField="isBilled" HeaderText="isBilled" NullDisplayText="-" /> 
               <asp:BoundField DataField="method" HeaderText="Method" NullDisplayText="-" />
               <asp:BoundField DataField="readType" HeaderText="readType" NullDisplayText="-" />
    

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

