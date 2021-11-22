<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="TraceBilling.Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <div><h3>REPORT MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        
        <div class="form-group col-sm-12" align="left">
             <hr/>
       <table>
           <tr>
               <td width="10%"><asp:ImageButton ID="Imagebalance"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagebalance_Click" /><br />
                   <b>             Balance Outstanding</b></td>
                <td width="10%"><asp:ImageButton ID="Imagereadingaudit"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagereadingaudit_Click" /><br />
                   <b>  Meter Reading Audit</b></td>
                <td width="10%"><asp:ImageButton ID="Imagetransaudit"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagetransaudit_Click" /><br />
                   <b>  Transaction Audit</b></td>
                <td width="10%"><asp:ImageButton ID="Imagecount"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagecount_Click" /><br />
                   <b>  Customer Count</b></td>
                <td width="10%"><asp:ImageButton ID="Imagestatement"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagestatement_Click" /><br />
                   <b>  Statement</b></td>
          <td width="10%"><asp:ImageButton ID="Imagesales"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagesales_Click" /><br />
                   <b>  Water Sales</b></td>
               <td width="10%"><asp:ImageButton ID="Imageconnection"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imageconnection_Click" /><br />
                   <b>  New Connection</b></td>
               <td width="10%"><asp:ImageButton ID="Imagebilling"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/report.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imagebilling_Click" /><br />
                   <b>  Billing Summary</b></td>
           </tr>
          <%-- <tr>

           </tr>--%>
       </table>
            <hr/>
            <div id ="exportexcel" align="left">
                 <asp:ImageButton ID="Imageexcel"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="images/excel.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="Imageexcel_Click" /><br />
                 <b> Export</b>
            </div>

                          </div>

                                <div id="displaybaloutstanding" runat="server" visible="false" align="left">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Balance outstanding</legend>
                          <table width="100%">

        <tr>

             <td style="width: 20%">
                         <asp:Label runat="server" Text="Branch" ID="branchid" Visible="true" Font-Bold="true"></asp:Label>
                  <asp:DropDownList ID="branch_list" CssClass="form-control" runat="server"  OnDataBound="branch_list_DataBound" Visible="true"
                      AutoPostBack="True" OnSelectedIndexChanged="branch_list_SelectedIndexChanged">
                </asp:DropDownList>
                    </td>
            <td style="width: 20%">
                         <asp:Label runat="server" Text="Territory" ID="txtterritory" Visible="true" Font-Bold="true"></asp:Label>
                  <asp:DropDownList ID="territory_list" CssClass="form-control" runat="server"  OnDataBound="territory_list_DataBound" Visible="true">
                </asp:DropDownList>
                    </td>
           <td class="modal-sm" style="width: 20%">
               <asp:Label runat="server" Text="Start Date" ID="lblstartdate" Visible="true" Font-Bold="true"></asp:Label>
                           <asp:TextBox ID="txtfromdatesrc" runat="server" CssClass="form-control" Width="150" Height="20" ></asp:TextBox>
                          </td> 
                           <td class="modal-sm" style="width: 20%">
                               <asp:Label runat="server" Text="End Date" ID="lblenddate" Visible="true" Font-Bold="true"></asp:Label>
                           <asp:TextBox ID="txttodatesrc" runat="server" CssClass="form-control" Width="150" Height="20"></asp:TextBox>
                           </td>
                                                                  
                          </tr>
                          </table>
                   <center>
              <asp:Button ID="balance" Width="150" Height="25" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="balance_Click" />
          </center>

               
                  
           
                   </fieldset>                                                      
              
            </div>
                       <br />
                                <div class="col-sm-12 home card" id="balanceviewdisplay" runat="server" visible="false">

                   <%--<h5 class="inline">View Fault Details</h5>--%>
                   <p id='P1' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
                             <asp:GridView ID="gvbalance" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False"
                                  OnRowDataBound="gvbalance_RowDataBound" 
                                 onselectedindexchanging="gvbalance_SelectedIndexChanging"
                                  onselectedindexchanged="gvbalance_SelectedIndexChanged"                                 
                                 >
             <Columns>            

   
            <asp:BoundField DataField="No" HeaderText="No" />
          <asp:BoundField DataField="jobno" HeaderText="Jobno" />
                    <asp:BoundField  DataField="section" HeaderText="Section" />
                 <asp:BoundField  DataField="CallLogDate" HeaderText="CallLogDate" /> 
       
           
                         <asp:BoundField  DataField="fault" HeaderText="fault" />
                  <asp:BoundField  DataField="Severity" HeaderText="Severity" />
                  <asp:BoundField  DataField="propertydamage" HeaderText="propertydamage" />
                  <asp:BoundField  DataField="status" HeaderText="status" />    
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
   

     
           <asp:Label runat="server" Text="0" id="lblreport" Visible="false"></asp:Label>
          <asp:Label runat="server" Text="0" id="lblstatus" Visible="false"></asp:Label>
    </form>

    <br /><br />
        
	</div>
       
  
</div>

</asp:Content>
