<%@ Page Title="Company Profile" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ManageProfile.aspx.cs" Inherits="TraceBilling.ManageProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>COMPANY PROFILE</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>
                          <div id="areadisplay" runat="server"  align="left">
              <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                   <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Manage Profile</legend>
            <table>
                <%--<tr>
                    <td>
                        <asp:Label runat="server" Text="Add New Area" Font-Bold="true" ></asp:Label>
                    </td>
                </tr>--%>
              
                <tr>
                    <td style="width:50%"><label for="areaname">Company Name</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtarea" placeholder="Enter areaName"/>

                    </td>
                </tr>
                 <tr>
                    <td style="width:50%"><label for="code">Physical Address</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtaddress" placeholder="Enter address"/>

                    </td>
                </tr>
                 <tr>
                    <td style="width:50%"><label for="alias">Toll Free line</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txttollfree" placeholder="Enter tollfree"/>

                    </td>
                </tr>
                <tr>
                    <td style="width:50%"><label for="alias">Alternative Contact</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtcontact" placeholder="Enter contact"/>

                    </td>
                </tr>
                <tr>
                    <td style="width:50%"><label for="alias">Email</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtmail" placeholder="Enter email"/>

                    </td>
                </tr>
                <tr>
                    <td style="width:50%"><label for="alias">Website</label>
                        </td>
                    <td style="width:50%">
            <asp:TextBox runat="server" CssClass="form-control" ID="txtweb" placeholder="Enter web"/>

                    </td>
                </tr>
              <%-- <tr>
                    <td style="width:50%"><label for="txtactive">Active</label>
                        </td>
                    <td style="width:50%">
                        <asp:checkbox runat="server" CssClass="form-control" ID="chkarea"></asp:checkbox>
                    </td>
                </tr>--%>
                <tr>
                    <td style="width: 502px">
                         
               <asp:Button ID="btnupdate" runat="server" Text="Update" cssclass ="btn-primary" OnClick="btnupdate_Click" />
          
                    </td>
                </tr>
                </table>
                       <br /><br />
                     <h5 class="inline">View company log</h5>
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
                
             
  
             <asp:BoundField DataField="companyId" HeaderText="companyId" NullDisplayText="-" Visible="false"/>             
             <asp:BoundField DataField="companyName" HeaderText="CompanyName" NullDisplayText="-"/>             
            <asp:BoundField DataField="physicalAddress" HeaderText="Address" NullDisplayText="-"/>             
           <asp:BoundField DataField="emailAddress" HeaderText="Email" NullDisplayText="-"/>             
             <asp:BoundField DataField="tollContact" HeaderText="Toll Free" NullDisplayText="-"/>             
             <asp:BoundField DataField="othercontact" HeaderText="Contact" NullDisplayText="-"/>             
             <asp:BoundField DataField="webAddress" HeaderText="Website" NullDisplayText="-"/>             


                   <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Modify
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="EditButton"
                                runat="server"
                                CommandName="RowEdit" 
                    CommandArgument='<%#Eval("companyId") %>'
                               
                                Text="Edit" />
                 
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


