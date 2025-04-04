using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using System.Windows.Input;
using System.Globalization;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter8 : ContentPage
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
    DevicePlatform dPlat = new();

    public StudyChapter8()
	{
		InitializeComponent();

        this.Title = StudyTab.Chapter_8_Title;

        dPlat = new DevicePlatform();
        MainThread.BeginInvokeOnMainThread(() =>
        {
            dPlat = DeviceInfo.Current.Platform;
        });

        if (dPlat == DevicePlatform.Android) this.SizeChanged += OnSizeChanged;
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

        labelT.Text = StudyTab.Chapter_8_Title;
        labelT.FontSize = FSize + 3.0;

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab1.Ch8T1 + Environment.NewLine,
            FontSize = FSize + 2.0,
            FontFamily = "OpenSansBold",

        };
        fStr.Spans.Add(span);            
        span = new()
        {
            Text = StudyTab1.Ch8_I,
            //FontSize = FSize + 6.0,
            //FontFamily = "OpenSansBold"
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab1.Ch8S1,
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);
        labelS0.FormattedText = fStr;

        labelS1.FormattedText?.Spans.Clear();
        labelS1.FormattedText = FormatStringWithTitle(StudyTab1.Ch8T2, StudyTab1.Ch8S2);

        labelS2.FormattedText?.Spans.Clear();
        labelS2.FormattedText = FormatStringWithTitle(StudyTab1.Ch8T3, StudyTab1.Ch8S3);

        labelS3.FormattedText?.Spans.Clear();
        labelS3.FormattedText = FormatStringWithTitle(StudyTab1.Ch8T4, StudyTab1.Ch8S4);

        //labelS4.FormattedText?.Spans.Clear();
        //labelS4.FormattedText = FormatStringWithTitle(StudyTab1.Ch8T4, StudyTab1.Ch8S4);

        labelT1.FormattedText?.Spans.Clear();
        //if (dPlat == DevicePlatform.iOS)
        //{
        //    TapGestureRecognizer tGR1 = new();
        //    tGR1.Tapped += async (s, e) =>
        //    {
        //        await Launcher.OpenAsync("https://delta-intkey.com");
        //    };
        //    labelT1.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R1t, StudyTab1.Ch8R1riOS);

        //    labelT1c.FormattedText?.Spans.Clear();
        //    labelT1c.FormattedText = FormatString(StudyTab1.Ch8R1riOS1);
        //    labelT1c.GestureRecognizers.Clear();
        //    labelT1c.GestureRecognizers.Add(tGR1);
        //}
        //else
        //{
            labelT1.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R1t, StudyTab1.Ch8R1r);
            //labelT1c.IsVisible = false;
            iosGrid.RowDefinitions[1].Height = 0;
        //}
        labelD1.FormattedText?.Spans.Clear();
        labelD1.FormattedText = FormatString(StudyTab1.Ch8R1d);

        labelT2.FormattedText?.Spans.Clear();
        //if (dPlat == DevicePlatform.iOS)
        //if (dPlat == DevicePlatform.Unknown)
        //{
            TapGestureRecognizer tGR2 = new();
            tGR2.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://insidewood.lib.ncsu.edu");
            };
            labelT2.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R2t, StudyTab1.Ch8R2riOS);

            labelT2c.FormattedText?.Spans.Clear();
            labelT2c.FormattedText = FormatString(StudyTab1.Ch8R2riOS1);
            labelT2c.GestureRecognizers.Clear();
            labelT2c.GestureRecognizers.Add(tGR2);
        //}
        //else
        //{
        //    labelT2.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R2t, StudyTab1.Ch8R2r);
        //    labelT2c.IsVisible = false;
        //    iosGrid1.RowDefinitions[1].Height = 0;
        //}
        labelD2.FormattedText?.Spans.Clear();
        labelD2.FormattedText = FormatString(StudyTab1.Ch8R2d);

        labelT3.FormattedText?.Spans.Clear();
        //if (dPlat == DevicePlatform.iOS)
        //{
        //    TapGestureRecognizer tGR3 = new();
        //    tGR3.Tapped += async (s, e) =>
        //    {
        //        await Launcher.OpenAsync("https://www.iucnredlist.org");
        //    };
        //    labelT3.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R3t, StudyTab1.Ch8R3riOS);

        //    labelT3c.FormattedText?.Spans.Clear();
        //    labelT3c.FormattedText = FormatString(StudyTab1.Ch8R3riOS1);
        //    labelT3c.GestureRecognizers.Clear();
        //    labelT3c.GestureRecognizers.Add(tGR3);
        //}
        //else
        //{
            labelT3.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R3t, StudyTab1.Ch8R3r);
            //labelT3c.IsVisible = false;
            iosGrid2.RowDefinitions[1].Height = 0;
        //}
        labelD3.FormattedText?.Spans.Clear();
        labelD3.FormattedText = FormatString(StudyTab1.Ch8R3d);

        labelT4.FormattedText?.Spans.Clear();
        labelT4.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R4t, StudyTab1.Ch8R4r);
        labelD4.FormattedText?.Spans.Clear();
        labelD4.FormattedText = FormatString(StudyTab1.Ch8R4d);

        //labelT5.FormattedText?.Spans.Clear();
        //labelT5.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R5t, StudyTab1.Ch8R5r);
        //labelD5.FormattedText?.Spans.Clear();
        //labelD5.FormattedText = FormatString(StudyTab1.Ch8R5d);

        //labelT6.FormattedText?.Spans.Clear();
        //labelT6.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R6t, StudyTab1.Ch8R6r);
        //labelD6.FormattedText?.Spans.Clear();
        //labelD6.FormattedText = FormatString(StudyTab1.Ch8R6d);

        //labelT7.FormattedText?.Spans.Clear();
        //labelT7.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R7t, StudyTab1.Ch8R7r);
        //labelD7.FormattedText?.Spans.Clear();
        //labelD7.FormattedText = FormatString(StudyTab1.Ch8R7d);
        //labelT7r.FormattedText?.Spans.Clear();
        //labelT7r.FormattedText = FormatString(StudyTab1.Ch8R7r1);

        //labelT8.FormattedText?.Spans.Clear();
        //labelT8.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R8t, StudyTab1.Ch8R8r);
        //labelD8.FormattedText?.Spans.Clear();
        //labelD8.FormattedText = FormatString(StudyTab1.Ch8R8d);

        //labelT9.FormattedText?.Spans.Clear();
        //labelT9.FormattedText = FormatReferenceStringWithTitle(StudyTab1.Ch8R9t, StudyTab1.Ch8R9r);
        //labelD9.FormattedText?.Spans.Clear();
        //labelD9.FormattedText = FormatString(StudyTab1.Ch8R9d);

        //labelS5.FormattedText?.Spans.Clear();
        //labelS5.FormattedText = FormatString(StudyTab1.Ch8S5);


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


        if (Preferences.Get("Ch8Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch8Bookmark", 0.0), false);

        return;
    }

    private FormattedString FormatString(string inputStr)
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
                    tGR.CommandParameter = span.Text;
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

    private FormattedString FormatReferenceStringWithTitle(string title, string inputStr)
    {
        string tStr = title;
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();

        Span span = new()
        {
            Text = tStr + Environment.NewLine,
            FontSize = fSize + 2.0,
            FontFamily = "OpenSansBoldItalic"
            //FontFamily = "OpenSansBold"
        };
        fStr.Spans.Add(span);

        span = new()
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
                    tGR.CommandParameter = span.Text;
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

    private FormattedString FormatStringWithTitle(string title, string inputStr)
    {
        string tStr = title;
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();

        Span span = new()
        {
            Text = tStr + Environment.NewLine,
            FontSize = fSize + 2.0,
            FontFamily = "OpenSansBold"
        };
        fStr.Spans.Add(span);

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

    public ICommand TapCommand2 => new Command<string>(async (url) => await Launcher.OpenAsync(url));

    //public Command TapCommand1 => new Command<string>(async (url) =>
    //{
    //    Uri uri = new Uri(url);
    //    await Launcher.OpenAsync(uri);
    //});

    //public Command TapCommand => new Command<string>(async (url) =>
    //{
    //    try
    //    {
    //        Uri uri = new Uri(url);
    //        if (dPlat == DevicePlatform.iOS) await Browser.Default.OpenAsync(uri, BrowserLaunchMode.External);
    //        else await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    //    }
    //    catch (Exception ex)
    //    {
    //        Console.WriteLine(ex.ToString());
    //    }
    //});

    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("Ch8Bookmark", e.ScrollY);
    }

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}