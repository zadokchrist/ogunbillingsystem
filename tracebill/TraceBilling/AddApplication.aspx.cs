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
    public partial class ApplicationForm : System.Web.UI.Page
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
                    LoadRequirementList();
                    LoadCustomerTypeList();
                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadIDList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void LoadRequirementList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetRequirementList();
                chkBoxRequired.DataSource = dt;

                chkBoxRequired.DataTextField = "requirementName";
                chkBoxRequired.DataValueField = "requirementId";
                chkBoxRequired.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayRequirement", error);
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

        private void LoadIDList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetIDList();
                cboID.DataSource = dt;

                cboID.DataTextField = "IdName";
                cboID.DataValueField = "IdType";
                cboID.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadIDList", error);
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
        protected void rtnServicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void rtnTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void country_list_DataBound(object sender, EventArgs e)
        {
            country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        }
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
        protected void cboID_DataBound(object sender, EventArgs e)
        {
            cboID.Items.Insert(0, new ListItem("- - select ID  Type- -", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                app = new ApplicationObj();
                app.CustomerType = customertype_list.Text;
                app.FirstName = txtfirstname.Text.Trim();
                app.OtherName = txtothername.Text.Trim();
                app.LastName = txtlastname.Text.Trim();
                app.Email = txtemail.Text.Trim();
                app.Occupation = txtoccupation.Text.Trim();
                app.Telephone = txtphone.Text.Trim();
                app.Address = txtaddress.Text.Trim();
                app.IdType = cboID.SelectedValue.ToString();
                app.IdNumber = txtidnumber.Text.Trim();
                app.Country = country_list.SelectedValue.ToString();
               // app.City = txtcity.Text.Trim();
               // app.State = txtstate.Text.Trim();
                app.Division = txtdivision.Text.Trim();
                app.Village = txtvillage.Text.Trim();
                app.PlotNumber = txtplot.Text.Trim();
                app.ServiceId = rtnServicetype.SelectedValue.ToString();
                app.CategoryId = rtncategory.SelectedValue.ToString();
                //set deault
                app.ApplicationDate = DateTime.Now;
                app.StatusId = "1"; //initaition
                string str = ""; string res = "";
                app.ApplicationId = "0";
                //get sessiondetails from selected
                string a = country_list.SelectedValue.ToString();
                string b = area_list.SelectedValue.ToString();
                string c = "0";//branch
                string userid = Session["userId"].ToString();
                app.Country = a;
                app.Area = b;
                app.Branch = c;
                LoadCodeSessions(a,b,c);
              
                //end default
                //validate input
                resp = bll.ValidateApplication(app.FirstName, app.LastName, app.Email, app.CustomerType, app.Occupation);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    if (app.Country.Equals("0"))
                    {
                        str = "Please select country";
                        DisplayMessage(str, true);
                    }
                    {
                        app.ApplicationNo = bll.GetApplicationNumber("0", Session["countryCode"].ToString(), Session["areaCode"].ToString(), Session["branchCode"].ToString(), userid);
                        //resp.Response_Code="test"; //test only
                        resp = bll.SaveApplication(app);
                        if (resp.Response_Code == "0")
                        {
                             str = " with new application(" + app.ApplicationNo + ") details saved";
                         
                             res = resp.Response_Message + str;
                            DisplayMessage(res, false);
                        }
                        else
                        {
                             str = " details with application identity(" + app.ApplicationNo + ")";
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
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadCodeSessions(string a,string b,string c)
        {
            Session["countryCode"] = bll.GetCodeIdentity(a,1);
            Session["areaCode"] = bll.GetCodeIdentity(b, 2);
            Session["branchCode"] = bll.GetCodeIdentity(c, 3);
        }

        private void RefreshControls()
        {
            txtfirstname.Text = "";
            txtlastname.Text = "";
            txtothername.Text = "";
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
        protected void area_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());

                //load session data
                LoadAreaSessions(areaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void LoadAreaSessions(int areaid)
        {
            try
            {
                dt = bll.GetAreaSessions(areaid);
                if(dt.Rows.Count > 0)
                {
                    //areaId,areaName,A.areaCode,C.countryCode,C.countryName,C.countryId
                    Session["areaCode"] = dt.Rows[0]["areaCode"].ToString();
                    Session["countryCode"] = dt.Rows[0]["countryCode"].ToString();
                    Session["branchCode"] = "0";
                }
                else
                {
                    Session["areaCode"] = "0";
                    Session["countryCode"] = "0";
                    Session["branchCode"] = "0";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}