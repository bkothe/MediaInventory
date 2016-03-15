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

        public VenueGetHandler(IRepository<Venue> venues)
        {
            _venues = venues;
        }

        public VenueModel Execute_Id(RequestGuidId request)
        {
            return Mapper.Map<VenueModel>(_venues.FirstOrThrowNotFound(x => x.Id == request.Id));
        }

        public List<VenueModel> Execute()
        {
            return Mapper.Map<List<VenueModel>>(_venues);
        }
    }
}