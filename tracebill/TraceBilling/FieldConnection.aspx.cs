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
    public partial class FieldConnection : System.Web.UI.Page
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
                throw ex;
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
        private void LoadBlockMaps(string areaid, string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBlockMaps(areaid,branchid);
                cboBlock.DataSource = dt;

                cboBlock.DataTextField = "blockNumber";
                cboBlock.DataValueField = "blockID";
                cboBlock.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadBlockMaps", error);
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
                string status = "11";//field connection
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
                docketdisplay.Visible = false;
                lblapplicant.Visible = false;
                btnlinks.Visible = false;
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

                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    connectioninvoice.Visible = true;

                    //txtcategory.Text = GridViewIssue.Rows[index].Cells[1].Text;
                    ShowMaterialDetails(jobnumber);
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

        private void ShowMaterialDetails(string jobnumber)
        {
            //throw new NotImplementedException();
            //btnlinks.Visible = true;
            //connectioninvoice.Visible = true;
            //docketdisplay.Visible = false;
            try
            {
                DataTable dt = bll.GetSurveyReportDetails(jobnumber, 0, 0, 11);
                if (dt.Rows.Count > 0)
                {

                    string appnumber = dt.Rows[0]["ApplicationNumber"].ToString();
                    string appname = dt.Rows[0]["ApplicantName"].ToString();
                    string jobnum = dt.Rows[0]["JobNumber"].ToString();
                    lblApplicationCode.Text = dt.Rows[0]["applicationID"].ToString();
                    string authorizer = Session["FullName"].ToString();
                    //DateTime surveydt = Convert.ToDateTime(dt.Rows[0]["surveyDate"].ToString());
                    //txtsurveydate.Text = surveydt.ToString("dddd, dd MMMM yyyy");//
                    //txtsurvey.Text = dt.Rows[0]["AssignedTo"].ToString();
                    lblarea.Text = dt.Rows[0]["areaId"].ToString();
                    string customertype = dt.Rows[0]["typeName"].ToString();
                    string category = dt.Rows[0]["className"].ToString();
                    string areacode = dt.Rows[0]["areaCode"].ToString();
                    //load controls
                    btnlinks.Visible = true;
                    string applicant = appnumber + "-->" + appname.Trim();
                    lblapplicant.Text = applicant;
                    lblareacode.Text = areacode;
                    //see customer details
                     showmaterialdetails(lblApplicationCode.Text);

                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private void showmaterialdetails(string text)
        {
            string appid = lblApplicationCode.Text.Trim();
            LoadMaterialCategories();
            LoadCostingMaterials(int.Parse(appid));
            LoadExpenseItems(int.Parse(appid));
            LoadPipeDiameterList();
            LoadPipeTypeList();
        }


        //protected void rtnTariff_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}



        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }






        protected void btnmaterials_Click(object sender, EventArgs e)
        {
            connectioninvoice.Visible = true;
            //materialdisplay.Visible = true;
            docketdisplay.Visible = false;
            LoadMaterialCategories();
            string appid = lblApplicationCode.Text.Trim();
            LoadCostingMaterials(int.Parse(appid));
            LoadExpenseItems(int.Parse(appid));
            LoadPipeDiameterList();
            LoadPipeTypeList();
            DisplayMessage(".", false);
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
                string comment = txtcomment.Text.Trim();
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
                else if (comment == "")
                {
                    DisplayMessage("Please enter a valid general field comment", true);
                }
                else
                {
                    //save details
                    resp = bll.SaveFieldExpenseLogs(estimateid, applicationid, pipediameter, pipetype, pipelength, excavationlength, createdby,comment);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with field expense details against application(" + applicant + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {

                        string str = " with field expense details against application(" + applicant + ") updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    //log to next level
                    bll.LogApplicationTransactions(int.Parse(applicationid), 12, int.Parse(createdby));
                    //clear conrols
                    ClearEstimatesControls();
                }

            }
            catch (Exception ex)
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
                    string returned = bll.SaveExpenditureDetails(CostCode, ApplicationCode, ExpenseItemCode, Size, Length, Quantity, UnitCost);
                    DisplayMessage(returned, false);
                    ClearCostingControls();
                    //LoadCostingControls();
                    LoadExpenseItems(int.Parse(ApplicationCode));

                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        private void LoadMaterialCategories()
        {
            string type = "EXPENSE";
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
            if(category_code.Equals(""))
            {
                category_code = "1";
            }
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
        private void LoadExpenseItems(int ApplicationID)
        {
            DataTable dataTable = bll.GetExpenseItems(ApplicationID);
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
        private void ClearDocketControls()
        {
            txtpipelength.Text = "";
            txtexcavation.Text = "";
            cboMeterSize.SelectedValue = "0";
            cboType.SelectedValue = "0";
            lblestimateid.Text = "0";
            txtcomment.Text = "";
            txtMeterRef.Text = "";
            cboBlock.SelectedValue = "0";
            txtConnectionNo.Text = "";
            txtNumber.Text = "";
            txtcomment.Text = "";
            txtDials.Text = "";
            txtInstallationDate.Text = "";
            txtInstalledby.Text = "";
            txtlattitude.Text = "";
            txtlongitude.Text = "";
            txtMeterLife.Text = "";
            txtManufacturedDate.Text = "";
            txtReading.Text = "";

        }
        private void ClearEstimatesControls()
        {
            txtpipelength.Text = "";
            txtexcavation.Text = "";
            pipediameter_list.SelectedValue = "0";
            pipematerial_list.SelectedValue = "0";
            lblestimateid.Text = "0";

        }

        protected void btndocket_Click(object sender, EventArgs e)
        {
            //materialdisplay.Visible = false;
            docketdisplay.Visible = true;
            connectioninvoice.Visible = false;
            LoadMaterialCategories();
            string appid = lblApplicationCode.Text.Trim();
            LoadPipeSizeList();
            LoadMeterTypes();
            LoadBlockSession();
            LoadDocketByApplication(int.Parse(appid));
            DisplayMessage(".", false);
        }

        private void LoadDocketByApplication(int appid)
        {
            try
            {
                DataTable dtapp = bll.GetFieldDocketByApplication(appid);
                if(dtapp.Rows.Count > 0)
                {
                    lblConnectionCode.Text = dtapp.Rows[0]["connectionId"].ToString();
                    txtMeterRef.Text = dtapp.Rows[0]["meterRef"].ToString();
                    txtDials.Text = dtapp.Rows[0]["dials"].ToString();
                    txtMeterLife.Text = dtapp.Rows[0]["meterLife"].ToString();
                    txtNumber.Text = dtapp.Rows[0]["meterNumber"].ToString();
                    txtReading.Text = dtapp.Rows[0]["initialReading"].ToString();
                    txtConnectionNo.Text = dtapp.Rows[0]["plotNumber"].ToString();
                    txtRemark.Text = dtapp.Rows[0]["remark"].ToString();
                    txtlongitude.Text = dtapp.Rows[0]["longitude"].ToString();
                    txtlattitude.Text = dtapp.Rows[0]["latitude"].ToString();
                    txtInstalledby.Text = dtapp.Rows[0]["installedBy"].ToString();
                    txtInstallationDate.Text = dtapp.Rows[0]["installedDate"].ToString();
                    //DateTime installdt = Convert.ToDateTime(dt.Rows[0]["installedDate"].ToString());
                    //txtInstallationDate.Text = installdt.ToString("dd-M-yy");//
                    txtManufacturedDate.Text = dtapp.Rows[0]["manufactureDate"].ToString();
                    string blockno = dtapp.Rows[0]["blockNumber"].ToString();
                    string metertype = dtapp.Rows[0]["meterTypeId"].ToString();
                    string metersize = dtapp.Rows[0]["meterSizeId"].ToString();
                    cboMeterSize.SelectedIndex = cboMeterSize.Items.IndexOf(cboMeterSize.Items.FindByValue(metersize));
                    cboType.SelectedIndex = cboType.Items.IndexOf(cboType.Items.FindByValue(metertype));
                    cboBlock.SelectedIndex = cboBlock.Items.IndexOf(cboBlock.Items.FindByText(blockno));
                }
            }
            catch(Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private void LoadBlockSession()
        {
            string areaid = Session["areaId"].ToString();
            string branchid = Session["branchId"].ToString();
            LoadBlockMaps(areaid, branchid);
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
        private void LoadMeterSize()
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
                bll.Log("LoadMeterSize", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboType_DataBound(object sender, EventArgs e)
        {
            cboType.Items.Insert(0, new ListItem("- - select meter Type - -", "0"));
        }
        protected void cboMeterSize_DataBound(object sender, EventArgs e)
        {
            cboMeterSize.Items.Insert(0, new ListItem("- - select meter size - -", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string RecordCode = lblMeterCode.Text.Trim();
                string ConnectionCode = lblConnectionCode.Text.Trim();
                string meterref = txtMeterRef.Text.Trim();
                string applicationid = lblApplicationCode.Text.Trim();
                string pipediameter = cboMeterSize.SelectedValue.ToString();
                string metertype = cboType.SelectedValue.ToString();
                string meternumber = txtNumber.Text.Trim();
                string remark = txtRemark.Text.Trim();
                string createdby = Session["UserID"].ToString();
                string applicant = lblapplicant.Text.Trim();
                string longitude = txtlongitude.Text.Trim();
                string latitude = txtlattitude.Text.Trim();
                string reading = txtReading.Text.Trim();
                string dials = txtDials.Text.Trim();
                string meterlife = txtMeterLife.Text.Trim();
                string manufacturedate = txtManufacturedDate.Text.Trim();
                string installedby = txtInstalledby.Text.Trim();
                string installdate = txtInstallationDate.Text.Trim();
                string blocknumber = cboBlock.SelectedItem.ToString();
                string connectionno = txtConnectionNo.Text.Trim();
                DateTime installdt = Convert.ToDateTime(installdate);
                DateTime manufacturedt = Convert.ToDateTime(manufacturedate);
                //if (!ConnectionCode.Equals("0"))//record being updated
                //{
                //    installdt = DateTime.Parse(installdate);
                //    manufacturedt = DateTime.Parse(manufacturedate);
                //}
                if (pipediameter == "0")
                {
                    DisplayMessage("Please select pipe diameter/size", true);
                }
                else if (metertype == "0")
                {
                    DisplayMessage("Please select meter type", true);
                }
                else if (meternumber == "")
                {
                    DisplayMessage("Please enter meter number/serial", true);
                }
                else if (reading == "")
                {
                    DisplayMessage("Please enter initial reading on meter", true);
                }
                else if (remark == "")
                {
                    DisplayMessage("Please enter a valid general field comment", true);
                }
                else if (dials == "")
                {
                    DisplayMessage("Please enter valid dials on meter", true);
                }
                else if (!bll.IsValidReadingDate(installdate))
                {
                    string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                    DisplayMessage("Invalid Meter Installation Date, It cannot be greater than Today ( " + Todate + " )", true);
                }
                else
                {
                    //save details
                    resp = bll.SaveFieldDocket(RecordCode, applicationid, pipediameter, metertype, meterref, meternumber, createdby, remark,longitude,latitude,
                        reading,dials,meterlife,manufacturedt,installedby,installdt,blocknumber,connectionno);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with field docket details against application(" + applicant + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {

                        string str = " with field docket details against application(" + applicant + ") updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    //log to next level
                    int status = 12;//forward to billing
                    bll.LogApplicationTransactions(int.Parse(applicationid), status, int.Parse(createdby));
                    //clear conrols
                    ClearDocketControls();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void cboBlock_DataBound(object sender, EventArgs e)
        {
            cboBlock.Items.Insert(0, new ListItem("- - Select - -", "0"));
        }
        protected void btnGetNumber_Click(object sender, EventArgs e)
        {
            try
            {
                string BlockNumber = cboBlock.SelectedItem.ToString();

                if (cboBlock.SelectedValue.ToString() == "0")
                {
                    DisplayMessage("Please Select Block Map Number", true);
                }
                else
                {
                    string areaid = Session["areaId"].ToString();
                    string branchid = Session["branchId"].ToString();
                    DataTable dataTable = bll.GetBlockConnectionNumber(areaid,branchid,BlockNumber);
                    if (dataTable.Rows.Count > 0)
                    {
                        int OldNumber = Convert.ToInt16(dataTable.Rows[0]["connectionNumber"].ToString());
                        int NewNumber = OldNumber + 1;
                        txtConnectionNo.Text = NewNumber.ToString();
                       
                    }
                    else
                    {
                        DisplayMessage("No Results founds for Block Number " + BlockNumber, true);
                        //txtMapNo.Focus();
                        txtConnectionNo.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btnGetMeterRef_Click(object sender, EventArgs e)
        {
            try
            {
                string BlockNumber = cboBlock.SelectedItem.ToString();
                string connectionno = txtConnectionNo.Text.Trim();
                string areacode = lblareacode.Text;
                if (cboBlock.SelectedValue.ToString() == "0")
                {
                    DisplayMessage("Please Select Block Map Number", true);
                }
                else if (connectionno == "")
                {
                    DisplayMessage("Please generate Number", true);
                }
                else
                {
                    string meterref = bll.GetMeterReference(areacode, BlockNumber, connectionno);
                    txtMeterRef.Text = meterref;
                }
                
            }
            catch(Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
    }
}