<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FieldConnection.aspx.cs" Inherits="TraceBilling.FieldConnection" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>  

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <ajaxToolkit:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></ajaxToolkit:ToolkitScriptManager>          

  
          <div><h3>FIELD INSTALLATION MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
            </center>

        <br />
       <%-- <div class="form-group col-sm-12">
        
                          <table width="100%">
    <tr>
    
           <th class="modal-sm" style="width: 250px">Job Number</th>
        <th class="modal-sm" style="width: 236px">Country</th>
         <th class="datepicker-inline" style="width: 226px">Operation Area</th>
        <th></th>
        </tr>
        <tr>
    
           <td class="modal-sm" style="width: 250px">
                           <asp:TextBox ID="txtjobnumber" runat="server" CssClass="form-control" Width="217px" ></asp:TextBox>
                     
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
                          </div>--%>
       
          <div id="returnbtn" runat="server">
              <asp:Button ID="btnreturn" Width="150" Height="40" CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Return to Job list" onclick="btnReturn_Click" />
          </div>
            <div class="col-sm-12 home card" id="maindisplay" runat="server" >

                   <h5 class="inline">View connection invoice Logs</h5>
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
             <asp:BoundField DataField="Location" HeaderText="Address" NullDisplayText="-" /> 
            
             
                
      
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
          <div class="form-group col-sm-12 col-md-12 col-lg-12">
              <div id="btnlinks" runat="server" visible="false">
              <center>
             <%-- <asp:Button ID="btncustomer" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btncustomer_Click" Text="CUSTOMER DETAILS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;--%>
                                  
                                    <asp:Button ID="btnmaterials" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnmaterials_Click" Text="MATERIAL EXPENDITURE" cssclass ="btn-primary"
                                        Width="178px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btndocket" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btndocket_Click" Text="DOCKET INITIATION" cssclass ="btn-primary"
                                        Width="160px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
       <div id="connectioninvoice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               
               <fieldset class="panel panel-primary" runat="server" id="matdetails">
    <legend class="w-auto">&nbsp;Material Details</legend>
                    <div id="materialdisplay" runat="server">
                        <%-- <h3>materials implementation not available!!!</h3>--%>
                           <div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">
                               <center>
                                   <asp:Label runat="server" Text="Material Expenditure" Font-Bold="true" ></asp:Label>
                               </center>
            <table>
                <tr>
                    <td style="width: 50%">
                       
                 
                    </td>
                </tr>
                   <tr>
                    <td style="width: 50%"><label for="materialoptions">Material Options</label>
            <asp:RadioButtonList ID="materialoptions" runat="server" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="materialoptions_SelectedIndexChanged" RepeatDirection="vertical"
                                                                    Width="95%">
                                                                </asp:RadioButtonList>

                    </td>
                </tr>
                   <tr>
                    <td style="width: 50%"><label for="material">Select Material</label>
           <asp:DropDownList ID="material_list" runat="server" AutoPostBack="true" OnDataBound="material_list_DataBound" 
                                      OnSelectedIndexChanged="material_list_SelectedIndexChanged" Width="95%">
                                                                </asp:DropDownList></td>
                </tr>
                  <tr>
                      <%--<td style="width: 50%"><label for="size">Size</label>
                      <asp:TextBox ID="txtsize" runat="server"></asp:TextBox>
                    </td>--%>
                      <td style="width: 50%"><label for="quantity">Quantity</label>
                      <asp:TextBox ID="txtquantity" runat="server"></asp:TextBox>
                    </td>
                      <td style="width: 50%"><label for="rate">Rate</label>
                      <asp:TextBox ID="txtrate" runat="server"></asp:TextBox>
                    </td>
                </tr>
                                     <tr>
                    <td style="width: 50%"><label for="material">Total Pipe Length laid(mtrs)</label>
<asp:TextBox ID="txtpipelength" runat="server"  Width="50%"></asp:TextBox>
          </td>
                </tr>
                                        <tr>
                    <td style="width: 50%"><label for="material">Excavated Length</label>
