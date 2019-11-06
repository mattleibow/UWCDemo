using SQLite;

namespace UWCDemo
{
    public class Joke
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string JokeText { get; set; }
    }
}
