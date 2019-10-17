using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphOfThrones.Core.Models;

namespace GraphOfThrones.Core.Services
{
    public class EpisodeResult
    {
        public List<Episode> episodes { get; set; }
    }

    public interface IEpisodeService
    {
        Task<IEnumerable<Episode>> GetAll();
    }

    public class EpisodeService : ServiceBase<EpisodeResult>, IEpisodeService
    {
            public EpisodeService() : base("https://raw.githubusercontent.com/jeffreylancaster/game-of-thrones/master/data/episodes.json")
            { }

            public async Task<IEnumerable<Episode>> GetAll()
            {
                var result = await Get();

                return result.episodes;
            }
        }
    }
