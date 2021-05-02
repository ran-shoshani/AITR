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
    public partial class staffPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if ((bool)HttpContext.Current.Session[Constants.SESSION_AUTH] == false)
            {
                Response.Redirect("errorPage.aspx");
            }

            //populate checkbox lists
            GetGenderOptions(sender, e);
            GetAgeOptions(sender, e);
            GetBanksOptions(sender, e);
            GetBankServicesOptions(sender, e);
            GetMagazinesOptions(sender, e);
            GetMagazineSectionsOptions(sender, e);
            GetSportsOptions(sender, e);
            GetTravelDestinationsOptions(sender, e);
            GetSuburbs(sender, e);
            GetPostcode(sender, e);

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
        /// feches all the genders from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetGenderOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {

                if (genderList.Items.Count == 0)
                {

                    try
                    {
                        // get gender options from database
                        SqlCommand getGenderOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_GENDER, connection);
                        SqlDataReader getGenderOptionsReader = getGenderOptions.ExecuteReader();

                        while (getGenderOptionsReader.Read())
                        {
                            ListItem gender = new ListItem(getGenderOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getGenderOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            genderList.Items.Add(gender);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }


        }



        /// <summary>
        /// feches all the age ranges from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetAgeOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (ageRangeList.Items.Count == 0)
                {

                    try
                    {
                        // get age options from database
                        SqlCommand getAgeOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_AGE, connection);
                        SqlDataReader getAgeOptionsReader = getAgeOptions.ExecuteReader();

                        while (getAgeOptionsReader.Read())
                        {
                            ListItem age = new ListItem(getAgeOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getAgeOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            ageRangeList.Items.Add(age);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }


        }


        /// <summary>
        /// feches all the banks from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetBanksOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (banksList.Items.Count == 0)
                {
                    try
                    {
                        // get bank options from database
                        SqlCommand getBanksOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_BANKS, connection);
                        SqlDataReader getBanksOptionsReader = getBanksOptions.ExecuteReader();

                        while (getBanksOptionsReader.Read())
                        {
                            ListItem bank = new ListItem(getBanksOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getBanksOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            banksList.Items.Add(bank);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }


        }




        /// <summary>
        /// feches all the bank services from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetBankServicesOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (bankServicesList.Items.Count == 0)
                {

                    try
                    {
                        // get bank services options from database
                        SqlCommand getBankServicesOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_SERVICES, connection);
                        SqlDataReader getBankServicesOptionsReader = getBankServicesOptions.ExecuteReader();

                        while (getBankServicesOptionsReader.Read())
                        {
                            ListItem bankServices = new ListItem(getBankServicesOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getBankServicesOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            bankServicesList.Items.Add(bankServices);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }


        }

        /// <summary>
        /// feches all the magazines from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetMagazinesOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (magazinesList.Items.Count == 0)
                {

                    try
                    {
                        // get magazines options from database
                        SqlCommand getMagazinesOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_MAGAZINES, connection);
                        SqlDataReader getMagazinesOptionsReader = getMagazinesOptions.ExecuteReader();

                        while (getMagazinesOptionsReader.Read())
                        {
                            ListItem magazine = new ListItem(getMagazinesOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getMagazinesOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            magazinesList.Items.Add(magazine);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }


        }



        /// <summary>
        /// feches all the magazines sections from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetMagazineSectionsOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (magazinesSectionsList.Items.Count == 0)
                {

                    try
                    {
                        // get magazine sections options from database
                        SqlCommand getMagazineSectionsOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_MAGAZINES_SECTIONS, connection);
                        SqlDataReader getMagazineSectionsOptionsReader = getMagazineSectionsOptions.ExecuteReader();

                        while (getMagazineSectionsOptionsReader.Read())
                        {
                            ListItem magazineSections = new ListItem(getMagazineSectionsOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getMagazineSectionsOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            magazinesSectionsList.Items.Add(magazineSections);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }


        }


        /// <summary>
        /// feches all the sports from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetSportsOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (sportList.Items.Count == 0)
                {

                    try
                    {
                        // get sports options from database
                        SqlCommand getSportsOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_SPORTS, connection);
                        SqlDataReader getSportsOptionsReader = getSportsOptions.ExecuteReader();

                        while (getSportsOptionsReader.Read())
                        {
                            ListItem sports = new ListItem(getSportsOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getSportsOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            sportList.Items.Add(sports);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }


        }

        /// <summary>
        /// feches all the travel destinations from option table
        /// adds them to check box list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetTravelDestinationsOptions(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (travelDestinationsList.Items.Count == 0)
                {

                    try
                    {
                        // get travel options from database
                        SqlCommand getTravelDestinationsOptions = new SqlCommand(Constants.SQL_QUERY_GET_OPTIONS + Constants.QUESTION_ID_TRAVEL, connection);
                        SqlDataReader getTravelDestinationsOptionsReader = getTravelDestinationsOptions.ExecuteReader();

                        while (getTravelDestinationsOptionsReader.Read())
                        {
                            ListItem destination = new ListItem(getTravelDestinationsOptionsReader[Constants.DB_COLUMN_VALUE].ToString(), getTravelDestinationsOptionsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            travelDestinationsList.Items.Add(destination);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }


        }

        /// <summary>
        /// feches all the suburbs from answer table
        /// adds them to drop down list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetSuburbs(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (suburbList.Items.Count == 0)
                {
                    suburbList.Items.Add("Select Suburbs");
                    try
                    {
                        // get suburb options from database
                        SqlCommand getSuburbs = new SqlCommand(Constants.SQL_QUERY_GET_DROPDOWN_OPTIONS + Constants.QUESTION_ID_SUBURB, connection);
                        SqlDataReader getSuburbsReader = getSuburbs.ExecuteReader();

                        while (getSuburbsReader.Read())
                        {
                            ListItem suburb = new ListItem(getSuburbsReader[Constants.DB_COLUMN_TYPED_ANSWER].ToString(), getSuburbsReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            suburbList.Items.Add(suburb);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                }
            }


        }


        /// <summary>
        /// feches all the postcodes from answer table
        /// adds them to drop down list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GetPostcode(object sender, EventArgs e)
        {


            using (SqlConnection connection = OpenSqlConnection())
            {
                if (postcodeList.Items.Count == 0)
                {
                    postcodeList.Items.Add("select Post Code");
                    try
                    {
                        // get age options from database
                        SqlCommand getPostcode = new SqlCommand(Constants.SQL_QUERY_GET_DROPDOWN_OPTIONS + Constants.QUESTION_ID_POSTCODE, connection);
                        SqlDataReader getPostcodeReader = getPostcode.ExecuteReader();

                        while (getPostcodeReader.Read())
                        {
                            ListItem postcode = new ListItem(getPostcodeReader[Constants.DB_COLUMN_TYPED_ANSWER].ToString(), getPostcodeReader[Constants.DB_COLUMN_OPTION_ID].ToString());
                            postcodeList.Items.Add(postcode);

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Redirect("errorPage.aspx");
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                }

            }


        }


        /// <summary>
        ///  search button searches all the checked items in the checkbox list and add them to a query
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void searchWithFiltersButton_Click(object sender, EventArgs e)
        {
            String queryOptions = null;

            foreach (ListItem item in genderList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in ageRangeList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in banksList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in bankServicesList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in magazinesList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in magazinesSectionsList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in sportList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }
            foreach (ListItem item in travelDestinationsList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = item.Value;
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_SEARCH_MIDDLE_PART + item.Value;
                    }
                }
            }


            using (SqlConnection connection = OpenSqlConnection())
            {
                try
                {
                    SqlCommand getSearchResult = new SqlCommand(Constants.SQL_QUERY_SEARCH_FIRST_PART + queryOptions + Constants.SQL_QUERY_SEARCH_LAST_PART, connection);

                    SqlDataAdapter searchResultDataAdapter = new SqlDataAdapter(getSearchResult);

                    DataTable searchResultTable = new DataTable();
                    searchResultDataAdapter.Fill(searchResultTable);

                    gridViewResults.DataSource = searchResultTable;
                    gridViewResults.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("errorPage.aspx");
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }



            }


        }
        /// <summary>
        /// filters answers from answer table based on drop down selection
        /// return respondents in grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropDownsearchButton_Click(object sender, EventArgs e)
        {
            String queryOptions = null;

            foreach (ListItem item in suburbList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = "'" + item.Text + "'";
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_DROPDOWN_SEARCH_MIDDLE_PART + "'" + item.Text + "'";
                    }
                }
            }
            foreach (ListItem item in postcodeList.Items)
            {
                if (item.Selected)
                {
                    if (String.IsNullOrEmpty(queryOptions))
                    {
                        queryOptions = "'" + item.Text + "'";
                    }
                    else
                    {
                        queryOptions += Constants.SQL_QUERY_DROPDOWN_SEARCH_MIDDLE_PART + "'" + item.Text + "'";
                    }
                }
            }

            using (SqlConnection connection = OpenSqlConnection())
            {
                try
                {
                    SqlCommand getSearchResult = new SqlCommand(Constants.SQL_QUERY_DROPDOWN_SEARCH_FIRST_PART + queryOptions + Constants.SQL_QUERY_DROPDOWN_SEARCH_LAST_PART, connection);

                    SqlDataAdapter searchResultDataAdapter = new SqlDataAdapter(getSearchResult);

                    DataTable searchResultTable = new DataTable();
                    searchResultDataAdapter.Fill(searchResultTable);

                    gridViewResults.DataSource = searchResultTable;
                    gridViewResults.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("errorPage.aspx");
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }

            }
        }








        /// <summary>
        ///  clears all selected values in all check boxes and dropdowns 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clearFilterButton_Click(object sender, EventArgs e)
        {
            genderList.ClearSelection();
            ageRangeList.ClearSelection();


            banksList.ClearSelection();
            bankServicesList.ClearSelection();

            magazinesList.ClearSelection();
            magazinesSectionsList.ClearSelection();

            sportList.ClearSelection();
            travelDestinationsList.ClearSelection();

            postcodeList.SelectedIndex = 0;
            suburbList.SelectedIndex = 0;
        }






        /// <summary>
        /// search first name last name and email in respondent table with values from text box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void textboxSearchButton_Click(object sender, EventArgs e)
        {
            // take value from text box and add wild cards (% = zero or more characters)
            // converts string into lowercase
            String keyword = "%" + searchTextbox.Text.ToLower() + "%";

            using (SqlConnection connection = OpenSqlConnection())
            {
                try
                {   // biuld search query command 
                    SqlCommand textboxSearch = new SqlCommand(Constants.SQL_QUERY_TEXT_BOX_SEARCH, connection);
                    
                    //turn text box value into parameter to avoid sql injection
                    textboxSearch.Parameters.Add(new SqlParameter(Constants.SQL_PARAMETER_KEYWORD, keyword));

                    // execute command and put results in grid view
                    SqlDataAdapter searchResultDataAdapter = new SqlDataAdapter(textboxSearch);

                    DataTable searchResultTable = new DataTable();
                    searchResultDataAdapter.Fill(searchResultTable);

                    gridViewResults.DataSource = searchResultTable;
                    gridViewResults.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Redirect("errorPage.aspx");
                    throw new Exception(ex.Message);
                }
                finally
                {
                    connection.Close();
                }



            }
        }







        /// <summary>
        /// clears the data grid view from results
        /// set session auth to false
        /// redirect to start page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void logoutButton_Click(object sender, EventArgs e)
        {
            gridViewResults.DataSource = null;
            gridViewResults.DataBind();

            HttpContext.Current.Session[Constants.SESSION_AUTH] = false;
            Response.Redirect("startPage.aspx");
        }




        /// <summary>
        ///  clear the data grid view from results
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void clearResultsButton_Click(object sender, EventArgs e)
        {
            gridViewResults.DataSource = null;
            gridViewResults.DataBind();
        }
    }
}