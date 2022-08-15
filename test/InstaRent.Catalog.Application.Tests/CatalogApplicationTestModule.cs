using Volo.Abp.Modularity;

namespace InstaRent.Catalog;

[DependsOn(
    typeof(CatalogApplicationModule),
    typeof(CatalogDomainTestModule)
    )]
public class CatalogApplicationTestModule : AbpModule
{

}
