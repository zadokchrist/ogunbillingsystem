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
using RKLib.ExportData;


namespace TraceBilling
{
    public partial class Adjustments : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        private DataTable dtUpdate;
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
                    CreateAdjustmentsDataTable();
                    dtUpdate = (DataTable)Session["dtAdjustments"];
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
            adjustmentdisplay.Visible = true;

            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
            string custref = txtcustref.Text.Trim();

            DataTable dataTable = bll.LoadCustomerDisplay(countryid, areaid, custref, 1);
            if (dataTable.Rows.Count > 0)
            {
                txtCustName.Text = dataTable.Rows[0]["name"].ToString();
                txtcustomer.Text = dataTable.Rows[0]["custref"].ToString();
                txtPropRef.Text = dataTable.Rows[0]["propertyRef"].ToString();
                LoadTransactionCodes();
                DisplayMessage(".", true);
                //customerdisplay.Visible = true;
            }
            else
            {
               
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
        protected void cbotranscode_DataBound(object sender, EventArgs e)
        {
            cbotranscode.Items.Insert(0, new ListItem("- - select trans code - -", "0"));
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

        protected void btnadd_Click(object sender, EventArgs e)
        {
            string countryid = country_list.SelectedValue.ToString();
            string areaid = area_list.SelectedValue.ToString();
            string period= bll.GetBillingPeriod(areaid);
            string area = area_list.SelectedItem.ToString();            
            string branch = "None";
            string custref = txtcustomer.Text.Trim();
            string docno = txtDocNo.Text.Trim();
            string transcode = cbotranscode.SelectedValue.ToString();
            string amount = txtAmount.Text.Trim();
            string total = txtTotal.Text.Trim();
            string vat = "";
            string effectivedate = txtEffectiveDate.Text.Trim();
            string comment = txtcomment.Text.Trim();
            DataTable transTable = bll.GetTranscodeDetails(transcode);
            double VATAmountIntl = 0; double TotalIntl = 0;
            if (transTable.Rows.Count > 0)
            {
                string vatable = transTable.Rows[0]["vatIndicator"].ToString();
                string adjtype = transTable.Rows[0]["balanceType"].ToString();
                if (vatable.Equals("True"))
                {
                    vat = bll.GetVATAmount(countryid, amount);
                    VATAmountIntl = Convert.ToDouble(vat);
                    TotalIntl = (Convert.ToDouble(amount) + VATAmountIntl);
                    total = Convert.ToString(TotalIntl);

                }
                else
                {
                    vat = "0";
                    VATAmountIntl = Convert.ToDouble(vat);
                    TotalIntl = (Convert.ToDouble(amount) + VATAmountIntl);
                    total = Convert.ToString(TotalIntl);

                }
                if (adjtype == "CR")
                {
                    amount = "-" + amount;
                    vat = "-" + vat;
                    total = "-" + total;
                }

            }
            // dtUpdate.Rows.Add(new object[] { custref, area, branch, transcode, docno, effectivedate,  amount, vat, total,  comment });
           
            dtUpdate = (DataTable)Session["dtAdjustments"];
            DataRow dr = dtUpdate.NewRow();
         
            //dr["No."] = i + 1;
            dr["CustRef"] = custref;
            dr["Area"] = area;
            dr["Branch"] = branch;
            dr["TransCode"] = transcode;
            dr["DocumentNo"] = docno;
            dr["EffectiveDate"] = effectivedate;
            dr["Amount"] = amount;
            dr["VAT"] = vat;
            dr["Total"] = total;
            dr["Comment"] = comment;
            dr["AreaId"] = areaid;
            dr["BranchId"] = "0";
            dr["Period"] = period;
            dr["CountryId"] = countryid;
            dtUpdate.Rows.Add(dr);
            dtUpdate.AcceptChanges();
            double NewTotal = 0; int NoOfEntries = 0;
            foreach (DataRow dr1 in dtUpdate.Rows)
            {

                NoOfEntries++;

                NewTotal += Convert.ToDouble(dr1["Total"].ToString());
            }
            gv_customerview.Visible = true;
            adjustmentlog.Visible = true;
            // ClearControls();
            Session["dtAdjustments"] = dtUpdate;
            lblAdjustmentTotal.Visible = true;
            lblAdjustmentTotal.Text = "TOTAL NO. OF ENTRIES IS " + NoOfEntries + " WITH TOTAL AMOUNT OF " + NewTotal.ToString("#,##0");
            gv_customerview.DataSource = dtUpdate.DefaultView;
            gv_customerview.DataBind();
            ClearAdjustmentControls();

        }

        private void GetTotalAmount(DataTable dtUpdate)
        {
            double NewTotal = 0; int NoOfEntries = 0;
            foreach (DataRow dr1 in dtUpdate.Rows)
            {

                NoOfEntries++;

                NewTotal += Convert.ToDouble(dr1["Total"].ToString());
            }
            lblAdjustmentTotal.Visible = true;
            lblAdjustmentTotal.Text = "TOTAL NO. OF ENTRIES IS " + NoOfEntries + " WITH TOTAL AMOUNT OF " + NewTotal.ToString("#,##0");
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            ClearAdjustmentControls();
        }

        private void ClearAdjustmentControls()
        {
            txtCustName.Text = "";
            txtcustomer.Text = "";
            txtPropRef.Text = "";
            txtDocNo.Text = "";
            txtEffectiveDate.Text = "";
            txtTotal.Text = "";
            txtcomment.Text = "";
            txtAmount.Text = "";
            cbotranscode.SelectedValue = "0";
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
            DataTable dt = (DataTable)Session["dtAdjustments"];
            string custref = "";
            string flag = "";
            int row = 0;
            //if (e.CommandName == "RowRemove")
            //{
            //    custref = Convert.ToString(e.CommandArgument.ToString());
                
            //}


        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["dtAdjustments"];


            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (dt.Rows.Count > 0)
            {

                if (gvRow.RowIndex <= dt.Rows.Count - 1)
                {
                    //Remove the Selected Row data and reset row number  
                    dt.Rows.Remove(dt.Rows[rowID]);
                    ResetRowID(dt);
                }
                //Store the current data in session for future reference  
                Session["dtAdjustments"] = dt;

                //Re bind the GridView for the updated data  
                gv_customerview.DataSource = dt;
                gv_customerview.DataBind();
                GetTotalAmount(dt);

            }
            else
            {
                //do not display grid
                gv_customerview.Visible = false;
                adjustmentlog.Visible = false;               
                lblAdjustmentTotal.Visible = false;
                string str = "No adjustment records to display";
                DisplayMessage(str, true);
            }



        }
        private void ResetRowID(DataTable dt)
        {
            int rowNumber = 1;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[0] = rowNumber;
                    rowNumber++;
                }
            }
        }
       

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAdjustments();
               // DisplayMessage("Submit option not ready yet", true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void SaveAdjustments()
        {
            try
            {
                DisplayMessage(".", true);
                DataTable dtAdjustments = (DataTable)Session["dtAdjustments"];
                if (dtAdjustments.Rows.Count == 0)
                {
                    DisplayMessage("Please Add Adjustments", true);
                }
                else
                {


                    string Returned = "";
                    string CreatedBy = Session["UserID"].ToString();
                    TransactionObj trans = new TransactionObj();
                    string comment = "";
                    foreach (DataRow dr in dtAdjustments.Rows)
                    {
                        trans = new TransactionObj();

                        //TO use incase to be captured as payment

                        trans.AreaID = Convert.ToInt32(dr["AreaId"].ToString());
                        trans.BranchID = Convert.ToInt32(dr["BranchId"].ToString());
                        trans.CountryID = Convert.ToInt32(dr["CountryId"].ToString());
                        trans.DocumentNo = dr["DocumentNo"].ToString();
                        trans.CustRef = dr["CustRef"].ToString();
                        // string branch = "0";
                        // trans.BranchID = int.Parse(branch);
                        trans.Period = dr["Period"].ToString();
                        trans.TransCode = dr["TransCode"].ToString();
                        trans.TransValue = Convert.ToDouble(dr["Amount"].ToString().Replace(",", "").Replace("-", ""));
                        trans.BasisConsumption = 0;
                        trans.VatValue = Convert.ToDouble(dr["VAT"].ToString().Replace(",", "").Replace("-", ""));
                        trans.CreatedBy = Convert.ToInt32(CreatedBy);
                        trans.InvoiceNumber = "N/A";
                        trans.PostDate = Convert.ToDateTime(dr["EffectiveDate"].ToString());
                        comment = dr["Comment"].ToString();
                        Returned = LogAdjustment(trans, comment);
                    }
                    if (Returned.Contains("successfully"))
                    {
                        DisplayMessage("Adjustments have been successfully submitted for Area/Branch head approval", false);
                        dtAdjustments.Clear();
                        btnsubmit.Visible = false;
                        gv_customerview.Visible = false;
                        lblAdjustmentTotal.Visible = false;

                    }
                    else
                    {
                        DisplayMessage(Returned, true);
                        dtAdjustments.Clear();
                        btnsubmit.Visible = false;
                        gv_customerview.Visible = false;
                        lblAdjustmentTotal.Visible = false;
                    }
                }
            }
            catch(Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private string LogAdjustment(TransactionObj trans, string comment)
        {
            string output = "";
            try
            {
                output = AdjustAccount(trans, comment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        private string AdjustAccount(TransactionObj trans, string comment)
        {
            string output = "";
            try
            {
                DataTable dtrans = new DataTable();
                dtrans = bll.GetTranscodeDetails(trans.TransCode);
                if (dtrans.Rows.Count > 0)
                {
                    //trans.VatCode = dtrans.Rows[0]["VatCode"].ToString();
                    trans.ChargeType = dtrans.Rows[0]["chargeType"].ToString();
                    trans.BalType = dtrans.Rows[0]["balanceType"].ToString();
                   // double VatAccount = Convert.ToDouble(dtrans.Rows[0]["VatAccount"].ToString());
                    bool IsVatable = Convert.ToBoolean(dtrans.Rows[0]["vatIndicator"].ToString());
                    bool IsActive = Convert.ToBoolean(dtrans.Rows[0]["active"].ToString());
                    if (IsActive)
                    {
                        if (trans.BalType == "CR")
                        {
                            trans.TransValue = 0 - trans.TransValue;
                        }
                        string vat = bll.GetVATAmount(trans.CountryID.ToString(), trans.TransValue.ToString());
                        trans.VatValue = double.Parse(vat);
                      
                        double TotalAdjustmentAmount = trans.TransValue + trans.VatValue;
                        bll.SaveAdjustmentInceptionLogs(trans, comment);
                        //output = string.Format("{0} has been successfully adjusted by {1}", trans.CustRef, trans.TransValue + trans.VatValue);
                        output = "Customer-" + trans.CustRef + " successfully adjusted by " + TotalAdjustmentAmount.ToString();
                    }
                    else
                    {
                        // output = String.Format("Transaction Code {0} is not active", trans.TransCode);
                        output = "Transaction code-" + trans.TransCode + " is not active";
                    }
                }
                else
                {
                    //output = String.Format("Transaction Code {0} Not Found", trans.TransCode);
                    output = "Transaction code-" + trans.TransCode + " is not found.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            gv_customerview.Visible = false;
            adjustmentlog.Visible = false;
            lblAdjustmentTotal.Visible = false;
            string str = ".";
            DisplayMessage(str, true);
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintAdjustments();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message,true);
            }
        }

        private void PrintAdjustments()
        {
            dtUpdate = (DataTable)Session["dtAdjustments"];
            if (dtUpdate.Rows.Count == 0)
            {
                DisplayMessage("Please Enter Adjustments for printing",true);
            }
            else
            {
              

                

                AdjExcelReport(dtUpdate);

            }
        }
        private void AdjExcelReport(DataTable dataTable)
        {

            int[] iColumns = {0,1, 2, 3, 4, 5,6, 7, 8, 9 };

            //Export the details of specified columns to Excel
            RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();
            objExport.ExportDetails(dataTable, iColumns, Export.ExportFormat.Excel, "Adjustments" + ".xls");
        }

        private void LoadTransactionCodes()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetTransactionCodes("1");
                cbotranscode.DataSource = dt;

                cbotranscode.DataTextField = "transdesc";
                cbotranscode.DataValueField = "transCode";
                cbotranscode.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadTransactionCodes", error);
                DisplayMessage(error, true);
            }
        }
        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            string CustRef = txtcustomer.Text;
            string DocNo = txtDocNo.Text;
            double sAmount = 0;
           

                //if (chkVAT.Checked == true)
                //{

                //    if (!String.IsNullOrEmpty(txtAmount.Text.Trim()))
                //    {

                //        double Amount = 0;
                //        if (!String.IsNullOrEmpty(txtAmount.Text.Trim()))
                //            Amount = Convert.ToDouble(txtAmount.Text.Trim());
                //        //double VATAmount = (Amount * VatAccount) / 100;
                //        double VATAmount = (Amount * 18) / 100;
                //        double Total = Amount + VATAmount;
                //        txtTotal.Text = Total.ToString("#,##0");
                //    }

                //}
                //else
                //{
                    if (!String.IsNullOrEmpty(txtAmount.Text.Trim()))
                    {

                        double Amount = 0;
                        if (!String.IsNullOrEmpty(txtAmount.Text.Trim()))
                            Amount = Convert.ToDouble(txtAmount.Text.Trim());
                        txtTotal.Text = Amount.ToString("#,##0");
                    }
                //}
            
        }
        private void CreateAdjustmentsDataTable()
        {
            DataTable dtAdjustments = new DataTable("Adjustments");
            dtAdjustments.Columns.Add(new DataColumn("CustRef", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Area", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Branch", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("TransCode", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("DocumentNo", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("EffectiveDate", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Amount", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("VAT", typeof(string)));            
            dtAdjustments.Columns.Add(new DataColumn("Total", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Comment", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("AreaId", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("BranchId", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Period", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("CountryId", typeof(string)));
            dtAdjustments.Rows.Clear();

            Session["dtAdjustments"] = dtAdjustments;
        }

       /* protected void btnsaveapproval_Click(object sender, EventArgs e)
        {
            try
            {
                string status = "0";


                string action = rbAction.SelectedValue.ToString();
                if (action.Equals("1"))
                {
                    status = "A";
                }
                else if (action.Equals("2"))
                {
                    status = "R";
                }
                string comment = txtapprovalcomment.Text.Trim();
                if (action.Equals(""))
                {
                    DisplayMessage("No Action Selected",true);
                }
                else if (comment.Equals(""))
                {
                    DisplayMessage("Please enter valid reason",true);
                }
                else if (comment.Length < 5)
                {
                    DisplayMessage("Reason entered must be more than 5 characters",true);
                }
                else
                {

                   
                    ProcessRecords(status, comment);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btncancelapproval_Click(object sender, EventArgs e)
        {

        }

        protected void btnprintapproval_Click(object sender, EventArgs e)
        {

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
            DataTable dt = (DataTable)Session["dtAdjustmentsApp"];
            string custref = "";
            string flag = "";
            int row = 0;
            //if (e.CommandName == "RowRemove")
            //{
            //    custref = Convert.ToString(e.CommandArgument.ToString());

            //}


        }
        private void ProcessRecords(string status, string comment)
        {

            // string arr = GetRecordsToProcess();
            string arr = GetRecordsToDump();
            arr = arr.TrimEnd(',');

            if (arr.Equals(""))
            {
                DisplayMessage("SELECT TRANSACTION(S) TO PROCESS",true);
            }
            else
            {
                PostAdjustmentTransactions(arr, status, comment);
            }


        }

      
        private string GetRecordsToDump()
        {
            int Count = 0;
            string ItemArr = "";
            //up = new ArrayList();

            foreach (GridViewRow Items in gv_approvalview.Rows)
            {
                CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
                if (chk.Checked)
                {
                    
                    Count++;
                    string ItemFound = Items.Cells[1].Text;
                    string ItemFound1 = Items.Cells[8].Text;
                    ItemArr = ItemArr += ItemFound + "+" + ItemFound1 + ",";
                    //add to uploaded arraylist
                    //up.Add(Count);
                }
            }
            return ItemArr;
        }
        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectAllItems();
                if (chkSelect.Checked == true)
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message,true);
            }
        }
        private void SelectAllItems()
        {
            foreach (GridViewRow Items in gv_approvalview.Rows)
            {
                CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
                if (chk.Checked)
                {
                    chk.Checked = false;
                }
                else
                {
                    chk.Checked = true;
                }
            }
        }


        private void PostAdjustmentTransactions(string str, string status, string comment)
        {
            try
            {
                ////ThirdPartyTransactions tran;

                ////dh = new DatabaseHandler();
                //string user = Session["UserID"].ToString();
                string confirmedBy = Session["UserID"].ToString();
                string[] arr = str.Split(',');
                int RecordId = 0;
                TransactionObj trans = new TransactionObj();
                foreach (string s in arr)
                {
                    string[] separators = { "+" };
                    string[] param = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    RecordId = Convert.ToInt32(param[0].ToString());
                    string custref = param[1].ToString();
                    if (RecordId != 0)
                    {
                        trans = new TransactionObj();
                        // tran = new ThirdPartyTransactions();
                        trans = bll.GetInternalTranObj(RecordId, custref);
                        if (status.Equals("A"))
                        {
                            ApproveTransaction(trans, status, confirmedBy, comment, RecordId);
                        }
                        else
                        {
                            RejectTransaction(trans, status, confirmedBy, comment, RecordId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RejectTransaction(TransactionObj trans, string status, string confirmedby, string comment, int recordid)
        {
            try
            {
                string str = "Adustment transaction rejected with reason " + comment;
                DisplayMessage(str,true);
                //log manager reason
                bool isapproved = false;
                status = "rejected";
                DateTime confirmdate = DateTime.Now;
                bll.LogAdjustmentStatus(recordid, trans.CustRef, status, comment, confirmedby, isapproved, confirmdate);
                ClearControls();
                LoadAdjustments();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ApproveTransaction(TransactionObj trans, string status, string confirmedby, string comment, int recordid)
        {
            try
            {
                string returned = "";
                returned = bll.SaveAdjustment(trans);
                if (returned.Contains("successfully"))
                {
                    DisplayMessage(returned,true);
                    //log manager reason
                    //log manager reason
                    bool isapproved = true;
                    status = "accepted";
                    DateTime confirmdate = DateTime.Now;
                    //dal.LogAdjustmentStatus(recordid, trans.CustRef, status, comment, confirmedby, isapproved, confirmdate);
                    ClearControls();
                    LoadAdjustments();

                }
                else
                {
                    DisplayMessage(returned,true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAdjustments()
        {
            string AreaID = area_list.SelectedValue.ToString();
            string BranchID = "0";
            string period = bll.GetBillingPeriod(AreaID);
            DataTable dataTable = bll.GetInceptionAdjustments(AreaID, BranchID, "", period);
            Session["AdjustmentsApp"] = dataTable;
            if (dataTable.Rows.Count > 0)
            {
                DisplayMessage(".",true);
                gv_approvalview.DataSource = dataTable;
                gv_approvalview.DataBind();
                approvaldisplay.Visible = true;
            }
            else
            {
                DisplayMessage("No record(s) found.",true);
                approvaldisplay.Visible = false;
            }
        }

        private void ClearControls()
        {
            txtcomment.Text = "";
            rbAction.ClearSelection();
           // chkselect.Checked = false;
        }*/

    }
}