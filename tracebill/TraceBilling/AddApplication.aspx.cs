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
using System.IO;

namespace TraceBilling
{
    public partial class ApplicationForm : System.Web.UI.Page
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
                    LoadRequirementList();
                    LoadCustomerTypeList();
                   // LoadCountryList();

                    LoadBranchList(0,0);
                    LoadIDList();
                    LoadFilters(10);
                    string sessioncountryid = Session["countryId"].ToString();

                    if (!sessioncountryid.Equals("1"))
                    {
                        //country_list.SelectedIndex = country_list.Items.IndexOf(new ListItem(Session["country"].ToString(), Session["countryId"].ToString()));
                        // country_list.Enabled = false;
                        // int countryid = int.Parse(country_list.SelectedValue.ToString());
                        //LoadAreaList(countryid);
                        LoadAreaList(int.Parse(sessioncountryid));
                        area_list.SelectedIndex = area_list.Items.IndexOf(new ListItem(Session["area"].ToString(), Session["areaId"].ToString()));
                        area_list.Enabled = false;
                        int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
                        int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());

                        LoadBranchList(areaid,operationid);
                    }
                    else
                    {
                        //int countryid = int.Parse(country_list.SelectedValue.ToString());
                        int countryid = int.Parse(sessioncountryid);
                        LoadAreaList(countryid);
                    }
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Adding New Applicant page");
                }
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }

        }

        private void LoadFilters(int areaid)
        {
            ddloperationarea.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea.DataBind();
            ddlterritory.DataSource = bll.GetTerritoryList(int.Parse(ddloperationarea.SelectedValue.ToString()), 0);
            ddlterritory.DataBind();
            ddlsubterritory.DataSource = bll.GetSubTerritoryList(int.Parse(ddlterritory.SelectedValue.ToString()));
            ddlsubterritory.DataBind();
        }

        private void LoadBranchList(int areaid,int operationid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid,operationid);
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
        private void LoadRequirementList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetRequirementList();
                chkBoxRequired.DataSource = dt;

                chkBoxRequired.DataTextField = "requirementName";
                chkBoxRequired.DataValueField = "requirementId";
                chkBoxRequired.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayRequirement", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadCustomerTypeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetCustomerTypeList();
                customertype_list.DataSource = dt;
                
                customertype_list.DataTextField = "typeName";
                customertype_list.DataValueField = "custTypeId";
                customertype_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayCustomerType", error);
                DisplayMessage(error, true);
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

        private void LoadIDList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetIDList();
                cboID.DataSource = dt;

                cboID.DataTextField = "IdName";
                cboID.DataValueField = "IdType";
                cboID.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadIDList", error);
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
        //protected void rtnServicetype_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //}
        protected void rtnTariff_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
        protected void cboID_DataBound(object sender, EventArgs e)
        {
            cboID.Items.Insert(0, new ListItem("- - select ID  Type- -", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                app = new ApplicationObj();
                app.CustomerType = customertype_list.Text;
                app.FirstName = txtfirstname.Text.Trim();
                app.OtherName = txtothername.Text.Trim();
                app.LastName = txtlastname.Text.Trim();
                app.Email = txtemail.Text.Trim();
                app.Occupation = txtoccupation.Text.Trim();
                app.Telephone = txtphone.Text.Trim();
                app.Address = txtaddress.Text.Trim();
                app.IdType = cboID.SelectedValue.ToString();
                app.IdNumber = txtidnumber.Text.Trim();
                app.Country = "2";//country_list.SelectedValue.ToString();
                                  // app.City = txtcity.Text.Trim();
                                  // app.State = txtstate.Text.Trim();
                                  // app.Division = txtdivision.Text.Trim();
                                  // app.Village = txtvillage.Text.Trim();
                app.PlotNumber = txtplot.Text.Trim();
                app.ServiceId = "1";//rtnServicetype.SelectedValue.ToString();1-water
                app.CategoryId = rtncategory.SelectedValue.ToString();
                //set deault
                app.ApplicationDate = DateTime.Now;
                app.StatusId = "1"; //initaition
                string str = ""; string res = "";
                app.ApplicationId = "0";
                //get sessiondetails from selected
                string a = app.Country;
                string b = area_list.SelectedValue.ToString();
                string c = "0";//branch
                string userid = Session["userId"].ToString();
                app.Country = a;
                app.Area = b;
                app.Branch = c;
                LoadCodeSessions(a, b, c);
                //new settings
                app.OperationId = ddloperationarea.SelectedValue.ToString();
                app.Branch = branch_list.SelectedValue.ToString();
                app.Territory = ddlterritory.SelectedValue.ToString();
                app.SubTerritory = ddlsubterritory.SelectedValue.ToString();
                //end default
                //validate input
                resp = bll.ValidateApplication(app.FirstName, app.LastName, app.Telephone, app.Address, app.IdNumber);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    if (app.Country.Equals("0"))
                    {
                        str = "Please select country";
                        DisplayMessage(str, true);
                    }
                    else if (app.OperationId.Equals("0"))
                    {
                        str = "Please select operation area";
                        DisplayMessage(str, true);
                    }
                    else if (customertype_list.SelectedValue.Equals("0"))
                    {
                        str = "Please select customer type";
                        DisplayMessage(str, true);
                    }


                    else
                    {
                        int bites = FileField.PostedFile.ContentLength;
                        String ext = Path.GetExtension(FileField.PostedFile.FileName);
                        int mbs = bites / 1024;
                        int maxbites = 3145728;//3mbs
                        //1mb=1048576 bytes,3mb=3145728 bytes
                        String[] allowed = { ".png", ".jpg", ".jpeg", ".pdf" };
                        Boolean isvalidFile = false;
                        foreach (string extension in allowed)
                        {
                            if (extension.Equals(ext))
                            {
                                isvalidFile = true;
                            }
                        }
                        if (!isvalidFile)
                        {
                            DisplayMessage("jpg,png,jpeg and pdf formats only are allowed" + ext, true);
                        }
                        else if (bites >= maxbites)
                        {
                            DisplayMessage("files too big not uploaded maximum file size 3mbs", true);
                        }
                        else
                        {
                            // app.ApplicationNo = bll.GetApplicationNumber("0", app.Country, app.Area, app.Branch, userid);
                            //new application number
                            app.ApplicationNo = bll.GetNewApplicationNumber("0", app.OperationId, app.Branch);

                            //resp.Response_Code="test"; //test only
                            resp = bll.SaveApplication(app);
                            if (resp.Response_Code == "0")
                            {
                                string ApplicationID = "0";
                                UploadFiles(ApplicationID);
                                str = " with new application(" + app.ApplicationNo + ") details saved and forwarded to surveyor for futher action";

                                res = resp.Response_Message + str;
                                //DisplayMessage(res, false);
                                //alert(res);
                                MsgBox(res, this.Page, this);
                            }
                            else
                            {
                                str = " details with application identity(" + app.ApplicationNo + ")";
                                res = resp.Response_Message + str;
                                DisplayMessage(res, true);
                            }
                            RefreshControls();
                        }

                    }

                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void UploadFiles(string ApplicationCode)
        {
            HttpFileCollection uploads;
            uploads = HttpContext.Current.Request.Files;
            int countfiles = 0;
            for (int i = 0; i <= (uploads.Count - 1); i++)
            {
                if (uploads[i].ContentLength > 0)
                {
                    // string c = System.IO.Path.GetFileName(uploads[i].FileName);
                    string c = DateTime.Now.Ticks + System.IO.Path.GetFileName(uploads[i].FileName);

                    string cNoSpace = c.Replace(" ", "_");
                    string c1 = "Serial-" + ApplicationCode + "-" + (countfiles + i + 1) + "-" + cNoSpace;
                    string Path = bll.GetFileApplicationPath();
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }

                    FileField.PostedFile.SaveAs(Path + "" + c1);
                    bll.SaveApplicationFiles(ApplicationCode, (Path + "" + c1), c);


                }
            }
        }


        private void LoadCodeSessions(string a,string b,string c)
        {
            Session["countryCode"] = bll.GetCodeIdentity(a,1);
            Session["areaCode"] = bll.GetCodeIdentity(b, 2);
            Session["branchCode"] = bll.GetCodeIdentity(c, 3);
        }

        private void RefreshControls()
        {
            txtfirstname.Text = "";
            txtlastname.Text = "";
            txtothername.Text = "";
            txtemail.Text = "";
            txtoccupation.Text = "";
            txtphone.Text = "";
            txtaddress.Text = "";
            cboID.SelectedValue = "0";
            txtidnumber.Text = "";
            //country_list.SelectedValue = "0";
            //area_list.SelectedValue = "0";
           // txtdivision.Text = "";
           // txtvillage.Text = "";
            txtplot.Text = "";
           // rtnServicetype.ClearSelection();
            rtncategory.ClearSelection();
            customertype_list.ClearSelection();
            chkBoxRequired.ClearSelection();
            ddloperationarea.SelectedValue = "0";
            ddlterritory.SelectedValue = "0";
            ddlsubterritory.SelectedValue = "0";
            branch_list.SelectedValue = "0";
            txtlandmark.Text = "";
        }
        //protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
        //        LoadAreaList(countryid);
        //      //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        protected void branch_list_DataBound(object sender, EventArgs e)
        {
            branch_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        //}
        }
        

        private void LoadAreaSessions(int areaid)
        {
            try
            {
                dt = bll.GetAreaSessions(areaid);
                if(dt.Rows.Count > 0)
                {
                    //areaId,areaName,A.areaCode,C.countryCode,C.countryName,C.countryId
                    Session["areaCode"] = dt.Rows[0]["areaCode"].ToString();
                    Session["countryCode"] = dt.Rows[0]["countryCode"].ToString();
                    Session["branchCode"] = "0";
                }
                else
                {
                    Session["areaCode"] = "0";
                    Session["countryCode"] = "0";
                    Session["branchCode"] = "0";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void operation_area_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int areaid = Convert.ToInt16(area_list.SelectedValue.ToString());
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());

                LoadBranchList(areaid,operationid);
                //load session data
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
                string appno = lblapplication.Text;
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
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("select area", "0"));
        }
        protected void ddloperationarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                int branchid = Convert.ToInt16(branch_list.SelectedValue.ToString());

                // LoadBranchList(operationid);
                ddlterritory.DataSource = bll.GetTerritoryList(operationid,branchid);
                ddlterritory.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlterritory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int territory = Convert.ToInt16(ddlterritory.SelectedValue.ToString());
                //LoadBranchList(operationid);
                ddlsubterritory.DataSource = bll.GetSubTerritoryList(territory);
                ddlsubterritory.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddlterritory_DataBound(object sender, EventArgs e)
        {
            ddlterritory.Items.Insert(0, new ListItem("--none--", "0"));
        }
        protected void ddlsubterritory_DataBound(object sender, EventArgs e)
        {
            ddlsubterritory.Items.Insert(0, new ListItem("--none--", "0"));
        }
        protected void branch_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                int branchid = Convert.ToInt16(branch_list.SelectedValue.ToString());

                // LoadBranchList(operationid);
                ddlterritory.DataSource = bll.GetTerritoryList(operationid, branchid);
                ddlterritory.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void MsgBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }
    }
}