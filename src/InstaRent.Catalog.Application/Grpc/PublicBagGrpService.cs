using InstaRent.Catalog.Bags;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;
using Grpc.Core;

namespace InstaRent.Catalog.Grpc
{
    public class PublicBagGrpService : BagPublic.BagPublicBase
    {
        private readonly IRepository<Bag, Guid> _bagRepository;
        private readonly IObjectMapper _objectMapper;

        public PublicBagGrpService(IRepository<Bag, Guid> bagRepository, IObjectMapper objectMapper)
        {
            _bagRepository = bagRepository;
            _objectMapper = objectMapper;
        }

        public override async Task<BagResponse> GetById(BagRequest request, ServerCallContext context)
        {
            var product = await _bagRepository.GetAsync(Guid.Parse(request.Id));
            return _objectMapper.Map<Bag, BagResponse>(product);
        }
    }

}
