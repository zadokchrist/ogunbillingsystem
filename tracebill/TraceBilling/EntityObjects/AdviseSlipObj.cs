using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class AdviseSlipObj
    {
        private string applicationno, applicationid, fullname, address, paymentcode,amount,createdby,paymentref,errormsg,contact,countryid,areaid,serial;

        public String ApplicationNo
        {
            get { return applicationno; }
            set { applicationno = value; }
        }
      
        public String ApplicationId
        {
            get { return applicationid; }
            set { applicationid = value; }
        }
        public String FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }
        public String Address
        {
            get { return address; }
            set { address = value; }
        }
        public String PaymentCode
        {
            get { return paymentcode; }
            set { paymentcode = value; }
        }
        public String Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public String CreatedBy
        {
            get { return createdby; }
            set { createdby = value; }
        }
        public String PaymentRef
        {
            get { return paymentref; }
            set { paymentref = value; }
        }
        public String ErrorMessage
        {
            get { return errormsg; }
            set { errormsg = value; }
        }
        public String Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        public String Country
        {
            get { return countryid; }
            set { countryid = value; }
        }
        public String Area
        {
            get { return areaid; }
            set { areaid = value; }
        }
        public String Serial
        {
            get { return serial; }
            set { serial = value; }
        }
    }
}