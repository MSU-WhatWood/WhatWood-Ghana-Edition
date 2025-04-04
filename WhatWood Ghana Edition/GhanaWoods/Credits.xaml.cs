using GhanaWoods.Resources.Languages;
using System;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace GhanaWoods;

public partial class Credits : ContentPage
{
    private double _fSize = Preferences.Get("FontSize", 16.0);
    public double FSize
    {
        get { return _fSize; }
        set
        {
            if (value == _fSize) return;

            _fSize = value;
            OnPropertyChanged();
        }
    }
    DevicePlatform dPlat = new();
    TapGestureRecognizer zTGR = new();

    public Credits()
	{
		InitializeComponent();

        this.BindingContext = this;

        FSize = Preferences.Get("FontSize", 16.0);

        this.Title = SettingsTab.Credits;

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
            if (s1 != null)
            {
                if (s1.Source != null)
                {
                    await AppShell.Current.Navigation.PushAsync(new ImageZoomPage(s1.Source), false);
                }
            }
        };
        fplImage.GestureRecognizers.Add(zTGR);
        msuImage.GestureRecognizers.Add(zTGR);
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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        FSize = Preferences.Get("FontSize", 16.0);

        creditsTitle.FontSize = FSize + 3.0;
        creditsTitle.TextColor = Colors.White;
        creditsTitle.Text = SettingsTab.Credits;

        creditsLabel.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        fStr = FormatReferenceString(SettingsTab.Credits_description);
        creditsLabel.FormattedText = fStr;

        creditsLabel1.FormattedText?.Spans.Clear();
        fStr = new();
        //if (dPlat == DevicePlatform.iOS)
        ////if (1 == 1)
        //{
            fStr = FormatReferenceString(SettingsTab.Credits_description_iOS);
            creditsLabel1.FormattedText = fStr;
            creditsLabel2.FormattedText?.Spans.Clear();
            fStr = new();
            fStr = FormatReferenceString(SettingsTab.Credits_description_iOS1);
            creditsLabel2.FormattedText = fStr;
            creditsLabel2.GestureRecognizers.Clear();
            TapGestureRecognizer tGR = new();
            tGR.Tapped += async (s, e) =>
            {
                //await Launcher.OpenAsync("https://research.fs.usda.gov/treesearch/64547");
                await Launcher.OpenAsync("https://research.fs.usda.gov/treesearch/60264");
            };
            creditsLabel2.GestureRecognizers.Add(tGR);
            creditsLabel3.FormattedText?.Spans.Clear();
            fStr = new();
            fStr = FormatReferenceString(SettingsTab.Credits_description_iOS2);
            creditsLabel3.FormattedText = fStr;
            //creditsLabel4.FormattedText?.Spans.Clear();
            //fStr = new();
            //fStr = FormatReferenceString(SettingsTab.Credits_description_iOS3);
            //creditsLabel4.FormattedText = fStr;
            //creditsLabel4.GestureRecognizers.Clear();
            //tGR = new();
            //tGR.Tapped += async (s, e) =>
            //{
            //    await Launcher.OpenAsync("https://commons.wikimedia.org/wiki/File:Caribbean_Sea_and_West_Indies.png");
            //};
            //creditsLabel4.GestureRecognizers.Add(tGR);
            //creditsLabel5.FormattedText?.Spans.Clear();
            //fStr = new();
            //fStr = FormatReferenceString(SettingsTab.Credits_description_iOS4);
            //creditsLabel5.FormattedText = fStr;
        //}
        //else
        //{
        //    fStr = FormatReferenceString(SettingsTab.Credits_description_cont);
        //    creditsLabel1.FormattedText = fStr;
        //    creditsLabel2.IsVisible = false;
        //    creditsLabel3.IsVisible = false;
        //    //creditsLabel4.IsVisible = false;
        //    //creditsLabel5.IsVisible = false;
        //    creditsGrid.RowDefinitions[3].Height = 0;
        //    creditsGrid.RowDefinitions[4].Height = 0;
        //    //creditsGrid.RowDefinitions[5].Height = 0;
        //    //creditsGrid.RowDefinitions[6].Height = 0;
        //}

        disclaimerLabel.TextColor = Colors.White;
        disclaimerLabel.Text = SettingsTab.Credits_disclaimer;

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

    private FormattedString FormatReferenceString(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();

        Span span = new()
        {
            FontSize = fSize,
            FontFamily = "OpenSansRegular",
            TextColor = Colors.White
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
                span.TextColor = Colors.LightBlue;
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
                        FontFamily = "OpenSansBold",
                        TextColor = Colors.White
                    };
                }
                else if (str[i] == '{')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansBoldItalic",
                        TextColor = Colors.White
                    };
                }
                else if (str[i] == '[')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansItalic",
                        TextColor = Colors.White
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
                    span.TextColor = Colors.LightBlue;
                }
                else if (str[i] == '|')
                {
                    TapGestureRecognizer tGR = new();
                    //if (span.Text.Contains("doi.org")) tGR.CommandParameter = "https://research.fs.usda.gov/treesearch/64547";
                    //else tGR.CommandParameter = span.Text;
                    if (span.Text.Contains("fpl.fs")) tGR.CommandParameter = "https://research.fs.usda.gov/treesearch/60264";
                    else tGR.CommandParameter = span.Text;
                    tGR.Command = TapCommand2;

                    //if (dPlat != DevicePlatform.iOS) span.GestureRecognizers.Add(tGR);
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular",
                        TextColor = Colors.White
                    };
                }
                else if (str[i] == ']' || str[i] == '}' || str[i] == '>')
                {
                    fStr.Spans.Add(span);
                    span = new()
                    {
                        FontSize = fSize,
                        FontFamily = "OpenSansRegular",
                        TextColor = Colors.White
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

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}