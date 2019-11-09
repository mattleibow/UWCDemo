using SkiaSharp;
using SkiaSharp.Views.Forms;
//using Spillman.Xamarin.Forms.ColorPicker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace SkiaSharpDemo
{
    [DesignTimeVisible(true)]
    public partial class OldMainPage : ContentPage
    {
        //SKPath currentPath = new SKPath();
        //List<(SKColor Color, SKPath Path)> paths = new List<(SKColor, SKPath)>();

        //ColorPickerViewModel colorPicker;

        public OldMainPage()
        {
        //    InitializeComponent();

        //    colorPicker = new ColorPickerViewModel();
        //    colorPicker.PropertyChanged += (sender, e) => colorPickerButton.BackgroundColor = colorPicker.Color;
        //    colorPickerView.ViewModel = colorPicker;
        //}

        //private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        //{
        //    var canvas = e.Surface.Canvas;

        //    canvas.Clear(SKColors.Azure);

        //    canvas.DrawCircle(new SKPoint(100, 100), 100, new SKPaint
        //    {
        //        Style = SKPaintStyle.Fill
        //    });


        //    canvas.Save();
        //    canvas.Translate(200, 0);
        //    canvas.Translate(100, 100);
        //    canvas.RotateDegrees(45);
        //    canvas.Translate(-100, -100);
        //    canvas.DrawRect(SKRect.Create(0, 0, 200, 200), new SKPaint
        //    {
        //        Style = SKPaintStyle.Fill,
        //        Shader = SKShader.CreateLinearGradient(
        //            start: new SKPoint(0, 0),
        //            end: new SKPoint(200, 0),
        //            colors: new[] { SKColors.Red, SKColors.Green },
        //            colorPos: null,
        //            mode: SKShaderTileMode.Clamp),
        //    });
        //    canvas.Restore();


        //    canvas.Save();
        //    canvas.Translate(500, 100);

        //    canvas.Scale(4, 4, -100, -100);

        //    canvas.DrawCircle(new SKPoint(0, 0), 100, new SKPaint
        //    {
        //        Style = SKPaintStyle.Fill,
        //        Color = SKColors.Purple
        //    });

        //    var dt = DateTime.Now;

        //    var x = 50f * Math.Cos(30);
        //    var y = 50f * Math.Sin(30);

        //    canvas.RotateDegrees(-90);

        //    canvas.Save();
        //    canvas.RotateDegrees(dt.Hour / 12f * 360f);
        //    canvas.DrawLine(0, 0, 50, 0, new SKPaint { Color = SKColors.GreenYellow, StrokeWidth = 5 });
        //    canvas.Restore();

        //    canvas.Save();
        //    canvas.RotateDegrees(dt.Minute / 60f * 360f);
        //    canvas.DrawLine(0, 0, 80, 0, new SKPaint { Color = SKColors.YellowGreen, StrokeWidth = 3 });
        //    canvas.Restore();

        //    canvas.Save();
        //    canvas.RotateDegrees(dt.Second / 60f * 360f);
        //    canvas.DrawLine(0, 0, 95, 0, new SKPaint { Color = SKColors.LightYellow, StrokeWidth = 1 });
        //    canvas.Restore();

        //    canvas.DrawCircle(new SKPoint(0, 0), 10, new SKPaint
        //    {
        //        Style = SKPaintStyle.Fill,
        //        Color = SKColors.BlanchedAlmond
        //    });

        //    canvas.Restore();


        //    using var paint = new SKPaint();
        //    paint.Style = SKPaintStyle.Stroke;
        //    paint.StrokeWidth = 5;

        //    foreach (var pair in paths)
        //    {
        //        paint.Color = pair.Color;
        //        canvas.DrawPath(pair.Path, paint);
        //    }
        //}

        //private void OnTouchSurface(object sender, SKTouchEventArgs e)
        //{
        //    if (e.InContact)
        //    {
        //        if (currentPath == null)
        //        {
        //            currentPath = new SKPath();
        //            currentPath.MoveTo(e.Location);

        //            paths.Add((colorPicker.SKColor, currentPath));
        //        }
        //        else
        //        {
        //            currentPath.LineTo(e.Location);
        //        }
        //    }
        //    else
        //    {
        //        currentPath = null;
        //    }

        //    skiaView.InvalidateSurface();

        //    e.Handled = true;
        //}

        //private void OnPickColor(object sender, System.EventArgs e)
        //{
        //    colorPickerView.IsVisible = !colorPickerView.IsVisible;
        }
    }
}
