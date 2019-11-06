using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace UWCDemo
{
    public partial class JokePage : ContentPage
    {
        public JokePage(Joke joke)
        {
            InitializeComponent();

            BindingContext = joke;
        }

        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;

            canvas.Clear(SKColors.Azure);
        }
    }
}
