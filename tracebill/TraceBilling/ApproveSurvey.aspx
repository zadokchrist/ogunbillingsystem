<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ApproveSurvey.aspx.cs" Inherits="TraceBilling.ApproveSurvey" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
                     <ajaxToolkit:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></ajaxToolkit:ToolkitScriptManager>          

              <div><h3>SURVEY REPORT APPROVAL</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        <br />
      <%--  <div class="form-group col-sm-12">
       
                          <table width="100%">
    <tr>
  
           <th class="modal-sm" style="width: 250px">Job Number</th>
         <th class="datepicker-inline" style="width: 226px">Operation Area</th>
        <th></th>
        </tr>
        <tr>
   
           <td class="modal-sm" style="width: 250px">
                           <asp:TextBox ID="txtjobnumber" runat="server" CssClass="form-control" Width="217px" ></asp:TextBox>
                     
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
                          </div>--%>
          <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to Job list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View survey report Logs</h5>
                   <p id='baltxt' class="inline" style="color:Green; font-size:14px;" runat="server"></p>
              <%--  <div class="text-center"  runat="server" style="width: 891px">
                      <asp:Button ID="btngenerate" runat="server" Text="Generate JobCard" cssclass ="btn-primary" OnClick="btngenerate_Click" />
                </div>--%>
              <%--  <div class="text-right"  runat="server" style="width: 914px">
                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" Font-Bold="True"
                            OnCheckedChanged="chkSelect_CheckedChanged" Text="Select All" />
                </div>--%>
                             <asp:GridView ID="gv_surveyjobs" runat="server" 
                       CssClass="grid-text" CellPadding="10" 
                              ForeColor="#333333" GridLines="None" Width="100%"
                                  AutoGenerateColumns="False" OnRowCommand="gv_surveyjobs_OnRowCommand"
                                  OnRowDataBound="gv_surveyjobs_RowDataBound"                                 
                                 >
             <Columns>
               
           <asp:BoundField DataField="No" HeaderText="No" NullDisplayText="-"/> 
             <%--    <asp:BoundField DataField="ApplicationID" HeaderText="ApplicationID" NullDisplayText="-" Visible="false"/> --%>
                 <asp:BoundField DataField="JobNumber" HeaderText="Job#" NullDisplayText="-" /> 
             <asp:BoundField DataField="ApplicationNumber" HeaderText="Application#" NullDisplayText="-"/> 
             
             <asp:BoundField DataField="ApplicantName" HeaderText="Name" NullDisplayText="-" /> 
