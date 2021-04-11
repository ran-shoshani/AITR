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
    public partial class startPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            HttpContext.Current.Session[Constants.SESSION_AUTH] = false;


            using (SqlConnection connection = OpenSqlConnection())
            {

                SqlCommand getFirstQuestion = new SqlCommand(Constants.SQL_QUERY_GET_FIRST_QUESTION, connection);

                try
                {
                    int firstQuestionId = (int)getFirstQuestion.ExecuteScalar();
                    HttpContext.Current.Session[Constants.SESSION_QUESTION_ID] = firstQuestionId;
                }
                catch(Exception ex)
                {
                    //redirect to a error page
                    Response.Redirect("errorPage.aspx");

                }
            } 
        }

        protected void startSurveyButton_Click(object sender, EventArgs e)
        {
            // start survey by festching the respondent ip address
            String ip_address = GetIPAddress();

            //store the ip address in session
            HttpContext.Current.Session[Constants.SESSION_IP] = ip_address;

            //store the time stamp of the survey in session
            DateTime date_of_survey = DateTime.Today;
            HttpContext.Current.Session[Constants.SESSION_DATE] = date_of_survey;

            //luanch survey
            Response.Redirect("surveyPage.aspx");

        }



        protected void loginButton_Click(object sender, EventArgs e)
        {
            
            if (signIn())
            {
                Response.Redirect("staffPage.aspx");
            }
          
             
                
            


        }


        protected string GetIPAddress()
        {
            //get IP through PROXY
            //====================
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //should break ipAddress down, but here is what it looks like:
            // return ipAddress;
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] address = ipAddress.Split(',');
                if (address.Length != 0)
                {
                    return address[0];
                }
            }
            //if not proxy, get nice ip, give that back :(
            //ACROSS WEB HTTP REQUEST
            //=======================
            ipAddress = context.Request.UserHostAddress;//ServerVariables["REMOTE_ADDR"];

            if (ipAddress.Trim() == "::1")//ITS LOCAL(either lan or on same machine), CHECK LAN IP INSTEAD
            {
                //This is for Local(LAN) Connected ID Address
                string stringHostName = System.Net.Dns.GetHostName();
                //Get Ip Host Entry
                System.Net.IPHostEntry ipHostEntries = System.Net.Dns.GetHostEntry(stringHostName);
                //Get Ip Address From The Ip Host Entry Address List
                System.Net.IPAddress[] arrIpAddress = ipHostEntries.AddressList;

                try
                {
                    ipAddress = arrIpAddress[1].ToString();
                }
                catch
                {
                    try
                    {
                        ipAddress = arrIpAddress[0].ToString();
                    }
                    catch
                    {
                        try
                        {
                            arrIpAddress = System.Net.Dns.GetHostAddresses(stringHostName);
                            ipAddress = arrIpAddress[0].ToString();
                        }
                        catch
                        {
                            ipAddress = "127.0.0.1";
                        }
                    }
                }
            }
            return ipAddress;
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
        /// contacts the database checks the login parameters
        /// </summary>
        /// <returns> true if match  , false if not </returns>
        protected Boolean signIn()
        {
            using (SqlConnection connection = OpenSqlConnection())
            {
                SqlCommand login = new SqlCommand(Constants.SQL_QUERY_SIGN_IN, connection);

                login.Parameters.Add(Constants.SQL_PARAMETER_USER_NAME, SqlDbType.VarChar, 256);
                login.Parameters[Constants.SQL_PARAMETER_USER_NAME].Value = userNameTextBox.Text;

                login.Parameters.Add(Constants.SQL_PARAMETER_PASSWORD, SqlDbType.VarChar, 256);
                login.Parameters[Constants.SQL_PARAMETER_PASSWORD].Value = userPasswordTextBox.Text;


                try
                {


                    SqlDataReader loginReader = login.ExecuteReader();
                    if (loginReader.HasRows)
                    {
                       HttpContext.Current.Session[Constants.SESSION_AUTH] = true;
                        return true;
                    }
                    else
                    {
                        userNameTextBox.Text = null;
                        userPasswordTextBox.Text = null;
                        return false;
                    }

                }
                catch(Exception ex)
                {
                    Response.Redirect("errorpage.aspx");
                    throw new Exception(ex.Message);
                }

            }
        }


    }


}