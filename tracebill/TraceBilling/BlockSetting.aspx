<%@ Page Title="Block Mapping" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="BlockSetting.aspx.cs" Inherits="TraceBilling.BlockSetting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>BLOCK OPERATIONAL SETUP</h3></div>
    
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
    <legend class="w-auto">&nbsp;Block map Setup</legend>
            <table>
                <tr><td style="width: 50%">
                         <asp:Label runat="server" Text="Operation Area" ID="Label1" Visible="true" Font-Bold="true"></asp:Label>
                  <asp:Label ID="l9x" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                 </td>
                     <td style="width: 50%">
                          <asp:DropDownList ID="ddloperationarea" 
                                    DataTextField="operationAreaName"
                                     DataValueField="operationId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddloperationarea_DataBound" Visible="true" AutoPostBack="true"
                              OnSelectedIndexChanged="ddloperationarea_SelectedIndexChanged">
                        </asp:DropDownList>
                     </td>
                    </tr>
                <tr>
                    <td style="width: 50%">
                        <label>Branch</label>

                 </td>
                    <td style="width: 50%">
             <asp:DropDownList ID="branch_list" CssClass="form-control" runat="server"  OnDataBound="branch_list_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
                </tr>
                   <tr>
                       <td style="width: 50%">
                        <label>Block Number</label>

                 </td>
                    <td style="width: 50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtblock" placeholder="Enter block" />
                    </td>
                </tr>
                  <tr>
                      <td style="width: 50%">
                        <label>Connection Number</label>
                 </td>
                    <td style="width: 50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtconnection" placeholder="Enter Connection" />
                    </td>
                </tr>
               
             <tr>
                 <td style="width: 50%">
                                   <asp:Label runat="server" Text="Is Active" ID="lblactive" Font-Bold="true" ></asp:Label>
                 </td>
                    <td style="width: 50%">
                            <asp:CheckBox ID="chkActive" runat="server" Font-Bold="True" Text="." />
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
         <label for="service">Status</label>
                 </td>
                    <td style="width: 50%">
           <asp:RadioButtonList ID="rtnblockstatus" runat="server" RepeatDirection="Horizontal" Width="80%"   >
                        <asp:ListItem Value="1">Permanent</asp:ListItem>
                        <asp:ListItem Value="2">Provisional</asp:ListItem>                        
                   </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                                  
                    </td>
                </tr>
            <tr>
                    <td style="width: 50%">
                         <label></label>
             <asp:Button ID="btnSave"  CssClass="btn-primary"
                                    runat="server" Text="Add" onclick="btnSave_Click" />
                    </td>
                </tr>
               
            </table>
                   
                   </fieldset>
               <br />
                             
                 <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
          <asp:Label ID="lblblockcode" runat="server" Text="0" Visible="False"></asp:Label>
            </div>
             
       </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View Block Settings</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
            
                             <asp:GridView ID="gv_blocksettings" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" OnRowCommand="gv_blocksettings_OnRowCommand"
                                  OnRowDataBound="gv_blocksettings_RowDataBound" OnSelectedIndexChanged="gv_blocksettings_SelectedIndexChanged"                                 
                                 >
             <Columns>
 
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
                 <asp:BoundField DataField="BlockID" HeaderText="blockId" Visible="false" NullDisplayText="-"/>
                 <asp:BoundField DataField="BlockNumber" HeaderText="BlockNumber" NullDisplayText="-" /> 
             <asp:BoundField DataField="ConnectionNumber" HeaderText="ConnectionNumber" NullDisplayText="-" /> 
<%--                 <asp:BoundField DataField="countryName" HeaderText="Country" NullDisplayText="-" /> --%>
             <asp:BoundField DataField="areaName" HeaderText="Area" NullDisplayText="-"/> 
              <asp:BoundField DataField="BranchName" HeaderText="Branch" NullDisplayText="-"/> 
              <asp:BoundField DataField="CreatedBy" HeaderText="Created By" NullDisplayText="-"/>                      
     
                  <asp:BoundField DataField="Active" HeaderText="Is Active" NullDisplayText="-" /> 
            <asp:BoundField DataField="status" HeaderText="Status" NullDisplayText="-"/>    
                 
            

                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Modify
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="EditButton"
                                runat="server"
                                CommandName="RowEdit" 
                    CommandArgument='<%#Eval("BlockID") %>'
                               
                                Text="Edit" />
                 
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
                   
        
    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>

