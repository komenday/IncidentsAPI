using bART_TestProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace bART_TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentsController : ControllerBase
    {
        private readonly IncidentsContext _context;
        public IncidentsController(IncidentsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Incidents!.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RequestEncapsulator request)
        {
            if (!_context.Accounts!.Any(acc => acc.Name == request.AccountName))
                return NotFound();

            Account account = await _context.Accounts!.FirstAsync(acc => acc.Name == request.AccountName);
            Contact? contact = await _context.Contacts!.FirstOrDefaultAsync(cont => cont.Email == request.ContactEmail);
            if (contact is not null)
            {
                await new ContactsController(_context).Put(request);
                return Ok();
            }
            else
            {
                Incident incident = new Incident
                {
                    Description = request.IncidentDescription,
                    Accounts = new List<Account>()
                };
                incident.Accounts.Add(account);

                _context.Incidents!.Add(incident);
                await _context.SaveChangesAsync();

                await new ContactsController(_context).Post(request);
                return Ok();
            }
        }
    }
}
