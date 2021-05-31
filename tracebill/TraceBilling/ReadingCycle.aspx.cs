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
    public partial class ReadingCycle : System.Web.UI.Page
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
        private void LoadFieldComments()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetFieldComments();
                comment_list.DataSource = dt;//Code,comment
                comment_list.DataTextField = "comment";
                comment_list.DataValueField = "Code";
                comment_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCommentList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadMeterReaders(string areaid,string roleid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetSystemUserByRole(areaid,roleid);
                reader_list.DataSource = dt;
                reader_list.DataTextField = "fullName";
                reader_list.DataValueField = "userID";
                reader_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayReaderList", error);
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
        protected void branch_list1_DataBound(object sender, EventArgs e)
        {
            branch_list1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void reader_list_DataBound(object sender, EventArgs e)
        {
            reader_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void cboReader1_DataBound(object sender, EventArgs e)
        {
            cboReader1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void comment_list_DataBound(object sender, EventArgs e)
        {
            comment_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }

        protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                LoadAreaList(countryid);
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
            generateschedule.Visible = false;
            string areaid = Session["areaId"].ToString();
            string user = Session["FullName"].ToString();
            txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
            txtuser.Text = user;
            int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
            LoadAreaList3(countryid);
            LoadBranchList1(0);
            capturereading.Visible = true;
            //confirminvoice.Visible = false;
            //lblapplicant.Visible = false;
            //btnlinks.Visible = false;
            //LoadInvoiceDetails();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        protected void btngenerateschedule_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = true;
            capturereading.Visible = false;
            downloadroute.Visible = false;
            uploadroutes.Visible = false;
            handleexeptions.Visible = false;
        }

        protected void btnreadingcapture_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = true;
            downloadroute.Visible = false;
            uploadroutes.Visible = false;
            handleexeptions.Visible = false;
            LoadDisplay();
        }

        protected void btnroutedownload_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = false;
            downloadroute.Visible = true;
            uploadroutes.Visible = false;
            handleexeptions.Visible = false;
            downloadgrid.Visible = false;
            LoadCountryList2();
            int countryid = Convert.ToInt16(country_list2.SelectedValue.ToString());
            LoadAreaList2(countryid);
            LoadBranchList(0);
        }

        protected void btnreadingupload_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = false;
            downloadroute.Visible = false;
            uploadroutes.Visible = true;
            handleexeptions.Visible = false;
        }

        protected void btnrexceptions_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = false;
            downloadroute.Visible = false;
            uploadroutes.Visible = false;
            handleexeptions.Visible = true;
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessRouteRequest();
            }
            catch(Exception ex)
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
                string country = country_list.SelectedValue.ToString();
                string area = area_list.SelectedValue.ToString();
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
                DataTable dataTable = bll.GetRouteFile(country, area, branch, book,walk);
                if (dataTable.Rows.Count > 0)
                {
                    downloadgrid.Visible = true;
                    ArrayList al = new ArrayList();
                    string countryn = country_list2.SelectedItem.ToString();
                    string arean = area_list2.SelectedItem.ToString();
                
                    String name = countryn + arean + book + walk;
                    string path = @"D:\\Files\\Route Files";
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
            catch(Exception ex)
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
        protected void rdgoptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string countryid = country_list.SelectedValue.ToString();//Session["countryId"].ToString();
                string areaid = area_list3.SelectedValue.ToString();//Session["areaId"].ToString();
                if (rdgoptions.SelectedValue.ToString() == "1")//one by one
                {
                 
                    //LoadAreaList3(int.Parse(countryid));
                    LoadBranchList1(int.Parse(areaid));
                    onebyonedisplay.Visible = true;
                    bulkdisplay.Visible = false;
                }
                else if (rdgoptions.SelectedValue.ToString() == "2")//bulk
                {
                    LoadMeterReaders_bulk(areaid, "11");
                    onebyonedisplay.Visible = false;
                    bulkdisplay.Visible = true;
                }
                else
                {
                    //LoadCustimaFileMeterReaders();
                    //MultiView3.ActiveViewIndex = 5;
                }
                DisplayMessage(".",true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message,true);
            }
        }

        protected void btnInquire_Click(object sender, EventArgs e)
        {
            if (country_list.SelectedValue.ToString() == "0")
            {
                DisplayMessage("Please Select a Country", true);
            }
            else if (area_list3.SelectedValue.ToString() == "0")
            {
                DisplayMessage("Please Select an Area", true);
            }
            else
            {
                string custref = txtInquireCustRef.Text.Trim();
                string propertyref = txtInquirePropRef.Text;
                string period = txtcurrentperiod.Text;
                string areaid = area_list3.SelectedValue.ToString();
                string branchid = branch_list1.SelectedValue.ToString();
                resp = bll.ValidateReadingInquiry(custref, propertyref,areaid);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    DataTable dTable = bll.GetLatestBilledReading(custref, areaid, branchid);
                    if (dTable.Rows.Count > 0)
                    {
                        txtPreReading.Text = dTable.Rows[0]["CurReading"].ToString();
                        DateTime CurReadingDate = Convert.ToDateTime(dTable.Rows[0]["CurReadingDate"].ToString());
                        txtPreReadDate.Text = CurReadingDate.ToString("dd/MM/yyyy");
                        txtConsumption.Text = dTable.Rows[0]["Consumption"].ToString();
                        txtAvgConsumption.Text = dTable.Rows[0]["AvgConsumption"].ToString();
                        txtIsBilled.Text = dTable.Rows[0]["Billed"].ToString();
                        txtType.Text = dTable.Rows[0]["readingType"].ToString();
                        txtCustName.Text = dTable.Rows[0]["customerName"].ToString();
                        txtMeterRef.Text = dTable.Rows[0]["meterRef"].ToString();
                        txtPropRef.Text = dTable.Rows[0]["propertyRef"].ToString();
                        txtdials.Text = dTable.Rows[0]["dials"].ToString();
                        LoadFieldComments();
                        //lblDials.Text = dTable.Rows[0]["Dials"].ToString();
                        // lblDials.Text = data.GetMeterDials(CustRes.MeterRef, Cust.AreaID);//sas
                    }
                    else
                    {
                        txtPreReading.Text = "0";
                        DateTime CurReadingDate = DateTime.Now;
                        txtPreReadDate.Text = CurReadingDate.ToString("MMMM dd, yyyy");
                        txtConsumption.Text = "0";
                        txtAvgConsumption.Text = "0";
                        txtIsBilled.Text = "YES";
                        txtType.Text = "NEW CONN";
                        if (String.IsNullOrEmpty(txtAvgConsumption.Text.Trim()))
                            txtAvgConsumption.Text = "0";
                       // lblDials.Text = "7";
                    }
                    DisplayMessage(".",true);
                    btnSave.Visible = true;
                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ReadingObj read = new ReadingObj();

                string meterref = txtMeterRef.Text.Trim();
                string custref = txtInquireCustRef.Text.Trim();
                string reading = txtReading.Text.Trim();
                string readingdate = txtReadDate.Text.Trim();
                string reader = reader_list.SelectedItem.ToString();
                string otherreader = txtotherReader.Text.Trim();
                string comment = comment_list.SelectedValue.ToString();
                string prereading = txtPreReading.Text.Trim();
                string prereadingdate = txtPreReadDate.Text.Trim();
                string createdby = Session["UserID"].ToString();
                DateTime readingdt = Convert.ToDateTime(readingdate);
                DateTime prereadingdt = bll.GetDate(prereadingdate);
                if (reading == "")
                {
                    DisplayMessage("Please enter current reading", true);
                }
                else if (readingdate == "")
                {
                    DisplayMessage("Please enter current reading date", true);
                }

                else if (!bll.IsValidReadingDate(readingdate))
                {
                    string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                    DisplayMessage("Invalid Reading Date, It cannot be greater than Today ( " + Todate + " )", true);
                }
                else if (!bll.IsValidDateComparison(prereadingdt, readingdt))
                {
                    string Todate = prereadingdt.ToString("dd/MM/yyyy");
                    DisplayMessage("Current Reading Date cannot be less than Previous Date ( " + Todate + " )", true);
                }
                else
                {
                    //save details
                    read = new ReadingObj();
                    read.CustRef = custref;
                    read.MeterRef = meterref;
                    read.Type = "PERIODIC";
                    read.Method = "M";
                    read.LevelID = 0;
                    read.CurReading = int.Parse(reading);
                    read.CurReadingDate = readingdt;
                    read.Estimated = chkEstimate.Checked;
                    read.PreReading = int.Parse(prereading);
                    read.PreReadingDate = prereadingdt;
                    read.Consumption = read.CurReading - read.PreReading;
                    read.Reader = reader;
                    read.Comment = comment;
                    read.Billed = false;
                    read.Period = txtcurrentperiod.Text;
                    read.Area = area_list3.SelectedValue.ToString();
                    read.Branch = branch_list1.SelectedValue.ToString();
                    read.CreatedBy = int.Parse(createdby);
                    read.Latitude = "0";
                    read.Longitude = "0";

                    resp = bll.SaveReading(read);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with reading details against Customer(" + read.CustRef + ") sucessfully saved.";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {

                        string str = " with reading details against Customer(" + read.CustRef + ") updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }

                    //clear conrols
                    ClearReadingControls();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearReadingControls()
        {
            txtPreReading.Text = "";
            txtPreReadDate.Text = "";
            txtConsumption.Text = "";
            txtAvgConsumption.Text = "";
            txtIsBilled.Text = "";
            txtType.Text ="";
            txtCustName.Text = "";
            txtMeterRef.Text = "";
            txtPropRef.Text = "";
            txtInquireCustRef.Text = "";
           txtReading.Text = "";
             txtReadDate.Text= "";
            reader_list.SelectedValue="0";
           txtotherReader.Text = "";
           comment_list.SelectedValue="0";
           
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

        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateFile();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message,true);
            }
        }
        private void LoadMeterReaders_bulk(string areaid, string roleid)
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
            bool HasHeader = chkHeader.Checked;

            if (FileUpload1.FileName.Trim().Equals(""))
            {
                DisplayMessage("Please select a file to upload.",true);
            }
            else if (Area.Equals("0"))
            {
                DisplayMessage("Please select an Area",true);
            }
            else if (Reader == "0")
            {
                DisplayMessage("Please Select Reader for the file to Upload",true);
            }
            else if (ReadingDate == "")
            {
                DisplayMessage("Please Enter Reading Date for the file to upload",true);
            }
            else if (!bll.IsValidReadingDate(ReadingDate))
            {
                string Todate = DateTime.Now.ToString("dd/MM/yyyy");
                DisplayMessage("Invalid Reading Date, It cannot be greater than Today(" + Todate + ")",true);
            }
         
            else
            {
                string ReaderI = cboReader1.SelectedValue.ToString();
                string ReadingDateI = txtReadingDate1.Text.Trim();
                string AreaI = area_list3.SelectedValue.ToString();
                string BranchI = branch_list1.SelectedValue.ToString();
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
                string fileType = "MN";
                string Filepath = ReturnPath(Reader, fileType);
                string FileNames = Path.GetFileName(Filepath);

                int ProcNumber = GetMaxProcess();
                ArrayList aList = df.readFile(Filepath);
                int NoOfRecords = (aList.Count) - 1;
                string fileExtention = Path.GetExtension(Filepath);
                if (bll.IsExtensionAllowed(fileExtention))
                {
                    if (bll.CheckFileFormat(Filepath) == false)
                    {
                        DisplayMessage("File Format is not OK, Columns must be 9..",true);
                    }
                    else
                    {
                        if (NoOfRecords > ProcNumber)
                        {
                            string FileType = "MN";
                           // data.SaveFileDetails(ReaderII, ReadingDateII, AreaII, BranchII, Filepath, CurPeriod, CapturingII, Processing, Processed, Failed, Success, Force, HasHeader, FileType);
                            string Msg = "Manual Reading File Has been Uploaded.Your File of (" + " " + NoOfRecords + " " + " )Records will be processed internally";
                            DisplayMessage(Msg,true);
                        }
                        else
                        {
                            //CallFileConfirmation(Filepath);
                            string Msg = "Manual Readings File Contains " + NoOfRecords + " Records. File Not Uploaded.";
                            bll.RemoveFile(Filepath);
                            DisplayMessage(Msg,true);
                        }
                    }
                }

                else
                {
                    string Msg = "File format(" + fileExtention + ") is not Supported - Upload Csv format";
                    bll.RemoveFile(Filepath);
                    DisplayMessage(Msg,true);
                }

            }
       
        }

        private string ReturnPath(string Reader, string FileType)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);
            string filepath = bll.GetReadingFilePath(filename, Reader, FileType);
            FileUpload1.SaveAs(filepath);
            return filepath;
        }
        private int GetMaxProcess()
        {
            int output = 1;
            string ParameterCode = "3";
            string MaxNo = "10";//dal.GetSystemParameter(ParameterCode);
            int Num;
            bool res = Int32.TryParse(MaxNo, out Num);
            if (res)
            {
                output = Convert.ToInt16(MaxNo);
            }
            return output;
        }

        private string HeaderChecker(bool HasHeader)
        {

            string Return = "";
            StreamReader srd = new StreamReader(FileUpload1.PostedFile.FileName.Trim());
            string[] FirstRecord = srd.ReadToEnd().Replace("\r", "").Split('\n');
            if (HasHeader)
            {
                int i = 1;
                int count = 0;
                count += 1;
                string line = FirstRecord[i].ToString();
                line = line.Trim();
                if (!line.Equals(""))
                {
                    string[] StrArray = line.Split(',');

                    if (StrArray.Length == 9)
                    {
                        int number;
                        string PropRef = StrArray[2].ToString().Trim();
                        string Comment = StrArray[7].ToString().ToUpper().Trim();
                        string Reading = StrArray[8].ToString().Trim();

                        if (Reading == "" && Comment == "")
                        {
                            throw new Exception("File not OK, Line (" + count + "). All Null Readings must have a comment");

                        }
                        else if (!bll.IsValidPropRef(PropRef))
                        {
                            throw new Exception("File not OK, Line (" + count + "). Invalid Property Ref");
                        }

                        bool result = Int32.TryParse(Reading, out number);
                        if (!result && !Reading.Equals(""))
                        {
                            throw new Exception("File not OK, Line (" + count + "). All Readings must be Numeric");
                        }
                    }
                    else
                    {
                        throw new Exception("File Format is not OK, Columns must be 9..");

                    }
                }

            }
            else
            {
                int i = 0;
                string line = FirstRecord[i].ToString();
                line = line.Trim();
                if (!line.Equals(""))
                {
                    string[] StrArray = line.Split(',');
                    if (StrArray.Length == 9)
                    {
                        int number;
                        string PropRef = StrArray[2].ToString().Trim();
                        string Comment = StrArray[7].ToString().ToUpper().Trim();
                        string Reading = StrArray[8].ToString().Trim();

                        if (Reading == "" && Comment == "")
                        {
                            throw new Exception("File not OK, Line (" + i + "). All Null Readings must have a comment");
                        }
                        else if (bll.IsValidPropRef(PropRef))
                        {
                            throw new Exception("File not OK, Line (" + i + "). Invalid Property Ref");
                        }
                        bool result = Int32.TryParse(Reading, out number);
                        if (!result == !Reading.Equals(""))
                        {
                            throw new Exception("File not OK, Line (" + i + "). All Readings must be Numeric");
                        }

                    }
                    else
                    {
                        throw new Exception("File Format is not OK, Columns must be 9..");
                    }
                }
            }
            return Return;

        }
    }
}