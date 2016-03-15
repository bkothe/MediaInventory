using System;
using MediaInventory.Core.Venue;
using MediaInventory.Infrastructure.Common;
using MediaInventory.Tests.Common.Extensions;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class VenuesDsl
    {
        private readonly Context _context;

        public VenuesDsl(Context context)
        {
            _context = context;
        }

        public VenueDsl Create(Action<Venue> configure = null)
        {
            return new VenueDsl(_context, _context.Repositories
                .VenueRepository.Add(new Venue
                {
                    Name = RandomString.GenerateAlphaNumeric(),
                    City = RandomString.GenerateAlphaNumeric(),
                    State = RandomString.GenerateAlphaNumeric(2)
                }
                .ActOn(configure))
                .ActOn(x => _context.Tracking.Venues.Add(x))
                .ThenDo(_context.Session.Flush));
        }
    }

    public class VenueDsl
    {
        private readonly Context _context;

        public VenueDsl(Context context, Venue venue)
        {
            _context = context;
            Venue = venue;
        }

        public Venue Venue { get; private set; }
    }
}