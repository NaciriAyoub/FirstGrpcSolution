using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstGrpcService.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<GreeterService> _logger;
        public CustomerService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();
            if (request.UserId == 1)
            {
                output.FirstName = "Ayoub";
                output.LastName = "Naciri";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Imane";
                output.LastName = "Benmanssour";
            }
            else
            {
                output.FirstName = "AhmedHamza";
                output.LastName = "Naciri";
            }
            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(CustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel>() {
                 new CustomerModel() {
                                 FirstName = "Ayoub",
                                 LastName = "Naciri",
                                 Age = 33
                              },
                 new CustomerModel() {
                                 FirstName = "Imane",
                                 LastName = "Benmanssour",
                                 Age = 32
                              },
                 new CustomerModel() {
                                 FirstName = "AhmedHamza",
                                 LastName = "Naciri",
                                 Age = 10
                              },
                 new CustomerModel() {
                                 FirstName = "Amane",
                                 LastName = "Naciri",
                                 Age = 6
                              },
                 new CustomerModel() {
                                 FirstName = "Amir",
                                 LastName = "Naciri",
                                 Age = 1
                              },
            };
            foreach (var cust in customers)
            {
                //await Task.Delay(2000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
