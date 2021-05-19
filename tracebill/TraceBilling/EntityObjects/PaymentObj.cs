using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class PaymentObj
    {
        private string custref, vendortransref, vendorcode, fullname, paymentdate, amount, createdby, paymentmethod, narration, contact, country, area, chequeno, paymentcode;

        public String CustRef
        {
            get { return custref; }
            set { custref = value; }
        }

        public String VendorTransRef
        {
            get { return vendortransref; }
            set { vendortransref = value; }
        }
        public String VendorCode
        {
            get { return vendorcode; }
            set { vendorcode = value; }
        }
        public String FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }
        public String PaymentMethod
        {
            get { return paymentmethod; }
            set { paymentmethod = value; }
        }
        public String ChequeNumber
        {
            get { return chequeno; }
            set { chequeno = value; }
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
        public String PaymentDate
        {
            get { return paymentdate; }
            set { paymentdate = value; }
        }
        public String Narration
        {
            get { return narration; }
            set { narration = value; }
        }
        public String Contact
        {
            get { return contact; }
            set { contact = value; }
        }
        public String Country
        {
            get { return country; }
            set { country = value; }
        }
        public String Area
        {
            get { return area; }
            set { area = value; }
        }
        public String PaymentCode
        {
            get { return paymentcode; }
            set { paymentcode = value; }
        }

    }
}