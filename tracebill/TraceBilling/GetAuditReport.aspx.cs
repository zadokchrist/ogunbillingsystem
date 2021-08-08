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
    public partial class GetAuditReport : System.Web.UI.Page
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
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Audit Report page");
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

        
        private void LoadAuditReport()
        {
            try
            {
                string uname = username.Text;
                string startdate = txtstartdate.Text;
                string enddate = txtenddate.Text;
                DataTable dt= bll.GetAuditReport(uname, startdate, enddate);
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

                bll.RecordAudittrail(Session["userName"].ToString(), "Accessed Retrieved Audit Report");
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

                LoadAuditReport();
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

                LoadAuditReport();
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