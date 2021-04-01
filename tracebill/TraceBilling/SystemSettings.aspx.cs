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
    public partial class SystemSettings : System.Web.UI.Page
    {
        BusinessLogic bll = new BusinessLogic();
        ResponseMessage resp = new ResponseMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    LoadCurrencyList();
                    LoadCountryDetails();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCurrencyList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCurrencyList();
                currency_list.DataSource = dt;

                currency_list.DataTextField = "currencyName";
                currency_list.DataValueField = "currencyId";
                currency_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCurrencyList", error);
                DisplayMessage(error, true);
            }
        }

        private void LoadCountryDetails()
        {
            DataTable dataTable = bll.GetCountrySettings();
            if (dataTable.Rows.Count > 0)
            {
                gv_countrysettings.DataSource = dataTable;
                gv_countrysettings.DataBind();
                DisplayMessage(".", true);
                maindisplay.Visible = true;
            }
            else
            {
                string error = "100: " + "No records found";
                bll.Log("LoadCountryDetails", error);
                DisplayMessage(error, true);
                maindisplay.Visible = false;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string countryname = txtcountry.Text.Trim();
                string countrycode = txtcountrycode.Text.Trim();
                string vat = txtvat.Text.Trim();
                string currency = currency_list.SelectedValue.ToString();
                string createdby = Session["UserID"].ToString();
                bool isactive = chkActive.Checked;
                string code = lblcountrycode.Text;
                if (countryname == "")
                {
                    DisplayMessage("Please Enter Country Name",true);
                    
                }
                else
                {
                    bll.SaveCountryDetails(code,countryname, countrycode, vat, currency, createdby, isactive);
                    string str = "";
                    if (code == "0")
                    {
                        str = "Country - " + countryname + " has been saved Successfully";
                    }
                    else
                    {
                        str = "Country - " + countryname + "'s details have been updated Successfully";
                    }
                   
                    DisplayMessage(str, false);
                    LoadCountryDetails();
                    RefreshControls();
                }
             
            }
            catch(Exception ex)
            {
                //throw ex;
                DisplayMessage(ex.Message, true);
            }
        }

        private void RefreshControls()
        {
            txtcountry.Text = "";
            txtcountrycode.Text = "";
            txtvat.Text = "";
            currency_list.SelectedValue = "0";
        }

        protected void currency_list_DataBound(object sender, EventArgs e)
        {
            currency_list.Items.Insert(0, new ListItem("- - select currency - -", "0"));
        }
        protected void gv_countrysettings_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            //int index = e.NewSelectedIndex;
            //if (index >= 0)
            //{
            //    //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
            //    string usercode = GridViewUser.Rows[index].Cells[1].Text;
            //    if (e.NewSelectedIndex.ToString() == "Edit")
            //    {
            //        //
            //        DisplayMessage("Hello xxxx", true);
            //    }

            //}
        }
        protected void gv_countrysettings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_countrysettings_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowEdit")
            {
                //string UserID = e.Item.Cells[0].Text;
                string countryId = Convert.ToString(e.CommandArgument.ToString());
                LoadCountrySetting(countryId);
              
            }
            else if (e.CommandName == "AddRegion")
            {
                string countryId = Convert.ToString(e.CommandArgument.ToString());

            }
        }

       
        private void LoadCountrySetting(string countryId)
        {
            DataTable dt = bll.GetCountrySettingByID(countryId);
            lblcountrycode.Text = dt.Rows[0]["countryId"].ToString();
            txtcountry.Text = dt.Rows[0]["countryName"].ToString();
            txtcountrycode.Text = dt.Rows[0]["countryCode"].ToString();
            txtvat.Text = dt.Rows[0]["vatrate"].ToString();
            string currency = dt.Rows[0]["currency"].ToString();
            currency_list.SelectedIndex = currency_list.Items.IndexOf(currency_list.Items.FindByText(currency));
            bool IsActive = Convert.ToBoolean(dt.Rows[0]["active"].ToString());
            chkActive.Checked = IsActive;
        }

        protected void gv_countrysettings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string status = e.Row.Cells[2].Text;

            }
        }
    }
}