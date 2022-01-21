<%@ Page Title="Billing Periods" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="BillingPeriods.aspx.cs" Inherits="TraceBilling.BillingPeriods" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">

          <br />
          <div><h3>BILLING PERIOD SETUP</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        <br />
        <div class="form-group col-sm-12">
          <%--  <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">--%>
         <div id="periodsetting" runat="server" >
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Add Billing period</legend>
            <table>
                
                <tr>
                    <td style="width: 50%">
                        <label>Area</label>
           <asp:DropDownList ID="ddloperationarea" 
                                    DataTextField="operationAreaName"
                                     DataValueField="operationId" 
                                    CssClass="form-control" runat="server"
                                    OnDataBound="ddloperationarea_DataBound" Visible="true"
                   
                             >
                        </asp:DropDownList>
                    </td>
                </tr>
                
                   <tr>
                    <td style="width: 50%">
                        <label>Start Date</label>
            <asp:TextBox ID="txttartdate" runat="server" BackColor="LightGreen" CssClass="startdate"
                                                                Font-Bold="True" ForeColor="Maroon" Width="50%"></asp:TextBox>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 50%">
                        <label>End Date</label>
            <asp:TextBox ID="txtenddate" runat="server" BackColor="LightGreen" CssClass="startdate"
                                                                Font-Bold="True" ForeColor="Maroon" Width="50%"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <td style="width: 50%">
                                  
                    </td>
                </tr>
            <tr>
                    <td style="width: 50%">
                         <label></label>
             <asp:Button ID="btnSave" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Add" onclick="btnSave_Click" />
                    </td>
                </tr>
               
            </table>
                   
                   </fieldset>
               <br />
               
            </div>
             <div id="qndisplay" runat="server" visible="false">
                  <table style="width: 100%">
                            <tr>
                                <td style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td class="InterFaceTableLeftRowUp" style="width: 100%; text-align: center">
                                    <asp:Label ID="lblQn" runat="server" Font-Bold="True" ForeColor="Maroon" Text="."></asp:Label>
                                    <asp:Button    ID="btnYes" runat="server" Font-Bold="True" OnClick="btnYes_Click" Text="Yes"
                                        Width="150px" CssClass="btn-primary"/>&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnNo" runat="server" Font-Bold="True" OnClick="btnNo_Click"
                                            Text="No" Width="150px"  CssClass="btn-primary"/></td>
                            </tr>
                            <tr>
                                <td style="width: 100px">
                                </td>
                            </tr>
                        </table>
             </div>
             
       </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View Billing period logs</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
            
                             <asp:GridView ID="gv_billingperiod" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" OnRowCommand="gv_billingperiod_OnRowCommand"
                                  OnRowDataBound="gv_billingperiod_RowDataBound" OnSelectedIndexChanged="gv_billingperiod_SelectedIndexChanged"                                 
                                 >
             <Columns>
 
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
                 <asp:BoundField DataField="code" HeaderText="Code"  NullDisplayText="-"/>
                 <asp:BoundField DataField="period" HeaderText="Period" NullDisplayText="-" />
              <asp:BoundField DataField="cycle" HeaderText="Cycle" NullDisplayText="-" /> 
 
             <asp:BoundField DataField="startDate" HeaderText="Start Date" NullDisplayText="-" /> 
                 <asp:BoundField DataField="endDate" HeaderText="End Date" NullDisplayText="-" /> 
             <asp:BoundField DataField="openDate" HeaderText="Open Date" NullDisplayText="-"/> 
              <asp:BoundField DataField="closeDate" HeaderText="Close Date" NullDisplayText="-"/> 
                 <asp:BoundField DataField="closed" HeaderText="Is Closed" NullDisplayText="-"/> 
              <asp:BoundField DataField="closedby" HeaderText="Closed By" NullDisplayText="-"/>                      
                        
            

                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Action
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="EditButton"
                                runat="server"
                                CommandName="RowEdit" 
                    CommandArgument='<%#Eval("code") %>'
                               
                                Text="Close" />
                 
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
                 <asp:Label ID="lblCode" runat="server" Text="0" Visible="False"></asp:Label>   
        
        
    </form>

    <br /><br />
        
	</div>
 
</div>
    <script type="text/javascript">
        $(function () {
            $(".startdate").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
        </script>
</asp:Content>


