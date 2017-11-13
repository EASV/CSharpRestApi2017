using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAppBLL;
using CustomerAppBLL.BusinessObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerRestAPI.Controllers
{
	[EnableCors("MyPolicy")]
	[Produces("application/json")]
	[Route("api/addresses")]
	public class AddressController : Controller
	{
        IBLLFacade facade;

        public AddressController(IBLLFacade facade)
        {
            this.facade = facade;
        }

		// POST: api/Addresses
		[HttpPost]
		public IActionResult Post([FromBody]AddressBO address)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			return Ok(facade.AddressService.Create(address));
		}



		// GET: api/Customers
		[HttpGet]
		public IEnumerable<AddressBO> Get()
		{

			return facade.AddressService.GetAll();
		}

		// GET: api/Customers/5
		[HttpGet("{id}")]
		public AddressBO Get(int id)
		{
			return facade.AddressService.Get(id);
		}
    }
}
