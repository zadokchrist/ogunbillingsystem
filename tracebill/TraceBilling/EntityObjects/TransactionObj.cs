using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class TransactionObj
    {

        private string documentno, transCode, tariffCode, period, chargeType, vatCode, custRef,
         reason, meterRef, meterSize, rdgType, suppressedCharges, invoicenumber, rdgmethod,transStatus,baltype;
        private double transvalue, vatvalue, unitCost, openbal, rdgRecordId;
        private int area, branch, createdBy, billnumber, classid,zone,country;
        private int basisconsumption;
        private DateTime postdate;
        private bool sewer,  isVatable;

        public string DocumentNo
        {
            get
            {
                return documentno;
            }
            set
            {
                documentno = value;
            }
        }
        public string SuppressedCharges
        {
            get
            {
                return suppressedCharges;
            }
            set
            {
                suppressedCharges = value;
            }
        }
        public string RdgType
        {
            get
            {
                return rdgType;
            }
            set
            {
                rdgType = value;
            }
        }
        public string MeterSize
        {
            get
            {
                return meterSize;
            }
            set
            {
                meterSize = value;
            }
        }
        public string MeterRef
        {
            get
            {
                return meterRef;
            }
            set
            {
                meterRef = value;
            }
        }
       
        public string TransCode
        {
            get
            {
                return transCode;
            }
            set
            {
                transCode = value;
            }
        }
        public string TariffCode
        {
            get
            {
                return tariffCode;
            }
            set
            {
                tariffCode = value;
            }
        }
       
        public string VatCode
        {
            get
            {
                return vatCode;
            }
            set
            {
                vatCode = value;
            }
        }
        public string Period
        {
            get
            {
                return period;
            }
            set
            {
                period = value;
            }
        }
        public string ChargeType
        {
            get
            {
                return chargeType;
            }
            set
            {
                chargeType = value;
            }
        }
       
        public string CustRef
        {
            get
            {
                return custRef;
            }
            set
            {
                custRef = value;
            }
        }
      
        public string Reason
        {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
            }
        }
        /// <summary>
        /// ////////////////////
        /// </summary>
        public double TransValue
        {
            get
            {
                return transvalue;
            }
            set
            {
                transvalue = value;
            }
        }
     
        public double VatValue
        {
            get
            {
                return vatvalue;
            }
            set
            {
                vatvalue = value;
            }
        }
        public double RdgRecordId
        {
            get
            {
                return rdgRecordId;
            }
            set
            {
                rdgRecordId = value;
            }
        }
       
        public double UnitCost
        {
            get
            {
                return unitCost;
            }
            set
            {
                unitCost = value;
            }
        }
        public double OpenBal
        {
            get
            {
                return openbal;
            }
            set
            {
                openbal = value;
            }
        }
        ////////////////////////////////

        public int AreaID
        {
            get
            {
                return area;
            }
            set
            {
                area = value;
            }
        }
        
        public int BranchID
        {
            get
            {
                return branch;
            }
            set
            {
                branch = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }
        public int BillNumber
        {
            get
            {
                return billnumber;
            }
            set
            {
                billnumber = value;
            }
        }
        public int BasisConsumption
        {
            get
            {
                return basisconsumption;
            }
            set
            {
                basisconsumption = value;
            }
        }
        public int ClassID
        {
            get
            {
                return classid;
            }
            set
            {
                classid = value;
            }
        }
        /////////////////////////////////
        public DateTime PostDate
        {
            get
            {
                return postdate;
            }
            set
            {
                postdate = value;
            }
        }


       
        public string InvoiceNumber
        {
            get
            {
                return invoicenumber;
            }
            set
            {
                invoicenumber = value;
            }
        }
        public string ReadingMethod
        {
            get
            {
                return rdgmethod;
            }
            set
            {
                rdgmethod = value;
            }
        }
        public string TransStatus
        {
            get
            {
                return transStatus;
            }
            set
            {
                transStatus = value;
            }
        }
        public bool IsVatable
        {
            get
            {
                return isVatable;
            }
            set
            {
                isVatable = value;
            }
        }
        public bool Sewer
        {
            get
            {
                return sewer;
            }
            set
            {
                sewer = value;
            }
        }
        public string BalType
        {
            get
            {
                return baltype;
            }
            set
            {
                baltype = value;
            }
        }
        public int CountryID
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
    }
}