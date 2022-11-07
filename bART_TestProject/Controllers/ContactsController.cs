using bART_TestProject.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bART_TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IncidentsContext _context;
        public ContactsController(IncidentsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Contacts!.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Contact>> Post([FromBody] RequestEncapsulator request)
        {
            if (request.ContactFirstName is null || request.ContactLastName is null || request.ContactEmail is null)
                return BadRequest();

            if (await _context.Contacts!.AnyAsync(c => c.Email == request.ContactEmail))
            {
                await Put(request);
                return Ok();
            }

            Account? account = await _context.Accounts!.FirstAsync(acc => acc.Name == request.AccountName);

            Contact contact = new Contact
            {
                FirstName = request.ContactFirstName,
                LastName = request.ContactLastName,
                Email = request.ContactEmail,
                Account = account,
                AccountId = account?.Id
            };

            await _context.Contacts!.AddAsync(contact);
            await _context.SaveChangesAsync();
            return new ObjectResult(contact);
        }

        [HttpPut]
        public async Task<ActionResult<Contact>> Put([FromBody] RequestEncapsulator request)
        {
            if (request.ContactFirstName is null || request.ContactLastName is null || request.ContactEmail is null)
                return BadRequest();

            Contact? contact = await _context.Contacts!.Where(c => c.Email == request.ContactEmail).FirstOrDefaultAsync();
            if (contact is null)
                return NotFound();
            else
                _context.Entry(contact).State = EntityState.Detached;

            Account? account = await _context.Accounts!.FirstAsync(acc => acc.Name == request.AccountName);

            Contact modifiedContact = new Contact
            {
                Id = contact.Id,
                FirstName = request.ContactFirstName,
                LastName = request.ContactLastName,
                Email = request.ContactEmail,
                Account = account,
                AccountId = account?.Id
            };

            _context.Entry(modifiedContact).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(modifiedContact);
        }
    }
}