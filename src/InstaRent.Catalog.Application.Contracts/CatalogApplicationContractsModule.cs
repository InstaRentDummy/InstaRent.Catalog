using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace InstaRent.Catalog;

[DependsOn(
    typeof(CatalogDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class CatalogApplicationContractsModule : AbpModule
{

}
