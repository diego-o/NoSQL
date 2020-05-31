using NoSQL.Infrastructure.Service;
using System;
using System.IO;

namespace NoSQL.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var service = new CollectionService("veiculos");

            using (var reader = new StreamReader(@"C:\Users\Diego\Desktop\Desafios\NoSql\veiculo.json"))
            {
                var teste = service.Insert(reader.ReadToEnd());
                var item = service.GetById("a8eac4e6-2fcd-4718-abe3-0db3b0f91443");
            }

            using (var reader = new StreamReader(@"C:\Users\Diego\Desktop\Desafios\NoSql\veiculo_update.json"))
            {
                var teste = service.Update(reader.ReadToEnd(), "27d78b59-e959-4699-8066-77ecd435f0da");
                var item = service.GetById("27d78b59-e959-4699-8066-77ecd435f0da");
            }

            Console.ReadKey();
        }
    }
}
