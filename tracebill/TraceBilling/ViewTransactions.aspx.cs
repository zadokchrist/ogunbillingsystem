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
    public partial class ViewTransactions : System.Web.UI.Page
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
                    LoadAllTransactions();
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
        private void LoadAllTransactions()
        {
            try
            {
                string countryid = country_list.SelectedValue.ToString();
                string areaid = area_list.SelectedValue.ToString();
                string startdate = txtstartdate.Text;
                string enddate = txtenddate.Text;
                DataTable dt = bll.GetAllTransactionsByDate(int.Parse(countryid), int.Parse(areaid), startdate,enddate);
                if (dt.Rows.Count > 0)
                {
                    DataGrid1.DataSource = dt;
                    DataGrid1.DataBind();
                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
                }

                bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Transactions page");
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

                LoadAllTransactions();
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

                LoadAllTransactions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        protected void btnreconexport_Click(object sender, EventArgs e)
        {

        }
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {
                //if (e.CommandName == "btnPrint")
                //{
                //    //string ApplicationID = e.Item.Cells[1].Text;
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}