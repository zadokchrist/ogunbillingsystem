
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
                        LoadFilters(10);
                        LoadAreaControls();
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
        private void LoadAreaControls()
        {
            ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea.Enabled = true;

        }
        private void LoadFilters(int areaid)
        {
            ddloperationarea.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea.DataBind();

        }
        private void LoadDisplay()
        {
          string area_code = "10";
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
                    string area_code = "10";
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
            txtenddate.Text = "";
            //area_list.SelectedValue = "0";

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
                string area_code = "10";
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
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("--select--", "0"));
        }
        
    }
}