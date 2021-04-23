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

namespace TraceBilling
{
    public partial class ViewApplications : System.Web.UI.Page
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

                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadApplicationByStatus();
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
        
        private void DisplayMessage(string message, Boolean isError)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "MESSAGE: " + message + ".";
            if (isError == true)
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Green;
            }
        }
        protected void country_list_DataBound(object sender, EventArgs e)
        {
            country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        }
        protected void area_list_DataBound(object sender, EventArgs e)
        {
            area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        }
       
        protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //int deptid = int.Parse(department_list.SelectedValue.ToString());
                int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                LoadAreaList(countryid);
                //load session data
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

                LoadApplicationByStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadApplicationByStatus()
        {
            try
            {
                string applicationame = txtapplicationname.Text.Trim();
                string country = country_list.SelectedValue.ToString();
                string area = area_list.SelectedValue.ToString();
                string status = "0";
                DataTable dataTable = bll.GetApplicationByStatus(applicationame, country, area, status);
                if (dataTable.Rows.Count > 0)
                {
                    gv_applicationview.DataSource = dataTable;
                    gv_applicationview.DataBind();
                    DisplayMessage(".", true);
                    maindisplay.Visible = true;
                }
                else
                {
                    string error = "100: " + "No records found";
                    bll.Log("GetApplication", error);
                    DisplayMessage(error, true);
                    maindisplay.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                LoadApplicationByStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void gv_applicationview_OnRowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //Get the Command Name.
        //    string commandName = e.CommandName;

        //    if (commandName == "btnPrint")//routecard
        //    {
        //        string str = "Sorry, Application Foam print out not available yet!!!";
        //        DisplayMessage(str, true);
        //    }

        //}



        //protected void gv_applicationview_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    /*if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        //
        //        // dispatchdisplay.Visible = true;
        //        TableCell link = (TableCell)e.Row.Cells[1];
        //        string status = e.Row.Cells[1].Text;
        //        if ((status.Equals("Emergency")))
        //        {

        //            link.ForeColor = Color.Red;
        //            //e.Row.BackColor = Color.Red;
        //            //e.Row.ForeColor = Color.White;
        //        }
        //        else if ((status.Equals("General")))
        //        {

        //            link.ForeColor = Color.Green;
        //            // e.Row.BackColor = Color.Green;
        //            // e.Row.ForeColor = Color.White;
        //        }
        //        else if ((status.Equals("Others")))
        //        {

        //            link.ForeColor = Color.Blue;
        //            // e.Row.BackColor = Color.Blue;
        //            //e.Row.ForeColor = Color.White;
        //        }
        //    }*/
        //}
        protected void gv_applicationview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //
                // dispatchdisplay.Visible = true;
                TableCell link = (TableCell)e.Row.Cells[2];
                string type = e.Row.Cells[6].Text;
               // e.Row.BackColor = Color.Blue;
                e.Row.ForeColor = Color.Green;

            }
        }
        protected void gv_applicationview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_applicationview_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "RowPrint")
            {
                //string UserID = e.Item.Cells[0].Text;
                string appid = Convert.ToString(e.CommandArgument.ToString());
                string str = "Sorry, Application Foam print out not available yet!!!";
                DisplayMessage(str, true);
            }
            else if (e.CommandName == "RowView")
            {
                string appid = Convert.ToString(e.CommandArgument.ToString());
                //string str = "Sorry, Application Foam print out not available yet!!!";
                // DisplayMessage(returned, true);
                LoadApplicationStatusLogs(appid);
            }
        }
        protected void gv_applicationview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_applicationview.Rows[index].Cells[1].Text;
                

            }
        }
        private void LoadApplicationStatusLogs(string appnumber)
        {
            DataTable dt = new DataTable();
            statuslogdisplay.Visible = true;
            try
            {
                dt = bll.GetApplicationTrackLogs(appnumber);
                gvlogdisplay.DataSource = dt;
                //gvMaterial.CurrentPageIndex = 0;
                gvlogdisplay.DataBind();
                gvlogdisplay.Visible = true;
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("LoadStatusLogDisplay", error);
                DisplayMessage(error, true);
            }
        }

        protected void btnreturn2_Click(object sender, EventArgs e)
        {
            try
            {
                maindisplay.Visible = true;
                btnreturn.Visible = true;
                LoadApplicationByStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnreturn3_Click(object sender, EventArgs e)
        {
            maindisplay.Visible = true;
            statuslogdisplay.Visible = false;
            LoadApplicationByStatus();
        }
    }
}