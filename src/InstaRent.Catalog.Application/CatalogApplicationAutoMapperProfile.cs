using AutoMapper;
using InstaRent.Catalog.Bags;

namespace InstaRent.Catalog;

public class CatalogApplicationAutoMapperProfile : Profile
{
    public CatalogApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Bag, BagDto>();
    }
}
