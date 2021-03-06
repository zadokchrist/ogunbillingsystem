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
    public partial class ViewCustomers : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        DataFile df = new DataFile();
        PDFPrints pp = new PDFPrints();
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
                        
                        loadFilters();
                        LoadAreaControls();
                        //LoadDisplay();
                    }


                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        protected void loadFilters()
        {


            ddlcusttype.DataSource = bll.GetCustomerTypeList();
            ddlcusttype.DataBind();
            ddloperationarea.DataSource = bll.GetOperationAreaList(10);
            ddloperationarea.DataBind();
            ddlbranch.DataSource = bll.GetBranchList(10, 0);
            ddlbranch.DataBind();


        }
        private void LoadAreaControls()
        {
            ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea.Enabled = true;
           

        }
        private void LoadDisplay()
        {
            int countryid = 2;// Convert.ToInt16(country_list.SelectedValue.ToString());
            int areaid = 10;
            int opid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
            int custtype = Convert.ToInt16(ddlcusttype.SelectedValue.ToString());
            string custref = txtsearch.Text.Trim();
            int branch = Convert.ToInt16(ddlbranch.SelectedValue.ToString());
            string propref = txtpropertyref.Text.Trim();
            DataTable dataTable = bll.LoadCustomerDisplayFiltered(countryid, areaid, custref, 1,opid,custtype,branch,propref);
            Session["dtviewinc"] = dataTable;

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


        //private void LoadCountryList()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string countryid = Session["countryId"].ToString();
        //        dt = bll.GetCountryList();
        //        country_list.DataSource = dt;
        //        country_list.SelectedValue = countryid;
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
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        

        protected void Button3_Click(object sender, EventArgs e)
        {
            ResetDisplay();
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
               // e.Row.ForeColor = Color.Green;

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
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
            customerdisplay.Visible = false;
            string custref = lblcustref.Text;
            string custtype = lblcusttype.Text;
            if(custtype.Contains("Flat"))
            {
                readingdisplay.Visible = false;
            }
            else
            {
                readingdisplay.Visible = true;
                LoadCustomerDisplayLogs(custref, 3);

            }
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
            int countryid = 0;
            int areaid = 0;
            DataTable dataTable = bll.LoadCustomerDisplay(countryid, areaid, custref, 2);
            if (dataTable.Rows.Count > 0)
            {

                txtappnumber.Text = dataTable.Rows[0]["applicationId"].ToString();
                txtcreationdate.Text = dataTable.Rows[0]["creationDate"].ToString();
                txtcustomer.Text = dataTable.Rows[0]["customerRef"].ToString();
                //txtMeterRef.Text = dataTable.Rows[0]["meterRef"].ToString();
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
                //chksewer.Checked = Convert.ToBoolean(dataTable.Rows[0]["IsSewer"].ToString());
                chkclosed.Checked = Convert.ToBoolean(dataTable.Rows[0]["closed"].ToString());
                string constatus = dataTable.Rows[0]["disconnectionId"].ToString();
                if (constatus.Equals("0"))
                {
                    chkactive.Checked = true;
                }
                lblapplicant.Text = txtcustname.Text + "-->" + txtcustomer.Text;
                lblcusttype.Text = txtcusttype.Text;
            }
            else
            {
                string error = "100: " + "No records found";
                bll.Log("LoadCustomerInformation", error);
                DisplayMessage(error, true);
                customerdisplay.Visible = false;
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
        private void ResetDisplay()
        {
            btnlinks.Visible = false;
            custdisplay.Visible = false;
            readingdisplay.Visible = false;
            billdisplay.Visible = false;
            transactiondisplay.Visible = false;
            paymentdisplay.Visible = false;
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
        protected void gvbilldisplay_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowPrint")
            {
                string str = "";
                //string UserID = e.Item.Cells[0].Text;
                //string appid = Convert.ToString(e.CommandArgument.ToString());
                //string str = "Sorry, Application Foam print out not available yet!!!";
                string[] arg = new string[4];
                arg = e.CommandArgument.ToString().Split(';');
                string custref = arg[0];
                string billno = arg[1];
                string period = arg[2];
                string areaid = arg[3];
                PrintInvoice(custref, billno, period, areaid);
                //DisplayMessage(str, true);
            }

        }

        private void PrintInvoice(string custref, string billno, string period, string areaid)
        {
            try
            {
                string str = "";
                string res = pp.GetPDFBillFile(int.Parse(areaid), custref, period, int.Parse(billno));
                if (res.Contains("pdf"))
                {
                    str = "Bill Invoice-" + res + " generated successfully.";
                    DisplayMessage(str, false);
                }
                else
                {
                    DisplayMessage(str, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void gvbilldisplay_RowDataBound(object sender, GridViewRowEventArgs e)
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
        protected void gvbilldisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvbilldisplay_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gvbilldisplay.Rows[index].Cells[1].Text;


            }
        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("--all--", "0"));
        }
        protected void ddlcusttype_DataBound(object sender, EventArgs e)
        {
            ddlcusttype.Items.Insert(0, new ListItem("--all--", "0"));
        }
        protected void gv_customerview_PageIndexChanged(object sender, EventArgs e)
        {

        }
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("--all--", "0"));
        }

        protected void gv_customerview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gv_customerview.PageIndex = e.NewPageIndex;
            gv_customerview.DataSource = Session["dtviewinc"] as DataTable;
            gv_customerview.DataBind();
        }
        protected void ddloperationarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                int branchid = Convert.ToInt16(ddlbranch.SelectedValue.ToString());

                ddlbranch.DataSource = bll.GetBranchList(10, operationid);
                ddlbranch.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}