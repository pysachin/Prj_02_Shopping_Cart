using Catalog.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection )
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfigureProduct());
            }
        }

        private static IEnumerable<Product> GetPreconfigureProduct()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "IPhone X",
                    Summary = "IPhone X",
                    Description = "IPhone ",
                    ImageFile = "product-1.png",
                    Price = 950.00M,
                    Category = "Smart Phone"

                },
                new Product()
                {
                    Name = "IPhone XR",
                    Summary = "IPhone XR",
                    Description = "IPhone ",
                    ImageFile = "product-1.png",
                    Price = 940.00M,
                    Category = "Smart Phone"

                },
                new Product()
                {
                    Name = "IPhone 12",
                    Summary = "IPhone 12",
                    Description = "IPhone ",
                    ImageFile = "product-1.png",
                    Price = 780.00M,
                    Category = "Smart Phone"

                },
                new Product()
                {
                    Name = "IPhone 13",
                    Summary = "IPhone 13",
                    Description = "IPhone ",
                    ImageFile = "product-1.png",
                    Price = 959.00M,
                    Category = "Smart Phone"

                },
                new Product()
                {
                    Name = "IPhone X-15",
                    Summary = "IPhone X-15",
                    Description = "IPhone ",
                    ImageFile = "product-1.png",
                    Price = 850.00M,
                    Category = "Smart Phone"

                },
                new Product()
                {
                    Name = "IPhone X-16",
                    Summary = "IPhone X",
                    Description = "IPhone ",
                    ImageFile = "product-1.png",
                    Price = 1950.00M,
                    Category = "Smart Phone"

                }
            };
        }
    }
}
