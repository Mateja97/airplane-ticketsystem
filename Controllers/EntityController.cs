using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using airplane_ticketsystem.Models;
using airplane_ticketsystem.Controllers;
namespace airplane_ticketsystem{
    public class EntityController : Controller{
        
        public static List<FlightModel> flights = airplane_ticketsystem.Controllers.HomeController.flights;
        private MySqlDatabase MySqlDatabase { get; set; }

          public EntityController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }
         public IActionResult Agent()
        {
            return View();
        }
         public IActionResult Admin()
        {
            return View();
        }
         public IActionResult CustomUser()
        {
            return View();
        }
         [HttpPost]
        public IActionResult addNewFlight(Destination st,Destination ed,int nums,int numt,DateTime d)
        {   
            flights.Add(
               new FlightModel(){startDestination= st,endDestination = ed,numSeats = nums, numTransfers = numt, date = d}
               );
            return View("Agent",new FlightListModel(flights));
         }
    }
}