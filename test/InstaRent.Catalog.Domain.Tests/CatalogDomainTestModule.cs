﻿ 
using InstaRent.Catalog.MongoDB;
using Volo.Abp.Modularity;

namespace InstaRent.Catalog;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(CatalogMongoDbTestModule)
    )]
public class CatalogDomainTestModule : AbpModule
{

}