<asp:TextBox ID="txtexcavation" runat="server"  Width="50%"></asp:TextBox>
          </td>
                </tr>
                                       <tr> <td style="width: 50%">
                        <label for="naration">General Comment</label>
            <asp:TextBox ID="txtcomment" runat="server" Height="53px" TextMode="MultiLine" Width="50%" BackColor="LightGreen" Font-Bold="True" ForeColor="Maroon"></asp:TextBox>

                          </td>
                </tr>
                </table>
                               <br />
                               <center>
                                   <asp:Button ID="btnsubmititem" runat="server" Text="Submit Item" cssclass ="btn-primary" OnClick="btnsubmititem_Click" />
                                   <br /><br />
                                   <div id ="itemdisplay" runat="server">
                                      
                                       <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                                                                Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Overline="False"
                                                                                Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="Horizontal"
                                                                                HorizontalAlign="Center" OnItemCommand="DataGrid1_ItemCommand1" PageSize="15"
                                                                                Style="text-align: justify; font: menu;" Width="98%" AllowPaging="True" >
                                                        <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                                                        <EditItemStyle BackColor="#999999" />
                                                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                                                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                                                        <ItemStyle ForeColor="#333333" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                                        <Columns>
                                                            <asp:BoundColumn DataField="expenseID" HeaderText="ExpenseID" Visible="False">
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="No." HeaderText="No.">
                                                                <HeaderStyle Width="5%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="MaterialName" HeaderText="Description" Visible="false">
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="material" HeaderText="Material" >
                                                                <HeaderStyle Width="10%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Size" HeaderText="Size">
                                                                <HeaderStyle Width="10%" />
                                                                <ItemStyle Width="120px" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Length" HeaderText="Length" Visible="False">
                                                                <HeaderStyle Width="5%" />
                                                                <ItemStyle Width="120px" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="quantity" HeaderText="Quantity">
                                                                <HeaderStyle Width="5%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="UnitCost" HeaderText="Rate">
                                                                <HeaderStyle Width="15%" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                                                <HeaderStyle Width="15%" />
                                                            </asp:BoundColumn>
                                                            <asp:ButtonColumn CommandName="btnEdit" HeaderText="Edit" Text="Edit">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" ForeColor="Blue" />
                                                            </asp:ButtonColumn>
                                                            <asp:ButtonColumn CommandName="btnRemove" HeaderText="Remove" Text="Remove">
                                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                    Font-Underline="False" ForeColor="Blue" />
                                                            </asp:ButtonColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                    </asp:DataGrid>
                                        <asp:Label ID="lblTotalCost" runat="server" Font-Bold="True" Font-Names="Arial Narrow"
                                                        Text="0" Visible="False"></asp:Label>
                                   </div>
                                   <br />
              
                                
                                   <asp:Button ID="btnsavematerials" runat="server" Text="Save Expenditure" cssclass ="btn-primary" OnClick="btnsavematerials_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                   <%--<asp:Button ID="btnPrintInvoice" runat="server" Text="Print Invoice" cssclass ="btn-primary" OnClick="btnPrintInvoice_Click" />--%>
                               </center>
                   </div></div>

               </fieldset>


               <br />
                             
               
            </div>
             
       </div>
           <div id="docketdisplay" runat="server" visible="false">
           <%--<div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">--%>
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;Docket Initiation</legend>
            <div class="row">
    <div class="col-md-6">

         <table style="width: 50%">
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Customer Type
                                                                                       <asp:Label ID="l1" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                                            </td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtcusttype" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                Block No
                        <asp:Label ID="l2" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                                 </td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                &nbsp;<asp:DropDownList ID="cboBlock" runat="server" OnDataBound="cboBlock_DataBound" AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <asp:Button ID="btnGetNumber" runat="server" OnClick="btnGetNumber_Click" Text="Get Number"
                                                                                    Width="103px" Font-Bold="True" Font-Italic="False" Font-Names="Arial Narrow" /></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Connection Number
                                                                                       <asp:Label ID="l3" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                                            </td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtConnectionNo" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                       <%--<tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Ref</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        ReadOnly="True" Width="80%"></asp:TextBox><br />
                                                                                    <asp:Button ID="btnGetMeterRef" runat="server" OnClick="btnGetMeterRef_Click" Text="Get Reference"
                                                                                    Width="103px" Font-Bold="True" Font-Italic="False" Font-Names="Arial Narrow" />
                                                                                </td>
                                                                            </tr>--%>
              <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Make / Type</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:DropDownList ID="cboType" runat="server" OnDataBound="cboType_DataBound" Width="80%">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    Meter &nbsp;Number</td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtNumber" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Size
                                                                                           <asp:Label ID="l4" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                                                </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px"><asp:DropDownList ID="cboMeterSize" runat="server" OnDataBound="cboMeterSize_DataBound"
                                                                                    Width="80%">
                                                                                </asp:DropDownList></td>
                                                                            </tr>
                                                                            
                                                                           
             <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    Remark
                                                                                           <asp:Label ID="l5" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                                                </td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtRemark" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Height="40px" TextMode="MultiLine" Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Lattitude</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtlattitude" runat="server"  CssClass="InterfaceTextboxLongReadOnly"
                                                                                         Width="80%"></asp:TextBox>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Longitude</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtlongitude" runat="server"  CssClass="InterfaceTextboxLongReadOnly"
                                                                                         Width="80%"></asp:TextBox></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" style="height: 12px">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
    </div>
    <div class="col-md-6">
       <!--table2 -->
         <%--<table><tr><td>yyyy</td></tr></table>--%>
        <table style="width: 50%">
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Initial
                                                                                    Reading</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtReading" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
                                      
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Dials</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtDials" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
                                                                                    <asp:RangeValidator ID="RangeValidator2" Type="Integer" MinimumValue="4" MaximumValue="10" ControlToValidate="txtDials" runat="server" ErrorMessage="enter only numbers between 1 and 10" ForeColor="Red"></asp:RangeValidator>
                                                                                </td>
                                                                            </tr>
                                                                            <%--<tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Life</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtMeterLife" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="80%" onkeypress="return NumberOnly()"></asp:TextBox>
                                                                                    <asp:RangeValidator ID="RangeValidator3" Type="Integer" MinimumValue="1" MaximumValue="10" ControlToValidate="txtMeterLife" runat="server" ErrorMessage="enter only numbers between 1 and 30" ForeColor="Red"></asp:RangeValidator>
                                                                                </td>
                                                                            </tr>--%>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Manufacture Date</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
