using Bogus;
using Domain.Entities;

namespace Infrastructure.Data
{
    public class DbSeeder
    {
        private readonly PhotoStockDbContext dbContext;

        public DbSeeder(PhotoStockDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.Authors.Any())
                {
                    dbContext.Authors.AddRange(this.GetSampleData());
                    dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Author> GetSampleData()
        {
            var photoGenerator = new Faker<Photo>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Link, f => f.Internet.Url())
                .RuleFor(p => p.OriginalSize, f => Decimal.Round(f.Random.Decimal(0M, 1000M), 2)) 
                .RuleFor(p => p.Cost, f => Decimal.Round(f.Random.Decimal(0M, 1000M), 2))
                .RuleFor(p => p.NumberOfSales, f => f.Random.Int(0, 10000))
                .RuleFor(p => p.Rating, f => f.Random.Int(1, 5));

            var textGenerator = new Faker<Text>()
                .RuleFor(t => t.Name, f => f.Commerce.ProductName())
                .RuleFor(t => t.TextDesc, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Cost, f => Decimal.Round(f.Random.Decimal(0M, 1000M), 2))
                .RuleFor(p => p.NumberOfSales, f => f.Random.Int(0, 10000))
                .RuleFor(p => p.Rating, f => f.Random.Int(1, 5));

            var authorGenerator = new Faker<Author>()
                .RuleFor(a => a.FirstName, f => f.Person.FirstName)
                .RuleFor(a => a.LastName, f => f.Person.LastName)
                .RuleFor(a => a.NickName, f => f.Person.FullName)
                .RuleFor(a => a.DateOfBirth, f => f.Person.DateOfBirth)
                .RuleFor(a => a.Photo, f => photoGenerator.Generate())
                .RuleFor(a => a.Text, f => textGenerator.Generate());

            var authors = authorGenerator.Generate(30);

            return authors;
        }
    }
}
