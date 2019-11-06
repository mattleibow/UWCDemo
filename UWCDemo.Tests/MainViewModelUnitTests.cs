using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UWCDemo.Tests
{
    public class MainViewModelUnitTests
    {
        [Fact]
        public void InitialStateIsCorrect()
        {
            // create useless things
            var httpJokes = Substitute.For<IHttpJokes>();
            var dbJokes = Substitute.For<IDbJokes>();

            // pretend we are a page
            var vm = new MainViewModel(httpJokes, dbJokes);

            // make sure we are all clean
            Assert.Empty(vm.Jokes);
            Assert.False(vm.IsLoadingJokes);
        }

        public class LoadMoreItemsCommand
        {
            [Fact]
            public void AlwaysReadsFromTheDatabase()
            {
                // create useless things
                var httpJokes = Substitute.For<IHttpJokes>();
                var dbJokes = Substitute.For<IDbJokes>();

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // get some jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got that 1 joke we expected
                dbJokes.ReceivedWithAnyArgs().GetJokesAsync(default, default);
            }

            [Fact]
            public void UsesCloudWhenThereIsNothingInTheDatabase()
            {
                // create a useless cloud
                var httpJokes = Substitute.For<IHttpJokes>();

                // create an empty database
                var dbJokes = Substitute.For<IDbJokes>();
                dbJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)null));

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // get some jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got that 1 joke we expected
                httpJokes.ReceivedWithAnyArgs().GetJokesAsync(default, default);
            }

            [Fact]
            public void DoesNotUseCloudWhenThereAreItemsInDatabase()
            {
                // create a useless cloud
                var httpJokes = Substitute.For<IHttpJokes>();

                // this joke is going to be "saved" in the database
                var localJoke = new Joke { Id = "1", JokeText = "database joke text" };

                // create a magical database
                var dbJokes = Substitute.For<IDbJokes>();
                dbJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)new[] { localJoke }));

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // get some jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we didn't try search it internet
                httpJokes.DidNotReceiveWithAnyArgs().GetJokesAsync(default, default);
            }

            [Fact]
            public void GetsJokeFromDatabaseWhenThereAreItemsInDatabase()
            {
                // create a useless cloud
                var httpJokes = Substitute.For<IHttpJokes>();

                // this joke is going to be "saved" in the database
                var localJoke = new Joke { Id = "1", JokeText = "database joke text" };

                // create a magical database
                var dbJokes = Substitute.For<IDbJokes>();
                dbJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)new[] { localJoke }));

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // get some jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got that 1 joke we expected
                Assert.Single(vm.Jokes);
                Assert.Equal(localJoke, vm.Jokes[0]);
            }

            [Fact]
            public void GetsJokeFromCloudWhenThereIsNothingInDatabase()
            {
                // this joke is on the "cloud"
                var cloudJoke = new Joke { Id = "1", JokeText = "internet joke text" };

                // create an empty database
                var dbJokes = Substitute.For<IDbJokes>();
                dbJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)null));

                // create a magical cloud
                var httpJokes = Substitute.For<IHttpJokes>();
                httpJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)new[] { cloudJoke }));

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // get some jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got that 1 joke we expected
                Assert.Single(vm.Jokes);
                Assert.Equal(cloudJoke, vm.Jokes[0]);
            }

            [Fact]
            public void MultipleDatabaseCallsGetMoreItems()
            {
                // create a useless cloud
                var httpJokes = Substitute.For<IHttpJokes>();

                // make some jokes
                var joke1 = new Joke { Id = "1", JokeText = "first joke" };
                var joke2 = new Joke { Id = "2", JokeText = "second joke" };

                // create a magical database
                var dbJokes = Substitute.For<IDbJokes>();
                dbJokes.GetJokesAsync(Arg.Is(0), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)new[] { joke1 }));
                dbJokes.GetJokesAsync(Arg.Is(1), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)new[] { joke2 }));

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // ask for first set of jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got the first joke
                Assert.Single(vm.Jokes);
                Assert.Equal(joke1, vm.Jokes[0]);

                // ask for more jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got the second joke
                Assert.Equal(2, vm.Jokes.Count);
                Assert.Equal(joke1, vm.Jokes[0]);
                Assert.Equal(joke2, vm.Jokes[1]);
            }

            [Fact]
            public void LoadFromCloudSavesToDatabase()
            {
                // this joke is on the "cloud"
                var cloudJoke = new Joke { Id = "1", JokeText = "internet joke text" };

                // create an empty database
                var dbJokes = Substitute.For<IDbJokes>();
                dbJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)null));

                // create a magical cloud
                var httpJokes = Substitute.For<IHttpJokes>();
                httpJokes.GetJokesAsync(Arg.Any<int>(), Arg.Any<int>())
                    .Returns(Task.FromResult((IEnumerable<Joke>)new[] { cloudJoke }));

                // pretend we are a page
                var vm = new MainViewModel(httpJokes, dbJokes);

                // get some jokes
                vm.LoadMoreItemsCommand.Execute(null);

                // make sure we got that 1 joke we expected
                dbJokes.Received().SaveJokesAsync(
                    Arg.Is<IEnumerable<Joke>>(jokes => jokes.Single() == cloudJoke));
            }
        }
    }
}
