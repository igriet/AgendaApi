using AgendaApi.Interface;
using AgendaApi.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AgendaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IService _service;
        public ContactController(IService service)
        {
            _service = service;
        }

        // GET: api/<ContactController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            IEnumerable<Contact> contacts =await _service.Read();
            return Ok(contacts);
        }

        // GET api/<ContactController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(Guid id)
        {
            Contact contact =await _service.Get(id);
            return Ok(contact);
        }

        // POST api/<ContactController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Contact newContact)
        {
            Guid newContactId=await _service.Create(newContact);
            return Ok(newContactId);
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Contact updateContact)
        {
            Contact contact = await _service.Update(id, updateContact);
            return Ok(contact);
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool isContactDeleted = await _service.Delete(id);
            return Ok($"Contact deleted {isContactDeleted}");
        }
    }
}
