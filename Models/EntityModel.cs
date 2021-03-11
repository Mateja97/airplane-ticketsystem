using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
namespace airplane_ticketsystem.Models
{
    public enum EntityType{
        Admin,
        User,
        Agent

    }
    public class EntityModel
    {
        public string username { get; set; }
        public string password {get;set;}

        public EntityType type {get;set;}

        public MySqlDatabase MySqlDatabase;
        public EntityModel(){}
        public EntityModel(MySqlDatabase mySqlDatabase){
            this.MySqlDatabase = mySqlDatabase;
        }
         public async Task InsertAsync()
        {
            using var cmd = MySqlDatabase.Connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Users (username,password,type) VALUES (@usr,@pw,@type);";
            cmd.Parameters.AddWithValue("@usr",this.username);
            cmd.Parameters.AddWithValue("@pw",this.password);
            cmd.Parameters.AddWithValue("@type",this.type.ToString());

            await cmd.ExecuteNonQueryAsync();
        }
    }
}