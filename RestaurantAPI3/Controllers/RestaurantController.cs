using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI3.Entities;
using RestaurantAPI3.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI3.Services;

namespace RestaurantAPI3.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ICustomerService _customerService;
        public RestaurantController(IRestaurantService restaurantService, ICustomerService customerService)
        {
            _restaurantService = restaurantService;
            _customerService = customerService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _restaurantService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _restaurantService.Create(dto);

            return Created($"/api/restaurant/{id}", null);
        }

        [HttpPost]
        [Route("customer")]
        public ActionResult CreateCustomer([FromBody] CreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var id = _customerService.Create(dto);

            return Created($"/api/restaurant/customer/{id}", null);
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {

            var restaurantsDtos = _restaurantService.GetAll();


            return Ok(restaurantsDtos);
        }

        [HttpGet]
        [Route("customers")]
        public ActionResult<IEnumerable<CustomerDto>> GetAllCustomers()
        {

            var customersDtos = _customerService.GetAll();


            return Ok(customersDtos);
        }
        
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute]int id)
        {
            var restaurant = _restaurantService.GetById(id);
            return Ok(restaurant);
        }

        
        [HttpGet("customer/{id}")]
        public ActionResult<CustomerDto2> GetCustomer([FromRoute] int id)
        {
            var customer = _customerService.Get(id);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateRestaurant([FromBody]UpdateRestaurantDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
             _restaurantService.Update(dto, id);
            return BadRequest();
        }
    }
}
 