
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
            ddloperationarea.Enabled = false;//chnaged from true to false as readonly

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
                string opid = ddloperationarea.SelectedValue.ToString();
                if (txttartdate.Text == "")
                {
                    DisplayMessage("Please Enter Period Start Date",true);
                }
               else  if (txtenddate.Text == "")
                {
                    DisplayMessage("Please Enter Period end Date", true);
                }
                else if (opid == "0")
                {
                    DisplayMessage("Please select area", true);
                }
                else
                {
                    string stdate = txttartdate.Text.Trim();
                    string endate = txtenddate.Text.Trim();
                    //DateTime StartDate = Convert.ToDateTime(txttartdate.Text.Trim());
                    DateTime StartDate = bll.GetDate(stdate);//european style dd/mm/yyyy
                    //var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                    int day = StartDate.Day;
                    int month = StartDate.Month;
                    int year = StartDate.Year;
                    StartDate = new DateTime(year, month, 1);
                    string area_code = "10";
                    lblopid.Text = opid;
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
            lblopid.Text = "0";
            lblCode.Text = "0";
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
                //string area = Convert.ToString(e.CommandArgument.ToString());
                string[] arg = new string[3];
                arg = e.CommandArgument.ToString().Split(';');
                string code = arg[0];
                string status = arg[1];
                string period = arg[2];
                string opid = arg[3];
                string str = "";
                if (status.Equals("YES"))
                {
                    str = "Billing Period("+ period+ ") already closed";
                    DisplayMessage(str, true);

                }
                else
                {
                    string AreaID = Session["AreaID"].ToString();
                    string Period = bll.GetBillingPeriod(AreaID);
                    lblopid.Text = opid;
                    // string area_code = cboAreas.SelectedValue.ToString();
                    // DataTable  dataTable = bll.GetUnBilledCustomers(AreaID, Period);
                    //if (dataTable.Rows.Count == 0)
                    //DataTable dt = new DataTable();
                    //if (dataTable.Rows.Count > 0)
                    //{
                    //    dt = FilterUnBilled(dataTable);
                    //}
                    //else
                    //{
                    //    DisplayMessage("No Customer Activity in the period",true);

                    //}
                    //if (dt.Rows.Count == 0)
                    //{
                    qndisplay.Visible = true;
                        lblQn.Text = "Are you sure you want to close period? ";
                        lblCode.Text = code;
                    DisplayMessage(".", true);
                    //}
                    //else
                    //{
                    //    Session["ClosePeriodDT"] = dt;

                    //    DisplayMessage("Cannot Close Period Because of the following unbilled accounts",true);
                    //   // DataGrid2.DataSource = dt;
                    //   // DataGrid2.DataBind();
                    //}
                }
              

            }

        }

        private DataTable FilterUnBilled(DataTable dataTable)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Branch", typeof(string));
                dt.Columns.Add("Territory", typeof(string));
                dt.Columns.Add("CustRef", typeof(string));
                dt.Columns.Add("CustName", typeof(string));
                dt.Columns.Add("MeterRef", typeof(string));
                dt.Columns.Add("PropertyRef", typeof(string));
                dt.Columns.Add("Address", typeof(string));
                dt.Columns.Add("HasReading", typeof(string));
                dt.Columns.Add("Suppressed", typeof(string));
                dt.Columns.Add("AVGConsumption", typeof(int));
                foreach (DataRow dr in dataTable.Rows)
                {
                    string CustRef = dr[2].ToString();
                    string CustName = dr[5].ToString();
                    string MeterRef = dr[4].ToString();
                    string PropRef = dr[3].ToString();
                    string Address = dr[6].ToString();
                    string HasRdg = dr[8].ToString();
                    string IsBilled = dr[9].ToString();
                    string Suppressed = dr[10].ToString();
                    int AvgCons = int.Parse(dr[7].ToString());
                    string Branch = dr[11].ToString();
                    string Territory = dr[12].ToString();
                    if ((HasRdg.Equals("NO") && IsBilled.Equals("NO") && Suppressed.Equals("NO")) || (HasRdg.Equals("YES") && IsBilled.Equals("NO") && Suppressed.Equals("NO"))
                        || (HasRdg.Equals("NO") && IsBilled.Equals("YES") && Suppressed.Equals("NO")))
                    {
                        dt.Rows.Add(Branch, Territory, CustRef, CustName, MeterRef, PropRef, Address, HasRdg, Suppressed, AvgCons);
                    }
                }
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
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
                DateTime enddate = new DateTime();
                string opid = lblopid.Text;
                if (!RecordCode.Equals("0"))
                {
                    //StartDate = DateTime.Now;
                    StartDate = bll.GetBillPeriodStartDate(int.Parse(RecordCode));
                    StartDate = StartDate.AddMonths(1);
                    enddate = bll.GetPeriodEndDate(StartDate);
                }
                else
                {
                    //StartDate = Convert.ToDateTime(txttartdate.Text.Trim());
                    //enddate = Convert.ToDateTime(txtenddate.Text.Trim());
                    string stdate = txttartdate.Text.Trim();
                    string endate = txtenddate.Text.Trim();
                    //DateTime StartDate = Convert.ToDateTime(txttartdate.Text.Trim());
                     StartDate = bll.GetDate(stdate);//european style dd/mm/yyyy
                    enddate = bll.GetDate(endate);//european style dd/mm/yyyy

                }
                string area_code = "10";
                string returned = bll.SaveBillingPeriod(RecordCode, area_code, StartDate,enddate,opid);
                if(returned.Contains("Success"))
                {
                    qndisplay.Visible = false;
                    DisplayMessage(returned, false);

                    RefreshControls();
                    LoadDisplay();
                }
                else
                {
                    DisplayMessage(returned, true);
                }
               
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