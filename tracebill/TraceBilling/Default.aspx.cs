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
    public partial class Default : System.Web.UI.Page
    {
        
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();
        ResponseMessage resp = new ResponseMessage();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["roleId"] == null)
            //{
            //    Response.Redirect("Default.aspx");
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                new Exception("Unable to connect the database");
                this.GetuserAccess();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                if (err.Contains("network"))
                {
                    err = "503: Remote Server error encountered at system access";
                }
                DisplayMessage(err, true);
                
            }
        }

        private void GetuserAccess()
        {
            string error = "";
            try
            {
                string username = txtusername.Text.Trim();
                string password = txtpassword.Text.Trim();
                string encrypted_password = bll.EncryptString(password);
                resp = bll.ValidateLogin(username, encrypted_password);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    dt = bll.GetSystemUser(username, encrypted_password);
                    CreateSession(dt);
                    //Response.Redirect("Inquiry.aspx", false);
                    string StartPage = Session["StartPage"].ToString();
                    bll.RecordAudittrail(Session["userName"].ToString(), "Logged Into the system");
                    Redirection(StartPage);
                }
                else
                {
                    error = resp.Response_Code + " " + resp.Response_Message;
                    bll.Log("GetUserAccess", error);
                    DisplayMessage(error, true);
                }

            }
            catch (Exception ex)
            {
                error = "100: " + ex.Message;
                bll.Log("User Access Failure", error);
                DisplayMessage(error, true);

            }
        }
        private void Redirection(string StartPage)
        {
            Response.Redirect(StartPage, true);
            // Server.Transfer(StartPage);
            //ShowMessage("Reached");
        }

        private void CreateSession(DataTable dt)
        {

            Session["userId"] = dt.Rows[0]["userId"].ToString();
            Session["userName"] = dt.Rows[0]["userName"].ToString();
            Session["FullName"] = dt.Rows[0]["FullName"].ToString();
            Session["designation"] = dt.Rows[0]["designation"].ToString();
            Session["emailAddress"] = dt.Rows[0]["emailAddress"].ToString();
            Session["roleName"] = dt.Rows[0]["roleName"].ToString();
           
            Session["roleId"] = dt.Rows[0]["roleId"].ToString();
           
            Session["startPage"] = dt.Rows[0]["startPage"].ToString();
            Session["country"] = dt.Rows[0]["countryName"].ToString();
            Session["countryId"] = dt.Rows[0]["countryId"].ToString();
            Session["area"] = dt.Rows[0]["areaName"].ToString();
            Session["areaId"] = dt.Rows[0]["areaId"].ToString();
            Session["branchId"] = dt.Rows[0]["branchId"].ToString();
            Session["branchName"] = dt.Rows[0]["branchName"].ToString();
            Session["areaCode"] = dt.Rows[0]["areaCode"].ToString();
            Session["operationId"] = dt.Rows[0]["operationId"].ToString();
            Session["operationAreaName"] = dt.Rows[0]["operationAreaName"].ToString();

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
        

        //added 24/10/2021 to enable pwd reset
        protected void rbnAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            string action = "";
            if (rbnAction.SelectedValue.ToString() == "0")
            {
                action = "ENABLE";
            }
            else
            {
                action = "RESET";
            }
        }
        protected void BtnForgotSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                //send mail to admins
                string username = txtforgetusername.Text.Trim();
                string action = rbnAction.SelectedItem.ToString();
                string actionvalue = rbnAction.SelectedValue.ToString();
                string fname = txtFullName.Text;
                string useremail = txtEmail.Text.Trim();
                string usercode = lbluserid.Text;
                if (username.Equals(""))
                {
                    DisplayMessage("Please Enter System Username.", true);
                }
                else
                {
                    string msg = "";
                    string output = "";
                    if (actionvalue.Equals("1"))//reset
                    {
                        //send mail to user with generated pwd
                        string randompwd = bll.GenerateRandomPassword();
                        msg = "Hello " + fname + " , your request to " + action + " against account has been noted." + "\r\n" +
                            "Please use the assigned temporary user credentials as; Username: " + username + " and Password: " + randompwd + "\r\n" +
                            "to log into Ogun Billing and then change to prefered password.";
                        // bll.callPasswordMailer(fname, useremail, msg);
                        output = bll.PasswordResponse(fname, useremail, msg, username);
                        if (output.Contains("success"))
                        {

                            msg = "Request Email notification successfully sent to " + fname + " for further action.";

                        }
                        else
                        {
                            msg = output;
                        }
                        DisplayMessage(msg, false);
                        RefreshControls();
                        //log request logs

                        bll.LogActivity(username, action);
                        //log generated pwds...username,userid,Randompwd
                        string createdby = usercode;
                        bll.LogRandomDetails(username, randompwd, action, createdby, usercode);

                    }
                    else
                    {
                        DataTable dt = bll.GetSystemUserByRole("10","1");//get system admins
                        if (dt.Rows.Count > 0)
                        {

                            foreach (DataRow dr in dt.Rows)
                            {
                                string user_code = dr["UserID"].ToString();
                                string fullName = dr["fullname"].ToString();
                                string email = dr["EmailAddress"].ToString();

                                msg = "Hello " + fullName + " , system request to " + action + " against user( " + fname + ") has been sent to you." + "\r\n";
                               output = bll.PasswordResponse(fullName, email, msg, username);

                            }
                            //log request logs
                            if(output.Contains("success"))
                            {
                                bll.LogActivity(username, action);

                                msg = "Email notification successfully sent to IT Systems Administration for further action.";
                            }
                           else
                            {
                                msg = output;
                            }

                            DisplayMessage(msg, false);
                            RefreshControls();

                        }
                        else
                        {
                            DisplayMessage("No system Admins available.", true);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RefreshControls()
        {
            txtforgetusername.Text = "";
            txtFullName.Text = "";
            txtEmail.Text = "";
        }

        protected void btnCancelSubmit_Click(object sender, EventArgs e)
        {
            changedisplay.Visible = false;
            logindisplay.Visible = true;
            forgotpwddisplay.Visible = false;
            DisplayMessage("", true);
        }
        protected void txtforgetusername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string username = txtforgetusername.Text.Trim();
                DataTable dt = bll.CheckUserDetails(username);

                if (dt.Rows.Count > 0)
                {
                    txtFullName.Text = dt.Rows[0]["fullname"].ToString();
                    txtEmail.Text = dt.Rows[0]["EmailAddress"].ToString();
                    lbluserid.Text = dt.Rows[0]["UserID"].ToString();
                    DisplayMessage(".", true);
                }
                else
                {
                    txtFullName.Text = "";
                    txtEmail.Text = "";
                    //cboAreas.SelectedIndex = cboAreas.Items.IndexOf(cboAreas.Items.FindByValue("0"));
                    DisplayMessage("Username specified is unknown to system.", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void linkGoSomewhere_Click(object sender, EventArgs e)
        {
            try
            {
                changedisplay.Visible = false;
                logindisplay.Visible = false;
                forgotpwddisplay.Visible = true;
                rbnAction.Items[0].Selected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            changedisplay.Visible = false;
            logindisplay.Visible = true;
            forgotpwddisplay.Visible = false;
            DisplayMessage("", true);
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
        private void SavePassword()
        {
            string EncryptedPassword = bll.EncryptString(txtNewPassword.Text.Trim());
            string output = bll.CheckPasswordUsage(Convert.ToInt32(lbluserid.Text.Trim()), EncryptedPassword);
            if (txtNewPassword.Text.Trim() == lblusername.Text.Trim())
            {
                DisplayMessage("You can not user the same Username as the Password. Please re-enter the Password.", true);

            }
            else if (!output.Equals("Success"))
            {

                DisplayMessage("You cannot Reuse this password,choose another one", true);
            }

            else
            {
                string UserCode = lbluserid.Text.Trim();
                string Password = txtNewPassword.Text.Trim();
                string Confirm = txtConfirmPassword.Text.Trim();
                // string pwdflag = "";
                string returned = bll.ChangeUserPassword(UserCode, Password, Confirm, false);

                if (returned.Contains("Successfully"))
                {
                    string Action = "Password Change";
                    bll.LogActivity(UserCode, Action);
                    txtpassword.Focus();
                    DisplayMessage(returned, false);

                    string userName = lblusername.Text.Trim();
                    string loginpwd = Password;

                    LoginContinue(userName, loginpwd);
                }
                else
                {
                    DisplayMessage(returned, true);
                }
            }
        }

        private void LoginContinue(string username, string loginpwd)
        {
            string error = "";
            string encrypted_password = bll.EncryptString(loginpwd);
            resp = bll.ValidateLogin(username, encrypted_password);
            if (resp.Response_Code.ToString().Equals("0"))
            {

                dt = bll.GetSystemUser(username, encrypted_password);
                if (dt.Rows.Count > 0)
                {
                    CreateSession(dt);
                    //Response.Redirect("Inquiry.aspx", false);
                    string StartPage = Session["StartPage"].ToString();
                    Redirection(StartPage);
                }
                else
                {
                    error = "106:" + " " + "No session details for user-" + username;
                    bll.Log("GetUserAccess", error);
                    DisplayMessage(error, true);
                }

            }
            else
            {
                error = resp.Response_Code + " " + resp.Response_Message;
                bll.Log("GetUserAccessLogin", error);
                DisplayMessage(error, true);
            }
        }
    }
}