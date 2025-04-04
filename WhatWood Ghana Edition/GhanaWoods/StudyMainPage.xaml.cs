using GhanaWoods.Resources.Languages;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;

namespace GhanaWoods;

public partial class StudyMainPage : ContentPage
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

    public StudyMainPage()
	{
		InitializeComponent();

		this.Title = StudyTab.studyFC;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        int plat = 0;
        if (DeviceInfo.Current.Platform == DevicePlatform.iOS) plat = 1;
        else plat = 0;
        int dim = 52;
        //float radiusFloat = 26;
        double radius = 26;
        RoundRectangle rRec = new RoundRectangle
        {
            CornerRadius = new CornerRadius(radius)            
        };

        FSize = Preferences.Get("FontSize", 16.0);

        //labelP.Text = StudyTab.Preface;
        //labelP.FontSize = FSize + 3.0;
        //TapGestureRecognizer tGR = new();
        //tGR.Tapped += (s, e) =>
        //{
        //    HapticFeedback.Default.Perform(HapticFeedbackType.Click);
        //    Shell.Current.GoToAsync("//Study/StudyPreface", false);
        //};
        //frameP.GestureRecognizers.Clear();
        //imageP.GestureRecognizers.Clear();
        //labelP.GestureRecognizers.Clear();
        //frameP.GestureRecognizers.Add(tGR);
        //imageP.GestureRecognizers.Add(tGR);
        //labelP.GestureRecognizers.Add(tGR);

        labelA.Text = StudyTab.Background_Title;
        //labelA.FontSize = FSize + 3.0;
        labelA.FontSize = FSize + 3.0;
        TapGestureRecognizer tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyBackground", false);
        };
        frameA.GestureRecognizers.Clear();
        imageA.GestureRecognizers.Clear();
        labelA.GestureRecognizers.Clear();
        frameA.GestureRecognizers.Add(tGR);
        imageA.GestureRecognizers.Add(tGR);
        labelA.GestureRecognizers.Add(tGR);


        label1.FormattedText?.Spans.Clear();
        FormattedString fStr = new();
        Span span = new()
        {
            Text = StudyTab.Chapter_1 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_1_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label1.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter1", false);
        };
        frame1.GestureRecognizers.Clear();
        image1.GestureRecognizers.Clear();
        label1.GestureRecognizers.Clear();
        frame1.GestureRecognizers.Add(tGR);
        image1.GestureRecognizers.Add(tGR);
        label1.GestureRecognizers.Add(tGR);


        label2.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_2 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_2_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label2.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter2", false);
        };
        frame2.GestureRecognizers.Clear();
        image2.GestureRecognizers.Clear();
        label2.GestureRecognizers.Clear();
        frame2.GestureRecognizers.Add(tGR);
        image2.GestureRecognizers.Add(tGR);
        label2.GestureRecognizers.Add(tGR);
        //double cRadius = image2.Width / 2.0;
        ////frameI2.CornerRadius = (float)cRadius;
        //frameI2.StrokeShape = new RoundRectangle
        //{
        //    CornerRadius = new CornerRadius(cRadius)
        //};


        label3.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_3 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_3_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label3.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter3", false);
        }; 
        frame3.GestureRecognizers.Clear();
        image3.GestureRecognizers.Clear();
        label3.GestureRecognizers.Clear();
        frame3.GestureRecognizers.Add(tGR);
        image3.GestureRecognizers.Add(tGR);
        label3.GestureRecognizers.Add(tGR);


        label4.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_4 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_4_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label4.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter4", false);
        };
        frame4.GestureRecognizers.Clear();
        image4.GestureRecognizers.Clear();
        label4.GestureRecognizers.Clear();
        frame4.GestureRecognizers.Add(tGR);
        image4.GestureRecognizers.Add(tGR);
        label4.GestureRecognizers.Add(tGR);


        label5.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_5 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_5_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label5.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter5", false);
        };
        frame5.GestureRecognizers.Clear();
        image5.GestureRecognizers.Clear();
        label5.GestureRecognizers.Clear();
        frame5.GestureRecognizers.Add(tGR);
        image5.GestureRecognizers.Add(tGR);
        label5.GestureRecognizers.Add(tGR);



        label6.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_6 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_6_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label6.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter6", false);
        };
        frame6.GestureRecognizers.Clear();
        image6.GestureRecognizers.Clear();
        label6.GestureRecognizers.Clear();
        frame6.GestureRecognizers.Add(tGR);
        image6.GestureRecognizers.Add(tGR);
        label6.GestureRecognizers.Add(tGR);



        label7.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_7 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_7_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label7.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter7", false);
        };
        frame7.GestureRecognizers.Clear();
        image7.GestureRecognizers.Clear();
        label7.GestureRecognizers.Clear();
        frame7.GestureRecognizers.Add(tGR);
        image7.GestureRecognizers.Add(tGR);
        label7.GestureRecognizers.Add(tGR);



        label8.FormattedText?.Spans.Clear();
        fStr = new();
        span = new()
        {
            Text = StudyTab.Chapter_8 + "\n",
            FontFamily = "OpenSansBold",
            FontSize = FSize + 3.0
        };
        fStr.Spans.Add(span);
        span = new()
        {
            Text = StudyTab.Chapter_8_Title,
            FontSize = FSize
        };
        fStr.Spans.Add(span);
        label8.FormattedText = fStr;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyChapter8", false);
        };
        frame8.GestureRecognizers.Clear();
        image8.GestureRecognizers.Clear();
        label8.GestureRecognizers.Clear();
        frame8.GestureRecognizers.Add(tGR);
        image8.GestureRecognizers.Add(tGR);
        label8.GestureRecognizers.Add(tGR);


        labelI.Text = StudyTab.Index;
        labelI.FontSize = FSize + 3.0;
        tGR = new();
        tGR.Tapped += (s, e) =>
        {
            HapticFeedback.Default.Perform(HapticFeedbackType.Click);
            Shell.Current.GoToAsync("//Study/StudyIndex", false);
        };
        frameI.GestureRecognizers.Clear();
        imageI.GestureRecognizers.Clear();
        labelI.GestureRecognizers.Clear();
        frameI.GestureRecognizers.Add(tGR);
        imageI.GestureRecognizers.Add(tGR);
        labelI.GestureRecognizers.Add(tGR);


        if (plat == 1)
        {
            //frameIP.CornerRadius = radius;
            //frameIP.StrokeShape = rRec;
            //frameIP.HeightRequest = dim;
            //frameIP.WidthRequest = dim;

            //frameIA.CornerRadius = radius;
            frameIA.StrokeShape = rRec;
            frameIA.HeightRequest = dim;
            frameIA.WidthRequest = dim;

            //frameI1.CornerRadius = radius;
            frameI1.StrokeShape = rRec;
            frameI1.HeightRequest = dim;
            frameI1.WidthRequest = dim;

            //frameI2.CornerRadius = radius;
            frameI2.StrokeShape = rRec;
            frameI2.HeightRequest = dim;
            frameI2.WidthRequest = dim;

            //frameI3.CornerRadius = radius;
            frameI3.StrokeShape = rRec;
            frameI3.HeightRequest = dim;
            frameI3.WidthRequest = dim;

            //frameI4.CornerRadius = radius;
            frameI4.StrokeShape = rRec;
            frameI4.HeightRequest = dim;
            frameI4.WidthRequest = dim;

            //frameI5.CornerRadius = radius;
            frameI5.StrokeShape = rRec;
            frameI5.HeightRequest = dim;
            frameI5.WidthRequest = dim;

            //frameI6.CornerRadius = radius;
            frameI6.StrokeShape = rRec;
            frameI6.HeightRequest = dim;
            frameI6.WidthRequest = dim;

            //frameI7.CornerRadius = radius;
            frameI7.StrokeShape = rRec;
            frameI7.HeightRequest = dim;
            frameI7.WidthRequest = dim;

            //frameI8.CornerRadius = radius;
            frameI8.StrokeShape = rRec;
            frameI8.HeightRequest = dim;
            frameI8.WidthRequest = dim;

            //frameII.CornerRadius = radius;
            frameII.StrokeShape = rRec;
            frameII.HeightRequest = dim;
            frameII.WidthRequest = dim;
        }

        return;
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        return true;
    }
}