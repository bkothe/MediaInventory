using System;
using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Core.Media;
using MediaInventory.Tests.Common.Extensions;
using MediaInventory.Tests.Common.Fakes.Data;
using MediaInventory.UI.api.media.audio;
using NSubstitute;
using NUnit.Framework;
using Should;
using Mapper = MediaInventory.Infrastructure.Common.Objects.Mapper;

namespace MediaInventory.Tests.Unit.Ui.api.media.audio
{
    [TestFixture]
    public class AudioPostHandlerTests
    {
        private AudioPostHandler _audioPostHandler;
        private MemoryRepository<Artist> _artists;
        private IArtistCreationService _artistCreationService;

        [SetUp]
        public void SetUp()
        {
            _artists = new MemoryRepository<Artist>(x => x.Id);
            _artistCreationService = Substitute.For<IArtistCreationService>();
            _audioPostHandler = new AudioPostHandler(
                new AudioCreationService(new MemoryRepository<Audio>(x => x.Id), new AudioValidator(_artists)),
                new Mapper(new List<Profile> { new AudioModelMapping() }),
                _artists, _artistCreationService);
        }

        [Test]
        public void should_not_create_artist_when_artist_with_specified_name_exists()
        {
            _audioPostHandler.Execute(new AudioPostHandler.Request
            {
                ArtistName = _artists.Add(new Artist { Name = RandomString.GenerateAlphaNumeric() }).Name,
                Title = RandomString.GenerateAlphaNumeric(),
                MediaFormat = MediaFormat.Cassette,
                MediaCount = 1
            });

            _artistCreationService.DidNotReceive().Create(Arg.Any<string>());
        }

        [Test]
        public void should_create_artist_when_artist_with_specified_name_does_not_exist()
        {
            var artistName = RandomString.GenerateAlphaNumeric();
            var artist = new Artist { Id = Guid.NewGuid(), Name = artistName };

            _artistCreationService.Create(Arg.Do<string>(x => _artists.Add(artist))).Returns(artist);

            var result = _audioPostHandler.Execute(new AudioPostHandler.Request
            {
                ArtistName = artistName,
                Title = RandomString.GenerateAlphaNumeric(),
                MediaFormat = MediaFormat.Cassette,
                MediaCount = 1
            });

            _artistCreationService.Received().Create(Arg.Is(artistName));

            result.ArtistId.ShouldEqual(artist.Id);
            result.ArtistName.ShouldEqual(artist.Name);
        }
    }
}