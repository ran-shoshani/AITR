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


        /// <summary>
        /// clear session values redirect to start page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// updates respondent table with register user info, validates text boxes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void register_Click(object sender, EventArgs e)
        {

            // warning if first name is missing
            if (string.IsNullOrWhiteSpace(firstName.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "FIRST NAME TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // warning if last name is missing
            else if (string.IsNullOrWhiteSpace(lastName.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "LAST NAME TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // warning if date of birth is missing
            else if (string.IsNullOrWhiteSpace(dob.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "DATE OF BIRTH TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // warning if phone number is missing
            else if (string.IsNullOrWhiteSpace(phoneNumber.Text))
            {
                messageLabel.ForeColor = System.Drawing.Color.Red;
                messageLabel.Text = "PHONE NUMBER TEXT FIELD MUST BE FILLED!!";
                messageLabel.Visible = true;
            }
            // warning if email is missing
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
                    // create command 
                    SqlCommand register = new SqlCommand(Constants.SQL_QUERY_USER_REGISTER, connection);

                    // create parameters 
                    register.Parameters.Add(Constants.SQL_PARAMETER_RESPONDENT_ID, SqlDbType.Int, 4);
                    register.Parameters[Constants.SQL_PARAMETER_RESPONDENT_ID].Value = (int)HttpContext.Current.Session[Constants.SESSION_RESPONDENT];

                    register.Parameters.Add(Constants.SQL_PARAMETER_FIRST_NAME, SqlDbType.VarChar, 64);
                    register.Parameters[Constants.SQL_PARAMETER_FIRST_NAME].Value = firstName.Text.ToLower();

                    register.Parameters.Add(Constants.SQL_PARAMETER_LAST_NAME, SqlDbType.VarChar, 64);
                    register.Parameters[Constants.SQL_PARAMETER_LAST_NAME].Value = lastName.Text.ToLower();

                    register.Parameters.Add(Constants.SQL_PARAMETER_EMAIL, SqlDbType.VarChar, 256);
                    register.Parameters[Constants.SQL_PARAMETER_EMAIL].Value = email.Text.ToLower();

                    register.Parameters.Add(Constants.SQL_PARAMETER_PHONE_NUMBER, SqlDbType.VarChar, 64);
                    register.Parameters[Constants.SQL_PARAMETER_PHONE_NUMBER].Value = phoneNumber.Text;

                    register.Parameters.Add(Constants.SQL_PARAMETER_DOB, SqlDbType.Date, 3);
                    register.Parameters[Constants.SQL_PARAMETER_DOB].Value = DateTime.Parse(dob.Text);

                    int rowsAffected = 0;
                        // execute command 
                    try
                    {
                        rowsAffected = register.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);

                    }
                    // if succesful registration
                    if (rowsAffected != 0)
                    {
                        messageLabel.ForeColor = System.Drawing.Color.Green;
                        messageLabel.Text = "REGISTER SUCCESSFUL";
                        messageLabel.Visible = true;

                        ClearTextBoxes();
                    }


                    connection.Close();
                }

            }


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
        ///  clear  text boxes after succesful registration
        /// </summary>
        private void ClearTextBoxes()
        {
            firstName.Text = null;
            lastName.Text = null;
            dob.Text = null;
            phoneNumber.Text = null;
            email.Text = null;
        }

    }
}