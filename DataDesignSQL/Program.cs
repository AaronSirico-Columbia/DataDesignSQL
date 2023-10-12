using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataDesignSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder mySqlConBldr = new SqlConnectionStringBuilder();
            mySqlConBldr["server"] = @"(localdb)\MSSQLLocalDB";
            mySqlConBldr["Trusted_Connection"] = true;
            mySqlConBldr["Integrated Security"] = "SSPI";
            mySqlConBldr["Initial Catalog"] = "PROG260FA23";
            string sqlConStr = mySqlConBldr.ToString();

            using (SqlConnection conn = new SqlConnection(sqlConStr))
            {
                conn.Open();
                string inlineSQL = @"Select * from Game";
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var value = $"{reader.GetValue(0)},{reader.GetValue(1)}.{reader.GetValue(2)}.{reader.GetValue(3)}.{reader.GetValue(4)}";

                        Console.WriteLine(value);
                    }
                    reader.Close();

                }
                conn.Close();
            }

            using (SqlConnection conn = new SqlConnection(sqlConStr))
            {
                conn.Open();
                string inlineSQL = @"INSERT [dbo].[Game] ([Name], [Publisher], [Release_Date], [Sold], [Rating]) VALUES ('The Secret of Monkey Island', 'Lucasfilm Games', '10-01-1990', NULL, 94)";
                using (var command = new SqlCommand(inlineSQL, conn))
                {
                    var query = command.ExecuteNonQuery();
                    Console.ReadLine();
                }
                conn.Close();

            }
        }
    }
}
