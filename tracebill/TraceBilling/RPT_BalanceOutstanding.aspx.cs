
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
    public partial class RPT_BalanceOutstanding : System.Web.UI.Page
    {
        BusinessLogic bll = new BusinessLogic();
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
                        string sessioncountryid = Session["countryId"].ToString();
                        int areaid = 10;
                        ddlbranch.DataSource = bll.GetBranchList(areaid);
                        ddlbranch.DataBind();
                        LoadDisplay();
                        bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Balance outstanding report");

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private void LoadDisplay()
        {
            try
            {
                string branch = ddlbranch.SelectedValue.ToString();
                string fromdate = txtfromdatesrc.Text.Trim();
                string todate = txttodatesrc.Text.Trim();
                string amount = txtamount.Text.Trim();
                if(amount.Equals(""))
                {
                    amount = "0";
                }
                DateTime startdate = Convert.ToDateTime(bll.ReturnDate(fromdate, 1));
                DateTime enddate = Convert.ToDateTime(bll.ReturnDate(todate, 2));
                DataTable dataTable = bll.GetBalanceOutstanding(branch, startdate, enddate, amount);
                Session["dtall"] = dataTable;
                if (dataTable.Rows.Count > 0)
                {
                    GridViewIssue.DataSource = dataTable;
                    GridViewIssue.DataBind();
                    DisplayMessage(".", true);
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("GetIssues", error);
                    DisplayMessage(error, true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            LoadDisplay();
        }
        private void DisplayMessage(string message, Boolean isError)
        {
            lblmsg.Visible = true;
            lblmsg.Text = message;
            if (isError == true)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }

        protected void GridViewIssue_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                string refnumber = GridViewIssue.Rows[index].Cells[0].Text;

            }
        }



        protected void GridViewIssue_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridViewIssue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string status = e.Row.Cells[2].Text;
                
            }
        }
        //protected void excel_Click(object sender, ImageClickEventArgs e)
        //{
        //    //download into excel
        //    ex(Session["dtall"] as DataTable);
        //}

 

        private void ex(DataTable dt)
        {
            // string attachment = "attachment; filename=Incidents.xls";
            string attachment1 = "attachment; ";
            string fileName = "filename=" + "BalanceOutstanding.xls";
            string attachment = attachment1 + fileName;
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
        }

        protected void Imageexcel_Click(object sender, ImageClickEventArgs e)
        {
            ex(Session["dtall"] as DataTable);
        }
    }
}