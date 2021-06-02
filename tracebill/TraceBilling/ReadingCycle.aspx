<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ReadingCycle.aspx.cs" Inherits="TraceBilling.ReadingCycle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>READING CYCLE MANAGEMENT</h3></div>
    
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
              <asp:Button ID="btngenerateschedule" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btngenerateschedule_Click" Text="GENERATE SCHEDULE" cssclass ="btn-primary"
                                        Width="153px" />&nbsp;&nbsp;&nbsp;&nbsp;
                   <asp:Button ID="btnreadingcapture" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnreadingcapture_Click" Text="CAPTURE READINGS" cssclass ="btn-primary"
                                        Width="144px" />&nbsp;&nbsp;&nbsp;&nbsp;
                  
                  <asp:Button ID="btnrexceptions" runat="server" BorderStyle="Inset" Font-Bold="True" Font-Names="Cambria"
                                        Font-Underline="False" OnClick="btnrexceptions_Click" Text="HANDLE EXCEPTIONS" cssclass ="btn-primary"
                                        Width="149px" />&nbsp;
                  <br /><br />
                <asp:Label ID="lblapplicant" runat="server" Text="." ForeColor="Maroon" Font-Bold="true"></asp:Label>
              </center>

              </div>
          </div>
          <br />
       <div id="generateschedule" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Generate Reading Schedule</legend>
                 
                   <br< />
                   <div id="scheduledisplay" runat="server" visible="true">
                 <p>This is reading schedule</p>
                   </div>
           
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               <div id="capturereading" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Capture Readings</legend>
                 
                   <br< />
                   <div id="capturedisplay" runat="server" visible="true">
                  
                      <%-- <p>this is reading capture</p>--%>
                        <label for="rdgoptions">Reading Options</label>
           <asp:RadioButtonList ID="rdgoptions" runat="server" RepeatDirection="Horizontal" Width="80%"  AutoPostBack="true" OnSelectedIndexChanged="rdgoptions_SelectedIndexChanged">
                        <asp:ListItem Value="1">One By One</asp:ListItem>
                        <asp:ListItem Value="2">Bulk Upload</asp:ListItem>
                   </asp:RadioButtonList>
