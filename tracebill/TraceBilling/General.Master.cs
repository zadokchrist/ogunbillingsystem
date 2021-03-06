using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TraceBilling
{
    public partial class General : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["RoleID"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                string fullname = Session["FullName"].ToString();
                string title = Session["Designation"].ToString();
                string role = Session["roleName"].ToString();
                string str = "";
                if(!title.Equals(""))
                {
                    str = role + "(" + title + ")";
                }
                else
                {
                    str = role;
                }
                userName.InnerHtml = "" + fullname;
                jtitle.InnerText = str;
                username2.InnerHtml = "" + fullname;
                username3.InnerHtml = "" + fullname;
                jtitle2.InnerText = str;
            }
            catch(Exception ex)
            {
                // throw ex;
                ClearSession();
            }
            
        }

        private void ClearSession()
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}