using System.Linq;
using Xamarin.Forms;

namespace UWCDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((MainViewModel)BindingContext).LoadMoreItemsCommand.Execute(null);
        }

        private async void OnJokeSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Joke joke)
            {
                await Navigation.PushAsync(new JokePage(joke));

                ((CollectionView)sender).SelectedItem = null;
            }
        }
    }
}
