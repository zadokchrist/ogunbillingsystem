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

                    //load controls
                    btnlinks.Visible = true;
                    string applicant = appnumber + "-->" + appname.Trim();
                    lblapplicant.Text = applicant;
                    //see customer details
                    // showmaterialdetails(lblApplicationCode.Text);

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
            
//private void showmaterialdetails(string text)
//        {
//            throw new NotImplementedException();
//        }


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
            LoadCostingItems(int.Parse(appid));
            LoadPipeDiameterList();
            LoadPipeTypeList();
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

        protected void btndocket_Click(object sender, EventArgs e)
        {
            //materialdisplay.Visible = false;
            docketdisplay.Visible = true;
            connectioninvoice.Visible = false;
            LoadMaterialCategories();
            string appid = lblApplicationCode.Text.Trim();
            
            LoadPipeDiameterList();
            LoadMeterTypes();
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

        }
    }
}