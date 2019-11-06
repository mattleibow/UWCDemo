using ICanHazDadJoke.NET;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UWCDemo
{
    public class HttpJokes : IHttpJokes
    {
        private readonly DadJokeClient client;

        public HttpJokes()
        {
            client = new DadJokeClient("UWC Demo");
        }

        public async Task<IEnumerable<Joke>> GetJokesAsync(int page, int limit)
        {
            // try the cloud
            var cloudJokes = await client.SearchJokesAsync(page: page + 1, limit: limit);

            // convert the cloud jokes into our jokes
            var jokes = cloudJokes.Results.Select(r => new Joke
            {
                Id = r.Id,
                JokeText = r.Joke
            }).ToArray();

            return jokes;
        }
    }
}
