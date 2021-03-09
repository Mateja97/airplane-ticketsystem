using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using airplane_ticketsystem.Models;
using airplane_ticketsystem.Controllers;
namespace airplane_ticketsystem{
    public class AgentController : Controller{
        
        public static List<FlightModel> flights = airplane_ticketsystem.Controllers.HomeController.flights;
        public static List<ReservationModel> reservations = airplane_ticketsystem.Controllers.HomeController.reservations;
        private MySqlDatabase MySqlDatabase { get; set; }
        public AgentController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }
         [HttpPost]
        public IActionResult addNewFlight(Destination st,Destination ed,int nums,int numt,DateTime d)
        {   
            flights.Add(
               new FlightModel(){startDestination= st,endDestination = ed,numSeats = nums, numTransfers = numt, date = d}
               );
            return View("~/Views/Entity/Agent.cshtml",new FlightListModel(flights));
         }

        public IActionResult Reservations(){
            return View(new ReservationListModel(reservations));
        }

        public IActionResult AcceptReservation(int id){
            foreach(ReservationModel r in reservations){
                if(r.reservationId == id)
                    r.accepted = true;
            }
            return View("Reservations",new ReservationListModel(reservations));
        }
    }
}