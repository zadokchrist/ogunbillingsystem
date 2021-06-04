<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MeterManagement.aspx.cs" Inherits="TraceBilling.MeterManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>METER ACTIVITY MANAGEMENT</h3></div>
    
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
              <asp:Button ID="btninventory" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btninventory_Click" Text="METER INVENTORY" cssclass ="btn-primary"
                                        Width="153px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnservicing" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnservicing_Click" Text="METER SERVICING" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="btnreplacement" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreplacement_Click" Text="METER REPLACEMENT" cssclass ="btn-primary"
                                        Width="159px" />&nbsp;&nbsp;&nbsp;&nbsp;                 
                                  
                                    <asp:Button ID="btntransfer" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btntransfer_Click" Text="METER TRANSFER" cssclass ="btn-primary"
                                        Width="160px" />&nbsp;&nbsp;&nbsp;&nbsp; 

                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
          <br />
       <div id="meterinventory" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Inventory</legend>
                 
                   <br< />
                   <div id="inventorydisplay" runat="server" visible="true">
<%--                 <p>This is meter inventory</p>--%>
                                   <table>
                   <tr>
                    <td style="width: 502px">
                        <label>Meter Type</label>
             <asp:DropDownList ID="cboType" runat="server" OnDataBound="cboType_DataBound" Width="80%">
                       </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 502px">
                         <label>Meter Serial</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtserial" placeholder="Enter serial" AutoPostBack="True" OnTextChanged="txtserial_TextChanged" />
                    </td>
                </tr>
                   <tr>
                    <td style="width: 502px">
                         <label>Meter Dials</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtdials" placeholder="Enter dials" ReadOnly="true" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
               <tr>
                    <td style="width: 502px">
                         <label>Initial Reading</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtreading" placeholder="Enter initial reading" ReadOnly="true" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
                                                              <tr>
                    <td style="width: 502px">
                         <label>Meter Life/Duration</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtlife" placeholder="Enter meter life" ReadOnly="true" onkeypress="return NumberOnly()"/>
                    </td>
                </tr>
                  <tr>
                    <td style="width: 502px">
                         <label>Manufacturing Date</label>
            <asp:TextBox runat="server" CssClass="manufacturedDate" ID="txtmanufacturedate" placeholder="Enter manufacture date" ReadOnly="true" Width="228px"/>
                    </td>
                </tr>
                    <tr>
                    <td style="width: 502px">
                         <label>Meter Condition</label>
            <asp:TextBox ID="txtcondition" runat="server" Height="40px" TextMode="MultiLine" Width="50%" BackColor="LightGreen" Font-Bold="True" ForeColor="Maroon" ReadOnly="true"></asp:TextBox>
                    </td>
                </tr>
                                          <tr>
                    <td style="width: 502px">
                         <label>Meter Status</label>
            <asp:CheckBox ID="chkactive" runat="server"  Text="Tick if meter is active" />
                    </td>
                </tr>
            </table>
 <br />
              <asp:Button ID="btninventorysave" runat="server" Text="Save" cssclass ="btn-primary" OnClick="btninventorysave_Click" />
              &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btninventorycancel" runat="server" Text="Cancel" cssclass ="btn-primary" OnClick="btninventorycancel_Click" />
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="meterservice" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Servicing</legend>
                 
                   <br< />
                   <div id="servicingisplay" runat="server" visible="true">
                 <p>This is meter servicing</p>

                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="meterreplacement" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Replacement</legend>
                 
                   <br< />
                   <div id="replacementdisplay" runat="server" visible="true">
                 <p>This is meter replacement</p>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
                         <div id="metertransfer" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Meter Transfer</legend>
                 
                   <br< />
                   <div id="transferdisplay" runat="server" visible="true">
                 <p>This is meter transfer</p>
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
        $(function () {
            $(".manufacturedDate").datepicker({
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

