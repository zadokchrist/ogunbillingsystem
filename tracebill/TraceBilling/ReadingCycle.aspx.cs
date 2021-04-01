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
                string countryid = Session["countryId"].ToString();
                string areaid = Session["areaId"].ToString();
                if (rdgoptions.SelectedValue.ToString() == "1")
                {
                 
                    LoadAreaList3(int.Parse(countryid));
                    LoadBranchList1(int.Parse(areaid));
                    onebyonedisplay.Visible = true;
                    bulkdisplay.Visible = false;
                }
                else if (rdgoptions.SelectedValue.ToString() == "2")
                {
                  
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

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}