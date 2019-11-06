using MvvmHelpers;
using System.Windows.Input;
using Xamarin.Forms;

namespace UWCDemo
{
    public class MainViewModel : ObservableObject
    {
        private bool isLoadingJokes;
        private int page;

        public MainViewModel()
        {
            LoadMoreItemsCommand = new Command(OnLoadMoreItems);
            ResetCommand = new Command(OnReset);

            Jokes = new ObservableRangeCollection<Joke>();
        }

        public ICommand LoadMoreItemsCommand { get; }

        public ICommand ResetCommand { get; }

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
            var jokes = await App.Service.GetJokesAsync(page++);
            Jokes.AddRange(jokes);

            // all done
            IsLoadingJokes = false;
        }

        private void OnReset()
        {
        }
    }
}
