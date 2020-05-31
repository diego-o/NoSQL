using Newtonsoft.Json.Linq;

namespace NoSQL.Infrastructure.Models
{
    public class ItemCollection
    {
        public string Id { get; set; }
        public JObject Item { get; set; }
    }
}
