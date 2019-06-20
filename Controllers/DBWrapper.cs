using System.Data.SqlClient;
using System;




namespace UserLoginAPI.Controllers
{
    class DBWrapper {

        public static  string _connectionString = "Server=GUPTAA5-STG4\\SQLEXPRESS;Integrated security=SSPI;database=master";


        public static User getUserLoginData(string username)
        {
            string queryString = "USE UserLoginDB; EXEC getUser '" + username + "' ;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new User {
                            Username = (string) reader["Username"],
                            Password = (string) reader["Password"]
                        };
                    }
                }
            }
            return null;
        }

        public static void sendCommand(string queryString) 
        {
            queryString = "USE UserLoginDB; " + queryString;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command = new SqlCommand(queryString, connection, transaction);
                try{
                    command.ExecuteNonQuery(); 
                    transaction.Commit();
                } catch (Exception e){
                    transaction.Rollback();
                    throw e;
                }
            }
        }
    }
}