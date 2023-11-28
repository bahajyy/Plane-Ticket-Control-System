// Controllers/TicketController.cs
using AirlineApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/tickets")]
[ApiController]
public class TicketController : ControllerBase
{
    private static List<Ticket> _tickets = new List<Ticket>();

    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetTickets()
    {
         Console.WriteLine($"Number of tickets: {_tickets.Count}");
        return Ok(_tickets);
    }

    [HttpGet("{ticketId}")]
    public ActionResult<Ticket> GetTicket(int ticketId)
    {
        var ticket = _tickets.FirstOrDefault(t => t.TicketId == ticketId);

        if (ticket == null)
        {
            return NotFound("Ticket not found");
        }

        return Ok(ticket);
    }
}
