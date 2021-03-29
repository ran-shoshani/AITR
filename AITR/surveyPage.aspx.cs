using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class surveyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            //redirect to start page and clear session 
            HttpContext.Current.Session[Constants.SESSION_ANSWER] = null;
            HttpContext.Current.Session[Constants.SESSION_IP] = null;
            HttpContext.Current.Session[Constants.SESSION_DATE] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_QUESTION_ID] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_EXTRA_QUESTION_ID]= null;
            HttpContext.Current.Session[Constants.DB_COLUMN_NEXT_QUESTION_ID] = null;
            Response.Redirect("startPage.aspx");
        }
    }
}