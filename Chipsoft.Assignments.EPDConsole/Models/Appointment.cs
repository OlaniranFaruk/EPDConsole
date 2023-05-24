using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int artId { get; set; }
        public int patientId { get; set; }
        public DateTime date { get; set; }


        public Appointment() { }

        public static Appointment requestAppointment()
        {
            Appointment appointment = new Appointment();

            Program.displayPhysicians();
            Art a = Program.selectPhysician();
            appointment.artId = a.Id;

            Program.displayPatients();
            Patient p = Program.selectPatient();
            appointment.patientId = p.Id;

            appointment.date = Appointment.requestAppointmentTime();

            return appointment;
        }

        public static DateTime requestAppointmentTime()
        {
            string dateInput;
            string dateTimeString;
            string format = "MM/dd/yyyy HH:mm";

            Console.WriteLine("Datum Invoeren (MM/DD/YYY): \t\t");
            dateInput = Console.ReadLine();
            while (!IsValidDate(dateInput))
            {
                Console.WriteLine("Ongeldige Datum\nDatum Invoeren (MM/DD/YYY): \t\t");
                dateInput = Console.ReadLine();
            }
            dateTimeString = dateInput;

            Console.WriteLine("Tijdstip Invoeren (HH:MM)\n" +
                " Let op, afspraken zijn alleen mogelijk met een tijdsinterval van 30 minuten." +
                "(bvb: 12:00, 12:30, 13:00 ...) : \t\t");
            dateInput = Console.ReadLine();
            while (!ValidateTime(dateInput))
            {
                Console.WriteLine("Ongeldige Formaat\n" +
                    "Tijdstip Invoeren (HH:MM)\n" +
                    " Let op, afspraken zijn alleen mogelijk met een tijdsinterval van 30 minuten." +
                    "(bvb: 12:00, 12:30, 13:00 ...) : \t\t");
                dateInput = Console.ReadLine();
            }
            dateTimeString += " ";
            dateTimeString += dateInput;

            DateTime dateTime = DateTime.ParseExact(dateTimeString, format, null);

            return dateTime;
        }

        public static bool IsValidDate(string date)
        {
                string datePattern = @"^(0[1-9]|1[0-2])/(0[1-9]|1\d|2\d|3[01])/\d{4}$";
                Regex regex = new Regex(datePattern);
                return regex.IsMatch(date);
        }
        public static bool ValidateTime(string time)
        {
            // Regular expression pattern for "hh:00" or "hh:30" format
            string pattern = @"^([01]?[0-9]|2[0-3]):(00|30)$";

            // Check if the time matches the pattern
            return Regex.IsMatch(time, pattern);
        }

    }
}
