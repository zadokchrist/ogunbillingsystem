<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="CustomerCreation.aspx.cs" Inherits="TraceBilling.CustomerCreation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <div class="container">
	<div class="row">
	  <form role="form" runat="server">
          <br />
          <div><h3>CUSTOMER CREATION MANAGEMENT</h3></div>
    
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
           <th class="modal-sm" style="width: 250px">Job Number</th>
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
                          <asp:Button ID="Button3"  CssClass="btn-primary"
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
                              ForeColor="#333333" GridLines="None" Width="90%"
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
                 <asp:BoundField DataField="typeName" HeaderText="Customer Type" NullDisplayText="-" /> 
                  <asp:BoundField DataField="className" HeaderText="Category" NullDisplayText="-" /> 
                 <asp:BoundField DataField="statusName" HeaderText="Status" NullDisplayText="-" /> 
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
          
           <div id="customerdisplay" runat="server" visible="false">
           <%--<div class="form-group col-xs-10 col-sm-6 col-md-6 col-lg-6">--%>
               <fieldset class="panel panel-primary">
    <legend class="w-auto">&nbsp;New Connection Entry Form</legend>
            <div class="row">
    <div class="col-md-6">

         <table style="width: 80%">
             <tr>                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                </td>
                                                                            <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                            </td>
                                                                            <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                <asp:Label ID="lblcustdetails" runat="server" Text="Customer Details" ForeColor="Blue" Font-Bold="true"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Application Number</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtappnumber" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Application Date</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtappdate" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
              <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Customer Ref</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtcustref" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        ReadOnly="True" Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox><br />
                                                                                    
                                                                                </td>
                                                                            </tr>
             <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Ref</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtMeterRef" runat="server" BackColor="LightGray" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        ReadOnly="True" Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon"></asp:TextBox><br />
                                                                                    
                                                                                </td>
                                                                            </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Full Name</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtfullname" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Customer Title</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txttitle" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="false" ></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Occupation</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtoccupation" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="false" ></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Telephone Contact 1</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtphone1" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="false" ></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Telephone Contact 2</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtphone2" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="false" ></asp:TextBox>
                                                                            </td>
                                                                        </tr>
             <tr>
                                                                            <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                Email Address</td>
                                                                            <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                &nbsp;</td>
                                                                            <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                <asp:TextBox ID="txtemail" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                    Width="80%" Font-Bold="True" Font-Size="Medium" ForeColor="Maroon" ReadOnly="false"></asp:TextBox>
                                                                            </td>
                                                                        </tr>

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
                                                                                    <asp:TextBox ID="txtmeterNumber" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Meter Size</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px"><asp:DropDownList ID="cboMeterSize" runat="server" OnDataBound="cboMeterSize_DataBound"
                                                                                    Width="80%">
                                                                                </asp:DropDownList></td>
                                                                            </tr>
                                                                            
                                                                           
             <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    Physical Address</td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtaddress" runat="server" CssClass="InterfaceTextboxLongReadOnly"
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
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Territory</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtterritory" runat="server"  CssClass="InterfaceTextboxLongReadOnly"
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
        <table style="width: 80%">
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    </td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:Label ID="lblcustbilling" runat="server" Text="Billing Details" ForeColor="Blue" Font-Bold="true"></asp:Label> 
                                      
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Customer Type</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <%--<asp:TextBox ID="txtcusttype" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray" ></asp:TextBox>--%>
                                                                                   <asp:DropDownList ID="customertype_list" runat="server" OnDataBound="customertype_list_DataBound"  
                                                                                    Width="80%">
                                                                                </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Area</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtarea" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                    
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Branch/Zone</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtzone" runat="server" CssClass="InterfaceTextboxLongReadOnly" Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox></td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRowUp" style="height: 10px">
                                                                                    PropertyRef</td>
                                                                                <td class="InterFaceTableMiddleRowUp" style="height: 10px">
                                                                                </td>
                                                                                <td class="InterFaceTableRightRowUp" style="height: 10px">
                                                                                    <asp:TextBox ID="txtproperty" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Block</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtblock" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Connection Number</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtconnectionno" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    General Classification</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <%--<asp:TextBox ID="txtcategory" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="cboclass" runat="server" OnDataBound="cboclass_DataBound"
                                                                                    Width="80%" AutoPostBack="true" OnSelectedIndexChanged="cboclass_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Tariff</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <%--<asp:TextBox ID="txttariff" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="cbotariff" runat="server" OnDataBound="cbotariff_DataBound"  
                                                                                    Width="80%">
                                                                                </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                      <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Category</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <%--<asp:TextBox ID="txtcategory" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="cbocategory" runat="server" OnDataBound="cbocategory_DataBound"
                                                                                    Width="80%" >
                                                                                </asp:DropDownList>
                                                                                </td>
                                                                            </tr>
                        <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Service Type</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <asp:TextBox ID="txtservicetype" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%" ForeColor="Maroon" ReadOnly="True" BackColor="LightGray"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Supply status</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <%--<asp:TextBox ID="txtsupply" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                     <asp:RadioButtonList ID="rtnSupplytype" runat="server" RepeatDirection="Horizontal" Width="79%" >
                        <asp:ListItem Value="1">On Supply</asp:ListItem>
                        <asp:ListItem Value="2">Off Supply</asp:ListItem>
                   </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    IsActive</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                    <%--<asp:TextBox ID="txtactive" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                    <asp:CheckBox ID="chkactive" runat="server"  Text="Tick if connection is active" />
                                                                                </td>
                                                                            </tr>
            <tr>
                                                                                <td class="InterFaceTableLeftRow" style="height: 10px">
                                                                                    Has sewer</td>
                                                                                <td class="InterFaceTableMiddleRow" style="height: 10px">
                                                                                    &nbsp;</td>
                                                                                <td class="InterFaceTableRightRow" style="height: 10px">
                                                                                   <%-- <asp:TextBox ID="txtsewer" runat="server" CssClass="InterfaceTextboxLongReadOnly"
                                                                                        Width="80%"></asp:TextBox>--%>
                                                                                    <asp:CheckBox ID="chksewer" runat="server"   Text="Tick if Application Included Sewer"/>
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
               <asp:Button ID="btnSave" runat="server" Text="Create Customer" cssclass ="btn-primary" OnClick="btnSave_Click" />&nbsp;&nbsp;&nbsp;
                      <asp:Button ID="btncancel"  CssClass="btn-primary" Visible="false"
                                    runat="server" Text="Cancel" onclick="btncancel_Click" />
          </center>
                  
                     </div>
                   </fieldset>
                       
            <%--</div>--%>
            
                  
              </div>
          <asp:Label ID="lblApplicationCode" runat="server" Text="." Visible="False"></asp:Label>
                <asp:Label ID="lblconnectionId" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblarea" runat="server" Text="0" Visible="False"></asp:Label>
                <asp:Label ID="lblCostItemID" runat="server" Text="0" Visible="False"></asp:Label>
               <asp:Label ID="lblCostcode" runat="server" Text="0" Visible="False"></asp:Label>
               <asp:Label ID="lblestimateid" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblCustomerCode" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblConnectionCode" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblareacode" runat="server" Text="0" Visible="False"></asp:Label>
          <asp:Label ID="lblbranch" runat="server" Text="0" Visible="False"></asp:Label>
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
