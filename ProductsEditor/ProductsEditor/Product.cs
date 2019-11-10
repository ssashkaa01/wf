using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsEditor
{
    class Product
    {
        public string name { get; set; }
        public string country { get; set; }
        public int price { get; set; }
        public int count { get; set; }
        public int sale { get; set; }

        public string GetID()
        {
            return $"{name} :: {price} UAH";
        }
    }
}
