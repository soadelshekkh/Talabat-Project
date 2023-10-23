using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
    public class Adrress
    {
        public Adrress(string firstname, string lastName, string city, string countary, string street)
        {
            Firstname = firstname;
            LastName = lastName;
            City = city;
            Countary = countary;
            Street = street;
        }
        public Adrress()
        {

        }
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Countary { get; set; }
        public string Street { get; set; }
    }
}
