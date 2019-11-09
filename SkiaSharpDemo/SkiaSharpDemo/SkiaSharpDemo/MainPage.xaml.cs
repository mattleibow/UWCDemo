using SkiaSharp;
using SkiaSharp.Views.Forms;
using Spillman.Xamarin.Forms.ColorPicker;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkiaSharpDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private ColorPickerViewModel colorPicker;
        bool visible;
        SKImage image;

        SKPath currentPath = null;
        List<(SKPath Path, SKColor Color)> paths = new List<(SKPath, SKColor)>();

        public MainPage()
        {
            InitializeComponent();

            colorPicker = new ColorPickerViewModel();
            colorPickerView.ViewModel = colorPicker;
            colorPicker.PropertyChanged += delegate
            {
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            visible = true;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                skiaView.InvalidateSurface();
                return visible;
            });

            using var stream = typeof(App).Assembly.GetManifestResourceStream("SkiaSharpDemo.kitten.jpg");
            image = SKImage.FromEncodedData(stream);
        }

        protected override void OnDisappearing()
        {
            visible = false;

            base.OnDisappearing();
        }

        private void OnPaint(object sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var centre = new SKPoint(e.Info.Width / 2, e.Info.Height / 2);
            var time = DateTime.Now;

            canvas.Clear(SKColors.Azure);

            canvas.Save();
            canvas.Translate(centre);

            var clip = new SKPath();
            clip.AddCircle(0, 0, 500);
            clip.AddCircle(0, 0, 100);
            clip.FillType = SKPathFillType.EvenOdd;

            // background
            canvas.DrawPath(clip, new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Black,
                MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 50)
            });

            // cat
            canvas.Save();
            canvas.ClipPath(clip, antialias: true);
            canvas.DrawImage(image, new SKRect(-500, -500, 500, 500));
            canvas.Restore();

            // move to 12 noon
            canvas.RotateDegrees(-90);

            // hour
            canvas.Save();
            canvas.RotateDegrees((time.Hour / 12f + time.Minute/60f /12f) * 360f);
            canvas.DrawOval(SKRect.Create(0, 0, 250, 30), new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.IndianRed,
                StrokeWidth = 15,
                Style = SKPaintStyle.Stroke
            });
            canvas.Restore();
            // minute
            canvas.Save();
            canvas.RotateDegrees((time.Minute+time.Second/60f) / 60f * 360f);
            canvas.DrawLine(0, 0, 400, 0, new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Orange,
                StrokeWidth = 10,
                Style = SKPaintStyle.Stroke
            });
            canvas.Restore();
            // second
            canvas.Save();
            canvas.RotateDegrees(time.Second / 60f * 360f);
            var hand = SKPath.ParseSvgPathData("m -3.5718767,19.811801 c -5.785067,-1.181735 -9.2918393,-2.940288 -12.6751723,-6.356271 -4.767172,-4.813194 -7.019845,-10.632705 -6.691542,-17.286764 0.16279,-3.29946 0.52992,-4.807902 1.904097,-7.823332 2.390778,-5.246212 5.750762,-8.566124 11.6299973,-11.491251 l 2.393037,-1.190626 0.17417,-1.322916 c 0.09579,-0.727604 0.306054,-12.157605 0.467246,-25.400001 0.161195,-13.242396 0.348805,-25.86302 0.416915,-28.04583 0.06811,-2.18282 0.187362,-9.02891 0.265004,-15.21355 0.07764,-6.18463 0.199408,-13.38791 0.270592,-16.00729 0.07118,-2.61937 0.250647,-12.44203 0.398814,-21.82812 0.327705,-20.75937 0.71129,-42.45588 1.047813,-59.26667 0.235588,-11.76864 0.328409,-19.32841 0.285176,-23.22549 -0.01482,-1.33579 -0.10931,-1.693 -0.481459,-1.82018 -1.853825,-0.63348 -4.606137,-2.21746 -6.1259753,-3.52554 -4.798828,-4.13023 -7.096046,-11.01123 -5.625516,-16.85048 0.820857,-3.2595 2.472164,-7.42435 3.92787,-9.90671 5.0688983,-8.64377 8.4657093,-18.66093 9.3205493,-27.48618 0.148002,-1.52797 0.404301,-15.87501 0.569552,-31.8823 0.605772,-58.67865 0.609748,-58.8853 1.15333003,-59.96109 1.152845,-2.28156 5.05290597,-2.39229 6.35941397,-0.18055 0.462452,0.78287 0.487309,1.6463 0.443489,15.40425 -0.02553,8.01619 -0.09903,16.3013 -0.163319,18.41135 -0.06429,2.11005 -0.188908,15.9213 -0.276921,30.69167 -0.178536,29.96197 -0.198784,29.50051 1.623631,37.00414 1.545384,6.36297 3.0909977,10.24528 7.3290777,18.40936 1.539901,2.96641 3.052022,7.19006 3.634944,10.1531 0.394745,2.00653 0.192807,6.22588 -0.385829,8.0616 -1.484934,4.71096 -5.319871,9.01541 -9.6796097,10.8647 -0.945885,0.40122 -1.898385,0.826 -2.116666,0.94396 -0.351216,0.18979 -0.389946,6.95324 -0.336635,58.79037 0.03313,32.21673 0.120552,62.74308 0.194265,67.83631 0.07371,5.09323 0.145577,21.583389 0.159695,36.644795 0.01413,15.061407 0.08499,27.741563 0.157493,28.178126 0.114615,0.690139 0.414023,0.931915 2.293607,1.852083 1.188979,0.582084 2.4983407,1.326224 2.9096887,1.653646 0.411345,0.327422 0.921509,0.595313 1.133697,0.595313 0.440685,0 5.659022,5.185912 5.659022,5.623877 0,0.158168 0.170614,0.476118 0.37914,0.706517 0.208526,0.230426 0.800807,1.27217 1.316175,2.314972 C 22.983893,-5.448182 22.631158,2.62915 18.758235,9.148246 14.797875,15.814502 7.2175493,20.047968 -0.58415867,19.950575 -1.9363627,19.933642 -3.2808347,19.871253 -3.5718767,19.811801 Z M 3.0010143,4.870251 C 5.5362963,3.90005 5.5326246,3.9092049 5.7285483,-1.944411 l 0.170312,-5.088414 -0.878914,-0.906806 c -1.0717103,-1.1057206 -3.010067,-1.791176 -5.15223897,-1.821921 -1.26588603,-0.01826 -1.90245503,0.136763 -3.14213703,0.764911 -2.310265,1.170569 -2.338443,1.248198 -2.379133,6.555581 -0.01924,2.509626 0.05756,4.860607 0.17067,5.22441 0.699468,2.249778 5.17889003,3.351662 8.483907,2.086901 z");
            canvas.DrawPath(hand, new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.Fuchsia,
                StrokeWidth = 5,
                Style = SKPaintStyle.Fill
            });
            canvas.Restore();
            canvas.DrawCircle(0, 0, 20, new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.LightBlue
            });

            canvas.Restore();

            // scribbles
            foreach (var pair in paths)
            {
                canvas.DrawPath(pair.Path, new SKPaint
                {
                    IsAntialias = true,
                    Color = pair.Color,
                    Style = SKPaintStyle.Stroke,
                    StrokeWidth = 20,
                    StrokeCap = SKStrokeCap.Round,
                    StrokeJoin = SKStrokeJoin.Round
                });
            }
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            if (e.InContact)
            {
                if (currentPath == null)
                {
                    currentPath = new SKPath();
                    currentPath.MoveTo(e.Location);
                    paths.Add((currentPath, colorPicker.SKColor));
                }
                else
                {
                    currentPath.LineTo(e.Location);
                }
            }
            else
            {
                currentPath = null;
            }

            skiaView.InvalidateSurface();

            e.Handled = true;
        }

        private void OnColorPickerClicked(object sender, EventArgs e)
        {
            colorPickerView.IsVisible = !colorPickerView.IsVisible;
        }
    }
}
