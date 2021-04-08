﻿using System;
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



        public const String DB_QUESTION_TABLE_TEXT = "text";
        public const String DB_QUESTION_TABLE_QUESTION_ID = "question_id";
        public const String DB_QUESTION_TABLE_NEXT_QUESTION_ID = "next_question_id";
        public const String DB_QUESTION_TABLE_QUESTION_TYPE = "question_type";

        public const String DB_OPTION_TABLE_OPTION_ID = "option_id";
        public const String DB_OPTION_TABLE_VALUE = "value";
        public const String DB_OPTION_TABLE_EXTRA_QUESTION_ID = "extra_question_id";


        public const String TEXTBOX_QUESTION = "text_input";
        public const String RADIOBUTTON_QUESTION = "radio_button";
        public const String MULTIPLE_CHOICE_QUESTION = "multiple_choice";
        public const String TEXTBOX_ANSWER_ID = "textbox_answer";
        public const String CHECKBOX_ANSWER_ID = "checkbox_answer";
        public const String RADIOBUTTONS_ANSWER_ID = "radiobutton_answer";



    }
}