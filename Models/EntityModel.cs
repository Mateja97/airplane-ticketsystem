using System.ComponentModel.DataAnnotations;
namespace airplane_ticketsystem.Models
{
    public enum EntityType{
        Admin,
        User,
        Agent

    }
    public class EntityModel
    {
        public int entityId {get;set;}
        [Required]
        public string username { get; set; }
        [Required]
        public string password {get;set;}

        public EntityType type {get;set;}
    }
}