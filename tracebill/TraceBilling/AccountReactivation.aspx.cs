using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.Drawing;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using System.Threading;

namespace TraceBilling
{
    public partial class AccountReactivation : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        DataFile df = new DataFile();
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
                    LoadDisplay();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Account Reactivation");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDisplay()
        {
            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
            string custref = txtcustref.Text.Trim();

            DataTable dataTable = bll.LoadCustomerDisplay(countryid, areaid, custref, 1);
            if (dataTable.Rows.Count > 0)
            {

                gv_customerview.DataSource = dataTable;
                gv_customerview.DataBind();
                DisplayMessage(".", true);
                customerdisplay.Visible = true;
            }
            else
            {
                string error = "100: " + "No records found";
                bll.Log("LoadDisplayCustomer", error);
                DisplayMessage(error, true);
                customerdisplay.Visible = false;
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            LoadDisplay();
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
                e.Row.ForeColor = Color.Green;

            }
        }
        protected void gv_customerview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_customerview_RowCommand(object sender, GridViewCommandEventArgs e)
        {


            if (e.CommandName == "RowView")
            {
                string custref = Convert.ToString(e.CommandArgument.ToString());
                //string str = "Sorry, Application Foam print out not available yet!!!";
                // DisplayMessage(returned, true);
                lblcustref.Text = custref;
                LoadCustomerDetails(custref);
            }
        }

        private void LoadCustomerDetails(string custref)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = true;
            readingdisplay.Visible = false;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            btnreturn.Visible = true;
            LoadCustomerInformation(custref);
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

