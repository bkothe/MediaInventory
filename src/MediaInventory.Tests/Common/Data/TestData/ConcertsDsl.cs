using System;
using MediaInventory.Core.Performance;
using MediaInventory.Infrastructure.Common;
using MediaInventory.Tests.Common.Extensions;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class ConcertsDsl
    {
        private readonly Context _context;
        private readonly ArtistsDsl _artistsDsl;
        private readonly VenuesDsl _venuesDsl;

        public ConcertsDsl(Context context)
        {
            _context = context;
            _venuesDsl = new VenuesDsl(_context);
            _artistsDsl = new ArtistsDsl(_context);
        }

        public ConcertDsl Create(Action<Concert> configure = null)
        {
            return new ConcertDsl(_context, _context.Repositories
                .Concerts.Add(new Concert
                {
                    Artist = _artistsDsl.Create().Artist,
                    Date = DateTime.Now.AddMonths(-int.Parse(RandomString.GenerateNumeric(1))).Date,
                    Venue = _venuesDsl.Create().Venue
                }
                .ActOn(configure))
                .ActOn(x => _context.Tracking.Concerts.Add(x))
                .ThenDo(_context.Session.Flush));
        }
    }

    public class ConcertDsl
    {
        private readonly Context _context;

        public ConcertDsl(Context context, Concert concert)
        {
            _context = context;
            Concert = concert;
        }

        public Concert Concert { get; private set; }
    }
}