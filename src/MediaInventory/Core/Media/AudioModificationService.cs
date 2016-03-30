using System;
using MediaInventory.Infrastructure.Common.Collections;
using MediaInventory.Infrastructure.Common.Data.Orm;
using MediaInventory.Infrastructure.Common.Exceptions;

namespace MediaInventory.Core.Media
{
    public interface IAudioModificationService
    {
        Audio Modify(Guid audioId, Action<Audio> modify);
    }

    public class AudioModificationService : IAudioModificationService
    {
        private readonly IRepository<Audio> _audios;
        private readonly AudioValidator _audioValidator;

        public AudioModificationService(IRepository<Audio> audios, AudioValidator audioValidator)
        {
            _audios = audios;
            _audioValidator = audioValidator;
        }

        public Audio Modify(Guid audioId, Action<Audio> modify)
        {
            var audio = _audios.FirstOrThrowNotFound(x => x.Id == audioId);

            TryModify(audio, modify);

            modify(audio);

            return audio;
        }

        private void TryModify(Audio originalAudio, Action<Audio> modify)
        {
            var audio = originalAudio.Clone();

            modify(audio);

            _audioValidator.ValidateAndThrow(audio);
        }
    }
}