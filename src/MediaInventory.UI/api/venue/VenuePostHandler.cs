namespace MediaInventory.Ui.api.venue
{
    public class VenuePostHandler
    {
        public class Request
        {
            public string Name { get; set; }
            public string City { get; set; }
            public string State { get; set; }
        }

        private readonly VenueCreationService _venueCreationService;
        private readonly IMapper _mapper;

        public VenuePostHandler(VenueCreationService venueCreationService, IMapper mapper)
        {
            _venueCreationService = venueCreationService;
            _mapper = mapper;
        }

        public VenueModel Execute(Request request)
        {
            return _mapper.Map<VenueModel>(_venueCreationService.Create(request.Name, request.City, request.State));
        }
    }
}