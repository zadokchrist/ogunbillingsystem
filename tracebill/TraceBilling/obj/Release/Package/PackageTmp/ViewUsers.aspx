<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ViewUsers.aspx.cs" Inherits="TraceBilling.ViewUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <div><h3>VIEW SYSTEM USERS</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        
        <div class="form-group col-sm-12">
         

                           <table width="100%">
    <tr>
        <th>Search (name..)</th>  
        <th>Branch</th>       
         <th>Role</th>
        <th></th>
        </tr>
        <tr>
            <td>
         <asp:TextBox ID="txtsearch" 
                               runat="server" CssClass="form-control" Width="216px"></asp:TextBox>
            </td>
             <td>
           
                                <asp:DropDownList ID="ddlbranch"
                                      DataTextField="branchName"
                                     DataValueField="branchId" 
                                     CssClass="form-control" runat="server" Width="216px" 
                                     OnDataBound="ddlbranch_DataBound" Visible="true">
                        </asp:DropDownList>
                </td>
                            
             <td>
           
                                <asp:DropDownList ID="ddlrole"
                                      DataTextField="RoleName"
                                     DataValueField="RoleID" 
                                     CssClass="form-control" runat="server" Width="216px" 
                                     OnDataBound="ddlrole_DataBound" Visible="true">
                        </asp:DropDownList>
                </td>
            <td>
                  <asp:Button ID="Button1" Width="150" Height="40" CssClass="btn-primary round_btn"
                                    runat="server" Text="Search" onclick="Button3_Click" />
                               </td>          
                          </tr>
                          </table>
             
                          </div>
         <%-- <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to Job list" onclick="btnReturn_Click" />
          </div>--%>
            <div class="col-sm-12 home card" id="maindisplay" runat="server">

                   <h5 class="inline">View User log</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
                             <asp:GridView ID="GridViewUser" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                                 AllowPaging="true"
                                  PageSize="10"
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False"
                                  OnRowDataBound="GridViewUser_RowDataBound" 
                                 onselectedindexchanging="GridViewUser_SelectedIndexChanging"
                                  onselectedindexchanged="GridViewUser_SelectedIndexChanged"
                                 OnRowCommand="GridViewUser_RowCommand"   
                                 OnPageIndexChanging="GridViewUser_PageIndexChanging"                            
                                 >
             <Columns>
           
           <asp:BoundField DataField="No" HeaderText="No." NullDisplayText="-"/> 
                 <asp:BoundField DataField="userID" HeaderText="UserID" Visible="false" NullDisplayText="-"/> 
             <asp:BoundField DataField="userCode" HeaderText="UserCode" Visible="false" NullDisplayText="-"/> 
               
             <asp:BoundField DataField="userName" HeaderText="UserName" NullDisplayText="-"/> 
             <asp:BoundField DataField="fullName" HeaderText="FullName" NullDisplayText="-" /> 
             <asp:BoundField DataField="designation" HeaderText="Designation" NullDisplayText="-" /> 
                 <asp:BoundField DataField="contactNo1" HeaderText="Contact" NullDisplayText="-" />
            <asp:BoundField DataField="emailAddress" HeaderText="EmailAddress" NullDisplayText="-" /> 
              <asp:BoundField DataField="roleName" HeaderText="Role" NullDisplayText="-" /> 
             
                 
             <asp:BoundField DataField="areaName" HeaderText="Area" NullDisplayText="-" /> 
       
           
              <asp:BoundField DataField="AccStatus" HeaderText="Active" NullDisplayText="-"/>
              
                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Modify
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="EditButton"
                                runat="server"
                                CommandName="RowEdit" 
                    CommandArgument='<%#Eval("userCode") %>'
                               
                                Text="Edit" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
                 <asp:TemplateField ShowHeader="True">
                     <HeaderTemplate>
                        Enable
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="ChangeButton"
                                runat="server"
                   
                                CommandName="RowChange" 
                    
                      CommandArgument='<%#Eval("userID") + ";" +Eval("userName") + ";" +Eval("AccStatus")%>'         
                                Text="Change" />
                 
            </ItemTemplate>
                     <ItemStyle Width="5%" />
                 </asp:TemplateField>
                <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Password
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="ResetButton"
                                runat="server"
                                CommandName="RowReset" 
                    CommandArgument='<%#Eval("userID") + ";" +Eval("userName") + ";" +Eval("fullName") + ";" +Eval("emailAddress") %>'
                               
                                Text="Reset" />
                 
            </ItemTemplate>
                     <ItemStyle Width="10%" />
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

               
                      
             
            </div>

          <br />
        
           <hr />
        
    </form>

    <br /><br />
        
	</div>
       
   
</div>
</asp:Content>
