using System;

namespace bartender.Models
{
    public class orderDrinks
    {
         public int ID { get; set; }
        public string CustName { get; set; }
        public string Drink { get; set; }
        public int Qty { get; set; }
        public Boolean Filled { get; set; }
    }

}
