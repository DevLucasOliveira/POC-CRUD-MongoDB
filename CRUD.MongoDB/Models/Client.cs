using MongoDB.Bson;
using System;

namespace CRUD.MongoDB.Models
{
    public class Client
    {
        public ObjectId Id { get; set; }
        public Guid ClientId { get; set; }
        public string Name { get; set; }
    }
}
