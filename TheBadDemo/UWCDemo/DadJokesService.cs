using ICanHazDadJoke.NET;
using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace UWCDemo
{
    public class DadJokesService
    {
        private const int PageLimit = 20;

        private readonly SQLiteAsyncConnection db;
    	private readonly Task init;
        private readonly DadJokeClient client;

        public DadJokesService()
        {
			var dbPath = Path.Combine(FileSystem.AppDataDirectory, "jokes.db");
			db = new SQLiteAsyncConnection(dbPath);

            init = Task.WhenAll(db.CreateTableAsync<Joke>());

            client = new DadJokeClient("UWC Demo");
        }

        public async Task<IEnumerable<Joke>> GetJokesAsync(int page)
        {
            // make sure the database is ready
            await init;

            // try local db
            var localJokes = await GetLocalJokesAsync(page, PageLimit);
            if (localJokes != null)
                return localJokes;

            // try the cloud
            var cloudJokes = await GetCloudJokesAsync(page, PageLimit);

            // save the cloud jokes to the database
            await db.RunInTransactionAsync(conn =>
            {
                foreach (var joke in cloudJokes)
                {
                    conn.InsertOrReplace(joke);
                }
            });

            // return the cloud jokes
            return cloudJokes;
        }

		public async Task<IEnumerable<Joke>> GetCloudJokesAsync(int page, int limit)
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

        public async Task<IEnumerable<Joke>> GetLocalJokesAsync(int page, int limit)
        {
            await init;

			// try the database
			var jokes = await db.Table<Joke>()
				.Skip(page * limit)
				.Take(limit)
			    .ToArrayAsync();

            // return from the database
            if (jokes?.Length > 0)
                return jokes;

            // we found no jokes, so let everyonw know
            return null;
        }
    }
}
