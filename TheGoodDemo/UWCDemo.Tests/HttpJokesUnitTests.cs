using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UWCDemo.Tests
{
    public class HttpJokesUnitTests
    {
        public class GetJokesAsync
        {
            [Fact]
            public async Task CanGetASingleJoke()
            {
                // create useless things
                var httpJokes = new HttpJokes();

                // get a joke
                var jokes = await httpJokes.GetJokesAsync(0, 1);

                // make sure we got that 1 joke we expected
                Assert.Single(jokes);
                Assert.NotEmpty(jokes.Single().JokeText);
            }

            [Fact]
            public async Task CanGetMultipleJokes()
            {
                // create useless things
                var httpJokes = new HttpJokes();

                // get a joke
                var jokes = await httpJokes.GetJokesAsync(0, 10);

                // make sure we got those jokes
                Assert.Equal(10, jokes.Count());
                Assert.All(jokes, j => Assert.NotEmpty(j.JokeText));
            }
        }
    }
}
