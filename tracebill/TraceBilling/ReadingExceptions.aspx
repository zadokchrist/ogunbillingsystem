<%@ Page Title="Reading Anormalies" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ReadingExceptions.aspx.cs" Inherits="TraceBilling.ReadingExceptions" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
           <ajaxToolkit:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></ajaxToolkit:ToolkitScriptManager>          

          <br />
          <div><h3>READING EXCEPTIONS AND ANOMALIES</h3></div>
    
      
          <div class=" col-sm-12">
                <div class="col-sm-12"> <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
          
             </div>     
             
  
       
          <div class="col-sm-3">Operation Area
               <asp:DropDownList ID="ddloperationarea" 
                                    DataTextField="operationAreaName"
                                     DataValueField="operationId" 
                                    CssClass="form-control" runat="server"
                                    OnDataBound="ddloperationarea_DataBound" Visible="true"
                             >
                        </asp:DropDownList>
          </div>
              <div class="col-sm-3">Branch
               <asp:DropDownList ID="ddlbranch" 
                                    DataTextField="BranchName"
                                     DataValueField="BranchId" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlbranch_DataBound" Visible="true">
                        </asp:DropDownList>
          </div>
               
          <div class="col-sm-3">BlockMap
               <asp:DropDownList ID="ddlblock" 
                                    DataTextField="blockNumber"
                                     DataValueField="blockID" 
                                    CssClass="form-control" runat="server"
                                     OnDataBound="ddlblock_DataBound" Visible="true">
                        </asp:DropDownList>
          </div>
          <div class="col-sm-3">
             Period 
              <asp:TextBox ID="txtsearch" 
                               runat="server" CssClass="form-control" ></asp:TextBox>
         </div>
              <div class="col-sm-3">
              Options 
             <asp:RadioButtonList ID="exception_list" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true"
                 OnSelectedIndexChanged="exception_list_SelectedIndexChanged">                       
                   </asp:RadioButtonList>
         </div>
        <div class="col-sm-3">  
        
                          <asp:Button ID="Button3" Width="150" Height="40" CssClass="btn-primary round_btn form-control"
                                    runat="server" Text="Search" onclick="Button3_Click"  />
                           
           </div>
      <%--   <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="253px" Height="40px" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to application view list" onclick="btnReturn_Click" />
          </div>--%>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View Exception Logs</h5>
             
              
<%--                             <asp:GridView ID="gv_applicationview" runat="server" 
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
            
                
                
                 <asp:BoundField DataField="statusName" HeaderText="Status" NullDisplayText="-" /> 
             <asp:BoundField DataField="typeName" HeaderText="CustomerType" NullDisplayText="-">
              
                 </asp:BoundField> 
   <asp:BoundField DataField="className" HeaderText="className" NullDisplayText="-" /> 
               
                  <asp:BoundField DataField="areaName" HeaderText="Area" NullDisplayText="-" /> 
                  <asp:BoundField DataField="areaId" HeaderText="AreaID" NullDisplayText="-" Visible="false" />
             
                 <asp:BoundField DataField="assignedTo" HeaderText="AssignedTo" NullDisplayText="-" /> 
            
          <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Foam
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="PrintButton"
                                runat="server"
                                CommandName="RowPrint" 
                     CommandArgument='<%#Eval("applicationNumber") + ";" +Eval("areaId") %>'
                               
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
                 <asp:TemplateField ShowHeader="True">
                      <HeaderTemplate>
                        Panel
                    </HeaderTemplate>
            <ItemTemplate>
                <asp:LinkButton ID="viewButtonPanel"
                                runat="server"
                                CommandName="RowPanel" 
                    CommandArgument='<%#Eval("applicationNumber") + ";" +Eval("fullName") + ";" +Eval("ApplicationID") %>'
                               
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
             </asp:GridView>--%>

               
        
                      
             
            </div>
                             <div id="zerodisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Zero Consumption</legend>
                 
                   <br< />
                 <p>This is zero handling</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                                           <div id="lowdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Low Consumption</legend>
                 
                   <br< />
                 <p>This is low exception</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

                                           <div id="highdisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;High Consumption</legend>
                 
                   <br< />
                 <p>This is high eceptiopn</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                                           <div id="negativedisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Negative Consumption</legend>
                 
                   <br< />
                 <p>This is negative exception</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                                           <div id="unreaddisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Unreads</legend>
                 
                   <br< />
                 <p>This is unread handling</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                                           <div id="estimatedisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Estimated Consumption</legend>
                 
                   <br< />
                 <p>This is estimated handling</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                                           <div id="suppressedisplay" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Suppressed but recording</legend>
                 
                   <br< />
                 <p>This is suppressed handling</p>
                   
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>


          <br />
      
                               <asp:Label ID="lblApplicationCode" runat="server" Text="0" Visible="False"></asp:Label>
                              <asp:Label ID="lblApplicationId" runat="server" Text="0" Visible="False"></asp:Label>

    </form>

    <br /><br />
        
	</div>
 
</div>
</asp:Content>
