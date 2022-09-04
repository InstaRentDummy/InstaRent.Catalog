using Xunit;

namespace InstaRent.Catalog.MongoDB;

[CollectionDefinition(Name)]
public class MongoTestCollection : ICollectionFixture<MongoDbFixture>
{
    public const string Name = "Test_InstaRent";
}
