using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;

namespace TraceBilling
{
    public partial class AddTransaction : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        PaymentObj trans = new PaymentObj();
        ResponseMessage resp = new ResponseMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadVendorList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void LoadVendorList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetVendorList();
                vendor_list.DataSource = dt;

                vendor_list.DataTextField = "vendorName";
                vendor_list.DataValueField = "vendorIdentity";
                vendor_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("GetVendorList", error);
                DisplayMessage(error, true);
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
        protected void vendor_list_DataBound(object sender, EventArgs e)
        {
            vendor_list.Items.Insert(0, new ListItem("- - select bank/vendor - -", "0"));
        }
        protected void rtnpaymethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                EnableCheques();
            }
            catch (Exception ex)
            {
           
                DisplayMessage(ex.Message, true);
            }
        }
        private void EnableCheques()
        {
            if (rtnpaymethod.SelectedValue == "CASH")
            {
                //cboBank.Enabled = false;
                txtcheque.Enabled = false;
                //cboBank.SelectedIndex = -1;
                txtcheque.Text = "";
            }
            else if (rtnpaymethod.SelectedValue == "EFT")
            {
                //cboBank.Enabled = false;
                txtcheque.Enabled = false;
                //cboBank.SelectedIndex = -1;
                txtcheque.Text = "";
            }
            else
            {
                //cboBank.Enabled = true;
                txtcheque.Enabled = true;
            }
        }
        protected void txtcustrefNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string custref = txtcustrefNo.Text.Trim();
                DataTable dt = bll.CheckCustomerDetails(custref);
                string str = "";
                if (dt.Rows.Count > 0)
                {
                    txtfullname.Text = dt.Rows[0]["fullName"].ToString();
                    txtarea.Text = dt.Rows[0]["area"].ToString();
                    txtcounry.Text = dt.Rows[0]["country"].ToString();
                    txtcontact.Text = dt.Rows[0]["contact"].ToString();
                    DisplayMessage(".", false);
                }
                else
                {
                    txtfullname.Text = "";
                    txtarea.Text = "";
                    txtcounry.Text = "";
                    txtcontact.Text = "";
                    str = "CustomerRef-"+custref + " not existing.";
                    DisplayMessage(str,true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "", res = "";
                trans = new PaymentObj();
                trans.CustRef = txtcustrefNo.Text.Trim();
                trans.VendorTransRef = txttransref.Text.Trim();
                trans.PaymentDate = txtpaymentDate.Text.Trim();
                trans.PaymentMethod = rtnpaymethod.SelectedItem.ToString();
                trans.FullName = txtfullname.Text.Trim();
                trans.Amount = txtamount.Text.Trim();
                trans.Contact = txtcontact.Text.Trim();
                trans.VendorCode = vendor_list.SelectedValue.ToString();
                trans.CreatedBy = Session["UserName"].ToString();
                trans.Narration = txtnaration.Text.Trim();
                trans.ChequeNumber = txtcheque.Text.Trim();
                if(!trans.ChequeNumber.Equals(""))
                {
                    trans.Narration = trans.ChequeNumber + ":" + trans.Narration;
                }
                if (bll.IsNumeric(trans.CustRef))
                {
                    trans.PaymentCode = "WS";
                }
                else
                {
                    trans.PaymentCode = trans.CustRef.Substring(0, 2);//trim 2 digits
                }
                //validate input
                resp = bll.ValidateTransaction(trans);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    if (trans.VendorCode.Equals("0"))
                    {
                        str = "Please select Bank/Vendor";
                        DisplayMessage(str, true);
                    }
                    {
                        //resp.Response_Code="test"; //test only
                        resp = bll.SavePaymentTransaction(trans);
                        if (resp.Response_Code == "0")
                        {
                            str = " with new payment transaction against(" + trans.CustRef + ") details saved";

                            res = resp.Response_Message + str;
                            DisplayMessage(res, false);
                        }
                        else
                        {
                            str = " payment details against customer (" + trans.CustRef + ")";
                            res = resp.Response_Message + str;
                            DisplayMessage(res, true);
                        }
                        RefreshControls();
                    }
                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }


        }

        private void RefreshControls()
        {
            txtcustrefNo.Text = "";
            txtfullname.Text = "";
            txtnaration.Text = "";
            txtpaymentDate.Text = "";
            txtcontact.Text = "";
            txtcounry.Text = "";
        }
    }
}