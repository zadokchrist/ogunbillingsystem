using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;

namespace TraceBilling
{
    public partial class GenerateConnectionInvoice : System.Web.UI.Page
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

                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadConnectionDetails();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        private void LoadCountryList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCountryList();
                country_list.DataSource = dt;

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
        private void LoadCustomerTypeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCustomerTypeList();
                customertype_list.DataSource = dt;

                customertype_list.DataTextField = "typeName";
                customertype_list.DataValueField = "custTypeId";
                customertype_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCustomerType", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadCustomerClass()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCustomerClass();
                category_list.DataSource = dt;

                category_list.DataTextField = "className";
                category_list.DataValueField = "ClassID";
                category_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCustomerClass", error);
                DisplayMessage(error, true);
            }
        }

        private void DisplayMessage(string message, Boolean isError)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "MESSAGE: " + message + ".";
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

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                LoadConnectionDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadConnectionDetails()
        {
            try
            {
                string jobnumber = txtjobnumber.Text.Trim();
                string country = country_list.SelectedValue.ToString();
                string area = area_list.SelectedValue.ToString();
                string status = "4";//survey
                DataTable dataTable = bll.GetSurveyReportDetails(jobnumber, int.Parse(country), int.Parse(area), int.Parse(status));
                if (dataTable.Rows.Count > 0)
                {
                    gv_surveyjobs.DataSource = dataTable;
                    gv_surveyjobs.DataBind();
                    DisplayMessage(".", true);
                    maindisplay.Visible = true;
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("LoadConnectionDetails", error);
                    DisplayMessage(error, true);
                    maindisplay.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                connectioninvoice.Visible = false;
                lblapplicant.Visible = false;
                LoadConnectionDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gv_surveyjobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Get the Command Name.
            string commandName = e.CommandName;
            if (commandName == "btnSelect")//details
            {
                //execute
                //int index = e.CommandArgument;
                int index = Int32.Parse((string)e.CommandArgument);
                if (index >= 0)
                {
                    // dispatchdisplay.Visible = true;
                    string jobnumber = gv_surveyjobs.Rows[index].Cells[2].Text;
                    txtjobnumber.Text = jobnumber;
                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    connectioninvoice.Visible = true;
                    customerdisplay.Visible = true;
                    //txtcategory.Text = GridViewIssue.Rows[index].Cells[1].Text;
                    ShowConnectionInvoiceDetails(jobnumber);
                }
            }
            //else if (commandName == "btnJobCard")//routecard
            //{
            //    string str = "Sorry, Job card details not available yet!!!";
            //    DisplayMessage(str, true);
            //}
            //Get the Row Index.
            //int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Get the Row reference in which Button was clicked.
            //GridViewRow row = GridView1.Rows[rowIndex];
        }

        private void ShowConnectionInvoiceDetails(string jobnumber)
        {
            try
            {
                DataTable dt = bll.GetSurveyReportDetails(jobnumber, 0, 0, 4);
                if (dt.Rows.Count > 0)
                {
                    LoadCustomerTypeList();
                    LoadCustomerClass();
                    txtappNo.Text = dt.Rows[0]["ApplicationNumber"].ToString();
                    txtname.Text = dt.Rows[0]["ApplicantName"].ToString();
                    txtjobNo.Text = dt.Rows[0]["JobNumber"].ToString();
                    lblApplicationCode.Text = dt.Rows[0]["applicationID"].ToString();
                    txtauthorizedby.Text = Session["FullName"].ToString();
                    //DateTime surveydt = Convert.ToDateTime(dt.Rows[0]["surveyDate"].ToString());
                    //txtsurveydate.Text = surveydt.ToString("dddd, dd MMMM yyyy");//
                    txtsurvey.Text= dt.Rows[0]["AssignedTo"].ToString();
                    lblarea.Text = dt.Rows[0]["areaId"].ToString();
                    string customertype = dt.Rows[0]["typeName"].ToString();
                    string category = dt.Rows[0]["className"].ToString();
                    customertype_list.SelectedIndex = customertype_list.Items.IndexOf(customertype_list.Items.FindByText(customertype));
                    category_list.SelectedIndex = category_list.Items.IndexOf(category_list.Items.FindByText(category));
                    //load controls
                    btnlinks.Visible = true;
                    string applicant = txtappNo.Text + "-->" + txtname.Text.Trim();
                    lblapplicant.Text = applicant;
                    //see customer details
                    showcustomerdetails(lblApplicationCode.Text);

                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void rtnTariff_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}



        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
       
        }

       

        protected void btncustomer_Click(object sender, EventArgs e)
        {
            customerdisplay.Visible = true;
            materialdisplay.Visible = false;
            //matdetails.Visible = false;
        }
        private void showcustomerdetails(string appid)
        {
            try
            {
                //check existing customer details and update
                DataTable dtcust = bll.GetFieldCustomerDetails(appid);
                if (dtcust.Rows.Count > 0)
                {
                    DateTime instructiondt = Convert.ToDateTime(dtcust.Rows[0]["instructionDate"].ToString());
                    txtinstructionDate.Text = instructiondt.ToString("dd-M-yy");
                    btnsavecustomer.Text = "Update Details";
                }
                else
                {
                    btnsavecustomer.Text = "Save Details";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnmaterials_Click(object sender, EventArgs e)
        {
            customerdisplay.Visible = false;
            materialdisplay.Visible = true;
            //condetails.Visible = false;
            LoadMaterialCategories();
            string appid = lblApplicationCode.Text.Trim();
            LoadCostingMaterials(int.Parse(appid));
            LoadCostingItems(int.Parse(appid));
            LoadPipeDiameterList();
            LoadPipeTypeList();
        }

        protected void btnsavecustomer_Click(object sender, EventArgs e)
        {
            try
            {
                string jobno = txtjobNo.Text.Trim();
                string appcode = txtappNo.Text.Trim();
                string appid = lblApplicationCode.Text.Trim();
                string conid = lblconnectionId.Text.Trim();
                string customertype = customertype_list.SelectedValue.ToString();
                string category = category_list.SelectedValue.ToString();
                DateTime connectiondate = DateTime.Now;
                string areaid = lblarea.Text;
                string authorizedby = txtauthorizedby.Text.Trim();
                string instuctiondt = txtinstructionDate.Text.Trim();
                string applicant = lblapplicant.Text.Trim();
                resp = bll.ValidateConnection(customertype, category, instuctiondt, authorizedby);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    DateTime instructiondate = Convert.ToDateTime(instuctiondt);
                    string createdby = Session["UserID"].ToString();
                    //save details
                    resp = bll.SaveFieldConnection(conid, appid, jobno, customertype, category, authorizedby, connectiondate, instructiondate, createdby, areaid);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with connection details against application(" + applicant + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {

                        string str = " with connection details against application(" + applicant + ") updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }


                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsavematerials_Click(object sender, EventArgs e)
        {
            try
            {
                string estimateid = lblestimateid.Text.Trim();
                string applicationid = lblApplicationCode.Text.Trim();
                string pipediameter = pipediameter_list.SelectedValue.ToString();
                string pipetype = pipematerial_list.SelectedValue.ToString();
                string pipelength = txtpipelength.Text.Trim();
                string excavationlength = txtexcavation.Text.Trim();
                string createdby = Session["UserID"].ToString();
                string applicant = lblapplicant.Text.Trim();
                if (pipediameter == "0")
                {
                    DisplayMessage("Please select pipe diameter", true);
                }
                else if (pipediameter == "0")
                {
                    DisplayMessage("Please select pipe type", true);
                }
                else if (pipelength == "")
                {
                    DisplayMessage("Please enter length of pipe", true);
                }
                else if (excavationlength == "")
                {
                    DisplayMessage("Please enter distance to be excavated", true);
                }
                else
                {
                    //save details
                    resp = bll.SaveFieldEstimates(estimateid, applicationid, pipediameter, pipetype, pipelength, excavationlength, createdby);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with field estimate details against application(" + applicant + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {

                        string str = " with connection details against application(" + applicant + ") updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    //log to next level
                    bll.LogApplicationTransactions(int.Parse(applicationid), 5, int.Parse(createdby));
                    //clear conrols
                    ClearEstimatesControls();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnPrintInvoice_Click(object sender, EventArgs e)
        {

        }

        protected void btnsubmititem_Click(object sender, EventArgs e)
        {
            try
            {
                string CostCode = lblCostcode.Text.Trim();
                string ApplicationCode = lblApplicationCode.Text.Trim();
                string ExpenseItemCode = lblCostItemID.Text.Trim();
                string Size = txtsize.Text.Trim();
                string Length = "N/A"; //txtCostLength.Text.Trim();
                string UnitCost = txtrate.Text.Trim();
                string Quantity = txtquantity.Text.Trim();
                if (ExpenseItemCode.Equals("NA"))
                {
                    DisplayMessage("No Material Code Identified", true);
                }
                else if (UnitCost == "")
                {
                    DisplayMessage("Please Enter Unit Cost", true);
                }
                else
                {
                    string returned = bll.SaveCostingDetails(CostCode, ApplicationCode, ExpenseItemCode, Size, Length, Quantity, UnitCost);
                    DisplayMessage(returned, false);
                    ClearCostingControls();
                    //LoadCostingControls();
                    LoadCostingItems(int.Parse(ApplicationCode));

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        private void LoadMaterialCategories()
        {
            string type = "COSTING";
            DataTable dt_classes = bll.GetMaterialOptions(type);
            materialoptions.DataSource = dt_classes;
            materialoptions.DataValueField = "categoryID";
            materialoptions.DataTextField = "category";
            materialoptions.DataBind();

            materialoptions.SelectedIndex = materialoptions.Items.IndexOf(materialoptions.Items.FindByValue("1"));
        }
        protected void DataGrid1_ItemCommand1(object source, DataGridCommandEventArgs e)
        {
        }
        public void LoadCostingMaterials(int applicationID)
        {
            string category_code = materialoptions.SelectedValue.ToString();
            int categoryId = int.Parse(category_code);
            DataTable dataTable = bll.GetCostMaterials(applicationID, categoryId);
            material_list.DataSource = dataTable;
            material_list.DataValueField = "materialID";
            material_list.DataTextField = "materialName";
            material_list.DataBind();
        }
        protected void materialoptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ApplicationCode = lblApplicationCode.Text.Trim();
                int ApplicationID = Convert.ToInt32(ApplicationCode);
                LoadCostingMaterials(ApplicationID);
               
                DisplayMessage(".", true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        protected void material_list_DataBound(object sender, EventArgs e)
        {
            material_list.Items.Insert(0, new ListItem("- - select material - -", "0"));
        }
        private void LoadCostingItems(int ApplicationID)
        {
            DataTable dataTable = bll.GetCostingItems(ApplicationID);
            DataGrid1.DataSource = dataTable;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataBind();
            GetTotalCost(dataTable);
        }
        private void GetTotalCost(DataTable Table)
        {
            if (Table.Rows.Count > 0)
            {
                double Total = 0;
                foreach (DataRow dr in Table.Rows)
                {
                    double amount = Convert.ToDouble(dr["Amount"].ToString());
                    Total += amount;
                }
                lblTotalCost.Visible = true;
                lblTotalCost.Text = "Total Cost(" + Total.ToString("#,##0") + ")";
            }
            else
            {
                lblTotalCost.Visible = false;
            }
        }
        private void CalculateCostingCharge()
        {
            int ItemID = Convert.ToInt16(lblCostItemID.Text.Trim());
            if (!ItemID.Equals(0))
            {

                DataTable dt_MDetails = bll.GetMaterialDetails(ItemID);
                if (dt_MDetails.Rows.Count > 0)
                {
                    string name = dt_MDetails.Rows[0]["material"].ToString();
                    string str_Amount = dt_MDetails.Rows[0]["unitCost"].ToString();
                    string measure = dt_MDetails.Rows[0]["measure"].ToString();//Measure
                    bool fixedrate = bool.Parse(dt_MDetails.Rows[0]["fixed"].ToString());
                    double Amount = double.Parse(str_Amount);

                    //lblCostMaterial.Text = name;
                    //lblMeasure.Text = measure;
                    if (fixedrate)
                    {
                        txtrate.Text = Amount.ToString("#,##0");
                        txtrate.Enabled = false;
                    }
                    else
                    {
                        txtrate.Enabled = true;
                        txtrate.Text = Amount.ToString("#,##0");
                        //txtCostRate.Text = "";
                    }
                }
            }
        }
        protected void material_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtsize.Text = "";
                txtquantity.Text = "";
                string material = material_list.Text.Trim();
                if (!material.Equals(""))
                {
                    lblCostItemID.Text = material_list.SelectedValue.ToString();
                    CalculateCostingCharge();
                }
                else
                {
                    lblCostItemID.Text = "NA";
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        private void ClearCostingControls()
        {
            txtquantity.Text = "";
            txtsize.Text = "";
            txtsize.Text = "";
            lblCostcode.Text = "0";
            lblCostItemID.Text = "0";

        }
        //new code on pipe materials 20/12/2020
        private void LoadPipeDiameterList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetPipeDiameterList();
                pipediameter_list.DataSource = dt;

                pipediameter_list.DataTextField = "diameter";
                pipediameter_list.DataValueField = "diameterId";
                pipediameter_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadPipeDiameterList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadPipeTypeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetPipeTypeList();
                pipematerial_list.DataSource = dt;

                pipematerial_list.DataTextField = "pipeDesc";
                pipematerial_list.DataValueField = "pipeTypeId";
                pipematerial_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadPipeTypeList", error);
                DisplayMessage(error, true);
            }
        }
        protected void pipediameter_list_DataBound(object sender, EventArgs e)
        {
            pipediameter_list.Items.Insert(0, new ListItem("- - select Diameter - -", "0"));
        }
        protected void pipematerial_list_DataBound(object sender, EventArgs e)
        {
            pipematerial_list.Items.Insert(0, new ListItem("- - select pipe material - -", "0"));
        }
        private void ClearEstimatesControls()
        {
            txtpipelength.Text = "";
            txtexcavation.Text = "";
            pipediameter_list.SelectedValue = "0";
            pipematerial_list.SelectedValue = "0";
            lblestimateid.Text = "0";

        }
    }
}