using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TraceBilling.ControlObjects;

namespace TraceBilling
{
    public partial class Logout : System.Web.UI.Page
    {
        BusinessLogic bll = new BusinessLogic();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RoleID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            bll.RecordAudittrail(Session["userName"].ToString(), "Logged out of the system");
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}