using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Media;
using MediaInventory.UI.api.media.audio;
using MediaInventory.UI.Core;
using NSubstitute;
using NUnit.Framework;
using Mapper = MediaInventory.Infrastructure.Common.Objects.Mapper;

namespace MediaInventory.Tests.Unit.Ui.api.media.audio
{
    [TestFixture]
    public class AudioPostHandlerTests
    {
        [Test]
        public void should_call_get_artist()
        {
            var artistService = Substitute.For<IArtistService>();

            new AudioPostHandler(Substitute.For<IAudioCreationService>(),
                new Mapper(new List<Profile> { new AudioModelMapping() }), artistService)
                .Execute(new AudioPostHandler.Request { ArtistName = "Rush" });

            artistService.Received(1).GetOrCreateArtist(Arg.Is("Rush"));
        }
    }
}