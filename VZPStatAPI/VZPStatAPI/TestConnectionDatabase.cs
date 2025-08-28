using Microsoft.Data.SqlClient;

namespace VZPStatAPI
{
    public class TestConnectionDatabase
    {
        public string Error { get; set; } = string.Empty;

        public bool IsConnected { get; set; } = false;

        public string ConnectionString { get; private set; } = string.Empty;

        public TestConnectionDatabase(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public bool IsServerConnected()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    IsConnected = true;
                    return true;
                }
                catch (SqlException ex)
                {
                    Error = ex.Message;
                    Error += ex.InnerException?.Message;
                    IsConnected = false;
                    return false;
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
