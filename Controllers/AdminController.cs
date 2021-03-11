using Microsoft.AspNetCore.Mvc;
using System;
using airplane_ticketsystem.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace airplane_ticketsystem{

    public class AdminController : Controller{
        private MySqlDatabase MySqlDatabase { get; set; }
        List<Tuple<string,int>> agentActivites = new List<Tuple<string, int>>();
        public AdminController(MySqlDatabase mySqlDatabase)
        {
            this.MySqlDatabase = mySqlDatabase;
        }

        public IActionResult Index(){
            return View();
        }

        public async Task<IActionResult> Flights(){
            FlightListModel fl = new FlightListModel(MySqlDatabase);
            return View(await fl.GetLatest());
        }
        public async Task<IActionResult>AgentActivity(){
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"SELECT agent,count(*) FROM Reservations where agent != '' GROUP BY agent;";
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    agentActivites.Add(new Tuple<string,int>(reader.GetString(0),reader.GetInt32(1)));
                }
            }
            return View(agentActivites);
        }
        public async Task<IActionResult> addNewUser(string usr,string pw,string type){
            
            EntityModel entity = new EntityModel(this.MySqlDatabase){
                username = usr,
                password = pw,
                type = (EntityType) Enum.Parse(typeof(EntityType),type, true)
            };
            await entity.InsertAsync();
            return View("Index");
        }
        public async Task<IActionResult> RemoveFlight(FlightModel flight){
            FlightModel fl = new FlightModel(MySqlDatabase){
                flightId = flight.flightId
            };
            await fl.DeleteAsync();
            return Redirect("Flights");
        }

    }

}