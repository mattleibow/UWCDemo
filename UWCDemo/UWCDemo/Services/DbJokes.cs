using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace UWCDemo
{
    public class DbJokes : IDbJokes
    {
        private readonly SQLiteAsyncConnection db;
        private readonly Task init;

        public DbJokes()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "jokes.db");
            db = new SQLiteAsyncConnection(dbPath);

            init = Task.WhenAll(db.CreateTableAsync<Joke>());
        }

        public async Task<IEnumerable<Joke>> GetJokesAsync(int page, int limit)
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

        public async Task SaveJokesAsync(IEnumerable<Joke> jokes)
        {
            await init;

            // save the cloud jokes to the database
            await db.RunInTransactionAsync(conn =>
            {
                foreach (var joke in jokes)
                {
                    conn.InsertOrReplace(joke);
                }
            });
        }
    }
}
