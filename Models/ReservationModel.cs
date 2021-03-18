using System.Threading.Tasks;
namespace airplane_ticketsystem.Models
{
    public class ReservationModel {

        public int reservationId {get;set;}
        public string username {get;set;}
        public int flightID {get;set;}

        public bool accepted = false;

        public int numSeats {get;set;}
        public string agent {get;set;}

       
    }
    
}