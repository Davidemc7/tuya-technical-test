using Application.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace Presentation.Test
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTest(WebApplicationFactory<Program> fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task Get_All_Customers_Return_Ok()
        {
            // Arrange
            var request = "/api/Customer/GetAll";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.IsType<List<CustomerDTO>>(response.Content.ReadFromJsonAsync<List<CustomerDTO>>().Result);
        }

        [Fact]
        public async Task Get_Customer_By_Id_Return_Ok()
        {
            // Arrange
            var request = "/api/Customer/GetById?Id=1";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Fact]
        public async Task Get_Customer_By_Id_Return_NotFound()
        {
            // Arrange
            var request = "/api/Customer/GetById?Id=0";

            // Act
            var response = await _client.GetAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Add_Customer_Return_Ok()
        {
            // Arrange
            var request = "/api/Customer/Add";
            var customer = new CustomerDTO
            {
                GivenName = "John",
                FamilyName = "Doe",
                Email = "jhon@doe.com",
                Phone = "1234567890",
                Address = "123 Main St, Anytown, USA"
            };

            // Act
            var response = await _client.PostAsJsonAsync(request, customer);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Add_Customer_Return_BadRequest()
        {
            // Arrange
            var request = "/api/Customer/Add";
            var customer = new CustomerDTO
            {
                GivenName = "John"
            };

            // Act
            var response = await _client.PostAsJsonAsync(request, customer);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_Customer_Return_Ok()
        {
            // Arrange
            var request = "/api/Customer/Update";
            var customer = new CustomerDTO
            {
                CustomerId = 4,
                GivenName = "John",
                FamilyName = "Does",
                Email = "jhon@does.com",
                Phone = "1234567890",
                Address = "123 Main St, Anytown, USA"
            };

            // Act
            var response = await _client.PutAsJsonAsync(request, customer);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Update_Customer_Return_BadRequest()
        {
            // Arrange
            var request = "/api/Customer/Update";
            var customer = new CustomerDTO
            {
                CustomerId = 4,
                GivenName = "John"
            };

            // Act
            var response = await _client.PutAsJsonAsync(request, customer);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Update_Customer_Return_NotFound()
        {
            // Arrange
            var request = "/api/Customer/Update";
            var customer = new CustomerDTO
            {
                CustomerId = 0,
                GivenName = "John",
                FamilyName = "Does",
                Email = "jhon@does.com",
                Phone = "1234567890",
                Address = "123 Main St, Anytown, USA"
            };

            // Act
            var response = await _client.PutAsJsonAsync(request, customer);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeleteLogically_Customer_By_Id_Return_Ok()
        {
            // Arrange
            var request = "/api/Customer/DeleteLogicallyById?Id=5";

            // Act
            var response = await _client.DeleteAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteLogically_Customer_By_Id_Return_NotFound()
        {
            // Arrange
            var request = "/api/Customer/DeleteLogicallyById?Id=0";

            // Act
            var response = await _client.DeleteAsync(request);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}