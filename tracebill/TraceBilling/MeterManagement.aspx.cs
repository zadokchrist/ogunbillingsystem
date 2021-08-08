using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Threading;


namespace TraceBilling
{
    public partial class MeterManagement : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    if (Session["roleId"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    string roleid = Session["roleId"].ToString();
                    if(roleid.Equals("9"))//BO
                    {
                        LoadDisplay();
                    }
                    else if(roleid.Equals("4"))//BM
                    {
                        LoadApprovalRequests();
                    }
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Meter Management page");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDisplay()
        {
            //meterinventory.Visible = true;
            searchdisplay.Visible = true;
            btninventory.Visible = true;
            btnservicing.Visible = true;
            btnreplacement.Visible = true;
            //btntransfer.Visible = false;
            btnapprove.Visible = false;

            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
            string custref = txtcustref.Text.Trim();

            DataTable dataTable = bll.LoadCustomerDisplay(countryid, areaid, custref, 1);
            if (dataTable.Rows.Count > 0)
            {
                gv_customerview.DataSource = dataTable;
                gv_customerview.DataBind();
                DisplayMessage(".", true);
                //customerdisplay.Visible = true;
            }
            else
            {
                gv_customerview.DataSource = dataTable;
                gv_customerview.DataBind();
                string error = "100: " + "No records found";
                bll.Log("LoadDisplayCustomer", error);
                DisplayMessage(error, true);
                //customerdisplay.Visible = false;
            }
        }

        private void LoadCountryList()
        {
            DataTable dt = new DataTable();
            try
            {
                string countryid = Session["countryId"].ToString();
                dt = bll.GetCountryList();
                country_list.DataSource = dt;
                country_list.SelectedValue = countryid;
                country_list.DataTextField = "countryName";
                country_list.DataValueField = "countryId";
                country_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCountryList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadAreaList(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetAreaList(countryid);
                area_list.DataSource = dt;

                area_list.DataTextField = "areaName";
                area_list.DataValueField = "areaId";
                area_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayAreaList", error);
                DisplayMessage(error, true);
            }
        }
        private void DisplayMessage(string message, Boolean isError)
        {
            lblmsg.Visible = true;
            lblmsg.Text = message + ".";
            if (isError == true)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        protected void country_list_DataBound(object sender, EventArgs e)
        {
            country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        }
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
        protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                LoadAreaList(countryid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void btninventory_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = true;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            DisplayMessage(".", true);
            searchdisplay.Visible = false;
            LoadMeterTypes();
        }

        protected void btnservicing_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = true;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            DisplayMessage(".", true);
        }

        protected void btnreplacement_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = true;
            metertransfer.Visible = false;
            DisplayMessage(".", true);
        }

        protected void btntransfer_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = true;
            DisplayMessage(".", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            string areaid = area_list.SelectedValue.ToString();
            if(areaid.Equals("0"))
            {
                DisplayMessage("Please select an operation area", true);
            }
            else
            {
                string roleid = Session["roleId"].ToString();
                if (roleid.Equals("9"))//BO
                {
                    LoadDisplay();
                }
                else if (roleid.Equals("4"))//BM
                {
                    LoadApprovalRequests();
                }
            }
         
        }

        protected void btninventorysave_Click(object sender, EventArgs e)
        {
            try
            {
                string metertype = cboType.SelectedValue.ToString();
                string serial = txtserial.Text.Trim();
                string dials = txtdials.Text.Trim();
                string initialrdg = txtreading.Text.Trim();
                string life = txtlife.Text.Trim();
                string manufacturedate = txtmanufacturedate.Text.Trim();
                string condition = txtcondition.Text.Trim();
                bool isactive = chkactive.Checked;
                DateTime manufacturedt = Convert.ToDateTime(manufacturedate);
                string createdby = Session["UserID"].ToString();
                if (metertype == "0")
                {
                    DisplayMessage("Please select meter type", true);
                }
                else if (serial == "")
                {
                    DisplayMessage("Please enter meter number/serial", true);
                }
                else if (initialrdg == "")
                {
                    DisplayMessage("Please enter initial reading on meter", true);
                }
                else if (condition == "")
                {
                    DisplayMessage("Please enter a valid condition status", true);
                }
                else if (dials == "")
                {
                    DisplayMessage("Please enter valid dials on meter", true);
                }
                else if (!bll.IsValidReadingDate(manufacturedate))
                {
                    string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                    DisplayMessage("Invalid Meter Manufacture Date, It cannot be greater than Today ( " + Todate + " )", true);
                }
                else
                {
                    //save details
                    resp = bll.SaveMeterInventory(metertype,serial,dials,initialrdg,life,manufacturedt,createdby,isactive,condition);
                    if (resp.Response_Code == "1")//save
                    {
                        string str = " with inventory details against meter(" + serial + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "106")//edit and update
                    {

                        string str = " with inventory details against meter(" + serial + ")";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
              
                    //clear conrols
                    ClearInventoryControls();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void ClearInventoryControls()
        {
            cboType.SelectedValue = "0";
            txtcondition.Text = "";
            txtdials.Text = "";
            txtserial.Text = "";
            txtlife.Text = "";
            txtmanufacturedate.Text = "";
            txtreading.Text = "";
            chkactive.Checked = false;
            txtdials.ReadOnly = true;
            txtmanufacturedate.ReadOnly = true;
            txtreading.ReadOnly = true;
            txtlife.ReadOnly = true;
            txtcondition.ReadOnly = true;
        }

        protected void btninventorycancel_Click(object sender, EventArgs e)
        {
            ClearInventoryControls();
        }
        private void LoadMeterTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterTypeList();
                cboType.DataSource = dt;//meterTypeId,meterName

                cboType.DataTextField = "meterName";
                cboType.DataValueField = "meterTypeId";
                cboType.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadMeterType", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboType_DataBound(object sender, EventArgs e)
        {
            cboType.Items.Insert(0, new ListItem("- - select meter Type - -", "0"));
        }
        protected void txtserial_TextChanged(object sender, EventArgs e)
        {
            string meterno = txtserial.Text.Trim();
            string str = "";
            if (!String.IsNullOrEmpty(meterno))
            {
                //check meter existence
                bool ismeterserial = bll.CheckExistingSerial(meterno);
                if(ismeterserial)
                {
                    str = "Meter Serial-"+ meterno + " is aleady captured."; 
                    DisplayMessage(str, true);
                }
                else
                {
                    str = ".";
                    DisplayMessage(str, true);
                    txtdials.ReadOnly = false;
                    txtmanufacturedate.ReadOnly = false;
                    txtreading.ReadOnly = false;
                    txtlife.ReadOnly = false;
                    txtcondition.ReadOnly = false;
                }
            }
            else
            {
                str = "Please enter valid meter serial";
                DisplayMessage(str, true);
            }
        }
        protected void gv_customerview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string type = e.Row.Cells[6].Text;
                // e.Row.BackColor = Color.Blue;
                //e.Row.ForeColor = Color.Green;

            }
        }
        protected void gv_customerview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_customerview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_customerview.Rows[index].Cells[1].Text;


            }
        }
        protected void gv_customerview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string custref = "";
            string flag = "";

            if (e.CommandName == "RowReplace")
            {
                custref = Convert.ToString(e.CommandArgument.ToString());
                flag = "1";

            }
            else if (e.CommandName == "RowService")
            {
                custref = Convert.ToString(e.CommandArgument.ToString());
                flag = "2";

            }
            else if (e.CommandName == "RowTransfer")
            {
                custref = Convert.ToString(e.CommandArgument.ToString());
                flag = "3";

            }
            LoadCustomerDisplay(custref, flag);

        }

        private void LoadCustomerDisplay(string custref, string flag)
        {
            try
            {
                lblcustref.Text = custref;
                if(flag.Equals("1"))//replacement
                {
                    meterinventory.Visible = false;
                    meterservice.Visible = false;
                    meterreplacement.Visible = true;
                    metertransfer.Visible = false;
                    DisplayMessage(".", true);
                    LoadReplacementReasons();
                    LoadReplacementDetails(custref);
                }
                else if (flag.Equals("2"))//service
                {
                    meterinventory.Visible = false;
                    meterservice.Visible = true;
                    meterreplacement.Visible = false;
                    metertransfer.Visible = false;
                    DisplayMessage(".", true);
                    LoadServiceReasons();
                    LoadServiceDetails(custref);
                }
                else if (flag.Equals("3"))//transfer
                {
                    meterinventory.Visible = false;
                    meterservice.Visible = false;
                    meterreplacement.Visible = false;
                    metertransfer.Visible = true;
                    string str = "Sorry, meter transfer not available yet.";
                    DisplayMessage(str, true);
                }
                btnreturn.Visible = true;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadReplacementDetails(string custref)
        {
            try
            {
                string areaid = area_list.SelectedValue.ToString();
                string branchid = "0";
                DataTable dTable = bll.GetLatestReadingDetails(custref, areaid, branchid);
                if (dTable.Rows.Count > 0)
                {
                    txtprerdgrep.Text = dTable.Rows[0]["CurReading"].ToString();
                    DateTime CurReadingDate = Convert.ToDateTime(dTable.Rows[0]["CurReadingDate"].ToString());
                    txtprerdgdtrep.Text = CurReadingDate.ToString("MM/dd/yyyy");
                    txtconsumptionrep.Text = dTable.Rows[0]["Consumption"].ToString();
                    txtsizerep.Text = dTable.Rows[0]["size"].ToString();
                    txtmakerep.Text = dTable.Rows[0]["meterName"].ToString();
                    txtserialrep.Text = dTable.Rows[0]["serial"].ToString();
                    string name = dTable.Rows[0]["customerName"].ToString();
                    txtmeterefrep.Text = dTable.Rows[0]["meterRef"].ToString();
                    txtproprefrep.Text = dTable.Rows[0]["propertyRef"].ToString();
                    txtdialsrep.Text = dTable.Rows[0]["dials"].ToString();
                    LoadReplacementReasons();
                    LoadMeterTypes_rep();
                    LoadPipeSizeList();
                    lblreplace.Text = name + "-->" + custref;
                    searchdisplay.Visible = false;
                    //btnreturn.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadServiceDetails(string custref)
        {
            try
            {
                string areaid = area_list.SelectedValue.ToString();
                string branchid = "0";
                DataTable dTable = bll.GetLatestReadingDetails(custref, areaid, branchid);
                if (dTable.Rows.Count > 0)
                {
                    txtViewPreRdg.Text = dTable.Rows[0]["CurReading"].ToString();
                    DateTime CurReadingDate = Convert.ToDateTime(dTable.Rows[0]["CurReadingDate"].ToString());
                    txtPreRdgDate.Text = CurReadingDate.ToString("MM/dd/yyyy");
                    txtConsumption.Text = dTable.Rows[0]["Consumption"].ToString();
                    txtViewSize.Text = dTable.Rows[0]["size"].ToString();
                    txtViewType.Text = dTable.Rows[0]["meterName"].ToString();
                    txtViewSerial.Text = dTable.Rows[0]["serial"].ToString();
                    string name = dTable.Rows[0]["customerName"].ToString();
                    txtViewMeterRef.Text = dTable.Rows[0]["meterRef"].ToString();
                    txtViewPropRef.Text = dTable.Rows[0]["propertyRef"].ToString();
                    txtViewDial.Text = dTable.Rows[0]["dials"].ToString();
                    lblservice.Text = name + "-->" + custref;
                    searchdisplay.Visible = false;
                    //btnreturn.Visible = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void txtReading_TextChanged(object sender, EventArgs e)
        {
            //TODO: To include reseting of the meter 
            if (!String.IsNullOrEmpty(txtrdg.Text.Trim()))
            {
                if (String.IsNullOrEmpty(txtViewPreRdg.Text.Trim()))
                {
                    DisplayMessage("Previous reading cannot be empty", true);
                }
                else
                {
                    double PreReading = Convert.ToDouble(txtViewPreRdg.Text.Trim());
                    double Reading = Convert.ToDouble(txtrdg.Text.Trim());
                    double Consumption = Reading - PreReading;
                    txtConsumption.Text = Consumption.ToString();
                }
                
            }
        }
        protected void txtrdgreplace_TextChanged(object sender, EventArgs e)
        {
            //TODO: To include reseting of the meter 
            if (!String.IsNullOrEmpty(txtrdgreplace.Text.Trim()))
            {
                if (String.IsNullOrEmpty(txtprerdgrep.Text.Trim()))
                {
                    DisplayMessage("Previous reading cannot be empty", true);
                }
                else
                {
                    double PreReading = Convert.ToDouble(txtprerdgrep.Text.Trim());
                    double Reading = Convert.ToDouble(txtrdgreplace.Text.Trim());
                    double Consumption = Reading - PreReading;
                    txtconsumptionrep.Text = Consumption.ToString();
                }
                    
            }
        }

        protected void btnservice_Click(object sender, EventArgs e)
        {
            try
            {
                string custref = lblcustref.Text;
                string meterref = txtViewMeterRef.Text;
                string oldtype = txtViewType.Text;
                string oldsize = txtViewSize.Text;
                string prevrdg = txtViewPreRdg.Text;
                string serial = txtViewSerial.Text;
                string prerdgdt = txtPreRdgDate.Text;
                string appfnlrdg = txtrdg.Text;
                string appfnlrdgdt = txtRdgDate.Text;
                bool isestimated = chkEstimated.Checked;
                string consumption = txtConsumption.Text;
                string reason = cboReason.SelectedItem.ToString();
                string olddials = txtViewDial.Text ;
                //new initials
                string newserial = serial;
                string newsize = oldsize;
                string newmake = oldtype;
                string newdials = olddials;
                string manufacturedt = prerdgdt;
                string newlife = "0";
                string comment = txtInitialReason.Text.Trim();
                string appinitialrdg = txtInitialReading.Text;
                string appinitedgdt = txtInitialRdgDate.Text;
                string createdby = Session["UserID"].ToString();
                string requesttype = "SERVICE";//for service
                string areaid = area_list.SelectedValue.ToString();
                string branchid = "0";
                string servedby = txtservedby.Text;
                string period = bll.GetBillingPeriod(areaid);
                string res =bll.LogMeterRequest(custref, meterref, oldtype, oldsize,olddials, prevrdg, serial, prerdgdt, appfnlrdg, appfnlrdgdt, isestimated,
                    consumption, reason, newserial, newsize, newmake, newdials, manufacturedt, newlife, comment, appinitialrdg, appinitedgdt,
                    createdby, requesttype,areaid,branchid,period,servedby);
                string str = "";
                if (res.Contains("success"))
                {
                    str = "Meter service against customer-" + custref + " successfully submited for approval";
                    DisplayMessage(str, false);
                }
                else
                {
                    str = "Meter service against customer-" + custref + " failed to be submitted for approval";
                    DisplayMessage(str, true);
                }
                RefreshServiceControls();

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshServiceControls()
        {
            txtViewMeterRef.Text = "";
            txtViewSize.Text = "";
            txtViewSize.Text = "";
            txtViewPreRdg.Text = "";
            txtViewSerial.Text = "";
            txtPreRdgDate.Text = "";
            txtrdg.Text = "";
            txtRdgDate.Text = "";
            chkEstimated.Checked = false;
            txtConsumption.Text = "";
            cboReason.SelectedValue = "0";
            txtViewDial.Text = "";
            txtInitialReason.Text = "";
            txtInitialReading.Text = "";
            txtInitialRdgDate.Text = "";
            txtservedby.Text = "";
        }

        protected void btnreturn2_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            searchdisplay.Visible = true;
        }
        private void LoadServiceReasons()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterActivityReasons("1");
                cboReason.DataSource = dt;//meterTypeId,meterName

                cboReason.DataTextField = "reason";
                cboReason.DataValueField = "reasonId";
                cboReason.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadServiceReasons", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadReplacementReasons()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterActivityReasons("2");
                cboReasonrep.DataSource = dt;//meterTypeId,meterName

                cboReasonrep.DataTextField = "reason";
                cboReasonrep.DataValueField = "reasonId";
                cboReasonrep.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadReplacementReasons", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboReason_DataBound(object sender, EventArgs e)
        {
            cboReason.Items.Insert(0, new ListItem("- - select reason - -", "0"));
        }
        protected void cboReasonrep_DataBound(object sender, EventArgs e)
        {
            cboReasonrep.Items.Insert(0, new ListItem("- - select reason - -", "0"));
        }

        protected void btnreplace_Click(object sender, EventArgs e)
        {
            try
            {
                string custref = lblcustref.Text;
                string meterref = txtmeterefrep.Text;
                string oldtype = txtmakerep.Text;
                string oldsize = txtsizerep.Text;
                string prevrdg = txtprerdgrep.Text;
                string serial = txtserialrep.Text;
                string prerdgdt = txtprerdgdtrep.Text;
                string appfnlrdg = txtrdgreplace.Text;
                string appfnlrdgdt = txtrdgdtreplace.Text;
                bool isestimated = chkreplace.Checked;
                string consumption = txtconsumptionrep.Text;
                string reason = cboReasonrep.SelectedItem.ToString();
                string olddials = txtdialsrep.Text ;
              
                //newdetails
                 string newserial = txtNewSerial.Text;
                 string newsize = cboMeterSize.SelectedItem.ToString();
                 string newmake = cboType2.SelectedItem.ToString();
                 string newdials = txtnewdials.Text;
                string manufacturedt = txtManufacturedDate.Text;
                 string newlife = txtnewliferep.Text;
                 
                 string comment = txtcommentrep.Text.Trim();
                 string appinitialrdg = txtNewReading.Text;
                 string appinitedgdt = txtNewRdgDate.Text;
                 string createdby = Session["UserID"].ToString();
                 string requesttype = "REPLACEMENT";//for replacement
                 string areaid = area_list.SelectedValue.ToString();
                 string branchid = "0";
                 string servedby = txtInstalledBy.Text;
                 string period = bll.GetBillingPeriod(areaid);
                

                string res = bll.LogMeterRequest(custref, meterref, oldtype, oldsize,olddials, prevrdg, serial, prerdgdt, appfnlrdg, appfnlrdgdt, isestimated,
                   consumption, reason, newserial, newsize, newmake, newdials, manufacturedt, newlife, comment, appinitialrdg, appinitedgdt,
                   createdby, requesttype, areaid, branchid, period,servedby);
                string str = "";
                if (res.Contains("success"))
                {
                    str = "Meter replacement against customer-" + custref + " successfully submited for approval";
                    DisplayMessage(str, false);
                }
                else
                {
                    str = "Meter replacement against customer-" + custref + " failed to be submitted for approval";
                    DisplayMessage(str, true);
                }
                RefreshReplacementControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshReplacementControls()
        {
            txtmeterefrep.Text = "";
            txtmakerep.Text = "";
            txtsizerep.Text = "";
            txtprerdgdtrep.Text = "";
            txtserialrep.Text = "";
            txtprerdgdtrep.Text = "";
            txtrdgreplace.Text = "";
            txtrdgdtreplace.Text = "";
            chkreplace.Checked = false;
            txtconsumptionrep.Text = "";
            cboReasonrep.SelectedValue = "0";
            txtdialsrep.Text = "";
            //newdetails
            txtNewSerial.Text = "";
            cboMeterSize.SelectedValue = "0";
            cboType2.SelectedValue = "0";
            txtnewdials.Text = "";
            txtManufacturedDate.Text = "";
            txtnewliferep.Text = "";
            txtcommentrep.Text = "";
            txtNewReading.Text = "";
            txtNewRdgDate.Text = "";
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            meterapproval.Visible = true;
            searchdisplay.Visible = false;
            LoadApprovalRequests();
            DisplayMessage(".", true);
        }

        private void LoadApprovalRequests()
        {
            meterapproval.Visible = true;
            btninventory.Visible = false;
            btnservicing.Visible = false;
            btnreplacement.Visible = false;
            //btntransfer.Visible = false;
            btnapprove.Visible = true;
            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
            string custref = txtcustref.Text.Trim();

            DataTable dataTable = bll.GetRequestsToApprove(countryid, areaid, custref);
            if (dataTable.Rows.Count > 0)
            {
                gv_approvalview.DataSource = dataTable;
                gv_approvalview.DataBind();
                DisplayMessage(".", true);
                //customerdisplay.Visible = true;
            }
            else
            {
                
                string error = "100: " + "No records found";
                bll.Log("LoadApprovalRequests", error);
                DisplayMessage(error, true);
                //customerdisplay.Visible = false;
            }
        }

        protected void btnreturnreplace_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            searchdisplay.Visible = true;
            DisplayMessage(".", true);
        }
        private void LoadPipeSizeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetPipeDiameterList();
                cboMeterSize.DataSource = dt;

                cboMeterSize.DataTextField = "diameter";
                cboMeterSize.DataValueField = "diameterId";
                cboMeterSize.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadPipeSizeList", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboMeterSize_DataBound(object sender, EventArgs e)
        {
            cboMeterSize.Items.Insert(0, new ListItem("- - select Diameter - -", "0"));
        }
        protected void cboType2_DataBound(object sender, EventArgs e)
        {
            cboType2.Items.Insert(0, new ListItem("- - select meter type - -", "0"));
        }
        private void LoadMeterTypes_rep()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterTypeList();
                cboType2.DataSource = dt;//meterTypeId,meterName

                cboType2.DataTextField = "meterName";
                cboType2.DataValueField = "meterTypeId";
                cboType2.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadMeterTypes_rep", error);
                DisplayMessage(error, true);
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                string requesttype = txtConfirmreqtype.Text.Trim();
                string requestid = txtconfirmid.Text.Trim();
                string Action = cboaction.SelectedItem.ToString();
                string Actionid = cboaction.SelectedValue.ToString();
                string MeterRef = txtConfirmMeterRef.Text.Trim();
                string custRef = txtconfirmcustref.Text.Trim();
                string Serial = txtconfirmnewserial.Text.Trim();
                string oldReading = txtConfirmPreReading.Text.Trim();
                string OldRdgDate = txtConfirmPreRdgDate.Text.Trim();
                string CurReading = txtConfirmCurReading.Text.Trim();
                string CurRdgDate = txtConfirmCurRdgDate.Text.Trim();
                string NewReading = txtconfirmnewreading.Text.Trim();
                string NewRdgDate = txtconfirminstalldate.Text.Trim();
                string finalconsumption = txtConfirmConsumption.Text.Trim();
                
                string Reason = txtConfirmreason.Text.Trim();
                string requestercomment = txtconfirmcomment.Text;
                string approvercomment = txtapprovercomment.Text;
                string dials = txtconfirmnewdials.Text.Trim();
                string InstalledBy = txtconfirminstalledby.Text.Trim();
                string Size = txtConfirmSize.Text.Trim();
                string Type = txtconfirmnewmake.Text;
                //get ids
                Size = lblsizeid.Text;
                Type = lbltypeid.Text;                
                string Life = txtconfirmnewlife.Text.Trim();
                string Area = lblarea.Text;
                string Branch = lblbranch.Text;
                string period = lblperiod.Text;
                bool isestimated = chkConfirmEstimated.Checked;
                string createdBy = Session["UserID"].ToString();
                string ManufacturedDate = txtconfirmmanufacturedate.Text.Trim();
                // string period = bll.GetBillingPeriod(Area);
                string suppressioncode = "1";//active
                string str = "";
                bool iscompleted = false;
                if (Actionid.Equals("0"))
                {
                    str = "Please select a required action for approval.";
                    DisplayMessage(str, true);
                }
                else
                {
                    if(Actionid.Equals("2"))//rejected
                    {
                        //log status
                        bll.UpdateMeterRequestStatus(custRef, requestid, Action, approvercomment, createdBy,iscompleted);
                        str = "Meter action-" + requesttype + " rejected against customer-" + custRef;
                        DisplayMessage(str, false);

                    }
                    else//accepted
                    {
                        string res = bll.ModifyMeter(Action, requesttype, MeterRef, custRef, Serial, oldReading, OldRdgDate, CurReading,
                  CurRdgDate, isestimated, NewReading, dials, InstalledBy, CurRdgDate, Type, Size, Life, ManufacturedDate, Reason, Area, Branch,
                  createdBy, period, finalconsumption, suppressioncode, approvercomment, requestid);

                        if (res.Contains("success"))
                        {
                            iscompleted = true;
                            bll.UpdateMeterRequestStatus(custRef, requestid, Action, approvercomment, createdBy, iscompleted);
                            LoadApprovalRequests();
                            str = "Meter action-" + requesttype + " approved and completed against customer-" + custRef;
                            DisplayMessage(str, false);
                        }
                        else
                        {
                            str = "Meter action-" + requesttype + " failed against customer-" + custRef;
                            DisplayMessage(str, true);
                        }
                    }
                   
                    RefreshConfirmControls();
                    //LoadApprovalRequests();
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshConfirmControls()
        {
            txtConfirmreqtype.Text="";
            cboaction.SelectedValue="0";
            txtConfirmMeterRef.Text="";
            txtconfirmcustref.Text="";
            txtconfirmnewserial.Text="";
            txtConfirmPreReading.Text="";
            txtConfirmPreRdgDate.Text="";
            txtConfirmCurReading.Text="";
            txtConfirmCurRdgDate.Text="";
           txtconfirmnewreading.Text="";
            txtconfirminstalldate.Text="";
           txtConfirmreason.Text="";
            txtconfirmcomment.Text="";
            txtapprovercomment.Text="";
            txtconfirmnewdials.Text="";
           txtconfirminstalledby.Text="";
           txtConfirmSize.Text="";
            txtconfirmnewmake.Text="";
           txtconfirmnewlife.Text="";
            txtconfirmid.Text = "";
            txtConfirmSerial.Text = "";
            txtConfirmConsumption.Text = "";
            txtConfirmOldDials.Text = "";
            txtConfirmType.Text = "";
            txtconfirmnewsize.Text = "";
            txtconfirmmanufacturedate.Text = "";
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            confirmdisplay.Visible = false;
            DisplayMessage(".", true);
        }
        protected void gv_approvalview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string type = e.Row.Cells[6].Text;
                // e.Row.BackColor = Color.Blue;
                //e.Row.ForeColor = Color.Green;

            }
        }
        protected void gv_approvalview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_approvalview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_approvalview.Rows[index].Cells[1].Text;


            }
        }
        protected void gv_approvalview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string custref = "";
            string flag = "";
            string recordid = "";string name = "";
            if (e.CommandName == "RowApprove")
            {
               // custref = Convert.ToString(e.CommandArgument.ToString());
                string[] arg = new string[3];
                arg = e.CommandArgument.ToString().Split(';');
                 custref = arg[0];
                 recordid = arg[1];
                name = arg[2];
            }
           
            LoadApproverDisplay(custref,recordid,name);

        }

        private void LoadApproverDisplay(string custref,string recordid,string name)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            meterapproval.Visible = true;
            DisplayMessage(".", true);
            LoadCustomerRequests(custref,recordid,name);
        }

        private void LoadCustomerRequests(string custref,string recordid,string name)
        {
            try
            {
               
                DataTable dTable = bll.GetApproverRequestByID(custref, recordid);
                if (dTable.Rows.Count > 0)
                {
                    confirmdisplay.Visible = true;
                    txtconfirmcustref.Text = dTable.Rows[0]["custref"].ToString();
                    txtConfirmMeterRef.Text = dTable.Rows[0]["meterref"].ToString();
                    txtConfirmreqtype.Text = dTable.Rows[0]["requestType"].ToString();
                    txtConfirmSerial.Text = dTable.Rows[0]["serial"].ToString();
                    txtConfirmOldDials.Text = dTable.Rows[0]["olddials"].ToString();
                    txtConfirmType.Text = dTable.Rows[0]["oldmake"].ToString();
                    txtConfirmSize.Text = dTable.Rows[0]["oldsize"].ToString();
                    

                    txtConfirmPreReading.Text = dTable.Rows[0]["prevrdg"].ToString();
                    txtConfirmPreRdgDate.Text = dTable.Rows[0]["prerdgdate"].ToString();
                    txtConfirmCurReading.Text = dTable.Rows[0]["appfnlrdg"].ToString();
                    txtConfirmCurRdgDate.Text = dTable.Rows[0]["appfnlrdgdate"].ToString();
                    txtConfirmConsumption.Text = dTable.Rows[0]["consumption"].ToString();
                    txtconfirmnewserial.Text = dTable.Rows[0]["newserial"].ToString();
                    txtconfirmnewsize.Text = dTable.Rows[0]["newsize"].ToString();
                    txtconfirmnewmake.Text = dTable.Rows[0]["newmake"].ToString();
                    txtconfirmnewdials.Text = dTable.Rows[0]["newdials"].ToString();
                    txtconfirmmanufacturedate.Text = dTable.Rows[0]["manufactureDate"].ToString();
                    txtconfirmnewlife.Text = dTable.Rows[0]["newlife"].ToString();
                    txtconfirmnewreading.Text = dTable.Rows[0]["appinitialrdg"].ToString();
                    txtconfirminstalldate.Text = dTable.Rows[0]["appinitrdgdate"].ToString();
                    txtconfirminstalledby.Text = dTable.Rows[0]["servedBy"].ToString();
                    txtConfirmreason.Text = dTable.Rows[0]["reason"].ToString();
                    txtconfirmcomment.Text = dTable.Rows[0]["comment"].ToString();
                    lblarea.Text = dTable.Rows[0]["areaId"].ToString();
                    lblbranch.Text = dTable.Rows[0]["branchId"].ToString();
                    lblperiod.Text = dTable.Rows[0]["period"].ToString();
                    string isestimate = dTable.Rows[0]["isestimated"].ToString();
                    if(isestimate.Equals("1") || isestimate.Equals("True"))
                    {
                        chkConfirmEstimated.Checked = true;
                    }
                    txtconfirmid.Text = recordid;
                    //d.diameterId,T.meterTypeId
                    string size = dTable.Rows[0]["diameterId"].ToString();
                    string type = dTable.Rows[0]["meterTypeId"].ToString();
                    if(size.Equals(""))
                    {
                        lblsizeid.Text = "0";
                    }
                    else
                    {
                        lblsizeid.Text = size;
                    }
                    if (type.Equals(""))
                    {
                        lbltypeid.Text = "0";
                    }
                    else
                    {
                        lbltypeid.Text = type;
                    }
                    lblapproval.Text = name + "-->" + custref;

                }
                else
                {
                    DisplayMessage("No records found", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            searchdisplay.Visible = true;
            DisplayMessage(".", true);
            lblcustref.Text = "0";
        }
    }
}