<%--                 <div runat="server" class="align:margin: auto;  width: 50%;  border: 3px solid green;  padding: 10px;">--%>
                     <table align="center" cellpadding="0" cellspacing="0" style="width: 79%">
                         <tr>
                             <td>
                                 <table>
                   <tr>
                    <td style="width: 400px">
                        <label>Area</label>
            <asp:DropDownList ID="area_list3" CssClass="form-control" runat="server"  OnDataBound="area_list3_DataBound" Visible="true"
                OnSelectedIndexChanged="area_list3_SelectedIndexChanged" AutoPostBack="true" >
                
                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 400px">
                         <label>Current Period</label>
             <asp:TextBox runat="server" CssClass="form-control" ID="txtcurrentperiod" placeholder="Enter period" ReadOnly="true"/>
                    </td>
                </tr>
                                     </table>
                             </td>
                             <td></td>
                             <td>
                                 <table>
                   <tr>
                    <td style="width: 400px">
                        <label>Branch/Zone</label>
            <asp:DropDownList ID="branch_list1" CssClass="form-control" runat="server"  OnDataBound="branch_list1_DataBound" Visible="true" >
                
                </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 400px">
                         <label>Capturing User</label>
            <asp:TextBox runat="server" CssClass="form-control" ID="txtuser" placeholder="Enter user" ReadOnly="true"/>
                    </td>
                </tr>
                                     </table>
                             </td>
                         </tr>
                     </table>
                     <div id="onebyonedisplay" runat="server" visible="false">

                         <table>
                            <tr>
                    <td style="vertical-align: middle; height: 20px; text-align: center; width: 641px;">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr>
                                <td class="row">
                                   <b>SINGLE
                                    READINGS CAPTURE</b></td>
                            </tr>
                        </table>
                         
                    </td>
                </tr>
                             
                         </table>
                      <table align="center" cellpadding="0" cellspacing="0" style="width: 85%">
                                                    <tr>
                                                      
                                                        <td class="InterfaceHeaderLabel2" colspan="2" style="vertical-align: middle; width: 30%;
                                                            height: 18px; text-align: center">
                                                           
                                                            <label> PROPERTY REF</label>
                                                        </td>
                                                        <td class="InterfaceHeaderLabel2" colspan="1" style="vertical-align: middle; width: 20%;
                                                            height: 18px; text-align: center">
                                                           
                                                            <label> CUST REF</label>
                                                        </td>
                                                        <td class="InterfaceHeaderLabel2" style="vertical-align: middle; width: 10%; height: 18px;
                                                            text-align: center">
                                                        </td>
                                                    </tr>
                                                   
                                                    <tr>
                                                       
                                                        <td colspan="2" style="vertical-align: middle; width: 30%; height: 23px; text-align: center">
                                                            <asp:TextBox ID="txtInquirePropRef" runat="server" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                                        <td colspan="1" style="vertical-align: middle; width: 20%; height: 23px; text-align: center">
                                                            <asp:TextBox ID="txtInquireCustRef" runat="server" BackColor="LightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="Maroon" Width="90%"></asp:TextBox></td>
                                                        <td style="vertical-align: middle; width: 20%; height: 23px; text-align: center">
                                                            &nbsp;<asp:Button ID="btnInquire" runat="server" BorderStyle="Inset" Font-Bold="True"
                                                                Font-Size="9pt" OnClick="btnInquire_Click" OnClientClick="changeButtonText(this);"
                                                                Text="Inquire" Width="95%" /></td>
                                                    </tr>
                                                  
                                                    <tr>
                                                        <td colspan="5" style="vertical-align: middle; width: 100%; height: 2px; text-align: center">
                                                            <asp:TextBox ID="txtCustName" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Style="text-align: center"
                                                                Width="98%"></asp:TextBox></td>
                                                    </tr>
                                                </table>   


     <div class="row">
    <div class="col-md-6">
       <!--table1 -->
       <%-- <table><tr><td>xxxxx</td></tr></table>--%>
         <table style="width: 50%">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 82px;">
                                                            Meter Ref</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px; width: 6px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="166%"></asp:TextBox></td>
                                                    </tr>
                                                   
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">
                                                            Prop Ref</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtPropRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="153%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">
                                                            Pre Reading</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtPreReading" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="156%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">
                                                            Pre Read Date</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtPreReadDate" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="151%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">
                                                            Consumption</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtConsumption" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="154%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">
                                                            Avg Cons.</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtAvgConsumption" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="152%"></asp:TextBox></td>
                                                    </tr>
             <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 82px;">
                                                            Meter Dials</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px; width: 6px;">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px; width: 85px;">
                                                            <asp:TextBox ID="txtdials" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="152%"></asp:TextBox></td>
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
        <table  style="width: 50%; ">
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            Type</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtType" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            Is Billed</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtIsBilled" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="DarkRed" ReadOnly="True" Width="80%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            Reading</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtReading" runat="server" BackColor="lightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="Maroon" Width="80%"  onkeypress="return NumberOnly()"></asp:TextBox>
                                                            <%--<asp:RangeValidator ID="RangeValidator1" Type="Integer"  ControlToValidate="txtReading" runat="server" ErrorMessage="enter only numbers" ForeColor="Red"></asp:RangeValidator>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            Read Date</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtReadDate" runat="server" BackColor="lightBlue" CssClass="readdate"
                                                                Font-Bold="True" ForeColor="Maroon" Width="80%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 156px;">
                                                            Reader</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                            &nbsp;</td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                            <asp:DropDownList ID="reader_list" runat="server" OnDataBound="reader_list_DataBound"
                                                                Width="80%">
                                                        </asp:DropDownList>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            Other Reader</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:TextBox ID="txtotherReader" runat="server" BackColor="lightBlue" CssClass="InterfaceTextboxLongReadOnly"
                                                                Font-Bold="True" ForeColor="Maroon" Width="80%"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRow" style="height: 10px; width: 156px;">
                                                            Comment</td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRow" style="height: 10px">
                                                            <asp:DropDownList ID="comment_list" runat="server" OnDataBound="comment_list_DataBound"
                                                                Width="80%">
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            </td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        <asp:CheckBox ID="chkEstimate" runat="server" Font-Bold="True" Font-Size ="X-Small"
                                                            Text="Tick If Reading Is An Estimate" Width="190px"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            </td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        <asp:CheckBox ID="ChkMeterReset" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                            Text="Tick only If meter was Reset!" Width="190px"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="InterFaceTableLeftRowUp" style="height: 10px; width: 156px;">
                                                            </td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                        <asp:CheckBox ID="ChkConsumption" runat="server" Font-Bold="True" Font-Size="X-Small"
                                                            Text="Tick only If expected consumption is genuinely negative!" Width="190px"/></td>
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
               <asp:Button ID="btnSave" runat="server" Text="Submit" cssclass ="btn-primary" OnClick="btnSave_Click" />
          </center>
                  
                     </div>
                     
                 <%--</div>--%>
                       
                   </div>
           <div id="bulkdisplay" runat="server" visible="false">
               <%--<p>bulk uploading</p>--%>
                                                  <table style="width: 100%">
                 <tr>
                    <td style="vertical-align: middle; height: 20px; text-align: center">
                        <table align="center" cellpadding="0" cellspacing="0" style="width: 50%">
                            <tr>
                                <td class="row">
                                    <b>BULK UPLOAD READINGS CAPTURE</b></td>
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
                                                            Header</td>
                                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                        </td>
                                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                            <asp:CheckBox ID="chkHeader" runat="server" Font-Bold="True" ForeColor="Maroon" Text="Yes File has header" />&nbsp;</td>
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
                                            <%--Warnings--%>

                                        </td>
                                        <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                        </td>
                                        <td class="InterFaceTableRightRowUp" style="height: 10px">
                                           <%-- <asp:CheckBox ID="chkWarnings" runat="server" Font-Bold="True" ForeColor="Maroon"
                                                Text="Yes Skip Warnings"/>
                                            &nbsp;--%>
                                        </td>
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
                       </div>
                   </fieldset>                                                      
              
            </div>
                 
       </div>
               
               <div id="handleexeptions" runat="server" visible="false">
           <div class="form-group col-sm-12 col-md-12 col-lg-12">
               <fieldset class="panel panel-primary" runat="server">
    <legend class="w-auto">&nbsp;Field Exceptions/Anormalies</legend>
                 
                   <br< />
                   <div id="exceptiondisplay" runat="server" visible="true">
                 <p>This is exceptions handling</p>
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
