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

        public ConcertGetHandler(IRepository<Concert> concerts)
        {
            _concerts = concerts;
        }

        public List<ConcertEnumerationModel> Execute()
        {
            return Mapper.Map<List<ConcertEnumerationModel>>(_concerts);
        }

        public ConcertModel Execute_Id(RequestGuidId request)
        {
            return Mapper.Map<ConcertModel>(_concerts.FirstOrThrowNotFound(x => x.Id == request.Id));
        }
    }
}