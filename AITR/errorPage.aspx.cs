using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class errorpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cancelButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = null;
            HttpContext.Current.Session[Constants.SESSION_IP] = null;
            HttpContext.Current.Session[Constants.SESSION_DATE] = null;
            HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = null;
            HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_EXTRA_QUESTION_ID] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_NEXT_QUESTION_ID] = null;
            
            if ((bool)HttpContext.Current.Session[Constants.SESSION_AUTH])
            {
                Response.Redirect("staffPage.aspx");
            }
            else
            {
                Response.Redirect("startPage.aspx");

            }


        }
    }
}