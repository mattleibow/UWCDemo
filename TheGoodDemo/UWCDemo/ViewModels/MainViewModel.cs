using System.Windows.Input;
using MvvmHelpers;
using Xamarin.Forms;

namespace UWCDemo
{
    public class MainViewModel : ObservableObject
    {
        private readonly DadJokesService service;

        private bool isLoadingJokes;
        private int page;

        public MainViewModel()
            : this(DependencyService.Get<IHttpJokes>(), DependencyService.Get<IDbJokes>())
        {
        }

        public MainViewModel(IHttpJokes httpJokes, IDbJokes dbJokes)
        {
            service = new DadJokesService(httpJokes, dbJokes);

            LoadMoreItemsCommand = new Command(OnLoadMoreItems);

            Jokes = new ObservableRangeCollection<Joke>();
        }

        public ICommand LoadMoreItemsCommand { get; }

        public ObservableRangeCollection<Joke> Jokes { get; }

        public bool IsLoadingJokes
        {
            get => isLoadingJokes;
            set => SetProperty(ref isLoadingJokes, value);
        }

        private async void OnLoadMoreItems()
        {
            // ony load one at a time
            if (IsLoadingJokes)
                return;

            // start the load
            IsLoadingJokes = true;

            // load the data
            var jokes = await service.GetJokesAsync(page++);
            Jokes.AddRange(jokes);

            // all done
            IsLoadingJokes = false;
        }
    }
}
