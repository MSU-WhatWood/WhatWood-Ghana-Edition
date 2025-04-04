using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.Controls;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyChapter7 : ContentPage
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

    public StudyChapter7()
	{
		InitializeComponent();

        this.Title = StudyTab.Chapter_7_Title;

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

        labelT.FormattedText?.Spans.Clear();
        if (dPlat == DevicePlatform.iOS)
        {
            labelT.FormattedText = FormatReferenceTitle(StudyTab.Chapter_7_Title_iOS);

            TapGestureRecognizer tGR = new();
            tGR.Tapped += async (s, e) =>
            {
                await Shell.Current.GoToAsync("//Reference", false);
            };
            labelTc.FormattedText?.Spans.Clear();
            labelTc.FormattedText = FormatReferenceTitle(StudyTab.Chapter_7_Title_iOS1);
            labelTc.GestureRecognizers.Clear();
            labelTc.GestureRecognizers.Add(tGR);
        }
        else
        {
            labelT.FormattedText = FormatReferenceTitle(StudyTab.Chapter_7_Title_Hyperlink);
            labelTc.IsVisible = false;
            iosGrid.RowDefinitions[1].Height = 0;
        }

        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.Ch7T0 + Environment.NewLine,
            FontSize = FSize + 2.0,
            FontFamily = "OpenSansBold"
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_7_T,
            //FontSize = FSize + 6.0,
            //FontFamily = "OpenSansBold"
            FontSize = FSize,
            FontFamily = "OpenSansRegular"
        };
        fStr.Spans.Add(span);

        if (dPlat == DevicePlatform.iOS)
        {
            FormattedString fStr1 = FormatReferenceString(StudyTab.Ch7S0iOS);
            for (int i = 0; i < fStr1.Spans.Count; i++) fStr.Spans.Add(fStr1.Spans[i]);
            labelS0.FormattedText = fStr;

            TapGestureRecognizer tGR1 = new();
            tGR1.Tapped += async (s, e) =>
            {
                await Shell.Current.GoToAsync("//ID", false);
            };
            labelS0c.FormattedText?.Spans.Clear();
            labelS0c.FormattedText = FormatReferenceString(StudyTab.Ch7S0iOS1);
            labelS0c.GestureRecognizers.Clear();
            labelS0c.GestureRecognizers.Add(tGR1);

            labelS0c1.FormattedText?.Spans.Clear();
            labelS0c1.FormattedText = FormatReferenceString(StudyTab.Ch7S0iOS2);
        }
        else
        {
            FormattedString fStr1 = FormatReferenceString(StudyTab.Ch7S0);
            for (int i = 0; i < fStr1.Spans.Count; i++) fStr.Spans.Add(fStr1.Spans[i]);
            labelS0.FormattedText = fStr;

            labelS0c.IsVisible = false;
            labelS0c1.IsVisible = false;
            iosGrid.RowDefinitions[1].Height = 0;
            iosGrid.RowDefinitions[2].Height = 0;
        }


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


        if (Preferences.Get("Ch7Bookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("Ch7Bookmark", 0.0), false);

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
                    span.GestureRecognizers.Add(tGR);
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
        Preferences.Set("Ch7Bookmark", e.ScrollY);
    }

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}