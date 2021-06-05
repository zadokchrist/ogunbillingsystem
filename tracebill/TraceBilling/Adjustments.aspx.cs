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
    public partial class Adjustments : System.Web.UI.Page
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
                    LoadDisplay();
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

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnprint_Click(object sender, EventArgs e)
        {

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

    }
}