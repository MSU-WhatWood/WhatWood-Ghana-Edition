using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter5 : ContentPage
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

    public StudyChapter5(int IndexReturn = 0)
	{
		InitializeComponent();

        this.Title = StudyTab.Chapter_5_Title;
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

    public StudyChapter5()
    {
        InitializeComponent();

        this.Title = StudyTab.Chapter_5_Title;
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

        labelT.Text = StudyTab.Chapter_5_Title;
        labelT.FontSize = FSize + 3.0;

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.Chapter_5_T,
            //FontSize = FSize + 6.0,
            //FontAttributes = FontAttributes.Bold
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Ch5S0,
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        labelS0.FormattedText = fStr;

        labelS1.FormattedText?.Spans.Clear();
        fStr = new();
        List<Span> spans = FormatStringWithTitle(StudyTab.Ch5T1, StudyTab.Ch5S1);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1.FormattedText = fStr;

        labelS2.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T2, StudyTab.Ch5S2);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS2.FormattedText = fStr;

        caption1.Text = StudyTab.Ch5Cap1;
        caption1.FontSize = FSize - 2.0;

        labelS3.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T3, StudyTab.Ch5S3);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS3.FormattedText = fStr;

        labelS4.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T4, StudyTab.Ch5S4);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS4.FormattedText = fStr;

        caption2.Text = StudyTab.Ch5Cap2;
        caption2.FontSize = FSize - 2.0;

        labelS5.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T5, StudyTab.Ch5S5);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS5.FormattedText = fStr;

        labelS6.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T6, StudyTab.Ch5S6);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS6.FormattedText = fStr;

        labelS7.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T7, StudyTab.Ch5S7);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS7.FormattedText = fStr;

        caption3.Text = StudyTab.Ch5Cap3;
        caption3.FontSize = FSize - 2.0;

        labelS7c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S7c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS7c.FormattedText = fStr;

        labelS8.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T8, StudyTab.Ch5S8);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS8.FormattedText = fStr;

        caption4.Text = StudyTab.Ch5Cap4;
        caption4.FontSize = FSize - 2.0;

        labelS8c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S8c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS8c.FormattedText = fStr;

        caption5.Text = StudyTab.Ch5Cap5;
        caption5.FontSize = FSize - 2.0;

        labelS9.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T9, StudyTab.Ch5S9);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS9.FormattedText = fStr;

        caption6.Text = StudyTab.Ch5Cap6;
        caption6.FontSize = FSize - 2.0;

        labelS9c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S9c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS9c.FormattedText = fStr;

        caption7.Text = StudyTab.Ch5Cap7;
        caption7.FontSize = FSize - 2.0;

        labelS9c1.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S9c1);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS9c1.FormattedText = fStr;

        caption8.Text = StudyTab.Ch5Cap8;
        caption8.FontSize = FSize - 2.0;

        labelS10.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithSmallTitle(StudyTab.Ch5T10, StudyTab.Ch5S10);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS10.FormattedText = fStr;

        caption9.Text = StudyTab.Ch5Cap9;
        caption9.FontSize = FSize - 2.0;

        labelS10c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S10c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS10c.FormattedText = fStr;

        labelS11.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T11, StudyTab.Ch5S11);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS11.FormattedText = fStr;

        caption10.Text = StudyTab.Ch5Cap10;
        caption10.FontSize = FSize - 2.0;

        labelS11c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S11c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS11c.FormattedText = fStr;

        labelS12.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Ch5T12, StudyTab.Ch5S12);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS12.FormattedText = fStr;

        labelS13.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T13, StudyTab.Ch5S13);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS13.FormattedText = fStr;

        caption11.Text = StudyTab.Ch5Cap11;
        caption11.FontSize = FSize - 2.0;

        labelS13c.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatString(StudyTab.Ch5S13c);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS13c.FormattedText = fStr;

        labelS14.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T14, StudyTab.Ch5S14);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS14.FormattedText = fStr;

        labelS15.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T15, StudyTab.Ch5S15);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS15.FormattedText = fStr;

        labelS16.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T16, StudyTab.Ch5S16);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS16.FormattedText = fStr;

        labelS17.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T17, StudyTab.Ch5S17);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS17.FormattedText = fStr;

        labelS18.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T18, StudyTab.Ch5S18);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS18.FormattedText = fStr;

        labelS19.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithItalicTitle(StudyTab.Ch5T19, StudyTab.Ch5S19);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS19.FormattedText = fStr;

        //labelS19c.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatString(StudyTab.Ch5S19c);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS19c.FormattedText = fStr;

        //labelS20.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatStringWithSmallTitle(StudyTab.Ch5T20, StudyTab.Ch5S20);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS20.FormattedText = fStr;

        //labelS20c.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatString(StudyTab.Ch5S20c);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS20c.FormattedText = fStr;

        //labelS20c1.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatString(StudyTab.Ch5S20c1);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS20c1.FormattedText = fStr;

        //labelS21.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatStringWithSmallTitle(StudyTab.Ch5T21, StudyTab.Ch5S21);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS21.FormattedText = fStr;

        //labelS22.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatStringWithSmallTitle(StudyTab.Ch5T22, StudyTab.Ch5S22);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS22.FormattedText = fStr;

        //labelS22c.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatString(StudyTab.Ch5S22c);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS22c.FormattedText = fStr;

        //labelS23.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatStringWithTitle(StudyTab.Ch5T23, StudyTab.Ch5S23);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS23.FormattedText = fStr;

        //caption12.Text = StudyTab.Ch5Cap12;
        //caption12.FontSize = FSize - 2.0;

        //labelS23c.FormattedText?.Spans.Clear();
        //fStr = new();
        //spans = FormatString(StudyTab.Ch5S23c);
        //for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //labelS23c.FormattedText = fStr;


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
            if (Preferences.Get("Ch5Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch5Bookmark", 0.0), false);
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

    private List<Span> FormatStringWithItalicTitle(string title, string inputStr)
    {
        string tStr = title;
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        List<Span> spans = new List<Span>();

        Span span = new()
        {
            Text = tStr + Environment.NewLine,
            FontSize = fSize + 2.0,
            FontFamily = "OpenSansBoldItalic"
        };
        spans.Add(span);

        span = new()
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

    private List<Span> FormatStringWithSmallTitle(string title, string inputStr)
    {
        string tStr = title;
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        List<Span> spans = new List<Span>();

        Span span = new()
        {
            Text = tStr + Environment.NewLine,
            FontSize = fSize + 1.0,
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

    private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("Ch5Bookmark", e.ScrollY);
    }

    //public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));
    public Command BackCommand => new Command(async () => 
    {
        if (iReturn == 1)
        {
            Preferences.Set("Ch5Bookmark", SV.ScrollY);
            await Shell.Current.Navigation.PopAsync(false);
        }
        else await Shell.Current.Navigation.PopToRootAsync(false);
    });

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        //Shell.Current.Navigation.PopToRootAsync(false);
        if (iReturn == 1)
        {
            Preferences.Set("Ch5Bookmark", SV.ScrollY);
            Shell.Current.Navigation.PopAsync(false);
        }
        else Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}