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
    public partial class BillingCycle : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        DataFile df = new DataFile();
        protected void Page_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {

                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadAreaList3(countryid);
                    LoadDisplay();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadCountryList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCountryList();
                country_list.DataSource = dt;

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
        private void LoadAreaList3(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetAreaList(countryid);
                area_list3.DataSource = dt;

                area_list3.DataTextField = "areaName";
                area_list3.DataValueField = "areaId";
                area_list3.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayAreaList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadBranchList1(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid);
                branch_list1.DataSource = dt;
                branch_list1.DataTextField = "branchName";
                branch_list1.DataValueField = "branchId";
                branch_list1.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayBranchList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadBlockMaps(string areaid, string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBlockMaps(areaid, branchid);
                block_list.DataSource = dt;

                block_list.DataTextField = "blockNumber";
                block_list.DataValueField = "blockID";
                block_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadBlockMaps", error);
                DisplayMessage(error, true);
            }
        }
        protected void area_list3_DataBound(object sender, EventArgs e)
        {
            area_list3.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
        protected void branch_list1_DataBound(object sender, EventArgs e)
        {
            branch_list1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void country_list_DataBound(object sender, EventArgs e)
        {
            country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        }
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
        protected void block_list_DataBound(object sender, EventArgs e)
        {
            block_list.Items.Insert(0, new ListItem("- - All blocks - -", "0"));
        }
        protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                LoadAreaList(countryid);
                LoadAreaList3(countryid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void area_list3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string areaid = area_list3.SelectedValue.ToString();
                txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
                string branchid = branch_list1.SelectedValue.ToString();
                LoadBlockMaps(areaid, branchid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void DisplayMessage(string message, Boolean isError)
        {
            lblmsg.Visible = true;
            lblmsg.Text =  message + ".";
            if (isError == true)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        private void LoadDisplay()
        {
            billschedule.Visible = true;
            viewrequests.Visible = false;
            string areaid = Session["areaId"].ToString();
            string user = Session["FullName"].ToString();
            txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
            txtuser.Text = user;
            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            LoadAreaList3(countryid);
            LoadBranchList1(0);
            //lblapplicant.Visible = false;
            //btnlinks.Visible = false;
            //LoadInvoiceDetails();
        }

        protected void btnschedulerequest_Click(object sender, EventArgs e)
        {

        }

        protected void btnviewrequest_Click(object sender, EventArgs e)
        {

        }

        protected void btnbillrequest_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateRequest();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void ValidateRequest()
        {
            try
            {
                string area = area_list3.SelectedValue.ToString();
                string branch = branch_list1.SelectedValue.ToString();
                string block = block_list.SelectedValue.ToString();
                string custref = txtcustref.Text.Trim();
                lblarea.Text = area_list3.SelectedItem.ToString();
                lblbranch.Text = branch_list1.SelectedItem.ToString();
                lblblock.Text = block_list.SelectedItem.ToString();
                lblperiod.Text = txtcurrentperiod.Text.Trim();
                lblcustref.Text = custref;
                if(area == "0")
                {
                    DisplayMessage("Please select an Area", true);
                }
                else
                {
                    if(!custref.Equals(""))
                    {
                        string err = "";
                        //1:check customer existing
                        if (bll.IsValidCustRefRefInArea(custref, area))
                        {
                            //check flat rate
                            if (bll.IsFlatRated(custref))//check flatrate
                            {
                                //verify further
                                VerifyRequest(area, branch, block, custref);
                            }
                            else
                            {
                                //2: check if billed last reading
                                DataTable dtread = bll.GetLatestReadingStatus(custref, area, branch);
                                if (dtread.Rows.Count > 0)
                                {

                                    //3:check bill status
                                    bool IsBilled = Convert.ToBoolean(dtread.Rows[0]["IsBilled"].ToString());
                                    if (!IsBilled)
                                    {
                                        //verify further
                                        VerifyRequest(area, branch, block, custref);
                                    }
                                    else
                                    {
                                        err = "No Unbilled reading found against customer-" + custref;
                                        DisplayMessage(err, true);
                                    }
                                }
                                else
                                {
                                    err = "No reading records found against customer-" + custref;
                                    DisplayMessage(err, true);
                                }
                            }

                        }
                        else
                        {
                            err = "Invalid customer-" + custref + " in selected area.";
                            DisplayMessage(err, true);
                        }


                    }
                    else
                    {
                        VerifyRequest(area, branch, block,custref);
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void VerifyRequest(string area, string branch,  string block, string custref)
        {
            try
            {
                requestsummary.Visible = true;
                billschedule.Visible = false;
                DisplaySummary();
                DisplayMessage(".", true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void DisplaySummary()
        {
            txtbillperiod.Text = lblperiod.Text;
            txtarea.Text = lblarea.Text;
            txtbranch.Text = lblbranch.Text;
            txtcustomerref.Text = lblcustref.Text;
            txtblock.Text = lblblock.Text;
            txttype.Text = bll.GetCustomerType(lblcustref.Text, "0", "0", "0");
            lblqn.Text = "Are you ready to bill this request?";
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                string Area = area_list3.SelectedValue.ToString();
                string Branch = branch_list1.SelectedValue.ToString();
                string block = block_list.SelectedValue.ToString();
                string CustRef = txtcustomerref.Text.Trim();
                string period = txtcurrentperiod.Text;
                bool BillNow = Convert.ToBoolean(chkBillRequestNow.Checked);
                DateTime ScheduleDate = DateTime.Now;
                if (chkBillRequestNow.Checked && !String.IsNullOrEmpty(txtscheduledate.Text.Trim()))
                {
                    string schDate = txtscheduledate.Text.Trim();
                   // string schTime = txtScheduleTime.Text.Trim();
                    ScheduleDate = Convert.ToDateTime(schDate);
                   // string[] ScheduleTime = txtScheduleTime.Text.Trim().Split(':');
                   // int Hour = Convert.ToInt32(ScheduleTime[0]); int Min = Convert.ToInt32(ScheduleTime[1]);
                   // ScheduleDate = new DateTime(ScheduleDate.Year, ScheduleDate.Month, ScheduleDate.Day, Hour, Min, 0);
                }
                //check flatrated
                string result = "";
                if(CustRef.Equals(""))//execute
                {
                    result = BillRequest(Area, Branch, block, CustRef, BillNow, ScheduleDate, period);
                }
                else
                {
                    //check flat rate
                    if (bll.IsFlatRated(CustRef))//check flatrate
                    {
                        result = BillRequest(Area, Branch, block, CustRef, BillNow, ScheduleDate, period);
                    }
                    else//check reading status
                    {
                        DataTable dtReadings = bll.GetAccountReading(CustRef, period, Area, Branch);
                        if (dtReadings.Rows.Count > 0)
                        {
                            result = BillRequest(Area, Branch, block, CustRef, BillNow, ScheduleDate, period);
                        }
                        else
                        {
                            result = "Metered account has no unbilled reading for current period.";
                        }
                    }
                       
                }
                
                
                //DisplayBillResult(result);
                ClearControls();
                DisplayMessage(result, true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void ClearControls()
        {
            txtcustomerref.Text = "";
            block_list.SelectedValue = "0";
            requestsummary.Visible = false;
            billschedule.Visible = true;
        }

        private string BillRequest(string area, string branch, string block, string custRef, bool billNow, DateTime scheduleDate, string period)
        {
            string output = "";
            try
            {
                DataTable datatable = bll.GetBillDetails(area, branch, block, custRef);
                if(datatable.Rows.Count > 0)
                {
                    int Success = 0;
                    int failed = 0;
                    int total = 0;
                    string Msg = "";
                    //sas 10/12
                   
                    foreach (DataRow dr in datatable.Rows)
                    {
                        string CustRef = dr["customerRef"].ToString();
                        string MeterRef = dr["meterRef"].ToString();
                        string MeterSize = dr["meterSizeId"].ToString();
                        string PropRef = dr["propertyRef"].ToString();
                        string CustTarrif = dr["tarrifCode"].ToString();
                        int CustClass = Convert.ToInt16(dr["classId"].ToString());
                        int AreaID = Convert.ToInt16(dr["areaId"].ToString());
                        int BranchID = Convert.ToInt16(dr["branchId"].ToString());
                        string createdby = Session["UserID"].ToString();
                        string returned = BillAccount(CustRef, MeterRef, MeterSize, PropRef, CustTarrif, CustClass, AreaID, BranchID, createdby,period);

                        if (returned == "SUCCESS")
                        {
                            Success++;
                        }
                        else
                        {
                            failed++;
                           
                        }
                    }
                    total = Success + failed;
                    if (failed != 0 && Success != 0)
                    {
                        Msg = "Your Bill Request of " + total + " accounts has been Processed with " + Success + " successfully and " + failed + " failed";
                    }
                    else if (failed == 0)
                    {
                        Msg = "Your Bill Request of " + total + " accounts has been Processed Successfully";
                    }
                    else
                    {
                        Msg = "Your Bill Request of " + total + " accounts has failed";
                    }
                   
                    return Msg;

                }
                else
                {
                    DisplayMessage("No billing data found", true);
                }
                RefreshControls();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return output;
        }

        private void RefreshControls()
        {
            area_list3.SelectedValue = "0";
            txtcustref.Text = "";
            block_list.SelectedValue = "0";
            chkBillRequestNow.Checked = false;
            txtscheduledate.Text = "";
        }

        private string BillAccount(string custRef, string meterRef, string meterSize, string propRef, string custTarrif, int custClass, int areaID, int branchID, string createdBy,string period)
        {
            string output = "";
            CustomerObj cust = new CustomerObj();
            //BillInterfaceApi inter = new BillInterfaceApi();
            //BillCustomer Cust = new BillCustomer();
            cust.CustRef = custRef;
            cust.MeterRef = meterRef;
            cust.MeterSize = meterSize;
            cust.PropertyRef = propRef;
            cust.Tariff = custTarrif;
            cust.Classification = custClass.ToString();
            cust.Area = areaID.ToString();
            cust.Branch = branchID.ToString();
            cust.CreatedBy = createdBy;
            cust.Period = period;
            cust.BillDate = DateTime.Now;
            output = bll.ProcessBill(cust);
            //pass bill info
            return output;
        }

        protected void chkBillRequestNow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBillRequestNow.Checked)
            {
                txtscheduledate.Enabled = true;
            }
            else
            {
                txtscheduledate.Enabled = false;
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            requestsummary.Visible = false;
            billschedule.Visible = true;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {

        }
    }
}