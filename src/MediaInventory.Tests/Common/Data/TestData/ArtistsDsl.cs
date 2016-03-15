using System;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common;
using MediaInventory.Tests.Common.Extensions;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class ArtistsDsl
    {
        private readonly Context _context;

        public ArtistsDsl(Context context)
        {
            _context = context;
        }

        public ArtistDsl Create(Action<Artist> configure = null)
        {
            return new ArtistDsl(_context, _context.Repositories
                .ArtistRepository.Add(new Artist
                {
                    Name = RandomString.GenerateAlphaNumeric()
                }
                .ActOn(configure))
                .ActOn(x => _context.Tracking.Artists.Add(x))
                .ThenDo(_context.Session.Flush));
        }
    }

    public class ArtistDsl
    {
        private readonly Context _context;

        public ArtistDsl(Context context, Artist artist)
        {
            _context = context;
            Artist = artist;
        }

        public Artist Artist { get; private set; }
    }
}