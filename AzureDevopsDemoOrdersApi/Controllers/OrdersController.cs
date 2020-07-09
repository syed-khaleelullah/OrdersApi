using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AzureDevopsDemoOrdersApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace AzureDevopsDemoOrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersRepository repository;
        private readonly IConfiguration config;

        public OrdersController(OrdersRepository repository, IConfiguration config)
        {
            this.repository = repository;
            this.config = config;
        }

        [HttpGet("{orderid}")]
        public async Task<IActionResult> Get(int orderid)
        {
            var order = repository.FindById(orderid);
            if (order == null)
                return NotFound();

            var dto = new OrderDTO
            {
                OrderId = order.OrderId,
                ProductId = order.ProductId,
                CustomerName = order.CustomerName,
                DeliveryAddress = order.DeliveryAddress,
                ProductDescription = "",
                ProductName = ""
            };


            var client = new HttpClient();
            client.BaseAddress = new Uri(config["ProductServiceUrl"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync("api/products/" + order.ProductId);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Product p = JsonConvert.DeserializeObject<Product>(content);
                dto.ProductName = p.Name;
                dto.ProductDescription = p.Description;
            }



            return Ok(dto);
        }
    }
}
