/****************************** Module DataAccess ******************************\
* Module Name:  DataAccess.cs
* Project:      Survey App
* Date:         28 August, 2013
* Copyright (c) Vikram Singh Saini       
* 
* Provide way for connecting to database and executing queries and script files.
* 
* THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
* EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
* WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
\***************************************************************************/

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;

namespace SurveyApp.Code
{
    /// <summary>
    /// Custom class for database tasks.
    /// </summary>
    class DataAccess
    {
        private static bool _queryResult;
        private static SqlConnection _sqlCon;
        private static SqlCommand _sqlCmd;

        #region Execute query and save data

        /// <summary>
        /// Save all information in database by Stored Procedure.
        /// </summary>
        /// <returns>Returns 'true' if succeeds else 'false'</returns>
        public static bool ExecuteQuery(string personName, long? phone, string priorAddress, string currAddress, long? peopleInHouse, string attorneyCanContact, string q1A1, string q2A2, string q3A1, string q3A2, string q3A3, string q3A4, string q3A5, string q3A6, string q3A7, string q3A8, string q3A8T, string q4A1, string q4A2, string q4A3, string q4A4, string q4A4T, string q5A1, long? q5A1T, string q5A2, long? q5A2T, string q5A3, long? q5A3T, string q5A4, long? q5A4T, string q5A5, long? q5A5T, string q5A6, long? q5A6T, string q5A7, long? q5A7T, string q5A8, long? q5A8T, string q5A9, string q6A1, string q6A2, string q6A3, string q6A4, string q6A5, string q6A6, string q6A7, string q6A8, string q7A7, string q8A1, long? q8A1T, string q8A2, long? q8A2T, string q8A3, long? q8A3T, string q8A4, long? q8A4T, string q8A5, long? q8A5T, string q8A6, long? q8A6T, string q8A7, string q8A7T, DateTime q9A9, string q10A10, long? q10A10T, string q11A11, string q12A12, long? q12A12T, string q13A13, string q14A14, string q15A1, long? q15A1T, string q15A2, long? q15A2T, string q15A3, long? q15A3T, string q15A4, long? q15A4T, string q16A16, string q16A16T, string q17A17, string q18A18, string q19A19, string q20A20, string q21A21, string q22A22)
        {
            try
            {
                using (_sqlCon = new SqlConnection(Utility.ConnectionString))
                {
                    _sqlCmd = new SqlCommand { CommandText = "SaveData", Connection = _sqlCon, CommandType = CommandType.StoredProcedure };

                    AddParameters("@PersonName", personName);
                    AddParameters("@Phone", phone);
                    AddParameters("@PriorAddress", priorAddress);
                    AddParameters("@CurrentAddress", currAddress);
                    AddParameters("@PeopleInHouse", peopleInHouse);
                    AddParameters("@AttorneyCanContact", attorneyCanContact);

                    AddParameters("@Q1A1", q1A1);
                    AddParameters("@Q2A2", q2A2);

                    AddParameters("@Q3A1", q3A1);
                    AddParameters("@Q3A2", q3A2);
                    AddParameters("@Q3A3", q3A3);
                    AddParameters("@Q3A4", q3A4);
                    AddParameters("@Q3A5", q3A5);
                    AddParameters("@Q3A6", q3A6);
                    AddParameters("@Q3A7", q3A7);
                    AddParameters("@Q3A8", q3A8);
                    AddParameters("@Q3A8T", q3A8T);

                    AddParameters("@Q4A1", q4A1);
                    AddParameters("@Q4A2", q4A2);
                    AddParameters("@Q4A3", q4A3);
                    AddParameters("@Q4A4", q4A4);
                    AddParameters("@Q4A4T", q4A4T);

                    AddParameters("@Q5A1", q5A1);
                    AddParameters("@Q5A1T", q5A1T);
                    AddParameters("@Q5A2", q5A2);
                    AddParameters("@Q5A2T", q5A2T);
                    AddParameters("@Q5A3", q5A3);
                    AddParameters("@Q5A3T", q5A3T);
                    AddParameters("@Q5A4", q5A4);
                    AddParameters("@Q5A4T", q5A4T);
                    AddParameters("@Q5A5", q5A5);
                    AddParameters("@Q5A5T", q5A5T);
                    AddParameters("@Q5A6", q5A6);
                    AddParameters("@Q5A6T", q5A6T);
                    AddParameters("@Q5A7", q5A7);
                    AddParameters("@Q5A7T", q5A7T);
                    AddParameters("@Q5A8", q5A8);
                    AddParameters("@Q5A8T", q5A8T);
                    AddParameters("@Q5A9", q5A9);

                    AddParameters("@Q6A1", q6A1);
                    AddParameters("@Q6A2", q6A2);
                    AddParameters("@Q6A3", q6A3);
                    AddParameters("@Q6A4", q6A4);
                    AddParameters("@Q6A5", q6A5);
                    AddParameters("@Q6A6", q6A6);
                    AddParameters("@Q6A7", q6A7);
                    AddParameters("@Q6A8", q6A8);

                    AddParameters("@Q7A7", q7A7);

                    AddParameters("@Q8A1", q8A1);
                    AddParameters("@Q8A1T", q8A1T);
                    AddParameters("@Q8A2", q8A2);
                    AddParameters("@Q8A2T", q8A2T);
                    AddParameters("@Q8A3", q8A3);
                    AddParameters("@Q8A3T", q8A3T);
                    AddParameters("@Q8A4", q8A4);
                    AddParameters("@Q8A4T", q8A4T);
                    AddParameters("@Q8A5", q8A5);
                    AddParameters("@Q8A5T", q8A5T);
                    AddParameters("@Q8A6", q8A6);
                    AddParameters("@Q8A6T", q8A6T);
                    AddParameters("@Q8A7", q8A7);
                    AddParameters("@Q8A7T", q8A7T);

                    _sqlCmd.Parameters.AddWithValue("@Q9A9", q9A9);

                    AddParameters("@Q10A10", q10A10);
                    AddParameters("@Q10A10T", q10A10T);

                    AddParameters("@Q11A11", q11A11);

                    AddParameters("@Q12A12", q12A12);
                    AddParameters("@Q12A12T", q12A12T);

                    AddParameters("@Q13A13", q13A13);
                    AddParameters("@Q14A14", q14A14);

                    AddParameters("@Q15A1", q15A1);
                    AddParameters("@Q15A1T", q15A1T);
                    AddParameters("@Q15A2", q15A2);
                    AddParameters("@Q15A2T", q15A2T);
                    AddParameters("@Q15A3", q15A3);
                    AddParameters("@Q15A3T", q15A3T);
                    AddParameters("@Q15A4", q15A4);
                    AddParameters("@Q15A4T", q15A4T);

                    AddParameters("@Q16A16", q16A16);
                    AddParameters("@Q16A16T", q16A16T);

                    AddParameters("@Q17A17", q17A17);
                    AddParameters("@Q18A18", q18A18);
                    AddParameters("@Q19A19", q19A19);
                    AddParameters("@Q20A20", q20A20);
                    AddParameters("@Q21A21", q21A21);
                    AddParameters("@Q22A22", q22A22);

                    _sqlCmd.Connection.Open();
                    _queryResult = _sqlCmd.ExecuteNonQuery() > 0;
                    _sqlCmd.Connection.Close();
                }
            }
            catch (SqlException exc)
            {
                if (_sqlCon.State.Equals(ConnectionState.Closed))
                {
                    var builder = new StringBuilder("EXEC SaveData ");
                    var paramCount = _sqlCmd.Parameters.Count;

                    for (var i = 0; i < paramCount; i++)
                    {
                        if (_sqlCmd.Parameters[i].SqlDbType.Equals(SqlDbType.BigInt) || _sqlCmd.Parameters[i].Value.Equals(DBNull.Value))
                            builder.Append(_sqlCmd.Parameters[i].SqlValue).Append(i < paramCount - 1 ? "," : "");
                        else
                            builder.Append("'").Append(_sqlCmd.Parameters[i].SqlValue).Append("'").Append(i < paramCount - 1 ? "," : "");
                    }

                    var query = builder.ToString();

                    Utility.SaveToScriptFile(query);
                    Utility.WriteError(exc);

                    return _queryResult;
                }
            }

            return _queryResult;
        }

