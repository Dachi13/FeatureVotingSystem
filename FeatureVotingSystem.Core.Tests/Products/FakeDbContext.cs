using FeatureVotingSystem.Core.Shared.Models;

namespace FeatureVotingSystem.Core.Tests.Products
{
    public static class FakeDbContext
    {
        private static List<Product> _products = GenerateDummyData();

        public static List<Product> GetListOfProducts() => _products;

        private static List<Product> GenerateDummyData()
        {
            List<Product> productsList = new List<Product>();

            for (int i = 0; i < 10; i++)
            {
                productsList.Add(new Product
                {
                    Id = i,
                    UserId = i,
                    Name = $"Product {i}",
                    ShortDesc = $"Short Description {i}",
                    IsDeleted = 0
                });
            }

            return productsList;
        }
    }
}
