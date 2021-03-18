using Microsoft.AspNetCore.Mvc;
using System;
using airplane_ticketsystem.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace airplane_ticketsystem{

    public class AdminController : Controller{
        private MySqlDatabase MySqlDatabase { get; set; }
    
        public AdminController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }

        public IActionResult Index(){
            return View();
        }

        public async Task<IActionResult> Flights(){
            FlightListModel fl = await MySqlDatabase.GetFlights();
            return View(fl);
        }
        public async Task<IActionResult>AgentActivity(){
            List<Tuple<string,int>> agentActivites = await MySqlDatabase.getAgentActivity();
            return View(agentActivites);
        }
        public async Task<IActionResult> addNewUser(string usr,string pw,string type){
            
            await this.MySqlDatabase.addNewUser(usr,pw,type);
            TempData["alert"] = "<script>alert('Successfully added');</script>";
            return View("Index");
        }
        public async Task<IActionResult> RemoveFlight(FlightModel flight){
            FlightModel fl = new FlightModel(){
                flightId = flight.flightId
            };
            await MySqlDatabase.DeleteFlight(flight);
            return Redirect("Flights");
        }

    }

}