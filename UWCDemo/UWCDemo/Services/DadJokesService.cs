using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWCDemo
{
    public class DadJokesService
    {
        private const int PageLimit = 20;

        public DadJokesService(IHttpJokes httpJokes, IDbJokes dbJokes)
        {
            HttpJokes = httpJokes ?? throw new ArgumentNullException(nameof(httpJokes));
            DbJokes = dbJokes ?? throw new ArgumentNullException(nameof(dbJokes));
        }

        public IHttpJokes HttpJokes { get; }

        public IDbJokes DbJokes { get; }

        public async Task<IEnumerable<Joke>> GetJokesAsync(int page)
        {
            // try local db
            var localJokes = await DbJokes.GetJokesAsync(page, PageLimit);
            if (localJokes != null)
                return localJokes;

            // try the cloud
            var cloudJokes = await HttpJokes.GetJokesAsync(page, PageLimit);

            // save the cloud jokes to the database
            await DbJokes.SaveJokesAsync(cloudJokes);

            // return the cloud jokes
            return cloudJokes;
        }
    }
}
