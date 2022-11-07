using bART_TestProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bART_TestProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IncidentsContext _context;
        public AccountsController(IncidentsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Accounts!.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Account>> Post([FromBody] RequestEncapsulator request)
        {
            if (request.AccountName is null || request.ContactEmail is null)
                return BadRequest();

            Account account = new Account { Name = request.AccountName };
            await _context.Accounts!.AddAsync(account);
            await _context.SaveChangesAsync();

            await new ContactsController(_context).Post(request);
            return Ok(account);
        }
    }
}
