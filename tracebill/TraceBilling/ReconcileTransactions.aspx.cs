using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.Collections;
using System.Drawing;
namespace TraceBilling
{
    public partial class ReconcileTransactions : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ApplicationObj app = new ApplicationObj();
        ResponseMessage resp = new ResponseMessage();
        ArrayList al = new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {

                    LoadCountryList();
                    int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadPaymentTransactions();
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
        private void LoadPaymentTransactions()
        {
            try
            {
                string countryid = country_list.SelectedValue.ToString();
                string areaid = area_list.SelectedValue.ToString();
                DataTable dt = bll.GetPaymentTransactions(int.Parse(countryid), int.Parse(areaid));
                if (dt.Rows.Count > 0)
                {
                    Session["TransactionDT"] = dt;
                    DataGrid1.DataSource = dt;
                    DataGrid1.DataBind();
                }
                else
                {
                    string str = "No records found.";
                    DisplayMessage(str, true);
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

                LoadPaymentTransactions();
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

                LoadPaymentTransactions();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnreconcile_Click(object sender, EventArgs e)
        {
            try
            {
                //new implementation
                string str_todump = GetRecordsToReconcile();
                string str = "";
                if (str_todump.Equals(""))
                {
                    str = "Please Select transaction record to reconcile";
                    DisplayMessage(str, true);
                }
                else
                {
                    UpdateReconcileTransactions(str_todump);
                    if (al.Count > 0)
                    {

                        DisplayMessage(al.Count.ToString() + " transaction records reconciled successfully.", true);
                    }
                }
               
                LoadPaymentTransactions();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private string GetRecordsToReconcile()
        {
            int Count = 0;
            string ItemArr = "";
            al = new ArrayList();

            foreach (DataGridItem Items in DataGrid1.Items)
            {
                CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
                if (chk.Checked)
                {
                    Count++;
                    string Item1 = Items.Cells[1].Text;//id
                    string Item2 = Items.Cells[2].Text;//custref
                    string Item3 = Items.Cells[6].Text;//amount
                    string Item4 = Items.Cells[8].Text;//vendorref
                    string Item5 = Items.Cells[7].Text;//paydate
                    string Item6 = Items.Cells[11].Text;//reconstatus
                    ItemArr += Item1 + "+" + Item2 + "+" + Item3 + "+" + Item6 + ",";
                    //add to uploaded arraylist
                    al.Add(Count);
                }
            }
            return ItemArr;
        }

        private void UpdateReconcileTransactions(string str_recon)
        {
            try
            {
                string reconciledBy = Session["userName"].ToString();
                DateTime reconciledDate = DateTime.Now;
               // ArrayList al = new ArrayList();
                string method = "MANUAL";
                string str = str_recon.TrimEnd(',');
                string[] arr = str.Split(',');
                //string createdby = Session["userId"].ToString();
                bool isreconciled = false;
                foreach (string s in arr)
                {
                    string[] separators = { "+" };
                    string[] param = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    string recordid = param[0].Trim();
                    string custref = param[1].Trim();
                    string amount = param[2].Trim();
                    string reconstatus = param[3].Trim();
                    //save to Db
                    if (reconstatus.Equals("NO"))
                    {
                        isreconciled = true;
                        bll.UpdateTransactionAsReconciled(isreconciled, int.Parse(recordid), float.Parse(amount), custref, reconciledDate, reconciledBy, method);
                       // al.Add(s);
                    }
                   

                }
             


            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void btnreconcancel_Click(object sender, EventArgs e)
        {

        }

        protected void btnreconexport_Click(object sender, EventArgs e)
        {

        }
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*protected void chkTransactions_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkTransactions.Visible = true;
                btnreconcile.Visible = true;
                SelectAllTransactions(chkTransactions.Checked);
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                DisplayMessage(err, true);
            }
        }
        private void SelectAllTransactions(bool controlchecked)
        {
            DataTable dt = (DataTable)Session["TransactionDT"];

            if (controlchecked == true)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["IsReconciled"] = "True";
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dr["IsReconciled"] = "False";
                }
            }

            DataGrid1.DataSource = dt;
            DataGrid1.DataBind();
        }*/
        protected void chkTransactions_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SelectAllItems();
                if (chkTransactions.Checked == true)
                {
                    chkTransactions.Checked = true;
                }
                else
                {
                    chkTransactions.Checked = false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                DisplayMessage(err, true);
            }
        }
        private void SelectAllItems()
        {
            foreach (DataGridItem Items in DataGrid1.Items)
            {
                CheckBox chk = ((CheckBox)(Items.FindControl("CheckBox1")));
                if (chk.Checked)
                {
                    chk.Checked = false;
                }
                else
                {
                    chk.Checked = true;
                }
            }
        }
    }
}