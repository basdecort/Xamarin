using System;
using System.Collections.Generic;
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
    }

    public class EpisodeService : ServiceBase<EpisodeResult>, IEpisodeService
    {
        public EpisodeService() : base("https://raw.githubusercontent.com/jeffreylancaster/game-of-thrones/master/data/episodes.json")
        {
            CachedResult = new EpisodeResult();
        }

        public Episode Create(Episode obj)
        {
            CachedResult.episodes.Add(obj);
            return obj;
        }

        public async Task<IEnumerable<Episode>> GetAll()
            {
                var result = await Get();

                return result.episodes;
            }
        }
    }
