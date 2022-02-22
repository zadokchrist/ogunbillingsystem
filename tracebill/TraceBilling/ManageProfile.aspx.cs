
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
    public partial class ManageProfile : System.Web.UI.Page
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

                    LoadDisplay("5");
                }
            }
            catch (Exception ex)
            {
                throw ex;
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


        private void LoadDisplay(string flag)
        {
            try
            {
                DisplayMessage("", false);
                DataTable dt = bll.GetSettingsDetails(flag);
                if (dt.Rows.Count > 0)
                {
                    GridViewIssue.DataSource = dt;
                    GridViewIssue.DataBind();
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("LoadDisplayProfile", error);
                    DisplayMessage(error, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void GridViewIssue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void GridViewIssue_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
        protected void GridViewIssue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string flag = "5";
           
            if (e.CommandName == "RowEdit")
            {
                string code = Convert.ToString(e.CommandArgument.ToString());
                LoadControls(code, flag);
            }

        }

        private void LoadControls(string code, string flag)
        {
            dt = bll.GetSettingsDetailsByID(flag, code);
            if (dt.Rows.Count > 0)
            {
                
                lblarea.Text = dt.Rows[0]["companyId"].ToString();
                txtarea.Text = dt.Rows[0]["companyName"].ToString();
                txtaddress.Text = dt.Rows[0]["physicalAddress"].ToString();
                txttollfree.Text = dt.Rows[0]["tollContact"].ToString();
                txtcontact.Text = dt.Rows[0]["othercontact"].ToString();
                txtmail.Text = dt.Rows[0]["emailAddress"].ToString();
                txtweb.Text = dt.Rows[0]["webAddress"].ToString();

                //string IsActive = dt.Rows[0]["Isactive"].ToString();
                //if (IsActive.Equals("YES"))
                //{
                //    chkarea.Checked = true;
                //}
                //else
                //{
                //    chkarea.Checked = false;
                //}

                //if (!lblarea.Text.Equals("0"))
                //{
                //    btnAddArea.Text = "Update";
                //}
            }
            else
            {
                DisplayMessage("No company records to edit", true);
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string areaname = txtarea.Text.Trim();
                string address = txtaddress.Text.Trim();
                string email = txtmail.Text.Trim();
                string tollfree = txttollfree.Text.Trim();
                string contact = txtcontact.Text;
                string web = txtweb.Text.Trim();
               // bool ckarea = chkarea.Checked;
                string areaid = lblarea.Text;
                if (areaid == "0")
                {
                    DisplayMessage("Sorry, you can only edit existing company profile!!!", true);
                }
                else if (areaname == "")
                {
                    DisplayMessage("Please enter area name", true);
                }
                else if (address == "")
                {
                    DisplayMessage("Please enter physical address", true);
                }
                else if (email == "")
                {
                    DisplayMessage("Please enter official email", true);
                }
                else if (tollfree == "")
                {
                    DisplayMessage("Please enter official toll free line", true);
                }
                else
                {
                    resp = bll.SaveProfile(areaid, areaname, address, email, contact,tollfree,web);
                    
                    if (resp.Response_Code == "1")//edit and update
                    {
                        string str = " with company(" + areaname + ") details updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else
                    {
                        DisplayMessage(resp.Response_Message, true);
                    }
                    RefreshControls();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RefreshControls()
        {
            txtarea.Text = "";
            txtaddress.Text = "";
            txtcontact.Text = "";
            txttollfree.Text = "";
            txtweb.Text = "";
            //chkarea.Checked = false;
            lblarea.Text = "0";
        }
    }
}