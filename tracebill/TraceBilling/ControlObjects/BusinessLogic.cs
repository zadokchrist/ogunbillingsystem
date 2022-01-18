using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TraceBilling.ControlObjects;
using TraceBilling.EntityObjects;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Collections;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace TraceBilling.ControlObjects
{
    public class BusinessLogic
    {
        DatabaseHandler dh = new DatabaseHandler();
        ResponseMessage resp = new ResponseMessage();
       
        private DataTable dt;
        private const string smtpServer = "smtp.gmail.com";
        private const string smtpUsername = "ogunwatercorp@gmail.com";
        private const string smtpPassword = "OgunWater@2020";
        internal DataTable GetRequirementList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetRequirementList();

            }
            catch (Exception ex)
            {
                Log("GetRequirementList", "101 " + ex.Message);
            }
            return dt;
        }
        internal void Log(string method, string error_message)
        {
            try
            {
                dh.Log(method, error_message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetCustomerTypeList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerTypeList();

            }
            catch (Exception ex)
            {
                Log("GetCustomerTypeList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCountryList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCountryList();

            }
            catch (Exception ex)
            {
                Log("GetCountryList", "101 " + ex.Message);
            }
            return dt;
        }

       

        internal DataTable GetSystemUserByCode(string userCode)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = dh.GetSystemUserByCode(userCode);

            }
            catch (Exception ex)
            {
                Log("GetSystemUserByCode", "101 " + ex.Message);
            }
            return dt;
        }

       

        internal DataTable GetAreaList(int countryid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetAreaList(countryid);

            }
            catch (Exception ex)
            {
                Log("GetAreaList", "101 " + ex.Message);
            }
            return dt;
        }



        internal DataTable GetSurveyQnList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetSurveyQnList();

            }
            catch (Exception ex)
            {
                Log("GetSurveyQnList", "101 " + ex.Message);
            }
            return dt;
        }

        internal DataTable GetBranchList(int areaid, int operationid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBranchList(areaid,operationid);

            }
            catch (Exception ex)
            {
                Log("GetBranchList", "101 " + ex.Message);
            }
            return dt;
        }



        internal DataTable GetAreaSessions(int areaid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetAreaSessions(areaid);

            }
            catch (Exception ex)
            {
                Log("GetAreaSessions", "101 " + ex.Message);
            }
            return dt;
        }

        internal DataTable GetUserList(int countryid, int roleid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetUserList(10, roleid);

            }
            catch (Exception ex)
            {
                Log("GetUserList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetRoleList()
        {
            DataTable dt = new DataTable();
            try
            {

                dt = dh.GetRoleList();

            }
            catch (Exception ex)
            {
                Log("GetRoleList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCustomerClass()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerClass();

            }
            catch (Exception ex)
            {
                Log("GetCustomerClass", "101 " + ex.Message);
            }
            return dt;
        }

        internal ResponseMessage ValidateApplication(string firstname, string lastname, string contact, string address, string identity)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {

                if (String.IsNullOrEmpty(firstname))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE FIRSTNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(lastname))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE LASTNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(contact))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE PHONE NUMBER CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(address))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE ADDRESS CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(identity))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE APPLICATION IDENTITY CANNOT BE EMPTY!";
                }
                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateLogin", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }

       

        internal ResponseMessage SaveApplication(ApplicationObj app)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveApplication(app);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveApplication", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }

       
        internal ResponseMessage ValidateLogin(string username, string encrypted_password)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {

                if (String.IsNullOrEmpty(username))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "THE USERNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(encrypted_password))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE PASSWORD CANNOT BE EMPTY!";
                }
                else if (!IsMatch(username, encrypted_password))
                {
                    message.Response_Code = "104";
                    message.Response_Message = "PLEASE CHECK YOUR LOGIN ENTRIES AND TRY AGAIN.";
                }
                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateLogin", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }

       

        public string EncryptString(string ClearText)
        {
            byte[] clearTextBytes = Encoding.UTF8.GetBytes(ClearText);
            System.Security.Cryptography.SymmetricAlgorithm done = SymmetricAlgorithm.Create();

            MemoryStream ms = new MemoryStream();
            byte[] key1 = Encoding.ASCII.GetBytes("ryojvlzmdalyglrj");
            byte[] key = Encoding.ASCII.GetBytes("hcxilkqbbhczfeultgbskdmaunivmfuo");
            CryptoStream cs = new CryptoStream(ms, done.CreateEncryptor(key, key1), CryptoStreamMode.Write);

            cs.Write(clearTextBytes, 0, clearTextBytes.Length);

            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }



        private bool IsMatch(string uswername, string encrypted_password)
        {
            DataTable dt = new DataTable();
            ResponseMessage message = new ResponseMessage();
            bool value = false;
            try
            {
                dt = dh.GetSystemUser(uswername, encrypted_password);
                if (dt.Rows.Count > 0)
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

            }
            catch (Exception ex)
            {
                Log("IsMatch", "101 " + ex.Message);
                value = false;
            }
            return value;
        }



        internal DataTable GetSystemUser(string username, string encrypted_password)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetSystemUser(username, encrypted_password);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetSystemUser", "101 " + ex.Message);
            }
            return dt;
        }



        public string GetCodeIdentity(string value, int flag)
        {
            string output = "0";
            output = dh.GetCodeIdentity(value, flag);
            return output;
        }



        public string GetApplicationNumber(string Code, string countrycode, string AreaCode, string BranchCode, string UserCode)
        {
            string output = "";
            if (Code.Equals("0"))
            {
                string SerialCode = "WAPN";
                string Date = DateTime.Now.ToString("ddMMyyyy");
                int RunningNumber = dh.GetSerialNumber(int.Parse(countrycode), int.Parse(AreaCode), int.Parse(BranchCode), SerialCode, UserCode);
                int NewNumber = RunningNumber + 1;
                output = Date + "/" + countrycode + "/" + AreaCode + "/" + BranchCode + "/" + NewNumber;
            }
            return output;
        }

       

        internal void DeactivateAccount(string custref, string reason, string recordedby,string crmreference)
        {
            dh.DeactivateAccount( custref, reason, recordedby,crmreference);
        }

       

        internal void ReactivateAccount(string custref,string reason,string recordedby,string crmreference) 
        {
            dh.ReactivateAccount(custref, reason, recordedby, crmreference);
        }

       

        internal DataTable GetApplicationByStatus(string applicationame, string country, string area, string status)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetApplicationByStatus(applicationame, country, area, status);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetApplicationByStatus", "101 " + ex.Message);
            }
            return dt;
        }

        

        internal DataTable GetApplicationByIDForPayment(string appid) 
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetApplicationByIDForPayment(appid);
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }



        internal DataTable GetSurveyDetails(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetApplicationDetails(appnumber);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetSurveyDetails", "101 " + ex.Message);
            }
            return dt;
        }

        internal string AssignSurveyJob(string applicationCode, string surveyor)
        {
            string output = "";
            int ApplicationID = Convert.ToInt32(applicationCode);
            int SurveyorID = Convert.ToInt32(surveyor);
            //check if jobcard is ready
            DataTable dt = dh.GetSurveyData(ApplicationID);
            if(dt.Rows.Count > 0)
            {
                dh.AssignSurveyor(ApplicationID, SurveyorID);
                output="Jobcard Assignment has been done Successfully";
            }
            else
            {
                output = "Jobcard not ready to be assigned. Please generate it!!";
            }
            return output;
        }
        internal ResponseMessage ValidateUser(string firstname, string lastname, string username, string reason)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {

                if (String.IsNullOrEmpty(firstname))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "THE FIRSTNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(lastname))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "THE LASTNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(username))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "THE USERNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(reason))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "THE REASON TO ADD/EDIT CANNOT BE EMPTY!";
                }
                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateLogin", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }

       

        internal ResponseMessage SaveSystemUser(UserObj user)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveUser(user);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveUser", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }

        internal ResponseMessage SendEmail(string emailAddress,string Subject,string Message) 
        {
            ResponseMessage response = new ResponseMessage();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(smtpUsername);
                mail.Sender = new MailAddress(smtpUsername);
                mail.To.Add(emailAddress);
                mail.IsBodyHtml = true;
                mail.Subject = Subject.Replace('\r', ' ').Replace('\n', ' ');
                mail.Body = Message;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = true;

                smtp.Timeout = 30000;
                try
                {

                    smtp.Send(mail);
                    response.Response_Code = "1";
                    response.Response_Message = "SUCCESS";
                }
                catch (SmtpException e)
                {
                    response.Response_Code = "1000";
                    response.Response_Message = e.Message;
                }
            }
            catch (Exception e)
            {
                response.Response_Code = "1000";
                response.Response_Message = e.Message;
            }
            return response;
        }



        internal string MakeUserName(string FName, string LName)
        {
            string MadeUserName = FName.Substring(0, 1) + LName;
            return MadeUserName.ToLower();
        }
        internal void LogUserActivity(UserObj user)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.LogUserActivity(user);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("LogUserActivity", resp.Response_Code + " " + resp.Response_Message);
            }

        }

       

        internal DataTable GetAllUsers(int countryid)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = dh.GetAllUsers(countryid);

            }
            catch (Exception ex)
            {
                Log("GetAllUsers", "101 " + ex.Message);
            }
            return dt;
        }
        internal string ChangeUserAccess(string UserCode, string UserName, string Status, string action)
        {
            int UserID = Convert.ToInt32(UserCode);
            bool IsActive = false;
            string output = "";
            string LoggedIn = HttpContext.Current.Session["UserID"].ToString();
            if (UserCode == LoggedIn)
            {
                output = "A logged In account cannot Enabled/Disable itself";
            }
            else
            {

                if (Status == "NO")
                {
                    IsActive = true;
                }
                dh.ChangeUserAccess(UserID, IsActive, UserName, LoggedIn, action);
                if (IsActive)
                {
                    output = "System Accessible for " + UserName + " has been enabled successfully";
                }
                else
                {
                    output = "System Accessible for " + UserName + " has been disabled successfully";
                }
            }
            return output;

        }






        //added 24/11/2020
        public string GenerateJobCards(string StringArray, string AppType)
        {
            string output = "";
            if (StringArray == "")
            {
                output = "Please Select Applications to Generate Job Cards for";
            }
            else
            {
                string[] arr = StringArray.Split(',');
                int i = 0;
                string ApplicationCode = "0";
                //int countrycode = Convert.ToInt16(HttpContext.Current.Session["countryCode"].ToString());
                //int areacode = Convert.ToInt16(HttpContext.Current.Session["areaCode"].ToString());
                //int branchcode = Convert.ToInt16(HttpContext.Current.Session["branchCode"].ToString());
                int CreatedBy = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
                int status = 3;//surveying
                for (i = 0; i < arr.Length; i++)
                {
                    // ApplicationCode = Convert.ToInt32(arr[i].ToString());
                    ApplicationCode = arr[i].ToString();
                    if (ApplicationCode != "0")
                    {
                        DataTable dt = GetSurveyDetails(ApplicationCode);
                        if (dt.Rows.Count > 0)
                        {
                            // A.applicationNumber,(A.firstName + '' + A.lastName) as 'fullName'
                            int countrycode = Convert.ToInt16(dt.Rows[0]["countryCode"].ToString());
                            int areacode = Convert.ToInt16(dt.Rows[0]["areaCode"].ToString());
                            int branchcode = Convert.ToInt16(dt.Rows[0]["branchCode"].ToString());
                            int appID = Convert.ToInt16(dt.Rows[0]["ApplicationID"].ToString());
                            int countryid = Convert.ToInt16(dt.Rows[0]["countryId"].ToString());
                            int areaid = Convert.ToInt16(dt.Rows[0]["areaId"].ToString());
                            int branchid = Convert.ToInt16(dt.Rows[0]["branchid"].ToString());
                            //string JobNumber = GetJobNumber(countryid, areaid, branchid, CreatedBy);
                            string JobNumber = "JCN/"+ApplicationCode;
                            dh.SaveSurveyJobNumber(appID, JobNumber, countryid, areaid, branchid, CreatedBy);
                            //auto Assigning
                            //Assignjob(appID, CreatedBy, countryid);
                            //log status
                            //LogApplicationTransactions(appID, status, CreatedBy);
                        }


                    }
                }
                output = "Job Cards have been created for " + i.ToString() + " application(s) Successfully";
            }
            return output;
        }

       

        private void Assignjob(int appID, int createdBy, int countryid)
        {
            DataTable dataTable = GetUserList(countryid, 5);
            int number = dataTable.Rows.Count;
            if (number > 0)
            {
                //if (number.Equals(1))
                //{
                    /// assign
                    /// 
                    string surv_code = dataTable.Rows[0]["userId"].ToString();
                    AssignSurveyJob(appID.ToString(), surv_code);
                //}
            }
        }

        private string GetJobNumber(int countrycode, int AreaCode, int BranchCode, int createdBy)
        {
            string output = "";
            string SerialCode = "JCN";
            //string Date = DateTime.Now.ToString("ddMMyyyy");
            int RunningNumber = dh.GetSerialNumber(countrycode, AreaCode, BranchCode, SerialCode, createdBy.ToString());
            int NewNumber = RunningNumber + 1;
            output = SerialCode + "/" + countrycode + "/" + AreaCode + "/" + BranchCode + "/" + NewNumber;

            return output;
        }
        //added 03/12/2020
        internal DataTable GetSurveyReportDetails(string jobnumber, int countryid, int areaid, int status)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetSurveyReportDetails(jobnumber, countryid, areaid, status);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetSurveyReportDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal void LogApplicationTransactions(int appid, int status, int createdby)
        {
            try
            {
                dh.LogApplicationTransactions(appid, status, createdby);
            }
            catch (Exception ex)
            {
                Log("LogApplicationTransaction", "101 " + ex.Message);
            }
        }
        internal void SaveSurveyDetails(string qnid, string ans, string appid, string createdby, DateTime surveydate)
        {
            try
            {
                dh.SaveSurveyDetails(appid, qnid, ans, createdby, surveydate);
            }
            catch (Exception ex)
            {
                Log("SaveSurveyDetails", "101 " + ex.Message);
            }
        }



        internal ResponseMessage ValidateConnection(string customertype, string category, string instructiondate, string authorizedby)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {

                if (customertype == "0")
                {
                    message.Response_Code = "102";
                    message.Response_Message = "PLEASE SELECT CUSTOMER TYPE!";
                }
                else if (category == "0")
                {
                    message.Response_Code = "102";
                    message.Response_Message = "PLEASE SELECT CUSTOMER CLASS!";
                }
                else if (String.IsNullOrEmpty(instructiondate))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "INSTRUCTION DATE CANNOT BE EMPTY!";
                }
                else if (authorizedby == "0")
                {
                    message.Response_Code = "102";
                    message.Response_Message = "PLEASE SELECT SURVEY AUTHORIZER!";
                }
                else if (String.IsNullOrEmpty(authorizedby))
                {
                    message.Response_Code = "102";
                    message.Response_Message = "PERSON AUHTORIZING CANNOT BE EMPTY!";
                }
                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateLogin", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }



        internal ResponseMessage SaveFieldConnection(string conid, string appid, string jobno, string customertype, string category, string authorizedby, DateTime connectiondate, DateTime instructiondate, string createdby, string areaid)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveFieldConnection(conid, appid, jobno, customertype, category, authorizedby, connectiondate, instructiondate, createdby, areaid);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveFieldConnection", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }

       


        //16/12/2020
        internal DataTable GetMaterialOptions(string type)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetMaterialOptions(type);

            }
            catch (Exception ex)
            {
                Log("GetMaterialOptions", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCostMaterials(int applicationID, int categoryId)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCostMaterials(applicationID, categoryId);

            }
            catch (Exception ex)
            {
                Log("GetCostMaterials", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCostingItems(int applicationID)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCostingItems(applicationID);

            }
            catch (Exception ex)
            {
                Log("GetCostingItems", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetMaterialDetails(int itemID)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetMaterialDetails(itemID);

            }
            catch (Exception ex)
            {
                Log("GetMaterialDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal string SaveCostingDetails(string costCode, string applicationCode, string expenseItemCode, string size, string length, string quantity, string unitCost)
        {
            string output = "";
            try
            {
                int CostID = Convert.ToInt32(costCode);
                int ApplicationID = Convert.ToInt32(applicationCode);
                int ItemID = Convert.ToInt16(expenseItemCode);
                int CreatedBy = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
                double Quantity = 1.0;
                if (quantity != "")
                {
                    Quantity = Convert.ToDouble(quantity);
                }
                double Amount = Convert.ToDouble(unitCost.Replace(",", ""));
                dh.SaveCostingDetails(CostID, ApplicationID, ItemID, Quantity, Amount, size, length, CreatedBy);
                if (costCode == "0")
                {
                    output = "Costing Details have been Captured Successfully";
                }
                else
                {
                    output = "Costing Details have been updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Log("SaveCostingDetails", "101 " + ex.Message);
            }
            return output;
        }



        internal DataTable GetFieldCustomerDetails(string appid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetFieldCustomerDetails(appid);

            }
            catch (Exception ex)
            {
                Log("GetFieldCustomerDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetPipeDiameterList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetPipeDiameter();

            }
            catch (Exception ex)
            {
                Log("GetPipeDiameter", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetPipeTypeList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetPipeType();

            }
            catch (Exception ex)
            {
                Log("GetPipeType", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage SaveFieldEstimates(string estimateid, string applicationid, string pipediameter, string pipetype, string pipelength, string excavationlength, string createdby)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveFieldEstimates(estimateid, applicationid, pipediameter, pipetype, pipelength, excavationlength, createdby);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveFieldEstimates", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }


        internal DataTable GetNonConsumptionInvoiceDetails(string paymentref) 
        {
            try
            {
                dt = dh.GetNonConsumptionInvoiceDetails(paymentref);
            }
            catch (Exception ex)
            {
                Log("GetNonConsumptionInvoiceDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetInvoiceDetails(string appnumber, int countryid, int areaid, int status)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetInvoiceDetails(appnumber, countryid, areaid, status);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetInvoiceDetails", "101 " + ex.Message);
            }
            return dt;
        }

      

        public string GetPaySlipsStringArray(bool fee, bool deposit)
        {
            string output = "";
            if (fee == false && deposit == false)
            {
                output = "";
            }
            else if (fee == true && deposit == true)
            {
                output = "NC,DP";
            }
            else if (fee == true && deposit == false)
            {
                output = "NC";
            }
            else if (fee == false && deposit == true)
            {
                output = "DP";
            }

            return output;
        }

       

        internal DataTable GetConnectionDetails(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetConnectionDetails(appnumber);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetConnectionDetails", "101 " + ex.Message);
            }
            return dt;
        }

        

        internal void SaveApplicationComment(string appid, string action, string comment, string createdby)
        {
            try
            {
                dh.SaveApplicationComment(appid, action, comment, createdby);
            }
            catch (Exception ex)
            {
                Log("SaveApplicationComment", "101 " + ex.Message);
            }
        }
        internal string GenerateAdviceSlipRef(string appcode, string amount, string code, string createdby)
        {
            string output = "";
            AdviseSlipObj slip = new AdviseSlipObj();
            string res = "";
            try
            {
                DataTable dt = dh.GetApplicationDetails(appcode);
                if (dt.Rows.Count > 0)
                {
                    //txtappcode.Text = dt.Rows[0]["applicationNumber"].ToString();
                    slip.FullName = dt.Rows[0]["fullName"].ToString();
                    slip.Address = dt.Rows[0]["address"].ToString();
                    slip.ApplicationId = dt.Rows[0]["ApplicationID"].ToString();
                    slip.ApplicationNo = appcode;
                    slip.Amount = amount;
                    slip.PaymentCode = code;
                    slip.CreatedBy = createdby;
                    slip.Contact = dt.Rows[0]["contact"].ToString();
                    slip.Country = dt.Rows[0]["countryId"].ToString();
                    slip.Area = dt.Rows[0]["areaId"].ToString();
                    slip.Serial = "0";
                    //check paycode existence against id
                    if (IsPayRefExisting(slip.PaymentCode, slip.ApplicationId))
                    {
                        res = dh.CheckPaymentRefGenerated(slip.PaymentCode, slip.ApplicationId).Rows[0]["PaymentRef"].ToString();
                    }
                    else
                    {
                        //get reference
                        res = dh.GetpaymentRef(slip);
                    }
                    if (!res.Equals("0") && res.Length < 12)//to exclude non serial returned error msgs
                    {
                        slip.PaymentRef = res;
                        slip.ErrorMessage = "NONE";
                        output = "Payment reference " + slip.PaymentRef + " generated Successfully";
                    }
                    else
                    {
                        slip.ErrorMessage = "Result error:" + res + ",," + slip.ApplicationId;
                        //log error                    
                        Log("SavePaymentSlip", "101 " + slip.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("GenerateAdviceSlipRef", "101 " + ex.Message);
            }
            return output;
        }

       

        private bool IsPayRefExisting(string paymentCode, string applicationId)
        {
            bool value = false;
            try
            {
                dt = dh.CheckPaymentRefGenerated(paymentCode, applicationId);
                if (dt.Rows.Count > 0)
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

            }
            catch (Exception ex)
            {
                Log("IsPayRefExisting", "101 " + ex.Message);
                value = false;
            }
            return value;
        }

       

        internal DataTable GetInvoiceDetailsByAppNumber(string appnumber)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetInvoiceDetailsByAppNumber(appnumber);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetInvoiceDetails", "101 " + ex.Message);
            }
            return dt;
        }
        public bool CheckPaymentInvoice(string appnumber)
        {
            bool value = false;
            try
            {
                dt = dh.GetInvoiceDetailsByAppNumber(appnumber);
                if (dt.Rows.Count > 0)
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

            }
            catch (Exception ex)
            {
                Log("CheckPaymentInvoice", "101 " + ex.Message);
                value = false;
            }
            return value;
        }
        internal DataTable GetPaymentTransactions(int countryid, int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetPaymentTransactions(countryid, areaid);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetPaymentTransactions", "101 " + ex.Message);
            }
            return dt;
        }
        internal void UpdateTransactionAsReconciled(bool isreconcile, int recordid, float amount, string custRef, DateTime reconciledDate, string username, string method)
        {
            try
            {
                dh.UpdateTransactionAsReconciled(isreconcile, recordid, amount, custRef, reconciledDate, username, method);
            }
            catch (Exception ex)
            {
                Log("UpdateTransactionAsReconciled", "101 " + ex.Message);
            }
        }
        internal DataTable GetAllTransactions(int countryid, int areaid)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetAllTransactions(countryid, areaid);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetAllTransactions", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetAllTransactionsByDate(int countryid, int areaid,DateTime startdate,DateTime enddate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetAllTransactionsByDate(countryid, areaid,startdate,enddate);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetAllTransactions", "101 " + ex.Message);
            }
            return dt;
        }
        internal void RecordAudittrail(string username, string actiontaken) 
        {
            try
            {
                dh.RecordAudittrail(username, actiontaken);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetRouteFile(string country, string area, string branch, string book, string walk)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetRouteFile(country, area, branch, book, walk);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetAllTransactions", "101 " + ex.Message);
            }
            return dt;
        }
        public void CheckPath(string Path)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }
        internal DataTable GetVendorList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetVendorList();

            }
            catch (Exception ex)
            {
                Log("GetVendorList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable CheckCustomerDetails(string custref)
        {
            dt = new DataTable();
            try
            {

                dt = dh.CheckCustomerDetails(custref);

            }
            catch (Exception ex)
            {
                Log("CheckCustomerDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage ValidateTransaction(PaymentObj trans)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {
                string strerror = "";
                if (String.IsNullOrEmpty(trans.CustRef))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE CUSTREF CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(trans.Amount))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE TRANSACTION AMOUNT CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(trans.PaymentDate))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE PAYMENT DATE CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(trans.VendorTransRef))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE VENDOR TRANSACTION REF CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(trans.VendorCode))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE VENDOR IDENTITY CANNOT BE EMPTY!";
                }
                else if (!IsNumeric(trans.Amount))
                {
                    message.Response_Code = "104";
                    message.Response_Message = "Invalid Transaction Amount.";
                }
                else if (IsDuplicateTrans(trans))
                {
                    message.Response_Code = "104";
                    message.Response_Message = "Duplicate Vendor Transaction Reference.[" + trans.VendorTransRef + "]";

                }
                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }
                //log error
                if (!message.Response_Code.Equals("0"))
                {
                    strerror = message.Response_Code + ":" + message.Response_Message;
                    dh.LogTransactionError(trans.VendorCode, strerror, trans.CustRef, trans.Area, trans.VendorTransRef, trans.Amount);
                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateTransaction", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }



        private bool IsDuplicateTrans(PaymentObj trans)
        {
            bool value = false;
            try
            {
                dt = dh.VerifyVendorTransRef(trans);
                if (dt.Rows.Count > 0)
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

            }
            catch (Exception ex)
            {
                Log("IsDuplicateTrans", "101 " + ex.Message);
                value = false;
            }
            return value;
        }

        internal ResponseMessage SavePaymentTransaction(PaymentObj trans)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SavePaymentTransaction(trans);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SavePaymentTransaction", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        public bool IsNumeric(string parameter)
        {
            bool isNum = false;
            double Num;
            isNum = double.TryParse(parameter, out Num);
            return isNum;
        }
        internal DataTable GetMeterTypeList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetMeterTypeList();

            }
            catch (Exception ex)
            {
                Log("GetMeterTypeList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCurrencyList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCurrencyList();

            }
            catch (Exception ex)
            {
                Log("GetCurrencyList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCountrySettings()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCountrySettings();

            }
            catch (Exception ex)
            {
                Log("GetCountrySettings", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCountrySettingByID(string countryId)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCountrySettingByID(countryId);

            }
            catch (Exception ex)
            {
                Log("GetCountrySettingByID", "101 " + ex.Message);
            }
            return dt;
        }
        internal void SaveCountryDetails(string code, string countryname, string countrycode, string vat, string currency, string createdby, bool isactive)
        {
            try
            {
                dh.SaveCountryDetails(code, countryname, countrycode, vat, currency, createdby, isactive);
            }
            catch (Exception ex)
            {
                Log("SaveCountryDetails", "101 " + ex.Message);
            }
        }
        internal string SaveExpenditureDetails(string costCode, string applicationCode, string expenseItemCode, string size, string length, string quantity, string unitCost)
        {
            string output = "";
            try
            {
                int CostID = Convert.ToInt32(costCode);
                int ApplicationID = Convert.ToInt32(applicationCode);
                int ItemID = Convert.ToInt16(expenseItemCode);
                int CreatedBy = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
                double Quantity = 1.0;
                if (quantity != "")
                {
                    Quantity = Convert.ToDouble(quantity);
                }
                double Amount = Convert.ToDouble(unitCost.Replace(",", ""));
                dh.SaveExpenditureDetails(CostID, ApplicationID, ItemID, Quantity, Amount, size, length, CreatedBy);
                if (costCode == "0")
                {
                    output = "Field expense Details have been Captured Successfully";
                }
                else
                {
                    output = "Field expense Details have been updated Successfully";
                }
            }
            catch (Exception ex)
            {
                Log("SaveExpenditureDetails", "101 " + ex.Message);
            }
            return output;
        }
        internal DataTable GetExpenseItems(int applicationID)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetExpenseItems(applicationID);

            }
            catch (Exception ex)
            {
                Log("GetExpenseItems", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage SaveFieldExpenseLogs(string estimateid, string applicationid, string pipediameter, string pipetype, string pipelength, string excavationlength, string createdby, string comment)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveFieldExpenseLogs(estimateid, applicationid, pipediameter, pipetype, pipelength, excavationlength, createdby, comment);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveFieldExpenseLogs", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        public bool IsValidReadingDate(string ReadDate)
        {
            DateTime RdgDate = Convert.ToDateTime(ReadDate);
            DateTime now = DateTime.Now;
            if (RdgDate > now)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsValidDateComparison(DateTime previousdate, DateTime currentdate)
        {

            if (currentdate < previousdate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        internal DataTable GetBlockMaps(string areaid, string branchid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBlockMaps(areaid, branchid);

            }
            catch (Exception ex)
            {
                Log("GetBlockMaps", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetBlockConnectionNumber(string areaid, string branchid, string blockno)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBlockConnectionNumber(areaid, branchid, blockno);

            }
            catch (Exception ex)
            {
                Log("GetBlockConnectionNumber", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage SaveFieldDocket(string recordCode, string applicationid, string pipediameter, string metertype, string meterref, string meternumber, string createdby, string remark,
            string longitude, string latitude, string reading, string dials, string meterlife, DateTime manufacturedate, string installedby, DateTime installdate, string blocknumber, string connectionno)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveFieldDocket(recordCode, applicationid, pipediameter, metertype, meterref, meternumber, createdby, remark, longitude, latitude, reading, dials, meterlife, manufacturedate, installdate,
                    blocknumber, connectionno, installedby);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveFieldExpenseLogs", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        public string GetMeterReference(string areaCode, string block, string connectionNumber)
        {
            string meterRef = "";

            int connLen = connectionNumber.Length;
            int zeros = 0;
            if(block.StartsWith("0"))
            {
                block = block.Remove(0, 1);
            }

            if (connLen == 1)
            {
                connectionNumber = "000" + connectionNumber;
            }
            else if (connLen == 2)
            {
                connectionNumber = "00" + connectionNumber;
            }
            else if (connLen == 3)
            {
                connectionNumber = "0" + connectionNumber;
            }
            meterRef = areaCode + block + connectionNumber;
            return meterRef;
        }
        internal DataTable GetFieldDocketByApplication(int appid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetFieldDocketByApplication(appid);

            }
            catch (Exception ex)
            {
                Log("GetFieldDocketByApplication", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetNewConnectionCustomerDetails(string appnumber)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetNewConnectionCustomerDetails(appnumber);

            }
            catch (Exception ex)
            {
                Log("GetNewConnectionCustomerDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetTariff(string classid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTariff(classid);

            }
            catch (Exception ex)
            {
                Log("GetTariff", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage ValidateCustomer(string custName, string meterRef, string propertyRef, string custtype, string category)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {

                if (String.IsNullOrEmpty(custName))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE CUSTNAME CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(meterRef) && !custtype.Equals("1"))//exclude flatrate
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE METERREF CANNOT BE EMPTY!";
                }
                else if (String.IsNullOrEmpty(propertyRef))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "THE PHONE NUMBER CANNOT BE EMPTY!";
                }

                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateCustomer", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }
        internal ResponseMessage SaveCustomerDetails(CustomerObj cust)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveCustomerDetails(cust);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveCustomerDetails", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        internal DataTable GetCustomerCategory()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerCategory();

            }
            catch (Exception ex)
            {
                Log("GetCustomerClass", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetApplicationTrackLogs(string appnumber)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetApplicationTrackLogs(appnumber);

            }
            catch (Exception ex)
            {
                Log("GetApplicationTrackLogs", "101 " + ex.Message);
            }
            return dt;
        }

        internal DataTable GetAuditReport(string username, string startdate, string enddate) 
        {
            try
            {
                dt = dh.GetAuditReport(username, startdate, enddate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        internal DataTable GetCustomerReportData(string appnumber, string flag)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerReportData(appnumber, flag);

            }
            catch (Exception ex)
            {
                Log("GetCustomerReportData", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetCompanyProfile(string compainyid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCompanyProfile(compainyid);

            }
            catch (Exception ex)
            {
                Log("GetCompanyProfile", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetLatestBilledReading(string custref, string areaid, string branchid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetLatestBilledReading(custref, areaid, branchid);

            }
            catch (Exception ex)
            {
                Log("GetLatestBilledReading", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage ValidateReadingInquiry(string custref, string propertyref, string areaid)
        {
            ResponseMessage message = new ResponseMessage();
            try
            {

                if (String.IsNullOrEmpty(custref) && String.IsNullOrEmpty(propertyref))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "BOTH CUSTREF AND PROPERTY CANNOT BE EMPTY!";
                }
                else if (!IsValidCustRefRefInArea(custref, areaid))
                {
                    message.Response_Code = "103";
                    message.Response_Message = "WRONG AREA SELECTED FOR CUSTREF!";
                }
                //else if (String.IsNullOrEmpty(propertyref) && !IsValidPropRef(propertyref))
                //{
                //    message.Response_Code = "103";
                //    message.Response_Message = "INVALID PROPERTY FORMAT!";
                //}                
                else
                {
                    message.Response_Code = "0";
                    message.Response_Message = "SUCCESS";

                }

            }
            catch (Exception ex)
            {
                message.Response_Code = "101";
                message.Response_Message = ex.Message;
                Log("ValidateLogin", message.Response_Code + " " + message.Response_Message);

            }
            return message;
        }
        public bool IsValidPropRef(string PropRef)
        {
            if (PropRef.Contains("/"))
            {
                string[] array = PropRef.Split('/');
                if (array.Length == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsValidCustRefRefInArea(string custref, string area)
        {
            bool value = false;
            dt = new DataTable();
            try
            {

                dt = dh.CheckCustRefRefInArea(custref, area);
                if (dt.Rows.Count > 0)
                {
                    value = true;
                }
                else
                {
                    value = true;//value = false;
                }

            }
            catch (Exception ex)
            {
                Log("ValidCustRef", "101 " + ex.Message);
            }
            return value;
        }

        public bool IsCustomerFlat(string custref, string area)
        {
            bool value = false;
            try
            {
                dt = dh.CheckCustRefRefInArea(custref, area);
                string customertype = dt.Rows[0]["custTypeId"].ToString();
                if (customertype.Equals("1"))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log("ValidCustRef", "101 " + ex.Message);
            }
            return value;
        }
        public string GetBillingPeriod(string areaid)
        {
            string output = "0";
            output = dh.GetBillingPeriod(areaid);
            return output;
        }
        internal DataTable GetFieldComments()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetFieldComments();

            }
            catch (Exception ex)
            {
                Log("GetFieldComments", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetSystemUserByRole(string areaid, string roleid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetSystemUserByRole(areaid, roleid);

            }
            catch (Exception ex)
            {
                Log("GetSystemUserByRole", "101 " + ex.Message);
            }
            return dt;
        }
        internal ResponseMessage SaveReading(ReadingObj read)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveReadingDetails(read);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveReadingDetails", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        public DateTime GetDate(string date)
        {
            try
            {
                DateTime dt; int str = 20; int str2 = 20; int newYear = 0;//int newNumber = int.Parse(a.ToString() + b.ToString())
                if (!date.Trim().Equals(""))
                {
                    if (date.Contains("/"))
                    {
                        string[] sDate = date.Split('/');
                        //new date format dd/mm/yyyy e.g 09/02/2021
                        int day = int.Parse(sDate[0].Trim());//reverted 02/03/2021 with format dd/mm/yyyy
                        int month = int.Parse(sDate[1].Trim());
                        // DateTime now = DateTime.Now;                        //
                        //// Write the month integer and then the three-letter month.                        //
                        //string currentmonth = now.Month.ToString();
                        //if (month < int.Parse(currentmonth))//switch positions...added 2nd/3/2021
                        //{
                        //    day = int.Parse(sDate[1].Trim());
                        //    month = int.Parse(sDate[0].Trim());
                        //}
                        //if (month > 12)//switch positions
                        //{
                        //     day = int.Parse(sDate[0].Trim());
                        //     month = int.Parse(sDate[1].Trim());
                        //}
                        int year = int.Parse(sDate[2].Trim());
                        if (sDate[2].Length < 3)
                        {
                            newYear = int.Parse(str.ToString() + sDate[2].Trim().ToString());
                        }
                        else
                        {
                            newYear = year;
                        }
                        if (month < 13)
                        {
                            dt = new DateTime(newYear, month, day);
                        }
                        else
                        {
                            dt = new DateTime(newYear, day, month);
                        }

                    }
                    else
                    {
                        string[] sDate = date.Split('-');
                        string a = sDate[0].Trim();//modified 8/12/2020
                        string x = "";//default value
                        int day = 0;
                        int month = 0;
                        int year = 0;
                        if (a.Length == 4)//date starting with year
                        {
                            day = int.Parse(sDate[2].Trim());
                            month = int.Parse(sDate[1].Trim());
                            year = int.Parse(sDate[0].Trim());
                            x = year.ToString();
                        }
                        else
                        {
                            day = int.Parse(sDate[0].Trim());
                            month = int.Parse(sDate[1].Trim());
                            year = int.Parse(sDate[2].Trim());
                            x = year.ToString();
                        }

                        if (x.Length < 3)
                        {
                            newYear = int.Parse(str.ToString() + sDate[2].Trim().ToString());
                        }
                        else
                        {
                            newYear = year;
                        }
                        dt = new DateTime(newYear, month, day);
                    }
                    //dt = new DateTime(year, month, day);
                }
                else
                {
                    dt = new DateTime(2000, 1, 1);
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetLatestReadingStatus(string custref, string areaid, string branchid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetLatestReadingStatus(custref, areaid, branchid);

            }
            catch (Exception ex)
            {
                Log("GetLatestReadingStatus", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetBillDetails(string area, string branch, string block, string custRef)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBillDetails(area, branch, block, custRef);

            }
            catch (Exception ex)
            {
                Log("GetBillDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal string ProcessBill(CustomerObj cust)
        {
            string output = "";
            try
            {
                string res = "";
                res = BillAccount(cust.CustRef, cust.MeterRef, cust.MeterSize, cust.PropertyRef, cust.Tariff,
               cust.Classification, cust.Area, cust.Branch, cust.CreatedBy, cust.Period, cust.BillDate);
                output = res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        private string BillAccount(string custRef, string meterRef, string meterSize, string propertyRef, string tariff, string classification, string area, string branch, string createdBy, string period, DateTime billDate)
        {
            string output = "";
            try
            {
                string res = "";
                string Period = period;
                if (Period.Equals(""))
                {
                    output = "NO Active Period in the System";
                }
                else
                {
                    DataTable dtReadings = dh.GetAccountReading(custRef, Period, area, branch);
                    int found = Convert.ToInt32(dtReadings.Rows.Count);
                    if (found > 0)
                    {
                        /// Now Bill Account
                        for (int i = 0; i < found; i++)
                        {
                            try
                            {
                                double recordId = double.Parse(dtReadings.Rows[i]["RecordID"].ToString());
                                string Rdg_Type = dtReadings.Rows[i]["type"].ToString();
                                //Get Consumption
                                int Consumption = int.Parse(dtReadings.Rows[i]["Consumption"].ToString());
                                DataTable dataTable = dh.GetBillBasis(custRef, area, branch);
                                if (dataTable.Rows.Count > 0)
                                {

                                    string TarriffCode = dataTable.Rows[0]["tariffId"].ToString();
                                    DateTime billingDate = DateTime.Now;
                                    double OpenBal = Convert.ToDouble(dataTable.Rows[0]["OpenBal"].ToString());
                                    double Bal = Convert.ToDouble(dataTable.Rows[0]["outstandingBalance"].ToString());
                                    bool Sewer = Convert.ToBoolean(dataTable.Rows[0]["IsSewer"].ToString());
                                    bool isvatable = Convert.ToBoolean(dataTable.Rows[0]["isVatable"].ToString());
                                    int CustClassID = Convert.ToInt16(dataTable.Rows[0]["classId"].ToString());
                                    string suppressionstatus = dataTable.Rows[0]["disconnectionId"].ToString();


                                    //if (!Billable)
                                    //{
                                    //    output = "Customer Account is set to not billable";
                                    //}

                                    if (suppressionstatus.Equals("0"))//account inactive
                                    {
                                        //output = "Customer Account is suppressed";
                                        //output = "Customer Account is suppressed but recording";
                                        if (Consumption == 0)
                                        {
                                            output = "SUCCESS";
                                        }
                                        else
                                        {
                                            output = "Customer Account is suppressed but recording with Consumption of: " + Consumption + " for period ( " + Period + " )";
                                        }

                                    }
                                    else
                                    {
                                        output = SaveBillTransaction(TarriffCode, custRef, Sewer, Period, CustClassID, isvatable, createdBy, area, branch, dtReadings, suppressionstatus, recordId, Rdg_Type, meterSize, OpenBal, billDate, meterRef);

                                    }
                                }
                                else
                                {
                                    output = "Failed to get Bill Basis";
                                }
                            }
                            catch (Exception ex)
                            {
                                output = ex.Message;
                            }
                        }
                    }
                    else if(IsFlatRated(custRef))//check flatrate
                    {
                        try
                        {
                            double recordId = 0;
                            string Rdg_Type = "PERIODIC";
                            //Get Consumption
                            int Consumption = 0;
                            DataTable dataTable = dh.GetBillBasis(custRef, area, branch);
                            if (dataTable.Rows.Count > 0)
                            {

                                string TarriffCode = dataTable.Rows[0]["tariffId"].ToString();
                                DateTime billingDate = DateTime.Now;
                                double OpenBal = Convert.ToDouble(dataTable.Rows[0]["OpenBal"].ToString());
                                double Bal = Convert.ToDouble(dataTable.Rows[0]["outstandingBalance"].ToString());
                                bool Sewer = Convert.ToBoolean(dataTable.Rows[0]["IsSewer"].ToString());
                                bool isvatable = Convert.ToBoolean(dataTable.Rows[0]["isVatable"].ToString());
                                int CustClassID = Convert.ToInt16(dataTable.Rows[0]["classId"].ToString());
                                string suppressionstatus = dataTable.Rows[0]["disconnectionId"].ToString();


                               

                                if (suppressionstatus.Equals("0"))//account inactive
                                {                                                                       
                                        output = "Customer Account is suppressed and cannot be billed.";                                  
                                }
                                else
                                {
                                    output = SaveBillTransactionUnmetered(TarriffCode, custRef, Sewer, Period, CustClassID, isvatable, createdBy, area, branch,
                                        suppressionstatus, recordId, Rdg_Type, meterSize, OpenBal, billDate, meterRef,"1");

                                }
                            }
                            else
                            {
                                output = "Failed to get Bill Basis";
                            }
                        }
                        catch(Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        output = "Metered Customer Account does not have any unbilled consumption for period ( " + Period + " )";

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }

        public bool IsFlatRated(string custRef)
        {
            bool value = false;
            dt = new DataTable();
            try
            {

                dt = dh.CheckFlatRated(custRef);
                if (dt.Rows.Count > 0)
                {
                    value = true;
                }

            }
            catch (Exception ex)
            {
                Log("IsFlatRated", "101 " + ex.Message);
            }
            return value;
        }

        private string SaveBillTransaction(string tarriffCode, string custRef, bool sewer, string period, int custClassID, bool isvatable, string createdBy, string area, string branch, DataTable dtReadings, string suppressionstatus, double recordId, string rdgType, string meterSize, double openBal, DateTime billDate, string meterref)
        {
            string output = "";
            try
            {

                TransactionObj trans = new TransactionObj();

                string watercode = "211";
                string servicecode = "231";
                string sewercode = "221";

                //trans.TransCode = WaterTransCode;
                double amt;
                string TariffAmount = GetTransValue(tarriffCode, area);
                if (Double.TryParse(TariffAmount, out amt))
                {
                    foreach (DataRow dr in dtReadings.Rows)
                    {
                        int Basis = Convert.ToInt32(dr["Consumption"].ToString());
                        double rdgRecordId = double.Parse(dr["RecordID"].ToString());
                        string rdgmethod = dr["Method"].ToString();
                        double UnitCost = Convert.ToDouble(TariffAmount);
                        //trans.WVatCode = WVatCode;
                        //trans.SVatCode = SVatCode;
                        //trans.WVatAccount = WVatAccount;
                        //trans.SVatAccount = SVatAccount;
                        //trans.WVatValue = GetVatValue(IsVatable, trans.TransValue, VatAccount);
                        trans.UnitCost = UnitCost;
                        trans.BasisConsumption = Basis;
                        trans.TariffCode = tarriffCode;

                        trans.Period = period;
                        trans.CustRef = custRef;
                        //trans.ChargeType = ChargeType;

                        trans.AreaID = int.Parse(area);
                        trans.BranchID = int.Parse(branch);
                        trans.CreatedBy = int.Parse(createdBy);
                        //trans.Billno = BillNoReturned;
                        trans.ClassID = custClassID;
                        trans.Sewer = sewer;
                        trans.SuppressedCharges = suppressionstatus;
                        trans.RdgRecordId = recordId;
                        trans.IsVatable = isvatable;
                        trans.MeterSize = meterSize;
                        trans.MeterRef = meterref;
                        trans.OpenBal = openBal;//just added
                        trans.Reason = rdgType;
                        trans.RdgType = rdgType;
                        //trans.PayDate = DateTime.Now;
                        trans.PostDate = billDate;
                        trans.InvoiceNumber = "";
                        trans.ReadingMethod = rdgmethod;
                        if (recordId == rdgRecordId)
                        {
                            output = dh.SaveBillTransaction(trans);
                        }
                    }
                }
                else
                {
                    output = "Invalid Tariff Amount";
                }


            
            }
            catch ( Exception ex)
            {
                throw ex;
            }
            return output;
        }

        private string GetTransValue(string tarriffCode, string area)
        {
            string output = "";
            string currentperiod = GetBillingPeriod(area);
            DataTable dtTariff = dh.GetTarrifAmount(tarriffCode);
            if (dtTariff.Rows.Count > 0)
            {
                string Amount = dtTariff.Rows[0]["amount"].ToString();
                bool IsActive = Convert.ToBoolean(dtTariff.Rows[0]["active"].ToString());
                if (IsActive)
                {
                    double transAmount = Convert.ToDouble(Amount);
                    output = transAmount.ToString();
                }
                else
                {
                    output = "Tarrif is not active";
                }
            }
            return output;
        }
        internal DataTable LoadCustomerDisplay(int countryid, int areaid, string custref, int flag)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerDisplay(countryid, areaid, custref,flag);

            }
            catch (Exception ex)
            {
                Log("LoadCustomerDisplay", "101 " + ex.Message);
            }
            return dt;
        }

        internal DataTable GetCustomerDetailsByIDMetered(int countryid, int areaid, string custref, int flag)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerDetailsByIDMetered(countryid, areaid, custref, flag);

            }
            catch (Exception ex)
            {
                Log("LoadCustomerDisplay", "101 " + ex.Message);
            }
            return dt;
        }
        //reading validation added 31/5/2021
        public string GetReadingFilePath(string fileName, string Reader, string fileType, string Area, string Branch)
        {
            //string Direct = "D:\\Billing\\ReadingFiles\\";
            //string Area = HttpContext.Current.Session["area"].ToString();
            //string Branch = HttpContext.Current.Session["BranchName"].ToString();
            string AreaName = Area;
            if (Branch.ToUpper() != "NONE")
            {
                AreaName = Area + "-" + Branch;
            }
            string paramCode = "2";
            string dbStatus = dh.GetSystemParameter(paramCode);
            string ParameterCode = "6";
            string Direct = "";//dal.GetSystemParameter(ParameterCode);
            if (Direct == "")
            {
                Direct = "C:\\Billing\\ReadingFiles\\";

            }
            if (dbStatus.ToUpper().Contains("TEST") && fileType.Equals("MN"))
            {
                Direct = Direct + "ManualReadings\\TestReadings\\";
            }           
            else if (dbStatus.ToUpper().Contains("LIVE") && fileType.Equals("MN"))
            {
                Direct = Direct + "ManualReadings\\LiveReadings\\";
            }
            else if (dbStatus.ToUpper().Contains("TEST") && fileType.Equals("MS"))
            {
                Direct = Direct + "MobileOnSpotReadings\\TestReadings\\";
            }
            else if (dbStatus.ToUpper().Contains("LIVE") && fileType.Equals("MS"))
            {
                Direct = Direct + "MobileOnSpotReadings\\LiveReadings\\";
            }

            
            string DtTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string filePath = Direct + "UnProcessed\\" + AreaName + "\\" + Reader.Replace(" ", "_") + "\\";
            string filepath = filePath + "" + DtTime + "-" + fileName;
            CheckPath(filePath);
            return filepath;
        }
        public void RemoveFile(string Filepath)
        {
            File.Delete(Filepath);
        }
        //public void CheckPath(string Path)
        //{
        //    if (!Directory.Exists(Path))
        //    {
        //        Directory.CreateDirectory(Path);
        //    }
        //}

        public bool IsExtensionAllowed(string fileExtension)
        {
            if (fileExtension.ToUpper() == ".TXT")
            {
                return true;
            }
            else if (fileExtension.ToUpper() == ".CSV")
            {
                return true;
            }           
            else
            {
                return false;
            }
        }
        public bool CheckFileFormat(string file)
        {
            bool output = true;
            DataFile df = new DataFile();
            ArrayList rdgFile = df.readFile(file);
            int failed = 0;
            foreach (string s in rdgFile)
            {
                string[] parameters = s.Split(',');
                int TotalCols = parameters.Length;
                if (TotalCols >= 28)
                {
                    output = true;
                }
                else
                {
                    output = false;
                    failed++;
                }
            }
            if (failed > 0)
            {
                output = false;
            }
            return output;
        }
        internal void SaveFileDetails(int reader, DateTime readingDate, int area, int branch, string filepath, string curPeriod, int capturing, bool processing, bool processed, int failed, int success, bool hasHeader, string fileType)
        {
            try
            {
                dh.SaveFileDetails(reader, readingDate, area,branch,filepath,curPeriod,capturing,processing,processed,failed,success,hasHeader,fileType);
            }
            catch (Exception ex)
            {
                Log("SaveFileDetails", "101 " + ex.Message);
            }
        }
        internal DataTable GetIDList()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetIDList();

            }
            catch (Exception ex)
            {
                Log("GetIDList", "101 " + ex.Message);
            }
            return dt;
        }
        internal bool CheckExistingSerial(string meterno)
        {
            bool value = false;
            dt = new DataTable();
            try
            {

                dt = dh.CheckExistingSerial(meterno);
                if(dt.Rows.Count > 0)
                {
                    value = true;
                }

            }
            catch (Exception ex)
            {
                Log("CheckExistingSerial", "101 " + ex.Message);
            }
            return value;
        }
        internal ResponseMessage SaveMeterInventory(string metertype, string meterserial, string dials, string reading, string life, DateTime manufacturedate, string createdby,bool isactive,string condition)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveMeterInventory(metertype,meterserial,dials,reading,life,manufacturedate,createdby,isactive,condition);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveMeterInventory", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        internal DataTable GetTariffSettings(string areaid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTariffSettings(areaid);

            }
            catch (Exception ex)
            {
                Log("GetTariffSettings", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetGeneralTariffs()
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetGeneralTariffs();

            }
            catch (Exception ex)
            {
                Log("GetGeneralTariffs", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetMeterActivityReasons(string activityid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetMeterActivityReasons(activityid);

            }
            catch (Exception ex)
            {
                Log("GetMeterActivityReasons", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetLatestReadingDetails(string custref, string areaid, string branchid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetLatestReadingDetails(custref, areaid, branchid);

            }
            catch (Exception ex)
            {
                Log("GetLatestReadingDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetTransactionCodes(string flag)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTransactionCodes(flag);

            }
            catch (Exception ex)
            {
                Log("GetTransactionCodes", "101 " + ex.Message);
            }
            return dt;
        }
        internal string GetVATAmount(string countryid, string amount)
        {
            string value = "0";
            //dt = new DataTable();
            try
            {

               
                double vatrate = dh.GetVatRateByCountry(countryid);
                double res = (vatrate * double.Parse(amount))/100;
                value = res.ToString();

            }
            catch (Exception ex)
            {
                Log("GetVATAmount", "101 " + ex.Message);
            }
            return value;
        }
        internal DataTable GetTranscodeDetails(string transcode)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTranscodeDetails(transcode);

            }
            catch (Exception ex)
            {
                Log("GetTranscodeDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal void SaveAdjustmentInceptionLogs(TransactionObj trans, string comment)
        {
            try
            {
                dh.SaveAdjustmentInceptionLogs(trans, comment);
            }
            catch (Exception ex)
            {
                Log("SaveAdjustmentInceptionLogs", "101 " + ex.Message);
            }
        }
        internal string LogMeterRequest(string custref, string meterref, string oldtype, string oldsize, string olddials,string prevrdg, string serial, string prerdgdt,
            string appfnlrdg, string appfnlrdgdt, bool isestimated, string consumption, string reason, 
            string newserial, string newsize, string newmake, string newdials, string manufacturedt, string newlife, string comment, string appinitialrdg,
            string appinitedgdt, string createdby, string requesttype,string areaid, string branchid, string period,string servedby)
        {
            string output = "";
            try
            {
                
                output = dh.SaveMeterRequestLogs(custref, meterref, oldtype, oldsize,olddials, prevrdg, serial, prerdgdt, appfnlrdg, appfnlrdgdt, isestimated,
                    consumption, reason, newserial, newsize, newmake, newdials, manufacturedt, newlife, comment, appinitialrdg, appinitedgdt,
                    createdby, requesttype, areaid, branchid, period,servedby);
            }
            catch (Exception ex)
            {
                Log("LogMeterRequest", "101 " + ex.Message);
            }
            return output;
        }
        internal DataTable GetRequestsToApprove(int countryid, int areaid, string custref)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetRequestsToApprove(countryid, areaid, custref);

            }
            catch (Exception ex)
            {
                Log("GetRequestsToApprove", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetApproverRequestByID(string custref,string recordid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetApproverRequestByID(custref,recordid);

            }
            catch (Exception ex)
            {
                Log("GetApproverRequestByID", "101 " + ex.Message);
            }
            return dt;
        }
        internal string ModifyMeter(string action, string requesttype, string meterRef, string custRef, string serial, 
            string oldReading, string oldRdgDate, string curReading, string curRdgDate1, bool isestimated, string newReading,
            string dials, string installedBy, string curRdgDate2, string type, string size, string life, string manufacturedDate,
            string reason, string area, string branch,string createdby,string period,string finalconsumption,string suppressioncode,
            string approvercomment,string requestid)
        {
            string output = "";
            output = dh.ModifyMeter(action, requesttype, meterRef, custRef, serial,
            oldReading, oldRdgDate, curReading, curRdgDate1, isestimated, newReading,
            dials, installedBy, curRdgDate2, type, size, life, manufacturedDate,
             reason,  area, branch,createdby,period,finalconsumption,suppressioncode,approvercomment,requestid);
           // output = "success";
            return output;
        }
        internal DataTable getCredentialsFile(string area, string branch)
        {
            dt = new DataTable();
            try
            {

                dt = dh.getCredentialsFile(area, branch);

            }
            catch (Exception ex)
            {
                Log("getCredentialsFile", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetTariffFileData(string country)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTariffsFile(country);

            }
            catch (Exception ex)
            {
                Log("GetTariffFileData", "101 " + ex.Message);
            }
            return dt;
        }
        public string GetSystemParameter(string param)
        {
            string output = "";
            output = dh.GetSystemParameter(param);
            return output;
        }
        internal bool CheckJobAssigned(string appid,string flag)
        {
            bool value = false;
            DataTable dt = dh.GetSurveyData(int.Parse(appid));
            if(dt.Rows.Count > 0)
            {
                if(flag.Equals("1"))//job generated
                {
                    value = true;
                }
                else if(flag.Equals("2"))//job assigned
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        string assign = dr["assignedTo"].ToString();
                        if (!assign.Equals(""))
                        {
                            value = true;
                            break;
                        }
                    }
                }
                
            }
            return value;
        }
        public void RemoveCostingItem(string application_code, string CostingCode)
        {
            int appId = int.Parse(application_code);
            int costId = int.Parse(CostingCode);
            dh.RemoveCostingItem(appId, costId);
        }
        internal DataTable GetCostingItemsByCostID(int applicationID,int costid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCostingItemsByCostID(applicationID,costid);

            }
            catch (Exception ex)
            {
                Log("GetCostingItemsByCostID", "101 " + ex.Message);
            }
            return dt;
        }
        private string SaveBillTransactionUnmetered(string tarriffCode, string custRef, bool sewer, string period, int custClassID, bool isvatable, string createdBy, 
            string area, string branch, string suppressionstatus, double recordId, string rdgType, string meterSize, double openBal,
            DateTime billDate, string meterref, string tariffrecord)
        {
            string output = "";
            try
            {

                TransactionObj trans = new TransactionObj();

                string watercode = "211";
                string servicecode = "231";
                string sewercode = "221";

                //trans.TransCode = WaterTransCode;
                double amt;
                string TariffAmount = GetTransValue(tarriffCode, area);
                if (Double.TryParse(TariffAmount, out amt))
                {

                    double UnitCost = Convert.ToDouble(TariffAmount);

                    trans.UnitCost = UnitCost;
                    trans.BasisConsumption = 1;//default to 1 for flatrate
                    trans.TariffCode = tarriffCode;

                    trans.Period = period;
                    trans.CustRef = custRef;
                    //trans.ChargeType = ChargeType;

                    trans.AreaID = int.Parse(area);
                    trans.BranchID = int.Parse(branch);
                    trans.CreatedBy = int.Parse(createdBy);
                    //trans.Billno = BillNoReturned;
                    trans.ClassID = custClassID;
                    trans.Sewer = sewer;
                    trans.SuppressedCharges = suppressionstatus;
                    trans.RdgRecordId = recordId;
                    trans.IsVatable = isvatable;
                    trans.MeterSize = meterSize;
                    trans.MeterRef = meterref;
                    trans.OpenBal = openBal;//just added
                    trans.Reason = rdgType;
                    trans.RdgType = rdgType;
                    //trans.PayDate = DateTime.Now;
                    trans.PostDate = billDate;
                    trans.InvoiceNumber = "";
                    trans.ReadingMethod = "M";

                    output = dh.SaveBillTransactionUnMetered(trans, tariffrecord);

                }
                else
                {
                    output = "Invalid Tariff Amount";
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return output;
        }
        internal DataTable GetReadingSheet(string areaid,string branchid,string block)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetReadingSheet(areaid,branchid,block);

            }
            catch (Exception ex)
            {
                Log("GetReadingSheet", "101 " + ex.Message);
            }
            return dt;
        }
        internal string CallSheetFilling(DataTable dt)
        {
            string filePath = "";
            string fileformat = ".csv";
            filePath = GetTempPath(fileformat);
            StreamWriter streamWriter = File.CreateText(filePath);
            streamWriter.WriteLine(BuildSheetFile(dt));
            streamWriter.Close();
            return filePath;
        }
        private string BuildSheetFile(DataTable dtdata)
        {
            string file = "Sequence,CustRef,PropRef,CustName,Address,SerialNo,Prev RdgDate,Comment,Cur Rdg\r\n";

      
            foreach (DataRow dr in dtdata.Rows)
            {
                string Seq = dr["plotNumber"].ToString();
                string custref = dr["customerRef"].ToString();
                string propRef = dr["PropertyRef"].ToString();
                string custName = dr["customerName"].ToString().Replace(",", "");
                string Address = dr["Address"].ToString().Replace(",", "");
                string Serial = dr["meterNumber"].ToString();
                DateTime PreRdgDate = Convert.ToDateTime(dr["PreRdgDate"].ToString());
                string PrevRdgDate = PreRdgDate.ToString("MM/dd/yyyy");
                file += Seq + "," + custref + "," + propRef + "," + custName + "," + Address + "," + Serial + "," + PrevRdgDate + "\r\n";
            }
            return file;
        }
        public string GetTempPath(string Extensive)
        {
            string ParameterCode = "4";
            string filePath = dh.GetSystemParameter(ParameterCode);
            string subfolder = "Temp";
            string DtTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            //string filePath = Direct + "\\" + AreaName + "\\" + Reader.Replace(" ", "_") + "\\";
            //string filepath = filePath + "" + DtTime + "" + Extensive;
            filePath = filePath + "\\" + subfolder + "\\";
            //string filepath = filePath + "\\"+ subfolder+"\\"+ DtTime + "" + Extensive;
            string filepath = filePath + "" + DtTime + "" + Extensive;
            CheckPath(filePath);
            return filepath;
        }
        internal DataTable GetAccountReading(string custref, string period, string areaid, string branchid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetAccountReading(custref,period,areaid,branchid);

            }
            catch (Exception ex)
            {
                Log("GetAccountReading", "101 " + ex.Message);
            }
            return dt;
        }
        internal void SaveBlockDetails(string code, string country, string area, string branch, string block, string connection, string createdby,
            bool isactive, string status,string oparea)
        {
            try
            {
                dh.SaveBlockDetails(code, country,area,branch,block,connection,createdby,
                    isactive,status,oparea);
            }
            catch (Exception ex)
            {
                Log("SaveBlockDetails", "101 " + ex.Message);
            }
        }
        internal DataTable GetBlockSettingByID(string blockId)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBlockSettingByID(blockId);

            }
            catch (Exception ex)
            {
                Log("GetBlockSettingByID", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetBlockDetails(string area,string branch,string block)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBlockDetails(area,branch,block);

            }
            catch (Exception ex)
            {
                Log("GetBlockDetails", "101 " + ex.Message);
            }
            return dt;
        }
        internal string GetCustomerType(string custref, string area, string branch, string block)
        {
            string output = "";

            dt = new DataTable();
            try
            {

                dt = dh.GetBillDetails(area, branch, block,custref);
                if(dt.Rows.Count > 0)
                {
                    output = dt.Rows[0]["typeName"].ToString();
                }

            }
            catch (Exception ex)
            {
                Log("GetCustomerType", "101 " + ex.Message);
            }
            
            return output;
        }
        internal bool CheckConnectionDetails(string appid,string flag)
        {
            bool value = false;
            DataTable dt = dh.GetFieldConnectionData(int.Parse(appid),int.Parse(flag));
            if (dt.Rows.Count > 0)
            {
                value = true;
            }
            return value;
        }
        internal void UpdateMeterRequestStatus(string custRef, string requestid, string action, string approvercomment, string createdBy, bool iscompleted)
        {
            try
            {
                dh.UpdateMeterRequestStatus(custRef,requestid,action,approvercomment,createdBy,iscompleted);
            }
            catch (Exception ex)
            {
                Log("UpdateMeterRequestStatus", "101 " + ex.Message);
            }
        }
        public string GetFileApplicationPath()
        {
            string Path = dh.GetSystemParameter("5");
            CheckPath(Path);
            return Path;
        }
        public void SaveApplicationFiles(string ApplicationCode, string FilePath, string FileName)
        {
            try
            {
                string userCode = HttpContext.Current.Session["UserID"].ToString();

                string ret = PostAppDocument(ApplicationCode, FilePath, FileName, userCode);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string PostAppDocument(string AppID, string filePath, string fileName, string user_code)
        {
            string output = "";
            if (String.IsNullOrEmpty(AppID))
            {
                output = "Please Provide Application ID";
            }
            else if (String.IsNullOrEmpty(filePath))
            {
                output = "Please Provide Application Document Path";
            }
            else if (String.IsNullOrEmpty(fileName))
            {
                output = "Please Provide Application Document File Name";
            }
            else
            {
                int appId = int.Parse(AppID);
                int user_id = int.Parse(user_code);
                dh.SaveApplicationFile(appId, filePath, fileName, user_id);
                output = "SAVED";
            }
            return output;
        }
        internal DataTable GetFileAttachments(string appid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetFileAttachments(appid);

            }
            catch (Exception ex)
            {
                Log("GetBlockDetails", "101 " + ex.Message);
            }
            return dt;
        }
        public void DeleteApplicationItem(string appid, int recordid, string deletedby)
        {
            try
            {
                dh.DeleteApplicationItem(appid, recordid, deletedby);
            }
            catch (Exception ex)
            {

                //throw ex;
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("DeleteApplicationItem", resp.Response_Code + " " + resp.Response_Message);
            }
        }
        public DateTime GetPeriodEndDate(DateTime StartDate)
        {
            DateTime output;
            int months = 30;
            //output= StartDate.AddMonths(months).AddDays(-1);
            output = new DateTime(StartDate.Year, StartDate.Month, 1).AddMonths(1).AddDays(-1);
            return output;

        }
        public DateTime GetBillPeriodStartDate(int recordcode)
        {
            DateTime output;
            
            output = GetBillPeriodStartDate(recordcode);
            return output;

        }
        public string SaveBillingPeriod(string recordCode, string area_code, DateTime startDate)
        {
            string output = "";
            int RecordID = Convert.ToInt32(recordCode);
            
            string Period = startDate.ToString("yyyyMM");
            string areacode = HttpContext.Current.Session["areaCode"].ToString();
            string PeriodCode = areacode + "" + Period;
            int AreaID = Convert.ToInt16(area_code);
            int CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserID"].ToString());
            if (PeriodExists(Period, AreaID) && recordCode == "0")
            {
                output = "Period Creation Failed, Period " + Period + " already exists";
            }
            else
            {
                dh.SaveBillingPeriod(RecordID, PeriodCode, Period, startDate, AreaID, CreatedBy);
                if (recordCode == "0")
                {
                    output = "Billing Period ( " + Period + " ) Details have been Created Successfully";
                }
                else
                {
                    output = "Billing Period Successfully Closed and Period " + Period + " Successfully Opened";
                }
            }
            return output;

        }

        private bool PeriodExists(string period, int areaID)
        {
            DataTable DTable = dh.CheckAreaPeriod(period, areaID);
            int foundRows = DTable.Rows.Count;
            if (foundRows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal DataTable GetAllBillingPeriod(string areaid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetAllBillingPeriod(areaid);

            }
            catch (Exception ex)
            {
                Log("GetAllBillingPeriod", "101 " + ex.Message);
            }
            return dt;
        }
        //reports
        internal DataTable GetBalanceOutstanding(string branch, DateTime fromdate, DateTime todate, string amount)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetBalanceOutstanding(branch,fromdate,todate,amount);

            }
            catch (Exception ex)
            {
                Log("GetBalanceOutstanding", "101 " + ex.Message);
            }
            return dt;
        }
        public DateTime ReturnDate(string date, int type)
        {
            DateTime dates;

            if (type == 1)
            {

                if (date == "")
                {
                    dates = DateTime.Parse("July 1, 2009");
                }
                else
                {
                    dates = DateTime.Parse(date);
                }
            }
            else
            {
                if (date == "")
                {
                    dates = DateTime.Now;
                }
                else
                {
                    dates = DateTime.Parse(date);
                }
            }

            return dates;
        }
        internal DataTable GetCustomerCount(string branch, string period)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetCustomerCount(branch, period);

            }
            catch (Exception ex)
            {
                Log("GetCustomerCount", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetMeterAudit(string branch, string period)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetMeterAudit(branch, period);

            }
            catch (Exception ex)
            {
                Log("GetMeterAudit", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetTransactionAudit(string branch, string period, string code)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTransactionAudit(branch, period,code);

            }
            catch (Exception ex)
            {
                Log("GetTransactionAudit", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetStatement(string custref, DateTime startdate, DateTime enddate)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetStatement(custref,startdate,enddate);

            }
            catch (Exception ex)
            {
                Log("GetStatement", "101 " + ex.Message);
            }
            return dt;
        }
        //build statement logic 10/11/2021
        public DataTable GetStatementData(DataTable dt)
        {
            try
            {
                string res = "";
                ArrayList a = new ArrayList();

                //string result = "";
                //int count = 0;
                //string space = "   ";
                //int i = 0;
                int rowcount = dt.Rows.Count;
                string balance = "";
                double prev = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    //get accountperiod
                    string PropRef = dr["PropertyRef"].ToString();
                    string Period = dr["Period"].ToString();
                    string date = dr["Date"].ToString();
                    string transactions = dr["Transactions"].ToString();
                    string docno = dr["DocNo"].ToString();
                    string amount = dr["Amount"].ToString();
                    string openbal = dr["OpenBal"].ToString();
                    string consumption = dr["Consumption"].ToString();
                    string selectdate = dr["startDate"].ToString();
                    string custref = dr["customerRef"].ToString();
                    string custname = dr["customerName"].ToString().Replace(",", "");
                    string address = dr["address"].ToString().Replace(",", "");
                    string estimated = dr["Estimated"].ToString();
                    string reading = dr["Reading"].ToString();
                    string meternumber = dr["MeterNumber"].ToString();
                    string enddate = dr["endDate"].ToString();
                    int rowindex = dt.Rows.IndexOf(dr);
                    //get openingbalance
                    if (rowindex == 0)
                    {
                        balance = (double.Parse(openbal) + double.Parse(amount)).ToString();
                        prev = double.Parse(balance);
                    }
                    else
                    {
                        balance = GetRunningBalance(openbal, amount, rowindex, dt, prev);
                        prev = double.Parse(balance);
                        //openbal = "";
                    }
                    //subtotal = Math.Round((double.Parse(custBillTable.Rows[0]["number19"].ToString())), 0, MidpointRounding.AwayFromZero);
                    double newopenbal = Math.Round((double.Parse(openbal)), 0, MidpointRounding.AwayFromZero);
                    double newbalance = Math.Round((double.Parse(balance)), 0, MidpointRounding.AwayFromZero);
                    double newamount = Math.Round((double.Parse(amount)), 0, MidpointRounding.AwayFromZero);
                    res = newopenbal.ToString() + "," + selectdate + "," + custref + "," + custname + "," + PropRef + "," + address + "," + date + "," + transactions + "," + Period + "," + docno + "," + estimated + "," +
                        reading + "," + consumption + "," + newamount + "," + meternumber + "," + newbalance.ToString();
                    a.Add(res);
                    //Console.WriteLine(res);
                    //build pdf
                }
                //add to datatable
                DataTable data = GetStatementData(a);
                //if (data.Rows.Count > 0)
                //{
                //clear to print
                return data;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private DataTable GetStatementData(ArrayList a)
        {
            //create new table
            //DataTable dt = new DataTable("Table2");
            DataTable dt = GetStatementTransactionsDataTable();

            //loop thru string
            foreach (string s in a)
            {
                string[] parameters = Regex.Split(s, ",");
                double openbal = double.Parse(parameters[0].Trim());
                string selectdate = parameters[1].Trim();
                string custref = parameters[2].Trim();
                string custname = parameters[3].Trim();
                string PropRef = parameters[4].Trim();
                string address = parameters[5].Trim();
                string date = parameters[6].Trim();
                string transactions = parameters[7].Trim();
                string Period = parameters[8].Trim();
                string docno = parameters[9].Trim();
                string estimated = parameters[10].Trim();
                string reading = parameters[11].Trim();
                string consumption = parameters[12].Trim();
                double amount = double.Parse(parameters[13].Trim());
                string meternumber = parameters[14].Trim();
                double newbalance = double.Parse(parameters[15].Trim());
                // string enddate = parameters[16].Trim();
                DataRow dr2 = dt.NewRow();
                //dr["No."] = i;
                //dr2["OpenBal"] = openbal.ToString("#,##0");
                //dr2["customerRef"] = custref;
                //  dr2["customerName"] = custname;
                //  dr2["PropertyRef"] = PropRef;
                //  dr2["address"] = address;
                dr2["Date"] = date;
                dr2["Transactions"] = transactions;
                dr2["Period"] = Period;
                dr2["DocNo"] = docno;
                dr2["Estimated"] = estimated;
                dr2["Reading"] = reading;
                dr2["Consumption"] = consumption;
                dr2["Amount"] = amount.ToString("#,##0");
                dr2["MeterNumber"] = meternumber;
                dr2["Balance"] = newbalance.ToString("#,##0");
                // dr2["endDate"] = enddate;
                dt.Rows.Add(dr2);
                dt.AcceptChanges();
            }
            return dt;
        }

        private string GetRunningBalance(string openbal, string amount, int rowindex, DataTable data, double prevbal)
        {
            string output = "";

            double amt = 0;
            if (rowindex == 0)
            {
                output = (double.Parse(openbal) + double.Parse(amount)).ToString();
            }
            else
            {
                //get previous row index
                int prevrowindex = rowindex - 1;
                //get amount against prevrowindex
                //double prevamt = GetPreviousAmount(data, prevrowindex);
                //add to currentamount on index
                //output = (prevamt + double.Parse(amount)).ToString();
                output = (prevbal + double.Parse(amount)).ToString();

            }
            //}
            return output;
        }

        private double GetPreviousAmount(DataTable data, int prevrowindex)
        {
            double output = 0;
            foreach (DataRow row in data.Rows)
            {
                int newprevrow = data.Rows.IndexOf(row);
                if (newprevrow.ToString() == prevrowindex.ToString())
                {
                    output = double.Parse(row["Amount"].ToString());
                    break;
                }
                else
                {
                    continue;
                }
            }
            return output;
        }
        private DataTable GetStatementTransactionsDataTable()
        {
            DataTable dt = new DataTable("Table2");
            // dt.Columns.Add("OpenBal");
            // dt.Columns.Add("startDate");
            // dt.Columns.Add("customerRef");
            // dt.Columns.Add("CustomerName");
            // dt.Columns.Add("PropertyRef");
            // dt.Columns.Add("address");
            dt.Columns.Add("Date");
            dt.Columns.Add("Transactions");
            dt.Columns.Add("Period");
            dt.Columns.Add("DocNo");
            dt.Columns.Add("Estimated");
            dt.Columns.Add("Reading");
            dt.Columns.Add("Consumption");
            dt.Columns.Add("Amount");
            dt.Columns.Add("MeterNumber");
            dt.Columns.Add("Balance");
            // dt.Columns.Add("endDate");
            return dt;
        }
        public bool IsDateTime(string text)
        {
            DateTime dateTime;
            bool isDateTime = false;
            try
            {
                // Check for empty string.
                if (string.IsNullOrEmpty(text))
                {
                    return false;
                }
                else
                {
                    isDateTime = DateTime.TryParse(text, out dateTime);
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }        
   
            return isDateTime;
        }
        public DateTime GetValidDate(string date)
        {
            DateTime output = DateTime.Now;
            try
            {
                //bool valid = IsDateTime(date);
                //if(valid)
                //{
                //    output = Convert.ToDateTime(date);
                //}
                //else
                //{
                //    output = GetDate2(date);
                //}
                output = GetDate2(date);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return output;
        }
        public DateTime GetDate2(string date)
        {
            DateTime dt= DateTime.Now;
            try
            {
                 int str = 20; int str2 = 20; int newYear = 0;//int newNumber = int.Parse(a.ToString() + b.ToString())
                if (!date.Trim().Equals(""))
                {
                    if (date.Contains("/"))
                    {
                        string[] sDate = date.Split('/');
                       
                        int day = int.Parse(sDate[1].Trim());//reverted 02/03/2021 with format dd/mm/yyyy
                        int month = int.Parse(sDate[0].Trim());
                        
                        int year = int.Parse(sDate[2].Trim());
                        if (sDate[2].Length < 3)
                        {
                            newYear = int.Parse(str.ToString() + sDate[2].Trim().ToString());
                        }
                        else
                        {
                            newYear = year;
                        }
                        if (month < 13)
                        {
                            dt = new DateTime(newYear, month, day);
                        }
                        else
                        {
                            dt = new DateTime(newYear, day, month);
                        }

                    }
                    
                    //dt = new DateTime(year, month, day);
                }
                else
                {
                    dt = new DateTime(1900, 1, 1);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetAllUsers_filtered(string search,string area,string branch)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = dh.GetAllUsers_filtered(search,area,branch);

            }
            catch (Exception ex)
            {
                Log("GetAllUsers_filtered", "101 " + ex.Message);
            }
            return dt;
        }
        public string ResetUserPassword(string UserCode, string UserName, string fullName, string changedBy, string action,string email)
        {
            //string output = "";
            string Password = EncryptString(UserName);
            int UserID = Convert.ToInt32(UserCode);

            dh.ResetPassword(UserID, Password, true, changedBy, UserName, action);
            //data.UpdatePassword(UserID, Password, true);
            string application_link = dh.GetSystemParameter("6");
            string message = "  Hello " + fullName;
            string message1 = "  New Billing System Logins have been reset";
            string message2 = "  UserName : " + UserName;
            string message3 = "  Password : " + UserName;
            string message4 = "  Please do not even share your logins with anyone";
            string Actuallink = "<a href= " + application_link + " > Click To Access </a>";
            string message5 = "LINK: " + Actuallink;
            string body = string.Format("<html>" + message + ",<BR>" +
            message1 + ",<BR>" + message2 + "<BR>" + message3 + "<BR>" + message4 + "<BR>" + message5 + "</body>");
            string Subject = "LOGIN CREDENTIAL";
            //Notify_(body, Email, Subject);
            if(!email.Equals(""))
            {
                SendEmail(email, Subject, message1);
            }

            return "Password for " + UserName + " has been reset successfully";
        }
        internal DataTable GetUserStatus()
        {
            DataTable dt = new DataTable();
            try
            {

                dt = dh.GetUserStatus();

            }
            catch (Exception ex)
            {
                Log("GetUserStatus", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetOperationAreaList(int area)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetOperationAreaList(area);

            }
            catch (Exception ex)
            {
                Log("GetOperationAreaList", "101 " + ex.Message);
            }
            return dt;
        }

        internal DataTable GetTerritoryList(int opid,int branchid)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetTerritoryList(opid,branchid);

            }
            catch (Exception ex)
            {
                Log("GetTerritoryList", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetSubTerritoryList(int territory)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetSubTerritoryList(territory);

            }
            catch (Exception ex)
            {
                Log("GetSubTerritoryList", "101 " + ex.Message);
            }
            return dt;
        }
        //added 29/12/2021
        public string GetNewApplicationNumber(string Code, string AreaCode, string BranchCode)
        {
            string output = "";
            if (Code.Equals("0"))
            {
                string SerialCode = "WAPN";
                string Date = DateTime.Now.ToString("ddMMyyyy");
                string areaalias = dh.GetSettingAlias(AreaCode, "1");
                string branchlias = dh.GetSettingAlias(BranchCode, "2");
                int RunningNumber = dh.GetIncrementNumberByArea( int.Parse(AreaCode), int.Parse(BranchCode));
                //int NewNumber = RunningNumber + 1;
                int NewNumber = RunningNumber;
                output = areaalias + "/" + branchlias + "/" + Date + "/" + NewNumber;
            }
            return output;
        }
        internal DataTable GetPaymentTransactionsByDate(int countryid, int areaid, DateTime startdate,DateTime enddate)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetPaymentTransactionsByDate(countryid, areaid,startdate,enddate);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetPaymentTransactions", "101 " + ex.Message);
            }
            return dt;
        }
        internal DataTable GetApplicationByStatusFiltered(string area,string branch, string status,string search,DateTime start, DateTime end)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetApplicationByStatusFiltered( area, branch,status,search,start,end);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetApplicationByStatus", "101 " + ex.Message);
            }
            return dt;
        }
        public ResponseMessage SaveArea(string areaid, string areaname, string code, string alias, bool ckarea)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveArea(areaid, areaname, code, alias, ckarea);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveArea", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        internal DataTable GetSettingsDetails(string flag)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = dh.GetSettingsDetails(flag);
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }


            }
            catch (Exception ex)
            {
                Log("GetSettingsDetails", "101 " + ex.Message);
            }
            return dt;
        }
        public void DeleteSettingItem(string recordid, int flag, string deletedby)
        {
            try
            {
                dh.DeleteSettingItem(recordid, flag, deletedby);
            }
            catch (Exception ex)
            {

                //throw ex;
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("DeleteSettingItem", resp.Response_Code + " " + resp.Response_Message);
            }
        }
        public DataTable GetSettingsDetailsByID(string flag, string recordid)
        {
            DataTable dt = new DataTable();
            try
            {

                dt = dh.GetSettingsDetailsByID(flag, recordid);

            }
            catch (Exception ex)
            {
                Log("GetSettingsDetailsByID", "101 " + ex.Message);
            }
            return dt;
        }
        public ResponseMessage SaveBranch(string branchid, string branchname, string code, string alias, string area, bool ckbranch)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveBranch(branchid, branchname, code, alias, area, ckbranch);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveBranch", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        public ResponseMessage SaveTerritory(string territoryid, string territory, string area, string branch, bool ckterritory)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveTerritory(territoryid, territory, area, branch, ckterritory);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveTerritory", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        public ResponseMessage SaveSubterritory(string subterritoryid, string subterritory, string territoryid, bool cksubter)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = dh.SaveSubTerritory(subterritoryid, subterritory, territoryid, cksubter);
                resp.Response_Code = dt.Rows[0]["Response_Code"].ToString();
                resp.Response_Message = dt.Rows[0]["Response_Desc"].ToString();

            }
            catch (Exception ex)
            {
                resp.Response_Code = "101";
                resp.Response_Message = ex.Message;
                Log("SaveSubterritory", resp.Response_Code + " " + resp.Response_Message);
            }
            return resp;
        }
        internal DataTable GetUserListByID(int countryid, int roleid, int flag)
        {
            dt = new DataTable();
            try
            {

                dt = dh.GetUserListByID(10, roleid, flag);

            }
            catch (Exception ex)
            {
                Log("GetUserListByID", "101 " + ex.Message);
            }
            return dt;
        }
    }
}