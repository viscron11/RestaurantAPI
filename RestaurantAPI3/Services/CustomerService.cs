using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantAPI3.Models;
using RestaurantAPI3.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.Logging;
using RestaurantAPI3.Exceptions;

namespace RestaurantAPI3.Services
{

    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        int Create(CreateCustomerDto dto);
        CustomerDto2 Get(int id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public CustomerService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = _dbContext
                .Customers
                .ToList();

            var customersDtos = _mapper.Map<List<Customer>>(customers);

            return customersDtos;
        }

        public CustomerDto2 Get(int id)
        {
            var customer = _dbContext
                .Customers
                .FirstOrDefault(r => r.Id == id);

            if (customer is null) throw new NotFoundException("Restaurant not found");
            var result = _mapper.Map<CustomerDto2>(customer);
            return result;
        }


        public int Create(CreateCustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();

            return customer.Id;
        }

    }
}
