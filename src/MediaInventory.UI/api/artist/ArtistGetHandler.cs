using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.artist
{
    public class ArtistGetHandler
    {
        private readonly IRepository<Artist> _artists;

        public ArtistGetHandler(IRepository<Artist> artists)
        {
            _artists = artists;
        }

        public ArtistModel Execute_Id(RequestGuidId request)
        {
            return Mapper.Map<ArtistModel>(_artists.FirstOrThrowNotFound(x => x.Id == request.Id));
        }

        public List<ArtistModel> Execute()
        {
            return Mapper.Map<List<ArtistModel>>(_artists);
        }
    }
}