using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using airplane_ticketsystem.Models;

namespace airplane_ticketsystem
{
  public class MySqlDatabase : IDisposable
  {
    public MySqlConnection Connection;

    public MySqlDatabase(string connectionString)
    {
      Connection = new MySqlConnection(connectionString);
      this.Connection.Open();
    }

    public void Dispose()
    {
      Connection.Close();
    }
    public async Task<FlightListModel> GetFlights()
        {   
             FlightListModel fl = new FlightListModel();
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Flights;";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var flight = new FlightModel()
                    {
                        flightId = reader.GetInt32(0),
                        startDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(1), true),
                        endDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(2), true),
                        date = reader.GetDateTime(3),
                        numTransfers = reader.GetInt32(4),
                        numSeats = reader.GetInt32(5)
                    };
                    fl.FlightList.Add(flight);
                }
            }
            return fl;
        }
        public async Task<ReservationListModel> GetReservations()
        {
            ReservationListModel reservation = new ReservationListModel();
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Reservations;";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var res = new ReservationModel()
                    {
                        reservationId = reader.GetInt32(0),
                        username = reader.GetString(1),
                        flightID = reader.GetInt32(2),
                        accepted = reader.GetBoolean(3),
                        agent = reader.GetString(4),
                        numSeats = reader.GetInt32(5)
    
                    };
                    reservation.ReservationList.Add(res);
                }
            }
            return reservation;
        }
    public async Task addNewFlight(FlightModel flight)
    {
        using var cmd = this.Connection.CreateCommand();
        cmd.CommandText = @"INSERT INTO Flights (startDestination,endDestination,date,numTransfers,numSeats) VALUES (@sd,@ed,@date,@numt,@nums);";
        cmd.Parameters.AddWithValue("@sd",flight.startDestination.ToString());
        cmd.Parameters.AddWithValue("@ed",flight.endDestination.ToString());
        cmd.Parameters.AddWithValue("@date",flight.date);
        cmd.Parameters.AddWithValue("@numt",flight.numTransfers);
        cmd.Parameters.AddWithValue("@nums",flight.numSeats);

        await cmd.ExecuteNonQueryAsync();
        flight.flightId = (int) cmd.LastInsertedId;
    }
    public async Task<int> addNewReservation(ReservationModel res)
        {
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"insert into  Reservations (username,flightID,accepted,agent,numSeats) VALUES (@usr,@fId,@active,@ag,@ns);";
            cmd.Parameters.AddWithValue("@usr",res.username);
            cmd.Parameters.AddWithValue("@fId",res.flightID);
            cmd.Parameters.AddWithValue("@active",false);
            cmd.Parameters.AddWithValue("@ag","");
            cmd.Parameters.AddWithValue("@ns",res.numSeats);

            await cmd.ExecuteNonQueryAsync();
            return (int)cmd.LastInsertedId;
        }
    public async Task DeleteFlight(FlightModel flight)
    {
        using var cmd = this.Connection.CreateCommand();
        cmd.CommandText = @"DELETE FROM Flights WHERE flightId = @id;";
        cmd.Parameters.AddWithValue("@id",flight.flightId);
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<List<Tuple<string,int>>> getAgentActivity(){
            List<Tuple<string,int>> agentActivites = new List<Tuple<string, int>>();
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"SELECT agent,count(*) FROM Reservations where agent != '' GROUP BY agent;";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    agentActivites.Add(new Tuple<string,int>(reader.GetString(0),reader.GetInt32(1)));
                }
            }
            return agentActivites;
        }
    public async Task addNewUser(string usr,string pw,string type){
            
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Users (username,password,type) VALUES (@usr,@pw,@type);";
            cmd.Parameters.AddWithValue("@usr",usr);
            cmd.Parameters.AddWithValue("@pw",pw);
            cmd.Parameters.AddWithValue("@type",type.ToString());

            await cmd.ExecuteNonQueryAsync();
        }
    public async Task AcceptReservation(int id,int numSeats)
      {
          using var cmd = this.Connection.CreateCommand();
          cmd.CommandText = @"UPDATE Reservations SET accepted = @ac, agent=@ag  WHERE reservationID = @id;";
          cmd.Parameters.AddWithValue("@ac",true);
          cmd.Parameters.AddWithValue("@ag",airplane_ticketsystem.Controllers.HomeController.logined.username);
          cmd.Parameters.AddWithValue("@id",id);
          
          await cmd.ExecuteNonQueryAsync();
          await UpdateFlights(id,numSeats);
      }
    public async Task UpdateFlights(int id,int numSeats){
      using var cmd = this.Connection.CreateCommand();
      cmd.CommandText = @"UPDATE Flights as f JOIN Reservations as r ON f.flightId = r.flightID SET f.numSeats= f.numSeats-@ns  WHERE r.ReservationID = @id;";
      cmd.Parameters.AddWithValue("@ns",numSeats);
      cmd.Parameters.AddWithValue("@id",id);
      
      await cmd.ExecuteNonQueryAsync();
  }
   public async Task<FlightListModel> FilterFlights(Destination sd,Destination ed, Boolean t){

            FlightListModel fl = new FlightListModel();
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Flights WHERE startDestination = '"+sd +"' AND endDestination ='" +ed +"'" ;
            cmd.CommandText+= t ? ";" :" AND numTransfers = 0;";

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var flight = new FlightModel()
                    {
                        flightId = reader.GetInt32(0),
                        startDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(1), true),
                        endDestination = (Destination) Enum.Parse(typeof(Destination),reader.GetString(2), true),
                        date = reader.GetDateTime(3),
                        numTransfers = reader.GetInt32(4),
                        numSeats = reader.GetInt32(5)
                    };

                    fl.FlightList.Add(flight);
                }
            }
            return fl;

        }
    public async Task<ReservationListModel> SpecificUserReservations(string usr)
        {
            ReservationListModel reservations = new ReservationListModel();
            using var cmd = this.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Reservations where username = @usr;";
            cmd.Parameters.AddWithValue("@usr",usr);
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var res = new ReservationModel()
                    {
                        reservationId = reader.GetInt32(0),
                        username = reader.GetString(1),
                        flightID = reader.GetInt32(2),
                        accepted = reader.GetBoolean(3),
                        agent = reader.GetString(4),
                        numSeats = reader.GetInt32(5)
    
                    };
                    reservations.ReservationList.Add(res);
                }
            }
            return reservations;
        }

  }
}