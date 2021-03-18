using System;
using System.Threading.Tasks;
namespace airplane_ticketsystem.Models{
    public enum Destination{
        Beograd,
        Nis,
        Kraljevo,
        Pristina

    }
    public class FlightModel{

        public int flightId {get;set;}
        public Destination startDestination{get;set;}
        public Destination endDestination{get;set;}
        public DateTime date {get;set;}

        public int numTransfers {get;set;}

        public int numSeats{get;set;}

    }
}