<%@ Page Title="Area Settings" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ManageAreas.aspx.cs" Inherits="TraceBilling.ManageAreas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>OPERATION AREA MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>
                          <div id="areadisplay" runat="server"  align="left">
              <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                   <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Manage Areas</legend>
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Add New Area" Font-Bold="true" ></asp:Label>
                    </td>
                </tr>
              
                <tr>
                    <td style="width:50%"><label for="areaname">Area Name</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtarea" placeholder="Enter areaName"/>

                    </td>
                </tr>
                 <tr>
                    <td style="width:50%"><label for="code">Area Code</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcode" placeholder="Enter areaCode"/>

                    </td>
                </tr>
                 <tr>
                    <td style="width:50%"><label for="alias">Area Alias</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtalias" placeholder="Enter areaAlias"/>

                    </td>
                </tr>
               <tr>
                    <td style="width:50%"><label for="txtactive">Active</label>
                        </td>
                    <td style="width:50%">
                        <asp:checkbox runat="server" CssClass="form-control" ID="chkarea"></asp:checkbox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         
               <asp:Button ID="btnAddArea" runat="server" Text="Add" cssclass ="btn-primary" OnClick="btnAddArea_Click" />
          
                    </td>
                </tr>
                </table>
                     <h5 class="inline">View Area log</h5>
                   <p id='P4' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
                             <asp:GridView ID="GridViewIssue" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False"
                                  OnRowDataBound="GridViewIssue_RowDataBound" 
                                 OnRowCommand="GridViewIssue_RowCommand"
                                 onselectedindexchanging="GridViewIssue_SelectedIndexChanging"
                                  onselectedindexchanged="GridViewIssue_SelectedIndexChanged">
             <Columns>
                
<%--           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> --%>
             
   
             <asp:BoundField DataField="operationId" HeaderText="operationId" NullDisplayText="-" Visible="false"/>             
             <asp:BoundField DataField="operationAreaName" HeaderText="AreaName" NullDisplayText="-"/>             
            <asp:BoundField DataField="operationCode" HeaderText="Code" NullDisplayText="-"/>             
           <asp:BoundField DataField="operationAlias" HeaderText="Alias" NullDisplayText="-"/>             

                 <asp:BoundField DataField="Isactive" HeaderText="Active" NullDisplayText="-"/>             

<%--            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Edit"   HeaderText="Details"/>
    <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Remove"   HeaderText="Details"/>--%>
                   <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Modify
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="EditButton"
                                runat="server"
                                CommandName="RowEdit" 
                    CommandArgument='<%#Eval("operationId") %>'
                               
                                Text="Edit" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
                 
                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Delete
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="DeleteButton"
                                runat="server"
                                CommandName="RowDelete" 
                    CommandArgument='<%#Eval("operationId") %>'
                               
                                Text="Remove" />
                 
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
                        </fieldset>    
                  </div>
          </div>
  <asp:Label runat="server" Text="0" id="lblarea" Visible="false"></asp:Label>

        
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>

