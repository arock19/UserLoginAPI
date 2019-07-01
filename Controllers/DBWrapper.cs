using System.Data.SqlClient;
using System;
using Newtonsoft.Json;

namespace UserLoginAPI.Controllers
{
    class DBWrapper {

        public static  string _connectionString = "Server=GUPTAA5-STG4\\SQLEXPRESS;Integrated security=SSPI;database=master";


        public static UserLogin getUserLoginData(string username)
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
                        return new UserLogin {
                            Username = username,
                            Password = (string) reader["Password"]
                        };
                    }
                }
            }
            return null;
        }

        public static UserSubscription getUserSubscriptionData(string username)
        {
            string queryString = "USE UserLoginDB; EXEC getUserSubs '" + username + "' ;";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserSubscription subscription = new UserSubscription {
                            Username = username
                        };

                        subscription.Alert1 = (bool)reader["Alert1"];
                        subscription.Alert2 = (bool)reader["Alert2"];
                        subscription.Alert3 = (bool)reader["Alert3"];
                        subscription.Alert4 = (bool)reader["Alert4"];
                        subscription.Alert5 = (bool)reader["Alert5"];

                        if(! DBNull.Value.Equals(reader["CustomAlert"])){
                            subscription.CustomAlert = JsonConvert.DeserializeObject<Alert[]>((string)reader["CustomAlert"]);
                        }

                        return subscription;
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