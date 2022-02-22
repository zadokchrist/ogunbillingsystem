<%@ Page Title="Statement of Account" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="RPT_Statement.aspx.cs" Inherits="TraceBilling.RPT_Statement" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <form role="form" runat="server"> 
    <ajaxToolkit:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></ajaxToolkit:ToolkitScriptManager>          

    <div class="container">
          <div class="row filters_div white_bg left_text">
      

         <div class=" col-sm-12">
                <div class="col-sm-12">
                     <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
          
             </div>     
             
  
          <div class="col-sm-3"> Customer Reference

               <asp:TextBox ID="txtcustref" runat="server" CssClass="form-control"
                              ></asp:TextBox>
          </div>
       
            <div class="col-sm-3">From Date
                 <%-- <asp:TextBox ID="txtstartdate" runat="server" CssClass="mydate"
                               ></asp:TextBox> --%> 
    <asp:TextBox ID="txtstartdate" CssClass="form-control"  runat="server" ></asp:TextBox>

   <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
       TargetControlID="txtstartdate" Format="dd/MM/yyyy" PopupPosition="BottomLeft"  />
                   

            </div>
         <div class="col-sm-3">To Date
                  <%--<asp:TextBox ID="txtenddate" runat="server" CssClass="mydate"
                               ></asp:TextBox> --%>    
     <asp:TextBox ID="txtenddate" CssClass="form-control"  runat="server" ></asp:TextBox>
                
 <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtenddate" 
     Format="dd/MM/yyyy" PopupPosition="BottomLeft"/>

            </div>
             
        <div class="col-sm-3">  
        Search 
                          <asp:Button ID="Button1" Width="150" Height="40" CssClass="btn-primary round_btn form-control"
                                    runat="server" Text="Search" onclick="Button3_Click"  />
                           
           </div>
              <div class="col-sm-3"> 
                   <asp:ImageButton ID="Imageexcel"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/excel.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imageexcel_Click" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:ImageButton ID="Imagepdf"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/pdf.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagepdf_Click" />      
                     <br />
                 <b> Export</b>
                  </div>
        </div>
        </div>
        <br /><br />
        <div class="row">
	      <div class="col-sm-12 white_bg card_border" id="maindisplay" runat="server">
                <%-- <div id ="exportexcel" align="left">
                 
            </div>--%>
                   <h5 class="inline">Customer Statement</h5>
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
         <%--  <asp:BoundField DataField="OpenBal" HeaderText="OpenBal" NullDisplayText="-"/> 
            <asp:BoundField DataField="startDate" HeaderText="Start Date" NullDisplayText="-"/>
            <asp:BoundField DataField="endDate" HeaderText="End Date" NullDisplayText="-"/>--%>
              <asp:BoundField DataField="customerRef" HeaderText="CustomerRef" NullDisplayText="-" />            
             <asp:BoundField DataField="customerName" HeaderText="CustomerName" NullDisplayText="-"/> 
             <asp:BoundField DataField="propertyRef" HeaderText="PropertyRef" NullDisplayText="-" /> 
              <%--<asp:BoundField DataField="address" HeaderText="Address" NullDisplayText="-" /> --%>
               <asp:BoundField DataField="Date" HeaderText="Post Date" NullDisplayText="-" /> 
               <asp:BoundField DataField="Transactions" HeaderText="Transactions" NullDisplayText="-" /> 
        <asp:BoundField DataField="Period" HeaderText="Period" NullDisplayText="-" /> 
          <asp:BoundField DataField="DocNo" HeaderText="DocNo" NullDisplayText="-" /> 
               <asp:BoundField DataField="Estimated" HeaderText="Estimated" NullDisplayText="-" /> 
               <asp:BoundField DataField="Reading" HeaderText="Reading" NullDisplayText="-" /> 
               <asp:BoundField DataField="Consumption" HeaderText="Consumption" NullDisplayText="-" /> 
               <asp:BoundField DataField="Amount" HeaderText="Amount" NullDisplayText="-" /> 
               <asp:BoundField DataField="MeterNumber" HeaderText="MeterNumber" NullDisplayText="-" /> 
               
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
          <asp:Label ID="lblcustref" runat="server" Text="0" Visible="False"></asp:Label>
       </form>
  <script type="text/javascript">
      $(function () {
          $(".mydate").datepicker({
              dateFormat: "dd/mm/yy"
          });
      });
      </script>
  
</asp:Content>

