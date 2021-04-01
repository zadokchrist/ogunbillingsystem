<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SurveyJobs.aspx.cs" Inherits="TraceBilling.SurveyJobs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>SURVEY JOB MANAGEMENT</h3></div>
    
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
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to Job list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View survey Job Logs</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
                <div class="text-center"  runat="server" style="width: 891px">
                      <asp:Button ID="btngenerate" runat="server" Text="Generate JobCard" cssclass ="btn-primary" OnClick="btngenerate_Click" />
                </div>
                <div class="text-right"  runat="server" style="width: 914px">
                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True"
                            OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" />
                </div>
                             <asp:GridView ID="gv_surveyjobs" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" OnRowCommand="gv_surveyjobs_OnRowCommand"
                                  OnRowDataBound="gv_surveyjobs_RowDataBound"                                 
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
             <%--    <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" NullDisplayText="-" Visible="false"/> --%>
             <asp:BoundField DataField="applicationNumber" HeaderText="Application#" NullDisplayText="-"/> 
             
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
                 <asp:ButtonField ButtonType="Button" CommandName="btnJobCard" HeaderText="Job Card"
            Text="Print" ItemStyle-ForeColor="Green" />
                 <asp:ButtonField ButtonType="Button" CommandName="btnDetails" HeaderText="Assign"
            Text="View" ItemStyle-ForeColor="Blue"/>
                
             <asp:TemplateField HeaderText="Select" ItemStyle-Width="150">           
                <ItemTemplate>
                    <asp:CheckBox ID="chkCtrl" runat="server" />
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
       <div id="assignsurvey" runat="server" visible="false">
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Survey Job Assignment</legend>
            <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Application Code</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtappcode" placeholder="Enter Code" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Application Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtname" placeholder="Enter name" ReadOnly="true"/>
                    </td>
                </tr>
           
                <tr>
                    <td style="width: 502px">
                                   <asp:Label runat="server" Text="Surveyor/Plumber" ID="surveyor" Visible="true"></asp:Label>
                         <asp:DropDownList ID="survey_list" CssClass="form-control" runat="server" AutoPostBack="True" OnDataBound="survey_list_DataBound"
                           Visible="true">
                       </asp:DropDownList>
                    </td>
                </tr>
                 <tr>
                    <td style="width: 502px">
                          <label>Date Of Instruction:</label>

                <div class="input-group date">
                  <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>
                    <%--<asp:TextBox runat="server" class="form-control pull-right" id="datepicker" ReadOnly="true" />--%>
                    <asp:TextBox ID="txtinstructionDate" CssClass="instructiondate"  runat="server"></asp:TextBox>
                    </div>
                    </td>
                </tr>
               
            </table>
                   </fieldset>
               <br />
                 <asp:Button ID="btnreturn2" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturn2_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnAssign" runat="server" Text="Save" cssclass ="btn-primary" OnClick="btnAssign_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp;              
                 <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
            
            </div>
       </div>
        
    </form>

    <br /><br />
        
	</div>
 
</div>
    <script type="text/javascript">
        $(function () {
            $(".instructiondate").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
    </script>
</asp:Content>
