using System;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common;
using MediaInventory.Tests.Common.Extensions;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class AudiosDsl
    {
        private readonly Context _context;
        private readonly ArtistsDsl _artistsDsl;

        public AudiosDsl(Context context)
        {
            _context = context;
            _artistsDsl = new ArtistsDsl(context);
        }

        public AudioDsl Create(Action<Audio> configure = null)
        {
            return new AudioDsl(_context, _context.Repositories
                .Audios.Add(new Audio
                {
                    Artist = _artistsDsl.Create().Artist,
                    Title = RandomString.GenerateAlphaNumeric(),
                    MediaFormat = MediaFormat.CompactDisc,
                    MediaCount = 1
                }
                .ActOn(configure))
                .ActOn(x => _context.Tracking.Audios.Add(x))
                .ThenDo(_context.Session.Flush));
        }
    }

    public class AudioDsl
    {
        private readonly Context _context;

        public AudioDsl(Context context, Audio audio)
        {
            _context = context;
            Audio = audio;
        }

        public Audio Audio { get; private set; }
    }
}