        protected void btncustdetails_Click(object sender, EventArgs e)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = true;
            readingdisplay.Visible = false;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            LoadCustomerInformation(custref);
        }

        protected void btnreadingdetails_Click(object sender, EventArgs e)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = false;
            readingdisplay.Visible = true;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            //LoadAreaList3(int.Parse(countryid));
            string areaid = Session["customerareaid"].ToString();
            //LoadBranchList1(int.Parse(areaid));
            onebyonedisplay.Visible = true;
            bulkdisplay.Visible = false;
            LoadCustomerDisplayLogs(custref, 3);
        }

        protected void btnenterlastreading_Click(object sender, EventArgs e)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = false;
            readingdisplay.Visible = true;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            LoadCustomerDisplayLogs(custref, 3);
        }

        protected void btnbilldetails_Click(object sender, EventArgs e)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = false;
            readingdisplay.Visible = false;
            billdisplay.Visible = true;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            LoadCustomerDisplayLogs(custref, 4);
        }

        protected void btntransactiondetails_Click(object sender, EventArgs e)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = false;
            readingdisplay.Visible = false;
            billdisplay.Visible = false;
            transactiondisplay.Visible = true;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            LoadCustomerDisplayLogs(custref, 5);
        }

        protected void btnpaymentdetails_Click(object sender, EventArgs e)
        {
            btnlinks.Visible = true;
            custdisplay.Visible = false;
            readingdisplay.Visible = false;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = true;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            LoadCustomerDisplayLogs(custref, 6);
        }
        private void LoadCustomerInformation(string custref)
        {
            try
            {
                int countryid = 0;
                int areaid = 0;
                DataTable dataTable = bll.LoadCustomerDisplay(countryid, areaid, custref, 2);
                string customertype = dataTable.Rows[0]["typeName"].ToString();
                if (customertype.ToLower().Contains("paid"))
                {
                    dataTable = bll.GetCustomerDetailsByIDMetered(countryid, areaid, custref, 2);
                    if (dataTable.Rows.Count > 0)
                    {
                        Session["customerareaid"] = dataTable.Rows[0]["areaId"].ToString();
                        area_list3.Text = dataTable.Rows[0]["Area"].ToString();
                        txtInquireCustRef.Text = dataTable.Rows[0]["customerRef"].ToString();
                        txtappnumber.Text = dataTable.Rows[0]["applicationId"].ToString();
                        txtcreationdate.Text = dataTable.Rows[0]["creationDate"].ToString();
                        txtcustomer.Text = dataTable.Rows[0]["customerRef"].ToString();
                        txtMeterRef.Text = dataTable.Rows[0]["meterRef"].ToString();
                        txtcustname.Text = dataTable.Rows[0]["customerName"].ToString();
                        txtcontact.Text = dataTable.Rows[0]["phoneNo1"].ToString();
                        txtbalance.Text = dataTable.Rows[0]["outstandingBalance"].ToString();
                        txtcategory.Text = dataTable.Rows[0]["categoryName"].ToString();
                        txtarea.Text = dataTable.Rows[0]["Area"].ToString();
                        txtaddress.Text = dataTable.Rows[0]["Address"].ToString();
                        txtcusttype.Text = dataTable.Rows[0]["typeName"].ToString();
                        txtemail.Text = dataTable.Rows[0]["custEmail"].ToString();
                        txtlatitude.Text = dataTable.Rows[0]["latitude"].ToString();
                        txtlongitude.Text = dataTable.Rows[0]["longitude"].ToString();
                        txtmetermake.Text = dataTable.Rows[0]["meterName"].ToString();
                        txtmeterNumber.Text = dataTable.Rows[0]["meterNumber"].ToString();
                        txtmetersize.Text = dataTable.Rows[0]["diameter"].ToString();
                        txtproperty.Text = dataTable.Rows[0]["propertyRef"].ToString();
                        txttariff.Text = dataTable.Rows[0]["tarrifName"].ToString();
                        txtterritory.Text = dataTable.Rows[0]["territory"].ToString();
                        txtzone.Text = dataTable.Rows[0]["Branch"].ToString();
                        chksewer.Checked = Convert.ToBoolean(dataTable.Rows[0]["IsSewer"].ToString());
                        chkclosed.Checked = Convert.ToBoolean(dataTable.Rows[0]["closed"].ToString());
                        string constatus = dataTable.Rows[0]["disconnectionId"].ToString();
                        if (constatus.Equals("1"))
                        {
                            chkactive.Checked = true;
                        }
                        lblapplicant.Text = txtcustname.Text + "-->" + txtcustomer.Text;
                    }
                    else
                    {
                        string error = "100: " + "No records found";
                        bll.Log("LoadCustomerInformation", error);
                        DisplayMessage(error, true);
                        customerdisplay.Visible = false;
                    }
                }
                else
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        Session["customerareaid"] = dataTable.Rows[0]["areaId"].ToString();
                        area_list3.Text = dataTable.Rows[0]["Area"].ToString();
                        txtInquireCustRef.Text = dataTable.Rows[0]["customerRef"].ToString();
                        txtappnumber.Text = dataTable.Rows[0]["applicationId"].ToString();
                        txtcreationdate.Text = dataTable.Rows[0]["creationDate"].ToString();
                        txtcustomer.Text = dataTable.Rows[0]["customerRef"].ToString();
                        txtMeterRef.Text = dataTable.Rows[0]["meterRef"].ToString();
                        txtcustname.Text = dataTable.Rows[0]["customerName"].ToString();
                        txtcontact.Text = dataTable.Rows[0]["phoneNo1"].ToString();
                        txtbalance.Text = dataTable.Rows[0]["outstandingBalance"].ToString();
                        txtcategory.Text = dataTable.Rows[0]["categoryName"].ToString();
                        txtarea.Text = dataTable.Rows[0]["Area"].ToString();
                        txtaddress.Text = dataTable.Rows[0]["Address"].ToString();
                        txtcusttype.Text = dataTable.Rows[0]["typeName"].ToString();
                        txtemail.Text = dataTable.Rows[0]["custEmail"].ToString();
                        txtlatitude.Text = dataTable.Rows[0]["latitude"].ToString();
                        txtlongitude.Text = dataTable.Rows[0]["longitude"].ToString();
                        txtmetersize.Text = dataTable.Rows[0]["diameter"].ToString();
                        txtproperty.Text = dataTable.Rows[0]["propertyRef"].ToString();
                        txttariff.Text = dataTable.Rows[0]["tarrifName"].ToString();
                        txtterritory.Text = dataTable.Rows[0]["territory"].ToString();
                        txtzone.Text = dataTable.Rows[0]["Branch"].ToString();
                        chksewer.Checked = Convert.ToBoolean(dataTable.Rows[0]["IsSewer"].ToString());
                        chkclosed.Checked = Convert.ToBoolean(dataTable.Rows[0]["closed"].ToString());
                        string constatus = dataTable.Rows[0]["disconnectionId"].ToString();
                        if (constatus.Equals("0"))
                        {
                            chkactive.Checked = true;
                        }
                        lblapplicant.Text = txtcustname.Text + "-->" + txtcustomer.Text;
                    }
                    else
                    {
                        string error = "100: " + "No records found";
                        bll.Log("LoadCustomerInformation", error);
                        DisplayMessage(error, true);
                        customerdisplay.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }


        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            LoadDisplay();
            btnlinks.Visible = false;
            custdisplay.Visible = false;
            readingdisplay.Visible = false;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            lblcustref.Text = "0";
        }
        private void LoadCustomerDisplayLogs(string custref, int flag)
        {
            DataTable dt = new DataTable();
            int countryid = 0;
            int areaid = 0;
            string str = "";
            try
            {
                if (flag == 3)//reading
                {

                    dt = bll.LoadCustomerDisplay(countryid, areaid, custref, flag);
                    if (dt.Rows.Count > 0)
                    {
                        gvreadingdisplay.DataSource = dt;
                        gvreadingdisplay.DataBind();
                        DisplayMessage(".", true);
                        capturereading.Visible = true;
                    }
                    else
                    {
                        // gvreadingdisplay.Visible = false;
                        str = "No reading records found against Customer-" + custref;
                        DisplayMessage(str, true);
                    }

                }
                else if (flag == 4)//bills
                {
                    capturereading.Visible = false;
                    dt = bll.LoadCustomerDisplay(countryid, areaid, custref, flag);
                    if (dt.Rows.Count > 0)
                    {
                        gvbilldisplay.DataSource = dt;
                        gvbilldisplay.DataBind();
                        DisplayMessage(".", true);
                    }
                    else
                    {
                        //gvbilldisplay.Visible = false;
                        str = "No bill records found against Customer-" + custref;
                        DisplayMessage(str, true);
                    }
                }
                else if (flag == 5)//transactions
                {
                    dt = bll.LoadCustomerDisplay(countryid, areaid, custref, flag);
                    if (dt.Rows.Count > 0)
                    {
                        gvtransdisplay.DataSource = dt;
                        gvtransdisplay.DataBind();
                        DisplayMessage(".", true);
                    }
                    else
                    {

                        // gvtransdisplay.Visible = false;
                        str = "No transaction records found against Customer-" + custref;
                        DisplayMessage(str, true);
                    }
                }
                if (flag == 6)//payments
                {
                    dt = bll.LoadCustomerDisplay(countryid, areaid, custref, flag);
                    if (dt.Rows.Count > 0)
                    {
                        gvpaymentdisplay.DataSource = dt;
                        gvpaymentdisplay.DataBind();
                        DisplayMessage(".", true);
                    }
                    else
                    {
                        //gvpaymentdisplay.Visible = false;
                        str = "No payment records found against Customer-" + custref;
                        DisplayMessage(str, true);
                    }
                }


            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadCustomerDisplayLogs", error);
                DisplayMessage(error, true);
            }
        }

        //protected void rdgoptions_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string countryid = country_list.SelectedValue.ToString();//Session["countryId"].ToString();
        //        string areaid = Session["customerareaid"].ToString();//area_list3.SelectedValue.ToString();//Session["areaId"].ToString();
        //        if (rdgoptions.SelectedValue.ToString() == "1")
        //        {

        //            //LoadAreaList3(int.Parse(countryid));
        //            LoadBranchList1(int.Parse(areaid));
        //            onebyonedisplay.Visible = true;
        //            bulkdisplay.Visible = false;
        //        }
        //        else if (rdgoptions.SelectedValue.ToString() == "2")
        //        {

        //            onebyonedisplay.Visible = false;
        //            bulkdisplay.Visible = true;
        //        }
        //        else
        //        {
        //            //LoadCustimaFileMeterReaders();
        //            //MultiView3.ActiveViewIndex = 5;
        //        }
        //        DisplayMessage(".", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        DisplayMessage(ex.Message, true);
        //    }
        //}

        //private void LoadBranchList1(int areaid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetBranchList(areaid);
        //        branch_list1.DataSource = dt;
        //        branch_list1.DataTextField = "branchName";
        //        branch_list1.DataValueField = "branchId";
        //        branch_list1.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayBranchList", error);
        //        DisplayMessage(error, true);
        //    }
        //}

        protected void area_list3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string areaid = Session["customerareaid"].ToString();//area_list3.SelectedValue.ToString();
                txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
                //LoadMeterReaders(areaid, "11");
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private void LoadMeterReaders(string areaid, string roleid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetSystemUserByRole(areaid, roleid);
        //        reader_list.DataSource = dt;
        //        reader_list.DataTextField = "fullName";
        //        reader_list.DataValueField = "userID";
        //        reader_list.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayReaderList", error);
        //        DisplayMessage(error, true);
        //    }
        //}

        protected void btnInquire_Click(object sender, EventArgs e)
        {
            if (country_list.SelectedValue.ToString() == "0")
            {
                DisplayMessage("Please Select a Country", true);
            }
            //else if (area_list3.SelectedValue.ToString() == "0")
            //{
            //    DisplayMessage("Please Select an Area", true);
            //}
            else
            {
                string custref = txtInquireCustRef.Text.Trim();
                string propertyref = txtInquirePropRef.Text;
                string period = txtcurrentperiod.Text;
                string areaid = Session["customerareaid"].ToString();//area_list3.SelectedValue.ToString();
                string branchid = "";//branch_list1.SelectedValue.ToString();
                resp = bll.ValidateReadingInquiry(custref, propertyref, areaid);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    DataTable dTable = bll.GetLatestBilledReading(custref, areaid, branchid);
                    if (dTable.Rows.Count > 0)
                    {
                        txtPreReading.Text = dTable.Rows[0]["CurReading"].ToString();
                        DateTime CurReadingDate = Convert.ToDateTime(dTable.Rows[0]["CurReadingDate"].ToString());
                        txtPreReadDate.Text = CurReadingDate.ToString("dd/MM/yyyy");
                        txtConsumption.Text = dTable.Rows[0]["Consumption"].ToString();
                        txtAvgConsumption.Text = dTable.Rows[0]["AvgConsumption"].ToString();
                        txtIsBilled.Text = dTable.Rows[0]["Billed"].ToString();
                        txtType.Text = dTable.Rows[0]["readingType"].ToString();
                        //txtCustName.Text = dTable.Rows[0]["customerName"].ToString();
                        txtMeterRef.Text = dTable.Rows[0]["meterRef"].ToString();
                        txtPropRef.Text = dTable.Rows[0]["propertyRef"].ToString();
                        txtdials.Text = dTable.Rows[0]["dials"].ToString();
                        LoadFieldComments();
                        //lblDials.Text = dTable.Rows[0]["Dials"].ToString();
                        // lblDials.Text = data.GetMeterDials(CustRes.MeterRef, Cust.AreaID);//sas
                    }
                    else
                    {
                        txtPreReading.Text = "0";
                        DateTime CurReadingDate = DateTime.Now;
                        txtPreReadDate.Text = CurReadingDate.ToString("MMMM dd, yyyy");
                        txtConsumption.Text = "0";
                        txtAvgConsumption.Text = "0";
                        txtIsBilled.Text = "YES";
                        txtType.Text = "NEW CONN";
                        if (String.IsNullOrEmpty(txtAvgConsumption.Text.Trim()))
                            txtAvgConsumption.Text = "0";
                        // lblDials.Text = "7";
                    }
                    DisplayMessage(".", true);
                    btnSave.Visible = true;
                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }
            }
        }

        private void LoadFieldComments()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetFieldComments();
                comment_list.DataSource = dt;//Code,comment
                comment_list.DataTextField = "comment";
                comment_list.DataValueField = "Code";
                comment_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCommentList", error);
                DisplayMessage(error, true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReadingObj read = new ReadingObj();

                string meterref = txtMeterRef.Text.Trim();
                string custref = txtInquireCustRef.Text.Trim();
                string reading = txtReading.Text.Trim();
                string readingdate = txtReadDate.Text.Trim();
                string reader = Session["Username"].ToString();//reader_list.SelectedItem.ToString();
                string otherreader = txtotherReader.Text.Trim();
                string comment = comment_list.SelectedValue.ToString();
                string prereading = txtPreReading.Text.Trim();
                string prereadingdate = txtPreReadDate.Text.Trim();
                string createdby = Session["UserID"].ToString();
                DateTime readingdt = Convert.ToDateTime(readingdate);
                DateTime prereadingdt = bll.GetDate(prereadingdate);
                if (reading == "")
                {
                    DisplayMessage("Please enter current reading", true);
                }
                else if (readingdate == "")
                {
                    DisplayMessage("Please enter current reading date", true);
                }

                else if (!bll.IsValidReadingDate(readingdate))
                {
                    string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                    DisplayMessage("Invalid Reading Date, It cannot be greater than Today ( " + Todate + " )", true);
                }
                else if (!bll.IsValidDateComparison(prereadingdt, readingdt))
                {
                    string Todate = prereadingdt.ToString("dd/MM/yyyy");
                    DisplayMessage("Current Reading Date cannot be less than Previous Date ( " + Todate + " )", true);
                }
                else
                {
                    //save details
                    read = new ReadingObj();
                    read.CustRef = custref;
                    read.MeterRef = meterref;
                    read.Type = "PERIODIC";
                    read.Method = "M";
                    read.LevelID = 0;
                    read.CurReading = int.Parse(reading);
                    read.CurReadingDate = readingdt;
                    read.Estimated = chkEstimate.Checked;
                    read.PreReading = int.Parse(prereading);
                    read.PreReadingDate = prereadingdt;
                    read.Consumption = read.CurReading - read.PreReading;
                    read.Reader = reader;
                    read.Comment = comment;
                    read.Billed = false;
                    read.Period = txtcurrentperiod.Text;
                    read.Area = Session["customerareaid"].ToString();//area_list3.SelectedValue.ToString();
                    read.Branch = "";//branch_list1.SelectedValue.ToString();
                    read.CreatedBy = int.Parse(createdby);
                    read.Latitude = "0";
                    read.Longitude = "0";

                    resp = bll.SaveReading(read);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with reading details against Customer(" + read.CustRef + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {

                        string str = " with reading details against Customer(" + read.CustRef + ") updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }

                    //clear conrols
                    ClearReadingControls();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearReadingControls()
        {
            txtPreReading.Text = "";
            txtPreReadDate.Text = "";
            txtConsumption.Text = "";
            txtAvgConsumption.Text = "";
            txtIsBilled.Text = "";
            txtType.Text = "";
            //txtCustName.Text = "";
            txtMeterRef.Text = "";
            txtPropRef.Text = "";
            txtInquireCustRef.Text = "";
            txtReading.Text = "";
            txtReadDate.Text = "";
            //reader_list.SelectedValue = "0";
            txtotherReader.Text = "";
            comment_list.SelectedValue = "0";

        }

        protected void comment_list_DataBound(object sender, EventArgs e)
        {
            comment_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }

        //protected void reader_list_DataBound(object sender, EventArgs e)
        //{
        //    reader_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        //}

        //protected void area_list3_DataBound(object sender, EventArgs e)
        //{
        //    area_list3.Items.Insert(0, new ListItem("- - select area - -", "0"));
        //}

        //protected void branch_list1_DataBound(object sender, EventArgs e)
        //{
        //    branch_list1.Items.Insert(0, new ListItem("- - None - -", "0"));
        //}
    }
}