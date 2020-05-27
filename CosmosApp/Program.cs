using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosApp
{
    class Program
    {
        static void Main(string[] args)
        {
            QueryForDocument().Wait();
        }

        private async  static Task QueryForDocument()
        {
            var endpoint = ConfigurationManager.AppSettings["CosmosEndPoint"];
            var masterkey = ConfigurationManager.AppSettings["CosmosMasterkey"];
            using (var client = new CosmosClient(endpoint, masterkey))
            {
                var container = client.GetContainer("mydb", "mystore");
                var sql = "SELECT * FROM c";
                var iterator = container.GetItemQueryIterator<dynamic>(sql);
                var page = await iterator.ReadNextAsync();
                foreach( var doc in page )
                {
                    Console.WriteLine($"empId { doc.emp.id} has empName {doc.emp.Name}");
                    Console.ReadLine();
                }
            }
        }
    }
}
