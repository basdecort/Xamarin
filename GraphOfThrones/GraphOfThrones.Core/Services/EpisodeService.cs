using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using GraphOfThrones.Core.Models;

namespace GraphOfThrones.Core.Services
{
    public class EpisodeResult
    {
        public List<Episode> episodes { get; set; } = new List<Episode>();
    }

    public interface IEpisodeService : IService<Episode>
    {
        Episode Create(Episode episode);
        IObservable<Episode> EpisodeAdded();
    }

    public class EpisodeService : ServiceBase<EpisodeResult>, IEpisodeService
    {
        private readonly ISubject<Episode> _sub = new ReplaySubject<Episode>(1);

        public EpisodeService() : base("https://raw.githubusercontent.com/jeffreylancaster/game-of-thrones/master/data/episodes.json")
        {
            CachedResult = new EpisodeResult();
        }

        public Episode Create(Episode obj)
        {
            CachedResult.episodes.Add(obj);
            _sub.OnNext(obj);
            return obj;
        }

        public IObservable<Episode> EpisodeAdded()
        {
            return _sub.AsObservable();
        }

        public async Task<IEnumerable<Episode>> GetAll()
        {
            var result = await Get();

            return result.episodes;
        }
    }
}
