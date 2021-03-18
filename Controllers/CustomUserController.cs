using Microsoft.AspNetCore.Mvc;
using airplane_ticketsystem.Models;
using System.Threading.Tasks;
using System;
using airplane_ticketsystem.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace airplane_ticketsystem.Controllers{
    public class CustomUserController: Controller
    {
        private MySqlDatabase MySqlDatabase { get; set; }
        private IHubContext<FlightsHub> HubContext{ get; set; }

        public CustomUserController(MySqlDatabase mySqlDatabase,IHubContext<FlightsHub>  hubcontext)
        {
            this.MySqlDatabase = mySqlDatabase;
            this.HubContext = hubcontext;
        }
        public IActionResult Index(){
            
            FlightListModel flights = new FlightListModel();
            return View(flights);
        }
        public async Task<IActionResult> showFlights(Destination sd,Destination ed,Boolean transfers){

            FlightListModel flights = await MySqlDatabase.FilterFlights(sd,ed,transfers);
            return View("Index",flights);

        }
        public async Task<IActionResult> Reservations(){
            ReservationListModel reservations = await this.MySqlDatabase.SpecificUserReservations(airplane_ticketsystem.Controllers.HomeController.logined.username);     
            return View(reservations);
        }
        public async Task<IActionResult> Book(int fID,int ns){
            ReservationModel res = new ReservationModel() {
                username = airplane_ticketsystem.Controllers.HomeController.logined.username,
                flightID = fID,
                numSeats = ns
            };
                     
            res.reservationId = await this.MySqlDatabase.addNewReservation(res);
            await this.HubContext.Clients.All.SendAsync("ReceiveReservation",res);
            return Redirect("Reservations");
            
        }
    }
}