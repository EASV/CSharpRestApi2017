using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace CustomerRestAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        BLLFacade facade = new BLLFacade();
        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerBO> Get()
        {

            return facade.CustomerService.GetAll();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public CustomerBO Get(int id)
        {
            return facade.CustomerService.Get(id);
        }
        
        // POST: api/Customers
        [HttpPost]
        public IActionResult Post([FromBody]CustomerBO cust)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 
            return Ok(facade.CustomerService.Create(cust));
        }
        
        //      api/ControllerName/id
        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerBO cust)
        {
            if(id != cust.Id)
            {
                return BadRequest("I AM LIVEEE");
            }
            try
            {
                var customer = facade.CustomerService.Update(cust);
                return Ok(customer);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(404, e.Message);
            }
            
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            facade.CustomerService.Delete(id);
        }
    }
}
//[EnableCors("MyPolicy")]
