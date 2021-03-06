﻿using System.Collections.Generic;

namespace MediaInventory.Ui.api.media.audio
{
    public class AudioGetHandler
    {
        private readonly IRepository<Audio> _audios;
        private readonly IMapper _mapper;

        public AudioGetHandler(IRepository<Audio> audios, IMapper mapper)
        {
            _audios = audios;
            _mapper = mapper;
        }

        public List<AudioModel> Execute()
        {
            return _mapper.Map<List<AudioModel>>(_audios);
        }

        public AudioModel Execute_Id(RequestGuidId request)
        {
            var l = _audios.Get(request.Id);
            return _mapper.Map<AudioModel>(l);
        }
    }
}