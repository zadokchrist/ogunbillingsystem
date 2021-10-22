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
using System.Text;
using System.Collections;

namespace TraceBilling
{
    public partial class PaymentInvoice : System.Web.UI.Page
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
                    else
                    {
                        string sessioncountryid = Session["countryId"].ToString();

                        if (!sessioncountryid.Equals("1"))
                        {

                            LoadAreaList(int.Parse(sessioncountryid));
                            area_list.SelectedIndex = area_list.Items.IndexOf(new ListItem(Session["area"].ToString(), Session["areaId"].ToString()));
                            area_list.Enabled = false;
                            int operationid = Convert.ToInt16(area_list.SelectedValue.ToString());
                            // LoadBranchList(operationid);
                        }
                        else
                        {
                            //int countryid = int.Parse(country_list.SelectedValue.ToString());
                            int countryid = int.Parse(sessioncountryid);
                            LoadAreaList(countryid);
                        }
                        LoadInvoiceDetails();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Payment Invoicing of the customer page");

                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        //private void LoadCountryList()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetCountryList();
        //        country_list.DataSource = dt;

        //        country_list.DataTextField = "countryName";
        //        country_list.DataValueField = "countryId";
        //        country_list.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayCountryList", error);
        //        DisplayMessage(error, true);
        //    }
        //}
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
            lblmsg.Text = "MESSAGE: " + message + ".";
            if (isError == true)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }

