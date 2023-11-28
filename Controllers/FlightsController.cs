using AirlineApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

[Route("api/flights")]
[ApiController]
public class FlightsController : ControllerBase
{
    private static List<Flight> _flights = new List<Flight>
    {
        new Flight { FlightId = 1, Date = DateTime.Now, FlightNumber = "F123", From = "Izmır", To = "Warsaw", Price = 100.0m },
        new Flight { FlightId = 2, Date = DateTime.Now, FlightNumber = "F456", From = "CityB", To = "CityC", Price = 150.0m },
    };

    private static List<Ticket> _tickets = new List<Ticket>();

    private bool IsValidUser(string username, string password)
{
    return username == "your_username" && password == "your_password";
}

    [HttpPost("buy")]
    public ActionResult<string> BuyTicket([FromBody] BuyTicketRequest request)
    {
        var username = request.username;
        var password = request.password;

        if (!IsValidUser(username, password))
    {
        return Unauthorized("Authentication failed");
    }

        // Find the flight
        var flight = _flights.FirstOrDefault(f => f.Date.Date == request.Date.Date
            && f.From.Equals(request.From, StringComparison.OrdinalIgnoreCase)
            && f.To.Equals(request.To, StringComparison.OrdinalIgnoreCase));

        if (flight == null)
        {
            return NotFound("Flight not found");
        }

        // Create a ticket
        var ticket = new Ticket
        {
            TicketId = _tickets.Count + 1,
            PurchaseDate = DateTime.Now,
            PassengerName = username,
            Flight = flight
        };

        // Add the ticket to the list
        _tickets.Add(ticket);
        Console.WriteLine($"Ticket added: {ticket.TicketId}");

        return Ok("Ticket purchased successfully");
    }

    [HttpGet]
public ActionResult<IEnumerable<Flight>> GetFlights(int page = 1, int pageSize = 10)
{
    var totalFlights = _flights.Count;
    
    var flights = _flights
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(f => new
        {
            f.Date,
            f.FlightNumber,
            f.Price
        })
        .ToList();

    var result = new
    {
        TotalFlights = totalFlights,
        Page = page,
        PageSize = pageSize,
        Flights = flights
    };
    return Ok(result);
}


    [HttpGet("{flightId}/tickets")]
    public ActionResult<IEnumerable<Ticket>> GetFlightTickets(int flightId)
    {
        var tickets = _tickets.Where(t => t.Flight.FlightId == flightId).ToList();

        return Ok(tickets);
    }
}

public class BuyTicketRequest
{
    public DateTime Date { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    
   

}
