
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.Drawing;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System;

namespace TraceBilling
{

    public partial class ChangePassword : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ResponseMessage resp = new ResponseMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {

                    if (Session["RoleID"] == null)
                    {
                        Response.Redirect("Default.aspx");
                    }
                    else
                    {
                        string username = Session["UserName"].ToString();
                        lblusername.Text = username;
                        txtusername.Text = lblusername.Text;
                    }
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
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SavePassword();
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        private void ClearControls()
        {
            txtcurrentpwd.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        private void SavePassword()
        {
            string UserCode = Session["UserID"].ToString();
            string uname = Session["UserName"].ToString();
            lbluserid.Text = UserCode;
            string EncryptedPassword = bll.EncryptString(txtNewPassword.Text.Trim());
            //string output = bll.CheckPasswordUsage(Convert.ToInt32(lbluserid.Text.Trim()), EncryptedPassword);
            if (txtNewPassword.Text.Trim() == txtcurrentpwd.Text.Trim())
            {
                DisplayMessage("You can not user the same old password as the new Password. Please re-enter the Password.", true);

            }
            //else if (!output.Equals("Success"))
            //{

            //    DisplayMessage("You cannot Reuse this password,choose another one", true);
            //}

            else
            {

                string Password = txtNewPassword.Text.Trim();
                string Confirm = txtConfirmPassword.Text.Trim();
                string pwdflag = "";
                string returned = bll.ChangeUserPassword(UserCode, Password, Confirm, false);

                if (returned.Contains("Successfully"))
                {
                    string Action = "Password Change";
                    //bll.LogActivity(UserCode, Action);
                    bll.RecordAudittrail(uname,Action);
                    DisplayMessage(returned, false);

                    string userName = lblusername.Text.Trim();
                    string loginpwd = Password;
                    ClearControls();
                    //LoginContinue(userName, loginpwd);
                }
                else
                {
                    DisplayMessage(returned, true);
                }
            }
        }

    }
}