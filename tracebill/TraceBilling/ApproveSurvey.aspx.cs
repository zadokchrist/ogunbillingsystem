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
//using System.Web.UI.WebControl.
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

                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadSurveyReportDetails();
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
            country_list.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- - select country - -", "0"));
        }
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- - select area - -", "0"));
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
                string jobnumber = txtjobnumber.Text.Trim();
                string country = country_list.SelectedValue.ToString();
                string area = area_list.SelectedValue.ToString();
                string status = "3";//jobcard
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
                DataTable dt = bll.GetSurveyReportDetails(jobnumber,0,0,int.Parse(status));
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
            /*if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[1];
                string status = e.Row.Cells[1].Text;
                if ((status.Equals("Emergency")))
                {

                    link.ForeColor = Color.Red;
                    //e.Row.BackColor = Color.Red;
                    //e.Row.ForeColor = Color.White;
                }
                else if ((status.Equals("General")))
                {

                    link.ForeColor = Color.Green;
                    // e.Row.BackColor = Color.Green;
                    // e.Row.ForeColor = Color.White;
                }
                else if ((status.Equals("Others")))
                {

                    link.ForeColor = Color.Blue;
                    // e.Row.BackColor = Color.Blue;
                    //e.Row.ForeColor = Color.White;
                }
            }*/
        }

        protected void btnreturn2_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                approvesurvey.Visible = false;
                surveydisplay.Visible = false;
                LoadSurveyReportDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                //save checklist item by id
                //ArrayList a = new ArrayList();
                 string str = "";
                /* for (int i = 0; i < chkBoxRequired.Items.Count; i++)
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

                DisplayMessage(res, false);
                */
                //send to next status 3
                //new implementation
                string str_todump = GetRecordsToDump();
                string surveydate = txtsurveyDate.Text.Trim();
                if (str_todump.Equals(""))
                {
                    str = "Please Select at least one survey question";
                    DisplayMessage(str, true);
                }
                else
                {
                    LoadConfirmations(str_todump,surveydate);
                    if (al.Count > 0)
                    {
                        
                        DisplayMessage(al.Count.ToString() + " survey questions selected and successfully approved.", true);
                    }
                }
                ClearApproveControls();
               
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);

            }
        }

        private void ClearApproveControls()
        {
            txtsurveyDate.Text = "";
            chkBoxRequired.ClearSelection();
            
        }

        private void LoadConfirmations(string str_todump, string surveydate)
        {
            try
            {
                string str = str_todump.TrimEnd(',');
                string[] arr = str.Split(',');
                string createdby = Session["userId"].ToString();
                string appid = lblApplicationCode.Text;
                DateTime Surveydt = Convert.ToDateTime(surveydate);
                foreach (string s in arr)
                {
                    string[] separators = { "+" };
                    string[] param = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string qnid = param[0].Trim();
                    string qn = param[1].Trim();
                    string ans = param[3].Trim();
                    
                    //save to Db
                    bll.SaveSurveyDetails(qnid, ans, appid,createdby,Surveydt);
                   
                }
                //take log of surveying
                bll.LogApplicationTransactions(int.Parse(appid), 2, int.Parse(createdby));
                //log approval
                bll.LogApplicationTransactions(int.Parse(appid), 4, int.Parse(createdby));

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private string GetRecordsToDump()
        {
            int Count = 0;
            string ItemArr = "";
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
                    ItemArr += Item1 + "+" + Item2 + "+" + Item3 + "+" + Item4  + ",";
                    //add to uploaded arraylist
                    al.Add(Count);
                }
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
        private void FrmExport_Load(object sender, EventArgs e)
        {
            LoadSurveyDisplay();
        }
       

      

        protected void btnjobcard_Click(object sender, EventArgs e)
        {
            DataTable dt = bll.GetSurveyQnList();
            DataGrid1.DataSource = dt;
            ExcelReport(dt);
           /* DataGrid1.CurrentPageIndex = 0;
            //DataGrid1.DataBind();
            Response.ContentType = "application / pdf";
            Response.AddHeader("content - disposition",  "attachment; filename = JobCard.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DataGrid1.AllowPaging = false;
            DataGrid1.DataBind();
           // DataGrid1.RenderControl(hw);
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
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        //private void button1_Click_1(object sender, EventArgs e)
        //{
        //    DataTable dt = bll.GetSurveyQnList();
        //    // creating Excel Application  
        //    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
        //    // creating new WorkBook within Excel application  
        //    Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
        //    // creating new Excelsheet in workbook  
        //    Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
        //    // see the excel sheet behind the program  
        //    app.Visible = true;
        //    // get the reference of first sheet. By default its name is Sheet1.  
        //    // store its reference to worksheet  
        //    worksheet = workbook.Sheets["Sheet1"];
        //    worksheet = workbook.ActiveSheet;
        //    // changing the name of active sheet  
        //    worksheet.Name = "Exported from gridview";
        //    // storing header part in Excel  
        //    for (int i = 1; i < dt.Columns.Count + 1; i++)
        //    {
        //        worksheet.Cells[1, i] = dt.Columns[i - 1].HeaderText;
        //    }
        //    // storing Each row and column value to excel sheet  
        //    for (int i = 0; i < dt.Rows.Count - 1; i++)
        //    {
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            worksheet.Cells[i + 2, j + 1] = dt.Rows[i].Cells[j].Value.ToString();
        //        }
        //    }
        //    // save the application  
        //    workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //    // Exit from the application  
        //    app.Quit();
        //}
    }
}