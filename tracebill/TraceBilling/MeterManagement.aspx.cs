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
            meterinventory.Visible = true;
            LoadMeterTypes();
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
            LoadDisplay();
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

    }
}