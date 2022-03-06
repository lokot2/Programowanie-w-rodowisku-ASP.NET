using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IRepository<Customer> repository;
        private readonly IMapper mapper;

        public CustomersController(IRepository<Customer> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            var customers = repository.FindAll()
                .Include(c => c.MembershipType)
                .ToList();

            var customerDtos = mapper.Map<IEnumerable<CustomerDto>>(customers);

            return Ok(customerDtos);
        }

        // GET /api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(string id)
        {
            var customer = repository.FindByCondition(c => c.Id == id)
                .Include(c => c.MembershipType)
                .FirstOrDefault();

            var customerDto = mapper.Map<CustomerDto>(customer);

            return Ok(customerDto);
        }

        // POST /api/customers
        [HttpPost]
        public ActionResult CreateCustomer(CustomerDto customerDto)
        {
            var customer = mapper.Map<Customer>(customerDto);
            repository.Create(customer);

            return Created($"api/customers/{customer.Id}", null);
        }

        // PUT /api/customers/
        [HttpPut]
        public ActionResult UpdateCustomer(CustomerDto customerDto)
        {
            var customerInDb = repository.FindObject(c => c.Id == customerDto.Id);
            mapper.Map(customerDto, customerInDb);
            repository.Update(customerInDb);

            return NoContent();
        }

        // DELETE /api/customers/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
            var customerInDb = repository.FindObject(c => c.Id == id);
            repository.Delete(customerInDb);

            return NoContent();
        }
    }
}