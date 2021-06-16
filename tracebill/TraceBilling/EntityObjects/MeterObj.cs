using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class MeterObj
    {
        private string meterRef, propRef, serial, size, newReading, reason, installedBy;
        private int meterLife, meterTypeID, dials, areaID, branchID, createdBy;
        private DateTime manufacturedDate, rdgDate;

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
        public int BranchId
        {
            get
            {
                return branchID;
            }
            set
            {
                branchID = value;
            }
        }
        public int AreaId
        {
            get
            {
                return areaID;
            }
            set
            {
                areaID = value;
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
        public string InstalledBy
        {
            get
            {
                return installedBy;
            }
            set
            {
                installedBy = value;
            }
        }
        public DateTime RdgDate
        {
            get
            {
                return rdgDate;
            }
            set
            {
                rdgDate = value;
            }
        }
        public string NewReading
        {
            get
            {
                return newReading;
            }
            set
            {
                newReading = value;
            }
        }
        public string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
        public int MeterType
        {
            get
            {
                return meterTypeID;
            }
            set
            {
                meterTypeID = value;
            }
        }
        public DateTime ManufactureDate
        {
            get
            {
                return manufacturedDate;
            }
            set
            {
                manufacturedDate = value;
            }
        }
        public int MeterLife
        {
            get
            {
                return meterLife;
            }
            set
            {
                meterLife = value;
            }
        }
        public int Dials
        {
            get
            {
                return dials;
            }
            set
            {
                dials = value;
            }
        }
        public string Serial
        {
            get
            {
                return serial;
            }
            set
            {
                serial = value;
            }
        }
        public string PropRef
        {
            get
            {
                return propRef;
            }
            set
            {
                propRef = value;
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
    }
}