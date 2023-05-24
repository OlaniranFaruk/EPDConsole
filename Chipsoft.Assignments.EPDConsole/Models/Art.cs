using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Art
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address address { get; set; }

        public Art(string _firstname, string _lastname, Address _address)
        {
            FirstName = _firstname;
            LastName = _lastname;
            address = _address;

        }

        public Art() { }

        public static Art requestArt()
        {
            Art a = new Art();

            Console.WriteLine("Gelieve artsgevegens in te voeren:\n");
            Console.WriteLine("Achternaam: \t\t");
            a.LastName = Console.ReadLine();

            Console.WriteLine("Voornaam: \t\t");
            a.FirstName = Console.ReadLine();

            a.address = Address.requestAddress();

            return a;
        }

        public override string ToString()
        {
            return $"Naam:\t{FirstName} {LastName} \n{address.ToString()}\n";
        }
    }
}
