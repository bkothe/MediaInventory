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
        public void should_call_resolve_artist()
        {
            var artistResolverService = Substitute.For<IArtistResolverService>();
            new ConcertPostHandler(Substitute.For<IConcertCreationService>(),
                new Mapper(new List<Profile> {new ConcertModelMapping(), new VenueModelMapping(), new ArtistModelMapping() }), artistResolverService, Substitute.For<IVenueResolverService>())
                .Execute(new ConcertPostHandler.Request { ArtistName = "Rush" });

            artistResolverService.Received(1).ResolveArtist(Arg.Is("Rush"));
        }

        [Test]
        public void should_call_resolve_venue()
        {
            var venueResolverService = Substitute.For<IVenueResolverService>();
            new ConcertPostHandler(Substitute.For<IConcertCreationService>(),
                new Mapper(new List<Profile> { new ConcertModelMapping(), new VenueModelMapping(), new ArtistModelMapping() }), Substitute.For<IArtistResolverService>(), venueResolverService)
                .Execute(new ConcertPostHandler.Request { VenueName = "Alpine Valley" });

            venueResolverService.Received(1).ResolveVenue(Arg.Is("Alpine Valley"));
        }
    }
}