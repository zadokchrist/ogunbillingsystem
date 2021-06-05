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
    public partial class MeterManagement : System.Web.UI.Page
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
            searchdisplay.Visible = true;
           
            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
            string custref = txtcustref.Text.Trim();

            DataTable dataTable = bll.LoadCustomerDisplay(countryid, areaid, custref, 1);
            if (dataTable.Rows.Count > 0)
            {
                gv_customerview.DataSource = dataTable;
                gv_customerview.DataBind();
                DisplayMessage(".", true);
                //customerdisplay.Visible = true;
            }
            else
            {
                gv_customerview.DataSource = dataTable;
                gv_customerview.DataBind();
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
        protected void btninventory_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = true;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            DisplayMessage(".", true);
            searchdisplay.Visible = false;
            LoadMeterTypes();
        }

        protected void btnservicing_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = true;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            DisplayMessage(".", true);
        }

        protected void btnreplacement_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = true;
            metertransfer.Visible = false;
            DisplayMessage(".", true);
        }

        protected void btntransfer_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = true;
            DisplayMessage(".", true);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LoadDisplay();
        }

        protected void btninventorysave_Click(object sender, EventArgs e)
        {
            try
            {
                string metertype = cboType.SelectedValue.ToString();
                string serial = txtserial.Text.Trim();
                string dials = txtdials.Text.Trim();
                string initialrdg = txtreading.Text.Trim();
                string life = txtlife.Text.Trim();
                string manufacturedate = txtmanufacturedate.Text.Trim();
                string condition = txtcondition.Text.Trim();
                bool isactive = chkactive.Checked;
                DateTime manufacturedt = Convert.ToDateTime(manufacturedate);
                string createdby = Session["UserID"].ToString();
                if (metertype == "0")
                {
                    DisplayMessage("Please select meter type", true);
                }
                else if (serial == "")
                {
                    DisplayMessage("Please enter meter number/serial", true);
                }
                else if (initialrdg == "")
                {
                    DisplayMessage("Please enter initial reading on meter", true);
                }
                else if (condition == "")
                {
                    DisplayMessage("Please enter a valid condition status", true);
                }
                else if (dials == "")
                {
                    DisplayMessage("Please enter valid dials on meter", true);
                }
                else if (!bll.IsValidReadingDate(manufacturedate))
                {
                    string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                    DisplayMessage("Invalid Meter Manufacture Date, It cannot be greater than Today ( " + Todate + " )", true);
                }
                else
                {
                    //save details
                    resp = bll.SaveMeterInventory(metertype,serial,dials,initialrdg,life,manufacturedt,createdby,isactive,condition);
                    if (resp.Response_Code == "1")//save
                    {
                        string str = " with inventory details against meter(" + serial + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "106")//edit and update
                    {

                        string str = " with inventory details against meter(" + serial + ")";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
              
                    //clear conrols
                    ClearInventoryControls();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void ClearInventoryControls()
        {
            cboType.SelectedValue = "0";
            txtcondition.Text = "";
            txtdials.Text = "";
            txtserial.Text = "";
            txtlife.Text = "";
            txtmanufacturedate.Text = "";
            txtreading.Text = "";
            chkactive.Checked = false;
            txtdials.ReadOnly = true;
            txtmanufacturedate.ReadOnly = true;
            txtreading.ReadOnly = true;
            txtlife.ReadOnly = true;
            txtcondition.ReadOnly = true;
        }

        protected void btninventorycancel_Click(object sender, EventArgs e)
        {
            ClearInventoryControls();
        }
        private void LoadMeterTypes()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterTypeList();
                cboType.DataSource = dt;//meterTypeId,meterName

                cboType.DataTextField = "meterName";
                cboType.DataValueField = "meterTypeId";
                cboType.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadMeterType", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboType_DataBound(object sender, EventArgs e)
        {
            cboType.Items.Insert(0, new ListItem("- - select meter Type - -", "0"));
        }
        protected void txtserial_TextChanged(object sender, EventArgs e)
        {
            string meterno = txtserial.Text.Trim();
            string str = "";
            if (!String.IsNullOrEmpty(meterno))
            {
                //check meter existence
                bool ismeterserial = bll.CheckExistingSerial(meterno);
                if(ismeterserial)
                {
                    str = "Meter Serial-"+ meterno + " is aleady captured."; 
                    DisplayMessage(str, true);
                }
                else
                {
                    str = ".";
                    DisplayMessage(str, true);
                    txtdials.ReadOnly = false;
                    txtmanufacturedate.ReadOnly = false;
                    txtreading.ReadOnly = false;
                    txtlife.ReadOnly = false;
                    txtcondition.ReadOnly = false;
                }
            }
            else
            {
                str = "Please enter valid meter serial";
                DisplayMessage(str, true);
            }
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

            if (e.CommandName == "RowReplace")
            {
                custref = Convert.ToString(e.CommandArgument.ToString());
                flag = "1";

            }
            else if (e.CommandName == "RowService")
            {
                custref = Convert.ToString(e.CommandArgument.ToString());
                flag = "2";

            }
            else if (e.CommandName == "RowTransfer")
            {
                custref = Convert.ToString(e.CommandArgument.ToString());
                flag = "3";

            }
            LoadCustomerDisplay(custref, flag);

        }

        private void LoadCustomerDisplay(string custref, string flag)
        {
            try
            {
                if(flag.Equals("1"))//replacement
                {
                    meterinventory.Visible = false;
                    meterservice.Visible = false;
                    meterreplacement.Visible = true;
                    metertransfer.Visible = false;
                    DisplayMessage(".", true);
                    LoadReplacementReasons();
                    LoadReplacementDetails(custref);
                }
                else if (flag.Equals("2"))//service
                {
                    meterinventory.Visible = false;
                    meterservice.Visible = true;
                    meterreplacement.Visible = false;
                    metertransfer.Visible = false;
                    DisplayMessage(".", true);
                    LoadServiceReasons();
                    LoadServiceDetails(custref);
                }
                else if (flag.Equals("3"))//transfer
                {
                    meterinventory.Visible = false;
                    meterservice.Visible = false;
                    meterreplacement.Visible = false;
                    metertransfer.Visible = true;
                    DisplayMessage(".", true);
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadReplacementDetails(string custref)
        {
            try
            {
                string areaid = area_list.SelectedValue.ToString();
                string branchid = "0";
                DataTable dTable = bll.GetLatestReadingDetails(custref, areaid, branchid);
                if (dTable.Rows.Count > 0)
                {
                    txtprerdgrep.Text = dTable.Rows[0]["CurReading"].ToString();
                    DateTime CurReadingDate = Convert.ToDateTime(dTable.Rows[0]["CurReadingDate"].ToString());
                    txtprerdgdtrep.Text = CurReadingDate.ToString("dd/MM/yyyy");
                    txtconsumptionrep.Text = dTable.Rows[0]["Consumption"].ToString();
                    txtsizerep.Text = dTable.Rows[0]["size"].ToString();
                    txtmakerep.Text = dTable.Rows[0]["meterName"].ToString();
                    txtserialrep.Text = dTable.Rows[0]["serial"].ToString();
                    txtnamereplace.Text = dTable.Rows[0]["customerName"].ToString();
                    txtmeterefrep.Text = dTable.Rows[0]["meterRef"].ToString();
                    txtproprefrep.Text = dTable.Rows[0]["propertyRef"].ToString();
                    txtdialsrep.Text = dTable.Rows[0]["dials"].ToString();
                    LoadReplacementReasons();
                    LoadMeterTypes_rep();
                    LoadPipeSizeList();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadServiceDetails(string custref)
        {
            try
            {
                string areaid = area_list.SelectedValue.ToString();
                string branchid = "0";
                DataTable dTable = bll.GetLatestReadingDetails(custref, areaid, branchid);
                if (dTable.Rows.Count > 0)
                {
                    txtViewPreRdg.Text = dTable.Rows[0]["CurReading"].ToString();
                    DateTime CurReadingDate = Convert.ToDateTime(dTable.Rows[0]["CurReadingDate"].ToString());
                    txtPreRdgDate.Text = CurReadingDate.ToString("dd/MM/yyyy");
                    txtConsumption.Text = dTable.Rows[0]["Consumption"].ToString();
                    txtViewSize.Text = dTable.Rows[0]["size"].ToString();
                    txtViewType.Text = dTable.Rows[0]["meterName"].ToString();
                    txtViewSerial.Text = dTable.Rows[0]["serial"].ToString();
                    txtCustName.Text = dTable.Rows[0]["customerName"].ToString();
                    txtViewMeterRef.Text = dTable.Rows[0]["meterRef"].ToString();
                    txtViewPropRef.Text = dTable.Rows[0]["propertyRef"].ToString();
                    txtViewDial.Text = dTable.Rows[0]["dials"].ToString();
                   
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        protected void txtReading_TextChanged(object sender, EventArgs e)
        {
            //TODO: To include reseting of the meter 
            if (!String.IsNullOrEmpty(txtrdg.Text.Trim()))
            {
                if (String.IsNullOrEmpty(txtViewPreRdg.Text.Trim()))
                {
                    DisplayMessage("Previous reading cannot be empty", true);
                }
                else
                {
                    double PreReading = Convert.ToDouble(txtViewPreRdg.Text.Trim());
                    double Reading = Convert.ToDouble(txtrdg.Text.Trim());
                    double Consumption = Reading - PreReading;
                    txtConsumption.Text = Consumption.ToString();
                }
                
            }
        }
        protected void txtrdgreplace_TextChanged(object sender, EventArgs e)
        {
            //TODO: To include reseting of the meter 
            if (!String.IsNullOrEmpty(txtrdgreplace.Text.Trim()))
            {
                if (String.IsNullOrEmpty(txtprerdgrep.Text.Trim()))
                {
                    DisplayMessage("Previous reading cannot be empty", true);
                }
                else
                {
                    double PreReading = Convert.ToDouble(txtprerdgrep.Text.Trim());
                    double Reading = Convert.ToDouble(txtrdgreplace.Text.Trim());
                    double Consumption = Reading - PreReading;
                    txtconsumptionrep.Text = Consumption.ToString();
                }
                    
            }
        }

        protected void btnservice_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnreturn2_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
        }
        private void LoadServiceReasons()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterActivityReasons("1");
                cboReason.DataSource = dt;//meterTypeId,meterName

                cboReason.DataTextField = "reason";
                cboReason.DataValueField = "reasonId";
                cboReason.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadServiceReasons", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadReplacementReasons()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterActivityReasons("2");
                cboReasonrep.DataSource = dt;//meterTypeId,meterName

                cboReasonrep.DataTextField = "reason";
                cboReasonrep.DataValueField = "reasonId";
                cboReasonrep.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadReplacementReasons", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboReason_DataBound(object sender, EventArgs e)
        {
            cboReason.Items.Insert(0, new ListItem("- - select reason - -", "0"));
        }
        protected void cboReasonrep_DataBound(object sender, EventArgs e)
        {
            cboReasonrep.Items.Insert(0, new ListItem("- - select reason - -", "0"));
        }

        protected void btnreplace_Click(object sender, EventArgs e)
        {

        }

        protected void btnapprove_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
            meterapproval.Visible = true;
            searchdisplay.Visible = false;
            DisplayMessage(".", true);
        }

        protected void btnreturnreplace_Click(object sender, EventArgs e)
        {
            meterinventory.Visible = false;
            meterservice.Visible = false;
            meterreplacement.Visible = false;
            metertransfer.Visible = false;
        }
        private void LoadPipeSizeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetPipeDiameterList();
                cboMeterSize.DataSource = dt;

                cboMeterSize.DataTextField = "diameter";
                cboMeterSize.DataValueField = "diameterId";
                cboMeterSize.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadPipeSizeList", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboMeterSize_DataBound(object sender, EventArgs e)
        {
            cboMeterSize.Items.Insert(0, new ListItem("- - select Diameter - -", "0"));
        }
        protected void cboType2_DataBound(object sender, EventArgs e)
        {
            cboType2.Items.Insert(0, new ListItem("- - select meter type - -", "0"));
        }
        private void LoadMeterTypes_rep()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetMeterTypeList();
                cboType2.DataSource = dt;//meterTypeId,meterName

                cboType2.DataTextField = "meterName";
                cboType2.DataValueField = "meterTypeId";
                cboType2.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadMeterTypes_rep", error);
                DisplayMessage(error, true);
            }
        }


    }
}