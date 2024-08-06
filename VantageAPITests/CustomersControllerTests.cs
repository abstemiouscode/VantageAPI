using Microsoft.EntityFrameworkCore;
using VantageAPI;
using VantageAPI.Controllers;
using VantageAPI.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace VantageAPITests
{
        public class CustomersControllerTests
        {
            private CustomersController _controller;
            private CustomerContext _context;

            [SetUp]
            public void Setup()
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                var options = new DbContextOptionsBuilder<CustomerContext>()
                    .UseInMemoryDatabase("CustomerListTest")
                    .UseInternalServiceProvider(serviceProvider)
                    .Options;

                _context = new CustomerContext(options);
                _context.Customers.Add(new Customer
                {
                    Name = "Test Customer",
                    Address = "123 Test St",
                    Country = "Testland",
                    PhoneNumber = "1234 218469",
                    Website = "https://test.com"
                });
                _context.SaveChanges();

                _controller = new CustomersController(_context);
            }

            [Test]
            public async Task GetCustomers_ReturnsAllCustomers()
            {
                var result = await _controller.GetCustomers();
                var customers = result.Value as List<Customer>;
                Assert.IsNotNull(customers);
                Assert.AreEqual(1, customers.Count);
            }

            [Test]
            public async Task GetCustomer_ReturnsCustomerById()
            {
                var result = await _controller.GetCustomer(1);
                var customer = result.Value as Customer;
                Assert.IsNotNull(customer);
                Assert.AreEqual("Test Customer", customer.Name);
            }

            [Test]
            public async Task PostCustomer_CreatesNewCustomer()
            {
                var newCustomer = new Customer
                {
                    Name = "New Customer",
                    Address = "456 New St",
                    Country = "United Kingdom",
                    PhoneNumber = "01234 222124",
                    Website = "https://new.com"
                };

                var result = await _controller.PostCustomer(newCustomer);
                var createdAtActionResult = result.Result as CreatedAtActionResult;
                var customer = createdAtActionResult.Value as Customer;

                Assert.IsNotNull(createdAtActionResult);
                Assert.That(createdAtActionResult.ActionName, Is.EqualTo("GetCustomer"));
                Assert.IsNotNull(customer);
                Assert.That(customer.Name, Is.EqualTo("New Customer"));
            }

            [Test]
            public async Task PutCustomer_UpdatesExistingCustomer()
            {
                var customer = await _context.Customers.FirstOrDefaultAsync();
                customer.Name = "Updated Customer";

                var result = await _controller.PutCustomer(customer.Id, customer);
                var noContentResult = result as NoContentResult;

                Assert.IsNotNull(noContentResult);
                Assert.That(noContentResult.StatusCode, Is.EqualTo(204));

                var updatedCustomer = await _context.Customers.FindAsync(customer.Id);
                Assert.That(updatedCustomer?.Name, Is.EqualTo("Updated Customer"));
            }

            [Test]
            public async Task DeleteCustomer_RemovesCustomer()
            {
                var customer = await _context.Customers.FirstOrDefaultAsync();

                var result = await _controller.DeleteCustomer(customer.Id);
                var noContentResult = result as NoContentResult;

                Assert.IsNotNull(noContentResult);
                Assert.That(noContentResult.StatusCode, Is.EqualTo(204));

                var deletedCustomer = await _context.Customers.FindAsync(customer.Id);
                Assert.IsNull(deletedCustomer);
            }
        }
    }
