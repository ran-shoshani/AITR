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

            // local variables
            int currentQuestionID = GetQuestionIDFromSession();
            List<int> extraQuestions = new List<int>();
            List<String> answerList = new List<String>();


            // check the session for answers and stores it the local variable 
            if(GetSessionAnswerList().Count != 0)
            {
                answerList.AddRange(GetSessionAnswerList());
            }

            


            if (HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] != null)
            {
                extraQuestions = (List<int>)HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS];
            }


            // block of code that runs while there is connection to database
            using (SqlConnection connection = OpenSqlConnection())
            {

                // build sql command to fetch next question 
                //
                SqlCommand getNextQuestionIdCommand = new SqlCommand(Constants.SQL_QUERY_GET_NEXT_QUESTION_ID + currentQuestionID, connection);
                SqlDataReader nextQuestionIdReader;

                // try executing the commmand and put the data in a SqlDataReader
                try
                {
                    nextQuestionIdReader = getNextQuestionIdCommand.ExecuteReader();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }


                // check the answer for textbox answer 
                TextBox textBox = (TextBox)answerPlaceHolder.FindControl(Constants.TEXTBOX_ANSWER_ID);
                // store the textbox answers with option and question id in the session
                if (textBox != null)
                {
                    String answer = textBox.Text + Constants.SESSION_ANSWER_SEPERATOR + (int)HttpContext.Current.Session[Constants.SESSION_TEXTBOX_OPTION_ID] + Constants.SESSION_ANSWER_SEPERATOR  + (int)HttpContext.Current.Session[Constants.SESSION_QUESTION_ID];
                    answerList.Add(answer);
                    HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = answerList;
                    
                    // check if 

                    
                }

                // check the answer for radiobutton answer 
                RadioButtonList radioButtonList = (RadioButtonList)answerPlaceHolder.FindControl(Constants.RADIOBUTTONS_ANSWER_ID);
                // store the radiobutton answers with option and question id in the session
                if (radioButtonList != null)
                {
                    foreach(ListItem radioButton in radioButtonList.Items)
                    {
                        if (radioButton.Selected)
                        {
                            String answer = radioButton.Text + Constants.SESSION_ANSWER_SEPERATOR + radioButton.Value + Constants.SESSION_ANSWER_SEPERATOR + (int)HttpContext.Current.Session[Constants.SESSION_QUESTION_ID];
                            answerList.Add(answer);
                            HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = answerList;
                        }
                    }
                }

                // check the answer for checkBoxList answer 
                CheckBoxList checkBoxList = (CheckBoxList)answerPlaceHolder.FindControl(Constants.CHECKBOX_ANSWER_ID);
                // store the checkBoxList answers with option and question id in the session
                if(checkBoxList != null)
                {
                    foreach(ListItem checkBox in checkBoxList.Items)
                    {
                        if (checkBox.Selected)
                        {
                            String answer = checkBox.Text + Constants.SESSION_ANSWER_SEPERATOR + checkBox.Value + Constants.SESSION_ANSWER_SEPERATOR + (int)HttpContext.Current.Session[Constants.SESSION_QUESTION_ID];
                            answerList.Add(answer);
                            HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = answerList;
                        }
                    }

                }



                if (extraQuestions.Count > 0)
                {
                    HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = extraQuestions[0];
                    extraQuestions.RemoveAt(0);
                    HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = extraQuestions;
                    Response.Redirect("surveyPage.aspx");
                }



                if (nextQuestionIdReader.Read())
                {
                    int nextQuestionIdColumnIndex = nextQuestionIdReader.GetOrdinal(Constants.DB_QUESTION_TABLE_NEXT_QUESTION_ID);
                    if (nextQuestionIdReader.IsDBNull(nextQuestionIdColumnIndex))
                    {
                        //survey is finished
                        //create respondent
                        //store answers in db
                        Response.Redirect("registerPage.aspx");
                    }
                    else
                    {
                        int nextQuestion_id = (int)nextQuestionIdReader[Constants.DB_QUESTION_TABLE_NEXT_QUESTION_ID];
                        HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = nextQuestion_id;
                        Response.Redirect("surveyPage.aspx");
                    }
                }
            }



                /*
                 List<int> extraQuestionsList = new List<int>();
                int extraQuestionIdIndex;


                // first row of the results, where we have access to next question id & a value for the extra question, possibly null
                        // we can save the next question id to session here if it exists

                        // check if the next question id has a value or not
                        int nextQuestionColumnIndex = questionWithOptionsReader.GetOrdinal(Constants.DB_QUESTION_TABLE_NEXT_QUESTION_ID);
                        if (!questionWithOptionsReader.IsDBNull(nextQuestionColumnIndex))
                        {
                            // save the next question id in the session
                            HttpContext.Current.Session[Constants.SESSION_NEXT_QUESTION_ID] = (int)questionWithOptionsReader[Constants.DB_QUESTION_TABLE_NEXT_QUESTION_ID];
                        }
                        else
                        {
                            // if there is no value in the reader, set the session to null 
                            HttpContext.Current.Session[Constants.SESSION_NEXT_QUESTION_ID] = null;
                        }




                // check if this row of the results has an extra question value, if so add it to the list?
                                extraQuestionIdIndex = questionWithOptionsReader.GetOrdinal(Constants.DB_OPTION_TABLE_EXTRA_QUESTION_ID);
                                if (!questionWithOptionsReader.IsDBNull(extraQuestionIdIndex))
                                {
                                    int next_question_id = (int)questionWithOptionsReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID];

                                    if (!extraQuestionsList.Contains(next_question_id))
                                    {
                                        extraQuestionsList.Add(next_question_id);
                                    }
                                }



                 //store the list of extra questions to the session here?
                                HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = extraQuestionsList;

                 */

        }




        /// <summary>
        /// takes the user back to start page, clears all the values from session
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cancelButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = null;
            HttpContext.Current.Session[Constants.SESSION_IP] = null;
            HttpContext.Current.Session[Constants.SESSION_DATE] = null;
            HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = null;
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
            if(HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] == null)
            {
                HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = 1;
            }

            return (int)HttpContext.Current.Session[Constants.SESSION_QUESTION_ID];
        }


        
        private static List<String> GetSessionAnswerList()
        {
            if(HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] == null)
            {
                List<String> answer_list = new List<String>();
                HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = answer_list;
            }
            return (List<String>)HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST];
        }

    }
}