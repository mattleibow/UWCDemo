using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWCDemo
{
    public interface IDbJokes
    {
        Task<IEnumerable<Joke>> GetJokesAsync(int page, int limit);

        Task SaveJokesAsync(IEnumerable<Joke> cloudJokes);
    }
}
