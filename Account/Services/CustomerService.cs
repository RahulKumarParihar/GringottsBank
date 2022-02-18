﻿using AutoMapper;
using BankLibrary.Abstracts;
using BankLibrary.Data;
using BankLibrary.Data.Tables;
using BankLibrary.DTOs;
using BankLibrary.Models;
using BankLibrary.RequestParameters;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankLibrary.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankDbContext _bankDbContext;
        private readonly IMapper _mapper;

        public CustomerService(BankDbContext bankDbContext, IMapper mapper)
        {
            _bankDbContext = bankDbContext;
            _mapper = mapper;
        }

        public PagedResponse<CustomerDto> GetCustomers(Parameters customerParameters)
        {
            var query = _bankDbContext.Customers.AsNoTracking();

            var result =  PagedResponse<Customer>.ToPagedList(query, customerParameters.PageNumber, customerParameters.PageSize);   

            return _mapper.Map<PagedResponse<CustomerDto>>(result);
        }

        public PagedResponse<CustomerDto> GetCustomer(int id)
        {
            var query = _bankDbContext.Customers.AsNoTracking().Where(cust => cust.Id == id);

            var result = PagedResponse<Customer>.ToPagedList(query, 1, 1);

            return _mapper.Map<PagedResponse<CustomerDto>>(result);
        }
    }
}