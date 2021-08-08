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
                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadBranchList1(0);
                    LoadBlockDetails();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Block Setting page");
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

        private void LoadBlockDetails()
        {
            string country = country_list.SelectedValue.ToString();
            string area = area_list.SelectedValue.ToString();
            string branch = branch_list1.SelectedValue.ToString();
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
        protected void branch_list1_DataBound(object sender, EventArgs e)
        {
            branch_list1.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void country_list_DataBound(object sender, EventArgs e)
        {
            country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        }
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
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
        protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                LoadAreaList(countryid);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void area_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string block = txtblock.Text.Trim();
                string connection = txtconnection.Text.Trim();
                bool isactive = chkActive.Checked;
                string country = country_list.SelectedValue.ToString();
                string area = area_list.SelectedValue.ToString();
                string branch = branch_list1.SelectedValue.ToString();
                string createdby = Session["UserID"].ToString();
                string status = rtnblockstatus.SelectedValue.ToString();
                string code = lblblockcode.Text;
                if (block == "")
                {
                    DisplayMessage("Please Enter block", true);

                }
                else if (country == "0")
                {
                    DisplayMessage("Please select country", true);

                }
                else if (area == "0")
                {
                    DisplayMessage("Please select area", true);

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
                    bll.SaveBlockDetails(code,country,area,branch,block,connection, createdby, isactive,status);
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
            country_list.SelectedValue = "0";
            area_list.SelectedValue = "0";
            branch_list1.SelectedValue = "0";
            rtnblockstatus.ClearSelection();
            chkActive.Checked = false;
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
            country_list.SelectedIndex = country_list.Items.IndexOf(country_list.Items.FindByText(country));
            string area = dt.Rows[0]["areaName"].ToString();
            area_list.SelectedIndex = area_list.Items.IndexOf(area_list.Items.FindByText(area));
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