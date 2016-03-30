using System;
using MediaInventory.Infrastructure.Common.Data.Orm;

namespace MediaInventory.Core.Media
{
    public interface IAudioDeletionService
    {
        void Delete(Guid audioId);
    }

    public class AudioDeletionService : IAudioDeletionService
    {
        private readonly IRepository<Audio> _audios;

        public AudioDeletionService(IRepository<Audio> audios)
        {
            _audios = audios;
        }

        public void Delete(Guid audioId)
        {
            _audios.Delete(audioId);
        }
    }
}