using FirstGrpcService;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace ConsoleAppGrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var input = new HelloRequest() { Name = "Ayoub" };
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            //var client = new Greeter.GreeterClient(channel);
            //var replay = await client.SayHelloAsync(input);
            //Console.WriteLine(replay.Message);

            
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Customer.CustomerClient(channel);

            var input = new CustomerLookupModel() { UserId = 1 };
            CustomerModel replay = await client.GetCustomerInfoAsync(input);
            Console.WriteLine($"First Name:{replay.FirstName} - Last Name: {replay.LastName}");

            var input1 = new CustomerLookupModel() { UserId = 2 };
            CustomerModel replay1 = await client.GetCustomerInfoAsync(input1);
            Console.WriteLine($"First Name:{replay1.FirstName} - Last Name: {replay1.LastName}");


            var input2 = new CustomerLookupModel() { UserId = 3 };
            CustomerModel replay2 = await client.GetCustomerInfoAsync(input2);
            Console.WriteLine($"First Name:{replay2.FirstName} - Last Name: {replay2.LastName}");


            Console.WriteLine();
            Console.WriteLine($"{Environment.NewLine}My Family stream");
            using (var call = client.GetNewCustomers(new CustomerRequest()))
            {
                while (await call.ResponseStream.MoveNext(new System.Threading.CancellationToken()))
                {
                    var currentCustomer = call.ResponseStream.Current;
                    Console.WriteLine($"First Name:{currentCustomer.FirstName} - Last Name: {currentCustomer.LastName} - Age:{currentCustomer.Age}");
                }
            }

            Console.ReadLine();
        }
    }
}
