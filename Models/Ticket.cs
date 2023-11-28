// Models/Ticket.cs
using System;

namespace AirlineApi.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string PassengerName { get; set; } 
        public Flight Flight { get; set; }
    }
}
