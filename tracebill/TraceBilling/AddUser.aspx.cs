﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;

namespace TraceBilling
{
    public partial class AddUser : System.Web.UI.Page
    {
        public DataTable dt;
        BusinessLogic bll = new BusinessLogic();      
        ResponseMessage resp = new ResponseMessage();
        UserObj user = new UserObj();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (IsPostBack == false)
                {


                    LoadCountryList();
                    int countryid = int.Parse(country_list.SelectedValue.ToString());
                    LoadAreaList(countryid);
                    LoadBranchList(0);
                    LoadRoleList();
                    
                    if (Request.QueryString["transferid"] != null)
                    {
                        string UserCode = Request.QueryString["transferid"].ToString();
                        LoadControls(UserCode);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadControls(string userCode)
        {
            try
            {
                DataTable dt = bll.GetSystemUserByCode(userCode);
                if (dt.Rows.Count > 0)
                {
        
                    lblCode.Text = dt.Rows[0]["userID"].ToString();
                    txtfirstname.Text = dt.Rows[0]["firstName"].ToString();
                    txtlastname.Text = dt.Rows[0]["lastName"].ToString();
                    txtothername.Text = dt.Rows[0]["middleName"].ToString();
                    txtphone.Text = dt.Rows[0]["contactNo1"].ToString();
                    txtphone2.Text = dt.Rows[0]["contactNo2"].ToString();
                    txtemail.Text = dt.Rows[0]["emailAddress"].ToString();
                    txtdesignation.Text = dt.Rows[0]["designation"].ToString();
                    txtusername.Text = dt.Rows[0]["userName"].ToString();
                    string roleid = dt.Rows[0]["roleId"].ToString();
                    string countryid = dt.Rows[0]["countryId"].ToString();
                    string areaid = dt.Rows[0]["area"].ToString();                  
                    bool isactive = bool.Parse(dt.Rows[0]["AccStatus"].ToString());
                    if (isactive)
                    {
                        chkactive.Checked = true;
                    }
                    role_list.SelectedIndex = role_list.Items.IndexOf(role_list.Items.FindByValue(roleid));
                    country_list.SelectedIndex = country_list.Items.IndexOf(country_list.Items.FindByValue(countryid));
                    area_list.SelectedIndex = area_list.Items.IndexOf(area_list.Items.FindByValue(areaid));
                   
                    LoadAreaList(int.Parse(countryid));
                    btnSave.Text = "Update User";
                    lbluser.Text = "EDIT USER DETAILS";
                    btnreturn.Visible = true;
                    reason.InnerText = "Reason to edit user";
                }
            }
            catch(Exception ex)
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
        private void LoadBranchList(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetBranchList(areaid);
                branch_list.DataSource = dt;
                branch_list.DataTextField = "branchName";
                branch_list.DataValueField = "branchId";
                branch_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayBranchList", error);
                DisplayMessage(error, true);
            }
        }
        private void LoadRoleList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = bll.GetRoleList();
                role_list.DataSource = dt;

                role_list.DataTextField = "roleName";
                role_list.DataValueField = "roleId";
                role_list.DataBind();
            }
            catch (Exception ex)
            {
                string error = "100: " + ex.Message;
                bll.Log("DisplayRole", error);
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
        protected void branch_list_DataBound(object sender, EventArgs e)
        {
            branch_list.Items.Insert(0, new ListItem("- - None - -", "0"));
        }
        protected void role_list_DataBound(object sender, EventArgs e)
        {
            role_list.Items.Insert(0, new ListItem("- - select role - -", "0"));
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                user = new UserObj();
                user.CreatedBy = Session["UserID"].ToString();
                user.UserID = lblCode.Text.Trim();
                user.FirstName = txtfirstname.Text.Trim();
                user.LastName = txtlastname.Text.Trim();
                user.OtherName = txtothername.Text.Trim();
                user.UserName = txtusername.Text.Trim();
                user.Contact1 = txtphone.Text.Trim();
                user.Contact2 = txtphone2.Text.Trim();
                user.EmailAddress = txtemail.Text.Trim();
                user.Designation = txtdesignation.Text.Trim();
                user.Country = country_list.SelectedValue.ToString();
                user.Area = area_list.SelectedValue.ToString();
                user.Role = role_list.SelectedValue.ToString();
                user.Branch = branch_list.SelectedValue.ToString();
                
                user.IsActive = chkactive.Checked;
                user.Reason = txtreason.Text.Trim();
                //validate input
                resp = bll.ValidateUser(user.FirstName, user.LastName, user.UserName, user.Reason);
                if (resp.Response_Code.ToString().Equals("0"))
                {
                    //user.UserName = bll.MakeUserName(user.FirstName, user.LastName);

                    user.Password = bll.EncryptString(user.UserName);

                    resp = bll.SaveSystemUser(user);
                    if (resp.Response_Code == "0")//save
                    {
                        //log activity
                        user.Reason = "ADD:" + user.Reason;
                        bll.LogUserActivity(user);
                        string str = " with System user(" + user.UserName + ") created";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else if (resp.Response_Code == "1")//edit and update
                    {
                        //log activity
                        user.Reason = "EDIT:" + user.Reason;
                        bll.LogUserActivity(user);
                        string str = " with System user(" + user.UserName + ") details updated";
                        string res = resp.Response_Message + str;
                        DisplayMessage(res, false);
                    }
                    else
                    {
                        DisplayMessage(resp.Response_Message, true);
                    }
                }
                else
                {
                    DisplayMessage(resp.Response_Message, true);
                }
                RefreshControls();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void RefreshControls()
        {
            txtfirstname.Text = "";
            txtlastname.Text = "";
            txtphone.Text = "";
            txtemail.Text = "";
            txtdesignation.Text = "";
            txtreason.Text = "";
            txtusername.Text = "";
            country_list.SelectedValue = "0";
            area_list.SelectedValue = "0";
            role_list.SelectedValue = "0";
            branch_list.SelectedValue = "0";            
            chkactive.Checked = false;
        }
        protected void chkuserChanged(object sender, EventArgs e)
        {
            if (chkuser.Checked)
            {
                txtusername.ReadOnly = false;
            }
            else
            {
                txtusername.ReadOnly = true;
            }

        }
        protected void txtlastname_TextChanged(object sender, EventArgs e)
        {
            string firstname = txtfirstname.Text.Trim();
            string lastname = txtlastname.Text.Trim();

            if (!firstname.Equals("") || !lastname.Equals(""))
            {
                //generate username
                string username = bll.MakeUserName(firstname, lastname);
                txtusername.Text = username;
            }
            else
            {
                txtusername.Text = "";
                string error = "100: " + "Either Firstname or lastname cannot be empty";
                bll.Log("CaptureUserDetails", error);
                DisplayMessage(error, true);
            }
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            string StartPage = "ViewUsers.aspx";
            Response.Redirect(StartPage, true);
        }
    }
}