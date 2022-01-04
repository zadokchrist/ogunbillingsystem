using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class UserObj
    {
        private string firstname, lastname,othername, designation, contact1,contact2, emailaddress, country, area, createdby, branch, 
            usercode, username, password, role, reason, userid,status,operationarea;
        private bool isactive;
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public string OtherName
        {
            get { return othername; }
            set { othername = value; }
        }
        public string Designation
        {
            get { return designation; }
            set { designation = value; }
        }
        public string Contact1
        {
            get { return contact1; }
            set { contact1 = value; }
        }
        public string Contact2
        {
            get { return contact2; }
            set { contact2 = value; }
        }
        public string EmailAddress
        {
            get { return emailaddress; }
            set { emailaddress = value; }
        }
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        public string Area
        {
            get { return area; }
            set { area = value; }
        }
       
        public string Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        public string CreatedBy
        {
            get { return createdby; }
            set { createdby = value; }
        }
        public bool IsActive
        {
            get { return isactive; }
            set { isactive = value; }
        }
        public string UserCode
        {
            get { return usercode; }
            set { usercode = value; }
        }
        public string UserName
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string Role
        {
            get { return role; }
            set { role = value; }
        }
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        public string UserID
        {
            get { return userid; }
            set { userid = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public string OperationArea
        {
            get { return operationarea; }
            set { operationarea = value; }
        }
    }
}