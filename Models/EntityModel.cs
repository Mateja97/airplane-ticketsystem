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
    }
}