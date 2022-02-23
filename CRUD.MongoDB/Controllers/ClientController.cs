using CRUD.MongoDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Linq;

namespace CRUD.MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ClientController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            var dbClient = new MongoClient(_configuration.GetConnectionString("MongoDB"));

            var dbList = dbClient.GetDatabase("testSQL").GetCollection<Client>("Client").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Create(Client client)
        {
            var dbClient = new MongoClient(_configuration.GetConnectionString("MongoDB"));
            client.ClientId = Guid.NewGuid();

            dbClient.GetDatabase("testSQL").GetCollection<Client>("Client").InsertOne(client);

            return new JsonResult(client);
        }

        [HttpPut]
        public JsonResult Update(Client client)
        {
            var dbClient = new MongoClient(_configuration.GetConnectionString("MongoDB"));

            var filter = GetFilter<Client>("ClientId", client.ClientId);
            var clientUpdate = Builders<Client>.Update.Set("Name", client.Name);

            dbClient.GetDatabase("testSQL").GetCollection<Client>("Client").UpdateOne(filter, clientUpdate);

            return new JsonResult(client);
        }

        [HttpDelete]
        public JsonResult Delete(Client client)
        {
            var dbClient = new MongoClient(_configuration.GetConnectionString("MongoDB"));

            var filter = GetFilter<Client>("ClientId", client.ClientId);
            dbClient.GetDatabase("testSQL").GetCollection<Client>("Client").DeleteOne(filter);

            return new JsonResult(client);
        }



        private FilterDefinition<T> GetFilter<T>(string key, Guid value)
        {
            return Builders<T>.Filter.Eq(key, value);
        }

    }
}
