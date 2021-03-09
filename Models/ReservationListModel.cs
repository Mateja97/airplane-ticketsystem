using System.Collections.Generic;
namespace airplane_ticketsystem.Models
{
    public class ReservationListModel
    {
        public List<ReservationModel> ReservationList { get; set; }

        public ReservationListModel(List<ReservationModel>  reservations)
        {
            ReservationList = new List<ReservationModel>();

            foreach(var r in reservations)
            {
                ReservationList.Add(r);
            }
        }
    }
}