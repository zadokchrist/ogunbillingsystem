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
            Session["areaCode"] = dt.Rows[0]["areaCode"].ToString();
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
    }
}