using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.venue
{
    public class VenueGetHandler
    {
        private readonly IRepository<Venue> _venues;
        private readonly IMapper _mapper;

        public VenueGetHandler(IRepository<Venue> venues, IMapper mapper)
        {
            _venues = venues;
            _mapper = mapper;
        }

        public VenueModel Execute_Id(RequestGuidId request)
        {
            return _mapper.Map<VenueModel>(_venues.FirstOrThrowNotFound(x => x.Id == request.Id));
        }

        public List<VenueModel> Execute()
        {
            return _mapper.Map<List<VenueModel>>(_venues);
        }
    }
}