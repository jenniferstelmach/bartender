using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace bartender.Models
{
    public class drinkCategoryViewModel
    {
        public List<drinkList> drinks;
        public SelectList categories;
        public string drinkCategory { get; set; }
    }
}
