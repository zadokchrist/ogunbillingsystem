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
    public partial class CustomerCreation : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        CustomerObj cust = new CustomerObj();
        ResponseMessage resp = new ResponseMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    if (Session["RoleID"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    //LoadCountryList();
                    //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    //LoadAreaList(countryid);
                    LoadBranchList(10);
                    LoadConnectionDetails();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Customer Creation page");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        
        //private void LoadAreaList(int countryid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetAreaList(countryid);
        //        area_list.DataSource = dt;

        //        area_list.DataTextField = "areaName";
        //        area_list.DataValueField = "areaId";
        //        area_list.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayAreaList", error);
        //        DisplayMessage(error, true);
        //    }
        //}
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
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        //protected void area_list_DataBound(object sender, EventArgs e)
        //{
        //    area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        //}

        
        private void LoadConnectionDetails()
        {
            try
            {
                string jobnumber = "";
                string country = "2";
                string area = "10";
                string status = "12";//customer creation
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
                customerdisplay.Visible = false;
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
                    string appnumber = gv_surveyjobs.Rows[index].Cells[2].Text;

                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    customerdisplay.Visible = true;
                    LoadMeterSize();
                    LoadMeterTypes();
                    LoadCategory();
                    LoadClassfication();
                    LoadTariff("0");
                    LoadCustomerTypeList();
                    ShowCustomerDetails(appnumber);
                }
            }
          
        }
        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void ShowCustomerDetails(string appnumber)
        {
            try
            {
                DataTable dt = bll.GetNewConnectionCustomerDetails(appnumber);
                if(dt.Rows.Count > 0)
                {
          
                   txtappnumber.Text = dt.Rows[0]["applicationNumber"].ToString();
                    string appid = dt.Rows[0]["ApplicationID"].ToString();
                    txtappdate.Text = dt.Rows[0]["ApplicationDate"].ToString();
                    txtfullname.Text = dt.Rows[0]["fullName"].ToString();
                    txtaddress.Text = dt.Rows[0]["address"].ToString();
                    txtphone1.Text = dt.Rows[0]["contact"].ToString();
                    txtoccupation.Text = dt.Rows[0]["ocupation"].ToString();
                    //txtarea.Text = dt.Rows[0]["areaName"].ToString();
                    txtarea.Text = dt.Rows[0]["operationAreaName"].ToString();
                    string custtype = dt.Rows[0]["typeName"].ToString();
                    string classtype = dt.Rows[0]["className"].ToString();
                    txtmeterNumber.Text = dt.Rows[0]["meterNumber"].ToString();
                    txtMeterRef.Text = dt.Rows[0]["meterRef"].ToString();
                    string metersize = dt.Rows[0]["meterSizeId"].ToString();
                    string metertype = dt.Rows[0]["meterTypeId"].ToString();
                    txtconnectionno.Text = dt.Rows[0]["plotNumber"].ToString();
                    txtblock.Text = dt.Rows[0]["blockNumber"].ToString();
                    txtlattitude.Text = dt.Rows[0]["latitude"].ToString();
                    txtlongitude.Text = dt.Rows[0]["longitude"].ToString();
                    string areacode = dt.Rows[0]["areaCode"].ToString();
                    string area = dt.Rows[0]["areaId"].ToString();
                    string branch = dt.Rows[0]["branch"].ToString();
                    string custref = dt.Rows[0]["custref"].ToString();
                   // txtcustref.Text = custref;
                    lblCustomerCode.Text = custref;
                    lblarea.Text = area;
                    lblbranch.Text = branch;
                    lblApplicationCode.Text = appid;
                    string block = txtblock.Text;
                    if (block.StartsWith("0"))
                    {
                        block = block.Remove(0, 1);
                    }
                    string book = block.Substring(0, 2);
                    string walk = block.Substring(2);
                    if (walk.StartsWith("0"))
                    {
                        walk = walk.Remove(0, 1);
                    }
                    string newblock = book + "/" + walk;
                    // string property = areacode+ "/" + txtblock.Text + "/" + txtconnectionno.Text;
                    string property = areacode + "/" + newblock + "/" + txtconnectionno.Text;
                    txtproperty.Text = property;
                    txtservicetype.Text = dt.Rows[0]["serviceName"].ToString();
                    //P.tariffId,P.classId,P.categoryId,P.disconnectionId,P.IsActive,P.IsSewer
                    string tariff = dt.Rows[0]["tariffId"].ToString();
                    string classid = dt.Rows[0]["classId"].ToString();
                    string category = dt.Rows[0]["categoryId"].ToString();
                    string disconstatus = dt.Rows[0]["disconnectionId"].ToString();
                    string isactive = dt.Rows[0]["IsActive"].ToString();
                    string issewer = dt.Rows[0]["IsSewer"].ToString();
                    string areasewer = dt.Rows[0]["hasSewer"].ToString();
                    cboMeterSize.SelectedIndex = cboMeterSize.Items.IndexOf(cboMeterSize.Items.FindByValue(metersize));
                    cboType.SelectedIndex = cboType.Items.IndexOf(cboType.Items.FindByValue(metertype));
                    cboclass.SelectedIndex = cboclass.Items.IndexOf(cboclass.Items.FindByText(classtype));
                    customertype_list.SelectedIndex = customertype_list.Items.IndexOf(customertype_list.Items.FindByText(custtype));
                    if(!custref.Equals("0"))//for valid custref
                    {
                        cboclass.SelectedIndex = cboclass.Items.IndexOf(cboclass.Items.FindByValue(classid));
                        cbotariff.SelectedIndex = cbotariff.Items.IndexOf(cbotariff.Items.FindByValue(tariff));
                        cbocategory.SelectedIndex = cbocategory.Items.IndexOf(cbocategory.Items.FindByValue(category));
                        rtnSupplytype.SelectedIndex = rtnSupplytype.Items.IndexOf(rtnSupplytype.Items.FindByValue(disconstatus));
                        txtphone1.Text = dt.Rows[0]["phoneNo1"].ToString();
                        txtphone2.Text = dt.Rows[0]["phoneNo2"].ToString();
                        txtemail.Text = dt.Rows[0]["custEmail"].ToString();
                        txtlattitude.Text = dt.Rows[0]["latitude"].ToString();
                        txtlongitude.Text = dt.Rows[0]["longitude"].ToString();
                       // txttitle.Text = dt.Rows[0]["title"].ToString();
                        if (isactive.Equals("True"))
                        {
                            chkactive.Checked = true;
                        }
                        //if (issewer.Equals("True"))
                        //{
                        //    chksewer.Checked = true;
                        //}
                    }
                    //if(areasewer.Equals("True"))
                    //{
                       
                    //    chksewer.Enabled = true;
                    //}
                    //else
                    //{
                    //    chksewer.Checked = false;
                    //    chksewer.Enabled = false;

                    //}
                    ManageCreationControls(custtype);

                }
                else
                {
                    customerdisplay.Visible = false;
                }
            }
            catch(Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                cust = new CustomerObj();
                cust.CustomerType = customertype_list.SelectedValue.ToString();
                string custtype = customertype_list.SelectedItem.ToString();
                cust.CustName = txtfullname.Text.Trim();
                cust.ApplicationNo = txtappnumber.Text.Trim();
                cust.ApplicationId = lblApplicationCode.Text;
                cust.Email = txtemail.Text.Trim();
                cust.Occupation = txtoccupation.Text.Trim();
                cust.Contact1 = txtphone1.Text.Trim();
                cust.Contact2 = txtphone2.Text.Trim();
                cust.Address = txtaddress.Text.Trim();
                //cust.Title = txttitle.Text.Trim();
                cust.ConnectionNumber = txtconnectionno.Text.Trim();
                //cust.Country = country_list.SelectedValue.ToString();
                
                cust.MeterRef = txtMeterRef.Text.Trim();
                cust.PropertyRef = txtproperty.Text.Trim();
                cust.Latitude = txtlattitude.Text.Trim();
                cust.Longitude = txtlongitude.Text.Trim();
                cust.Category = cbocategory.SelectedValue.ToString();
                cust.Classification = cboclass.SelectedValue.ToString();
                cust.Tariff = cbotariff.SelectedValue.ToString();
                cust.SupplyStatus = rtnSupplytype.SelectedValue.ToString();
                cust.IsActive = chkactive.Checked;
                cust.HasSewer = false;//chksewer.Checked;
                cust.Area = lblarea.Text;
                cust.Branch = lblbranch.Text;
                cust.CreatedBy = Session["UserID"].ToString();
                cust.ApplicationId = lblApplicationCode.Text;
                cust.Block = txtblock.Text.Trim();
                cust.CustRef = lblCustomerCode.Text;
                cust.MeterNumber = txtmeterNumber.Text.Trim();
                cust.OperationId = Session["operationId"].ToString();
                // cust.Territory = txtterritory.Text.Trim();

                string str = ""; string res = "";

                //set deault
                //app.ApplicationDate = DateTime.Now;
                cust.Status = "13"; //creeated
                                    //validate input
                resp = bll.ValidateCustomer(cust.CustName, cust.MeterRef, cust.PropertyRef, cust.CustomerType, cust.Category);
                if (custtype.ToLower().Contains("paid"))
                {
                    cust.MeterMake = cboType.SelectedValue.ToString();
                    cust.MeterSize = cboMeterSize.SelectedValue.ToString();                   
                   
                }
                else//flatrate
                {
                    cust.MeterMake = "0";
                    cust.MeterSize = "0";
                    txtmeterNumber.Text = "";
                }
                if (resp.Response_Code.ToString().Equals("0"))
                {

                    //resp.Response_Code="test"; //test only
                    resp = bll.SaveCustomerDetails(cust);
                    if (resp.Response_Code == "1")
                    {
                        if (cust.CustRef.Equals("0"))
                        {
                            str = " details saved";
                        }
                        else
                        {
                            str = " details updated";
                        }


                        res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else
                    {
                        str = " details with application identity(" + cust.ApplicationNo + ")";
                        res = resp.Response_Message + str;
                        DisplayMessage(res, true);
                    }



                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }
                RefreshControls();




                //end default

            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        private void RefreshControls()
        {
            try
            {
                txtappnumber.Text = "";
                txtappdate.Text = "";
                txtfullname.Text = "";
                txtaddress.Text = "";
                txtphone1.Text = "";
                txtoccupation.Text ="";
                txtarea.Text = "";             
                txtmeterNumber.Text = "";
                txtMeterRef.Text = "";              
                txtconnectionno.Text = "";
                txtblock.Text = "";
                txtlattitude.Text = "";
                txtlongitude.Text ="";             
                //txtcustref.Text = "";
                lblCustomerCode.Text = "0";
                lblarea.Text = "0";
                lblbranch.Text = "0";
                lblApplicationCode.Text = "0";            
                txtproperty.Text = "";
                txtservicetype.Text = "";
                cbocategory.SelectedValue = "0";
                cboclass.SelectedValue = "0";
                cboMeterSize.SelectedValue = "0";
                cbotariff.SelectedValue = "0";
                cboType.SelectedValue = "0";
                rtnSupplytype.SelectedValue = "0";
                chkactive.Checked = false;
                //chksewer.Checked = false;
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

        private void LoadClassfication()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCustomerClass();
                cboclass.DataSource = dt;

                cboclass.DataTextField = "className";
                cboclass.DataValueField = "classID";
                cboclass.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadClassfication", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadCategory()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCustomerCategory();
                cbocategory.DataSource = dt;

                cbocategory.DataTextField = "categoryName";
                cbocategory.DataValueField = "categoryID";
                cbocategory.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadCategory", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadTariff(string classid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetTariff(classid);
                cbotariff.DataSource = dt;

                cbotariff.DataTextField = "tarrifName";
                cbotariff.DataValueField = "tarrifId";
                cbotariff.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadTariff", error);
                DisplayMessage(error, true);
            }
        }
        protected void cboType_DataBound(object sender, EventArgs e)
        {
            cboType.Items.Insert(0, new ListItem("- - select - -", "0"));
        }
        protected void cboMeterSize_DataBound(object sender, EventArgs e)
        {
            cboMeterSize.Items.Insert(0, new ListItem("- - select  - -", "0"));
        }
        protected void cbotariff_DataBound(object sender, EventArgs e)
        {
            cbotariff.Items.Insert(0, new ListItem("- - select  - -", "0"));
        }
        protected void cbocategory_DataBound(object sender, EventArgs e)
        {
            cbocategory.Items.Insert(0, new ListItem("- - select  - -", "0"));
        }
        protected void cboclass_DataBound(object sender, EventArgs e)
        {
            cboclass.Items.Insert(0, new ListItem("- - select  - -", "0"));
        }
        protected void customertype_list_DataBound(object sender, EventArgs e)
        {
            customertype_list.Items.Insert(0, new ListItem("- - select- -", "0"));
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

        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        protected void cboclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string classid = cboclass.SelectedValue.ToString();
                LoadTariff(classid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                customerdisplay.Visible = false;
                LoadConnectionDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ManageCreationControls(string customertype)
        {
            if (customertype.ToLower().Contains("flat"))
            {
               
                cboType.Enabled = false;
                txtmeterNumber.Enabled = false;
                cboMeterSize.Enabled = false;
                
            }
            else
            {
                cboType.Enabled = true;
                txtmeterNumber.Enabled = true;
                cboMeterSize.Enabled = true;
            }
        }
        protected void branch_list_DataBound(object sender, EventArgs e)
        {
            branch_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        private void LoadBranchList(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid,0);
                branch_list.DataSource = dt;
                branch_list.DataTextField = "branchName";
                branch_list.DataValueField = "branchId";
                branch_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayBranchList", error);
                DisplayMessage(error, true);
            }
        }
        //protected void customertype_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string custytype = customertype_list.SelectedItem.ToString();

        //        ManageCreationControls(custytype);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

    }
}