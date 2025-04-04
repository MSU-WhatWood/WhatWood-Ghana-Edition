using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using System.Globalization;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter6 : ContentPage
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
    DevicePlatform dPlat = new DevicePlatform();

    public StudyChapter6(int IndexReturn = 0)
    {
        InitializeComponent();

        this.Title = StudyTab.Chapter_6_Title;
        iReturn = IndexReturn;

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
        image0.GestureRecognizers.Add(zTGR);
    }

    public StudyChapter6()
    {
        InitializeComponent();

        this.Title = StudyTab.Chapter_6_Title;
        iReturn = 0;

        dPlat = new DevicePlatform();
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
        image0.GestureRecognizers.Add(zTGR);
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

        labelT.FormattedText?.Spans.Clear();
        if (dPlat == DevicePlatform.iOS)
        {
            labelT.FormattedText = FormatReferenceTitle(StudyTab.Chapter_6_Title_iOS);

            TapGestureRecognizer tGR = new();
            tGR.Tapped += async (s, e) =>
            {
                await Shell.Current.GoToAsync("//ID", false);
            };
            labelTc.FormattedText?.Spans.Clear();
            labelTc.FormattedText = FormatReferenceTitle(StudyTab.Chapter_6_Title_iOS1);
            labelTc.GestureRecognizers.Clear();
            labelTc.GestureRecognizers.Add(tGR);
        }
        else
        {
            labelT.FormattedText = FormatReferenceTitle(StudyTab.Chapter_6_Title_Hyperlink);
            labelTc.IsVisible = false;
            iosGridT.RowDefinitions[1].Height = 0;
        }

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.Ch6T0 + Environment.NewLine,
            FontSize = FSize + 2.0,
            FontFamily = "OpenSansBold"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_6_T,
            //FontSize = FSize + 6.0,
            //FontFamily = "OpenSansBold"
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);

        if (dPlat == DevicePlatform.iOS)
        {
            TapGestureRecognizer tGR1 = new();
            FormattedString fStr1 = FormatReferenceString("T" + StudyTab.Ch6S0iOS);
            labelS0.FormattedText = fStr1;
            labelS0c.FormattedText?.Spans.Clear();
            fStr1 = FormatReferenceString(StudyTab.Ch6S0iOS1);
            labelS0c.FormattedText = fStr1;
            tGR1.Tapped += async (s, e) =>
            {
                await Shell.Current.GoToAsync("//Reference", false);
            };
            labelS0c.GestureRecognizers.Clear();
            labelS0c.GestureRecognizers.Add(tGR1);
            labelS0c1.FormattedText?.Spans.Clear();
            fStr1 = FormatReferenceString(StudyTab.Ch6S0iOS2);
            labelS0c1.FormattedText = fStr1;
            labelS0c2.FormattedText?.Spans.Clear();
            fStr1 = FormatReferenceString(StudyTab.Ch6S0iOS3);
            labelS0c2.FormattedText = fStr1;
            labelS0c2.GestureRecognizers.Clear();
            labelS0c2.GestureRecognizers.Add(tGR1);
            labelS0c3.FormattedText?.Spans.Clear();
            fStr1 = FormatReferenceString(StudyTab.Ch6S0iOS4);
            labelS0c3.FormattedText = fStr1;
            labelS0c4.FormattedText?.Spans.Clear();
            fStr1 = FormatReferenceString(StudyTab.Ch6S0iOS5);
            labelS0c4.FormattedText = fStr1;
            labelS0c4.GestureRecognizers.Clear();
            labelS0c4.GestureRecognizers.Add(tGR1);
            labelS0c5.FormattedText?.Spans.Clear();
            fStr1 = FormatReferenceString(StudyTab.Ch6S0iOS6);
            labelS0c5.FormattedText = fStr1;
        }
        else
        {
            FormattedString fStr1 = FormatReferenceString("T" + StudyTab.Ch6S0);
            for (int i = 0; i < fStr1.Spans.Count; i++) fStr.Spans.Add(fStr1.Spans[i]);
            labelS0.FormattedText = fStr;
            labelS0c.IsVisible = false;
            labelS0c1.IsVisible = false;
            labelS0c2.IsVisible = false;
            labelS0c3.IsVisible = false;
            labelS0c4.IsVisible = false;
            labelS0c5.IsVisible = false;
            iosGrid.RowDefinitions[1].Height = 0;
            iosGrid.RowDefinitions[2].Height = 0;
            iosGrid.RowDefinitions[3].Height = 0;
            iosGrid.RowDefinitions[4].Height = 0;
            iosGrid.RowDefinitions[5].Height = 0;
            iosGrid.RowDefinitions[6].Height = 0;
        }


        labelS1.FormattedText?.Spans.Clear();
        fStr = new();
        fStr = FormatSmallReferenceTitle(StudyTab.Ch6T1);
        //if (dPlat == DevicePlatform.iOS)
        //{
        //    TapGestureRecognizer tGR2 = new();
        //    tGR2.Tapped += async (s, e) =>
        //    {
        //        await Shell.Current.GoToAsync("//ID", false);
        //    };
        //    labelS1.FormattedText = fStr;
        //    labelS1.GestureRecognizers.Clear();
        //    labelS1.GestureRecognizers.Add(tGR2);

        //    labelS1c.FormattedText?.Spans.Clear();
        //    fStr = new();
        //    span = new()
        //    {
        //        Text = Environment.NewLine,
        //        FontSize = FSize + 2.0,
        //        FontFamily = "OpenSansBold"
        //    };
        //    fStr.Spans.Add(span);
        //    List<Span> spans = FormatStringWithSmallTitle(StudyTab.Ch6T1c, StudyTab.Ch6S1);
        //    for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        //    labelS1c.FormattedText = fStr;
        //}
        //else
        //{
        span = new()
        {
            //Text = Environment.NewLine + Environment.NewLine,
            Text = Environment.NewLine,
            FontSize = FSize + 2.0,
            FontFamily = "OpenSansBold"
        };
        fStr.Spans.Add(span);

        //List<Span> spans = FormatStringWithSmallTitle(StudyTab.Ch6T1c, StudyTab.Ch6S1);
        FormattedString fStr2 = FormatReferenceString(StudyTab.Ch6S1);
            for (int i = 0; i < fStr2.Spans.Count; i++) fStr.Spans.Add(fStr2.Spans[i]);
            labelS1.FormattedText = fStr;
            labelS1c.IsVisible = false;
            iosGrid1.RowDefinitions[1].Height = 0;
        //}

        //if (CultureInfo.CurrentCulture.Name.Contains("en")) image0.Source = ImageSource.FromFile("ch6image1i.jpg");
        //else if (CultureInfo.CurrentCulture.Name.Contains("es")) image0.Source = ImageSource.FromFile("ch6image2i.jpg");
        image0.Source = ImageSource.FromFile("ch6image1i.jpg");

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
            if (Preferences.Get("Ch6Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch6Bookmark", 0.0), false);
        }

        return;
    }

    private FormattedString FormatReferenceTitle(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0) + 3.0;
        FormattedString fStr = new FormattedString();

        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansSemibold"
        };
        if (Application.Current != null)
        {
            if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.White;
            else span.TextColor = Colors.Black;
        }
        if (str.Contains('{') || str.Contains('[') || str.Contains('<') || str.Contains('^'))
        {
            if (str[0] == '<') span.FontFamily = "OpenSansBold";
            else if (str[0] == '{') span.FontFamily = "OpenSansBoldItalic";
            else if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            else if (str[0] == '^')
            {
                span.FontFamily = "OpenSansSemibold";
                span.TextDecorations = TextDecorations.Underline;
                if (Application.Current != null)
                {
                    if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.LightBlue;
                    else span.TextColor = Colors.Blue;
                }
            }
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '<')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                }
                else if (str[i] == '{')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBoldItalic"
                    };
                }
                else if (str[i] == '[')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansItalic"
                    };
                }
                else if (str[i] == '^')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansSemibold",
                        TextDecorations = TextDecorations.Underline
                    };
                    if (Application.Current != null)
                    {
                        if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.LightBlue;
                        else span.TextColor = Colors.Blue;
                    }
                }
                else if (str[i] == '|')
                {
                    TapGestureRecognizer tGR = new();
                    if (span.Text.Contains("Key") || span.Text.Contains("Clave")) tGR.CommandParameter = "//ID";
                    else tGR.CommandParameter = "//Reference";
                    tGR.Command = TapCommand2;
                    if (dPlat != DevicePlatform.iOS) span.GestureRecognizers.Add(tGR);
                    fStr.Spans.Add(span);

                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansSemibold"
                    };
                    if (Application.Current != null)
                    {
                        if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.White;
                        else span.TextColor = Colors.Black;
                    }
                }
                else if (str[i] == ']' || str[i] == '}' || str[i] == '>')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansSemibold"
                    };
                    if (Application.Current != null)
                    {
                        if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.White;
                        else span.TextColor = Colors.Black;
                    }
                }
                else
                {
                    span.Text += str[i];
                }
            }
            fStr.Spans.Add(span);
        }
        else
        {
            span.Text = str;
            fStr.Spans.Add(span);
        }

        return fStr;
    }

    private FormattedString FormatSmallReferenceTitle(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0) + 2.0;
        FormattedString fStr = new FormattedString();

        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansBold"
        };
        if (str.Contains('{') || str.Contains('[') || str.Contains('<') || str.Contains('^'))
        {
            if (str[0] == '^')
            {
                span.FontFamily = "OpenSansBold";
                span.TextDecorations = TextDecorations.Underline;
                span.TextColor = Colors.Blue;
            }
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '^')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold",
                        TextDecorations = TextDecorations.Underline
                    };
                    span.TextColor = Colors.Blue;
                }
                else if (str[i] == '|')
                {
                    TapGestureRecognizer tGR = new();
                    if (span.Text.Contains("Key") || span.Text.Contains("Clave")) tGR.CommandParameter = "//ID";
                    else tGR.CommandParameter = "//Reference";
                    tGR.Command = TapCommand2;
                    if (dPlat != DevicePlatform.iOS) span.GestureRecognizers.Add(tGR);
                    fStr.Spans.Add(span);

                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                }
                else if (str[i] == ']' || str[i] == '}' || str[i] == '>')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                }
                else
                {
                    span.Text += str[i];
                }
            }
            fStr.Spans.Add(span);
        }
        else
        {
            span.Text = str;
            fStr.Spans.Add(span);
        }

        return fStr;
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
            FontFamily = "OpenSansSemiboldItalic"
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

    private FormattedString FormatReferenceString(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();

        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansRegular"
        };
        if (str.Contains('{') || str.Contains('[') || str.Contains('<') || str.Contains('^'))
        {
            if (str[0] == '<') span.FontFamily = "OpenSansBold";
            else if (str[0] == '{') span.FontFamily = "OpenSansBoldItalic";
            else if (str[0] == '[') span.FontFamily = "OpenSansItalic";
            else if (str[0] == '^')
            {
                span.FontFamily = "OpenSansRegular";
                span.TextDecorations = TextDecorations.Underline;
                span.TextColor = Colors.Blue;
            }
            else span.Text += str[0];

            for (int i = 1; i < str.Length; i++)
            {
                if (str[i] == '<')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBold"
                    };
                }
                else if (str[i] == '{')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBoldItalic"
                    };
                }
                else if (str[i] == '[')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansItalic"
                    };
                }
                else if (str[i] == '^')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular",
                        TextDecorations = TextDecorations.Underline
                    };
                    span.TextColor = Colors.Blue;
                }
                else if (str[i] == '|')
                {
                    TapGestureRecognizer tGR = new();
                    if (span.Text.Contains("Key") || span.Text.Contains("Clave")) tGR.CommandParameter = "//ID";
                    else tGR.CommandParameter = "//Reference";
                    tGR.Command = TapCommand2;
                    if (dPlat != DevicePlatform.iOS) span.GestureRecognizers.Add(tGR);
                    fStr.Spans.Add(span);

                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular"
                    };
                }
                else if (str[i] == ']' || str[i] == '}' || str[i] == '>')
                {
                    fStr.Spans.Add(span);
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
            fStr.Spans.Add(span);
        }
        else
        {
            span.Text = str;
            fStr.Spans.Add(span);
        }

        return fStr;
    }

    public ICommand TapCommand2 => new Command<string>(async (route) => await Shell.Current.GoToAsync(route, false));

    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("Ch6Bookmark", e.ScrollY);
    }

    //public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));
    public Command BackCommand => new Command(async () => 
    {
        if (iReturn == 1)
        {
            Preferences.Set("Ch6Bookmark", SV.ScrollY);
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
            Preferences.Set("Ch6Bookmark", SV.ScrollY);
            Shell.Current.Navigation.PopAsync(false);
        }
        else Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}