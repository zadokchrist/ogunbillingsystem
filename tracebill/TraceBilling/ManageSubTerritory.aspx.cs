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
    public partial class ManageSubTerritory : System.Web.UI.Page
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

                    LoadDisplay("4");
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
                ddlterritory.DataSource = bll.GetTerritoryList(0,0);
                ddlterritory.DataBind();
                DataTable dt = bll.GetSettingsDetails(flag);
                if (dt.Rows.Count > 0)
                {
                    GridViewIssue.DataSource = dt;
                    GridViewIssue.DataBind();
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("LoadDisplaySubTerritory", error);
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
            string flag = "4";
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
                lblsubterritory.Text = dt.Rows[0]["subTerritoryId"].ToString();
                txtsubterritory.Text = dt.Rows[0]["subTerritory"].ToString();
                string territory = dt.Rows[0]["territory"].ToString();

                string IsActive = dt.Rows[0]["Isactive"].ToString();
                if (IsActive.Equals("YES"))
                {
                    chksubterritory.Checked = true;
                }
                else
                {
                    chksubterritory.Checked = false;
                }
                if (!lblsubterritory.Text.Equals("0"))
                {
                    btnAddSubTerritory.Text = "Update";
                }

            }
            else
            {
                DisplayMessage("No subterritory records to edit", true);
            }
        }

        protected void btnAddSubTerritory_Click(object sender, EventArgs e)
        {
            try
            {
                string subterritory = txtsubterritory.Text.Trim();
                string territory = ddlterritory.SelectedValue.ToString();
                bool cksubterritory = chksubterritory.Checked;
                string subterritoryid = lblsubterritory.Text;
                if (subterritory == "")
                {
                    DisplayMessage("Please enter subterritory name", true);
                }                
                else
                {
                    resp = bll.SaveSubterritory(subterritoryid, subterritory, territory, cksubterritory);
                    if (resp.Response_Code == "0")//save
                    {
                        string str = " with new subterritory(" + subterritory + ") saved";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);

                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {
                        string str = " with area(" + subterritory + ") details updated";
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
            txtsubterritory.Text = "";
            ddlterritory.SelectedValue = "0";
            chksubterritory.Checked = false;
            lblsubterritory.Text = "0";
        }
        protected void ddlterritory_DataBound(object sender, EventArgs e)
        {
            ddlterritory.Items.Insert(0, new ListItem("select territory", "0"));
        }
    }
}