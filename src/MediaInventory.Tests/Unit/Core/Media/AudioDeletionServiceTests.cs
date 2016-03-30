using System.Linq;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;
using Should;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class AudioDeletionServiceTests
    {
        private MemoryRepository<Audio> _audios;
        private AudioDeletionService _audioDeletionService;

        [SetUp]
        public void SetUp()
        {
            _audios = new MemoryRepository<Audio>(x => x.Id);
            _audioDeletionService = new AudioDeletionService(_audios);
        }

        [Test]
        public void should_delete_audio()
        {
            var audio = _audios.Add(new Audio());
            _audios.Count(x => x.Id == audio.Id).ShouldEqual(1);

            _audioDeletionService.Delete(audio.Id);

            _audios.Count(x => x.Id == audio.Id).ShouldEqual(0);
        }
    }
}