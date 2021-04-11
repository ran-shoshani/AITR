using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

            //assign the current question id from the session to a local variable so that the correct question can be fetched from the database
            int currentQuestionID = (int)HttpContext.Current.Session[Constants.SESSION_QUESTION_ID];

            Console.Error.WriteLine("current questio id : ", currentQuestionID);
            Console.WriteLine("current questio id : ", currentQuestionID);
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
                    Response.Redirect("errorPage.aspx");
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

                connection.Close();
            }


        }





        /// <summary>
        ///  this fuction check for extra questions 
        ///  store answers in the session
        ///  redirect to the next questions
        ///  create the respondent 
        ///  store answers in the database
        ///  redirect to register page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void nextButton_Click(object sender, EventArgs e)
        {

            // local variables
            int currentQuestionID = (int)HttpContext.Current.Session[Constants.SESSION_QUESTION_ID];
            List<int> extraQuestions = new List<int>();


            // check the session for extra questions and assign it to the local variable
            if (HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] != null)
            {
                extraQuestions = (List<int>)HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS];
            }



            // block of code that runs while there is connection to database
            using (SqlConnection connection = OpenSqlConnection())
            {

                // check the answer for textbox answer 
                TextBox textBox = (TextBox)answerPlaceHolder.FindControl(Constants.TEXTBOX_ANSWER_ID);
                // store the textbox answers with option and question id in the session
                if (textBox != null)
                {
                    // store the answer in session
                    if (textBox.Text.Length > 0)
                    {
                        storeAnswerInSession(textBox.Text, HttpContext.Current.Session[Constants.SESSION_TEXTBOX_OPTION_ID].ToString(), HttpContext.Current.Session[Constants.SESSION_QUESTION_ID].ToString());

                    }



                    // check if there is an extra question id on this option id 
                    // build command to get extra question id
                    SqlCommand getExtraQuestionIdCommand = new SqlCommand(Constants.SQL_QUERY_GET_EXTRA_QUESTION_ID + (int)HttpContext.Current.Session[Constants.SESSION_TEXTBOX_OPTION_ID], connection);
                    SqlDataReader extraQuestionIdReader;

                    

                    try
                    {
                        // execute command
                        extraQuestionIdReader = getExtraQuestionIdCommand.ExecuteReader();

                        // read the row and check if it is null or not
                        if (extraQuestionIdReader.Read())
                        {
                            int extraQuestionIdColumnIndex = extraQuestionIdReader.GetOrdinal(Constants.DB_COLUMN_EXTRA_QUESTION_ID);
                            if (!extraQuestionIdReader.IsDBNull(extraQuestionIdColumnIndex))
                            {
                                

                                if (!extraQuestions.Contains((int)extraQuestionIdReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID]))
                                {
                                    // add the extra question id to the session

                                    extraQuestions.Add((int)extraQuestionIdReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID]);
                                    HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = extraQuestions;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    
                }


                // check the answer for radiobutton answer 
                RadioButtonList radioButtonList = (RadioButtonList)answerPlaceHolder.FindControl(Constants.RADIOBUTTONS_ANSWER_ID);
                // store the radiobutton answers with option and question id in the session
                if (radioButtonList != null)
                {
                    foreach (ListItem radioButton in radioButtonList.Items)
                    {
                        if (radioButton.Selected)
                        {
                            //store answer in session
                            storeAnswerInSession(radioButton.Text, radioButton.Value.ToString(), HttpContext.Current.Session[Constants.SESSION_QUESTION_ID].ToString());


                            // check if there is an extra question id on this option id 
                            // build command to get extra question id
                            SqlCommand getExtraQuestionIdCommand = new SqlCommand(Constants.SQL_QUERY_GET_EXTRA_QUESTION_ID + Int32.Parse(radioButton.Value), connection);
                            SqlDataReader extraQuestionIdReader;



                            try
                            {
                                // execute command
                                extraQuestionIdReader = getExtraQuestionIdCommand.ExecuteReader();

                                // read the row and check if it is null or not
                                if (extraQuestionIdReader.Read())
                                {
                                    int extraQuestionIdColumnIndex = extraQuestionIdReader.GetOrdinal(Constants.DB_COLUMN_EXTRA_QUESTION_ID);
                                    if (!extraQuestionIdReader.IsDBNull(extraQuestionIdColumnIndex))
                                    {

                                        if (!extraQuestions.Contains((int)extraQuestionIdReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID]))
                                        {
                                            // add the extra question id to the session

                                            extraQuestions.Add((int)extraQuestionIdReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID]);
                                            HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = extraQuestions;
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Redirect("errorPage.aspx");
                                throw new Exception(ex.Message);
                            }
                        }
                    }
                }

                // check the answer for checkBoxList answer 
                CheckBoxList checkBoxList = (CheckBoxList)answerPlaceHolder.FindControl(Constants.CHECKBOX_ANSWER_ID);
                // store the checkBoxList answers with option and question id in the session
                if (checkBoxList != null)
                {
                    foreach (ListItem checkBox in checkBoxList.Items)
                    {
                        if (checkBox.Selected)
                        {
                            //store answer in session
                            storeAnswerInSession(checkBox.Text, checkBox.Value.ToString(), HttpContext.Current.Session[Constants.SESSION_QUESTION_ID].ToString());
                            

                            // check if there is an extra question id on this option id 
                            // build command to get extra question id
                            SqlCommand getExtraQuestionIdCommand = new SqlCommand(Constants.SQL_QUERY_GET_EXTRA_QUESTION_ID + Int32.Parse(checkBox.Value), connection);
                            SqlDataReader extraQuestionIdReader;



                            try
                            {
                                // execute command
                                extraQuestionIdReader = getExtraQuestionIdCommand.ExecuteReader();

                                // read the row and check if it is null or not
                                if (extraQuestionIdReader.Read())
                                {
                                    int extraQuestionIdColumnIndex = extraQuestionIdReader.GetOrdinal(Constants.DB_COLUMN_EXTRA_QUESTION_ID);
                                    if (!extraQuestionIdReader.IsDBNull(extraQuestionIdColumnIndex))
                                    {
                                        // add the extra question id to the session
                                        addExtraQuestionIdToSession((int)extraQuestionIdReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID]);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Response.Redirect("errorPage.aspx");
                                throw new Exception(ex.Message);
                            }


                        }
                    }

                }


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
                    Response.Redirect("errorPage.aspx");
                    throw new Exception(ex.Message);
                }
                

                // if the extra question list has items, add the first to the session question id and reload the survey page 
                if (extraQuestions.Count > 0)
                {
                    HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = extraQuestions[0];
                    extraQuestions.RemoveAt(0);
                    HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = extraQuestions;
                    Response.Redirect("surveyPage.aspx");
                    return;
                }

                if (nextQuestionIdReader.Read())
                {
                    //check the next question id column for the value , if null finish the survey
                    int nextQuestionIdColumnIndex = nextQuestionIdReader.GetOrdinal(Constants.DB_QUESTION_TABLE_NEXT_QUESTION_ID);
                    if (nextQuestionIdReader.IsDBNull(nextQuestionIdColumnIndex))
                    {
                        //survey is finished
                        //create respondent
                        createRespondent();
                        //store answers in db
                        storeAnswersInDatabase();
                        Response.Redirect("registerPage.aspx");
                    }
                    else
                    {

                        // assign the next question id to the session questin id
                        int nextQuestion_id = (int)nextQuestionIdReader[Constants.DB_QUESTION_TABLE_NEXT_QUESTION_ID];
                        HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = nextQuestion_id;
                        Response.Redirect("surveyPage.aspx");
                    }
                }
                connection.Close();

            }



        }

        /// <summary>
        /// check the session for previus answers then add new ones to the list
        /// </summary>
        /// <param name="answerText">current answer text</param>
        /// <param name="optionId">current option id</param>
        /// <param name="questionId">current question id</param>
        private static void storeAnswerInSession(String answerText, String optionId, String questionId)
        {

            List<String> answerList = new List<String>();

            // check the session for answers and stores it the local variable 

            if (HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] != null)
            {
                answerList = (List<String>)HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST];
            }

            String answer = answerText + Constants.SESSION_ANSWER_SEPERATOR + optionId + Constants.SESSION_ANSWER_SEPERATOR + questionId;
            answerList.Add(answer);
            HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = answerList;



        }


        /// <summary>
        /// check the database if the option has an extra question id
        /// </summary>
        /// <param name="optionId">option id to check for extra question id<param>
        private static void checkForExtraQuestion(int optionId)
        {


            
            
            using (SqlConnection connection = OpenSqlConnection())
            {
                // build command to get extra question id
                SqlCommand getExtraQuestionIdCommand = new SqlCommand(Constants.SQL_QUERY_GET_EXTRA_QUESTION_ID + optionId, connection);
                SqlDataReader extraQuestionIdReader;

                try
                {
                    // execute command
                    extraQuestionIdReader = getExtraQuestionIdCommand.ExecuteReader();

                    // read the row and check if it is null or not
                    if (extraQuestionIdReader.Read())
                    {
                        int extraQuestionIdColumnIndex = extraQuestionIdReader.GetOrdinal(Constants.DB_COLUMN_EXTRA_QUESTION_ID);
                        if (!extraQuestionIdReader.IsDBNull(extraQuestionIdColumnIndex))
                        {
                            // add the extra question id to the session
                            addExtraQuestionIdToSession((int)extraQuestionIdReader[Constants.DB_COLUMN_EXTRA_QUESTION_ID]);
                        }
                    }
                }
                catch(Exception e)
                {

                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();
                }

               
            }
        }




        /// <summary>
        /// add extra question to session
        /// </summary>
        /// <param name="extraQuestionId">extra id to be added</param>
        private static void addExtraQuestionIdToSession(int extraQuestionId)
        {

            List<int> extraQuestions = new List<int>();

            // check the session for extra questions and assign it to the local variable
            if (HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] != null)
            {
                extraQuestions = (List<int>)HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS];
            }

            extraQuestions.Add(extraQuestionId);
            HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = extraQuestions;

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
            HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = null;
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
        /// create an anonymous respondent with values from the session, and store the returned id in the sessio 
        /// </summary>
        protected void createRespondent()
        {



            using (SqlConnection connection = OpenSqlConnection())
            {

                // create sql commmand for creating a respondent 
                SqlCommand createRespondent = new SqlCommand(Constants.SQL_QUERY_CREATE_RESPONDENT, connection);

                // create parameter for ip address and assign a value from the session
                createRespondent.Parameters.Add(Constants.SQL_PARAMETER_IP_ADDRESS, SqlDbType.VarChar, 32);
                createRespondent.Parameters[Constants.SQL_PARAMETER_IP_ADDRESS].Value = (String)HttpContext.Current.Session[Constants.SESSION_IP]; ;

                // create parameter for date and time and assign a value from the session
                createRespondent.Parameters.Add(Constants.SQL_PARAMETER_SURVEY_DATE, SqlDbType.Date, 3);
                createRespondent.Parameters[Constants.SQL_PARAMETER_SURVEY_DATE].Value = (DateTime)HttpContext.Current.Session[Constants.SESSION_DATE];

                try
                {
                    int respondent_id = (int) createRespondent.ExecuteScalar();
                    HttpContext.Current.Session[Constants.SESSION_RESPONDENT] = respondent_id;
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    connection.Close();
                }

            }



       
        }


        /// <summary>
        /// loop through the answers in the list braek the string into usable parts and store the answers in the database
        /// </summary>
        protected void storeAnswersInDatabase()
        {

            // fetch the list of answers from session
            List<String> answerList = (List<String>)HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST];

            using(SqlConnection connection = OpenSqlConnection())
            {
                // loop through the list 
                foreach(String answer in answerList)
                {

                    // break the answer string down into parts to access its data
                    // seperate the string into three parts : text/value , option id , question id
                    String[] answerParts = answer.Split(Constants.SESSION_ANSWER_SEPERATOR);

                    String typedAnswer = answerParts[0];
                    int option_id = Int32.Parse(answerParts[1]);
                    int question_id = Int32.Parse(answerParts[2]);


                    /// create the sql command to insert the answer
                    SqlCommand insertAnswer = new SqlCommand(Constants.SQL_QUERY_INSERT_ANSWER, connection);

                    insertAnswer.Parameters.Add(Constants.SQL_PARAMETER_TYPED_ANSWER, SqlDbType.Text, 16);
                    insertAnswer.Parameters[Constants.SQL_PARAMETER_TYPED_ANSWER].Value = typedAnswer;

                    insertAnswer.Parameters.Add(Constants.SQL_PARAMETER_RESPONDENT_ID, SqlDbType.Int, 4);
                    insertAnswer.Parameters[Constants.SQL_PARAMETER_RESPONDENT_ID].Value = (int)HttpContext.Current.Session[Constants.SESSION_RESPONDENT];


                    insertAnswer.Parameters.Add(Constants.SQL_PARAMETER_OPTION_ID, SqlDbType.Int ,4);
                    insertAnswer.Parameters[Constants.SQL_PARAMETER_OPTION_ID].Value = option_id;

                    insertAnswer.Parameters.Add(Constants.SQL_PARAMETER_QUESTION_ID, SqlDbType.Int, 4);
                    insertAnswer.Parameters[Constants.SQL_PARAMETER_QUESTION_ID].Value = question_id;


                    // execute command to store the answer row in the database 
                    try
                    {
                        int rowsAffected = insertAnswer.ExecuteNonQuery();
                    }
                    catch(Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    
                    

                    
                }
                // close the connection once the function is  finished
                connection.Close();
            }
        }

    }
}