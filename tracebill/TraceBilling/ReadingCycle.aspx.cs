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
                    else
                    {
                        string sessioncountryid = Session["countryId"].ToString();

                        //if (!sessioncountryid.Equals("1"))
                        //{

                        //    LoadAreaList3(int.Parse(sessioncountryid));
                        //    area_list3.SelectedIndex = area_list3.Items.IndexOf(new ListItem(Session["area"].ToString(), Session["areaId"].ToString()));
                        //    area_list3.Enabled = false;
                        //    int operationid = Convert.ToInt16(area_list3.SelectedValue.ToString());
                        //    // LoadBranchList(operationid);
                        //}
                        //else
                        //{
                        //    //int countryid = int.Parse(country_list.SelectedValue.ToString());
                        //    int countryid = int.Parse(sessioncountryid);
                        //    LoadAreaList3(countryid);
                        //}
                        LoadBranchList1(0, 0);
                        LoadFilters(10);
                        LoadAreaControls();
                        LoadDisplay();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Reading Cycle page");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadAreaControls()
        {
            ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea.Enabled = true;
            ddloperationarea1.SelectedIndex = ddloperationarea1.Items.IndexOf(new ListItem(Session["operationAreaName"].ToString(), Session["operationId"].ToString()));
            ddloperationarea1.Enabled = true;

        }

        private void LoadFilters(int areaid)
        {
            ddloperationarea.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea.DataBind();
            ddloperationarea1.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea1.DataBind();

            LoadMeterReaders(areaid.ToString(), "11");
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
        //private void LoadAreaList3(int countryid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetAreaList(countryid);
        //        area_list3.DataSource = dt;

        //        area_list3.DataTextField = "areaName";
        //        area_list3.DataValueField = "areaId";
        //        area_list3.DataBind();
        //       // area_list3.SelectedValue = "10";
        //        //area_list3.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayAreaList", error);
        //        DisplayMessage(error, true);
        //    }
        //}
        private void LoadBranchList1(int areaid, int operationid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid, operationid);
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
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        //protected void area_list_DataBound(object sender, EventArgs e)
        //{
        //    area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        //}
       
     
       
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

        //protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
        //        LoadAreaList(countryid);
        //        LoadAreaList3(countryid);
        //        LoadAreaListSheet(countryid);
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

       

        private void LoadDisplay()
        {
            generateschedule.Visible = false;
            string areaid = Session["areaId"].ToString();
            string user = Session["FullName"].ToString();
            txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
            txtuser.Text = user;
            //int countryid = 2;
            //LoadAreaList3(countryid);
            LoadBranchList1(int.Parse(areaid),0);
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
           
            handleexeptions.Visible = false;
            int countryid = 2;
            int area = 10;
           
            LoadBranchListSheet(area);
            string areaid = "10";
            string branchid = branch_listsheet.SelectedValue.ToString();
            LoadBlockMaps(areaid, branchid);
            LoadReadingSchedule();
        }

        protected void btnreadingcapture_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = true;
           
            handleexeptions.Visible = false;
            LoadDisplay();
        }

        protected void btnroutedownload_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = false;
            
            handleexeptions.Visible = false;
           
        }

        protected void btnreadingupload_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = false;
            
            handleexeptions.Visible = false;
        }

        protected void btnrexceptions_Click(object sender, EventArgs e)
        {
            generateschedule.Visible = false;
            capturereading.Visible = false;
           
            handleexeptions.Visible = true;
        }


    
        protected void rdgoptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string countryid = country_list.SelectedValue.ToString();//Session["countryId"].ToString();
                string areaid = "10";//Session["areaId"].ToString();
                if (rdgoptions.SelectedValue.ToString() == "1")//one by one
                {
                 
                    //LoadAreaList3(int.Parse(countryid));
                    LoadBranchList1(int.Parse(areaid),0);
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
            //if (country_list.SelectedValue.ToString() == "0")
            //{
            //    DisplayMessage("Please Select a Country", true);
            //}
            string custref = txtInquireCustRef.Text.Trim();
            string propertyref = txtInquirePropRef.Text;
            string searchstr = "";
            if(!custref.Equals(""))
            {
                searchstr = custref;
            }
            else
            {
                searchstr = propertyref;
            }
            string str = "";
            if (bll.IsFlatRated(searchstr))//check flatrate
            {
                str = "Customer-" + searchstr + " is Flat Rate and does not require consumption.";
                DisplayMessage(str, true);
            }
            else
            {
                
                string period = txtcurrentperiod.Text;
                string areaid = "10";
                string branchid = branch_list1.SelectedValue.ToString();
                resp = bll.ValidateReadingInquiry(custref, propertyref,areaid);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    DataTable dTable = bll.GetLatestBilledReading(searchstr, areaid, branchid);
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
                //DateTime readingdt = Convert.ToDateTime(readingdate);
                DateTime readingdt = bll.GetDate(readingdate);//european style dd/mm/yyyy
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
                    DisplayMessage("Invalid Reading Date "+ readingdate + ", It cannot be greater than Today ( " + Todate + " )", true);
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
                    read.Area = "10";
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
            
                string areaid = "10";
                txtcurrentperiod.Text = bll.GetBillingPeriod(areaid);
                LoadMeterReaders(areaid, "11");
                LoadBranchList1(int.Parse(areaid),0);
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
            string Area = "10";
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
                string AreaI = "10";
                string BranchI = branch_list1.SelectedValue.ToString();
                string area = "Ogun";
                string branch = branch_list1.SelectedItem.ToString();
                if(branch.Contains("None"))
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
                string fileType = "MN";
                string Filepath = ReturnPath(Reader, fileType,area,branch);
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
                           bll.SaveFileDetails(ReaderII, ReadingDateII, AreaII, BranchII, Filepath, CurPeriod, CapturingII, Processing, Processed, Failed, Success,  HasHeader, FileType);
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

        private string ReturnPath(string Reader, string FileType, string area, string branch)
        {
            string filename = Path.GetFileName(FileUpload1.FileName);
            string filepath = bll.GetReadingFilePath(filename, Reader, FileType, area,branch);
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

      
        protected void cboBlock_DataBound(object sender, EventArgs e)
        {
            cboBlock.Items.Insert(0, new ListItem("- - Select - -", "0"));
        }
        //protected void area_listsheet_DataBound(object sender, EventArgs e)
        //{
        //    area_listsheet.Items.Insert(0, new ListItem("- - Select Area - -", "0"));
        //}
        protected void branch_listsheet_DataBound(object sender, EventArgs e)
        {
            branch_listsheet.Items.Insert(0, new ListItem("- - All -", "0"));
        }
        private void LoadBlockMaps(string areaid, string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBlockMaps(areaid, branchid);
                cboBlock.DataSource = dt;

                cboBlock.DataTextField = "blockNumber";
                cboBlock.DataValueField = "blockID";
                cboBlock.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadBlockMaps", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadBranchListSheet(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid,0);
                branch_listsheet.DataSource = dt;
                branch_listsheet.DataTextField = "branchName";
                branch_listsheet.DataValueField = "branchId";
                branch_listsheet.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayBranchList", error);
                DisplayMessage(error, true);
            }
        }
        //private void LoadAreaListSheet(int countryid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetAreaList(countryid);
        //        area_listsheet.DataSource = dt;

        //        area_listsheet.DataTextField = "areaName";
        //        area_listsheet.DataValueField = "areaId";
        //        area_listsheet.DataBind();
        //        //area_listsheet.SelectedValue = "10";
        //        //area_listsheet.Enabled = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayAreaList", error);
        //        DisplayMessage(error, true);
        //    }
        //}
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                LoadReadingSchedule();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void LoadReadingSchedule()
        {
            string areaid = "10";
            string branchid = branch_listsheet.SelectedValue.ToString();
            //LoadBlockMaps(areaid, branchid);
            string block = cboBlock.SelectedValue.ToString();
            string blockname = cboBlock.SelectedItem.ToString();
            if (block.Equals("0"))
            {
                DisplayMessage("Please select block", true);
            }
            else
            {
                DataTable dt = bll.GetReadingSheet(areaid, branchid, blockname);
                if (dt.Rows.Count > 0)
                {
                    Session["RdgSheetDT"] = dt;
                    gv_rdgsheet.DataSource = dt;
                    gv_rdgsheet.DataBind();
                    DisplayMessage(".", true);
                    sheetdisplay.Visible = true;
                }
                else
                {
                    string error = "100: " + "No records found";
                    DisplayMessage(error, true);
                    sheetdisplay.Visible = false;
                }
            }
           
        }

        protected void gv_rdgsheet_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }
        protected void gv_rdgsheet_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        //protected void area_listsheet_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        string areaid = area_listsheet.SelectedValue.ToString();
        //        LoadBlockMaps(areaid, "0");

             
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        protected void btnexportsheet_Click(object sender, EventArgs e)
        {
            DataTable dataTable = (DataTable)Session["RdgSheetDT"];
            if(dataTable.Rows.Count > 0)
            {
                ExportCSV(dataTable);
            }
        }

        private void ExportCSV(DataTable dataTable)
        {
            string filePath = bll.CallSheetFilling(dataTable);
            DownloadFile(filePath, true);
        }
        private void DownloadFile(string path, bool forceDownload)
        {
            string name = Path.GetFileName(path);
            string type = "Application/vnd.ms-excel";
            if (forceDownload)
            {
                Response.AppendHeader("content-disposition",
                    "attachment; filename=" + name);
            }
            if (type != "")
                Response.ContentType = type;
            Response.WriteFile(path);
            Response.End();
            bll.RemoveFile(path);
        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("--select--", "0"));
        }
        protected void ddloperationarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                int branchid = Convert.ToInt16(branch_listsheet.SelectedValue.ToString());
                LoadBranchList1(10, operationid);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddloperationarea1_DataBound(object sender, EventArgs e)
        {
            ddloperationarea1.Items.Insert(0, new ListItem("--select--", "0"));
        }
        protected void ddloperationarea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea1.SelectedValue.ToString());
                int branchid = Convert.ToInt16(branch_list1.SelectedValue.ToString());
                LoadBranchList1(10, operationid);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}