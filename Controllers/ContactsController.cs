using AssignmentApi.Data;
using AssignmentApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace AssignmentApi.Controllers
{
    [ApiController]
    [Route("Api/Contacts")]
    public class ContactsController : Controller
    {
        private readonly ContactsApiContext apiContext;

        public ContactsController(ContactsApiContext apiContext)
        {
            this.apiContext = apiContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await apiContext.Contact.ToListAsync());

        }

        //[HttpGet]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> GetContact([FromRoute] Guid id)
        //{
        //    var contacts = await apiContext.Contact.FindAsync(id);
        //    if (contacts == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(contacts);
        //}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id)
        {
            var contacts = await apiContext.Contact.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }
            return Ok(contacts);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contacts()
            {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                Email = addContactRequest.Email,
                PhoneNumber = addContactRequest.PhoneNumber,
                Address = addContactRequest.Address,
            };
            await apiContext.Contact.AddAsync(contact);
            await apiContext.SaveChangesAsync();

            return Ok(apiContext.Contact);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contacts = await apiContext.Contact.FindAsync(id);
            if (contacts != null)
            {
                contacts.FullName = updateContactRequest.FullName;
                contacts.Address = updateContactRequest.Address;
                contacts.PhoneNumber = updateContactRequest.PhoneNumber;
                contacts.Email = updateContactRequest.Email;

                await apiContext.SaveChangesAsync();
                return Ok(contacts);
            }
            return NotFound();

        }

        //[HttpGet]
        //[Route("{id:guid}")]
        //public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        //{
        //    var contact = await apiContext.Contact.FindAsync(id);
        //    if (contact != null)
        //    {
        //        apiContext.Remove(contact);
        //        await apiContext.SaveChangesAsync();
        //        return Ok(contact);
        //    }
        //    return NotFound();
        //}
    }
}
