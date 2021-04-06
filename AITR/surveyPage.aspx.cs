using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

            int currentQuestionID = GetQuestionIDFromSession();
            List<int> extraQuestions = new List<int>();


            // block of code that runs while there is connection to database
            using (SqlConnection connection = OpenSqlConnection())
            {

                // build sql command to fetch question and options from database
                SqlCommand getQuestionWithOptionsCommand = new SqlCommand(Constants.SQL_QUERY_GET_CURRENT_QUESTION_WITH_OPTIONS + currentQuestionID, connection);
                SqlDataReader questionWithOptionsReader;

                // try executing the commmand and put the data in a SqlDataReader
                try
                {
                     questionWithOptionsReader = getQuestionWithOptionsCommand.ExecuteReader();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


                if (questionWithOptionsReader.Read())
                {
                    // turn the value from the qustion text column into a string
                    // then put the question text into the lable on the form
                    String question_text = questionWithOptionsReader[Constants.DB_QUESTION_TABLE_TEXT].ToString();
                    questionLabel.Text = question_text;

                    

                    //turn the value from the question type column into a string
                    // so that we can display the correct element for the answer
                    String question_type = questionWithOptionsReader[Constants.DB_QUESTION_TABLE_QUESTION_TYPE].ToString();

                    
                    // programatically add elements for the answers depending on the type of question 
                    switch (question_type)
                    {
                        case Constants.TEXTBOX_QUESTION:

                            //create a text box and assign it an ID
                            TextBox textBox = new TextBox();
                            textBox.ID = Constants.TEXTBOX_ANSWER_ID;

                            // store the option id value in the session
                            HttpContext.Current.Session[Constants.SESSION_TEXTBOX_OPTION_ID] = (int)questionWithOptionsReader[Constants.DB_OPTION_TABLE_OPTION_ID];
                            
                            // put the text box in the placeholder for the answer
                            answerPlaceHolder.Controls.Add(textBox);
                            break;

                        case Constants.RADIOBUTTON_QUESTION:

                            //create a radiobutton list and assign and ID
                            RadioButtonList radioButtonList = new RadioButtonList();
                            radioButtonList.ID = Constants.RADIOBUTTONS_ANSWER_ID;



                            // add the first row of data to the radio button list
                            ListItem radioButtonOption = new ListItem(questionWithOptionsReader[Constants.DB_OPTION_TABLE_VALUE].ToString(), questionWithOptionsReader[Constants.DB_OPTION_TABLE_OPTION_ID].ToString());
                            radioButtonList.Items.Add(radioButtonOption);
                            // then loop through the rest data create a radio button for each option
                            while (questionWithOptionsReader.Read())
                            {
                               
                                radioButtonOption = new ListItem(questionWithOptionsReader[Constants.DB_OPTION_TABLE_VALUE].ToString(), questionWithOptionsReader[Constants.DB_OPTION_TABLE_OPTION_ID].ToString());
                                radioButtonList.Items.Add(radioButtonOption);
                            }
                            // put the radio button list in the placeholder for the answers
                            answerPlaceHolder.Controls.Add(radioButtonList);

                            break;

                        case Constants.MULTIPLE_CHOICE_QUESTION:

                            //create a checkbox list and assign and ID
                            CheckBoxList checkBoxList = new CheckBoxList();
                            checkBoxList.ID = Constants.CHECKBOX_ANSWER_ID;


                            // add the first row of data to the checkbox list
                            ListItem checkboxOption = new ListItem(questionWithOptionsReader[Constants.DB_OPTION_TABLE_VALUE].ToString(), questionWithOptionsReader[Constants.DB_OPTION_TABLE_OPTION_ID].ToString());
                            checkBoxList.Items.Add(checkboxOption);
                            // then loop through the rest data create a checkbox for each option
                            while (questionWithOptionsReader.Read())
                            {

                                checkboxOption = new ListItem(questionWithOptionsReader[Constants.DB_OPTION_TABLE_VALUE].ToString(), questionWithOptionsReader[Constants.DB_OPTION_TABLE_OPTION_ID].ToString());
                                checkBoxList.Items.Add(checkboxOption);
                            }
                            // put the checkbox list in the placeholder for the answers
                            answerPlaceHolder.Controls.Add(checkBoxList);




                            break;
                    }

                    
                }
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nextButton_Click(object sender, EventArgs e)
        {
           

            int currentQuestionID = GetQuestionIDFromSession();
            
            // block of code that runs while there is connection to database
            using (SqlConnection connection = OpenSqlConnection())
            {

            }
        }



        /// <summary>
        /// takes the user back to start page, clears all the values from session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cancelButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Constants.SESSION_ANSWER] = null;
            HttpContext.Current.Session[Constants.SESSION_IP] = null;
            HttpContext.Current.Session[Constants.SESSION_DATE] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_QUESTION_ID] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_EXTRA_QUESTION_ID]= null;
            HttpContext.Current.Session[Constants.DB_COLUMN_NEXT_QUESTION_ID] = null;
            Response.Redirect("startPage.aspx");
        }



        /// <summary>
        /// setup new connection to database 
        /// </summary>
        /// <returns>open sql connection</returns>
        private static SqlConnection OpenSqlConnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings[Constants.DB_CONNECTION_STRING].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }




        /// <summary>
        /// gets the question id from the session
        /// </summary>
        /// <returns>question id as integer</returns>
        private static int GetQuestionIDFromSession()
        {
            if(HttpContext.Current.Session[Constants.DB_COLUMN_QUESTION_ID] == null)
            {
                HttpContext.Current.Session[Constants.DB_COLUMN_QUESTION_ID] = 1;
            }

            return (int)HttpContext.Current.Session[Constants.DB_COLUMN_QUESTION_ID];
        }




    }
}