<%--             <asp:BoundField DataField="Location" HeaderText="Address" NullDisplayText="-" /> --%>
            
                  <asp:BoundField DataField="Area" HeaderText="Area" NullDisplayText="-" /> 
              <%--   <asp:BoundField DataField="countryId" HeaderText="CountryID" NullDisplayText="-"  Visible="false"/> 
                  <asp:BoundField DataField="areaId" HeaderText="AreaID" NullDisplayText="-" Visible="false" /> --%>
                 <asp:BoundField DataField="AssignedTo" HeaderText="AssignedTo" NullDisplayText="-" /> 
               <%--  <asp:ButtonField ButtonType="Button" CommandName="btnJobCard" HeaderText="Job Card"
            Text="Print" ItemStyle-ForeColor="Green" />--%>
                 <asp:ButtonField ButtonType="link" CommandName="btnSelect" HeaderText="Select"
            Text="Select" ItemStyle-ForeColor="Blue"/>
                
       
                                 
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
       <div id="approvesurvey" runat="server" visible="false">
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
<%--    <legend class="w-auto">&nbsp;Survey Question Checks</legend>--%>
            <table>
                   <tr>
                    <td style="width: 50%">
                        <label>Application Number</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtappcode" placeholder="Enter Code" ReadOnly="true"/>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 50%">
                        <label>Job Number</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtjobno" placeholder="Enter jobnumber" ReadOnly="true"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                         <label>Application Name</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtname" placeholder="Enter name" ReadOnly="true"/>
                    </td>
                </tr>
            
               
            </table>
                   
               <br />
                             
                 <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
               <asp:Label ID="lblstatus" runat="server" Text="0" Visible="False"></asp:Label>
               <asp:Label ID="lblappcode" runat="server" Text="." Visible="False"></asp:Label>
          
            </div>
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <table>
                    <tr>
                    <td style="width: 50%">
                         <label>Operation Area</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtarea" placeholder="Enter area" ReadOnly="true"/>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 50%; height: 91px;">
                          <label>Date Of Survey:</label>

               <%-- <div class="input-group date">
                  <div class="input-group-addon">
                    <i class="fa fa-calendar"></i>
                  </div>
                    <asp:TextBox ID="txtsurveyDate" CssClass="surveydate"  runat="server"></asp:TextBox>
                    </div>--%>
 <asp:TextBox ID="txtsurveyDate" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="txtsurveyDate_CalendarExtender" runat="server" TargetControlID="txtsurveyDate" Format="dd/MM/yyyy" PopupPosition="TopLeft"/>
                    </td>
                </tr>
                <tr>
                    <td style="width: 50%">
                                   <asp:Label runat="server" Text="Survey Question Check List" ID="surveyqns" Visible="false"></asp:Label>
                           <asp:CheckBoxList ID="chkBoxRequired" runat="server" Font-Bold="True" RepeatDirection="Vertical" Visible="false"
                                                    Width="98%" style="font: menu" Font-Names="Arial Narrow">
                                                </asp:CheckBoxList>
                    </td>
                </tr>
            
               </table>
               </div>
             <div class="form-group col-sm-12 col-md-12 col-lg-12">
     <asp:Button ID="btnjobcard" runat="server" Text="Export JobCard" cssclass ="btn-primary" OnClick="btnjobcard_Click" />

                  <center>

                      <div id="surveydisplay" runat="server">
                           <asp:Label runat="server" Text="Survey Question Check List" ID="lblQnLst" Font-Bold="true" Visible="true"></asp:Label><br />
                  <asp:Label runat="server" Text="NB: Please tick list item and enter field  answer" ID="Label1" Font-Bold="true" Visible="true" ForeColor="Red"></asp:Label>

                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                        GridLines="Horizontal" HorizontalAlign="Center" 
                                         PageSize="30" Style="font: menu;
                                        text-align: justify" Width="80%">
                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                        <EditItemStyle BackColor="#999999" />
                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" ForeColor="#333333" />
                                        <Columns>
                                            
                                            <asp:BoundColumn DataField="questionId" HeaderText="QuestionID">
                                                <HeaderStyle Width="15%" />
                                            </asp:BoundColumn>
                                            
                                            <asp:BoundColumn DataField="question" HeaderText="Question">
                                                <HeaderStyle Width="30%" />
                                            </asp:BoundColumn>
                                            
                                             <asp:BoundColumn DataField="answer" HeaderText="Answer" Visible="false">
                                                <HeaderStyle Width="10%" />
                                             </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Field Answer"> 
                                            <ItemTemplate> <asp:TextBox ID="txtanswer"  runat="server" BackColor="Khaki" CssClass="InterfaceTextboxLongReadOnly"
                                            Font-Bold="True" ForeColor="Maroon" Width="40%" AutoPostBack="false" Text='<%# Eval("answer") %>'></asp:TextBox></ItemTemplate> 
                                            </asp:TemplateColumn> 
                                             <asp:TemplateColumn HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server"   Width="40px"  AutoPostBack="false"/>
                                    </ItemTemplate>
                                    <HeaderStyle Width="2%" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    </asp:DataGrid>
                   </div>
                      <br />
                      <div class="col-sm-3">
                          <asp:RadioButtonList ID="rtnAction" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true" OnSelectedIndexChanged="rtnAction_SelectedIndexChanged">
                        <asp:ListItem Value="1">Approve</asp:ListItem>
                        <asp:ListItem Value="2">Reject</asp:ListItem>
                   </asp:RadioButtonList>
                      </div>
<%--               <asp:Button ID="btnreturn2" runat="server" Text="Return" cssclass ="btn-primary" OnClick="btnreturn2_Click" />--%>
              <%--&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnApprove" runat="server" Text="Approve" cssclass ="btn-primary" OnClick="btnApprove_Click" style="height: 26px" />
              &nbsp;&nbsp;&nbsp;&nbsp;  
                <asp:Button ID="btnreject" runat="server" Text="Reject" cssclass ="btn-primary" OnClick="btnreject_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;--%>
          </center>
                  </div>
       </div>
                   <div id="approvecon" runat="server" visible="false">
           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Action required</legend>
            <table>
           
                <tr>
                    <td style="width: 502px">                        
                        <asp:Label ID="lblaction" runat="server" Text="." Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                         <label>Remark/Comment</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtremark" placeholder="Enter remark" TextMode="MultiLine" Height="51px" Width="385px" BackColor="LightGreen" ForeColor="Maroon"/>
                    </td>
                </tr>
                           
            </table>
                    <br />
              <asp:Button ID="btnsubmit" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnsubmit_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnapprovecancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btnapprovecancel_Click" />
                   </fieldset>
                       
            </div>
               </div>

    </form>

    <br /><br />
        
	</div>
 
</div>
     <script type="text/javascript">
        $(function () {
            $(".surveydate").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
    </script>
    </asp:Content>