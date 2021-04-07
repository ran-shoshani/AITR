using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class registerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<String> answerListSession = new List<String>();
            answerListSession = (List<String>)HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST];

            RadioButtonList radioButtonList = new RadioButtonList();


            foreach(String answer in answerListSession)
            {
                ListItem answerItem = new ListItem(answer, answer);
                radioButtonList.Items.Add(answerItem);

            }

            AnswerList.Controls.Add(radioButtonList);
        }



    }
}