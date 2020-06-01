using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NoSQL.Infrastructure.Models;
using NoSQL.Infrastructure.Service.Interfaces;
using NoSQL.Infrastructure.Tools;

namespace NoSQL.Infrastructure.Service
{
    public class CollectionService : ICollectionService
    {
        private readonly string _collectionName;
        private readonly string _collectionFilePath;
        private CollectionModel _collection;

        public CollectionService(string collectionName)
        {
            this._collectionName = collectionName;
            this._collectionFilePath = $@"{Directory.GetCurrentDirectory()}\collection_{collectionName}.json";
            this.LoadCollection();
        }

        public string Insert(string json)
        {
            var obj = JsonConvert.DeserializeObject<JObject>(json);
            var id = Guid.NewGuid().ToString();
            obj.Add("id", id);
            var item = new ItemCollection()
            {
                Id = id,
                Item = obj
            };

            _collection.CollectionItens.Add(item);
            WriteFile();

            return ObjectToJson(obj);
        }

        public string Update(string json, string id)
        {
            var obj = _collection[id];
            if (obj == null)
                throw new ArgumentException("id não encontrado");

            _collection.CollectionItens.Remove(_collection.CollectionItens.Find(t => t.Id == id));

            var newObj = JsonConvert.DeserializeObject<JObject>(json);
            var item = new ItemCollection()
            {
                Id = id,
                Item = newObj
            };

            _collection.CollectionItens.Add(item);

            WriteFile();
            return ObjectToJson(newObj);
        }

        public void Delete(string id)
        {
            _collection.CollectionItens.Remove(_collection.CollectionItens.Find(t => t.Id == id));
            WriteFile();
        }

        public string GetById(string id)
        {
            var obj = _collection[id];
            return ObjectToJson(obj);
        }

        private void LoadCollection()
        {
            if (File.Exists(_collectionFilePath))
                _collection = JsonConvert.DeserializeObject<CollectionModel>(JsonTools.ReadJson(_collectionFilePath));
            else
                _collection = new CollectionModel(_collectionName);
        }

        private void WriteFile()
        {
            var strFile = JsonConvert.SerializeObject(_collection);
            File.WriteAllText(_collectionFilePath, strFile);
        }

        private string ObjectToJson(object obj) => JsonConvert.SerializeObject(obj);
    }
}