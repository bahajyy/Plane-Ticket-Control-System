// Models/Flight.cs
using System;

namespace AirlineApi.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public DateTime Date { get; set; }
        public string FlightNumber { get; set; } 
        public string From { get; set; }
        public string To { get; set; }
        public decimal Price { get; set; }
    }
}
