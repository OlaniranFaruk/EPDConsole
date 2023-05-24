using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address address { get; set; }

        public string ContactInfo { get; set; }

        public Patient(string _firstname, string _lastname, Address _address, string _contactInfo)
        {
            FirstName = _firstname;
            LastName = _lastname;
            address = _address;
            ContactInfo = _contactInfo;
        }

        public Patient() { }

        public static Patient requestPatient()
        {
            Patient p = new Patient();

            Console.WriteLine("Gelieve patientgevegens in te voeren:\n");
            Console.WriteLine("Achternaam: \t\t");
            p.LastName = Console.ReadLine();

            Console.WriteLine("Voornaam: \t\t");
            p.FirstName = Console.ReadLine();

            Console.WriteLine("ContactGevegens (telefoon/e-mail):\t ");
            p.ContactInfo = Console.ReadLine();

            p.address = Address.requestAddress();

            return p;
        }

        public override string ToString()
        {
            return $"Naam: \t{FirstName} {LastName}\n{this.address.ToString()}\nContact: \t{ContactInfo}";
        }
    }
}
