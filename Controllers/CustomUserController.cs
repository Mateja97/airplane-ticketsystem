using Microsoft.AspNetCore.Mvc;
using airplane_ticketsystem.Models;
using System.Threading.Tasks;
using System;
namespace airplane_ticketsystem.Controllers{
    public class CustomUserController: Controller
    {
        public FlightListModel flights;
        public ReservationListModel reservations;
        private MySqlDatabase MySqlDatabase { get; set; }

        public CustomUserController(MySqlDatabase mySqlDatabase)
        {         
            this.MySqlDatabase = mySqlDatabase;
        }
        public IActionResult Index(){
            
            if(flights == null){
                return View( new FlightListModel(this.MySqlDatabase));
            }
            Console.WriteLine(flights.FlightList.Count);
            return View(flights);
        }

        [HttpPost]
        public async Task<IActionResult> showFlights(Destination sd,Destination ed,Boolean transfers){

            this.flights = new FlightListModel(this.MySqlDatabase);
            await this.flights.Filter(sd,ed,transfers);
           
            return View("Index",this.flights);

        }
        public async Task<IActionResult> Reservations(){
            this.reservations = new ReservationListModel(this.MySqlDatabase);
            await this.reservations.SpecificUserReservations(airplane_ticketsystem.Controllers.HomeController.logined.username);
            return View(this.reservations);
        }
        [HttpPost]
        public async Task<IActionResult> Book(int fID,int ns){
            ReservationModel res = new ReservationModel(this.MySqlDatabase) {
                username = airplane_ticketsystem.Controllers.HomeController.logined.username,
                flightID = fID,
                numSeats = ns
            };
            await res.InsertAsync();
            return Redirect("Reservations");
            
        }
    }
}