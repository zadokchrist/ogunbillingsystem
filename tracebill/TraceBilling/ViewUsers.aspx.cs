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
    public partial class ViewUsers : System.Web.UI.Page
    {
        BusinessLogic bll = new BusinessLogic();
        ResponseMessage resp = new ResponseMessage();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if ((Session["RoleID"] == null))
            {
                Response.Redirect("Default.aspx?redirect_url=" + url);
            }
            try
            {
                if (IsPostBack == false)
                {
                    loadFilters();
                    LoadUsers();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed View Users page");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void loadFilters()
        {
        
          
            ddlbranch.DataSource = bll.GetBranchList(10,0);
            ddlbranch.DataBind();
            ddloperationarea.DataSource = bll.GetOperationAreaList(10);
            ddloperationarea.DataBind();


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

        private void LoadUsers()
        {
            try
            {
                DataTable dataTable = bll.GetAllUsers_filtered(txtsearch.Text.Trim(), ddloperationarea.SelectedValue.ToString(),ddlbranch.SelectedValue.ToString());
                Session["dtusr"] = dataTable;
                if (dataTable.Rows.Count > 0)
                {
                    GridViewUser.DataSource = dataTable;
                    GridViewUser.DataBind();
                    DisplayMessage(".", true);
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("GetAllUsers_filtered", error);
                    DisplayMessage(error, true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
               
                LoadUsers();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - All countries - -", "0"));
        //}



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

        protected void GridViewUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = GridViewUser.Rows[index].Cells[1].Text;
                if (e.NewSelectedIndex.ToString() == "Edit")
                {
                    //
                    DisplayMessage("Hello xxxx", true);
                }

            }
        }
        protected void GridViewUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowEdit")
            {
                //string UserID = e.Item.Cells[0].Text;
                string Usercode = Convert.ToString(e.CommandArgument.ToString());
                //Response.Redirect("./AddUser.aspx?transferid=" + UserID, true);
                Server.Transfer("./AddUser.aspx?transferid=" + Usercode, true);
                //DisplayMessage("Hello World", true);
            }
            else if (e.CommandName == "RowChange")
            {
                string action = "";
                string[] arg = new string[3];
                arg = e.CommandArgument.ToString().Split(';');
                string userid = arg[0];
                string username = arg[1];
                string status = arg[2];
                if (status.Equals("YES"))
                {
                    action = "DISABLED";
                }
                else
                {
                    action = "ENABLED";
                }
                string returned = "";
                returned = bll.ChangeUserAccess(userid, username, status, action);
                LoadUsers();
                DisplayMessage(returned, true);
            }
            else if (e.CommandName == "RowReset")
            {
                //string UserID = e.Item.Cells[0].Text;
                string[] arg = new string[3];
                arg = e.CommandArgument.ToString().Split(';');
                string userid = arg[0];
                string username = arg[1];
                string fullname = arg[2];
                string email = arg[3];
                string changedBy = Session["userID"].ToString();
                string action = "RESET PASSWORD";
                string returned = bll.ResetUserPassword(userid, username, fullname, changedBy, action,email);
                DisplayMessage(returned, false);

            }
        }
        /*protected void GridViewIssue_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           // GridViewRow row = (GridViewRow)GridViewIssue.Rows[e.RowIndex];

        }
        protected void GridViewIssue_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //GridViewRow row = (GridViewRow)GridViewIssue.Rows[e.NewEditIndex];
            //gvProducts.EditIndex = e.NewEditIndex;
            //BindData();

        }*/



        protected void GridViewIssue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewUser_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string status = e.Row.Cells[2].Text;

            }
        }
        //protected void ddlrole_DataBound(object sender, EventArgs e)
        //{
        //    ddlrole.Items.Insert(0, new ListItem("All", "0"));
        //}
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }
        protected void GridViewUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewUser.PageIndex = e.NewPageIndex;
            GridViewUser.DataSource = Session["dtusr"] as DataTable;
            GridViewUser.DataBind();
        }
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("--select--", "0"));
        }
    }
}