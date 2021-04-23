<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ViewApplications.aspx.cs" Inherits="TraceBilling.ViewApplications" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>VIEW APPLICATIONS INITIATED</h3></div>
    
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
           <th class="modal-sm" style="width: 250px">Application Name</th>
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
                           <asp:TextBox ID="txtapplicationname" runat="server" CssClass="form-control" Width="217px" ></asp:TextBox>
                     
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
              <asp:Button ID="btnreturn" Width="253px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to application view list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View application Logs</h5>
             
              
                             <asp:GridView ID="gv_applicationview" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" 
                                 OnRowCommand="gv_applicationview_RowCommand"
                                  OnRowDataBound="gv_applicationview_RowDataBound"   
                                 onselectedindexchanging="gv_applicationview_SelectedIndexChanging"
                                  onselectedindexchanged="gv_applicationview_SelectedIndexChanged"                                                             
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
             <asp:BoundField DataField="applicationNumber" HeaderText="Application#" NullDisplayText="-"/> 
               <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" NullDisplayText="-" Visible="false"/> 

             <asp:BoundField DataField="fullName" HeaderText="Name" NullDisplayText="-" /> 
             <asp:BoundField DataField="address" HeaderText="Address" NullDisplayText="-" /> 
            
            <%-- <asp:BoundField DataField="contact" HeaderText="Contact" NullDisplayText="-" /> --%>
                
                
                 <asp:BoundField DataField="statusName" HeaderText="Status" NullDisplayText="-" /> 
             <asp:BoundField DataField="typeName" HeaderText="CustomerType" NullDisplayText="-">
              
                 </asp:BoundField> 
   <asp:BoundField DataField="className" HeaderText="className" NullDisplayText="-" /> 
                 <%-- <asp:BoundField DataField="serviceName" HeaderText="serviceName" NullDisplayText="-" /> --%>
               <asp:BoundField DataField="countryName" HeaderText="Country" NullDisplayText="-" /> 
                  <asp:BoundField DataField="areaName" HeaderText="Area" NullDisplayText="-" /> 
              <%--   <asp:BoundField DataField="countryId" HeaderText="CountryID" NullDisplayText="-"  Visible="false"/> 
                  <asp:BoundField DataField="areaId" HeaderText="AreaID" NullDisplayText="-" Visible="false" /> --%>
                 <asp:BoundField DataField="assignedTo" HeaderText="AssignedTo" NullDisplayText="-" /> 
             <%--    <asp:ButtonField ButtonType="Button" CommandName="btnPrint" HeaderText="Foam"
            Text="Print" ItemStyle-ForeColor="Green" />
                <asp:CommandField ShowSelectButton="true" ButtonType="Button" SelectText="View"   HeaderText="Details"/>--%>
          <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Foam
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="PrintButton"
                                runat="server"
                                CommandName="RowPrint" 
                    CommandArgument='<%#Eval("applicationNumber") %>'
                               
                                Text="Print" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
                       <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Logs
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="viewButton"
                                runat="server"
                                CommandName="RowView" 
                    CommandArgument='<%#Eval("applicationNumber") %>'
                               
                                Text="View" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>          
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
                  <%--<center>--%>
               <div id="statuslogdisplay" style="width:800px; margin:0 auto;  text-align: center;"  runat="server" visible="false">
                <asp:Label runat="server" Text="View Application status logs" id="lbllogmsg" Visible="true" Font-Bold="true" ForeColor="Blue"></asp:Label>
               <asp:GridView ID="gvlogdisplay" runat="server" 
                       CssClass="grid-text" CellPadding="5" 
                              ForeColor="#333333" GridLines="None" Width="92%"
                                  AutoGenerateColumns="False"
                              
                   >
             <Columns>                
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                <asp:BoundField DataField="applicationNumber" HeaderText="application#" NullDisplayText="-" />   
             <asp:BoundField DataField="statusName" HeaderText="Status"  NullDisplayText="-"/>                
             <asp:BoundField DataField="LoggedBy" HeaderText="Logged By" NullDisplayText="-"/> 
             <asp:BoundField DataField="LogDate" HeaderText="log Date" NullDisplayText="-" />                         

        
              
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
          <%--</center>--%>
            <br />
            <center>
               <asp:Button ID="btnreturn3" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturn3_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
               
          </center>
                  </div>
        
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
