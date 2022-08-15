using InstaRent.Catalog.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace InstaRent.Catalog;

public abstract class CatalogController : AbpControllerBase
{
    protected CatalogController()
    {
        LocalizationResource = typeof(CatalogResource);
    }
}
