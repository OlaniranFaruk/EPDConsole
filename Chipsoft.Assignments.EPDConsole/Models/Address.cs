using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int HouseNr { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }

        public Address(int _houseNr, string street, string city, int postalCode)
        {
            HouseNr = _houseNr;
            Street = street;
            City = city;
            PostalCode = postalCode;
        }
        public Address() { }

        public static Address requestAddress()
        {
            Address a = new Address();
            string input;

            Console.WriteLine("Adres Invullen\n ");

            Console.WriteLine("Huis Nummer: \t\t");
            input = Console.ReadLine();
            //validate house number is an integer
            while(!int.TryParse(input, out int output))
            {
                Console.WriteLine("Ongeldige Input\nHuis Nummer: \t\t");
                input = Console.ReadLine();
            }
            a.HouseNr = int.Parse(input);

            Console.WriteLine("Straat: \t\t");
            input = Console.ReadLine();
            a.Street = input;

            Console.WriteLine("Stad: \t\t");
            input = Console.ReadLine();
            a.City = input;

            Console.WriteLine("Postcode: \t\t");
            input = Console.ReadLine();
            while (!int.TryParse(input, out int output) || !validPostcodeOrNot(input))
            {
                Console.WriteLine("Ongeldige Input\nPostcode \t\t");
                input = Console.ReadLine();
            }
            a.PostalCode = int.Parse(input);

            return a;
        }

        public static bool validPostcodeOrNot(string postcodestr)
        {
            if (int.TryParse(postcodestr, out int postcode))
            {
                if (postcode >= 1000 && postcode <= 9999)
                {
                    return true;
                }
            }

            return false;
        }
        public override string ToString()
        {
            return $"Adres:\n {HouseNr} {Street},\n {City}, {PostalCode}";
        }
    }



}
