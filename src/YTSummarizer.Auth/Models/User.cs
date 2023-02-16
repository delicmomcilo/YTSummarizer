using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace YTSummarizer.Models;

[CollectionName("users")]
public class User : MongoIdentityUser<Guid>
{
    public string FullName { get; set; }
}