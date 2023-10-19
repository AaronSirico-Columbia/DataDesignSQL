using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataDesignSQL
{
    public class Product
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public float Price { get; set; }
       
        
        public string UoM { get; set; }

        public string SellByDate { get; set; }


        public Product()
        {

        }
        public Product(int id, string name, string location, float price, string uoM, string sellByDate )
        {

            ID = id;
            Name = name;
            Location = location;
            try
            {
                Price = price;
            }
            catch (Exception)
            {
                price = 0;
            }
            UoM = uoM;
            SellByDate = sellByDate;


        }

        public string NewLocation(ref string Location)
        {
            if (Location.Contains("F"))
            {
               Location = Location.Replace("F", "Z");
            }
            else if (Location.Contains("A"))
            {
               Location = Location.Replace("A", "Z");
            }
            return Location;
        }

        public string SellDate(ref string SellByDate)
        {
            if (SellByDate.Contains("2022"))
            {
                SellByDate = null;
            }
            return SellByDate;
        }

        public float UpdatePrice(ref float Price)
        {
            float NewPrice;
            if (Price != null)
            {
                NewPrice = Price;
                Price = Price + 1;
                return NewPrice;
            }
            return 0;

        }
    }
}
