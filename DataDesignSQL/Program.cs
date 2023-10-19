using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;
using static System.Net.WebRequestMethods;

namespace DataDesignSQL
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Product p = new Product();  
           
            var Path = ($"{Directory.GetCurrentDirectory()}\\Files");
            string File = $"{Path}\\Produce.txt";
            List<Product>Products = new List<Product>();
            int i = 0;


            using (StreamReader sr = new StreamReader(File))
            {  
                var line = sr.ReadLine();
                while (line != null)
                {
                   if (i < 1)
                    {
                        i++;
                        line = sr.ReadLine();
                    }
                   else
                    {
                        var data = line.Split('|').ToList();
                        data = data.ToList();
                        Product Prod = new Product(1, data[0], data[1], float.Parse(data[2]), data[3], data[4]);
                        if (data[4].Contains("2023") || data[4].Contains("2024"))
                        {
                            Products.Add(Prod);
                        }
                       
                        line = sr.ReadLine();
                    }
                    
                }
            }
            Update up = new Update();

            

            foreach (Product prod in Products)
            {
                //New Location
                string NewLocation;
                NewLocation = prod.Location;
                p.NewLocation(ref NewLocation);
                prod.Location = NewLocation;
            }


            foreach (Product prod in Products)
            {
                //New Location
                float NewPrice;
                NewPrice = prod.Price;
                p.UpdatePrice(ref NewPrice);
                prod.Price = NewPrice;
                

            }



            SqlConnectionStringBuilder mySqlConBldr = new SqlConnectionStringBuilder();
            mySqlConBldr["server"] = @"(localdb)\MSSQLLocalDB";
            mySqlConBldr["Trusted_Connection"] = true;
            mySqlConBldr["Integrated Security"] = "SSPI";
            mySqlConBldr["Initial Catalog"] = "PROG260FA23";

            string sqlConStr = mySqlConBldr.ToString();

            foreach (Product prod in Products)
            {
                using (SqlConnection conn = new SqlConnection(sqlConStr))
                {
                    
                    conn.Open();
                    string inlineSQL = $@"INSERT [dbo].[Produce] ([Name], [Location], [Price], [UoM], [Sell_by_Date]) VALUES ('{prod.Name}', '{prod.Location}', '{prod.Price}', '{prod.UoM}', '{prod.SellByDate}')";
                    using (var command = new SqlCommand(inlineSQL, conn))
                    {
                        var query = command.ExecuteNonQuery();
                        Console.ReadLine();
                    }
                    conn.Close();

                }
            }

            using (SqlConnection conn = new SqlConnection(sqlConStr))
            {
                conn.Open();
                string inlineSQL = @"Select * from Produce";
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
        }
    }
}