        //protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
        //        LoadAreaList(countryid);
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        private void LoadInvoiceDetails()
        {
            try
            {
                string appnumber = txtappnumber.Text.Trim();
                string country = "2";
                string area = area_list.SelectedValue.ToString();
                string status = "";
                string roleid = Session["roleId"].ToString();
                if (roleid.Equals("2"))//commercial
                {
                    status = "7";
                }
                else if (roleid.Equals("3"))//mgr
                {
                    status = "6";
                }
                else if (roleid.Equals("8"))//revenue
                {
                    status = "8";
                }
                else if (roleid.Equals("6"))//engineer
                {
                    status = "6";
                }

                if (string.IsNullOrEmpty(country))
                {
                    country = "0";
                }
                else if (string.IsNullOrEmpty(area))
                {
                    area = "0";
                }
                DataTable dataTable = bll.GetInvoiceDetails(appnumber, int.Parse(country), int.Parse(area), int.Parse(status));
                if (dataTable.Rows.Count > 0)
                {
                    gv_surveyjobs.DataSource = dataTable;
                    gv_surveyjobs.DataBind();
                    DisplayMessage(".", true);
                    maindisplay.Visible = true;
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("LoadInvoiceDetails", error);
                    DisplayMessage(error, true);
                    maindisplay.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                LoadInvoiceDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInvoiceList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadInvoiceList()
        {
            maindisplay.Visible = true;
            btnreturn.Visible = true;
            generateinvoice.Visible = false;
            confirminvoice.Visible = false;
            lblapplicant.Visible = false;
            btnlinks.Visible = false;
            LoadInvoiceDetails();
        }

        protected void btngenerate_Click(object sender, EventArgs e)
        {
            generateinvoice.Visible = true;
            // approveinvoice.Visible = false;
            confirminvoice.Visible = false;
            reconcileinvoice.Visible = false;
            if (bll.CheckPaymentInvoice(lblApplicationCode.Text))
            {
                btninvoicegeneration.Visible = false;
                //btninvoicegeneration.ForeColor = Color.LightGray;
                txtDeposit.ReadOnly = true;
            }
            else
            {
                btninvoicegeneration.Visible = true;
                // btninvoicegeneration.ForeColor = Color.LightBlue;
                txtDeposit.ReadOnly = false;
            }
        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            generateinvoice.Visible = false;
            // approveinvoice.Visible = true;
            confirminvoice.Visible = false;
            reconcileinvoice.Visible = false;
        }

        protected void btnconfirm_Click(object sender, EventArgs e)
        {
            generateinvoice.Visible = false;
            // approveinvoice.Visible = false;
            confirminvoice.Visible = true;
            reconcileinvoice.Visible = false;
            LoadSlipConfirmation();
        }

        protected void btnreconcile_Click(object sender, EventArgs e)
        {
            generateinvoice.Visible = false;
            // approveinvoice.Visible = false;
            confirminvoice.Visible = false;
            reconcileinvoice.Visible = true;
            LoadReconcileControls();
        }

        private void LoadReconcileControls()
        {
            try
            {
                string appcode = lblApplicationCode.Text.Trim();
                string a = "", b = "", c = "", d = "", e = "", f = "";
                string str = "";
                DataTable dt = bll.GetInvoiceDetailsByAppNumber(appcode);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        //I.paymentRef,I.paymentCode,I.fullName,I.paymentDate,I.amountInvoiced
                        string paymentref = dr["paymentRef"].ToString();
                        string paymentcode = dr["paymentCode"].ToString();
                        double invoiced = Convert.ToDouble(dr["amountInvoiced"].ToString());
                        double paid = Convert.ToDouble(dr["totalPaid"].ToString());
                        if (paymentcode.Equals("NC"))
                        {
                            a = paymentref + ":" + invoiced.ToString();
                            b = paymentref + ":" + paid.ToString();
                        }
                        if (paymentcode.Equals("DP"))
                        {
                            c = paymentref + ":" + invoiced.ToString();
                            d = paymentref + ":" + paid.ToString();
                        }
                    }
                    lblInvoiced.Text = a + "\r\n" + c;
                    lblpaid.Text = b + "\r\n" + d;
                    txtinvoice1.Text = lblInvoiced.Text;
                    txtpaid1.Text = lblpaid.Text;
                    //ToString("#,##0");
                    double total = 0, total2 = 0;
                    //decimal total2 = 0;
                    foreach (DataRow dr1 in dt.Rows)
                    {
                        total += double.Parse(dr1["amountInvoiced"].ToString().Replace(",", ""));
                        total2 += double.Parse(dr1["totalPaid"].ToString().Replace(",", ""));
                        e = total.ToString("#,##0");
                        f = total2.ToString("#,##0");
                    }

                    if (total2 < total)
                    {
                        str = "Total amount paid: " + f + " is less than Total invoiced: " + e;
                    }
                    else
                    {
                        str = "Invoice fully paid";
                    }
                    lblremark.Text = str;
                    lblremark.Visible = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btninvoicegeneration_Click(object sender, EventArgs e)
        {
            //generate reference for each slip
            bool newcon = false;
            bool deposit = false;
            if (!txtNew.Equals(""))
            {
                newcon = true;
            }
            if (!txtDeposit.Equals(""))
            {
                deposit = true;
            }
            string strArray = bll.GetPaySlipsStringArray(newcon, deposit);
            if (strArray == "")
            {
                DisplayMessage("Please Select Payslip Type to Generate", true);
            }
            else
            {
                string[] array = strArray.Split(',');
                string appcode = lblApplicationCode.Text.Trim();
                GeneratePaySlips(array, appcode);
                // LoadSlipConfirmation();
            }
        }

        private void LoadSlipConfirmation()
        {
            try
            {
                string appcode = lblApplicationCode.Text.Trim();
                DataTable dt = bll.GetInvoiceDetailsByAppNumber(appcode);
                if (dt.Rows.Count > 0)
                {
                    DataGrid1.DataSource = dt;
                    DataGrid1.DataBind();
                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
        //    try
        //    {

        //        if (e.CommandName == "btnView")
        //        {
        //            //string ApplicationID = e.Item.Cells[1].Text;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        }

        public void PrintFoam(string paymentref, string areaid)
        {
            try
            {
                PDFPrints pp = new PDFPrints();
                //string referenceno = "26012021/234/210/0/1";//1:application no, 2:paymentref, 3:referenceno
                string flag = "1";
                string res = "";
                //string companyid = "2";
                string user = Session["FullName"].ToString();
                DataTable dt = bll.GetNonConsumptionInvoiceDetails(paymentref);//GetCustomerReportData(appnumber, flag);
                DataTable dtprofile = bll.GetCompanyProfile(areaid);
                if (dt.Rows.Count > 0)
                {
                    res = pp.GetInvoiceForm(dt, dtprofile, user);//GetPDFForm(dt, dtprofile, user);
                    //Console.WriteLine(res);
                    DisplayMessage(res, true);
                }
                else
                {
                    res = "Sorry, Application Foam print out not available yet!!!";
                    DisplayMessage(res, true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DataGrid2_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GeneratePaySlips(string[] array, string appcode)
        {
            try
            {
                string Code = "";
                string Msg = "";
                string returned = "";
                string createdby = Session["UserID"].ToString();
                //string ConnectionCode = lblConnectionID.Text.Trim();
                int i = 0;
                int count = 0;
                for (i = 0; i < array.Length; i++)
                {
                    Code = array[i].ToString();
                    if (Code != "")
                    {
                        string Amount = GetAmount(Code);
                        returned = bll.GenerateAdviceSlipRef(appcode, Amount, Code, createdby);
                        if (!returned.Contains("Successfully"))
                        {
                            Msg = returned;
                            //ShowMessage(Msg);
                            break;
                        }
                        else
                        {
                            count++;
                        }

                    }
                }
                if (count != 0)
                {
                    Msg = i + " Payslip(s) have been generated";
                    //take status log
                    string appid = lblappid.Text;
                    bll.LogApplicationTransactions(int.Parse(appid), 7, int.Parse(createdby));//generate invoice

                }
                else
                {
                    if (count == 0)
                    {
                        Msg = returned;
                    }
                }
                DisplayMessage(Msg, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal string GetAmount(string Code)
        {
            string output = "";
            if (Code == "NC")
            {
                output = txtgross.Text.Trim();

            }
            else if (Code == "DP")
            {
                output = txtDeposit.Text.Trim();

            }

            return output;
        }


        protected void btninvoiceapprove_Click(object sender, EventArgs e)
        {

        }

        protected void btninvoiceconfirm_Click(object sender, EventArgs e)
        {
            try
            {
                string appnumber = lblApplicationCode.Text;
                if (appnumber == "0")
                {
                    DisplayMessage("Sorry, there was a problem with application. Try again", true);
                }
                else
                {
                    dt = bll.GetInvoiceDetailsByAppNumber(appnumber);
                    if (dt.Rows.Count > 0)
                    {
                        ArrayList a = new ArrayList();//sucess
                        ArrayList b = new ArrayList();//fail
                        foreach (DataRow dr in dt.Rows)
                        {
                            string PaymentRef = dr["paymentRef"].ToString();
                            double invoiced = Convert.ToDouble(dr["amountInvoiced"].ToString());
                            double amountpaid = Convert.ToDouble(dr["amountPaid"].ToString());
                            double balance = Convert.ToDouble(dr["balance"].ToString());
                            string Confirmed = dr["IsConfirmed"].ToString();
                            string paid = dr["IsPaid"].ToString();

                            if (amountpaid >= invoiced)//update result
                            {
                                //check invoice status
                                a.Add(PaymentRef);

                            }
                            else
                            {
                                //display error
                                b.Add(PaymentRef);
                            }
                        }
                        if (a.Count > 0)
                        {
                            string appid = lblappid.Text;
                            string createdby = Session["UserID"].ToString();
                            //take log of paid..check paid invoices
                            //bll.LogApplicationTransactions(int.Parse(appid), 8, int.Parse(createdby));//payment reconciled
                            bll.LogApplicationTransactions(int.Parse(appid), 11, int.Parse(createdby));//send to field
                            DisplayMessage("Payment invoices confirmed successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Sorry, No payment made against payment invoices", true);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btninvoicereconcile_Click(object sender, EventArgs e)
        {

        }
        protected void gv_surveyjobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Get the Command Name.
            string commandName = e.CommandName;
            if (commandName == "btnSelect")//details
            {
                //execute
                //int index = e.CommandArgument;
                int index = Int32.Parse((string)e.CommandArgument);
                if (index >= 0)
                {
                    // dispatchdisplay.Visible = true;
                    string appnumber = gv_surveyjobs.Rows[index].Cells[2].Text;

                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    generateinvoice.Visible = true;
                    invoicedisplay.Visible = true;
                    ShowInvoiceDetails(appnumber);
                }
            }

        }

        private void ShowInvoiceDetails(string appnumber)
        {
            try
            {
                string status = "";
                string roleid = Session["roleId"].ToString();
                if (roleid.Equals("2"))//commercial
                {
                    status = "7";
                }
                else if (roleid.Equals("3"))//mgr
                {
                    status = "10";
                }
                else if (roleid.Equals("8"))//revenue
                {
                    status = "8";
                }
                else if (roleid.Equals("6"))
                {
                    status = "6";
                }

                DataTable dt = bll.GetInvoiceDetails(appnumber, 0, 0, int.Parse(status));
                if (dt.Rows.Count > 0)
                {

                    txtappcode.Text = dt.Rows[0]["ApplicationNumber"].ToString();
                    txtname.Text = dt.Rows[0]["ApplicantName"].ToString();
                    string jobno = dt.Rows[0]["JobNumber"].ToString();
                    lblappid.Text = dt.Rows[0]["ApplicationID"].ToString();
                    lblarea.Text = dt.Rows[0]["areaId"].ToString();
                    double New = Convert.ToDouble(dt.Rows[0]["Amount"].ToString());
                    double deposit = Convert.ToDouble(dt.Rows[0]["Deposit"].ToString());
                    double vat = Convert.ToDouble(dt.Rows[0]["Vat"].ToString());
                    txtNew.Text = New.ToString("#,##0");
                    txtDeposit.Text = deposit.ToString("#,##0");
                    // txtvat.Text = vat.ToString("#,##0");
                    //load controls
                    btnlinks.Visible = true;
                    string applicant = txtappcode.Text + "-->" + txtname.Text;
                    lblapplicant.Text = applicant;
                    lblApplicationCode.Text = txtappcode.Text;
                    double totalCharge = New + deposit + vat;
                    txtTotalFee.Text = totalCharge.ToString("#,##0");
                    double gross = New + vat;
                    txtgross.Text = gross.ToString("#,##0");
                    //check if invoice already generated
                    if (bll.CheckPaymentInvoice(lblApplicationCode.Text))
                    {
                        generateinvoice.Visible = false;
                        confirminvoice.Visible = true;
                        LoadSlipConfirmation();
                    }
                    else
                    {
                        generateinvoice.Visible = true;
                        confirminvoice.Visible = false;
                    }
                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btninvoicecancel_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInvoiceList();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btninvoicecancel2_Click(object sender, EventArgs e)
        {
            try
            {
                LoadInvoiceList();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btnreconcancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnreconexport_Click(object sender, EventArgs e)
        {

        }
        protected void txtDeposit_TextChanged(object sender, EventArgs e)
        {
            string newcon = txtNew.Text;
            string newconvat = txtgross.Text;
            string deposit = txtDeposit.Text;

            double totalCharge = double.Parse(newconvat) + double.Parse(deposit);
            txtTotalFee.Text = totalCharge.ToString("#,##0");
        }
    }
}