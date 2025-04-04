using GhanaWoods.Resources.Languages;
using GhanaWoods.Resources.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Animations;
using System.Globalization;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls.Shapes;
using GhanaWoods.Database;

namespace GhanaWoods;

public partial class StudyIndex : ContentPage
{
    private double fSize = Preferences.Get("FontSize", 16.0);
    bool isCreated = false;
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

    public StudyIndex()
	{
		InitializeComponent();

        this.Title = StudyTab.Index;

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

        labelT.Text = StudyTab.Index;
        labelT.FontSize = FSize + 3.0;

        if (!isCreated)
        {
            PopulateVSL();
            isCreated = true;
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


        if (Preferences.Get("IndexBookmark", 0.0) > 0.0) await SV.ScrollToAsync(0.0, Preferences.Get("IndexBookmark", 0.0), false);

        isCreated = true;
        return;
    }

    private void PopulateVSL()
    {
        vslS0.Children.Clear();

        int currentLang = 0;
        //if (CultureInfo.CurrentCulture.Name.Contains("en")) currentLang = 0;
        //else if (CultureInfo.CurrentCulture.Name.Contains("es")) currentLang = 1;
        currentLang = 0;

        AddIndexEntry(StudyTab1.Local_Name, StudyTab1.Trade_Names, StudyTab1.Scientific_Names);
        //if (currentLang == 0)
        //{
            //using var stream = await FileSystem.OpenAppPackageFileAsync("StudyIndexData.txt");
            //using var reader = new StreamReader(stream);

            //string line = string.Empty;
            //while ((line = reader.ReadLine()) != null)
            //{
            //    List<string> strs = new List<string>();
            //    strs = line.Split("+  ").ToList();

            //    AddIndexEntry(strs[0], strs[1], strs[2]);
            //}

        List<SpeciesTbl> speciesTbls = new List<SpeciesTbl>();
        //speciesTbls = App.DB.GetSpeciesList();
        speciesTbls = App.DB.GetSpeciesListByLocalName();

        for (int i = 0; i < speciesTbls.Count; i++) 
        {
            //if (!string.IsNullOrEmpty(speciesTbls[i].LocalName) && !string.IsNullOrEmpty(speciesTbls[i].TradeName))
            //{
                AddIndexEntry(speciesTbls[i].LocalName, speciesTbls[i].TradeName, speciesTbls[i].Name, speciesTbls[i].SpeciesGroupID);
            //}
        }

        //}
        //else
        //{
        //    using var stream = await FileSystem.OpenAppPackageFileAsync("StudyIndexDataES.txt");
        //    using var reader = new StreamReader(stream);

        //    string line = string.Empty;
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        List<string> strs = new List<string>();
        //        strs = line.Split("+  ").ToList();

        //        AddIndexEntry(strs[0], strs[1], strs[2]);
        //    }
        //}
    }

    private void AddIndexEntry(string Term, string Chapter, string Figure, int sGID = 0)
    {
        string term = Term;
        string chap = Chapter;
        string fig = Figure;

        Grid grid = new Grid()
        {
            ColumnDefinitions =
            {
                new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                new ColumnDefinition{ Width = new GridLength(2, GridUnitType.Star)},
                new ColumnDefinition{ Width = new GridLength(3, GridUnitType.Star)}
            },
            RowDefinitions =
            {
                new RowDefinition{ Height = new GridLength(1, GridUnitType.Star)}
            }
        };
        grid.ColumnSpacing = 3;

        Label labelTerm = new()
        {
            FontSize = FSize,
            FontFamily = "OpenSansRegular",
            HorizontalTextAlignment = TextAlignment.Start
        };
        if (term != StudyTab1.Local_Name)
        {
            //if (term[0] == '!')
            //{
            //    labelTerm.Text = term.Substring(1, term.Length - 1);
            //    grid.Margin = new Thickness(0, 20, 0, 0);
            //}
            //else labelTerm.Text = term;
            labelTerm.Text = term;

            //if (!string.IsNullOrEmpty(chap) && chap != " ")
            //{
                labelTerm.TextDecorations = TextDecorations.Underline;
                labelTerm.TextColor = Colors.Blue;
            //}
        }
        else
        {
            labelTerm.Text = term;
            labelTerm.FontFamily = "OpenSansBold";
            grid.Margin = new Thickness(0, 0, 0, 5);
        }

        Label labelChap = new()
        {
            Text = chap,
            FontSize = FSize,
            FontFamily = "OpenSansRegular",
            HorizontalTextAlignment = TextAlignment.Start
        };
        if (chap == StudyTab1.Trade_Names) labelChap.FontFamily = "OpenSansBold";
        else if (chap != " ")
        {
            labelChap.TextDecorations = TextDecorations.Underline;
            labelChap.TextColor = Colors.Blue;
        }
        Label labelFig = new()
        {
            //Text = fig,
            //FormattedText = FormatSpeciesName(fig),
            FontSize = FSize,
            //FontFamily = "OpenSansRegular",
            HorizontalTextAlignment = TextAlignment.Start
        };
        if (fig == StudyTab1.Scientific_Names)
        {
            labelFig.FontFamily = "OpenSansBold";
            labelFig.Text = fig;
        }
        else labelFig.FormattedText = FormatSpeciesName(fig);

        grid.Add(labelTerm, 0, 0);
        grid.Add(labelChap, 1, 0);
        grid.Add(labelFig, 2, 0);

        int sGroupID = sGID;
        if (sGroupID > 0)
        {
            TapGestureRecognizer tGR = new();
            tGR.Tapped += async (s, e) =>
            {
                await Shell.Current.Navigation.PushAsync(new ReferenceGroupPageNew(App.DB.GetSpeciesGroupByID(sGroupID), 2));    
            };
            labelTerm.GestureRecognizers.Add(tGR);
            if (chap != " ") labelChap.GestureRecognizers.Add(tGR);
            labelFig.GestureRecognizers.Add(tGR);
        }

        //if (!string.IsNullOrEmpty(chap) && chap != " " && chap != StudyTab1.Trade_Names)
        //{
        //    TapGestureRecognizer tGR = new TapGestureRecognizer();
        //    int chapNum = int.Parse(chap);
        //    if (chapNum == 2) tGR.Tapped += async (s, e) => { await Shell.Current.Navigation.PushAsync(new StudyChapter2(1), false); };
        //    else if (chapNum == 5) tGR.Tapped += async (s, e) => { await Shell.Current.Navigation.PushAsync(new StudyChapter5(1), false); };
        //    else if (chapNum == 6) tGR.Tapped += async (s, e) => { await Shell.Current.Navigation.PushAsync(new StudyChapter6(1), false); };
        //    else tGR.Tapped += async (s, e) => { await Shell.Current.GoToAsync("//Study/StudyChapter" + chap, false); };
        //    grid.GestureRecognizers.Add(tGR);
        //}

        vslS0.Children.Add(grid);

        return;
    }

    private FormattedString FormatSpeciesName(string inputStr)
    {
        string str = inputStr;
        double fSize = Preferences.Get("FontSize", 16.0);
        FormattedString fStr = new FormattedString();
        Span span = new()
        {
            FontSize = fSize,
            TextDecorations = TextDecorations.Underline,
            TextColor = Colors.Blue
        };
        //if (Application.Current != null)
        //{
        //    if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.White;
        //    else span.TextColor = Colors.Black;
        //}
        //span.TextColor = Colors.Black;

        if (str[0] == '[') span.FontFamily = "OpenSansItalic";
        else
        {
            span.FontFamily = "OpenSansRegular";
            span.Text += str[0];
        }

        for (int i = 1; i < str.Length; i++)
        {
            if (str[i] == '[')
            {
                fStr.Spans.Add(span);
                span = new()
                {
                    FontSize = fSize,
                    FontFamily = "OpenSansItalic",
                    TextDecorations = TextDecorations.Underline,
                    TextColor = Colors.Blue
                };
                //if (Application.Current != null)
                //{
                //    if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.White;
                //    else span.TextColor = Colors.Black;
                //}
                //span.TextColor = Colors.Black;
            }
            else if (str[i] == ']')
            {
                fStr.Spans.Add(span);
                span = new()
                {
                    FontSize = fSize,
                    FontFamily = "OpenSansRegular",
                    TextDecorations = TextDecorations.Underline,
                    TextColor = Colors.Blue
                };
                //if (Application.Current != null)
                //{
                //    if (Application.Current.RequestedTheme == AppTheme.Dark) span.TextColor = Colors.White;
                //    else span.TextColor = Colors.Black;
                //}
                //span.TextColor = Colors.Black;
            }
            else
            {
                span.Text += str[i];
            }

            //if (str[i] == ' ' && str[i + 1] == '(')
            //{
            //    if (str[i - 1] != ']') fStr.Spans.Add(span);

            //    return fStr;
            //}
        }
        if (span.Text != null || span.Text != string.Empty || span.Text != "") fStr.Spans.Add(span);

        return fStr;
    }

    void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
    {
        Preferences.Set("IndexBookmark", e.ScrollY);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        Preferences.Set("IndexBookmark", SV.ScrollY);
    }

    public Command BackCommand => new Command(async () => await Shell.Current.Navigation.PopToRootAsync(false));

    protected override bool OnBackButtonPressed()
    {
        // Return true to prevent back button 
        Shell.Current.Navigation.PopToRootAsync(false);
        return true;
    }
}