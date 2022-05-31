using AgendaApi.Interface.Service;
using AgendaApi.Logging;
using AgendaApi.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IAgendaService _service;
        private readonly ILogManager _logger;
        public ContactController(ILogManager logger, IAgendaService service)
        {
            _logger = logger;
            _service = service;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            try
            {
                IEnumerable<Contact> contacts = await _service.GetAll();
                if (contacts != null)
                    return Ok(contacts);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message);
                return Problem(ex?.Message);
            }
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(Guid id)
        {
            try
            {
                Contact contact = await _service.Get(id);
                if (contact != null)
                    return Ok(contact);
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex?.Message} id {id}");
                return Problem($"{ex?.Message} id {id}");
            }
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Contact newContact)
        {
            try
            {
                Guid newContactId = await _service.Create(newContact);
                if (newContactId != Guid.Empty)
                    return Ok(newContactId);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex?.Message} newContact {newContact}");
                return Problem($"{ex?.Message} newContact {newContact}");
            }
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Contact updateContact)
        {
            try
            {
                Contact contact = await _service.Update(id, updateContact);
                if (contact != null)
                    return Ok(contact);
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex?.Message} id {id} updateContact {updateContact}");
                return Problem($"{ex?.Message} id {id} updateContact {updateContact}");
            }
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                bool isContactDeleted = await _service.Delete(id);
                if (isContactDeleted)
                    return Ok($"Contact deleted {isContactDeleted}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex?.Message} id {id}");
                return Problem($"{ex?.Message} id {id}");
            }
        }
    }
}
