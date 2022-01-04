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
    public partial class ManageBranches : System.Web.UI.Page
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

                    LoadDisplay("2");
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
                ddloperationarea.DataSource = bll.GetOperationAreaList(10);
                ddloperationarea.DataBind();
                DataTable dt = bll.GetSettingsDetails(flag);
                if (dt.Rows.Count > 0)
                {
                    GridViewIssue.DataSource = dt;
                    GridViewIssue.DataBind();
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("LoadDisplay", error);
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
            string flag = "2";
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
                lblbranch.Text = dt.Rows[0]["branchId"].ToString();
                txtbranch.Text = dt.Rows[0]["branchName"].ToString();
                txtcode.Text = dt.Rows[0]["branchCode"].ToString();
                txtalias.Text = dt.Rows[0]["branchAlias"].ToString();
                string area = dt.Rows[0]["operationAreaName"].ToString();
                ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(ddloperationarea.Items.FindByText(area));
                string IsActive = dt.Rows[0]["Isactive"].ToString();
                if (IsActive.Equals("YES"))
                {
                    chkbranch.Checked = true;
                }
                else
                {
                    chkbranch.Checked = false;
                }
                if (!lblbranch.Text.Equals("0"))
                {
                    btnAddBranch.Text = "Update";
                }

            }
            else
            {
                DisplayMessage("No branch records to edit", true);
            }
        }

        protected void btnAddBranch_Click(object sender, EventArgs e)
        {
            try
            {
                string branchname = txtbranch.Text.Trim();
                string code = txtcode.Text.Trim();
                string alias = txtalias.Text.Trim();
                bool ckbranch = chkbranch.Checked;
                string branchid = lblbranch.Text;
                string area = ddloperationarea.SelectedValue.ToString();
                if (branchname == "")
                {
                    DisplayMessage("Please enter branch name", true);
                }
                else if (code == "")
                {
                    DisplayMessage("Please enter branch code", true);
                }
                else if (alias == "")
                {
                    DisplayMessage("Please enter branch alias", true);
                }
                else if(area.Equals("0"))
                {
                    DisplayMessage("Please attach branch to an area", true);
                }
                else
                {
                    resp = bll.SaveBranch(branchid, branchname, code, alias, area, ckbranch);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with new branch(" + branchname + ") saved";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);

                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {
                        string str = " with branch(" + branchname + ") details updated";
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
            txtbranch.Text = "";
            txtcode.Text = "";
            txtalias.Text = "";
            chkbranch.Checked = false;
            lblbranch.Text = "0";
            ddloperationarea.SelectedValue = "0";
        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("select area", "0"));
        }
    }
}