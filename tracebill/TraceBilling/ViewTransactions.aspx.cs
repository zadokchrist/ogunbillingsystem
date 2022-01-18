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
                    if (Session["roleId"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    //LoadCountryList();
                    //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    //LoadAreaList(countryid);
                    LoadFilters(10);

                    LoadAllTransactions();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Transactions Report page");
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

        }

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
        //protected void country_list_DataBound(object sender, EventArgs e)
        //{
        //    country_list.Items.Insert(0, new ListItem("- - select country - -", "0"));
        //}
        //protected void area_list_DataBound(object sender, EventArgs e)
        //{
        //    area_list.Items.Insert(0, new ListItem("- - select area - -", "0"));
        //}

       
        private void LoadAllTransactions()
        {
            try
            {
                DateTime start = DateTime.Parse(DateTime.Now.ToShortDateString());
                DateTime end = DateTime.Now;



                String from = txtfromdatesrc.Text.Trim();
                String to = txttodatesrc.Text.Trim();

                if (!from.Equals(""))
                {
                    start = DateTime.Parse(from);
                }
                if (!to.Equals(""))
                {
                    end = DateTime.Parse(to);
                }

                if (ddloperationarea.SelectedValue.Equals("0") || ddlbranch.SelectedValue.Equals("0"))
                {
                    start = DateTime.Parse("2020-01-01");
                }
                string countryid = "2";
                string areaid = "10";
                //string startdate = txtstartdate.Text;
                //string enddate = txtenddate.Text;
                string area = ddloperationarea.SelectedValue.ToString();
                string status = "0";
                string search = txtsearch.Text.Trim();
                string branch = ddlbranch.SelectedValue.ToString();
                DataTable dt = bll.GetAllTransactionsByDate(int.Parse(countryid), int.Parse(areaid), start,end);
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
        protected void ddloperationarea_DataBound(object sender, EventArgs e)
        {
            ddloperationarea.Items.Insert(0, new ListItem("all", "0"));
        }
        protected void ddlbranch_DataBound(object sender, EventArgs e)
        {
            ddlbranch.Items.Insert(0, new ListItem("all", "0"));
        }
    }
}