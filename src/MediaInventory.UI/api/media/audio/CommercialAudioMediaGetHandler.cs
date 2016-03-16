using System.Collections.Generic;
using AutoMapper;
using MediaInventory.Core.Media;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.UI.api.media.audio
{
    public class CommercialAudioMediaGetHandler
    {
        private readonly IRepository<CommercialAudioMedia> _commercialAudioMediae;

        public CommercialAudioMediaGetHandler(IRepository<CommercialAudioMedia> commercialAudioMediae)
        {
            _commercialAudioMediae = commercialAudioMediae;
        }

        public List<CommercialAudioMediaModel> Execute()
        {
            return Mapper.Map<List<CommercialAudioMediaModel>>(_commercialAudioMediae);
        }

        public CommercialAudioMediaModel Execute_Id(RequestGuidId request)
        {
            return Mapper.Map<CommercialAudioMediaModel>(_commercialAudioMediae.Get(request.Id));
        }
    }
}