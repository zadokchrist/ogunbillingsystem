
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
    public partial class ManageTerritory : System.Web.UI.Page
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

                    LoadDisplay("3");
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
                LoadFilters();
               
                DataTable dt = bll.GetSettingsDetails(flag);
                if (dt.Rows.Count > 0)
                {
                    GridViewIssue.DataSource = dt;
                    GridViewIssue.DataBind();
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("LoadDisplayTerritory", error);
                    DisplayMessage(error, true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadFilters()
        {
            ddloperationarea.DataSource = bll.GetOperationAreaList(10);
            ddloperationarea.DataBind();
            ddlbranch.DataSource = bll.GetBranchList(0, 0);
            ddlbranch.DataBind();
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
            string flag = "3";
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
                LoadControls(code, flag);
            }

        }

        private void LoadControls(string code, string flag)
        {
            dt = bll.GetSettingsDetailsByID(flag, code);
            if (dt.Rows.Count > 0)
            {
                lblterritory.Text = dt.Rows[0]["territoryId"].ToString();
                txtterritory.Text = dt.Rows[0]["territory"].ToString();
                string branch = dt.Rows[0]["branchName"].ToString();
                string area = dt.Rows[0]["operationAreaName"].ToString();
                ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(ddloperationarea.Items.FindByText(area));
                ddlbranch.SelectedIndex = ddlbranch.Items.IndexOf(ddlbranch.Items.FindByText(branch));

                string IsActive = dt.Rows[0]["Isactive"].ToString();
                if (IsActive.Equals("YES"))
                {
                    chkterritory.Checked = true;
                }
                else
                {
                    chkterritory.Checked = false;
                }
                if (!lblterritory.Text.Equals("0"))
                {
                    btnAddTerritory.Text = "Update";
                }


            }
            else
            {
                DisplayMessage("No territory records to edit", true);
            }
        }

        protected void btnAddTerritory_Click(object sender, EventArgs e)
        {
            try
            {
                string territory = txtterritory.Text.Trim();
                string branch = ddlbranch.SelectedValue.ToString();
                string area = ddloperationarea.SelectedValue.ToString();
                bool ckterritory = chkterritory.Checked;
                string territoryid = lblterritory.Text;
                if (territory == "")
                {
                    DisplayMessage("Please enter territory name", true);
                }
                else if (area == "0")
                {
                    DisplayMessage("Please select area", true);
                }
                else if (branch == "0")
                {
                    DisplayMessage("Please select branch", true);
                }
                else
                {
                    resp = bll.SaveTerritory(territoryid, territory, area, branch, ckterritory);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with new territory(" + territory + ") saved";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);

                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {
                        string str = " with territory(" + territory + ") details updated";
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
            txtterritory.Text = "";
            ddloperationarea.SelectedValue = "0";
            ddlbranch.SelectedValue = "0";
            chkterritory.Checked = false;
            lblterritory.Text = "0";
        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("select area", "0"));
        }
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("select branch", "0"));
        }
        protected void ddloperationarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                //int branchid = Convert.ToInt16(branch_list.SelectedValue.ToString());
                ddlbranch.DataSource = bll.GetBranchList(10, operationid);
                ddlbranch.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}