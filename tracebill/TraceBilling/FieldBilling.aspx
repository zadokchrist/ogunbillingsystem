<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FieldBilling.aspx.cs" Inherits="TraceBilling.FieldBilling" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>FIELD BILLING MANAGEMENT</h3></div>
    
          <center>
                <asp:Label runat="server" ID="lblmsg" Visible="false" ></asp:Label>
               <div>
                 
                   <table width="100%">
    <tr>
    <%--<th>New</th>--%>
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
            </center>

        <br />
         
        <div class="form-group col-sm-12 col-md-12 col-lg-12">
              <div id="btnlinks" runat="server" visible="true">
              <center>
              <asp:Button ID="btnroutedownload" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnroutedownload_Click" Text="DOWNLOAD ROUTE" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;     
                                            
                                  
                                    <asp:Button ID="btnreadingupload" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreadingupload_Click" Text="ONSPOT UPLOAD " cssclass ="btn-primary"
                                        Width="160px" />&nbsp;&nbsp;&nbsp;&nbsp; 
                  <asp:Button ID="btnreconciliation" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreconciliation_Click" Text="INVOICE RECONCILIATION" cssclass ="btn-primary"
                                        Width="187px" />&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
          <br />
       <div id="reconcileschedule" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Bill Reconciliation</legend>
                 
                   <br< />
                   <div id="scheduledisplay" runat="server" visible="true">
                 <p>This is bill reconciliation</p>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="downloadroute" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Download Routes</legend>
                 
                   <br< />
                   <div id="downloaddisplay" runat="server" visible="true">
                <%-- <p>This is routes download</p>--%>
                        <table width="100%">
    <tr>
    <%--<th>New</th>--%>
        <th class="modal-sm" style="width: 207px">Country</th>
         <th class="modal-sm" style="width: 226px">Operation Area</th>
        <th class="modal-sm" style="width: 226px">Branch/Zone</th>
        <th class="modal-sm" style="width: 226px">Book</th>
        <th class="modal-sm" style="width: 226px">Walk</th>
        <th></th>
        </tr>
        <tr>
  
          
                          <td class="modal-sm" style="width: 207px" >
                      
            <asp:DropDownList ID="country_list2" CssClass="form-control" runat="server"  OnDataBound="country_list2_DataBound" Visible="true" AutoPostBack="True"
                 OnSelectedIndexChanged="country_list2_SelectedIndexChanged"  >
                </asp:DropDownList>
                    </td>
             
                           
             <td class="modal-sm" style="width: 226px" >
                      
            <asp:DropDownList ID="area_list2" CssClass="form-control" runat="server"  OnDataBound="area_list2_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
                <td class="modal-sm" style="width: 226px" >
                      
            <asp:DropDownList ID="branch_list" CssClass="form-control" runat="server"  OnDataBound="branch_list_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
              <td class="modal-sm" style="width: 226px" >
                      
            <asp:TextBox runat="server" CssClass="form-control" ID="txtbook" placeholder="Enter Book Number" BackColor="LightGreen"  Font-Bold="True" ForeColor="Maroon" Width="120px"/>
                    </td>
                  <td class="modal-sm" style="width: 226px" >
                      
            <asp:TextBox runat="server" CssClass="form-control" ID="txtwalk" placeholder="Enter Walk Number" BackColor="LightGreen"  Font-Bold="True" ForeColor="Maroon" Width="120px"/>
                    </td>
                 
                          <td>
                         
           
                          </td>
                                                 
                          </tr>
                          </table>
                       <center>
                            <asp:Button ID="Button1" Width="150" Height="40" CssClass="btn-primary"
                                    runat="server" Text="Search" onclick="Button4_Click" />
                      
                       </center>
                       <hr />
                        <div id="downloadgrid" runat="server" visible="false">
 <asp:DataGrid ID="DataGriddownloads" runat="server" AutoGenerateColumns="False"
                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Courier New"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                GridLines="Horizontal" HorizontalAlign="Justify"
                OnItemCommand="download_command"
                PageSize="50" Style="font: menu;text-align: justify" Width="100%">
                <FooterStyle BackColor="InactiveCaption" Font-Bold="False" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="Route" HeaderText="Route"></asp:BoundColumn>
                    <asp:ButtonColumn CommandName="download" HeaderText="Download" Text="Download"></asp:ButtonColumn>
                    
                    </Columns>
            </asp:DataGrid>
          </div>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="uploadroutes" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Onspot Upload Routes</legend>
                 
                   <br< />
                  
                              <div id="uploaddisplay" runat="server" visible="true">
               <%--<p>bulk uploading</p>--%>
                                                  <table style="width: 100%">
                 <tr>
                    <td style="vertical-align: middle; height: 20px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr>
                                <td class="row">
                                    <b>FIELD ONSPOT UPLOAD</b></td>
                            </tr>
                        </table>
                    </td>
                </tr>
              
                <tr>
                    <td colspan="3" style="vertical-align: top; width: 100%; height: 5px; text-align: center">
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="width: 48%; text-align: right; vertical-align: top;">
                                                <table align="right" cellpadding="0" cellspacing="0" style="width: 72%">
                                                        <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                            Area</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:DropDownList ID="area_list3" CssClass="form-control" runat="server"  OnDataBound="area_list3_DataBound" Visible="true"
                OnSelectedIndexChanged="area_list3_SelectedIndexChanged" AutoPostBack="true" >
                
                </asp:DropDownList></td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td colspan="3" style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                            Read Date</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:TextBox ID="txtReadingDate1" runat="server" BackColor="LightGreen" CssClass="uploaddate"
                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 2px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                            Browse File</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="InterfaceDropdownList"
                                                Width="80%" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="width: 2%; height: 2px">
                            </td>
                            <td style="width: 48%; text-align: left; vertical-align: top;">
                                <table align="left" cellpadding="0" cellspacing="0" style="width: 60%">
                                   
                                     <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                            Branch</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                             <asp:DropDownList ID="branch_list1" CssClass="form-control" runat="server"  OnDataBound="branch_list1_DataBound" Visible="true" >
                
                </asp:DropDownList></td>
                                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                            Reader</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px"><asp:DropDownList ID="cboReader1" runat="server" OnDataBound="cboReader1_DataBound"
                                                                Width="90%">
                                        </asp:DropDownList>&nbsp;</td>
                                    </tr>
                                            <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                            Current Period</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtcurrentperiod" placeholder="Enter period" ReadOnly="true" Height="20px"/>
                                             </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 2px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                            Click Here</td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                            <asp:Button ID="btnOK" runat="server" Font-Size="9pt" Height="23px" OnClick="btnOK_Click"
                                                Text="UPLOAD" Width="145px" Font-Bold="True" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 12px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 824px">
                    </td>
                </tr>
            </table>

                     </div>

           
                   </fieldset>                                                      
              
            </div>
                 
       </div>

         
        
    </form>

    <br /><br />
        
	</div>
 
</div>
    <script type="text/javascript">
        $(function () {
            $(".readdate").datepicker({
                dateFormat: "dd/mm/yy"
            });
        });
        $(function () {
            $(".uploaddate").datepicker({
                dateFormat: "dd/mm/yy"
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

