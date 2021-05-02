using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AITR
{
    public class Constants
    {

        public const String SESSION_EXTRA_QUESTIONS = "extra_questions";
        public const String SESSION_ANSWER_LIST = "answer_list";
        public const char SESSION_ANSWER_SEPERATOR = '±';
        public const String SESSION_TEXTBOX_OPTION_ID = "textbox_option_id";

        public const String SESSION_NEXT_QUESTION_ID = "next_question_id";

        public const String SESSION_QUESTION_ID = "session_question_id";

        public const String SESSION_RESPONDENT = "respondent";
        public const String SESSION_DATE = "today";
        public const String SESSION_IP = "ip_address";
        /*public const String*/
        public const String DB_CONNECTION_STRING = "database_connection";


        public const String DB_COLUMN_QUESTION_ID = "question_id";
        public const String DB_COLUMN_EXTRA_QUESTION_ID = "extra_question_id";
        public const String DB_COLUMN_NEXT_QUESTION_ID = "next_question_id";
        public const String DB_COLUMN_VALUE = "value";
        public const String DB_COLUMN_OPTION_ID = "option_id";
        public const String DB_COLUMN_TYPED_ANSWER = "typed_answer";



        public const String SQL_QUERY_GET_CURRENT_QUESTION_WITH_OPTIONS = "SELECT [question].question_id, [question].text, [question].question_type, [question].next_question_id, [option].option_id, [option].value, [option].extra_question_id FROM [question] INNER JOIN [option] ON [question].question_id = [option].question_id WHERE [question].question_id = ";
        public const String SQL_QUERY_GET_NEXT_QUESTION_ID = "SELECT next_question_id FROM [question] WHERE question_id = ";
        public const String SQL_QUERY_GET_EXTRA_QUESTION_ID = "SELECT extra_question_id FROM [option] WHERE option_id = ";
        public const String SQL_QUERY_CREATE_RESPONDENT = "INSERT INTO [respondent] ([ip_address], [date_of_survey]) VALUES (@ip_address, @date_of_survey); SELECT CAST(scope_identity() as int);";

        public const String SQL_PARAMETER_IP_ADDRESS = "@ip_address";
        public const String SQL_PARAMETER_SURVEY_DATE = "@date_of_survey";

        public const String SQL_QUERY_INSERT_ANSWER = "INSERT INTO [answer] ([typed_answer], [respondent_id], [option_id], [question_id]) VALUES (@typed_answer, @respondent_id, @option_id, @question_id)";
        public const String SQL_PARAMETER_TYPED_ANSWER = "@typed_answer";
        public const String SQL_PARAMETER_RESPONDENT_ID = "@respondent_id";
        public const String SQL_PARAMETER_OPTION_ID = "@option_id";
        public const String SQL_PARAMETER_QUESTION_ID = "@question_id";

        public const String SQL_QUERY_GET_FIRST_QUESTION = "SELECT TOP 1 * FROM [question] ORDER BY question_id ASC; SELECT CAST(scope_identity() as int);";

        // signin page constants
        public const String SQL_QUERY_SIGN_IN = "SELECT * FROM staff WHERE user_name = @user_name AND password = @password";
        public const String SQL_PARAMETER_USER_NAME = "@user_name";
        public const String SQL_PARAMETER_PASSWORD = "@password";
        public const String SESSION_AUTH = "signed_in";


        //  register page constants
        public const String SQL_QUERY_USER_REGISTER = "UPDATE [respondent] SET [first_name] = @first_name, [last_name] = @last_name, [phone_number] = @phone_number , [date_of_birth] = @date_of_birth, [email] = @email, [is_registered] = 1 WHERE [respondent_id] = @respondent_id ";
        public const String SQL_PARAMETER_FIRST_NAME = "@first_name";
        public const String SQL_PARAMETER_LAST_NAME = "@last_name";
        public const String SQL_PARAMETER_PHONE_NUMBER = "@phone_number";
        public const String SQL_PARAMETER_EMAIL = "@email";
        public const String SQL_PARAMETER_DOB = "@date_of_birth";


        // staff page constants
        public const String SQL_QUERY_GET_OPTIONS = "SELECT * FROM [option] WHERE question_id = ";
        public const String SQL_QUERY_GET_DROPDOWN_OPTIONS = "SELECT DISTINCT [typed_answer], [option_id] FROM [answer] WHERE [question_id] = ";
        public const String SQL_QUERY_SEARCH_FIRST_PART = "SELECT * FROM [respondent] WHERE [respondent_id] IN (SELECT [respondent_id] FROM [answer] WHERE [option_id] = ";
        public const String SQL_QUERY_SEARCH_LAST_PART = ") ORDER BY [last_name] ASC";
        public const String SQL_QUERY_SEARCH_MIDDLE_PART = " OR [option_id] = ";


        public const String SQL_QUERY_DROPDOWN_SEARCH_FIRST_PART = "SELECT * FROM [respondent] WHERE [respondent_id] IN (SELECT [respondent_id] FROM [answer] WHERE [typed_answer] LIKE ";
        public const String SQL_QUERY_DROPDOWN_SEARCH_MIDDLE_PART = " OR [typed_answer] LIKE ";
        public const String SQL_QUERY_DROPDOWN_SEARCH_LAST_PART = ") ORDER BY [last_name] ASC";


        public const String SQL_QUERY_TEXT_BOX_SEARCH = "SELECT * FROM [respondent] WHERE [first_name] LIKE @keyword OR [last_name] LIKE @keyword OR [email] LIKE @keyword ORDER BY [last_name] ASC";
       


        public const String SQL_PARAMETER_KEYWORD = "@keyword";   


        // staff page checkbox list and dropdown-list
        public const int    QUESTION_ID_GENDER = 1;
        public const int    QUESTION_ID_AGE = 2;
        public const int QUESTION_ID_SUBURB = 4;
        public const int QUESTION_ID_POSTCODE = 5;

        public const int    QUESTION_ID_BANKS = 6;
        public const int    QUESTION_ID_SERVICES = 7;
        public const int    QUESTION_ID_MAGAZINES = 8;
        public const int    QUESTION_ID_MAGAZINES_SECTIONS = 9;
        public const int    QUESTION_ID_SPORTS = 10;
        public const int    QUESTION_ID_TRAVEL = 11;





        // column names in database table
        public const String DB_QUESTION_TABLE_TEXT = "text";
        public const String DB_QUESTION_TABLE_QUESTION_ID = "question_id";
        public const String DB_QUESTION_TABLE_NEXT_QUESTION_ID = "next_question_id";
        public const String DB_QUESTION_TABLE_QUESTION_TYPE = "question_type";

        public const String DB_OPTION_TABLE_OPTION_ID = "option_id";
        public const String DB_OPTION_TABLE_VALUE = "value";
        public const String DB_OPTION_TABLE_EXTRA_QUESTION_ID = "extra_question_id";


        // surveyPage constants for answers and options
        public const String TEXTBOX_QUESTION = "text_input";
        public const String RADIOBUTTON_QUESTION = "radio_button";
        public const String MULTIPLE_CHOICE_QUESTION = "multiple_choice";
        public const String TEXTBOX_ANSWER_ID = "textbox_answer";
        public const String CHECKBOX_ANSWER_ID = "checkbox_answer";
        public const String RADIOBUTTONS_ANSWER_ID = "radiobutton_answer";



    }
}