        #endregion

        #region Execute script to server

        public static void ExecuteQuery(string filePath)
        {
            var form = Application.OpenForms["FrmMain"];

            try
            {
                string script;
                using (var reader = new StreamReader(filePath))
                {
                    script = reader.ReadToEnd();
                }

                using (var con = new SqlConnection(Utility.ConnectionString))
                {
                    con.Open();
                    using (var trans = con.BeginTransaction())
                    {
                        using (var cmd = new SqlCommand(script, con, trans))
                        {
                            var result = cmd.ExecuteNonQuery() > 0;
                            trans.Commit();

                            if (result)
                            {
                                Utility.ClearScriptFile();
                                RadMessageBox.Show(form, "Executed script and created records successfully.", "Data saved", MessageBoxButtons.OK, RadMessageIcon.Info);
                            }
                            else
                            {
                                RadMessageBox.Show(form, "Unable to execute scripts.", "Data saving failed", MessageBoxButtons.OK, RadMessageIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (FileNotFoundException exc)
            {
                RollBackExecution(exc);
                RadMessageBox.Show(form, filePath + " not found.\n\n *Ensure file with such name exists or not.", "Script file not found", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
            catch (InvalidOperationException exc)
            {
                if (exc.Message.Contains("Invalid operation. The connection is closed."))
                {
                    RollBackExecution(exc);
                    RadMessageBox.Show(form, "Survey App cannot connect to SQL Server.\n\n *Please check if server is not down.", "SQL Server inaccessible", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
                else
                {
                    RollBackExecution(exc);
                    RadMessageBox.Show(form, "An unhandled error occured.\n\n *An entry of error had been made in log file. Consult your vendor for further assistance.", "Ooops!", MessageBoxButtons.OK, RadMessageIcon.Error);
                }
            }
            catch (Exception exc)
            {
                RollBackExecution(exc);
                RadMessageBox.Show(form, "An unhandled error occured.\n\n *An entry of error had been made in log file. Consult your vendor for further assistance.", "Whoops!", MessageBoxButtons.OK, RadMessageIcon.Error);
            }
        }

        private static void RollBackExecution(Exception exc)
        {
            Utility.WriteError(exc);
        }

        #endregion

        #region Polymorphism : AddParameters

        /// <summary>
        /// Add string type parameter to SqlCommand parameters collection. 
        /// Also provide way for adding null value if parameter is null.
        /// </summary>
        /// <param name="param">name of parameter</param>
        /// <param name="text">value for parameter</param>
        public static void AddParameters(string param, string text)
        {
            if (text != null)
                _sqlCmd.Parameters.AddWithValue(param, text);
            else
                _sqlCmd.Parameters.AddWithValue(param, DBNull.Value);
        }

        /// <summary>
        /// Add long type parameter to SqlCommand parameters collection. 
        /// Also provide way for adding null value if parameter is null.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="num"></param>
        public static void AddParameters(string param, long? num)
        {
            if (num != null)
                _sqlCmd.Parameters.AddWithValue(param, num);
            else
                _sqlCmd.Parameters.AddWithValue(param, DBNull.Value);
        }

        #endregion
    }
}