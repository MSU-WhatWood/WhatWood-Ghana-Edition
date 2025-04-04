using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter2 : ContentPage
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
    public int iReturn = 0;

    public StudyChapter2(int IndexReturn = 0)
	{
		InitializeComponent();

        this.Title = StudyTab.Chapter_2_Title_short;
        iReturn = IndexReturn;

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

    public StudyChapter2()
    {
        InitializeComponent();

        this.Title = StudyTab.Chapter_2_Title_short;
        iReturn = 0;

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

        labelT.Text = StudyTab.Chapter_2_Title;
        labelT.FontSize = FSize + 3.0;

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.B,
            //FontSize = FSize + 6.0,
            //FontAttributes = FontAttributes.Bold
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Ch2S0,
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        labelS0.FormattedText = fStr;

        labelS1.FormattedText?.Spans.Clear();
        fStr = new();
        List<Span> spans = FormatStringWithTitle(StudyTab.Ch2T1, StudyTab.Ch2S1);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1.FormattedText = fStr;

        labelS2.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T2, StudyTab.Ch2S2);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS2.FormattedText = fStr;

        caption1.Text = StudyTab.Ch2Cap1;
        caption1.FontSize = FSize - 2.0;

        labelS2c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch2S2c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS2c.FormattedText = fStr;

        labelS3.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T3, StudyTab.Ch2S3);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS3.FormattedText = fStr;

        labelS4.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T4, StudyTab.Ch2S4);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS4.FormattedText = fStr;

        caption2.Text = StudyTab.Ch2Cap2;
        caption2.FontSize = FSize - 2.0;

        labelS4c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch2S4c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS4c.FormattedText = fStr;

        labelS5.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T5, StudyTab.Ch2S5);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS5.FormattedText = fStr;

        caption3.Text = StudyTab.Ch2Cap3;
        caption3.FontSize = FSize - 2.0;

        labelS5c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch2S5c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS5c.FormattedText = fStr;

        labelS6.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T6, StudyTab.Ch2S6);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS6.FormattedText = fStr;

        caption4.Text = StudyTab.Ch2Cap4;
        caption4.FontSize = FSize - 2.0;

        labelS6c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch2S6c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS6c.FormattedText = fStr;

        labelS7.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T7, StudyTab.Ch2S7);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS7.FormattedText = fStr;

        caption5.Text = StudyTab.Ch2Cap5;
        caption5.FontSize = FSize - 2.0;

        labelS7c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch2S7c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS7c.FormattedText = fStr;

        labelS8.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T8, StudyTab.Ch2S8);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS8.FormattedText = fStr;

        caption6.Text = StudyTab.Ch2Cap6;
        caption6.FontSize = FSize - 2.0;

        labelS8c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch2S8c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS8c.FormattedText = fStr;

        labelS9.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T9, StudyTab.Ch2S9);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS9.FormattedText = fStr;

        labelS10.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch2T10, StudyTab.Ch2S10);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS10.FormattedText = fStr;


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

        if (iReturn == 1) await SV.ScrollToAsync(0.0, 0.0, false);
        else
        {
            if (Preferences.Get("Ch2Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch2Bookmark", 0.0), false);
        }

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
            if (str[0] == '{') span.FontFamily = "OpenSansBoldItalic";
            if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            if (str[0] == '<') span.FontFamily = "OpenSansBold";
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '{')
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
                else if (str[i] == '<')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
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
        double fSize = Preferences.Get("FontSize", 16.0);
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
        if (str.Contains('{') || str.Contains('[') || str.Contains('<'))
        {
            if (str[0] == '{') span.FontFamily = "OpenSansBoldItalic";
            if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            if (str[0] == '<') span.FontFamily = "OpenSansBold";
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '{')
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
                else if (str[i] == '<')
                {
                    spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
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

    private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("Ch2Bookmark", e.ScrollY);
    }

    public Command BackCommand => new Command(async () => 
    {
        if (iReturn == 1)
        {
            Preferences.Set("Ch2Bookmark", SV.ScrollY);
            await Shell.Current.Navigation.PopAsync(false);
        }
        else await Shell.Current.Navigation.PopToRootAsync(false);
    });

    public Command BackCommand1 => new Command(async () =>
    {
        await Shell.Current.Navigation.PopAsync(false);
    });

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        //Shell.Current.Navigation.PopToRootAsync(false);
        if (iReturn == 1)
        {
            Preferences.Set("Ch2Bookmark", SV.ScrollY);
            Shell.Current.Navigation.PopAsync(false);
        }
        else Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}