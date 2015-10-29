using System.Linq;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class CommercialAudioMediaDeletionServiceTests
    {
        private MemoryRepository<CommercialAudioMedia> _commercialAudioMediae;
        private CommercialAudioMediaDeletionService _commercialAudioMediaDeletionService;

        [SetUp]
        public void SetUp()
        {
            _commercialAudioMediae = new MemoryRepository<CommercialAudioMedia>(x => x.Id);
            _commercialAudioMediaDeletionService = new CommercialAudioMediaDeletionService(_commercialAudioMediae);
        }

        [Test]
        public void should_delete_commercial_audio_media()
        {
            var commercialAudioMedia = _commercialAudioMediae.Add(new CommercialAudioMedia());
            _commercialAudioMediae.Count(x => x.Id == commercialAudioMedia.Id).ShouldEqual(1);

            _commercialAudioMediaDeletionService.Delete(commercialAudioMedia.Id);

            _commercialAudioMediae.Count(x => x.Id == commercialAudioMedia.Id).ShouldEqual(0);
        }
    }
}