using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UOW.Entities.Domain
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }
    }
}
