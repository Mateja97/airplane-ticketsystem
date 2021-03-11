using System.Collections.Generic;
using System.Threading.Tasks;
using System;
namespace airplane_ticketsystem.Models
{
    public class FlightListModel
    {
        public List<FlightModel> FlightList { get; set; }
        private MySqlDatabase MySqlDatabase { get; set; }
        
        public FlightListModel( MySqlDatabase mySqlDatabase){
            FlightList = new List<FlightModel>();
            this.MySqlDatabase = mySqlDatabase;
        }
        public async Task<FlightListModel> Filter(Destination sd,Destination ed, Boolean t){

            FlightList = new List<FlightModel>();
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Flights WHERE startDestination = '"+sd +"' AND endDestination ='" +ed +"'" ;
            cmd.CommandText+= t ? ";" :" AND numTransfers = 0;";

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var flight = new FlightModel(MySqlDatabase)
                    {
                        flightId = reader.GetInt32(0),
                        startDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(1), true),
                        endDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(2), true),
                        date = reader.GetDateTime(3),
                        numTransfers = reader.GetInt32(4),
                        numSeats = reader.GetInt32(5)
                    };

                    FlightList.Add(flight);
                }
            }
            return this;

        }
        public async Task<FlightListModel> GetLatest()
        {   
            FlightList = new List<FlightModel>();
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Flights;";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var flight = new FlightModel(MySqlDatabase)
                    {
                        flightId = reader.GetInt32(0),
                        startDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(1), true),
                        endDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(2), true),
                        date = reader.GetDateTime(3),
                        numTransfers = reader.GetInt32(4),
                        numSeats = reader.GetInt32(5)
                    };
                    FlightList.Add(flight);
                }
            }
            return this;
        }
    }
}