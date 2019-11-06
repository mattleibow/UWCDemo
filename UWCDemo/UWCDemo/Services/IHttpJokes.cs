using System.Collections.Generic;
using System.Threading.Tasks;

namespace UWCDemo
{
    public interface IHttpJokes
    {
        Task<IEnumerable<Joke>> GetJokesAsync(int page, int limit);
    }
}
