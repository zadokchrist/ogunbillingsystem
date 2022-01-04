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
    public partial class TariffManagement : System.Web.UI.Page
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
                    //LoadCountryList();
                    //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    //LoadAreaList(countryid);
                    LoadDisplay();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private void LoadCountryList()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string countryid = Session["countryId"].ToString();
        //        dt = bll.GetCountryList();
        //        country_list.DataSource = dt;
        //        country_list.SelectedValue = countryid;
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
        //private void LoadAreaList(int countryid)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        dt = bll.GetAreaList(countryid);
        //        area_list.DataSource = dt;

        //        area_list.DataTextField = "areaName";
        //        area_list.DataValueField = "areaId";
        //        area_list.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        string error = "100: " + ex.Message;
        //        bll.Log("DisplayAreaList", error);
        //        DisplayMessage(error, true);
        //    }
        //}
       
    


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
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        //protected void area_list_DataBound(object sender, EventArgs e)
        //{
        //    area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        //}
    
      

      
      

        //protected void country_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //int deptid = int.Parse(department_list.SelectedValue.ToString());
        //        int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
        //        LoadAreaList(countryid);
              
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        private void LoadDisplay()
        {

            
            try
            {
                
                string areaid = "10";
                DataTable dataTable = bll.GetTariffSettings(areaid);
                if (dataTable.Rows.Count > 0)
                {
                    gv_tariffview.DataSource = dataTable;
                    gv_tariffview.DataBind();
                    DisplayMessage(".", true);
                    tariffdisplay.Visible = true;
                    generaltariffdisplay.Visible = false;
                    tariffschedule.Visible = true;
                    generaltariffschedule.Visible = false;
                }
                else
                {
                    //string error = "100: " + "No records found";
                    //bll.Log("GetTariff", error);
                    //DisplayMessage(error, true);
                    //tariffdisplay.Visible = false;
                    dataTable = bll.GetGeneralTariffs();
                    gv_tariffviewgn.DataSource = dataTable;
                    gv_tariffviewgn.DataBind();
                    DisplayMessage(".", true);
                    generaltariffdisplay.Visible = true;
                    tariffdisplay.Visible = false;
                    generaltariffschedule.Visible = true;
                    tariffschedule.Visible = false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            LoadDisplay();
        }


        

        
        //protected void area_list_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        string areaid = area_list.SelectedValue.ToString();
        //        LoadDisplay();
        //        //load session data
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        protected void gv_tariffview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }
        protected void gv_tariffview_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_tariffview_RowCommand(object sender, GridViewCommandEventArgs e)
        {

           
        }
        protected void gv_tariffview_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_tariffview.Rows[index].Cells[1].Text;


            }
        }
        //get general display
        protected void gv_tariffviewgn_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gv_tariffviewgn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gv_tariffviewgn_RowCommand(object sender, GridViewCommandEventArgs e)
        {


        }
        protected void gv_tariffviewgn_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int index = e.NewSelectedIndex;
            if (index >= 0)
            {
                //string refnumber = GridViewIssue.Rows[index].Cells[0].Text;
                string usercode = gv_tariffview.Rows[index].Cells[1].Text;


            }
        }
    }
}