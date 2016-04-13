using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Performance;
using MediaInventory.UI.api.artist;
using MediaInventory.UI.api.concert;
using MediaInventory.UI.api.venue;
using MediaInventory.UI.Core;
using NSubstitute;
using NUnit.Framework;
using Mapper = MediaInventory.Infrastructure.Common.Objects.Mapper;

namespace MediaInventory.Tests.Unit.Ui.api.concert
{
    [TestFixture]
    public class ConcertPostHandlerTests
    {
        [Test]
        public void should_call_get_artist()
        {
            var artistService = Substitute.For<IArtistService>();
            new ConcertPostHandler(Substitute.For<IConcertCreationService>(),
                new Mapper(new List<Profile> {new ConcertModelMapping(), new VenueModelMapping(), new ArtistModelMapping() }), artistService)
                .Execute(new ConcertPostHandler.Request { ArtistName = "Rush" });

            artistService.Received(1).GetOrCreateArtist(Arg.Is("Rush"));
        }
    }
}