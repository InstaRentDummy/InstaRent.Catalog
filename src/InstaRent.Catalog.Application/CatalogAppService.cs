using InstaRent.Catalog.Localization;
using Volo.Abp.Application.Services;

namespace InstaRent.Catalog;

public abstract class CatalogAppService : ApplicationService
{
    protected CatalogAppService()
    {
        LocalizationResource = typeof(CatalogResource);
        ObjectMapperContext = typeof(CatalogApplicationModule);
    }
}
