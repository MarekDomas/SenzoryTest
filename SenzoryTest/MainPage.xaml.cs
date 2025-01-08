using System.Diagnostics;
using Microsoft.Maui.Controls.Shapes;

namespace SenzoryTest;

public partial class MainPage : ContentPage
{
    private static Random random = new ();
    public MainPage()
    {
        InitializeComponent();
        ToggleShake();
    }

    private void ToggleShake()
    {
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                // Turn on accelerometer
                Accelerometer.Default.ShakeDetected += Accelerometer_ShakeDetected;
                Accelerometer.Default.Start(SensorSpeed.Game);
            }
            else
            {
                // Turn off accelerometer
                Accelerometer.Default.Stop();
                Accelerometer.Default.ShakeDetected -= Accelerometer_ShakeDetected;
            }
        }
    }

    private async void Accelerometer_ShakeDetected(object sender, EventArgs e)
    {
        var response = await DisplayAlert("Upozornění", "Opravdu chcete všechny obrazce smazat?", "Ano", "Ne");
        clearShapes(response);
    }

    private void clearShapes(bool response)
    {
        if (!response) return;

        MainView.Children.Clear();
    }

    private void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        View newShape;
        var tapPosition = e.GetPosition(MainView);
        var cislo = random.Next(0, 3);

        if (cislo == 0)
        {
            newShape = new BoxView
            {
                Color = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)),
                WidthRequest = random.Next(50,100),
                HeightRequest = random.Next(50, 100),
                CornerRadius = random.Next(0, 20)
            };
        }
        else
        {
            newShape = new Ellipse
            {
                Fill = Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255)),
                WidthRequest = random.Next(50, 100),
                HeightRequest = random.Next(50, 100)
            };
        }

        AbsoluteLayout.SetLayoutBounds(newShape, new Rect(tapPosition.Value.X, tapPosition.Value.Y, newShape.WidthRequest, newShape.HeightRequest));

        MainView.Children.Add(newShape);
    }
}