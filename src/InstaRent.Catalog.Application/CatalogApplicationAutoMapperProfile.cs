using InstaRent.Catalog.TotalClicks;
using InstaRent.Catalog.DailyClicks;
using InstaRent.Catalog.UserPreferences;
using System;
using InstaRent.Catalog.Shared;
using Volo.Abp.AutoMapper;
using InstaRent.Catalog.Bags;
using AutoMapper;

namespace InstaRent.Catalog;

public class CatalogApplicationAutoMapperProfile : Profile
{
    public CatalogApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Bag, BagDto>();

        CreateMap<DailyClick, DailyClickDto>();
        CreateMap<DailyClickWithNavigationProperties, DailyClickWithNavigationPropertiesDto>();
        CreateMap<Bag, LookupDto<Guid?>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.bag_name));

        CreateMap<TotalClick, TotalClickDto>();
        CreateMap<TotalClickWithNavigationProperties, TotalClickWithNavigationPropertiesDto>();
        CreateMap<Bag, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.bag_name));

        CreateMap<UserPreference, UserPreferenceDto>();
        CreateMap<Tag,TagDto>();
        CreateMap<TagDto, Tag>();
    }
}