using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter3 : ContentPage
{
    private double fSize = Preferences.Get("FontSize", 16.0);
    public double FSize
    {
        get { return fSize; }
        set
        {
            if (value == fSize) return;

            fSize = value;
            OnPropertyChanged();
        }
    }
    TapGestureRecognizer zTGR = new();

    public StudyChapter3()
	{
		InitializeComponent();

        this.Title = StudyTab.Chapter_3_Title;

        DevicePlatform dPlat = new DevicePlatform();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        if (dPlat == DevicePlatform.Android) this.SizeChanged += OnSizeChanged;

        zTGR = new TapGestureRecognizer();
        zTGR.Tapped += async (s, e) =>
        {
            Image? s1 = s as Image;
            if (s1 != null) await AppShell.Current.Navigation.PushAsync(new ImageZoomPage(s1.Source), false);
        };
        for (int i = 0; i < aGrid.Children.Count; i++)
        {
            if (aGrid.Children[i].GetType() == typeof(Image)) ((Image)aGrid.Children[i]).GestureRecognizers.Add(zTGR);
        }
    }

    private void OnSizeChanged(object? sender, EventArgs e)
    {
        DisplayOrientation dOri = new DisplayOrientation();
        double sWid = 0;
        double dWid = 0;
        double dHei = 0;
        double dDen = 0;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            dOri = DeviceDisplay.Current.MainDisplayInfo.Orientation;
            dWid = DeviceDisplay.Current.MainDisplayInfo.Width;
            dHei = DeviceDisplay.Current.MainDisplayInfo.Height;
            dDen = DeviceDisplay.Current.MainDisplayInfo.Density;
        });

        if (dHei < dWid) sWid = (dHei / dDen);
        else sWid = (dWid / dDen);

        if (dOri == DisplayOrientation.Landscape)
        {
            if (sWid < 700)
            {
                Shell.SetNavBarIsVisible(this, false);
                Shell.SetTabBarIsVisible(this, false);
            }
        }
        else
        {
            Shell.SetNavBarIsVisible(this, true);
            Shell.SetTabBarIsVisible(this, true);
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        FSize = Preferences.Get("FontSize", 16.0);

        labelT.Text = StudyTab.Chapter_3_Title;
        labelT.FontSize = FSize + 3.0;

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.A,
            //FontSize = FSize + 6.0,
            //FontAttributes = FontAttributes.Bold
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Ch3S0,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        labelS0.FormattedText = fStr;

        caption1.Text = StudyTab.Ch3Cap1;
        caption1.FontSize = FSize - 2.0;

        labelS1.Text = StudyTab.Ch3S0c;
        labelS1.FontSize = FSize;

        caption2.Text = StudyTab.Ch3Cap2;
        caption2.FontSize = FSize - 2.0;

        labelS2.Text = StudyTab.Ch3S0c1;
        labelS2.FontSize = FSize;

        caption3.Text = StudyTab.Ch3Cap3;
        caption3.FontSize = FSize - 2.0;

        labelS3.Text = StudyTab.Ch3S0c2;
        labelS3.FontSize = FSize;


        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
        {
            //frameT.CornerRadius = 26;
            RoundRectangle rRec = new RoundRectangle
            {
                CornerRadius = new CornerRadius(26)
            };
            frameT.StrokeShape = rRec;
            frameT.HeightRequest = 52;
            frameT.WidthRequest = 52;
        }


        if (Preferences.Get("Ch3Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch3Bookmark", 0.0), false);

        return;
    }

    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("Ch3Bookmark", e.ScrollY);
    }

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}