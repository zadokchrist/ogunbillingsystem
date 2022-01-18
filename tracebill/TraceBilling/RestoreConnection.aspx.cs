using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
namespace TraceBilling
{
    public partial class RestoreConnection : System.Web.UI.Page
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

                    //LoadCountryList();
                    //int countryid = Convert.ToInt16(country_list.SelectedValue.ToString());
                    //LoadAreaList(countryid);
                    LoadApplicationByStatus();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Accessed restore New Connection page");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                string applicationame = "";
                string country = "2";
                string area = "10";
                string status = "14";
                DataTable dataTable = bll.GetApplicationByStatus(applicationame, country, area, status);
                if (dataTable.Rows.Count > 0)
                {
                    gv_surveyjobs.DataSource = dataTable;
                    gv_surveyjobs.DataBind();
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
                authorizeapps.Visible = false;
                connectionapps.Visible = false;
                LoadApplicationByStatus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gv_surveyjobs_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Get the Command Name.
            string commandName = e.CommandName;
            if (commandName == "btnSelect")//details
            {
                //execute
                //int index = e.CommandArgument;
                int index = Int32.Parse((string)e.CommandArgument);
                if (index >= 0)
                {
                    // dispatchdisplay.Visible = true;
                    string appnumber = gv_surveyjobs.Rows[index].Cells[1].Text;

                    maindisplay.Visible = false;
                    btnreturn.Visible = true;
                    authorizeapps.Visible = true;
                    connectionapps.Visible = true;
                    //txtcategory.Text = GridViewIssue.Rows[index].Cells[1].Text;
                    ShowApplicationDetails(appnumber);
                }
            }

        }

        private void ShowApplicationDetails(string appnumber)
        {
            try
            {
                DataTable dt = bll.GetConnectionDetails(appnumber);
                if (dt.Rows.Count > 0)
                {

                    txtappcode.Text = dt.Rows[0]["applicationNumber"].ToString();
                    txtname.Text = dt.Rows[0]["fullName"].ToString();
                    lblApplicationCode.Text = dt.Rows[0]["applicationID"].ToString();
                    int country = Convert.ToInt32(dt.Rows[0]["countryId"].ToString());
                    //txtcountry.Text = dt.Rows[0]["countryName"].ToString();
                    txtarea.Text = dt.Rows[0]["areaName"].ToString();
                    txttype.Text = dt.Rows[0]["typeName"].ToString();
                    txtcategory.Text = dt.Rows[0]["className"].ToString();
                    txtaddress.Text = dt.Rows[0]["physicalAddress"].ToString();
                    txtcontact.Text = dt.Rows[0]["telephone"].ToString();
                    //txtvillage.Text = dt.Rows[0]["village"].ToString();
                    //txtdivision.Text = dt.Rows[0]["division"].ToString();
                    txtoccupation.Text = dt.Rows[0]["ocupation"].ToString();
                    txtidentity.Text = dt.Rows[0]["IdNumber"].ToString();
                    //txtlength.Text = dt.Rows[0]["Pipelength"].ToString();
                    //txtdivision.Text = dt.Rows[0]["excavationlength"].ToString();
                    //txtdiameter.Text = dt.Rows[0]["diameter"].ToString();
                    // txtmaterial.Text = dt.Rows[0]["pipeDesc"].ToString();
                    txtnewcon.Text = dt.Rows[0]["NetAmount"].ToString();
                    txtvat.Text = dt.Rows[0]["Vat"].ToString();
                    double fee = double.Parse(txtnewcon.Text);
                    double vat = double.Parse(txtvat.Text);
                    double total = fee + vat;
                    txttotal.Text = total.ToString("#,00#");
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
        protected void gv_surveyjobs_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        //protected void btnapprove_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        approvecon.Visible = true;
        //        lblaction.Text = "Approve Connection";
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void btnterminate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        approvecon.Visible = true;
        //        lblaction.Text = "Terminate Connection";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //protected void btnhold_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        approvecon.Visible = true;
        //        lblaction.Text = "Connection Put on hold";
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string applicationid = lblApplicationCode.Text;
                string createdby = Session["UserID"].ToString();
                string action = lblaction.Text;
                string comment = txtremark.Text.Trim();
                if (comment.Equals("") || comment.Length < 5)
                {
                    DisplayMessage("Please enter valid comment", true);

                }
                else
                {

                    bll.SaveApplicationComment(applicationid, action, comment, createdby);
                    //log change status
                    int statusid = 0;
                    string output = "";
                    if (action.Contains("Restore"))
                    {
                        statusid = 15;
                        output = "APPLICATION RESTORED BACK TO AUTHORIZATION";
                        bll.LogApplicationTransactions(int.Parse(applicationid), statusid, int.Parse(createdby));
                        //authorization
                        bll.LogApplicationTransactions(int.Parse(applicationid), 10, int.Parse(createdby));

                    }
                    //else if (action.Contains("Terminate"))
                    //{
                    //    statusid = 16;
                    //    output = "Action logged successfully as " + action;
                    //}
                   
                    DisplayMessage(output, false);
                    ClearControls();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearControls()
        {
            try
            {
                approvecon.Visible = false;
                lblaction.Text = ".";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnapprovecancel_Click(object sender, EventArgs e)
        {
            try
            {
                approvecon.Visible = false;
                lblaction.Text = ".";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void rtnAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                approvecon.Visible = true;
                if (rtnAction.SelectedValue == "1")
                {
                    lblaction.Text = "Restore Connection";

                }
                //else if (rtnAction.SelectedValue == "2")
                //{
                //    lblaction.Text = "Put on hold Connection";

                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}