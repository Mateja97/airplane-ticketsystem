using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using airplane_ticketsystem.Models;

namespace airplane_ticketsystem.Controllers
{
    public class HomeController : Controller
    {
        public static EntityModel logined = null;
         private MySqlDatabase MySqlDatabase { get; set; }
         public HomeController(MySqlDatabase mySqlDatabase)
        {
             this.MySqlDatabase = mySqlDatabase;
        }
      
        public IActionResult Index()
        {
            logined = null;
            return View();
           
        }
         [HttpPost]
        public async Task<IActionResult> login(string usr,string pw)
        {  
            using var cmd = this.MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Users where Username = @username and Password = @password;";
            cmd.Parameters.AddWithValue("@username",usr);
            cmd.Parameters.AddWithValue("@password",pw);
            using(var reader = await cmd.ExecuteReaderAsync()){
                if (!reader.HasRows){
                    logined = null;
                    TempData["alert"] = "<script>alert('Wrong username or password');</script>";
                    return View("Index");
                    }else{
                        await reader.ReadAsync();
                        logined = new EntityModel(){
                        username = reader.GetString(0),
                        password = reader.GetString(1),
                        type = (EntityType) Enum.Parse(typeof(EntityType),reader.GetString(2), true)};
                        }
            }
   
            if(logined.type == EntityType.Admin){

                 return RedirectToAction("Index","Admin");

            }else if(logined.type == EntityType.User){

                return RedirectToAction("Index","CustomUser");
            }else
            {
                return RedirectToAction("Index","Agent");
            }

           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
