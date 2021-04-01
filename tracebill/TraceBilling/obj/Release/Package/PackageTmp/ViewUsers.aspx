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
          <%--  <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">--%>
                          <table width="100%">
    <tr>
    <th></th>
           <th>From Date</th>
        <th>To Date</th>
         <th>Country</th>
        <th></th>
        </tr>
        <tr>
        <td>
        
        <%--<asp:ImageButton ID="ImageButtonedit"  ImageAlign="AbsMiddle"
         AlternateText="search" runat="server" ImageUrl="assets/dist/img/add.png" 
         CssClass="btn-default inline" Width="50" Height="40" OnClick="ImageButton1_Click" />--%>
        </td>
           <td>
                           <asp:TextBox ID="txtfromdatesrc" runat="server" CssClass="form-control" Width="150" ></asp:TextBox>
                    
                          </td> 
                           <td>
                           <asp:TextBox ID="txttodatesrc" runat="server" CssClass="form-control" Width="150"></asp:TextBox>
                     
                           </td>
                            <th>
           
                                <asp:DropDownList ID="country_list" CssClass="form-control" runat="server" Width="216px"  OnDataBound="country_list_DataBound" Visible="true">
                        </asp:DropDownList>
                </th>
                 
                          <th>
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button3_Click" />
                           
           
                          </th>
                                                 
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
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False"
                                  OnRowDataBound="GridViewUser_RowDataBound" 
                                 onselectedindexchanging="GridViewUser_SelectedIndexChanging"
                                  onselectedindexchanged="GridViewUser_SelectedIndexChanged"
                                 OnRowCommand="GridViewUser_RowCommand"                               
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
             
             <asp:BoundField DataField="countryName" HeaderText="Country" NullDisplayText="-">             
                 </asp:BoundField> 

             <asp:BoundField DataField="areaName" HeaderText="Area" NullDisplayText="-" /> 
       
           
              <asp:BoundField DataField="AccStatus" HeaderText="Active" NullDisplayText="-"/>
                 
             <%--<asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="View"   HeaderText="Details"/>
            <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Edit"   HeaderText="Change" />
                 <asp:CommandField ShowSelectButton="true" ButtonType="Link" SelectText="Reset"   HeaderText="Password"/>
                 
            --%>
          <%-- <asp:CommandField
            ButtonType="Link"
            ShowEditButton="true"
            EditText="Edit"           
           
           
            ShowDeleteButton="true"
            DeleteText="Delete"
            />--%>
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
                <%-- <asp:TemplateField>
        <ItemTemplate>
            <asp:LinkButton ID="DeleteButton"
                            Text="Delete"
                            CommandName="RowDelete" 
                            runat="server" />
        </ItemTemplate>
    </asp:TemplateField>--%>
              
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
        
           <hr />
        
    </form>

    <br /><br />
        
	</div>
       
   
</div>
</asp:Content>
