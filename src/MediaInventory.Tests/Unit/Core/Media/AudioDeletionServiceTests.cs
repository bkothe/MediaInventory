using FluentAssertions;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Fakes.Data;
using NUnit.Framework;

namespace MediaInventory.Tests.Unit.Core.Media
{
    [TestFixture]
    public class AudioDeletionServiceTests
    {
        private MemoryRepository<Audio> _audios;
        private AudioDeletionService _sut;

        [SetUp]
        public void SetUp()
        {
            _audios = new MemoryRepository<Audio>(x => x.Id);
            _sut = new AudioDeletionService(_audios);
        }

        [Test]
        public void should_delete_audio()
        {
            var audio = _audios.Add(new Audio());
            _audios.Should().Contain(x => x == audio);

            _sut.Delete(audio.Id);

            _audios.Should().NotContain(x => x == audio);
        }
    }
}