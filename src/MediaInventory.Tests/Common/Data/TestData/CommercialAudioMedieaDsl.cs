using System;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common;
using MediaInventory.Tests.Common.Extensions;

namespace MediaInventory.Tests.Common.Data.TestData
{
    public class CommercialAudioMedieaDsl
    {
        private readonly Context _context;
        private readonly ArtistsDsl _artistsDsl;

        public CommercialAudioMedieaDsl(Context context)
        {
            _context = context;
            _artistsDsl = new ArtistsDsl(context);
        }

        public CommercialAudioMediaDsl Create(Action<CommercialAudioMedia> configure = null)
        {
            return new CommercialAudioMediaDsl(_context, _context.Repositories
                .CommercialAudioMediaRepository.Add(new CommercialAudioMedia
                {
                    Artist = _artistsDsl.Create().Artist,
                    Title = RandomString.GenerateAlphaNumeric(),
                    MediaFormat = MediaFormat.CompactDisc,
                    MediaCount = 1
                }
                .ActOn(configure))
                .ActOn(x => _context.Tracking.CommercialAudioMediae.Add(x))
                .ThenDo(_context.Session.Flush));
        }
    }

    public class CommercialAudioMediaDsl
    {
        private readonly Context _context;

        public CommercialAudioMediaDsl(Context context, CommercialAudioMedia commercialAudioMedia)
        {
            _context = context;
            CommercialAudioMedia = commercialAudioMedia;
        }

        public CommercialAudioMedia CommercialAudioMedia { get; private set; }
    }
}