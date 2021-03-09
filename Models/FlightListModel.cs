using System.Collections.Generic;
namespace airplane_ticketsystem.Models
{
    public class FlightListModel
    {
        public List<FlightModel> FlightList { get; set; }

        public FlightListModel(List<FlightModel>  flights)
        {
            FlightList = new List<FlightModel>();

            foreach(var flight in flights)
            {
                FlightList.Add(flight);
            }
        }
    }
}