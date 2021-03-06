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
using System.Text;

namespace TraceBilling
{
    public partial class FieldBilling : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        DataFile df = new DataFile();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (Session["roleId"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    LoadFilters(10);
                    LoadAreaControls();
                    LoadDisplay();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Field Billing");

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
            ddloperationarea2.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea2.DataBind();
            ddloperationarea3.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea3.DataBind();
            txtcurrentperiod.Text = bll.GetBillingPeriod(areaid.ToString());
            LoadMeterReaders(areaid.ToString(), "11");

        }
        private void LoadAreaControls()
        {
            ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea.Enabled = true;
            ddloperationarea2.SelectedIndex = ddloperationarea2.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea2.Enabled = true;
            ddloperationarea3.SelectedIndex = ddloperationarea3.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea3.Enabled = true;

        }

        //private void LoadCountryList()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string countryid = Session["countryId"].ToString();
        //        dt = bll.GetCountryList();
        //        country_list.DataSource = dt;
        //        country_list.SelectedValue = countryid;
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
        //private void LoadCountryList2()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetCountryList();
        //        country_list2.DataSource = dt;

        //        country_list2.DataTextField = "countryName";
        //        country_list2.DataValueField = "countryId";
        //        country_list2.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayCountryList", error);
        //        DisplayMessage(error, true);
        //    }
        //}


        private void LoadBranchList(int areaid, int opid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid,opid);
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
        

        //private void LoadBranchList1(int areaid, int opid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetBranchList(areaid,opid);
        //        branch_list1.DataSource = dt;
        //        branch_list1.DataTextField = "branchName";
        //        branch_list1.DataValueField = "branchId";
        //        branch_list1.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayBranchList", error);
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
        //protected void country_list2_DataBound(object sender, EventArgs e)
        //{
        //    country_list2.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        
        protected void branch_list_DataBound(object sender, EventArgs e)
        {
            branch_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        

        protected void cboReader1_DataBound(object sender, EventArgs e)
        {
            cboReader1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        //protected void branch_list1_DataBound(object sender, EventArgs e)
        //{
        //    branch_list1.Items.Insert(0, new ListItem("- - None - -", "0"));
        //}

        //protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
        //        LoadAreaList(countryid);
        //        LoadAreaList2(countryid);
        //        LoadAreaList3(countryid);
        //        LoadAreacredentials(countryid);
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        //protected void country_list2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
        //        LoadAreaList2(countryid);
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        private void LoadDisplay()
        {
         
            downloadroute.Visible = true;
            uploadroutes.Visible = false;
            downloadgrid.Visible = false;
           // reconcileschedule.Visible = false;
            //LoadCountryList2();
            //int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
            //LoadAreaList2(countryid);
            LoadBranchList(10,0);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        

        protected void btnroutedownload_Click(object sender, EventArgs e)
        {
            downloadroute.Visible = true;
            uploadroutes.Visible = false;
            downloadgrid.Visible = false;
            //reconcileschedule.Visible = false;
            settingsdisplay.Visible = false;
            int areaid = 10;
            //LoadCountryList2();
            //int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
            //LoadAreaList2(countryid);
            LoadBranchList(areaid,0);
        }

        protected void btnreadingupload_Click(object sender, EventArgs e)
        {
           
            downloadroute.Visible = false;
            uploadroutes.Visible = true;
           // reconcileschedule.Visible = false;
            settingsdisplay.Visible = false;
            //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            //LoadAreaList3(countryid);
            int areaid = 10;
            //LoadBranchList1(areaid,0);
            ddlbranch.DataSource = bll.GetBranchList(10, 0);
            ddlbranch.DataBind();
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessRouteRequest();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ProcessRouteRequest()
        {
            try
            {
                ArrayList filepaths_download = new ArrayList();
                ArrayList filepath_names = new ArrayList();
                string country = "2";
                string area = "10";
                string branch = branch_list.SelectedValue.ToString();
                string book = txtbook.Text.Trim();
                string walk = txtwalk.Text.Trim();
                if (book.Equals("0"))
                {
                    book = "0";
                }
                if (walk.Equals(""))
                {
                    walk = "0";
                }
                DataTable dataTable = bll.GetRouteFile(country, area, branch, book, walk);
                if (dataTable.Rows.Count > 0)
                {
                    downloadgrid.Visible = true;
                    ArrayList al = new ArrayList();
                    ArrayList b = new ArrayList();
                    string countryn = "2";
                    string arean = "10";

                    String name = countryn + arean + book + walk;
                    string path = "";
                    // string path = @"C:\\Files\\Route Files";
                    string paramCode = "4";
                    string fpath = bll.GetSystemParameter(paramCode);
                  // string newpath = "@" + fpath;
                    path = fpath;
                    //get tariff file
                    string tariff_name = path + "\\tariff.txt";
                   
                    string tariff_file = GetTariffFile(country);
                    

                    //write to routes
                    String json = JsonConvert.SerializeObject(dataTable);
                    al.Add(json);
                    string file_path = path + "\\" + name + ".txt";
                    //check path
                    bll.CheckPath(path);
                    filepath_names.Add(name);
                    filepaths_download.Add(file_path);
                    //write to path
                    df.writeToFile(file_path, al);
                    //add tariff to grid------------
                    if(!tariff_file.Contains("record"))//no records found
                    {
                        b.Add(tariff_file);
                        tariff_name = path + "\\tariff.txt";
                        df.writeToTarrifFile(tariff_name, b);
                        //SHOW ON GRID WITH DOWNLOAD BUTTONS
                        filepaths_download.Add(tariff_name);
                        filepath_names.Add("tariff");
                    }
                   
                    //end tariff
                    //download file
                    DataTable downloadsdt = new DataTable();
                    downloadsdt.Columns.Add("Route");
                    foreach (String downloads in filepath_names)
                    {
                        downloadsdt.Rows.Add(downloads);
                    }
                    DataGriddownloads.DataSource = downloadsdt;
                    DataGriddownloads.DataBind();
                    DataGriddownloads.Visible = true;
                    int records = filepath_names.Count;
                    DisplayMessage(records + " File(s) successfully downloaded", false);
                    // maindisplay.Visible = true;
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("ProcessRouteRequest", error);
                    DisplayMessage(error, true);
                    DataGriddownloads.Visible = false;

                }

                bll.RecordAudittrail(Session["userName"].ToString(), "Processed Route Request");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void download_command(object source, DataGridCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "download")
                {
                    string filename = e.Item.Cells[0].Text;
                    PromptDownload(filename);
                }

            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }

        }

        public void PromptDownload(string filename)
        {
            try
            {
                string path = "";

                // path = @"C:\\Files\\Route Files";
                string paramCode = "4";
                string fpath = bll.GetSystemParameter(paramCode);
                //string newpath = "@" + fpath;
                path = fpath;
                string result = path + "\\" + filename + ".txt";
                FileStream fs = null;
                fs = File.Open(result, FileMode.Open);
                byte[] btFile = new byte[fs.Length];
                fs.Read(btFile, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                Response.AddHeader("Content-disposition", "attachment; filename=" + filename + ".txt");
                Response.ContentType = "application/octet-stream";
                Response.BinaryWrite(btFile);
                //  Response.End();
                // Response.Redirect(path, false);
                //Context.ApplicationInstance.CompleteRequest();
                // Response.BuffferOutput = True;
                Response.Flush();
                Response.Close();
            }
            catch (ThreadAbortException Ex)
            {
                string ErrMsg = Ex.Message;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }



        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFile();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }
        private void LoadMeterReaders(string areaid, string roleid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetSystemUserByRole(areaid, roleid);
                cboReader1.DataSource = dt;
                cboReader1.DataTextField = "fullName";
                cboReader1.DataValueField = "userID";
                cboReader1.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayReaderList", error);
                DisplayMessage(error, true);
            }
        }
        private void ValidateFile()
        {
            string Reader = cboReader1.SelectedValue.ToString();
            string ReadingDate = txtReadingDate1.Text.Trim();
            string Area = "10";
            string BllPeriod = bll.GetBillingPeriod(Area);
            bool HasHeader = false;

            if (FileUpload1.FileName.Trim().Equals(""))
            {
                DisplayMessage("Please select a file to upload.", true);
            }
            else if (Area.Equals("0"))
            {
                DisplayMessage("Please select an Area", true);
            }
            else if (Reader == "0")
            {
                DisplayMessage("Please Select Reader for the file to Upload", true);
            }
            else if (ReadingDate == "")
            {
                DisplayMessage("Please Enter Reading Date for the file to upload", true);
            }
            else if (!bll.IsValidReadingDate(ReadingDate))
            {
                string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                DisplayMessage("Invalid Reading Date, It cannot be greater than Today(" + Todate + ")", true);
            }

            else
            {
                string ReaderI = cboReader1.SelectedValue.ToString();
                string ReadingDateI = txtReadingDate1.Text.Trim();
                string AreaI = "10";
                string BranchI = ddlbranch.SelectedValue.ToString();
                string area = "Ogun";
                string branch = ddlbranch.SelectedItem.ToString();
                if (branch.Contains("None"))
                {
                    branch = "";
                }
                string CapturingI = Session["UserID"].ToString();
                int ReaderII = int.Parse(ReaderI);
                DateTime ReadingDateII = DateTime.Parse(ReadingDateI);
                int AreaII = int.Parse(AreaI);
                int BranchII = int.Parse(BranchI);
                int CapturingII = int.Parse(CapturingI);
                string CurPeriod = txtcurrentperiod.Text.Trim();
                // bool Force = chkWarnings.Checked;
                bool Processing = false;
                bool Processed = false;
                int Failed = 0;
                int Success = 0;
                string fileType = "OS";
                string Filepath = ReturnPath(Reader, fileType, area, branch);
                string FileNames = Path.GetFileName(Filepath);

                int ProcNumber = GetMaxProcess();
                ArrayList aList = df.readFile(Filepath);
                int NoOfRecords = (aList.Count) - 1;
                string fileExtention = Path.GetExtension(Filepath);
                if (bll.IsExtensionAllowed(fileExtention))
                {
                    if (bll.CheckFileFormat(Filepath) == false)
                    {
                        DisplayMessage("File Format is not OK, Columns must be 28..", true);
                    }
                    else
                    {
                        if (NoOfRecords > ProcNumber)
                        {
                            string FileType = "OS";
                            bll.SaveFileDetails(ReaderII, ReadingDateII, AreaII, BranchII, Filepath, CurPeriod, CapturingII, Processing, Processed, Failed, Success, HasHeader, FileType);
                            string Msg = "Mobile Onspot Reading File Has been Uploaded.Your File of (" + " " + NoOfRecords + " " + " )Records will be processed internally";
                            DisplayMessage(Msg, true);
                        }
                        else
                        {
                            //CallFileConfirmation(Filepath);
                            string Msg = "Mobile Onspot Readings File Contains " + NoOfRecords + " Records. File Not Uploaded.";
                            bll.RemoveFile(Filepath);
                            DisplayMessage(Msg, true);
                        }
                    }
                }

                else
                {
                    string Msg = "File format(" + fileExtention + ") is not Supported - Upload Csv format";
                    bll.RemoveFile(Filepath);
                    DisplayMessage(Msg, true);
                }

            }

        }

        private string ReturnPath(string Reader, string FileType, string area, string branch)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);
            string filepath = bll.GetReadingFilePath(filename, Reader, FileType, area, branch);
            FileUpload1.SaveAs(filepath);
            return filepath;
        }
        private int GetMaxProcess()
        {
            int output = 1;
            string ParameterCode = "3";
            string MaxNo = "1";//dal.GetSystemParameter(ParameterCode);
            int Num;
            bool res = Int32.TryParse(MaxNo, out Num);
            if (res)
            {
                output = Convert.ToInt16(MaxNo);
            }
            return output;
        }


        //protected void btnreconciliation_Click(object sender, EventArgs e)
        //{
        //    downloadroute.Visible = false;
        //    uploadroutes.Visible = false;
        //    //reconcileschedule.Visible = true;
        //    settingsdisplay.Visible = false;
        //}
        //protected void area_list3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        string areaid = area_list3.SelectedValue.ToString();
        //        txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
        //        LoadMeterReaders(areaid, "11");
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        protected void btnonspotsettings_Click(object sender, EventArgs e)
        {
            downloadroute.Visible = false;
            uploadroutes.Visible = false;
            //reconcileschedule.Visible = false;
            settingsdisplay.Visible = true;
            int countryid = 2;
            int areaid = 10;
            //LoadAreacredentials(countryid);            
            LoadBranchCredentials(areaid,0);
        }
        
       
        protected void cboBranchesCredentials_DataBound(object sender, EventArgs e)
        {
            cboBranchesCredentials.Items.Insert(0, new ListItem("- - select - -", "0"));
        }
        private void LoadBranchCredentials(int areaid, int opid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid,opid);
                cboBranchesCredentials.DataSource = dt;
                cboBranchesCredentials.DataTextField = "branchName";
                cboBranchesCredentials.DataValueField = "branchId";
                cboBranchesCredentials.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayBranchList", error);
                DisplayMessage(error, true);
            }
        }
       

        protected void btnCredentialsSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string Area = "10";
                string Branch = cboBranchesCredentials.SelectedValue.ToString();
                exportCredentials(Area, Branch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void exportCredentials(String area, String branch)
        {
            try
            {
                DataTable dt = bll.getCredentialsFile(area, branch);
                if (dt.Rows.Count > 0)
                {
                    string attachment1 = "attachment; ";
                    string fileName = "filename=" + "credentials.csv";
                    string attachment = attachment1 + fileName;
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.ms-excel";
                    string tab = "";
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow row in dt.Rows)
                    {
                        String userid = row["UserID"].ToString();
                        String username = row["UserName"].ToString();
                        String fullname = row["fullName"].ToString();
                        String password = row["password"].ToString();
                        string space = ",";
                        sb.AppendLine(userid + space + username + space + fullname + space + password);
                    }
                    Response.Write(sb.ToString());
                    //Response.Write("\n");
                    int i;
                    Response.End();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        private string GetTariffFile(string country)
        {
            string output = "";
            DataTable dt = bll.GetTariffFileData(country);
            if (dt.Rows.Count > 0)
            {
                output = df.GetTariffFile(dt);
            }
            else
            {
                output = "No Records found";
            }
            return output;

        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("--select--", "0"));
        }
        protected void ddloperationarea2_DataBound(object sender, EventArgs e)
        {
            ddloperationarea2.Items.Insert(0, new ListItem("--select--", "0"));
        }
        protected void ddloperationarea3_DataBound(object sender, EventArgs e)
        {
            ddloperationarea3.Items.Insert(0, new ListItem("--select--", "0"));
        }
        protected void ddloperationarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int areaid = 10;
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());

                LoadBranchList(areaid, operationid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddloperationarea2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int areaid = 10;
                int operationid = Convert.ToInt16(ddloperationarea2.SelectedValue.ToString());

                // LoadBranchList1(areaid, operationid);
                ddlbranch.DataSource = bll.GetBranchList(10, operationid);
                ddlbranch.DataBind();
                downloadroute.Visible = false;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddloperationarea3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int areaid = 10;
                int operationid = Convert.ToInt16(ddloperationarea3.SelectedValue.ToString());

                LoadBranchCredentials(areaid, operationid);
                downloadroute.Visible = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }

    }
}