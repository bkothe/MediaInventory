using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.concert
{
    public class ConcertGetHandler
    {
        private readonly IRepository<Concert> _concerts;
        private readonly IMapper _mapper;

        public ConcertGetHandler(IRepository<Concert> concerts, IMapper mapper)
        {
            _concerts = concerts;
            _mapper = mapper;
        }

        public List<ConcertEnumerationModel> Execute()
        {
            return _mapper.Map<List<ConcertEnumerationModel>>(_concerts);
        }

        public ConcertModel Execute_Id(RequestGuidId request)
        {
            return _mapper.Map<ConcertModel>(_concerts.FirstOrThrowNotFound(x => x.Id == request.Id));
        }
    }
}