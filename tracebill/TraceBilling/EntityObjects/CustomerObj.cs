using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class CustomerObj
    {
        private string applicationno, applicationid, custname, meterref,custref, propertyref, address, email, occupation, contact1, contact2, country, area, title, metermake, meternumber, metersize, latitude, longitude, branch;
        private string block, connectionno, category, tariff, supplystatus,status, createdby, customertype,territory,classification,period;
        private DateTime effectiveDate,billdate;
        private bool isactive,hassewer;

        public String ApplicationNo
        {
            get { return applicationno; }
            set { applicationno = value; }
        }
        public bool IsActive
        {
            get { return isactive; }
            set { isactive = value; }
        }
        public bool HasSewer
        {
            get { return hassewer; }
            set { hassewer = value; }
        }
        public String ApplicationId
        {
            get { return applicationid; }
            set { applicationid = value; }
        }
        public String CustName
        {
            get { return custname; }
            set { custname = value; }
        }
        public String CustRef
        {
            get { return custref; }
            set { custref = value; }
        }
        public String MeterRef
        {
            get { return meterref; }
            set { meterref = value; }
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
        public String PropertyRef
        {
            get { return propertyref; }
            set { propertyref = value; }
        }
        public String Contact1
        {
            get { return contact1; }
            set { contact1 = value; }
        }
        public String Contact2
        {
            get { return contact2; }
            set { contact2 = value; }
        }
        public String Country
        {
            get { return country; }
            set { country = value; }
        }
        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        public String MeterMake
        {
            get { return metermake; }
            set { metermake = value; }
        }
        public String MeterNumber
        {
            get { return meternumber; }
            set { meternumber = value; }
        }
        public String MeterSize
        {
            get { return metersize; }
            set { metersize = value; }
        }
        public String Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }
        public String Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }
        public String ConnectionNumber
        {
            get { return connectionno; }
            set { connectionno = value; }
        }
        public String Block
        {
            get { return block; }
            set { block = value; }
        }
        public String Area
        {
            get { return area; }
            set { area = value; }
        }
        public String Branch
        {
            get { return branch; }
            set { branch = value; }
        }
        public String Category
        {
            get { return category; }
            set { category = value; }
        }
        public String Classification
        {
            get { return classification; }
            set { classification = value; }
        }
        public String Tariff
        {
            get { return tariff; }
            set { tariff = value; }
        }
        public String SupplyStatus
        {
            get { return supplystatus; }
            set { supplystatus = value; }
        }
        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        public String CreatedBy
        {
            get { return createdby; }
            set { createdby = value; }
        }
        public String CustomerType
        {
            get { return customertype; }
            set { customertype = value; }
        }
        public DateTime EffectiveDate
        {
            get { return effectiveDate; }
            set { effectiveDate = value; }
        }
        public String Territory
        {
            get { return territory; }
            set { territory = value; }
        }
        public DateTime BillDate
        {
            get { return billdate; }
            set { billdate = value; }
        }
        public String Period
        {
            get { return period; }
            set { period = value; }
        }

    }
}