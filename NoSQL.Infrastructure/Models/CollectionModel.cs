using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace NoSQL.Infrastructure.Models
{
    public class CollectionModel
    {
        public CollectionModel(string collectionName)
        {
            CollectionName = collectionName;
            CollectionItens = new List<ItemCollection>();
        }

        public string CollectionName { get; set; }
        public List<ItemCollection> CollectionItens { get; set; }

        public JObject this[string index]
        {
            get { return this.CollectionItens.Find(t => t.Id == index).Item; }
        }
    }
}
