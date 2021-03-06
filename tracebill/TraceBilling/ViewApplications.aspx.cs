using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.Drawing;
using RKLib.ExportData;

namespace TraceBilling
{
    public partial class ViewApplications : System.Web.UI.Page
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
                    else
                    {
                        string sessioncountryid = Session["countryId"].ToString();
                        LoadFilters(10);
                        if (!sessioncountryid.Equals("1"))
                        {

                            //LoadAreaList(int.Parse(sessioncountryid));
                            //area_list.SelectedIndex = area_list.Items.IndexOf(new ListItem(Session["area"].ToString(), Session["areaId"].ToString()));
                            //area_list.Enabled = false;
                            //int operationid = Convert.ToInt16(area_list.SelectedValue.ToString());
                            // LoadBranchList(operationid);
                        }
                        else
                        {
                            //int countryid = int.Parse(country_list.SelectedValue.ToString());
                            //int countryid = int.Parse(sessioncountryid);
                            //LoadAreaList(countryid);
                        }
                        LoadApplicationByStatus();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed View Applications page");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadFilters(int areaid)
        {
            ddloperationarea.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea.DataBind();
            ddlbranch.DataSource = bll.GetBranchList(areaid,0);
            ddlbranch.DataBind();
         
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

                LoadApplicationByStatusFilter();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadApplicationByStatusFilter()
        {
            try
            {
                DateTime start = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime end = DateTime.Now;



                String from = txtfromdatesrc.Text.Trim();
                String to = txttodatesrc.Text.Trim();

                if (!from.Equals(""))
                {
                    start = DateTime.Parse(from);
                }
                if (!to.Equals(""))
                {
                    end = DateTime.Parse(to);
                }

                if (ddloperationarea.SelectedValue.Equals("0") || ddlbranch.SelectedValue.Equals("0"))                    
                {
                    start = DateTime.Parse("2020-01-01");
                }
                string applicationame = "";
                string country = "2";
                string area = ddloperationarea.SelectedValue.ToString();
                string status = "0";
                string search = txtsearch.Text.Trim();
                string branch = ddlbranch.SelectedValue.ToString();
                DataTable dataTable = bll.GetApplicationByStatusFiltered( area,branch, status,search,start,end);
                if (dataTable.Rows.Count > 0)
                {
                    gv_applicationview.DataSource = dataTable;
                    gv_applicationview.DataBind();
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

        private void LoadApplicationByStatus()
        {
            try
            {
                string applicationame = "";
                string country = "2";
                string area = "10";
                string status = "0";
                DataTable dataTable = bll.GetApplicationByStatus(applicationame, country, area, status);
                if (dataTable.Rows.Count > 0)
                {
                    gv_applicationview.DataSource = dataTable;
                    gv_applicationview.DataBind();
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
                LoadApplicationByStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void gv_applicationview_OnRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //Get the Command Name.
        //    string commandName = e.CommandName;

        //    if (commandName == "btnPrint")//routecard
        //    {
        //        string str = "Sorry, Application Foam print out not available yet!!!";
        //        DisplayMessage(str, true);
        //    }

        //}



        //protected void gv_applicationview_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    /*if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //
        //        // dispatchdisplay.Visible = true;
        //        TableCell link = (TableCell)e.Row.Cells[1];
        //        string status = e.Row.Cells[1].Text;
        //        if ((status.Equals("Emergency")))
        //        {

        //            link.ForeColor = Color.Red;
        //            //e.Row.BackColor = Color.Red;
        //            //e.Row.ForeColor = Color.White;
        //        }
        //        else if ((status.Equals("General")))
        //        {

        //            link.ForeColor = Color.Green;
        //            // e.Row.BackColor = Color.Green;
        //            // e.Row.ForeColor = Color.White;
        //        }
        //        else if ((status.Equals("Others")))
        //        {

        //            link.ForeColor = Color.Blue;
        //            // e.Row.BackColor = Color.Blue;
        //            //e.Row.ForeColor = Color.White;
        //        }
        //    }*/
        //}
        protected void gv_applicationview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[5];
                string type = e.Row.Cells[5].Text;
                type = type.ToLower();
                // e.Row.BackColor = Color.Blue;
                //e.Row.ForeColor = Color.Green;
                if ((type.Contains("flat")))
                {

                    // link.ForeColor = Color.Red;
                    e.Row.Cells[5].BackColor = Color.Red;
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }
                else if ((type.Contains("post")))
                {

                    e.Row.Cells[5].BackColor = Color.Blue;
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }
                else if ((type.Contains("pre")))
                {

                    e.Row.Cells[5].BackColor = Color.Green;
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }

            }
        }
        protected void gv_applicationview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_applicationview_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowPrint")
            {
                string str = "";
                //string UserID = e.Item.Cells[0].Text;
                //string appid = Convert.ToString(e.CommandArgument.ToString());
                //string str = "Sorry, Application Foam print out not available yet!!!";
                string[] arg = new string[2];
                arg = e.CommandArgument.ToString().Split(';');
                string appnumber = arg[0];
                string areaid = arg[1];
                PrintFoam(appnumber, areaid);
                //DisplayMessage(str, true);
            }
            else if (e.CommandName == "RowView")
            {
                string appid = Convert.ToString(e.CommandArgument.ToString());
                //string str = "Sorry, Application Foam print out not available yet!!!";
                // DisplayMessage(returned, true);
                LoadApplicationStatusLogs(appid);
            }
            else if (e.CommandName == "RowPanel")
            {
                //string appid = Convert.ToString(e.CommandArgument.ToString());
                //string str = "Sorry, Application Foam print out not available yet!!!";
                // DisplayMessage(returned, true);
                string[] arg = new string[3];
                arg = e.CommandArgument.ToString().Split(';');
                string appnumber = arg[0];
                string name = arg[1];
                string appid = arg[2];
                lblApplicationCode.Text = appnumber;
                lblApplicationId.Text = appid;
                LoadApplicationPanel(appnumber,name,appid);
            }
        }

        private void LoadApplicationPanel(string appnumber,string name,string appid)
        {
            lblname.Text = appnumber+"-->"+name;
            paneldisplay.Visible = true;
            maindisplay.Visible = false;
            statuslogdisplay.Visible = false;
            documentdisplay.Visible = false;
        }

        public void PrintFoam(string appnumber, string areaid)
        {
            try
            {
                PDFPrints pp = new PDFPrints();
                //string referenceno = "26012021/234/210/0/1";//1:application no, 2:paymentref, 3:referenceno
                string flag = "1";
                string res = "";
                //string companyid = "2";
                string user = Session["FullName"].ToString();
                DataTable dt = bll.GetCustomerReportData(appnumber, flag);
                DataTable dtprofile = bll.GetCompanyProfile(areaid);
                if (dt.Rows.Count > 0)
                {
                     res = pp.GetPDFForm(dt, dtprofile, user);
                    //Console.WriteLine(res);
                    DisplayMessage(res, true);
                }
                else
                {
                    res = "Sorry, Application Foam print out not available yet!!!";
                    DisplayMessage(res, true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gv_applicationview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_applicationview.Rows[index].Cells[1].Text;
                

            }
        }
        private void LoadApplicationStatusLogs(string appnumber)
        {
            DataTable dt = new DataTable();
            statuslogdisplay.Visible = true;
            try
            {
                dt = bll.GetApplicationTrackLogs(appnumber);
                gvlogdisplay.DataSource = dt;
                //gvMaterial.CurrentPageIndex = 0;
                gvlogdisplay.DataBind();
                gvlogdisplay.Visible = true;
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadStatusLogDisplay", error);
                DisplayMessage(error, true);
            }
        }

        protected void btnreturn2_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                LoadApplicationByStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnreturn3_Click(object sender, EventArgs e)
        {
            maindisplay.Visible = true;
            statuslogdisplay.Visible = false;
            LoadApplicationByStatus();
        }

        protected void btnAppDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string application_code = lblApplicationCode.Text.Trim();
                //lblReport.Text = "1";
                LoadForm(application_code);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private void LoadForm(string application_code)
        {
            try
            {
                PrintFoam(application_code, "10");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                LoadDocuments();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private void LoadDocuments()
        {
            documentdisplay.Visible = true;
            string appid = lblApplicationId.Text;
            LoadAttachments(appid);
        }

        protected void btnJobCard_Click(object sender, EventArgs e)
        {
            try
            {
                string ApplicationCode = lblApplicationCode.Text.Trim();
                //lblReport.Text = "2";
                LoadJobCard(ApplicationCode);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private void LoadJobCard(string applicationCode)
        {
            DataTable dt = bll.GetSurveyQnList();
       
            ExcelReport(dt);
        }

        private void ExcelReport(DataTable dataTable)
        {
            try
            {
                int[] iColumns = { 0, 1, 2 };
                string appcode = lblApplicationCode.Text;
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

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                statuslogdisplay.Visible = true;
                string appid = lblApplicationId.Text.Trim();
              
                LoadApplicationStatusLogs(appid);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btnSlips_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPaySlips();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        private void LoadPaySlips()
        {
            
        }

        protected void btnExpense_Click(object sender, EventArgs e)
        {

        }

        protected void btnTrench_Click(object sender, EventArgs e)
        {

        }

        protected void btnDocket_Click(object sender, EventArgs e)
        {

        }

        protected void btnreturnpanel_Click(object sender, EventArgs e)
        {
            maindisplay.Visible = true;
            statuslogdisplay.Visible = false;
            paneldisplay.Visible = false;
            documentdisplay.Visible = false;
            LoadApplicationByStatus();
        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("all", "0"));
        }
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("all", "0"));
        }
        protected void gvdocuments_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
        protected void gvdocuments_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowDelete")
            {
                //string UserID = e.Item.Cells[0].Text;
                string ItemId = "0";

                //ItemId = e.Item.Cells[1].Text;
                ItemId = Convert.ToString(e.CommandArgument.ToString());
                //delete record              
                string appno = lblApplicationId.Text;
                string deletedby = Session["UserID"].ToString();
                bll.DeleteApplicationItem(appno, int.Parse(ItemId), deletedby);
                LoadAttachments(appno);
            }


        }

        private void LoadAttachments(string appid)
        {
            DataTable dt = bll.GetFileAttachments(appid);
            gvdocuments.DataSource = dt;
            gvdocuments.DataBind();
        }

        protected void gvdocuments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvdocuments_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}