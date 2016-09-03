using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace bartender.Models
{
    //GET combined data
    public class Combined
    {
        //public string MyBar { get; set; }

         public List<bartender.Models.drinkCategoryViewModel> Drinks { get; set; }
         public List<bartender.Models.orderDrinks>Orders { get; set; }

        //public  drinkList myDrinkList;
        //public orderDrink myDrinkOrder;

    }




}
