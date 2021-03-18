using Microsoft.AspNetCore.Mvc;
using System;
using airplane_ticketsystem.Models;
using airplane_ticketsystem.Controllers;
using System.Threading.Tasks;
using airplane_ticketsystem.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace airplane_ticketsystem{
    public class AgentController : Controller{
        
        private MySqlDatabase MySqlDatabase { get; set; }
        private IHubContext<FlightsHub> HubContext{ get; set; }
        public AgentController(MySqlDatabase mySqlDatabase,IHubContext<FlightsHub> hubcontext)
        {
            this.MySqlDatabase = mySqlDatabase;
            this.HubContext = hubcontext;
        }
        public async Task<IActionResult> Index(){
            FlightListModel fl = await MySqlDatabase.GetFlights();
            return View(fl);
        }
        public  async Task<IActionResult> addNewFlight(Destination st,Destination ed,int nums,int numt,DateTime d)
        {   
           FlightModel flight = 
               new FlightModel(){startDestination= st,endDestination = ed,numSeats = nums, numTransfers = numt, date = d}
               ;
            await MySqlDatabase.addNewFlight(flight);
            return Redirect("Index");
         }

        public async Task<IActionResult> Reservations(){
            
            ReservationListModel reservations = await MySqlDatabase.GetReservations();
            return View(reservations);
        }
        public async Task<IActionResult> AcceptReservation(int id,int numSeats){
            
            await this.MySqlDatabase.AcceptReservation(id,numSeats);
            await this.HubContext.Clients.All.SendAsync("ReceiveAcceptedReservation",id);
            return View("Reservations",await this.MySqlDatabase.GetReservations());
        }
    }
}