
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.Drawing;
using RKLib.ExportData;

namespace TraceBilling
{
    public partial class ReadingExceptions : System.Web.UI.Page
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

                    if (Session["roleId"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        LoadFilters(10);
                       
                        //LoadExceptionsByOption();
                        LoadException();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Reading Exception page");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadFilters(int areaid)
        {
            ddloperationarea.DataSource = bll.GetOperationAreaList(areaid);
            ddloperationarea.DataBind();
            ddlbranch.DataSource = bll.GetBranchList(areaid, 0);
            ddlbranch.DataBind();
            ddlblock.DataSource = bll.GetBlockMaps(areaid.ToString(), "0");
            ddlblock.DataBind();

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
        private void LoadException()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetReadingExceptions();
                exception_list.DataSource = dt;// exceptionId,exceptionName
                exception_list.DataTextField = "exceptionName";
                exception_list.DataValueField = "exceptionId";
                exception_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadException", error);
                DisplayMessage(error, true);
            }
        }
    

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {

                LoadExceptionsByOption();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadExceptionsByOption()
        {
            try
            {
                
                string area = ddloperationarea.SelectedValue.ToString();
                string period = txtsearch.Text.Trim();
                string branch = ddlbranch.SelectedValue.ToString();
                string block = ddlblock.SelectedValue.ToString();
                string option = exception_list.SelectedValue.ToString();
                DataTable dataTable = bll.GetExceptionsData(area, branch,  period, block,option);
                //if (dataTable.Rows.Count > 0)
                //{
                //    gv_applicationview.DataSource = dataTable;
                //    gv_applicationview.DataBind();
                //    DisplayMessage(".", true);
                //    maindisplay.Visible = true;
                //}
                //else
                //{
                //    string error = "100: " + "No records found";
                //    bll.Log("GetApplication", error);
                //    DisplayMessage(error, true);
                //    maindisplay.Visible = false;
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //protected void btnReturn_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        maindisplay.Visible = true;
        //        btnreturn.Visible = true;
        //        LoadApplicationByStatus();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
   
        protected void gv_applicationview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[5];
                string type = e.Row.Cells[5].Text;
                type = type.ToLower();
                // e.Row.BackColor = Color.Blue;
                //e.Row.ForeColor = Color.Green;
                if ((type.Contains("flat")))
                {

                    // link.ForeColor = Color.Red;
                    e.Row.Cells[5].BackColor = Color.Red;
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }
                else if ((type.Contains("post")))
                {

                    e.Row.Cells[5].BackColor = Color.Blue;
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }
                else if ((type.Contains("pre")))
                {

                    e.Row.Cells[5].BackColor = Color.Green;
                    e.Row.Cells[5].ForeColor = Color.White;
                    e.Row.Cells[5].Font.Bold = true;
                }

            }
        }
        protected void gv_applicationview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_applicationview_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            
        }

       

        protected void gv_applicationview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
               // string usercode = gv_applicationview.Rows[index].Cells[1].Text;


            }
        }
    
    
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("all", "0"));
        }
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("all", "0"));
        }
        protected void ddlblock_DataBound(object sender, EventArgs e)
        {
            ddlblock.Items.Insert(0, new ListItem("all", "0"));
        }
        protected void gvdocuments_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }
        protected void exception_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string optionid = exception_list.SelectedValue.ToString();
                if (optionid == "1")
                {

                    zerodisplay.Visible = true;
                    lowdisplay.Visible = false;
                    highdisplay.Visible = false;
                    negativedisplay.Visible = false;
                    estimatedisplay.Visible = false;
                    unreaddisplay.Visible = false;
                    suppressedisplay.Visible = false;
                }
                else if (optionid == "2")
                {
                    zerodisplay.Visible = false;
                    lowdisplay.Visible = true;
                    highdisplay.Visible = false;
                    negativedisplay.Visible = false;
                    estimatedisplay.Visible = false;
                    unreaddisplay.Visible = false;
                    suppressedisplay.Visible = false;
                }
                else
                {
                    zerodisplay.Visible = false;
                    lowdisplay.Visible = false;
                    highdisplay.Visible = true;
                    negativedisplay.Visible = false;
                    estimatedisplay.Visible = false;
                    unreaddisplay.Visible = false;
                    suppressedisplay.Visible = false;
                }
                DisplayMessage(".", true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }



    }
}