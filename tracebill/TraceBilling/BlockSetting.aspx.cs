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

namespace TraceBilling
{
    public partial class BlockSetting : System.Web.UI.Page
    {
        BusinessLogic bll = new BusinessLogic();
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
                    ddloperationarea.DataSource = bll.GetOperationAreaList(10);
                    ddloperationarea.DataBind();
                    int areaid = 10;
                    int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                    LoadBranchList(areaid, operationid);

                    LoadBlockDetails();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Block Setting page");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        private void LoadBranchList(int areaid, int operationid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid, operationid);
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

        private void LoadBlockDetails()
        {
            string country = "2";
            string area = ddloperationarea.SelectedValue.ToString();
            string branch = branch_list.SelectedValue.ToString();
            DataTable dataTable = bll.GetBlockDetails(country,area,branch);
            if (dataTable.Rows.Count > 0)
            {
                gv_blocksettings.DataSource = dataTable;
                gv_blocksettings.DataBind();
                DisplayMessage(".", true);
                maindisplay.Visible = true;
            }
            else
            {
                string error = "100: " + "No records found";
                bll.Log("LoadBlockDetails", error);
                DisplayMessage(error, true);
                maindisplay.Visible = false;
            }
        }
        
        protected void branch_list_DataBound(object sender, EventArgs e)
        {
            branch_list.Items.Insert(0, new ListItem("- - None - -", "0"));
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
        protected void ddloperationarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int operationid = Convert.ToInt16(ddloperationarea.SelectedValue.ToString());
                int branchid = Convert.ToInt16(branch_list.SelectedValue.ToString());

                LoadBranchList(10, operationid);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("--select--", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string block = txtblock.Text.Trim();
                string connection = txtconnection.Text.Trim();
                bool isactive = chkActive.Checked;
                string country = "2";
                string area = "10";
                string branch = branch_list.SelectedValue.ToString();
                string oparea = ddloperationarea.SelectedValue.ToString();

                string createdby = Session["UserID"].ToString();
                string status = rtnblockstatus.SelectedValue.ToString();
                string code = lblblockcode.Text;
                if (block == "")
                {
                    DisplayMessage("Please Enter block", true);

                }               
                else if (connection == "")
                {
                    DisplayMessage("Please enter connection Number", true);

                }
                else if (!bll.IsNumeric(connection))
                {                   
                    DisplayMessage("Please enter connection as Integer", true);
                }
                else
                {
                    bll.SaveBlockDetails(code,country,area,branch,block,connection, createdby, isactive,status,oparea);
                    string str = "";
                    if (code == "0")
                    {
                        str = "Block - " + block + " has been saved Successfully";
                    }
                    else
                    {
                        str = "Block - " + block + "'s details have been updated Successfully";
                    }

                    DisplayMessage(str, false);                    
                    RefreshControls();
                    LoadBlockDetails();
                }

            }
            catch (Exception ex)
            {
                //throw ex;
                DisplayMessage(ex.Message, true);
            }
        }

        private void RefreshControls()
        {
            txtconnection.Text = "";
            txtblock.Text = "";
            ddloperationarea.SelectedValue = "0";
            branch_list.SelectedValue = "0";
            rtnblockstatus.ClearSelection();
            chkActive.Checked = false;
            lblblockcode.Text = "0";
            btnSave.Text = "Add";
        }

       
        protected void gv_blocksettings_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
          
        }
        protected void gv_blocksettings_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_blocksettings_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowEdit")
            {
                //string UserID = e.Item.Cells[0].Text;
                string blockId = Convert.ToString(e.CommandArgument.ToString());
                LoadBlockSetting(blockId);

            }
            
        }


        private void LoadBlockSetting(string blockId)
        {

            DataTable dt = bll.GetBlockSettingByID(blockId);
            lblblockcode.Text = dt.Rows[0]["BlockID"].ToString();
            txtblock.Text = dt.Rows[0]["BlockNumber"].ToString();
            txtconnection.Text = dt.Rows[0]["ConnectionNumber"].ToString();
            string country = dt.Rows[0]["countryName"].ToString();
            string area = dt.Rows[0]["areaName"].ToString();
            string branch = dt.Rows[0]["branchName"].ToString();
            ddloperationarea.SelectedIndex = ddloperationarea.Items.IndexOf(ddloperationarea.Items.FindByText(area));
            branch_list.SelectedIndex = branch_list.Items.IndexOf(branch_list.Items.FindByText(branch));

            bool IsActive = Convert.ToBoolean(dt.Rows[0]["Active"].ToString());
            chkActive.Checked = IsActive;
            string status = dt.Rows[0]["status"].ToString();
            if(status.Equals("Permanent"))
            {
                rtnblockstatus.SelectedValue = "1";
            }
            else
            {
                rtnblockstatus.SelectedValue = "2";
            }
            if (!lblblockcode.Text.Equals("0"))
            {
                btnSave.Text = "Update";
            }
        }

        protected void gv_blocksettings_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string status = e.Row.Cells[2].Text;

            }
        }
    }
}