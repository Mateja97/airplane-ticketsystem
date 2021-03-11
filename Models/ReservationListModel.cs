using System.Collections.Generic;
using System.Threading.Tasks;
namespace airplane_ticketsystem.Models
{
    public class ReservationListModel
    {
        public List<ReservationModel> ReservationList { get; set; }
        private MySqlDatabase MySqlDatabase { get; set; }
    
        public ReservationListModel(MySqlDatabase mySqlDatabase){
            this.MySqlDatabase = mySqlDatabase;
        }
         public async Task<ReservationListModel> GetLatest()
        {
            ReservationList = new List<ReservationModel>();
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Reservations;";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var res = new ReservationModel(MySqlDatabase)
                    {
                        reservationId = reader.GetInt32(0),
                        username = reader.GetString(1),
                        flightID = reader.GetInt32(2),
                        accepted = reader.GetBoolean(3),
                        agent = reader.GetString(4),
                        numSeats = reader.GetInt32(5)
    
                    };
                    ReservationList.Add(res);
                }
            }
            return this;
        }
        public async Task<ReservationListModel> SpecificUserReservations(string usr)
        {
            ReservationList = new List<ReservationModel>();
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Reservations where username = @usr;";
            cmd.Parameters.AddWithValue("@usr",usr);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var res = new ReservationModel(MySqlDatabase)
                    {
                        reservationId = reader.GetInt32(0),
                        username = reader.GetString(1),
                        flightID = reader.GetInt32(2),
                        accepted = reader.GetBoolean(3),
                        agent = reader.GetString(4),
                        numSeats = reader.GetInt32(5)
    
                    };
                    ReservationList.Add(res);
                }
            }
            return this;
        }
    }
}