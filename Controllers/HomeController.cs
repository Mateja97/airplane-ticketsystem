using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using airplane_ticketsystem.Models;
using MySql.Data.MySqlClient;

namespace airplane_ticketsystem.Controllers
{
    public class HomeController : Controller
    {
        public static List<FlightModel> flights;

         public static List<ReservationModel> reservations = new List<ReservationModel>()
         {
             new ReservationModel{reservationId = 1,username = "trta", flight = new FlightModel() {startDestination = Destination.Beograd,endDestination = Destination.Nis,date = new DateTime(2021,3,21),numSeats = 30,numTransfers = 0}},
             new ReservationModel{reservationId = 2,username = "kurta", flight = new FlightModel() {startDestination = Destination.Beograd,endDestination = Destination.Nis,date = new DateTime(2021,3,21),numSeats = 30,numTransfers = 0}},
             new ReservationModel{reservationId = 3,username = "murta", flight = new FlightModel() {startDestination = Destination.Beograd,endDestination = Destination.Nis,date = new DateTime(2021,3,21),numSeats = 30,numTransfers = 0}}
        };
        
        public static EntityModel logined = null;
         private MySqlDatabase MySqlDatabase { get; set; }
         public HomeController(MySqlDatabase mySqlDatabase)
        {
            flights =   new List<FlightModel> ();
            this.MySqlDatabase = mySqlDatabase;
            using(var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand){
                cmd.CommandText = @"SELECT * FROM Flights;";
                using(MySqlDataReader result = cmd.ExecuteReader()){
                    while(result.Read()){
                        flights.Add(new FlightModel() {
                            flightId = result.GetInt32(0),
                            startDestination = (Destination) Enum.Parse(typeof(Destination),result.GetString(1), true),
                            endDestination = (Destination) Enum.Parse(typeof(Destination),result.GetString(2), true),
                            date = result.GetDateTime(3),
                            numTransfers = result.GetInt32(4),
                            numSeats = result.GetInt32(5)
                        });
                    }
                }
            }
        }
      
        public IActionResult Index()
        {
            logined = null;
            return View();
           
        }
         [HttpPost]
        public IActionResult login(string usr,string pw)
        {  
            using(var cmd = this.MySqlDatabase.Connection.CreateCommand() as MySqlCommand){
                cmd.CommandText = @"SELECT * FROM Users where Username = @username and Password = @password;";
                cmd.Parameters.AddWithValue("@username",usr);
                cmd.Parameters.AddWithValue("@password",pw);
                using(MySqlDataReader result = cmd.ExecuteReader()){
                    if (result == null){
                        logined = null;
                        return View("~/Views/Home/Index.cshtml");
                    }else{
                        result.Read();
                        logined = new EntityModel(){
                            username = result.GetString(0),
                            password = result.GetString(1),
                            type = (EntityType) Enum.Parse(typeof(EntityType),result.GetString(2), true)};
                            }
                
                }
            }
            if(logined.type == EntityType.Admin){
                 return View("~/Views/Entity/Admin.cshtml",logined);
            }else if(logined.type == EntityType.User){
                return View("~/Views/Entity/CustomUser.cshtml",logined);
            }
            else
            {
                return View("~/Views/Entity/Agent.cshtml",new FlightListModel(flights));
            }

           
        }
        public IActionResult login()
        {
             if(logined.type == EntityType.Admin){
                 return View("~/Views/Entity/Admin.cshtml",logined);
            }else if(logined.type == EntityType.User){
                return View("~/Views/Entity/CustomUser.cshtml",logined);
            }
            else
            {
                return View("~/Views/Entity/Agent.cshtml",new FlightListModel(flights));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
