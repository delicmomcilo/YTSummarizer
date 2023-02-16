using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YTSummarizer.Auth.Models
{
    public class Data
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? CreatedBy { get; set; } = null!;
        public string SummaryContent { get; set; } = null!;
        public string Category { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
