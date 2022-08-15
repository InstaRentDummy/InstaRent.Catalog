using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace InstaRent.Catalog;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(CatalogHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class CatalogConsoleApiClientModule : AbpModule
{

}
