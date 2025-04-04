using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter4 : ContentPage
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

    public StudyChapter4()
	{
		InitializeComponent();

        this.Title = StudyTab.Chapter_4_Title;

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

        labelT.Text = StudyTab.Chapter_4_Title;
        labelT.FontSize = FSize + 3.0;

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.T,
            //FontSize = FSize + 6.0,
            //FontAttributes = FontAttributes.Bold
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Ch4S0,
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        labelS0.FormattedText = fStr;

        labelS1.FormattedText?.Spans.Clear();
        fStr = new();
        List<Span> spans = FormatStringWithTitle(StudyTab.Ch4T1, StudyTab.Ch4S1);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1.FormattedText = fStr;

        caption1.Text = StudyTab.Ch4Cap1;
        caption1.FontSize = FSize - 2.0;

        labelS1c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S1c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1c.FormattedText = fStr;

        caption2.Text = StudyTab.Ch4Cap2;
        caption2.FontSize = FSize - 2.0;

        labelS1c1.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S1c1);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1c1.FormattedText = fStr;

        caption3.Text = StudyTab.Ch4Cap3;
        caption3.FontSize = FSize - 2.0;

        labelS1c2.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S1c2);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1c2.FormattedText = fStr;

        labelS2.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T2, StudyTab.Ch4S2);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS2.FormattedText = fStr;

        caption4.Text = StudyTab.Ch4Cap4;
        caption4.FontSize = FSize - 2.0;

        labelS2c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S2c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS2c.FormattedText = fStr;

        labelS3.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T3, StudyTab.Ch4S3);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS3.FormattedText = fStr;

        caption5.Text = StudyTab.Ch4Cap5;
        caption5.FontSize = FSize - 2.0;

        labelS3c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S3c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS3c.FormattedText = fStr;

        labelS4.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T4, StudyTab.Ch4S4);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS4.FormattedText = fStr;

        labelS5.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T5, StudyTab.Ch4S5);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS5.FormattedText = fStr;

        caption6.Text = StudyTab.Ch4Cap6;
        caption6.FontSize = FSize - 2.0;

        labelS5c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S5c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS5c.FormattedText = fStr;

        labelS6.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T6, StudyTab.Ch4S6);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS6.FormattedText = fStr;

        caption7.Text = StudyTab.Ch4Cap7;
        caption7.FontSize = FSize - 2.0;

        labelS6c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S6c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS6c.FormattedText = fStr;

        labelS7.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T7, StudyTab.Ch4S7);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS7.FormattedText = fStr;

        caption8.Text = StudyTab.Ch4Cap8;
        caption8.FontSize = FSize - 2.0;

        labelS7c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch4S7c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS7c.FormattedText = fStr;

        labelS8.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch4T8, StudyTab.Ch4S8);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS8.FormattedText = fStr;


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


        if (Preferences.Get("Ch4Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch4Bookmark", 0.0), false);

        return;
    }

    private List<Span> FormatString(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        List<Span> spans = new List<Span>();
        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansRegular"
        };
        if (str.Contains('{') || str.Contains('[') || str.Contains('<'))
        {
            if (str[0] == '<') span.FontFamily = "OpenSansBold";
            else if (str[0] == '{') span.FontFamily = "OpenSansBoldItalic";
            else if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '<')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                }
                else if (str[i] == '{')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBoldItalic"
                    };
                }
                else if (str[i] == '[')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansItalic"
                    };
                }
                else if (str[i] == ']' || str[i] == '}' || str[i] == '>')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular"
                    };
                }
                else
                {
                    span.Text += str[i];
                }
            }
            spans.Add(span);
        }
        else
        {
            span.Text = str;
            spans.Add(span);
        }

        return spans;
    }

    private List<Span> FormatStringWithTitle(string title, string inputStr)
    {
        string tStr = title;
        string str = inputStr;
        //bool italics = false;
        double fSize = Preferences.Get("FontSize", 16.0);
        //FormattedString fStr = new FormattedString();
        List<Span> spans = new List<Span>();

        Span span = new()
        {
            Text = tStr + Environment.NewLine,
            FontSize = fSize + 2.0,
            FontFamily = "OpenSansBold"
        };
        spans.Add(span);

        span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansRegular"
        };
        //if (str != string.Empty || str != " " || !string.IsNullOrEmpty(str))
        if (str.Contains('{') || str.Contains('[') || str.Contains('<'))
        {
            if (str[0] == '<') span.FontFamily = "OpenSansBold";
            else if (str[0] == '{') span.FontFamily = "OpenSansBoldItalic";
            else if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '<')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                }
                else if (str[i] == '{')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBoldItalic"
                    };
                }
                else if (str[i] == '[')
                {
                    //italics = true;
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansItalic"
                    };
                }
                else if (str[i] == ']' || str[i] == '}' || str[i] == '>')
                {
                    //italics = false;
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular"
                    };
                }
                else
                {
                    span.Text += str[i];
                }
            }
            spans.Add(span);
        }
        else
        {
            span.Text = str;
            spans.Add(span);
        }

        return spans;
    }

    private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("Ch4Bookmark", e.ScrollY);
    }

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}