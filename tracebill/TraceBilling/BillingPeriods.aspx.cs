﻿
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
    public partial class BillingPeriods : System.Web.UI.Page
    {
        BusinessLogic bll = new BusinessLogic();
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
                        LoadDisplay();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Billing period page");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDisplay()
        {
          string area_code = area_list.SelectedValue.ToString();
            DataTable dt = bll.GetAllBillingPeriod(area_code);
            if (dt.Rows.Count > 0)
            {
                DisplayMessage(".", true);
                gv_billingperiod.DataSource = dt;
                gv_billingperiod.DataBind();
            }
            else
            {
                DisplayMessage("No period record found", true);
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

      
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
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
      
        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayMessage(".",false);
                if (txttartdate.Text == "")
                {
                    DisplayMessage("Please Enter Period Start Date",true);
                }
                else
                {
                    
                    DateTime StartDate = Convert.ToDateTime(txttartdate.Text.Trim());
                    //var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    int day = StartDate.Day;
                    int month = StartDate.Month;
                    int year = StartDate.Year;
                    StartDate = new DateTime(year, month, 1);
                    string area_code = area_list.SelectedValue.ToString();
                    DateTime EndDate = bll.GetPeriodEndDate(StartDate);
                    if(day!= 1)
                    {
                        qndisplay.Visible = false;
                        DisplayMessage("Please Enter First Day of selected month", true);
                    }
                    else
                    {
                        qndisplay.Visible = true;
                        if (lblCode.Text == "0")
                        {

                            lblQn.Text = "Are you sure you want to save period (" + StartDate.ToString("MMMM dd, yyyy") + " - " + EndDate.ToString("MMMM dd, yyyy") + ") ";
                        }
                        else
                        {
                            lblQn.Text = "Are you sure you want to update period (" + StartDate.ToString("MMMM dd, yyyy") + " - " + EndDate.ToString("MMMM dd, yyyy") + ") ";
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                DisplayMessage(ex.Message, true);
            }
        }

        private void RefreshControls()
        {
            txttartdate.Text = "";       
            area_list.SelectedValue = "0";
          
        }


        protected void gv_billingperiod_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
        protected void gv_billingperiod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_billingperiod_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowEdit")
            {
                //string UserID = e.Item.Cells[0].Text;
                string area = Convert.ToString(e.CommandArgument.ToString());
                

            }

        }



        protected void gv_billingperiod_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string status = e.Row.Cells[2].Text;

            }
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            try
            {
                DisplayMessage(".",false);
                string RecordCode = lblCode.Text.Trim();
                DateTime StartDate = new DateTime();
                if (!RecordCode.Equals("0"))
                {
                    //StartDate = DateTime.Now;
                    StartDate = bll.GetBillPeriodStartDate(int.Parse(RecordCode));
                    StartDate = StartDate.AddMonths(1);
                }
                else
                {
                    StartDate = Convert.ToDateTime(txttartdate.Text.Trim());
                  
                }
                string area_code = area_list.SelectedValue.ToString();
                string returned = bll.SaveBillingPeriod(RecordCode, area_code, StartDate);
                DisplayMessage(returned,false);
                txttartdate.Text = "";
                LoadDisplay();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message,true);
            }
        }

      

        protected void btnNo_Click(object sender, EventArgs e)
        {
            qndisplay.Visible = false;
        }
    }
}