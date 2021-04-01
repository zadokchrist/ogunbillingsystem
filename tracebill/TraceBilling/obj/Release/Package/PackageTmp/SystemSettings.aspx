<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SystemSettings.aspx.cs" Inherits="TraceBilling.SystemSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>COUNTRY OPERATIONAL SETUP</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        <br />
        <div class="form-group col-sm-12">
          <%--  <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">--%>
         <div id="countrysetting" runat="server" >
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Global Location Setup</legend>
            <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Country Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcountry" placeholder="Enter Country" />
                    </td>
                </tr>
                  <tr>
                    <td style="width: 502px">
                        <label>Country Code</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcountrycode" placeholder="Enter CountryCode" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>VAT % applicable</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtvat" placeholder="Enter vat" />
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                         <label>Currency applicable</label>
            <asp:DropDownList ID="currency_list" CssClass="form-control" runat="server"  OnDataBound="currency_list_DataBound"  >
                
                </asp:DropDownList>
                    </td>
                </tr>
            
                <tr>
                    <td style="width: 502px">
                                   <asp:Label runat="server" Text="Is Active" ID="lblactive" Font-Bold="true" ></asp:Label>
                            <asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="." />
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                                  
                    </td>
                </tr>
            <tr>
                    <td style="width: 502px">
                         <label></label>
             <asp:Button ID="btnSave" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Save" onclick="btnSave_Click" />
                    </td>
                </tr>
               
            </table>
                   
                   </fieldset>
               <br />
                             
                 <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblcountrycode" runat="server" Text="0" Visible="False"></asp:Label>
            </div>
             
       </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View Country Settings</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
            
                             <asp:GridView ID="gv_countrysettings" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" OnRowCommand="gv_countrysettings_OnRowCommand"
                                  OnRowDataBound="gv_countrysettings_RowDataBound"                                 
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
                 <asp:BoundField DataField="countryId" HeaderText="countryId" Visible="false" NullDisplayText="-"/>
                 <asp:BoundField DataField="countryName" HeaderText="Country" NullDisplayText="-" /> 
             <asp:BoundField DataField="countryCode" HeaderText="Country Code" NullDisplayText="-"/> 
             
             <asp:BoundField DataField="vatrate" HeaderText="VAT Rate" NullDisplayText="-" /> 
             <asp:BoundField DataField="currency" HeaderText="Currency" NullDisplayText="-" /> 
            
             
                
      
                  <asp:BoundField DataField="Active" HeaderText="Is Active" NullDisplayText="-" /> 
            
                 
            
<%--                 <asp:ButtonField ButtonType="Button" CommandName="btnSelect" HeaderText="Select"
            Text="Select" ItemStyle-ForeColor="Blue"/>
                --%>
                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Modify
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="EditButton"
                                runat="server"
                                CommandName="RowEdit" 
                    CommandArgument='<%#Eval("countryId") %>'
                               
                                Text="Edit" />
                 
            </ItemTemplate>
                      </asp:TemplateField>
                  <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Region
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="AddRegion"
                                runat="server"
                                CommandName="AddRegion" 
                    CommandArgument='<%#Eval("countryId") %>'
                               
                                Text="View/Add" />
                 
            </ItemTemplate>
                      </asp:TemplateField>
                  <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        State
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="AddState"
                                runat="server"
                                CommandName="AddState" 
                    CommandArgument='<%#Eval("countryId") %>'
                               
                                Text="View/Add" />
                 
            </ItemTemplate>
                      </asp:TemplateField>
                    
        <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Zones/Area
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="AddZone"
                                runat="server"
                                CommandName="AddZone" 
                    CommandArgument='<%#Eval("countryId") %>'
                               
                                Text="View/Add" />
                 
            </ItemTemplate>
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
            <div id="addregion" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;View and Add Region</legend>
                 
                   <br< />
                   <div id="div3" runat="server" visible="true">
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <p>this is region setting</p>
                           
                           </div>
                           
                       
                   </div>
           
                   </fieldset>
               
               <br />
                
              
              
            </div>
                          
                
       </div>
            <div id="addstate" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;View and Add State</legend>
                 
                   <br< />
                   <div id="div2" runat="server" visible="true">
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <p>this is state setting</p>
                           
                           </div>
                           
                       
                   </div>
           
                   </fieldset>
               
               <br />
                
              
              
            </div>
                          
                
       </div>
            <div id="addzone" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;View and Add Zone</legend>
                 
                   <br< />
                   <div id="div4" runat="server" visible="true">
                       <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
            <p>this is zone setting</p>
                           
                           </div>
                           
                       
                   </div>
           
                   </fieldset>
               
               <br />
                
              
              
            </div>
                          
                
       </div>

       
        
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
