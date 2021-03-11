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
         private MySqlDatabase MySqlDatabase { get; set; }

         public ReservationModel( MySqlDatabase mySqlDatabase){
            this.MySqlDatabase = mySqlDatabase;
        }
        public async Task InsertAsync()
        {
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"insert into  Reservations (username,flightID,accepted,agent,numSeats) VALUES (@usr,@fId,@active,@ag,@ns);";
            cmd.Parameters.AddWithValue("@usr",this.username);
            cmd.Parameters.AddWithValue("@fId",this.flightID);
            cmd.Parameters.AddWithValue("@active",this.accepted);
            cmd.Parameters.AddWithValue("@ag","");
            cmd.Parameters.AddWithValue("@ns",this.numSeats);

            await cmd.ExecuteNonQueryAsync();
            reservationId = (int) cmd.LastInsertedId;
        }
        public async Task AcceptReservation()
        {
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE Reservations SET accepted = @ac, agent=@ag  WHERE reservationID = @id;";
            cmd.Parameters.AddWithValue("@ac",true);
            cmd.Parameters.AddWithValue("@ag",airplane_ticketsystem.Controllers.HomeController.logined.username);
            cmd.Parameters.AddWithValue("@id",this.reservationId);
            
            await cmd.ExecuteNonQueryAsync();
            await UpdateFlights();
            
        }

        public async Task UpdateFlights(){
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"UPDATE Flights as f JOIN Reservations as r ON f.flightId = r.flightID SET f.numSeats= f.numSeats-@ns  WHERE r.ReservationID = @id;";
            cmd.Parameters.AddWithValue("@ns",this.numSeats);
            cmd.Parameters.AddWithValue("@id",this.reservationId);
            
            await cmd.ExecuteNonQueryAsync();
        }
    }
    
}