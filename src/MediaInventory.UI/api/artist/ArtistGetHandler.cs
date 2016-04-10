using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediaInventory.Core.Artist;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.artist
{
    public class ArtistGetHandler
    {
        private readonly IRepository<Artist> _artists;
        private readonly IMapper _mapper;

        public class RequerstNameFilter
        {
            public string Query { get; set; }
        }

        public ArtistGetHandler(IRepository<Artist> artists, IMapper mapper)
        {
            _artists = artists;
            _mapper = mapper;
        }

        public ArtistModel Execute_Id(RequestGuidId request)
        {
            return _mapper.Map<ArtistModel>(_artists.FirstOrThrowNotFound(x => x.Id == request.Id));
        }

        public List<ArtistModel> Execute()
        {
            return _mapper.Map<List<ArtistModel>>(_artists);
        }

        public List<ArtistModel> Execute_Filter_Query(RequerstNameFilter filter)
        {
            return _mapper.Map<List<ArtistModel>>(_artists.Where(x => x.Name.Contains(filter.Query)));
        }
    }
}