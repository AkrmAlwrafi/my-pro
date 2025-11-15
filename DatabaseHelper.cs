using System;
using System.Data;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    public static string connectionString = "Data Source=.;Initial Catalog=institute system;Integrated Security=True";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}