<%--                                                                                    <asp:TextBox ID="txtManufacturedDate" runat="server" CssClass="manufacturedDate" Width="80%"></asp:TextBox>--%>
                                                                                    <asp:TextBox ID="txtManufacturedDate" CssClass="form-control"  runat="server" style="left: 0px; top: 0px"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="txtManufacturedDate_CalendarExtender" runat="server" TargetControlID="txtManufacturedDate" Format="dd/MM/yyyy" />
                                                                                </td>
                                                                                    
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    Installed By
                                                                                           <asp:Label ID="l6" runat="server" Text="**" Visible="true" Font-Bold="true" ForeColor="Red"></asp:Label>

                                                                                </td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                   <%-- <asp:TextBox ID="txtInstalledby" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="plumber_list" CssClass="form-control" runat="server" Width="80%" AutoPostBack="false" OnDataBound="plumber_list_DataBound"
                           Visible="true">
                       </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Date of Connecting</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                   <%-- <asp:TextBox ID="txtInstallationDate" runat="server" CssClass="InstallationDate"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                     <asp:TextBox ID="txtInstallationDate" CssClass="form-control"  runat="server" style="left: 0px; top: 0px"></asp:TextBox>
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInstallationDate" Format="dd/MM/yyyy" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="3" style="height: 12px">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
    </div>
</div>
                            <div class="form-group col-sm-12 col-md-12 col-lg-12">
                  <center>
               <asp:Button ID="btnSave" runat="server" Text="Save Details" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  
                     </div>
                   </fieldset>
                       
            <%--</div>--%>
            
                  
              </div>
          <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblconnectionId" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblarea" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblCostItemID" runat="server" Text="0" Visible="False"></asp:Label>
               <asp:Label ID="lblCostcode" runat="server" Text="0" Visible="False"></asp:Label>
               <asp:Label ID="lblestimateid" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblMeterCode" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblConnectionCode" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblareacode" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblcustomertype" runat="server" Text="0" Visible="False"></asp:Label>
              <%-- <asp:Label ID="lblapplicant" runat="server" Text="." Visible="False"></asp:Label>--%>
    </form>

    <br /><br />
        
	</div>
 
</div>
    <script type="text/javascript">
        $(function () {
            $(".manufacturedDate").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        $(function () {
            $(".InstallationDate").datepicker({
                dateFormat: "dd-M-yy"
            });
        });
        function NumberOnly() {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 48 && AsciiValue <= 57) || (AsciiValue == 8 || AsciiValue == 127))
                event.returnValue = true;
            else
                event.returnValue = false;
        }
    </script>
</asp:Content>
