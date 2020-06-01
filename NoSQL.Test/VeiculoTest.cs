using Newtonsoft.Json;
using NoSQL.Infrastructure.Service;
using NoSQL.Infrastructure.Service.Interfaces;
using NoSQL.Test.Model;
using NUnit.Framework;
using System.IO;

namespace NoSQL.Test
{
    public class VeiculoTest
    {
        private ICollectionService _collectionService;
        private readonly string _rootPath = $@"{Directory.GetCurrentDirectory()}\JSON\";
        private VeiculoModel _veiculoModel;

        [SetUp]
        public void Setup()
        {
            _collectionService = new CollectionService("veiculo");
        }

        [Test]
        public void STEP01_Salvar()
        {
            var json = string.Empty;
            using (var reader = new StreamReader($"{_rootPath}veiculo.json"))
                json = reader.ReadToEnd();

            _veiculoModel = JsonConvert.DeserializeObject<VeiculoModel>(_collectionService.Insert(json));
            Assert.IsTrue(!string.IsNullOrEmpty(_veiculoModel.id));
        }

        [Test]
        public void STEP02_Consultar()
        {
            this.STEP01_Salvar();
            var veiculo = JsonConvert.DeserializeObject<VeiculoModel>(_collectionService.GetById(_veiculoModel.id));
            Assert.IsNotNull(veiculo);
        }

        [Test]
        public void STEP03_Alterar()
        {
            this.STEP01_Salvar();
            var veiculo = JsonConvert.DeserializeObject<VeiculoModel>(_collectionService.GetById(_veiculoModel.id));
            veiculo.marca = "Volkswagen";
            veiculo.modelo = "Golf GTI 2.0";
            veiculo.ano = "2015";

            _collectionService.Update(JsonConvert.SerializeObject(veiculo), veiculo.id);

            var veiculoAtualizado = JsonConvert.DeserializeObject<VeiculoModel>(_collectionService.GetById(_veiculoModel.id));

            Assert.IsNotNull(veiculoAtualizado);
            Assert.IsTrue(veiculoAtualizado.marca == veiculo.marca);
            Assert.IsTrue(veiculoAtualizado.modelo == veiculo.modelo);
            Assert.IsTrue(veiculoAtualizado.ano == veiculo.ano);
        }
    }
}