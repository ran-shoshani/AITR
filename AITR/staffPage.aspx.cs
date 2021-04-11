using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class staffPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((bool)HttpContext.Current.Session[Constants.SESSION_AUTH] == false)
            {
                Response.Redirect("errorPage.aspx");
            }

        }
    }
}