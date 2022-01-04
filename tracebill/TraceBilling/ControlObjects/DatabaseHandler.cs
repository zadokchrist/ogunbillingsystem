using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using TraceBilling.EntityObjects;

namespace TraceBilling.ControlObjects
{
    public class DatabaseHandler
    {
        private Database DatabaseConnector;
        private DbCommand command;
        DataTable dt = null;



        public DatabaseHandler()
        {
            try
            {
                DatabaseConnector = DatabaseFactory.CreateDatabase("DatabaseConnectionString");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        internal void ExecuteCommand(string procedure, params object[] data)
        {
            try
            {
                command = DatabaseConnector.GetStoredProcCommand(procedure, data);
                DatabaseConnector.ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        internal DataTable ExecuteDataSet(string procedure, params object[] data)
        {
            try
            {
                dt = new DataTable();
                command = DatabaseConnector.GetStoredProcCommand(procedure, data);
                dt = DatabaseConnector.ExecuteDataSet(command).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal void Log(String method_name, String error_message)
        {
            try
            {
                ExecuteCommand("Sp_LogError", method_name, error_message);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        internal DataTable GetRequirementList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetRequirementList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCustomerTypeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustomerType");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCustomerClass()
        {

            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustomerClass");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCountryList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCountryList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

       

        internal DataTable GetUserList(int areaid,int roleid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetUserList", areaid,roleid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetAreaList(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAreaList", countryid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetAreaSessions(int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAreaSessions", areaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable SaveApplication(ApplicationObj app)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveApplication", app.ApplicationId,app.ApplicationNo,app.FirstName, app.LastName,app.OtherName, app.Address, app.Email,
    app.Occupation, app.WorkPlace, app.Telephone, app.Country, app.State, app.Constituency, app.City,
   app.Street, app.Village, app.ZipCode, app.Division, app.IdNumber, app.IdType, app.CustomerType,
    app.ConnectionType, app.OptionId, app.ServiceId, app.CategoryId, app.StatusId, app.CapturedBy, app.ApplicationDate,app.Area,app.Branch,app.OperationId,app.Territory,app.SubTerritory);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

     

        internal DataTable GetSystemUser(string username, string encrypted_password)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetSystemUser", username, encrypted_password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal int GetSerialNumber(int countrycode, int areacode, int branchcode, string serialCode, string userCode)
        {
            int output = 0;
            try
            {
                int UserID = Convert.ToInt32(userCode);
                
                dt = ExecuteDataSet("Sp_GetSerialNumber", countrycode, areacode,branchcode,serialCode,userCode);
                if (dt.Rows.Count > 0)
                {
                    output = Convert.ToInt16(dt.Rows[0]["Number"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        internal string GetCodeIdentity(string value, int flag)
        {
            string output = "0";
            try
            {
                
                dt = ExecuteDataSet("Sp_GetCodeIdentity", int.Parse(value), flag);
                if (dt.Rows.Count > 0)
                {
                    output = dt.Rows[0]["code"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        internal DataTable GetApplicationByStatus(string applicationame, string country, string area, string status)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetApplicationByStatus",  int.Parse(country),int.Parse(area), applicationame, int.Parse(status));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetApplicationByIDForPayment(string appid) 
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetApplicationByIDForPayment", appid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetApplicationDetails(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetDetailsByAppNumber", appnumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void AssignSurveyor(int applicationID, int surveyorID)
        {
            try
            {
                ExecuteCommand("Sp_AssignSurveyor", applicationID, surveyorID);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable SaveUser(UserObj user)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveUser", user.UserCode, user.UserName, user.Password, user.FirstName, user.LastName,user.OtherName, user.Designation, user.Contact1,user.Contact2, user.EmailAddress,
                    user.Role, user.Country, user.Area,user.Branch, user.IsActive, user.CreatedBy, user.UserID,user.OperationArea,user.Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable LogUserActivity(UserObj user)
        {
            DataTable dt = new DataTable();
            try
            {
             
                 string fullname = user.FirstName + " " + user.LastName;
                dt = ExecuteDataSet("Sp_SaveUserAuditLog", user.UserID, user.UserName, fullname, user.Designation, user.Contact1, user.EmailAddress,
                    user.Role, user.Country, user.Area,  user.IsActive, user.Branch, user.CreatedBy, user.Reason);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetAllUsers(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAllSystemUsers", countryid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal void ChangeUserAccess(int userID, bool isActive, string userName, string loggedIn, string action)
        {
            try
            {
                ExecuteCommand("Sp_ChangeUserAccess", userID, userName, isActive, loggedIn, action);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetRoleList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetRoleList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetSystemUserByCode(string userCode)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_GetSystemUserByCode", userCode);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetBranchList(int areaid, int operationid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBranchList", areaid,operationid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void SaveSurveyJobNumber(int appID, string jobNumber, int countryid, int areaid, int branchid, int createdBy)
        {
            try
            {
                ExecuteCommand("Sp_SaveSurveyJob", countryid, areaid, branchid, jobNumber, appID,createdBy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //added 03/12/2020
        internal DataTable GetSurveyQnList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetSurveyQnList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetSurveyReportDetails(string jobnumber, int countryid,int areaid,int status)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetSurveyReportDetails", jobnumber,countryid,areaid,status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void DeactivateAccount(string custref, string reason, string recordedby,string crmreference)
        {
            ExecuteCommand("sp_CloseAccount", custref, reason, recordedby, crmreference);
        }

        internal void ReactivateAccount(string custref, string reason, string recordedby,string crmreference)
        {
            ExecuteCommand("sp_ReactivateAccount", custref, reason, recordedby, crmreference);
        }

        internal void LogApplicationTransactions(int appID, int status, int createdBy)
        {
            try
            {
                ExecuteCommand("Sp_LogApplicationTransactions", appID, status, createdBy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void SaveSurveyDetails(string appid, string qnid, string ans,string createdby, DateTime surveydate)
        {
            try
            {
                ExecuteCommand("Sp_SaveSurveyDetails", int.Parse(appid), int.Parse(qnid), ans,int.Parse(createdby),surveydate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable SaveFieldConnection(string conid, string appid, string jobno,string customertype, string category, string authorizedby, DateTime connectiondate, DateTime instructiondate, string createdby, string areaid)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveFieldConnection", int.Parse(conid),int.Parse(appid),jobno,int.Parse(customertype),int.Parse(category),authorizedby,connectiondate,instructiondate,int.Parse(createdby),int.Parse(areaid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetMaterialOptions(string type)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetMaterialOptions", type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetCostMaterials(int applicationID, int categoryId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAppConnectionMaterials", applicationID,categoryId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetCostingItems(int applicationID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCostingItems", applicationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetMaterialDetails(int itemID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetMaterialByID", itemID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void SaveCostingDetails(int costID, int applicationID, int itemID, double quantity, double amount, string size, string length, int createdBy)
        {
            try
            {
              
                ExecuteCommand("Sp_SaveCostingDetails", costID,applicationID,itemID,size,length,quantity,amount,createdBy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable GetFieldCustomerDetails(string appid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetFieldCustomerDetails", int.Parse(appid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetPipeDiameter()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetPipeDiameterList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetPipeType()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetPipeTypeList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable SaveFieldEstimates(string estimateid, string appid, string diameterid, string pipetypeid, string pipelength, string excavationlength, string createdby)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveFieldEstimates", int.Parse(estimateid), int.Parse(appid), int.Parse(diameterid), int.Parse(pipetypeid), pipelength, excavationlength, int.Parse(createdby));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetInvoiceDetails(string appnumber, int countryid, int areaid, int status)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetInvoiceDetails", appnumber, countryid, areaid, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetNonConsumptionInvoiceDetails(string paymentref)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("GetNonConsumptionInvoiceByPaymentRef", paymentref);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetConnectionDetails(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetConnectionDetailsByAppNumber", appnumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void SaveApplicationComment(string appid, string action, string comment, string createdby)
        {
            try
            {
                ExecuteCommand("Sp_SaveApplicationComment", int.Parse(appid), action,comment,int.Parse(createdby));

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal string GetpaymentRef(AdviseSlipObj slip)
        {
            string output = "0";
            try
            {
             
                command = DatabaseConnector.GetStoredProcCommand("Sp_SaveNonConsumptionInvoice", slip.ApplicationId, slip.PaymentRef, slip.PaymentCode,slip.FullName,slip.Amount,slip.Contact,
                    slip.Address,slip.Country,slip.Area,slip.Serial,slip.CreatedBy);
                output = DatabaseConnector.ExecuteDataSet(command).Tables[0].Rows[0][0].ToString();
            }
            catch (Exception ex)
            {
                //throw ex;
                string err = ex.Message;
                output = err;
                //log error
            }
            return output;
        }

        internal DataTable CheckPaymentRefGenerated(string paymentCode, string applicationId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_CheckPaymentRefExistence", paymentCode,applicationId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetInvoiceDetailsByAppNumber(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetInvoiceDetailsByAppNumber", appnumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetPaymentTransactions(int countryid,int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetPaymentTransactions",countryid,areaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void UpdateTransactionAsReconciled(bool Reconciled, int RecordID, float amountpaid1, string CustRef,
        DateTime reconciledDate, string username, string method)
        {
            try
            {
              
                ExecuteCommand("Sp_UpdateTransactionAsReconciled", Reconciled, RecordID, amountpaid1, CustRef, reconciledDate, username, method);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        internal DataTable GetAllTransactionsByDate(int countryid, int areaid,string startdate,string enddate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAllTransactionsByDate", countryid, areaid,startdate,enddate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetAllTransactions(int countryid, int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAllTransactions", countryid, areaid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        internal DataTable GetRouteFile(string country, string area, string branch, string book, string walk)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetRouteFile", int.Parse(country), int.Parse(area),int.Parse(branch),book,walk);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void RecordAudittrail(string username,string actiontaken) 
        {
            try
            {
                ExecuteCommand("Sp_InsertAudittrail", username, actiontaken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetVendorList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetVendorList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable CheckCustomerDetails(string custref)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_CheckCustomerDetails", custref);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable SavePaymentTransaction(PaymentObj trans)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_SavePaymentTransaction", trans.CustRef, trans.Contact, trans.PayDate, trans.Amount, trans.PaymentMethod,
                    trans.VendorTransRef, trans.VendorCode, trans.PaymentCode, trans.Narration, trans.CreatedBy);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void LogTransactionError(string vendorCode, string strerror, string custRef, string area, string vendorTransRef, string amount)
        {
            try
            {

                ExecuteCommand("Sp_LogTransactionError",vendorCode,strerror,custRef,area,vendorTransRef,amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable VerifyVendorTransRef(PaymentObj trans)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_VerifyVendorTransRef", trans.VendorCode, trans.VendorTransRef, trans.Amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetMeterTypeList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetMeterTypeList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCurrencyList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCurrencyList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void SaveCountryDetails(string code,string countryname, string countrycode, string vat, string currency, string createdby, bool isactive)
        {
            try
            {
                ExecuteCommand("Sp_SaveCountryDetails", code,countryname,countrycode,float.Parse(vat),int.Parse(currency),int.Parse(createdby), isactive);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetCountrySettings()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCountrySettings");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetCountrySettingByID(string countryId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCountrySettingsByID", int.Parse(countryId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal void SaveExpenditureDetails(int costID, int applicationID, int itemID, double quantity, double amount, string size, string length, int createdBy)
        {
            try
            {

                ExecuteCommand("Sp_SaveExpenditureDetails", costID, applicationID, itemID, size, length, quantity, amount, createdBy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetExpenseItems(int applicationID)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetExpenseItems", applicationID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable SaveFieldExpenseLogs(string estimateid, string appid, string diameterid, string pipetypeid, string pipelength, string excavationlength, string createdby, string comment)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveFieldExpenseLogs", int.Parse(estimateid), int.Parse(appid), int.Parse(diameterid), int.Parse(pipetypeid), pipelength, excavationlength, int.Parse(createdby),comment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetBlockMaps(string areaid, string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBlockMaps", int.Parse(areaid),int.Parse(branchid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetBlockConnectionNumber(string areaid, string branchid,string blockno)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBlockConnectionNumber", int.Parse(areaid), int.Parse(branchid),blockno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable SaveFieldDocket(string recordCode, string applicationid, string pipediameter, string metertype, string meterref, string meternumber, string createdby, string remark,
            string longitude, string latitude, string reading, string dials, string meterlife, DateTime manufacturedate, DateTime installdate, string blocknumber, string connectionno,string installedby)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveFieldDocket", int.Parse(recordCode), int.Parse(applicationid), int.Parse(pipediameter), int.Parse(metertype), meterref, meternumber, int.Parse(createdby), remark,
                    longitude,latitude,reading,dials,meterlife,manufacturedate,installdate,blocknumber,connectionno,installedby);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetFieldDocketByApplication(int appid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetFieldCustomerDetails", appid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetNewConnectionCustomerDetails(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetNewConnectionCustomerDetails", appnumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetTariff(string classid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTariff", int.Parse(classid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCustomerCategory()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustomerCategory");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable SaveCustomerDetails(CustomerObj cust)
        {
            DataTable dt = new DataTable();
            try
            {
//               
                dt = ExecuteDataSet("Sp_SaveCustomerDetails", cust.CustRef,cust.PropertyRef,cust.MeterRef,cust.ApplicationId,cust.CustName,cust.Title,
                    cust.Occupation,cust.Contact1,cust.Contact2,cust.Email,cust.Address,cust.Territory,int.Parse(cust.MeterMake),cust.MeterNumber,int.Parse(cust.MeterSize),
                    cust.Longitude,cust.Latitude,int.Parse(cust.Area),int.Parse(cust.Branch),int.Parse(cust.ConnectionNumber),int.Parse(cust.Classification),int.Parse(cust.Tariff), int.Parse(cust.Category),
                    int.Parse(cust.CustomerType), int.Parse(cust.SupplyStatus),cust.IsActive,cust.HasSewer,int.Parse(cust.CreatedBy),cust.Block,int.Parse(cust.Status));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetApplicationTrackLogs(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetApplicationTrackLogs",appnumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCustomerReportData(string reference, string flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustomerReportData", reference, flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetAuditReport(string username, string startdate,string enddate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAuditTrail", username, startdate, enddate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetCompanyProfile(string companyid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCompanyProfile", int.Parse(companyid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetLatestBilledReading(string custref,string areaid,string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetLatestBilledReading", custref,int.Parse(areaid),int.Parse(branchid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal string GetBillingPeriod(string areaid)
        {
            string output = "0";
            try
            {

                dt = ExecuteDataSet("Sp_GetBillingPeriod", int.Parse(areaid));
                if (dt.Rows.Count > 0)
                {
                    output = dt.Rows[0]["period"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }
        internal DataTable GetFieldComments()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetFieldComments");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetSystemUserByRole(string areaid, string role)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetSystemUserByRole", int.Parse(areaid),int.Parse(role));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable CheckCustRefRefInArea(string custref, string areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_CheckCustRefRefInArea", custref,int.Parse(areaid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable SaveReadingDetails(ReadingObj read)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = ExecuteDataSet("Sp_SaveReading", read.Type, read.Method, read.LevelID, read.CustRef,
                read.MeterRef,read.CurReading,read.CurReadingDate, read.Estimated,read.PreReading,
                read.PreReadingDate, read.Consumption,read.Reader,read.Comment,read.Billed,
                 read.Area,read.Branch,read.Period,read.CreatedBy,read.Latitude,read.Longitude);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetLatestReadingStatus(string custref, string areaid, string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetLatestReadingStatus", custref, int.Parse(areaid), int.Parse(branchid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetBillDetails(string area, string branch, string block, string custRef)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBillDetails", custRef, int.Parse(area), int.Parse(branch),block);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetAccountReading(string custRef, string period, string area, string branch)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetUnbilledReading", custRef, int.Parse(area), int.Parse(branch), period);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetBillBasis(string custRef,  string area, string branch)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBillBasis", custRef, int.Parse(area), int.Parse(branch));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal string SaveBillTransaction(TransactionObj trans)
        {
            string output = "";
            try
            {
                 ExecuteCommand("Sp_ProcessBill", trans.CustRef, trans.RdgType, trans.Period, trans.OpenBal, trans.Reason, trans.AreaID, trans.BranchID, trans.CreatedBy, trans.SuppressedCharges, trans.BasisConsumption,
                    trans.ClassID, trans.RdgRecordId, trans.UnitCost,  trans.TariffCode, trans.Sewer, trans.IsVatable,  trans.DocumentNo, 
                     trans.MeterSize, trans.TransCode,  trans.MeterRef, trans.PostDate, trans.InvoiceNumber, trans.ReadingMethod);
                output = "SUCCESS";
            }
            catch (Exception ex)
            {
                //throw ex;
                output = ex.Message;
            }
            return output;
        }

        internal DataTable GetTarrifAmount(string tarriffCode)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTarrifAmount", tarriffCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetCustomerDisplay(int countryid, int areaid, string custref,int flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustomerDetailsByID", countryid,areaid,custref,flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetCustomerDetailsByIDMetered(int countryid, int areaid, string custref, int flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustomerDetailsByIDMetered", countryid, areaid, custref, flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal string GetSystemParameter(string paramCode)
        {
            string output = "0";
            try
            {

                dt = ExecuteDataSet("Sp_GetSystemParameter", int.Parse(paramCode));
                if (dt.Rows.Count > 0)
                {
                    output = dt.Rows[0]["parameterValue"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        internal void SaveFileDetails(int reader, DateTime readingDate, int area, int branch, string filepath, string curPeriod, int capturing, bool processing,bool processed, int failed, int success, bool hasHeader, string fileType)
        {
            try
            {
                ExecuteCommand("Sp_SaveReadingsQueueLogs", reader, readingDate, area,branch,filepath,curPeriod,capturing,processing,processed,failed,success,hasHeader,fileType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetIDList()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetIDList");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable CheckExistingSerial(string meterno)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_CheckMeterSerial",meterno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable SaveMeterInventory(string metertype,string meterserial, string dials,string reading,string life,DateTime manufacturedate, string createdby, bool isactve,string condition)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_SaveMeterInventory", int.Parse(metertype),meterserial,int.Parse(dials),int.Parse(reading),int.Parse(life),manufacturedate,int.Parse(createdby),isactve,condition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetTariffSettings(string areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTariffSettings", int.Parse(areaid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetGeneralTariffs()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetGeneralTariffs");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetMeterActivityReasons(string activityid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetMeterActivityReasons", int.Parse(activityid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetLatestReadingDetails(string custref, string areaid, string branchid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetLatestReadingDetails", custref, int.Parse(areaid), int.Parse(branchid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetTransactionCodes(string flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTransactionCodes",int.Parse(flag));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal double GetVatRateByCountry(string countryid)
        {
            double output = 0;
            try
            {

                dt = ExecuteDataSet("Sp_GetCountrySettingsByID", int.Parse(countryid));
                if (dt.Rows.Count > 0)
                {
                    output = double.Parse(dt.Rows[0]["vatrate"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        internal DataTable GetTranscodeDetails(string transcode)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTranscodeDetails", transcode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void SaveAdjustmentInceptionLogs(TransactionObj trans, string comment)
        {
            try
            {
                //Sp_SaveAdjustmentInceptionLogs
                ExecuteCommand("Sp_SaveAdjustmentTransaction", trans.CustRef, trans.InvoiceNumber, trans.DocumentNo, trans.Period,
                trans.ChargeType, trans.TransValue, trans.VatValue, trans.CreatedBy, trans.TransCode, trans.AreaID, trans.BranchID, comment, trans.PostDate);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal string SaveMeterRequestLogs(string custref, string meterref, string oldtype, string oldsize,string olddials, string prevrdg,
            string serial, string prerdgdt, string appfnlrdg, string appfnlrdgdt, bool isestimated, string consumption, string reason, 
            string newserial, string newsize, string newmake, string newdials, string manufacturedt, string newlife, string comment,
            string appinitialrdg, string appinitedgdt, string createdby, string requesttype,string areaid,string branchid,string period,string servedby)
        {
            string output = "";
            try
            {

                ExecuteCommand("Sp_SaveMeterRequestLogs", custref, meterref, oldtype, oldsize,int.Parse(olddials), int.Parse(prevrdg), serial, Convert.ToDateTime(prerdgdt), int.Parse(appfnlrdg), Convert.ToDateTime(appfnlrdgdt), isestimated,
                    int.Parse(consumption), reason, 
                    newserial, newsize, newmake, int.Parse(newdials), Convert.ToDateTime(manufacturedt), int.Parse(newlife), comment, int.Parse(appinitialrdg), Convert.ToDateTime(appinitedgdt),
                    int.Parse(createdby), requesttype,int.Parse(areaid),int.Parse(branchid),period,servedby);
                output = "success";

            }
            catch (Exception ex)
            {
                //throw ex;
                Log("LogMeterRequest", "101 " + ex.Message);
                output = ex.Message;
            }
            return output;
        }
        internal DataTable GetRequestsToApprove(int countryid, int areaid, string custref)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetRequestsToApprove", countryid, areaid, custref);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetApproverRequestByID(string custref,string recordid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetApproverRequestByID", custref,int.Parse(recordid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetBillDetailsByCustRef(int areaID, string customerRef)
        {

            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBillDetailsByCustRefs", areaID, customerRef);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }

        internal DataTable GetCustBillNoByPeriod(string custRef, string period)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustBillNoByPeriod", custRef, period);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //internal DataTable GetRelationshipManagerDetails(string custRef)
        //{
        //    throw new NotImplementedException();
        //}

        internal DataTable GetCustBill(string custRef, int billNo, int areaID, string period)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustBillPrinting", custRef, billNo, areaID, period);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        internal DataTable GetCustBillDetails(string custRef, int billNo, int flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCustBillDetails", custRef, billNo, flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable getCredentialsFile(string area, string branch)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCredentialsFile", area,branch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        
        internal DataTable GetTariffsFile(string country)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTariffsFile", int.Parse(country));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetSurveyData(int appid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetSurveyData", appid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCostingItemsByCostID(int applicationID,int costid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetCostingItemsByCostID", applicationID,costid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void RemoveCostingItem(int appId, int costId)
        {
            try
            {
                ExecuteCommand("Sp_RemoveCostingItem", appId, costId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //added 5/8/2021
        internal DataTable CheckFlatRated(string custRef)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_CheckFlatRated", custRef);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal string SaveBillTransactionUnMetered(TransactionObj trans,string tariffrecord)
        {
            string output = "";
            try
            {
                ExecuteCommand("Sp_ProcessBillUnmetered", trans.CustRef, trans.RdgType, trans.Period, trans.OpenBal, trans.Reason, trans.AreaID, trans.BranchID, trans.CreatedBy, trans.SuppressedCharges, trans.BasisConsumption,
                   trans.ClassID, trans.RdgRecordId, trans.UnitCost, trans.TariffCode, trans.Sewer, trans.IsVatable, trans.DocumentNo,
                    trans.MeterSize, trans.TransCode, trans.MeterRef, trans.PostDate, trans.InvoiceNumber, trans.ReadingMethod,int.Parse(tariffrecord));
                output = "SUCCESS";
            }
            catch (Exception ex)
            {
                //throw ex;
                output = ex.Message;
            }
            return output;
        }
        internal DataTable GetReadingSheet(string areaid,string branchid,string block)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetReadingSheet", int.Parse(areaid),int.Parse(branchid),block);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetBlockSettingByID(string blockId)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBlockDetailsByID", int.Parse(blockId));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal void SaveBlockDetails(string code, string country, string area, string branch, string block, string connection, string createdby, bool isactive, string status)
        {
            try
            {
                ExecuteCommand("Sp_SaveBlockDetails", code,int.Parse(area),int.Parse(branch),block,connection,int.Parse(createdby),isactive,status);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetBlockDetails(string areaid,string branchid, string block)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetBlockDetails", int.Parse(areaid),int.Parse(branchid),block);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetFieldConnectionData(int appid,int flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetFieldConnectionData", appid,flag);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal string ModifyMeter(string action, string requesttype,
            string meterRef, string custRef, string serial, string oldReading, string oldRdgDate,
            string curReading, string curRdgDate1, bool isestimated, string newReading, string dials,
            string installedBy, string curRdgDate2, string type, string size, string life,
            string manufacturedDate, string reason, string area, string branch, string createdby,
            string period, string finalconsumption, string suppressioncode, string approvercomment, string recordid)
        {

            string output = "";
            try
            {
                ExecuteCommand("Sp_ModifyMeter", meterRef, serial, int.Parse(life), Convert.ToDateTime(manufacturedDate),
                    size, int.Parse(type), int.Parse(dials), installedBy, reason, area, int.Parse(createdby), 0, requesttype,
                    "M", 1, isestimated, int.Parse(curReading), Convert.ToDateTime(curRdgDate1), "", int.Parse(oldReading),
                   Convert.ToDateTime(oldRdgDate), "", int.Parse(finalconsumption), "", int.Parse(branch), installedBy,
                   period, 0, int.Parse(newReading), custRef, 0, suppressioncode, 1, 1, 0, 1, "N/A", 0, "WS", DateTime.Now,
                   action,approvercomment,int.Parse(recordid));
                output = "success";

            }
            catch (Exception ex)
            {
                //throw ex;
                Log("Sp_ModifyMeter", "101 " + ex.Message);
                output = ex.Message;
            }
            return output;
        }

        internal void UpdateMeterRequestStatus(string custRef, string requestid, string action, string approvercomment, string createdBy, bool iscompleted)
        {
            try
            {
                ExecuteCommand("Sp_UpdateMeterRequestStatus", custRef,int.Parse(requestid),action,approvercomment,int.Parse(createdBy),iscompleted);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal void SaveApplicationFile(int appId, string filePath, string fileName, int user_id)
        {
            try
            {
                ExecuteCommand("Sp_SaveApplicationFile", appId, filePath, fileName, user_id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetFileAttachments(string appid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetApplicationFiles", int.Parse(appid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void DeleteApplicationItem(string appid, int itemno, string deletedby)
        {
            try
            {
                ExecuteCommand("Sp_DeleteApplicationItem", appid, itemno, int.Parse(deletedby));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DateTime GetBillPeriodStartDate(int RecordCode)
        {
            DateTime startDate = new DateTime();
            try
            {
               
                dt = ExecuteDataSet("Sp_GetBillPeriodStartDate", RecordCode);
                if(dt.Rows.Count > 0)
                {
                    startDate = DateTime.Parse(dt.Rows[0]["StartDate"].ToString());
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return startDate;
        }

        internal void SaveBillingPeriod(int recordID, string periodCode, string period, DateTime startDate, int areaID, int createdBy)
        {
            try
            {
                ExecuteCommand("Sp_SaveBillingPeriod", recordID, periodCode, period, startDate, areaID, createdBy);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable CheckAreaPeriod(string period, int areaID)
        {

            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_CheckAreaPeriod", areaID, period);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        // 
        internal DataTable GetAllBillingPeriod(string areaid)
        {

            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAllBillingPeriod", int.Parse(areaid));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetBalanceOutstanding(string branch, DateTime fromdate, DateTime todate, string amount)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("RPT_GetBalanceOutstanding", 10,int.Parse(branch),fromdate,todate,amount);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCustomerCount(string branch,  string period)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("RPT_GetCustomerCount", 10, int.Parse(branch), period);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetMeterAudit(string branch, string period)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("RPT_GetMeterAudit", 10, int.Parse(branch), period);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetTransactionAudit(string branch, string period, string code)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("RPT_GetTransactionAudit", 10, int.Parse(branch), period,code);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetStatement(string custref, DateTime startdate, DateTime enddate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("RPT_GetStatementBetweenDates", custref,startdate,enddate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public string GetOutstandingBalance(DataTable dtprint)
        {
            string output = "0";
            DataSet ds = new DataSet();
            ds.Tables.Add(dtprint);
            //To find last row in data set

            if (ds.Tables[0].Rows.Count > 0)
            {
                output = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["Balance"].ToString();
            }
            return output;
        }
        public DataTable GetAllUsers_filtered(string search,string roleid,string branch)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetAllSystemUsers_filtered", search,roleid,branch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public void ResetPassword(int userID, string password, bool reset, string changedBy, string userName, string action)
        {
            try
            {
                ExecuteCommand("Sp_ResetUserPassword", userID, password, reset, changedBy, userName, action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetUserStatus()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetUserStatus");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetOperationAreaList(int area)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetOperationAreaList", area);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetTerritoryList(int opid,int branch)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetTerritoryList", opid,branch);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetSubTerritoryList(int territoryid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetSubTerritoryList", territoryid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal string GetSettingAlias(string mycode, string flag)
        {
            string output = "00";
            try
            {

                dt = ExecuteDataSet("Sp_GetSettingAlias", mycode,flag);
                if (dt.Rows.Count > 0)
                {
                    output = dt.Rows[0]["alias"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }
        internal int GetIncrementNumberByArea(int areaid, int branchid)
        {
            int output = 0;
            try
            {

                dt = ExecuteDataSet("Sp_GetIncrementNumber", areaid, branchid);
                if (dt.Rows.Count > 0)
                {
                    output = Convert.ToInt16(dt.Rows[0]["incrementNo"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }
        internal DataTable GetPaymentTransactionsByDate(int countryid, int areaid,DateTime startdate,DateTime enddate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetPaymentTransactionsByDate", countryid, areaid, startdate,enddate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetApplicationByStatusFiltered(string area,string branch, string status,string search,DateTime start,DateTime end)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = ExecuteDataSet("Sp_GetApplicationByStatusFiltered", area, branch,int.Parse(status),search,start,end);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}