using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;

namespace TraceBilling
{
    public partial class SurveyJobs : System.Web.UI.Page
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
                    LoadApplicationByStatus();
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
        private void LoadSurveyList(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetUserList(countryid,5);
                survey_list.DataSource = dt;

                survey_list.DataTextField = "fullname";
                survey_list.DataValueField = "userId";
                survey_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplaySurveyList", error);
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
        protected void survey_list_DataBound(object sender, EventArgs e)
        {
            survey_list.Items.Insert(0, new ListItem("- - select survey - -", "0"));
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
                
                LoadApplicationByStatus();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadApplicationByStatus()
        {
            try
            {
                string applicationame = txtapplicationname.Text.Trim();
                string country = country_list.SelectedValue.ToString();
                string area = area_list.SelectedValue.ToString();
                string status = "1";
                DataTable dataTable = bll.GetApplicationByStatus(applicationame,country,area,status);
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
                    bll.Log("GetApplication", error);
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
                assignsurvey.Visible = false;
                LoadApplicationByStatus();
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
            if (commandName == "btnDetails")//details
            {
                //execute
                //int index = e.CommandArgument;
                int index = Int32.Parse((string)e.CommandArgument);
                if (index >= 0)
                {
                   // dispatchdisplay.Visible = true;
                    string appnumber = gv_surveyjobs.Rows[index].Cells[1].Text;
                  
                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    assignsurvey.Visible = true;
                    //txtcategory.Text = GridViewIssue.Rows[index].Cells[1].Text;
                    ShowSurveyDetails(appnumber);
                }
            }
            else if (commandName == "btnJobCard")//routecard
            {
                string str = "Sorry, Job card details not available yet!!!";
                DisplayMessage(str, true);
            }
            //Get the Row Index.
            //int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Get the Row reference in which Button was clicked.
            //GridViewRow row = GridView1.Rows[rowIndex];
        }

        private void ShowSurveyDetails(string appnumber)
        {
            try
            {
                DataTable dt = bll.GetSurveyDetails(appnumber);
                if(dt.Rows.Count > 0)
                {
                   // A.applicationNumber,(A.firstName + '' + A.lastName) as 'fullName'
                    txtappcode.Text = dt.Rows[0]["applicationNumber"].ToString();
                    txtname.Text = dt.Rows[0]["fullName"].ToString();
                    lblApplicationCode.Text = dt.Rows[0]["applicationID"].ToString();
                    int country = Convert.ToInt32(dt.Rows[0]["countryId"].ToString());
                    LoadSurveyList(country);
                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        
        }

        protected void btnreturn2_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                assignsurvey.Visible = false;
                LoadApplicationByStatus();
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string ApplicationCode = lblApplicationCode.Text.Trim();
                string Surveyor = survey_list.SelectedValue.ToString();
                if (Surveyor == "0")
                {
                    string str = "Please Select Surveyor to Assign";
                    DisplayMessage(str, true);
                }
                else
                {
                    string returned = bll.AssignSurveyJob(ApplicationCode, Surveyor);
                    DisplayMessage(returned, false);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);

            }
        }

        protected void btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string StringToProcess = GetJobToGenerate().TrimEnd(',');
                string returned = bll.GenerateJobCards(StringToProcess, "WATER");
                if (returned.Contains("Successfully"))
                {
                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadApplicationByStatus();
                }
                if (returned.Contains("Successfully"))
                {
                    DisplayMessage(returned, false);
                }
                else
                {
                    DisplayMessage(returned, true);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private string GetJobToGenerate()
        {
            int Count = 0;
            string ItemArr = "";
            foreach (GridViewRow row in gv_surveyjobs.Rows)
            {
               // CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
                CheckBox chk = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                if (chk.Checked)
                {
                    Count++;
                    string ItemFound = row.Cells[1].Text;
                    ItemArr = ItemArr += ItemFound + ",";
                }
            }
            return ItemArr;
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectAllItems();
                if (chkSelect.Checked == true)
                {
                    chkSelect.Checked = true;
                }
                else
                {
                    chkSelect.Checked = false;
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        private void SelectAllItems()
        {
           
            foreach (GridViewRow row in gv_surveyjobs.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = (row.Cells[0].FindControl("chkCtrl") as CheckBox);
                    if (chk.Checked)
                    {
                        chk.Checked = false;
                    }
                    else
                    {
                        chk.Checked = true;
                    }
                }
            }
        }
    }
}