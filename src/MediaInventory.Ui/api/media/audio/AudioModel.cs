using System;

namespace MediaInventory.Ui.api.media.audio
{
    public class AudioModel
    {
        public Guid Id { get; set; }
        public Guid ArtistId { get; set; }
        public string ArtistName { get; set; }
        public string Title { get; set; }
        public MediaFormat MediaFormat { get; set; }
        public DateTime? Released { get; set; }
        public DateTime? Purchased { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string PurchaseLocation { get; set; }
        public int MediaCount { get; set; }
        public string Notes { get; set; }
    }

    public class AudioModelMapping : Profile
    {
        protected override void Configure()
        {
            CreateMap<Audio, AudioModel>();
        }
    }
}