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
            string custref = "";
            string flag = "";


        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SaveAdjustments();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void SaveAdjustments()
        {
            DisplayMessage(".",true);
            if (dtUpdate.Rows.Count == 0)
            {
                DisplayMessage("Please Add Adjustments",true);
            }
            else
            {
               // Session["dtAdjustments"] = dtUpdate;
                DataTable dtAdjustments = (DataTable)Session["dtAdjustments"];

                string Returned = "";
                string CreatedBy = Session["UserID"].ToString();
                TransactionObj trans = new TransactionObj();
                string comment = "";
                foreach (DataRow dr in dtAdjustments.Rows)
                {
                    trans = new TransactionObj();

                    //TO use incase to be captured as payment
                    
                    trans.AreaID = Convert.ToInt32(dr["AreaID"].ToString());
                    //trans.BranchID = Convert.ToInt32(dr["BranchID"].ToString());
                    trans.DocumentNo = dr["DocumentNo"].ToString();
                    trans.CustRef = dr["CustRef"].ToString();
                    string branch = "0";
                    trans.BranchID = int.Parse(branch);
                    trans.Period = dr["Period"].ToString();
                    trans.TransCode = dr["TransCode"].ToString();
                    trans.TransValue = Convert.ToDouble(dr["Amount"].ToString().Replace("-", "").Replace(",", ""));
                    trans.BasisConsumption = 0;
                    trans.VatValue = Convert.ToDouble(dr["VATAmount"].ToString().Replace("-", "").Replace(",", ""));
                    trans.CreatedBy = Convert.ToInt32(CreatedBy);
                    trans.InvoiceNumber = dr["InvoiceNo"].ToString();
                    trans.PostDate = Convert.ToDateTime(dr["EffectiveDate"].ToString());
                    comment = dr["Comment"].ToString();
                    //Returned = inter.Adjust(trans);//revised 14/04/2021 to submit for approval
                    Returned = LogAdjustment(trans, comment);
                }
                if (Returned.Contains("successfully"))
                {
                    DisplayMessage("Adjustments have been successfully submitted for Area/Branch head approval",false);
                    dtUpdate.Clear();
                    btnsubmit.Visible = false;
                    gv_customerview.Visible = false;
                    lblAdjustmentTotal.Visible = false;

                }
                else
                {
                    DisplayMessage(Returned,true);
                    dtUpdate.Clear();
                    btnsubmit.Visible = false;
                    gv_customerview.Visible = false;
                    lblAdjustmentTotal.Visible = false;
                }
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
                    trans.VatCode = dtrans.Rows[0]["VatCode"].ToString();
                    trans.ChargeType = dtrans.Rows[0]["ChargeType"].ToString();
                    trans.BalType = dtrans.Rows[0]["BalanceType"].ToString();
                    double VatAccount = Convert.ToDouble(dtrans.Rows[0]["VatAccount"].ToString());
                    bool IsVatable = Convert.ToBoolean(dtrans.Rows[0]["VatIndicator"].ToString());
                    bool IsActive = Convert.ToBoolean(dtrans.Rows[0]["Active"].ToString());
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
                        output = string.Format("{0} has been successfully adjusted by {1}", trans.CustRef, trans.TransValue + trans.VatValue);
                    }
                    else
                    {
                        output = String.Format("Transaction Code {0} is not active", trans.TransCode);
                    }
                }
                else
                {
                    output = String.Format("Transaction Code {0} Not Found", trans.TransCode);
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

            int[] iColumns = {0,1, 2, 3, 4, 5, 7, 8, 9 };

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
            //dtAdjustments.Columns.Add(new DataColumn("Period", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("EffectiveDate", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Amount", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("VAT", typeof(string)));            
            dtAdjustments.Columns.Add(new DataColumn("Total", typeof(string)));
            dtAdjustments.Columns.Add(new DataColumn("Comment", typeof(string)));
            dtAdjustments.Rows.Clear();

            Session["dtAdjustments"] = dtAdjustments;
        }

    }
}