
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
    public partial class ManageAreas : System.Web.UI.Page
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
                    
                    LoadDisplay("1");
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
                    bll.Log("LoadDisplayArea", error);
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
            string flag = "1";
            if (e.CommandName == "RowDelete")
            {
                //string UserID = e.Item.Cells[0].Text;
                string ItemId = "0";

                //ItemId = e.Item.Cells[1].Text;
                ItemId = Convert.ToString(e.CommandArgument.ToString());
                //delete record              
               
                string deletedby = Session["UserID"].ToString();
                bll.DeleteSettingItem(ItemId, int.Parse(flag), deletedby);
                LoadDisplay(flag);
            }
            else if (e.CommandName == "RowEdit")
            {
                string code = Convert.ToString(e.CommandArgument.ToString());
                LoadControls(code,flag);
            }

        }

        private void LoadControls(string code,string flag)
        {
            dt = bll.GetSettingsDetailsByID(flag, code);
            if (dt.Rows.Count > 0)
            {
                lblarea.Text = dt.Rows[0]["operationId"].ToString();
                txtarea.Text = dt.Rows[0]["operationAreaName"].ToString();
                txtcode.Text = dt.Rows[0]["operationCode"].ToString();
                txtalias.Text = dt.Rows[0]["operationAlias"].ToString();

                string IsActive = dt.Rows[0]["Isactive"].ToString();
                if (IsActive.Equals("YES"))
                {
                    chkarea.Checked = true;
                }
                else
                {
                    chkarea.Checked = false;
                }

                if (!lblarea.Text.Equals("0"))
                {
                    btnAddArea.Text = "Update";
                }
            }
            else
            {
                DisplayMessage("No area records to edit", true);
            }
        }

        protected void btnAddArea_Click(object sender, EventArgs e)
        {
            try
            {
                string areaname = txtarea.Text.Trim();
                string code = txtcode.Text.Trim();
                string alias = txtalias.Text.Trim();
                bool ckarea = chkarea.Checked;
                string areaid = lblarea.Text;
                if (areaname == "")
                {
                    DisplayMessage("Please enter area name", true);
                }
                else if (code == "")
                {
                    DisplayMessage("Please enter area code", true);
                }
                else if (alias == "")
                {
                    DisplayMessage("Please enter area alias", true);
                }
                else
                {
                    resp = bll.SaveArea(areaid, areaname,code,alias, ckarea);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with new area(" + areaname + ") saved";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);

                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {
                        string str = " with area(" + areaname + ") details updated";
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
            txtcode.Text = "";
            txtalias.Text = "";
            chkarea.Checked = false;
            lblarea.Text = "0";
        }
    }
}