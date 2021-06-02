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
        private void LoadCountryList2()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCountryList();
                country_list2.DataSource = dt;

                country_list2.DataTextField = "countryName";
                country_list2.DataValueField = "countryId";
                country_list2.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCountryList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadAreaList2(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetAreaList(countryid);
                area_list2.DataSource = dt;

                area_list2.DataTextField = "areaName";
                area_list2.DataValueField = "areaId";
                area_list2.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayAreaList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadBranchList(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid);
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
        private void LoadAreaList3(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetAreaList(countryid);
                area_list3.DataSource = dt;

                area_list3.DataTextField = "areaName";
                area_list3.DataValueField = "areaId";
                area_list3.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayAreaList", error);
                DisplayMessage(error, true);
            }
        }

        private void LoadBranchList1(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid);
                branch_list1.DataSource = dt;
                branch_list1.DataTextField = "branchName";
                branch_list1.DataValueField = "branchId";
                branch_list1.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayBranchList", error);
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
        protected void country_list2_DataBound(object sender, EventArgs e)
        {
            country_list2.Items.Insert(0, new ListItem("- - select country - -", "0"));
        }
        protected void area_list2_DataBound(object sender, EventArgs e)
        {
            area_list2.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
        protected void branch_list_DataBound(object sender, EventArgs e)
        {
            branch_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void area_list3_DataBound(object sender, EventArgs e)
        {
            area_list3.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }

        protected void cboReader1_DataBound(object sender, EventArgs e)
        {
            cboReader1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void branch_list1_DataBound(object sender, EventArgs e)
        {
            branch_list1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }

        protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                LoadAreaList(countryid);
                LoadAreaList2(countryid);
                LoadAreaList3(countryid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void country_list2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
                LoadAreaList2(countryid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private void LoadDisplay()
        {
         
            downloadroute.Visible = true;
            uploadroutes.Visible = false;
            downloadgrid.Visible = false;
            reconcileschedule.Visible = false;
            LoadCountryList2();
            int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
            LoadAreaList2(countryid);
            LoadBranchList(0);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        

        protected void btnroutedownload_Click(object sender, EventArgs e)
        {
            downloadroute.Visible = true;
            uploadroutes.Visible = false;
            downloadgrid.Visible = false;
            reconcileschedule.Visible = false;
            LoadCountryList2();
            int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
            LoadAreaList2(countryid);
            LoadBranchList(0);
        }

        protected void btnreadingupload_Click(object sender, EventArgs e)
        {
           
            downloadroute.Visible = false;
            uploadroutes.Visible = true;
            reconcileschedule.Visible = false;
            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            LoadAreaList3(countryid);
            LoadBranchList1(0);
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
                string country = country_list2.SelectedValue.ToString();
                string area = area_list2.SelectedValue.ToString();
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
                    string countryn = country_list2.SelectedItem.ToString();
                    string arean = area_list2.SelectedItem.ToString();

                    String name = countryn + arean + book + walk;
                    string path = @"C:\\Files\\Route Files";
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
                path = @"D:\\Files\\Route Files";
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
            string Area = area_list3.SelectedValue.ToString();
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
                string AreaI = area_list3.SelectedValue.ToString();
                string BranchI = branch_list1.SelectedValue.ToString();
                string area = area_list3.SelectedItem.ToString();
                string branch = branch_list1.SelectedItem.ToString();
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


        protected void btnreconciliation_Click(object sender, EventArgs e)
        {
            downloadroute.Visible = false;
            uploadroutes.Visible = false;
            reconcileschedule.Visible = true;
        }
        protected void area_list3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                string areaid = area_list3.SelectedValue.ToString();
                txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
                LoadMeterReaders(areaid, "11");
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}