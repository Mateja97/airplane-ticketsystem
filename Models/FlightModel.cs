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

        private MySqlDatabase MySqlDatabase { get; set; }
        public FlightModel(){

        }
         public FlightModel( MySqlDatabase mySqlDatabase){
            this.MySqlDatabase = mySqlDatabase;
        }

         public async Task InsertAsync()
        {
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Flights (startDestination,endDestination,date,numTransfers,numSeats) VALUES (@sd,@ed,@date,@numt,@nums);";
            cmd.Parameters.AddWithValue("@sd",this.startDestination.ToString());
            cmd.Parameters.AddWithValue("@ed",this.endDestination.ToString());
            cmd.Parameters.AddWithValue("@date",this.date);
            cmd.Parameters.AddWithValue("@numt",this.numTransfers);
            cmd.Parameters.AddWithValue("@nums",this.numSeats);

            await cmd.ExecuteNonQueryAsync();
            flightId = (int) cmd.LastInsertedId;
        }
        public async Task DeleteAsync()
        {
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"DELETE FROM Flights WHERE flightId = @id;";
            cmd.Parameters.AddWithValue("@id",this.flightId);
            await cmd.ExecuteNonQueryAsync();
        }
    }
}