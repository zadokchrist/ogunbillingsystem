using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class ApplicationObj
    {
        private string applicationno, applicationid, firstname, othername,lastname,address,email,occupation,workplace,telephone,country,state,constituency,city,village,zipcode,division,plotnumber,street;
        private string idnumber, idtype, connectiontype, optionid, serviceid, categoryid, statusid, capturedby,customertype,areaid,branchid,territory,subterritory;
        private DateTime applicationDate;
        private bool cancelled;

        public String ApplicationNo
        {
            get { return applicationno; }
            set { applicationno = value; }
        }
        public bool Cancelled
        {
            get { return cancelled; }
            set { cancelled = value; }
        }
        public String ApplicationId
        {
            get { return applicationid; }
            set { applicationid = value; }
        }
        public String FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public String OtherName
        {
            get { return othername; }
            set { othername = value; }
        }
        public String LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        public String Email
        {
            get { return email; }
            set { email = value; }
        }
        public String Occupation
        {
            get { return occupation; }
            set { occupation = value; }
        }
        public String WorkPlace
        {
            get { return workplace; }
            set { workplace = value; }
        }
        public String Telephone
        {
            get { return telephone; }
            set { telephone = value; }
        }
        public String Country
        {
            get { return country; }
            set { country = value; }
        }
        public String State
        {
            get { return state; }
            set { state = value; }
        }
        public String Constituency
        {
            get { return constituency; }
            set { constituency = value; }
        }
        public String City
        {
            get { return city; }
            set { city = value; }
        }
        public String Village
        {
            get { return village; }
            set { village = value; }
        }
        public String ZipCode
        {
            get { return zipcode; }
            set { zipcode = value; }
        }
        public String Division
        {
            get { return division; }
            set { division = value; }
        }
        public String PlotNumber
        {
            get { return plotnumber; }
            set { plotnumber = value; }
        }
        public String IdNumber
        {
            get { return idnumber; }
            set { idnumber = value; }
        }
        public String IdType
        {
            get { return idtype; }
            set { idtype = value; }
        }
        public String ConnectionType
        {
            get { return connectiontype; }
            set { connectiontype = value; }
        }
        public String OptionId
        {
            get { return optionid; }
            set { optionid = value; }
        }
        public String ServiceId
        {
            get { return serviceid; }
            set { serviceid = value; }
        }
        public String CategoryId
        {
            get { return categoryid; }
            set { categoryid = value; }
        }
        public String StatusId
        {
            get { return statusid; }
            set { statusid = value; }
        }
        public String CapturedBy
        {
            get { return capturedby; }
            set { capturedby = value; }
        }
        public String CustomerType
        {
            get { return customertype; }
            set { customertype = value; }
        }
        public DateTime ApplicationDate
        {
            get { return applicationDate; }
            set { applicationDate = value; }
        }
        public String Street
        {
            get { return street; }
            set { street = value; }
        }
        public String Area
        {
            get { return areaid; }
            set { areaid = value; }
        }
        public String Branch
        {
            get { return branchid; }
            set { branchid = value; }
        }
        public String Territory
        {
            get { return territory; }
            set { territory = value; }
        }
        public String SubTerritory
        {
            get { return subterritory; }
            set { subterritory = value; }
        }

    }
}