using Microsoft.AspNetCore.Mvc;
using airplane_ticketsystem.Models;
using System.Threading.Tasks;
using System;
using airplane_ticketsystem.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace airplane_ticketsystem.Controllers{
    public class CustomUserController: Controller
    {
        public FlightListModel flights;
        public ReservationListModel reservations;
        private MySqlDatabase MySqlDatabase { get; set; }
        private IHubContext<FlightsHub> HubContext{ get; set; }

        public CustomUserController(MySqlDatabase mySqlDatabase,IHubContext<FlightsHub>  hubcontext)
        {
            this.MySqlDatabase = mySqlDatabase;
            this.HubContext = hubcontext;
        }
        public IActionResult Index(){
            
            if(flights == null){
                return View( new FlightListModel(this.MySqlDatabase));
            }
            return View(flights);
        }
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
        public async Task<IActionResult> Book(int fID,int ns){
            ReservationModel res = new ReservationModel(this.MySqlDatabase) {
                username = airplane_ticketsystem.Controllers.HomeController.logined.username,
                flightID = fID,
                numSeats = ns
            };
            await res.InsertAsync();
            await this.HubContext.Clients.All.SendAsync("ReceiveReservation",res);
            return Redirect("Reservations");
            
        }
    }
}