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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using RKLib.ExportData;

namespace TraceBilling
{
    public partial class ApproveSurvey : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        ArrayList al = new ArrayList();
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
                        string sessioncountryid = Session["countryId"].ToString();

                        if (!sessioncountryid.Equals("1"))
                        {

                            //LoadAreaList(int.Parse(sessioncountryid));
                            //area_list.SelectedIndex = area_list.Items.IndexOf(new System.Web.UI.WebControls.ListItem(Session["area"].ToString(), Session["areaId"].ToString()));
                            //area_list.Enabled = false;
                            //int operationid = Convert.ToInt16(area_list.SelectedValue.ToString());
                            // LoadBranchList(operationid);
                        }
                        else
                        {
                            //int countryid = int.Parse(country_list.SelectedValue.ToString());
                            int countryid = int.Parse(sessioncountryid);
                           // LoadAreaList(countryid);
                        }
                        LoadSurveyReportDetails();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Approve Survey page");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void LoadCountryList()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetCountryList();
        //        country_list.DataSource = dt;

        //        country_list.DataTextField = "countryName";
        //        country_list.DataValueField = "countryId";
        //        country_list.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayCountryList", error);
        //        DisplayMessage(error, true);
        //    }
        //}
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
        private void LoadSurveyQnList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetSurveyQnList();
                chkBoxRequired.DataSource = dt;

                chkBoxRequired.DataTextField = "question";
                chkBoxRequired.DataValueField = "questionId";
                chkBoxRequired.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplaySurveyQns", error);
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
        //    country_list.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- - select country - -", "0"));
        //}
        //protected void area_list_DataBound(object sender, EventArgs e)
        //{
        //    area_list.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- - select area - -", "0"));
        //}

