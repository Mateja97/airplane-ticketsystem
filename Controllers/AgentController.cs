using Microsoft.AspNetCore.Mvc;
using System;
using airplane_ticketsystem.Models;
using airplane_ticketsystem.Controllers;
using System.Threading.Tasks;
using airplane_ticketsystem.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace airplane_ticketsystem{
    public class AgentController : Controller{
        
        public FlightListModel flights;
        public  ReservationListModel reservations;
        private MySqlDatabase MySqlDatabase { get; set; }
        private IHubContext<FlightsHub> HubContext{ get; set; }
        public AgentController(MySqlDatabase mySqlDatabase,IHubContext<FlightsHub> hubcontext)
        {
            this.MySqlDatabase = mySqlDatabase;
            this.HubContext = hubcontext;
        }
        public async Task<IActionResult> Index(){
            this.flights = new FlightListModel(this.MySqlDatabase);
            return View(await this.flights.GetLatest());
        }
        public  async Task<IActionResult> addNewFlight(Destination st,Destination ed,int nums,int numt,DateTime d)
        {   
           FlightModel flight = 
               new FlightModel(MySqlDatabase){startDestination= st,endDestination = ed,numSeats = nums, numTransfers = numt, date = d}
               ;
            await flight.InsertAsync();
            return Redirect("Index");
         }

        public async Task<IActionResult> Reservations(){

            this.reservations = new ReservationListModel(this.MySqlDatabase);
            
            return View(await this.reservations.GetLatest());
        }
        public async Task<IActionResult> AcceptReservation(int id){
            
            this.reservations = new ReservationListModel(this.MySqlDatabase);
            this.reservations = await this.reservations.GetLatest();

            foreach(ReservationModel r in this.reservations.ReservationList){
                if(r.reservationId == id){
                    await r.AcceptReservation();
                }
                   
            }
            await this.HubContext.Clients.All.SendAsync("ReceiveAcceptedReservation",id);
            return RedirectToAction("Reservations");
        }
    }
}