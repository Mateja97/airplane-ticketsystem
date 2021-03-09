namespace airplane_ticketsystem.Models
{

    public class ReservationModel {

        public int reservationId {get;set;}
        public string username {get;set;}
        public FlightModel flight {get;set;}

        public bool accepted = false;
    }
    
}