        //protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
        //        LoadAreaList(countryid);
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                LoadSurveyReportDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSurveyReportDetails()
        {
            try
            {
                string jobnumber = "";
                string country = "2";
                string area = "10";
                string status = "4";//approve survey report
                lblstatus.Text = status;
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
                approvesurvey.Visible = false;
                LoadSurveyReportDetails();
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
                    approvesurvey.Visible = true;
                    //txtcategory.Text = GridViewIssue.Rows[index].Cells[1].Text;
                    ShowSurveyReportDetails(jobnumber);
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

        private void ShowSurveyReportDetails(string jobnumber)
        {
            try
            {
                string status = lblstatus.Text;
                DataTable dt = bll.GetSurveyReportDetails(jobnumber, 0, 0, int.Parse(status));
                if (dt.Rows.Count > 0)
                {
                    // A.applicationNumber,(A.firstName + '' + A.lastName) as 'fullName'
                    txtappcode.Text = dt.Rows[0]["ApplicationNumber"].ToString();
                    txtname.Text = dt.Rows[0]["ApplicantName"].ToString();
                    txtjobno.Text = dt.Rows[0]["JobNumber"].ToString();
                    lblApplicationCode.Text = dt.Rows[0]["applicationID"].ToString();
                    txtarea.Text = dt.Rows[0]["Area"].ToString();
                    lblappcode.Text = txtappcode.Text;
                    LoadSurveyQnList();
                    LoadSurveyDisplay();
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


        private void LoadSurveyDisplay()
        {
            DataTable dt = bll.GetSurveyQnList();
            DataGrid1.DataSource = dt;
            DataGrid1.CurrentPageIndex = 0;
            DataGrid1.DataBind();
        }

        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        //protected void btnreturn2_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        maindisplay.Visible = true;
        //        btnreturn.Visible = true;
        //        approvesurvey.Visible = false;
        //        surveydisplay.Visible = false;
        //        LoadSurveyReportDetails();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        public string Approve()
        {
            string output = "";
            try
            {
                //save checklist item by id
                ArrayList a = new ArrayList();
                string str = "";
                for (int i = 0; i < chkBoxRequired.Items.Count; i++)
                {
                    if (chkBoxRequired.Items[i].Selected == true)// getting selected value from CheckBox List  
                    {
                        string qn = chkBoxRequired.Items[i].Text;
                        str += chkBoxRequired.Items[i].Text + " ***"; // add selected Item text to the String .  
                        a.Add(str);
                        // save to DB


                    }


                }
                string res = a.Count.ToString() + " survey questions saved and report successfully approved";

                //DisplayMessage(res, false);

                //send to next status 3
                string createdby = Session["userId"].ToString();
                string appid = lblApplicationCode.Text;
                //bll.LogApplicationTransactions(int.Parse(appid), 3, int.Parse(createdby));//send to 5
                //new implementation
                string str_todump = GetRecordsToDump();
                DateTime start = DateTime.Parse(DateTime.Now.ToShortDateString());
                String from = txtsurveyDate.Text.Trim();
                if (!from.Equals(""))
                {
                    //start = DateTime.Parse(from);
                    start = bll.GetDate(from);//european style dd/mm/yyyy
                }
               // string surveydate = txtsurveyDate.Text.Trim();
                if (str_todump.Equals(""))
                {
                    str = "Please Select at least one survey question";
                    //(str, true);
                    output = str;
                }
                else
                {
                    LoadConfirmations(str_todump, start);
                    if (al.Count > 0)
                    {

                        //DisplayMessage(al.Count.ToString() + " survey questions selected and successfully approved.", true);
                        output = al.Count.ToString() + " survey questions selected and successfully approved.";
                    }
                }
                ClearApproveControls();
            }
            catch (Exception ex)
            {
                // DisplayMessage(ex.Message, true);
                output = ex.Message;
            }
            return output;
        }
        private void ClearApproveControls()
        {
            txtsurveyDate.Text = "";
            chkBoxRequired.ClearSelection();
            

        }

        private void LoadConfirmations(string str_todump, DateTime surveydate)
        {
            try
            {
                string str = str_todump.TrimEnd(',');
                string[] arr = str.Split(',');
                string createdby = Session["userId"].ToString();
                string appid = lblApplicationCode.Text;
               // DateTime Surveydt = Convert.ToDateTime(surveydate);
                DateTime Surveydt = surveydate;

                foreach (string s in arr)
                {
                    string[] separators = { "+" };
                    string[] param = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string qnid = param[0].Trim();
                    string qn = param[1].Trim();
                    string ans = param[3].Trim();

                    //save to Db
                    bll.SaveSurveyDetails(qnid, ans, appid, createdby, Surveydt);

                }
                //take log of surveying
                //bll.LogApplicationTransactions(int.Parse(appid), 2, int.Parse(createdby));
                //log approval
                bll.LogApplicationTransactions(int.Parse(appid), 5, int.Parse(createdby));//capture invoice
                bll.LogApplicationTransactions(int.Parse(appid), 6, int.Parse(createdby));//send to 5

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private string GetRecordsToDump()
        {
            string ItemArr = "";
            try
            {
                int Count = 0;
                al = new ArrayList();

                foreach (DataGridItem Items in DataGrid1.Items)
                {
                    CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
                    if (chk.Checked)
                    {
                        Count++;
                        string Item1 = Items.Cells[0].Text;//id
                        string Item2 = Items.Cells[1].Text;//qn
                        string Item3 = Items.Cells[2].Text;//ans
                                                           //string Item4 = txtanswer;//Custname
                        TextBox ans = ((TextBox)Items.FindControl("txtanswer"));
                        string Item4 = ans.Text.Trim();
                        ItemArr += Item1 + "+" + Item2 + "+" + Item3 + "+" + Item4 + ",";
                        //add to uploaded arraylist
                        al.Add(Count);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }

            return ItemArr;
        }

        protected void btnreject_Click(object sender, EventArgs e)
        {
            try
            {
                //log with reason
                //assign status
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);

            }
        }
        //private void FrmExport_Load(object sender, EventArgs e)
        //{
        //    LoadSurveyDisplay();
        //}




        protected void btnjobcard_Click(object sender, EventArgs e)
        {
            DataTable dt = bll.GetSurveyQnList();
            DataGrid1.DataSource = dt;
            DataGrid1.CurrentPageIndex = 0;
            ExcelReport(dt);
            //DataGrid1.DataBind();
            /*Response.ContentType = "application / pdf";
            Response.AddHeader("content - disposition", "attachment; filename = JobCard.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DataGrid1.AllowPaging = false;
            DataGrid1.DataBind();
            DataGrid1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();*/
        }
        private void ExcelReport(DataTable dataTable)
        {
            try
            {
                int[] iColumns = { 0, 1, 2 };
                string appcode = lblappcode.Text;
                string filename = "JobCard-" + appcode + "-" + DateTime.Now;
                //Export the details of specified columns to Excel
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();
                objExport.ExportDetails(dataTable, iColumns, Export.ExportFormat.Excel, filename + ".xls");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string applicationid = lblApplicationCode.Text;
                string createdby = Session["UserID"].ToString();
                string action = lblaction.Text;
                string comment = txtremark.Text.Trim();
                if (comment.Equals("") || comment.Length < 5)
                {
                    DisplayMessage("Please enter valid comment", true);

                }
                else
                {

                    //log change status
                    int statusid = 0;
                    string output = "";
                    if (action.Contains("Approve"))
                    {
                        output = Approve();
                        if(output.Contains("success"))
                        {
                            bll.SaveApplicationComment(applicationid, action, comment, createdby);
                            DisplayMessage(output, false);
                        }
                        else
                        {
                            DisplayMessage(output, true);
                        }

                    }
                    else if (action.Contains("Reject"))
                    {
                        bll.SaveApplicationComment(applicationid, action, comment, createdby);
                        statusid = 18;
                        output = "Action logged successfully as " + action;
                        bll.LogApplicationTransactions(int.Parse(applicationid), statusid, int.Parse(createdby));
                        DisplayMessage(output, false);
                    }                  
                    
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ClearControls()
        {
            try
            {
                approvecon.Visible = false;
                lblaction.Text = ".";
                rtnAction.ClearSelection();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void btnapprovecancel_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                approvesurvey.Visible = false;
                surveydisplay.Visible = false;
                approvecon.Visible = false;
                LoadSurveyReportDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void rtnAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                approvecon.Visible = true;
                if(rtnAction.SelectedValue=="1")
                {
                    lblaction.Text = "Approve Survey";

                }
                else if (rtnAction.SelectedValue == "2")
                {
                    lblaction.Text = "Reject Survey";

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
