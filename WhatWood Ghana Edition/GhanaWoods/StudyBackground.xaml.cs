using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;

namespace GhanaWoods;

public partial class StudyBackground : ContentPage
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

    public StudyBackground()
	{
		InitializeComponent();

        this.Title = StudyTab.Background_Title;

        DevicePlatform dPlat = new DevicePlatform();
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

        labelT.Text = StudyTab.Background_Title;
        labelT.FontSize = FSize + 3.0;

        //labelS0.FormattedText?.Spans.Clear();
        //FormattedString fStr = new();
        //Span span = new()
        //{
        //    Text = StudyTab.Acknowledgements_F,
        //    FontSize = FSize + 6.0,
        //    FontFamily = "OpenSansBold"
        //};
        //fStr.Spans.Add(span);
        //span = new()
        //{
        //    Text = StudyTab.AckS0,
        //    FontSize = FSize,
        //    FontFamily = "OpenSansRegular"
        //};
        //fStr.Spans.Add(span);
        //labelS0.FormattedText = fStr;
        labelS0.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        List<Span> spans = FormatStringWithTitle(StudyTab.Abstract, StudyTab.Abs0);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS0.FormattedText = fStr;

        labelS1.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Background, StudyTab.Bac0);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS1.FormattedText = fStr;

        labelS2.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Objective, StudyTab.Obj0);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS2.FormattedText = fStr;

        labelS3.FormattedText?.Spans.Clear();
        fStr = new();
        spans = FormatStringWithTitle(StudyTab.Acknowledgments, StudyTab.Ack0);
        for (int i = 0; i < spans.Count; i++) fStr.Spans.Add(spans[i]);
        labelS3.FormattedText = fStr;


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


        if (Preferences.Get("AckBookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("AckBookmark", 0.0), false);

        return;
    }

    private List<Span> FormatString(string inputStr)
    {
        string str = inputStr;
        //bool italics = false;
        double fSize = Preferences.Get("FontSize", 16.0);
        //FormattedString fStr = new FormattedString();
        List<Span> spans = new List<Span>();
        Span span = new()
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

    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("AckBookmark", e.ScrollY);
    }

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}