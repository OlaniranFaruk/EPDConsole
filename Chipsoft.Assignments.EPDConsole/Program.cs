using Chipsoft.Assignments.EPDConsole.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Program
    {
        //Don't create EF migrations, use the reset db option
        //This deletes and recreates the db, this makes sure all tables exist
        private static EPDDbContext db = new EPDDbContext();

        private static void newLine()
        {
            Console.WriteLine("\n=========================================================================\n");
        }
        private static void AddPatient()
        {
            newLine();
            //using var db = new EPDDbContext();
            Patient patient = Patient.requestPatient();
            Console.WriteLine("Toevoegen van nieuwe patiënt...");
            newLine();

            db.Add(patient);
            db.SaveChanges();
        }

        private static void ShowAppointment()
        {
            //using var db = new EPDDbContext();
            string input;
            List<Appointment> listOfAppointment = new List<Appointment>();
            newLine();
            Console.WriteLine("Afspraken zoeken...");
            if (!db.Appointments.Any())
            {
                Console.WriteLine("Spijtig genoeg zijn er momenteel geen afspraak gemaakt.\n");
            }
            else
            {
                Console.WriteLine("1). Filter Lijst per Artsen \n" +
                    "2). Filter Lijst per Patienten \n" +
                    "3). Toon alle afspraken\n");
                input = Console.ReadLine();
                while (!int.TryParse(input, out int option) || !(option > 0 && option <=4))
                {
                    Console.WriteLine("Ongeldige Input\n" +
                                    "1). Filter Lijst per Artsen \n" +
                                    "2). Filter Lijst per Patienten \n" +
                                    "3). Toon alle afspraken\n");
                    input = Console.ReadLine();
                }
                int choice = int.Parse(input);
                switch (choice)
                {
                    case 1:
                        displayPhysicians();
                        Art selectedPhysician = selectPhysician();
                        listOfAppointment = db.Appointments
                            //.Include(a => a.date)
                            .Where(a => a.artId == selectedPhysician.Id)
                            .ToList();
                        break;

                    case 2:
                        displayPatients();
                        Patient selectedPatient = selectPatient();
                        listOfAppointment = db.Appointments
                            //.Include(a => a.date)
                            .Where(p => p.patientId == selectedPatient.Id)
                            .ToList();
                        break;

                    case 3:
                        listOfAppointment = db.Appointments
                            //.Include(a => a.date)
                            .ToList();
                        break;
                }
                Patient p = new Patient();
                Art a = new Art();
                //Console.WriteLine("Arts\t\tPatient\t\tAfspraak\n");
                foreach (Appointment appointment in listOfAppointment)
                {
                    a = db.Artsen.Include(a => a.address).Where(a => a.Id == appointment.artId).FirstOrDefault();
                    p = db.Patients.Include(p => p.address).Where(p => p.Id == appointment.patientId).FirstOrDefault();

                    Console.WriteLine($"Art - \n{a.ToString()}\n");
                    Console.WriteLine($"Patient - \n{p.ToString()}\n");
                    Console.WriteLine($"Tijstip - \n{appointment.date.ToString()}\n");
                }
            }
            newLine();
        }

        private static void AddAppointment()
        {
            newLine();
            //using var db = new EPDDbContext();
            Appointment appointment= Appointment.requestAppointment();
            
            Console.WriteLine("Toevoegen van afspraak...");
            newLine();
            db.Add(appointment);
            db.SaveChanges();
        }

        private static void DeletePhysician()
        {
            //using var db = new EPDDbContext();
            IEnumerable<Art> listOfPhysicians = db.Artsen.Include(a => a.address);
            newLine();
            if (listOfPhysicians.Any())
            {
                displayPhysicians();
                Art toBeDeleted = selectPhysician();
                toBeDeleted.address = null;
                Console.WriteLine("Verwijderen...");
                db.Remove(toBeDeleted);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("0 resultaat gevonden");
            }
            newLine();
        }

        private static void AddPhysician()
        {
            newLine();
            //using var db = new EPDDbContext();
            Art art = Art.requestArt();

            Console.WriteLine("Toevoegen van Art...");
            newLine();
            db.Add(art);
            db.SaveChanges();
        }

        private static void DeletePatient()
        {
            //using var db = new EPDDbContext();
            IEnumerable<Patient> listOfPatients = db.Patients.Include(p => p.address);
            newLine();
            if (listOfPatients.Any())
            {
                displayPatients();

                Patient toBeDeleted = selectPatient();
                //toBeDeleted.address = null;
                Console.WriteLine("Verwijderen...");
                db.Remove(toBeDeleted);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("0 resultaat gevonden");
            }
            newLine();
        }

        public static void displayPatients()
        {
            newLine();
            //using var db = new EPDDbContext();
            int i = 1;
            IEnumerable<Patient> listOfPatients = db.Patients
                                                    .Include(p => p.address);
            if (listOfPatients.Any())
            {
                foreach (Patient patient in listOfPatients)
                {
                    Console.WriteLine(i + ").\n" + patient.ToString());
                    i++;
                }
            }
            newLine();
        }

        public static Patient selectPatient()
        {
            newLine();
            //using var db = new EPDDbContext();
            string input;
            int i = 1;
            IEnumerable<Patient> listOfPatients = db.Patients.Include(p => p.address);
            Console.WriteLine("Kies een patient: \n");
            input = Console.ReadLine();
            while (!int.TryParse(input, out int option) || !(option > 0 && option <= listOfPatients.Count()))
            {
                foreach (Patient patient in listOfPatients)
                {
                    Console.WriteLine(i + ").\n" + patient.ToString());
                    i++;
                }
                Console.WriteLine("Ongeldige Input \nKies een patient: \n");
                input = Console.ReadLine();
            }

            int indexToBeDeleted = int.Parse(input);
            newLine();
            return listOfPatients.ElementAt(indexToBeDeleted - 1);
        }

        public static Art selectPhysician()
        {
            newLine();
            //using var db = new EPDDbContext();
            int i = 1;
            string input;
            IEnumerable<Art> listOfPhysicians = db.Artsen.Include(a => a.address);

            Console.WriteLine("Kies een Arts: \n");
            input = Console.ReadLine();
            while (!int.TryParse(input, out int option) || !(option > 0 && option <= listOfPhysicians.Count()))
            {
                foreach (Art art in listOfPhysicians)
                {
                    Console.WriteLine(i + ").\n" + art.ToString());
                    i++;
                }
                Console.WriteLine("Ongeldige Input \nKies een Arts: \n");
                input = Console.ReadLine();
            }
            int indexToBeDeleted = int.Parse(input);
            newLine();
            return listOfPhysicians.ElementAt(indexToBeDeleted - 1);
        }

        public static void displayPhysicians()
        {
            newLine();
            //using var db = new EPDDbContext();
            int i = 1;
            IEnumerable<Art> listOfPhysicians = db.Artsen
                                                  .Include(a => a.address);
            if (listOfPhysicians.Any())
            {
                foreach (Art art in listOfPhysicians)
                {
                    Console.WriteLine(i + ").\n" + art.ToString());
                    i++;
                }
            }
            newLine();
        }


        #region FreeCodeForAssignment
        static void Main(string[] args)
        {
            while (ShowMenu())
            {
                //Continue
            }
        }

        public static bool ShowMenu()
        {
            //Sleep om de kans te krijgen om de input te kunnen zien
            Thread.Sleep(5000);
            Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patient toevoegen");
            Console.WriteLine("2 - Patienten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        AddPatient();
                        return true;
                    case 2:
                        DeletePatient();
                        return true;
                    case 3:
                        AddPhysician();
                        return true;
                    case 4:
                        DeletePhysician();
                        return true;
                    case 5:
                        AddAppointment();
                        return true;
                    case 6:
                        ShowAppointment();
                        return true;
                    case 7:
                        return false;
                    case 8:
                        EPDDbContext dbContext = new EPDDbContext();
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();
                        return true;
                    default:
                        return true;
                }
            }
            return true;
        }

        #endregion
    }
}