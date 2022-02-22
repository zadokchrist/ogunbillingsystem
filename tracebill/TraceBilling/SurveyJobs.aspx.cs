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
                    if (Session["RoleID"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    //LoadCountryList();
                    //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    //LoadAreaList(countryid);
                    LoadApplicationByStatus();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Survey Jobs page");
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
        private void LoadSurveyList(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetUserListByID(countryid,5,1);
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
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        //protected void area_list_DataBound(object sender, EventArgs e)
        //{
        //    area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        //}
        protected void survey_list_DataBound(object sender, EventArgs e)
        {
            survey_list.Items.Insert(0, new ListItem("- - select - -", "0"));
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
                string applicationame = "";
                string country = "2";
                string area = "10";
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

                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                string appnumber = arg[0];
                string appid = arg[1];
                bool isassigned = bll.CheckJobAssigned(appid,"1");
                if (isassigned)
                {
                    DisplayMessage(".", true);
                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    assignsurvey.Visible = true;
                    ShowSurveyDetails(appnumber);
                }
                else
                {
                    string str = "Sorry, Job card needs to be generated before assigning it.";
                    DisplayMessage(str, true);
                }


            }
            //else if (commandName == "btnJobCard")//routecard
            //{
            //    string[] arg = new string[2];
            //    arg = e.CommandArgument.ToString().Split(';');
            //    string appnumber = arg[0];
            //    string appid = arg[1];
            //    string str = "Sorry, Job card details not ready for printing!!!";
            //    DisplayMessage(str, true);
            //}
           else  if (commandName == "btnSubmit")//details
            {

                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                string appnumber = arg[0];
                string appid = arg[1];
               
                //string appid = lblApplicationCode.Text;
                int CreatedBy = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
                int status = 4;//surveying
                               //log status
                bool isassigned = bll.CheckJobAssigned(appid, "2");
                string str = "";
                if (isassigned)
                {
                    //log job card
<<<<<<< HEAD
                    bll.LogApplicationTransactions(int.Parse(appid), 3, CreatedBy);//send to 5
=======
                    bll.LogApplicationTransactions(int.Parse(appid), 3, CreatedBy);
>>>>>>> 9c0520f00807ab170598d17142cd6b13a408cf4d
                    //approve survey
                    bll.LogApplicationTransactions(int.Parse(appid), status, CreatedBy);
                    str = "Jobcard submitted to Surveyor for further action.";
                    DisplayMessage(str, false);
                    LoadApplicationByStatus();
                }
                else
                {
                    str = "Jobcard not assigned to any surveyor. Please assign it!!";
                    DisplayMessage(str, true);
                }


            }
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
                    //DisplayMessage(returned, true);
                    if (returned.Contains("Successfully"))
                    {
                        DisplayMessage(returned, false);
                        assignsurvey.Visible = false;
                        LoadApplicationByStatus();
                    }
                    else
                    {
                        DisplayMessage(returned, true);
                    }
                    ClearAssignControl();
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);

            }
        }

        private void ClearAssignControl()
        {
            txtappcode.Text = "";
            txtname.Text = "";
            survey_list.SelectedValue = "0";
           // txtinstructionDate.Text = "";
        }

        protected void btngenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string StringToProcess = GetJobToGenerate().TrimEnd(',');
                string returned = bll.GenerateJobCards(StringToProcess, "WATER");
                if (returned.Contains("Successfully"))
                {
                   
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

        /*protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string appid = lblApplicationCode.Text;
            int CreatedBy = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
            int status = 4;//surveying
            //log status
            bool isassigned = bll.CheckJobAssigned(appid,"2");
            string str = "";
            if(isassigned)
            {
                bll.LogApplicationTransactions(int.Parse(appid), status, CreatedBy);
                str = "Jobcard submitted to Surveyor for further action.";
                DisplayMessage(str, false);
            }
            else
            {
                str = "Jobcard not assigned to any surveyor. Please assign it!!";
                DisplayMessage(str, true);
            }
           
        }*/
        protected void gv_surveyjobs_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_surveyjobs.Rows[index].Cells[1].Text;


            }
        }
        protected void gv_surveyjobs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}