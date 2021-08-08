<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="TariffManagement.aspx.cs" Inherits="TraceBilling.TariffManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>TARIFF MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
               <div>
                 
                   <table width="100%">
    <tr>
    <%--<th>New</th>--%>
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
          
                          <td class="modal-sm" style="width: 236px" >
                      
            <asp:DropDownList ID="country_list" CssClass="form-control" runat="server"  OnDataBound="country_list_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list_SelectedIndexChanged" Width="235px">
                </asp:DropDownList>
                    </td>
             
                           
             <td class="datepicker-inline" style="width: 226px" >
                      
            <asp:DropDownList ID="area_list" CssClass="form-control" runat="server"  OnDataBound="area_list_DataBound" Visible="true" OnSelectedIndexChanged="area_list_SelectedIndexChanged" >
                
                </asp:DropDownList>
                    </td>
                
                 
                          <td>
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button3_Click" />
                      
           
                          </td>
                                                 
                          </tr>
                          </table>
          </div>
            </center>

        <br />
         <hr />
       
       <div id="tariffschedule" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Tariff Settings</legend>
                 
                   <br< />
                   <div id="tariffdisplay" runat="server" visible="true">
                <%-- <p>This is tariff setting</p>--%>
                        <asp:GridView ID="gv_tariffview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" 
                                 OnRowCommand="gv_tariffview_RowCommand"
                                  OnRowDataBound="gv_tariffview_RowDataBound"   
                                 onselectedindexchanging="gv_tariffview_SelectedIndexChanging"
                                  onselectedindexchanged="gv_tariffview_SelectedIndexChanged"                                                             
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
                  <asp:BoundField DataField="area" HeaderText="Area" NullDisplayText="-" /> 
             <asp:BoundField DataField="consumptionCategory" HeaderText="Category" NullDisplayText="-"/> 
               <asp:BoundField DataField="tariffCode" HeaderText="Tariff Code" NullDisplayText="-" /> 

             <asp:BoundField DataField="fixed" HeaderText="Fixed" NullDisplayText="-" /> 
             <asp:BoundField DataField="monthly" HeaderText="Monthly" NullDisplayText="-" /> 
            
                
                
                 <asp:BoundField DataField="monthCharge" HeaderText="Monthly Charge" NullDisplayText="-" /> 
             <asp:BoundField DataField="unitOfMeasure" HeaderText="Unit of Measure" NullDisplayText="-"/>
              
              <asp:BoundField DataField="shortCode" HeaderText="Short Code" NullDisplayText="-" /> 

    
                            
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
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               
       <div id="generaltariffschedule" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Tariff Settings</legend>
                 
                   <br< />
                   <div id="generaltariffdisplay" runat="server" visible="true">
                <%-- <p>This is tariff setting</p>--%>
                        <asp:GridView ID="gv_tariffviewgn" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" 
                                 OnRowCommand="gv_tariffviewgn_RowCommand"
                                  OnRowDataBound="gv_tariffviewgn_RowDataBound"   
                                 onselectedindexchanging="gv_tariffviewgn_SelectedIndexChanging"
                                  onselectedindexchanged="gv_tariffviewgn_SelectedIndexChanged"                                                             
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
                  <asp:BoundField DataField="tarrifCode" HeaderText="Tariff Code" NullDisplayText="-" /> 
             <asp:BoundField DataField="tarrifName" HeaderText="Tarrif Name" NullDisplayText="-"/> 
               <asp:BoundField DataField="className" HeaderText="Class" NullDisplayText="-" /> 

             <asp:BoundField DataField="amount" HeaderText="Main Charge" NullDisplayText="-" /> 
             <asp:BoundField DataField="notch1" HeaderText="Step1 charge" NullDisplayText="-" /> 
                
                 <asp:BoundField DataField="notch2" HeaderText="Step2 charge" NullDisplayText="-" /> 
             <asp:BoundField DataField="startDate" HeaderText="Start Date" NullDisplayText="-" Visible="false"/>
              <asp:BoundField DataField="endDate" HeaderText="End Date" NullDisplayText="-" Visible="false"/>
              <asp:BoundField DataField="active" HeaderText="Active" NullDisplayText="-" /> 
                            
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
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

         
        
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>

