using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class ReadingObj
    {
        private string recordCode, meterRef, propRef, type, comment, method, curreadingtime, custName, address, serialno,
        prereadingtime, reader, period, reason, reasonType, area, branch, reading, readingDate, custRef, classId, estimate, billable, longitude, latitude;
        private int billnumber, curreading, newreading, initialReading, prereading, custclassId, levelid, areaId, branchId, createdby, billedBy, custima_seq, sequenceNo;
        private double recordid, consumption;
        private DateTime curreadingdate, prereadingdate, billedDate;
        private bool estimated, billed, force;

        public string RecordCode
        {
            get {  return recordCode; }
            set { recordCode = value; }
        }
        public string CustName
        {
            get
            {
                return custName;
            }
            set
            {
                custName = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public string MeterNo
        {
            get
            {
                return serialno;
            }
            set
            {
                serialno = value;
            }
        }
        public int InitialReading
        {
            get
            {
                return initialReading;
            }
            set
            {
                initialReading = value;
            }
        }
        public int SequenceNumber
        {
            get
            {
                return sequenceNo;
            }
            set
            {
                sequenceNo = value;
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
        public string Billable
        {
            get
            {
                return billable;
            }
            set
            {
                billable = value;
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
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public string Method
        {
            get
            {
                return method;
            }
            set
            {
                method = value;
            }
        }
        public string Estimate
        {
            get
            {
                return estimate;
            }
            set
            {
                estimate = value;
            }
        }
        public string CurreadingTime
        {
            get
            {
                return curreadingtime;
            }
            set
            {
                curreadingtime = value;
            }
        }
        public string PrereadingTime
        {
            get
            {
                return prereadingtime;
            }
            set
            {
                prereadingtime = value;
            }
        }
        public string Reader
        {
            get
            {
                return reader;
            }
            set
            {
                reader = value;
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
        public string ReasonType
        {
            get
            {
                return reasonType;
            }
            set
            {
                reasonType = value;
            }
        }
        public double RecordID
        {
            get
            {
                return recordid;
            }
            set
            {
                recordid = value;
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
        public int CustimaSequence
        {
            get
            {
                return custima_seq;
            }
            set
            {
                custima_seq = value;
            }
        }
        public string Readings
        {
            get
            {
                return reading;
            }
            set
            {
                reading = value;
            }
        }
        public string ReadingDate
        {
            get
            {
                return readingDate;
            }
            set
            {
                readingDate = value;
            }
        }
        public int CustClassID
        {
            get
            {
                return custclassId;
            }
            set
            {
                custclassId = value;
            }
        }
        public int CurReading
        {
            get
            {
                return curreading;
            }
            set
            {
                curreading = value;
            }
        }
        public int NewReading
        {
            get
            {
                return newreading;
            }
            set
            {
                newreading = value;
            }
        }
        public string CustClass
        {
            get
            {
                return classId;
            }
            set
            {
                classId = value;
            }
        }
        public int PreReading
        {
            get
            {
                return prereading;
            }
            set
            {
                prereading = value;
            }
        }
        public double Consumption
        {
            get
            {
                return consumption;
            }
            set
            {
                consumption = value;
            }
        }
        public int LevelID
        {
            get
            {
                return levelid;
            }
            set
            {
                levelid = value;
            }
        }
        public int AreaID
        {
            get
            {
                return areaId;
            }
            set
            {
                areaId = value;
            }
        }
        public int BranchID
        {
            get
            {
                return branchId;
            }
            set
            {
                branchId = value;
            }
        }
        public string Area
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
        public string Branch
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
        public int BilledBy
        {
            get
            {
                return billedBy;
            }
            set
            {
                billedBy = value;
            }
        }
        public DateTime BilledDate
        {
            get
            {
                return billedDate;
            }
            set
            {
                billedDate = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return createdby;
            }
            set
            {
                createdby = value;
            }
        }
        public DateTime CurReadingDate
        {
            get
            {
                return curreadingdate;
            }
            set
            {
                curreadingdate = value;
            }
        }
        public DateTime PreReadingDate
        {
            get
            {
                return prereadingdate;
            }
            set
            {
                prereadingdate = value;
            }
        }
        public bool Estimated
        {
            get
            {
                return estimated;
            }
            set
            {
                estimated = value;
            }
        }
        public bool Billed
        {
            get
            {
                return billed;
            }
            set
            {
                billed = value;
            }
        }
        public bool Force
        {
            get
            {
                return force;
            }
            set
            {
                force = value;
            }
        }
        public string Longitude
        {
            get
            {
                return longitude;
            }
            set
            {
                longitude = value;
            }
        }
        public string Latitude
        {
            get
            {
                return latitude;
            }
            set
            {
                latitude = value;
            }
        }
    }
}