using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TraceBilling.EntityObjects
{
    public class ResponseMessage
    {
        private string respcode = "", respmessage = "";
        public string Response_Code
        {
            get { return respcode; }
            set { respcode = value; }
        }
        public string Response_Message
        {
            get { return respmessage; }
            set { respmessage = value; }
        }
    }
}