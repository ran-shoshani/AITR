using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AITR
{
    public partial class registerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session[Constants.SESSION_ANSWER_LIST] = null;
            HttpContext.Current.Session[Constants.SESSION_IP] = null;
            HttpContext.Current.Session[Constants.SESSION_DATE] = null;
            HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = null;
            HttpContext.Current.Session[Constants.SESSION_EXTRA_QUESTIONS] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_EXTRA_QUESTION_ID] = null;
            HttpContext.Current.Session[Constants.DB_COLUMN_NEXT_QUESTION_ID] = null;
            Response.Redirect("startPage.aspx");
        }

        protected void register_Click(object sender, EventArgs e)
        {

            //bool isIncomplete = Controls.OfType<TextBox>().Any(textbox => string.IsNullOrWhiteSpace(textbox.Text));

            //bool test = Controls.OfType<TextBox>().Any(textbox => string.IsNullOrWhiteSpace(textbox.Text));
            
            // if any textbox is missing values
            
            // if first name is missing
            if (string.IsNullOrWhiteSpace(firstName.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "FIRST NAME TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // if last name is missing
            else if (string.IsNullOrWhiteSpace(lastName.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "LAST NAME TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // if date of birth is missing
            else if (string.IsNullOrWhiteSpace(dob.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "DATE OF BIRTH TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // if phone number is missing
            else if (string.IsNullOrWhiteSpace(phoneNumber.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "PHONE NUMBER TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // if email is missing
            else if (string.IsNullOrWhiteSpace(email.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "EMAIL TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            else
            {
            // all good register user
                using (SqlConnection connection = OpenSqlConnection())
                {
                    SqlCommand register = new SqlCommand(Constants.SQL_QUERY_USER_REGISTER, connection);

                    register.Parameters.Add(Constants.SQL_PARAMETER_RESPONDENT_ID, SqlDbType.Int, 4);
                    register.Parameters[Constants.SQL_PARAMETER_RESPONDENT_ID].Value = (int)HttpContext.Current.Session[Constants.SESSION_RESPONDENT];

                    register.Parameters.Add(Constants.SQL_PARAMETER_FIRST_NAME, SqlDbType.VarChar, 64);
                    register.Parameters[Constants.SQL_PARAMETER_FIRST_NAME].Value = firstName.Text;

                    register.Parameters.Add(Constants.SQL_PARAMETER_LAST_NAME, SqlDbType.VarChar, 64);
                    register.Parameters[Constants.SQL_PARAMETER_LAST_NAME].Value = lastName.Text;


                    register.Parameters.Add(Constants.SQL_PARAMETER_EMAIL, SqlDbType.VarChar, 256);
                    register.Parameters[Constants.SQL_PARAMETER_EMAIL].Value = email.Text;


                    register.Parameters.Add(Constants.SQL_PARAMETER_PHONE_NUMBER, SqlDbType.VarChar, 64);
                    register.Parameters[Constants.SQL_PARAMETER_PHONE_NUMBER].Value = phoneNumber.Text;


                    register.Parameters.Add(Constants.SQL_PARAMETER_DOB, SqlDbType.Date, 3);
                    register.Parameters[Constants.SQL_PARAMETER_DOB].Value = DateTime.Parse(dob.Text);

                    int rowsAffected = 0;

                    try
                    {
                        rowsAffected = register.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);

                    }

                    if (rowsAffected != 0)
                    {
                        messageLabel.ForeColor = System.Drawing.Color.Green;
                        messageLabel.Text = "REGISTER SUCCESSFUL";
                        messageLabel.Visible = true;


                        System.Timers.Timer timer = new System.Timers.Timer();
                        timer.Interval = 5000;

                        timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                        timer.Start();


                    }


                    connection.Close();
                }

            }


        }

            



        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            messageLabel.Visible = false;
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

    }
}